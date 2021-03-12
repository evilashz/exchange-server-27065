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
	// Token: 0x02000AE5 RID: 2789
	[Cmdlet("Set", "OnPremisesOrganization", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetOnPremisesOrganization : SetSystemConfigurationObjectTask<OnPremisesOrganizationIdParameter, OnPremisesOrganization>
	{
		// Token: 0x17001E09 RID: 7689
		// (get) Token: 0x06006309 RID: 25353 RVA: 0x0019DEA6 File Offset: 0x0019C0A6
		// (set) Token: 0x0600630A RID: 25354 RVA: 0x0019DEBD File Offset: 0x0019C0BD
		[Parameter(Mandatory = false)]
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

		// Token: 0x17001E0A RID: 7690
		// (get) Token: 0x0600630B RID: 25355 RVA: 0x0019DED0 File Offset: 0x0019C0D0
		// (set) Token: 0x0600630C RID: 25356 RVA: 0x0019DEE7 File Offset: 0x0019C0E7
		[Parameter(Mandatory = false)]
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

		// Token: 0x17001E0B RID: 7691
		// (get) Token: 0x0600630D RID: 25357 RVA: 0x0019DEFA File Offset: 0x0019C0FA
		// (set) Token: 0x0600630E RID: 25358 RVA: 0x0019DF11 File Offset: 0x0019C111
		[Parameter(Mandatory = false)]
		public string OrganizationName
		{
			get
			{
				return (string)base.Fields[OnPremisesOrganizationSchema.OrganizationName];
			}
			set
			{
				base.Fields[OnPremisesOrganizationSchema.OrganizationName] = value;
			}
		}

		// Token: 0x17001E0C RID: 7692
		// (get) Token: 0x0600630F RID: 25359 RVA: 0x0019DF24 File Offset: 0x0019C124
		// (set) Token: 0x06006310 RID: 25360 RVA: 0x0019DF3B File Offset: 0x0019C13B
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

		// Token: 0x06006311 RID: 25361 RVA: 0x0019DF50 File Offset: 0x0019C150
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			OnPremisesOrganization onPremisesOrganization = (OnPremisesOrganization)this.GetDynamicParameters();
			if (base.Fields.IsModified(OnPremisesOrganizationSchema.InboundConnectorLink))
			{
				onPremisesOrganization.InboundConnector = this.GetLinkedDataObject<InboundConnectorIdParameter, TenantInboundConnector>(this.InboundConnector, new Func<InboundConnectorIdParameter, LocalizedString>(Strings.OnPremisesOrganizationInboundConnectorNotExists), new Func<InboundConnectorIdParameter, LocalizedString>(Strings.OnPremisesOrganizationInboundConnectorNotUnique), new Func<string, InboundConnectorIdParameter>(InboundConnectorIdParameter.Parse));
			}
			if (base.Fields.IsModified(OnPremisesOrganizationSchema.OutboundConnectorLink))
			{
				onPremisesOrganization.OutboundConnector = this.GetLinkedDataObject<OutboundConnectorIdParameter, TenantOutboundConnector>(this.OutboundConnector, new Func<OutboundConnectorIdParameter, LocalizedString>(Strings.OnPremisesOrganizationOutboundConnectorNotExists), new Func<OutboundConnectorIdParameter, LocalizedString>(Strings.OnPremisesOrganizationOutboundConnectorNotUnique), new Func<string, OutboundConnectorIdParameter>(OutboundConnectorIdParameter.Parse));
			}
			if (base.Fields.IsModified(OnPremisesOrganizationSchema.OrganizationName))
			{
				onPremisesOrganization.OrganizationName = (string)base.Fields[OnPremisesOrganizationSchema.OrganizationName];
			}
			if (base.Fields.IsModified(OnPremisesOrganizationSchema.OrganizationRelationshipLink))
			{
				onPremisesOrganization.OrganizationRelationship = this.GetLinkedDataObject<OrganizationRelationshipIdParameter, OrganizationRelationship>(this.OrganizationRelationship, new Func<OrganizationRelationshipIdParameter, LocalizedString>(Strings.OnPremisesOrganizationOrganizationRelationshipNotExists), new Func<OrganizationRelationshipIdParameter, LocalizedString>(Strings.OnPremisesOrganizationOrganizationRelationshipNotUnique), new Func<string, OrganizationRelationshipIdParameter>(OrganizationRelationshipIdParameter.Parse));
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06006312 RID: 25362 RVA: 0x0019E080 File Offset: 0x0019C280
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
