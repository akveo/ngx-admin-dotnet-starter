/*
 * Copyright (c) Akveo 2019. All Rights Reserved.
 * Licensed under the Single Application / Multi Application License.
 * See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the 'docs' folder for license information on type of purchased license.
 */

import { of as observableOf,  Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { PhoneData, Contact, RecentUser } from '../../interfaces/iot/phone';

@Injectable()
export class PhoneService extends PhoneData {

  private time: Date = new Date;

  private types = {
    mobile: 'mobile',
    home: 'home',
    work: 'work',
  };

  private contacts: Contact[] = [
    { name: 'Nick Jones', picture: 'assets/images/nick.png', type: this.types.mobile },
    { name: 'Eva Moor', picture: 'assets/images/eva.png', type: this.types.home },
    { name: 'Jack Williams', picture: 'assets/images/jack.png', type: this.types.mobile },
    { name: 'Lee Wong', picture: 'assets/images/lee.png', type: this.types.mobile },
    { name: 'Alan Thompson', picture: 'assets/images/alan.png', type: this.types.home },
    { name: 'Kate Martinez', picture: 'assets/images/kate.png', type: this.types.work },
  ];

  private recentUsers: RecentUser[]  = [
    {
      name: this.contacts[0].name,
      picture: this.contacts[0].picture,
      type: this.contacts[0].type,
      time: this.time.setHours(21, 12),
    },
    {
      name: this.contacts[2].name,
      picture: this.contacts[2].picture,
      type: this.contacts[2].type,
      time: this.time.setHours(20, 13),
    },
    {
      name: this.contacts[1].name,
      picture: this.contacts[1].picture,
      type: this.contacts[1].type,
      time: this.time.setHours(18, 10),
    },
    {
      name: this.contacts[0].name,
      picture: this.contacts[0].picture,
      type: this.contacts[0].type,
      time: this.time.setHours(15, 45),
    },
    {
      name: this.contacts[4].name,
      picture: this.contacts[4].picture,
      type: this.contacts[4].type,
      time: this.time.setHours(14, 33),
    },
    {
      name: this.contacts[4].name,
      picture: this.contacts[4].picture,
      type: this.contacts[4].type,
      time: this.time.setHours(14, 29),
    },
  ];

  getContacts(): Observable<Contact[]> {
    return observableOf(this.contacts);
  }

  getRecentUsers(): Observable<RecentUser[]> {
    return observableOf(this.recentUsers);
  }
}
