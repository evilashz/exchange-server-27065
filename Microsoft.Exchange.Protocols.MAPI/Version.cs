using System;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x02000097 RID: 151
	public struct Version
	{
		// Token: 0x0600056D RID: 1389 RVA: 0x00026FB4 File Offset: 0x000251B4
		public Version(ulong version)
		{
			this.version = version;
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x00026FBD File Offset: 0x000251BD
		public Version(ushort productMajor, ushort productMinor, ushort buildMajor, ushort buildMinor)
		{
			this.version = ((ulong)productMajor << 48 | (ulong)productMinor << 32 | (ulong)buildMajor << 16 | (ulong)buildMinor);
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x00026FDA File Offset: 0x000251DA
		public static bool operator >(Version left, Version right)
		{
			return left.version > right.version;
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x00026FEC File Offset: 0x000251EC
		public static bool operator <(Version left, Version right)
		{
			return left.version < right.version;
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000571 RID: 1393 RVA: 0x00026FFE File Offset: 0x000251FE
		public ulong Value
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000572 RID: 1394 RVA: 0x00027006 File Offset: 0x00025206
		public ushort ProductMajor
		{
			get
			{
				return (ushort)(this.version >> 48 & 65535UL);
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000573 RID: 1395 RVA: 0x00027019 File Offset: 0x00025219
		public ushort ProductMinor
		{
			get
			{
				return (ushort)(this.version >> 32 & 65535UL);
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000574 RID: 1396 RVA: 0x0002702C File Offset: 0x0002522C
		public ushort BuildMajor
		{
			get
			{
				return (ushort)(this.version >> 16 & 65535UL);
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000575 RID: 1397 RVA: 0x0002703F File Offset: 0x0002523F
		public ushort BuildMinor
		{
			get
			{
				return (ushort)(this.version & 65535UL);
			}
		}

		// Token: 0x04000326 RID: 806
		private readonly ulong version;

		// Token: 0x04000327 RID: 807
		public static Version Minimum = new Version(0UL);

		// Token: 0x04000328 RID: 808
		internal static readonly Version Exchange15MinVersion = new Version(15, 0, 0, 0);

		// Token: 0x04000329 RID: 809
		internal static readonly Version SupportsTableRowDeletedExtendedVersion = new Version(15, 0, 861, 0);
	}
}
