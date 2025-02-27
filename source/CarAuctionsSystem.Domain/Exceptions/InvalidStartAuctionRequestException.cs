namespace CarAuctionsSystem.Domain.Exceptions;

public class InvalidStartAuctionRequestException(string message) : Exception
{
    public int StatusCode { get; set; } = 400;
    public new object? Message { get; set; } = message;
}
