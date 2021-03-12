using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x020000B2 RID: 178
	public class RecipientCollection
	{
		// Token: 0x06000A18 RID: 2584 RVA: 0x000516FA File Offset: 0x0004F8FA
		public RecipientCollection(Message parent)
		{
			this.parent = parent;
			this.recipients = new List<Recipient>();
			this.changed = true;
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x0005171B File Offset: 0x0004F91B
		public RecipientCollection(Message parent, byte[][] blob) : this(parent)
		{
			this.FromBinary(blob);
			this.changed = false;
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000A1A RID: 2586 RVA: 0x00051732 File Offset: 0x0004F932
		internal ObjectPropertySchema RecipientSchema
		{
			get
			{
				if (this.recipientPropertySchema == null)
				{
					this.recipientPropertySchema = PropertySchema.GetObjectSchema(this.parent.Mailbox.Database, ObjectType.Recipient);
				}
				return this.recipientPropertySchema;
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000A1B RID: 2587 RVA: 0x0005175E File Offset: 0x0004F95E
		public Message ParentMessage
		{
			get
			{
				return this.parent;
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000A1C RID: 2588 RVA: 0x00051766 File Offset: 0x0004F966
		public int Count
		{
			get
			{
				return this.recipients.Count;
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000A1D RID: 2589 RVA: 0x00051773 File Offset: 0x0004F973
		// (set) Token: 0x06000A1E RID: 2590 RVA: 0x0005177B File Offset: 0x0004F97B
		public bool Changed
		{
			get
			{
				return this.changed;
			}
			set
			{
				this.changed = value;
			}
		}

		// Token: 0x17000232 RID: 562
		public Recipient this[int index]
		{
			get
			{
				return this.recipients[index];
			}
			set
			{
				this.recipients[index] = value;
				this.Changed = true;
			}
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x000517A8 File Offset: 0x0004F9A8
		public Recipient Add(string email, string name, RecipientType type, int rowId)
		{
			Recipient recipient = new Recipient(this);
			recipient.Email = email;
			recipient.Name = name;
			recipient.RecipientType = type;
			recipient.RowId = rowId;
			this.Add(recipient);
			return recipient;
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x000517E4 File Offset: 0x0004F9E4
		public byte[][] ToBinary(Context context)
		{
			if (this.recipients.Count == 0)
			{
				return null;
			}
			byte[][] array = new byte[this.recipients.Count][];
			for (int i = 0; i < this.recipients.Count; i++)
			{
				this.recipients[i].ToBinary(context, out array[i]);
			}
			return array;
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x00051841 File Offset: 0x0004FA41
		public IEnumerator GetEnumerator()
		{
			return this.recipients.GetEnumerator();
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x00051853 File Offset: 0x0004FA53
		public void Delete()
		{
			this.recipients = new List<Recipient>();
			this.Changed = true;
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x00051867 File Offset: 0x0004FA67
		public void CopyTo(Array array, int index)
		{
			throw new NotSupportedException((LID)60024U, "CopyTo on RecipientCollection is unsupported");
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x0005187D File Offset: 0x0004FA7D
		public void Clear()
		{
			this.Delete();
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x00051885 File Offset: 0x0004FA85
		public void Remove(Recipient recipient)
		{
			this.recipients.Remove(recipient);
			this.Changed = true;
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x0005189B File Offset: 0x0004FA9B
		public int IndexOf(Recipient recipient)
		{
			return this.recipients.IndexOf(recipient);
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x000518A9 File Offset: 0x0004FAA9
		private int Add(Recipient recipient)
		{
			this.recipients.Add(recipient);
			this.Changed = true;
			return this.recipients.Count;
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x000518CC File Offset: 0x0004FACC
		private void FromBinary(byte[][] blob)
		{
			if (blob == null)
			{
				return;
			}
			for (int i = 0; i < blob.Length; i++)
			{
				Recipient recipient = new Recipient(this, blob[i]);
				recipient.RowId = this.recipients.Count;
				this.recipients.Add(recipient);
			}
		}

		// Token: 0x040004C0 RID: 1216
		private Message parent;

		// Token: 0x040004C1 RID: 1217
		private List<Recipient> recipients;

		// Token: 0x040004C2 RID: 1218
		private bool changed;

		// Token: 0x040004C3 RID: 1219
		private ObjectPropertySchema recipientPropertySchema;
	}
}
