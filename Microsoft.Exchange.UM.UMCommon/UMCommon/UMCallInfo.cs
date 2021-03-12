using System;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x02000020 RID: 32
	[Serializable]
	public class UMCallInfo
	{
		// Token: 0x06000209 RID: 521 RVA: 0x000082E5 File Offset: 0x000064E5
		public UMCallInfo()
		{
		}

		// Token: 0x0600020A RID: 522 RVA: 0x000082F4 File Offset: 0x000064F4
		public UMCallInfo(UMCallInfoEx properties)
		{
			this.callState = properties.CallState;
			this.EventCause = properties.EventCause;
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600020B RID: 523 RVA: 0x0000831B File Offset: 0x0000651B
		// (set) Token: 0x0600020C RID: 524 RVA: 0x00008323 File Offset: 0x00006523
		public UMCallState CallState
		{
			get
			{
				return this.callState;
			}
			set
			{
				this.callState = value;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600020D RID: 525 RVA: 0x0000832C File Offset: 0x0000652C
		// (set) Token: 0x0600020E RID: 526 RVA: 0x00008334 File Offset: 0x00006534
		public UMEventCause EventCause
		{
			get
			{
				return this.eventCause;
			}
			set
			{
				this.eventCause = value;
			}
		}

		// Token: 0x040000AA RID: 170
		private UMCallState callState = UMCallState.Disconnected;

		// Token: 0x040000AB RID: 171
		private UMEventCause eventCause;
	}
}
