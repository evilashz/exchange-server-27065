using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A80 RID: 2688
	[Cmdlet("Set", "IPBlockListProvider", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetIPBlockListProvider : SetSystemConfigurationObjectTask<IPBlockListProviderIdParameter, IPBlockListProvider>
	{
		// Token: 0x17001CCF RID: 7375
		// (get) Token: 0x06005F84 RID: 24452 RVA: 0x0018FE43 File Offset: 0x0018E043
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetIPBlockListProvider(this.Identity.ToString());
			}
		}

		// Token: 0x06005F85 RID: 24453 RVA: 0x0018FE58 File Offset: 0x0018E058
		protected override void InternalProcessRecord()
		{
			IConfigurationSession session = (IConfigurationSession)base.DataSession;
			NewIPListProvider<IPBlockListProvider>.AdjustPriorities(session, this.DataObject, true);
			base.InternalProcessRecord();
		}
	}
}
