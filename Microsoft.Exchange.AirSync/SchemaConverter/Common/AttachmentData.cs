using System;

namespace Microsoft.Exchange.AirSync.SchemaConverter.Common
{
	// Token: 0x0200017B RID: 379
	[Serializable]
	internal struct AttachmentData
	{
		// Token: 0x06001085 RID: 4229 RVA: 0x0005C9CA File Offset: 0x0005ABCA
		public AttachmentData(string attName, long attSize, int attMethod, string displayName)
		{
			this.AttName = attName;
			this.AttSize = attSize;
			this.AttMethod = attMethod;
			this.DisplayName = displayName;
		}

		// Token: 0x04000AC9 RID: 2761
		public int AttMethod;

		// Token: 0x04000ACA RID: 2762
		public string AttName;

		// Token: 0x04000ACB RID: 2763
		public long AttSize;

		// Token: 0x04000ACC RID: 2764
		public string DisplayName;
	}
}
