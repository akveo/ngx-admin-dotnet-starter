/*
 * Copyright (c) Akveo 2019. All Rights Reserved.
 * Licensed under the Single Application / Multi Application License.
 * See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the 'docs' folder for license information on type of purchased license.
 */

import { ModuleWithProviders, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CommonMockModule } from '../common/common-mock.module';

import { CountryOrderData } from '../../interfaces/ecommerce/country-order';
import { OrdersProfitChartData } from '../../interfaces/ecommerce/orders-profit-chart';
import { ProfitBarAnimationChartData } from '../../interfaces/ecommerce/profit-bar-animation-chart';
import { StatsBarData } from '../../interfaces/ecommerce/stats-bar';
import { StatsProgressBarData } from '../../interfaces/ecommerce/stats-progress-bar';
import { TrafficListData } from '../../interfaces/ecommerce/traffic-list';
import { UserActivityData } from '../../interfaces/ecommerce/user-activity';
import { EarningData } from '../../interfaces/ecommerce/earning';
import { VisitorsAnalyticsData } from '../../interfaces/ecommerce/visitors-analytics';

import { CountryOrderService } from './country-order.service';
import { OrdersProfitChartService } from './orders-profit-chart.service';
import { ProfitBarAnimationChartService } from './profit-bar-animation-chart.service';
import { StatsBarService } from './stats-bar.service';
import { StatsProgressBarService } from './stats-progress-bar.service';
import { TrafficListService } from './traffic-list.service';
import { UserActivityService } from './user-activity.service';
import { EarningService } from './earning.service';
import { OrdersChartService } from './orders-chart.service';
import { ProfitChartService } from './profit-chart.service';
import { VisitorsAnalyticsService } from './visitors-analytics.service';

const SERVICES = [
  { provide: CountryOrderData, useClass: CountryOrderService },
  { provide: OrdersProfitChartData, useClass: OrdersProfitChartService },
  { provide: ProfitBarAnimationChartData, useClass: ProfitBarAnimationChartService },
  { provide: StatsBarData, useClass: StatsBarService },
  { provide: StatsProgressBarData, useClass: StatsProgressBarService },
  { provide: TrafficListData, useClass: TrafficListService },
  { provide: UserActivityData, useClass: UserActivityService },
  { provide: EarningData, useClass: EarningService },
  { provide: VisitorsAnalyticsData, useClass: VisitorsAnalyticsService },
];

const INTERNAL_SERVICES = [
  OrdersChartService,
  ProfitChartService,
];

@NgModule({
  imports: [CommonModule, CommonMockModule],
  exports: [CommonMockModule],
})
export class EcommerceMockModule {
  static forRoot(): ModuleWithProviders<EcommerceMockModule> {
    return {
      ngModule: EcommerceMockModule,
      providers: [
        ...INTERNAL_SERVICES,
        ...SERVICES,
      ],
    };
  }
}
