/*
 * Copyright (c) Akveo 2019. All Rights Reserved.
 * Licensed under the Single Application / Multi Application License.
 * See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the 'docs' folder for license information on type of purchased license.
 */
import { ChangeDetectionStrategy, ChangeDetectorRef, Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NB_AUTH_OPTIONS, NbAuthService, NbAuthResult } from '@nebular/auth';
import { getDeepFromObject } from '../../helpers';
import { EMAIL_PATTERN } from '../constants';

@Component({
  selector: 'ngx-request-password-page',
  styleUrls: ['./request-password.component.scss'],
  templateUrl: './request-password.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class NgxRequestPasswordComponent implements OnInit {
  redirectDelay: number = this.getConfigValue('forms.requestPassword.redirectDelay');
  showMessages: any = this.getConfigValue('forms.requestPassword.showMessages');
  strategy: string = this.getConfigValue('forms.requestPassword.strategy');
  isEmailRequired: boolean = this.getConfigValue('forms.validation.email.required');

  submitted = false;
  errors: string[] = [];
  messages: string[] = [];
  user: any = {};
  requestPasswordForm: FormGroup;

  constructor(protected service: NbAuthService,
              @Inject(NB_AUTH_OPTIONS) protected options = {},
              protected cd: ChangeDetectorRef,
              protected fb: FormBuilder,
              protected router: Router) { }

  get email() { return this.requestPasswordForm.get('email'); }

  ngOnInit(): void {
    const passwordValidators = [
      Validators.pattern(EMAIL_PATTERN),
    ];
    this.isEmailRequired && passwordValidators.push(Validators.required);

    this.requestPasswordForm = this.fb.group({
      email: this.fb.control('', [...passwordValidators]),
    });
  }

  requestPass(): void {
    this.user = this.requestPasswordForm.value;
    this.errors = this.messages = [];
    this.submitted = true;

    this.service.requestPassword(this.strategy, this.user).subscribe((result: NbAuthResult) => {
      this.submitted = false;
      if (result.isSuccess()) {
        this.messages = result.getMessages();
      } else {
        this.errors = result.getErrors();
      }

      const redirect = result.getRedirect();
      if (redirect) {
        setTimeout(() => {
          return this.router.navigateByUrl(redirect);
        }, this.redirectDelay);
      }
      this.cd.detectChanges();
    });
  }

  getConfigValue(key: string): any {
    return getDeepFromObject(this.options, key, null);
  }
}
