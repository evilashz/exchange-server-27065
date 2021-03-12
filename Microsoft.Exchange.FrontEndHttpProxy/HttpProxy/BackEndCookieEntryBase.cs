using System;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x02000039 RID: 57
	internal abstract class BackEndCookieEntryBase
	{
		// Token: 0x060001B4 RID: 436 RVA: 0x00009F67 File Offset: 0x00008167
		protected BackEndCookieEntryBase(BackEndCookieEntryType entryType, ExDateTime expiryTime)
		{
			this.EntryType = entryType;
			this.ExpiryTime = expiryTime;
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x00009F7D File Offset: 0x0000817D
		// (set) Token: 0x060001B6 RID: 438 RVA: 0x00009F85 File Offset: 0x00008185
		public ExDateTime ExpiryTime { get; protected set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x00009F8E File Offset: 0x0000818E
		public bool Expired
		{
			get
			{
				return this.ExpiryTime < ExDateTime.UtcNow;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x00009FA0 File Offset: 0x000081A0
		// (set) Token: 0x060001B9 RID: 441 RVA: 0x00009FA8 File Offset: 0x000081A8
		public BackEndCookieEntryType EntryType { get; protected set; }

		// Token: 0x060001BA RID: 442 RVA: 0x00009FB1 File Offset: 0x000081B1
		public string ToObscureString()
		{
			return BackEndCookieEntryBase.Obscurify(this.ToString());
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00009FBE File Offset: 0x000081BE
		public virtual bool ShouldInvalidate(BackEndServer badTarget)
		{
			return false;
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00009FC4 File Offset: 0x000081C4
		internal static string ConvertBackEndCookieEntryTypeToString(BackEndCookieEntryType entryType)
		{
			switch (entryType)
			{
			case BackEndCookieEntryType.Server:
				return BackEndCookieEntryBase.BackEndCookieEntryTypeServerName;
			case BackEndCookieEntryType.Database:
				return BackEndCookieEntryBase.BackEndCookieEntryTypeDatabaseName;
			default:
				throw new InvalidOperationException(string.Format("Unknown cookie type: {0}", entryType));
			}
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000A004 File Offset: 0x00008204
		internal static bool TryGetBackEndCookieEntryTypeFromString(string entryTypeString, out BackEndCookieEntryType entryType)
		{
			if (string.Equals(entryTypeString, BackEndCookieEntryBase.BackEndCookieEntryTypeDatabaseName, StringComparison.OrdinalIgnoreCase))
			{
				entryType = BackEndCookieEntryType.Database;
				return true;
			}
			if (string.Equals(entryTypeString, BackEndCookieEntryBase.BackEndCookieEntryTypeServerName, StringComparison.OrdinalIgnoreCase))
			{
				entryType = BackEndCookieEntryType.Server;
				return true;
			}
			entryType = BackEndCookieEntryType.Server;
			return false;
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000A030 File Offset: 0x00008230
		protected static string Obscurify(string clearString)
		{
			byte[] bytes = BackEndCookieEntryBase.Encoding.GetBytes(clearString);
			byte[] array = new byte[bytes.Length];
			for (int i = 0; i < bytes.Length; i++)
			{
				byte[] array2 = array;
				int num = i;
				byte[] array3 = bytes;
				int num2 = i;
				array2[num] = (array3[num2] ^= BackEndCookieEntryBase.ObfuscateValue);
			}
			return Convert.ToBase64String(array);
		}

		// Token: 0x040000E0 RID: 224
		public const int MaxBackEndServerCookieEntries = 5;

		// Token: 0x040000E1 RID: 225
		protected const char Separator = '~';

		// Token: 0x040000E2 RID: 226
		public static readonly TimeSpan BackEndServerCookieLifeTime = TimeSpan.FromMinutes(10.0);

		// Token: 0x040000E3 RID: 227
		public static readonly TimeSpan LongLivedBackEndServerCookieLifeTime = TimeSpan.FromDays(30.0);

		// Token: 0x040000E4 RID: 228
		internal static readonly byte ObfuscateValue = byte.MaxValue;

		// Token: 0x040000E5 RID: 229
		internal static readonly ASCIIEncoding Encoding = new ASCIIEncoding();

		// Token: 0x040000E6 RID: 230
		internal static readonly string BackEndCookieEntryTypeServerName = string.Format(CultureInfo.InvariantCulture, "{0}", new object[]
		{
			Enum.GetName(typeof(BackEndCookieEntryType), BackEndCookieEntryType.Server)
		});

		// Token: 0x040000E7 RID: 231
		internal static readonly string BackEndCookieEntryTypeDatabaseName = string.Format(CultureInfo.InvariantCulture, "{0}", new object[]
		{
			Enum.GetName(typeof(BackEndCookieEntryType), BackEndCookieEntryType.Database)
		});
	}
}
