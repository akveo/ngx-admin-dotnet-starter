/*
 * Copyright (c) Akveo 2019. All Rights Reserved.
 * Licensed under the Single Application / Multi Application License.
 * See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the 'docs' folder for license information on type of purchased license.
 */

import { Injectable } from '@angular/core';
import { of as observableOf,  Observable } from 'rxjs';
import { SolarData, SolarEnergyStatistics } from '../../interfaces/iot/solar';

@Injectable()
export class SolarService extends SolarData {

  private value: SolarEnergyStatistics = {
    totalValue: 8421,
    solarValue: 6421,
    percent: 42,
    unitOfMeasure: 'kWh',
  };

  getSolarData(): Observable<SolarEnergyStatistics> {
    return observableOf(this.value);
  }
}
