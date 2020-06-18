﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Warehouse.Services
{
    public interface ITemperatureService
    {
        public double GetTemperatureCelsius();
        public double GetTemperatureFahreinheit();
    }
}
