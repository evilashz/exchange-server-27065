using System;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x02000187 RID: 391
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	public class WorkingHours
	{
		// Token: 0x06000A6A RID: 2666 RVA: 0x0002C4F0 File Offset: 0x0002A6F0
		internal static WorkingHours LoadFrom(MailboxSession session, StoreId folderId)
		{
			StorageWorkingHours storageWorkingHours = StorageWorkingHours.LoadFrom(session, folderId);
			if (storageWorkingHours == null)
			{
				return null;
			}
			return new WorkingHours
			{
				storageWorkingHours = storageWorkingHours
			};
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x0002C518 File Offset: 0x0002A718
		internal static WorkingHours CreateDefaultWorkingHours(ExTimeZone timeZone)
		{
			return WorkingHours.Create(timeZone, DaysOfWeek.Monday | DaysOfWeek.Tuesday | DaysOfWeek.Wednesday | DaysOfWeek.Thursday | DaysOfWeek.Friday, 480, 1020);
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x0002C52C File Offset: 0x0002A72C
		internal static WorkingHours Create(ExTimeZone timeZone, DaysOfWeek daysOfWeek, int startTimeInMinutes, int endTimeInMinutes)
		{
			return new WorkingHours(timeZone, daysOfWeek, startTimeInMinutes, endTimeInMinutes);
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x0002C538 File Offset: 0x0002A738
		internal void SaveTo(MailboxSession session, StoreId folderId)
		{
			if (this.WorkingPeriodArray == null || this.WorkingPeriodArray.Length != 1 || this.WorkingPeriodArray[0] == null)
			{
				throw new ArgumentException("WorkingPeriodArray", "WorkingPeriodArray must have one element");
			}
			WorkingPeriod workingPeriod = this.WorkingPeriodArray[0];
			this.storageWorkingHours.SaveTo(session, folderId);
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x0002C588 File Offset: 0x0002A788
		private WorkingHours(ExTimeZone timeZone, DaysOfWeek daysOfWeek, int startTimeInMinutes, int endTimeInMinutes)
		{
			if (timeZone == null)
			{
				throw new ArgumentException("timeZone");
			}
			this.storageWorkingHours = StorageWorkingHours.Create(timeZone, (int)WorkingHours.ToStorageDaysOfWeek(daysOfWeek), startTimeInMinutes, endTimeInMinutes);
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000A6F RID: 2671 RVA: 0x0002C5B3 File Offset: 0x0002A7B3
		// (set) Token: 0x06000A70 RID: 2672 RVA: 0x0002C5CA File Offset: 0x0002A7CA
		[DataMember]
		[XmlElement(IsNullable = false)]
		public SerializableTimeZone TimeZone
		{
			get
			{
				if (this.HasNullDelegate())
				{
					return null;
				}
				return new SerializableTimeZone(this.ExTimeZone);
			}
			set
			{
				if (value != null)
				{
					this.ExTimeZone = value.TimeZone;
				}
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000A71 RID: 2673 RVA: 0x0002C5DC File Offset: 0x0002A7DC
		// (set) Token: 0x06000A72 RID: 2674 RVA: 0x0002C615 File Offset: 0x0002A815
		[XmlArray(IsNullable = false)]
		[XmlArrayItem(Type = typeof(WorkingPeriod), IsNullable = false)]
		[DataMember]
		public WorkingPeriod[] WorkingPeriodArray
		{
			get
			{
				if (this.HasNullDelegate())
				{
					return null;
				}
				return new WorkingPeriod[]
				{
					new WorkingPeriod(this.DaysOfWeek, this.StartTimeInMinutes, this.EndTimeInMinutes)
				};
			}
			set
			{
				if (value == null || value.Length != 1)
				{
					throw new ArgumentException("WorkingPeriodArray can not be null or have more than one element in Version1.");
				}
				this.storageWorkingHours.UpdateWorkingPeriod((DaysOfWeek)value[0].DayOfWeek, value[0].StartTimeInMinutes, value[0].EndTimeInMinutes);
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000A73 RID: 2675 RVA: 0x0002C64E File Offset: 0x0002A84E
		// (set) Token: 0x06000A74 RID: 2676 RVA: 0x0002C665 File Offset: 0x0002A865
		[XmlIgnore]
		internal ExTimeZone ExTimeZone
		{
			get
			{
				if (this.storageWorkingHours == null)
				{
					return null;
				}
				return this.storageWorkingHours.TimeZone;
			}
			set
			{
				if (this.storageWorkingHours != null)
				{
					this.storageWorkingHours.TimeZone = value;
				}
			}
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x0002C67C File Offset: 0x0002A87C
		public override string ToString()
		{
			if (this.toString == null)
			{
				StringBuilder stringBuilder = new StringBuilder(100);
				stringBuilder.AppendFormat("TimeZone = {0}\n", this.ExTimeZone);
				if (this.WorkingPeriodArray == null || this.WorkingPeriodArray.Length == 0)
				{
					stringBuilder.Append("<no working hours>");
				}
				else
				{
					stringBuilder.AppendFormat("WorkingPeriods\n", new object[0]);
					foreach (WorkingPeriod arg in this.WorkingPeriodArray)
					{
						stringBuilder.AppendFormat("  WorkingPeriod : {0}\n", arg);
					}
				}
				this.toString = stringBuilder.ToString();
			}
			return this.toString;
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x0002C715 File Offset: 0x0002A915
		public WorkingHours()
		{
			this.storageWorkingHours = StorageWorkingHours.Create(ExTimeZone.CurrentTimeZone);
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x0002C730 File Offset: 0x0002A930
		public bool InWorkingHours(ExDateTime startUtc, ExDateTime endUtc)
		{
			DaysOfWeek daysOfWeek;
			int num;
			this.UtcToWorkTime(startUtc, out daysOfWeek, out num);
			DaysOfWeek daysOfWeek2;
			int num2;
			this.UtcToWorkTime(endUtc, out daysOfWeek2, out num2);
			if (this.WorkingPeriodArray == null || this.WorkingPeriodArray.Length != 1)
			{
				throw new ArgumentException("WorkingPeriodArray can not be null or have more than one element in Version1.");
			}
			WorkingPeriod workingPeriod = this.WorkingPeriodArray[0];
			if (daysOfWeek == daysOfWeek2 && (daysOfWeek & workingPeriod.DayOfWeek) != (DaysOfWeek)0 && num >= workingPeriod.StartTimeInMinutes && num <= workingPeriod.EndTimeInMinutes && num2 >= workingPeriod.StartTimeInMinutes && num2 <= workingPeriod.EndTimeInMinutes)
			{
				return true;
			}
			if (daysOfWeek != daysOfWeek2 && workingPeriod.StartTimeInMinutes == 0 && workingPeriod.EndTimeInMinutes >= 1439)
			{
				DaysOfWeek daysOfWeek3 = daysOfWeek;
				while ((daysOfWeek3 & workingPeriod.DayOfWeek) != (DaysOfWeek)0)
				{
					if (daysOfWeek3 == daysOfWeek2)
					{
						return true;
					}
					daysOfWeek3 = WorkingHours.NextDays(daysOfWeek3);
				}
				return false;
			}
			return false;
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x0002C7F4 File Offset: 0x0002A9F4
		public static DaysOfWeek DayToDays(DayOfWeek dow)
		{
			switch (dow)
			{
			case DayOfWeek.Sunday:
				return DaysOfWeek.Sunday;
			case DayOfWeek.Monday:
				return DaysOfWeek.Monday;
			case DayOfWeek.Tuesday:
				return DaysOfWeek.Tuesday;
			case DayOfWeek.Wednesday:
				return DaysOfWeek.Wednesday;
			case DayOfWeek.Thursday:
				return DaysOfWeek.Thursday;
			case DayOfWeek.Friday:
				return DaysOfWeek.Friday;
			case DayOfWeek.Saturday:
				return DaysOfWeek.Saturday;
			default:
				throw new ArgumentException("dow");
			}
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x0002C844 File Offset: 0x0002AA44
		public static DaysOfWeek NextDays(DaysOfWeek d)
		{
			if (d <= DaysOfWeek.Wednesday)
			{
				switch (d)
				{
				case DaysOfWeek.Sunday:
					return DaysOfWeek.Monday;
				case DaysOfWeek.Monday:
					return DaysOfWeek.Tuesday;
				case DaysOfWeek.Sunday | DaysOfWeek.Monday:
					break;
				case DaysOfWeek.Tuesday:
					return DaysOfWeek.Wednesday;
				default:
					if (d == DaysOfWeek.Wednesday)
					{
						return DaysOfWeek.Thursday;
					}
					break;
				}
			}
			else
			{
				if (d == DaysOfWeek.Thursday)
				{
					return DaysOfWeek.Friday;
				}
				if (d == DaysOfWeek.Friday)
				{
					return DaysOfWeek.Saturday;
				}
				if (d == DaysOfWeek.Saturday)
				{
					return DaysOfWeek.Sunday;
				}
			}
			throw new ArgumentException("d");
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000A7A RID: 2682 RVA: 0x0002C8A1 File Offset: 0x0002AAA1
		public DaysOfWeek DaysOfWeek
		{
			get
			{
				if (this.HasNullDelegate())
				{
					return (DaysOfWeek)0;
				}
				return (DaysOfWeek)this.storageWorkingHours.DaysOfWeek;
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000A7B RID: 2683 RVA: 0x0002C8B8 File Offset: 0x0002AAB8
		public int StartTimeInMinutes
		{
			get
			{
				if (this.HasNullDelegate())
				{
					return 0;
				}
				return this.storageWorkingHours.StartTimeInMinutes;
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000A7C RID: 2684 RVA: 0x0002C8CF File Offset: 0x0002AACF
		public int EndTimeInMinutes
		{
			get
			{
				if (this.HasNullDelegate())
				{
					return 0;
				}
				return this.storageWorkingHours.EndTimeInMinutes;
			}
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x0002C8E6 File Offset: 0x0002AAE6
		private static DaysOfWeek FromStorageDaysOfWeek(DaysOfWeek daysOfWeek)
		{
			return (DaysOfWeek)daysOfWeek;
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x0002C8E9 File Offset: 0x0002AAE9
		private static DaysOfWeek ToStorageDaysOfWeek(DaysOfWeek daysOfWeek)
		{
			return (DaysOfWeek)daysOfWeek;
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x0002C8EC File Offset: 0x0002AAEC
		private bool HasNullDelegate()
		{
			return this.storageWorkingHours == null;
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x0002C8F8 File Offset: 0x0002AAF8
		private void UtcToWorkTime(ExDateTime utcTime, out DaysOfWeek day, out int minuteOfDay)
		{
			ExDateTime exDateTime = (this.ExTimeZone ?? ExTimeZone.CurrentTimeZone).ConvertDateTime(utcTime);
			day = WorkingHours.DayToDays(exDateTime.DayOfWeek);
			minuteOfDay = exDateTime.Hour * 60 + exDateTime.Minute;
		}

		// Token: 0x04000800 RID: 2048
		private StorageWorkingHours storageWorkingHours;

		// Token: 0x04000801 RID: 2049
		private string toString;
	}
}
