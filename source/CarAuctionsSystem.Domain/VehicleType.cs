using System.ComponentModel;

namespace CarAuctionsSystem.Domain;

public enum VehicleType
{
    [Description("Hatchback")]
    Hatchback,

    [Description("Sedan")]
    Sedan,

    [Description("SUV")]
    SUV,

    [Description("Truck")]
    Truck
}
