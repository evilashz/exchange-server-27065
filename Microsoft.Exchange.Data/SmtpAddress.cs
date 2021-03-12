using System;
using Microsoft.Exchange.Data.Mime.Internal;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000093 RID: 147
	[Serializable]
	public struct SmtpAddress : IEquatable<SmtpAddress>, IComparable<SmtpAddress>
	{
		// Token: 0x0600042D RID: 1069 RVA: 0x0000EFFD File Offset: 0x0000D1FD
		public SmtpAddress(string address)
		{
			this.address = (string.IsNullOrEmpty(address) ? null : address);
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0000F011 File Offset: 0x0000D211
		public SmtpAddress(byte[] address)
		{
			this.address = ((address != null && address.Length != 0) ? MimeInternalHelpers.BytesToString(address, true) : null);
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0000F02C File Offset: 0x0000D22C
		public SmtpAddress(string local, string domain)
		{
			if (local == null)
			{
				throw new ArgumentNullException("local");
			}
			if (domain != null)
			{
				this.address = local + "@" + domain;
				return;
			}
			if (local != SmtpAddress.NullReversePath.address)
			{
				throw new ArgumentNullException("domain");
			}
			this = SmtpAddress.NullReversePath;
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000430 RID: 1072 RVA: 0x0000F085 File Offset: 0x0000D285
		public int Length
		{
			get
			{
				if (this.address != null)
				{
					return this.address.Length;
				}
				return 0;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000431 RID: 1073 RVA: 0x0000F09C File Offset: 0x0000D29C
		public string Local
		{
			get
			{
				if (this.address == null)
				{
					return null;
				}
				int num;
				if (MimeInternalHelpers.IsValidSmtpAddress(this.address, true, out num, MimeInternalHelpers.IsEaiEnabled()))
				{
					return this.address.Substring(0, num - 1);
				}
				if (this.address == SmtpAddress.NullReversePath.address)
				{
					return this.address;
				}
				return null;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000432 RID: 1074 RVA: 0x0000F0F8 File Offset: 0x0000D2F8
		public string Domain
		{
			get
			{
				if (this.address == null)
				{
					return null;
				}
				int startIndex;
				if (!MimeInternalHelpers.IsValidSmtpAddress(this.address, true, out startIndex, MimeInternalHelpers.IsEaiEnabled()))
				{
					return null;
				}
				return this.address.Substring(startIndex);
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000433 RID: 1075 RVA: 0x0000F132 File Offset: 0x0000D332
		public string Address
		{
			get
			{
				return this.address;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000434 RID: 1076 RVA: 0x0000F13A File Offset: 0x0000D33A
		public bool IsUTF8
		{
			get
			{
				return SmtpAddress.IsUTF8Address(this.address);
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000435 RID: 1077 RVA: 0x0000F148 File Offset: 0x0000D348
		public bool IsValidAddress
		{
			get
			{
				int num;
				return this.address != null && (MimeInternalHelpers.IsValidSmtpAddress(this.address, true, out num, MimeInternalHelpers.IsEaiEnabled()) || this.address == SmtpAddress.NullReversePath.address);
			}
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x0000F18C File Offset: 0x0000D38C
		public static bool IsValidSmtpAddress(string address)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			int domainStart;
			return (MimeInternalHelpers.IsValidSmtpAddress(address, true, out domainStart, MimeInternalHelpers.IsEaiEnabled()) && !SmtpAddress.IsDomainIPLiteral(address, domainStart)) || address == SmtpAddress.NullReversePath.address;
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x0000F1D2 File Offset: 0x0000D3D2
		public static bool IsUTF8Address(string address)
		{
			return !MimeInternalHelpers.IsPureASCII(address);
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x0000F1DD File Offset: 0x0000D3DD
		public static bool IsValidDomain(string domain)
		{
			if (domain == null)
			{
				throw new ArgumentNullException("domain");
			}
			return MimeInternalHelpers.IsValidDomain(domain, 0, true, MimeInternalHelpers.IsEaiEnabled()) && !SmtpAddress.IsDomainIPLiteral(domain);
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x0000F208 File Offset: 0x0000D408
		public static SmtpAddress Parse(string address)
		{
			SmtpAddress result = new SmtpAddress(address);
			if (!result.IsValidAddress)
			{
				throw new FormatException(DataStrings.InvalidSmtpAddress(address));
			}
			return result;
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x0000F238 File Offset: 0x0000D438
		public static bool operator ==(SmtpAddress value1, SmtpAddress value2)
		{
			return value1.Equals(value2);
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x0000F242 File Offset: 0x0000D442
		public static bool operator !=(SmtpAddress value1, SmtpAddress value2)
		{
			return !(value1 == value2);
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x0000F24E File Offset: 0x0000D44E
		public static explicit operator string(SmtpAddress address)
		{
			return address.address ?? string.Empty;
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x0000F260 File Offset: 0x0000D460
		public static explicit operator SmtpAddress(string address)
		{
			return new SmtpAddress(address);
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x0000F268 File Offset: 0x0000D468
		public override int GetHashCode()
		{
			if (this.address == null)
			{
				return 0;
			}
			return StringComparer.OrdinalIgnoreCase.GetHashCode(this.address);
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x0000F284 File Offset: 0x0000D484
		public int CompareTo(SmtpAddress address)
		{
			return StringComparer.OrdinalIgnoreCase.Compare(this.address, address.address);
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x0000F2A0 File Offset: 0x0000D4A0
		public int CompareTo(object address)
		{
			if (address is SmtpAddress)
			{
				return this.CompareTo((SmtpAddress)address);
			}
			string text = address as string;
			if (text != null)
			{
				return string.Compare(this.address, text, StringComparison.OrdinalIgnoreCase);
			}
			if (this.address != null)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x0000F2E5 File Offset: 0x0000D4E5
		public byte[] GetBytes()
		{
			if (this.address == null)
			{
				return null;
			}
			return MimeInternalHelpers.StringToBytes(this.address, MimeInternalHelpers.IsEaiEnabled());
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x0000F304 File Offset: 0x0000D504
		public int GetBytes(byte[] array, int offset)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (0 > offset)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (this.address == null)
			{
				return 0;
			}
			int length = this.Length;
			if (length == 0)
			{
				return 0;
			}
			if (length > array.Length - offset)
			{
				throw new ArgumentException(DataStrings.InsufficientSpace);
			}
			return MimeInternalHelpers.StringToBytes(this.address, 0, length, array, offset, MimeInternalHelpers.IsEaiEnabled());
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x0000F370 File Offset: 0x0000D570
		public override bool Equals(object address)
		{
			if (address is SmtpAddress)
			{
				return this.Equals((SmtpAddress)address);
			}
			string text = address as string;
			if (text != null)
			{
				return string.Equals(this.address, text, StringComparison.OrdinalIgnoreCase);
			}
			return this.address == null;
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x0000F3B3 File Offset: 0x0000D5B3
		public bool Equals(SmtpAddress address)
		{
			return string.Equals(this.address, address.address, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x0000F3C8 File Offset: 0x0000D5C8
		public override string ToString()
		{
			return this.address ?? string.Empty;
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x0000F3D9 File Offset: 0x0000D5D9
		private static bool IsDomainIPLiteral(string domain)
		{
			return !string.IsNullOrEmpty(domain) && domain[0] == '[' && domain[domain.Length - 1] == ']';
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x0000F404 File Offset: 0x0000D604
		private static bool IsDomainIPLiteral(string address, int domainStart)
		{
			return !string.IsNullOrEmpty(address) && domainStart >= 0 && domainStart < address.Length && address[domainStart] == '[' && address[address.Length - 1] == ']';
		}

		// Token: 0x04000214 RID: 532
		public const int MaxLength = 571;

		// Token: 0x04000215 RID: 533
		public const int MaxEmailNameLength = 315;

		// Token: 0x04000216 RID: 534
		public static readonly SmtpAddress Empty = default(SmtpAddress);

		// Token: 0x04000217 RID: 535
		public static readonly SmtpAddress NullReversePath = new SmtpAddress("<>");

		// Token: 0x04000218 RID: 536
		private readonly string address;
	}
}
