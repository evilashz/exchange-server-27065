using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000040 RID: 64
	internal abstract class BlindTransferBase : TransferBase
	{
		// Token: 0x060002AB RID: 683 RVA: 0x0000BDB2 File Offset: 0x00009FB2
		protected BlindTransferBase(BaseUMCallSession session, CallContext context, PhoneNumber number) : base(session, context)
		{
			this.number = number;
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060002AC RID: 684 RVA: 0x0000BDC3 File Offset: 0x00009FC3
		protected PhoneNumber Number
		{
			get
			{
				return this.number;
			}
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000BDCC File Offset: 0x00009FCC
		internal override void Transfer()
		{
			PIIMessage data = PIIMessage.Create(PIIType._PhoneNumber, this.number);
			CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, data, "BlindTransfer to: _PhoneNumber.", new object[0]);
			if (base.Context.DialPlan.URIType == UMUriType.SipName || this.number.UriType == UMUriType.SipName)
			{
				base.FrameTransferTargetAndTransferForSIPNames(this.number);
				return;
			}
			base.Session.TransferAsync(base.GetReferTargetForPhoneNumbers(this.number, null));
		}

		// Token: 0x040000D9 RID: 217
		private PhoneNumber number;
	}
}
