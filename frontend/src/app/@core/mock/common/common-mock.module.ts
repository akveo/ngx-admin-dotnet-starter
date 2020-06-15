/*
 * Copyright (c) Akveo 2019. All Rights Reserved.
 * Licensed under the Single Application / Multi Application License.
 * See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the 'docs' folder for license information on type of purchased license.
 */

import { ModuleWithProviders, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UserData } from '../../interfaces/common/users';
import { SmartTableData } from '../../interfaces/common/smart-table';

import { UsersService } from './users.service';
import { SmartTableService } from './smart-table.service';
import { PeriodsService } from './periods.service';
import { SettingsData } from '../../interfaces/common/settings';
import { SettingsService } from './settings.service';

const SERVICES = [
  { provide: UserData, useClass: UsersService },
  { provide: SmartTableData, useClass: SmartTableService },
  { provide: PeriodsService, useClass: PeriodsService },
  { provide: SettingsData, useClass: SettingsService },
];

@NgModule({
  imports: [CommonModule],
})
export class CommonMockModule {
  static forRoot(): ModuleWithProviders<CommonMockModule> {
    return {
      ngModule: CommonMockModule,
      providers: [
        ...SERVICES,
      ],
    };
  }
}
