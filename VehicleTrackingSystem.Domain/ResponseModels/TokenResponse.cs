using System;

namespace VehicleTrackingSystem.Application.ResponseModels
{
    public class TokenResponse
    {
        public string Token { get; set; }
        public DateTime Validity { get; set; }

    }
}
