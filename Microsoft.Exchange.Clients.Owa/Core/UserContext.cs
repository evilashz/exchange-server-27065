using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Web;
using Microsoft.Exchange.Clients.Common;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Clients.Owa.Core.Directory;
using Microsoft.Exchange.Clients.Owa.Core.Transcoding;
using Microsoft.Exchange.Clients.Owa2.Server.Core;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Search;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Data.Storage.RightsManagement;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Security.RightsManagement;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000279 RID: 633
	public sealed class UserContext : UserContextBase, ISessionContext
	{
		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x06001517 RID: 5399 RVA: 0x0007FB56 File Offset: 0x0007DD56
		// (set) Token: 0x06001518 RID: 5400 RVA: 0x0007FB5E File Offset: 0x0007DD5E
		internal UserContextTerminationStatus TerminationStatus { get; set; }

		// Token: 0x06001519 RID: 5401 RVA: 0x0007FB68 File Offset: 0x0007DD68
		internal UserContext(UserContextKey key) : base(key)
		{
			this.isProxy = true;
			this.isClientSideDataCollectingEnabled = this.GetIsClientSideDataCollectingEnabled();
			this.IsBposUser = false;
			if (!Globals.DisableBreadcrumbs)
			{
				this.breadcrumbBuffer = new BreadcrumbBuffer(Globals.MaxBreadcrumbs);
			}
			this.pendingRequestManager = new PendingRequestManager(this);
			this.irmLicensingManager = new IrmLicensingManager(this);
		}

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x0600151A RID: 5402 RVA: 0x0007FC80 File Offset: 0x0007DE80
		public static int ReminderPollInterval
		{
			get
			{
				return 28800;
			}
		}

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x0600151B RID: 5403 RVA: 0x0007FC88 File Offset: 0x0007DE88
		public InstantMessagingTypeOptions InstantMessagingType
		{
			get
			{
				PolicyConfiguration policyConfiguration;
				if (this.TryGetPolicyConfigurationFromCache(out policyConfiguration))
				{
					return policyConfiguration.InstantMessagingType;
				}
				return this.configuration.InstantMessagingType;
			}
		}

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x0600151C RID: 5404 RVA: 0x0007FCB1 File Offset: 0x0007DEB1
		public bool IsPushNotificationsEnabled
		{
			get
			{
				return !this.IsBasicExperience && !this.IsWebPartRequest && this.isPushNotificationsEnabled;
			}
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x0600151D RID: 5405 RVA: 0x0007FCCB File Offset: 0x0007DECB
		public bool IsPullNotificationsEnabled
		{
			get
			{
				return this.isPullNotificationsEnabled;
			}
		}

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x0600151E RID: 5406 RVA: 0x0007FCD4 File Offset: 0x0007DED4
		public string SetPhotoURL
		{
			get
			{
				PolicyConfiguration policyConfiguration;
				if (this.TryGetPolicyConfigurationFromCache(out policyConfiguration))
				{
					return policyConfiguration.SetPhotoURL;
				}
				return this.configuration.SetPhotoURL;
			}
		}

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x0600151F RID: 5407 RVA: 0x0007FCFD File Offset: 0x0007DEFD
		internal PerformanceNotifier PerformanceConsoleNotifier
		{
			get
			{
				if (this.performanceNotifier == null)
				{
					this.performanceNotifier = new PerformanceNotifier();
				}
				return this.performanceNotifier;
			}
		}

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x06001520 RID: 5408 RVA: 0x0007FD18 File Offset: 0x0007DF18
		internal MailTipsNotificationHandler MailTipsNotificationHandler
		{
			get
			{
				if (this.mailTipsNotificationHandler == null)
				{
					this.mailTipsNotificationHandler = new MailTipsNotificationHandler(this);
				}
				return this.mailTipsNotificationHandler;
			}
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x06001521 RID: 5409 RVA: 0x0007FD34 File Offset: 0x0007DF34
		// (set) Token: 0x06001522 RID: 5410 RVA: 0x0007FD3C File Offset: 0x0007DF3C
		public bool IsPerformanceConsoleOn
		{
			get
			{
				return this.isPerformanceConsoleOn;
			}
			set
			{
				this.isPerformanceConsoleOn = value;
				if (this.isPerformanceConsoleOn)
				{
					this.performanceNotifier.RegisterWithPendingRequestNotifier();
					return;
				}
				this.performanceNotifier.UnregisterWithPendingRequestNotifier();
			}
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x06001523 RID: 5411 RVA: 0x0007FD64 File Offset: 0x0007DF64
		public bool IsClientSideDataCollectingEnabled
		{
			get
			{
				return this.isClientSideDataCollectingEnabled;
			}
		}

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x06001524 RID: 5412 RVA: 0x0007FD6C File Offset: 0x0007DF6C
		internal bool IsEmbeddedReadingPaneDisabled
		{
			get
			{
				return this.shouldDisableEmbeddedReadingPane;
			}
		}

		// Token: 0x06001525 RID: 5413 RVA: 0x0007FD74 File Offset: 0x0007DF74
		internal void DisableEmbeddedReadingPane()
		{
			this.shouldDisableEmbeddedReadingPane = true;
		}

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x06001526 RID: 5414 RVA: 0x0007FD7D File Offset: 0x0007DF7D
		// (set) Token: 0x06001527 RID: 5415 RVA: 0x0007FD85 File Offset: 0x0007DF85
		internal AutoCompleteCache AutoCompleteCache
		{
			get
			{
				return this.autoCompleteCache;
			}
			set
			{
				this.autoCompleteCache = value;
			}
		}

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x06001528 RID: 5416 RVA: 0x0007FD8E File Offset: 0x0007DF8E
		// (set) Token: 0x06001529 RID: 5417 RVA: 0x0007FD96 File Offset: 0x0007DF96
		internal RoomsCache RoomsCache
		{
			get
			{
				return this.roomsCache;
			}
			set
			{
				this.roomsCache = value;
			}
		}

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x0600152A RID: 5418 RVA: 0x0007FD9F File Offset: 0x0007DF9F
		// (set) Token: 0x0600152B RID: 5419 RVA: 0x0007FDA7 File Offset: 0x0007DFA7
		internal SendFromCache SendFromCache
		{
			get
			{
				return this.sendFromCache;
			}
			set
			{
				this.sendFromCache = value;
			}
		}

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x0600152C RID: 5420 RVA: 0x0007FDB0 File Offset: 0x0007DFB0
		// (set) Token: 0x0600152D RID: 5421 RVA: 0x0007FDB8 File Offset: 0x0007DFB8
		internal SubscriptionCache SubscriptionCache
		{
			get
			{
				return this.subscriptionCache;
			}
			set
			{
				this.subscriptionCache = value;
			}
		}

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x0600152E RID: 5422 RVA: 0x0007FDC4 File Offset: 0x0007DFC4
		public ulong SegmentationFlags
		{
			get
			{
				this.ThrowIfProxy();
				PolicyConfiguration policyConfiguration;
				ulong num;
				if (this.TryGetPolicyConfigurationFromCache(out policyConfiguration))
				{
					num = policyConfiguration.SegmentationFlags;
				}
				else
				{
					num = this.configuration.SegmentationFlags;
				}
				if (this.shouldDisableUncAndWssFeatures)
				{
					num &= 18446744073705619455UL;
				}
				if (this.IsExplicitLogonOthersMailbox || this.shouldDisableTextMessageFeatures)
				{
					num &= 18446744073441116159UL;
				}
				return num & 18446744073705619455UL;
			}
		}

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x0600152F RID: 5423 RVA: 0x0007FE2C File Offset: 0x0007E02C
		public ulong RestrictedCapabilitiesFlags
		{
			get
			{
				if (this.restrictedCapabilitiesFlags == 0UL)
				{
					if (!VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).OwaDeployment.UsePersistedCapabilities.Enabled)
					{
						this.restrictedCapabilitiesFlags = this.SegmentationFlags;
					}
					else if (this.MailboxIdentity != null)
					{
						OWAMiniRecipient owaminiRecipient = this.MailboxIdentity.GetOWAMiniRecipient();
						if (owaminiRecipient[ADUserSchema.PersistedCapabilities] != null)
						{
							using (IEnumerator enumerator = Enum.GetValues(typeof(Feature)).GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									object obj = enumerator.Current;
									ulong num = (ulong)obj;
									if (num != 18446744073709551615UL)
									{
										string text = string.Format("Owa{0}Restrictions", Enum.GetName(typeof(Feature), num));
										try
										{
											if (ExchangeRunspaceConfiguration.IsFeatureValidOnObject(text, owaminiRecipient))
											{
												this.restrictedCapabilitiesFlags |= num;
											}
											else
											{
												ExTraceGlobals.UserContextTracer.TraceDebug<string>(0L, "Feature {0} is restricted by user capabilities", text);
											}
										}
										catch (ArgumentException ex)
										{
											ExTraceGlobals.UserContextTracer.TraceDebug<string, string>(0L, "There was an exception in ExchangeRunspaceConfiguration.IsFeatureValidOnObject when validating feature: {0}. {1}", text, ex.Message);
										}
									}
								}
								goto IL_127;
							}
						}
						this.restrictedCapabilitiesFlags = this.SegmentationFlags;
					}
				}
				IL_127:
				return this.restrictedCapabilitiesFlags;
			}
		}

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x06001530 RID: 5424 RVA: 0x0007FF84 File Offset: 0x0007E184
		public uint[] SegmentationBitsForJavascript
		{
			get
			{
				if (this.segmentationBitsForJavascript == null)
				{
					this.segmentationBitsForJavascript = Utilities.GetSegmentationBitsForJavascript(this);
				}
				return this.segmentationBitsForJavascript;
			}
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x06001531 RID: 5425 RVA: 0x0007FFA0 File Offset: 0x0007E1A0
		public AttachmentPolicy AttachmentPolicy
		{
			get
			{
				PolicyConfiguration policyConfiguration;
				if (this.TryGetPolicyConfigurationFromCache(out policyConfiguration))
				{
					return policyConfiguration.AttachmentPolicy;
				}
				return this.configuration.AttachmentPolicy;
			}
		}

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x06001532 RID: 5426 RVA: 0x0007FFCC File Offset: 0x0007E1CC
		public int DefaultClientLanguage
		{
			get
			{
				PolicyConfiguration policyConfiguration;
				if (this.TryGetPolicyConfigurationFromCache(out policyConfiguration))
				{
					return policyConfiguration.DefaultClientLanguage;
				}
				return this.configuration.DefaultClientLanguage;
			}
		}

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x06001533 RID: 5427 RVA: 0x0007FFF8 File Offset: 0x0007E1F8
		public int LogonAndErrorLanguage
		{
			get
			{
				PolicyConfiguration policyConfiguration;
				if (this.TryGetPolicyConfigurationFromCache(out policyConfiguration))
				{
					return policyConfiguration.LogonAndErrorLanguage;
				}
				return this.configuration.LogonAndErrorLanguage;
			}
		}

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x06001534 RID: 5428 RVA: 0x00080024 File Offset: 0x0007E224
		public bool UseGB18030
		{
			get
			{
				PolicyConfiguration policyConfiguration;
				if (this.TryGetPolicyConfigurationFromCache(out policyConfiguration))
				{
					return policyConfiguration.UseGB18030;
				}
				return this.configuration.UseGB18030;
			}
		}

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x06001535 RID: 5429 RVA: 0x00080050 File Offset: 0x0007E250
		public bool UseISO885915
		{
			get
			{
				PolicyConfiguration policyConfiguration;
				if (this.TryGetPolicyConfigurationFromCache(out policyConfiguration))
				{
					return policyConfiguration.UseISO885915;
				}
				return this.configuration.UseISO885915;
			}
		}

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x06001536 RID: 5430 RVA: 0x00080079 File Offset: 0x0007E279
		// (set) Token: 0x06001537 RID: 5431 RVA: 0x00080081 File Offset: 0x0007E281
		internal bool IsBposUser { get; private set; }

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x06001538 RID: 5432 RVA: 0x0008008A File Offset: 0x0007E28A
		// (set) Token: 0x06001539 RID: 5433 RVA: 0x00080092 File Offset: 0x0007E292
		internal string LastRecipientSessionDCServerName { get; set; }

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x0600153A RID: 5434 RVA: 0x0008009C File Offset: 0x0007E29C
		internal OutboundCharsetOptions OutboundCharset
		{
			get
			{
				PolicyConfiguration policyConfiguration;
				if (this.TryGetPolicyConfigurationFromCache(out policyConfiguration))
				{
					return policyConfiguration.OutboundCharset;
				}
				return this.configuration.OutboundCharset;
			}
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x0600153B RID: 5435 RVA: 0x000800C5 File Offset: 0x0007E2C5
		// (set) Token: 0x0600153C RID: 5436 RVA: 0x000800CD File Offset: 0x0007E2CD
		internal bool ShouldDisableUncAndWssFeatures
		{
			get
			{
				return this.shouldDisableUncAndWssFeatures;
			}
			set
			{
				this.shouldDisableUncAndWssFeatures = value;
			}
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x0600153D RID: 5437 RVA: 0x000800D6 File Offset: 0x0007E2D6
		internal PendingRequestManager PendingRequestManager
		{
			get
			{
				return this.pendingRequestManager;
			}
		}

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x0600153E RID: 5438 RVA: 0x000800DE File Offset: 0x0007E2DE
		internal OwaMapiNotificationManager MapiNotificationManager
		{
			get
			{
				this.ThrowIfProxy();
				if (this.mapiNotificationManager == null)
				{
					this.mapiNotificationManager = new OwaMapiNotificationManager(this);
				}
				return this.mapiNotificationManager;
			}
		}

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x0600153F RID: 5439 RVA: 0x00080100 File Offset: 0x0007E300
		public string Canary
		{
			get
			{
				return base.Key.Canary.ToString();
			}
		}

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x06001540 RID: 5440 RVA: 0x00080112 File Offset: 0x0007E312
		internal bool ArchiveAccessed
		{
			get
			{
				return this.archiveAccessed;
			}
		}

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x06001541 RID: 5441 RVA: 0x0008011C File Offset: 0x0007E31C
		internal bool HasArchive
		{
			get
			{
				bool result = false;
				if (this.exchangePrincipal != null)
				{
					IRecipientSession recipientSession = Utilities.CreateADRecipientSession(CultureInfo.CurrentCulture.LCID, false, ConsistencyMode.PartiallyConsistent, false, this, false);
					ADUser aduser = (ADUser)recipientSession.Read<ADUser>(this.ExchangePrincipal.ObjectId);
					if (aduser != null)
					{
						result = (aduser.ArchiveState == ArchiveState.Local || aduser.ArchiveState == ArchiveState.HostedProvisioned);
					}
				}
				return result;
			}
		}

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x06001542 RID: 5442 RVA: 0x0008017C File Offset: 0x0007E37C
		internal string ArchiveMailboxOwnerLegacyDN
		{
			get
			{
				string result = null;
				IMailboxInfo archiveMailbox = this.exchangePrincipal.GetArchiveMailbox();
				if (this.HasArchive && archiveMailbox != null)
				{
					result = this.exchangePrincipal.LegacyDn + "/guid=" + archiveMailbox.MailboxGuid;
				}
				return result;
			}
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x06001543 RID: 5443 RVA: 0x000801C4 File Offset: 0x0007E3C4
		internal string ArchiveMailboxDisplayName
		{
			get
			{
				string result = null;
				IMailboxInfo archiveMailbox = this.exchangePrincipal.GetArchiveMailbox();
				if (this.exchangePrincipal != null && archiveMailbox != null)
				{
					result = archiveMailbox.ArchiveName;
				}
				return result;
			}
		}

		// Token: 0x06001544 RID: 5444 RVA: 0x000801F2 File Offset: 0x0007E3F2
		public void CommitRecipientCaches()
		{
			if (this.AutoCompleteCache != null)
			{
				this.AutoCompleteCache.Commit(true);
			}
			if (this.RoomsCache != null)
			{
				this.RoomsCache.Commit(true);
			}
			if (this.SendFromCache != null)
			{
				this.SendFromCache.Commit(true);
			}
		}

		// Token: 0x06001545 RID: 5445 RVA: 0x00080230 File Offset: 0x0007E430
		internal bool HasValidMailboxSession()
		{
			return null != this.mailboxSession;
		}

		// Token: 0x06001546 RID: 5446 RVA: 0x0008023E File Offset: 0x0007E43E
		internal void CleanupOnEndRequest()
		{
			this.ThrowIfNotHoldingLock();
			this.ClearAllSessionHandles();
			this.DisconnectAllSessions();
		}

		// Token: 0x06001547 RID: 5447 RVA: 0x00080252 File Offset: 0x0007E452
		internal void DisconnectAllSessions()
		{
			this.ThrowIfNotHoldingLock();
			this.DisconnectMailboxSession();
			if (this.alternateMailboxSessionManager != null)
			{
				this.alternateMailboxSessionManager.DisconnectAllSessions();
			}
			if (this.publicFolderSessionCache != null)
			{
				this.publicFolderSessionCache.DisconnectAllSessions();
			}
		}

		// Token: 0x06001548 RID: 5448 RVA: 0x00080288 File Offset: 0x0007E488
		internal UserContextLoadResult Load(OwaContext owaContext)
		{
			ExTraceGlobals.UserContextCallTracer.TraceDebug<UserContext>(0L, "UserContext.Load, User context instance={0}", this);
			this.isExplicitLogon = owaContext.IsExplicitLogon;
			this.isDifferentMailbox = owaContext.IsDifferentMailbox;
			this.clientBrowserStatus = Utilities.GetClientBrowserStatus(owaContext.HttpContext.Request.Browser);
			this.isProxy = false;
			if (!FormsRegistryManager.IsLoaded)
			{
				throw new OwaInvalidOperationException("Forms registry hasn't been loaded", null, this);
			}
			this.isOWAEnabled = owaContext.ExchangePrincipal.MailboxInfo.Configuration.IsOwaEnabled;
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(null, true, ConsistencyMode.IgnoreInvalid, null, owaContext.ExchangePrincipal.MailboxInfo.OrganizationId.ToADSessionSettings(), 1174, "Load", "f:\\15.00.1497\\sources\\dev\\clients\\src\\owa\\bin\\core\\UserContext.cs");
			ADRecipient adrecipient = DirectoryHelper.ReadADRecipient(owaContext.ExchangePrincipal.MailboxInfo.MailboxGuid, owaContext.ExchangePrincipal.MailboxInfo.IsArchive, tenantOrRootOrgRecipientSession);
			if (adrecipient != null)
			{
				this.isHiddenUser = adrecipient.HiddenFromAddressListsEnabled;
			}
			this.owaMailboxPolicy = owaContext.ExchangePrincipal.MailboxInfo.Configuration.OwaMailboxPolicy;
			ADObjectId adobjectId;
			if (OwaSegmentationSettings.UpdateOwaMailboxPolicy(owaContext.ExchangePrincipal.MailboxInfo.OrganizationId, this.owaMailboxPolicy, out adobjectId))
			{
				this.owaMailboxPolicy = adobjectId;
			}
			if (this.owaMailboxPolicy == null)
			{
				ExTraceGlobals.UserContextTracer.TraceDebug(0L, "No OwaMailboxPolicy available applied to this user");
			}
			else
			{
				ExTraceGlobals.UserContextTracer.TraceDebug<string, Guid, string>(0L, "OwaMailboxPolicy  applied to this user. Policy name = {0}, id = {1}, DN = {2}", this.owaMailboxPolicy.Name, this.owaMailboxPolicy.ObjectGuid, this.owaMailboxPolicy.DistinguishedName);
			}
			try
			{
				this.mailboxSession = this.CreateMailboxSession(owaContext);
			}
			catch
			{
				PerformanceCounterManager.AddStoreLogonResult(false);
				throw;
			}
			if (this.mailboxSession != null)
			{
				PerformanceCounterManager.AddStoreLogonResult(true);
				if (this.CanActAsOwner)
				{
					using (VersionedXmlDataProvider versionedXmlDataProvider = new VersionedXmlDataProvider(this.mailboxSession))
					{
						TextMessagingAccount textMessagingAccount = (TextMessagingAccount)versionedXmlDataProvider.Read<TextMessagingAccount>(null);
						if (textMessagingAccount.TextMessagingSettings.PersonToPersonPreferences.Count != 0)
						{
							this.shouldDisableTextMessageFeatures = false;
						}
						goto IL_200;
					}
				}
				this.shouldDisableTextMessageFeatures = true;
				ExTraceGlobals.UserContextTracer.TraceDebug(0L, "No permission to read the text message configuration setting.");
			}
			else
			{
				PerformanceCounterManager.AddStoreLogonResult(false);
			}
			IL_200:
			if (this.userOptions == null)
			{
				this.userOptions = new UserOptions(this);
			}
			bool flag = true;
			try
			{
				this.userOptions.LoadAll();
			}
			catch (QuotaExceededException ex)
			{
				ExTraceGlobals.UserContextCallTracer.TraceDebug<string>(0L, "UserContext.Load: userOptions.LoadAll failed. Exception: {0}", ex.Message);
				flag = false;
			}
			string timeZoneName = this.userOptions.TimeZone;
			if (!ExTimeZoneEnumerator.Instance.TryGetTimeZoneByName(timeZoneName, out this.timeZone))
			{
				ExTraceGlobals.UserContextTracer.TraceDebug(0L, "The timezone id in the user options is not valid");
				if (this.Configuration.DefaultClientLanguage <= 0 && !this.IsWebPartRequest && flag)
				{
					if (this.mapiNotificationManager != null)
					{
						this.mapiNotificationManager.Dispose();
						this.mapiNotificationManager = null;
					}
					this.ClearAllSessionHandles();
					this.DisconnectMailboxSession();
					this.mailboxSession.Dispose();
					this.mailboxSession = null;
					return UserContextLoadResult.InvalidTimeZoneKeyName;
				}
				this.timeZone = ExTimeZone.CurrentTimeZone;
			}
			this.TimeZone = this.timeZone;
			RequestDispatcherUtilities.LookupExperiencesForRequest(owaContext, this.userOptions.IsOptimizedForAccessibility, this.IsFeatureEnabled(Feature.RichClient), out this.browserType, out this.browserVersion, out this.experiences);
			if (this.experiences == null || this.experiences.Length == 0)
			{
				throw new OwaClientNotSupportedException("FormsRegistryManager.LookupExperiences couldn't find any experience for this client.", null, this);
			}
			this.browserPlatform = Utilities.GetBrowserPlatform(owaContext.HttpContext.Request.UserAgent);
			this.isMonitoringRequest = UserAgentUtilities.IsMonitoringRequest(owaContext.HttpContext.Request.UserAgent);
			if (!this.IsBasicExperience || this.IsFeatureEnabled(Feature.OWALight))
			{
				this.LoadUserTheme();
				this.messageViewFirstRender = true;
				this.lastClientViewState = new DefaultClientViewState();
				this.mailboxOwnerLegacyDN = this.MailboxSession.MailboxOwnerLegacyDN;
				OWAMiniRecipient owaminiRecipient = owaContext.LogonIdentity.GetOWAMiniRecipient();
				this.LastRecipientSessionDCServerName = owaContext.LogonIdentity.LastRecipientSessionDCServerName;
				this.IsBposUser = CapabilityHelper.HasBposSKUCapability(owaminiRecipient.PersistedCapabilities);
				if (this.InstantMessagingType == InstantMessagingTypeOptions.Ocs)
				{
					this.sipUri = InstantMessageUtilities.GetSipUri(owaminiRecipient);
				}
				this.mobilePhoneNumber = owaminiRecipient.MobilePhoneNumber;
				this.isLoaded = true;
				return UserContextLoadResult.Success;
			}
			if (!this.IsFeatureEnabled(Feature.RichClient))
			{
				throw new OwaDisabledException();
			}
			if (this.userOptions.IsOptimizedForAccessibility || RequestDispatcherUtilities.IsLayoutParameterForLight(owaContext.HttpContext.Request))
			{
				throw new OwaLightDisabledException();
			}
			throw new OwaBrowserUpdateRequiredException(this.browserPlatform);
		}

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x06001549 RID: 5449 RVA: 0x00080704 File Offset: 0x0007E904
		internal bool IsFullyInitialized
		{
			get
			{
				return this.isFullyIntialized;
			}
		}

		// Token: 0x0600154A RID: 5450 RVA: 0x0008070C File Offset: 0x0007E90C
		internal void SetFullyInitialized()
		{
			this.isFullyIntialized = true;
		}

		// Token: 0x0600154B RID: 5451 RVA: 0x00080715 File Offset: 0x0007E915
		public bool IsSignedOutOfIM()
		{
			return InstantMessageUtilities.IsSignedOut(this);
		}

		// Token: 0x0600154C RID: 5452 RVA: 0x0008071D File Offset: 0x0007E91D
		public void SaveSignedOutOfIMStatus()
		{
			InstantMessageUtilities.SetSignedOutFlag(this, true);
		}

		// Token: 0x0600154D RID: 5453 RVA: 0x00080726 File Offset: 0x0007E926
		public void SaveSignedInToIMStatus()
		{
			InstantMessageUtilities.SetSignedOutFlag(this, false);
		}

		// Token: 0x0600154E RID: 5454 RVA: 0x00080730 File Offset: 0x0007E930
		internal void RecreateMailboxSession(OwaContext owaContext)
		{
			if (owaContext == null)
			{
				throw new ArgumentNullException("owaContext");
			}
			if (this.mailboxSession == null)
			{
				throw new InvalidOperationException("Cannot call RecreateMailboxSession if mailboxSession is null");
			}
			this.timeZone = this.mailboxSession.ExTimeZone;
			if (this.mapiNotificationManager != null)
			{
				this.mapiNotificationManager.Dispose();
				this.mapiNotificationManager = null;
			}
			this.ClearAllSessionHandles();
			this.DisconnectMailboxSession();
			this.mailboxSession.Dispose();
			this.mailboxSession = this.CreateMailboxSession(owaContext);
			this.mailboxSession.ExTimeZone = this.timeZone;
		}

		// Token: 0x0600154F RID: 5455 RVA: 0x000807BE File Offset: 0x0007E9BE
		internal void RecreatePublicFolderSessions()
		{
			if (this.publicFolderSessionCache != null)
			{
				this.publicFolderSessionCache.Dispose();
				this.publicFolderSessionCache = null;
			}
		}

		// Token: 0x06001550 RID: 5456 RVA: 0x000807DA File Offset: 0x0007E9DA
		public bool IsPublicRequest(HttpRequest request)
		{
			return UserContextUtilities.IsPublicRequest(request);
		}

		// Token: 0x06001551 RID: 5457 RVA: 0x000807E4 File Offset: 0x0007E9E4
		private MailboxSession CreateMailboxSession(OwaContext owaContext)
		{
			MailboxSession mailboxSession;
			if (!this.IsWebPartRequest)
			{
				mailboxSession = owaContext.LogonIdentity.CreateMailboxSession(owaContext.ExchangePrincipal, Thread.CurrentThread.CurrentCulture, owaContext.HttpContext.Request);
			}
			else
			{
				mailboxSession = owaContext.LogonIdentity.CreateWebPartMailboxSession(owaContext.ExchangePrincipal, Thread.CurrentThread.CurrentCulture, owaContext.HttpContext.Request);
			}
			this.canActAsOwner = mailboxSession.CanActAsOwner;
			return mailboxSession;
		}

		// Token: 0x06001552 RID: 5458 RVA: 0x00080858 File Offset: 0x0007EA58
		private void LoadUserTheme()
		{
			if (this.IsBasicExperience)
			{
				this.theme = ThemeManager.BaseTheme;
				return;
			}
			if (!this.IsFeatureEnabled(Feature.Themes))
			{
				this.theme = this.DefaultTheme;
				return;
			}
			if (string.IsNullOrEmpty(this.userOptions.ThemeStorageId))
			{
				this.theme = this.DefaultTheme;
				return;
			}
			uint idFromStorageId = ThemeManager.GetIdFromStorageId(this.userOptions.ThemeStorageId);
			if (idFromStorageId == 4294967295U)
			{
				this.theme = this.DefaultTheme;
				return;
			}
			this.theme = ThemeManager.Themes[(int)((UIntPtr)idFromStorageId)];
		}

		// Token: 0x06001553 RID: 5459 RVA: 0x000808E3 File Offset: 0x0007EAE3
		public void OnPostLoadUserContext()
		{
			this.RefreshIsJunkEmailEnabled();
			this.GetAllFolderPolicy();
			this.GetMailboxQuotaLimits();
		}

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x06001554 RID: 5460 RVA: 0x000808F7 File Offset: 0x0007EAF7
		// (set) Token: 0x06001555 RID: 5461 RVA: 0x000808FF File Offset: 0x0007EAFF
		internal ExchangePrincipal ExchangePrincipal
		{
			get
			{
				return this.exchangePrincipal;
			}
			set
			{
				this.exchangePrincipal = value;
			}
		}

		// Token: 0x06001556 RID: 5462 RVA: 0x00080908 File Offset: 0x0007EB08
		internal void UpdateDisplayPictureCanary()
		{
			this.displayPictureChangeTime = DateTime.UtcNow.ToBinary();
		}

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x06001557 RID: 5463 RVA: 0x00080928 File Offset: 0x0007EB28
		internal string DisplayPictureCanary
		{
			get
			{
				return this.displayPictureChangeTime.ToString("X");
			}
		}

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x06001558 RID: 5464 RVA: 0x0008093A File Offset: 0x0007EB3A
		// (set) Token: 0x06001559 RID: 5465 RVA: 0x00080942 File Offset: 0x0007EB42
		internal byte[] UploadedDisplayPicture
		{
			get
			{
				return this.uploadedDisplayPicture;
			}
			set
			{
				this.uploadedDisplayPicture = value;
			}
		}

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x0600155A RID: 5466 RVA: 0x0008094B File Offset: 0x0007EB4B
		// (set) Token: 0x0600155B RID: 5467 RVA: 0x00080953 File Offset: 0x0007EB53
		internal bool? HasPicture
		{
			get
			{
				return this.hasPicture;
			}
			set
			{
				this.hasPicture = value;
			}
		}

		// Token: 0x0600155C RID: 5468 RVA: 0x0008095C File Offset: 0x0007EB5C
		private void ThrowIfProxy()
		{
			if (this.IsProxy)
			{
				throw new OwaInvalidOperationException("Operation not allowed in a proxy user context");
			}
		}

		// Token: 0x0600155D RID: 5469 RVA: 0x00080971 File Offset: 0x0007EB71
		internal bool IsSafeToAccessFromCurrentThread()
		{
			return !this.isLoaded || base.LockedByCurrentThread();
		}

		// Token: 0x0600155E RID: 5470 RVA: 0x00080983 File Offset: 0x0007EB83
		private void ThrowIfNotHoldingLock()
		{
			if (!this.IsSafeToAccessFromCurrentThread())
			{
				throw new InvalidOperationException("Attempted to use MailboxSession in a thread that wasn't holding the UC lock");
			}
		}

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x0600155F RID: 5471 RVA: 0x00080998 File Offset: 0x0007EB98
		// (set) Token: 0x06001560 RID: 5472 RVA: 0x000809A0 File Offset: 0x0007EBA0
		public Configuration Configuration
		{
			get
			{
				return this.configuration;
			}
			set
			{
				this.configuration = value;
			}
		}

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x06001561 RID: 5473 RVA: 0x000809A9 File Offset: 0x0007EBA9
		internal MailboxSession MailboxSession
		{
			get
			{
				this.ThrowIfNotHoldingLock();
				this.ThrowIfProxy();
				Utilities.ReconnectStoreSession(this.mailboxSession, this);
				if (this.mailboxSession.ItemBinder == null)
				{
					this.mailboxSession.ItemBinder = this.ItemBinder;
				}
				return this.mailboxSession;
			}
		}

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x06001562 RID: 5474 RVA: 0x000809E8 File Offset: 0x0007EBE8
		internal PublicFolderSession DefaultPublicFolderSession
		{
			get
			{
				this.ThrowIfNotHoldingLock();
				this.ThrowIfProxy();
				if (!this.IsFeatureEnabled(Feature.PublicFolders))
				{
					throw new OwaSegmentationException("Public Folder feature is disabled");
				}
				PublicFolderSession publicFolderHierarchySession = this.PublicFolderSessionCache.GetPublicFolderHierarchySession();
				Utilities.ReconnectStoreSession(publicFolderHierarchySession, this);
				return publicFolderHierarchySession;
			}
		}

		// Token: 0x06001563 RID: 5475 RVA: 0x00080A2C File Offset: 0x0007EC2C
		internal bool IsPublicFoldersAvailable()
		{
			try
			{
				return this.DefaultPublicFolderSession != null;
			}
			catch (StoragePermanentException)
			{
			}
			catch (StorageTransientException)
			{
			}
			catch (OwaSegmentationException)
			{
			}
			return false;
		}

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x06001564 RID: 5476 RVA: 0x00080A7C File Offset: 0x0007EC7C
		// (set) Token: 0x06001565 RID: 5477 RVA: 0x00080A84 File Offset: 0x0007EC84
		internal PublicFolderViewStatesCache PublicFolderViewStatesCache
		{
			get
			{
				return this.publicFolderViewStatesCache;
			}
			set
			{
				this.publicFolderViewStatesCache = value;
			}
		}

		// Token: 0x06001566 RID: 5478 RVA: 0x00080A8D File Offset: 0x0007EC8D
		internal FolderViewStates GetFolderViewStates(Folder folder)
		{
			if (this.IsInMyMailbox(folder))
			{
				return new MailboxFolderViewStates(folder);
			}
			return new PublicFolderViewStates(this, folder);
		}

		// Token: 0x06001567 RID: 5479 RVA: 0x00080AA8 File Offset: 0x0007ECA8
		internal PublicFolderSession GetContentAvailableSession(StoreObjectId publicFolderStoreObjectId)
		{
			if (publicFolderStoreObjectId == null)
			{
				throw new ArgumentNullException("publicFolderStoreObjectId");
			}
			if (!this.IsFeatureEnabled(Feature.PublicFolders))
			{
				throw new OwaSegmentationException("Public Folder feature is disabled");
			}
			Utilities.ReconnectStoreSession(this.PublicFolderSessionCache.GetPublicFolderHierarchySession(), this);
			PublicFolderSession publicFolderSession = this.PublicFolderSessionCache.GetPublicFolderSession(publicFolderStoreObjectId);
			Utilities.ReconnectStoreSession(publicFolderSession, this);
			return publicFolderSession;
		}

		// Token: 0x06001568 RID: 5480 RVA: 0x00080B00 File Offset: 0x0007ED00
		internal DelegateSessionHandle GetDelegateSessionHandle(ExchangePrincipal exchangePrincipal)
		{
			return this.MailboxSession.GetDelegateSessionHandle(exchangePrincipal);
		}

		// Token: 0x06001569 RID: 5481 RVA: 0x00080B1C File Offset: 0x0007ED1C
		internal MailboxSession GetArchiveMailboxSession(string mailboxOwnerLegacyDN)
		{
			ExchangePrincipal principal = this.AlternateMailboxSessionManager.GetExchangePrincipal(mailboxOwnerLegacyDN);
			return this.GetArchiveMailboxSession(principal);
		}

		// Token: 0x0600156A RID: 5482 RVA: 0x00080B40 File Offset: 0x0007ED40
		internal MailboxSession GetArchiveMailboxSession(IExchangePrincipal principal)
		{
			if (principal == null)
			{
				throw new ArgumentNullException("principal");
			}
			if (!principal.MailboxInfo.IsArchive)
			{
				throw new ArgumentException("principal is not for archive mailbox");
			}
			return this.AlternateMailboxSessionManager.GetMailboxSession(principal);
		}

		// Token: 0x0600156B RID: 5483 RVA: 0x00080B84 File Offset: 0x0007ED84
		internal void TryLoopArchiveMailboxes(UserContext.DelegateWithMailboxSession doWithArchiveSession)
		{
			if (!this.IsExplicitLogon && !this.ExchangePrincipal.MailboxInfo.IsArchive && !this.ExchangePrincipal.MailboxInfo.IsAggregated)
			{
				IMailboxInfo archiveMailbox = this.ExchangePrincipal.GetArchiveMailbox();
				if (archiveMailbox != null)
				{
					ExchangePrincipal archiveExchangePrincipal = this.ExchangePrincipal.GetArchiveExchangePrincipal(RemotingOptions.AllowCrossSite | RemotingOptions.AllowCrossPremise);
					Guid mailboxGuid = archiveExchangePrincipal.MailboxInfo.MailboxGuid;
					MailboxSession mailboxSession = null;
					try
					{
						mailboxSession = this.GetArchiveMailboxSession(archiveExchangePrincipal);
					}
					catch (MailboxCrossSiteFailoverException innerException)
					{
						string message = string.Format(CultureInfo.InvariantCulture, LocalizedStrings.GetNonEncoded(-1277698471), new object[]
						{
							mailboxGuid.ToString()
						});
						throw new OwaArchiveInTransitException(message, innerException);
					}
					catch (WrongServerException innerException2)
					{
						string message2 = string.Format(CultureInfo.InvariantCulture, LocalizedStrings.GetNonEncoded(-1277698471), new object[]
						{
							mailboxGuid.ToString()
						});
						throw new OwaArchiveInTransitException(message2, innerException2);
					}
					catch (MailboxInTransitException innerException3)
					{
						string message3 = string.Format(CultureInfo.InvariantCulture, LocalizedStrings.GetNonEncoded(-1277698471), new object[]
						{
							mailboxGuid.ToString()
						});
						throw new OwaArchiveInTransitException(message3, innerException3);
					}
					catch (StoragePermanentException innerException4)
					{
						string message4 = string.Format(CultureInfo.InvariantCulture, LocalizedStrings.GetNonEncoded(-1277698471), new object[]
						{
							mailboxGuid.ToString()
						});
						throw new OwaArchiveNotAvailableException(message4, innerException4);
					}
					catch (StorageTransientException innerException5)
					{
						string message5 = string.Format(CultureInfo.InvariantCulture, LocalizedStrings.GetNonEncoded(-1277698471), new object[]
						{
							mailboxGuid.ToString()
						});
						throw new OwaArchiveNotAvailableException(message5, innerException5);
					}
					if (mailboxSession != null)
					{
						this.archiveAccessed = true;
						doWithArchiveSession(mailboxSession);
					}
				}
			}
		}

		// Token: 0x0600156C RID: 5484 RVA: 0x00080DC8 File Offset: 0x0007EFC8
		internal OwaStoreObjectId GetArchiveRootFolderId()
		{
			if (this.archiveRootFolderId != null)
			{
				return this.archiveRootFolderId;
			}
			OwaStoreObjectId archiveFolderId = null;
			this.TryLoopArchiveMailboxes(delegate(MailboxSession archiveSession)
			{
				StoreObjectId defaultFolderId = archiveSession.GetDefaultFolderId(DefaultFolderType.Root);
				archiveFolderId = OwaStoreObjectId.CreateFromSessionFolderId(this, archiveSession, defaultFolderId);
				this.archiveRootFolderId = archiveFolderId;
			});
			return archiveFolderId;
		}

		// Token: 0x0600156D RID: 5485 RVA: 0x00080E10 File Offset: 0x0007F010
		internal string GetArchiveRootFolderIdString()
		{
			string result = null;
			OwaStoreObjectId owaStoreObjectId = this.GetArchiveRootFolderId();
			if (owaStoreObjectId != null)
			{
				result = owaStoreObjectId.ToString();
			}
			return result;
		}

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x0600156E RID: 5486 RVA: 0x00080E34 File Offset: 0x0007F034
		private PublicFolderSessionCache PublicFolderSessionCache
		{
			get
			{
				if (this.publicFolderSessionCache == null)
				{
					this.publicFolderSessionCache = new PublicFolderSessionCache(this.ExchangePrincipal.MailboxInfo.OrganizationId, this.ExchangePrincipal, this.LogonIdentity.ClientSecurityContext, this.UserCulture, "Client=OWA;Action=PublicFolder", null, null, false);
				}
				return this.publicFolderSessionCache;
			}
		}

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x0600156F RID: 5487 RVA: 0x00080E89 File Offset: 0x0007F089
		private AlternateMailboxSessionManager AlternateMailboxSessionManager
		{
			get
			{
				if (this.alternateMailboxSessionManager == null)
				{
					this.alternateMailboxSessionManager = new AlternateMailboxSessionManager(this);
				}
				return this.alternateMailboxSessionManager;
			}
		}

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x06001570 RID: 5488 RVA: 0x00080EA5 File Offset: 0x0007F0A5
		internal OwaNotificationManager NotificationManager
		{
			get
			{
				this.ThrowIfProxy();
				if (this.notificationManager == null)
				{
					this.notificationManager = new OwaNotificationManager();
				}
				return this.notificationManager;
			}
		}

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x06001571 RID: 5489 RVA: 0x00080EC6 File Offset: 0x0007F0C6
		internal InstantMessageManager InstantMessageManager
		{
			get
			{
				this.ThrowIfProxy();
				if (this.PrivateIsInstantMessageEnabled(true) && this.instantMessageManager == null)
				{
					this.instantMessageManager = new InstantMessageManager(this);
				}
				return this.instantMessageManager;
			}
		}

		// Token: 0x06001572 RID: 5490 RVA: 0x00080EF1 File Offset: 0x0007F0F1
		internal StoreObjectId TryGetMyDefaultFolderId(DefaultFolderType defaultFolderType)
		{
			return this.MailboxSession.GetDefaultFolderId(defaultFolderType);
		}

		// Token: 0x06001573 RID: 5491 RVA: 0x00080EFF File Offset: 0x0007F0FF
		private StoreObjectId GetMyDefaultFolderId(DefaultFolderType defaultFolderType)
		{
			return Utilities.GetDefaultFolderId(this.MailboxSession, defaultFolderType);
		}

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x06001574 RID: 5492 RVA: 0x00080F0D File Offset: 0x0007F10D
		internal StoreObjectId CalendarFolderId
		{
			get
			{
				return this.GetMyDefaultFolderId(DefaultFolderType.Calendar);
			}
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x06001575 RID: 5493 RVA: 0x00080F16 File Offset: 0x0007F116
		public string CalendarFolderOwaIdString
		{
			get
			{
				return this.CalendarFolderOwaId.ToString();
			}
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x06001576 RID: 5494 RVA: 0x00080F23 File Offset: 0x0007F123
		internal OwaStoreObjectId CalendarFolderOwaId
		{
			get
			{
				return OwaStoreObjectId.CreateFromMailboxFolderId(this.CalendarFolderId);
			}
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x06001577 RID: 5495 RVA: 0x00080F30 File Offset: 0x0007F130
		internal StoreObjectId ContactsFolderId
		{
			get
			{
				return this.GetMyDefaultFolderId(DefaultFolderType.Contacts);
			}
		}

		// Token: 0x06001578 RID: 5496 RVA: 0x00080F3C File Offset: 0x0007F13C
		internal OwaStoreObjectId GetDeletedItemsFolderId(MailboxSession mailboxSession)
		{
			StoreObjectId defaultFolderId = mailboxSession.GetDefaultFolderId(DefaultFolderType.DeletedItems);
			return OwaStoreObjectId.CreateFromSessionFolderId(this, mailboxSession, defaultFolderId);
		}

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x06001579 RID: 5497 RVA: 0x00080F59 File Offset: 0x0007F159
		internal StoreObjectId DraftsFolderId
		{
			get
			{
				if (this.draftsFolderId == null)
				{
					this.draftsFolderId = this.GetMyDefaultFolderId(DefaultFolderType.Drafts);
				}
				return this.draftsFolderId;
			}
		}

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x0600157A RID: 5498 RVA: 0x00080F76 File Offset: 0x0007F176
		internal StoreObjectId InboxFolderId
		{
			get
			{
				if (this.inboxFolderId == null)
				{
					this.inboxFolderId = this.GetMyDefaultFolderId(DefaultFolderType.Inbox);
				}
				return this.inboxFolderId;
			}
		}

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x0600157B RID: 5499 RVA: 0x00080F93 File Offset: 0x0007F193
		internal StoreObjectId JunkEmailFolderId
		{
			get
			{
				return this.GetMyDefaultFolderId(DefaultFolderType.JunkEmail);
			}
		}

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x0600157C RID: 5500 RVA: 0x00080F9C File Offset: 0x0007F19C
		internal StoreObjectId JournalFolderId
		{
			get
			{
				StoreObjectId result;
				try
				{
					result = this.GetMyDefaultFolderId(DefaultFolderType.Journal);
				}
				catch (ObjectNotFoundException)
				{
					result = null;
				}
				return result;
			}
		}

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x0600157D RID: 5501 RVA: 0x00080FCC File Offset: 0x0007F1CC
		internal StoreObjectId NotesFolderId
		{
			get
			{
				StoreObjectId result;
				try
				{
					result = this.GetMyDefaultFolderId(DefaultFolderType.Notes);
				}
				catch (ObjectNotFoundException)
				{
					result = null;
				}
				return result;
			}
		}

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x0600157E RID: 5502 RVA: 0x00080FFC File Offset: 0x0007F1FC
		internal StoreObjectId OutboxFolderId
		{
			get
			{
				return this.GetMyDefaultFolderId(DefaultFolderType.Outbox);
			}
		}

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x0600157F RID: 5503 RVA: 0x00081006 File Offset: 0x0007F206
		internal StoreObjectId RemindersSearchFolderId
		{
			get
			{
				if (this.remindersSearchFolderId == null)
				{
					this.remindersSearchFolderId = this.GetMyDefaultFolderId(DefaultFolderType.Reminders);
				}
				return this.remindersSearchFolderId;
			}
		}

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x06001580 RID: 5504 RVA: 0x00081024 File Offset: 0x0007F224
		internal OwaStoreObjectId RemindersSearchFolderOwaId
		{
			get
			{
				if (this.remindersSearchFolderId == null)
				{
					this.remindersSearchFolderId = this.GetMyDefaultFolderId(DefaultFolderType.Reminders);
				}
				return OwaStoreObjectId.CreateFromMailboxFolderId(this.remindersSearchFolderId);
			}
		}

		// Token: 0x06001581 RID: 5505 RVA: 0x00081047 File Offset: 0x0007F247
		internal StoreObjectId GetRootFolderId(MailboxSession mailboxSession)
		{
			return mailboxSession.GetDefaultFolderId(DefaultFolderType.Root);
		}

		// Token: 0x06001582 RID: 5506 RVA: 0x00081054 File Offset: 0x0007F254
		internal OwaStoreObjectId GetSearchFoldersId(MailboxSession mailboxSession)
		{
			StoreObjectId defaultFolderId = mailboxSession.GetDefaultFolderId(DefaultFolderType.SearchFolders);
			return OwaStoreObjectId.CreateFromSessionFolderId(this, mailboxSession, defaultFolderId);
		}

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x06001583 RID: 5507 RVA: 0x00081072 File Offset: 0x0007F272
		internal StoreObjectId SentItemsFolderId
		{
			get
			{
				return this.GetMyDefaultFolderId(DefaultFolderType.SentItems);
			}
		}

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x06001584 RID: 5508 RVA: 0x0008107C File Offset: 0x0007F27C
		internal StoreObjectId TasksFolderId
		{
			get
			{
				return this.GetMyDefaultFolderId(DefaultFolderType.Tasks);
			}
		}

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x06001585 RID: 5509 RVA: 0x00081086 File Offset: 0x0007F286
		internal OwaStoreObjectId TasksFolderOwaId
		{
			get
			{
				return OwaStoreObjectId.CreateFromMailboxFolderId(this.TasksFolderId);
			}
		}

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x06001586 RID: 5510 RVA: 0x00081093 File Offset: 0x0007F293
		internal StoreObjectId FlaggedItemsAndTasksFolderId
		{
			get
			{
				return this.GetMyDefaultFolderId(DefaultFolderType.ToDoSearch);
			}
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x06001587 RID: 5511 RVA: 0x0008109D File Offset: 0x0007F29D
		internal StoreObjectId PublicFolderRootId
		{
			get
			{
				return this.DefaultPublicFolderSession.GetIpmSubtreeFolderId();
			}
		}

		// Token: 0x06001588 RID: 5512 RVA: 0x000810AC File Offset: 0x0007F2AC
		private StoreObjectId TryGetPublicFolderRootId()
		{
			try
			{
				return this.PublicFolderRootId;
			}
			catch (StoragePermanentException)
			{
			}
			catch (StorageTransientException)
			{
			}
			catch (OwaSegmentationException)
			{
			}
			return null;
		}

		// Token: 0x06001589 RID: 5513 RVA: 0x000810F8 File Offset: 0x0007F2F8
		internal string TryGetPublicFolderRootIdString()
		{
			StoreObjectId storeObjectId = this.TryGetPublicFolderRootId();
			if (storeObjectId != null)
			{
				return OwaStoreObjectId.CreateFromPublicFolderId(storeObjectId).ToString();
			}
			return null;
		}

		// Token: 0x0600158A RID: 5514 RVA: 0x0008111C File Offset: 0x0007F31C
		internal bool IsPublicFolderRootId(StoreObjectId folderId)
		{
			if (folderId == null)
			{
				throw new ArgumentNullException("folderId");
			}
			return folderId.Equals(this.TryGetPublicFolderRootId());
		}

		// Token: 0x0600158B RID: 5515 RVA: 0x00081138 File Offset: 0x0007F338
		internal void ReloadUserSettings(OwaContext owaContext, UserSettings settingToReload)
		{
			try
			{
				NewNotification newItemNotify = this.userOptions.NewItemNotify;
				this.userOptions.ReloadAll();
				if (Utilities.IsFlagSet((int)settingToReload, 16))
				{
					this.MailboxSession.PreferedCulture.DateTimeFormat.ShortDatePattern = this.UserOptions.DateFormat;
					this.MailboxSession.PreferedCulture.DateTimeFormat.ShortTimePattern = this.UserOptions.TimeFormat;
					DateTimeUtilities.SetSessionTimeZone(this);
				}
				if (Utilities.IsFlagSet((int)settingToReload, 4))
				{
					this.workingHours = WorkingHours.CreateForSession(this.MailboxSession, this.TimeZone);
					this.calendarSettings = CalendarSettings.CreateForSession(this);
				}
				if (Utilities.IsFlagSet((int)settingToReload, 32))
				{
					CultureInfo cultureInfo = null;
					HttpCookie httpCookie = HttpContext.Current.Request.Cookies["mkt"];
					if (httpCookie != null && !string.IsNullOrEmpty(httpCookie.Value))
					{
						cultureInfo = Culture.GetSupportedBrowserLanguage(httpCookie.Value);
					}
					if (cultureInfo == null)
					{
						IRecipientSession recipientSession = Utilities.CreateADRecipientSession(CultureInfo.CurrentCulture.LCID, false, ConsistencyMode.PartiallyConsistent, false, this, false);
						ADUser aduser = (ADUser)recipientSession.Read<ADUser>(this.ExchangePrincipal.ObjectId);
						if (aduser == null)
						{
							throw new OwaADObjectNotFoundException();
						}
						cultureInfo = Culture.GetPreferredCulture(aduser, owaContext.UserContext);
					}
					if (cultureInfo != null)
					{
						this.UserCulture = cultureInfo;
						Culture.UpdateUserCulture(owaContext.UserContext, this.UserCulture, false);
					}
					else
					{
						ExTraceGlobals.UserContextCallTracer.TraceDebug(0L, "UserContext.ReloadUserSettings: The user doesn't have a valid culture in AD.");
					}
				}
				if (Utilities.IsFlagSet((int)settingToReload, 1))
				{
					NewNotification newItemNotify2 = this.userOptions.NewItemNotify;
					if (newItemNotify2 == NewNotification.None && newItemNotify != newItemNotify2)
					{
						if (this.IsPushNotificationsEnabled)
						{
							this.MapiNotificationManager.UnsubscribeNewMail();
						}
						if (this.IsPullNotificationsEnabled)
						{
							this.NotificationManager.DisposeOwaLastEventAdvisor();
						}
					}
				}
			}
			catch (QuotaExceededException ex)
			{
				ExTraceGlobals.UserContextCallTracer.TraceDebug<string>(0L, "UserContext.ReloadUserSettings: userOptions.LoadAll failed. Exception: {0}", ex.Message);
			}
		}

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x0600158C RID: 5516 RVA: 0x00081314 File Offset: 0x0007F514
		public UserOptions UserOptions
		{
			get
			{
				this.ThrowIfProxy();
				return this.userOptions;
			}
		}

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x0600158D RID: 5517 RVA: 0x00081322 File Offset: 0x0007F522
		public WorkingHours WorkingHours
		{
			get
			{
				if (this.workingHours == null)
				{
					this.workingHours = WorkingHours.CreateForSession(this.MailboxSession, this.TimeZone);
				}
				return this.workingHours;
			}
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x0600158E RID: 5518 RVA: 0x00081349 File Offset: 0x0007F549
		public System.DayOfWeek WeekStartDay
		{
			get
			{
				return this.UserOptions.WeekStartDay;
			}
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x0600158F RID: 5519 RVA: 0x00081356 File Offset: 0x0007F556
		public string DateFormat
		{
			get
			{
				return this.UserOptions.DateFormat;
			}
		}

		// Token: 0x06001590 RID: 5520 RVA: 0x00081363 File Offset: 0x0007F563
		public string GetWeekdayDateFormat(bool useFullWeekdayFormat)
		{
			return this.UserOptions.GetWeekdayDateFormat(useFullWeekdayFormat);
		}

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x06001591 RID: 5521 RVA: 0x00081371 File Offset: 0x0007F571
		internal OwaDelegateSessionManager DelegateSessionManager
		{
			get
			{
				if (this.delegateSessionManager == null)
				{
					this.delegateSessionManager = new OwaDelegateSessionManager(this);
				}
				return this.delegateSessionManager;
			}
		}

		// Token: 0x06001592 RID: 5522 RVA: 0x00081390 File Offset: 0x0007F590
		internal WorkingHours GetOthersWorkingHours(OwaStoreObjectId folderId)
		{
			if (!folderId.IsOtherMailbox)
			{
				throw new ArgumentException("folderId should belong to other's mailbox");
			}
			if (this.othersWorkingHours == null)
			{
				this.othersWorkingHours = new Dictionary<string, WorkingHours>();
			}
			string key = folderId.MailboxOwnerLegacyDN;
			WorkingHours workingHours;
			if (!this.othersWorkingHours.TryGetValue(key, out workingHours))
			{
				MailboxSession mailboxSession = (MailboxSession)folderId.GetSession(this);
				workingHours = WorkingHours.CreateForSession(mailboxSession, mailboxSession.ExTimeZone);
				this.othersWorkingHours.Add(key, workingHours);
			}
			return workingHours;
		}

		// Token: 0x06001593 RID: 5523 RVA: 0x00081404 File Offset: 0x0007F604
		internal void AddSessionHandle(OwaStoreObjectIdSessionHandle owaStoreObjectIdSessionHandle)
		{
			if (owaStoreObjectIdSessionHandle == null)
			{
				return;
			}
			if (owaStoreObjectIdSessionHandle.HandleType != OwaStoreObjectIdType.OtherUserMailboxObject && owaStoreObjectIdSessionHandle.HandleType != OwaStoreObjectIdType.GSCalendar)
			{
				owaStoreObjectIdSessionHandle.Dispose();
				owaStoreObjectIdSessionHandle = null;
				return;
			}
			if (this.sessionHandles == null)
			{
				this.sessionHandles = new List<OwaStoreObjectIdSessionHandle>();
			}
			this.sessionHandles.Add(owaStoreObjectIdSessionHandle);
		}

		// Token: 0x06001594 RID: 5524 RVA: 0x00081450 File Offset: 0x0007F650
		internal void ClearAllSessionHandles()
		{
			if (this.sessionHandles != null)
			{
				foreach (OwaStoreObjectIdSessionHandle owaStoreObjectIdSessionHandle in this.sessionHandles)
				{
					if (owaStoreObjectIdSessionHandle != null)
					{
						owaStoreObjectIdSessionHandle.Dispose();
					}
				}
				this.sessionHandles.Clear();
			}
		}

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x06001595 RID: 5525 RVA: 0x000814B8 File Offset: 0x0007F6B8
		public Experience[] Experiences
		{
			get
			{
				this.ThrowIfProxy();
				return this.experiences;
			}
		}

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x06001596 RID: 5526 RVA: 0x000814C6 File Offset: 0x0007F6C6
		internal CalendarSettings CalendarSettings
		{
			get
			{
				if (this.calendarSettings == null)
				{
					this.calendarSettings = CalendarSettings.CreateForSession(this);
				}
				return this.calendarSettings;
			}
		}

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x06001597 RID: 5527 RVA: 0x000814E2 File Offset: 0x0007F6E2
		public bool IsBasicExperience
		{
			get
			{
				return string.Compare("Basic", this.Experiences[0].Name, StringComparison.OrdinalIgnoreCase) == 0;
			}
		}

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x06001598 RID: 5528 RVA: 0x000814FF File Offset: 0x0007F6FF
		public bool IsRtl
		{
			get
			{
				return Culture.IsRtl;
			}
		}

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x06001599 RID: 5529 RVA: 0x00081508 File Offset: 0x0007F708
		public bool IsPhoneticNamesEnabled
		{
			get
			{
				PolicyConfiguration policyConfiguration;
				if (this.TryGetPolicyConfigurationFromCache(out policyConfiguration))
				{
					return policyConfiguration.PhoneticSupportEnabled;
				}
				return this.configuration.PhoneticSupportEnabled;
			}
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x0600159A RID: 5530 RVA: 0x00081531 File Offset: 0x0007F731
		public GlobalAddressListInfo GlobalAddressListInfo
		{
			get
			{
				this.globalAddressListInfo = DirectoryAssistance.GetGlobalAddressListInfo(this);
				return this.globalAddressListInfo;
			}
		}

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x0600159B RID: 5531 RVA: 0x00081545 File Offset: 0x0007F745
		public AddressListInfo AllRoomsAddressBookInfo
		{
			get
			{
				if (this.allRoomsAddressBookInfo == null)
				{
					this.allRoomsAddressBookInfo = DirectoryAssistance.GetAllRoomsAddressBookInfo(this);
				}
				if (this.allRoomsAddressBookInfo.IsEmpty)
				{
					return null;
				}
				return this.allRoomsAddressBookInfo;
			}
		}

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x0600159C RID: 5532 RVA: 0x00081570 File Offset: 0x0007F770
		public BrowserType BrowserType
		{
			get
			{
				return this.browserType;
			}
		}

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x0600159D RID: 5533 RVA: 0x00081578 File Offset: 0x0007F778
		public BrowserPlatform BrowserPlatform
		{
			get
			{
				return this.browserPlatform;
			}
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x0600159E RID: 5534 RVA: 0x00081580 File Offset: 0x0007F780
		// (set) Token: 0x0600159F RID: 5535 RVA: 0x00081588 File Offset: 0x0007F788
		internal AddressListInfo LastUsedAddressBookInfo
		{
			get
			{
				return this.lastUsedAddressBookInfo;
			}
			set
			{
				this.lastUsedAddressBookInfo = value;
			}
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x060015A0 RID: 5536 RVA: 0x00081594 File Offset: 0x0007F794
		public AddressBookBase GlobalAddressList
		{
			get
			{
				if (!this.isGlobalAddressListLoaded)
				{
					IConfigurationSession configurationSession = Utilities.CreateADSystemConfigurationSession(true, ConsistencyMode.IgnoreInvalid, this);
					IRecipientSession recipientSession = Utilities.CreateADRecipientSession(CultureInfo.CurrentCulture.LCID, true, ConsistencyMode.IgnoreInvalid, false, this, false);
					this.globalAddressList = AddressBookBase.GetGlobalAddressList(this.LogonIdentity.ClientSecurityContext, configurationSession, recipientSession, this.GlobalAddressListId);
					this.isGlobalAddressListLoaded = true;
				}
				return this.globalAddressList;
			}
		}

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x060015A1 RID: 5537 RVA: 0x000815F4 File Offset: 0x0007F7F4
		public ADObjectId GlobalAddressListId
		{
			get
			{
				if (this.globalAddressListId == null)
				{
					IConfigurationSession configurationSession = Utilities.CreateADSystemConfigurationSession(true, ConsistencyMode.IgnoreInvalid, this);
					this.globalAddressListId = DirectoryHelper.GetGlobalAddressListFromAddressBookPolicy(this.ExchangePrincipal.MailboxInfo.Configuration.AddressBookPolicy, configurationSession);
				}
				return this.globalAddressListId;
			}
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x060015A2 RID: 5538 RVA: 0x0008163C File Offset: 0x0007F83C
		public IEnumerable<AddressBookBase> AllAddressLists
		{
			get
			{
				IConfigurationSession configurationSession = Utilities.CreateADSystemConfigurationSession(true, ConsistencyMode.IgnoreInvalid, this);
				return AddressBookBase.GetAllAddressLists(this.LogonIdentity.ClientSecurityContext, configurationSession, this.exchangePrincipal.MailboxInfo.Configuration.AddressBookPolicy);
			}
		}

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x060015A3 RID: 5539 RVA: 0x00081678 File Offset: 0x0007F878
		public AddressBookBase AllRoomsAddressList
		{
			get
			{
				IConfigurationSession configurationSession = Utilities.CreateADSystemConfigurationSession(true, ConsistencyMode.IgnoreInvalid, this);
				return AddressBookBase.GetAllRoomsAddressList(this.LogonIdentity.ClientSecurityContext, configurationSession, this.exchangePrincipal.MailboxInfo.Configuration.AddressBookPolicy);
			}
		}

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x060015A4 RID: 5540 RVA: 0x000816B4 File Offset: 0x0007F8B4
		// (set) Token: 0x060015A5 RID: 5541 RVA: 0x000816BC File Offset: 0x0007F8BC
		internal bool IsAutoCompleteSessionStarted
		{
			get
			{
				return this.isAutoCompleteSessionStarted;
			}
			set
			{
				this.isAutoCompleteSessionStarted = value;
			}
		}

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x060015A6 RID: 5542 RVA: 0x000816C5 File Offset: 0x0007F8C5
		// (set) Token: 0x060015A7 RID: 5543 RVA: 0x000816CD File Offset: 0x0007F8CD
		internal bool IsRoomsCacheSessionStarted
		{
			get
			{
				return this.isRoomsCacheSessionStarted;
			}
			set
			{
				this.isRoomsCacheSessionStarted = value;
			}
		}

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x060015A8 RID: 5544 RVA: 0x000816D6 File Offset: 0x0007F8D6
		// (set) Token: 0x060015A9 RID: 5545 RVA: 0x000816DE File Offset: 0x0007F8DE
		internal bool IsSendFromCacheSessionStarted
		{
			get
			{
				return this.isSendFromCacheSessionStarted;
			}
			set
			{
				this.isSendFromCacheSessionStarted = value;
			}
		}

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x060015AA RID: 5546 RVA: 0x000816E7 File Offset: 0x0007F8E7
		// (set) Token: 0x060015AB RID: 5547 RVA: 0x000816EF File Offset: 0x0007F8EF
		internal bool IsMruSessionStarted
		{
			get
			{
				return this.isMruSessionStarted;
			}
			set
			{
				this.isMruSessionStarted = value;
			}
		}

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x060015AC RID: 5548 RVA: 0x000816F8 File Offset: 0x0007F8F8
		// (set) Token: 0x060015AD RID: 5549 RVA: 0x00081700 File Offset: 0x0007F900
		public bool MessageViewFirstRender
		{
			get
			{
				return this.messageViewFirstRender;
			}
			set
			{
				this.messageViewFirstRender = value;
			}
		}

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x060015AE RID: 5550 RVA: 0x0008170C File Offset: 0x0007F90C
		public bool IsPublicLogon
		{
			get
			{
				if (this.isPublicLogon == null)
				{
					if (this.IsProxy)
					{
						this.isPublicLogon = new bool?(this.IsPublicRequest(HttpContext.Current.Request));
					}
					else
					{
						this.isPublicLogon = new bool?(UserContextUtilities.IsPublicLogon(this.ExchangePrincipal.MailboxInfo.OrganizationId, HttpContext.Current));
					}
				}
				return this.isPublicLogon.Value;
			}
		}

		// Token: 0x060015AF RID: 5551 RVA: 0x0008177B File Offset: 0x0007F97B
		public void UpdateLastUserRequestTime()
		{
			this.lastUserRequestTime = Globals.ApplicationTime;
			if (this.IsInstantMessageEnabled() && this.instantMessageManager != null)
			{
				this.instantMessageManager.ResetPresence();
			}
		}

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x060015B0 RID: 5552 RVA: 0x000817A3 File Offset: 0x0007F9A3
		public long LastUserRequestTime
		{
			get
			{
				return this.lastUserRequestTime;
			}
		}

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x060015B1 RID: 5553 RVA: 0x000817AB File Offset: 0x0007F9AB
		// (set) Token: 0x060015B2 RID: 5554 RVA: 0x000817B3 File Offset: 0x0007F9B3
		public int RenderingFlags
		{
			get
			{
				return this.renderingFlags;
			}
			set
			{
				this.renderingFlags = value;
			}
		}

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x060015B3 RID: 5555 RVA: 0x000817BC File Offset: 0x0007F9BC
		// (set) Token: 0x060015B4 RID: 5556 RVA: 0x000817C4 File Offset: 0x0007F9C4
		internal OwaStoreObjectId SearchFolderId
		{
			get
			{
				return this.searchFolderId;
			}
			set
			{
				this.searchFolderId = value;
			}
		}

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x060015B5 RID: 5557 RVA: 0x000817CD File Offset: 0x0007F9CD
		// (set) Token: 0x060015B6 RID: 5558 RVA: 0x000817D5 File Offset: 0x0007F9D5
		public string LastSearchQueryFilter
		{
			get
			{
				return this.lastSearchQueryFilter;
			}
			set
			{
				this.lastSearchQueryFilter = value;
			}
		}

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x060015B7 RID: 5559 RVA: 0x000817DE File Offset: 0x0007F9DE
		// (set) Token: 0x060015B8 RID: 5560 RVA: 0x000817E6 File Offset: 0x0007F9E6
		internal SearchScope LastSearchScope
		{
			get
			{
				return this.searchScope;
			}
			set
			{
				this.searchScope = value;
			}
		}

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x060015B9 RID: 5561 RVA: 0x000817EF File Offset: 0x0007F9EF
		// (set) Token: 0x060015BA RID: 5562 RVA: 0x000817F7 File Offset: 0x0007F9F7
		internal OwaStoreObjectId LastSearchFolderId
		{
			get
			{
				return this.lastSearchFolderId;
			}
			set
			{
				this.lastSearchFolderId = value;
			}
		}

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x060015BB RID: 5563 RVA: 0x00081800 File Offset: 0x0007FA00
		// (set) Token: 0x060015BC RID: 5564 RVA: 0x00081808 File Offset: 0x0007FA08
		internal bool ForceNewSearch
		{
			get
			{
				return this.forceNewSearch;
			}
			set
			{
				this.forceNewSearch = value;
			}
		}

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x060015BD RID: 5565 RVA: 0x00081811 File Offset: 0x0007FA11
		// (set) Token: 0x060015BE RID: 5566 RVA: 0x00081819 File Offset: 0x0007FA19
		public string LastUMCallId
		{
			get
			{
				return this.lastUMCallId;
			}
			set
			{
				this.lastUMCallId = value;
			}
		}

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x060015BF RID: 5567 RVA: 0x00081822 File Offset: 0x0007FA22
		// (set) Token: 0x060015C0 RID: 5568 RVA: 0x0008182A File Offset: 0x0007FA2A
		public string LastUMPhoneNumber
		{
			get
			{
				return this.lastUMPhoneNumber;
			}
			set
			{
				this.lastUMPhoneNumber = value;
			}
		}

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x060015C1 RID: 5569 RVA: 0x00081833 File Offset: 0x0007FA33
		// (set) Token: 0x060015C2 RID: 5570 RVA: 0x00081841 File Offset: 0x0007FA41
		public Theme Theme
		{
			get
			{
				this.ThrowIfProxy();
				return this.theme;
			}
			set
			{
				this.theme = value;
			}
		}

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x060015C3 RID: 5571 RVA: 0x0008184C File Offset: 0x0007FA4C
		public Theme DefaultTheme
		{
			get
			{
				this.ThrowIfProxy();
				PolicyConfiguration policyConfiguration;
				string text;
				if (this.TryGetPolicyConfigurationFromCache(out policyConfiguration))
				{
					text = policyConfiguration.DefaultTheme;
				}
				else
				{
					text = this.configuration.DefaultTheme;
				}
				if (this.defaultTheme == null || this.defaultTheme.StorageId != text)
				{
					this.defaultTheme = ThemeManager.GetDefaultTheme(text);
				}
				return this.defaultTheme;
			}
		}

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x060015C4 RID: 5572 RVA: 0x000818AB File Offset: 0x0007FAAB
		// (set) Token: 0x060015C5 RID: 5573 RVA: 0x000818B3 File Offset: 0x0007FAB3
		public ClientViewState LastClientViewState
		{
			get
			{
				return this.lastClientViewState;
			}
			set
			{
				this.lastClientViewState = value;
			}
		}

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x060015C6 RID: 5574 RVA: 0x000818BC File Offset: 0x0007FABC
		// (set) Token: 0x060015C7 RID: 5575 RVA: 0x000818C4 File Offset: 0x0007FAC4
		public bool IsRemindersSessionStarted
		{
			get
			{
				return this.isRemindersSessionStarted;
			}
			set
			{
				this.isRemindersSessionStarted = value;
			}
		}

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x060015C8 RID: 5576 RVA: 0x000818CD File Offset: 0x0007FACD
		// (set) Token: 0x060015C9 RID: 5577 RVA: 0x000818D5 File Offset: 0x0007FAD5
		public int RemindersTimeZoneOffset
		{
			get
			{
				return this.remindersTimeZoneOffset;
			}
			set
			{
				this.remindersTimeZoneOffset = value;
			}
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x060015CA RID: 5578 RVA: 0x000818DE File Offset: 0x0007FADE
		// (set) Token: 0x060015CB RID: 5579 RVA: 0x000818E6 File Offset: 0x0007FAE6
		public ColumnId DocumentsModuleSortedColumnId
		{
			get
			{
				return this.documentsSortedColumnId;
			}
			set
			{
				this.documentsSortedColumnId = value;
			}
		}

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x060015CC RID: 5580 RVA: 0x000818EF File Offset: 0x0007FAEF
		// (set) Token: 0x060015CD RID: 5581 RVA: 0x000818F7 File Offset: 0x0007FAF7
		public SortOrder DocumentModuleSortOrder
		{
			get
			{
				return this.documentsSortOrder;
			}
			set
			{
				this.documentsSortOrder = value;
			}
		}

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x060015CE RID: 5582 RVA: 0x00081900 File Offset: 0x0007FB00
		internal ComplianceReader ComplianceReader
		{
			get
			{
				if (this.complianceReader == null)
				{
					this.complianceReader = new ComplianceReader(this.exchangePrincipal.MailboxInfo.OrganizationId);
				}
				return this.complianceReader;
			}
		}

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x060015CF RID: 5583 RVA: 0x0008192C File Offset: 0x0007FB2C
		public bool IsPublicFolderEnabled
		{
			get
			{
				Guid guid;
				return this.Configuration.IsPublicFoldersEnabledOnThisVdir && this.IsFeatureEnabled(Feature.PublicFolders) && PublicFolderSession.TryGetPrimaryHierarchyMailboxGuid(this.exchangePrincipal.MailboxInfo.OrganizationId, out guid);
			}
		}

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x060015D0 RID: 5584 RVA: 0x0008196A File Offset: 0x0007FB6A
		internal StoreSession.IItemBinder ItemBinder
		{
			get
			{
				if (this.itemBinder == null)
				{
					this.itemBinder = new OwaItemBinder(this);
				}
				return this.itemBinder;
			}
		}

		// Token: 0x060015D1 RID: 5585 RVA: 0x00081986 File Offset: 0x0007FB86
		public void RenderThemeFileUrl(TextWriter writer, ThemeFileId themeFileId)
		{
			SessionContextUtilities.RenderThemeFileUrl(writer, themeFileId, this);
		}

		// Token: 0x060015D2 RID: 5586 RVA: 0x00081990 File Offset: 0x0007FB90
		public void RenderThemeFileUrl(TextWriter writer, int themeFileIndex)
		{
			SessionContextUtilities.RenderThemeFileUrl(writer, themeFileIndex, this);
		}

		// Token: 0x060015D3 RID: 5587 RVA: 0x0008199A File Offset: 0x0007FB9A
		public void RenderThemeImage(StringBuilder builder, ThemeFileId themeFileId, string styleClass, params object[] extraAttributes)
		{
			SessionContextUtilities.RenderThemeImage(builder, themeFileId, styleClass, this, extraAttributes);
		}

		// Token: 0x060015D4 RID: 5588 RVA: 0x000819A7 File Offset: 0x0007FBA7
		public void RenderThemeImage(TextWriter writer, ThemeFileId themeFileId)
		{
			SessionContextUtilities.RenderThemeImage(writer, themeFileId, this);
		}

		// Token: 0x060015D5 RID: 5589 RVA: 0x000819B1 File Offset: 0x0007FBB1
		public void RenderThemeImage(TextWriter writer, ThemeFileId themeFileId, string styleClass, params object[] extraAttributes)
		{
			SessionContextUtilities.RenderThemeImage(writer, themeFileId, styleClass, this, extraAttributes);
		}

		// Token: 0x060015D6 RID: 5590 RVA: 0x000819BE File Offset: 0x0007FBBE
		public void RenderBaseThemeImage(TextWriter writer, ThemeFileId themeFileId)
		{
			SessionContextUtilities.RenderBaseThemeImage(writer, themeFileId, this);
		}

		// Token: 0x060015D7 RID: 5591 RVA: 0x000819C8 File Offset: 0x0007FBC8
		public void RenderBaseThemeImage(TextWriter writer, ThemeFileId themeFileId, string styleClass, params object[] extraAttributes)
		{
			SessionContextUtilities.RenderBaseThemeImage(writer, themeFileId, styleClass, this, extraAttributes);
		}

		// Token: 0x060015D8 RID: 5592 RVA: 0x000819D5 File Offset: 0x0007FBD5
		public void RenderThemeImageWithToolTip(TextWriter writer, ThemeFileId themeFileId, string styleClass, params string[] extraAttributes)
		{
			SessionContextUtilities.RenderThemeImageWithToolTip(writer, themeFileId, styleClass, this, extraAttributes);
		}

		// Token: 0x060015D9 RID: 5593 RVA: 0x000819E2 File Offset: 0x0007FBE2
		public void RenderThemeImageWithToolTip(TextWriter writer, ThemeFileId themeFileId, string styleClass, Strings.IDs tooltipStringId, params string[] extraAttributes)
		{
			SessionContextUtilities.RenderThemeImageWithToolTip(writer, themeFileId, styleClass, tooltipStringId, this, extraAttributes);
		}

		// Token: 0x060015DA RID: 5594 RVA: 0x000819F1 File Offset: 0x0007FBF1
		public void RenderThemeImageStart(TextWriter writer, ThemeFileId themeFileId, string styleClass)
		{
			SessionContextUtilities.RenderThemeImageStart(writer, themeFileId, styleClass, this);
		}

		// Token: 0x060015DB RID: 5595 RVA: 0x000819FC File Offset: 0x0007FBFC
		public void RenderThemeImageStart(TextWriter writer, ThemeFileId themeFileId, string styleClass, bool renderBaseTheme)
		{
			SessionContextUtilities.RenderThemeImageStart(writer, themeFileId, styleClass, renderBaseTheme, this);
		}

		// Token: 0x060015DC RID: 5596 RVA: 0x00081A09 File Offset: 0x0007FC09
		public void RenderThemeImageEnd(TextWriter writer, ThemeFileId themeFileId)
		{
			SessionContextUtilities.RenderThemeImageEnd(writer, themeFileId);
		}

		// Token: 0x060015DD RID: 5597 RVA: 0x00081A12 File Offset: 0x0007FC12
		public string GetThemeFileUrl(ThemeFileId themeFileId)
		{
			return SessionContextUtilities.GetThemeFileUrl(themeFileId, this);
		}

		// Token: 0x060015DE RID: 5598 RVA: 0x00081A1B File Offset: 0x0007FC1B
		public void RenderCssFontThemeFileUrl(TextWriter writer)
		{
			SessionContextUtilities.RenderCssFontThemeFileUrl(writer, this);
		}

		// Token: 0x060015DF RID: 5599 RVA: 0x00081A24 File Offset: 0x0007FC24
		public bool AreFeaturesNotRestricted(ulong features)
		{
			return this.MailboxIdentity == null || (this.RestrictedCapabilitiesFlags & features) == features;
		}

		// Token: 0x060015E0 RID: 5600 RVA: 0x00081A3B File Offset: 0x0007FC3B
		public bool IsFeatureEnabled(Feature feature)
		{
			return this.AreFeaturesEnabled((ulong)feature);
		}

		// Token: 0x060015E1 RID: 5601 RVA: 0x00081A44 File Offset: 0x0007FC44
		public bool AreFeaturesEnabled(ulong features)
		{
			return (features & this.SegmentationFlags) == features && this.AreFeaturesNotRestricted(features);
		}

		// Token: 0x060015E2 RID: 5602 RVA: 0x00081A5C File Offset: 0x0007FC5C
		public bool IsMobileSyncEnabled()
		{
			if (!this.IsFeatureEnabled(Feature.EasMobileOptions))
			{
				return false;
			}
			bool result = false;
			OWAMiniRecipient owaminiRecipient = this.MailboxIdentity.GetOWAMiniRecipient();
			if (owaminiRecipient != null)
			{
				result = owaminiRecipient.ActiveSyncEnabled;
			}
			return result;
		}

		// Token: 0x060015E3 RID: 5603 RVA: 0x00081A92 File Offset: 0x0007FC92
		public bool IsInstantMessageEnabled()
		{
			return this.PrivateIsInstantMessageEnabled(false);
		}

		// Token: 0x060015E4 RID: 5604 RVA: 0x00081A9B File Offset: 0x0007FC9B
		public bool IsSenderPhotosFeatureEnabled(Feature feature)
		{
			return this.IsFeatureEnabled(feature) && !AppSettings.GetConfiguredValue<bool>("SenderPhotosDisabled", false);
		}

		// Token: 0x060015E5 RID: 5605 RVA: 0x00081AB8 File Offset: 0x0007FCB8
		private bool PrivateIsInstantMessageEnabled(bool trace)
		{
			bool flag;
			if (this.InstantMessagingType == InstantMessagingTypeOptions.Ocs)
			{
				flag = (this.IsFeatureEnabled(Feature.InstantMessage) && InstantMessageOCSProvider.EndpointManager != null && this.sipUri != null && !this.IsExplicitLogon && !this.IsWebPartRequest);
				if (!flag && trace)
				{
					ExTraceGlobals.InstantMessagingTracer.TraceDebug(0L, "UserContext.IsInstantMessageEnabled, IM is disabled for user {0}, IsFeatureEnabled = {1}, EndpointManager = {2}, SipUri = {3}, IsExplicitLogon = {4}, IsWebPart = {5}", new object[]
					{
						(this.mailboxOwnerLegacyDN != null) ? this.mailboxOwnerLegacyDN : "null",
						this.IsFeatureEnabled(Feature.InstantMessage),
						(InstantMessageOCSProvider.EndpointManager != null) ? "<object>" : "null",
						(this.sipUri != null) ? this.sipUri : "null",
						this.IsExplicitLogon,
						this.IsWebPartRequest
					});
				}
			}
			else
			{
				if (trace)
				{
					ExTraceGlobals.InstantMessagingTracer.TraceDebug<string, InstantMessagingTypeOptions>(0L, "UserContext.IsInstantMessageEnabled, IM is disabled for user {0}, Instant Messaging type is not valid: {1}", (this.mailboxOwnerLegacyDN != null) ? this.mailboxOwnerLegacyDN : "null", this.InstantMessagingType);
				}
				flag = false;
			}
			return flag;
		}

		// Token: 0x060015E6 RID: 5606 RVA: 0x00081BD4 File Offset: 0x0007FDD4
		private bool GetIsClientSideDataCollectingEnabled()
		{
			Random random = new Random();
			double num = (double)random.Next(1, 100000) / 100000.0;
			double configuredValue = AppSettings.GetConfiguredValue<double>("ClientSideDataCollectSamplingProbability", 0.0);
			return num <= configuredValue;
		}

		// Token: 0x060015E7 RID: 5607 RVA: 0x00081C1C File Offset: 0x0007FE1C
		public void RefreshIsJunkEmailEnabled()
		{
			if (this.IsFeatureEnabled(Feature.JunkEMail) && this.CanActAsOwner)
			{
				JunkEmailRule.JunkEmailStatus junkEmailRuleStatus = this.MailboxSession.GetJunkEmailRuleStatus();
				if ((junkEmailRuleStatus & JunkEmailRule.JunkEmailStatus.IsPresent) != JunkEmailRule.JunkEmailStatus.None && (junkEmailRuleStatus & JunkEmailRule.JunkEmailStatus.IsEnabled) != JunkEmailRule.JunkEmailStatus.None)
				{
					this.isJunkEmailEnabled = true;
					return;
				}
			}
			this.isJunkEmailEnabled = false;
		}

		// Token: 0x060015E8 RID: 5608 RVA: 0x00081C64 File Offset: 0x0007FE64
		public long UpdateUsedQuota()
		{
			this.lastQuotaUpdateTime = Globals.ApplicationTime;
			this.MailboxSession.Mailbox.ForceReload(new PropertyDefinition[]
			{
				MailboxSchema.QuotaUsedExtended
			});
			long num = 0L;
			object obj = this.MailboxSession.Mailbox.TryGetProperty(MailboxSchema.QuotaUsedExtended);
			if (!(obj is PropertyError))
			{
				num = (long)obj;
			}
			if (num >= this.quotaWarning || num >= this.quotaSend)
			{
				this.isQuotaAboveWarning = true;
			}
			else
			{
				this.isQuotaAboveWarning = false;
			}
			return num;
		}

		// Token: 0x060015E9 RID: 5609 RVA: 0x00081CE8 File Offset: 0x0007FEE8
		private void GetMailboxQuotaLimits()
		{
			this.quotaSend = 0L;
			object obj = this.mailboxSession.Mailbox.TryGetProperty(MailboxSchema.QuotaProhibitSend);
			if (!(obj is PropertyError))
			{
				this.quotaSend = (long)((int)obj) * 1024L;
			}
			this.quotaWarning = 0L;
			obj = this.mailboxSession.Mailbox.TryGetProperty(MailboxSchema.StorageQuotaLimit);
			if (!(obj is PropertyError))
			{
				this.quotaWarning = (long)((int)obj) * 1024L;
			}
		}

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x060015EA RID: 5610 RVA: 0x00081D69 File Offset: 0x0007FF69
		// (set) Token: 0x060015EB RID: 5611 RVA: 0x00081D77 File Offset: 0x0007FF77
		public ExTimeZone TimeZone
		{
			get
			{
				this.ThrowIfProxy();
				return this.timeZone;
			}
			set
			{
				this.timeZone = value;
				this.MailboxSession.ExTimeZone = value;
				if (this.alternateMailboxSessionManager != null)
				{
					this.alternateMailboxSessionManager.UpdateTimeZoneOnAllSessions(value);
				}
				if (this.publicFolderSessionCache != null)
				{
					this.publicFolderSessionCache.UpdateTimeZoneOnAllSessions(value);
				}
			}
		}

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x060015EC RID: 5612 RVA: 0x00081DB4 File Offset: 0x0007FFB4
		// (set) Token: 0x060015ED RID: 5613 RVA: 0x00081DBC File Offset: 0x0007FFBC
		public OwaIdentity LogonIdentity
		{
			get
			{
				return this.logonIdentity;
			}
			internal set
			{
				this.logonIdentity = value;
			}
		}

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x060015EE RID: 5614 RVA: 0x00081DC5 File Offset: 0x0007FFC5
		// (set) Token: 0x060015EF RID: 5615 RVA: 0x00081DCD File Offset: 0x0007FFCD
		public OwaIdentity MailboxIdentity
		{
			get
			{
				return this.mailboxIdentity;
			}
			internal set
			{
				this.mailboxIdentity = value;
			}
		}

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x060015F0 RID: 5616 RVA: 0x00081DD6 File Offset: 0x0007FFD6
		public bool IsJunkEmailEnabled
		{
			get
			{
				return this.isJunkEmailEnabled;
			}
		}

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x060015F1 RID: 5617 RVA: 0x00081DDE File Offset: 0x0007FFDE
		// (set) Token: 0x060015F2 RID: 5618 RVA: 0x00081DE6 File Offset: 0x0007FFE6
		public RequestExecution RequestExecution
		{
			get
			{
				return this.requestExecution;
			}
			internal set
			{
				this.requestExecution = value;
			}
		}

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x060015F3 RID: 5619 RVA: 0x00081DEF File Offset: 0x0007FFEF
		// (set) Token: 0x060015F4 RID: 5620 RVA: 0x00081DF7 File Offset: 0x0007FFF7
		internal ProxyUriQueue ProxyUriQueue
		{
			get
			{
				return this.proxyUriQueue;
			}
			set
			{
				this.proxyUriQueue = value;
			}
		}

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x060015F5 RID: 5621 RVA: 0x00081E00 File Offset: 0x00080000
		// (set) Token: 0x060015F6 RID: 5622 RVA: 0x00081E08 File Offset: 0x00080008
		internal UserContextCookie ProxyUserContextCookie
		{
			get
			{
				return this.proxyUserContextCookie;
			}
			set
			{
				this.proxyUserContextCookie = value;
			}
		}

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x060015F7 RID: 5623 RVA: 0x00081E11 File Offset: 0x00080011
		public bool IsProxy
		{
			get
			{
				return this.isProxy;
			}
		}

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x060015F8 RID: 5624 RVA: 0x00081E1C File Offset: 0x0008001C
		public bool IsWebPartRequest
		{
			get
			{
				OwaContext owaContext = OwaContext.Current;
				if (owaContext != null)
				{
					if (owaContext.RequestType == OwaRequestType.WebPart || owaContext.IsProxyWebPart)
					{
						this.isWebPartRequest = true;
					}
					else if (owaContext.RequestType == OwaRequestType.Form14)
					{
						ApplicationElement applicationElement = RequestDispatcherUtilities.GetApplicationElement(owaContext.HttpContext.Request);
						if (applicationElement == ApplicationElement.Folder || applicationElement == ApplicationElement.StartPage)
						{
							this.isWebPartRequest = false;
						}
					}
				}
				return this.isWebPartRequest;
			}
		}

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x060015F9 RID: 5625 RVA: 0x00081E7C File Offset: 0x0008007C
		// (set) Token: 0x060015FA RID: 5626 RVA: 0x00081E84 File Offset: 0x00080084
		public bool IsExplicitLogon
		{
			get
			{
				return this.isExplicitLogon;
			}
			set
			{
				this.isExplicitLogon = value;
			}
		}

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x060015FB RID: 5627 RVA: 0x00081E8D File Offset: 0x0008008D
		public bool IsExplicitLogonOthersMailbox
		{
			get
			{
				return this.isExplicitLogon && this.MailboxIdentity != null && this.LogonIdentity != null && !this.MailboxIdentity.IsPartial && !this.LogonIdentity.IsEqualsTo(this.MailboxIdentity);
			}
		}

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x060015FC RID: 5628 RVA: 0x00081ECA File Offset: 0x000800CA
		public bool IsDifferentMailbox
		{
			get
			{
				return this.isDifferentMailbox;
			}
		}

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x060015FD RID: 5629 RVA: 0x00081ED2 File Offset: 0x000800D2
		// (set) Token: 0x060015FE RID: 5630 RVA: 0x00081EDA File Offset: 0x000800DA
		public bool CanActAsOwner
		{
			get
			{
				return this.canActAsOwner;
			}
			set
			{
				this.canActAsOwner = value;
			}
		}

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x060015FF RID: 5631 RVA: 0x00081EE3 File Offset: 0x000800E3
		public bool AllFoldersPolicyMustDisplayComment
		{
			get
			{
				return this.allFoldersPolicyMustDisplayComment;
			}
		}

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x06001600 RID: 5632 RVA: 0x00081EEB File Offset: 0x000800EB
		public string AllFoldersPolicyComment
		{
			get
			{
				return this.allFoldersPolicyComment;
			}
		}

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06001601 RID: 5633 RVA: 0x00081EF3 File Offset: 0x000800F3
		public long QuotaSend
		{
			get
			{
				return this.quotaSend;
			}
		}

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x06001602 RID: 5634 RVA: 0x00081EFB File Offset: 0x000800FB
		public long QuotaWarning
		{
			get
			{
				return this.quotaWarning;
			}
		}

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x06001603 RID: 5635 RVA: 0x00081F03 File Offset: 0x00080103
		public long LastQuotaUpdateTime
		{
			get
			{
				return this.lastQuotaUpdateTime;
			}
		}

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x06001604 RID: 5636 RVA: 0x00081F0B File Offset: 0x0008010B
		// (set) Token: 0x06001605 RID: 5637 RVA: 0x00081F13 File Offset: 0x00080113
		public long SignIntoIMTime
		{
			get
			{
				return this.signIntoIMTime;
			}
			set
			{
				this.signIntoIMTime = value;
			}
		}

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x06001606 RID: 5638 RVA: 0x00081F1C File Offset: 0x0008011C
		public bool IsQuotaAboveWarning
		{
			get
			{
				return this.isQuotaAboveWarning;
			}
		}

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x06001607 RID: 5639 RVA: 0x00081F24 File Offset: 0x00080124
		// (set) Token: 0x06001608 RID: 5640 RVA: 0x00081F2C File Offset: 0x0008012C
		public object WorkingData
		{
			get
			{
				return this.workingData;
			}
			set
			{
				this.workingData = value;
			}
		}

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x06001609 RID: 5641 RVA: 0x00081F35 File Offset: 0x00080135
		public bool IsOWAEnabled
		{
			get
			{
				return this.isOWAEnabled;
			}
		}

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x0600160A RID: 5642 RVA: 0x00081F3D File Offset: 0x0008013D
		public bool IsHiddenUser
		{
			get
			{
				return this.isHiddenUser;
			}
		}

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x0600160B RID: 5643 RVA: 0x00081F45 File Offset: 0x00080145
		// (set) Token: 0x0600160C RID: 5644 RVA: 0x00081F4D File Offset: 0x0008014D
		public CultureInfo UserCulture
		{
			get
			{
				return this.userCulture;
			}
			set
			{
				this.userCulture = value;
			}
		}

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x0600160D RID: 5645 RVA: 0x00081F56 File Offset: 0x00080156
		// (set) Token: 0x0600160E RID: 5646 RVA: 0x00081F5E File Offset: 0x0008015E
		public string PreferredDC
		{
			get
			{
				return this.preferredDC;
			}
			set
			{
				this.preferredDC = value;
			}
		}

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x0600160F RID: 5647 RVA: 0x00081F67 File Offset: 0x00080167
		// (set) Token: 0x06001610 RID: 5648 RVA: 0x00081F6F File Offset: 0x0008016F
		public string SipUri
		{
			get
			{
				return this.sipUri;
			}
			set
			{
				this.sipUri = value;
			}
		}

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x06001611 RID: 5649 RVA: 0x00081F78 File Offset: 0x00080178
		// (set) Token: 0x06001612 RID: 5650 RVA: 0x00081F80 File Offset: 0x00080180
		public string MobilePhoneNumber
		{
			get
			{
				return this.mobilePhoneNumber;
			}
			set
			{
				this.mobilePhoneNumber = value;
			}
		}

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x06001613 RID: 5651 RVA: 0x00081F89 File Offset: 0x00080189
		public bool HideMailTipsByDefault
		{
			get
			{
				return this.UserOptions.HideMailTipsByDefault;
			}
		}

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x06001614 RID: 5652 RVA: 0x00081F96 File Offset: 0x00080196
		public bool ShowWeekNumbers
		{
			get
			{
				return this.UserOptions.ShowWeekNumbers;
			}
		}

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x06001615 RID: 5653 RVA: 0x00081FA3 File Offset: 0x000801A3
		public CalendarWeekRule FirstWeekOfYear
		{
			get
			{
				return ((FirstWeekRules)this.UserOptions.FirstWeekOfYear).ToCalendarWeekRule();
			}
		}

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x06001616 RID: 5654 RVA: 0x00081FB5 File Offset: 0x000801B5
		public string TimeFormat
		{
			get
			{
				return this.UserOptions.TimeFormat;
			}
		}

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x06001617 RID: 5655 RVA: 0x00081FC2 File Offset: 0x000801C2
		public int HourIncrement
		{
			get
			{
				return this.UserOptions.HourIncrement;
			}
		}

		// Token: 0x06001618 RID: 5656 RVA: 0x00081FCF File Offset: 0x000801CF
		internal MasterCategoryList GetMasterCategoryList()
		{
			return this.GetMasterCategoryList(false);
		}

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x06001619 RID: 5657 RVA: 0x00081FD8 File Offset: 0x000801D8
		public string MailboxOwnerLegacyDN
		{
			get
			{
				return this.mailboxOwnerLegacyDN;
			}
		}

		// Token: 0x0600161A RID: 5658 RVA: 0x00081FE0 File Offset: 0x000801E0
		internal MasterCategoryList GetMasterCategoryList(bool reFetchCategories)
		{
			if (this.CanActAsOwner)
			{
				try
				{
					try
					{
						if (this.masterCategoryList == null || reFetchCategories)
						{
							this.masterCategoryList = this.MailboxSession.GetMasterCategoryList();
						}
					}
					catch (CorruptDataException)
					{
						this.MailboxSession.DeleteMasterCategoryList();
						this.masterCategoryList = this.MailboxSession.GetMasterCategoryList();
					}
					if (this.masterCategoryList.Count == 0)
					{
						this.masterCategoryList = this.MailboxSession.GetMasterCategoryList();
						this.AddDefaultCategories(this.masterCategoryList);
						this.masterCategoryList.Save();
					}
				}
				catch (QuotaExceededException ex)
				{
					ExTraceGlobals.CoreTracer.TraceDebug<string>(0L, "UserContext.GetMasterCategoryList: Failed. Exception: {0}", ex.Message);
				}
			}
			return this.masterCategoryList;
		}

		// Token: 0x0600161B RID: 5659 RVA: 0x000820A8 File Offset: 0x000802A8
		internal MasterCategoryList GetMasterCategoryList(OwaStoreObjectId folderId)
		{
			if (folderId != null && folderId.IsOtherMailbox)
			{
				return this.GetMasterCategoryList((MailboxSession)folderId.GetSession(this));
			}
			return this.GetMasterCategoryList();
		}

		// Token: 0x0600161C RID: 5660 RVA: 0x000820D0 File Offset: 0x000802D0
		internal MasterCategoryList GetMasterCategoryList(MailboxSession session)
		{
			MasterCategoryList masterCategoryList = null;
			if (this.othersCategories == null)
			{
				this.othersCategories = new Dictionary<string, MasterCategoryList>();
			}
			if (!this.othersCategories.TryGetValue(session.MailboxOwnerLegacyDN, out masterCategoryList))
			{
				try
				{
					masterCategoryList = session.GetMasterCategoryList();
				}
				catch (StoragePermanentException)
				{
					masterCategoryList = new MasterCategoryList(session);
					this.AddDefaultCategories(masterCategoryList);
				}
				catch (ArgumentNullException)
				{
					masterCategoryList = new MasterCategoryList(session);
					this.AddDefaultCategories(masterCategoryList);
				}
				this.othersCategories.Add(session.MailboxOwnerLegacyDN, masterCategoryList);
			}
			return masterCategoryList;
		}

		// Token: 0x0600161D RID: 5661 RVA: 0x00082164 File Offset: 0x00080364
		internal void AddDefaultCategories(MasterCategoryList masterCategoryList)
		{
			if (masterCategoryList == null)
			{
				throw new ArgumentNullException("masterCategoryList");
			}
			masterCategoryList.Add(Category.Create(LocalizedStrings.GetNonEncoded(-1273337485), 0, true));
			masterCategoryList.Add(Category.Create(LocalizedStrings.GetNonEncoded(-630217384), 1, true));
			masterCategoryList.Add(Category.Create(LocalizedStrings.GetNonEncoded(777220966), 3, true));
			masterCategoryList.Add(Category.Create(LocalizedStrings.GetNonEncoded(-784120797), 4, true));
			masterCategoryList.Add(Category.Create(LocalizedStrings.GetNonEncoded(-1899490322), 7, true));
			masterCategoryList.Add(Category.Create(LocalizedStrings.GetNonEncoded(-136944284), 9, true));
		}

		// Token: 0x0600161E RID: 5662 RVA: 0x0008220C File Offset: 0x0008040C
		public void DangerousBeginUnlockedAction(bool serializedUnlockedAction, out int numLocksSuccessfullyUnlocked)
		{
			numLocksSuccessfullyUnlocked = 0;
			ExTraceGlobals.UserContextCallTracer.TraceDebug<UserContext>(0L, "UserContext.DangerousBeginUnlockedAction, User context instance={0}", this);
			if (this.mailboxSession != null && this.mailboxSession.IsConnected)
			{
				this.ClearAllSessionHandles();
				this.mailboxSession.Disconnect();
			}
			int numberOfLocksHeld = base.NumberOfLocksHeld;
			for (numLocksSuccessfullyUnlocked = 0; numLocksSuccessfullyUnlocked < numberOfLocksHeld; numLocksSuccessfullyUnlocked++)
			{
				base.Unlock();
			}
			if (base.LockedByCurrentThread())
			{
				ExWatson.SendReport(new InvalidOperationException(string.Format("NumberOfLocksHeld value ({0}) was incorrect when used in DangerousBeginUnlockedAction. Did not unlock all locks.", numberOfLocksHeld)), ReportOptions.None, null);
				base.ForceReleaseLock();
			}
			if (serializedUnlockedAction && !Monitor.TryEnter(this.unlockedActionLock, Globals.UserContextLockTimeout))
			{
				throw new OwaLockTimeoutException("Attempt to acquire unlocked action lock timed out", null, this);
			}
		}

		// Token: 0x0600161F RID: 5663 RVA: 0x000822C0 File Offset: 0x000804C0
		public bool DangerousEndUnlockedAction(bool serializedUnlockedAction, int numLocksToRestore)
		{
			if (serializedUnlockedAction && Monitor.IsEntered(this.unlockedActionLock))
			{
				Monitor.Exit(this.unlockedActionLock);
			}
			ExTraceGlobals.UserContextCallTracer.TraceDebug<UserContext>(0L, "UserContext.DangerousEndUnlockedAction, User context instance={0}", this);
			for (int i = numLocksToRestore; i > 0; i--)
			{
				base.Lock();
			}
			return base.State == UserContextState.Active;
		}

		// Token: 0x06001620 RID: 5664 RVA: 0x00082317 File Offset: 0x00080517
		public void LogBreadcrumb(string message)
		{
			if (Globals.DisableBreadcrumbs)
			{
				return;
			}
			if (this.breadcrumbBuffer == null)
			{
				return;
			}
			this.breadcrumbBuffer.Add(new Breadcrumb(ExDateTime.UtcNow, (message != null) ? message : "<null>"));
		}

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x06001621 RID: 5665 RVA: 0x0008234A File Offset: 0x0008054A
		internal BreadcrumbBuffer Breadcrumbs
		{
			get
			{
				return this.breadcrumbBuffer;
			}
		}

		// Token: 0x06001622 RID: 5666 RVA: 0x00082354 File Offset: 0x00080554
		public string DumpBreadcrumbs()
		{
			if (Globals.DisableBreadcrumbs)
			{
				return string.Empty;
			}
			if (this.breadcrumbBuffer == null)
			{
				return "<Breadcrumb buffer is null>";
			}
			StringBuilder stringBuilder = new StringBuilder(this.breadcrumbBuffer.Count * 128);
			stringBuilder.Append("OWA breadcrumbs:\r\n");
			for (int i = 0; i < this.breadcrumbBuffer.Count; i++)
			{
				Breadcrumb breadcrumb = this.breadcrumbBuffer[i];
				if (breadcrumb == null)
				{
					stringBuilder.AppendLine("<Found empty breadcrumb entry>");
				}
				stringBuilder.Append(breadcrumb.ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001623 RID: 5667 RVA: 0x000823E4 File Offset: 0x000805E4
		private void GetAllFolderPolicy()
		{
			if (this.CanActAsOwner)
			{
				using (UserConfiguration folderConfiguration = UserConfigurationUtilities.GetFolderConfiguration("ELC", this, this.InboxFolderId))
				{
					if (folderConfiguration != null)
					{
						IDictionary dictionary = folderConfiguration.GetDictionary();
						if (dictionary["elcComment"] != null)
						{
							this.allFoldersPolicyComment = (string)dictionary["elcComment"];
							if (dictionary["elcFlags"] != null)
							{
								this.allFoldersPolicyMustDisplayComment = (((int)dictionary["elcFlags"] & 4) > 0);
							}
						}
					}
				}
			}
		}

		// Token: 0x06001624 RID: 5668 RVA: 0x0008247C File Offset: 0x0008067C
		public bool IsInternetExplorer7()
		{
			return this.browserType == BrowserType.IE && this.browserVersion.Build >= 7;
		}

		// Token: 0x06001625 RID: 5669 RVA: 0x0008249A File Offset: 0x0008069A
		public void RenderBlankPage(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.Write(this.GetBlankPage());
		}

		// Token: 0x06001626 RID: 5670 RVA: 0x000824B6 File Offset: 0x000806B6
		public void RenderBlankPage(string path, TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			writer.Write(this.GetBlankPage(path));
		}

		// Token: 0x06001627 RID: 5671 RVA: 0x000824E4 File Offset: 0x000806E4
		internal string GetCachedFolderName(StoreObjectId folderId, MailboxSession mailboxSession)
		{
			if (this.folderNameCache == null)
			{
				this.folderNameCache = new Dictionary<StoreObjectId, string>(1);
			}
			string text = null;
			if (this.folderNameCache.TryGetValue(folderId, out text))
			{
				return text;
			}
			string result;
			using (Folder folder = Folder.Bind(mailboxSession, folderId, new PropertyDefinition[]
			{
				FolderSchema.DisplayName
			}))
			{
				text = folder.DisplayName;
				this.folderNameCache.Add(folderId, text);
				result = text;
			}
			return result;
		}

		// Token: 0x06001628 RID: 5672 RVA: 0x00082564 File Offset: 0x00080764
		internal void ClearFolderNameCache()
		{
			if (this.folderNameCache != null)
			{
				this.folderNameCache = null;
			}
		}

		// Token: 0x06001629 RID: 5673 RVA: 0x00082575 File Offset: 0x00080775
		internal int GetCachedADCount(string containerId, string searchString)
		{
			if (this.adCountCache == null)
			{
				throw new OwaInvalidOperationException("Attempted to get the cached AD count when it does not exist");
			}
			return this.adCountCache[containerId][(!string.IsNullOrEmpty(searchString)) ? searchString : string.Empty];
		}

		// Token: 0x0600162A RID: 5674 RVA: 0x000825AC File Offset: 0x000807AC
		internal void SetCachedADCount(string containerId, string searchString, int totalCount)
		{
			if (this.adCountCache == null)
			{
				this.adCountCache = new Dictionary<string, Dictionary<string, int>>(1);
			}
			Dictionary<string, int> dictionary;
			if (!this.adCountCache.TryGetValue(containerId, out dictionary))
			{
				dictionary = new Dictionary<string, int>(1);
			}
			dictionary[(!string.IsNullOrEmpty(searchString)) ? searchString : string.Empty] = totalCount;
			this.adCountCache[containerId] = dictionary;
		}

		// Token: 0x0600162B RID: 5675 RVA: 0x00082608 File Offset: 0x00080808
		public override string ToString()
		{
			return this.GetHashCode().ToString();
		}

		// Token: 0x0600162C RID: 5676 RVA: 0x00082624 File Offset: 0x00080824
		internal long CalculateTimeout()
		{
			long num = (long)this.Configuration.SessionTimeout * 60L;
			long num2 = 4L * (long)this.Configuration.NotificationInterval;
			if (this.isMonitoringRequest)
			{
				num = 30L;
			}
			else if (num2 > 0L && num2 < num && !this.IsProxy && !this.IsWebPartRequest && this.IsFeatureEnabled(Feature.Notifications) && !this.IsBasicExperience)
			{
				num = num2;
			}
			this.timeout = num * 1000L;
			return this.timeout;
		}

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x0600162D RID: 5677 RVA: 0x000826A5 File Offset: 0x000808A5
		internal long Timeout
		{
			get
			{
				return this.timeout;
			}
		}

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x0600162E RID: 5678 RVA: 0x000826AD File Offset: 0x000808AD
		public ClientBrowserStatus ClientBrowserStatus
		{
			get
			{
				return this.clientBrowserStatus;
			}
		}

		// Token: 0x0600162F RID: 5679 RVA: 0x000826B5 File Offset: 0x000808B5
		private bool TryGetPolicyConfigurationFromCache(out PolicyConfiguration policyConfiguration)
		{
			policyConfiguration = null;
			if (this.owaMailboxPolicy == null)
			{
				return false;
			}
			policyConfiguration = OwaMailboxPolicyCache.Instance.Get(this.owaMailboxPolicy);
			return policyConfiguration != null;
		}

		// Token: 0x06001630 RID: 5680 RVA: 0x000826DD File Offset: 0x000808DD
		internal bool IsInMyMailbox(StoreObject storeObject)
		{
			return this.IsMyMailbox(storeObject.Session);
		}

		// Token: 0x06001631 RID: 5681 RVA: 0x000826EC File Offset: 0x000808EC
		internal bool IsMyMailbox(StoreSession storeSession)
		{
			MailboxSession mailboxSession = storeSession as MailboxSession;
			return mailboxSession != null && this.IsMyMailbox(mailboxSession);
		}

		// Token: 0x06001632 RID: 5682 RVA: 0x0008270C File Offset: 0x0008090C
		internal bool IsInOtherMailbox(StoreObject storeObject)
		{
			return this.IsOtherMailbox(storeObject.Session);
		}

		// Token: 0x06001633 RID: 5683 RVA: 0x0008271C File Offset: 0x0008091C
		internal bool IsOtherMailbox(StoreSession storeSession)
		{
			MailboxSession mailboxSession = storeSession as MailboxSession;
			return mailboxSession != null && !Utilities.IsArchiveMailbox(mailboxSession) && !this.IsMyMailbox(mailboxSession);
		}

		// Token: 0x06001634 RID: 5684 RVA: 0x00082747 File Offset: 0x00080947
		private bool IsMyMailbox(MailboxSession session)
		{
			return string.Equals(this.MailboxSession.MailboxOwnerLegacyDN, session.MailboxOwnerLegacyDN, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x06001635 RID: 5685 RVA: 0x00082760 File Offset: 0x00080960
		public string DirectionMark
		{
			get
			{
				return this.GetDirectionMark();
			}
		}

		// Token: 0x06001636 RID: 5686 RVA: 0x00082768 File Offset: 0x00080968
		public void RenderCssLink(TextWriter writer, HttpRequest request, bool phase1Only)
		{
			SessionContextUtilities.RenderCssLink(writer, request, this, phase1Only);
		}

		// Token: 0x06001637 RID: 5687 RVA: 0x00082773 File Offset: 0x00080973
		public void RenderCssLink(TextWriter writer, HttpRequest request)
		{
			this.RenderCssLink(writer, request, false);
		}

		// Token: 0x06001638 RID: 5688 RVA: 0x00082780 File Offset: 0x00080980
		public void RenderOptionsCssLink(TextWriter writer, HttpRequest request)
		{
			writer.Write("<link type=\"text/css\" rel=\"stylesheet\" href=\"");
			ThemeManager.RenderBaseThemeFileUrl(writer, ThemeFileId.PremiumCss);
			writer.Write("\">");
			writer.Write("<link type=\"text/css\" rel=\"stylesheet\" href=\"");
			ThemeManager.RenderCssFontThemeFileUrl(writer, this.IsBasicExperience);
			writer.Write("\">");
			writer.Write("<link type=\"text/css\" rel=\"stylesheet\" href=\"");
			ThemeManager.RenderBaseThemeFileUrl(writer, ThemeFileId.OptionsCss);
			writer.Write("\">");
			writer.Write("<link type=\"text/css\" rel=\"stylesheet\" href=\"");
			ThemeManager.RenderBaseThemeFileUrl(writer, ThemeFileId.CssSpritesCss);
			writer.Write("\">");
			writer.Write("<link type=\"text/css\" rel=\"stylesheet\" href=\"");
			ThemeManager.RenderBaseThemeFileUrl(writer, ThemeFileId.CssSpritesCss2);
			writer.Write("\">");
		}

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x06001639 RID: 5689 RVA: 0x00082823 File Offset: 0x00080A23
		public bool IsSmsEnabled
		{
			get
			{
				return this.IsFeatureEnabled(Feature.TextMessage);
			}
		}

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x0600163A RID: 5690 RVA: 0x00082834 File Offset: 0x00080A34
		internal bool IsIrmEnabled
		{
			get
			{
				if (!this.IsFeatureEnabled((Feature)(-2147483648)))
				{
					return false;
				}
				bool result;
				try
				{
					result = RmsClientManager.IRMConfig.IsClientAccessServerEnabledForTenant(this.exchangePrincipal.MailboxInfo.OrganizationId);
				}
				catch (ExchangeConfigurationException innerException)
				{
					throw new RightsManagementTransientException(ServerStrings.RmExceptionGenericMessage, innerException);
				}
				catch (RightsManagementException ex)
				{
					if (ex.IsPermanent)
					{
						throw new RightsManagementPermanentException(ServerStrings.RmExceptionGenericMessage, ex);
					}
					throw new RightsManagementTransientException(ServerStrings.RmExceptionGenericMessage, ex);
				}
				return result;
			}
		}

		// Token: 0x0600163B RID: 5691 RVA: 0x000828BC File Offset: 0x00080ABC
		internal void DisconnectMailboxSession()
		{
			if (this.mailboxSession != null)
			{
				Utilities.DisconnectStoreSession(this.mailboxSession);
			}
		}

		// Token: 0x0600163C RID: 5692 RVA: 0x000828D1 File Offset: 0x00080AD1
		protected override void OnBeforeUnlock()
		{
			if (this.TerminationStatus == UserContextTerminationStatus.TerminatePending)
			{
				UserContextManager.TerminateSession(this, base.AbandonedReason);
			}
		}

		// Token: 0x0600163D RID: 5693 RVA: 0x000828E8 File Offset: 0x00080AE8
		internal void AsyncAcquireIrmLicenses(OwaStoreObjectId messageId, string publishLicense, string requestCorrelator)
		{
			if (messageId == null)
			{
				throw new ArgumentNullException("messageId");
			}
			if (messageId.StoreObjectId == null)
			{
				throw new ArgumentNullException("messageId.StoreObjectId");
			}
			if (string.IsNullOrEmpty(publishLicense))
			{
				throw new ArgumentNullException("publishLicense");
			}
			if (!this.IsIrmEnabled || this.IsBasicExperience)
			{
				return;
			}
			if (this.MailboxSession.LogonType != LogonType.Owner)
			{
				return;
			}
			if (messageId.StoreObjectId.IsFakeId)
			{
				ExTraceGlobals.UserContextCallTracer.TraceError<UserContext, OwaStoreObjectId>(0L, "UserContext.AsyncAcquireIrmLicenses: Ignoring embedded item. User context instance={0}; MessageId={1}", this, messageId);
				return;
			}
			this.irmLicensingManager.AsyncAcquireLicenses(this.MailboxSession.MailboxOwner.MailboxInfo.OrganizationId, messageId, publishLicense, this.MailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString(), this.MailboxSession.MailboxOwner.Sid, this.MailboxSession.MailboxOwner.RecipientTypeDetails, requestCorrelator);
		}

		// Token: 0x0600163E RID: 5694 RVA: 0x000829D0 File Offset: 0x00080BD0
		private void ClearSearchFolders()
		{
			if (this.SearchFolderId != null)
			{
				Utilities.Delete(this, DeleteItemFlags.HardDelete, new OwaStoreObjectId[]
				{
					this.SearchFolderId
				});
			}
			FolderSearch.ClearSearchFolders(this.MailboxSession);
			if (this.alternateMailboxSessionManager != null)
			{
				this.alternateMailboxSessionManager.ClearAllSearchFolders();
			}
		}

		// Token: 0x0600163F RID: 5695 RVA: 0x00082A1C File Offset: 0x00080C1C
		protected override void InternalDispose(bool isDisposing)
		{
			ExTraceGlobals.UserContextCallTracer.TraceDebug<bool, UserContext>(0L, "UserContext.Dispose. IsDisposing: {0}, User context instance={1}", isDisposing, this);
			if (isDisposing && !this.isDisposed)
			{
				this.ThrowIfNotHoldingLock();
				if (this.mailboxSession != null)
				{
					try
					{
						this.mailboxSession.ItemBinder = null;
						if (this.publicFolderViewStatesCache != null)
						{
							try
							{
								this.publicFolderViewStatesCache.Commit();
							}
							catch (StoragePermanentException ex)
							{
								ExTraceGlobals.UserContextTracer.TraceDebug<string>((long)this.GetHashCode(), "Unable to save the public folder view states due to the exception {0}", ex.Message);
							}
							catch (StorageTransientException ex2)
							{
								ExTraceGlobals.UserContextTracer.TraceDebug<string>((long)this.GetHashCode(), "Unable to save the public folder view states due to the transient exception {0}", ex2.Message);
							}
						}
						if (this.isAutoCompleteSessionStarted)
						{
							try
							{
								AutoCompleteCache.FinishSession(this);
								RoomsCache.FinishSession(this);
								SendFromCache.FinishSession(this);
							}
							catch (StoragePermanentException ex3)
							{
								ExTraceGlobals.UserContextTracer.TraceDebug<string>(0L, "Unable to finish autocomplete cache session due to exception {0}", ex3.Message);
							}
							catch (StorageTransientException ex4)
							{
								ExTraceGlobals.UserContextTracer.TraceDebug<string>(0L, "Unable to finish autocomplete cache session due to exception {0}", ex4.Message);
							}
						}
						if (this.isMruSessionStarted)
						{
							try
							{
								FolderMruCache.FinishCacheSession(this);
							}
							catch (StoragePermanentException ex5)
							{
								ExTraceGlobals.UserContextTracer.TraceDebug<string>(0L, "Unable to finish mru cache session due to exception {0}", ex5.Message);
							}
							catch (StorageTransientException ex6)
							{
								ExTraceGlobals.UserContextTracer.TraceDebug<string>(0L, "Unable to finish mru cache session due to exception {0}", ex6.Message);
							}
						}
						try
						{
							this.ClearSearchFolders();
						}
						catch (StoragePermanentException ex7)
						{
							ExTraceGlobals.UserContextTracer.TraceDebug<string>(0L, "Unable to remove search folder due to exception {0}", ex7.Message);
						}
						catch (StorageTransientException ex8)
						{
							ExTraceGlobals.UserContextTracer.TraceDebug<string>(0L, "Unable to remove search folder due to exception {0}", ex8.Message);
						}
						catch (ResourceUnhealthyException ex9)
						{
							ExTraceGlobals.UserContextTracer.TraceDebug<string>(0L, "Unable to remove search folder due to exception {0}", ex9.Message);
						}
						try
						{
							if (this.canActAsOwner)
							{
								JunkEmailRule junkEmailRule = this.MailboxSession.JunkEmailRule;
								if (junkEmailRule.IsContactsFolderTrusted && junkEmailRule.IsContactsCacheOutOfDate)
								{
									Utilities.JunkEmailRuleSynchronizeContactsCache(junkEmailRule);
									junkEmailRule.Save();
								}
							}
						}
						catch (StoragePermanentException ex10)
						{
							ExTraceGlobals.UserContextTracer.TraceDebug<string>(0L, "Unable to synchronize contacts cache due to exception {0}", ex10.Message);
						}
						catch (StorageTransientException ex11)
						{
							ExTraceGlobals.UserContextTracer.TraceDebug<string>(0L, "Unable to synchronize contacts cache due to exception {0}", ex11.Message);
						}
						catch (ADTransientException ex12)
						{
							ExTraceGlobals.UserContextTracer.TraceDebug<string>(0L, "Unable to synchronize contacts cache due to exception {0}", ex12.Message);
						}
						try
						{
							if (this.canActAsOwner)
							{
								this.MailboxSession.SaveMasterCategoryList();
							}
						}
						catch (StoragePermanentException ex13)
						{
							ExTraceGlobals.UserContextTracer.TraceDebug<string>(0L, "Unable to save master category list due to exception {0}", ex13.Message);
						}
						catch (StorageTransientException ex14)
						{
							ExTraceGlobals.UserContextTracer.TraceDebug<string>(0L, "Unable to save master category list due to exception {0}", ex14.Message);
						}
					}
					finally
					{
						try
						{
							if (this.mapiNotificationManager != null)
							{
								this.mapiNotificationManager.Dispose();
								this.mapiNotificationManager = null;
							}
							this.ClearAllSessionHandles();
							this.DisconnectMailboxSession();
						}
						catch (StoragePermanentException ex15)
						{
							ExTraceGlobals.UserContextTracer.TraceDebug<string>(0L, "Unable to disconnect mailbox session.  exception {0}", ex15.Message);
						}
						catch (StorageTransientException ex16)
						{
							ExTraceGlobals.UserContextTracer.TraceDebug<string>(0L, "Unable to disconnect mailbox session.  exception {0}", ex16.Message);
						}
						finally
						{
							this.mailboxSession.Dispose();
							this.mailboxSession = null;
							this.isDisposed = true;
						}
					}
				}
				if (this.alternateMailboxSessionManager != null)
				{
					this.alternateMailboxSessionManager.Dispose();
					this.alternateMailboxSessionManager = null;
				}
				if (this.publicFolderSessionCache != null)
				{
					this.publicFolderSessionCache.Dispose();
					this.publicFolderSessionCache = null;
				}
				if (this.notificationManager != null)
				{
					this.notificationManager.Dispose();
					this.notificationManager = null;
				}
				if (this.instantMessageManager != null)
				{
					this.instantMessageManager.Dispose();
					this.instantMessageManager = null;
				}
				if (this.pendingRequestManager != null)
				{
					this.pendingRequestManager.Dispose();
					this.pendingRequestManager = null;
				}
				if (this.logonIdentity != null)
				{
					this.logonIdentity.Dispose();
					this.logonIdentity = null;
				}
				if (this.mailboxIdentity != null)
				{
					this.mailboxIdentity.Dispose();
					this.mailboxIdentity = null;
				}
				if (base.Key != null && base.Key.UserContextId != null)
				{
					TranscodingTaskManager.RemoveSession(base.Key.UserContextId);
				}
				if (this.workingData != null && this.workingData is IDisposable)
				{
					(this.workingData as IDisposable).Dispose();
					this.workingData = null;
				}
				if (this.delegateSessionManager != null)
				{
					this.delegateSessionManager.ClearAllExchangePrincipals();
				}
				if (this.othersWorkingHours != null)
				{
					this.othersWorkingHours.Clear();
					this.othersWorkingHours = null;
				}
				if (this.othersCategories != null)
				{
					this.othersCategories.Clear();
					this.othersCategories = null;
				}
			}
		}

		// Token: 0x06001640 RID: 5696 RVA: 0x0008300C File Offset: 0x0008120C
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<UserContext>(this);
		}

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x06001641 RID: 5697 RVA: 0x00083014 File Offset: 0x00081214
		public bool IsReplyByPhoneEnabled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001642 RID: 5698 RVA: 0x00083018 File Offset: 0x00081218
		public void RenderCustomizedFormRegistry(TextWriter output)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			if (this.customizedFormRegistryCache == null)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (Experience experience in this.Experiences)
				{
					foreach (FormKey formKey in experience.FormsRegistry.CustomizedFormKeys)
					{
						StringBuilder stringBuilder2 = new StringBuilder();
						stringBuilder.Append("{");
						if (formKey.Application != ApplicationElement.NotSet)
						{
							stringBuilder2.Append(string.Format("\"ae\":\"{0}\",", Utilities.JavascriptEncode(formKey.Application.ToString())));
						}
						if (!string.IsNullOrEmpty(formKey.Class))
						{
							stringBuilder2.Append(string.Format("\"t\":\"{0}\",", Utilities.JavascriptEncode(formKey.Class)));
						}
						if (!string.IsNullOrEmpty(formKey.Action))
						{
							stringBuilder2.Append(string.Format("\"a\":\"{0}\",", Utilities.JavascriptEncode(formKey.Action)));
						}
						if (!string.IsNullOrEmpty(formKey.State))
						{
							stringBuilder2.Append(string.Format("\"s\":\"{0}\",", Utilities.JavascriptEncode(formKey.State)));
						}
						if (stringBuilder2.Length > 0)
						{
							stringBuilder2.Remove(stringBuilder2.Length - 1, 1);
						}
						stringBuilder.Append(stringBuilder2.ToString());
						stringBuilder.Append("} ,");
					}
				}
				this.customizedFormRegistryCache = SanitizedHtmlString.GetSanitizedStringWithoutEncoding(stringBuilder.ToString());
			}
			if (this.customizedFormRegistryCache != SanitizedHtmlString.Empty)
			{
				output.WriteLine("var a_rgCustomizedFormRegistry = [");
				output.Write(this.customizedFormRegistryCache);
				output.WriteLine("];");
			}
		}

		// Token: 0x040010DD RID: 4317
		private const string AllFolderPolicyConfigurationMessageName = "ELC";

		// Token: 0x040010DE RID: 4318
		private const ulong DisableUncAndWssFeatures = 18446744073705619455UL;

		// Token: 0x040010DF RID: 4319
		private Experience[] experiences;

		// Token: 0x040010E0 RID: 4320
		private ExchangePrincipal exchangePrincipal;

		// Token: 0x040010E1 RID: 4321
		private MailboxSession mailboxSession;

		// Token: 0x040010E2 RID: 4322
		private OwaIdentity logonIdentity;

		// Token: 0x040010E3 RID: 4323
		private OwaIdentity mailboxIdentity;

		// Token: 0x040010E4 RID: 4324
		private string mailboxOwnerLegacyDN;

		// Token: 0x040010E5 RID: 4325
		private RequestExecution requestExecution;

		// Token: 0x040010E6 RID: 4326
		private UserContextCookie proxyUserContextCookie;

		// Token: 0x040010E7 RID: 4327
		private ProxyUriQueue proxyUriQueue;

		// Token: 0x040010E8 RID: 4328
		private UserOptions userOptions;

		// Token: 0x040010E9 RID: 4329
		private WorkingHours workingHours;

		// Token: 0x040010EA RID: 4330
		private ExTimeZone timeZone;

		// Token: 0x040010EB RID: 4331
		private Theme theme;

		// Token: 0x040010EC RID: 4332
		private Theme defaultTheme;

		// Token: 0x040010ED RID: 4333
		private bool isAutoCompleteSessionStarted;

		// Token: 0x040010EE RID: 4334
		private bool isRoomsCacheSessionStarted;

		// Token: 0x040010EF RID: 4335
		private bool isSendFromCacheSessionStarted;

		// Token: 0x040010F0 RID: 4336
		private bool isMruSessionStarted;

		// Token: 0x040010F1 RID: 4337
		private bool messageViewFirstRender = true;

		// Token: 0x040010F2 RID: 4338
		private OwaStoreObjectId searchFolderId;

		// Token: 0x040010F3 RID: 4339
		private string lastSearchQueryFilter;

		// Token: 0x040010F4 RID: 4340
		private SearchScope searchScope = SearchScope.AllFoldersAndItems;

		// Token: 0x040010F5 RID: 4341
		private OwaStoreObjectId lastSearchFolderId;

		// Token: 0x040010F6 RID: 4342
		private bool forceNewSearch;

		// Token: 0x040010F7 RID: 4343
		private bool isDisposed;

		// Token: 0x040010F8 RID: 4344
		private OwaNotificationManager notificationManager;

		// Token: 0x040010F9 RID: 4345
		private OwaMapiNotificationManager mapiNotificationManager;

		// Token: 0x040010FA RID: 4346
		private InstantMessageManager instantMessageManager;

		// Token: 0x040010FB RID: 4347
		private string lastUMCallId;

		// Token: 0x040010FC RID: 4348
		private string lastUMPhoneNumber;

		// Token: 0x040010FD RID: 4349
		private BrowserType browserType = BrowserType.Other;

		// Token: 0x040010FE RID: 4350
		private BrowserPlatform browserPlatform = BrowserPlatform.Other;

		// Token: 0x040010FF RID: 4351
		private UserAgentParser.UserAgentVersion browserVersion;

		// Token: 0x04001100 RID: 4352
		private bool isMonitoringRequest;

		// Token: 0x04001101 RID: 4353
		private bool isProxy;

		// Token: 0x04001102 RID: 4354
		private bool isLoaded;

		// Token: 0x04001103 RID: 4355
		private bool isFullyIntialized;

		// Token: 0x04001104 RID: 4356
		private CultureInfo userCulture;

		// Token: 0x04001105 RID: 4357
		private string preferredDC = string.Empty;

		// Token: 0x04001106 RID: 4358
		private PublicFolderSessionCache publicFolderSessionCache;

		// Token: 0x04001107 RID: 4359
		private PublicFolderViewStatesCache publicFolderViewStatesCache;

		// Token: 0x04001108 RID: 4360
		private StoreSession.IItemBinder itemBinder;

		// Token: 0x04001109 RID: 4361
		private ADObjectId owaMailboxPolicy;

		// Token: 0x0400110A RID: 4362
		private AlternateMailboxSessionManager alternateMailboxSessionManager;

		// Token: 0x0400110B RID: 4363
		private IrmLicensingManager irmLicensingManager;

		// Token: 0x0400110C RID: 4364
		private long displayPictureChangeTime = DateTime.UtcNow.ToBinary();

		// Token: 0x0400110D RID: 4365
		private byte[] uploadedDisplayPicture;

		// Token: 0x0400110E RID: 4366
		private bool? hasPicture = null;

		// Token: 0x0400110F RID: 4367
		private long timeout;

		// Token: 0x04001110 RID: 4368
		private bool isOWAEnabled = true;

		// Token: 0x04001111 RID: 4369
		private bool isHiddenUser;

		// Token: 0x04001112 RID: 4370
		private object unlockedActionLock = new object();

		// Token: 0x04001113 RID: 4371
		private long lastUserRequestTime = Globals.ApplicationTime;

		// Token: 0x04001114 RID: 4372
		private StoreObjectId remindersSearchFolderId;

		// Token: 0x04001115 RID: 4373
		private StoreObjectId inboxFolderId;

		// Token: 0x04001116 RID: 4374
		private StoreObjectId draftsFolderId;

		// Token: 0x04001117 RID: 4375
		private AddressBookBase globalAddressList;

		// Token: 0x04001118 RID: 4376
		private ADObjectId globalAddressListId;

		// Token: 0x04001119 RID: 4377
		private bool isGlobalAddressListLoaded;

		// Token: 0x0400111A RID: 4378
		private GlobalAddressListInfo globalAddressListInfo;

		// Token: 0x0400111B RID: 4379
		private AddressListInfo allRoomsAddressBookInfo;

		// Token: 0x0400111C RID: 4380
		private AddressListInfo lastUsedAddressBookInfo;

		// Token: 0x0400111D RID: 4381
		private int renderingFlags;

		// Token: 0x0400111E RID: 4382
		private ClientViewState lastClientViewState;

		// Token: 0x0400111F RID: 4383
		private bool isRemindersSessionStarted;

		// Token: 0x04001120 RID: 4384
		private int remindersTimeZoneOffset;

		// Token: 0x04001121 RID: 4385
		private SortOrder documentsSortOrder;

		// Token: 0x04001122 RID: 4386
		private ColumnId documentsSortedColumnId = ColumnId.Count;

		// Token: 0x04001123 RID: 4387
		private bool isJunkEmailEnabled;

		// Token: 0x04001124 RID: 4388
		private bool isWebPartRequest;

		// Token: 0x04001125 RID: 4389
		private bool isExplicitLogon;

		// Token: 0x04001126 RID: 4390
		private bool isDifferentMailbox;

		// Token: 0x04001127 RID: 4391
		private bool canActAsOwner = true;

		// Token: 0x04001128 RID: 4392
		private long lastQuotaUpdateTime = Globals.ApplicationTime;

		// Token: 0x04001129 RID: 4393
		private long signIntoIMTime = Globals.ApplicationTime;

		// Token: 0x0400112A RID: 4394
		private bool allFoldersPolicyMustDisplayComment;

		// Token: 0x0400112B RID: 4395
		private string allFoldersPolicyComment = string.Empty;

		// Token: 0x0400112C RID: 4396
		private long quotaSend;

		// Token: 0x0400112D RID: 4397
		private long quotaWarning;

		// Token: 0x0400112E RID: 4398
		private bool isQuotaAboveWarning;

		// Token: 0x0400112F RID: 4399
		private object workingData;

		// Token: 0x04001130 RID: 4400
		private Configuration configuration = OwaConfigurationManager.Configuration;

		// Token: 0x04001131 RID: 4401
		private BreadcrumbBuffer breadcrumbBuffer;

		// Token: 0x04001132 RID: 4402
		private CalendarSettings calendarSettings;

		// Token: 0x04001133 RID: 4403
		private ClientBrowserStatus clientBrowserStatus;

		// Token: 0x04001134 RID: 4404
		private OwaDelegateSessionManager delegateSessionManager;

		// Token: 0x04001135 RID: 4405
		private List<OwaStoreObjectIdSessionHandle> sessionHandles;

		// Token: 0x04001136 RID: 4406
		private Dictionary<string, WorkingHours> othersWorkingHours;

		// Token: 0x04001137 RID: 4407
		private Dictionary<string, MasterCategoryList> othersCategories;

		// Token: 0x04001138 RID: 4408
		private MasterCategoryList masterCategoryList;

		// Token: 0x04001139 RID: 4409
		private bool shouldDisableUncAndWssFeatures;

		// Token: 0x0400113A RID: 4410
		private bool shouldDisableTextMessageFeatures = true;

		// Token: 0x0400113B RID: 4411
		private string sipUri;

		// Token: 0x0400113C RID: 4412
		private string mobilePhoneNumber;

		// Token: 0x0400113D RID: 4413
		private PendingRequestManager pendingRequestManager;

		// Token: 0x0400113E RID: 4414
		private PerformanceNotifier performanceNotifier;

		// Token: 0x0400113F RID: 4415
		private MailTipsNotificationHandler mailTipsNotificationHandler;

		// Token: 0x04001140 RID: 4416
		private bool isPerformanceConsoleOn;

		// Token: 0x04001141 RID: 4417
		private bool isClientSideDataCollectingEnabled;

		// Token: 0x04001142 RID: 4418
		private Dictionary<StoreObjectId, string> folderNameCache;

		// Token: 0x04001143 RID: 4419
		private Dictionary<string, Dictionary<string, int>> adCountCache;

		// Token: 0x04001144 RID: 4420
		private AutoCompleteCache autoCompleteCache;

		// Token: 0x04001145 RID: 4421
		private RoomsCache roomsCache;

		// Token: 0x04001146 RID: 4422
		private SendFromCache sendFromCache;

		// Token: 0x04001147 RID: 4423
		private SubscriptionCache subscriptionCache;

		// Token: 0x04001148 RID: 4424
		private bool isPushNotificationsEnabled = Globals.IsPushNotificationsEnabled;

		// Token: 0x04001149 RID: 4425
		private bool isPullNotificationsEnabled = Globals.IsPullNotificationsEnabled;

		// Token: 0x0400114A RID: 4426
		private bool shouldDisableEmbeddedReadingPane;

		// Token: 0x0400114B RID: 4427
		private uint[] segmentationBitsForJavascript;

		// Token: 0x0400114C RID: 4428
		private ulong restrictedCapabilitiesFlags;

		// Token: 0x0400114D RID: 4429
		private ComplianceReader complianceReader;

		// Token: 0x0400114E RID: 4430
		private bool archiveAccessed;

		// Token: 0x0400114F RID: 4431
		private OwaStoreObjectId archiveRootFolderId;

		// Token: 0x04001150 RID: 4432
		private bool? isPublicLogon;

		// Token: 0x04001151 RID: 4433
		private SanitizedHtmlString customizedFormRegistryCache;

		// Token: 0x0200027A RID: 634
		// (Invoke) Token: 0x06001644 RID: 5700
		internal delegate void DelegateWithMailboxSession(MailboxSession mailboxSession);
	}
}
