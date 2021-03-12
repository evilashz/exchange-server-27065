using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x020003DB RID: 987
	internal sealed class PercentCompleteDropDownList : DropDownList
	{
		// Token: 0x0600245E RID: 9310 RVA: 0x000D38BB File Offset: 0x000D1ABB
		public PercentCompleteDropDownList(string id, string percentComplete) : base(id, false, percentComplete, null)
		{
			this.percentComplete = percentComplete;
		}

		// Token: 0x0600245F RID: 9311 RVA: 0x000D38CE File Offset: 0x000D1ACE
		protected override void RenderExpandoData(TextWriter writer)
		{
			base.RenderExpandoData(writer);
			writer.Write(" L_InvldPc=\"");
			writer.Write(LocalizedStrings.GetHtmlEncoded(-1094601321));
			writer.Write("\"");
		}

		// Token: 0x06002460 RID: 9312 RVA: 0x000D38FD File Offset: 0x000D1AFD
		protected override void RenderSelectedValue(TextWriter writer)
		{
			writer.Write("<input type=\"text\" id=\"txtInput\" maxlength=\"3\" value=\"");
			writer.Write(this.percentComplete);
			writer.Write("\">");
		}

		// Token: 0x06002461 RID: 9313 RVA: 0x000D3921 File Offset: 0x000D1B21
		protected override void RenderListItems(TextWriter writer)
		{
		}

		// Token: 0x0400193F RID: 6463
		private string percentComplete;
	}
}
