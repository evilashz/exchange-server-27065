using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000408 RID: 1032
	internal sealed class ReminderDropDownList : DropDownList
	{
		// Token: 0x0600255F RID: 9567 RVA: 0x000D843A File Offset: 0x000D663A
		public ReminderDropDownList(string id, double reminderTime) : base(id, reminderTime.ToString(), null)
		{
			this.reminderTime = reminderTime;
		}

		// Token: 0x06002560 RID: 9568 RVA: 0x000D8459 File Offset: 0x000D6659
		public ReminderDropDownList(string id, double reminderTime, bool isSnooze) : base(id, reminderTime.ToString(), null)
		{
			this.reminderTime = reminderTime;
			this.isSnooze = isSnooze;
		}

		// Token: 0x06002561 RID: 9569 RVA: 0x000D8480 File Offset: 0x000D6680
		protected override void RenderSelectedValue(TextWriter writer)
		{
			for (int i = 0; i < ReminderDropDownList.reminderTimes.Length; i++)
			{
				ReminderDropDownList.ReminderTime reminderTime = ReminderDropDownList.reminderTimes[i];
				if (this.reminderTime == ReminderDropDownList.reminderTimes[i].Time)
				{
					writer.Write(SanitizedHtmlString.FromStringId(ReminderDropDownList.reminderTimes[i].Display));
					return;
				}
			}
			this.isStockTime = false;
			writer.Write(DateTimeUtilities.FormatDuration((int)this.reminderTime));
		}

		// Token: 0x06002562 RID: 9570 RVA: 0x000D8500 File Offset: 0x000D6700
		protected override DropDownListItem[] CreateListItems()
		{
			List<DropDownListItem> list = new List<DropDownListItem>();
			int i = 0;
			if (this.isSnooze)
			{
				while (i < ReminderDropDownList.snoozeTimes.Length)
				{
					list.Add(new DropDownListItem(ReminderDropDownList.snoozeTimes[i].Time, ReminderDropDownList.snoozeTimes[i].Display));
					i++;
				}
				i = 1;
			}
			while (i < ReminderDropDownList.reminderTimes.Length)
			{
				list.Add(new DropDownListItem(ReminderDropDownList.reminderTimes[i].Time, ReminderDropDownList.reminderTimes[i].Display));
				if (!this.isStockTime && (i == ReminderDropDownList.reminderTimes.Length - 1 || (ReminderDropDownList.reminderTimes[i].Time < this.reminderTime && this.reminderTime < ReminderDropDownList.reminderTimes[i + 1].Time)))
				{
					list.Add(new DropDownListItem(this.reminderTime, DateTimeUtilities.FormatDuration((int)this.reminderTime), false));
				}
				i++;
			}
			return list.ToArray();
		}

		// Token: 0x040019DB RID: 6619
		private static readonly ReminderDropDownList.ReminderTime[] reminderTimes = new ReminderDropDownList.ReminderTime[]
		{
			new ReminderDropDownList.ReminderTime(0.0, -1884236483),
			new ReminderDropDownList.ReminderTime(5.0, 2007446286),
			new ReminderDropDownList.ReminderTime(10.0, 2098102450),
			new ReminderDropDownList.ReminderTime(15.0, 2098102453),
			new ReminderDropDownList.ReminderTime(30.0, 935303036),
			new ReminderDropDownList.ReminderTime(60.0, 104450136),
			new ReminderDropDownList.ReminderTime(120.0, 1670534077),
			new ReminderDropDownList.ReminderTime(180.0, -1058349278),
			new ReminderDropDownList.ReminderTime(240.0, 507734663),
			new ReminderDropDownList.ReminderTime(480.0, -1105403445),
			new ReminderDropDownList.ReminderTime(720.0, 2000165286),
			new ReminderDropDownList.ReminderTime(1440.0, -1685054980),
			new ReminderDropDownList.ReminderTime(2880.0, -1685054979),
			new ReminderDropDownList.ReminderTime(4320.0, -1685054978),
			new ReminderDropDownList.ReminderTime(10080.0, 636052262),
			new ReminderDropDownList.ReminderTime(20160.0, -930031679)
		};

		// Token: 0x040019DC RID: 6620
		private static readonly ReminderDropDownList.ReminderTime[] snoozeTimes = new ReminderDropDownList.ReminderTime[]
		{
			new ReminderDropDownList.ReminderTime(-15.0, 84937006),
			new ReminderDropDownList.ReminderTime(-10.0, 844451893),
			new ReminderDropDownList.ReminderTime(-5.0, 1844088193),
			new ReminderDropDownList.ReminderTime(0.0, 1844088196)
		};

		// Token: 0x040019DD RID: 6621
		private static readonly double MinutesInHour = 60.0;

		// Token: 0x040019DE RID: 6622
		private static readonly double MinutesInDay = ReminderDropDownList.MinutesInHour * 24.0;

		// Token: 0x040019DF RID: 6623
		private double reminderTime;

		// Token: 0x040019E0 RID: 6624
		private bool isStockTime = true;

		// Token: 0x040019E1 RID: 6625
		private bool isSnooze;

		// Token: 0x02000409 RID: 1033
		private struct ReminderTime
		{
			// Token: 0x06002564 RID: 9572 RVA: 0x000D88D5 File Offset: 0x000D6AD5
			public ReminderTime(double time, Strings.IDs display)
			{
				this.time = time;
				this.display = display;
			}

			// Token: 0x170009EC RID: 2540
			// (get) Token: 0x06002565 RID: 9573 RVA: 0x000D88E5 File Offset: 0x000D6AE5
			public double Time
			{
				get
				{
					return this.time;
				}
			}

			// Token: 0x170009ED RID: 2541
			// (get) Token: 0x06002566 RID: 9574 RVA: 0x000D88ED File Offset: 0x000D6AED
			public Strings.IDs Display
			{
				get
				{
					return this.display;
				}
			}

			// Token: 0x040019E2 RID: 6626
			private double time;

			// Token: 0x040019E3 RID: 6627
			private Strings.IDs display;
		}
	}
}
