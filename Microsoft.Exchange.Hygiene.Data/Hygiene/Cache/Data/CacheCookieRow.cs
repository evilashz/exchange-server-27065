using System;

namespace Microsoft.Exchange.Hygiene.Cache.Data
{
	// Token: 0x02000049 RID: 73
	[Serializable]
	internal class CacheCookieRow : IEquatable<CacheCookieRow>
	{
		// Token: 0x060002F6 RID: 758 RVA: 0x000095D1 File Offset: 0x000077D1
		public CacheCookieRow(int copyIndex, DateTime changedTS, Guid changedEntityId)
		{
			this.CopyIndex = copyIndex;
			this.LastChangedDateTime = changedTS;
			this.LastChangedEntityId = changedEntityId;
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x000095EE File Offset: 0x000077EE
		// (set) Token: 0x060002F8 RID: 760 RVA: 0x000095F6 File Offset: 0x000077F6
		public int CopyIndex { get; set; }

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x000095FF File Offset: 0x000077FF
		// (set) Token: 0x060002FA RID: 762 RVA: 0x00009607 File Offset: 0x00007807
		public DateTime LastChangedDateTime { get; set; }

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060002FB RID: 763 RVA: 0x00009610 File Offset: 0x00007810
		// (set) Token: 0x060002FC RID: 764 RVA: 0x00009618 File Offset: 0x00007818
		public Guid LastChangedEntityId { get; set; }

		// Token: 0x060002FD RID: 765 RVA: 0x00009621 File Offset: 0x00007821
		public bool Equals(CacheCookieRow c2)
		{
			return c2 != null && (this.CopyIndex == c2.CopyIndex && this.LastChangedDateTime == c2.LastChangedDateTime && this.LastChangedEntityId == c2.LastChangedEntityId);
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000965F File Offset: 0x0000785F
		public override bool Equals(object obj)
		{
			return obj != null && obj is CacheCookieRow && this.Equals(obj as CacheCookieRow);
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000967C File Offset: 0x0000787C
		public override int GetHashCode()
		{
			string text = string.Format("{0}:{1}:{2}", this.CopyIndex, this.LastChangedDateTime, this.LastChangedEntityId);
			return text.GetHashCode();
		}
	}
}
