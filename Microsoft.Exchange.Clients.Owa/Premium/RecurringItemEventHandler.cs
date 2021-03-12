using System;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x0200049D RID: 1181
	internal abstract class RecurringItemEventHandler : ItemEventHandler
	{
		// Token: 0x06002D89 RID: 11657 RVA: 0x000FFFB8 File Offset: 0x000FE1B8
		protected Recurrence CreateRecurrenceFromRequest()
		{
			Recurrence result = null;
			if (base.IsParameterSet("RcrT"))
			{
				OwaRecurrenceType owaRecurrenceType = (OwaRecurrenceType)base.GetParameter("RcrT");
				RecurrencePattern recurrencePattern = null;
				OwaRecurrenceType owaRecurrenceType2 = owaRecurrenceType;
				if (owaRecurrenceType2 <= (OwaRecurrenceType.Monthly | OwaRecurrenceType.MonthlyTh))
				{
					if (owaRecurrenceType2 <= OwaRecurrenceType.Monthly)
					{
						switch (owaRecurrenceType2)
						{
						case OwaRecurrenceType.Daily:
							recurrencePattern = new DailyRecurrencePattern((int)base.GetParameter("RcrI"));
							break;
						case OwaRecurrenceType.None | OwaRecurrenceType.Daily:
							break;
						case OwaRecurrenceType.Weekly:
							recurrencePattern = new WeeklyRecurrencePattern((DaysOfWeek)base.GetParameter("RcrDys"), (int)base.GetParameter("RcrI"));
							break;
						default:
							if (owaRecurrenceType2 == OwaRecurrenceType.Monthly)
							{
								recurrencePattern = new MonthlyRecurrencePattern((int)base.GetParameter("RcrDy"), (int)base.GetParameter("RcrI"));
							}
							break;
						}
					}
					else if (owaRecurrenceType2 != OwaRecurrenceType.Yearly)
					{
						if (owaRecurrenceType2 != (OwaRecurrenceType.Daily | OwaRecurrenceType.DailyEveryWeekday))
						{
							if (owaRecurrenceType2 == (OwaRecurrenceType.Monthly | OwaRecurrenceType.MonthlyTh))
							{
								recurrencePattern = new MonthlyThRecurrencePattern((DaysOfWeek)base.GetParameter("RcrDys"), (RecurrenceOrderType)base.GetParameter("RcrO"), (int)base.GetParameter("RcrI"));
							}
						}
						else
						{
							recurrencePattern = new WeeklyRecurrencePattern(DaysOfWeek.Weekdays);
						}
					}
					else
					{
						recurrencePattern = new YearlyRecurrencePattern((int)base.GetParameter("RcrDy"), (int)base.GetParameter("RcrM"));
					}
				}
				else if (owaRecurrenceType2 <= (OwaRecurrenceType.Daily | OwaRecurrenceType.DailyRegenerating))
				{
					if (owaRecurrenceType2 != (OwaRecurrenceType.Yearly | OwaRecurrenceType.YearlyTh))
					{
						if (owaRecurrenceType2 == (OwaRecurrenceType.Daily | OwaRecurrenceType.DailyRegenerating))
						{
							recurrencePattern = new DailyRegeneratingPattern((int)base.GetParameter("RgrI"));
						}
					}
					else
					{
						recurrencePattern = new YearlyThRecurrencePattern((DaysOfWeek)base.GetParameter("RcrDys"), (RecurrenceOrderType)base.GetParameter("RcrO"), (int)base.GetParameter("RcrM"));
					}
				}
				else if (owaRecurrenceType2 != (OwaRecurrenceType.Weekly | OwaRecurrenceType.WeeklyRegenerating))
				{
					if (owaRecurrenceType2 != (OwaRecurrenceType.Monthly | OwaRecurrenceType.MonthlyRegenerating))
					{
						if (owaRecurrenceType2 == (OwaRecurrenceType.Yearly | OwaRecurrenceType.YearlyRegenerating))
						{
							recurrencePattern = new YearlyRegeneratingPattern((int)base.GetParameter("RgrI"));
						}
					}
					else
					{
						recurrencePattern = new MonthlyRegeneratingPattern((int)base.GetParameter("RgrI"));
					}
				}
				else
				{
					recurrencePattern = new WeeklyRegeneratingPattern((int)base.GetParameter("RgrI"));
				}
				if (owaRecurrenceType != OwaRecurrenceType.None)
				{
					RecurrenceRangeType recurrenceRangeType = (RecurrenceRangeType)base.GetParameter("RcrRngT");
					ExDateTime startDate = (ExDateTime)base.GetParameter("RcrRngS");
					RecurrenceRange recurrenceRange;
					switch (recurrenceRangeType)
					{
					case RecurrenceRangeType.Numbered:
						recurrenceRange = new NumberedRecurrenceRange(startDate, (int)base.GetParameter("RcrRngO"));
						goto IL_2C8;
					case RecurrenceRangeType.EndDate:
						recurrenceRange = new EndDateRecurrenceRange(startDate, (ExDateTime)base.GetParameter("RcrRngE"));
						goto IL_2C8;
					}
					recurrenceRange = new NoEndRecurrenceRange(startDate);
					IL_2C8:
					if (recurrencePattern != null && recurrenceRange != null)
					{
						result = new Recurrence(recurrencePattern, recurrenceRange);
					}
				}
			}
			return result;
		}

		// Token: 0x04001E34 RID: 7732
		public const string Recurrence = "RcrT";

		// Token: 0x04001E35 RID: 7733
		public const string RecurrenceInterval = "RcrI";

		// Token: 0x04001E36 RID: 7734
		public const string RegeneratingInterval = "RgrI";

		// Token: 0x04001E37 RID: 7735
		public const string RecurrenceDays = "RcrDys";

		// Token: 0x04001E38 RID: 7736
		public const string RecurrenceDay = "RcrDy";

		// Token: 0x04001E39 RID: 7737
		public const string RecurrenceMonth = "RcrM";

		// Token: 0x04001E3A RID: 7738
		public const string RecurrenceOrder = "RcrO";

		// Token: 0x04001E3B RID: 7739
		public const string RecurrenceRangeT = "RcrRngT";

		// Token: 0x04001E3C RID: 7740
		public const string RecurrenceRangeStart = "RcrRngS";

		// Token: 0x04001E3D RID: 7741
		public const string RecurrenceRangeOccurences = "RcrRngO";

		// Token: 0x04001E3E RID: 7742
		public const string RecurrenceRangeEndDate = "RcrRngE";
	}
}
