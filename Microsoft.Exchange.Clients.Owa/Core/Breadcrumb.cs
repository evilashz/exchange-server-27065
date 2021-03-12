using System;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020000D8 RID: 216
	internal class Breadcrumb
	{
		// Token: 0x06000783 RID: 1923 RVA: 0x0003956D File Offset: 0x0003776D
		internal Breadcrumb(ExDateTime timestamp, string text)
		{
			this.timestamp = timestamp;
			this.text = text;
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000784 RID: 1924 RVA: 0x00039583 File Offset: 0x00037783
		internal ExDateTime Timestamp
		{
			get
			{
				return this.timestamp;
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000785 RID: 1925 RVA: 0x0003958B File Offset: 0x0003778B
		internal string Text
		{
			get
			{
				return this.text;
			}
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x00039594 File Offset: 0x00037794
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(128);
			stringBuilder.Append(this.Timestamp.ToString("hh:mm:ss:ff", DateTimeFormatInfo.InvariantInfo));
			stringBuilder.Append(" ");
			stringBuilder.AppendLine((this.Text != null) ? this.Text : "<null>");
			return stringBuilder.ToString();
		}

		// Token: 0x04000523 RID: 1315
		private string text;

		// Token: 0x04000524 RID: 1316
		private ExDateTime timestamp;
	}
}
