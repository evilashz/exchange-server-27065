using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Basic.Controls
{
	// Token: 0x02000065 RID: 101
	internal sealed class MessageListViewContents : ListViewContents
	{
		// Token: 0x060002D3 RID: 723 RVA: 0x00018A08 File Offset: 0x00016C08
		public MessageListViewContents(ViewDescriptor viewDescriptor, ColumnId sortedColumn, SortOrder sortOrder, bool showFolderNameTooltip, UserContext userContext) : base(viewDescriptor, sortedColumn, sortOrder, showFolderNameTooltip, userContext)
		{
			base.AddProperty(MessageItemSchema.IsRead);
			base.AddProperty(MessageItemSchema.IsDraft);
			base.AddProperty(ItemSchema.IconIndex);
			base.AddProperty(MessageItemSchema.MessageInConflict);
			base.AddProperty(MessageItemSchema.HasBeenSubmitted);
			base.AddProperty(ItemSchema.Id);
			base.AddProperty(StoreObjectSchema.ItemClass);
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x00018A70 File Offset: 0x00016C70
		protected override bool RenderItemRowStyle(TextWriter writer, int itemIndex)
		{
			bool flag = true;
			object itemProperty = base.DataSource.GetItemProperty(itemIndex, MessageItemSchema.IsRead);
			if (itemProperty is bool)
			{
				flag = (bool)itemProperty;
			}
			if (!flag)
			{
				writer.Write(" style=\"font-weight:bold;\"");
				return true;
			}
			return false;
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x00018AB4 File Offset: 0x00016CB4
		protected override bool IsItemForCompose(int itemIndex)
		{
			string itemPropertyString = base.DataSource.GetItemPropertyString(itemIndex, StoreObjectSchema.ItemClass);
			if (!ObjectClass.IsMessage(itemPropertyString, false) && !ObjectClass.IsMeetingMessage(itemPropertyString))
			{
				return false;
			}
			bool itemPropertyBool = base.DataSource.GetItemPropertyBool(itemIndex, MessageItemSchema.HasBeenSubmitted, true);
			bool itemPropertyBool2 = base.DataSource.GetItemPropertyBool(itemIndex, MessageItemSchema.IsDraft, false);
			return itemPropertyBool2 && !itemPropertyBool;
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x00018B18 File Offset: 0x00016D18
		protected override bool RenderIcon(TextWriter writer, int itemIndex)
		{
			string itemClass = base.DataSource.GetItemProperty(itemIndex, StoreObjectSchema.ItemClass) as string;
			int itemPropertyInt = base.DataSource.GetItemPropertyInt(itemIndex, ItemSchema.IconIndex, -1);
			bool itemPropertyBool = base.DataSource.GetItemPropertyBool(itemIndex, MessageItemSchema.MessageInConflict, false);
			bool itemPropertyBool2 = base.DataSource.GetItemPropertyBool(itemIndex, MessageItemSchema.IsRead, false);
			return ListViewContentsRenderingUtilities.RenderMessageIcon(writer, base.UserContext, itemClass, itemPropertyBool2, itemPropertyBool, itemPropertyInt);
		}
	}
}
