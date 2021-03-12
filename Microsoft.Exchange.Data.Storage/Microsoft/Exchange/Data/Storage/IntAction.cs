using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000B7D RID: 2941
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class IntAction : ActionBase
	{
		// Token: 0x06006A2A RID: 27178 RVA: 0x001C5E6E File Offset: 0x001C406E
		protected IntAction(ActionType actionType, int number, Rule rule) : base(actionType, rule)
		{
			this.number = number;
		}

		// Token: 0x17001D01 RID: 7425
		// (get) Token: 0x06006A2B RID: 27179 RVA: 0x001C5E7F File Offset: 0x001C407F
		public int Number
		{
			get
			{
				return this.number;
			}
		}

		// Token: 0x04003C69 RID: 15465
		private readonly int number;
	}
}
