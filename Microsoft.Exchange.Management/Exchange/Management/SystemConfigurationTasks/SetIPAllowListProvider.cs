using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A78 RID: 2680
	[Cmdlet("Set", "IPAllowListProvider", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetIPAllowListProvider : SetSystemConfigurationObjectTask<IPAllowListProviderIdParameter, IPAllowListProvider>
	{
		// Token: 0x17001CC4 RID: 7364
		// (get) Token: 0x06005F6F RID: 24431 RVA: 0x0018FD5B File Offset: 0x0018DF5B
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetIPAllowListProvider(this.Identity.ToString());
			}
		}

		// Token: 0x06005F70 RID: 24432 RVA: 0x0018FD70 File Offset: 0x0018DF70
		protected override void InternalProcessRecord()
		{
			IConfigurationSession session = (IConfigurationSession)base.DataSession;
			NewIPListProvider<IPAllowListProvider>.AdjustPriorities(session, this.DataObject, true);
			base.InternalProcessRecord();
		}
	}
}
