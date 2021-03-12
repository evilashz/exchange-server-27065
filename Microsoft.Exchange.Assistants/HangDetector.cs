using System;
using System.Threading;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x020000BA RID: 186
	internal abstract class HangDetector
	{
		// Token: 0x0600057A RID: 1402 RVA: 0x0001B4C0 File Offset: 0x000196C0
		public HangDetector()
		{
			this.MonitoredThread = Thread.CurrentThread;
			this.Timeout = HangDetector.DefaultTimeout;
			this.Period = HangDetector.DefaultPeriod;
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x0600057B RID: 1403 RVA: 0x0001B4FB File Offset: 0x000196FB
		// (set) Token: 0x0600057C RID: 1404 RVA: 0x0001B503 File Offset: 0x00019703
		public Thread MonitoredThread { get; private set; }

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x0600057D RID: 1405 RVA: 0x0001B50C File Offset: 0x0001970C
		// (set) Token: 0x0600057E RID: 1406 RVA: 0x0001B514 File Offset: 0x00019714
		public int HangsDetected { get; private set; }

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x0600057F RID: 1407 RVA: 0x0001B51D File Offset: 0x0001971D
		// (set) Token: 0x06000580 RID: 1408 RVA: 0x0001B525 File Offset: 0x00019725
		public TimeSpan Timeout { get; set; }

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000581 RID: 1409 RVA: 0x0001B52E File Offset: 0x0001972E
		// (set) Token: 0x06000582 RID: 1410 RVA: 0x0001B536 File Offset: 0x00019736
		public TimeSpan Period { get; set; }

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000583 RID: 1411 RVA: 0x0001B53F File Offset: 0x0001973F
		// (set) Token: 0x06000584 RID: 1412 RVA: 0x0001B547 File Offset: 0x00019747
		public DateTime InvokeTime { get; private set; }

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000585 RID: 1413 RVA: 0x0001B550 File Offset: 0x00019750
		// (set) Token: 0x06000586 RID: 1414 RVA: 0x0001B558 File Offset: 0x00019758
		public string AssistantName
		{
			get
			{
				return this.assistantName;
			}
			set
			{
				if (string.IsNullOrWhiteSpace(value))
				{
					this.assistantName = "not set by assistant";
					return;
				}
				this.assistantName = value;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000587 RID: 1415 RVA: 0x0001B575 File Offset: 0x00019775
		// (set) Token: 0x06000588 RID: 1416 RVA: 0x0001B57D File Offset: 0x0001977D
		public string DatabaseName { get; set; }

		// Token: 0x06000589 RID: 1417 RVA: 0x0001B588 File Offset: 0x00019788
		public void InvokeUnderHangDetection(HangDetector.InvokeDelegate delegateToBeInvoked)
		{
			this.HangsDetected = 0;
			this.timerIsDisposed = false;
			using (new Timer(new TimerCallback(this.HangDetectionCallback), null, this.Timeout, this.Period))
			{
				this.InvokeTime = DateTime.UtcNow;
				delegateToBeInvoked(this);
				this.timerIsDisposed = true;
			}
		}

		// Token: 0x0600058A RID: 1418
		protected abstract void OnHangDetected(int hangsDetected);

		// Token: 0x0600058B RID: 1419 RVA: 0x0001B5F8 File Offset: 0x000197F8
		private void HangDetectionCallback(object stateNotUsed)
		{
			if (this.timerIsDisposed)
			{
				return;
			}
			this.OnHangDetected(this.HangsDetected++);
		}

		// Token: 0x04000356 RID: 854
		public const string DefaultAssistantName = "Common Code";

		// Token: 0x04000357 RID: 855
		private static readonly TimeSpan DefaultTimeout = TimeSpan.FromMinutes(1.0);

		// Token: 0x04000358 RID: 856
		private static readonly TimeSpan DefaultPeriod = TimeSpan.FromMinutes(30.0);

		// Token: 0x04000359 RID: 857
		private bool timerIsDisposed = true;

		// Token: 0x0400035A RID: 858
		private string assistantName = "Common Code";

		// Token: 0x020000BB RID: 187
		// (Invoke) Token: 0x0600058E RID: 1422
		public delegate void InvokeDelegate(HangDetector hangDetector);
	}
}
