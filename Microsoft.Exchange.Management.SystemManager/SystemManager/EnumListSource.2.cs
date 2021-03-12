using System;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x02000029 RID: 41
	public class EnumListSource<T> : EnumListSource
	{
		// Token: 0x060001F4 RID: 500 RVA: 0x000080BB File Offset: 0x000062BB
		public EnumListSource() : base(typeof(T))
		{
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x000080CD File Offset: 0x000062CD
		public EnumListSource(Array values) : base(values, typeof(T))
		{
		}
	}
}
