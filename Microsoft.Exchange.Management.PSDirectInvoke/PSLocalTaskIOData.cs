using System;
using System.Globalization;

namespace Microsoft.Exchange.Management.PSDirectInvoke
{
	// Token: 0x02000008 RID: 8
	internal sealed class PSLocalTaskIOData
	{
		// Token: 0x06000079 RID: 121 RVA: 0x00003335 File Offset: 0x00001535
		public PSLocalTaskIOData(PSLocalTaskIOType type, DateTime when, string data)
		{
			this.Type = type;
			this.When = when;
			this.Data = data;
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00003352 File Offset: 0x00001552
		// (set) Token: 0x0600007B RID: 123 RVA: 0x0000335A File Offset: 0x0000155A
		public PSLocalTaskIOType Type { get; private set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00003363 File Offset: 0x00001563
		// (set) Token: 0x0600007D RID: 125 RVA: 0x0000336B File Offset: 0x0000156B
		public DateTime When { get; private set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00003374 File Offset: 0x00001574
		// (set) Token: 0x0600007F RID: 127 RVA: 0x0000337C File Offset: 0x0000157C
		public string Data { get; private set; }

		// Token: 0x06000080 RID: 128 RVA: 0x00003388 File Offset: 0x00001588
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				this.Type,
				":",
				this.When.ToLocalTime().ToString("yyyy/MM/dd:HH:mm:ss.fff", CultureInfo.InvariantCulture),
				" ",
				this.Data
			});
		}
	}
}
