/*
 * Copyright (c) Akveo 2019. All Rights Reserved.
 * Licensed under the Single Application / Multi Application License.
 * See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the 'docs' folder for license information on type of purchased license.
 */

import { Observable } from 'rxjs';

export interface Contact {
  name: string;
  picture: string;
  type: string;
}

export interface RecentUser extends Contact {
  time: number;
}

export abstract class PhoneData {
  abstract getContacts(): Observable<Contact[]>;
  abstract getRecentUsers(): Observable<RecentUser[]>;
}
