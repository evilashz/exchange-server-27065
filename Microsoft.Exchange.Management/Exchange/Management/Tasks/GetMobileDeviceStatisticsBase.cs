using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Management.Automation;
using Microsoft.Exchange.AirSync;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Management.AirSync;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000071 RID: 113
	public class GetMobileDeviceStatisticsBase<TIdentity, TDataObject> : SystemConfigurationObjectActionTask<TIdentity, TDataObject> where TIdentity : MobileDeviceIdParameter, new() where TDataObject : MobileDevice, new()
	{
		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000380 RID: 896 RVA: 0x0000E900 File Offset: 0x0000CB00
		// (set) Token: 0x06000381 RID: 897 RVA: 0x0000E917 File Offset: 0x0000CB17
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = true, ParameterSetName = "Mailbox", ValueFromPipeline = true)]
		public MailboxIdParameter Mailbox
		{
			get
			{
				return (MailboxIdParameter)base.Fields["Mailbox"];
			}
			set
			{
				base.Fields["Mailbox"] = value;
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000382 RID: 898 RVA: 0x0000E92A File Offset: 0x0000CB2A
		// (set) Token: 0x06000383 RID: 899 RVA: 0x0000E950 File Offset: 0x0000CB50
		[Parameter(Mandatory = false)]
		public SwitchParameter GetMailboxLog
		{
			get
			{
				return (SwitchParameter)(base.Fields["GetMailboxLog"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["GetMailboxLog"] = value;
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000384 RID: 900 RVA: 0x0000E968 File Offset: 0x0000CB68
		// (set) Token: 0x06000385 RID: 901 RVA: 0x0000E97F File Offset: 0x0000CB7F
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> NotificationEmailAddresses
		{
			get
			{
				return (MultiValuedProperty<string>)base.Fields["NotificationEmailAddresses"];
			}
			set
			{
				base.Fields["NotificationEmailAddresses"] = value;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000386 RID: 902 RVA: 0x0000E992 File Offset: 0x0000CB92
		// (set) Token: 0x06000387 RID: 903 RVA: 0x0000E9B8 File Offset: 0x0000CBB8
		[Parameter(Mandatory = false)]
		public SwitchParameter ActiveSync
		{
			get
			{
				return (SwitchParameter)(base.Fields["ActiveSync"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["ActiveSync"] = value;
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000388 RID: 904 RVA: 0x0000E9D0 File Offset: 0x0000CBD0
		// (set) Token: 0x06000389 RID: 905 RVA: 0x0000E9F6 File Offset: 0x0000CBF6
		[Parameter(Mandatory = false)]
		public SwitchParameter OWAforDevices
		{
			get
			{
				return (SwitchParameter)(base.Fields["OWAforDevices"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["OWAforDevices"] = value;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x0600038A RID: 906 RVA: 0x0000EA0E File Offset: 0x0000CC0E
		// (set) Token: 0x0600038B RID: 907 RVA: 0x0000EA34 File Offset: 0x0000CC34
		[Parameter]
		public SwitchParameter ShowRecoveryPassword
		{
			get
			{
				return (SwitchParameter)(base.Fields["ShowRecoveryPassword"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["ShowRecoveryPassword"] = value;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x0600038C RID: 908 RVA: 0x0000EA4C File Offset: 0x0000CC4C
		// (set) Token: 0x0600038D RID: 909 RVA: 0x0000EA54 File Offset: 0x0000CC54
		public MobileDeviceConfiguration DeviceConfiguration { get; set; }

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x0600038E RID: 910 RVA: 0x0000EA60 File Offset: 0x0000CC60
		private string UserName
		{
			get
			{
				if (this.userName == null && this.mailboxSession != null)
				{
					this.userName = this.mailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString();
					if (this.userName != null)
					{
						int num = this.userName.IndexOf('@');
						if (num >= 0)
						{
							this.userName = this.userName.Substring(0, num);
						}
					}
					else
					{
						this.userName = this.mailboxSession.MailboxOwner.MailboxInfo.DisplayName;
					}
				}
				return this.userName;
			}
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0000EAF8 File Offset: 0x0000CCF8
		protected override IConfigDataProvider CreateSession()
		{
			ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(base.RootOrgContainerId, (this.principal != null) ? this.principal.MailboxInfo.OrganizationId : base.CurrentOrganizationId, (this.principal != null) ? this.principal.MailboxInfo.OrganizationId : base.ExecutingUserOrganizationId, false);
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, false, ConsistencyMode.PartiallyConsistent, sessionSettings, 193, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\AirSync\\GetMobileDeviceStatistics.cs");
			tenantOrTopologyConfigurationSession.UseConfigNC = false;
			tenantOrTopologyConfigurationSession.UseGlobalCatalog = (base.DomainController == null && base.ServerSettings.ViewEntireForest);
			return tenantOrTopologyConfigurationSession;
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0000EBA0 File Offset: 0x0000CDA0
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			try
			{
				if (base.ParameterSetName == "Identity")
				{
					base.InternalValidate();
					if (base.HasErrors)
					{
						return;
					}
					if (MobileDeviceTaskHelper.IsRunningUnderMyOptionsRole(this, base.TenantGlobalCatalogSession, base.SessionSettings))
					{
						ADObjectId id;
						if (!base.TryGetExecutingUserId(out id))
						{
							throw new ExecutingUserPropertyNotFoundException("executingUserid");
						}
						TDataObject dataObject = this.DataObject;
						if (!dataObject.Id.Parent.Parent.Equals(id))
						{
							TIdentity identity = this.Identity;
							base.WriteError(new LocalizedException(Strings.ErrorObjectNotFound(identity.ToString())), ErrorCategory.InvalidArgument, null);
						}
					}
				}
				IRecipientSession recipientSession = this.CreateTenantGlobalCatalogSession(base.SessionSettings);
				this.GetExchangePrincipal(recipientSession);
				if (MobileDeviceTaskHelper.IsRunningUnderMyOptionsRole(this, base.TenantGlobalCatalogSession, base.SessionSettings))
				{
					ADObjectId id2;
					if (!base.TryGetExecutingUserId(out id2))
					{
						throw new ExecutingUserPropertyNotFoundException("executingUserid");
					}
					if (!this.principal.ObjectId.Equals(id2))
					{
						base.WriteError(new RecipientNotFoundException(this.Mailbox.ToString()), ErrorCategory.InvalidArgument, null);
					}
				}
				if (this.GetMailboxLog)
				{
					IList<LocalizedString> list = null;
					ADObjectId executingUserId = null;
					base.TryGetExecutingUserId(out executingUserId);
					this.validatedAddresses = MobileDeviceTaskHelper.ValidateAddresses(recipientSession, executingUserId, this.NotificationEmailAddresses, out list);
					if (list != null)
					{
						foreach (LocalizedString text in list)
						{
							this.WriteWarning(text);
						}
					}
				}
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0000ED84 File Offset: 0x0000CF84
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			bool flag = this.principal.RecipientTypeDetails == RecipientTypeDetails.MonitoringMailbox;
			try
			{
				this.mailboxSession = MailboxSession.OpenAsAdmin(this.principal, CultureInfo.InvariantCulture, "Client=Management;Action=Get-MobileDeviceStatistics");
				List<DeviceInfo> list = new List<DeviceInfo>();
				if (base.Fields["ActiveSync"] == null && base.Fields["OWAforDevices"] == null)
				{
					base.Fields["ActiveSync"] = new SwitchParameter(true);
					base.Fields["OWAforDevices"] = new SwitchParameter(true);
				}
				else if (base.Fields["ActiveSync"] == null)
				{
					if (this.OWAforDevices == false)
					{
						base.Fields["ActiveSync"] = new SwitchParameter(true);
					}
				}
				else if (base.Fields["OWAforDevices"] == null && this.ActiveSync == false)
				{
					base.Fields["OWAforDevices"] = new SwitchParameter(true);
				}
				if (this.ActiveSync == true)
				{
					DeviceInfo[] allDeviceInfo = DeviceInfo.GetAllDeviceInfo(this.mailboxSession, MobileClientType.EAS);
					if (allDeviceInfo != null)
					{
						list.AddRange(allDeviceInfo);
					}
				}
				if (this.OWAforDevices == true)
				{
					DeviceInfo[] allDeviceInfo2 = DeviceInfo.GetAllDeviceInfo(this.mailboxSession, MobileClientType.MOWA);
					if (allDeviceInfo2 != null)
					{
						list.AddRange(allDeviceInfo2);
					}
				}
				if (list != null)
				{
					List<MobileDevice> allMobileDevices = this.GetAllMobileDevices();
					int num = 0;
					using (List<DeviceInfo>.Enumerator enumerator = list.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							DeviceInfo deviceInfo = enumerator.Current;
							if (deviceInfo.DeviceADObjectId != null && ADObjectId.Equals(deviceInfo.UserADObjectId, this.principal.ObjectId))
							{
								MobileDevice mobileDevice = allMobileDevices.Find((MobileDevice currentDevice) => currentDevice.DeviceId.Equals(deviceInfo.DeviceIdentity.DeviceId));
								if (mobileDevice != null)
								{
									num++;
									this.deviceConfiguration = this.CreateDeviceConfiguration(deviceInfo);
									if (this.Identity != null)
									{
										TIdentity identity = this.Identity;
										if (!identity.InternalADObjectId.Equals(this.deviceConfiguration.Identity))
										{
											continue;
										}
									}
									if (mobileDevice != null && this.deviceConfiguration.DeviceAccessStateReason < DeviceAccessStateReason.UserAgentsChanges && mobileDevice.ClientType == MobileClientType.EAS)
									{
										if (!flag)
										{
											DeviceAccessState deviceAccessState = DeviceAccessState.Unknown;
											DeviceAccessStateReason deviceAccessStateReason = DeviceAccessStateReason.Unknown;
											ADObjectId deviceAccessControlRule = null;
											bool flag2 = false;
											if (mobileDevice.OrganizationId != OrganizationId.ForestWideOrgId && (mobileDevice.DeviceAccessStateReason != DeviceAccessStateReason.Individual || mobileDevice.DeviceAccessState != DeviceAccessState.Blocked) && mobileDevice.DeviceAccessStateReason != DeviceAccessStateReason.Policy)
											{
												Command.DetermineDeviceAccessState(this.LoadAbq(OrganizationId.ForestWideOrgId), mobileDevice.DeviceType, mobileDevice.DeviceModel, mobileDevice.DeviceUserAgent, mobileDevice.DeviceOS, out deviceAccessState, out deviceAccessStateReason, out deviceAccessControlRule);
												if (deviceAccessState == DeviceAccessState.Blocked)
												{
													mobileDevice.DeviceAccessState = deviceAccessState;
													mobileDevice.DeviceAccessStateReason = deviceAccessStateReason;
													mobileDevice.DeviceAccessControlRule = deviceAccessControlRule;
													flag2 = true;
												}
											}
											if (!flag2 && mobileDevice.DeviceAccessState != DeviceAccessState.DeviceDiscovery && mobileDevice.DeviceAccessStateReason != DeviceAccessStateReason.Individual && mobileDevice.DeviceAccessStateReason != DeviceAccessStateReason.Upgrade && mobileDevice.DeviceAccessStateReason != DeviceAccessStateReason.Policy)
											{
												Command.DetermineDeviceAccessState(this.LoadAbq(mobileDevice.OrganizationId), mobileDevice.DeviceType, mobileDevice.DeviceModel, mobileDevice.DeviceUserAgent, mobileDevice.DeviceOS, out deviceAccessState, out deviceAccessStateReason, out deviceAccessControlRule);
												mobileDevice.DeviceAccessState = deviceAccessState;
												mobileDevice.DeviceAccessStateReason = deviceAccessStateReason;
												mobileDevice.DeviceAccessControlRule = deviceAccessControlRule;
											}
										}
										this.deviceConfiguration.DeviceAccessState = mobileDevice.DeviceAccessState;
										this.deviceConfiguration.DeviceAccessStateReason = mobileDevice.DeviceAccessStateReason;
										this.deviceConfiguration.DeviceAccessControlRule = mobileDevice.DeviceAccessControlRule;
									}
									if (this.ShowRecoveryPassword == false)
									{
										this.deviceConfiguration.RecoveryPassword = "********";
									}
									if (this.GetMailboxLog)
									{
										this.deviceConfiguration.MailboxLogReport = deviceInfo.GetOrCreateMailboxLogReport(this.mailboxSession);
									}
									base.WriteObject(this.deviceConfiguration);
									if (this.Identity != null)
									{
										this.ProcessMailboxLogger(new DeviceInfo[]
										{
											deviceInfo
										});
									}
								}
							}
						}
					}
					if (this.Identity == null)
					{
						this.ProcessMailboxLogger(list.ToArray());
					}
					if (num > 0)
					{
						using (IBudget budget = StandardBudget.Acquire(this.mailboxSession.MailboxOwner.Sid, BudgetType.Eas, this.mailboxSession.GetADSessionSettings()))
						{
							if (budget != null)
							{
								IThrottlingPolicy throttlingPolicy = budget.ThrottlingPolicy;
								if (throttlingPolicy != null && !throttlingPolicy.EasMaxDevices.IsUnlimited && (long)num >= (long)((ulong)throttlingPolicy.EasMaxDevices.Value))
								{
									this.WriteWarning((num == 1) ? Strings.MaxDevicesReachedSingular(throttlingPolicy.EasMaxDevices.Value) : Strings.MaxDevicesReached(num, throttlingPolicy.EasMaxDevices.Value));
								}
							}
						}
					}
				}
			}
			catch (StorageTransientException ex)
			{
				TaskLogger.LogError(ex);
				base.WriteError(ex, ErrorCategory.ReadError, this.principal);
			}
			catch (StoragePermanentException ex2)
			{
				TaskLogger.LogError(ex2);
				base.WriteError(ex2, ErrorCategory.InvalidOperation, this.principal);
			}
			finally
			{
				if (this.mailboxSession != null)
				{
					this.mailboxSession.Dispose();
				}
				TaskLogger.LogExit();
			}
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0000F364 File Offset: 0x0000D564
		protected virtual MobileDeviceConfiguration CreateDeviceConfiguration(DeviceInfo deviceInfo)
		{
			return new MobileDeviceConfiguration(deviceInfo);
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000F36C File Offset: 0x0000D56C
		private OrganizationSettingsData LoadAbq(OrganizationId organizationId)
		{
			ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(base.RootOrgContainerId, organizationId, organizationId, false);
			IConfigurationSession configurationSession = this.CreateConfigurationSession(sessionSettings);
			ActiveSyncOrganizationSettings[] array = configurationSession.Find<ActiveSyncOrganizationSettings>(configurationSession.GetOrgContainerId(), QueryScope.SubTree, null, null, 2);
			if (array == null || array.Length == 0)
			{
				base.WriteError(new NoActiveSyncOrganizationSettingsException(organizationId.ToString()), ErrorCategory.InvalidArgument, null);
			}
			OrganizationSettingsData organizationSettingsData = new OrganizationSettingsData(array[0], configurationSession);
			this.organizationSettings[organizationId] = organizationSettingsData;
			return organizationSettingsData;
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000F3D4 File Offset: 0x0000D5D4
		private List<MobileDevice> GetAllMobileDevices()
		{
			IConfigurationSession configurationSession = (IConfigurationSession)this.CreateSession();
			ADPagedReader<MobileDevice> adpagedReader = configurationSession.FindPaged<MobileDevice>(this.principal.ObjectId, QueryScope.SubTree, null, null, 0);
			List<MobileDevice> list = new List<MobileDevice>();
			foreach (MobileDevice mobileDevice in adpagedReader)
			{
				if (!mobileDevice.MaximumSupportedExchangeObjectVersion.IsOlderThan(mobileDevice.ExchangeVersion) && 0 > mobileDevice.Id.DistinguishedName.IndexOf("Soft Deleted Objects", StringComparison.OrdinalIgnoreCase) && 0 > mobileDevice.Id.Rdn.EscapedName.IndexOf("-", StringComparison.OrdinalIgnoreCase) && 0 > mobileDevice.Id.Parent.Rdn.EscapedName.IndexOf("-", StringComparison.OrdinalIgnoreCase))
				{
					list.Add(mobileDevice);
				}
			}
			return list;
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0000F4BC File Offset: 0x0000D6BC
		private void GetExchangePrincipal(IRecipientSession recipientSession)
		{
			Exception ex = null;
			if (this.Mailbox != null)
			{
				this.principal = MobileDeviceTaskHelper.GetExchangePrincipal(base.SessionSettings, recipientSession, this.Mailbox, "Get-MobileDeviceStatistics", out ex);
			}
			else if (this.Identity != null)
			{
				TIdentity identity = this.Identity;
				MailboxIdParameter mailboxId = identity.GetMailboxId();
				if (mailboxId == null && this.DataObject != null)
				{
					this.Identity = (TIdentity)((object)this.CreateIdentityObject());
					TIdentity identity2 = this.Identity;
					mailboxId = identity2.GetMailboxId();
				}
				if (mailboxId == null)
				{
					TIdentity identity3 = this.Identity;
					base.WriteError(new LocalizedException(Strings.ErrorObjectNotFound(identity3.ToString())), ErrorCategory.InvalidArgument, null);
				}
				this.principal = MobileDeviceTaskHelper.GetExchangePrincipal(base.SessionSettings, recipientSession, mailboxId, base.CommandRuntime.ToString(), out ex);
			}
			if (ex != null)
			{
				base.WriteError(ex, ErrorCategory.InvalidArgument, null);
			}
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0000F5A8 File Offset: 0x0000D7A8
		protected virtual MobileDeviceIdParameter CreateIdentityObject()
		{
			return new MobileDeviceIdParameter(this.DataObject);
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0000F5BC File Offset: 0x0000D7BC
		private void ProcessMailboxLogger(params DeviceInfo[] deviceInfos)
		{
			if (!this.GetMailboxLog)
			{
				if (base.Fields["GetMailboxLog"] != null)
				{
					this.WriteWarning(Strings.MobileDeviceLogNotRetrieved);
				}
				return;
			}
			try
			{
				bool flag;
				if (!DeviceInfo.SendMailboxLog(this.mailboxSession, this.UserName, deviceInfos, this.validatedAddresses, out flag))
				{
					if (flag)
					{
						base.WriteError(new MobileDeviceLogException(Strings.MobileDeviceLogEMailFailure), ErrorCategory.NotSpecified, this.mailboxSession.MailboxOwner);
					}
					else
					{
						base.WriteError(new MobileDeviceLogException(Strings.MobileDeviceLogNoLogsExist), ErrorCategory.NotSpecified, this.mailboxSession.MailboxOwner);
					}
				}
			}
			catch (IOException ex)
			{
				base.WriteError(new MobileDeviceLogException(ex.Message), ErrorCategory.NotSpecified, this.mailboxSession.MailboxOwner);
			}
			catch (StoragePermanentException ex2)
			{
				base.WriteError(new MobileDeviceLogException(ex2.Message), ErrorCategory.NotSpecified, this.mailboxSession.MailboxOwner);
			}
			catch (StorageTransientException ex3)
			{
				base.WriteError(new MobileDeviceLogException(ex3.Message), ErrorCategory.NotSpecified, this.mailboxSession.MailboxOwner);
			}
		}

		// Token: 0x04000204 RID: 516
		private string userName;

		// Token: 0x04000205 RID: 517
		private ExchangePrincipal principal;

		// Token: 0x04000206 RID: 518
		private MailboxSession mailboxSession;

		// Token: 0x04000207 RID: 519
		private List<string> validatedAddresses;

		// Token: 0x04000208 RID: 520
		private Dictionary<OrganizationId, OrganizationSettingsData> organizationSettings = new Dictionary<OrganizationId, OrganizationSettingsData>(2);

		// Token: 0x04000209 RID: 521
		private MobileDeviceConfiguration deviceConfiguration;
	}
}
