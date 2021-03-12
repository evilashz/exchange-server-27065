using System;

namespace Microsoft.Exchange.Entities.EntitySets.Commands
{
	// Token: 0x02000021 RID: 33
	public interface IKeyedEntityCommand<TEntitySet, out TResult> : IEntityCommand<TEntitySet, TResult>
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000C5 RID: 197
		// (set) Token: 0x060000C6 RID: 198
		string EntityKey { get; set; }
	}
}
