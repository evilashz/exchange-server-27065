using System;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x020003F5 RID: 1013
	internal class PrintSchedulingAreaVisual : PrintCalendarVisual
	{
		// Token: 0x06002524 RID: 9508 RVA: 0x000D6E94 File Offset: 0x000D5094
		public PrintSchedulingAreaVisual(ISessionContext sessionContext, SchedulingAreaVisual visual, ICalendarDataSource dataSource, int viewStartTime, int viewEndTime) : base(sessionContext, visual, dataSource)
		{
			this.viewStartTime = viewStartTime;
			TimeStripMode persistedTimeStripMode = DailyViewBase.GetPersistedTimeStripMode(base.SessionContext);
			int num = (persistedTimeStripMode == TimeStripMode.FifteenMinutes) ? 4 : 2;
			this.rowCount = (viewEndTime - viewStartTime) * num;
			if (base.Top < 0.0)
			{
				base.Height += base.Top;
				base.Top = 0.0;
			}
			if (base.Top + base.Height > (double)this.rowCount)
			{
				base.Height = (double)this.rowCount - base.Top;
			}
			if (base.Height < 1.0)
			{
				base.Height = 1.0;
			}
		}

		// Token: 0x170009E4 RID: 2532
		// (get) Token: 0x06002525 RID: 9509 RVA: 0x000D6F54 File Offset: 0x000D5154
		protected override string TimeDescription
		{
			get
			{
				if (base.StartTime.Hour < this.viewStartTime || base.StartTime.Minute != 0)
				{
					return base.StartTime.ToString(base.SessionContext.TimeFormat);
				}
				return string.Empty;
			}
		}

		// Token: 0x06002526 RID: 9510 RVA: 0x000D6FA8 File Offset: 0x000D51A8
		protected override void RenderVisualPosition(TextWriter writer)
		{
			writer.Write("top: ");
			writer.Write(string.Format(CultureInfo.InvariantCulture, "{0:0.####}", new object[]
			{
				100.0 / (double)this.rowCount * base.Top
			}));
			writer.Write("%; height: ");
			writer.Write(string.Format(CultureInfo.InvariantCulture, "{0:0.####}", new object[]
			{
				100.0 / (double)this.rowCount * base.Height
			}));
			writer.Write("%; width: ");
			writer.Write(string.Format(CultureInfo.InvariantCulture, "{0:0.####}", new object[]
			{
				100.0 * base.Width
			}));
			writer.Write("%; left: ");
			writer.Write(string.Format(CultureInfo.InvariantCulture, "{0:0.####}", new object[]
			{
				100.0 * base.Left
			}));
			writer.Write("%;");
		}

		// Token: 0x040019A9 RID: 6569
		public const int IconWidth = 12;

		// Token: 0x040019AA RID: 6570
		private int rowCount;

		// Token: 0x040019AB RID: 6571
		private int viewStartTime;
	}
}
