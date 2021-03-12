using System;
using Microsoft.Exchange.Collections;

namespace Microsoft.Exchange.Transport.Storage
{
	// Token: 0x020000A5 RID: 165
	[Serializable]
	internal class ColumnCacheByteArray : ColumnCache<byte[]>
	{
		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060005A8 RID: 1448 RVA: 0x000173E0 File Offset: 0x000155E0
		// (set) Token: 0x060005A9 RID: 1449 RVA: 0x00017413 File Offset: 0x00015613
		public override byte[] Value
		{
			get
			{
				if (base.Value == null)
				{
					return null;
				}
				byte[] array = new byte[base.Value.Length];
				base.Value.CopyTo(array, 0);
				return array;
			}
			set
			{
				if (!ArrayComparer<byte>.Comparer.Equals(this.Value, value))
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
