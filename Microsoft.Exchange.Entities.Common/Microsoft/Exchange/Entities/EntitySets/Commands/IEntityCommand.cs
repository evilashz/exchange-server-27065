using System;
using Microsoft.Exchange.Entities.DataModel;

namespace Microsoft.Exchange.Entities.EntitySets.Commands
{
	// Token: 0x0200001C RID: 28
	public interface IEntityCommand<TScope, out TResult>
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000B0 RID: 176
		Guid Id { get; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000B1 RID: 177
		// (set) Token: 0x060000B2 RID: 178
		TScope Scope { get; set; }

		// Token: 0x060000B3 RID: 179
		TResult Execute(CommandContext context);
	}
}
