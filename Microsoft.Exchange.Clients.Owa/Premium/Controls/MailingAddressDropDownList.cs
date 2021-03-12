using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x020003AB RID: 939
	internal sealed class MailingAddressDropDownList : DropDownList
	{
		// Token: 0x06002352 RID: 9042 RVA: 0x000CB6CC File Offset: 0x000C98CC
		public MailingAddressDropDownList(string id, PhysicalAddressType addressType)
		{
			int num = (int)addressType;
			base..ctor(id, num.ToString(), null);
			this.addressType = addressType;
		}

		// Token: 0x06002353 RID: 9043 RVA: 0x000CB6F1 File Offset: 0x000C98F1
		protected override void RenderSelectedValue(TextWriter writer)
		{
			Utilities.HtmlEncode(ContactUtilities.GetPhysicalAddressString(this.addressType), writer);
		}

		// Token: 0x06002354 RID: 9044 RVA: 0x000CB704 File Offset: 0x000C9904
		protected override DropDownListItem[] CreateListItems()
		{
			DropDownListItem[] array = new DropDownListItem[MailingAddressDropDownList.physicalAddressTypes.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new DropDownListItem((int)MailingAddressDropDownList.physicalAddressTypes[i], ContactUtilities.GetPhysicalAddressString(MailingAddressDropDownList.physicalAddressTypes[i]), false);
			}
			return array;
		}

		// Token: 0x040018AE RID: 6318
		private static readonly PhysicalAddressType[] physicalAddressTypes = new PhysicalAddressType[]
		{
			PhysicalAddressType.None,
			PhysicalAddressType.Home,
			PhysicalAddressType.Business,
			PhysicalAddressType.Other
		};

		// Token: 0x040018AF RID: 6319
		private PhysicalAddressType addressType;
	}
}
