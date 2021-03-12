using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Security;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.ELC;
using Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch;
using Microsoft.Exchange.Security.OAuth;
using Microsoft.Exchange.SoapWebClient;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Exchange.WebServices.Autodiscover;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x02000030 RID: 48
	internal class DiscoveryHoldSynchronizer
	{
		// Token: 0x0600016E RID: 366 RVA: 0x00009AC4 File Offset: 0x00007CC4
		internal void Start(MailboxSession mailboxSession)
		{
			if (DiscoveryHoldSynchronizer.IsOnPremDiscoveryArbitrationMailbox(mailboxSession))
			{
				if (this.cancellationTokenSource == null)
				{
					this.cancellationTokenSource = new CancellationTokenSource();
				}
				if (this.synchronizeTask != null && this.synchronizeTask.IsCompleted)
				{
					this.synchronizeTask.Dispose();
					this.synchronizeTask = null;
				}
				if (this.synchronizeTask == null)
				{
					this.synchronizeTask = new Task(new Action<object>(this.Synchronize), mailboxSession.MailboxOwner.ObjectId.ObjectGuid, this.cancellationTokenSource.Token);
					this.synchronizeTask.ContinueWith(delegate(Task task)
					{
						if (task.IsFaulted && task.Exception != null && task.Exception.InnerException != null)
						{
							if (!(task.Exception.InnerException is LocalizedException) && GrayException.IsGrayException(task.Exception.InnerException))
							{
								ExWatson.SendReport(task.Exception.InnerException, ReportOptions.None, null);
							}
							SearchEventLogger.Instance.LogFailedToSyncDiscoveryHoldToExchangeOnlineEvent(task.Exception.InnerException);
						}
					}, this.cancellationTokenSource.Token);
					this.synchronizeTask.Start();
				}
			}
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00009B94 File Offset: 0x00007D94
		internal void Shutdown()
		{
			if (this.synchronizeTask != null)
			{
				try
				{
					if (!this.synchronizeTask.Wait(30000))
					{
						this.cancellationTokenSource.Cancel();
						this.synchronizeTask.Wait(30000);
					}
				}
				catch (AggregateException ex)
				{
					if (ex.InnerException != null)
					{
						if (!(ex.InnerException is LocalizedException) && GrayException.IsGrayException(ex.InnerException))
						{
							ExWatson.SendReport(ex.InnerException, ReportOptions.None, null);
						}
						SearchEventLogger.Instance.LogFailedToSyncDiscoveryHoldToExchangeOnlineEvent(ex.InnerException);
					}
				}
				finally
				{
					if (this.synchronizeTask.IsCompleted)
					{
						this.synchronizeTask.Dispose();
						this.cancellationTokenSource.Dispose();
					}
					this.synchronizeTask = null;
					this.cancellationTokenSource = null;
				}
			}
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00009C70 File Offset: 0x00007E70
		internal void Synchronize(object argument)
		{
			try
			{
				ServicePointManager.ServerCertificateValidationCallback = (RemoteCertificateValidationCallback)Delegate.Combine(ServicePointManager.ServerCertificateValidationCallback, new RemoteCertificateValidationCallback(CertificateValidation.CertificateErrorHandler));
				Guid b = (Guid)argument;
				SearchEventLogger.Instance.LogSyncDiscoveryHoldToExchangeOnlineStartEvent(b.ToString());
				DiscoverySearchDataProvider discoverySearchDataProvider = new DiscoverySearchDataProvider(OrganizationId.ForestWideOrgId);
				if (discoverySearchDataProvider.ObjectGuid == b)
				{
					Dictionary<string, MailboxDiscoverySearch> dictionary = new Dictionary<string, MailboxDiscoverySearch>();
					SmtpAddress discoveryHolds = DiscoveryHoldSynchronizer.GetDiscoveryHolds(discoverySearchDataProvider, dictionary);
					SearchEventLogger.Instance.LogSyncDiscoveryHoldToExchangeOnlineDetailsEvent(dictionary.Count, discoveryHolds.ToString());
					if (discoveryHolds != SmtpAddress.Empty)
					{
						Uri uri = null;
						string str = string.Empty;
						EndPointDiscoveryInfo endPointDiscoveryInfo;
						bool flag = RemoteDiscoveryEndPoint.TryGetDiscoveryEndPoint(OrganizationId.ForestWideOrgId, discoveryHolds.Domain, null, null, null, out uri, out endPointDiscoveryInfo);
						if (endPointDiscoveryInfo != null && endPointDiscoveryInfo.Status != EndPointDiscoveryInfo.DiscoveryStatus.Success)
						{
							str = endPointDiscoveryInfo.Message;
							DiscoveryHoldSynchronizer.Tracer.TraceDebug<EndPointDiscoveryInfo.DiscoveryStatus, string>((long)this.GetHashCode(), "Getting autodiscover url encountered problem with status {0}. {1}", endPointDiscoveryInfo.Status, endPointDiscoveryInfo.Message);
						}
						if (flag && uri != null)
						{
							uri = EwsWsSecurityUrl.FixForAnonymous(uri);
							AutodiscoverService autodiscoverService = new AutodiscoverService(uri, 4);
							OAuthCredentials credentials = new OAuthCredentials(OAuthCredentials.GetOAuthCredentialsForAppToken(OrganizationId.ForestWideOrgId, discoveryHolds.Domain));
							autodiscoverService.Credentials = credentials;
							GetUserSettingsResponse userSettings = autodiscoverService.GetUserSettings(discoveryHolds.ToString(), new UserSettingName[]
							{
								58
							});
							if (userSettings != null && userSettings.ErrorCode == null && userSettings.Settings != null && userSettings.Settings.ContainsKey(58))
							{
								string uriString = userSettings.Settings[58].ToString();
								ExchangeService exchangeService = new ExchangeService(4);
								exchangeService.Credentials = credentials;
								exchangeService.Url = new Uri(uriString);
								exchangeService.ManagementRoles = new ManagementRoles(null, "LegalHoldApplication");
								GetDiscoverySearchConfigurationResponse discoverySearchConfiguration = exchangeService.GetDiscoverySearchConfiguration(null, false, true);
								if (discoverySearchConfiguration.Result == 2)
								{
									SearchEventLogger.Instance.LogFailedToSyncDiscoveryHoldToExchangeOnlineEvent(string.Format("ErrorCode={0}&ErrorMessage={1}", discoverySearchConfiguration.ErrorCode, discoverySearchConfiguration.ErrorMessage));
									goto IL_402;
								}
								foreach (DiscoverySearchConfiguration discoverySearchConfiguration2 in discoverySearchConfiguration.DiscoverySearchConfigurations)
								{
									MailboxDiscoverySearch mailboxDiscoverySearch = null;
									if (dictionary.TryGetValue(discoverySearchConfiguration2.InPlaceHoldIdentity, out mailboxDiscoverySearch))
									{
										if (mailboxDiscoverySearch.Name != discoverySearchConfiguration2.SearchId)
										{
											if (DiscoveryHoldSynchronizer.CallSetHoldOnMailboxes(exchangeService, discoverySearchConfiguration2.SearchId, 2, discoverySearchConfiguration2.SearchQuery, discoverySearchConfiguration2.InPlaceHoldIdentity, null))
											{
												DiscoveryHoldSynchronizer.CallSetHoldOnMailboxes(exchangeService, mailboxDiscoverySearch.Name, 0, mailboxDiscoverySearch.CalculatedQuery, mailboxDiscoverySearch.InPlaceHoldIdentity, mailboxDiscoverySearch.ItemHoldPeriod.ToString());
											}
										}
										else
										{
											DiscoveryHoldSynchronizer.CallSetHoldOnMailboxes(exchangeService, mailboxDiscoverySearch.Name, 1, mailboxDiscoverySearch.CalculatedQuery, mailboxDiscoverySearch.InPlaceHoldIdentity, mailboxDiscoverySearch.ItemHoldPeriod.ToString());
										}
										dictionary.Remove(discoverySearchConfiguration2.InPlaceHoldIdentity);
									}
									else if (discoverySearchConfiguration2.ManagedByOrganization == "b5d6efcd-1aee-42b9-b168-6fef285fe613")
									{
										DiscoveryHoldSynchronizer.CallSetHoldOnMailboxes(exchangeService, discoverySearchConfiguration2.SearchId, 2, discoverySearchConfiguration2.SearchQuery, discoverySearchConfiguration2.InPlaceHoldIdentity, null);
									}
								}
								using (Dictionary<string, MailboxDiscoverySearch>.ValueCollection.Enumerator enumerator = dictionary.Values.GetEnumerator())
								{
									while (enumerator.MoveNext())
									{
										MailboxDiscoverySearch mailboxDiscoverySearch2 = enumerator.Current;
										DiscoveryHoldSynchronizer.CallSetHoldOnMailboxes(exchangeService, mailboxDiscoverySearch2.Name, 0, mailboxDiscoverySearch2.CalculatedQuery, mailboxDiscoverySearch2.InPlaceHoldIdentity, mailboxDiscoverySearch2.ItemHoldPeriod.ToString());
									}
									goto IL_402;
								}
							}
							string str2 = string.Empty;
							if (userSettings != null && userSettings.ErrorCode != null)
							{
								str2 = string.Format("ErrorCode={0}&ErrorMessage={1}", userSettings.ErrorCode, userSettings.ErrorMessage);
							}
							SearchEventLogger.Instance.LogFailedToSyncDiscoveryHoldToExchangeOnlineEvent("Failed to get autodiscover settings. " + str2);
						}
						else
						{
							SearchEventLogger.Instance.LogFailedToSyncDiscoveryHoldToExchangeOnlineEvent("Failed to get autodiscover URL. " + str);
						}
					}
				}
				IL_402:;
			}
			finally
			{
				ServicePointManager.ServerCertificateValidationCallback = (RemoteCertificateValidationCallback)Delegate.Remove(ServicePointManager.ServerCertificateValidationCallback, new RemoteCertificateValidationCallback(CertificateValidation.CertificateErrorHandler));
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000171 RID: 369 RVA: 0x0000A0D8 File Offset: 0x000082D8
		private static bool IsOnPremise
		{
			get
			{
				if (DiscoveryHoldSynchronizer.isOnPremise == null)
				{
					DiscoveryHoldSynchronizer.isOnPremise = new bool?(VariantConfiguration.InvariantNoFlightingSnapshot.MailboxAssistants.ElcAssistantDiscoveryHoldSynchronizer.Enabled);
				}
				return DiscoveryHoldSynchronizer.isOnPremise.Value;
			}
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000A128 File Offset: 0x00008328
		private static SmtpAddress GetDiscoveryHolds(DiscoverySearchDataProvider dataProvider, Dictionary<string, MailboxDiscoverySearch> discoveryHolds)
		{
			SmtpAddress smtpAddress = SmtpAddress.Empty;
			ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest(), OrganizationId.ForestWideOrgId, null, false);
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(true, ConsistencyMode.IgnoreInvalid, sessionSettings, 324, "GetDiscoveryHolds", "f:\\15.00.1497\\sources\\dev\\MailboxAssistants\\src\\assistants\\elc\\DiscoveryHoldSynchronizer.cs");
			foreach (MailboxDiscoverySearch mailboxDiscoverySearch in dataProvider.GetAll<MailboxDiscoverySearch>())
			{
				if (mailboxDiscoverySearch.InPlaceHoldEnabled)
				{
					discoveryHolds.Add(mailboxDiscoverySearch.InPlaceHoldIdentity, mailboxDiscoverySearch);
					if (smtpAddress == SmtpAddress.Empty)
					{
						Result<ADRawEntry>[] first = tenantOrRootOrgRecipientSession.FindByLegacyExchangeDNs(mailboxDiscoverySearch.Sources.ToArray(), new ADPropertyDefinition[]
						{
							ADRecipientSchema.RecipientType,
							ADRecipientSchema.RecipientTypeDetails,
							ADUserSchema.ArchiveDomain,
							ADUserSchema.ArchiveGuid,
							ADRecipientSchema.RawExternalEmailAddress,
							ADUserSchema.ArchiveStatus
						});
						foreach (ADRawEntry adrawEntry in from x in first
						select x.Data)
						{
							if (adrawEntry != null)
							{
								RecipientType recipientType = (RecipientType)adrawEntry[ADRecipientSchema.RecipientType];
								RecipientTypeDetails recipientTypeDetails = (RecipientTypeDetails)adrawEntry[ADRecipientSchema.RecipientTypeDetails];
								SmtpDomain smtpDomain = (SmtpDomain)adrawEntry[ADUserSchema.ArchiveDomain];
								ArchiveStatusFlags archiveStatusFlags = (ArchiveStatusFlags)adrawEntry[ADUserSchema.ArchiveStatus];
								if (RemoteMailbox.IsRemoteMailbox(recipientTypeDetails))
								{
									smtpAddress = new SmtpAddress(((ProxyAddress)adrawEntry[ADRecipientSchema.RawExternalEmailAddress]).AddressString);
								}
								else if (recipientType == RecipientType.UserMailbox && smtpDomain != null && !string.IsNullOrEmpty(smtpDomain.Domain) && (archiveStatusFlags & ArchiveStatusFlags.Active) == ArchiveStatusFlags.Active)
								{
									Guid guid = (Guid)adrawEntry[ADUserSchema.ArchiveGuid];
									if (guid != Guid.Empty)
									{
										smtpAddress = new SmtpAddress(SmtpProxyAddress.EncapsulateExchangeGuid(smtpDomain.Domain, guid));
									}
								}
							}
						}
					}
				}
			}
			return smtpAddress;
		}

		// Token: 0x06000173 RID: 371 RVA: 0x0000A370 File Offset: 0x00008570
		private static bool CallSetHoldOnMailboxes(ExchangeService service, string searchId, HoldAction holdAction, string query, string inplaceHoldIdentity, string itemHoldPeriod = null)
		{
			bool result = true;
			ServiceResponse serviceResponse = service.SetHoldOnMailboxes(searchId, holdAction, query, inplaceHoldIdentity, itemHoldPeriod);
			if (serviceResponse.Result == 2)
			{
				result = false;
				SearchEventLogger.Instance.LogSingleFailureSyncDiscoveryHoldToExchangeOnlineEvent(searchId, holdAction.ToString(), query, inplaceHoldIdentity, serviceResponse.ErrorCode.ToString(), serviceResponse.ErrorMessage);
			}
			return result;
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0000A3C8 File Offset: 0x000085C8
		private static bool IsOnPremDiscoveryArbitrationMailbox(MailboxSession mailboxSession)
		{
			return mailboxSession.MailboxOwner.RecipientTypeDetails == RecipientTypeDetails.ArbitrationMailbox && mailboxSession.MailboxOwner.Alias == "SystemMailbox{e0dc1c29-89c3-4034-b678-e6c29d823ed9}" && mailboxSession.MailboxOwner.ObjectId.Name == "SystemMailbox{e0dc1c29-89c3-4034-b678-e6c29d823ed9}" && DiscoveryHoldSynchronizer.IsOnPremise;
		}

		// Token: 0x04000152 RID: 338
		private const int AsyncTimeout = 30000;

		// Token: 0x04000153 RID: 339
		private static readonly Trace Tracer = ExTraceGlobals.ELCAssistantTracer;

		// Token: 0x04000154 RID: 340
		private static bool? isOnPremise;

		// Token: 0x04000155 RID: 341
		private Task synchronizeTask;

		// Token: 0x04000156 RID: 342
		private CancellationTokenSource cancellationTokenSource;
	}
}
