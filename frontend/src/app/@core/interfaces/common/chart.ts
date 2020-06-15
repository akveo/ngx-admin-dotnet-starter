/*
 * Copyright (c) Akveo 2019. All Rights Reserved.
 * Licensed under the Single Application / Multi Application License.
 * See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the 'docs' folder for license information on type of purchased license.
 */

export interface ChartData {
  chartLabel: string;
  axisXLabels: string[];
  linesData: number[][];
  legend: string[];
}

export interface AggregatedChartData extends ChartData {
  aggregatedData: ChartSummary[];
}

export interface ChartSummary {
  title: string;
  value: number;
}
