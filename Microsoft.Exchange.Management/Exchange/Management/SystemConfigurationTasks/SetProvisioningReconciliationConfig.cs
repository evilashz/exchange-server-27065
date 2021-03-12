using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000B05 RID: 2821
	[Cmdlet("Set", "ProvisioningReconciliationConfig", SupportsShouldProcess = true)]
	public sealed class SetProvisioningReconciliationConfig : SetSingletonSystemConfigurationObjectTask<ProvisioningReconciliationConfig>
	{
		// Token: 0x17001E6F RID: 7791
		// (get) Token: 0x06006448 RID: 25672 RVA: 0x001A2C10 File Offset: 0x001A0E10
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001E70 RID: 7792
		// (get) Token: 0x06006449 RID: 25673 RVA: 0x001A2C13 File Offset: 0x001A0E13
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmSetProvisioningReconciliationConfig;
			}
		}

		// Token: 0x17001E71 RID: 7793
		// (get) Token: 0x0600644A RID: 25674 RVA: 0x001A2C1A File Offset: 0x001A0E1A
		// (set) Token: 0x0600644B RID: 25675 RVA: 0x001A2C22 File Offset: 0x001A0E22
		internal new Fqdn DomainController { get; set; }

		// Token: 0x17001E72 RID: 7794
		// (get) Token: 0x0600644C RID: 25676 RVA: 0x001A2C2B File Offset: 0x001A0E2B
		// (set) Token: 0x0600644D RID: 25677 RVA: 0x001A2C42 File Offset: 0x001A0E42
		[Parameter(Mandatory = true)]
		public MultiValuedProperty<ReconciliationCookie> ReconciliationCookies
		{
			get
			{
				return (MultiValuedProperty<ReconciliationCookie>)base.Fields["ReconciliationCookies"];
			}
			set
			{
				base.Fields["ReconciliationCookies"] = value;
			}
		}

		// Token: 0x0600644E RID: 25678 RVA: 0x001A2C58 File Offset: 0x001A0E58
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			ProvisioningReconciliationConfig dataObject = this.DataObject;
			if (dataObject != null && this.ReconciliationCookies != null && this.ReconciliationCookies.Count > 0)
			{
				dataObject.ReconciliationCookies = this.ReconciliationCookies;
			}
			base.InternalProcessRecord();
			TaskLogger.LogExit();
		}
	}
}
