/*
 * Copyright (c) Akveo 2019. All Rights Reserved.
 * Licensed under the Single Application / Multi Application License.
 * See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the 'docs' folder for license information on type of purchased license.
 */

import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { NbThemeService } from '@nebular/theme';
import { takeWhile } from 'rxjs/operators';
import { TrafficChartData } from '../../../@core/interfaces/iot/traffic-chart';
import { TrafficChartComponent } from './traffic-chart.component';

@Component({
  selector: 'ngx-traffic',
  styleUrls: ['./traffic.component.scss'],
  template: `
    <nb-card size="tiny">
      <nb-card-header>
        <span>Traffic Consumption</span>

        <nb-select [(selected)]="type" (selectedChange)="fetchData()">
          <nb-option *ngFor="let t of types" [value]="t">{{ t }}</nb-option>
        </nb-select>
      </nb-card-header>
      <nb-card-body [nbSpinner]="!trafficChartPoints">
        <ngx-traffic-chart #chart *ngIf="trafficChartPoints" [points]="trafficChartPoints"></ngx-traffic-chart>
      </nb-card-body>
    </nb-card>
  `,
})
export class TrafficComponent implements OnInit, OnDestroy {

  private alive = true;

  trafficChartPoints: number[];
  type = 'month';
  types = ['week', 'month', 'year'];
  currentTheme: string;

  @ViewChild('chart') chart: TrafficChartComponent;

  constructor(private themeService: NbThemeService,
              private trafficChartService: TrafficChartData) {
    this.themeService.getJsTheme()
      .pipe(takeWhile(() => this.alive))
      .subscribe(theme => {
      this.currentTheme = theme.name;
    });
  }

  ngOnInit(): void {
    this.fetchData();
  }

  fetchData() {
    this.trafficChartService.getTrafficChartData(this.type)
      .pipe(takeWhile(() => this.alive))
      .subscribe((data) => {
        this.trafficChartPoints = data;
        this.chart && this.chart.resizeChart();
      });
  }

  ngOnDestroy() {
    this.alive = false;
  }
}
