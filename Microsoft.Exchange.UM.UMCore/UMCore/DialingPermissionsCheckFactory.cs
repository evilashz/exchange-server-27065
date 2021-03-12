using System;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000115 RID: 277
	internal class DialingPermissionsCheckFactory
	{
		// Token: 0x060007C9 RID: 1993 RVA: 0x0001F980 File Offset: 0x0001DB80
		internal static DialingPermissionsCheck Create(BaseUMCallSession vo)
		{
			UMDialPlan dialPlan = vo.CurrentCallContext.DialPlan;
			DialingPermissionsCheck result;
			switch (vo.CurrentCallContext.CallType)
			{
			case 1:
				result = new DialingPermissionsCheck(dialPlan, false);
				break;
			case 2:
				result = new DialingPermissionsCheck(vo.CurrentCallContext.AutoAttendantInfo, vo.CurrentCallContext.CurrentAutoAttendantSettings, dialPlan);
				break;
			case 3:
				result = new DialingPermissionsCheck((ADUser)vo.CurrentCallContext.CallerInfo.ADRecipient, dialPlan);
				break;
			default:
				throw new ArgumentException("Invalid calltype: " + vo.CurrentCallContext.CallType.ToString());
			}
			return result;
		}
	}
}
