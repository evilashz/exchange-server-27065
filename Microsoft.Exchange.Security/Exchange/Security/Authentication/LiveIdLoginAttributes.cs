using System;

namespace Microsoft.Exchange.Security.Authentication
{
	// Token: 0x02000075 RID: 117
	[Serializable]
	public class LiveIdLoginAttributes
	{
		// Token: 0x06000400 RID: 1024 RVA: 0x00020363 File Offset: 0x0001E563
		public LiveIdLoginAttributes(uint loginAttributes)
		{
			this.loginAttributesBitField = (loginAttributes & 255U);
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000401 RID: 1025 RVA: 0x00020378 File Offset: 0x0001E578
		public uint Value
		{
			get
			{
				return this.loginAttributesBitField;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000402 RID: 1026 RVA: 0x00020380 File Offset: 0x0001E580
		public bool IsInsideCorpnetSession
		{
			get
			{
				return (this.loginAttributesBitField & 32U) == 32U;
			}
		}

		// Token: 0x04000445 RID: 1093
		private const byte PP_LOGINATTRIBUTE_TRUSTEDDEVICE = 1;

		// Token: 0x04000446 RID: 1094
		private const byte PP_LOGINATTRIBUTE_PIN = 2;

		// Token: 0x04000447 RID: 1095
		private const byte PP_LOGINATTRIBUTE_STRONGPWD = 4;

		// Token: 0x04000448 RID: 1096
		private const byte PP_LOGINATTRIBUTE_STRONGPWDEXPIRY = 8;

		// Token: 0x04000449 RID: 1097
		private const byte PP_LOGINATTRIBUTE_CERTIFICATE = 16;

		// Token: 0x0400044A RID: 1098
		private const byte PP_LOGINATTRIBUTE_INSIDECORPORATENETWORK = 32;

		// Token: 0x0400044B RID: 1099
		private const byte PP_LOGINATTRIBUTE_DC_ISREGISTEREDUSER = 64;

		// Token: 0x0400044C RID: 1100
		private const byte PP_LOGINATTRIBUTE_DC_ISMANAGED = 128;

		// Token: 0x0400044D RID: 1101
		private readonly uint loginAttributesBitField;
	}
}
