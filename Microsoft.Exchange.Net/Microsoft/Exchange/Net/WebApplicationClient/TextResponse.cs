using System;
using System.IO;
using System.Net;

namespace Microsoft.Exchange.Net.WebApplicationClient
{
	// Token: 0x02000B18 RID: 2840
	internal class TextResponse : WebApplicationResponse
	{
		// Token: 0x06003D5E RID: 15710 RVA: 0x0009FFA0 File Offset: 0x0009E1A0
		public override void SetResponse(HttpWebResponse response)
		{
			base.SetResponse(response);
			using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
			{
				this.Text = streamReader.ReadToEnd();
			}
		}

		// Token: 0x17000F27 RID: 3879
		// (get) Token: 0x06003D5F RID: 15711 RVA: 0x0009FFE8 File Offset: 0x0009E1E8
		// (set) Token: 0x06003D60 RID: 15712 RVA: 0x0009FFF0 File Offset: 0x0009E1F0
		public string Text { get; private set; }

		// Token: 0x06003D61 RID: 15713 RVA: 0x0009FFF9 File Offset: 0x0009E1F9
		protected bool Contains(string text)
		{
			return this.Text.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0;
		}
	}
}
