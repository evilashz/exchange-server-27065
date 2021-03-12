using System;
using Microsoft.Ceres.InteractionEngine.Services.Exchange;
using Microsoft.Ceres.SearchCore.Admin.Model;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Mdb;
using Microsoft.Exchange.Search.OperatorSchema;

namespace Microsoft.Exchange.Search.Fast
{
	// Token: 0x02000007 RID: 7
	internal class DocEntry : IDocEntry, IEquatable<IDocEntry>
	{
		// Token: 0x06000036 RID: 54 RVA: 0x0000312B File Offset: 0x0000132B
		public DocEntry()
		{
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00003133 File Offset: 0x00001333
		public DocEntry(ISearchResultItem item)
		{
			this.Parse(item);
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00003142 File Offset: 0x00001342
		// (set) Token: 0x06000039 RID: 57 RVA: 0x0000314A File Offset: 0x0000134A
		public string RawItemId { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00003153 File Offset: 0x00001353
		public MdbItemIdentity ItemId
		{
			get
			{
				if (this.itemId == null)
				{
					this.itemId = MdbItemIdentity.Parse(this.RawItemId);
				}
				return this.itemId;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00003174 File Offset: 0x00001374
		public int DocumentId
		{
			get
			{
				return Microsoft.Exchange.Search.OperatorSchema.IndexId.GetDocumentId(this.IndexId);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00003181 File Offset: 0x00001381
		public string EntryId
		{
			get
			{
				return this.ItemId.ItemId.ToString();
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00003193 File Offset: 0x00001393
		// (set) Token: 0x0600003E RID: 62 RVA: 0x0000319B File Offset: 0x0000139B
		public long IndexId { get; private set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600003F RID: 63 RVA: 0x000031A4 File Offset: 0x000013A4
		// (set) Token: 0x06000040 RID: 64 RVA: 0x000031AC File Offset: 0x000013AC
		public Guid MailboxGuid { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000041 RID: 65 RVA: 0x000031B5 File Offset: 0x000013B5
		internal static IndexSystemField[] Schema
		{
			get
			{
				return DocEntry.schema;
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000031BC File Offset: 0x000013BC
		public override string ToString()
		{
			if (this.RawItemId != null)
			{
				return string.Format("ItemId: {0}", this.RawItemId);
			}
			return string.Format("ItemId: null, MailboxGuid: {0}, IndexId: {1}", this.MailboxGuid, this.IndexId);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000031F8 File Offset: 0x000013F8
		public override bool Equals(object other)
		{
			DocEntry docEntry = other as DocEntry;
			return docEntry != null && (object.ReferenceEquals(this, docEntry) || this.IndexId == docEntry.IndexId);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x0000322C File Offset: 0x0000142C
		public override int GetHashCode()
		{
			return this.IndexId.GetHashCode();
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00003248 File Offset: 0x00001448
		public bool Equals(IDocEntry other)
		{
			return other != null && this.IndexId.Equals(other.IndexId);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x0000326E File Offset: 0x0000146E
		protected virtual void SetProp(string name, object value)
		{
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00003270 File Offset: 0x00001470
		private void Parse(ISearchResultItem item)
		{
			foreach (IFieldHolder fieldHolder in item.Fields)
			{
				string name;
				if (fieldHolder.Value != null && (name = fieldHolder.Name) != null)
				{
					if (!(name == "DocId"))
					{
						if (!(name == "Rank"))
						{
							if (name == "Other")
							{
								ISearchResultItem searchResultItem = (ISearchResultItem)fieldHolder.Value;
								foreach (IFieldHolder fieldHolder2 in searchResultItem.Fields)
								{
									string name2 = fieldHolder2.Name;
									object value = fieldHolder2.Value;
									if (!string.IsNullOrEmpty(name2) && value != null)
									{
										if (name2 == FastIndexSystemSchema.ItemId.Name)
										{
											this.RawItemId = (string)value;
										}
										else if (name2 == FastIndexSystemSchema.MailboxGuid.Name)
										{
											this.MailboxGuid = new Guid((string)value);
										}
										else
										{
											this.SetProp(name2, value);
										}
									}
								}
							}
						}
					}
					else
					{
						this.IndexId = (long)fieldHolder.Value;
					}
				}
			}
		}

		// Token: 0x04000019 RID: 25
		internal const string DocIdField = "DocId";

		// Token: 0x0400001A RID: 26
		internal const string RankField = "Rank";

		// Token: 0x0400001B RID: 27
		internal const string OtherField = "Other";

		// Token: 0x0400001C RID: 28
		private static readonly IndexSystemField[] schema = new IndexSystemField[]
		{
			FastIndexSystemSchema.ItemId.Definition,
			FastIndexSystemSchema.MailboxGuid.Definition
		};

		// Token: 0x0400001D RID: 29
		private MdbItemIdentity itemId;
	}
}
