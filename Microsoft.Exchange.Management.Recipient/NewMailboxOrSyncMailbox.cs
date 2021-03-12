using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.ProvisioningCache;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.CmdletInfra;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000047 RID: 71
	public abstract class NewMailboxOrSyncMailbox : NewMailboxBase
	{
		// Token: 0x17000147 RID: 327
		// (get) Token: 0x0600039F RID: 927 RVA: 0x00011592 File Offset: 0x0000F792
		protected override bool runUMSteps
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x00011595 File Offset: 0x0000F795
		// (set) Token: 0x060003A1 RID: 929 RVA: 0x0001159D File Offset: 0x0000F79D
		[Parameter(Mandatory = true, Position = 0, ValueFromPipelineByPropertyName = true)]
		public new string Name
		{
			get
			{
				return base.Name;
			}
			set
			{
				base.Name = MailboxTaskHelper.GetNameOfAcceptableLengthForMultiTenantMode(value, out this.nameWarning);
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060003A2 RID: 930 RVA: 0x000115B1 File Offset: 0x0000F7B1
		// (set) Token: 0x060003A3 RID: 931 RVA: 0x000115C8 File Offset: 0x0000F7C8
		[Parameter(Mandatory = false, ParameterSetName = "WindowsLiveCustomDomains")]
		[Parameter(Mandatory = false, ParameterSetName = "ImportLiveId")]
		[Parameter(Mandatory = false, ParameterSetName = "RemovedMailbox")]
		[Parameter(Mandatory = false, ParameterSetName = "FederatedUser")]
		[Parameter(Mandatory = false, ParameterSetName = "MicrosoftOnlineServicesFederatedUser")]
		[Parameter(Mandatory = false, ParameterSetName = "RemoteArchive")]
		[Parameter(Mandatory = false, ParameterSetName = "MicrosoftOnlineServicesID")]
		[Parameter(Mandatory = false, ParameterSetName = "MailboxPlan")]
		[Parameter(Mandatory = false, ParameterSetName = "DisabledUser")]
		[Parameter(Mandatory = false, ParameterSetName = "User")]
		[Parameter(Mandatory = false, ParameterSetName = "WindowsLiveID")]
		public MailboxPlanIdParameter MailboxPlan
		{
			get
			{
				return (MailboxPlanIdParameter)base.Fields[ADRecipientSchema.MailboxPlan];
			}
			set
			{
				base.Fields[ADRecipientSchema.MailboxPlan] = value;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060003A4 RID: 932 RVA: 0x000115DB File Offset: 0x0000F7DB
		// (set) Token: 0x060003A5 RID: 933 RVA: 0x000115E3 File Offset: 0x0000F7E3
		[Parameter(Mandatory = false, ParameterSetName = "MicrosoftOnlineServicesID")]
		[Parameter(Mandatory = false, ParameterSetName = "DisabledUser")]
		[Parameter(Mandatory = false, ParameterSetName = "RemoteArchive")]
		[Parameter(Mandatory = false, ParameterSetName = "WindowsLiveID")]
		[Parameter(Mandatory = false, ParameterSetName = "WindowsLiveCustomDomains")]
		[Parameter(Mandatory = false, ParameterSetName = "MailboxPlan")]
		[Parameter(Mandatory = false, ParameterSetName = "User")]
		[Parameter(Mandatory = false, ParameterSetName = "ImportLiveId")]
		[Parameter(Mandatory = false, ParameterSetName = "RemovedMailbox")]
		[Parameter(Mandatory = false, ParameterSetName = "FederatedUser")]
		[Parameter(Mandatory = false, ParameterSetName = "MicrosoftOnlineServicesFederatedUser")]
		public override Capability SKUCapability
		{
			get
			{
				return base.SKUCapability;
			}
			set
			{
				base.SKUCapability = value;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060003A6 RID: 934 RVA: 0x000115EC File Offset: 0x0000F7EC
		// (set) Token: 0x060003A7 RID: 935 RVA: 0x000115F4 File Offset: 0x0000F7F4
		[Parameter(Mandatory = false, ParameterSetName = "MicrosoftOnlineServicesID")]
		[Parameter(Mandatory = false, ParameterSetName = "MailboxPlan")]
		[Parameter(Mandatory = false, ParameterSetName = "User")]
		[Parameter(Mandatory = false, ParameterSetName = "WindowsLiveID")]
		[Parameter(Mandatory = false, ParameterSetName = "WindowsLiveCustomDomains")]
		[Parameter(Mandatory = false, ParameterSetName = "DisabledUser")]
		[Parameter(Mandatory = false, ParameterSetName = "ImportLiveId")]
		[Parameter(Mandatory = false, ParameterSetName = "RemovedMailbox")]
		[Parameter(Mandatory = false, ParameterSetName = "FederatedUser")]
		[Parameter(Mandatory = false, ParameterSetName = "MicrosoftOnlineServicesFederatedUser")]
		[Parameter(Mandatory = false, ParameterSetName = "RemoteArchive")]
		public override MultiValuedProperty<Capability> AddOnSKUCapability
		{
			get
			{
				return base.AddOnSKUCapability;
			}
			set
			{
				base.AddOnSKUCapability = value;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x000115FD File Offset: 0x0000F7FD
		// (set) Token: 0x060003A9 RID: 937 RVA: 0x00011605 File Offset: 0x0000F805
		[Parameter(Mandatory = false, ParameterSetName = "DisabledUser")]
		[Parameter(Mandatory = false, ParameterSetName = "FederatedUser")]
		[Parameter(Mandatory = false, ParameterSetName = "MicrosoftOnlineServicesFederatedUser")]
		[Parameter(Mandatory = false, ParameterSetName = "RemoteArchive")]
		[Parameter(Mandatory = false, ParameterSetName = "MicrosoftOnlineServicesID")]
		[Parameter(Mandatory = false, ParameterSetName = "ImportLiveId")]
		[Parameter(Mandatory = false, ParameterSetName = "RemovedMailbox")]
		[Parameter(Mandatory = false, ParameterSetName = "MailboxPlan")]
		[Parameter(Mandatory = false, ParameterSetName = "User")]
		[Parameter(Mandatory = false, ParameterSetName = "WindowsLiveID")]
		[Parameter(Mandatory = false, ParameterSetName = "WindowsLiveCustomDomains")]
		public override bool SKUAssigned
		{
			get
			{
				return base.SKUAssigned;
			}
			set
			{
				base.SKUAssigned = value;
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060003AA RID: 938 RVA: 0x0001160E File Offset: 0x0000F80E
		// (set) Token: 0x060003AB RID: 939 RVA: 0x0001161B File Offset: 0x0000F81B
		[Parameter(Mandatory = false, ParameterSetName = "WindowsLiveID")]
		[Parameter(Mandatory = false, ParameterSetName = "MicrosoftOnlineServicesFederatedUser")]
		[Parameter(Mandatory = false, ParameterSetName = "WindowsLiveCustomDomains")]
		[Parameter(Mandatory = false, ParameterSetName = "MicrosoftOnlineServicesID")]
		[Parameter(Mandatory = false, ParameterSetName = "FederatedUser")]
		[Parameter(Mandatory = false, ParameterSetName = "User")]
		[Parameter(Mandatory = false, ParameterSetName = "DisabledUser")]
		[Parameter(Mandatory = false, ParameterSetName = "ImportLiveId")]
		public CountryInfo UsageLocation
		{
			get
			{
				return this.DataObject.UsageLocation;
			}
			set
			{
				this.DataObject.UsageLocation = value;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060003AC RID: 940 RVA: 0x00011629 File Offset: 0x0000F829
		// (set) Token: 0x060003AD RID: 941 RVA: 0x0001164F File Offset: 0x0000F84F
		[Parameter(Mandatory = false)]
		public SwitchParameter TargetAllMDBs
		{
			get
			{
				return (SwitchParameter)(base.Fields["TargetAllMDBs"] ?? true);
			}
			set
			{
				base.Fields["TargetAllMDBs"] = value;
			}
		}

		// Token: 0x060003AE RID: 942 RVA: 0x00011667 File Offset: 0x0000F867
		protected override ITaskModuleFactory CreateTaskModuleFactory()
		{
			return new NewMailboxTaskModuleFactory();
		}

		// Token: 0x060003AF RID: 943 RVA: 0x000116C4 File Offset: 0x0000F8C4
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			if (this.nameWarning != LocalizedString.Empty)
			{
				this.WriteWarning(this.nameWarning);
			}
			base.InternalBeginProcessing();
			base.CheckExclusiveParameters(new object[]
			{
				ADRecipientSchema.MailboxPlan,
				"SKUCapability"
			});
			if (this.MailboxPlan != null)
			{
				this.mailboxPlanObject = base.ProvisioningCache.TryAddAndGetOrganizationDictionaryValue<ADUser, string>(CannedProvisioningCacheKeys.CacheKeyMailboxPlanIdParameterId, base.CurrentOrganizationId, this.MailboxPlan.RawIdentity, () => (ADUser)base.GetDataObject<ADUser>(this.MailboxPlan, base.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.ErrorMailboxPlanNotFound(this.MailboxPlan.ToString())), new LocalizedString?(Strings.ErrorMailboxPlanNotUnique(this.MailboxPlan.ToString())), ExchangeErrorCategory.Client));
				this.ValidateMailboxPlanRelease();
			}
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(true, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 395, "InternalBeginProcessing", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\RecipientTasks\\mailbox\\NewMailbox.cs");
			Organization organization = null;
			using (new CmdletMonitoredScope(base.CurrentTaskContext.UniqueId, "BizLogic", "ITopologyConfigurationSession.GetOrgContainer", LoggerHelper.CmdletPerfMonitors))
			{
				organization = topologyConfigurationSession.GetOrgContainer();
			}
			this.isMailboxForcedReplicationDisabled = organization.IsMailboxForcedReplicationDisabled;
			TaskLogger.LogExit();
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x000117DC File Offset: 0x0000F9DC
		protected virtual void ValidateMailboxPlanRelease()
		{
			MailboxTaskHelper.ValidateMailboxPlanRelease(this.mailboxPlanObject, new Task.ErrorLoggerDelegate(base.WriteError));
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x000117F8 File Offset: 0x0000F9F8
		protected override void PrepareRecipientObject(ADUser user)
		{
			TaskLogger.LogEnter();
			ADUser aduser = this.mailboxPlanObject;
			if (aduser != null)
			{
				user.MailboxPlan = aduser.Id;
			}
			else if (user.MailboxPlan != null)
			{
				if (user.MailboxPlanObject != null)
				{
					aduser = user.MailboxPlanObject;
				}
				else
				{
					MailboxPlanIdParameter mailboxPlanIdParameter = new MailboxPlanIdParameter(user.MailboxPlan);
					aduser = (ADUser)base.GetDataObject<ADUser>(mailboxPlanIdParameter, base.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.ErrorMailboxPlanNotFound(mailboxPlanIdParameter.ToString())), new LocalizedString?(Strings.ErrorMailboxPlanNotUnique(mailboxPlanIdParameter.ToString())), ExchangeErrorCategory.Client);
				}
				this.mailboxPlanObject = aduser;
			}
			user.MailboxPlanObject = null;
			user.propertyBag.ResetChangeTracking(ADRecipientSchema.MailboxPlanObject);
			if (aduser != null)
			{
				using (new CmdletMonitoredScope(base.CurrentTaskContext.UniqueId, "BizLogic", "NewMailboxOrSyncMailbox.PrepareRecipientObject", LoggerHelper.CmdletPerfMonitors))
				{
					ADUser aduser2 = new ADUser();
					aduser2.StampPersistableDefaultValues();
					aduser2.StampDefaultValues(RecipientType.UserMailbox);
					aduser2.ResetChangeTracking();
					User.FromDataObject(aduser2).ApplyCloneableProperties(User.FromDataObject(aduser));
					Mailbox.FromDataObject(aduser2).ApplyCloneableProperties(Mailbox.FromDataObject(aduser));
					CASMailbox.FromDataObject(aduser2).ApplyCloneableProperties(CASMailbox.FromDataObject(aduser));
					UMMailbox.FromDataObject(aduser2).ApplyCloneableProperties(UMMailbox.FromDataObject(aduser));
					bool litigationHoldEnabled = user.LitigationHoldEnabled;
					ElcMailboxFlags elcMailboxFlags = user.ElcMailboxFlags;
					user.CopyChangesFrom(aduser2);
					if (base.SoftDeletedObject != null)
					{
						if (litigationHoldEnabled != user.LitigationHoldEnabled)
						{
							user.LitigationHoldEnabled = litigationHoldEnabled;
						}
						if (elcMailboxFlags != user.ElcMailboxFlags)
						{
							user.ElcMailboxFlags = elcMailboxFlags;
						}
					}
				}
			}
			base.PrepareRecipientObject(user);
			MailboxTaskHelper.WriteWarningWhenMailboxIsUnlicensed(user, new Task.TaskWarningLoggingDelegate(this.WriteWarning));
			TaskLogger.LogExit();
		}

		// Token: 0x040000BF RID: 191
		private LocalizedString nameWarning = LocalizedString.Empty;

		// Token: 0x040000C0 RID: 192
		protected ADUser mailboxPlanObject;
	}
}
