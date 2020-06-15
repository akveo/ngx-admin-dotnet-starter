/*
 * Copyright (c) Akveo 2019. All Rights Reserved.
 * Licensed under the Single Application / Multi Application License.
 * See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the 'docs' folder for license information on type of purchased license.
 */

import { of as observableOf,  Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { User, UserData } from '../../interfaces/common/users';
import { LocalDataSource } from 'ng2-smart-table';
import { DataSource } from 'ng2-smart-table/lib/lib/data-source/data-source';

@Injectable()
export class UsersService extends UserData {

  get gridDataSource(): DataSource {
    return new LocalDataSource(this.data);
  }

  getCurrentUser(): Observable<User> {
    return observableOf(this.data[0]);
  }

  list(pageNumber: number = 1, pageSize: number = 10): Observable<User[]> {
    return observableOf(this.data);
  }

  get(id: number): Observable<User> {
    return observableOf(this.data.find(x => x.id === id));
  }

  updateCurrent(user: User): Observable<User> {
    this.data[0] = user;

    return observableOf(user);
  }

  update(user: User): Observable<User> {
    const i = this.data.indexOf(this.data.find(x => x.id === user.id));
    if (i >= 0) {
      this.data[i] = user;
    }
    return observableOf(user);
  }

  create(user: User): Observable<User> {
    user.id = Math.max(...this.data.map(x => x.id)) + 1;
    this.data.push(user);
    return observableOf(user);
  }

  delete(id: number): Observable<boolean> {
    this.data = [...this.data.filter(x => x.id !== id)];
    return observableOf();
  }

  private data: User[] = [
    {
      id: 1,
      role: 'user',
      firstName: 'Mark',
      lastName: 'Walmart',
      login: '@mdo',
      email: 'mdo@gmail.com',
      age: 0,
      picture: '',
      address: {
        street: 'Wall St.',
        city: 'New York',
        zipCode: '10005',
      },
      settings: {
        themeName: 'cosmic',
      },
    },
    {
      id: 2,
      role: 'user',
      firstName: 'Jacob',
      lastName: 'Cuba',
      login: '@mdo',
      email: 'mdo@gmail.com',
      age: 0,
      picture: '',
      address: {
        street: 'Wall St.',
        city: 'New York',
        zipCode: '10005',
      },
      settings: {
        themeName: 'cosmic',
      },
    },
    {
      id: 3,
      role: 'user',
      firstName: 'Larry',
      lastName: 'Page',
      login: '@twitter',
      email: 'twitter@outlook.com',
      age: 0,
      picture: '',
      address: {
        street: 'Wall St.',
        city: 'New York',
        zipCode: '10005',
      },
      settings: {
        themeName: 'cosmic',
      },
    },
    {
      id: 4,
      role: 'user',
      firstName: 'John',
      lastName: 'Snow',
      login: '@snow',
      email: 'snow@gmail.com',
      age: 0,
      picture: '',
      address: {
        street: 'Wall St.',
        city: 'New York',
        zipCode: '10005',
      },
      settings: {
        themeName: 'cosmic',
      },
    }];
}
