using System;

namespace Microsoft.Exchange.Data.Transport.Email
{
	// Token: 0x020000F3 RID: 243
	internal sealed class ParameterValues
	{
		// Token: 0x06000635 RID: 1589 RVA: 0x00010D8E File Offset: 0x0000EF8E
		private ParameterValues()
		{
			throw new NotSupportedException();
		}

		// Token: 0x040003CE RID: 974
		public const string WinMailDotDat = "winmail.dat";

		// Token: 0x040003CF RID: 975
		public const string DeliveryStatus = "delivery-status";

		// Token: 0x040003D0 RID: 976
		public const string DispositionNotification = "disposition-notification";

		// Token: 0x040003D1 RID: 977
		public const string SignedData = "signed-data";

		// Token: 0x040003D2 RID: 978
		public const string EnvelopedData = "enveloped-data";

		// Token: 0x040003D3 RID: 979
		public const string CertsOnly = "certs-only";

		// Token: 0x040003D4 RID: 980
		public const string ApplicationPgpSignature = "application/pgp-signature";

		// Token: 0x040003D5 RID: 981
		public const string ApplicationPgpEncrypted = "application/pgp-encrypted";

		// Token: 0x040003D6 RID: 982
		public const string ApplicationXSmimeSignedXml = "application/xsmime-signed+xml";

		// Token: 0x040003D7 RID: 983
		public const string ApplicationXSmimeEncryptedXml = "application/xsmime-encrypted+xml";

		// Token: 0x040003D8 RID: 984
		public const string MessageRpmsg = "message.rpmsg";
	}
}
