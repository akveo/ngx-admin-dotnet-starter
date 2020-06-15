/*
 * Copyright (c) Akveo 2019. All Rights Reserved.
 * Licensed under the Single Application / Multi Application License.
 * See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the 'docs' folder for license information on type of purchased license.
 */

import { Pipe, PipeTransform } from '@angular/core';
import { NbAuthService, NbAuthToken } from '@nebular/auth';
import { Observable, of as observableOf } from 'rxjs';
import { map } from 'rxjs/operators';

@Pipe({ name: 'ngxAuthToken' })
export class AuthPipe implements PipeTransform {
  constructor(private authService: NbAuthService) {}

  transform(url: string): Observable<string> {
    if (!url) {
      return observableOf(url);
    }
    return this.authService.getToken()
      .pipe(map((token: NbAuthToken) => {
        const separator = url.indexOf('?') > 0 ? '&' : '?';
        return `${url}${separator}token=${token.getValue()}`;
      }));

  }
}
