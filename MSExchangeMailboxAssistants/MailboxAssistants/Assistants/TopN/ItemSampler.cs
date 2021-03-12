using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.Assistants.TopN;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.TopN
{
	// Token: 0x0200018A RID: 394
	internal class ItemSampler
	{
		// Token: 0x06000FA6 RID: 4006 RVA: 0x0005C8CA File Offset: 0x0005AACA
		public override string ToString()
		{
			if (this.toString == null)
			{
				this.toString = "ItemSampler for user " + this.mailboxSession.MailboxOwner + ". ";
			}
			return this.toString;
		}

		// Token: 0x06000FA7 RID: 4007 RVA: 0x0005C8FA File Offset: 0x0005AAFA
		internal ItemSampler(MailboxSession mailboxSession)
		{
			this.mailboxSession = mailboxSession;
		}

		// Token: 0x06000FA8 RID: 4008 RVA: 0x0005C9C0 File Offset: 0x0005ABC0
		internal List<ItemScanner> GetItemsToScan()
		{
			List<ItemScanner> itemList = new List<ItemScanner>(10);
			AllItemsFolderHelper.CheckAndCreateDefaultFolders(this.mailboxSession);
			AllItemsFolderHelper.RunQueryOnAllItemsFolder<bool>(this.mailboxSession, AllItemsFolderHelper.SupportedSortBy.ReceivedTime, delegate(QueryResult queryResults)
			{
				for (;;)
				{
					object[][] rows = queryResults.GetRows(500);
					ItemSampler.Tracer.TraceDebug<ItemSampler, int>((long)this.GetHashCode(), "{0}: GetRows returned {1} items.", this, rows.Length);
					if (rows.Length <= 0)
					{
						return true;
					}
					foreach (object[] array2 in rows)
					{
						VersionedId versionedId = array2[0] as VersionedId;
						if (versionedId != null)
						{
							itemList.Add(new ItemScanner(this.mailboxSession, versionedId.ObjectId));
							if (itemList.Count >= 2000)
							{
								goto Block_2;
							}
						}
					}
				}
				Block_2:
				return true;
			}, ItemSampler.dataColumns);
			return itemList;
		}

		// Token: 0x040009ED RID: 2541
		internal const int QueryResultBatchSize = 500;

		// Token: 0x040009EE RID: 2542
		internal const int SampleSize = 2000;

		// Token: 0x040009EF RID: 2543
		private const int ItemIdIndex = 0;

		// Token: 0x040009F0 RID: 2544
		private MailboxSession mailboxSession;

		// Token: 0x040009F1 RID: 2545
		private string toString;

		// Token: 0x040009F2 RID: 2546
		private static readonly PropertyDefinition[] dataColumns = new PropertyDefinition[]
		{
			ItemSchema.Id
		};

		// Token: 0x040009F3 RID: 2547
		private static readonly Trace Tracer = ExTraceGlobals.TopNAssistantTracer;

		// Token: 0x040009F4 RID: 2548
		private static readonly Trace TracerPfd = ExTraceGlobals.PFDTracer;
	}
}
