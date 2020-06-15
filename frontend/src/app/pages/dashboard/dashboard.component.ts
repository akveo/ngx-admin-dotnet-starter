/*
 * Copyright (c) Akveo 2019. All Rights Reserved.
 * Licensed under the Single Application / Multi Application License.
 * See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the 'docs' folder for license information on type of purchased license.
 */

import {Component, OnDestroy} from '@angular/core';
import { takeWhile } from 'rxjs/operators' ;
import { SolarData, SolarEnergyStatistics } from '../../@core/interfaces/iot/solar';
import { Device, DevicesData } from '../../@core/interfaces/iot/devices';

@Component({
  selector: 'ngx-dashboard',
  styleUrls: ['./dashboard.component.scss'],
  templateUrl: './dashboard.component.html',
})
export class DashboardComponent implements OnDestroy {

  private alive = true;

  solarValue: SolarEnergyStatistics;

  devices: Device[];

  constructor(private devicesService: DevicesData,
              private solarService: SolarData) {
    this.devicesService.list()
      .pipe(takeWhile(() => this.alive))
      .subscribe(data => {
        this.devices = data.filter(x => x.settings);
      });


    this.solarService.getSolarData()
      .pipe(takeWhile(() => this.alive))
      .subscribe((data) => {
        this.solarValue = data;
      });
  }

  changeDeviceStatus(device: Device, isOn: boolean) {
    device.isOn = isOn;
    this.devicesService.edit(device)
      .pipe(takeWhile(() => this.alive))
      .subscribe();
  }

  ngOnDestroy() {
    this.alive = false;
  }
}
