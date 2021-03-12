using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x0200042B RID: 1067
	internal sealed class TimeZoneDropDownToolbarButton : ToolbarButton
	{
		// Token: 0x0600260B RID: 9739 RVA: 0x000DC425 File Offset: 0x000DA625
		public TimeZoneDropDownToolbarButton() : base("noaction", ToolbarButtonFlags.NoAction | ToolbarButtonFlags.HasInnerControl, -1018465893, ThemeFileId.None)
		{
		}

		// Token: 0x0600260C RID: 9740 RVA: 0x000DC440 File Offset: 0x000DA640
		public override void RenderControl(TextWriter writer)
		{
			List<DropDownListItem> list = new List<DropDownListItem>();
			ExTimeZone timeZone = OwaContext.Current.SessionContext.TimeZone;
			string selectedValue = string.Empty;
			foreach (ExTimeZone exTimeZone in ExTimeZoneEnumerator.Instance)
			{
				if (string.Equals(exTimeZone.Id, timeZone.Id, StringComparison.OrdinalIgnoreCase))
				{
					selectedValue = exTimeZone.Id;
				}
				list.Add(new DropDownListItem(exTimeZone.Id, exTimeZone.LocalizableDisplayName.ToString()));
			}
			DropDownList dropDownList = new DropDownList("divTimeZoneList", selectedValue, list.ToArray());
			dropDownList.Render(writer);
			writer.Write("<span id=\"divMeasure\" class=\"tbLh tbBefore tbAfter fltAfter\">");
			writer.Write(SanitizedHtmlString.FromStringId(2126414109));
			writer.Write("</span>");
		}
	}
}
