using System;
using System.Collections.Generic;
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
	// Token: 0x020009EF RID: 2543
	[Cmdlet("New", "OrganizationRelationship", SupportsShouldProcess = true)]
	public sealed class NewOrganizationRelationship : NewMultitenancySystemConfigurationObjectTask<OrganizationRelationship>
	{
		// Token: 0x17001B21 RID: 6945
		// (get) Token: 0x06005ACD RID: 23245 RVA: 0x0017C6B0 File Offset: 0x0017A8B0
		// (set) Token: 0x06005ACE RID: 23246 RVA: 0x0017C6BD File Offset: 0x0017A8BD
		[Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true)]
		public MultiValuedProperty<SmtpDomain> DomainNames
		{
			get
			{
				return this.DataObject.DomainNames;
			}
			set
			{
				this.DataObject.DomainNames = value;
			}
		}

		// Token: 0x17001B22 RID: 6946
		// (get) Token: 0x06005ACF RID: 23247 RVA: 0x0017C6CB File Offset: 0x0017A8CB
		// (set) Token: 0x06005AD0 RID: 23248 RVA: 0x0017C6D8 File Offset: 0x0017A8D8
		[Parameter(Mandatory = false)]
		public bool FreeBusyAccessEnabled
		{
			get
			{
				return this.DataObject.FreeBusyAccessEnabled;
			}
			set
			{
				this.DataObject.FreeBusyAccessEnabled = value;
			}
		}

		// Token: 0x17001B23 RID: 6947
		// (get) Token: 0x06005AD1 RID: 23249 RVA: 0x0017C6E6 File Offset: 0x0017A8E6
		// (set) Token: 0x06005AD2 RID: 23250 RVA: 0x0017C6F3 File Offset: 0x0017A8F3
		[Parameter(Mandatory = false)]
		public FreeBusyAccessLevel FreeBusyAccessLevel
		{
			get
			{
				return this.DataObject.FreeBusyAccessLevel;
			}
			set
			{
				this.DataObject.FreeBusyAccessLevel = value;
			}
		}

		// Token: 0x17001B24 RID: 6948
		// (get) Token: 0x06005AD3 RID: 23251 RVA: 0x0017C701 File Offset: 0x0017A901
		// (set) Token: 0x06005AD4 RID: 23252 RVA: 0x0017C718 File Offset: 0x0017A918
		[Parameter(Mandatory = false)]
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

		// Token: 0x17001B25 RID: 6949
		// (get) Token: 0x06005AD5 RID: 23253 RVA: 0x0017C72B File Offset: 0x0017A92B
		// (set) Token: 0x06005AD6 RID: 23254 RVA: 0x0017C738 File Offset: 0x0017A938
		[Parameter(Mandatory = false)]
		public bool MailboxMoveEnabled
		{
			get
			{
				return this.DataObject.MailboxMoveEnabled;
			}
			set
			{
				this.DataObject.MailboxMoveEnabled = value;
			}
		}

		// Token: 0x17001B26 RID: 6950
		// (get) Token: 0x06005AD7 RID: 23255 RVA: 0x0017C746 File Offset: 0x0017A946
		// (set) Token: 0x06005AD8 RID: 23256 RVA: 0x0017C753 File Offset: 0x0017A953
		[Parameter(Mandatory = false)]
		public bool DeliveryReportEnabled
		{
			get
			{
				return this.DataObject.DeliveryReportEnabled;
			}
			set
			{
				this.DataObject.DeliveryReportEnabled = value;
			}
		}

		// Token: 0x17001B27 RID: 6951
		// (get) Token: 0x06005AD9 RID: 23257 RVA: 0x0017C761 File Offset: 0x0017A961
		// (set) Token: 0x06005ADA RID: 23258 RVA: 0x0017C76E File Offset: 0x0017A96E
		[Parameter(Mandatory = false)]
		public bool MailTipsAccessEnabled
		{
			get
			{
				return this.DataObject.MailTipsAccessEnabled;
			}
			set
			{
				this.DataObject.MailTipsAccessEnabled = value;
			}
		}

		// Token: 0x17001B28 RID: 6952
		// (get) Token: 0x06005ADB RID: 23259 RVA: 0x0017C77C File Offset: 0x0017A97C
		// (set) Token: 0x06005ADC RID: 23260 RVA: 0x0017C789 File Offset: 0x0017A989
		[Parameter(Mandatory = false)]
		public MailTipsAccessLevel MailTipsAccessLevel
		{
			get
			{
				return this.DataObject.MailTipsAccessLevel;
			}
			set
			{
				this.DataObject.MailTipsAccessLevel = value;
			}
		}

		// Token: 0x17001B29 RID: 6953
		// (get) Token: 0x06005ADD RID: 23261 RVA: 0x0017C797 File Offset: 0x0017A997
		// (set) Token: 0x06005ADE RID: 23262 RVA: 0x0017C7AE File Offset: 0x0017A9AE
		[Parameter(Mandatory = false)]
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

		// Token: 0x17001B2A RID: 6954
		// (get) Token: 0x06005ADF RID: 23263 RVA: 0x0017C7C1 File Offset: 0x0017A9C1
		// (set) Token: 0x06005AE0 RID: 23264 RVA: 0x0017C7CE File Offset: 0x0017A9CE
		[Parameter(Mandatory = false)]
		public bool ArchiveAccessEnabled
		{
			get
			{
				return this.DataObject.ArchiveAccessEnabled;
			}
			set
			{
				this.DataObject.ArchiveAccessEnabled = value;
			}
		}

		// Token: 0x17001B2B RID: 6955
		// (get) Token: 0x06005AE1 RID: 23265 RVA: 0x0017C7DC File Offset: 0x0017A9DC
		// (set) Token: 0x06005AE2 RID: 23266 RVA: 0x0017C7E9 File Offset: 0x0017A9E9
		[Parameter(Mandatory = false)]
		public bool PhotosEnabled
		{
			get
			{
				return this.DataObject.PhotosEnabled;
			}
			set
			{
				this.DataObject.PhotosEnabled = value;
			}
		}

		// Token: 0x17001B2C RID: 6956
		// (get) Token: 0x06005AE3 RID: 23267 RVA: 0x0017C7F7 File Offset: 0x0017A9F7
		// (set) Token: 0x06005AE4 RID: 23268 RVA: 0x0017C804 File Offset: 0x0017AA04
		[Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true)]
		public Uri TargetApplicationUri
		{
			get
			{
				return this.DataObject.TargetApplicationUri;
			}
			set
			{
				this.DataObject.TargetApplicationUri = value;
			}
		}

		// Token: 0x17001B2D RID: 6957
		// (get) Token: 0x06005AE5 RID: 23269 RVA: 0x0017C812 File Offset: 0x0017AA12
		// (set) Token: 0x06005AE6 RID: 23270 RVA: 0x0017C81F File Offset: 0x0017AA1F
		[Parameter(Mandatory = false)]
		public Uri TargetSharingEpr
		{
			get
			{
				return this.DataObject.TargetSharingEpr;
			}
			set
			{
				this.DataObject.TargetSharingEpr = value;
			}
		}

		// Token: 0x17001B2E RID: 6958
		// (get) Token: 0x06005AE7 RID: 23271 RVA: 0x0017C82D File Offset: 0x0017AA2D
		// (set) Token: 0x06005AE8 RID: 23272 RVA: 0x0017C83A File Offset: 0x0017AA3A
		[Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true)]
		public Uri TargetAutodiscoverEpr
		{
			get
			{
				return this.DataObject.TargetAutodiscoverEpr;
			}
			set
			{
				this.DataObject.TargetAutodiscoverEpr = value;
			}
		}

		// Token: 0x17001B2F RID: 6959
		// (get) Token: 0x06005AE9 RID: 23273 RVA: 0x0017C848 File Offset: 0x0017AA48
		// (set) Token: 0x06005AEA RID: 23274 RVA: 0x0017C855 File Offset: 0x0017AA55
		[Parameter(Mandatory = false)]
		public SmtpAddress OrganizationContact
		{
			get
			{
				return this.DataObject.OrganizationContact;
			}
			set
			{
				this.DataObject.OrganizationContact = value;
			}
		}

		// Token: 0x17001B30 RID: 6960
		// (get) Token: 0x06005AEB RID: 23275 RVA: 0x0017C863 File Offset: 0x0017AA63
		// (set) Token: 0x06005AEC RID: 23276 RVA: 0x0017C870 File Offset: 0x0017AA70
		[Parameter(Mandatory = false)]
		public bool Enabled
		{
			get
			{
				return this.DataObject.Enabled;
			}
			set
			{
				this.DataObject.Enabled = value;
			}
		}

		// Token: 0x17001B31 RID: 6961
		// (get) Token: 0x06005AED RID: 23277 RVA: 0x0017C87E File Offset: 0x0017AA7E
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewOrganizationRelationship(base.Name, base.FormatMultiValuedProperty(this.DomainNames));
			}
		}

		// Token: 0x17001B32 RID: 6962
		// (get) Token: 0x06005AEE RID: 23278 RVA: 0x0017C897 File Offset: 0x0017AA97
		// (set) Token: 0x06005AEF RID: 23279 RVA: 0x0017C8A4 File Offset: 0x0017AAA4
		[Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true)]
		public Uri TargetOwaURL
		{
			get
			{
				return this.DataObject.TargetOwaURL;
			}
			set
			{
				this.DataObject.TargetOwaURL = value;
			}
		}

		// Token: 0x06005AF0 RID: 23280 RVA: 0x0017C8B4 File Offset: 0x0017AAB4
		internal static bool DomainsExist(MultiValuedProperty<SmtpDomain> domains, IConfigurationSession configurationSession)
		{
			return NewOrganizationRelationship.DomainsExist(domains, configurationSession, null);
		}

		// Token: 0x06005AF1 RID: 23281 RVA: 0x0017C8D4 File Offset: 0x0017AAD4
		internal static bool DomainsExist(MultiValuedProperty<SmtpDomain> domains, IConfigurationSession configurationSession, Guid? objectToExclude)
		{
			List<ComparisonFilter> list = new List<ComparisonFilter>(domains.Count);
			foreach (SmtpDomain smtpDomain in domains)
			{
				list.Add(new ComparisonFilter(ComparisonOperator.Equal, OrganizationRelationshipSchema.DomainNames, smtpDomain.Domain));
			}
			QueryFilter queryFilter;
			if (list.Count == 1)
			{
				queryFilter = list[0];
			}
			else
			{
				queryFilter = new OrFilter(list.ToArray());
			}
			if (objectToExclude != null)
			{
				queryFilter = new AndFilter(new QueryFilter[]
				{
					new ComparisonFilter(ComparisonOperator.NotEqual, ADObjectSchema.Guid, objectToExclude.Value),
					queryFilter
				});
			}
			OrganizationRelationship[] array = configurationSession.Find<OrganizationRelationship>(configurationSession.GetOrgContainerId(), QueryScope.SubTree, queryFilter, null, 1);
			return array.Length > 0;
		}

		// Token: 0x06005AF2 RID: 23282 RVA: 0x0017C9B0 File Offset: 0x0017ABB0
		protected override IConfigurable PrepareDataObject()
		{
			OrganizationRelationship organizationRelationship = (OrganizationRelationship)base.PrepareDataObject();
			organizationRelationship.SetId((IConfigurationSession)base.DataSession, base.Name);
			return organizationRelationship;
		}

		// Token: 0x06005AF3 RID: 23283 RVA: 0x0017C9E1 File Offset: 0x0017ABE1
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			TaskLogger.LogExit();
		}

		// Token: 0x06005AF4 RID: 23284 RVA: 0x0017C9F4 File Offset: 0x0017ABF4
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			SharedConfigurationTaskHelper.VerifyIsNotTinyTenant(base.CurrentOrgState, new Task.ErrorLoggerDelegate(base.WriteError));
			base.InternalValidate();
			if (NewOrganizationRelationship.DomainsExist(this.DataObject.DomainNames, this.ConfigurationSession))
			{
				base.WriteError(new DuplicateOrganizationRelationshipDomainException(base.FormatMultiValuedProperty(this.DataObject.DomainNames)), ErrorCategory.InvalidOperation, base.Name);
			}
			if (this.FreeBusyAccessScopeADGroup != null)
			{
				this.DataObject.FreeBusyAccessScope = this.FreeBusyAccessScopeADGroup.Id;
			}
			if (this.MailTipsAccessScopeADGroup != null)
			{
				this.DataObject.MailTipsAccessScope = this.MailTipsAccessScopeADGroup.Id;
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06005AF5 RID: 23285 RVA: 0x0017CAA0 File Offset: 0x0017ACA0
		protected override void WriteResult(IConfigurable dataObject)
		{
			TaskLogger.LogEnter();
			if (this.FreeBusyAccessScopeADGroup != null || this.MailTipsAccessScopeADGroup != null)
			{
				OrganizationRelationship organizationRelationship = (OrganizationRelationship)dataObject;
				if (this.FreeBusyAccessScopeADGroup != null)
				{
					organizationRelationship[OrganizationRelationshipNonAdProperties.FreeBusyAccessScopeCache] = this.FreeBusyAccessScopeADGroup.Id;
				}
				if (this.MailTipsAccessScopeADGroup != null)
				{
					organizationRelationship[OrganizationRelationshipNonAdProperties.MailTipsAccessScopeScopeCache] = this.MailTipsAccessScopeADGroup.Id;
				}
			}
			base.WriteResult(dataObject);
			TaskLogger.LogExit();
		}

		// Token: 0x17001B33 RID: 6963
		// (get) Token: 0x06005AF6 RID: 23286 RVA: 0x0017CB14 File Offset: 0x0017AD14
		private ADGroup FreeBusyAccessScopeADGroup
		{
			get
			{
				if (this.freeBusyAccessScopeAdGroup == null && this.FreeBusyAccessScope != null)
				{
					this.freeBusyAccessScopeAdGroup = (ADGroup)base.GetDataObject<ADGroup>(this.FreeBusyAccessScope, base.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.ErrorRecipientNotFound(this.FreeBusyAccessScope.ToString())), new LocalizedString?(Strings.ErrorRecipientNotUnique(this.FreeBusyAccessScope.ToString())));
				}
				return this.freeBusyAccessScopeAdGroup;
			}
		}

		// Token: 0x17001B34 RID: 6964
		// (get) Token: 0x06005AF7 RID: 23287 RVA: 0x0017CB80 File Offset: 0x0017AD80
		private ADGroup MailTipsAccessScopeADGroup
		{
			get
			{
				if (this.mailTipsAccessScopeAdGroup == null && this.MailTipsAccessScope != null)
				{
					this.mailTipsAccessScopeAdGroup = (ADGroup)base.GetDataObject<ADGroup>(this.MailTipsAccessScope, base.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.ErrorRecipientNotFound(this.MailTipsAccessScope.ToString())), new LocalizedString?(Strings.ErrorRecipientNotUnique(this.MailTipsAccessScope.ToString())));
				}
				return this.mailTipsAccessScopeAdGroup;
			}
		}

		// Token: 0x040033DE RID: 13278
		internal const string MailTipsAccessScopeFieldName = "MailTipsAccessScope";

		// Token: 0x040033DF RID: 13279
		private ADGroup freeBusyAccessScopeAdGroup;

		// Token: 0x040033E0 RID: 13280
		private ADGroup mailTipsAccessScopeAdGroup;
	}
}
