using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Mime;
using System.Text;
using System.Web;

namespace Microsoft.Exchange.Net.WebApplicationClient
{
	// Token: 0x02000B21 RID: 2849
	internal class HtmlFormBody : TextBody, IEnumerable
	{
		// Token: 0x06003D7F RID: 15743 RVA: 0x000A02E8 File Offset: 0x0009E4E8
		public HtmlFormBody() : this(Encoding.UTF8)
		{
		}

		// Token: 0x06003D80 RID: 15744 RVA: 0x000A02F8 File Offset: 0x0009E4F8
		public HtmlFormBody(Encoding encoding)
		{
			base.ContentType = new ContentType("application/x-www-form-urlencoded")
			{
				CharSet = encoding.WebName
			};
			this.Encoding = encoding;
			this.Parameters = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x17000F33 RID: 3891
		// (get) Token: 0x06003D81 RID: 15745 RVA: 0x000A0340 File Offset: 0x0009E540
		// (set) Token: 0x06003D82 RID: 15746 RVA: 0x000A0348 File Offset: 0x0009E548
		public Encoding Encoding { get; private set; }

		// Token: 0x17000F34 RID: 3892
		// (get) Token: 0x06003D83 RID: 15747 RVA: 0x000A0351 File Offset: 0x0009E551
		// (set) Token: 0x06003D84 RID: 15748 RVA: 0x000A0359 File Offset: 0x0009E559
		public IDictionary<string, object> Parameters { get; private set; }

		// Token: 0x06003D85 RID: 15749 RVA: 0x000A0362 File Offset: 0x0009E562
		public void Add(string key, object value)
		{
			this.Parameters.Add(key, value);
		}

		// Token: 0x06003D86 RID: 15750 RVA: 0x000A0371 File Offset: 0x0009E571
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.Parameters.GetEnumerator();
		}

		// Token: 0x06003D87 RID: 15751 RVA: 0x000A0380 File Offset: 0x0009E580
		public override void Write(TextWriter writer)
		{
			bool flag = false;
			foreach (KeyValuePair<string, object> keyValuePair in this.Parameters)
			{
				if (keyValuePair.Value is Array)
				{
					Array array = (Array)keyValuePair.Value;
					for (int i = 0; i < array.Length; i++)
					{
						this.Write(writer, keyValuePair.Key, array.GetValue(i), ref flag);
					}
				}
				else
				{
					this.Write(writer, keyValuePair.Key, keyValuePair.Value, ref flag);
				}
			}
		}

		// Token: 0x06003D88 RID: 15752 RVA: 0x000A042C File Offset: 0x0009E62C
		private void Write(TextWriter writer, string name, object value, ref bool writeSeparator)
		{
			if (writeSeparator)
			{
				writer.Write('&');
			}
			writer.Write(HttpUtility.UrlEncode(name, this.Encoding));
			writer.Write('=');
			writer.Write(HttpUtility.UrlEncode(Convert.ToString(value, CultureInfo.InvariantCulture), this.Encoding));
			writeSeparator = true;
		}
	}
}
