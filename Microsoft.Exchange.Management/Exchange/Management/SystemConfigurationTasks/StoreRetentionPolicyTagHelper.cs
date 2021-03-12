using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Web.Services.Protocols;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.Common;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.Common.ELC;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Net.Protocols;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.SoapWebClient;
using Microsoft.Exchange.SoapWebClient.EWS;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000323 RID: 803
	internal class StoreRetentionPolicyTagHelper : IDisposable
	{
		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x06001B4D RID: 6989 RVA: 0x000790C8 File Offset: 0x000772C8
		// (set) Token: 0x06001B4E RID: 6990 RVA: 0x000790D0 File Offset: 0x000772D0
		public Dictionary<Guid, StoreTagData> TagData
		{
			get
			{
				return this.tagData;
			}
			set
			{
				this.tagData = value;
			}
		}

		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x06001B4F RID: 6991 RVA: 0x000790D9 File Offset: 0x000772D9
		public Dictionary<Guid, StoreTagData> DefaultArchiveTagData
		{
			get
			{
				return this.defaultArchiveTagData;
			}
		}

		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x06001B50 RID: 6992 RVA: 0x000790E1 File Offset: 0x000772E1
		public ADUser Mailbox
		{
			get
			{
				return this.user;
			}
		}

		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x06001B51 RID: 6993 RVA: 0x000790E9 File Offset: 0x000772E9
		internal ExchangePrincipal UserPrincipal
		{
			get
			{
				return this.exchangePrincipal;
			}
		}

		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x06001B52 RID: 6994 RVA: 0x000790F1 File Offset: 0x000772F1
		// (set) Token: 0x06001B53 RID: 6995 RVA: 0x000790F9 File Offset: 0x000772F9
		public RetentionHoldData HoldData
		{
			get
			{
				return this.retentionHoldData;
			}
			set
			{
				this.retentionHoldData = value;
			}
		}

		// Token: 0x06001B54 RID: 6996 RVA: 0x00079102 File Offset: 0x00077302
		public void Dispose()
		{
			this.recipientSession = null;
			this.user = null;
			this.tagData = null;
			if (this.mailboxSession != null)
			{
				this.mailboxSession.Dispose();
				this.mailboxSession = null;
			}
		}

		// Token: 0x06001B55 RID: 6997 RVA: 0x00079133 File Offset: 0x00077333
		internal static StoreRetentionPolicyTagHelper FromMailboxId(string domainController, MailboxIdParameter mailbox, OrganizationId organizationId)
		{
			return StoreRetentionPolicyTagHelper.FromMailboxId(domainController, mailbox, false, organizationId);
		}

		// Token: 0x06001B56 RID: 6998 RVA: 0x0007913E File Offset: 0x0007733E
		private static bool HasOnPremArchiveMailbox(ExchangePrincipal exchangePrincipal)
		{
			return exchangePrincipal != null && exchangePrincipal.GetArchiveMailbox() != null;
		}

		// Token: 0x06001B57 RID: 6999 RVA: 0x00079154 File Offset: 0x00077354
		internal static bool HasOnPremArchiveMailbox(ADUser user)
		{
			if (user.ArchiveState == ArchiveState.Local)
			{
				ExchangePrincipal exchangePrincipal = ExchangePrincipal.FromADUser(user, RemotingOptions.AllowCrossSite);
				return StoreRetentionPolicyTagHelper.HasOnPremArchiveMailbox(exchangePrincipal);
			}
			return false;
		}

		// Token: 0x06001B58 RID: 7000 RVA: 0x0007918C File Offset: 0x0007738C
		internal static void SyncOptionalTagsFromPrimaryToArchive(ADUser user)
		{
			if (user == null || !StoreRetentionPolicyTagHelper.HasOnPremArchiveMailbox(user))
			{
				return;
			}
			using (StoreRetentionPolicyTagHelper storeRetentionPolicyTagHelper = StoreRetentionPolicyTagHelper.FromADUser(user, false))
			{
				if (storeRetentionPolicyTagHelper.configItemExists)
				{
					using (StoreRetentionPolicyTagHelper storeRetentionPolicyTagHelper2 = StoreRetentionPolicyTagHelper.FromADUser(user, true))
					{
						List<StoreTagData> list = (from x in storeRetentionPolicyTagHelper.tagData.Values
						where !x.Tag.IsArchiveTag
						select x).ToList<StoreTagData>();
						if (!storeRetentionPolicyTagHelper2.configItemExists || storeRetentionPolicyTagHelper2.tagData == null || storeRetentionPolicyTagHelper2.tagData.Count == 0)
						{
							if (storeRetentionPolicyTagHelper2.tagData == null)
							{
								storeRetentionPolicyTagHelper2.tagData = new Dictionary<Guid, StoreTagData>(list.Count);
							}
							foreach (StoreTagData storeTagData in list)
							{
								if (!storeRetentionPolicyTagHelper2.tagData.Values.Contains(storeTagData))
								{
									storeRetentionPolicyTagHelper2.tagData.Add(storeTagData.Tag.RetentionId, storeTagData);
								}
							}
							storeRetentionPolicyTagHelper2.Save();
						}
					}
				}
			}
		}

		// Token: 0x06001B59 RID: 7001 RVA: 0x000792C8 File Offset: 0x000774C8
		internal static ADUser FetchRecipientFromMailboxId(string domainController, MailboxIdParameter mailbox, out IRecipientSession session, OrganizationId orgId)
		{
			session = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(domainController, true, ConsistencyMode.IgnoreInvalid, orgId.ToADSessionSettings(), 191, "FetchRecipientFromMailboxId", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Elc\\StoreRetentionPolicyTagHelper.cs");
			LocalizedString? localizedString = null;
			IEnumerable<ADUser> objects = mailbox.GetObjects<ADUser>(null, session, null, out localizedString);
			ADUser aduser = null;
			if (objects != null)
			{
				foreach (ADUser aduser2 in objects)
				{
					if (aduser != null)
					{
						throw new ManagementObjectAmbiguousException(Strings.ErrorRecipientNotUnique(mailbox.ToString()));
					}
					aduser = aduser2;
				}
			}
			if (aduser == null)
			{
				throw new ManagementObjectNotFoundException(localizedString ?? Strings.ErrorObjectNotFound(mailbox.ToString()));
			}
			return aduser;
		}

		// Token: 0x06001B5A RID: 7002 RVA: 0x00079390 File Offset: 0x00077590
		internal static StoreRetentionPolicyTagHelper FromMailboxId(string domainController, MailboxIdParameter mailbox, bool isArchiveMailbox, OrganizationId orgId)
		{
			StoreRetentionPolicyTagHelper storeRetentionPolicyTagHelper = new StoreRetentionPolicyTagHelper();
			storeRetentionPolicyTagHelper.user = StoreRetentionPolicyTagHelper.FetchRecipientFromMailboxId(domainController, mailbox, out storeRetentionPolicyTagHelper.recipientSession, orgId);
			storeRetentionPolicyTagHelper.isArchiveMailbox = isArchiveMailbox;
			StoreRetentionPolicyTagHelper.FetchRetentionPolicyTagDataFromMailbox(storeRetentionPolicyTagHelper);
			return storeRetentionPolicyTagHelper;
		}

		// Token: 0x06001B5B RID: 7003 RVA: 0x000793C8 File Offset: 0x000775C8
		private static StoreRetentionPolicyTagHelper FromADUser(ADUser user, bool isArchiveMailbox)
		{
			StoreRetentionPolicyTagHelper storeRetentionPolicyTagHelper = new StoreRetentionPolicyTagHelper();
			storeRetentionPolicyTagHelper.user = user;
			storeRetentionPolicyTagHelper.isArchiveMailbox = isArchiveMailbox;
			StoreRetentionPolicyTagHelper.FetchRetentionPolicyTagDataFromMailbox(storeRetentionPolicyTagHelper);
			return storeRetentionPolicyTagHelper;
		}

		// Token: 0x06001B5C RID: 7004 RVA: 0x000793F0 File Offset: 0x000775F0
		private static void FetchRetentionPolicyTagDataFromMailbox(StoreRetentionPolicyTagHelper srpth)
		{
			StoreRetentionPolicyTagHelper.InitializePrincipal(srpth);
			if (srpth.exchangePrincipal != null)
			{
				if (srpth.exchangePrincipal.MailboxInfo.Location.ServerVersion >= Server.E14SP1MinVersion)
				{
					ExTraceGlobals.ELCTracer.TraceDebug(0L, "Fetch retention policy tag data from EWS since user's version is " + srpth.exchangePrincipal.MailboxInfo.Location.ServerVersion);
					StoreRetentionPolicyTagHelper.FetchRetentionPolicyTagDataFromService(srpth);
					return;
				}
				ExTraceGlobals.ELCTracer.TraceDebug(0L, "Fetch retention policy tag data from XSO since user's version is " + srpth.exchangePrincipal.MailboxInfo.Location.ServerVersion);
				StoreRetentionPolicyTagHelper.FetchRetentionPolcyTagDataFromXSO(srpth);
			}
		}

		// Token: 0x06001B5D RID: 7005 RVA: 0x00079498 File Offset: 0x00077698
		private static void InitializePrincipal(StoreRetentionPolicyTagHelper srpth)
		{
			if (srpth.user != null)
			{
				srpth.exchangePrincipal = ExchangePrincipal.FromADUser(srpth.user.OrganizationId.ToADSessionSettings(), srpth.user);
				if (srpth.exchangePrincipal != null && srpth.isArchiveMailbox && srpth.user.ArchiveState == ArchiveState.Local)
				{
					srpth.exchangePrincipal = srpth.exchangePrincipal.GetArchiveExchangePrincipal(RemotingOptions.LocalConnectionsOnly);
					return;
				}
			}
			else
			{
				ExTraceGlobals.ELCTracer.TraceDebug(0L, "Exchange principal cannot be found because user is not available.");
			}
		}

		// Token: 0x06001B5E RID: 7006 RVA: 0x00079510 File Offset: 0x00077710
		private static void FetchRetentionPolcyTagDataFromXSO(StoreRetentionPolicyTagHelper srpth)
		{
			if (srpth.exchangePrincipal == null)
			{
				ExTraceGlobals.ELCTracer.TraceDebug(0L, "Cannot fetch retention policy tag data because Exchange principal is not available.");
				return;
			}
			srpth.mailboxSession = MailboxSession.OpenAsAdmin(srpth.exchangePrincipal, CultureInfo.InvariantCulture, "Client=Management;Action=Get-/Set-RetentionPolicyTag");
			srpth.configItem = ElcMailboxHelper.OpenFaiMessage(srpth.mailboxSession, "MRM", true);
			if (srpth.configItem != null)
			{
				srpth.TagData = MrmFaiFormatter.Deserialize(srpth.configItem, srpth.exchangePrincipal, out srpth.deletedTags, out srpth.retentionHoldData, true, out srpth.defaultArchiveTagData, out srpth.fullCrawlRequired);
				return;
			}
			srpth.TagData = new Dictionary<Guid, StoreTagData>();
		}

		// Token: 0x06001B5F RID: 7007 RVA: 0x0007970C File Offset: 0x0007790C
		private static void FetchRetentionPolicyTagDataFromService(StoreRetentionPolicyTagHelper srpth)
		{
			StoreRetentionPolicyTagHelper.InitializeServiceBinding(srpth);
			if (srpth.serviceBinding != null)
			{
				GetUserConfigurationType getUserConfiguration = new GetUserConfigurationType
				{
					UserConfigurationName = new UserConfigurationNameType
					{
						Name = "MRM",
						Item = new DistinguishedFolderIdType
						{
							Id = DistinguishedFolderIdNameType.inbox
						}
					},
					UserConfigurationProperties = (UserConfigurationPropertyType.Dictionary | UserConfigurationPropertyType.XmlData | UserConfigurationPropertyType.BinaryData)
				};
				StoreRetentionPolicyTagHelper.CallEwsWithRetries(() => srpth.serviceBinding.GetUserConfiguration(getUserConfiguration), delegate(ResponseMessageType responseMessage, int messageIndex)
				{
					GetUserConfigurationResponseMessageType getUserConfigurationResponseMessageType = responseMessage as GetUserConfigurationResponseMessageType;
					if (getUserConfigurationResponseMessageType.ResponseClass == ResponseClassType.Success && getUserConfigurationResponseMessageType.UserConfiguration != null)
					{
						if (getUserConfigurationResponseMessageType.UserConfiguration.XmlData != null)
						{
							ExTraceGlobals.ELCTracer.TraceDebug(0L, "Acquired MRM user configuration.");
							srpth.TagData = MrmFaiFormatter.Deserialize(getUserConfigurationResponseMessageType.UserConfiguration.XmlData, srpth.exchangePrincipal, out srpth.deletedTags, out srpth.retentionHoldData, true, out srpth.defaultArchiveTagData, out srpth.fullCrawlRequired);
							srpth.configItemExists = true;
						}
						else
						{
							ExTraceGlobals.ELCTracer.TraceDebug(0L, "MRM user configuration is null");
							srpth.TagData = new Dictionary<Guid, StoreTagData>();
							srpth.configItemExists = false;
						}
						return true;
					}
					if (getUserConfigurationResponseMessageType.ResponseClass == ResponseClassType.Error && getUserConfigurationResponseMessageType.ResponseCode == ResponseCodeType.ErrorItemNotFound)
					{
						ExTraceGlobals.ELCTracer.TraceDebug(0L, "MRM user configuration not found");
						srpth.TagData = new Dictionary<Guid, StoreTagData>();
						srpth.configItemExists = false;
						return true;
					}
					ExTraceGlobals.ELCTracer.TraceDebug(0L, "Getting MRM user configuration failed");
					return false;
				}, srpth);
				return;
			}
			throw new ElcUserConfigurationException(Strings.ElcUserConfigurationServiceBindingNotAvailable);
		}

		// Token: 0x06001B60 RID: 7008 RVA: 0x000797D4 File Offset: 0x000779D4
		private static void InitializeServiceBinding(StoreRetentionPolicyTagHelper srpth)
		{
			if (srpth.exchangePrincipal != null)
			{
				string text = StoreRetentionPolicyTagHelper.DiscoverEwsUrl(srpth.exchangePrincipal);
				if (string.IsNullOrEmpty(text))
				{
					return;
				}
				srpth.serviceBinding = StoreRetentionPolicyTagHelper.CreateBinding(srpth.exchangePrincipal.MailboxInfo.PrimarySmtpAddress.ToString());
				srpth.serviceBinding.Timeout = (int)StoreRetentionPolicyTagHelper.DefaultSoapClientTimeout.TotalMilliseconds;
				srpth.serviceBinding.Url = text;
				ExTraceGlobals.ELCTracer.TraceDebug(0L, "Refreshed service binding, new url: " + text);
			}
		}

		// Token: 0x06001B61 RID: 7009 RVA: 0x00079864 File Offset: 0x00077A64
		private static string DiscoverEwsUrl(ExchangePrincipal exchangePrincipal)
		{
			ExTraceGlobals.ELCTracer.TraceDebug<ExchangePrincipal>(0L, "Will try to discover the URL for EWS with the Backendlocator for mailbox {0}", exchangePrincipal);
			try
			{
				Uri backEndWebServicesUrl = BackEndLocator.GetBackEndWebServicesUrl(exchangePrincipal.MailboxInfo);
				if (backEndWebServicesUrl != null)
				{
					ExTraceGlobals.ELCTracer.TraceDebug<string, ExchangePrincipal>(0L, "Found Uri from the back end locator.{0}, {1}", backEndWebServicesUrl.ToString(), exchangePrincipal);
					return backEndWebServicesUrl.ToString();
				}
				ExTraceGlobals.ELCTracer.TraceDebug<ExchangePrincipal>(0L, "Unable to discover internal URL for EWS for mailbox {0}. BackEndLocator call returned null", exchangePrincipal);
			}
			catch (LocalizedException arg)
			{
				ExTraceGlobals.ELCTracer.TraceError<ExchangePrincipal, LocalizedException>(0L, "Unable to discover internal URL for EWS for mailbox {0} due exception {1}", exchangePrincipal, arg);
			}
			return null;
		}

		// Token: 0x06001B62 RID: 7010 RVA: 0x000798F8 File Offset: 0x00077AF8
		private static ExchangeServiceBinding CreateBinding(string email)
		{
			NetworkServiceImpersonator.Initialize();
			if (NetworkServiceImpersonator.Exception != null)
			{
				ExTraceGlobals.ELCTracer.TraceError<LocalizedException>(0L, "Unable to impersonate network service to call EWS due to exception {0}", NetworkServiceImpersonator.Exception);
				throw new ElcUserConfigurationException(Strings.ElcUserConfigurationServiceBindingNotAvailable, NetworkServiceImpersonator.Exception);
			}
			ExchangeServiceBinding exchangeServiceBinding = new ExchangeServiceBinding(new RemoteCertificateValidationCallback(StoreRetentionPolicyTagHelper.CertificateErrorHandler));
			exchangeServiceBinding.UserAgent = WellKnownUserAgent.GetEwsNegoAuthUserAgent("MRMTask");
			exchangeServiceBinding.RequestServerVersionValue = new RequestServerVersion
			{
				Version = ExchangeVersionType.Exchange2010_SP1
			};
			StoreRetentionPolicyTagHelper.SetSecurityHeader(exchangeServiceBinding, email);
			return exchangeServiceBinding;
		}

		// Token: 0x06001B63 RID: 7011 RVA: 0x0007997C File Offset: 0x00077B7C
		private static void SetSecurityHeader(ExchangeServiceBinding binding, string email)
		{
			binding.Authenticator = SoapHttpClientAuthenticator.CreateNetworkService();
			binding.Authenticator.AdditionalSoapHeaders.Add(new OpenAsAdminOrSystemServiceType
			{
				ConnectingSID = new ConnectingSIDType
				{
					Item = new PrimarySmtpAddressType
					{
						Value = email
					}
				},
				LogonType = SpecialLogonType.SystemService
			});
		}

		// Token: 0x06001B64 RID: 7012 RVA: 0x000799D4 File Offset: 0x00077BD4
		internal static bool CertificateErrorHandler(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			if (sslPolicyErrors == SslPolicyErrors.None)
			{
				return true;
			}
			if (sslPolicyErrors == SslPolicyErrors.RemoteCertificateNameMismatch)
			{
				return true;
			}
			if (SslConfiguration.AllowInternalUntrustedCerts)
			{
				ExTraceGlobals.ELCTracer.TraceDebug(0L, "Accepting SSL certificate because registry config AllowInternalUntrustedCerts tells to ignore errors");
				return true;
			}
			ExTraceGlobals.ELCTracer.TraceError<SslPolicyErrors>(0L, "Failed because SSL certificate contains the following errors: {0}", sslPolicyErrors);
			return false;
		}

		// Token: 0x06001B65 RID: 7013 RVA: 0x00079A6C File Offset: 0x00077C6C
		private static void UpdateUserConfiguration(StoreRetentionPolicyTagHelper tagHelper)
		{
			byte[] xmlData = MrmFaiFormatter.Serialize(tagHelper.TagData, tagHelper.defaultArchiveTagData, tagHelper.deletedTags, tagHelper.retentionHoldData, tagHelper.fullCrawlRequired, tagHelper.exchangePrincipal);
			UpdateUserConfigurationType updateUserConfiguration = new UpdateUserConfigurationType
			{
				UserConfiguration = new UserConfigurationType
				{
					UserConfigurationName = new UserConfigurationNameType
					{
						Name = "MRM",
						Item = new DistinguishedFolderIdType
						{
							Id = DistinguishedFolderIdNameType.inbox
						}
					},
					XmlData = xmlData
				}
			};
			StoreRetentionPolicyTagHelper.CallEwsWithRetries(() => tagHelper.serviceBinding.UpdateUserConfiguration(updateUserConfiguration), delegate(ResponseMessageType responseMessage, int messageIndex)
			{
				if (responseMessage.ResponseClass == ResponseClassType.Success)
				{
					ExTraceGlobals.ELCTracer.TraceDebug(0L, "Successfully updated MRM user configuration");
					return true;
				}
				ExTraceGlobals.ELCTracer.TraceDebug(0L, "MRM user configuration was not updated");
				return false;
			}, tagHelper);
		}

		// Token: 0x06001B66 RID: 7014 RVA: 0x00079BBC File Offset: 0x00077DBC
		private static void CreateUserConfiguration(StoreRetentionPolicyTagHelper tagHelper)
		{
			byte[] xmlData = MrmFaiFormatter.Serialize(tagHelper.TagData, tagHelper.defaultArchiveTagData, tagHelper.deletedTags, tagHelper.retentionHoldData, tagHelper.fullCrawlRequired, tagHelper.exchangePrincipal);
			CreateUserConfigurationType createUserConfiguration = new CreateUserConfigurationType
			{
				UserConfiguration = new UserConfigurationType
				{
					UserConfigurationName = new UserConfigurationNameType
					{
						Name = "MRM",
						Item = new DistinguishedFolderIdType
						{
							Id = DistinguishedFolderIdNameType.inbox
						}
					},
					XmlData = xmlData
				}
			};
			StoreRetentionPolicyTagHelper.CallEwsWithRetries(() => tagHelper.serviceBinding.CreateUserConfiguration(createUserConfiguration), delegate(ResponseMessageType responseMessage, int messageIndex)
			{
				if (responseMessage.ResponseClass == ResponseClassType.Success)
				{
					ExTraceGlobals.ELCTracer.TraceDebug(0L, "Successfully created MRM user configuration");
					return true;
				}
				ExTraceGlobals.ELCTracer.TraceDebug(0L, "MRM user configuration was not created");
				return false;
			}, tagHelper);
		}

		// Token: 0x06001B67 RID: 7015 RVA: 0x00079CB0 File Offset: 0x00077EB0
		private static void CallEwsWithRetries(Func<BaseResponseMessageType> delegateEwsCall, Func<ResponseMessageType, int, bool> responseMessageProcessor, StoreRetentionPolicyTagHelper srpth)
		{
			ExDateTime t = ExDateTime.UtcNow.Add(StoreRetentionPolicyTagHelper.TotalExecutionTimeWindow);
			Exception ex = null;
			int num = -1;
			bool flag;
			do
			{
				ex = null;
				flag = false;
				try
				{
					BaseResponseMessageType baseResponseMessageType = delegateEwsCall();
					num++;
					int i = 0;
					while (i < baseResponseMessageType.ResponseMessages.Items.Length)
					{
						ResponseMessageType responseMessageType = baseResponseMessageType.ResponseMessages.Items[i];
						if (responseMessageProcessor != null && responseMessageProcessor(responseMessageType, i))
						{
							ExTraceGlobals.ELCTracer.TraceDebug(0L, "Successfully executed EWS call");
							break;
						}
						if (responseMessageType.ResponseClass == ResponseClassType.Error)
						{
							if (responseMessageType.ResponseCode == ResponseCodeType.ErrorCrossSiteRequest)
							{
								ExTraceGlobals.ELCTracer.TraceDebug(0L, "Crosssite request error , recreate exchange binding and reset the url caches");
								flag = true;
								StoreRetentionPolicyTagHelper.InitializePrincipal(srpth);
								StoreRetentionPolicyTagHelper.InitializeServiceBinding(srpth);
								break;
							}
							if (!StoreRetentionPolicyTagHelper.TransientServiceErrors.Contains(responseMessageType.ResponseCode))
							{
								ExTraceGlobals.ELCTracer.TraceError(0L, string.Format("Permanent error encountered:  {0}, {1}, {2}", responseMessageType.ResponseClass.ToString(), responseMessageType.ResponseCode.ToString(), responseMessageType.MessageText.ToString()));
								throw new ElcUserConfigurationException(Strings.FailedToGetElcUserConfigurationFromService(responseMessageType.ResponseClass.ToString(), responseMessageType.ResponseCode.ToString(), responseMessageType.MessageText.ToString()));
							}
							flag = true;
							ex = new ElcUserConfigurationException(Strings.FailedToGetElcUserConfigurationFromService(responseMessageType.ResponseClass.ToString(), responseMessageType.ResponseCode.ToString(), responseMessageType.MessageText.ToString()));
							ExTraceGlobals.ELCTracer.TraceDebug(0L, "Transient error encountered, will attempt to retry, Exception: " + ex);
							break;
						}
						else
						{
							i++;
						}
					}
				}
				catch (CommunicationException ex2)
				{
					ex = ex2;
					ExTraceGlobals.ELCTracer.TraceDebug(0L, "Transient error encountered, will attempt to retry, Exception: " + ex);
					flag = true;
				}
				catch (WebException ex3)
				{
					ex = ex3;
					ExTraceGlobals.ELCTracer.TraceDebug(0L, "Transient error encountered, will attempt to retry, Exception: " + ex);
					flag = true;
				}
				catch (TimeoutException ex4)
				{
					ex = ex4;
					flag = false;
				}
				catch (SoapException ex5)
				{
					ex = ex5;
					flag = false;
				}
				catch (IOException ex6)
				{
					ex = ex6;
					flag = false;
				}
				catch (InvalidOperationException ex7)
				{
					ex = ex7;
					flag = false;
				}
				catch (LocalizedException ex8)
				{
					ex = ex8;
					flag = false;
				}
			}
			while (flag && t > ExDateTime.UtcNow);
			if (ex != null)
			{
				ExTraceGlobals.ELCTracer.TraceError(0L, string.Format("Failed to access elc user configuration.  Total attempts made {0}, Exception: {1} ", num, ex));
				throw new ElcUserConfigurationException(Strings.ErrorElcUserConfigurationServiceCall, ex);
			}
		}

		// Token: 0x06001B68 RID: 7016 RVA: 0x00079FCC File Offset: 0x000781CC
		internal void Save()
		{
			if (this.exchangePrincipal == null)
			{
				ExTraceGlobals.ELCTracer.TraceDebug(0L, "Cannot save changes because Exchange principal is not available to verify version.");
				return;
			}
			if (this.exchangePrincipal.MailboxInfo.Location.ServerVersion >= Server.E14SP1MinVersion)
			{
				ExTraceGlobals.ELCTracer.TraceDebug(0L, "Save changes to EWS since user's version is " + this.exchangePrincipal.MailboxInfo.Location.ServerVersion);
				this.SaveToService();
				return;
			}
			ExTraceGlobals.ELCTracer.TraceDebug(0L, "Save changes to XSO since user's version is " + this.exchangePrincipal.MailboxInfo.Location.ServerVersion);
			this.SaveToXSO();
		}

		// Token: 0x06001B69 RID: 7017 RVA: 0x0007A080 File Offset: 0x00078280
		private void SaveToService()
		{
			StoreRetentionPolicyTagHelper.InitializePrincipal(this);
			StoreRetentionPolicyTagHelper.InitializeServiceBinding(this);
			if (this.serviceBinding == null)
			{
				throw new ElcUserConfigurationException(Strings.ElcUserConfigurationServiceBindingNotAvailable);
			}
			if (this.configItemExists)
			{
				StoreRetentionPolicyTagHelper.UpdateUserConfiguration(this);
				return;
			}
			StoreRetentionPolicyTagHelper.CreateUserConfiguration(this);
		}

		// Token: 0x06001B6A RID: 7018 RVA: 0x0007A0C8 File Offset: 0x000782C8
		internal void SaveToXSO()
		{
			if (this.configItem != null)
			{
				MrmFaiFormatter.Serialize(this.TagData, this.defaultArchiveTagData, this.deletedTags, this.retentionHoldData, this.configItem, this.fullCrawlRequired, this.mailboxSession.MailboxOwner);
				this.configItem.Save();
				return;
			}
			throw new ElcUserConfigurationException(Strings.ElcUserConfigurationConfigurationItemNotAvailable);
		}

		// Token: 0x04000BC1 RID: 3009
		private const string ServiceBindingComponentId = "MRMTask";

		// Token: 0x04000BC2 RID: 3010
		private const UserConfigurationPropertyType ElcConfigurationTypes = UserConfigurationPropertyType.Dictionary | UserConfigurationPropertyType.XmlData | UserConfigurationPropertyType.BinaryData;

		// Token: 0x04000BC3 RID: 3011
		private static TimeSpan ServiceTopologyTimeout = TimeSpan.FromSeconds(10.0);

		// Token: 0x04000BC4 RID: 3012
		private static readonly TimeSpan TotalExecutionTimeWindow = TimeSpan.FromSeconds(10.0);

		// Token: 0x04000BC5 RID: 3013
		private static readonly TimeSpan DefaultSoapClientTimeout = TimeSpan.FromSeconds(120.0);

		// Token: 0x04000BC6 RID: 3014
		private static readonly List<ResponseCodeType> TransientServiceErrors = new List<ResponseCodeType>
		{
			ResponseCodeType.ErrorADOperation,
			ResponseCodeType.ErrorADUnavailable,
			ResponseCodeType.ErrorConnectionFailed,
			ResponseCodeType.ErrorInsufficientResources,
			ResponseCodeType.ErrorInternalServerTransientError,
			ResponseCodeType.ErrorMailboxMoveInProgress,
			ResponseCodeType.ErrorMailboxStoreUnavailable,
			ResponseCodeType.ErrorServerBusy,
			ResponseCodeType.ErrorCrossSiteRequest,
			ResponseCodeType.ErrorExceededConnectionCount
		};

		// Token: 0x04000BC7 RID: 3015
		private IRecipientSession recipientSession;

		// Token: 0x04000BC8 RID: 3016
		private ADUser user;

		// Token: 0x04000BC9 RID: 3017
		private Dictionary<Guid, StoreTagData> tagData;

		// Token: 0x04000BCA RID: 3018
		private bool fullCrawlRequired;

		// Token: 0x04000BCB RID: 3019
		private Dictionary<Guid, StoreTagData> defaultArchiveTagData;

		// Token: 0x04000BCC RID: 3020
		private List<Guid> deletedTags;

		// Token: 0x04000BCD RID: 3021
		private MailboxSession mailboxSession;

		// Token: 0x04000BCE RID: 3022
		private RetentionHoldData retentionHoldData;

		// Token: 0x04000BCF RID: 3023
		private ExchangePrincipal exchangePrincipal;

		// Token: 0x04000BD0 RID: 3024
		private bool configItemExists;

		// Token: 0x04000BD1 RID: 3025
		private bool isArchiveMailbox;

		// Token: 0x04000BD2 RID: 3026
		private ExchangeServiceBinding serviceBinding;

		// Token: 0x04000BD3 RID: 3027
		private UserConfiguration configItem;
	}
}
