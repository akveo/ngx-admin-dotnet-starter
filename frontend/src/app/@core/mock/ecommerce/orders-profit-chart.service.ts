/*
 * Copyright (c) Akveo 2019. All Rights Reserved.
 * Licensed under the Single Application / Multi Application License.
 * See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the 'docs' folder for license information on type of purchased license.
 */

import { of as observableOf,  Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { ChartData, ChartSummary } from '../../interfaces/common/chart';
import { OrdersProfitChartData } from '../../interfaces/ecommerce/orders-profit-chart';
import { OrdersChartService } from './orders-chart.service';
import { ProfitChartService } from './profit-chart.service';

@Injectable()
export class OrdersProfitChartService extends OrdersProfitChartData {

  private summary = [
    {
      title: 'Marketplace',
      value: 3654,
    },
    {
      title: 'Last Month',
      value: 946,
    },
    {
      title: 'Last Week',
      value: 654,
    },
    {
      title: 'Today',
      value: 230,
    },
  ];

  constructor(private ordersChartService: OrdersChartService,
              private profitChartService: ProfitChartService) {
    super();
  }

  getOrderProfitChartSummary(): Observable<ChartSummary[]> {
    return observableOf(this.summary);
  }

  getOrdersChartData(period: string): Observable<ChartData> {
    return observableOf(this.ordersChartService.getOrdersChartData(period));
  }

  getProfitChartData(period: string): Observable<ChartData> {
    return observableOf(this.profitChartService.getProfitChartData(period));
  }
}
