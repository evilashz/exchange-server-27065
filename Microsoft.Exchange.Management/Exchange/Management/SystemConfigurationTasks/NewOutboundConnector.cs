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
	// Token: 0x02000B3C RID: 2876
	[Cmdlet("New", "OutboundConnector", SupportsShouldProcess = true)]
	public class NewOutboundConnector : NewMultitenancySystemConfigurationObjectTask<TenantOutboundConnector>
	{
		// Token: 0x17001FDB RID: 8155
		// (get) Token: 0x060067B3 RID: 26547 RVA: 0x001AD1FF File Offset: 0x001AB3FF
		// (set) Token: 0x060067B4 RID: 26548 RVA: 0x001AD20C File Offset: 0x001AB40C
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

		// Token: 0x17001FDC RID: 8156
		// (get) Token: 0x060067B5 RID: 26549 RVA: 0x001AD21A File Offset: 0x001AB41A
		// (set) Token: 0x060067B6 RID: 26550 RVA: 0x001AD227 File Offset: 0x001AB427
		[Parameter(Mandatory = false)]
		public bool UseMXRecord
		{
			get
			{
				return this.DataObject.UseMXRecord;
			}
			set
			{
				this.DataObject.UseMXRecord = value;
			}
		}

		// Token: 0x17001FDD RID: 8157
		// (get) Token: 0x060067B7 RID: 26551 RVA: 0x001AD235 File Offset: 0x001AB435
		// (set) Token: 0x060067B8 RID: 26552 RVA: 0x001AD242 File Offset: 0x001AB442
		[Parameter(Mandatory = false)]
		public TenantConnectorType ConnectorType
		{
			get
			{
				return this.DataObject.ConnectorType;
			}
			set
			{
				this.DataObject.ConnectorType = value;
			}
		}

		// Token: 0x17001FDE RID: 8158
		// (get) Token: 0x060067B9 RID: 26553 RVA: 0x001AD250 File Offset: 0x001AB450
		// (set) Token: 0x060067BA RID: 26554 RVA: 0x001AD25D File Offset: 0x001AB45D
		[Parameter(Mandatory = false)]
		public TenantConnectorSource ConnectorSource
		{
			get
			{
				return this.DataObject.ConnectorSource;
			}
			set
			{
				this.DataObject.ConnectorSource = value;
			}
		}

		// Token: 0x17001FDF RID: 8159
		// (get) Token: 0x060067BB RID: 26555 RVA: 0x001AD26B File Offset: 0x001AB46B
		// (set) Token: 0x060067BC RID: 26556 RVA: 0x001AD278 File Offset: 0x001AB478
		[Parameter(Mandatory = false)]
		public string Comment
		{
			get
			{
				return this.DataObject.Comment;
			}
			set
			{
				this.DataObject.Comment = value;
			}
		}

		// Token: 0x17001FE0 RID: 8160
		// (get) Token: 0x060067BD RID: 26557 RVA: 0x001AD286 File Offset: 0x001AB486
		// (set) Token: 0x060067BE RID: 26558 RVA: 0x001AD293 File Offset: 0x001AB493
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<SmtpDomainWithSubdomains> RecipientDomains
		{
			get
			{
				return this.DataObject.RecipientDomains;
			}
			set
			{
				this.DataObject.RecipientDomains = value;
			}
		}

		// Token: 0x17001FE1 RID: 8161
		// (get) Token: 0x060067BF RID: 26559 RVA: 0x001AD2A1 File Offset: 0x001AB4A1
		// (set) Token: 0x060067C0 RID: 26560 RVA: 0x001AD2AE File Offset: 0x001AB4AE
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<SmartHost> SmartHosts
		{
			get
			{
				return this.DataObject.SmartHosts;
			}
			set
			{
				this.DataObject.SmartHosts = value;
			}
		}

		// Token: 0x17001FE2 RID: 8162
		// (get) Token: 0x060067C1 RID: 26561 RVA: 0x001AD2BC File Offset: 0x001AB4BC
		// (set) Token: 0x060067C2 RID: 26562 RVA: 0x001AD2C9 File Offset: 0x001AB4C9
		[Parameter(Mandatory = false)]
		public SmtpDomainWithSubdomains TlsDomain
		{
			get
			{
				return this.DataObject.TlsDomain;
			}
			set
			{
				this.DataObject.TlsDomain = value;
			}
		}

		// Token: 0x17001FE3 RID: 8163
		// (get) Token: 0x060067C3 RID: 26563 RVA: 0x001AD2D7 File Offset: 0x001AB4D7
		// (set) Token: 0x060067C4 RID: 26564 RVA: 0x001AD2E4 File Offset: 0x001AB4E4
		[Parameter(Mandatory = false)]
		public TlsAuthLevel? TlsSettings
		{
			get
			{
				return this.DataObject.TlsSettings;
			}
			set
			{
				this.DataObject.TlsSettings = value;
			}
		}

		// Token: 0x17001FE4 RID: 8164
		// (get) Token: 0x060067C5 RID: 26565 RVA: 0x001AD2F2 File Offset: 0x001AB4F2
		// (set) Token: 0x060067C6 RID: 26566 RVA: 0x001AD2FF File Offset: 0x001AB4FF
		[Parameter(Mandatory = false)]
		public bool IsTransportRuleScoped
		{
			get
			{
				return this.DataObject.IsTransportRuleScoped;
			}
			set
			{
				this.DataObject.IsTransportRuleScoped = value;
			}
		}

		// Token: 0x17001FE5 RID: 8165
		// (get) Token: 0x060067C7 RID: 26567 RVA: 0x001AD30D File Offset: 0x001AB50D
		// (set) Token: 0x060067C8 RID: 26568 RVA: 0x001AD31A File Offset: 0x001AB51A
		[Parameter(Mandatory = false)]
		public bool RouteAllMessagesViaOnPremises
		{
			get
			{
				return this.DataObject.RouteAllMessagesViaOnPremises;
			}
			set
			{
				this.DataObject.RouteAllMessagesViaOnPremises = value;
			}
		}

		// Token: 0x17001FE6 RID: 8166
		// (get) Token: 0x060067C9 RID: 26569 RVA: 0x001AD328 File Offset: 0x001AB528
		// (set) Token: 0x060067CA RID: 26570 RVA: 0x001AD353 File Offset: 0x001AB553
		[Parameter(Mandatory = false)]
		public bool BypassValidation
		{
			get
			{
				return base.Fields.Contains("BypassValidation") && (bool)base.Fields["BypassValidation"];
			}
			set
			{
				base.Fields["BypassValidation"] = value;
			}
		}

		// Token: 0x060067CB RID: 26571 RVA: 0x001AD36C File Offset: 0x001AB56C
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			ManageTenantOutboundConnectors.ValidateOutboundConnectorDataObject(this.DataObject, this, base.DataSession, this.BypassValidation);
			IEnumerable<TenantOutboundConnector> enumerable = base.DataSession.FindPaged<TenantOutboundConnector>(null, ((IConfigurationSession)base.DataSession).GetOrgContainerId().GetDescendantId(this.DataObject.ParentPath), false, null, ADGenericPagedReader<TenantOutboundConnector>.DefaultPageSize);
			foreach (TenantOutboundConnector tenantOutboundConnector in enumerable)
			{
				if (StringComparer.OrdinalIgnoreCase.Equals(this.DataObject.Name, tenantOutboundConnector.Name))
				{
					base.WriteError(new ErrorOutboundConnectorAlreadyExistsException(tenantOutboundConnector.Name), ErrorCategory.InvalidOperation, null);
					break;
				}
			}
			ManageTenantOutboundConnectors.ValidateIfAcceptedDomainsCanBeRoutedWithConnectors(this.DataObject, base.DataSession, this, false);
			TaskLogger.LogExit();
		}

		// Token: 0x17001FE7 RID: 8167
		// (get) Token: 0x060067CC RID: 26572 RVA: 0x001AD458 File Offset: 0x001AB658
		// (set) Token: 0x060067CD RID: 26573 RVA: 0x001AD465 File Offset: 0x001AB665
		[Parameter(Mandatory = false)]
		public bool CloudServicesMailEnabled
		{
			get
			{
				return this.DataObject.CloudServicesMailEnabled;
			}
			set
			{
				this.DataObject.CloudServicesMailEnabled = value;
			}
		}

		// Token: 0x17001FE8 RID: 8168
		// (get) Token: 0x060067CE RID: 26574 RVA: 0x001AD473 File Offset: 0x001AB673
		// (set) Token: 0x060067CF RID: 26575 RVA: 0x001AD480 File Offset: 0x001AB680
		[Parameter(Mandatory = false)]
		public bool AllAcceptedDomains
		{
			get
			{
				return this.DataObject.AllAcceptedDomains;
			}
			set
			{
				this.DataObject.AllAcceptedDomains = value;
			}
		}

		// Token: 0x060067D0 RID: 26576 RVA: 0x001AD48E File Offset: 0x001AB68E
		protected override void InternalProcessRecord()
		{
			ManageTenantOutboundConnectors.ClearSmartHostsListIfNecessary(this.DataObject);
			base.InternalProcessRecord();
		}

		// Token: 0x17001FE9 RID: 8169
		// (get) Token: 0x060067D1 RID: 26577 RVA: 0x001AD4A1 File Offset: 0x001AB6A1
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewOutboundConnector(base.Name);
			}
		}

		// Token: 0x060067D2 RID: 26578 RVA: 0x001AD4B0 File Offset: 0x001AB6B0
		protected override IConfigurable PrepareDataObject()
		{
			TenantOutboundConnector tenantOutboundConnector = (TenantOutboundConnector)base.PrepareDataObject();
			tenantOutboundConnector.SetId(base.DataSession as IConfigurationSession, base.Name);
			return tenantOutboundConnector;
		}
	}
}
