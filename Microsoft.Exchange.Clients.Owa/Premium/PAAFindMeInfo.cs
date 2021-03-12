using System;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x020004AC RID: 1196
	[OwaEventStruct("pfmi")]
	internal sealed class PAAFindMeInfo
	{
		// Token: 0x04001F0B RID: 7947
		public const string StructNamespace = "pfmi";

		// Token: 0x04001F0C RID: 7948
		public const string DescriptionName = "desc";

		// Token: 0x04001F0D RID: 7949
		public const string KeyName = "k";

		// Token: 0x04001F0E RID: 7950
		public const string Phone1Name = "ph1";

		// Token: 0x04001F0F RID: 7951
		public const string Timeout1Name = "tm1";

		// Token: 0x04001F10 RID: 7952
		public const string Phone2Name = "ph2";

		// Token: 0x04001F11 RID: 7953
		public const string Timeout2Name = "tm2";

		// Token: 0x04001F12 RID: 7954
		[OwaEventField("desc", true, 0)]
		public string Desc;

		// Token: 0x04001F13 RID: 7955
		[OwaEventField("k", false, 0)]
		public int Key;

		// Token: 0x04001F14 RID: 7956
		[OwaEventField("ph1", true, "")]
		public string Ph1;

		// Token: 0x04001F15 RID: 7957
		[OwaEventField("tm1", true, 5)]
		public int Tm1;

		// Token: 0x04001F16 RID: 7958
		[OwaEventField("ph2", true, "")]
		public string Ph2;

		// Token: 0x04001F17 RID: 7959
		[OwaEventField("tm2", true, 5)]
		public int Tm2;
	}
}
