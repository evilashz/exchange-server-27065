using System;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x020003EA RID: 1002
	internal class PrintEventAreaVisual : PrintCalendarVisual
	{
		// Token: 0x060024BD RID: 9405 RVA: 0x000D5008 File Offset: 0x000D3208
		public PrintEventAreaVisual(ISessionContext sessionContext, EventAreaVisual visual, ICalendarDataSource dataSource, int dayCount) : base(sessionContext, visual, dataSource)
		{
			this.dayCount = dayCount;
			this.leftBreak = visual.LeftBreak;
			if (sessionContext.IsRtl)
			{
				base.Left = (double)this.dayCount - base.Left - base.Width;
			}
		}

		// Token: 0x170009C2 RID: 2498
		// (get) Token: 0x060024BE RID: 9406 RVA: 0x000D5058 File Offset: 0x000D3258
		protected override string TimeDescription
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				if (this.leftBreak)
				{
					stringBuilder.Append(base.StartTime.ToString(base.SessionContext.DateFormat));
				}
				if (base.StartTime.Minute != 0 || base.StartTime.Hour != 0)
				{
					if (this.leftBreak)
					{
						stringBuilder.Append(" ");
					}
					stringBuilder.Append(base.StartTime.ToString(base.SessionContext.TimeFormat));
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x060024BF RID: 9407 RVA: 0x000D50F0 File Offset: 0x000D32F0
		protected override void RenderVisualPosition(TextWriter writer)
		{
			writer.Write("left:");
			writer.Write(string.Format(CultureInfo.InvariantCulture, "{0:0.####}", new object[]
			{
				100.0 / (double)this.dayCount * base.Left
			}));
			writer.Write("%; top:");
			writer.Write(string.Format(CultureInfo.InvariantCulture, "{0:0.####}", new object[]
			{
				base.Top * 20.0
			}));
			writer.Write("px; width:");
			writer.Write(string.Format(CultureInfo.InvariantCulture, "{0:0.####}", new object[]
			{
				100.0 / (double)this.dayCount * base.Width
			}));
			writer.Write("%; height:");
			writer.Write(string.Format(CultureInfo.InvariantCulture, "{0:0.####}", new object[]
			{
				20
			}));
			writer.Write("px; ");
		}

		// Token: 0x060024C0 RID: 9408 RVA: 0x000D520A File Offset: 0x000D340A
		protected override void RenderExtraClasses(TextWriter writer)
		{
			writer.Write(" eventAreaVisual");
		}

		// Token: 0x0400197E RID: 6526
		public const int RowHeight = 20;

		// Token: 0x0400197F RID: 6527
		private int dayCount;

		// Token: 0x04001980 RID: 6528
		private bool leftBreak;
	}
}
