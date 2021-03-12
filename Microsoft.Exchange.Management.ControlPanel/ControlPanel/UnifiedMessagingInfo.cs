using System;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200048B RID: 1163
	[Serializable]
	public sealed class UnifiedMessagingInfo
	{
		// Token: 0x06003A1C RID: 14876 RVA: 0x000B0310 File Offset: 0x000AE510
		public UnifiedMessagingInfo(string enableTemplate, string disableTemplate, string callForwardingType)
		{
			this.enableTemplate = enableTemplate;
			this.disableTemplate = disableTemplate;
			this.callForwardingType = callForwardingType;
		}

		// Token: 0x170022F5 RID: 8949
		// (get) Token: 0x06003A1D RID: 14877 RVA: 0x000B034E File Offset: 0x000AE54E
		public string EnableTemplate
		{
			get
			{
				return this.enableTemplate;
			}
		}

		// Token: 0x170022F6 RID: 8950
		// (get) Token: 0x06003A1E RID: 14878 RVA: 0x000B0356 File Offset: 0x000AE556
		public string DisableTemplate
		{
			get
			{
				return this.disableTemplate;
			}
		}

		// Token: 0x170022F7 RID: 8951
		// (get) Token: 0x06003A1F RID: 14879 RVA: 0x000B035E File Offset: 0x000AE55E
		public string CallForwardingType
		{
			get
			{
				return this.callForwardingType;
			}
		}

		// Token: 0x06003A20 RID: 14880 RVA: 0x000B0366 File Offset: 0x000AE566
		public string RenderEnableSequence(string phoneNumber)
		{
			return string.Format(this.EnableTemplate, phoneNumber);
		}

		// Token: 0x06003A21 RID: 14881 RVA: 0x000B0374 File Offset: 0x000AE574
		public string RenderDisableSequence(string phoneNumber)
		{
			if (!string.IsNullOrEmpty(phoneNumber))
			{
				return string.Format(this.DisableTemplate, phoneNumber);
			}
			return string.Empty;
		}

		// Token: 0x06003A22 RID: 14882 RVA: 0x000B0390 File Offset: 0x000AE590
		public override string ToString()
		{
			return string.Format("{0}|{1}|{2}", this.EnableTemplate, this.DisableTemplate, this.CallForwardingType);
		}

		// Token: 0x040026E3 RID: 9955
		private string callForwardingType = string.Empty;

		// Token: 0x040026E4 RID: 9956
		private string enableTemplate = string.Empty;

		// Token: 0x040026E5 RID: 9957
		private string disableTemplate = string.Empty;
	}
}
