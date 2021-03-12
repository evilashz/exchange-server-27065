using System;
using System.Security.Cryptography;

namespace Microsoft.Exchange.Security.Cryptography
{
	// Token: 0x02000A49 RID: 2633
	internal static class WellKnownOid
	{
		// Token: 0x040030E9 RID: 12521
		public static readonly Oid X957Sha1Dsa = new Oid("1.2.840.10040.4.3");

		// Token: 0x040030EA RID: 12522
		public static readonly Oid RsaRsa = new Oid("1.2.840.113549.1.1.1");

		// Token: 0x040030EB RID: 12523
		public static readonly Oid RsaSha1Rsa = new Oid("1.2.840.113549.1.1.5");

		// Token: 0x040030EC RID: 12524
		public static readonly Oid Sha256Rsa = new Oid("1.2.840.113549.1.1.11");

		// Token: 0x040030ED RID: 12525
		public static readonly Oid Sha384Rsa = new Oid("1.2.840.113549.1.1.12");

		// Token: 0x040030EE RID: 12526
		public static readonly Oid Sha512Rsa = new Oid("1.2.840.113549.1.1.13");

		// Token: 0x040030EF RID: 12527
		public static readonly Oid ExchangeKPK = new Oid("1.2.840.113556.1.4.7000.102.50757", "MLS Server Certificate");

		// Token: 0x040030F0 RID: 12528
		public static readonly Oid ExchangeMLSKey = new Oid("1.2.840.113556.1.4.7000.102.50767", "MLS Key pair");

		// Token: 0x040030F1 RID: 12529
		public static readonly Oid PkixKpServerAuth = new Oid("1.3.6.1.5.5.7.3.1");

		// Token: 0x040030F2 RID: 12530
		public static readonly Oid PkixKpClientAuth = new Oid("1.3.6.1.5.5.7.3.2");

		// Token: 0x040030F3 RID: 12531
		public static readonly Oid CommonName = new Oid("2.5.4.3");

		// Token: 0x040030F4 RID: 12532
		public static readonly Oid KeyUsage = new Oid("2.5.29.15");

		// Token: 0x040030F5 RID: 12533
		public static readonly Oid BasicConstraints = new Oid("2.5.29.19");

		// Token: 0x040030F6 RID: 12534
		public static readonly Oid SubjectAltName = new Oid("2.5.29.17");

		// Token: 0x040030F7 RID: 12535
		public static readonly Oid EmailProtection = new Oid("1.3.6.1.5.5.7.3.4");

		// Token: 0x040030F8 RID: 12536
		public static readonly Oid SubjectKeyIdentifier = new Oid("2.5.29.14");

		// Token: 0x040030F9 RID: 12537
		public static readonly Oid AnyExtendedKeyUsage = new Oid("2.5.29.37.0");

		// Token: 0x040030FA RID: 12538
		public static readonly Oid ECPublicKey = new Oid("1.2.840.10045.2.1");
	}
}
