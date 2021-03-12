using System;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020001A7 RID: 423
	internal class PhoneUtil
	{
		// Token: 0x06000C6E RID: 3182 RVA: 0x0003614C File Offset: 0x0003434C
		internal static void SetTransferTargetPhone(ActivityManager manager, TransferExtension ext, PhoneNumber phone)
		{
			PhoneUtil.SetTransferTargetPhone(manager, ext, phone, null);
		}

		// Token: 0x06000C6F RID: 3183 RVA: 0x00036158 File Offset: 0x00034358
		internal static void SetTransferTargetPhone(ActivityManager manager, TransferExtension ext, PhoneNumber phone, ContactInfo targetContactInfo)
		{
			PIIMessage[] data = new PIIMessage[]
			{
				PIIMessage.Create(PIIType._PhoneNumber, phone),
				PIIMessage.Create(PIIType._PII, targetContactInfo)
			};
			CallIdTracer.TraceDebug(ExTraceGlobals.AutoAttendantTracer, manager, data, "Setting transfer extension = _PhoneNumber Type = {0}. TargetContactInfo = _PII", new object[]
			{
				ext
			});
			manager.TargetPhoneNumber = phone;
			manager.WriteVariable("targetContactInfo", targetContactInfo);
			manager.WriteVariable("transferExtension", ext);
		}
	}
}
