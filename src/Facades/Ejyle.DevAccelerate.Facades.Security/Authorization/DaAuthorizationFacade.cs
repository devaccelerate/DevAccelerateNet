﻿// ----------------------------------------------------------------------------------------------------------------------
// Author: Tanveer Yousuf (@tanveery)
// ----------------------------------------------------------------------------------------------------------------------
// Copyright © Ejyle Technologies (P) Ltd. All rights reserved.
// Licensed under the MIT license. See the LICENSE file in the project's root directory for complete license information.
// ----------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using Ejyle.DevAccelerate.Identity;
using Ejyle.DevAccelerate.Identity.EF;
using Ejyle.DevAccelerate.EnterpriseSecurity.EF.Tenants;
using Ejyle.DevAccelerate.EnterpriseSecurity.Tenants;
using Ejyle.DevAccelerate.EnterpriseSecurity.EF.SubscriptionPlans;
using Ejyle.DevAccelerate.EnterpriseSecurity.SubscriptionPlans;
using Ejyle.DevAccelerate.EnterpriseSecurity.EF.Subscriptions;
using Ejyle.DevAccelerate.EnterpriseSecurity.Subscriptions;
using Ejyle.DevAccelerate.EnterpriseSecurity.Apps;
using Ejyle.DevAccelerate.EnterpriseSecurity.UserAgreements;
using Ejyle.DevAccelerate.EnterpriseSecurity.EF.Apps;
using Ejyle.DevAccelerate.EnterpriseSecurity.EF.UserAgreements;
using System.Collections.Generic;
using Ejyle.DevAccelerate.Core;
using Microsoft.AspNetCore.Identity;
using Ejyle.DevAccelerate.Lists.Countries;
using Ejyle.DevAccelerate.Lists.Currencies;
using Ejyle.DevAccelerate.Lists.DateFormats;
using Ejyle.DevAccelerate.Lists.SystemLanguages;
using Ejyle.DevAccelerate.Lists.TimeZones;
using Ejyle.DevAccelerate.Lists.EF.Countries;
using Ejyle.DevAccelerate.Lists.EF.Currencies;
using Ejyle.DevAccelerate.Lists.EF.SystemLanguages;
using Ejyle.DevAccelerate.Lists.EF.TimeZones;

namespace Ejyle.DevAccelerate.Facades.Security.Authorization
{
    public class DaAuthorizationFacade : DaAuthorizationFacade<int, int?, DaAuthorizedFeatureInfo, DaAuthorizationInfo, DaAuthorizedActionInfo, UserManager<DaUser>, DaUser, RoleManager<DaRole>, DaRole, DaTenantManager, DaTenant, DaTenantUser, DaTenantAttribute, DaUserAgreementManager, DaUserAgreement, DaUserAgreementVersion, DaUserAgreementVersionAction, DaAppManager, DaApp, DaAppAttribute, DaFeatureManager, DaFeature, DaAppFeature, DaFeatureAction, DaSubscriptionPlanManager, DaSubscriptionPlan, DaSubscriptionPlanAttribute, DaBillingCycleOption, DaSubscriptionPlanApp, DaSubscriptionPlanFeature, DaSubscriptionPlanFeatureAttribute, DaSubscriptionManager, DaSubscription, DaSubscriptionAttribute, DaSubscriptionApp, DaSubscriptionFeature, DaSubscriptionFeatureAttribute, DaSubscriptionAppRole, DaSubscriptionAppUser, DaSubscriptionFeatureRole, DaSubscriptionFeatureRoleAction, DaSubscriptionFeatureUser, DaSubscriptionFeatureUserAction, DaBillingCycle, DaBillingCycleAttribute, DaBillingCycleFeatureUsage, DaCurrencyManager, DaCurrency, DaCountryManager, DaCountry, DaCountryRegion, DaCountryDateFormat, DaCountrySystemLanguage, DaCountryTimeZone, DaDateFormat, DaTimeZoneManager, DaTimeZone, DaSystemLanguageManager, DaSystemLanguage>
    {
        public DaAuthorizationFacade(UserManager<DaUser> userManager, RoleManager<DaRole> roleManager, DaTenantManager tenantManager, DaAppManager appManager, DaFeatureManager featureManager, DaUserAgreementManager userAgreementManager, DaSubscriptionPlanManager subscriptionPlanManager, DaSubscriptionManager subscriptionManager, DaCurrencyManager currencyManager, DaCountryManager countryManager, DaTimeZoneManager timeZoneManager, DaSystemLanguageManager systemLanguageManager)
            : base(userManager, roleManager, tenantManager, appManager, featureManager, userAgreementManager, subscriptionPlanManager, subscriptionManager, currencyManager, countryManager, timeZoneManager, systemLanguageManager)
        {
        }
    }

