using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Clients.Owa.Basic.Controls
{
	// Token: 0x02000009 RID: 9
	internal class AddressBookItemList : ListViewContents
	{
		// Token: 0x0600004E RID: 78 RVA: 0x00004002 File Offset: 0x00002202
		public AddressBookItemList(ViewDescriptor viewDescriptor, ColumnId sortedColumn, SortOrder sortOrder, UserContext userContext) : base(viewDescriptor, sortedColumn, sortOrder, false, userContext)
		{
			base.AddProperty(ADObjectSchema.ObjectCategory);
			base.AddProperty(ADRecipientSchema.RecipientType);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00004028 File Offset: 0x00002228
		protected override bool RenderItemRowStyle(TextWriter writer, int itemIndex)
		{
			RecipientType recipientType = (RecipientType)base.DataSource.GetItemProperty(itemIndex, ADRecipientSchema.RecipientType);
			return Utilities.IsADDistributionList(recipientType);
		}
	}
}
