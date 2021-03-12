using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Entities;
using Microsoft.Exchange.Entities.DataModel.Items;
using Microsoft.Exchange.Entities.DataProviders;
using Microsoft.Exchange.Entities.EntitySets.Commands;

namespace Microsoft.Exchange.Entities.EntitySets.AttachmentCommands
{
	// Token: 0x0200002E RID: 46
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class UpdateAttachment : UpdateEntityCommand<Attachments, IAttachment>
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00004392 File Offset: 0x00002592
		protected override ITracer Trace
		{
			get
			{
				return ExTraceGlobals.UpdateAttachmentTracer;
			}
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00004399 File Offset: 0x00002599
		protected override IAttachment OnExecute()
		{
			throw new NotSupportedException(Strings.ErrorUnsupportedOperation("Update"));
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000043AF File Offset: 0x000025AF
		protected override void SetETag(string eTag)
		{
		}
	}
}
