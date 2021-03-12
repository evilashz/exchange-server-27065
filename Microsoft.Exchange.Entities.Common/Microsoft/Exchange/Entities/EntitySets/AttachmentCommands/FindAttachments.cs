using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Entities;
using Microsoft.Exchange.Entities.DataModel.Items;
using Microsoft.Exchange.Entities.EntitySets.Commands;
using Microsoft.Exchange.Entities.EntitySets.Linq.ExtensionMethods;

namespace Microsoft.Exchange.Entities.EntitySets.AttachmentCommands
{
	// Token: 0x02000028 RID: 40
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class FindAttachments : FindEntitiesCommand<Attachments, IAttachment>
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00004289 File Offset: 0x00002489
		protected override ITracer Trace
		{
			get
			{
				return ExTraceGlobals.FindAttachmentsTracer;
			}
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00004290 File Offset: 0x00002490
		protected override IEnumerable<IAttachment> OnExecute()
		{
			IEnumerable<IAttachment> allAttachments = this.Scope.AttachmentDataProvider.GetAllAttachments();
			return base.QueryOptions.ApplyTo(allAttachments.AsQueryable<IAttachment>());
		}
	}
}
