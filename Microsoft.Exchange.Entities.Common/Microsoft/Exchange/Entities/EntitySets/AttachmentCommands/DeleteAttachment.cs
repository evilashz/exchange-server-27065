using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Entities;
using Microsoft.Exchange.Entities.EntitySets.Commands;

namespace Microsoft.Exchange.Entities.EntitySets.AttachmentCommands
{
	// Token: 0x02000025 RID: 37
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class DeleteAttachment : DeleteEntityCommand<Attachments>
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000CE RID: 206 RVA: 0x0000423B File Offset: 0x0000243B
		protected override ITracer Trace
		{
			get
			{
				return ExTraceGlobals.DeleteAttachmentTracer;
			}
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00004242 File Offset: 0x00002442
		protected override VoidResult OnExecute()
		{
			this.Scope.AttachmentDataProvider.Delete(base.EntityKey, DeleteItemFlags.None);
			return VoidResult.Value;
		}
	}
}
