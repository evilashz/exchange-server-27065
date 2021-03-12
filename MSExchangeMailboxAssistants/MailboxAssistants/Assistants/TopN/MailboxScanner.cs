using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.Assistants.TopN;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.Common.TopN;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.TopN
{
	// Token: 0x02000189 RID: 393
	internal class MailboxScanner
	{
		// Token: 0x06000F9E RID: 3998 RVA: 0x0005C512 File Offset: 0x0005A712
		public override string ToString()
		{
			if (this.toString == null)
			{
				this.toString = "MailboxScanner for user " + this.mailboxSession.MailboxOwner + ". ";
			}
			return this.toString;
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06000F9F RID: 3999 RVA: 0x0005C542 File Offset: 0x0005A742
		internal MailboxSession MailboxSession
		{
			get
			{
				return this.mailboxSession;
			}
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06000FA0 RID: 4000 RVA: 0x0005C54A File Offset: 0x0005A74A
		internal TopNAssistant Assistant
		{
			get
			{
				return this.assistant;
			}
		}

		// Token: 0x06000FA1 RID: 4001 RVA: 0x0005C552 File Offset: 0x0005A752
		internal MailboxScanner(MailboxSession mailboxSession)
		{
			this.mailboxSession = mailboxSession;
			this.assistant = null;
		}

		// Token: 0x06000FA2 RID: 4002 RVA: 0x0005C568 File Offset: 0x0005A768
		internal MailboxScanner(MailboxSession mailboxSession, TopNAssistant assistant)
		{
			this.mailboxSession = mailboxSession;
			this.assistant = assistant;
		}

		// Token: 0x06000FA3 RID: 4003 RVA: 0x0005C590 File Offset: 0x0005A790
		internal void ProcessMailbox()
		{
			MailboxScanner.Tracer.TraceDebug<MailboxScanner>((long)this.GetHashCode(), "{0}: Starting to process mailbox.", this);
			TopNConfiguration topNConfiguration = new TopNConfiguration(this.mailboxSession);
			if (this.assistant != null)
			{
				this.assistant.ThrowIfShuttingDown(this.mailboxSession.MailboxOwner);
			}
			if (!topNConfiguration.ReadMetaData())
			{
				MailboxScanner.Tracer.TraceDebug<MailboxScanner>((long)this.GetHashCode(), "{0}: Skipping this mailbox because config.ReadMetaData() failed.", this);
				return;
			}
			if (!topNConfiguration.ScanRequested)
			{
				MailboxScanner.Tracer.TraceDebug<MailboxScanner>((long)this.GetHashCode(), "{0}: Skipping this mailbox because there's no request pending.", this);
				return;
			}
			ExDateTime exDateTime = topNConfiguration.LastScanTime + TopNConfiguration.UpdateInterval;
			if (ExDateTime.Now < exDateTime)
			{
				MailboxScanner.Tracer.TraceDebug<MailboxScanner, ExDateTime>((long)this.GetHashCode(), "{0}: Skipping this mailbox because we're not past the next due time: {1}.", this, exDateTime);
				return;
			}
			if (this.assistant != null)
			{
				this.assistant.ThrowIfShuttingDown(this.mailboxSession.MailboxOwner);
			}
			ItemSampler itemSampler = new ItemSampler(this.mailboxSession);
			List<ItemScanner> itemsToScan = itemSampler.GetItemsToScan();
			if (itemsToScan == null || itemsToScan.Count == 0)
			{
				MailboxScanner.Tracer.TraceDebug<MailboxScanner>((long)this.GetHashCode(), "{0}: Skipping this mailbox because there are no items to scan.", this);
				return;
			}
			Dictionary<string, int> dictionary = new Dictionary<string, int>(10);
			foreach (ItemScanner itemScanner in itemsToScan)
			{
				if (this.assistant != null)
				{
					this.assistant.ThrowIfShuttingDown(this.mailboxSession.MailboxOwner);
				}
				Dictionary<string, int> dictionary2 = itemScanner.Scan();
				if (dictionary2 != null && dictionary2.Count != 0)
				{
					foreach (KeyValuePair<string, int> keyValuePair in dictionary2)
					{
						int num = 0;
						if (dictionary.TryGetValue(keyValuePair.Key, out num))
						{
							dictionary[keyValuePair.Key] = num + 1;
						}
						else if (dictionary.Count < 100000)
						{
							dictionary[keyValuePair.Key] = 1;
						}
					}
				}
			}
			if (dictionary.Count == 0)
			{
				MailboxScanner.Tracer.TraceDebug<MailboxScanner>((long)this.GetHashCode(), "{0}: Empty Mailbox, aborting TopN.", this);
				return;
			}
			KeyValuePair<string, int>[] array = new KeyValuePair<string, int>[dictionary.Count];
			int num2 = 0;
			foreach (KeyValuePair<string, int> keyValuePair2 in dictionary)
			{
				array[num2++] = keyValuePair2;
			}
			Array.Sort<KeyValuePair<string, int>>(array, (KeyValuePair<string, int> a, KeyValuePair<string, int> b) => -(a.Value - b.Value));
			MailboxScanner.Tracer.TraceDebug<MailboxScanner>((long)this.GetHashCode(), "{0}: TopN table built. Attempting save.", this);
			topNConfiguration.Version = this.mailboxSession.MailboxOwner.MailboxInfo.Location.ServerVersion;
			topNConfiguration.LastScanTime = ExDateTime.Now;
			topNConfiguration.WordFrequency = array;
			topNConfiguration.Save(false);
			MailboxScanner.Tracer.TraceDebug<MailboxScanner>((long)this.GetHashCode(), "{0}: TopN table saved.", this);
		}

		// Token: 0x040009E6 RID: 2534
		internal const int MaxWords = 100000;

		// Token: 0x040009E7 RID: 2535
		private MailboxSession mailboxSession;

		// Token: 0x040009E8 RID: 2536
		private TopNAssistant assistant;

		// Token: 0x040009E9 RID: 2537
		private string toString;

		// Token: 0x040009EA RID: 2538
		private static readonly Trace Tracer = ExTraceGlobals.TopNAssistantTracer;

		// Token: 0x040009EB RID: 2539
		private static readonly Trace TracerPfd = ExTraceGlobals.PFDTracer;
	}
}
