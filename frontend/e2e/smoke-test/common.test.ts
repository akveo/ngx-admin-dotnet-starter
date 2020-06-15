/*
 * Copyright (c) Akveo 2019. All Rights Reserved.
 * Licensed under the Single Application / Multi Application License.
 * See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the 'docs' folder for license information on type of purchased license.
 */

import { browser } from 'protractor';
import { environment } from '../../src/environments/environment';

export async function testUserInit() {
  // navigate to some valid page before setting localStorage
  await browser.get(`${browser.baseUrl}/auth/login`);

  // set demo test token
  const token_item = {
    name: 'nb:auth:oauth2:jwt:token',
    ownerStrategyName: 'email',
    createdAt: (new Date()).getTime(),
    value: environment.testUser.token,
  };
  const token_item_string = JSON.stringify(token_item);

  browser.executeScript(`
      localStorage.setItem('auth_app_token', '${token_item_string}');
      localStorage.setItem('demo_token_initialized', 'true');
    `);
}
