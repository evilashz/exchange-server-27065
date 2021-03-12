using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MobileTransport;

namespace Microsoft.Exchange.TextMessaging.MobileDriver
{
	// Token: 0x02000044 RID: 68
	internal class TextMessageDeliveringPipeline : DisposeTrackableBase
	{
		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000180 RID: 384 RVA: 0x00009A70 File Offset: 0x00007C70
		// (set) Token: 0x06000181 RID: 385 RVA: 0x00009A78 File Offset: 0x00007C78
		private ThreadSafeQueue<TextMessageDeliveryContext> DispatchingQueue { get; set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000182 RID: 386 RVA: 0x00009A81 File Offset: 0x00007C81
		// (set) Token: 0x06000183 RID: 387 RVA: 0x00009A89 File Offset: 0x00007C89
		private ThreadSafeQueue<TextMessageDeliveryContext> TranslatingQueue { get; set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000184 RID: 388 RVA: 0x00009A92 File Offset: 0x00007C92
		// (set) Token: 0x06000185 RID: 389 RVA: 0x00009A9A File Offset: 0x00007C9A
		private ThreadSafeQueue<TextMessageDeliveryContext> ComposingQueue { get; set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000186 RID: 390 RVA: 0x00009AA3 File Offset: 0x00007CA3
		// (set) Token: 0x06000187 RID: 391 RVA: 0x00009AAB File Offset: 0x00007CAB
		private ThreadSafeQueue<TextMessageDeliveryContext> DeliveringQueue { get; set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000188 RID: 392 RVA: 0x00009AB4 File Offset: 0x00007CB4
		// (set) Token: 0x06000189 RID: 393 RVA: 0x00009ABC File Offset: 0x00007CBC
		private ThreadSafeQueue<TextMessageDeliveryContext> ReportingQueue { get; set; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600018A RID: 394 RVA: 0x00009AC5 File Offset: 0x00007CC5
		// (set) Token: 0x0600018B RID: 395 RVA: 0x00009ACD File Offset: 0x00007CCD
		private ThreadSafeQueue<TextMessageDeliveryContext> CleaningQueue { get; set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600018C RID: 396 RVA: 0x00009AD6 File Offset: 0x00007CD6
		// (set) Token: 0x0600018D RID: 397 RVA: 0x00009ADE File Offset: 0x00007CDE
		private QueuePorter<TextMessageDeliveryContext> Worker { get; set; }

		// Token: 0x0600018E RID: 398 RVA: 0x00009AE8 File Offset: 0x00007CE8
		public TextMessageDeliveringPipeline()
		{
			this.DispatchingQueue = new ThreadSafeQueue<TextMessageDeliveryContext>();
			this.TranslatingQueue = new ThreadSafeQueue<TextMessageDeliveryContext>();
			this.ComposingQueue = new ThreadSafeQueue<TextMessageDeliveryContext>();
			this.DeliveringQueue = new ThreadSafeQueue<TextMessageDeliveryContext>();
			this.ReportingQueue = new ThreadSafeQueue<TextMessageDeliveryContext>();
			this.CleaningQueue = new ThreadSafeQueue<TextMessageDeliveryContext>();
			QueuePorterWorkingContext<TextMessageDeliveryContext>[] contexts = new QueuePorterWorkingContext<TextMessageDeliveryContext>[]
			{
				new QueuePorterWorkingContext<TextMessageDeliveryContext>(this.DispatchingQueue, new QueueDataAvailableEventHandler<TextMessageDeliveryContext>(this.DispatcherEventHandler), 1),
				new QueuePorterWorkingContext<TextMessageDeliveryContext>(this.TranslatingQueue, new QueueDataAvailableEventHandler<TextMessageDeliveryContext>(this.TranslatorEventHandler), 1),
				new QueuePorterWorkingContext<TextMessageDeliveryContext>(this.ComposingQueue, new QueueDataAvailableEventHandler<TextMessageDeliveryContext>(this.ComposerEventHandler), 1),
				new QueuePorterWorkingContext<TextMessageDeliveryContext>(this.DeliveringQueue, new QueueDataAvailableEventHandler<TextMessageDeliveryContext>(this.DelivererEventHandler), 1),
				new QueuePorterWorkingContext<TextMessageDeliveryContext>(this.ReportingQueue, new QueueDataAvailableEventHandler<TextMessageDeliveryContext>(this.ReporterEventHandler), 1),
				new QueuePorterWorkingContext<TextMessageDeliveryContext>(this.CleaningQueue, new QueueDataAvailableEventHandler<TextMessageDeliveryContext>(this.CleanerEventHandler), 1)
			};
			this.Worker = new QueuePorter<TextMessageDeliveryContext>(contexts, false);
			this.Start();
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00009BFB File Offset: 0x00007DFB
		public void Start()
		{
			base.CheckDisposed();
			this.Worker.Start();
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00009C0E File Offset: 0x00007E0E
		public void Stop()
		{
			base.CheckDisposed();
			this.Worker.Stop();
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00009C21 File Offset: 0x00007E21
		public void Deliver(TextMessageDeliveryContext context)
		{
			base.CheckDisposed();
			this.DispatchingQueue.Enqueue(context);
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00009C38 File Offset: 0x00007E38
		private void DispatcherEventHandler(QueueDataAvailableEventSource<TextMessageDeliveryContext> src, QueueDataAvailableEventArgs<TextMessageDeliveryContext> e)
		{
			bool flag = false;
			TextMessageDeliverer textMessageDeliverer = new TextMessageDeliverer(e.Item);
			try
			{
				flag = textMessageDeliverer.Stage0Dispatch();
			}
			catch (LocalizedException e2)
			{
				textMessageDeliverer.GenerateDsn(e2);
			}
			finally
			{
				if (flag)
				{
					this.TranslatingQueue.Enqueue(e.Item);
				}
				else
				{
					this.CleaningQueue.Enqueue(e.Item);
				}
			}
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00009CAC File Offset: 0x00007EAC
		private void TranslatorEventHandler(QueueDataAvailableEventSource<TextMessageDeliveryContext> src, QueueDataAvailableEventArgs<TextMessageDeliveryContext> e)
		{
			bool flag = false;
			TextMessageDeliverer textMessageDeliverer = new TextMessageDeliverer(e.Item);
			try
			{
				textMessageDeliverer.Stage1Translate();
				flag = true;
			}
			catch (LocalizedException e2)
			{
				textMessageDeliverer.GenerateDsn(e2);
			}
			finally
			{
				if (flag)
				{
					this.ComposingQueue.Enqueue(e.Item);
				}
				else
				{
					this.CleaningQueue.Enqueue(e.Item);
				}
			}
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00009D24 File Offset: 0x00007F24
		private void ComposerEventHandler(QueueDataAvailableEventSource<TextMessageDeliveryContext> src, QueueDataAvailableEventArgs<TextMessageDeliveryContext> e)
		{
			bool flag = false;
			TextMessageDeliverer textMessageDeliverer = new TextMessageDeliverer(e.Item);
			try
			{
				textMessageDeliverer.Stage2Compose();
				flag = true;
			}
			catch (LocalizedException e2)
			{
				textMessageDeliverer.GenerateDsn(e2);
			}
			finally
			{
				if (flag)
				{
					this.DeliveringQueue.Enqueue(e.Item);
				}
				else
				{
					this.CleaningQueue.Enqueue(e.Item);
				}
			}
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00009D9C File Offset: 0x00007F9C
		private void DelivererEventHandler(QueueDataAvailableEventSource<TextMessageDeliveryContext> src, QueueDataAvailableEventArgs<TextMessageDeliveryContext> e)
		{
			bool flag = false;
			TextMessageDeliverer textMessageDeliverer = new TextMessageDeliverer(e.Item);
			try
			{
				textMessageDeliverer.Stage3Deliver();
				flag = true;
			}
			catch (LocalizedException e2)
			{
				textMessageDeliverer.GenerateDsn(e2);
			}
			finally
			{
				if (flag)
				{
					this.ReportingQueue.Enqueue(e.Item);
				}
				else
				{
					this.CleaningQueue.Enqueue(e.Item);
				}
			}
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00009E14 File Offset: 0x00008014
		private void ReporterEventHandler(QueueDataAvailableEventSource<TextMessageDeliveryContext> src, QueueDataAvailableEventArgs<TextMessageDeliveryContext> e)
		{
			TextMessageDeliverer textMessageDeliverer = new TextMessageDeliverer(e.Item);
			try
			{
				textMessageDeliverer.Stage4Report();
			}
			catch (LocalizedException arg)
			{
				ExTraceGlobals.XsoTracer.TraceError<LocalizedException>((long)this.GetHashCode(), "ReporterEventHandler raises Exception: {0}", arg);
			}
			finally
			{
				this.CleaningQueue.Enqueue(e.Item);
			}
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00009E80 File Offset: 0x00008080
		private void CleanerEventHandler(QueueDataAvailableEventSource<TextMessageDeliveryContext> src, QueueDataAvailableEventArgs<TextMessageDeliveryContext> e)
		{
			if (e.Item.CleanerEventHandler == null)
			{
				return;
			}
			try
			{
				e.Item.CleanerEventHandler(src, e);
			}
			catch (LocalizedException arg)
			{
				ExTraceGlobals.XsoTracer.TraceError<LocalizedException>((long)this.GetHashCode(), "CleanerEventHandler raises Exception: {0}", arg);
			}
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00009EDC File Offset: 0x000080DC
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<TextMessageDeliveringPipeline>(this);
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00009EE4 File Offset: 0x000080E4
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				if (this.TranslatingQueue != null)
				{
					this.TranslatingQueue.Dispose();
					this.TranslatingQueue = null;
				}
				if (this.ComposingQueue != null)
				{
					this.ComposingQueue.Dispose();
					this.ComposingQueue = null;
				}
				if (this.DeliveringQueue != null)
				{
					this.DeliveringQueue.Dispose();
					this.DeliveringQueue = null;
				}
				if (this.ReportingQueue != null)
				{
					this.ReportingQueue.Dispose();
					this.ReportingQueue = null;
				}
				if (this.CleaningQueue != null)
				{
					this.CleaningQueue.Dispose();
					this.CleaningQueue = null;
				}
			}
		}
	}
}
