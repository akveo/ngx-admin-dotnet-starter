/*
 * Copyright (c) Akveo 2019. All Rights Reserved.
 * Licensed under the Single Application / Multi Application License.
 * See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the 'docs' folder for license information on type of purchased license.
 */

import { Observable } from 'rxjs';
import { ChartData, ChartSummary } from '../common/chart';

export interface Month {
  month: string;
  delta: string;
  down: boolean;
  kWatts: string;
  cost: string;
}

export interface Electricity {
  title: string;
  active?: boolean;
  months: Month[];
}

export interface ElectricityChart {
  chart: ChartData;
  consumed: ChartSummary;
  spent: ChartSummary;
}

export abstract class ElectricityData {
  abstract getListData(yearsCount: number): Observable<Electricity[]>;
  abstract getChartData(period: string): Observable<ElectricityChart>;
}
