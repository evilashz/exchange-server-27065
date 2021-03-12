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
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000072 RID: 114
	[Cmdlet("New", "MailboxPlan", SupportsShouldProcess = true, DefaultParameterSetName = "MailboxPlan")]
	public sealed class NewMailboxPlan : NewMailboxOrSyncMailbox
	{
		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000858 RID: 2136 RVA: 0x000255C8 File Offset: 0x000237C8
		// (set) Token: 0x06000859 RID: 2137 RVA: 0x000255D0 File Offset: 0x000237D0
		private new AddressBookMailboxPolicyIdParameter AddressBookPolicy
		{
			get
			{
				return base.AddressBookPolicy;
			}
			set
			{
				base.AddressBookPolicy = value;
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x0600085A RID: 2138 RVA: 0x000255D9 File Offset: 0x000237D9
		private new Guid MailboxContainerGuid
		{
			get
			{
				return base.MailboxContainerGuid;
			}
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x0600085B RID: 2139 RVA: 0x000255E1 File Offset: 0x000237E1
		private new SwitchParameter ForestWideDomainControllerAffinityByExecutingUser
		{
			get
			{
				return base.ForestWideDomainControllerAffinityByExecutingUser;
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x0600085C RID: 2140 RVA: 0x000255E9 File Offset: 0x000237E9
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewMailboxPlan(base.Name.ToString(), this.UserPrincipalName.ToString(), base.RecipientContainerId.ToString());
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x0600085D RID: 2141 RVA: 0x00025611 File Offset: 0x00023811
		// (set) Token: 0x0600085E RID: 2142 RVA: 0x00025637 File Offset: 0x00023837
		[Parameter(Mandatory = false, ParameterSetName = "MailboxPlan")]
		public SwitchParameter IsDefault
		{
			get
			{
				return (SwitchParameter)(base.Fields["IsDefault"] ?? false);
			}
			set
			{
				base.Fields["IsDefault"] = value;
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x0600085F RID: 2143 RVA: 0x0002564F File Offset: 0x0002384F
		// (set) Token: 0x06000860 RID: 2144 RVA: 0x0002565C File Offset: 0x0002385C
		[Parameter(Mandatory = false, ParameterSetName = "MailboxPlan")]
		public string MailboxPlanIndex
		{
			get
			{
				return this.DataObject.MailboxPlanIndex;
			}
			set
			{
				this.DataObject.MailboxPlanIndex = value;
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000861 RID: 2145 RVA: 0x0002566A File Offset: 0x0002386A
		// (set) Token: 0x06000862 RID: 2146 RVA: 0x00025681 File Offset: 0x00023881
		[Parameter(Mandatory = false, ParameterSetName = "MailboxPlan")]
		public MailboxPlanRelease MailboxPlanRelease
		{
			get
			{
				return (MailboxPlanRelease)base.Fields["MailboxPlanRelease"];
			}
			set
			{
				base.Fields["MailboxPlanRelease"] = value;
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000863 RID: 2147 RVA: 0x00025699 File Offset: 0x00023899
		// (set) Token: 0x06000864 RID: 2148 RVA: 0x000256BA File Offset: 0x000238BA
		[Parameter(Mandatory = false, ParameterSetName = "MailboxPlan")]
		public bool IsPilotMailboxPlan
		{
			get
			{
				return (bool)(this.DataObject[MailboxPlanSchema.IsPilotMailboxPlan] ?? false);
			}
			set
			{
				this.DataObject[MailboxPlanSchema.IsPilotMailboxPlan] = value;
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000865 RID: 2149 RVA: 0x000256D2 File Offset: 0x000238D2
		// (set) Token: 0x06000866 RID: 2150 RVA: 0x000256D9 File Offset: 0x000238D9
		private new string ExternalDirectoryObjectId
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000867 RID: 2151 RVA: 0x000256E0 File Offset: 0x000238E0
		private new Capability SKUCapability
		{
			get
			{
				return base.SKUCapability;
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000868 RID: 2152 RVA: 0x000256E8 File Offset: 0x000238E8
		// (set) Token: 0x06000869 RID: 2153 RVA: 0x000256F0 File Offset: 0x000238F0
		private new MultiValuedProperty<Capability> AddOnSKUCapability
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

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x0600086A RID: 2154 RVA: 0x000256F9 File Offset: 0x000238F9
		private new bool SKUAssigned
		{
			get
			{
				return base.SKUAssigned;
			}
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x0600086B RID: 2155 RVA: 0x00025701 File Offset: 0x00023901
		// (set) Token: 0x0600086C RID: 2156 RVA: 0x00025708 File Offset: 0x00023908
		private new CountryInfo UsageLocation
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x0600086D RID: 2157 RVA: 0x0002570F File Offset: 0x0002390F
		// (set) Token: 0x0600086E RID: 2158 RVA: 0x00025735 File Offset: 0x00023935
		private new SwitchParameter BypassLiveId
		{
			get
			{
				return (SwitchParameter)(base.Fields["BypassLiveId"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["BypassLiveId"] = value;
			}
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x0600086F RID: 2159 RVA: 0x0002574D File Offset: 0x0002394D
		private new DatabaseIdParameter Database
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x00025750 File Offset: 0x00023950
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			this.isDatabaseRequired = false;
			base.InternalBeginProcessing();
			TaskLogger.LogExit();
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x00025788 File Offset: 0x00023988
		protected override void PrepareRecipientObject(ADUser user)
		{
			TaskLogger.LogEnter();
			base.PrepareRecipientObject(user);
			user.Database = ProvisioningCache.Instance.TryAddAndGetGlobalData<ADObjectId>(CannedProvisioningCacheKeys.DatabaseContainerId, () => base.GlobalConfigSession.GetDatabasesContainerId());
			string str = ProvisioningCache.Instance.TryAddAndGetGlobalData<string>(CannedProvisioningCacheKeys.AdministrativeGroupLegDN, () => base.GlobalConfigSession.GetAdministrativeGroup().LegacyExchangeDN);
			user.MailboxRelease = MailboxRelease.None;
			user.ArchiveRelease = MailboxRelease.None;
			user.ServerLegacyDN = str + "/cn=Servers/cn=73BCAADF75B34A4781FDCDA446B76E7C";
			MailboxTaskHelper.StampMailboxRecipientTypes(user, "MailboxPlan");
			base.IsSetRandomPassword = true;
			if (this.IsDefault || ((IRecipientSession)base.DataSession).Find(null, QueryScope.SubTree, RecipientFilterHelper.MailboxPlanFilter, null, 1).Length == 0)
			{
				user.IsDefault = true;
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x00025845 File Offset: 0x00023A45
		protected override void ValidateMailboxPlanRelease()
		{
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x00025848 File Offset: 0x00023A48
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			ADUser aduser = null;
			if (this.DataObject.IsDefault)
			{
				aduser = RecipientTaskHelper.ResetOldDefaultPlan((IRecipientSession)base.DataSession, this.DataObject.Id, this.DataObject.OrganizationalUnitRoot, new Task.ErrorLoggerDelegate(base.WriteError));
			}
			bool flag = false;
			try
			{
				this.DataObject[MailboxPlanSchema.MailboxPlanRelease] = (base.Fields.IsModified("MailboxPlanRelease") ? this.MailboxPlanRelease : MailboxPlanRelease.AllReleases);
				base.InternalProcessRecord();
				flag = true;
			}
			finally
			{
				if (!flag && aduser != null)
				{
					aduser.IsDefault = true;
					try
					{
						base.DataSession.Save(aduser);
					}
					catch (DataSourceTransientException exception)
					{
						base.WriteError(exception, ExchangeErrorCategory.Client, null);
					}
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x00025928 File Offset: 0x00023B28
		protected override void WriteResult(ADObject result)
		{
			TaskLogger.LogEnter(new object[]
			{
				result.Identity
			});
			ADUser aduser = (ADUser)result;
			if (null != aduser.MasterAccountSid)
			{
				aduser.LinkedMasterAccount = SecurityPrincipalIdParameter.GetFriendlyUserName(aduser.MasterAccountSid, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose));
				aduser.ResetChangeTracking();
			}
			base.WriteResult(new MailboxPlan(aduser));
			TaskLogger.LogExit();
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x00025994 File Offset: 0x00023B94
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			return Microsoft.Exchange.Data.Directory.Management.MailboxPlan.FromDataObject((ADUser)dataObject);
		}

		// Token: 0x040001E3 RID: 483
		private const string MailboxServerId = "73BCAADF75B34A4781FDCDA446B76E7C";
	}
}
