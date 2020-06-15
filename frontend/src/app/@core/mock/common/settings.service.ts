/*
 * Copyright (c) Akveo 2019. All Rights Reserved.
 * Licensed under the Single Application / Multi Application License.
 * See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the 'docs' folder for license information on type of purchased license.
 */

import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { SettingsData, Settings } from '../../interfaces/common/settings';

@Injectable()
export class SettingsService extends SettingsData {

  getCurrentSetting(): Observable<Settings> {
    return of({
      themeName: 'cosmic',
    });
  }

  updateCurrent(setting: any): Observable<Settings> {
    return of({
      themeName: 'corporate',
    });
  }
}
