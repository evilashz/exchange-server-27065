using System;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000093 RID: 147
	internal class Breadcrumb
	{
		// Token: 0x0600057C RID: 1404 RVA: 0x0001080F File Offset: 0x0000EA0F
		internal Breadcrumb(ExDateTime timestamp, string text)
		{
			this.Timestamp = timestamp;
			this.Text = text;
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x0600057D RID: 1405 RVA: 0x00010825 File Offset: 0x0000EA25
		// (set) Token: 0x0600057E RID: 1406 RVA: 0x0001082D File Offset: 0x0000EA2D
		internal ExDateTime Timestamp { get; private set; }

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x0600057F RID: 1407 RVA: 0x00010836 File Offset: 0x0000EA36
		// (set) Token: 0x06000580 RID: 1408 RVA: 0x0001083E File Offset: 0x0000EA3E
		internal string Text { get; private set; }

		// Token: 0x06000581 RID: 1409 RVA: 0x00010848 File Offset: 0x0000EA48
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(128);
			stringBuilder.Append(this.Timestamp.ToString("hh:mm:ss:ff", DateTimeFormatInfo.InvariantInfo));
			stringBuilder.Append(" ");
			stringBuilder.AppendLine((this.Text != null) ? this.Text : "<null>");
			return stringBuilder.ToString();
		}
	}
}
