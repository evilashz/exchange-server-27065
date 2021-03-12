using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000AE3 RID: 2787
	[Cmdlet("New", "OnPremisesOrganization", SupportsShouldProcess = true)]
	public sealed class NewOnPremisesOrganization : NewMultitenancySystemConfigurationObjectTask<OnPremisesOrganization>
	{
		// Token: 0x17001E02 RID: 7682
		// (get) Token: 0x060062F7 RID: 25335 RVA: 0x0019DC0B File Offset: 0x0019BE0B
		// (set) Token: 0x060062F8 RID: 25336 RVA: 0x0019DC18 File Offset: 0x0019BE18
		[Parameter(Mandatory = true)]
		public Guid OrganizationGuid
		{
			get
			{
				return this.DataObject.OrganizationGuid;
			}
			set
			{
				this.DataObject.OrganizationGuid = value;
			}
		}

		// Token: 0x17001E03 RID: 7683
		// (get) Token: 0x060062F9 RID: 25337 RVA: 0x0019DC26 File Offset: 0x0019BE26
		// (set) Token: 0x060062FA RID: 25338 RVA: 0x0019DC33 File Offset: 0x0019BE33
		[Parameter(Mandatory = true)]
		public MultiValuedProperty<SmtpDomain> HybridDomains
		{
			get
			{
				return this.DataObject.HybridDomains;
			}
			set
			{
				this.DataObject.HybridDomains = value;
			}
		}

		// Token: 0x17001E04 RID: 7684
		// (get) Token: 0x060062FB RID: 25339 RVA: 0x0019DC41 File Offset: 0x0019BE41
		// (set) Token: 0x060062FC RID: 25340 RVA: 0x0019DC58 File Offset: 0x0019BE58
		[Parameter(Mandatory = true)]
		public InboundConnectorIdParameter InboundConnector
		{
			get
			{
				return (InboundConnectorIdParameter)base.Fields[OnPremisesOrganizationSchema.InboundConnectorLink];
			}
			set
			{
				base.Fields[OnPremisesOrganizationSchema.InboundConnectorLink] = value;
			}
		}

		// Token: 0x17001E05 RID: 7685
		// (get) Token: 0x060062FD RID: 25341 RVA: 0x0019DC6B File Offset: 0x0019BE6B
		// (set) Token: 0x060062FE RID: 25342 RVA: 0x0019DC82 File Offset: 0x0019BE82
		[Parameter(Mandatory = true)]
		public OutboundConnectorIdParameter OutboundConnector
		{
			get
			{
				return (OutboundConnectorIdParameter)base.Fields[OnPremisesOrganizationSchema.OutboundConnectorLink];
			}
			set
			{
				base.Fields[OnPremisesOrganizationSchema.OutboundConnectorLink] = value;
			}
		}

		// Token: 0x17001E06 RID: 7686
		// (get) Token: 0x060062FF RID: 25343 RVA: 0x0019DC95 File Offset: 0x0019BE95
		// (set) Token: 0x06006300 RID: 25344 RVA: 0x0019DCA2 File Offset: 0x0019BEA2
		[Parameter(Mandatory = false)]
		public string OrganizationName
		{
			get
			{
				return this.DataObject.OrganizationName;
			}
			set
			{
				this.DataObject.OrganizationName = value;
			}
		}

		// Token: 0x17001E07 RID: 7687
		// (get) Token: 0x06006301 RID: 25345 RVA: 0x0019DCB0 File Offset: 0x0019BEB0
		// (set) Token: 0x06006302 RID: 25346 RVA: 0x0019DCC7 File Offset: 0x0019BEC7
		[Parameter(Mandatory = false)]
		public OrganizationRelationshipIdParameter OrganizationRelationship
		{
			get
			{
				return (OrganizationRelationshipIdParameter)base.Fields[OnPremisesOrganizationSchema.OrganizationRelationshipLink];
			}
			set
			{
				base.Fields[OnPremisesOrganizationSchema.OrganizationRelationshipLink] = value;
			}
		}

		// Token: 0x06006303 RID: 25347 RVA: 0x0019DCDC File Offset: 0x0019BEDC
		protected override IConfigurable PrepareDataObject()
		{
			OnPremisesOrganization onPremisesOrganization = (OnPremisesOrganization)base.PrepareDataObject();
			onPremisesOrganization.SetId(this.ConfigurationSession, base.Name);
			onPremisesOrganization.InboundConnector = this.GetLinkedDataObject<InboundConnectorIdParameter, TenantInboundConnector>(this.InboundConnector, new Func<InboundConnectorIdParameter, LocalizedString>(Strings.OnPremisesOrganizationInboundConnectorNotExists), new Func<InboundConnectorIdParameter, LocalizedString>(Strings.OnPremisesOrganizationInboundConnectorNotUnique), new Func<string, InboundConnectorIdParameter>(InboundConnectorIdParameter.Parse));
			onPremisesOrganization.OutboundConnector = this.GetLinkedDataObject<OutboundConnectorIdParameter, TenantOutboundConnector>(this.OutboundConnector, new Func<OutboundConnectorIdParameter, LocalizedString>(Strings.OnPremisesOrganizationOutboundConnectorNotExists), new Func<OutboundConnectorIdParameter, LocalizedString>(Strings.OnPremisesOrganizationOutboundConnectorNotUnique), new Func<string, OutboundConnectorIdParameter>(OutboundConnectorIdParameter.Parse));
			onPremisesOrganization.OrganizationRelationship = this.GetLinkedDataObject<OrganizationRelationshipIdParameter, OrganizationRelationship>(this.OrganizationRelationship, new Func<OrganizationRelationshipIdParameter, LocalizedString>(Strings.OnPremisesOrganizationOrganizationRelationshipNotExists), new Func<OrganizationRelationshipIdParameter, LocalizedString>(Strings.OnPremisesOrganizationOrganizationRelationshipNotUnique), new Func<string, OrganizationRelationshipIdParameter>(OrganizationRelationshipIdParameter.Parse));
			return onPremisesOrganization;
		}

		// Token: 0x06006304 RID: 25348 RVA: 0x0019DDAA File Offset: 0x0019BFAA
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.CreateParentContainerIfNeeded(this.DataObject);
			base.InternalProcessRecord();
			TaskLogger.LogExit();
		}

		// Token: 0x06006305 RID: 25349 RVA: 0x0019DDC8 File Offset: 0x0019BFC8
		private ADObjectId GetLinkedDataObject<TLinkedIdentity, TLinkedDataObject>(TLinkedIdentity idParameter, Func<TLinkedIdentity, LocalizedString> notExists, Func<TLinkedIdentity, LocalizedString> notUnique, Func<string, TLinkedIdentity> createId) where TLinkedIdentity : ADIdParameter, new() where TLinkedDataObject : ADObject, new()
		{
			if (idParameter != null)
			{
				if (idParameter.InternalADObjectId == null && !idParameter.RawIdentity.Contains("\\"))
				{
					string arg = base.CurrentOrganizationId.ConfigurationUnit.ToString();
					idParameter = createId(string.Format("{0}\\{1}", arg, idParameter.ToString()));
				}
				TLinkedDataObject tlinkedDataObject = (TLinkedDataObject)((object)base.GetDataObject<TLinkedDataObject>(idParameter, base.GlobalConfigSession, this.RootId, new LocalizedString?(notExists(idParameter)), new LocalizedString?(notUnique(idParameter)), ExchangeErrorCategory.Client));
				return (ADObjectId)tlinkedDataObject.Identity;
			}
			return null;
		}
	}
}
