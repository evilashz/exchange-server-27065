using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.Common;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.Mwi
{
	// Token: 0x02000107 RID: 263
	internal class MwiDelayTable
	{
		// Token: 0x06000AD8 RID: 2776 RVA: 0x00046D50 File Offset: 0x00044F50
		internal MwiDelayTable(TimeSpan delayInterval, TimeSpan timerResolution, ProcessMailboxDelegate processMailboxCallback)
		{
			this.delayTable = new Dictionary<Guid, MwiDelayTable.DelayTableEntry>();
			this.delayQueue = new RetryQueue<MwiDelayTable.DelayTableEntry>(ExTraceGlobals.MWITracer, delayInterval);
			this.processMailboxCallback = processMailboxCallback;
			this.timerThreadSyncEvent = new AutoResetEvent(true);
			this.timer = new Timer(new TimerCallback(this.DelayTimerCallback), null, timerResolution, timerResolution);
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x00046DAC File Offset: 0x00044FAC
		internal void Shutdown(TimeSpan shutdownTimeout)
		{
			ExTraceGlobals.MWITracer.TraceDebug((long)this.GetHashCode(), "MwiDelayTable.Shutdown: Starting shutdown");
			if (this.timerThreadSyncEvent.WaitOne(shutdownTimeout, false))
			{
				ExTraceGlobals.MWITracer.TraceDebug((long)this.GetHashCode(), "MwiDelayTable.Shutdown: Got timerThreadRunning event. Disposing timer.");
				if (this.timer != null)
				{
					this.timer.Dispose();
					this.timer = null;
					return;
				}
			}
			else
			{
				ExTraceGlobals.MWITracer.TraceWarning<double>((long)this.GetHashCode(), "MwiDelayTable.Shutdown: Didn't get timerThreadRunning event within {0}s!", shutdownTimeout.TotalSeconds);
			}
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x00046E2C File Offset: 0x0004502C
		internal void EnqueueMailbox(MailboxInfo mailbox, MailboxSession mailboxSession)
		{
			ExTraceGlobals.MWITracer.TraceDebug<MailboxInfo, MailboxSession>((long)this.GetHashCode(), "MwiDelayTable.EnqueueMailbox({0}, {1})", mailbox, mailboxSession);
			bool flag = false;
			MwiDelayTable.DelayTableEntry delayTableEntry;
			lock (this)
			{
				if (!this.delayTable.TryGetValue(mailbox.Guid, out delayTableEntry))
				{
					ExTraceGlobals.MWITracer.TraceDebug<MailboxInfo>((long)this.GetHashCode(), "MwiDelayTable: Mailbox {0} not present, processing now", mailbox);
					flag = true;
					delayTableEntry = new MwiDelayTable.DelayTableEntry(mailbox);
					this.delayTable[mailbox.Guid] = delayTableEntry;
				}
				else
				{
					ExTraceGlobals.MWITracer.TraceDebug<MailboxInfo>((long)this.GetHashCode(), "MwiDelayTable: Mailbox {0} already present->ProcessWhenExpired=true.", mailbox);
					delayTableEntry.ProcessWhenExpired = true;
				}
			}
			if (flag)
			{
				this.ProcessEntryAndAddToDelayQueue(delayTableEntry, mailboxSession);
			}
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x00046EF0 File Offset: 0x000450F0
		private void DelayTimerCallback(object state)
		{
			List<MwiDelayTable.DelayTableEntry> list = null;
			bool flag = false;
			try
			{
				flag = this.timerThreadSyncEvent.WaitOne(0, false);
				if (!flag)
				{
					ExTraceGlobals.MWITracer.TraceWarning((long)this.GetHashCode(), "MwiDelayTable.DelayTimerCallback: Shutdown or overlaping timer calls");
				}
				else
				{
					lock (this)
					{
						MwiDelayTable.DelayTableEntry delayTableEntry;
						while ((delayTableEntry = this.delayQueue.Dequeue()) != null)
						{
							if (delayTableEntry.ProcessWhenExpired)
							{
								ExTraceGlobals.MWITracer.TraceDebug<MailboxInfo>((long)this.GetHashCode(), "MwiDelayTable.DelayTimerCallback: {0} expired but needs update.", delayTableEntry.Mailbox);
								if (list == null)
								{
									list = new List<MwiDelayTable.DelayTableEntry>();
								}
								delayTableEntry.ProcessWhenExpired = false;
								list.Add(delayTableEntry);
							}
							else
							{
								ExTraceGlobals.MWITracer.TraceDebug<MailboxInfo>((long)this.GetHashCode(), "MwiDelayTable.DelayTimerCallback: {0} expired, removing", delayTableEntry.Mailbox);
								this.delayTable.Remove(delayTableEntry.Mailbox.Guid);
							}
						}
					}
					if (list != null)
					{
						foreach (MwiDelayTable.DelayTableEntry entry in list)
						{
							this.ProcessEntryAndAddToDelayQueue(entry, null);
						}
					}
				}
			}
			catch (Exception ex)
			{
				ExTraceGlobals.MWITracer.TraceError<Exception>((long)this.GetHashCode(), "MwiDelayTable.DelayTimerCallback: {0}", ex);
				if (!GrayException.IsGrayException(ex))
				{
					throw;
				}
				ExWatson.SendReport(ex, ReportOptions.None, null);
			}
			finally
			{
				if (flag)
				{
					this.timerThreadSyncEvent.Set();
				}
			}
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x000470AC File Offset: 0x000452AC
		private void ProcessEntryAndAddToDelayQueue(MwiDelayTable.DelayTableEntry entry, MailboxSession mailboxSession)
		{
			ExTraceGlobals.MWITracer.TraceDebug<MailboxInfo>((long)this.GetHashCode(), "MwiDelayTable.ProcessEntryAndAddToDelayQueue: Processing {0}", entry.Mailbox);
			this.processMailboxCallback(entry.Mailbox, mailboxSession);
			lock (this)
			{
				ExTraceGlobals.MWITracer.TraceDebug<MailboxInfo>((long)this.GetHashCode(), "MwiDelayTable.ProcessEntryAndAddToDelayQueue: Adding {0} to delay queue.", entry.Mailbox);
				this.delayQueue.Enqueue(entry);
			}
		}

		// Token: 0x040006EA RID: 1770
		private ProcessMailboxDelegate processMailboxCallback;

		// Token: 0x040006EB RID: 1771
		private RetryQueue<MwiDelayTable.DelayTableEntry> delayQueue;

		// Token: 0x040006EC RID: 1772
		private Dictionary<Guid, MwiDelayTable.DelayTableEntry> delayTable;

		// Token: 0x040006ED RID: 1773
		private Timer timer;

		// Token: 0x040006EE RID: 1774
		private AutoResetEvent timerThreadSyncEvent;

		// Token: 0x02000108 RID: 264
		private class DelayTableEntry
		{
			// Token: 0x06000ADD RID: 2781 RVA: 0x00047138 File Offset: 0x00045338
			internal DelayTableEntry(MailboxInfo mailbox)
			{
				this.mailbox = mailbox;
				this.processWhenExpired = false;
			}

			// Token: 0x1700028F RID: 655
			// (get) Token: 0x06000ADE RID: 2782 RVA: 0x0004714E File Offset: 0x0004534E
			internal MailboxInfo Mailbox
			{
				get
				{
					return this.mailbox;
				}
			}

			// Token: 0x17000290 RID: 656
			// (get) Token: 0x06000ADF RID: 2783 RVA: 0x00047156 File Offset: 0x00045356
			// (set) Token: 0x06000AE0 RID: 2784 RVA: 0x0004715E File Offset: 0x0004535E
			internal bool ProcessWhenExpired
			{
				get
				{
					return this.processWhenExpired;
				}
				set
				{
					this.processWhenExpired = value;
				}
			}

			// Token: 0x040006EF RID: 1775
			private MailboxInfo mailbox;

			// Token: 0x040006F0 RID: 1776
			private bool processWhenExpired;
		}
	}
}
