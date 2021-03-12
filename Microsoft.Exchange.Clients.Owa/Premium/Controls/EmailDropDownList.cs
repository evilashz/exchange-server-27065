using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000372 RID: 882
	internal sealed class EmailDropDownList : DropDownList
	{
		// Token: 0x0600210C RID: 8460 RVA: 0x000BE1C2 File Offset: 0x000BC3C2
		public EmailDropDownList(string id, ContactPropertyInfo selectedEmailProperty) : base(id, selectedEmailProperty.Id, null)
		{
			this.selectedEmailProperty = selectedEmailProperty;
		}

		// Token: 0x0600210D RID: 8461 RVA: 0x000BE1D9 File Offset: 0x000BC3D9
		protected override void RenderSelectedValue(TextWriter writer)
		{
			writer.Write(LocalizedStrings.GetHtmlEncoded(this.selectedEmailProperty.Label));
		}

		// Token: 0x0600210E RID: 8462 RVA: 0x000BE1F4 File Offset: 0x000BC3F4
		protected override DropDownListItem[] CreateListItems()
		{
			int num = ContactUtilities.EmailAddressProperties.Length;
			DropDownListItem[] array = new DropDownListItem[num];
			for (int i = 0; i < num; i++)
			{
				ContactPropertyInfo contactPropertyInfo = ContactUtilities.EmailAddressProperties[i];
				array[i] = new DropDownListItem(contactPropertyInfo.Id, contactPropertyInfo.Label);
			}
			return array;
		}

		// Token: 0x0400179C RID: 6044
		private ContactPropertyInfo selectedEmailProperty;
	}
}
