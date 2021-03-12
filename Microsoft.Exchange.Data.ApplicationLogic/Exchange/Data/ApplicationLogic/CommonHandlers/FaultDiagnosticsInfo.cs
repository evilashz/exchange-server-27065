using System;

namespace Microsoft.Exchange.Data.ApplicationLogic.CommonHandlers
{
	// Token: 0x020000C1 RID: 193
	public class FaultDiagnosticsInfo
	{
		// Token: 0x06000839 RID: 2105 RVA: 0x00021A63 File Offset: 0x0001FC63
		public FaultDiagnosticsInfo(int errorID, string errorText)
		{
			this.errorID = errorID;
			this.errorText = errorText;
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x0600083A RID: 2106 RVA: 0x00021A79 File Offset: 0x0001FC79
		public string ErrorText
		{
			get
			{
				return this.errorText;
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x0600083B RID: 2107 RVA: 0x00021A81 File Offset: 0x0001FC81
		public int ErrorID
		{
			get
			{
				return this.errorID;
			}
		}

		// Token: 0x040003AC RID: 940
		private readonly int errorID;

		// Token: 0x040003AD RID: 941
		private readonly string errorText;
	}
}
