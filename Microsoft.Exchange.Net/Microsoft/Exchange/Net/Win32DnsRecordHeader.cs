using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000CA6 RID: 3238
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct Win32DnsRecordHeader
	{
		// Token: 0x170011E7 RID: 4583
		// (get) Token: 0x06004742 RID: 18242 RVA: 0x000BFF78 File Offset: 0x000BE178
		public DnsResponseSection Section
		{
			get
			{
				return (DnsResponseSection)(this.flags & 3U);
			}
		}

		// Token: 0x170011E8 RID: 4584
		// (get) Token: 0x06004743 RID: 18243 RVA: 0x000BFF84 File Offset: 0x000BE184
		public Encoding Encoding
		{
			get
			{
				switch (this.flags >> 3 & 3U)
				{
				case 1U:
					return Encoding.Unicode;
				case 2U:
					return Encoding.UTF8;
				case 3U:
					return Encoding.ASCII;
				default:
					return null;
				}
			}
		}

		// Token: 0x04003C60 RID: 15456
		public static readonly int MarshalSize = Marshal.SizeOf(typeof(Win32DnsRecordHeader));

		// Token: 0x04003C61 RID: 15457
		public IntPtr nextRecord;

		// Token: 0x04003C62 RID: 15458
		public string name;

		// Token: 0x04003C63 RID: 15459
		public DnsRecordType recordType;

		// Token: 0x04003C64 RID: 15460
		public ushort dataLength;

		// Token: 0x04003C65 RID: 15461
		private uint flags;

		// Token: 0x04003C66 RID: 15462
		public uint ttl;

		// Token: 0x04003C67 RID: 15463
		public uint reserved;
	}
}
