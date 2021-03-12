using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Collections;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x02000023 RID: 35
	internal sealed class Bookmark : ISortKey<Guid>
	{
		// Token: 0x060000F6 RID: 246 RVA: 0x00005AFB File Offset: 0x00003CFB
		private Bookmark(Guid identity, int numberOfWatermarks)
		{
			this.Identity = identity;
			this.watermarks = new Dictionary<Guid, long>(numberOfWatermarks);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00005B18 File Offset: 0x00003D18
		private Bookmark(Guid identity, Bookmark bookmark) : this(identity, bookmark.watermarks.Count)
		{
			foreach (KeyValuePair<Guid, long> keyValuePair in bookmark.watermarks)
			{
				this.watermarks.Add(keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00005B90 File Offset: 0x00003D90
		// (set) Token: 0x060000F9 RID: 249 RVA: 0x00005B98 File Offset: 0x00003D98
		public Guid Identity { get; private set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00005BA1 File Offset: 0x00003DA1
		public Guid SortKey
		{
			get
			{
				return this.Identity;
			}
		}

		// Token: 0x17000056 RID: 86
		public long this[Guid assistantId]
		{
			get
			{
				return this.watermarks[assistantId];
			}
			set
			{
				this.watermarks[assistantId] = value;
			}
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00005BC8 File Offset: 0x00003DC8
		public static Bookmark CreateFromDatabaseBookmark(Guid mailboxGuid, Bookmark databaseBookmark)
		{
			return new Bookmark(mailboxGuid, databaseBookmark);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00005BE0 File Offset: 0x00003DE0
		public static Bookmark Create(Guid identity, int numberOfWatermarks)
		{
			return new Bookmark(identity, numberOfWatermarks);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00005BF8 File Offset: 0x00003DF8
		public long GetLowestWatermark()
		{
			long num = long.MaxValue;
			foreach (long val in this.watermarks.Values)
			{
				num = Math.Min(num, val);
			}
			return num;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00005C5C File Offset: 0x00003E5C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder((this.watermarks.Count + 1) * 60);
			stringBuilder.Append("Bookmark for " + this.Identity + ". Watermarks:");
			foreach (KeyValuePair<Guid, long> keyValuePair in this.watermarks)
			{
				stringBuilder.AppendFormat(" [{0},{1}]", keyValuePair.Key, keyValuePair.Value);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040000F8 RID: 248
		private Dictionary<Guid, long> watermarks;
	}
}
