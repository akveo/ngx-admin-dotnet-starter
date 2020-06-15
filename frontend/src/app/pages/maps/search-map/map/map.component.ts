/*
 * Copyright (c) Akveo 2019. All Rights Reserved.
 * Licensed under the Single Application / Multi Application License.
 * See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the 'docs' folder for license information on type of purchased license.
 */

import { Component, Input, OnInit } from '@angular/core';
import { PositionModel } from '../entity/position.model';

@Component({
  selector: 'ngx-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.scss'],
})
export class MapComponent implements OnInit {

  position: PositionModel = null;
  zoom: number = 1;

  @Input()
  public set searchedPosition(position: PositionModel) {
    if (!position) {
      return;
    }

    this.position = position;
    this.zoom = 12;
  }

  ngOnInit(): void {
    // set up current location
    if ('geolocation' in navigator) {
      navigator.geolocation.getCurrentPosition((position) => {
        this.searchedPosition = new PositionModel(
          position.coords.latitude,
          position.coords.longitude,
        );
      });
    }
  }
}
