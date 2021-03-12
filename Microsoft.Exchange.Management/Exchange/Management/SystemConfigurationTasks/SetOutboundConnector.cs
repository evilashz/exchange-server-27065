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
	// Token: 0x02000B4F RID: 2895
	[Cmdlet("Set", "OutboundConnector", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public class SetOutboundConnector : SetSystemConfigurationObjectTask<OutboundConnectorIdParameter, TenantOutboundConnector>
	{
		// Token: 0x1700206A RID: 8298
		// (get) Token: 0x0600690B RID: 26891 RVA: 0x001B0D6D File Offset: 0x001AEF6D
		// (set) Token: 0x0600690C RID: 26892 RVA: 0x001B0D75 File Offset: 0x001AEF75
		private string CurrentName { get; set; }

		// Token: 0x1700206B RID: 8299
		// (get) Token: 0x0600690D RID: 26893 RVA: 0x001B0D7E File Offset: 0x001AEF7E
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetOutboundConnector(this.Identity.ToString());
			}
		}

		// Token: 0x1700206C RID: 8300
		// (get) Token: 0x0600690E RID: 26894 RVA: 0x001B0D90 File Offset: 0x001AEF90
		// (set) Token: 0x0600690F RID: 26895 RVA: 0x001B0DBB File Offset: 0x001AEFBB
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

		// Token: 0x06006910 RID: 26896 RVA: 0x001B0DD4 File Offset: 0x001AEFD4
		protected override IConfigurable ResolveDataObject()
		{
			ADObject adobject = (ADObject)base.ResolveDataObject();
			this.CurrentName = adobject.Name;
			return adobject;
		}

		// Token: 0x06006911 RID: 26897 RVA: 0x001B0DFA File Offset: 0x001AEFFA
		protected override void InternalProcessRecord()
		{
			ManageTenantOutboundConnectors.ClearSmartHostsListIfNecessary(this.DataObject);
			base.InternalProcessRecord();
		}

		// Token: 0x06006912 RID: 26898 RVA: 0x001B0E10 File Offset: 0x001AF010
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			LocalizedException ex = ManageTenantOutboundConnectors.ValidateConnectorNameReferences(this.DataObject, this.CurrentName, base.DataSession);
			if (ex != null)
			{
				base.WriteError(ex, ErrorCategory.InvalidArgument, null);
			}
			ManageTenantOutboundConnectors.ValidateOutboundConnectorDataObject(this.DataObject, this, base.DataSession, this.BypassValidation);
			ManageTenantOutboundConnectors.ValidateIfAcceptedDomainsCanBeRoutedWithConnectors(this.DataObject, base.DataSession, this, false);
			TaskLogger.LogExit();
		}
	}
}
