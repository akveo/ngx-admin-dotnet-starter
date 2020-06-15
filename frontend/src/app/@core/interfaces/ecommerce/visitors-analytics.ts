/*
 * Copyright (c) Akveo 2019. All Rights Reserved.
 * Licensed under the Single Application / Multi Application License.
 * See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the 'docs' folder for license information on type of purchased license.
 */

import { Observable } from 'rxjs';

export interface OutlineData {
  label: string;
  value: number;
}

export abstract class VisitorsAnalyticsData {
  abstract getInnerLineChartData(): Observable<number[]>;
  abstract getOutlineLineChartData(): Observable<OutlineData[]>;
  abstract getPieChartData(): Observable<number>;
}
