using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Entities;
using Microsoft.Exchange.Entities.DataModel.Items;
using Microsoft.Exchange.Entities.EntitySets.Commands;

namespace Microsoft.Exchange.Entities.EntitySets.AttachmentCommands
{
	// Token: 0x0200002B RID: 43
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ReadAttachment : ReadEntityCommand<Attachments, IAttachment>
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000DB RID: 219 RVA: 0x000042CF File Offset: 0x000024CF
		protected override ITracer Trace
		{
			get
			{
				return ExTraceGlobals.ReadAttachmentTracer;
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x000042D6 File Offset: 0x000024D6
		protected override IAttachment OnExecute()
		{
			return this.Scope.AttachmentDataProvider.Read(base.EntityKey);
		}
	}
}
