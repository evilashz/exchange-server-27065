using System;
using System.Management.Automation;
using System.Security;
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
	// Token: 0x0200006F RID: 111
	[Cmdlet("Set", "MailboxPlan", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetMailboxPlan : SetMailboxBase<MailboxPlanIdParameter, MailboxPlan>
	{
		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x0600081A RID: 2074 RVA: 0x00024E35 File Offset: 0x00023035
		// (set) Token: 0x0600081B RID: 2075 RVA: 0x00024E3D File Offset: 0x0002303D
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

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x0600081C RID: 2076 RVA: 0x00024E46 File Offset: 0x00023046
		// (set) Token: 0x0600081D RID: 2077 RVA: 0x00024E6C File Offset: 0x0002306C
		[Parameter(Mandatory = false)]
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

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x0600081E RID: 2078 RVA: 0x00024E84 File Offset: 0x00023084
		// (set) Token: 0x0600081F RID: 2079 RVA: 0x00024EAA File Offset: 0x000230AA
		[Parameter(Mandatory = false)]
		public SwitchParameter IsDefaultForPreviousVersion
		{
			get
			{
				return (SwitchParameter)(base.Fields["IsDefaultForPreviousVersion"] ?? false);
			}
			set
			{
				base.Fields["IsDefaultForPreviousVersion"] = value;
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000820 RID: 2080 RVA: 0x00024EC2 File Offset: 0x000230C2
		// (set) Token: 0x06000821 RID: 2081 RVA: 0x00024ED9 File Offset: 0x000230D9
		[Parameter(Mandatory = false)]
		public DatabaseIdParameter Database
		{
			get
			{
				return (DatabaseIdParameter)base.Fields["Database"];
			}
			set
			{
				base.Fields["Database"] = value;
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000822 RID: 2082 RVA: 0x00024EEC File Offset: 0x000230EC
		// (set) Token: 0x06000823 RID: 2083 RVA: 0x00024F03 File Offset: 0x00023103
		[Parameter(Mandatory = false)]
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

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000824 RID: 2084 RVA: 0x00024F1B File Offset: 0x0002311B
		// (set) Token: 0x06000825 RID: 2085 RVA: 0x00024F41 File Offset: 0x00023141
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

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000826 RID: 2086 RVA: 0x00024F59 File Offset: 0x00023159
		// (set) Token: 0x06000827 RID: 2087 RVA: 0x00024F70 File Offset: 0x00023170
		private new NetID NetID
		{
			get
			{
				return (NetID)base.Fields["NetID"];
			}
			set
			{
				base.Fields["NetID"] = value;
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000828 RID: 2088 RVA: 0x00024F83 File Offset: 0x00023183
		internal new MultiValuedProperty<DeliveryRecipientIdParameter> AcceptMessagesOnlyFrom
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000829 RID: 2089 RVA: 0x00024F86 File Offset: 0x00023186
		internal new MultiValuedProperty<DeliveryRecipientIdParameter> AcceptMessagesOnlyFromDLMembers
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x0600082A RID: 2090 RVA: 0x00024F89 File Offset: 0x00023189
		internal new MultiValuedProperty<DeliveryRecipientIdParameter> AcceptMessagesOnlyFromSendersOrMembers
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x0600082B RID: 2091 RVA: 0x00024F8C File Offset: 0x0002318C
		internal new SwitchParameter Arbitration
		{
			get
			{
				return base.Arbitration;
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x0600082C RID: 2092 RVA: 0x00024F94 File Offset: 0x00023194
		internal new SwitchParameter PublicFolder
		{
			get
			{
				return base.PublicFolder;
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x0600082D RID: 2093 RVA: 0x00024F9C File Offset: 0x0002319C
		internal new MailboxIdParameter ArbitrationMailbox
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x0600082E RID: 2094 RVA: 0x00024F9F File Offset: 0x0002319F
		internal new MultiValuedProperty<DeliveryRecipientIdParameter> BypassModerationFromSendersOrMembers
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x0600082F RID: 2095 RVA: 0x00024FA2 File Offset: 0x000231A2
		internal new bool CreateDTMFMap
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000830 RID: 2096 RVA: 0x00024FA5 File Offset: 0x000231A5
		internal new PSCredential LinkedCredential
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000831 RID: 2097 RVA: 0x00024FA8 File Offset: 0x000231A8
		internal new RecipientIdParameter ForwardingAddress
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000832 RID: 2098 RVA: 0x00024FAB File Offset: 0x000231AB
		internal new MultiValuedProperty<RecipientIdParameter> GrantSendOnBehalfTo
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000833 RID: 2099 RVA: 0x00024FAE File Offset: 0x000231AE
		internal new string LinkedDomainController
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000834 RID: 2100 RVA: 0x00024FB1 File Offset: 0x000231B1
		internal new UserIdParameter LinkedMasterAccount
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000835 RID: 2101 RVA: 0x00024FB4 File Offset: 0x000231B4
		internal new MultiValuedProperty<ModeratorIDParameter> ModeratedBy
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000836 RID: 2102 RVA: 0x00024FB7 File Offset: 0x000231B7
		internal new SecureString Password
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000837 RID: 2103 RVA: 0x00024FBA File Offset: 0x000231BA
		internal new MultiValuedProperty<DeliveryRecipientIdParameter> RejectMessagesFrom
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000838 RID: 2104 RVA: 0x00024FBD File Offset: 0x000231BD
		internal new MultiValuedProperty<DeliveryRecipientIdParameter> RejectMessagesFromDLMembers
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000839 RID: 2105 RVA: 0x00024FC0 File Offset: 0x000231C0
		internal new MultiValuedProperty<DeliveryRecipientIdParameter> RejectMessagesFromSendersOrMembers
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x0600083A RID: 2106 RVA: 0x00024FC3 File Offset: 0x000231C3
		internal new SwitchParameter RemoveManagedFolderAndPolicy
		{
			get
			{
				return base.RemoveManagedFolderAndPolicy;
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x0600083B RID: 2107 RVA: 0x00024FCB File Offset: 0x000231CB
		internal new SwitchParameter RemovePicture
		{
			get
			{
				return base.RemovePicture;
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x0600083C RID: 2108 RVA: 0x00024FD3 File Offset: 0x000231D3
		internal new SwitchParameter RemoveSpokenName
		{
			get
			{
				return base.RemoveSpokenName;
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x0600083D RID: 2109 RVA: 0x00024FDB File Offset: 0x000231DB
		internal new string SecondaryAddress
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x0600083E RID: 2110 RVA: 0x00024FDE File Offset: 0x000231DE
		internal new UMDialPlanIdParameter SecondaryDialPlan
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x0600083F RID: 2111 RVA: 0x00024FE1 File Offset: 0x000231E1
		internal new ConvertibleMailboxSubType Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000840 RID: 2112 RVA: 0x00024FE9 File Offset: 0x000231E9
		private new string FederatedIdentity
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x00024FEC File Offset: 0x000231EC
		protected override IConfigurable ResolveDataObject()
		{
			ADRecipient adrecipient = (ADRecipient)base.ResolveDataObject();
			if (MailboxTaskHelper.ExcludeArbitrationMailbox(adrecipient, this.Arbitration) || MailboxTaskHelper.ExcludePublicFolderMailbox(adrecipient, this.PublicFolder) || MailboxTaskHelper.ExcludeMailboxPlan(adrecipient, true) || MailboxTaskHelper.ExcludeAuditLogMailbox(adrecipient, base.AuditLog))
			{
				base.WriteError(new ManagementObjectNotFoundException(base.GetErrorMessageObjectNotFound(this.Identity.ToString(), typeof(ADUser).ToString(), (base.DataSession != null) ? base.DataSession.Source : null)), ExchangeErrorCategory.Client, this.Identity);
			}
			return adrecipient;
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x00025094 File Offset: 0x00023294
		protected override void StampChangesOn(IConfigurable dataObject)
		{
			base.StampChangesOn(dataObject);
			if (base.Fields.IsModified("IsDefault"))
			{
				ADUser aduser = (ADUser)dataObject;
				aduser.IsDefault = this.IsDefault;
			}
			if (base.Fields.IsModified("IsDefaultForPreviousVersion"))
			{
				ADUser aduser2 = (ADUser)dataObject;
				aduser2[MailboxPlanSchema.IsDefault_R3] = this.IsDefaultForPreviousVersion;
			}
			if (base.Fields.IsModified("MailboxPlanRelease"))
			{
				MailboxPlanRelease mailboxPlanRelease = (MailboxPlanRelease)((ADUser)dataObject)[MailboxPlanSchema.MailboxPlanRelease];
				MailboxPlanRelease mailboxPlanRelease2 = mailboxPlanRelease;
				if (mailboxPlanRelease2 != MailboxPlanRelease.AllReleases)
				{
					if (mailboxPlanRelease2 != MailboxPlanRelease.CurrentRelease)
					{
						if (mailboxPlanRelease2 == MailboxPlanRelease.NonCurrentRelease)
						{
							if (this.MailboxPlanRelease != MailboxPlanRelease.CurrentRelease)
							{
								((ADUser)dataObject)[MailboxPlanSchema.MailboxPlanRelease] = this.MailboxPlanRelease;
							}
							else
							{
								base.WriteError(new TaskInvalidOperationException(Strings.ErrorMbxPlanReleaseTransition(((ADUser)dataObject).Name, mailboxPlanRelease.ToString(), this.MailboxPlanRelease.ToString())), ExchangeErrorCategory.Client, null);
							}
						}
					}
					else if (this.MailboxPlanRelease != MailboxPlanRelease.NonCurrentRelease)
					{
						((ADUser)dataObject)[MailboxPlanSchema.MailboxPlanRelease] = this.MailboxPlanRelease;
					}
					else
					{
						base.WriteError(new TaskInvalidOperationException(Strings.ErrorMbxPlanReleaseTransition(((ADUser)dataObject).Name, mailboxPlanRelease.ToString(), this.MailboxPlanRelease.ToString())), ExchangeErrorCategory.Client, null);
					}
				}
				else
				{
					((ADUser)dataObject)[MailboxPlanSchema.MailboxPlanRelease] = this.MailboxPlanRelease;
				}
			}
			if (base.ApplyMandatoryProperties)
			{
				ADUser aduser3 = (ADUser)dataObject;
				aduser3.Database = this.DatabasesContainerId;
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000843 RID: 2115 RVA: 0x00025258 File Offset: 0x00023458
		private ADObjectId DatabasesContainerId
		{
			get
			{
				if (this.databasesContainerId == null)
				{
					this.databasesContainerId = base.GlobalConfigSession.GetDatabasesContainerId();
				}
				return this.databasesContainerId;
			}
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x0002527C File Offset: 0x0002347C
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			Mailbox mailbox = (Mailbox)this.GetDynamicParameters();
			if (base.Fields.IsModified("Database"))
			{
				if (this.Database != null)
				{
					MailboxDatabase mailboxDatabase = (MailboxDatabase)base.GetDataObject<MailboxDatabase>(this.Database, base.GlobalConfigSession, null, new LocalizedString?(Strings.ErrorMailboxDatabaseNotFound(this.Database.ToString())), new LocalizedString?(Strings.ErrorMailboxDatabaseNotUnique(this.Database.ToString())), ExchangeErrorCategory.Client);
					mailbox[ADMailboxRecipientSchema.Database] = (ADObjectId)mailboxDatabase.Identity;
				}
				else
				{
					mailbox[ADMailboxRecipientSchema.Database] = null;
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x0002532C File Offset: 0x0002352C
		protected override void InternalProcessRecord()
		{
			ADUser aduser = null;
			if (this.IsDefault)
			{
				aduser = RecipientTaskHelper.ResetOldDefaultPlan((IRecipientSession)base.DataSession, this.DataObject.Id, this.DataObject.OrganizationalUnitRoot, new Task.ErrorLoggerDelegate(base.WriteError));
			}
			bool flag = false;
			try
			{
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
						this.WriteError(exception, ExchangeErrorCategory.ServerTransient, null, false);
					}
				}
			}
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x000253D0 File Offset: 0x000235D0
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			return Microsoft.Exchange.Data.Directory.Management.MailboxPlan.FromDataObject((ADUser)dataObject);
		}

		// Token: 0x040001E2 RID: 482
		private ADObjectId databasesContainerId;
	}
}
