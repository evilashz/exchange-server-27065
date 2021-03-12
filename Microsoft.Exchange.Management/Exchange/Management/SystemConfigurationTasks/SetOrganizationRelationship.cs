using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020009F8 RID: 2552
	[Cmdlet("Set", "OrganizationRelationship", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetOrganizationRelationship : SetSystemConfigurationObjectTask<OrganizationRelationshipIdParameter, OrganizationRelationship>
	{
		// Token: 0x17001B57 RID: 6999
		// (get) Token: 0x06005B56 RID: 23382 RVA: 0x0017E594 File Offset: 0x0017C794
		// (set) Token: 0x06005B57 RID: 23383 RVA: 0x0017E5AB File Offset: 0x0017C7AB
		[Parameter(ValueFromPipelineByPropertyName = true)]
		public MultiValuedProperty<SmtpDomain> DomainNames
		{
			get
			{
				return (MultiValuedProperty<SmtpDomain>)base.Fields[OrganizationRelationshipSchema.DomainNames];
			}
			set
			{
				base.Fields[OrganizationRelationshipSchema.DomainNames] = value;
			}
		}

		// Token: 0x17001B58 RID: 7000
		// (get) Token: 0x06005B58 RID: 23384 RVA: 0x0017E5BE File Offset: 0x0017C7BE
		// (set) Token: 0x06005B59 RID: 23385 RVA: 0x0017E5D5 File Offset: 0x0017C7D5
		[Parameter]
		public bool FreeBusyAccessEnabled
		{
			get
			{
				return (bool)base.Fields[OrganizationRelationshipSchema.FreeBusyAccessEnabled];
			}
			set
			{
				base.Fields[OrganizationRelationshipSchema.FreeBusyAccessEnabled] = value;
			}
		}

		// Token: 0x17001B59 RID: 7001
		// (get) Token: 0x06005B5A RID: 23386 RVA: 0x0017E5ED File Offset: 0x0017C7ED
		// (set) Token: 0x06005B5B RID: 23387 RVA: 0x0017E604 File Offset: 0x0017C804
		[Parameter]
		public FreeBusyAccessLevel FreeBusyAccessLevel
		{
			get
			{
				return (FreeBusyAccessLevel)base.Fields[OrganizationRelationshipSchema.FreeBusyAccessLevel];
			}
			set
			{
				base.Fields[OrganizationRelationshipSchema.FreeBusyAccessLevel] = value;
			}
		}

		// Token: 0x17001B5A RID: 7002
		// (get) Token: 0x06005B5C RID: 23388 RVA: 0x0017E61C File Offset: 0x0017C81C
		// (set) Token: 0x06005B5D RID: 23389 RVA: 0x0017E633 File Offset: 0x0017C833
		[Parameter]
		public GroupIdParameter FreeBusyAccessScope
		{
			get
			{
				return (GroupIdParameter)base.Fields["FreeBusyAccessScope"];
			}
			set
			{
				base.Fields["FreeBusyAccessScope"] = value;
			}
		}

		// Token: 0x17001B5B RID: 7003
		// (get) Token: 0x06005B5E RID: 23390 RVA: 0x0017E646 File Offset: 0x0017C846
		// (set) Token: 0x06005B5F RID: 23391 RVA: 0x0017E65D File Offset: 0x0017C85D
		[Parameter]
		public bool MailboxMoveEnabled
		{
			get
			{
				return (bool)base.Fields[OrganizationRelationshipSchema.MailboxMoveEnabled];
			}
			set
			{
				base.Fields[OrganizationRelationshipSchema.MailboxMoveEnabled] = value;
			}
		}

		// Token: 0x17001B5C RID: 7004
		// (get) Token: 0x06005B60 RID: 23392 RVA: 0x0017E675 File Offset: 0x0017C875
		// (set) Token: 0x06005B61 RID: 23393 RVA: 0x0017E68C File Offset: 0x0017C88C
		[Parameter]
		public bool DeliveryReportEnabled
		{
			get
			{
				return (bool)base.Fields[OrganizationRelationshipSchema.DeliveryReportEnabled];
			}
			set
			{
				base.Fields[OrganizationRelationshipSchema.DeliveryReportEnabled] = value;
			}
		}

		// Token: 0x17001B5D RID: 7005
		// (get) Token: 0x06005B62 RID: 23394 RVA: 0x0017E6A4 File Offset: 0x0017C8A4
		// (set) Token: 0x06005B63 RID: 23395 RVA: 0x0017E6BB File Offset: 0x0017C8BB
		[Parameter]
		public bool MailTipsAccessEnabled
		{
			get
			{
				return (bool)base.Fields[OrganizationRelationshipSchema.MailTipsAccessEnabled];
			}
			set
			{
				base.Fields[OrganizationRelationshipSchema.MailTipsAccessEnabled] = value;
			}
		}

		// Token: 0x17001B5E RID: 7006
		// (get) Token: 0x06005B64 RID: 23396 RVA: 0x0017E6D3 File Offset: 0x0017C8D3
		// (set) Token: 0x06005B65 RID: 23397 RVA: 0x0017E6EA File Offset: 0x0017C8EA
		[Parameter]
		public MailTipsAccessLevel MailTipsAccessLevel
		{
			get
			{
				return (MailTipsAccessLevel)base.Fields[OrganizationRelationshipSchema.MailTipsAccessLevel];
			}
			set
			{
				base.Fields[OrganizationRelationshipSchema.MailTipsAccessLevel] = value;
			}
		}

		// Token: 0x17001B5F RID: 7007
		// (get) Token: 0x06005B66 RID: 23398 RVA: 0x0017E702 File Offset: 0x0017C902
		// (set) Token: 0x06005B67 RID: 23399 RVA: 0x0017E719 File Offset: 0x0017C919
		[Parameter]
		public GroupIdParameter MailTipsAccessScope
		{
			get
			{
				return (GroupIdParameter)base.Fields["MailTipsAccessScope"];
			}
			set
			{
				base.Fields["MailTipsAccessScope"] = value;
			}
		}

		// Token: 0x17001B60 RID: 7008
		// (get) Token: 0x06005B68 RID: 23400 RVA: 0x0017E72C File Offset: 0x0017C92C
		// (set) Token: 0x06005B69 RID: 23401 RVA: 0x0017E743 File Offset: 0x0017C943
		[Parameter]
		public bool ArchiveAccessEnabled
		{
			get
			{
				return (bool)base.Fields[OrganizationRelationshipSchema.ArchiveAccessEnabled];
			}
			set
			{
				base.Fields[OrganizationRelationshipSchema.ArchiveAccessEnabled] = value;
			}
		}

		// Token: 0x17001B61 RID: 7009
		// (get) Token: 0x06005B6A RID: 23402 RVA: 0x0017E75B File Offset: 0x0017C95B
		// (set) Token: 0x06005B6B RID: 23403 RVA: 0x0017E772 File Offset: 0x0017C972
		[Parameter]
		public bool PhotosEnabled
		{
			get
			{
				return (bool)base.Fields[OrganizationRelationshipSchema.PhotosEnabled];
			}
			set
			{
				base.Fields[OrganizationRelationshipSchema.PhotosEnabled] = value;
			}
		}

		// Token: 0x17001B62 RID: 7010
		// (get) Token: 0x06005B6C RID: 23404 RVA: 0x0017E78A File Offset: 0x0017C98A
		// (set) Token: 0x06005B6D RID: 23405 RVA: 0x0017E7A1 File Offset: 0x0017C9A1
		[Parameter(ValueFromPipelineByPropertyName = true)]
		public Uri TargetApplicationUri
		{
			get
			{
				return (Uri)base.Fields[OrganizationRelationshipSchema.TargetApplicationUri];
			}
			set
			{
				base.Fields[OrganizationRelationshipSchema.TargetApplicationUri] = value;
			}
		}

		// Token: 0x17001B63 RID: 7011
		// (get) Token: 0x06005B6E RID: 23406 RVA: 0x0017E7B4 File Offset: 0x0017C9B4
		// (set) Token: 0x06005B6F RID: 23407 RVA: 0x0017E7CB File Offset: 0x0017C9CB
		[Parameter]
		public Uri TargetSharingEpr
		{
			get
			{
				return (Uri)base.Fields[OrganizationRelationshipSchema.TargetSharingEpr];
			}
			set
			{
				base.Fields[OrganizationRelationshipSchema.TargetSharingEpr] = value;
			}
		}

		// Token: 0x17001B64 RID: 7012
		// (get) Token: 0x06005B70 RID: 23408 RVA: 0x0017E7DE File Offset: 0x0017C9DE
		// (set) Token: 0x06005B71 RID: 23409 RVA: 0x0017E7F5 File Offset: 0x0017C9F5
		[Parameter(ValueFromPipelineByPropertyName = true)]
		public Uri TargetAutodiscoverEpr
		{
			get
			{
				return (Uri)base.Fields[OrganizationRelationshipSchema.TargetAutodiscoverEpr];
			}
			set
			{
				base.Fields[OrganizationRelationshipSchema.TargetAutodiscoverEpr] = value;
			}
		}

		// Token: 0x17001B65 RID: 7013
		// (get) Token: 0x06005B72 RID: 23410 RVA: 0x0017E808 File Offset: 0x0017CA08
		// (set) Token: 0x06005B73 RID: 23411 RVA: 0x0017E81F File Offset: 0x0017CA1F
		[Parameter(ValueFromPipelineByPropertyName = true)]
		public Uri TargetOwaURL
		{
			get
			{
				return (Uri)base.Fields[OrganizationRelationshipSchema.TargetOwaURL];
			}
			set
			{
				base.Fields[OrganizationRelationshipSchema.TargetOwaURL] = value;
			}
		}

		// Token: 0x17001B66 RID: 7014
		// (get) Token: 0x06005B74 RID: 23412 RVA: 0x0017E832 File Offset: 0x0017CA32
		// (set) Token: 0x06005B75 RID: 23413 RVA: 0x0017E849 File Offset: 0x0017CA49
		[Parameter]
		public SmtpAddress OrganizationContact
		{
			get
			{
				return (SmtpAddress)base.Fields[OrganizationRelationshipSchema.OrganizationContact];
			}
			set
			{
				base.Fields[OrganizationRelationshipSchema.OrganizationContact] = value;
			}
		}

		// Token: 0x17001B67 RID: 7015
		// (get) Token: 0x06005B76 RID: 23414 RVA: 0x0017E861 File Offset: 0x0017CA61
		// (set) Token: 0x06005B77 RID: 23415 RVA: 0x0017E878 File Offset: 0x0017CA78
		[Parameter]
		public bool Enabled
		{
			get
			{
				return (bool)base.Fields[OrganizationRelationshipSchema.Enabled];
			}
			set
			{
				base.Fields[OrganizationRelationshipSchema.Enabled] = value;
			}
		}

		// Token: 0x17001B68 RID: 7016
		// (get) Token: 0x06005B78 RID: 23416 RVA: 0x0017E890 File Offset: 0x0017CA90
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetOrganizationRelationship(this.Identity.ToString());
			}
		}

		// Token: 0x17001B69 RID: 7017
		// (get) Token: 0x06005B79 RID: 23417 RVA: 0x0017E8A2 File Offset: 0x0017CAA2
		// (set) Token: 0x06005B7A RID: 23418 RVA: 0x0017E8AA File Offset: 0x0017CAAA
		[Parameter(Mandatory = false)]
		public SwitchParameter Force { get; set; }

		// Token: 0x06005B7B RID: 23419 RVA: 0x0017E8B4 File Offset: 0x0017CAB4
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			foreach (ADPropertyDefinition adpropertyDefinition in SetOrganizationRelationship.setProperties)
			{
				if (base.Fields.IsModified(adpropertyDefinition))
				{
					this.DataObject[adpropertyDefinition] = base.Fields[adpropertyDefinition];
				}
			}
			if (NewOrganizationRelationship.DomainsExist(this.DataObject.DomainNames, this.ConfigurationSession, new Guid?(this.DataObject.Guid)))
			{
				base.WriteError(new DuplicateOrganizationRelationshipDomainException(base.FormatMultiValuedProperty(this.DataObject.DomainNames)), ErrorCategory.InvalidOperation, this.Identity);
			}
			if (base.Fields.IsModified("FreeBusyAccessScope"))
			{
				if (this.FreeBusyAccessScope != null)
				{
					ADGroup adgroup = (ADGroup)base.GetDataObject<ADGroup>(this.FreeBusyAccessScope, base.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.ErrorRecipientNotFound(this.FreeBusyAccessScope.ToString())), new LocalizedString?(Strings.ErrorRecipientNotUnique(this.FreeBusyAccessScope.ToString())));
					this.DataObject.FreeBusyAccessScope = adgroup.Id;
				}
				else
				{
					this.DataObject.FreeBusyAccessScope = null;
				}
			}
			if (base.Fields.IsModified("MailTipsAccessScope"))
			{
				if (this.MailTipsAccessScope != null)
				{
					ADGroup adgroup2 = (ADGroup)base.GetDataObject<ADGroup>(this.MailTipsAccessScope, base.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.ErrorRecipientNotFound(this.MailTipsAccessScope.ToString())), new LocalizedString?(Strings.ErrorRecipientNotUnique(this.MailTipsAccessScope.ToString())));
					this.DataObject.MailTipsAccessScope = adgroup2.Id;
				}
				else
				{
					this.DataObject.MailTipsAccessScope = null;
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06005B7C RID: 23420 RVA: 0x0017EA5C File Offset: 0x0017CC5C
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (!this.Force && this.DataObject.IsChanged(OrganizationRelationshipSchema.Enabled) && !base.ShouldContinue(this.DataObject.Enabled ? Strings.ConfirmationMessageEnableOrganizationRelationship(this.Identity.ToString()) : Strings.ConfirmationMessageDisableOrganizationRelationship(this.Identity.ToString())))
			{
				TaskLogger.LogExit();
				return;
			}
			base.InternalProcessRecord();
			TaskLogger.LogExit();
		}

		// Token: 0x040033F6 RID: 13302
		private static readonly PropertyDefinition[] setProperties = new PropertyDefinition[]
		{
			OrganizationRelationshipSchema.DomainNames,
			OrganizationRelationshipSchema.FreeBusyAccessEnabled,
			OrganizationRelationshipSchema.FreeBusyAccessLevel,
			OrganizationRelationshipSchema.MailboxMoveEnabled,
			OrganizationRelationshipSchema.DeliveryReportEnabled,
			OrganizationRelationshipSchema.MailTipsAccessEnabled,
			OrganizationRelationshipSchema.MailTipsAccessLevel,
			OrganizationRelationshipSchema.TargetApplicationUri,
			OrganizationRelationshipSchema.TargetOwaURL,
			OrganizationRelationshipSchema.TargetSharingEpr,
			OrganizationRelationshipSchema.TargetAutodiscoverEpr,
			OrganizationRelationshipSchema.OrganizationContact,
			OrganizationRelationshipSchema.ArchiveAccessEnabled,
			OrganizationRelationshipSchema.PhotosEnabled,
			OrganizationRelationshipSchema.Enabled
		};
	}
}
