/*
 * Copyright (c) Akveo 2019. All Rights Reserved.
 * Licensed under the Single Application / Multi Application License.
 * See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the 'docs' folder for license information on type of purchased license.
 */

import { Observable } from 'rxjs';
import { Device, DeviceParameter } from './devices';

export abstract class TemperatureHumidityData {
  abstract getTemperatureDevice(): Observable<Device>;
  abstract getHumidityDevice(): Observable<Device>;

  abstract setTemperatureDevice(device: Device): Observable<Device>;
  abstract setHumidityDevice(device: Device): Observable<Device>;

  abstract setTemperatureParameter(parameter: DeviceParameter): Observable<DeviceParameter>;
  abstract setHumidityParameter(parameter: DeviceParameter): Observable<DeviceParameter>;
}
