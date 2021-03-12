using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200019C RID: 412
	public sealed class SkippedItemCounts : XMLSerializableBase
	{
		// Token: 0x06000F52 RID: 3922 RVA: 0x00022E47 File Offset: 0x00021047
		public SkippedItemCounts()
		{
			this.counts = new Dictionary<SkippedItemCounts.CategoryKey, int>();
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x06000F53 RID: 3923 RVA: 0x00022E5A File Offset: 0x0002105A
		// (set) Token: 0x06000F54 RID: 3924 RVA: 0x00022E62 File Offset: 0x00021062
		[XmlAttribute(AttributeName = "Corrupt")]
		public int CorruptCount { get; set; }

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06000F55 RID: 3925 RVA: 0x00022E6B File Offset: 0x0002106B
		// (set) Token: 0x06000F56 RID: 3926 RVA: 0x00022E73 File Offset: 0x00021073
		[XmlAttribute(AttributeName = "Missing")]
		public int MissingCount { get; set; }

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06000F57 RID: 3927 RVA: 0x00022E7C File Offset: 0x0002107C
		// (set) Token: 0x06000F58 RID: 3928 RVA: 0x00022E84 File Offset: 0x00021084
		[XmlAttribute(AttributeName = "Large")]
		public int LargeCount { get; set; }

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06000F59 RID: 3929 RVA: 0x00022E8D File Offset: 0x0002108D
		// (set) Token: 0x06000F5A RID: 3930 RVA: 0x00022E95 File Offset: 0x00021095
		[XmlAttribute(AttributeName = "Other")]
		public int OtherCount { get; set; }

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06000F5B RID: 3931 RVA: 0x00022EA0 File Offset: 0x000210A0
		// (set) Token: 0x06000F5C RID: 3932 RVA: 0x00022F34 File Offset: 0x00021134
		[XmlElement(ElementName = "C")]
		public SkippedItemCounts.CategoryCount[] Counts
		{
			get
			{
				if (this.counts == null || this.counts.Count == 0)
				{
					return null;
				}
				List<SkippedItemCounts.CategoryCount> list = new List<SkippedItemCounts.CategoryCount>(this.counts.Count);
				foreach (KeyValuePair<SkippedItemCounts.CategoryKey, int> keyValuePair in this.counts)
				{
					list.Add(new SkippedItemCounts.CategoryCount(keyValuePair.Key, keyValuePair.Value));
				}
				return list.ToArray();
			}
			set
			{
				this.counts.Clear();
				if (value != null)
				{
					for (int i = 0; i < value.Length; i++)
					{
						SkippedItemCounts.CategoryCount categoryCount = value[i];
						if (categoryCount != null)
						{
							this.counts[categoryCount.Key] = categoryCount.Count;
						}
					}
				}
			}
		}

		// Token: 0x06000F5D RID: 3933 RVA: 0x00022F7D File Offset: 0x0002117D
		public void AddBadItem(BadMessageRec badItem)
		{
			this.AccountBadItem(badItem);
		}

		// Token: 0x06000F5E RID: 3934 RVA: 0x00022F88 File Offset: 0x00021188
		public void RecordMissingItems(List<FolderSizeRec> verificationResults)
		{
			this.MissingCount = 0;
			List<SkippedItemCounts.CategoryKey> list = new List<SkippedItemCounts.CategoryKey>();
			foreach (SkippedItemCounts.CategoryKey categoryKey in this.counts.Keys)
			{
				if (categoryKey.Kind == BadItemKind.MissingItem)
				{
					list.Add(categoryKey);
				}
			}
			foreach (SkippedItemCounts.CategoryKey key in list)
			{
				this.counts.Remove(key);
			}
			foreach (FolderSizeRec folderSizeRec in verificationResults)
			{
				if (folderSizeRec.MissingItems != null)
				{
					foreach (BadMessageRec item in folderSizeRec.MissingItems)
					{
						this.AccountBadItem(item);
					}
				}
			}
		}

		// Token: 0x06000F5F RID: 3935 RVA: 0x000230C0 File Offset: 0x000212C0
		private void AccountBadItem(BadMessageRec item)
		{
			switch (item.Kind)
			{
			case BadItemKind.MissingItem:
				this.MissingCount++;
				break;
			case BadItemKind.CorruptItem:
				this.CorruptCount++;
				break;
			case BadItemKind.LargeItem:
				this.LargeCount++;
				break;
			default:
				this.OtherCount++;
				break;
			}
			SkippedItemCounts.CategoryKey key = new SkippedItemCounts.CategoryKey
			{
				Kind = item.Kind,
				Category = item.Category
			};
			int num;
			if (!this.counts.TryGetValue(key, out num))
			{
				num = 0;
			}
			this.counts[key] = num + 1;
		}

		// Token: 0x040008A1 RID: 2209
		private Dictionary<SkippedItemCounts.CategoryKey, int> counts;

		// Token: 0x0200019D RID: 413
		internal class CategoryKey : IEquatable<SkippedItemCounts.CategoryKey>
		{
			// Token: 0x170004C6 RID: 1222
			// (get) Token: 0x06000F60 RID: 3936 RVA: 0x00023167 File Offset: 0x00021367
			// (set) Token: 0x06000F61 RID: 3937 RVA: 0x0002316F File Offset: 0x0002136F
			public BadItemKind Kind { get; set; }

			// Token: 0x170004C7 RID: 1223
			// (get) Token: 0x06000F62 RID: 3938 RVA: 0x00023178 File Offset: 0x00021378
			// (set) Token: 0x06000F63 RID: 3939 RVA: 0x00023180 File Offset: 0x00021380
			public string Category { get; set; }

			// Token: 0x06000F64 RID: 3940 RVA: 0x00023189 File Offset: 0x00021389
			public override int GetHashCode()
			{
				return this.Kind.GetHashCode() ^ ((this.Category != null) ? this.Category.GetHashCode() : 0);
			}

			// Token: 0x06000F65 RID: 3941 RVA: 0x000231B2 File Offset: 0x000213B2
			public override bool Equals(object o)
			{
				return ((IEquatable<SkippedItemCounts.CategoryKey>)this).Equals(o as SkippedItemCounts.CategoryKey);
			}

			// Token: 0x06000F66 RID: 3942 RVA: 0x000231C0 File Offset: 0x000213C0
			bool IEquatable<SkippedItemCounts.CategoryKey>.Equals(SkippedItemCounts.CategoryKey other)
			{
				return other != null && other.Kind == this.Kind && StringComparer.InvariantCultureIgnoreCase.Equals(this.Category, other.Category);
			}
		}

		// Token: 0x0200019E RID: 414
		public sealed class CategoryCount : XMLSerializableBase
		{
			// Token: 0x06000F68 RID: 3944 RVA: 0x000231F3 File Offset: 0x000213F3
			public CategoryCount()
			{
				this.Key = new SkippedItemCounts.CategoryKey();
			}

			// Token: 0x06000F69 RID: 3945 RVA: 0x00023206 File Offset: 0x00021406
			internal CategoryCount(SkippedItemCounts.CategoryKey key, int count)
			{
				this.Key = key;
				this.Count = count;
			}

			// Token: 0x170004C8 RID: 1224
			// (get) Token: 0x06000F6A RID: 3946 RVA: 0x0002321C File Offset: 0x0002141C
			// (set) Token: 0x06000F6B RID: 3947 RVA: 0x00023224 File Offset: 0x00021424
			[XmlIgnore]
			internal SkippedItemCounts.CategoryKey Key { get; private set; }

			// Token: 0x170004C9 RID: 1225
			// (get) Token: 0x06000F6C RID: 3948 RVA: 0x0002322D File Offset: 0x0002142D
			// (set) Token: 0x06000F6D RID: 3949 RVA: 0x00023244 File Offset: 0x00021444
			[XmlAttribute(AttributeName = "Kind")]
			public string KindStr
			{
				get
				{
					return this.Key.Kind.ToString();
				}
				set
				{
				}
			}

			// Token: 0x170004CA RID: 1226
			// (get) Token: 0x06000F6E RID: 3950 RVA: 0x00023246 File Offset: 0x00021446
			// (set) Token: 0x06000F6F RID: 3951 RVA: 0x00023253 File Offset: 0x00021453
			[XmlAttribute(AttributeName = "KindInt")]
			public int KindInt
			{
				get
				{
					return (int)this.Key.Kind;
				}
				set
				{
					this.Key.Kind = (BadItemKind)value;
				}
			}

			// Token: 0x170004CB RID: 1227
			// (get) Token: 0x06000F70 RID: 3952 RVA: 0x00023261 File Offset: 0x00021461
			// (set) Token: 0x06000F71 RID: 3953 RVA: 0x0002326E File Offset: 0x0002146E
			[XmlAttribute(AttributeName = "Cat")]
			public string Category
			{
				get
				{
					return this.Key.Category;
				}
				set
				{
					this.Key.Category = value;
				}
			}

			// Token: 0x170004CC RID: 1228
			// (get) Token: 0x06000F72 RID: 3954 RVA: 0x0002327C File Offset: 0x0002147C
			// (set) Token: 0x06000F73 RID: 3955 RVA: 0x00023284 File Offset: 0x00021484
			[XmlAttribute(AttributeName = "Num")]
			public int Count { get; set; }
		}
	}
}
