using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Logging
{
	// Token: 0x02000084 RID: 132
	internal class ProtocolLogRowFormatter : LogRowFormatter
	{
		// Token: 0x060003B8 RID: 952 RVA: 0x00010724 File Offset: 0x0000E924
		private static string[] InitializeEventString()
		{
			return new string[]
			{
				"+",
				"-",
				">",
				"<",
				"*"
			};
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x00010761 File Offset: 0x0000E961
		public ProtocolLogRowFormatter(LogSchema schema) : base(schema)
		{
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060003BA RID: 954 RVA: 0x0001076A File Offset: 0x0000E96A
		public override bool ShouldConvertEncoding
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0001076D File Offset: 0x0000E96D
		protected override byte[] Encode(object value)
		{
			if (value is ProtocolEvent)
			{
				return base.Encode(ProtocolLogRowFormatter.EventString[(int)value]);
			}
			return base.Encode(value);
		}

		// Token: 0x04000236 RID: 566
		private static readonly string[] EventString = ProtocolLogRowFormatter.InitializeEventString();
	}
}
