using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000338 RID: 824
	internal sealed class ColorPickerMenu : ContextMenu
	{
		// Token: 0x06001F36 RID: 7990 RVA: 0x000B3670 File Offset: 0x000B1870
		private ColorPickerMenu(UserContext userContext, string id) : base(id, userContext)
		{
		}

		// Token: 0x06001F37 RID: 7991 RVA: 0x000B367A File Offset: 0x000B187A
		internal static ColorPickerMenu Create(UserContext userContext, string id)
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			if (string.IsNullOrEmpty(id))
			{
				throw new ArgumentNullException("id");
			}
			return new ColorPickerMenu(userContext, id);
		}

		// Token: 0x06001F38 RID: 7992 RVA: 0x000B36A4 File Offset: 0x000B18A4
		protected override void RenderExpandoData(TextWriter output)
		{
			output.Write(" _colorIndexStart=\"");
			output.Write(CalendarColorManager.GetClientColorIndex(0));
			output.Write("\"");
		}

		// Token: 0x06001F39 RID: 7993 RVA: 0x000B36C8 File Offset: 0x000B18C8
		protected override void RenderMenuItems(TextWriter output)
		{
		}
	}
}
