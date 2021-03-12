using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Entities;
using Microsoft.Exchange.Entities.DataModel.Items;
using Microsoft.Exchange.Entities.EntitySets.Commands;

namespace Microsoft.Exchange.Entities.EntitySets.AttachmentCommands
{
	// Token: 0x02000020 RID: 32
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class CreateAttachment : CreateEntityCommand<Attachments, IAttachment>
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00004183 File Offset: 0x00002383
		protected override ITracer Trace
		{
			get
			{
				return ExTraceGlobals.CreateAttachmentTracer;
			}
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x0000418A File Offset: 0x0000238A
		protected override IAttachment OnExecute()
		{
			return this.Scope.AttachmentDataProvider.Create(base.Entity);
		}
	}
}
