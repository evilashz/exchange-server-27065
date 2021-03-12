using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics.Components.Search;
using Microsoft.Exchange.Search.Core.Common;

namespace Microsoft.Exchange.Search.Mdb
{
	// Token: 0x0200000B RID: 11
	internal sealed class CrawlerWatermarkManager : WatermarkManager<int>
	{
		// Token: 0x06000035 RID: 53 RVA: 0x000058EE File Offset: 0x00003AEE
		internal CrawlerWatermarkManager(int batchSize) : base(batchSize)
		{
			base.DiagnosticsSession.ComponentName = "CrawlerWatermarkManager";
			base.DiagnosticsSession.Tracer = ExTraceGlobals.CrawlerWatermarkManagerTracer;
			this.stateUpdateListPerMailbox = new Dictionary<int, SortedDictionary<int, bool>>();
			this.lastDocumentIdPerMailbox = new Dictionary<int, int>();
		}

		// Token: 0x06000036 RID: 54 RVA: 0x0000592D File Offset: 0x00003B2D
		internal void SetLast(int mailboxNumber, int lastDocumentId)
		{
			base.DiagnosticsSession.TraceDebug<int, int>("Set last document (DocId={1}) for mailbox({0})", mailboxNumber, lastDocumentId);
			this.lastDocumentIdPerMailbox.Add(mailboxNumber, lastDocumentId);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00005950 File Offset: 0x00003B50
		internal void Add(MdbItemIdentity compositeId)
		{
			int mailboxNumber = compositeId.MailboxNumber;
			int documentId = compositeId.DocumentId;
			lock (this.stateUpdateListPerMailbox)
			{
				SortedDictionary<int, bool> sortedDictionary;
				if (!this.stateUpdateListPerMailbox.TryGetValue(mailboxNumber, out sortedDictionary))
				{
					base.DiagnosticsSession.TraceDebug<int>("Create a new SortedDictionary for mailbox({0})", mailboxNumber);
					sortedDictionary = new SortedDictionary<int, bool>();
					this.stateUpdateListPerMailbox.Add(mailboxNumber, sortedDictionary);
				}
				base.DiagnosticsSession.TraceDebug<int, int>("Add a new document (DocId={1}) from mailbox({0})", mailboxNumber, documentId);
				sortedDictionary.Add(documentId, false);
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000059E8 File Offset: 0x00003BE8
		internal bool TryComplete(MdbItemIdentity compositeId, out MailboxCrawlerState stateToUpdate)
		{
			int mailboxNumber = compositeId.MailboxNumber;
			int documentId = compositeId.DocumentId;
			bool result;
			lock (this.stateUpdateListPerMailbox)
			{
				SortedDictionary<int, bool> sortedDictionary;
				if (!this.stateUpdateListPerMailbox.TryGetValue(mailboxNumber, out sortedDictionary))
				{
					throw new InvalidOperationException("Not found the list for mailbox " + mailboxNumber);
				}
				bool flag2;
				if (!sortedDictionary.TryGetValue(documentId, out flag2))
				{
					throw new InvalidOperationException("Not found the docId for document " + documentId);
				}
				if (flag2)
				{
					throw new InvalidOperationException("Invalid to complete again for document " + documentId);
				}
				base.DiagnosticsSession.TraceDebug<int, int>("Try complete the document (DocId={1}) in mailbox({0})", mailboxNumber, documentId);
				sortedDictionary[documentId] = true;
				int num;
				if (base.TryFindNewWatermark(sortedDictionary, out num))
				{
					int num2;
					if (this.lastDocumentIdPerMailbox.TryGetValue(mailboxNumber, out num2) && num2 == num)
					{
						stateToUpdate = new MailboxCrawlerState(mailboxNumber, int.MaxValue, 0);
					}
					else
					{
						stateToUpdate = new MailboxCrawlerState(mailboxNumber, num, 0);
					}
					base.DiagnosticsSession.TraceDebug<int, int>("State needs to update to {1} for mailbox({0})", stateToUpdate.MailboxNumber, stateToUpdate.LastDocumentIdIndexed);
					result = true;
				}
				else
				{
					stateToUpdate = null;
					result = false;
				}
			}
			return result;
		}

		// Token: 0x04000036 RID: 54
		private readonly Dictionary<int, SortedDictionary<int, bool>> stateUpdateListPerMailbox;

		// Token: 0x04000037 RID: 55
		private readonly Dictionary<int, int> lastDocumentIdPerMailbox;
	}
}
