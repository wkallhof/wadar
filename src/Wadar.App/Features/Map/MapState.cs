namespace Wadar.App.Features.Map
{
    public record MapState
    {
        public decimal Longitude {get; set;}
        public decimal Latitude {get; set;}
        public int Zoom {get; set;}
    }
}