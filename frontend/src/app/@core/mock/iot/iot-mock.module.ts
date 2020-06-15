/*
 * Copyright (c) Akveo 2019. All Rights Reserved.
 * Licensed under the Single Application / Multi Application License.
 * See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the 'docs' folder for license information on type of purchased license.
 */

import { ModuleWithProviders, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CommonMockModule } from '../common/common-mock.module';

import { DevicesData } from '../../interfaces/iot/devices';
import { PhoneData } from '../../interfaces/iot/phone';
import { ElectricityData } from '../../interfaces/iot/electricity';
import { SolarData } from '../../interfaces/iot/solar';
import { TemperatureHumidityData } from '../../interfaces/iot/temperature-humidity';
import { TrafficChartData } from '../../interfaces/iot/traffic-chart';
import { SecurityCamerasData } from '../../interfaces/iot/security-cameras';


import { DevicesService } from './devices.service';
import { ElectricityService } from './electricity.service';
import { PhoneService } from './phone.service';
import { SolarService } from './solar.service';
import { TemperatureHumidityService } from './temperature-humidity.service';
import { TrafficChartService } from './traffic-chart.service';
import { SecurityCamerasService } from './security-cameras.service';

const SERVICES = [
  { provide: DevicesData, useClass: DevicesService },
  { provide: ElectricityData, useClass: ElectricityService },
  { provide: PhoneData, useClass: PhoneService },
  { provide: SolarData, useClass: SolarService },
  { provide: TemperatureHumidityData, useClass: TemperatureHumidityService },
  { provide: TrafficChartData, useClass: TrafficChartService },
  { provide: SecurityCamerasData, useClass: SecurityCamerasService },
];

@NgModule({
  imports: [CommonModule, CommonMockModule],
  exports: [CommonMockModule],
})
export class IotMockModule {
  static forRoot(): ModuleWithProviders<IotMockModule> {
    return {
      ngModule: IotMockModule,
      providers: [
        ...SERVICES,
      ],
    };
  }
}
