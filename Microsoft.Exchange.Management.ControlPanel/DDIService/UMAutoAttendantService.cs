using System;
using System.Collections.Generic;
using System.Data;
using System.ServiceModel;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.ControlPanel;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x020004D4 RID: 1236
	public class UMAutoAttendantService : DDICodeBehind
	{
		// Token: 0x06003C75 RID: 15477 RVA: 0x000B5AF4 File Offset: 0x000B3CF4
		public static void OnPostGetObject(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			if (dataTable.Rows.Count == 0 || store == null)
			{
				return;
			}
			DataRow dataRow = dataTable.Rows[0];
			UMAutoAttendant umautoAttendant = (UMAutoAttendant)store.GetDataObject("UMAutoAttendant");
			if (umautoAttendant != null)
			{
				UMDialPlan dialPlan = umautoAttendant.GetDialPlan();
				dataRow["ExtensionLength"] = dialPlan.NumberOfDigitsInExtension;
				dataRow["IsTelex"] = (dialPlan.URIType == UMUriType.TelExtn);
				List<UMAAMenuKeyMapping> value;
				List<UMAAMenuKeyMapping> value2;
				UMAAMenuKeyMapping.CreateMappings((MultiValuedProperty<CustomMenuKeyMapping>)dataRow["BusinessHoursKeyMapping"], (MultiValuedProperty<CustomMenuKeyMapping>)dataRow["AfterHoursKeyMapping"], out value, out value2);
				dataRow["BusinessHoursKeyMapping"] = value;
				dataRow["AfterHoursKeyMapping"] = value2;
				dataRow["BusinessHoursSchedule"] = new ScheduleBuilder(new Schedule(umautoAttendant.BusinessHoursSchedule)).GetEntireState();
			}
		}

		// Token: 0x06003C76 RID: 15478 RVA: 0x000B5BD0 File Offset: 0x000B3DD0
		public static void OnPostGetObjectForNew(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			if (dataTable.Rows.Count == 0)
			{
				return;
			}
			DataRow dataRow = dataTable.Rows[0];
			UMDialPlan umdialPlan = (UMDialPlan)store.GetDataObject("UMDialPlan");
			dataRow["UMDialPlan"] = umdialPlan.Name;
			dataRow["ExtensionLength"] = umdialPlan.NumberOfDigitsInExtension;
			dataRow["IsTelex"] = (umdialPlan.URIType == UMUriType.TelExtn);
		}

		// Token: 0x06003C77 RID: 15479 RVA: 0x000B5C49 File Offset: 0x000B3E49
		public static List<DropDownItemData> GetAvailableUmLanguages()
		{
			return UMDialPlanService.GetAvailableUmLanguages();
		}

		// Token: 0x06003C78 RID: 15480 RVA: 0x000B5C50 File Offset: 0x000B3E50
		public static List<DropDownItemData> GetAllTimeZones()
		{
			List<DropDownItemData> list = new List<DropDownItemData>();
			foreach (ExTimeZone exTimeZone in ExTimeZoneEnumerator.Instance)
			{
				list.Add(new DropDownItemData(exTimeZone.DisplayName, exTimeZone.Id));
			}
			return list;
		}

		// Token: 0x06003C79 RID: 15481 RVA: 0x000B5CB4 File Offset: 0x000B3EB4
		public static object GetInfoAnnouncementEnabled(string infoAnnouncementFilename, bool isInfoAnnouncementInterruptible)
		{
			return UMDialPlanService.GetInfoAnnouncementEnabled(infoAnnouncementFilename, isInfoAnnouncementInterruptible);
		}

		// Token: 0x06003C7A RID: 15482 RVA: 0x000B5CCA File Offset: 0x000B3ECA
		public static CustomMenuKeyMapping[] UMAAMenuKeyMappingToTask(object value)
		{
			if (!DDIHelper.IsEmptyValue(value))
			{
				return Array.ConvertAll<object, CustomMenuKeyMapping>((object[])value, (object x) => ((UMAAMenuKeyMapping)x).ToCustomKeyMapping());
			}
			return null;
		}

		// Token: 0x06003C7B RID: 15483 RVA: 0x000B5D0B File Offset: 0x000B3F0B
		public static HolidaySchedule[] UMAAHolidayScheduleToTask(object value)
		{
			if (!DDIHelper.IsEmptyValue(value))
			{
				return Array.ConvertAll<object, HolidaySchedule>((object[])value, (object x) => ((UMAAHolidaySchedule)x).ToHolidaySchedule());
			}
			return null;
		}

		// Token: 0x06003C7C RID: 15484 RVA: 0x000B5D48 File Offset: 0x000B3F48
		public static IEnumerable<ScheduleInterval> BoolArrayToScheduleInterval(object value)
		{
			if (DDIHelper.IsEmptyValue(value))
			{
				return null;
			}
			bool[] array = Array.ConvertAll<object, bool>((object[])value, (object x) => (bool)x);
			if (array.Length != 672)
			{
				throw new FaultException(Strings.EditUMAutoAttendantBusinessHoursScheduleFault(array.Length));
			}
			ScheduleBuilder scheduleBuilder = new ScheduleBuilder();
			scheduleBuilder.SetEntireState(array);
			return scheduleBuilder.Schedule.Intervals;
		}

		// Token: 0x040027A4 RID: 10148
		private const string UMDialPlanName = "UMDialPlan";

		// Token: 0x040027A5 RID: 10149
		private const string ExtensionLengthName = "ExtensionLength";

		// Token: 0x040027A6 RID: 10150
		private const string IsTelexName = "IsTelex";

		// Token: 0x040027A7 RID: 10151
		private const string UMAutoAttendantName = "UMAutoAttendant";

		// Token: 0x040027A8 RID: 10152
		private const string BusinessHoursScheduleName = "BusinessHoursSchedule";

		// Token: 0x040027A9 RID: 10153
		private const string BusinessHoursKeyMappingName = "BusinessHoursKeyMapping";

		// Token: 0x040027AA RID: 10154
		private const string AfterHoursKeyMappingName = "AfterHoursKeyMapping";

		// Token: 0x040027AB RID: 10155
		private const int BusinessHoursScheduleStructureLength = 672;
	}
}
