using System;
using System.Threading;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.ManagementGUI.Resources;
using Microsoft.ManagementGUI;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200015C RID: 348
	public class ScheduleDataHandler : ExchangeDataHandler
	{
		// Token: 0x06000E12 RID: 3602 RVA: 0x0003557F File Offset: 0x0003377F
		public ScheduleDataHandler() : this(null)
		{
		}

		// Token: 0x06000E13 RID: 3603 RVA: 0x00035588 File Offset: 0x00033788
		public ScheduleDataHandler(DataHandler nestedDataHandler) : this(nestedDataHandler, null, Unlimited<uint>.UnlimitedValue)
		{
		}

		// Token: 0x06000E14 RID: 3604 RVA: 0x000355AC File Offset: 0x000337AC
		public ScheduleDataHandler(DataHandler nestedDataHandler, DateTime? startTime, Unlimited<uint> intervalHours)
		{
			if (nestedDataHandler != null)
			{
				base.DataHandlers.Add(nestedDataHandler);
			}
			this.StartTaskAtScheduledTime = (startTime != null);
			this.StartTime = ((startTime != null) ? startTime.Value : ((DateTime)ExDateTime.Now));
			this.IntervalHours = intervalHours;
			base.DataSource = this;
		}

		// Token: 0x06000E15 RID: 3605 RVA: 0x00035624 File Offset: 0x00033824
		internal override void OnReadData(CommandInteractionHandler interactionHandler, string pageName)
		{
			try
			{
				base.OnReadData(interactionHandler, pageName);
			}
			finally
			{
				base.DataSource = this;
			}
		}

		// Token: 0x06000E16 RID: 3606 RVA: 0x00035654 File Offset: 0x00033854
		internal override void OnSaveData(CommandInteractionHandler interactionHandler)
		{
			Timer timer = null;
			Timer timer2 = null;
			try
			{
				if (this.StartTaskAtScheduledTime && this.StartTime > (DateTime)ExDateTime.Now)
				{
					this.startEvent.Reset();
					timer = new Timer(new TimerCallback(this.OnStart), null, 0, 1000);
				}
				if (WaitHandle.WaitAny(new WaitHandle[]
				{
					this.startEvent,
					this.cancelEvent
				}) == 0 && !base.Cancelled)
				{
					if (this.CancelTaskAtScheduledTime)
					{
						this.cancelEvent.Reset();
						timer2 = new Timer(new TimerCallback(this.OnCancel), null, (long)((ulong)this.IntervalHours.Value * 3600UL * 1000UL), -1L);
					}
					base.OnSaveData(interactionHandler);
				}
			}
			finally
			{
				if (timer != null)
				{
					timer.Dispose();
				}
				if (timer2 != null)
				{
					timer2.Dispose();
				}
			}
		}

		// Token: 0x06000E17 RID: 3607 RVA: 0x00035744 File Offset: 0x00033944
		public override void Cancel()
		{
			base.Cancel();
			this.cancelEvent.Set();
		}

		// Token: 0x06000E18 RID: 3608 RVA: 0x00035758 File Offset: 0x00033958
		private void OnStart(object target)
		{
			this.OnCountDown();
			if ((this.StartTime - (DateTime)ExDateTime.Now).TotalSeconds < 1.0)
			{
				this.startEvent.Set();
			}
		}

		// Token: 0x06000E19 RID: 3609 RVA: 0x0003579F File Offset: 0x0003399F
		private void OnCancel(object target)
		{
			this.Cancel();
		}

		// Token: 0x06000E1A RID: 3610 RVA: 0x000357A8 File Offset: 0x000339A8
		private void OnCountDown()
		{
			EventHandler eventHandler = this.countDown;
			if (eventHandler != null)
			{
				eventHandler(this, EventArgs.Empty);
			}
		}

		// Token: 0x14000054 RID: 84
		// (add) Token: 0x06000E1B RID: 3611 RVA: 0x000357CB File Offset: 0x000339CB
		// (remove) Token: 0x06000E1C RID: 3612 RVA: 0x000357E4 File Offset: 0x000339E4
		public event EventHandler CountDown
		{
			add
			{
				this.countDown = (EventHandler)SynchronizedDelegate.Combine(this.countDown, value);
			}
			remove
			{
				this.countDown = (EventHandler)SynchronizedDelegate.Remove(this.countDown, value);
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000E1D RID: 3613 RVA: 0x000357FD File Offset: 0x000339FD
		// (set) Token: 0x06000E1E RID: 3614 RVA: 0x00035805 File Offset: 0x00033A05
		public DateTime StartTime
		{
			get
			{
				return this.startTime;
			}
			set
			{
				this.startTime = value;
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000E1F RID: 3615 RVA: 0x0003580E File Offset: 0x00033A0E
		// (set) Token: 0x06000E20 RID: 3616 RVA: 0x00035818 File Offset: 0x00033A18
		public Unlimited<uint> IntervalHours
		{
			get
			{
				return this.intervalHours;
			}
			set
			{
				bool flag = value != Unlimited<uint>.UnlimitedValue;
				if (flag && (value.Value > 48U || value.Value < 1U))
				{
					throw new ArgumentException(Strings.IntervalOutOfRange(1U, 48U));
				}
				this.CancelTaskAtScheduledTime = flag;
				this.intervalHours = value;
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000E21 RID: 3617 RVA: 0x0003586A File Offset: 0x00033A6A
		// (set) Token: 0x06000E22 RID: 3618 RVA: 0x00035872 File Offset: 0x00033A72
		public bool CancelTaskAtScheduledTime
		{
			get
			{
				return this.cancelTaskAtScheduledTime;
			}
			set
			{
				this.cancelTaskAtScheduledTime = value;
			}
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000E23 RID: 3619 RVA: 0x0003587B File Offset: 0x00033A7B
		// (set) Token: 0x06000E24 RID: 3620 RVA: 0x00035883 File Offset: 0x00033A83
		public bool StartTaskAtScheduledTime
		{
			get
			{
				return this.startTaskAtScheduledTime;
			}
			set
			{
				this.startTaskAtScheduledTime = value;
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000E25 RID: 3621 RVA: 0x0003588C File Offset: 0x00033A8C
		// (set) Token: 0x06000E26 RID: 3622 RVA: 0x00035894 File Offset: 0x00033A94
		public bool DoNotApply
		{
			get
			{
				return this.doNotApply;
			}
			set
			{
				this.doNotApply = value;
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000E27 RID: 3623 RVA: 0x000358A0 File Offset: 0x00033AA0
		public string RemainTimeText
		{
			get
			{
				string result = string.Empty;
				TimeSpan timeSpan = this.StartTime - (DateTime)ExDateTime.Now;
				if (timeSpan.TotalSeconds >= 1.0 && this.StartTaskAtScheduledTime)
				{
					if (timeSpan.Days > 0)
					{
						result = Strings.CountDownDays((long)timeSpan.Days, (long)timeSpan.Hours, (long)timeSpan.Minutes, (long)timeSpan.Seconds);
					}
					else if (timeSpan.Hours > 0)
					{
						result = Strings.CountDownHours((long)timeSpan.Hours, (long)timeSpan.Minutes, (long)timeSpan.Seconds);
					}
					else if (timeSpan.Minutes > 0)
					{
						result = Strings.CountDownMinutes((long)timeSpan.Minutes, (long)timeSpan.Seconds);
					}
					else
					{
						result = Strings.CountDownSeconds((long)timeSpan.Seconds);
					}
				}
				return result;
			}
		}

		// Token: 0x04000598 RID: 1432
		internal const uint MaximumIntervalHours = 48U;

		// Token: 0x04000599 RID: 1433
		internal const uint MinimumIntervalHours = 1U;

		// Token: 0x0400059A RID: 1434
		private ManualResetEvent cancelEvent = new ManualResetEvent(false);

		// Token: 0x0400059B RID: 1435
		private ManualResetEvent startEvent = new ManualResetEvent(true);

		// Token: 0x0400059C RID: 1436
		private bool cancelTaskAtScheduledTime;

		// Token: 0x0400059D RID: 1437
		private bool startTaskAtScheduledTime;

		// Token: 0x0400059E RID: 1438
		private DateTime startTime;

		// Token: 0x0400059F RID: 1439
		private Unlimited<uint> intervalHours;

		// Token: 0x040005A0 RID: 1440
		private EventHandler countDown;

		// Token: 0x040005A1 RID: 1441
		private bool doNotApply;
	}
}
