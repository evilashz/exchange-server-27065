using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x020003A9 RID: 937
	internal sealed class LanguageDropDownList : DropDownList
	{
		// Token: 0x0600234E RID: 9038 RVA: 0x000CB62D File Offset: 0x000C982D
		public LanguageDropDownList(string id, string selectedValue, LanguageDropDownListItem[] listItems) : base(id, selectedValue, listItems)
		{
			this.languageListItems = listItems;
		}

		// Token: 0x0600234F RID: 9039 RVA: 0x000CB640 File Offset: 0x000C9840
		protected override void RenderListItems(TextWriter writer)
		{
			foreach (LanguageDropDownListItem languageDropDownListItem in this.languageListItems)
			{
				DropDownList.RenderListItemHtml(writer, languageDropDownListItem.ItemValue, Utilities.SanitizeHtmlEncode(languageDropDownListItem.Display), languageDropDownListItem.ItemId, languageDropDownListItem.IsBold, languageDropDownListItem.IsRtl);
			}
		}

		// Token: 0x040018AC RID: 6316
		private LanguageDropDownListItem[] languageListItems;
	}
}
