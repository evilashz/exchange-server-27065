using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.UM;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x02000D36 RID: 3382
	[Cmdlet("Remove", "UMHuntGroup", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveUMHuntGroup : RemoveSystemConfigurationObjectTask<UMHuntGroupIdParameter, UMHuntGroup>
	{
		// Token: 0x1700284A RID: 10314
		// (get) Token: 0x060081B7 RID: 33207 RVA: 0x002126D7 File Offset: 0x002108D7
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveUMHuntGroup(this.Identity.ToString());
			}
		}

		// Token: 0x060081B8 RID: 33208 RVA: 0x002126EC File Offset: 0x002108EC
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.InternalProcessRecord();
			if (!base.HasErrors)
			{
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_HuntGroupRemoved, null, new object[]
				{
					base.DataObject.DistinguishedName,
					base.DataObject.PilotIdentifier,
					base.DataObject.UMDialPlan.Name
				});
			}
			TaskLogger.LogExit();
		}
	}
}
