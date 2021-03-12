using System;
using System.IO;
using System.Web.UI;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020005F8 RID: 1528
	public class InlineHtmlTextWriter : HtmlTextWriter
	{
		// Token: 0x06004490 RID: 17552 RVA: 0x000CF129 File Offset: 0x000CD329
		public InlineHtmlTextWriter(TextWriter writer) : base(writer, string.Empty)
		{
		}

		// Token: 0x06004491 RID: 17553 RVA: 0x000CF137 File Offset: 0x000CD337
		public override void WriteLine()
		{
		}
	}
}
