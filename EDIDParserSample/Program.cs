using System;
using System.Collections.Generic;
using System.Linq;
using WindowsDisplayAPI;
using EDIDParser;
using EDIDParserSample.Properties;
using NvAPIWrapper.GPU;
using NvAPIWrapper.Native.GPU;

namespace EDIDParserSample
{
    internal class Program
    {
        private static void BrowseEDID(byte[] bytes, string title)
        {
            var edid = new EDID(bytes);
            ConsoleNavigation.PrintNavigation(new Dictionary<object, Action>
            {
                {
                    "View General Information",
                    () => { ConsoleNavigation.PrintObject(edid, "EDID"); }
                },
                {
                    "View Display Parameters",
                    () => { ConsoleNavigation.PrintObject(edid.DisplayParameters, "EDID.DisplayParameters"); }
                },
                {
                    "View Display Chromaticity Coordinates",
                    () =>
                    {
                        ConsoleNavigation.PrintObject(edid.DisplayParameters.ChromaticityCoordinates,
                            "EDID.DisplayParameters.ChromaticityCoordinates");
                    }
                },
                {
                    "View Descriptors",
                    () => { ConsoleNavigation.PrintObject(edid.Descriptors.ToArray(), "EDID.Descriptors"); }
                },
                {
                    "View Extensions",
                    () => { ConsoleNavigation.PrintObject(edid.Extensions.ToArray(), "EDID.Extensions"); }
                },
                {
                    "View Timings",
                    () => { ConsoleNavigation.PrintObject(edid.Timings.ToArray(), "EDID.Timings"); }
                }
            }, title, "Select an option to browse the EDID information.");
        }

        private static void Main()
        {
            var paths = new Dictionary<object, Action>
            {
                {
                    "View Embedded EDID Sample",
                    () => { BrowseEDID(Resources.EDID, "Embedded EDID Sample"); }
                },
                {
                    "Read From Display Using NVIDIA GPU",
                    () =>
                    {
                        ConsoleNavigation.PrintObject(
                            PhysicalGPU.GetPhysicalGPUs()
                                .SelectMany(gpu => gpu.GetConnectedDisplayDevices(ConnectedIdsFlag.None))
                                .ToArray(),
                            display =>
                            {
                                var edidData = display.PhysicalGPU.ReadEDIDData(display.Output);
                                BrowseEDID(edidData,
                                    $"DisplayDevice #{display.DisplayId} @ {display.PhysicalGPU.FullName} EDID Data");
                            }, "NVIDIA Displays", "Select a Display to parse EDID data.");
                    }
                },
                {
                    "Read From Windows Registry",
                    () =>
                    {
                        ConsoleNavigation.PrintObject(Display.GetDisplays().ToArray(), display =>
                        {
                            byte[] edidData;
                            using (var key = display.OpenDevicePnPKey())
                            {
                                using (var subkey = key.OpenSubKey("Device Parameters"))
                                {
                                    // ReSharper disable once PossibleNullReferenceException
                                    edidData = (byte[]) subkey.GetValue("EDID", null);
                                }
                            }
                            BrowseEDID(edidData, $"{display.DisplayName} @ {display.Adapter.DeviceName} EDID Data");
                        }, "Windows Displays", "Select a Display to parse EDID data.");
                    }
                }
            };
            ConsoleNavigation.PrintNavigation(paths, "EDID Parser Sample",
                "Select an option to explore EDID parser functionalities.");
        }
    }
}