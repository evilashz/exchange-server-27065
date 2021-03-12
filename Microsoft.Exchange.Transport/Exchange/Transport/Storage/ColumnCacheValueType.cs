using System;

namespace Microsoft.Exchange.Transport.Storage
{
	// Token: 0x020000A3 RID: 163
	[Serializable]
	internal class ColumnCacheValueType<T> : ColumnCache<T> where T : IEquatable<T>
	{
		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060005A2 RID: 1442 RVA: 0x00017350 File Offset: 0x00015550
		// (set) Token: 0x060005A3 RID: 1443 RVA: 0x00017375 File Offset: 0x00015575
		public override T Value
		{
			get
			{
				if (!base.HasValue)
				{
					return default(T);
				}
				return this.data;
			}
			set
			{
				if (!base.HasValue || !this.data.Equals(value))
				{
					base.Value = value;
				}
			}
		}
	}
}
