using System;

namespace Microsoft.Exchange.Transport.Storage
{
	// Token: 0x020000A2 RID: 162
	[Serializable]
	internal abstract class ColumnCache<T> : ColumnCache
	{
		// Token: 0x1700014B RID: 331
		// (get) Token: 0x0600059D RID: 1437 RVA: 0x000172F2 File Offset: 0x000154F2
		// (set) Token: 0x0600059E RID: 1438 RVA: 0x000172FA File Offset: 0x000154FA
		public virtual T Value
		{
			get
			{
				return this.data;
			}
			set
			{
				this.data = value;
				base.HasValue = true;
			}
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x0001730A File Offset: 0x0001550A
		public override void CloneFrom(ColumnCache from)
		{
			base.CloneFrom(from);
			if (from.HasValue)
			{
				this.data = ((ColumnCache<T>)from).data;
			}
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x0001732C File Offset: 0x0001552C
		protected override void SetValueToDefault()
		{
			this.data = default(T);
		}

		// Token: 0x040002DC RID: 732
		protected T data = default(T);
	}
}
