using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x020000D3 RID: 211
	[Cmdlet("New", "SyncUser", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class NewSyncUser : NewGeneralRecipientObjectTask<ADUser>
	{
		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x06001082 RID: 4226 RVA: 0x0003BE6F File Offset: 0x0003A06F
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewSyncUser(base.Name.ToString());
			}
		}

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x06001083 RID: 4227 RVA: 0x0003BE81 File Offset: 0x0003A081
		// (set) Token: 0x06001084 RID: 4228 RVA: 0x0003BE8E File Offset: 0x0003A08E
		[Parameter(Mandatory = false)]
		public string OnPremisesObjectId
		{
			get
			{
				return this.DataObject.OnPremisesObjectId;
			}
			set
			{
				this.DataObject.OnPremisesObjectId = value;
			}
		}

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x06001085 RID: 4229 RVA: 0x0003BE9C File Offset: 0x0003A09C
		// (set) Token: 0x06001086 RID: 4230 RVA: 0x0003BEA9 File Offset: 0x0003A0A9
		[Parameter(Mandatory = false)]
		public bool IsDirSynced
		{
			get
			{
				return this.DataObject.IsDirSynced;
			}
			set
			{
				this.DataObject.IsDirSynced = value;
			}
		}

		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x06001087 RID: 4231 RVA: 0x0003BEB7 File Offset: 0x0003A0B7
		// (set) Token: 0x06001088 RID: 4232 RVA: 0x0003BEC4 File Offset: 0x0003A0C4
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> DirSyncAuthorityMetadata
		{
			get
			{
				return this.DataObject.DirSyncAuthorityMetadata;
			}
			set
			{
				this.DataObject.DirSyncAuthorityMetadata = value;
			}
		}

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x06001089 RID: 4233 RVA: 0x0003BED2 File Offset: 0x0003A0D2
		// (set) Token: 0x0600108A RID: 4234 RVA: 0x0003BEDF File Offset: 0x0003A0DF
		[Parameter(Mandatory = true, ParameterSetName = "WindowsLiveID")]
		public SmtpAddress WindowsLiveID
		{
			get
			{
				return this.DataObject.WindowsLiveID;
			}
			set
			{
				this.DataObject.WindowsLiveID = value;
			}
		}

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x0600108B RID: 4235 RVA: 0x0003BEED File Offset: 0x0003A0ED
		// (set) Token: 0x0600108C RID: 4236 RVA: 0x0003BEFA File Offset: 0x0003A0FA
		[Parameter(Mandatory = true, ParameterSetName = "WindowsLiveID")]
		public NetID NetID
		{
			get
			{
				return this.DataObject.NetID;
			}
			set
			{
				this.DataObject.NetID = value;
			}
		}

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x0600108D RID: 4237 RVA: 0x0003BF08 File Offset: 0x0003A108
		// (set) Token: 0x0600108E RID: 4238 RVA: 0x0003BF15 File Offset: 0x0003A115
		[Parameter(Mandatory = false)]
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

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x0600108F RID: 4239 RVA: 0x0003BF23 File Offset: 0x0003A123
		// (set) Token: 0x06001090 RID: 4240 RVA: 0x0003BF30 File Offset: 0x0003A130
		[Parameter(Mandatory = false)]
		public ReleaseTrack? ReleaseTrack
		{
			get
			{
				return this.DataObject.ReleaseTrack;
			}
			set
			{
				this.DataObject.ReleaseTrack = value;
			}
		}

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x06001091 RID: 4241 RVA: 0x0003BF3E File Offset: 0x0003A13E
		// (set) Token: 0x06001092 RID: 4242 RVA: 0x0003BF4B File Offset: 0x0003A14B
		[Parameter(Mandatory = false)]
		public RemoteRecipientType RemoteRecipientType
		{
			get
			{
				return this.DataObject.RemoteRecipientType;
			}
			set
			{
				this.DataObject.RemoteRecipientType = value;
			}
		}

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x06001093 RID: 4243 RVA: 0x0003BF59 File Offset: 0x0003A159
		// (set) Token: 0x06001094 RID: 4244 RVA: 0x0003BF70 File Offset: 0x0003A170
		[Parameter(Mandatory = false)]
		public string ValidationOrganization
		{
			get
			{
				return (string)base.Fields["ValidationOrganization"];
			}
			set
			{
				base.Fields["ValidationOrganization"] = value;
			}
		}

		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x06001095 RID: 4245 RVA: 0x0003BF83 File Offset: 0x0003A183
		// (set) Token: 0x06001096 RID: 4246 RVA: 0x0003BF9A File Offset: 0x0003A19A
		[Parameter(Mandatory = false)]
		public SwitchParameter AccountDisabled
		{
			get
			{
				return (SwitchParameter)base.Fields[SyncUserSchema.AccountDisabled];
			}
			set
			{
				base.Fields[SyncUserSchema.AccountDisabled] = value;
			}
		}

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x06001097 RID: 4247 RVA: 0x0003BFB2 File Offset: 0x0003A1B2
		// (set) Token: 0x06001098 RID: 4248 RVA: 0x0003BFBF File Offset: 0x0003A1BF
		[Parameter(Mandatory = false)]
		public DateTime? StsRefreshTokensValidFrom
		{
			get
			{
				return this.DataObject.StsRefreshTokensValidFrom;
			}
			set
			{
				this.DataObject.StsRefreshTokensValidFrom = value;
			}
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x0003BFD0 File Offset: 0x0003A1D0
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			if (this.ValidationOrganization != null && !string.Equals(this.ValidationOrganization, base.CurrentOrganizationId.ToString(), StringComparison.OrdinalIgnoreCase))
			{
				base.ThrowTerminatingError(new ValidationOrgCurrentOrgNotMatchException(this.ValidationOrganization, base.CurrentOrganizationId.ToString()), ExchangeErrorCategory.Client, null);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0600109A RID: 4250 RVA: 0x0003C030 File Offset: 0x0003A230
		protected override void PrepareRecipientObject(ADUser user)
		{
			TaskLogger.LogEnter();
			base.PrepareRecipientObject(user);
			if (user.IsModified(ADRecipientSchema.WindowsLiveID) && user.WindowsLiveID != SmtpAddress.Empty)
			{
				user.UserPrincipalName = user.WindowsLiveID.ToString();
			}
			if (!user.IsModified(ADRecipientSchema.DisplayName))
			{
				user.DisplayName = user.Name;
			}
			if (!user.IsModified(IADSecurityPrincipalSchema.SamAccountName))
			{
				user.SamAccountName = RecipientTaskHelper.GenerateUniqueSamAccountName(base.PartitionOrRootOrgGlobalCatalogSession, user.Id.DomainId, user.Name, false, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), true);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0600109B RID: 4251 RVA: 0x0003C0E0 File Offset: 0x0003A2E0
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.DataSession.Save(this.DataObject);
			base.WriteVerbose(Strings.VerboseSettingPassword(this.DataObject.Id.ToString()));
			MailboxTaskHelper.SetMailboxPassword((IRecipientSession)base.DataSession, this.DataObject, null, new Task.ErrorLoggerDelegate(base.WriteError));
			this.DataObject = (ADUser)base.DataSession.Read<ADUser>(this.DataObject.Identity);
			this.DataObject[ADUserSchema.PasswordLastSetRaw] = new long?(-1L);
			this.DataObject.UserAccountControl = UserAccountControlFlags.NormalAccount;
			if (base.Fields.IsModified(SyncMailUserSchema.AccountDisabled))
			{
				SyncTaskHelper.SetExchangeAccountDisabled(this.DataObject, this.AccountDisabled);
			}
			base.InternalProcessRecord();
			TaskLogger.LogExit();
		}

		// Token: 0x0600109C RID: 4252 RVA: 0x0003C1C1 File Offset: 0x0003A3C1
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			return SyncUser.FromDataObject((ADUser)dataObject);
		}
	}
}
