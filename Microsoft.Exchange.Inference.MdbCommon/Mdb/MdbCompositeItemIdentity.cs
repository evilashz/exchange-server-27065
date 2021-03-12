using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Common;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Inference.Mdb
{
	// Token: 0x0200000B RID: 11
	[DataContract]
	[Serializable]
	internal class MdbCompositeItemIdentity : IIdentity, IEquatable<IIdentity>
	{
		// Token: 0x0600001A RID: 26 RVA: 0x000030A9 File Offset: 0x000012A9
		internal MdbCompositeItemIdentity(Guid mdbGuid, Guid mailboxGuid, StoreObjectId itemId, int documentId) : this(mdbGuid, mailboxGuid, 1, itemId, documentId)
		{
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000030B8 File Offset: 0x000012B8
		internal MdbCompositeItemIdentity(Guid mdbGuid, MapiEvent mapiEvent)
		{
			Util.ThrowOnNullArgument(mapiEvent, "mapiEvent");
			this.Initialize(mdbGuid, mapiEvent.MailboxGuid, mapiEvent.MailboxNumber, StoreObjectId.FromProviderSpecificId(mapiEvent.ItemEntryId, ObjectClass.GetObjectType(mapiEvent.ObjectClass)), mapiEvent.DocumentId);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00003105 File Offset: 0x00001305
		internal MdbCompositeItemIdentity(Guid mdbGuid, Guid mailboxGuid, int mailboxNumber, StoreObjectId itemId, int documentId)
		{
			Util.ThrowOnNullArgument(itemId, "itemId");
			this.Initialize(mdbGuid, mailboxGuid, mailboxNumber, itemId, documentId);
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00003126 File Offset: 0x00001326
		// (set) Token: 0x0600001E RID: 30 RVA: 0x0000312E File Offset: 0x0000132E
		[DataMember]
		internal Guid MdbGuid { get; private set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00003137 File Offset: 0x00001337
		// (set) Token: 0x06000020 RID: 32 RVA: 0x0000313F File Offset: 0x0000133F
		[DataMember]
		internal Guid MailboxGuid { get; private set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00003148 File Offset: 0x00001348
		// (set) Token: 0x06000022 RID: 34 RVA: 0x00003150 File Offset: 0x00001350
		[DataMember]
		internal int MailboxNumber { get; private set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00003159 File Offset: 0x00001359
		// (set) Token: 0x06000024 RID: 36 RVA: 0x00003161 File Offset: 0x00001361
		[DataMember]
		internal StoreObjectId ItemId { get; private set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000025 RID: 37 RVA: 0x0000316A File Offset: 0x0000136A
		// (set) Token: 0x06000026 RID: 38 RVA: 0x00003172 File Offset: 0x00001372
		[DataMember]
		internal int DocumentId { get; private set; }

		// Token: 0x06000027 RID: 39 RVA: 0x0000317C File Offset: 0x0000137C
		public static MdbCompositeItemIdentity Parse(string mdbComositeItemIdentity)
		{
			string[] array = mdbComositeItemIdentity.Split(new char[]
			{
				','
			});
			if (array.Length != 5)
			{
				throw new ArgumentException(string.Format("The string representation of the Identity is invalid. {0}", mdbComositeItemIdentity));
			}
			string text = array[3];
			return new MdbCompositeItemIdentity(new Guid(array[0]), new Guid(array[1]), int.Parse(array[2]), text.Equals(MdbCompositeItemIdentity.DummyStoreObjectId) ? StoreObjectId.DummyId : StoreObjectId.Deserialize(text), int.Parse(array[4]));
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000031F7 File Offset: 0x000013F7
		public override bool Equals(object other)
		{
			return this.Equals(other as IIdentity);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00003205 File Offset: 0x00001405
		public virtual bool Equals(IIdentity other)
		{
			return this.Equals(other as MdbCompositeItemIdentity);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00003214 File Offset: 0x00001414
		public override int GetHashCode()
		{
			return this.ItemId.GetHashCode();
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00003230 File Offset: 0x00001430
		public override string ToString()
		{
			if (this.stringizedId == null)
			{
				this.stringizedId = string.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}", new object[]
				{
					',',
					this.MdbGuid,
					this.MailboxGuid,
					this.MailboxNumber,
					this.ItemId,
					this.DocumentId
				});
			}
			return this.stringizedId;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000032B0 File Offset: 0x000014B0
		private bool Equals(MdbCompositeItemIdentity other)
		{
			return other != null && (object.ReferenceEquals(other, this) || (this.MdbGuid.Equals(other.MdbGuid) && this.MailboxGuid.Equals(other.MailboxGuid) && this.MailboxNumber.Equals(other.MailboxNumber) && this.ItemId.Equals(other.ItemId) && this.DocumentId.Equals(other.DocumentId)));
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000333B File Offset: 0x0000153B
		private void Initialize(Guid mdbGuid, Guid mailboxGuid, int mailboxNumber, StoreObjectId itemId, int documentId)
		{
			this.MdbGuid = mdbGuid;
			this.MailboxGuid = mailboxGuid;
			this.MailboxNumber = mailboxNumber;
			this.ItemId = itemId;
			this.DocumentId = documentId;
		}

		// Token: 0x04000024 RID: 36
		private const char SeparatorChar = ',';

		// Token: 0x04000025 RID: 37
		private static readonly string DummyStoreObjectId = StoreObjectId.DummyId.ToString();

		// Token: 0x04000026 RID: 38
		[DataMember]
		private string stringizedId;

		// Token: 0x0200000C RID: 12
		private enum Segments
		{
			// Token: 0x0400002D RID: 45
			MdbGuid,
			// Token: 0x0400002E RID: 46
			MailboxGuid,
			// Token: 0x0400002F RID: 47
			MailboxNumber,
			// Token: 0x04000030 RID: 48
			ItemId,
			// Token: 0x04000031 RID: 49
			DocumentId,
			// Token: 0x04000032 RID: 50
			Size
		}
	}
}
