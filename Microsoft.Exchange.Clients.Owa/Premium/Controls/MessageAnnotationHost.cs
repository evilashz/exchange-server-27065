using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x020003B2 RID: 946
	public abstract class MessageAnnotationHost : OwaPage
	{
		// Token: 0x0600239A RID: 9114 RVA: 0x000CCB27 File Offset: 0x000CAD27
		public static void RenderMessageAnnotationDivStart(TextWriter output, string messageNoteId)
		{
			output.Write("<div id=\"");
			Utilities.HtmlEncode(messageNoteId, output);
			output.Write("\"");
			output.Write(" class=\"divAnnot\"");
			output.Write(">");
		}

		// Token: 0x0600239B RID: 9115 RVA: 0x000CCB5C File Offset: 0x000CAD5C
		public static void RenderMessageAnnotationDivEnd(TextWriter output)
		{
			output.Write("</div>");
		}

		// Token: 0x0600239C RID: 9116 RVA: 0x000CCB69 File Offset: 0x000CAD69
		public static void RenderMessageAnnotationControl(TextWriter output, string messageNoteControlId, string messageNoteText)
		{
			output.Write("<textarea id=\"");
			Utilities.HtmlEncode(messageNoteControlId, output);
			output.Write(string.Format("\" class=\"Annot\">{0}</textarea>", messageNoteText));
		}
	}
}
