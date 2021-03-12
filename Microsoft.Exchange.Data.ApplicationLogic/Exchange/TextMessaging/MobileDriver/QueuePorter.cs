using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MobileTransport;

namespace Microsoft.Exchange.TextMessaging.MobileDriver
{
	// Token: 0x020001AF RID: 431
	internal class QueuePorter<T>
	{
		// Token: 0x0600107B RID: 4219 RVA: 0x00043A9C File Offset: 0x00041C9C
		public QueuePorter(IList<QueuePorterWorkingContext<T>> contexts, bool allowToHandleErrors)
		{
			if (contexts == null)
			{
				throw new ArgumentNullException("contexts");
			}
			this.AllowToHandleErrors = allowToHandleErrors;
			this.Contexts = (contexts.IsReadOnly ? contexts : new ReadOnlyCollection<QueuePorterWorkingContext<T>>(contexts));
			this.StopRequested = true;
			this.Watcher = new Thread(new ThreadStart(this.Processor));
			this.Watcher.IsBackground = true;
		}

		// Token: 0x0600107C RID: 4220 RVA: 0x00043B08 File Offset: 0x00041D08
		public QueuePorter(ThreadSafeQueue<T> queue, QueueDataAvailableEventHandler<T> dataAvailableEventHandler, bool allowToHandleErrors) : this(new QueuePorterWorkingContext<T>[]
		{
			new QueuePorterWorkingContext<T>(queue, dataAvailableEventHandler, 1)
		}, allowToHandleErrors)
		{
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x0600107D RID: 4221 RVA: 0x00043B2F File Offset: 0x00041D2F
		// (set) Token: 0x0600107E RID: 4222 RVA: 0x00043B37 File Offset: 0x00041D37
		private IList<QueuePorterWorkingContext<T>> Contexts { get; set; }

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x0600107F RID: 4223 RVA: 0x00043B40 File Offset: 0x00041D40
		// (set) Token: 0x06001080 RID: 4224 RVA: 0x00043B48 File Offset: 0x00041D48
		private Thread Watcher { get; set; }

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06001081 RID: 4225 RVA: 0x00043B51 File Offset: 0x00041D51
		// (set) Token: 0x06001082 RID: 4226 RVA: 0x00043B59 File Offset: 0x00041D59
		private bool AllowToHandleErrors { get; set; }

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06001083 RID: 4227 RVA: 0x00043B62 File Offset: 0x00041D62
		// (set) Token: 0x06001084 RID: 4228 RVA: 0x00043B6C File Offset: 0x00041D6C
		private bool StopRequested
		{
			get
			{
				return this.stopRequested;
			}
			set
			{
				this.stopRequested = value;
				if (value)
				{
					this.StopRequestedWaitHandle.Set();
					return;
				}
				this.StopRequestedWaitHandle.Reset();
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06001085 RID: 4229 RVA: 0x00043B9E File Offset: 0x00041D9E
		private ManualResetEvent StopRequestedWaitHandle
		{
			get
			{
				if (this.stopRequestedWaitHandle == null)
				{
					this.stopRequestedWaitHandle = new ManualResetEvent(this.StopRequested);
				}
				return this.stopRequestedWaitHandle;
			}
		}

		// Token: 0x06001086 RID: 4230 RVA: 0x00043BC0 File Offset: 0x00041DC0
		public void Start()
		{
			lock (this)
			{
				if (this.StopRequested)
				{
					while (ThreadState.Stopped != (ThreadState.Stopped & this.Watcher.ThreadState) && ThreadState.Unstarted != (ThreadState.Unstarted & this.Watcher.ThreadState))
					{
						Thread.SpinWait(100);
					}
					this.StopRequested = false;
					this.Watcher.Start();
				}
			}
		}

		// Token: 0x06001087 RID: 4231 RVA: 0x00043C3C File Offset: 0x00041E3C
		public void Stop()
		{
			lock (this)
			{
				this.StopRequested = true;
			}
		}

		// Token: 0x06001088 RID: 4232 RVA: 0x00043C78 File Offset: 0x00041E78
		public void Processor()
		{
			List<WaitHandle> list = new List<WaitHandle>(1 + this.Contexts.Count);
			list.Add(this.StopRequestedWaitHandle);
			foreach (QueuePorterWorkingContext<T> queuePorterWorkingContext in this.Contexts)
			{
				list.Add(queuePorterWorkingContext.Queue.DataAvailable);
			}
			WaitHandle[] waitHandles = list.ToArray();
			int num;
			while (!this.StopRequested && 0 < (num = WaitHandle.WaitAny(waitHandles)))
			{
				int index = num - 1;
				T item;
				while (!this.StopRequested && this.Contexts[index].Queue.Dequeue(out item))
				{
					ThreadPool.QueueUserWorkItem(new WaitCallback(this.HandleDataAvailableEvent), new QueuePorter<T>.DequeuedItem(index, item));
				}
			}
		}

		// Token: 0x06001089 RID: 4233 RVA: 0x00043D5C File Offset: 0x00041F5C
		private void HandleDataAvailableEvent(object state)
		{
			QueuePorter<T>.DequeuedItem dequeuedItem = (QueuePorter<T>.DequeuedItem)state;
			try
			{
				this.Contexts[dequeuedItem.ContextIndex].OnDataAvailable(new QueueDataAvailableEventSource<T>(this.Contexts[dequeuedItem.ContextIndex].Queue), new QueueDataAvailableEventArgs<T>(dequeuedItem.Item));
			}
			catch (Exception ex)
			{
				ExTraceGlobals.CoreTracer.TraceError<Exception>((long)this.GetHashCode(), "OnDataAvailable raises Exception: {0}", ex);
				if (!this.AllowToHandleErrors || !GrayException.IsGrayException(ex))
				{
					throw;
				}
				ExWatson.SendReport(ex, ReportOptions.None, null);
			}
		}

		// Token: 0x040008C5 RID: 2245
		private const int SpinWaitInterval = 100;

		// Token: 0x040008C6 RID: 2246
		private bool stopRequested;

		// Token: 0x040008C7 RID: 2247
		private ManualResetEvent stopRequestedWaitHandle;

		// Token: 0x020001B0 RID: 432
		private struct DequeuedItem
		{
			// Token: 0x0600108A RID: 4234 RVA: 0x00043DF8 File Offset: 0x00041FF8
			public DequeuedItem(int index, T item)
			{
				this.contextIndex = index;
				this.item = item;
			}

			// Token: 0x170003F9 RID: 1017
			// (get) Token: 0x0600108B RID: 4235 RVA: 0x00043E08 File Offset: 0x00042008
			public int ContextIndex
			{
				get
				{
					return this.contextIndex;
				}
			}

			// Token: 0x170003FA RID: 1018
			// (get) Token: 0x0600108C RID: 4236 RVA: 0x00043E10 File Offset: 0x00042010
			public T Item
			{
				get
				{
					return this.item;
				}
			}

			// Token: 0x040008CB RID: 2251
			private int contextIndex;

			// Token: 0x040008CC RID: 2252
			private T item;
		}
	}
}
