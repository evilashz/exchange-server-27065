using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.WorkingSet.SignalApi;

namespace Microsoft.Exchange.Data.Storage.WorkingSet.SignalApiEx
{
	// Token: 0x02000EE3 RID: 3811
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ExchangeItem : Item
	{
		// Token: 0x06008385 RID: 33669 RVA: 0x0023B8C5 File Offset: 0x00239AC5
		public ExchangeItem(string identifier, string sourceSystem, bool forceDownload, Item item) : base(identifier, sourceSystem, forceDownload)
		{
			if (identifier == null)
			{
				throw new ArgumentException("Identifier can't be null");
			}
			this.Item = item;
		}

		// Token: 0x06008386 RID: 33670 RVA: 0x0023B8E8 File Offset: 0x00239AE8
		internal ExchangeItem(BinaryReader reader, IUnpacker unpacker) : base(reader)
		{
			string text = reader.ReadString();
			if (!text.Equals("n"))
			{
				this.AttachContentId = new Guid(text);
				unpacker.SetContent(this, this.AttachContentId);
			}
		}

		// Token: 0x170022E8 RID: 8936
		// (get) Token: 0x06008387 RID: 33671 RVA: 0x0023B929 File Offset: 0x00239B29
		// (set) Token: 0x06008388 RID: 33672 RVA: 0x0023B931 File Offset: 0x00239B31
		public Item Item { get; set; }

		// Token: 0x170022E9 RID: 8937
		// (get) Token: 0x06008389 RID: 33673 RVA: 0x0023B93A File Offset: 0x00239B3A
		// (set) Token: 0x0600838A RID: 33674 RVA: 0x0023B942 File Offset: 0x00239B42
		internal Guid AttachContentId { get; private set; }

		// Token: 0x0600838B RID: 33675 RVA: 0x0023B94B File Offset: 0x00239B4B
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600838C RID: 33676 RVA: 0x0023B954 File Offset: 0x00239B54
		public override bool Equals(object obj)
		{
			if (!base.Equals(obj))
			{
				return false;
			}
			if (obj != null)
			{
				ExchangeItem exchangeItem = obj as ExchangeItem;
				if (exchangeItem == null)
				{
					return false;
				}
				if (this.Item != null)
				{
					return this.Item.Equals(exchangeItem.Item);
				}
				if (exchangeItem.Item != null)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600838D RID: 33677 RVA: 0x0023B9A0 File Offset: 0x00239BA0
		internal override bool WriteObject(BinaryWriter writer)
		{
			base.WriteObject(writer);
			if (this.HasAttachment())
			{
				this.AttachContentId = Guid.NewGuid();
				writer.Write(this.AttachContentId.ToString());
				return true;
			}
			writer.Write("n");
			return false;
		}

		// Token: 0x0600838E RID: 33678 RVA: 0x0023B9F0 File Offset: 0x00239BF0
		internal override Item.ItemTypeCode GetItemTypeCode()
		{
			return 2;
		}

		// Token: 0x0600838F RID: 33679 RVA: 0x0023B9F3 File Offset: 0x00239BF3
		internal override bool HasAttachment()
		{
			return this.Item != null;
		}
	}
}
