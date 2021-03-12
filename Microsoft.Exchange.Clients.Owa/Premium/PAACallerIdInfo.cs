using System;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x020004AB RID: 1195
	[OwaEventStruct("pcii")]
	internal sealed class PAACallerIdInfo
	{
		// Token: 0x04001F04 RID: 7940
		public const string StructNamespace = "pcii";

		// Token: 0x04001F05 RID: 7941
		public const string HasPhones = "fph";

		// Token: 0x04001F06 RID: 7942
		public const string HasCnts = "fcnt";

		// Token: 0x04001F07 RID: 7943
		public const string IsInCntFolder = "fld";

		// Token: 0x04001F08 RID: 7944
		[OwaEventField("fph", true, false)]
		public bool HasPhoneNumbers;

		// Token: 0x04001F09 RID: 7945
		[OwaEventField("fcnt", true, false)]
		public bool HasContacts;

		// Token: 0x04001F0A RID: 7946
		[OwaEventField("fld", true, false)]
		public bool IsInContactFolder;
	}
}
