using System;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x020004AD RID: 1197
	[OwaEventStruct("pxti")]
	internal sealed class PAATransferToInfo
	{
		// Token: 0x04001F18 RID: 7960
		public const string StructNamespace = "pxti";

		// Token: 0x04001F19 RID: 7961
		public const string DescriptionName = "desc";

		// Token: 0x04001F1A RID: 7962
		public const string KeyName = "k";

		// Token: 0x04001F1B RID: 7963
		public const string PhoneName = "ph";

		// Token: 0x04001F1C RID: 7964
		public const string ContactName = "rcp";

		// Token: 0x04001F1D RID: 7965
		public const string DirectlyToVoiceMailName = "VM";

		// Token: 0x04001F1E RID: 7966
		[OwaEventField("desc", true, 0)]
		public string Desc;

		// Token: 0x04001F1F RID: 7967
		[OwaEventField("k", false, 0)]
		public int Key;

		// Token: 0x04001F20 RID: 7968
		[OwaEventField("ph", true, "")]
		public string Ph;

		// Token: 0x04001F21 RID: 7969
		[OwaEventField("rcp", true, null)]
		public string Contact;

		// Token: 0x04001F22 RID: 7970
		[OwaEventField("VM", true, true)]
		public bool VM;
	}
}
