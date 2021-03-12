using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Inference;
using Microsoft.Exchange.Inference.Common;
using Microsoft.Exchange.Inference.PeopleICommunicateWith;
using Microsoft.Exchange.Search.Core.Diagnostics;

namespace Microsoft.Exchange.Inference.PeopleRelevance
{
	// Token: 0x02000021 RID: 33
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class RecipientInformationGenerator
	{
		// Token: 0x06000120 RID: 288 RVA: 0x00006AF0 File Offset: 0x00004CF0
		public RecipientInformationGenerator()
		{
			this.DiagnosticsSession = Microsoft.Exchange.Search.Core.Diagnostics.DiagnosticsSession.CreateComponentDiagnosticsSession("RecipientInformationGenerator", ExTraceGlobals.MdbTrainingFeederTracer, (long)this.GetHashCode());
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00006B14 File Offset: 0x00004D14
		// (set) Token: 0x06000122 RID: 290 RVA: 0x00006B1C File Offset: 0x00004D1C
		public IDiagnosticsSession DiagnosticsSession { get; set; }

		// Token: 0x06000123 RID: 291 RVA: 0x00006B30 File Offset: 0x00004D30
		internal IEnumerable<IRecipientInfo> RunTrainingQuery(MailboxSession session)
		{
			this.DiagnosticsSession.TraceDebug("Querying Recipient information from mailbox", new object[0]);
			IPicwActions picwActions = PicwActions.Create(session);
			ICollection<IRecipientInfo> recipientInfoItems = picwActions.GetRecipientInfoItems(RecipientInfoSortType.LastSentTimeUtc, SortOrder.Ascending);
			return (from s in recipientInfoItems
			where s.SentCount > 0U
			select s).ToList<IRecipientInfo>();
		}

		// Token: 0x04000088 RID: 136
		private const string ComponentName = "RecipientInformationGenerator";
	}
}
