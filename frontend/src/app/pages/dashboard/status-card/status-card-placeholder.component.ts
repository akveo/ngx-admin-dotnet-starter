/*
 * Copyright (c) Akveo 2019. All Rights Reserved.
 * Licensed under the Single Application / Multi Application License.
 * See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the 'docs' folder for license information on type of purchased license.
 */

import { Component } from '@angular/core';

@Component({
  selector: 'ngx-status-card-placeholder',
  styleUrls: ['./status-card.component.scss'],
  template: `
    <nb-card [ngClass]="{'off': true}">
      <div class="icon-container">
        <div class="icon primary">
        </div>
      </div>
      <div class="details">
        <div class="title"></div>
        <div class="status">OFF</div>
      </div>
    </nb-card>
  `,
})
export class StatusCardPlaceholderComponent {
}
