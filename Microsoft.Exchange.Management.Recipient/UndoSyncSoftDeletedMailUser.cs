using System;
using System.Management.Automation;
using System.Security;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x020000D0 RID: 208
	[Cmdlet("Undo", "SyncSoftDeletedMailUser", SupportsShouldProcess = true, DefaultParameterSetName = "SoftDeletedMailUser")]
	public sealed class UndoSyncSoftDeletedMailUser : NewMailUserBase
	{
		// Token: 0x0600101D RID: 4125 RVA: 0x0003AD3C File Offset: 0x00038F3C
		public UndoSyncSoftDeletedMailUser()
		{
			base.NumberofCalls = ProvisioningCounters.NumberOfUndoSyncSoftDeletedMailuserCalls;
			base.NumberofSuccessfulCalls = ProvisioningCounters.NumberOfSuccessfulUndoSyncSoftDeletedMailuserCalls;
			base.AverageTimeTaken = ProvisioningCounters.AverageUndoSyncSoftDeletedMailuserResponseTime;
			base.AverageBaseTimeTaken = ProvisioningCounters.AverageUndoSyncSoftDeletedMailuserResponseTimeBase;
			base.AverageTimeTakenWithCache = ProvisioningCounters.AverageUndoSyncSoftDeletedMailuserResponseTimeWithCache;
			base.AverageBaseTimeTakenWithCache = ProvisioningCounters.AverageUndoSyncSoftDeletedMailuserResponseTimeBaseWithCache;
			base.AverageTimeTakenWithoutCache = ProvisioningCounters.AverageUndoSyncSoftDeletedMailuserResponseTimeWithoutCache;
			base.AverageBaseTimeTakenWithoutCache = ProvisioningCounters.AverageUndoSyncSoftDeletedMailuserResponseTimeBaseWithoutCache;
			base.TotalResponseTime = ProvisioningCounters.TotalUndoSyncSoftDeletedMailuserResponseTime;
			base.CacheActivePercentage = ProvisioningCounters.UndoSyncSoftDeletedMailuserCacheActivePercentage;
			base.CacheActiveBasePercentage = ProvisioningCounters.UndoSyncSoftDeletedMailuserCacheActivePercentageBase;
		}

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x0600101E RID: 4126 RVA: 0x0003ADC8 File Offset: 0x00038FC8
		// (set) Token: 0x0600101F RID: 4127 RVA: 0x0003ADD5 File Offset: 0x00038FD5
		[Parameter(Mandatory = true, Position = 0, ParameterSetName = "SoftDeletedMailUser", ValueFromPipeline = true)]
		public new MailUserIdParameter SoftDeletedObject
		{
			get
			{
				return (MailUserIdParameter)base.SoftDeletedObject;
			}
			set
			{
				base.SoftDeletedObject = value;
			}
		}

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x06001020 RID: 4128 RVA: 0x0003ADDE File Offset: 0x00038FDE
		// (set) Token: 0x06001021 RID: 4129 RVA: 0x0003ADE6 File Offset: 0x00038FE6
		[Parameter(Mandatory = false, ParameterSetName = "SoftDeletedMailUser")]
		public new string Name
		{
			get
			{
				return base.Name;
			}
			set
			{
				base.Name = value;
			}
		}

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x06001022 RID: 4130 RVA: 0x0003ADEF File Offset: 0x00038FEF
		// (set) Token: 0x06001023 RID: 4131 RVA: 0x0003ADF7 File Offset: 0x00038FF7
		[Parameter(Mandatory = false, ParameterSetName = "SoftDeletedMailUser")]
		public new WindowsLiveId WindowsLiveID
		{
			get
			{
				return base.WindowsLiveID;
			}
			set
			{
				base.WindowsLiveID = value;
			}
		}

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x06001024 RID: 4132 RVA: 0x0003AE00 File Offset: 0x00039000
		// (set) Token: 0x06001025 RID: 4133 RVA: 0x0003AE08 File Offset: 0x00039008
		[Parameter(Mandatory = false, ParameterSetName = "SoftDeletedMailUser")]
		public override SecureString Password
		{
			get
			{
				return base.Password;
			}
			set
			{
				base.Password = value;
			}
		}

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x06001026 RID: 4134 RVA: 0x0003AE11 File Offset: 0x00039011
		// (set) Token: 0x06001027 RID: 4135 RVA: 0x0003AE19 File Offset: 0x00039019
		[Parameter(Mandatory = false, ParameterSetName = "SoftDeletedMailUser")]
		public new SwitchParameter BypassLiveId
		{
			get
			{
				return base.BypassLiveId;
			}
			set
			{
				base.BypassLiveId = value;
			}
		}

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x06001028 RID: 4136 RVA: 0x0003AE22 File Offset: 0x00039022
		protected override bool AllowBypassLiveIdWithoutWlid
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001029 RID: 4137 RVA: 0x0003AE28 File Offset: 0x00039028
		protected override void InternalStateReset()
		{
			base.InternalStateReset();
			SmtpAddress windowsLiveID = this.DataObject.WindowsLiveID;
			NetID netID = this.DataObject.NetID;
			string name = this.DataObject.Name;
			string displayName = this.DataObject.DisplayName;
			this.DataObject = SoftDeletedTaskHelper.GetSoftDeletedADUser(base.CurrentOrganizationId, this.SoftDeletedObject, new Task.ErrorLoggerDelegate(base.WriteError));
			if (this.DataObject.WindowsLiveID != windowsLiveID)
			{
				this.DataObject.EmailAddressPolicyEnabled = false;
				this.DataObject.WindowsLiveID = windowsLiveID;
				this.DataObject.UserPrincipalName = windowsLiveID.ToString();
				this.DataObject.PrimarySmtpAddress = windowsLiveID;
			}
			if (this.DataObject.NetID != netID)
			{
				this.DataObject.NetID = netID;
			}
			if (!string.IsNullOrEmpty(name))
			{
				this.DataObject.Name = name;
			}
			this.DataObject.Name = SoftDeletedTaskHelper.GetUniqueNameForRecovery((IRecipientSession)base.DataSession, this.DataObject.Name, this.DataObject.Id);
			if (!string.IsNullOrEmpty(displayName))
			{
				this.DataObject.DisplayName = displayName;
			}
		}

		// Token: 0x0600102A RID: 4138 RVA: 0x0003AF58 File Offset: 0x00039158
		protected override void PrepareUserObject(ADUser user)
		{
			TaskLogger.LogEnter();
			base.PrepareUserObject(user);
			if (this.WindowsLiveID != null && this.WindowsLiveID.SmtpAddress != SmtpAddress.Empty)
			{
				user.EmailAddressPolicyEnabled = false;
				SmtpProxyAddress item = new SmtpProxyAddress(this.WindowsLiveID.SmtpAddress.ToString(), false);
				if (!user.EmailAddresses.Contains(item))
				{
					user.EmailAddresses.Add(item);
				}
			}
			if (user.ExchangeGuid == SoftDeletedTaskHelper.PredefinedExchangeGuid)
			{
				user.ExchangeGuid = user.PreviousExchangeGuid;
				if (!RecipientTaskHelper.IsExchangeGuidOrArchiveGuidUnique(user, ADMailboxRecipientSchema.ExchangeGuid, user.ExchangeGuid, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerDelegate(base.WriteError), ExchangeErrorCategory.Client))
				{
					user.ExchangeGuid = Guid.Empty;
				}
				user.PreviousExchangeGuid = Guid.Empty;
			}
			SoftDeletedTaskHelper.UpdateShadowWhenSoftDeletedProperty((IRecipientSession)base.DataSession, this.ConfigurationSession, base.CurrentOrganizationId, this.DataObject);
			this.DataObject.RecipientSoftDeletedStatus = 0;
			this.DataObject.WhenSoftDeleted = null;
			this.DataObject.InternalOnly = false;
			TaskLogger.LogExit();
		}

		// Token: 0x0600102B RID: 4139 RVA: 0x0003B088 File Offset: 0x00039288
		protected override void WriteResult(ADObject result)
		{
			TaskLogger.LogEnter(new object[]
			{
				result.Identity
			});
			SyncMailUser result2 = new SyncMailUser((ADUser)result);
			base.WriteResult(result2);
			TaskLogger.LogExit();
		}

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x0600102C RID: 4140 RVA: 0x0003B0C3 File Offset: 0x000392C3
		protected override string ClonableTypeName
		{
			get
			{
				return typeof(SyncMailUser).FullName;
			}
		}

		// Token: 0x0600102D RID: 4141 RVA: 0x0003B0D4 File Offset: 0x000392D4
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			return SyncMailUser.FromDataObject((ADUser)dataObject);
		}

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x0600102E RID: 4142 RVA: 0x0003B0E1 File Offset: 0x000392E1
		private new string Alias
		{
			get
			{
				return base.Alias;
			}
		}

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x0600102F RID: 4143 RVA: 0x0003B0E9 File Offset: 0x000392E9
		private new MailboxIdParameter ArbitrationMailbox
		{
			get
			{
				return base.ArbitrationMailbox;
			}
		}

		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x06001030 RID: 4144 RVA: 0x0003B0F1 File Offset: 0x000392F1
		private new SwitchParameter EvictLiveId
		{
			get
			{
				return base.EvictLiveId;
			}
		}

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x06001031 RID: 4145 RVA: 0x0003B0F9 File Offset: 0x000392F9
		private new string ExternalDirectoryObjectId
		{
			get
			{
				return base.ExternalDirectoryObjectId;
			}
		}

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x06001032 RID: 4146 RVA: 0x0003B101 File Offset: 0x00039301
		private new ProxyAddress ExternalEmailAddress
		{
			get
			{
				return base.ExternalEmailAddress;
			}
		}

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x06001033 RID: 4147 RVA: 0x0003B109 File Offset: 0x00039309
		private new string FederatedIdentity
		{
			get
			{
				return base.FederatedIdentity;
			}
		}

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x06001034 RID: 4148 RVA: 0x0003B111 File Offset: 0x00039311
		private new string FirstName
		{
			get
			{
				return base.FirstName;
			}
		}

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x06001035 RID: 4149 RVA: 0x0003B119 File Offset: 0x00039319
		private new string ImmutableId
		{
			get
			{
				return base.ImmutableId;
			}
		}

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x06001036 RID: 4150 RVA: 0x0003B121 File Offset: 0x00039321
		private new SwitchParameter ImportLiveId
		{
			get
			{
				return base.ImportLiveId;
			}
		}

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x06001037 RID: 4151 RVA: 0x0003B129 File Offset: 0x00039329
		private new string Initials
		{
			get
			{
				return base.Initials;
			}
		}

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x06001038 RID: 4152 RVA: 0x0003B131 File Offset: 0x00039331
		private new string LastName
		{
			get
			{
				return base.LastName;
			}
		}

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06001039 RID: 4153 RVA: 0x0003B139 File Offset: 0x00039339
		private new MacAttachmentFormat MacAttachmentFormat
		{
			get
			{
				return base.MacAttachmentFormat;
			}
		}

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x0600103A RID: 4154 RVA: 0x0003B141 File Offset: 0x00039341
		private new MessageBodyFormat MessageBodyFormat
		{
			get
			{
				return base.MessageBodyFormat;
			}
		}

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x0600103B RID: 4155 RVA: 0x0003B149 File Offset: 0x00039349
		private new MessageFormat MessageFormat
		{
			get
			{
				return base.MessageFormat;
			}
		}

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x0600103C RID: 4156 RVA: 0x0003B151 File Offset: 0x00039351
		private new WindowsLiveId MicrosoftOnlineServicesID
		{
			get
			{
				return base.MicrosoftOnlineServicesID;
			}
		}

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x0600103D RID: 4157 RVA: 0x0003B159 File Offset: 0x00039359
		private new MultiValuedProperty<ModeratorIDParameter> ModeratedBy
		{
			get
			{
				return base.ModeratedBy;
			}
		}

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x0600103E RID: 4158 RVA: 0x0003B161 File Offset: 0x00039361
		private new bool ModerationEnabled
		{
			get
			{
				return base.ModerationEnabled;
			}
		}

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x0600103F RID: 4159 RVA: 0x0003B169 File Offset: 0x00039369
		private new NetID NetID
		{
			get
			{
				return base.NetID;
			}
		}

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x06001040 RID: 4160 RVA: 0x0003B171 File Offset: 0x00039371
		private new OrganizationalUnitIdParameter OrganizationalUnit
		{
			get
			{
				return base.OrganizationalUnit;
			}
		}

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x06001041 RID: 4161 RVA: 0x0003B179 File Offset: 0x00039379
		private new SwitchParameter OverrideRecipientQuotas
		{
			get
			{
				return base.OverrideRecipientQuotas;
			}
		}

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x06001042 RID: 4162 RVA: 0x0003B181 File Offset: 0x00039381
		private new SmtpAddress PrimarySmtpAddress
		{
			get
			{
				return base.PrimarySmtpAddress;
			}
		}

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x06001043 RID: 4163 RVA: 0x0003B189 File Offset: 0x00039389
		private new bool RemotePowerShellEnabled
		{
			get
			{
				return base.RemotePowerShellEnabled;
			}
		}

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x06001044 RID: 4164 RVA: 0x0003B191 File Offset: 0x00039391
		private new bool ResetPasswordOnNextLogon
		{
			get
			{
				return base.ResetPasswordOnNextLogon;
			}
		}

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x06001045 RID: 4165 RVA: 0x0003B199 File Offset: 0x00039399
		private new string SamAccountName
		{
			get
			{
				return base.SamAccountName;
			}
		}

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x06001046 RID: 4166 RVA: 0x0003B1A1 File Offset: 0x000393A1
		private new TransportModerationNotificationFlags SendModerationNotifications
		{
			get
			{
				return base.SendModerationNotifications;
			}
		}

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x06001047 RID: 4167 RVA: 0x0003B1A9 File Offset: 0x000393A9
		private new bool SKUAssigned
		{
			get
			{
				return base.SKUAssigned;
			}
		}

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x06001048 RID: 4168 RVA: 0x0003B1B1 File Offset: 0x000393B1
		private new MultiValuedProperty<Capability> AddOnSKUCapability
		{
			get
			{
				return base.AddOnSKUCapability;
			}
		}

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x06001049 RID: 4169 RVA: 0x0003B1B9 File Offset: 0x000393B9
		private new Capability SKUCapability
		{
			get
			{
				return base.SKUCapability;
			}
		}

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x0600104A RID: 4170 RVA: 0x0003B1C1 File Offset: 0x000393C1
		private new CountryInfo UsageLocation
		{
			get
			{
				return base.UsageLocation;
			}
		}

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x0600104B RID: 4171 RVA: 0x0003B1C9 File Offset: 0x000393C9
		private new SwitchParameter UseExistingLiveId
		{
			get
			{
				return base.UseExistingLiveId;
			}
		}

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x0600104C RID: 4172 RVA: 0x0003B1D1 File Offset: 0x000393D1
		private new bool UsePreferMessageFormat
		{
			get
			{
				return base.UsePreferMessageFormat;
			}
		}

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x0600104D RID: 4173 RVA: 0x0003B1D9 File Offset: 0x000393D9
		private new string UserPrincipalName
		{
			get
			{
				return base.UserPrincipalName;
			}
		}
	}
}
