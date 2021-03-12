using System;
using System.Globalization;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x0200009F RID: 159
	internal class FolderIdMapping : IdMapping
	{
		// Token: 0x17000362 RID: 866
		// (get) Token: 0x060008EF RID: 2287 RVA: 0x0003579A File Offset: 0x0003399A
		// (set) Token: 0x060008F0 RID: 2288 RVA: 0x000357A1 File Offset: 0x000339A1
		public override ushort TypeId
		{
			get
			{
				return FolderIdMapping.typeId;
			}
			set
			{
				FolderIdMapping.typeId = value;
			}
		}

		// Token: 0x17000363 RID: 867
		public override string this[ISyncItemId mailboxId]
		{
			get
			{
				string text = base[mailboxId];
				if (text != null && !base.IsInDeletedItemsBuffer(text))
				{
					return text;
				}
				return null;
			}
		}

		// Token: 0x17000364 RID: 868
		public override ISyncItemId this[string syncId]
		{
			get
			{
				if (base.IsInDeletedItemsBuffer(syncId))
				{
					return null;
				}
				return base[syncId];
			}
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x000357E4 File Offset: 0x000339E4
		public string Add(ISyncItemId mailboxFolderId)
		{
			AirSyncDiagnostics.Assert(mailboxFolderId != null);
			string text;
			if (base.OldIds.ContainsKey(mailboxFolderId))
			{
				text = base.OldIds[mailboxFolderId];
			}
			else
			{
				text = base.UniqueCounter.ToString(CultureInfo.InvariantCulture);
			}
			base.Add(mailboxFolderId, text);
			return text;
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x00035837 File Offset: 0x00033A37
		public new void Add(ISyncItemId mailboxFolderId, string syncId)
		{
			AirSyncDiagnostics.Assert(mailboxFolderId != null);
			base.Add(mailboxFolderId, syncId);
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x00035850 File Offset: 0x00033A50
		public override bool Contains(ISyncItemId mailboxId)
		{
			string text = base[mailboxId];
			return text != null && !base.IsInDeletedItemsBuffer(text);
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x00035874 File Offset: 0x00033A74
		public override bool Contains(string syncId)
		{
			return base.Contains(syncId) && !base.IsInDeletedItemsBuffer(syncId);
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x0003588B File Offset: 0x00033A8B
		public override ICustomSerializable BuildObject()
		{
			return new FolderIdMapping();
		}

		// Token: 0x040005AA RID: 1450
		private static ushort typeId;
	}
}
