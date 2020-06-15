/*
 * Copyright (c) Akveo 2019. All Rights Reserved.
 * Licensed under the Single Application / Multi Application License.
 * See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the 'docs' folder for license information on type of purchased license.
 */

import { Injectable } from '@angular/core';
import { of as observableOf,  Observable } from 'rxjs';
import { TemperatureHumidityData } from '../../interfaces/iot/temperature-humidity';
import { Device, DeviceParameter } from '../../interfaces/iot/devices';

@Injectable()
export class TemperatureHumidityService extends TemperatureHumidityData {
  private temperatureDevice: Device = {
    id: 5,
    name: 'Air Conditioner',
    isOn: true,
    type: 'Conditioner',
    parameters: [
      {
        id: 1,
        name: 'Temperature',
        value: 24,
        min: 12,
        max: 30,
      },
      {
        id: 2,
        name: 'Mode',
        value: 'Cooling',
      },
    ],
  };

  private humidityDevice: Device = {
    id: 6,
    name: 'Humidifier',
    isOn: true,
    type: 'Humidifier',
    parameters: [
      {
        id: 2,
        name: 'Humidity',
        value: 87,
        min: 0,
        max: 100,
      },
      {
        id: 4,
        name: 'Mode',
        value: 'Circulation',
      },
    ],
  };

  getTemperatureDevice(): Observable<Device> {
    return observableOf(this.temperatureDevice);
  }

  getHumidityDevice(): Observable<Device> {
    return observableOf(this.humidityDevice);
  }

  setTemperatureDevice(device: Device): Observable<Device> {
    return observableOf(device);
  }
  setHumidityDevice(device: Device): Observable<Device> {
    return observableOf(device);
  }

  setTemperatureParameter(parameter: DeviceParameter): Observable<DeviceParameter> {
    return observableOf(parameter);
  }

  setHumidityParameter(parameter: DeviceParameter): Observable<DeviceParameter> {
    return observableOf(parameter);
  }
}
