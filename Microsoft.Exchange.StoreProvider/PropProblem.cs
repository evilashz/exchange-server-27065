using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Mapi
{
	// Token: 0x02000203 RID: 515
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal struct PropProblem
	{
		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000863 RID: 2147 RVA: 0x0002B6C1 File Offset: 0x000298C1
		public int Index
		{
			get
			{
				return this.index;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000864 RID: 2148 RVA: 0x0002B6C9 File Offset: 0x000298C9
		public PropTag PropTag
		{
			get
			{
				return this.propTag;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000865 RID: 2149 RVA: 0x0002B6D1 File Offset: 0x000298D1
		public int Scode
		{
			get
			{
				return this.scode;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000866 RID: 2150 RVA: 0x0002B6D9 File Offset: 0x000298D9
		public PropType PropType
		{
			get
			{
				return (PropType)(this.propTag & (PropTag)65535U);
			}
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x0002B6E7 File Offset: 0x000298E7
		public PropProblem(int index, PropTag propTag, int scode)
		{
			this.index = index;
			this.propTag = propTag;
			this.scode = scode;
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x0002B6FE File Offset: 0x000298FE
		internal unsafe PropProblem(SPropProblem* pspp)
		{
			this.index = pspp->ulIndex;
			this.propTag = (PropTag)pspp->ulPropTag;
			this.scode = pspp->scode;
		}

		// Token: 0x040009FA RID: 2554
		private readonly int index;

		// Token: 0x040009FB RID: 2555
		private readonly PropTag propTag;

		// Token: 0x040009FC RID: 2556
		private readonly int scode;
	}
}
