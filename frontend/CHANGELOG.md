# Change Log

## 03.04.2020 (2020-04-03)

### Feature
* feat(updates): update to angular 9 and Nebular 5

## 20.03.2020 (2020-03-20)

### Bug Fixes
* fix(iot): fix contact list, add scroll
* fix(role): fix shoving UI depends on user role
* fix(role): fix get token in user role
* fix(token): fix test token on dev

## 26.02.2020 (2020-02-26)
### Bug Fixes

* fix(user): user id validation for starter bundle
* fix(forms): user forms
* fix(ui-role): update ui depend on user role
* fix(user-profile): add server validate to user profile page, add server error handler
* fix(demo-theme): set default dark theme for demo 
* fix(ckeditor): fix console error, downgrade to 4.6.2 version 
* fix(age-input): fix on demo age input, set danger outline 
* fix(demo-token): updating token for logged-in users 
* fix(demo-build): fix demo build 
* feat(demo-split): split demo bundle on types 
* fix(styles): fix styles issues 
* fix(advanced-chart): fix advanced chart style 
* fix(IoT): fix scroll in contact card 
* fix(TinyMCE): fix TinyMCE editor, show TinyMCE editor 
* fix(tree-grid): fix tree grid console issue, update nebular 
* fix(order): fix order page on demo
* fix(role): update role for default demo user, add admin role 
* fix(user-role): update users roles, fix log in/log out user 

<a name="1.8.2020"></a>
## 1.8.2020 (2020-01-08)


### Bug Fixes

* fix(order): order component validation fix
* fix(user): update current user from grid
* fix(smart-table): add custom smart table filter by number form control
* fix: validation for register form
* fix(input): fix input types
* fix(tree grid): refactor code and move pipe provider to tables.module
* fix(tree-grid): add measure converter
* fix(toaster): fix style for toaster without icon
* fix(user): fix starter user component
* fix(chat): fix console issue when send txt type file
* fix(smart-table): fix invisible letters in inputs smart table
* fix(ckeditor): fix invisible letters in inputs ckeditor
* fix(users): fix invisible letters in inputs fields in users table
* fix(orders): fix invisible letters in inputs fields in orders table
* fix(user): fix user validation, handle error on user edit, error during creating new order, standart url
* feat(auth): implement refresh token after user edit
* fix(sign-out): removed clear localstorage function before logout method call
* fix(auth): refactor reset password page
* fix(order): timezone and validation
* fix(pages): change validation for order and user forms
* fix(roles): fix users page
* fix(e-commerce): fix e-commerce order chart size

### 1.4, October 14, 2019

Fixes:

 - user.component.ts, sass, html
 - users.service.ts
 - user.store.ts
 - header.component.ts
 - pages-menu.ts
 - one-column.layout.ts
 - constants.ts

 - order.component.html, ts, sass
 - orders.module.ts

### 1.2

Fixes:
 - https://github.com/akveo/ngx-admin-bundle-support/issues/16 The "data" argument must be one of type string. 
 - https://github.com/akveo/ngx-admin-bundle-support/issues/14 order is not working correctly 
 - https://github.com/akveo/ngx-admin-bundle-support/issues/17 different concerns on ngx-admin Backend Bundle Node
 - user profile fixed
 - https://github.com/akveo/ngx-admin/pull/5491 fix merged
 - fixed issues with TS code compilation in prod mode (related to styles)
 - remove extra unused components
 - fix roles check in ACL
 - correct error handling during the app start
 - fixed sass theme variable warnings
 - add basic protractor smoke tests
 - fix layout to properly support RTL
 - fix theme saving 

Updates:
 - nebular updated to the latest version
 - ng2-smart-table version upgrade

List of Files to Update / Add for UI:
> Those who bought starter bundle - please ignore files for ecom / iot / orders

 - e2e/smoke-test/common.test.ts
 - e2e/smoke-test/ecom.e2e-spec.ts
 - e2e/smoke-test/iot.e2e-spec.ts
 - e2e/smoke-test/starter.e2e-spec.ts
 - package-lock.json
 - package.json
 - protractor.conf.js
 - src/app/@auth/components/constants.ts
 - src/app/@auth/role.provider.ts
 - src/app/@components/validation-message/validation-message.component.scss
 - src/app/@core/backend/common/api/http.service.ts
 - src/app/@core/backend/common/api/users.api.ts
 - src/app/@core/backend/common/services/users.service.ts
 - src/app/@core/backend/ecommerce/api/orders.api.ts
 - src/app/@core/backend/ecommerce/services/orders.service.ts
 - src/app/@core/backend/iot/api/traffic-aggregated.api.ts
 - src/app/@core/interfaces/common/users.ts
 - src/app/@core/interfaces/ecommerce/orders.ts
 - src/app/@core/mock/common/users.service.ts
 - src/app/@core/stores/user.store.ts
 - src/app/@theme/components/header/header.component.ts
 - src/app/@theme/components/tiny-mce/tiny-mce.component.ts
 - src/app/@theme/layouts/one-column/one-column.layout.ts
 - src/app/app.module.ts
 - src/app/pages/charts/d3/d3.component.scss
 - src/app/pages/e-commerce/progress-section/progress-section.component.scss
 - src/app/pages/orders/order/order.component.html
 - src/app/pages/orders/order/order.component.scss
 - src/app/pages/orders/order/order.component.ts
 - src/app/pages/orders/orders-table/orders-table.component.ts
 - src/app/pages/orders/orders.module.ts
 - src/app/pages/pages-menu.ts
 - src/app/pages/users/user/user.component.html
 - src/app/pages/users/user/user.component.scss
 - src/app/pages/users/user/user.component.ts
 - src/app/pages/users/users-table/users-table.component.ts
 - src/environments/environment.prod.ts
 - src/environments/environment.ts
 - src/tsconfig.app.json

### 1.1

 - Update to the latest NGX-Admin and Nebular

### 1.0

UI: 

 - Initial version containing the whole UI and Backend code
 - Auth integration, JWT token is used
 - Nebular Auth integration
 - Mock services to change api data and mock data
 - ...

Backend:

 - Initial version of solution structure
 - User Repository with Entity Framework Core data access
 - Dependency Injection
 - Async API controllers
 - Auth with JWT tokens
 - ACL and user role management
 - ...
