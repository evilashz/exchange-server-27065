using System;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x0200074C RID: 1868
	[Serializable]
	internal class ResubmitRequestException : Exception
	{
		// Token: 0x06005ADD RID: 23261 RVA: 0x0013E3C2 File Offset: 0x0013C5C2
		public ResubmitRequestException(ResubmitRequestResponseCode code, string message) : base(message)
		{
			this.errorCode = code;
		}

		// Token: 0x17001F82 RID: 8066
		// (get) Token: 0x06005ADE RID: 23262 RVA: 0x0013E3D2 File Offset: 0x0013C5D2
		public ResubmitRequestResponseCode ErrorCode
		{
			get
			{
				return this.errorCode;
			}
		}

		// Token: 0x04003CED RID: 15597
		private ResubmitRequestResponseCode errorCode;
	}
}
