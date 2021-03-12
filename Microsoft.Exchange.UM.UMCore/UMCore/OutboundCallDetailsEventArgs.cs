using System;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000196 RID: 406
	internal class OutboundCallDetailsEventArgs : EventArgs
	{
		// Token: 0x06000BFE RID: 3070 RVA: 0x00034110 File Offset: 0x00032310
		internal OutboundCallDetailsEventArgs(Exception error, UMCallInfoEx exInfo, object state)
		{
			this.Error = error;
			this.CallInfoEx = exInfo;
			this.PlatformState = state;
			this.CallOutcome = ((error == null && exInfo.EndResult == UMOperationResult.Success) ? OutboundCallDetailsEventArgs.OutboundCallOutcome.Success : OutboundCallDetailsEventArgs.OutboundCallOutcome.Failure);
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000BFF RID: 3071 RVA: 0x00034143 File Offset: 0x00032343
		// (set) Token: 0x06000C00 RID: 3072 RVA: 0x0003414B File Offset: 0x0003234B
		internal OutboundCallDetailsEventArgs.OutboundCallOutcome CallOutcome { get; private set; }

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000C01 RID: 3073 RVA: 0x00034154 File Offset: 0x00032354
		// (set) Token: 0x06000C02 RID: 3074 RVA: 0x0003415C File Offset: 0x0003235C
		internal Exception Error { get; private set; }

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000C03 RID: 3075 RVA: 0x00034165 File Offset: 0x00032365
		// (set) Token: 0x06000C04 RID: 3076 RVA: 0x0003416D File Offset: 0x0003236D
		internal UMCallInfoEx CallInfoEx { get; private set; }

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000C05 RID: 3077 RVA: 0x00034176 File Offset: 0x00032376
		// (set) Token: 0x06000C06 RID: 3078 RVA: 0x0003417E File Offset: 0x0003237E
		internal object PlatformState { get; private set; }

		// Token: 0x02000197 RID: 407
		internal enum OutboundCallOutcome
		{
			// Token: 0x04000A0F RID: 2575
			Success,
			// Token: 0x04000A10 RID: 2576
			Failure
		}
	}
}
