using System;

namespace Microsoft.Exchange.Transport.Storage
{
	// Token: 0x020000A4 RID: 164
	[Serializable]
	internal class ColumnCacheString : ColumnCache<string>
	{
		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060005A5 RID: 1445 RVA: 0x000173A2 File Offset: 0x000155A2
		// (set) Token: 0x060005A6 RID: 1446 RVA: 0x000173AA File Offset: 0x000155AA
		public override string Value
		{
			get
			{
				return base.Value;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					value = null;
				}
				if (!string.Equals(this.Value, value))
				{
					if (value != null)
					{
						base.Value = value;
						return;
					}
					base.HasValue = false;
				}
			}
		}
	}
}
