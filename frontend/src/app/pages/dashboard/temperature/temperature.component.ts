/*
 * Copyright (c) Akveo 2019. All Rights Reserved.
 * Licensed under the Single Application / Multi Application License.
 * See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the 'docs' folder for license information on type of purchased license.
 */

import { Component, OnDestroy } from '@angular/core';
import { NbThemeService } from '@nebular/theme';
import { TemperatureHumidityData } from '../../../@core/interfaces/iot/temperature-humidity';
import { DeviceParameter, Device } from '../../../@core/interfaces/iot/devices';
import { takeWhile } from 'rxjs/operators';
import { forkJoin } from 'rxjs';

@Component({
  selector: 'ngx-temperature',
  styleUrls: ['./temperature.component.scss'],
  templateUrl: './temperature.component.html',
})
export class TemperatureComponent implements OnDestroy {

  private alive = true;

  temperatureDevice: Device;
  humidityDevice: Device;

  temperatureData: DeviceParameter;
  temperatureMode: DeviceParameter;

  humidityData: DeviceParameter;
  humidityMode: DeviceParameter;

  theme: any;
  themeSubscription: any;

  constructor(private themeService: NbThemeService,
              private temperatureHumidityService: TemperatureHumidityData) {
    this.themeService.getJsTheme()
      .pipe(takeWhile(() => this.alive))
      .subscribe(config => {
      this.theme = config.variables.temperature;
    });

    forkJoin(
      this.temperatureHumidityService.getTemperatureDevice(),
      this.temperatureHumidityService.getHumidityDevice(),
    )
      .subscribe(([temperatureData, humidityData]: [Device, Device]) => {
        this.temperatureDevice = temperatureData;
        this.humidityDevice = humidityData;

        this.temperatureData = this.temperatureDevice.parameters.find(p => p.name === 'Temperature');
        this.humidityData = this.humidityDevice.parameters.find(p => p.name === 'Humidity');

        this.temperatureMode = this.temperatureDevice.parameters.find(p => p.name === 'Mode');
        this.humidityMode = this.humidityDevice.parameters.find(p => p.name === 'Mode');
      });
  }

  powerTemperatureDevice(isOn: boolean) {
    this.temperatureDevice.isOn = isOn;
    this.temperatureHumidityService.setTemperatureDevice(this.temperatureDevice)
      .pipe(takeWhile(() => this.alive))
      .subscribe();
  }

  powerHumidityDevice(isOn: boolean) {
    this.humidityDevice.isOn = isOn;
    this.temperatureHumidityService.setHumidityDevice(this.humidityDevice)
      .pipe(takeWhile(() => this.alive))
      .subscribe();
  }

  setTemperatureMode() {
    this.temperatureHumidityService.setTemperatureParameter(this.temperatureMode)
      .pipe(takeWhile(() => this.alive))
      .subscribe();
  }

  setHumidityMode() {
    this.temperatureHumidityService.setHumidityParameter(this.humidityMode)
      .pipe(takeWhile(() => this.alive))
      .subscribe();
  }

  setTemperatureData() {
    this.temperatureHumidityService.setTemperatureParameter(this.temperatureData)
      .pipe(takeWhile(() => this.alive))
      .subscribe();
  }

  setHumidityData() {
    this.temperatureHumidityService.setHumidityParameter(this.humidityData)
      .pipe(takeWhile(() => this.alive))
      .subscribe();
  }

  ngOnDestroy() {
    this.alive = false;
  }
}
