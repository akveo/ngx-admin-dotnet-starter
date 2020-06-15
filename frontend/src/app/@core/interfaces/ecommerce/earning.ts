/*
 * Copyright (c) Akveo 2019. All Rights Reserved.
 * Licensed under the Single Application / Multi Application License.
 * See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the 'docs' folder for license information on type of purchased license.
 */

import { Observable } from 'rxjs';

export interface LiveUpdateChart {
  liveChart: { value: [string, number] }[];
  delta: {
    up: boolean;
    value: number;
  };
  dailyIncome: number;
}

export interface PieChart {
  value: number;
  name: string;
}

export abstract class EarningData {
  abstract getEarningLiveUpdateCardData(currency: string): Observable<any[]>;
  abstract getEarningCardData(currency: string): Observable<LiveUpdateChart>;
  abstract getEarningPieChartData(): Observable<PieChart[]>;
}
