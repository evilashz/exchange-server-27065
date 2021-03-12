using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020001DF RID: 479
	internal class CalendarItemTypeConverter : EnumConverter
	{
		// Token: 0x06000CC6 RID: 3270 RVA: 0x00041D80 File Offset: 0x0003FF80
		public static string ToString(CalendarItemType calendarItemType)
		{
			string result = null;
			switch (calendarItemType)
			{
			case CalendarItemType.Single:
				result = "Single";
				break;
			case CalendarItemType.Occurrence:
				result = "Occurrence";
				break;
			case CalendarItemType.Exception:
				result = "Exception";
				break;
			case CalendarItemType.RecurringMaster:
				result = "RecurringMaster";
				break;
			}
			return result;
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x00041DC8 File Offset: 0x0003FFC8
		public static CalendarItemType Parse(string calendarItemTypeString)
		{
			if (calendarItemTypeString != null)
			{
				CalendarItemType result;
				if (!(calendarItemTypeString == "Exception"))
				{
					if (!(calendarItemTypeString == "Occurrence"))
					{
						if (!(calendarItemTypeString == "RecurringMaster"))
						{
							if (!(calendarItemTypeString == "Single"))
							{
								goto IL_4D;
							}
							result = CalendarItemType.Single;
						}
						else
						{
							result = CalendarItemType.RecurringMaster;
						}
					}
					else
					{
						result = CalendarItemType.Occurrence;
					}
				}
				else
				{
					result = CalendarItemType.Exception;
				}
				return result;
			}
			IL_4D:
			throw new FormatException("Invalid calendarItemType string: " + calendarItemTypeString);
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x00041E34 File Offset: 0x00040034
		public override object ConvertToObject(string propertyString)
		{
			return CalendarItemTypeConverter.Parse(propertyString);
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x00041E41 File Offset: 0x00040041
		public override string ConvertToString(object propertyValue)
		{
			return CalendarItemTypeConverter.ToString((CalendarItemType)propertyValue);
		}

		// Token: 0x04000A57 RID: 2647
		private const string ExceptionStringValue = "Exception";

		// Token: 0x04000A58 RID: 2648
		private const string OccurrenceStringValue = "Occurrence";

		// Token: 0x04000A59 RID: 2649
		private const string RecurringMasterStringValue = "RecurringMaster";

		// Token: 0x04000A5A RID: 2650
		private const string SingleStringValue = "Single";
	}
}
