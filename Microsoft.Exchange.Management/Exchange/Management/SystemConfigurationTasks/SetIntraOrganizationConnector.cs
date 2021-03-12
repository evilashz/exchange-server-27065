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
	// Token: 0x02000A16 RID: 2582
	[Cmdlet("Set", "IntraOrganizationConnector", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetIntraOrganizationConnector : SetSystemConfigurationObjectTask<IntraOrganizationConnectorIdParameter, IntraOrganizationConnector>
	{
		// Token: 0x17001BC4 RID: 7108
		// (get) Token: 0x06005C9D RID: 23709 RVA: 0x00186426 File Offset: 0x00184626
		// (set) Token: 0x06005C9E RID: 23710 RVA: 0x0018643D File Offset: 0x0018463D
		[Parameter]
		public MultiValuedProperty<SmtpDomain> TargetAddressDomains
		{
			get
			{
				return (MultiValuedProperty<SmtpDomain>)base.Fields[IntraOrganizationConnectorSchema.TargetAddressDomains];
			}
			set
			{
				base.Fields[IntraOrganizationConnectorSchema.TargetAddressDomains] = value;
			}
		}

		// Token: 0x17001BC5 RID: 7109
		// (get) Token: 0x06005C9F RID: 23711 RVA: 0x00186450 File Offset: 0x00184650
		// (set) Token: 0x06005CA0 RID: 23712 RVA: 0x00186467 File Offset: 0x00184667
		[Parameter]
		public Uri DiscoveryEndpoint
		{
			get
			{
				return (Uri)base.Fields[IntraOrganizationConnectorSchema.DiscoveryEndpoint];
			}
			set
			{
				base.Fields[IntraOrganizationConnectorSchema.DiscoveryEndpoint] = value;
			}
		}

		// Token: 0x17001BC6 RID: 7110
		// (get) Token: 0x06005CA1 RID: 23713 RVA: 0x0018647A File Offset: 0x0018467A
		// (set) Token: 0x06005CA2 RID: 23714 RVA: 0x00186491 File Offset: 0x00184691
		[Parameter]
		public bool Enabled
		{
			get
			{
				return (bool)base.Fields[IntraOrganizationConnectorSchema.Enabled];
			}
			set
			{
				base.Fields[IntraOrganizationConnectorSchema.Enabled] = value;
			}
		}

		// Token: 0x17001BC7 RID: 7111
		// (get) Token: 0x06005CA3 RID: 23715 RVA: 0x001864A9 File Offset: 0x001846A9
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetIntraOrganizationConnector(this.Identity.ToString());
			}
		}

		// Token: 0x06005CA4 RID: 23716 RVA: 0x001864BC File Offset: 0x001846BC
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			foreach (ADPropertyDefinition adpropertyDefinition in SetIntraOrganizationConnector.setProperties)
			{
				if (base.Fields.IsModified(adpropertyDefinition))
				{
					this.DataObject[adpropertyDefinition] = base.Fields[adpropertyDefinition];
				}
			}
			if (NewIntraOrganizationConnector.DomainExists(this.DataObject.TargetAddressDomains, this.ConfigurationSession, new Guid?(this.DataObject.Guid)))
			{
				base.WriteError(new DuplicateIntraOrganizationConnectorDomainException(base.FormatMultiValuedProperty(this.DataObject.TargetAddressDomains)), ErrorCategory.InvalidOperation, this.Identity);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x04003470 RID: 13424
		private static readonly PropertyDefinition[] setProperties = new PropertyDefinition[]
		{
			IntraOrganizationConnectorSchema.TargetAddressDomains,
			IntraOrganizationConnectorSchema.DiscoveryEndpoint,
			IntraOrganizationConnectorSchema.Enabled
		};
	}
}
