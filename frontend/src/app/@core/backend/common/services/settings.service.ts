/*
 * Copyright (c) Akveo 2019. All Rights Reserved.
 * Licensed under the Single Application / Multi Application License.
 * See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the 'docs' folder for license information on type of purchased license.
 */

import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { SettingsApi } from '../api/settings.api';
import { SettingsData, Settings } from '../../../interfaces/common/settings';

@Injectable()
export class SettingsService extends SettingsData {
  constructor(private api: SettingsApi) {
    super();
  }

  getCurrentSetting(): Observable<Settings> {
    return this.api.getCurrent();
  }

  updateCurrent(setting: any): Observable<Settings> {
    return this.api.updateCurrent(setting);
  }
}
