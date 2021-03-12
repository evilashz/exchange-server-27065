using System;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x02000022 RID: 34
	[Serializable]
	public class UMCallInfoEx : UMCallInfo
	{
		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600020F RID: 527 RVA: 0x0000833D File Offset: 0x0000653D
		// (set) Token: 0x06000210 RID: 528 RVA: 0x00008345 File Offset: 0x00006545
		public int ResponseCode
		{
			get
			{
				return this.responseCode;
			}
			set
			{
				this.responseCode = value;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000211 RID: 529 RVA: 0x0000834E File Offset: 0x0000654E
		// (set) Token: 0x06000212 RID: 530 RVA: 0x00008356 File Offset: 0x00006556
		public string ResponseText
		{
			get
			{
				return this.responseText;
			}
			set
			{
				this.responseText = value;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000213 RID: 531 RVA: 0x0000835F File Offset: 0x0000655F
		// (set) Token: 0x06000214 RID: 532 RVA: 0x00008367 File Offset: 0x00006567
		public UMOperationResult EndResult
		{
			get
			{
				return this.endResult;
			}
			set
			{
				this.endResult = value;
			}
		}

		// Token: 0x040000B0 RID: 176
		private int responseCode;

		// Token: 0x040000B1 RID: 177
		private string responseText = string.Empty;

		// Token: 0x040000B2 RID: 178
		private UMOperationResult endResult;
	}
}
