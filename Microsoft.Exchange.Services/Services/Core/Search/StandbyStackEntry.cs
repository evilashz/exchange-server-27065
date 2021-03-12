using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Services.Core.Search
{
	// Token: 0x02000280 RID: 640
	internal class StandbyStackEntry<TInput, TOutput>
	{
		// Token: 0x060010AE RID: 4270 RVA: 0x00050B85 File Offset: 0x0004ED85
		public StandbyStackEntry(TInput inputFilter)
		{
			this.inputFilter = inputFilter;
			this.workingStack = new Stack<TOutput>();
			this.currentChild = 0;
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x060010AF RID: 4271 RVA: 0x00050BA6 File Offset: 0x0004EDA6
		// (set) Token: 0x060010B0 RID: 4272 RVA: 0x00050BAE File Offset: 0x0004EDAE
		public TInput Filter
		{
			get
			{
				return this.inputFilter;
			}
			set
			{
				this.inputFilter = value;
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x060010B1 RID: 4273 RVA: 0x00050BB7 File Offset: 0x0004EDB7
		// (set) Token: 0x060010B2 RID: 4274 RVA: 0x00050BBF File Offset: 0x0004EDBF
		public Stack<TOutput> WorkingStack
		{
			get
			{
				return this.workingStack;
			}
			set
			{
				this.workingStack = value;
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x060010B3 RID: 4275 RVA: 0x00050BC8 File Offset: 0x0004EDC8
		// (set) Token: 0x060010B4 RID: 4276 RVA: 0x00050BD0 File Offset: 0x0004EDD0
		public int CurrentChild
		{
			get
			{
				return this.currentChild;
			}
			set
			{
				this.currentChild = value;
			}
		}

		// Token: 0x04000C33 RID: 3123
		private TInput inputFilter;

		// Token: 0x04000C34 RID: 3124
		private Stack<TOutput> workingStack;

		// Token: 0x04000C35 RID: 3125
		private int currentChild;
	}
}
