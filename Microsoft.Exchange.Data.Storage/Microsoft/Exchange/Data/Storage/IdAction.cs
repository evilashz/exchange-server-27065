using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000B74 RID: 2932
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class IdAction : ActionBase
	{
		// Token: 0x06006A0E RID: 27150 RVA: 0x001C5A6B File Offset: 0x001C3C6B
		protected IdAction(ActionType actionType, StoreObjectId id, Rule rule) : base(actionType, rule)
		{
			this.id = id;
		}

		// Token: 0x17001CFD RID: 7421
		// (get) Token: 0x06006A0F RID: 27151 RVA: 0x001C5A7C File Offset: 0x001C3C7C
		public StoreObjectId Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x04003C65 RID: 15461
		private readonly StoreObjectId id;
	}
}
