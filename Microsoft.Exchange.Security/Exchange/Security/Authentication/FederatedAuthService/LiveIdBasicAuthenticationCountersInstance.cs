using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Security.Authentication.FederatedAuthService
{
	// Token: 0x02000008 RID: 8
	internal sealed class LiveIdBasicAuthenticationCountersInstance : PerformanceCounterInstance
	{
		// Token: 0x0600001B RID: 27 RVA: 0x000029F4 File Offset: 0x00000BF4
		internal LiveIdBasicAuthenticationCountersInstance(string instanceName, LiveIdBasicAuthenticationCountersInstance autoUpdateTotalInstance) : base(instanceName, "MSExchange LiveIdBasicAuthentication")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.NumberOfCurrentRequests = new ExPerformanceCounter(base.CategoryName, "Current Requests Pending", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfCurrentRequests);
				ExPerformanceCounter exPerformanceCounter = new ExPerformanceCounter(base.CategoryName, "Auth Requests/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter);
				this.NumberOfAuthRequests = new ExPerformanceCounter(base.CategoryName, "Total Auth Requests", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter
				});
				list.Add(this.NumberOfAuthRequests);
				ExPerformanceCounter exPerformanceCounter2 = new ExPerformanceCounter(base.CategoryName, "Successful Auth/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter2);
				this.NumberOfSuccessfulAuthentications = new ExPerformanceCounter(base.CategoryName, "Successful Auth Total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter2
				});
				list.Add(this.NumberOfSuccessfulAuthentications);
				ExPerformanceCounter exPerformanceCounter3 = new ExPerformanceCounter(base.CategoryName, "Invalid Credentials/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter3);
				this.NumberOfInvalidCredentials = new ExPerformanceCounter(base.CategoryName, "Invalid Credentials Total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter3
				});
				list.Add(this.NumberOfInvalidCredentials);
				ExPerformanceCounter exPerformanceCounter4 = new ExPerformanceCounter(base.CategoryName, "Failed Auth/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter4);
				this.NumberOfFailedAuthentications = new ExPerformanceCounter(base.CategoryName, "Failed Auth Total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter4
				});
				list.Add(this.NumberOfFailedAuthentications);
				ExPerformanceCounter exPerformanceCounter5 = new ExPerformanceCounter(base.CategoryName, "Failed User Recoverable Auth/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter5);
				this.NumberOfFailedRecoverableAuthentications = new ExPerformanceCounter(base.CategoryName, "Failed User Recoverable Auth Total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter5
				});
				list.Add(this.NumberOfFailedRecoverableAuthentications);
				ExPerformanceCounter exPerformanceCounter6 = new ExPerformanceCounter(base.CategoryName, "Failed Requests/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter6);
				this.NumberOfFailedRequests = new ExPerformanceCounter(base.CategoryName, "Failed Requests Total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter6
				});
				list.Add(this.NumberOfFailedRequests);
				ExPerformanceCounter exPerformanceCounter7 = new ExPerformanceCounter(base.CategoryName, "Timed Out Requests/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter7);
				this.NumberOfTimedOutRequests = new ExPerformanceCounter(base.CategoryName, "Timed Out Requests Total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter7
				});
				list.Add(this.NumberOfTimedOutRequests);
				ExPerformanceCounter exPerformanceCounter8 = new ExPerformanceCounter(base.CategoryName, "Application Password Requests/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter8);
				this.NumberOfAppPasswords = new ExPerformanceCounter(base.CategoryName, "Application Password Requests Total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter8
				});
				list.Add(this.NumberOfAppPasswords);
				this.AverageResponseTime = new ExPerformanceCounter(base.CategoryName, "Average Auth Time", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageResponseTime);
				this.AverageResponseTimeBase = new ExPerformanceCounter(base.CategoryName, "Base for Average Auth Time", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageResponseTimeBase);
				this.NumberOfRequestsLastMinute = new ExPerformanceCounter(base.CategoryName, "Requests Last Minute Total", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfRequestsLastMinute);
				this.NumberOfOrgIdRequestsLastMinute = new ExPerformanceCounter(base.CategoryName, "OrgId Requests Last Minute Total", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfOrgIdRequestsLastMinute);
				this.PercentageFailedAuthenticationsLastMinute = new ExPerformanceCounter(base.CategoryName, "Failed Authentications Percentage Last Minute Total", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PercentageFailedAuthenticationsLastMinute);
				this.PercentageFailedRequestsLastMinute = new ExPerformanceCounter(base.CategoryName, "Failed Requests Percentage Last Minute Total", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PercentageFailedRequestsLastMinute);
				this.PercentageTimedOutRequestsLastMinute = new ExPerformanceCounter(base.CategoryName, "Timed Out Requests Percentage Last Minute Total", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PercentageTimedOutRequestsLastMinute);
				this.NumberOfCachedRequests = new ExPerformanceCounter(base.CategoryName, "Cached Auth Requests", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfCachedRequests);
				this.LogonCacheHit = new ExPerformanceCounter(base.CategoryName, "Logon Cache hit percentage", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.LogonCacheHit);
				this.LogonCacheSize = new ExPerformanceCounter(base.CategoryName, "Logon Cache Size", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.LogonCacheSize);
				this.InvalidCredCacheSize = new ExPerformanceCounter(base.CategoryName, "Invalid Cred Cache Size", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.InvalidCredCacheSize);
				this.AdUserCacheHit = new ExPerformanceCounter(base.CategoryName, "User Entry Cache Hit Percentage", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AdUserCacheHit);
				this.AdUserCacheSize = new ExPerformanceCounter(base.CategoryName, "User Entry Cache Size", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AdUserCacheSize);
				this.AverageAdResponseTime = new ExPerformanceCounter(base.CategoryName, "AD Average Auth Time", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageAdResponseTime);
				this.AverageAdResponseTimeBase = new ExPerformanceCounter(base.CategoryName, "Base for AverageADResponseTime", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageAdResponseTimeBase);
				ExPerformanceCounter exPerformanceCounter9 = new ExPerformanceCounter(base.CategoryName, "AD Auth Requests/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter9);
				this.NumberOfAdRequests = new ExPerformanceCounter(base.CategoryName, "AD Auth Requests", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter9
				});
				list.Add(this.NumberOfAdRequests);
				ExPerformanceCounter exPerformanceCounter10 = new ExPerformanceCounter(base.CategoryName, "AD Access Requests/sec for OfflineOrgId", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter10);
				this.NumberOfAdRequestForOfflineOrgId = new ExPerformanceCounter(base.CategoryName, "AD Access Requests for OfflineOrgId", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter10
				});
				list.Add(this.NumberOfAdRequestForOfflineOrgId);
				ExPerformanceCounter exPerformanceCounter11 = new ExPerformanceCounter(base.CategoryName, "AD Failed Auth Requests/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter11);
				this.FailedAdRequests = new ExPerformanceCounter(base.CategoryName, "AD Failed Auth Requests", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter11
				});
				list.Add(this.FailedAdRequests);
				ExPerformanceCounter exPerformanceCounter12 = new ExPerformanceCounter(base.CategoryName, "AD Password Synchronizations/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter12);
				this.AdPasswordSyncs = new ExPerformanceCounter(base.CategoryName, "AD Password Synchronizations", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter12
				});
				list.Add(this.AdPasswordSyncs);
				ExPerformanceCounter exPerformanceCounter13 = new ExPerformanceCounter(base.CategoryName, "AD UPN Synchronizations/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter13);
				this.AdUpnSyncs = new ExPerformanceCounter(base.CategoryName, "AD UPN Synchronizations", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter13
				});
				list.Add(this.AdUpnSyncs);
				this.AverageHrdResponseTime = new ExPerformanceCounter(base.CategoryName, "Average Windows Live Home Realm Discovery Response Time", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageHrdResponseTime);
				this.AverageHrdResponseTimeBase = new ExPerformanceCounter(base.CategoryName, "Base for AverageHrdResponseTimeAverage", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageHrdResponseTimeBase);
				this.HrdCacheHit = new ExPerformanceCounter(base.CategoryName, "Cache hit percentage - Home Realm Discovery", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.HrdCacheHit);
				this.HrdCacheHitBase = new ExPerformanceCounter(base.CategoryName, "Base for HrdCacheHit", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.HrdCacheHitBase);
				this.HrdCacheSize = new ExPerformanceCounter(base.CategoryName, "Home Realm Discovery Domain Cache Size", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.HrdCacheSize);
				ExPerformanceCounter exPerformanceCounter14 = new ExPerformanceCounter(base.CategoryName, "Home Realm Discovery Requests/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter14);
				this.NumberOfOutgoingHrdRequests = new ExPerformanceCounter(base.CategoryName, "Home Realm Discovery Requests Total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter14
				});
				list.Add(this.NumberOfOutgoingHrdRequests);
				ExPerformanceCounter exPerformanceCounter15 = new ExPerformanceCounter(base.CategoryName, "Home Realm Discovery Requests/sec to AD", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter15);
				this.NumberOfADHrdRequests = new ExPerformanceCounter(base.CategoryName, "Home Realm Discovery Requests Total to AD", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter15
				});
				list.Add(this.NumberOfADHrdRequests);
				ExPerformanceCounter exPerformanceCounter16 = new ExPerformanceCounter(base.CategoryName, "Failed Home Realm Discovery Requests/sec to AD", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter16);
				this.NumberOfFailedADHrdRequests = new ExPerformanceCounter(base.CategoryName, "Failed Home Realm Discovery Requests Total to AD", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter16
				});
				list.Add(this.NumberOfFailedADHrdRequests);
				ExPerformanceCounter exPerformanceCounter17 = new ExPerformanceCounter(base.CategoryName, "Home Realm Discovery Record Update per second in AD", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter17);
				this.NumberOfADHrdUpdate = new ExPerformanceCounter(base.CategoryName, "Home Realm Discovery Record Update Count in AD", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter17
				});
				list.Add(this.NumberOfADHrdUpdate);
				this.PendingHrdRequests = new ExPerformanceCounter(base.CategoryName, "Current Home Realm Discovery Requests Pending", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PendingHrdRequests);
				this.AverageLiveIdResponseTime = new ExPerformanceCounter(base.CategoryName, "Average Windows Live Response Time", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageLiveIdResponseTime);
				this.AverageLiveIdResponseTimeBase = new ExPerformanceCounter(base.CategoryName, "Base for Average Windows Live Response Time", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageLiveIdResponseTimeBase);
				ExPerformanceCounter exPerformanceCounter18 = new ExPerformanceCounter(base.CategoryName, "LiveID STS Requests/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter18);
				this.NumberOfLiveIdStsRequests = new ExPerformanceCounter(base.CategoryName, "LiveID STS Requests Total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter18
				});
				list.Add(this.NumberOfLiveIdStsRequests);
				this.PendingStsRequests = new ExPerformanceCounter(base.CategoryName, "Current LiveID STS Requests Pending", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PendingStsRequests);
				this.AverageMsoIdResponseTime = new ExPerformanceCounter(base.CategoryName, "Average Microsoft Online Response Time", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageMsoIdResponseTime);
				this.AverageMsoIdResponseTimeBase = new ExPerformanceCounter(base.CategoryName, "Base for Average Microsoft Online Response Time", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageMsoIdResponseTimeBase);
				ExPerformanceCounter exPerformanceCounter19 = new ExPerformanceCounter(base.CategoryName, "Microsoft Online STS Requests/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter19);
				this.NumberOfMsoIdStsRequests = new ExPerformanceCounter(base.CategoryName, "Microsoft Online STS Requests Total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter19
				});
				list.Add(this.NumberOfMsoIdStsRequests);
				this.PendingMsoIdStsRequests = new ExPerformanceCounter(base.CategoryName, "Current Microsoft Online STS Requests Pending", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PendingMsoIdStsRequests);
				this.AverageRPSCallLatency = new ExPerformanceCounter(base.CategoryName, "Average RPS Call Latency", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageRPSCallLatency);
				this.AverageRPSCallLatencyBase = new ExPerformanceCounter(base.CategoryName, "Base for Average RPS Call Latency", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageRPSCallLatencyBase);
				this.AverageFedStsResponseTime = new ExPerformanceCounter(base.CategoryName, "Average Federated STS Response Time", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageFedStsResponseTime);
				this.AverageFedStsResponseTimeBase = new ExPerformanceCounter(base.CategoryName, "Base for Average Federated STS Response Time", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageFedStsResponseTimeBase);
				ExPerformanceCounter exPerformanceCounter20 = new ExPerformanceCounter(base.CategoryName, "Federated STS Auth Requests/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter20);
				this.PendingFedStsRequests = new ExPerformanceCounter(base.CategoryName, "Current Federated STS Requests Pending", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PendingFedStsRequests);
				this.AverageSamlStsResponseTime = new ExPerformanceCounter(base.CategoryName, "Average Shibboleth STS Response Time", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageSamlStsResponseTime);
				this.AverageSamlStsResponseTimeBase = new ExPerformanceCounter(base.CategoryName, "Base for Average Shibboleth STS Response Time", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageSamlStsResponseTimeBase);
				this.NumberOfOutgoingSamlStsRequests = new ExPerformanceCounter(base.CategoryName, "Shibboleth STS Auth Requests Total", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfOutgoingSamlStsRequests);
				ExPerformanceCounter exPerformanceCounter21 = new ExPerformanceCounter(base.CategoryName, "Shibboleth STS Auth Requests/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter21);
				this.NumberOfOutgoingFedStsRequests = new ExPerformanceCounter(base.CategoryName, "Federated STS Auth Requests Total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter20,
					exPerformanceCounter21
				});
				list.Add(this.NumberOfOutgoingFedStsRequests);
				this.PendingSamlStsRequests = new ExPerformanceCounter(base.CategoryName, "Current Shibboleth STS Requests Pending", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PendingSamlStsRequests);
				ExPerformanceCounter exPerformanceCounter22 = new ExPerformanceCounter(base.CategoryName, "Failed TOU Checks/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter22);
				this.NumberOfTOUFailures = new ExPerformanceCounter(base.CategoryName, "Failed TOU Checks Total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter22
				});
				list.Add(this.NumberOfTOUFailures);
				this.NamespaceBlacklistSize = new ExPerformanceCounter(base.CategoryName, "Black-listed Namespace Count", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.NamespaceBlacklistSize);
				this.NamespaceWhitelistSize = new ExPerformanceCounter(base.CategoryName, "White-listed Namespace Count", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.NamespaceWhitelistSize);
				ExPerformanceCounter exPerformanceCounter23 = new ExPerformanceCounter(base.CategoryName, "Offline OrgId Auth/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter23);
				this.OfflineOrgIdAuthenticationCount = new ExPerformanceCounter(base.CategoryName, "Offline OrgId Authentication Count", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter23
				});
				list.Add(this.OfflineOrgIdAuthenticationCount);
				this.NumberOfOfflineOrgIdRequestsLastMinute = new ExPerformanceCounter(base.CategoryName, "OfflineOrgId Requests Last Minute Total", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfOfflineOrgIdRequestsLastMinute);
				this.OfflineOrgIdRedirectCount = new ExPerformanceCounter(base.CategoryName, "Offline OrgId Authentication Redirect Count", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.OfflineOrgIdRedirectCount);
				this.NumberOfLowConfidenceOfflineOrgIdRequests = new ExPerformanceCounter(base.CategoryName, "Low Password Confidence Auth Requests to AD", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfLowConfidenceOfflineOrgIdRequests);
				this.NumberOfFailedOfflineOrgIdRequests = new ExPerformanceCounter(base.CategoryName, "Failed Offline OrgId Auth Requests to AD", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfFailedOfflineOrgIdRequests);
				this.NumberOfOfflineOrgIdRequestWithInvalidCredential = new ExPerformanceCounter(base.CategoryName, "Offline OrgId Requests with invalid credential", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfOfflineOrgIdRequestWithInvalidCredential);
				ExPerformanceCounter exPerformanceCounter24 = new ExPerformanceCounter(base.CategoryName, "Mailbox access count/sec for last logon timestamp", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter24);
				this.NumberOfMailboxAccess = new ExPerformanceCounter(base.CategoryName, "Mailbox access for last logon timestamp", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter24
				});
				list.Add(this.NumberOfMailboxAccess);
				ExPerformanceCounter exPerformanceCounter25 = new ExPerformanceCounter(base.CategoryName, "Tenant Nego config requests/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter25);
				this.NumberOfTenantNegoRequests = new ExPerformanceCounter(base.CategoryName, "Tenant Nego config requests total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter25
				});
				list.Add(this.NumberOfTenantNegoRequests);
				this.TenantNegoCacheHit = new ExPerformanceCounter(base.CategoryName, "Cache hit percentage - Tenant Nego config", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TenantNegoCacheHit);
				this.TenantNegoCacheHitBase = new ExPerformanceCounter(base.CategoryName, "Base for TenantNegoCacheHit", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TenantNegoCacheHitBase);
				this.TenantNegoCacheSize = new ExPerformanceCounter(base.CategoryName, "Tenant Nego config cache size", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TenantNegoCacheSize);
				this.AverageMServResponseTime = new ExPerformanceCounter(base.CategoryName, "Average MServ response time", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageMServResponseTime);
				this.AverageMServResponseTimeBase = new ExPerformanceCounter(base.CategoryName, "Base for AverageMServResponseTimeAverage", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageMServResponseTimeBase);
				ExPerformanceCounter exPerformanceCounter26 = new ExPerformanceCounter(base.CategoryName, "MServ requests/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter26);
				this.NumberOfMServRequests = new ExPerformanceCounter(base.CategoryName, "MServ requests total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter26
				});
				list.Add(this.NumberOfMServRequests);
				this.PendingMServRequests = new ExPerformanceCounter(base.CategoryName, "Current MServ requests pending", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PendingMServRequests);
				ExPerformanceCounter exPerformanceCounter27 = new ExPerformanceCounter(base.CategoryName, "Failed MServ requests/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter27);
				this.FailedMServRequests = new ExPerformanceCounter(base.CategoryName, "Failed MServ requests total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter27
				});
				list.Add(this.FailedMServRequests);
				ExPerformanceCounter exPerformanceCounter28 = new ExPerformanceCounter(base.CategoryName, "Cookie Based Auth requests/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter28);
				this.CookieBasedAuthRequests = new ExPerformanceCounter(base.CategoryName, "Cookie Based Auth requests total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter28
				});
				list.Add(this.CookieBasedAuthRequests);
				this.PercentageOfCookieBasedAuth = new ExPerformanceCounter(base.CategoryName, "Percentage of cookie based auth", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PercentageOfCookieBasedAuth);
				ExPerformanceCounter exPerformanceCounter29 = new ExPerformanceCounter(base.CategoryName, "Expired Cookie Based Auth requests/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter29);
				this.ExpiredCookieAuthRequests = new ExPerformanceCounter(base.CategoryName, "Total auth requests with expired cookie", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter29
				});
				list.Add(this.ExpiredCookieAuthRequests);
				ExPerformanceCounter exPerformanceCounter30 = new ExPerformanceCounter(base.CategoryName, "Failed Cookie Based Auth requests/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter30);
				this.FailedCookieAuthRequests = new ExPerformanceCounter(base.CategoryName, "Total failed auth requests with cookie", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter30
				});
				list.Add(this.FailedCookieAuthRequests);
				ExPerformanceCounter exPerformanceCounter31 = new ExPerformanceCounter(base.CategoryName, "Remote Auth requests/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter31);
				this.RemoteAuthRequests = new ExPerformanceCounter(base.CategoryName, "Total remote auth requests", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter31
				});
				list.Add(this.RemoteAuthRequests);
				long num = this.NumberOfCurrentRequests.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter32 in list)
					{
						exPerformanceCounter32.Close();
					}
				}
			}
			this.counters = list.ToArray();
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00003D94 File Offset: 0x00001F94
		internal LiveIdBasicAuthenticationCountersInstance(string instanceName) : base(instanceName, "MSExchange LiveIdBasicAuthentication")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.NumberOfCurrentRequests = new ExPerformanceCounter(base.CategoryName, "Current Requests Pending", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfCurrentRequests);
				ExPerformanceCounter exPerformanceCounter = new ExPerformanceCounter(base.CategoryName, "Auth Requests/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter);
				this.NumberOfAuthRequests = new ExPerformanceCounter(base.CategoryName, "Total Auth Requests", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter
				});
				list.Add(this.NumberOfAuthRequests);
				ExPerformanceCounter exPerformanceCounter2 = new ExPerformanceCounter(base.CategoryName, "Successful Auth/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter2);
				this.NumberOfSuccessfulAuthentications = new ExPerformanceCounter(base.CategoryName, "Successful Auth Total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter2
				});
				list.Add(this.NumberOfSuccessfulAuthentications);
				ExPerformanceCounter exPerformanceCounter3 = new ExPerformanceCounter(base.CategoryName, "Invalid Credentials/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter3);
				this.NumberOfInvalidCredentials = new ExPerformanceCounter(base.CategoryName, "Invalid Credentials Total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter3
				});
				list.Add(this.NumberOfInvalidCredentials);
				ExPerformanceCounter exPerformanceCounter4 = new ExPerformanceCounter(base.CategoryName, "Failed Auth/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter4);
				this.NumberOfFailedAuthentications = new ExPerformanceCounter(base.CategoryName, "Failed Auth Total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter4
				});
				list.Add(this.NumberOfFailedAuthentications);
				ExPerformanceCounter exPerformanceCounter5 = new ExPerformanceCounter(base.CategoryName, "Failed User Recoverable Auth/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter5);
				this.NumberOfFailedRecoverableAuthentications = new ExPerformanceCounter(base.CategoryName, "Failed User Recoverable Auth Total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter5
				});
				list.Add(this.NumberOfFailedRecoverableAuthentications);
				ExPerformanceCounter exPerformanceCounter6 = new ExPerformanceCounter(base.CategoryName, "Failed Requests/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter6);
				this.NumberOfFailedRequests = new ExPerformanceCounter(base.CategoryName, "Failed Requests Total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter6
				});
				list.Add(this.NumberOfFailedRequests);
				ExPerformanceCounter exPerformanceCounter7 = new ExPerformanceCounter(base.CategoryName, "Timed Out Requests/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter7);
				this.NumberOfTimedOutRequests = new ExPerformanceCounter(base.CategoryName, "Timed Out Requests Total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter7
				});
				list.Add(this.NumberOfTimedOutRequests);
				ExPerformanceCounter exPerformanceCounter8 = new ExPerformanceCounter(base.CategoryName, "Application Password Requests/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter8);
				this.NumberOfAppPasswords = new ExPerformanceCounter(base.CategoryName, "Application Password Requests Total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter8
				});
				list.Add(this.NumberOfAppPasswords);
				this.AverageResponseTime = new ExPerformanceCounter(base.CategoryName, "Average Auth Time", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageResponseTime);
				this.AverageResponseTimeBase = new ExPerformanceCounter(base.CategoryName, "Base for Average Auth Time", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageResponseTimeBase);
				this.NumberOfRequestsLastMinute = new ExPerformanceCounter(base.CategoryName, "Requests Last Minute Total", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfRequestsLastMinute);
				this.NumberOfOrgIdRequestsLastMinute = new ExPerformanceCounter(base.CategoryName, "OrgId Requests Last Minute Total", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfOrgIdRequestsLastMinute);
				this.PercentageFailedAuthenticationsLastMinute = new ExPerformanceCounter(base.CategoryName, "Failed Authentications Percentage Last Minute Total", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PercentageFailedAuthenticationsLastMinute);
				this.PercentageFailedRequestsLastMinute = new ExPerformanceCounter(base.CategoryName, "Failed Requests Percentage Last Minute Total", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PercentageFailedRequestsLastMinute);
				this.PercentageTimedOutRequestsLastMinute = new ExPerformanceCounter(base.CategoryName, "Timed Out Requests Percentage Last Minute Total", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PercentageTimedOutRequestsLastMinute);
				this.NumberOfCachedRequests = new ExPerformanceCounter(base.CategoryName, "Cached Auth Requests", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfCachedRequests);
				this.LogonCacheHit = new ExPerformanceCounter(base.CategoryName, "Logon Cache hit percentage", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.LogonCacheHit);
				this.LogonCacheSize = new ExPerformanceCounter(base.CategoryName, "Logon Cache Size", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.LogonCacheSize);
				this.InvalidCredCacheSize = new ExPerformanceCounter(base.CategoryName, "Invalid Cred Cache Size", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.InvalidCredCacheSize);
				this.AdUserCacheHit = new ExPerformanceCounter(base.CategoryName, "User Entry Cache Hit Percentage", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AdUserCacheHit);
				this.AdUserCacheSize = new ExPerformanceCounter(base.CategoryName, "User Entry Cache Size", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AdUserCacheSize);
				this.AverageAdResponseTime = new ExPerformanceCounter(base.CategoryName, "AD Average Auth Time", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageAdResponseTime);
				this.AverageAdResponseTimeBase = new ExPerformanceCounter(base.CategoryName, "Base for AverageADResponseTime", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageAdResponseTimeBase);
				ExPerformanceCounter exPerformanceCounter9 = new ExPerformanceCounter(base.CategoryName, "AD Auth Requests/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter9);
				this.NumberOfAdRequests = new ExPerformanceCounter(base.CategoryName, "AD Auth Requests", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter9
				});
				list.Add(this.NumberOfAdRequests);
				ExPerformanceCounter exPerformanceCounter10 = new ExPerformanceCounter(base.CategoryName, "AD Access Requests/sec for OfflineOrgId", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter10);
				this.NumberOfAdRequestForOfflineOrgId = new ExPerformanceCounter(base.CategoryName, "AD Access Requests for OfflineOrgId", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter10
				});
				list.Add(this.NumberOfAdRequestForOfflineOrgId);
				ExPerformanceCounter exPerformanceCounter11 = new ExPerformanceCounter(base.CategoryName, "AD Failed Auth Requests/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter11);
				this.FailedAdRequests = new ExPerformanceCounter(base.CategoryName, "AD Failed Auth Requests", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter11
				});
				list.Add(this.FailedAdRequests);
				ExPerformanceCounter exPerformanceCounter12 = new ExPerformanceCounter(base.CategoryName, "AD Password Synchronizations/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter12);
				this.AdPasswordSyncs = new ExPerformanceCounter(base.CategoryName, "AD Password Synchronizations", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter12
				});
				list.Add(this.AdPasswordSyncs);
				ExPerformanceCounter exPerformanceCounter13 = new ExPerformanceCounter(base.CategoryName, "AD UPN Synchronizations/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter13);
				this.AdUpnSyncs = new ExPerformanceCounter(base.CategoryName, "AD UPN Synchronizations", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter13
				});
				list.Add(this.AdUpnSyncs);
				this.AverageHrdResponseTime = new ExPerformanceCounter(base.CategoryName, "Average Windows Live Home Realm Discovery Response Time", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageHrdResponseTime);
				this.AverageHrdResponseTimeBase = new ExPerformanceCounter(base.CategoryName, "Base for AverageHrdResponseTimeAverage", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageHrdResponseTimeBase);
				this.HrdCacheHit = new ExPerformanceCounter(base.CategoryName, "Cache hit percentage - Home Realm Discovery", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.HrdCacheHit);
				this.HrdCacheHitBase = new ExPerformanceCounter(base.CategoryName, "Base for HrdCacheHit", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.HrdCacheHitBase);
				this.HrdCacheSize = new ExPerformanceCounter(base.CategoryName, "Home Realm Discovery Domain Cache Size", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.HrdCacheSize);
				ExPerformanceCounter exPerformanceCounter14 = new ExPerformanceCounter(base.CategoryName, "Home Realm Discovery Requests/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter14);
				this.NumberOfOutgoingHrdRequests = new ExPerformanceCounter(base.CategoryName, "Home Realm Discovery Requests Total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter14
				});
				list.Add(this.NumberOfOutgoingHrdRequests);
				ExPerformanceCounter exPerformanceCounter15 = new ExPerformanceCounter(base.CategoryName, "Home Realm Discovery Requests/sec to AD", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter15);
				this.NumberOfADHrdRequests = new ExPerformanceCounter(base.CategoryName, "Home Realm Discovery Requests Total to AD", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter15
				});
				list.Add(this.NumberOfADHrdRequests);
				ExPerformanceCounter exPerformanceCounter16 = new ExPerformanceCounter(base.CategoryName, "Failed Home Realm Discovery Requests/sec to AD", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter16);
				this.NumberOfFailedADHrdRequests = new ExPerformanceCounter(base.CategoryName, "Failed Home Realm Discovery Requests Total to AD", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter16
				});
				list.Add(this.NumberOfFailedADHrdRequests);
				ExPerformanceCounter exPerformanceCounter17 = new ExPerformanceCounter(base.CategoryName, "Home Realm Discovery Record Update per second in AD", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter17);
				this.NumberOfADHrdUpdate = new ExPerformanceCounter(base.CategoryName, "Home Realm Discovery Record Update Count in AD", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter17
				});
				list.Add(this.NumberOfADHrdUpdate);
				this.PendingHrdRequests = new ExPerformanceCounter(base.CategoryName, "Current Home Realm Discovery Requests Pending", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PendingHrdRequests);
				this.AverageLiveIdResponseTime = new ExPerformanceCounter(base.CategoryName, "Average Windows Live Response Time", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageLiveIdResponseTime);
				this.AverageLiveIdResponseTimeBase = new ExPerformanceCounter(base.CategoryName, "Base for Average Windows Live Response Time", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageLiveIdResponseTimeBase);
				ExPerformanceCounter exPerformanceCounter18 = new ExPerformanceCounter(base.CategoryName, "LiveID STS Requests/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter18);
				this.NumberOfLiveIdStsRequests = new ExPerformanceCounter(base.CategoryName, "LiveID STS Requests Total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter18
				});
				list.Add(this.NumberOfLiveIdStsRequests);
				this.PendingStsRequests = new ExPerformanceCounter(base.CategoryName, "Current LiveID STS Requests Pending", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PendingStsRequests);
				this.AverageMsoIdResponseTime = new ExPerformanceCounter(base.CategoryName, "Average Microsoft Online Response Time", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageMsoIdResponseTime);
				this.AverageMsoIdResponseTimeBase = new ExPerformanceCounter(base.CategoryName, "Base for Average Microsoft Online Response Time", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageMsoIdResponseTimeBase);
				ExPerformanceCounter exPerformanceCounter19 = new ExPerformanceCounter(base.CategoryName, "Microsoft Online STS Requests/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter19);
				this.NumberOfMsoIdStsRequests = new ExPerformanceCounter(base.CategoryName, "Microsoft Online STS Requests Total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter19
				});
				list.Add(this.NumberOfMsoIdStsRequests);
				this.PendingMsoIdStsRequests = new ExPerformanceCounter(base.CategoryName, "Current Microsoft Online STS Requests Pending", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PendingMsoIdStsRequests);
				this.AverageRPSCallLatency = new ExPerformanceCounter(base.CategoryName, "Average RPS Call Latency", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageRPSCallLatency);
				this.AverageRPSCallLatencyBase = new ExPerformanceCounter(base.CategoryName, "Base for Average RPS Call Latency", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageRPSCallLatencyBase);
				this.AverageFedStsResponseTime = new ExPerformanceCounter(base.CategoryName, "Average Federated STS Response Time", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageFedStsResponseTime);
				this.AverageFedStsResponseTimeBase = new ExPerformanceCounter(base.CategoryName, "Base for Average Federated STS Response Time", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageFedStsResponseTimeBase);
				ExPerformanceCounter exPerformanceCounter20 = new ExPerformanceCounter(base.CategoryName, "Federated STS Auth Requests/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter20);
				this.PendingFedStsRequests = new ExPerformanceCounter(base.CategoryName, "Current Federated STS Requests Pending", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PendingFedStsRequests);
				this.AverageSamlStsResponseTime = new ExPerformanceCounter(base.CategoryName, "Average Shibboleth STS Response Time", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageSamlStsResponseTime);
				this.AverageSamlStsResponseTimeBase = new ExPerformanceCounter(base.CategoryName, "Base for Average Shibboleth STS Response Time", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageSamlStsResponseTimeBase);
				this.NumberOfOutgoingSamlStsRequests = new ExPerformanceCounter(base.CategoryName, "Shibboleth STS Auth Requests Total", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfOutgoingSamlStsRequests);
				ExPerformanceCounter exPerformanceCounter21 = new ExPerformanceCounter(base.CategoryName, "Shibboleth STS Auth Requests/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter21);
				this.NumberOfOutgoingFedStsRequests = new ExPerformanceCounter(base.CategoryName, "Federated STS Auth Requests Total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter20,
					exPerformanceCounter21
				});
				list.Add(this.NumberOfOutgoingFedStsRequests);
				this.PendingSamlStsRequests = new ExPerformanceCounter(base.CategoryName, "Current Shibboleth STS Requests Pending", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PendingSamlStsRequests);
				ExPerformanceCounter exPerformanceCounter22 = new ExPerformanceCounter(base.CategoryName, "Failed TOU Checks/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter22);
				this.NumberOfTOUFailures = new ExPerformanceCounter(base.CategoryName, "Failed TOU Checks Total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter22
				});
				list.Add(this.NumberOfTOUFailures);
				this.NamespaceBlacklistSize = new ExPerformanceCounter(base.CategoryName, "Black-listed Namespace Count", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.NamespaceBlacklistSize);
				this.NamespaceWhitelistSize = new ExPerformanceCounter(base.CategoryName, "White-listed Namespace Count", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.NamespaceWhitelistSize);
				ExPerformanceCounter exPerformanceCounter23 = new ExPerformanceCounter(base.CategoryName, "Offline OrgId Auth/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter23);
				this.OfflineOrgIdAuthenticationCount = new ExPerformanceCounter(base.CategoryName, "Offline OrgId Authentication Count", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter23
				});
				list.Add(this.OfflineOrgIdAuthenticationCount);
				this.NumberOfOfflineOrgIdRequestsLastMinute = new ExPerformanceCounter(base.CategoryName, "OfflineOrgId Requests Last Minute Total", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfOfflineOrgIdRequestsLastMinute);
				this.OfflineOrgIdRedirectCount = new ExPerformanceCounter(base.CategoryName, "Offline OrgId Authentication Redirect Count", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.OfflineOrgIdRedirectCount);
				this.NumberOfLowConfidenceOfflineOrgIdRequests = new ExPerformanceCounter(base.CategoryName, "Low Password Confidence Auth Requests to AD", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfLowConfidenceOfflineOrgIdRequests);
				this.NumberOfFailedOfflineOrgIdRequests = new ExPerformanceCounter(base.CategoryName, "Failed Offline OrgId Auth Requests to AD", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfFailedOfflineOrgIdRequests);
				this.NumberOfOfflineOrgIdRequestWithInvalidCredential = new ExPerformanceCounter(base.CategoryName, "Offline OrgId Requests with invalid credential", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfOfflineOrgIdRequestWithInvalidCredential);
				ExPerformanceCounter exPerformanceCounter24 = new ExPerformanceCounter(base.CategoryName, "Mailbox access count/sec for last logon timestamp", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter24);
				this.NumberOfMailboxAccess = new ExPerformanceCounter(base.CategoryName, "Mailbox access for last logon timestamp", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter24
				});
				list.Add(this.NumberOfMailboxAccess);
				ExPerformanceCounter exPerformanceCounter25 = new ExPerformanceCounter(base.CategoryName, "Tenant Nego config requests/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter25);
				this.NumberOfTenantNegoRequests = new ExPerformanceCounter(base.CategoryName, "Tenant Nego config requests total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter25
				});
				list.Add(this.NumberOfTenantNegoRequests);
				this.TenantNegoCacheHit = new ExPerformanceCounter(base.CategoryName, "Cache hit percentage - Tenant Nego config", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TenantNegoCacheHit);
				this.TenantNegoCacheHitBase = new ExPerformanceCounter(base.CategoryName, "Base for TenantNegoCacheHit", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TenantNegoCacheHitBase);
				this.TenantNegoCacheSize = new ExPerformanceCounter(base.CategoryName, "Tenant Nego config cache size", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TenantNegoCacheSize);
				this.AverageMServResponseTime = new ExPerformanceCounter(base.CategoryName, "Average MServ response time", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageMServResponseTime);
				this.AverageMServResponseTimeBase = new ExPerformanceCounter(base.CategoryName, "Base for AverageMServResponseTimeAverage", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageMServResponseTimeBase);
				ExPerformanceCounter exPerformanceCounter26 = new ExPerformanceCounter(base.CategoryName, "MServ requests/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter26);
				this.NumberOfMServRequests = new ExPerformanceCounter(base.CategoryName, "MServ requests total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter26
				});
				list.Add(this.NumberOfMServRequests);
				this.PendingMServRequests = new ExPerformanceCounter(base.CategoryName, "Current MServ requests pending", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PendingMServRequests);
				ExPerformanceCounter exPerformanceCounter27 = new ExPerformanceCounter(base.CategoryName, "Failed MServ requests/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter27);
				this.FailedMServRequests = new ExPerformanceCounter(base.CategoryName, "Failed MServ requests total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter27
				});
				list.Add(this.FailedMServRequests);
				ExPerformanceCounter exPerformanceCounter28 = new ExPerformanceCounter(base.CategoryName, "Cookie Based Auth requests/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter28);
				this.CookieBasedAuthRequests = new ExPerformanceCounter(base.CategoryName, "Cookie Based Auth requests total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter28
				});
				list.Add(this.CookieBasedAuthRequests);
				this.PercentageOfCookieBasedAuth = new ExPerformanceCounter(base.CategoryName, "Percentage of cookie based auth", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PercentageOfCookieBasedAuth);
				ExPerformanceCounter exPerformanceCounter29 = new ExPerformanceCounter(base.CategoryName, "Expired Cookie Based Auth requests/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter29);
				this.ExpiredCookieAuthRequests = new ExPerformanceCounter(base.CategoryName, "Total auth requests with expired cookie", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter29
				});
				list.Add(this.ExpiredCookieAuthRequests);
				ExPerformanceCounter exPerformanceCounter30 = new ExPerformanceCounter(base.CategoryName, "Failed Cookie Based Auth requests/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter30);
				this.FailedCookieAuthRequests = new ExPerformanceCounter(base.CategoryName, "Total failed auth requests with cookie", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter30
				});
				list.Add(this.FailedCookieAuthRequests);
				ExPerformanceCounter exPerformanceCounter31 = new ExPerformanceCounter(base.CategoryName, "Remote Auth requests/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter31);
				this.RemoteAuthRequests = new ExPerformanceCounter(base.CategoryName, "Total remote auth requests", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter31
				});
				list.Add(this.RemoteAuthRequests);
				long num = this.NumberOfCurrentRequests.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter32 in list)
					{
						exPerformanceCounter32.Close();
					}
				}
			}
			this.counters = list.ToArray();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00005134 File Offset: 0x00003334
		public override void GetPerfCounterDiagnosticsInfo(XElement topElement)
		{
			XElement xelement = null;
			foreach (ExPerformanceCounter exPerformanceCounter in this.counters)
			{
				try
				{
					if (xelement == null)
					{
						xelement = new XElement(ExPerformanceCounter.GetEncodedName(exPerformanceCounter.InstanceName));
						topElement.Add(xelement);
					}
					xelement.Add(new XElement(ExPerformanceCounter.GetEncodedName(exPerformanceCounter.CounterName), exPerformanceCounter.NextValue()));
				}
				catch (XmlException ex)
				{
					XElement content = new XElement("Error", ex.Message);
					topElement.Add(content);
				}
			}
		}

		// Token: 0x04000093 RID: 147
		public readonly ExPerformanceCounter NumberOfCurrentRequests;

		// Token: 0x04000094 RID: 148
		public readonly ExPerformanceCounter NumberOfAuthRequests;

		// Token: 0x04000095 RID: 149
		public readonly ExPerformanceCounter NumberOfSuccessfulAuthentications;

		// Token: 0x04000096 RID: 150
		public readonly ExPerformanceCounter NumberOfInvalidCredentials;

		// Token: 0x04000097 RID: 151
		public readonly ExPerformanceCounter NumberOfFailedAuthentications;

		// Token: 0x04000098 RID: 152
		public readonly ExPerformanceCounter NumberOfFailedRecoverableAuthentications;

		// Token: 0x04000099 RID: 153
		public readonly ExPerformanceCounter NumberOfFailedRequests;

		// Token: 0x0400009A RID: 154
		public readonly ExPerformanceCounter NumberOfTimedOutRequests;

		// Token: 0x0400009B RID: 155
		public readonly ExPerformanceCounter NumberOfAppPasswords;

		// Token: 0x0400009C RID: 156
		public readonly ExPerformanceCounter AverageResponseTime;

		// Token: 0x0400009D RID: 157
		public readonly ExPerformanceCounter AverageResponseTimeBase;

		// Token: 0x0400009E RID: 158
		public readonly ExPerformanceCounter NumberOfRequestsLastMinute;

		// Token: 0x0400009F RID: 159
		public readonly ExPerformanceCounter NumberOfOrgIdRequestsLastMinute;

		// Token: 0x040000A0 RID: 160
		public readonly ExPerformanceCounter PercentageFailedAuthenticationsLastMinute;

		// Token: 0x040000A1 RID: 161
		public readonly ExPerformanceCounter PercentageFailedRequestsLastMinute;

		// Token: 0x040000A2 RID: 162
		public readonly ExPerformanceCounter PercentageTimedOutRequestsLastMinute;

		// Token: 0x040000A3 RID: 163
		public readonly ExPerformanceCounter NumberOfCachedRequests;

		// Token: 0x040000A4 RID: 164
		public readonly ExPerformanceCounter LogonCacheHit;

		// Token: 0x040000A5 RID: 165
		public readonly ExPerformanceCounter LogonCacheSize;

		// Token: 0x040000A6 RID: 166
		public readonly ExPerformanceCounter InvalidCredCacheSize;

		// Token: 0x040000A7 RID: 167
		public readonly ExPerformanceCounter AdUserCacheHit;

		// Token: 0x040000A8 RID: 168
		public readonly ExPerformanceCounter AdUserCacheSize;

		// Token: 0x040000A9 RID: 169
		public readonly ExPerformanceCounter AverageAdResponseTime;

		// Token: 0x040000AA RID: 170
		public readonly ExPerformanceCounter AverageAdResponseTimeBase;

		// Token: 0x040000AB RID: 171
		public readonly ExPerformanceCounter NumberOfAdRequests;

		// Token: 0x040000AC RID: 172
		public readonly ExPerformanceCounter NumberOfAdRequestForOfflineOrgId;

		// Token: 0x040000AD RID: 173
		public readonly ExPerformanceCounter FailedAdRequests;

		// Token: 0x040000AE RID: 174
		public readonly ExPerformanceCounter AdPasswordSyncs;

		// Token: 0x040000AF RID: 175
		public readonly ExPerformanceCounter AdUpnSyncs;

		// Token: 0x040000B0 RID: 176
		public readonly ExPerformanceCounter AverageHrdResponseTime;

		// Token: 0x040000B1 RID: 177
		public readonly ExPerformanceCounter AverageHrdResponseTimeBase;

		// Token: 0x040000B2 RID: 178
		public readonly ExPerformanceCounter HrdCacheHit;

		// Token: 0x040000B3 RID: 179
		public readonly ExPerformanceCounter HrdCacheHitBase;

		// Token: 0x040000B4 RID: 180
		public readonly ExPerformanceCounter HrdCacheSize;

		// Token: 0x040000B5 RID: 181
		public readonly ExPerformanceCounter NumberOfOutgoingHrdRequests;

		// Token: 0x040000B6 RID: 182
		public readonly ExPerformanceCounter NumberOfADHrdRequests;

		// Token: 0x040000B7 RID: 183
		public readonly ExPerformanceCounter NumberOfFailedADHrdRequests;

		// Token: 0x040000B8 RID: 184
		public readonly ExPerformanceCounter NumberOfADHrdUpdate;

		// Token: 0x040000B9 RID: 185
		public readonly ExPerformanceCounter PendingHrdRequests;

		// Token: 0x040000BA RID: 186
		public readonly ExPerformanceCounter AverageLiveIdResponseTime;

		// Token: 0x040000BB RID: 187
		public readonly ExPerformanceCounter AverageLiveIdResponseTimeBase;

		// Token: 0x040000BC RID: 188
		public readonly ExPerformanceCounter NumberOfLiveIdStsRequests;

		// Token: 0x040000BD RID: 189
		public readonly ExPerformanceCounter PendingStsRequests;

		// Token: 0x040000BE RID: 190
		public readonly ExPerformanceCounter AverageMsoIdResponseTime;

		// Token: 0x040000BF RID: 191
		public readonly ExPerformanceCounter AverageMsoIdResponseTimeBase;

		// Token: 0x040000C0 RID: 192
		public readonly ExPerformanceCounter NumberOfMsoIdStsRequests;

		// Token: 0x040000C1 RID: 193
		public readonly ExPerformanceCounter PendingMsoIdStsRequests;

		// Token: 0x040000C2 RID: 194
		public readonly ExPerformanceCounter AverageRPSCallLatency;

		// Token: 0x040000C3 RID: 195
		public readonly ExPerformanceCounter AverageRPSCallLatencyBase;

		// Token: 0x040000C4 RID: 196
		public readonly ExPerformanceCounter AverageFedStsResponseTime;

		// Token: 0x040000C5 RID: 197
		public readonly ExPerformanceCounter AverageFedStsResponseTimeBase;

		// Token: 0x040000C6 RID: 198
		public readonly ExPerformanceCounter NumberOfOutgoingFedStsRequests;

		// Token: 0x040000C7 RID: 199
		public readonly ExPerformanceCounter PendingFedStsRequests;

		// Token: 0x040000C8 RID: 200
		public readonly ExPerformanceCounter AverageSamlStsResponseTime;

		// Token: 0x040000C9 RID: 201
		public readonly ExPerformanceCounter AverageSamlStsResponseTimeBase;

		// Token: 0x040000CA RID: 202
		public readonly ExPerformanceCounter NumberOfOutgoingSamlStsRequests;

		// Token: 0x040000CB RID: 203
		public readonly ExPerformanceCounter PendingSamlStsRequests;

		// Token: 0x040000CC RID: 204
		public readonly ExPerformanceCounter NumberOfTOUFailures;

		// Token: 0x040000CD RID: 205
		public readonly ExPerformanceCounter NamespaceBlacklistSize;

		// Token: 0x040000CE RID: 206
		public readonly ExPerformanceCounter NamespaceWhitelistSize;

		// Token: 0x040000CF RID: 207
		public readonly ExPerformanceCounter OfflineOrgIdAuthenticationCount;

		// Token: 0x040000D0 RID: 208
		public readonly ExPerformanceCounter NumberOfOfflineOrgIdRequestsLastMinute;

		// Token: 0x040000D1 RID: 209
		public readonly ExPerformanceCounter OfflineOrgIdRedirectCount;

		// Token: 0x040000D2 RID: 210
		public readonly ExPerformanceCounter NumberOfLowConfidenceOfflineOrgIdRequests;

		// Token: 0x040000D3 RID: 211
		public readonly ExPerformanceCounter NumberOfFailedOfflineOrgIdRequests;

		// Token: 0x040000D4 RID: 212
		public readonly ExPerformanceCounter NumberOfOfflineOrgIdRequestWithInvalidCredential;

		// Token: 0x040000D5 RID: 213
		public readonly ExPerformanceCounter NumberOfMailboxAccess;

		// Token: 0x040000D6 RID: 214
		public readonly ExPerformanceCounter NumberOfTenantNegoRequests;

		// Token: 0x040000D7 RID: 215
		public readonly ExPerformanceCounter TenantNegoCacheHit;

		// Token: 0x040000D8 RID: 216
		public readonly ExPerformanceCounter TenantNegoCacheHitBase;

		// Token: 0x040000D9 RID: 217
		public readonly ExPerformanceCounter TenantNegoCacheSize;

		// Token: 0x040000DA RID: 218
		public readonly ExPerformanceCounter AverageMServResponseTime;

		// Token: 0x040000DB RID: 219
		public readonly ExPerformanceCounter AverageMServResponseTimeBase;

		// Token: 0x040000DC RID: 220
		public readonly ExPerformanceCounter NumberOfMServRequests;

		// Token: 0x040000DD RID: 221
		public readonly ExPerformanceCounter PendingMServRequests;

		// Token: 0x040000DE RID: 222
		public readonly ExPerformanceCounter FailedMServRequests;

		// Token: 0x040000DF RID: 223
		public readonly ExPerformanceCounter CookieBasedAuthRequests;

		// Token: 0x040000E0 RID: 224
		public readonly ExPerformanceCounter PercentageOfCookieBasedAuth;

		// Token: 0x040000E1 RID: 225
		public readonly ExPerformanceCounter ExpiredCookieAuthRequests;

		// Token: 0x040000E2 RID: 226
		public readonly ExPerformanceCounter FailedCookieAuthRequests;

		// Token: 0x040000E3 RID: 227
		public readonly ExPerformanceCounter RemoteAuthRequests;
	}
}
