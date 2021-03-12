using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000363 RID: 867
	internal sealed class DurationDropDownList : DropDownList
	{
		// Token: 0x06002097 RID: 8343 RVA: 0x000BCAE5 File Offset: 0x000BACE5
		private DurationDropDownList(int duration, string id) : base(id, false, null, null)
		{
			this.duration = duration;
		}

		// Token: 0x06002098 RID: 8344 RVA: 0x000BCAF8 File Offset: 0x000BACF8
		public static void RenderDurationPicker(TextWriter writer, int duration, string id)
		{
			DurationDropDownList durationDropDownList = new DurationDropDownList(duration, id);
			durationDropDownList.Render(writer);
		}

		// Token: 0x06002099 RID: 8345 RVA: 0x000BCB14 File Offset: 0x000BAD14
		protected override void RenderSelectedValue(TextWriter writer)
		{
			writer.Write("<input type=text id=\"txtInput\" value=\"");
			writer.Write(DateTimeUtilities.FormatDuration(this.duration));
			writer.Write("\">");
		}

		// Token: 0x0600209A RID: 8346 RVA: 0x000BCB3D File Offset: 0x000BAD3D
		protected override void RenderListItems(TextWriter writer)
		{
		}

		// Token: 0x0600209B RID: 8347 RVA: 0x000BCB3F File Offset: 0x000BAD3F
		protected override void RenderExpandoData(TextWriter writer)
		{
			base.RenderExpandoData(writer);
			writer.Write(" L_Dec=\"");
			writer.Write(SanitizedHtmlString.FromStringId(-1032346272));
			writer.Write("\"");
		}

		// Token: 0x04001774 RID: 6004
		private int duration;
	}
}
