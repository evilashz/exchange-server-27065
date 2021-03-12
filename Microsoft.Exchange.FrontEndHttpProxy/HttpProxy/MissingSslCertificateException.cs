using System;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x02000046 RID: 70
	[Serializable]
	internal class MissingSslCertificateException : Exception
	{
		// Token: 0x0600021D RID: 541 RVA: 0x0000BE2E File Offset: 0x0000A02E
		public MissingSslCertificateException() : base(MissingSslCertificateException.ErrorMessage)
		{
		}

		// Token: 0x04000118 RID: 280
		private static readonly string ErrorMessage = "Failed to load SSL certificate.";
	}
}
