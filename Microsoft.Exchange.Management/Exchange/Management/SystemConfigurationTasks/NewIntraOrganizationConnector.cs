using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A14 RID: 2580
	[Cmdlet("New", "IntraOrganizationConnector", SupportsShouldProcess = true)]
	public sealed class NewIntraOrganizationConnector : NewMultitenancySystemConfigurationObjectTask<IntraOrganizationConnector>
	{
		// Token: 0x17001BBF RID: 7103
		// (get) Token: 0x06005C8F RID: 23695 RVA: 0x001861BD File Offset: 0x001843BD
		// (set) Token: 0x06005C90 RID: 23696 RVA: 0x001861CA File Offset: 0x001843CA
		[Parameter(Mandatory = true)]
		public MultiValuedProperty<SmtpDomain> TargetAddressDomains
		{
			get
			{
				return this.DataObject.TargetAddressDomains;
			}
			set
			{
				this.DataObject.TargetAddressDomains = value;
			}
		}

		// Token: 0x17001BC0 RID: 7104
		// (get) Token: 0x06005C91 RID: 23697 RVA: 0x001861D8 File Offset: 0x001843D8
		// (set) Token: 0x06005C92 RID: 23698 RVA: 0x001861E5 File Offset: 0x001843E5
		[Parameter(Mandatory = true)]
		public Uri DiscoveryEndpoint
		{
			get
			{
				return this.DataObject.DiscoveryEndpoint;
			}
			set
			{
				this.DataObject.DiscoveryEndpoint = value;
			}
		}

		// Token: 0x17001BC1 RID: 7105
		// (get) Token: 0x06005C93 RID: 23699 RVA: 0x001861F3 File Offset: 0x001843F3
		// (set) Token: 0x06005C94 RID: 23700 RVA: 0x00186200 File Offset: 0x00184400
		[Parameter]
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

		// Token: 0x17001BC2 RID: 7106
		// (get) Token: 0x06005C95 RID: 23701 RVA: 0x0018620E File Offset: 0x0018440E
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewIntraOrganizationConnector(base.Name, base.FormatMultiValuedProperty(this.TargetAddressDomains));
			}
		}

		// Token: 0x06005C96 RID: 23702 RVA: 0x00186228 File Offset: 0x00184428
		internal static bool DomainExists(MultiValuedProperty<SmtpDomain> domains, IConfigurationSession configurationSession)
		{
			return NewIntraOrganizationConnector.DomainExists(domains, configurationSession, null);
		}

		// Token: 0x06005C97 RID: 23703 RVA: 0x00186248 File Offset: 0x00184448
		internal static bool DomainExists(MultiValuedProperty<SmtpDomain> domains, IConfigurationSession configurationSession, Guid? objectToExclude)
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
			IntraOrganizationConnector[] array = configurationSession.Find<IntraOrganizationConnector>(configurationSession.GetOrgContainerId(), QueryScope.SubTree, queryFilter, null, 1);
			return array.Length > 0;
		}

		// Token: 0x06005C98 RID: 23704 RVA: 0x00186324 File Offset: 0x00184524
		protected override IConfigurable PrepareDataObject()
		{
			ADObjectId containerId = IntraOrganizationConnector.GetContainerId(this.ConfigurationSession);
			if (this.ConfigurationSession.Read<Container>(containerId) == null)
			{
				IConfigurationSession configurationSession = (IConfigurationSession)base.DataSession;
				OrganizationId currentOrganizationId = this.ConfigurationSession.SessionSettings.CurrentOrganizationId;
				Container container = new Container();
				container.OrganizationId = currentOrganizationId;
				container.SetId(containerId);
				configurationSession.Save(container);
			}
			IntraOrganizationConnector intraOrganizationConnector = (IntraOrganizationConnector)base.PrepareDataObject();
			intraOrganizationConnector.SetId((IConfigurationSession)base.DataSession, base.Name);
			return intraOrganizationConnector;
		}

		// Token: 0x06005C99 RID: 23705 RVA: 0x001863AC File Offset: 0x001845AC
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (NewIntraOrganizationConnector.DomainExists(this.DataObject.TargetAddressDomains, this.ConfigurationSession))
			{
				base.WriteError(new DuplicateIntraOrganizationConnectorDomainException(base.FormatMultiValuedProperty(this.DataObject.TargetAddressDomains)), ErrorCategory.InvalidOperation, base.Name);
			}
			TaskLogger.LogExit();
		}
	}
}