    public class DaAuthorizationFacade<TKey, TNullableKey, TAuthorizedFeatureInfo, TAuthorizationInfo, TAuthorizedActionInfo, TUserManager, TUser, TRoleManager, TRole, TTenantManager, TTenant, TTenantUser, TTenantAttribute, TUserAgreementManager, TUserAgreement, TUserAgreementVersion, TUserAgreementVersionAction, TAppManager, TApp, TAppAttribute, TFeatureManager, TFeature, TAppFeature, TFeatureAction, TSubscriptionPlanManager, TSubscriptionPlan, TSubscriptionPlanAttribute, TBillingCycleOption, TSubscriptionPlanApp, TSubscriptionPlanFeature, TSubscriptionPlanFeatureAttribute, TSubscriptionManager, TSubscription, TSubscriptionAttribute, TSubscriptionApp, TSubscriptionFeature, TSubscriptionFeatureAttribute, TSubscriptionAppRole, TSubscriptionAppUser, TSubscriptionFeatureRole, TSubscriptionFeatureRoleAction, TSubscriptionFeatureUser, TSubscriptionFeatureUserAction, TBillingCycle, TBillingCycleAttribute, TBillingCycleFeatureUsage, TCurrencyManager, TCurrency, TCountryManager, TCountry, TCountryRegion, TCountryDateFormat, TCountrySystemLanguage, TCountryTimeZone, TDateFormat, TTimeZoneManager, TTimeZone, TSystemLanguageManager, TSystemLanguage>
        where TKey : IEquatable<TKey>
        where TAuthorizedFeatureInfo : DaAuthorizedFeatureInfo<TKey>, new()
        where TAuthorizationInfo : DaAuthorizationInfo<TKey, TAuthorizedActionInfo>, new()
        where TAuthorizedActionInfo : DaAuthorizedActionInfo<TKey>, new()
        where TUserManager : UserManager<TUser>
        where TUser : DaUser<TKey, TNullableKey>, new()
        where TRoleManager : RoleManager<TRole>
        where TRole : DaRole<TKey>, new()
        where TAppManager : DaAppManager<TKey, TApp>
        where TApp : DaApp<TKey, TNullableKey, TAppAttribute, TFeature, TAppFeature, TSubscriptionApp, TSubscriptionPlanApp, TUserAgreement>
        where TAppAttribute : DaAppAttribute<TKey, TApp>
        where TAppFeature : DaAppFeature<TKey, TApp, TFeature>
        where TBillingCycleOption : DaBillingCycleOption<TKey, TNullableKey, TSubscriptionPlan>
        where TFeatureAction : DaFeatureAction<TKey, TNullableKey, TFeature>
        where TFeatureManager : DaFeatureManager<TKey, TNullableKey, TFeature>
        where TFeature : DaFeature<TKey, TNullableKey, TApp, TAppFeature, TFeatureAction, TSubscriptionFeature, TSubscriptionPlanFeature>
        where TSubscriptionManager : DaSubscriptionManager<TKey, TNullableKey, TSubscription>
        where TSubscriptionAppRole : DaSubscriptionAppRole<TKey, TSubscriptionApp>
        where TSubscriptionApp : DaSubscriptionApp<TKey, TNullableKey, TApp, TSubscriptionAppRole, TSubscription, TSubscriptionAppUser>, new()
        where TSubscriptionAppUser : DaSubscriptionAppUser<TKey, TSubscriptionApp>
        where TSubscriptionFeatureAttribute : DaSubscriptionFeatureAttribute<TKey, TSubscriptionFeature>, new()
        where TSubscriptionFeatureRoleAction : DaSubscriptionFeatureRoleAction<TKey, TSubscriptionFeatureRole>
        where TSubscriptionFeatureRole : DaSubscriptionFeatureRole<TKey, TSubscriptionFeatureRoleAction, TSubscriptionFeature>
        where TSubscriptionFeature : DaSubscriptionFeature<TKey, TNullableKey, TFeature, TSubscriptionFeatureAttribute, TSubscriptionFeatureRole, TSubscription, TSubscriptionFeatureUser, TBillingCycleFeatureUsage>, new()
        where TSubscriptionFeatureUserAction : DaSubscriptionFeatureUserAction<TKey, TSubscriptionFeatureUser>
        where TSubscriptionFeatureUser : DaSubscriptionFeatureUser<TKey, TNullableKey, TSubscriptionFeature, TSubscriptionFeatureUserAction>
        where TSubscriptionPlanManager : DaSubscriptionPlanManager<TKey, TNullableKey, TSubscriptionPlan>
        where TSubscriptionPlanApp : DaSubscriptionPlanApp<TKey, TNullableKey, TApp, TSubscriptionPlan>
        where TSubscriptionPlanFeatureAttribute : DaSubscriptionPlanFeatureAttribute<TKey, TSubscriptionPlanFeature>
        where TSubscriptionPlanFeature : DaSubscriptionPlanFeature<TKey, TNullableKey, TFeature, TSubscriptionPlanFeatureAttribute, TSubscriptionPlan>
        where TSubscriptionPlan : DaSubscriptionPlan<TKey, TNullableKey, TSubscriptionPlanAttribute, TBillingCycleOption, TSubscriptionPlanApp, TSubscriptionPlanFeature, TSubscription>
        where TSubscriptionPlanAttribute : DaSubscriptionPlanAttribute<TKey, TNullableKey, TSubscriptionPlan>
        where TSubscription : DaSubscription<TKey, TNullableKey, TSubscriptionAttribute, TSubscriptionApp, TSubscriptionFeature, TSubscriptionPlan, TBillingCycle>, new()
        where TBillingCycle : DaBillingCycle<TKey, TNullableKey, TBillingCycleAttribute, TSubscription, TBillingCycleFeatureUsage>
        where TBillingCycleAttribute : DaBillingCycleAttribute<TKey, TNullableKey, TBillingCycle>
        where TBillingCycleFeatureUsage : DaBillingCycleFeatureUsage<TKey, TNullableKey, TBillingCycle, TSubscriptionFeature>
        where TSubscriptionAttribute : DaSubscriptionAttribute<TKey, TNullableKey, TSubscription>
        where TTenantManager : DaTenantManager<TKey, TNullableKey, TTenant>
        where TTenant : DaTenant<TKey, TNullableKey, TTenantUser, TTenantAttribute>, new()
        where TTenantAttribute : DaTenantAttribute<TKey, TNullableKey, TTenant>
        where TTenantUser : DaTenantUser<TKey, TNullableKey, TTenant>, new()
        where TUserAgreementManager : DaUserAgreementManager<TKey, TNullableKey, TUserAgreement, TUserAgreementVersion, TUserAgreementVersionAction>
        where TUserAgreement : DaUserAgreement<TKey, TNullableKey, TApp, TUserAgreementVersion>
        where TUserAgreementVersion : DaUserAgreementVersion<TKey, TNullableKey, TUserAgreement, TUserAgreementVersionAction>
        where TUserAgreementVersionAction : DaUserAgreementVersionAction<TKey, TUserAgreementVersion>
        where TCurrencyManager : DaCurrencyManager<TKey, TCurrency>
        where TCurrency : DaCurrency<TKey, TNullableKey, TCountry>
        where TCountryManager : DaCountryManager<TKey, TNullableKey, TCountry, TCountryRegion>
        where TCountry : DaCountry<TKey, TNullableKey, TCurrency, TCountryTimeZone, TCountryRegion, TCountrySystemLanguage, TCountryDateFormat>
        where TCountryRegion : DaCountryRegion<TKey, TNullableKey, TCountryRegion, TCountry>
        where TCountryDateFormat : DaCountryDateFormat<TKey, TNullableKey, TCountry, TDateFormat>
        where TCountrySystemLanguage : DaCountrySystemLanguage<TKey, TNullableKey, TCountry, TSystemLanguage>
        where TCountryTimeZone : DaCountryTimeZone<TKey, TNullableKey, TCountry, TTimeZone>
        where TDateFormat : DaDateFormat<TKey, TCountryDateFormat>
        where TTimeZoneManager : DaTimeZoneManager<TKey, TNullableKey, TTimeZone>
        where TTimeZone : DaTimeZone<TKey, TCountryTimeZone>
        where TSystemLanguageManager : DaSystemLanguageManager<TKey, TSystemLanguage>
        where TSystemLanguage : DaSystemLanguage<TKey, TCountrySystemLanguage>
    {
        public DaAuthorizationFacade(TUserManager userManager, TRoleManager roleManager, TTenantManager tenantManager, TAppManager appManager, TFeatureManager featureManager, TUserAgreementManager userAgreementManager, TSubscriptionPlanManager subscriptionPlanManager, TSubscriptionManager subscriptionManager, TCurrencyManager currencyManager, TCountryManager countryManager, TTimeZoneManager timeZoneManager, TSystemLanguageManager systemLanguageManager)
        {
            UserManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            RoleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            TenantManager = tenantManager ?? throw new ArgumentNullException(nameof(tenantManager));
            AppManager = appManager ?? throw new ArgumentNullException(nameof(appManager));
            FeatureManager = featureManager ?? throw new ArgumentNullException(nameof(featureManager));
            UserAgreementManager = userAgreementManager ?? throw new ArgumentNullException(nameof(userAgreementManager));
            SubscriptionManager = subscriptionManager ?? throw new ArgumentNullException(nameof(subscriptionManager));
            SubscriptionPlanManager = subscriptionPlanManager ?? throw new ArgumentNullException(nameof(subscriptionPlanManager));
            CurrencyManager = currencyManager ?? throw new ArgumentNullException(nameof(currencyManager));
            CountryManager = countryManager ?? throw new ArgumentNullException(nameof(countryManager));
            TimeZoneManager = timeZoneManager ?? throw new ArgumentNullException(nameof(timeZoneManager));
            SystemLanguageManager = systemLanguageManager ?? throw new ArgumentNullException(nameof(systemLanguageManager));
        }

        public TUserManager UserManager
        {
            get;
            private set;
        }

        public TRoleManager RoleManager
        {
            get;
            private set;
        }

        public TTenantManager TenantManager
        {
            get;
            private set;
        }

        public TSubscriptionManager SubscriptionManager
        {
            get;
            private set;
        }

        public TAppManager AppManager
        {
            get;
            private set;
        }

        public TFeatureManager FeatureManager
        {
            get;
            private set;
        }

        public TUserAgreementManager UserAgreementManager
        {
            get;
            private set;
        }

        public TSubscriptionPlanManager SubscriptionPlanManager
        {
            get;
            private set;
        }

        public TCurrencyManager CurrencyManager
        {
            get;
            private set;
        }

        public TCountryManager CountryManager
        {
            get;
            private set;
        }

        public TTimeZoneManager TimeZoneManager
        {
            get;
            private set;
        }

        public TSystemLanguageManager SystemLanguageManager
        {
            get;
            private set;
        }


        public List<TAuthorizedFeatureInfo> GetAuthorizedFeatures(TKey userId, TKey subscriptionId)
        {
            return DaAsyncHelper.RunSync<List<TAuthorizedFeatureInfo>>(() => GetAuthorizedFeaturesAsync(userId, subscriptionId));
        }

        public virtual async Task<List<TAuthorizedFeatureInfo>> GetAuthorizedFeaturesAsync(TKey userId, TKey subscriptionId)
        {
            var user = await UserManager.FindByIdAsync(userId.ToString());

            if (user == null)
            {
                return null;
            }

            if (user.Status != DaAccountStatus.Active)
            {
                return null;
            }

            var subscription = await SubscriptionManager.FindByIdAsync(subscriptionId);

            if (subscription == null)
            {
                return null;
            }

            if (!subscription.IsActive)
            {
                return null;
            }

            var tenants = await TenantManager.FindByUserIdAsync(userId);
            TTenant tenant = null;

            for (var i = 0; i < tenants.Count; i++)
            {
                if (tenants[i].Id.Equals(subscription.TenantId))
                {
                    if (tenants[i].Status == DaTenantStatus.Active)
                    {
                        tenant = tenants[i];
                    }

                    break;
                }
            }

            if (tenant == null)
            {
                return null;
            }

            if (subscription.SubscriptionFeatures == null || subscription.SubscriptionFeatures.Count <= 0)
            {
                return null;
            }

            var result = new List<TAuthorizedFeatureInfo>();

            foreach (var feature in subscription.SubscriptionFeatures)
            {
                var subscriptionFeatureInfo = new TAuthorizedFeatureInfo();
                subscriptionFeatureInfo.Id = feature.Feature.Id;
                subscriptionFeatureInfo.Name = feature.Feature.Name;
                subscriptionFeatureInfo.Key = feature.Feature.Key;

                result.Add(subscriptionFeatureInfo);
            }

            return result;
        }

        public DaAuthorizationResult<TKey, TAuthorizationInfo, TAuthorizedActionInfo> Authorize(TKey userId, TKey subscriptionId, string featureKey)
        {
            return DaAsyncHelper.RunSync<DaAuthorizationResult<TKey, TAuthorizationInfo, TAuthorizedActionInfo>>(() => AuthorizeAsync(userId, subscriptionId, featureKey));
        }

        public virtual async Task<DaAuthorizationResult<TKey, TAuthorizationInfo, TAuthorizedActionInfo>> AuthorizeAsync(TKey userId, TKey subscriptionId, string featureKey)
        {
            var user = await UserManager.FindByIdAsync(userId.ToString());

            if (user == null)
            {
                throw new ArgumentException("Invalid user ID.");
            }

            if (user.Status != DaAccountStatus.Active)
            {
                return new DaAuthorizationResult<TKey, TAuthorizationInfo, TAuthorizedActionInfo>(DaAuthorizationStatus.UserAccountNotActive);
            }

            var subscription = await SubscriptionManager.FindByIdAsync(subscriptionId);

            if (subscription == null)
            {
                throw new ArgumentException("Invalid subscription ID.");
            }

            if (!subscription.IsActive)
            {
                return new DaAuthorizationResult<TKey, TAuthorizationInfo, TAuthorizedActionInfo>(DaAuthorizationStatus.SubscriptionNotActive);
            }

            var tenants = await TenantManager.FindByUserIdAsync(userId);
            TTenant tenant = null;

            for (var i = 0; i < tenants.Count; i++)
            {
                if (tenants[i].Id.Equals(subscription.TenantId))
                {
                    if (tenants[i].Status == DaTenantStatus.Active)
                    {
                        tenant = tenants[i];
                    }

                    break;
                }
            }

            if (tenant == null)
            {
                return new DaAuthorizationResult<TKey, TAuthorizationInfo, TAuthorizedActionInfo>(DaAuthorizationStatus.NotAssociatedWithAnActiveTenant);
            }

            var feature = subscription.SubscriptionFeatures.Where(m => m.Feature.Key == featureKey).SingleOrDefault();

            if (feature == null)
            {
                return new DaAuthorizationResult<TKey, TAuthorizationInfo, TAuthorizedActionInfo>(DaAuthorizationStatus.FeatureNotFoundInSubscription);
            }

            var subscriptionFeatureUser = feature.SubscriptionFeatureUsers.Where(m => m.UserId.Equals(userId)).SingleOrDefault();

            if (subscriptionFeatureUser != null)
            {
                if (!subscriptionFeatureUser.IsEnabled)
                {
                    return new DaAuthorizationResult<TKey, TAuthorizationInfo, TAuthorizedActionInfo>(DaAuthorizationStatus.FeatureDenidToUser);
                }
            }

            var authorizationInfo = new TAuthorizationInfo();
            authorizationInfo.Id = feature.Id;
            authorizationInfo.Key = feature.Feature.Key;

            var authorizedActions = new List<TAuthorizedActionInfo>();

            if (feature.SubscriptionFeatureRoles != null && feature.SubscriptionFeatureRoles.Count > 0)
            {
                foreach (var roleName in await UserManager.GetRolesAsync(user))
                {
                    var role = await RoleManager.FindByNameAsync(roleName);
                    var featureRoles = feature.SubscriptionFeatureRoles.Where(m => m.RoleId.Equals(role.Id)).ToList();

                    foreach (var featureRole in featureRoles)
                    {
                        foreach (var action in featureRole.SubscriptionFeatureRoleActions)
                        {
                            var currentAction = authorizedActions.Where(m => m.ActionName == action.ActionName).SingleOrDefault();

                            if (currentAction == null)
                            {
                                authorizedActions.Add(new TAuthorizedActionInfo()
                                {
                                    ActionName = action.ActionName,
                                    Allowed = action.Allowed
                                });
                            }
                            else
                            {
                                if (currentAction.Allowed != false)
                                {
                                    if (currentAction.Allowed == null)
                                    {
                                        currentAction.Allowed = action.Allowed;
                                    }
                                    else
                                    {
                                        if (action.Allowed != null)
                                        {
                                            currentAction.Allowed = action.Allowed;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (feature.SubscriptionFeatureUsers != null && feature.SubscriptionFeatureUsers.Count > 0)
            {
                var featureUsers = feature.SubscriptionFeatureUsers.Where(m => m.UserId.Equals(userId)).ToList();

                foreach (var featureUser in featureUsers)
                {
                    foreach (var action in featureUser.SubscriptionFeatureUserActions)
                    {
                        var currentAction = authorizedActions.Where(m => m.ActionName == action.ActionName).SingleOrDefault();

                        if (currentAction == null)
                        {
                            authorizedActions.Add(new TAuthorizedActionInfo()
                            {
                                ActionName = action.ActionName,
                                Allowed = action.Allowed
                            });
                        }
                        else
                        {
                            if (currentAction.Allowed != false)
                            {
                                if (currentAction.Allowed == null)
                                {
                                    currentAction.Allowed = action.Allowed;
                                }
                                else
                                {
                                    if (action.Allowed != null)
                                    {
                                        currentAction.Allowed = action.Allowed;
                                    }
                                }
                            }
                        }
                    }
                }

                authorizationInfo.Actions = authorizedActions;
            }

            return new DaAuthorizationResult<TKey, TAuthorizationInfo, TAuthorizedActionInfo>(authorizationInfo); ;
        }
    }
}
