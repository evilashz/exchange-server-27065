using System;
using System.Management.Automation;
using System.Text;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C38 RID: 3128
	[Cmdlet("Remove", "OabVirtualDirectory", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveOabVirtualDirectory : RemoveExchangeVirtualDirectory<ADOabVirtualDirectory>
	{
		// Token: 0x1700247A RID: 9338
		// (get) Token: 0x06007669 RID: 30313 RVA: 0x001E361B File Offset: 0x001E181B
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveOabVirtualDirectory(this.Identity.ToString());
			}
		}

		// Token: 0x1700247B RID: 9339
		// (get) Token: 0x0600766A RID: 30314 RVA: 0x001E362D File Offset: 0x001E182D
		// (set) Token: 0x0600766B RID: 30315 RVA: 0x001E3653 File Offset: 0x001E1853
		[Parameter]
		public SwitchParameter Force
		{
			get
			{
				return (SwitchParameter)(base.Fields["Force"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["Force"] = value;
			}
		}

		// Token: 0x0600766C RID: 30316 RVA: 0x001E366C File Offset: 0x001E186C
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			try
			{
				if (!this.Force)
				{
					OfflineAddressBook[] array = this.ConfigurationSession.FindOABsForWebDistributionPoint(base.DataObject);
					if (array != null && array.Length > 0)
					{
						StringBuilder stringBuilder = new StringBuilder();
						stringBuilder.Append(array[0].Name);
						for (int i = 1; i < array.Length; i++)
						{
							stringBuilder.Append(", ");
							stringBuilder.Append(array[i].Name);
						}
						if (!base.ShouldContinue(Strings.RemoveNonEmptyOabVirtualDirectory(this.Identity.ToString(), stringBuilder.ToString())))
						{
							return;
						}
					}
				}
			}
			catch (DataSourceTransientException ex)
			{
				TaskLogger.Trace("The action of quary offline address books associcated with this virtual directory raised exception {0}", new object[]
				{
					ex.ToString()
				});
			}
			base.InternalProcessRecord();
			TaskLogger.LogExit();
		}
	}
}
