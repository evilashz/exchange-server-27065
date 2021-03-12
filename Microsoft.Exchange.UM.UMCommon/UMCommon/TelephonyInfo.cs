using System;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x02000038 RID: 56
	internal struct TelephonyInfo
	{
		// Token: 0x060002CD RID: 717 RVA: 0x0000B0F0 File Offset: 0x000092F0
		public TelephonyInfo(PhoneNumber accessNumber, PhoneNumber voicemailNumber)
		{
			this.AccessNumber = accessNumber;
			this.VoicemailNumber = voicemailNumber;
		}

		// Token: 0x040000E5 RID: 229
		public static readonly TelephonyInfo Empty = new TelephonyInfo(null, null);

		// Token: 0x040000E6 RID: 230
		public PhoneNumber AccessNumber;

		// Token: 0x040000E7 RID: 231
		public PhoneNumber VoicemailNumber;
	}
}
