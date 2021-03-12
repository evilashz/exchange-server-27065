using System;

namespace Microsoft.Exchange.Entities.EntitySets.Commands
{
	// Token: 0x02000023 RID: 35
	public interface IDeleteEntityCommand<TScope> : IKeyedEntityCommand<TScope, VoidResult>, IEntityCommand<TScope, VoidResult>
	{
	}
}
