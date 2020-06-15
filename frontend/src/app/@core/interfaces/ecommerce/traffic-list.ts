/*
 * Copyright (c) Akveo 2019. All Rights Reserved.
 * Licensed under the Single Application / Multi Application License.
 * See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the 'docs' folder for license information on type of purchased license.
 */

import { Observable } from 'rxjs';

export interface TrafficListItem {
  date: string;
  value: number;
  delta: {
    up: boolean;
    value: number;
  };
  comparison: Comparison;
}

export interface Comparison {
  prevDate: string;
  prevValue: number;
  nextDate: string;
  nextValue: number;
}

export abstract class TrafficListData {
  abstract getTrafficListData(period: string): Observable<TrafficListItem[]>;
}
