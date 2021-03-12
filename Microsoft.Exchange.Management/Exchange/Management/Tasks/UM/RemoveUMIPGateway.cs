using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.UM;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x02000D38 RID: 3384
	[Cmdlet("Remove", "UMIPGateway", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveUMIPGateway : RemoveSystemConfigurationObjectTask<UMIPGatewayIdParameter, UMIPGateway>
	{
		// Token: 0x1700284C RID: 10316
		// (get) Token: 0x060081C1 RID: 33217 RVA: 0x00212A2E File Offset: 0x00210C2E
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveUMIPGateway(this.Identity.ToString());
			}
		}

		// Token: 0x060081C2 RID: 33218 RVA: 0x00212A40 File Offset: 0x00210C40
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (!base.HasErrors)
			{
				foreach (UMHuntGroup instance in base.DataObject.HuntGroups)
				{
					base.DataSession.Delete(instance);
				}
			}
			base.InternalProcessRecord();
			if (!base.HasErrors)
			{
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_IPGatewayRemoved, null, new object[]
				{
					base.DataObject.Name,
					base.DataObject.Address.ToString()
				});
			}
			TaskLogger.LogExit();
		}
	}
}
