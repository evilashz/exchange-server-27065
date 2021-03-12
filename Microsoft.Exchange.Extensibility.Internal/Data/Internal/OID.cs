using System;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Internal
{
	// Token: 0x02000034 RID: 52
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal struct OID : IComparable, IComparable<OID>
	{
		// Token: 0x060001EF RID: 495 RVA: 0x0000A3AF File Offset: 0x000085AF
		public OID(int t1, int t2)
		{
			OID.CheckT1T2(t1, t2);
			this.bytes = new byte[1];
			this.bytes[0] = (byte)(t1 * 40 + t2);
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000A3D4 File Offset: 0x000085D4
		public OID(int t1, int t2, int t3)
		{
			OID.CheckT1T2(t1, t2);
			int num = 1 + OID.GetTLength(t3);
			this.bytes = new byte[num];
			this.bytes[0] = (byte)(t1 * 40 + t2);
			int num2 = 1;
			num2 += OID.EncodeT(t3, this.bytes, num2);
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000A420 File Offset: 0x00008620
		public OID(int t1, int t2, int t3, int t4)
		{
			OID.CheckT1T2(t1, t2);
			int num = 1 + OID.GetTLength(t3) + OID.GetTLength(t4);
			this.bytes = new byte[num];
			this.bytes[0] = (byte)(t1 * 40 + t2);
			int num2 = 1;
			num2 += OID.EncodeT(t3, this.bytes, num2);
			num2 += OID.EncodeT(t4, this.bytes, num2);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000A484 File Offset: 0x00008684
		public OID(int t1, int t2, int t3, int t4, int t5)
		{
			OID.CheckT1T2(t1, t2);
			int num = 1 + OID.GetTLength(t3) + OID.GetTLength(t4) + OID.GetTLength(t5);
			this.bytes = new byte[num];
			this.bytes[0] = (byte)(t1 * 40 + t2);
			int num2 = 1;
			num2 += OID.EncodeT(t3, this.bytes, num2);
			num2 += OID.EncodeT(t4, this.bytes, num2);
			num2 += OID.EncodeT(t5, this.bytes, num2);
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000A500 File Offset: 0x00008700
		public OID(int t1, int t2, int t3, int t4, int t5, int t6)
		{
			OID.CheckT1T2(t1, t2);
			int num = 1 + OID.GetTLength(t3) + OID.GetTLength(t4) + OID.GetTLength(t5) + OID.GetTLength(t6);
			this.bytes = new byte[num];
			this.bytes[0] = (byte)(t1 * 40 + t2);
			int num2 = 1;
			num2 += OID.EncodeT(t3, this.bytes, num2);
			num2 += OID.EncodeT(t4, this.bytes, num2);
			num2 += OID.EncodeT(t5, this.bytes, num2);
			num2 += OID.EncodeT(t6, this.bytes, num2);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000A598 File Offset: 0x00008798
		public OID(int t1, int t2, int t3, int t4, int t5, int t6, int t7)
		{
			OID.CheckT1T2(t1, t2);
			int num = 1 + OID.GetTLength(t3) + OID.GetTLength(t4) + OID.GetTLength(t5) + OID.GetTLength(t6) + OID.GetTLength(t7);
			this.bytes = new byte[num];
			this.bytes[0] = (byte)(t1 * 40 + t2);
			int num2 = 1;
			num2 += OID.EncodeT(t3, this.bytes, num2);
			num2 += OID.EncodeT(t4, this.bytes, num2);
			num2 += OID.EncodeT(t5, this.bytes, num2);
			num2 += OID.EncodeT(t6, this.bytes, num2);
			num2 += OID.EncodeT(t7, this.bytes, num2);
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0000A648 File Offset: 0x00008848
		public OID(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8)
		{
			OID.CheckT1T2(t1, t2);
			int num = 1 + OID.GetTLength(t3) + OID.GetTLength(t4) + OID.GetTLength(t5) + OID.GetTLength(t6) + OID.GetTLength(t7) + OID.GetTLength(t8);
			this.bytes = new byte[num];
			this.bytes[0] = (byte)(t1 * 40 + t2);
			int num2 = 1;
			num2 += OID.EncodeT(t3, this.bytes, num2);
			num2 += OID.EncodeT(t4, this.bytes, num2);
			num2 += OID.EncodeT(t5, this.bytes, num2);
			num2 += OID.EncodeT(t6, this.bytes, num2);
			num2 += OID.EncodeT(t7, this.bytes, num2);
			num2 += OID.EncodeT(t8, this.bytes, num2);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0000A710 File Offset: 0x00008910
		public OID(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9)
		{
			OID.CheckT1T2(t1, t2);
			int num = 1 + OID.GetTLength(t3) + OID.GetTLength(t4) + OID.GetTLength(t5) + OID.GetTLength(t6) + OID.GetTLength(t7) + OID.GetTLength(t8) + OID.GetTLength(t9);
			this.bytes = new byte[num];
			this.bytes[0] = (byte)(t1 * 40 + t2);
			int num2 = 1;
			num2 += OID.EncodeT(t3, this.bytes, num2);
			num2 += OID.EncodeT(t4, this.bytes, num2);
			num2 += OID.EncodeT(t5, this.bytes, num2);
			num2 += OID.EncodeT(t6, this.bytes, num2);
			num2 += OID.EncodeT(t7, this.bytes, num2);
			num2 += OID.EncodeT(t8, this.bytes, num2);
			num2 += OID.EncodeT(t9, this.bytes, num2);
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0000A7F0 File Offset: 0x000089F0
		public OID(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9, int t10)
		{
			OID.CheckT1T2(t1, t2);
			int num = 1 + OID.GetTLength(t3) + OID.GetTLength(t4) + OID.GetTLength(t5) + OID.GetTLength(t6) + OID.GetTLength(t7) + OID.GetTLength(t8) + OID.GetTLength(t9) + OID.GetTLength(t10);
			this.bytes = new byte[num];
			this.bytes[0] = (byte)(t1 * 40 + t2);
			int num2 = 1;
			num2 += OID.EncodeT(t3, this.bytes, num2);
			num2 += OID.EncodeT(t4, this.bytes, num2);
			num2 += OID.EncodeT(t5, this.bytes, num2);
			num2 += OID.EncodeT(t6, this.bytes, num2);
			num2 += OID.EncodeT(t7, this.bytes, num2);
			num2 += OID.EncodeT(t8, this.bytes, num2);
			num2 += OID.EncodeT(t9, this.bytes, num2);
			num2 += OID.EncodeT(t10, this.bytes, num2);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000A8EC File Offset: 0x00008AEC
		public OID(params int[] t)
		{
			if (t.Length < 2)
			{
				throw new InvalidOperationException("invalid OID value - should have at least 2 components");
			}
			OID.CheckT1T2(t[0], t[1]);
			int num = 1;
			for (int i = 2; i < t.Length; i++)
			{
				num += OID.GetTLength(t[i]);
			}
			this.bytes = new byte[num];
			this.bytes[0] = (byte)(t[0] * 40 + t[1]);
			int num2 = 1;
			for (int j = 2; j < t.Length; j++)
			{
				num2 += OID.EncodeT(t[j], this.bytes, num2);
			}
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000A971 File Offset: 0x00008B71
		internal OID(byte[] buffer, int offset, int length)
		{
			this.bytes = new byte[length];
			Buffer.BlockCopy(buffer, offset, this.bytes, 0, length);
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000A98E File Offset: 0x00008B8E
		private OID(int t1)
		{
			this.bytes = null;
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001FB RID: 507 RVA: 0x0000A997 File Offset: 0x00008B97
		public int Length
		{
			get
			{
				if (this.bytes != null)
				{
					return this.bytes.Length;
				}
				return 0;
			}
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0000A9AB File Offset: 0x00008BAB
		public static bool operator ==(OID x, OID y)
		{
			return 0 == OID.CompareBytes(x.bytes, y.bytes);
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0000A9C3 File Offset: 0x00008BC3
		public static bool operator !=(OID x, OID y)
		{
			return !(x == y);
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000A9CF File Offset: 0x00008BCF
		public override bool Equals(object obj)
		{
			return obj is OID && this == (OID)obj;
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000A9EC File Offset: 0x00008BEC
		public override int GetHashCode()
		{
			if (this.bytes == null)
			{
				return 0;
			}
			int num = 0;
			for (int i = 0; i < this.bytes.Length; i++)
			{
				num = (num << 8 ^ (int)this.bytes[i] ^ num >> 24);
			}
			return num;
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000AA2B File Offset: 0x00008C2B
		public int CompareTo(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			if (!(obj is OID))
			{
				throw new ArgumentException("obj");
			}
			return this.CompareTo((OID)obj);
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000AA5A File Offset: 0x00008C5A
		public int CompareTo(OID other)
		{
			return OID.CompareBytes(this.bytes, other.bytes);
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000AA70 File Offset: 0x00008C70
		public override string ToString()
		{
			if (this.bytes == null)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(((int)(this.bytes[0] / 40)).ToString());
			stringBuilder.Append(".");
			stringBuilder.Append(((int)(this.bytes[0] % 40)).ToString());
			int num = 1;
			while (num != this.bytes.Length)
			{
				int num2 = 0;
				byte b;
				do
				{
					b = this.bytes[num++];
					num2 = (num2 << 7) + (int)(b & 127);
				}
				while ((b & 128) != 0 && num < this.bytes.Length);
				stringBuilder.Append(".");
				stringBuilder.Append(num2.ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000AB30 File Offset: 0x00008D30
		private static int CompareBytes(byte[] x, byte[] y)
		{
			if (x == y)
			{
				return 0;
			}
			if (y == null)
			{
				return 1;
			}
			if (x == null)
			{
				return -1;
			}
			for (int i = 0; i < x.Length; i++)
			{
				if (i >= y.Length)
				{
					return 1;
				}
				if (x[i] > y[i])
				{
					return -1;
				}
				if (x[i] < y[i])
				{
					return -1;
				}
			}
			if (x.Length < y.Length)
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000AB82 File Offset: 0x00008D82
		private static void CheckT1T2(int t1, int t2)
		{
			if (t1 >= 3 || t2 >= 40)
			{
				throw new InvalidOperationException("invalid OID value - first two elements");
			}
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000AB98 File Offset: 0x00008D98
		private static int GetTLength(int t)
		{
			if (t >= 0)
			{
				if (t <= 127)
				{
					return 1;
				}
				if (t <= 16383)
				{
					return 2;
				}
				if (t <= 2097151)
				{
					return 3;
				}
				if (t <= 268435455)
				{
					return 4;
				}
			}
			throw new InvalidOperationException("invalid OID value - element is negative or too large");
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0000ABD0 File Offset: 0x00008DD0
		private static int EncodeT(int t, byte[] buffer, int offset)
		{
			int tlength = OID.GetTLength(t);
			int num = OID.Masks[tlength - 1];
			for (int num2 = OID.Shifts[tlength - 1]; num2 != 0; num2 -= 7)
			{
				buffer[offset++] = (byte)((t & num) >> num2 | 128);
				num >>= 7;
			}
			buffer[offset] = (byte)(t & 127);
			return tlength;
		}

		// Token: 0x040001EB RID: 491
		private static readonly int[] Masks = new int[]
		{
			127,
			16256,
			2080768,
			266338304
		};

		// Token: 0x040001EC RID: 492
		private static readonly int[] Shifts = new int[]
		{
			0,
			7,
			14,
			21
		};

		// Token: 0x040001ED RID: 493
		public static readonly OID DS = new OID(2, 5);

		// Token: 0x040001EE RID: 494
		public static readonly OID DSALG = new OID(2, 5, 8);

		// Token: 0x040001EF RID: 495
		public static readonly OID DSALGCRPT = new OID(2, 5, 8, 1);

		// Token: 0x040001F0 RID: 496
		public static readonly OID DSALGHash = new OID(2, 5, 8, 2);

		// Token: 0x040001F1 RID: 497
		public static readonly OID DSALGSign = new OID(2, 5, 8, 3);

		// Token: 0x040001F2 RID: 498
		public static readonly OID DSALGRSA = new OID(2, 5, 8, 1, 1);

		// Token: 0x040001F3 RID: 499
		public static readonly OID AuthorityKeyIdentifier = new OID(2, 5, 29, 1);

		// Token: 0x040001F4 RID: 500
		public static readonly OID KeyUsageRestriction = new OID(2, 5, 29, 4);

		// Token: 0x040001F5 RID: 501
		public static readonly OID LegacyPolicyMappings = new OID(2, 5, 29, 5);

		// Token: 0x040001F6 RID: 502
		public static readonly OID SubjectALTName = new OID(2, 5, 29, 7);

		// Token: 0x040001F7 RID: 503
		public static readonly OID IssuerALTName = new OID(2, 5, 29, 8);

		// Token: 0x040001F8 RID: 504
		public static readonly OID BasicConstraints = new OID(2, 5, 29, 10);

		// Token: 0x040001F9 RID: 505
		public static readonly OID SubjectKeyIdentifier = new OID(2, 5, 29, 14);

		// Token: 0x040001FA RID: 506
		public static readonly OID KeyUsage = new OID(2, 5, 29, 15);

		// Token: 0x040001FB RID: 507
		public static readonly OID SubjectALTName2 = new OID(2, 5, 29, 17);

		// Token: 0x040001FC RID: 508
		public static readonly OID IssuerALTName2 = new OID(2, 5, 29, 18);

		// Token: 0x040001FD RID: 509
		public static readonly OID BasicConstraints2 = new OID(2, 5, 29, 19);

		// Token: 0x040001FE RID: 510
		public static readonly OID CRLNumber = new OID(2, 5, 29, 20);

		// Token: 0x040001FF RID: 511
		public static readonly OID CRLReasonCode = new OID(2, 5, 29, 21);

		// Token: 0x04000200 RID: 512
		public static readonly OID ReasonCodeHold = new OID(2, 5, 29, 23);

		// Token: 0x04000201 RID: 513
		public static readonly OID DeltaCRLIndicator = new OID(2, 5, 29, 27);

		// Token: 0x04000202 RID: 514
		public static readonly OID IssuingDISTPoint = new OID(2, 5, 29, 28);

		// Token: 0x04000203 RID: 515
		public static readonly OID NameConstraints = new OID(2, 5, 29, 30);

		// Token: 0x04000204 RID: 516
		public static readonly OID CRLDISTPoints = new OID(2, 5, 29, 31);

		// Token: 0x04000205 RID: 517
		public static readonly OID CertPolicies = new OID(2, 5, 29, 32);

		// Token: 0x04000206 RID: 518
		public static readonly OID PolicyMappings = new OID(2, 5, 29, 33);

		// Token: 0x04000207 RID: 519
		public static readonly OID AuthorityKeyIdentifier2 = new OID(2, 5, 29, 35);

		// Token: 0x04000208 RID: 520
		public static readonly OID PolicyConstraints = new OID(2, 5, 29, 36);

		// Token: 0x04000209 RID: 521
		public static readonly OID EnhancedKeyUsage = new OID(2, 5, 29, 37);

		// Token: 0x0400020A RID: 522
		public static readonly OID FreshestCRL = new OID(2, 5, 29, 46);

		// Token: 0x0400020B RID: 523
		public static readonly OID MicrosoftEncryptionKeyPreference = new OID(1, 3, 6, 1, 4, 1, 311, 16, 4);

		// Token: 0x0400020C RID: 524
		public static readonly OID EnrollCerttypeExtension = new OID(1, 3, 6, 1, 4, 1, 311, 20, 2);

		// Token: 0x0400020D RID: 525
		public static readonly OID NTPrincipalName = new OID(1, 3, 6, 1, 4, 1, 311, 20, 2, 3);

		// Token: 0x0400020E RID: 526
		public static readonly OID CertificateTemplate = new OID(1, 3, 6, 1, 4, 1, 311, 21, 7);

		// Token: 0x0400020F RID: 527
		public static readonly OID RDNDummySigner = new OID(1, 3, 6, 1, 4, 1, 311, 21, 9);

		// Token: 0x04000210 RID: 528
		public static readonly OID AuthorityInfoAccess = new OID(1, 3, 6, 1, 5, 5, 7, 1, 1);

		// Token: 0x04000211 RID: 529
		public static readonly OID RSA = new OID(1, 2, 840, 113549);

		// Token: 0x04000212 RID: 530
		public static readonly OID PKCS = new OID(1, 2, 840, 113549, 1);

		// Token: 0x04000213 RID: 531
		public static readonly OID RSAHASH = new OID(1, 2, 840, 113549, 2);

		// Token: 0x04000214 RID: 532
		public static readonly OID RSAENCRYPT = new OID(1, 2, 840, 113549, 3);

		// Token: 0x04000215 RID: 533
		public static readonly OID PKCS1 = new OID(1, 2, 840, 113549, 1, 1);

		// Token: 0x04000216 RID: 534
		public static readonly OID PKCS2 = new OID(1, 2, 840, 113549, 1, 2);

		// Token: 0x04000217 RID: 535
		public static readonly OID PKCS3 = new OID(1, 2, 840, 113549, 1, 3);

		// Token: 0x04000218 RID: 536
		public static readonly OID PKCS4 = new OID(1, 2, 840, 113549, 1, 4);

		// Token: 0x04000219 RID: 537
		public static readonly OID PKCS5 = new OID(1, 2, 840, 113549, 1, 5);

		// Token: 0x0400021A RID: 538
		public static readonly OID PKCS6 = new OID(1, 2, 840, 113549, 1, 6);

		// Token: 0x0400021B RID: 539
		public static readonly OID PKCS7 = new OID(1, 2, 840, 113549, 1, 7);

		// Token: 0x0400021C RID: 540
		public static readonly OID PKCS8 = new OID(1, 2, 840, 113549, 1, 8);

		// Token: 0x0400021D RID: 541
		public static readonly OID PKCS9 = new OID(1, 2, 840, 113549, 1, 9);

		// Token: 0x0400021E RID: 542
		public static readonly OID PKCS10 = new OID(1, 2, 840, 113549, 1, 10);

		// Token: 0x0400021F RID: 543
		public static readonly OID PKCS12 = new OID(1, 2, 840, 113549, 1, 12);

		// Token: 0x04000220 RID: 544
		public static readonly OID RSARSA = new OID(1, 2, 840, 113549, 1, 1, 1);

		// Token: 0x04000221 RID: 545
		public static readonly OID RSAMD2RSA = new OID(1, 2, 840, 113549, 1, 1, 2);

		// Token: 0x04000222 RID: 546
		public static readonly OID RSAMD4RSA = new OID(1, 2, 840, 113549, 1, 1, 3);

		// Token: 0x04000223 RID: 547
		public static readonly OID RSAMD5RSA = new OID(1, 2, 840, 113549, 1, 1, 4);

		// Token: 0x04000224 RID: 548
		public static readonly OID RSASHA1RSA = new OID(1, 2, 840, 113549, 1, 1, 5);

		// Token: 0x04000225 RID: 549
		public static readonly OID RSASETOAEPRSA = new OID(1, 2, 840, 113549, 1, 1, 6);

		// Token: 0x04000226 RID: 550
		public static readonly OID RSADH = new OID(1, 2, 840, 113549, 1, 3, 1);

		// Token: 0x04000227 RID: 551
		public static readonly OID RSAData = new OID(1, 2, 840, 113549, 1, 7, 1);

		// Token: 0x04000228 RID: 552
		public static readonly OID RSASignedData = new OID(1, 2, 840, 113549, 1, 7, 2);

		// Token: 0x04000229 RID: 553
		public static readonly OID RSAEnvelopedData = new OID(1, 2, 840, 113549, 1, 7, 3);

		// Token: 0x0400022A RID: 554
		public static readonly OID RSASignEnvData = new OID(1, 2, 840, 113549, 1, 7, 4);

		// Token: 0x0400022B RID: 555
		public static readonly OID RSADigestedData = new OID(1, 2, 840, 113549, 1, 7, 5);

		// Token: 0x0400022C RID: 556
		public static readonly OID RSAHashedData = new OID(1, 2, 840, 113549, 1, 7, 5);

		// Token: 0x0400022D RID: 557
		public static readonly OID RSAEncryptedData = new OID(1, 2, 840, 113549, 1, 7, 6);

		// Token: 0x0400022E RID: 558
		public static readonly OID RSAEmailAddr = new OID(1, 2, 840, 113549, 1, 9, 1);

		// Token: 0x0400022F RID: 559
		public static readonly OID RSAUnstructName = new OID(1, 2, 840, 113549, 1, 9, 2);

		// Token: 0x04000230 RID: 560
		public static readonly OID RSAContentType = new OID(1, 2, 840, 113549, 1, 9, 3);

		// Token: 0x04000231 RID: 561
		public static readonly OID RSAMessageDigest = new OID(1, 2, 840, 113549, 1, 9, 4);

		// Token: 0x04000232 RID: 562
		public static readonly OID RSASigningTime = new OID(1, 2, 840, 113549, 1, 9, 5);

		// Token: 0x04000233 RID: 563
		public static readonly OID RSACounterSign = new OID(1, 2, 840, 113549, 1, 9, 6);

		// Token: 0x04000234 RID: 564
		public static readonly OID RSAChallengePwd = new OID(1, 2, 840, 113549, 1, 9, 7);

		// Token: 0x04000235 RID: 565
		public static readonly OID RSAUnstructAddr = new OID(1, 2, 840, 113549, 1, 9, 8);

		// Token: 0x04000236 RID: 566
		public static readonly OID RSAExtCertAttrs = new OID(1, 2, 840, 113549, 1, 9, 9);

		// Token: 0x04000237 RID: 567
		public static readonly OID RSACertExtensions = new OID(1, 2, 840, 113549, 1, 9, 14);

		// Token: 0x04000238 RID: 568
		public static readonly OID RSASMIMECapabilities = new OID(1, 2, 840, 113549, 1, 9, 15);

		// Token: 0x04000239 RID: 569
		public static readonly OID RSAPreferSignedData = new OID(1, 2, 840, 113549, 1, 9, 15, 1);

		// Token: 0x0400023A RID: 570
		public static readonly OID OIWSESha1 = new OID(1, 3, 14, 3, 2, 26);

		// Token: 0x0400023B RID: 571
		public static readonly OID RSAMD2 = new OID(1, 2, 840, 113549, 2, 2);

		// Token: 0x0400023C RID: 572
		public static readonly OID RSAMD4 = new OID(1, 2, 840, 113549, 2, 4);

		// Token: 0x0400023D RID: 573
		public static readonly OID RSAMD5 = new OID(1, 2, 840, 113549, 2, 5);

		// Token: 0x0400023E RID: 574
		public static readonly OID OIWSECSHA256 = new OID(2, 16, 840, 1, 101, 3, 4, 1);

		// Token: 0x0400023F RID: 575
		public static readonly OID OIWSECSHA384 = new OID(2, 16, 840, 1, 101, 3, 4, 2);

		// Token: 0x04000240 RID: 576
		public static readonly OID OIWSECSHA512 = new OID(2, 16, 840, 1, 101, 3, 4, 3);

		// Token: 0x04000241 RID: 577
		public static readonly OID RSARC2CBC = new OID(1, 2, 840, 113549, 3, 2);

		// Token: 0x04000242 RID: 578
		public static readonly OID RSARC4 = new OID(1, 2, 840, 113549, 3, 4);

		// Token: 0x04000243 RID: 579
		public static readonly OID RSADESEDE3CBC = new OID(1, 2, 840, 113549, 3, 7);

		// Token: 0x04000244 RID: 580
		public static readonly OID RSARC5CBCPad = new OID(1, 2, 840, 113549, 3, 9);

		// Token: 0x04000245 RID: 581
		public static readonly OID OIWSECDesCBC = new OID(1, 3, 14, 3, 2, 7);

		// Token: 0x04000246 RID: 582
		public static readonly OID RSASMIMEalg = new OID(1, 2, 840, 113549, 1, 9, 16, 3);

		// Token: 0x04000247 RID: 583
		public static readonly OID RSASMIMEalgESDH = new OID(1, 2, 840, 113549, 1, 9, 16, 3, 5);

		// Token: 0x04000248 RID: 584
		public static readonly OID RSASMIMEalgCMS3DESwrap = new OID(1, 2, 840, 113549, 1, 9, 16, 3, 6);

		// Token: 0x04000249 RID: 585
		public static readonly OID RSASMIMEalgCMSRC2wrap = new OID(1, 2, 840, 113549, 1, 9, 16, 3, 7);

		// Token: 0x0400024A RID: 586
		public static readonly OID OIWSECSha1RSASign = new OID(1, 3, 14, 3, 2, 29);

		// Token: 0x0400024B RID: 587
		public static readonly OID AnsiX942 = new OID(1, 2, 840, 10046);

		// Token: 0x0400024C RID: 588
		public static readonly OID AnsiX942DH = new OID(1, 2, 840, 10046, 2, 1);

		// Token: 0x0400024D RID: 589
		public static readonly OID X957 = new OID(1, 2, 840, 10040);

		// Token: 0x0400024E RID: 590
		public static readonly OID X957DSA = new OID(1, 2, 840, 10040, 4, 1);

		// Token: 0x0400024F RID: 591
		public static readonly OID X957SHA1DSA = new OID(1, 2, 840, 10040, 4, 3);

		// Token: 0x04000250 RID: 592
		public static readonly OID CommonName = new OID(2, 5, 4, 3);

		// Token: 0x04000251 RID: 593
		public static readonly OID SurName = new OID(2, 5, 4, 4);

		// Token: 0x04000252 RID: 594
		public static readonly OID DeviceSerialNumber = new OID(2, 5, 4, 5);

		// Token: 0x04000253 RID: 595
		public static readonly OID CountryName = new OID(2, 5, 4, 6);

		// Token: 0x04000254 RID: 596
		public static readonly OID LocalityName = new OID(2, 5, 4, 7);

		// Token: 0x04000255 RID: 597
		public static readonly OID StateOrProvinceName = new OID(2, 5, 4, 8);

		// Token: 0x04000256 RID: 598
		public static readonly OID StreetAddress = new OID(2, 5, 4, 9);

		// Token: 0x04000257 RID: 599
		public static readonly OID OrganizationName = new OID(2, 5, 4, 10);

		// Token: 0x04000258 RID: 600
		public static readonly OID OrganizationalUnitName = new OID(2, 5, 4, 11);

		// Token: 0x04000259 RID: 601
		public static readonly OID TITLE = new OID(2, 5, 4, 12);

		// Token: 0x0400025A RID: 602
		public static readonly OID DESCRIPTION = new OID(2, 5, 4, 13);

		// Token: 0x0400025B RID: 603
		public static readonly OID SearchGuide = new OID(2, 5, 4, 14);

		// Token: 0x0400025C RID: 604
		public static readonly OID BusinessCategory = new OID(2, 5, 4, 15);

		// Token: 0x0400025D RID: 605
		public static readonly OID PostalAddress = new OID(2, 5, 4, 16);

		// Token: 0x0400025E RID: 606
		public static readonly OID PostalCode = new OID(2, 5, 4, 17);

		// Token: 0x0400025F RID: 607
		public static readonly OID PostOfficeBox = new OID(2, 5, 4, 18);

		// Token: 0x04000260 RID: 608
		public static readonly OID PhysicalDeliveryOfficeName = new OID(2, 5, 4, 19);

		// Token: 0x04000261 RID: 609
		public static readonly OID TelephoneNumber = new OID(2, 5, 4, 20);

		// Token: 0x04000262 RID: 610
		public static readonly OID TelexNumber = new OID(2, 5, 4, 21);

		// Token: 0x04000263 RID: 611
		public static readonly OID TeletextTerminalIdentifier = new OID(2, 5, 4, 22);

		// Token: 0x04000264 RID: 612
		public static readonly OID FacsimileTelephoneNumber = new OID(2, 5, 4, 23);

		// Token: 0x04000265 RID: 613
		public static readonly OID X21Address = new OID(2, 5, 4, 24);

		// Token: 0x04000266 RID: 614
		public static readonly OID InternationalISDNNumber = new OID(2, 5, 4, 25);

		// Token: 0x04000267 RID: 615
		public static readonly OID RegisteredAddress = new OID(2, 5, 4, 26);

		// Token: 0x04000268 RID: 616
		public static readonly OID DestinationIndicator = new OID(2, 5, 4, 27);

		// Token: 0x04000269 RID: 617
		public static readonly OID PreferredDeliveryMethod = new OID(2, 5, 4, 28);

		// Token: 0x0400026A RID: 618
		public static readonly OID PresentationAddress = new OID(2, 5, 4, 29);

		// Token: 0x0400026B RID: 619
		public static readonly OID SupportedApplicationContext = new OID(2, 5, 4, 30);

		// Token: 0x0400026C RID: 620
		public static readonly OID MEMBER = new OID(2, 5, 4, 31);

		// Token: 0x0400026D RID: 621
		public static readonly OID OWNER = new OID(2, 5, 4, 32);

		// Token: 0x0400026E RID: 622
		public static readonly OID RoleOccupant = new OID(2, 5, 4, 33);

		// Token: 0x0400026F RID: 623
		public static readonly OID SeeAlso = new OID(2, 5, 4, 34);

		// Token: 0x04000270 RID: 624
		public static readonly OID UserPassword = new OID(2, 5, 4, 35);

		// Token: 0x04000271 RID: 625
		public static readonly OID UserCertificate = new OID(2, 5, 4, 36);

		// Token: 0x04000272 RID: 626
		public static readonly OID CACertificate = new OID(2, 5, 4, 37);

		// Token: 0x04000273 RID: 627
		public static readonly OID AuthorityRevocationList = new OID(2, 5, 4, 38);

		// Token: 0x04000274 RID: 628
		public static readonly OID CertificateRevocationList = new OID(2, 5, 4, 39);

		// Token: 0x04000275 RID: 629
		public static readonly OID CrossCertificatePair = new OID(2, 5, 4, 40);

		// Token: 0x04000276 RID: 630
		public static readonly OID GivenName = new OID(2, 5, 4, 42);

		// Token: 0x04000277 RID: 631
		public static readonly OID INITIALS = new OID(2, 5, 4, 43);

		// Token: 0x04000278 RID: 632
		public static readonly OID DnQualifier = new OID(2, 5, 4, 46);

		// Token: 0x04000279 RID: 633
		public static readonly OID DomainComponent = new OID(0, 9, 2342, 19200300, 100, 1, 25);

		// Token: 0x0400027A RID: 634
		public static readonly OID Pkcs12FriendlyNameAttr = new OID(1, 2, 840, 113549, 1, 9, 20);

		// Token: 0x0400027B RID: 635
		public static readonly OID Pkcs12LocalKeyID = new OID(1, 2, 840, 113549, 1, 9, 21);

		// Token: 0x0400027C RID: 636
		public static readonly OID Pkcs12KeyProviderNameAttr = new OID(1, 3, 6, 1, 4, 1, 311, 17, 1);

		// Token: 0x0400027D RID: 637
		public static readonly OID LocalMachineKeyset = new OID(1, 3, 6, 1, 4, 1, 311, 17, 2);

		// Token: 0x0400027E RID: 638
		public static readonly OID KeyIdRDN = new OID(1, 3, 6, 1, 4, 1, 311, 10, 7, 1);

		// Token: 0x0400027F RID: 639
		private byte[] bytes;
	}
}
