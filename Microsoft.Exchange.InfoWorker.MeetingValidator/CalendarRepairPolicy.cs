using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Infoworker.MeetingValidator
{
	// Token: 0x02000027 RID: 39
	internal sealed class CalendarRepairPolicy
	{
		// Token: 0x06000135 RID: 309 RVA: 0x0000A333 File Offset: 0x00008533
		private CalendarRepairPolicy()
		{
		}

		// Token: 0x06000136 RID: 310 RVA: 0x0000A33C File Offset: 0x0000853C
		internal static CalendarRepairPolicy CreateInstance(string name)
		{
			return new CalendarRepairPolicy
			{
				repairFlags = new HashSet<CalendarInconsistencyFlag>(),
				RepairMode = CalendarRepairType.ValidateOnly,
				name = name
			};
		}

		// Token: 0x06000137 RID: 311 RVA: 0x0000A369 File Offset: 0x00008569
		private void AddRepairFlags(CalendarInconsistencyFlag flag)
		{
			if (!this.ContainsRepairFlag(flag))
			{
				this.repairFlags.Add(flag);
				this.RepairMode = CalendarRepairType.RepairAndValidate;
			}
		}

		// Token: 0x06000138 RID: 312 RVA: 0x0000A388 File Offset: 0x00008588
		public void RemoveRepairFlags(params CalendarInconsistencyFlag[] flags)
		{
			if (flags != null)
			{
				bool flag = false;
				foreach (CalendarInconsistencyFlag calendarInconsistencyFlag in flags)
				{
					if (calendarInconsistencyFlag != CalendarInconsistencyFlag.None)
					{
						flag |= this.repairFlags.Remove(calendarInconsistencyFlag);
					}
				}
				if (flag)
				{
					this.RepairMode = ((this.repairFlags.Count > 1) ? CalendarRepairType.RepairAndValidate : CalendarRepairType.ValidateOnly);
				}
			}
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0000A3DB File Offset: 0x000085DB
		internal bool ContainsRepairFlag(CalendarInconsistencyFlag flag)
		{
			return flag == CalendarInconsistencyFlag.None || this.repairFlags.Contains(flag);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x0000A3F0 File Offset: 0x000085F0
		public void InitializeWithDefaults()
		{
			this.daysInWindowForward = 45;
			this.daysInWindowBackward = 45;
			foreach (object obj in Enum.GetValues(typeof(CalendarInconsistencyFlag)))
			{
				CalendarInconsistencyFlag flag = (CalendarInconsistencyFlag)obj;
				this.AddRepairFlags(flag);
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600013B RID: 315 RVA: 0x0000A464 File Offset: 0x00008664
		// (set) Token: 0x0600013C RID: 316 RVA: 0x0000A46C File Offset: 0x0000866C
		internal string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600013D RID: 317 RVA: 0x0000A475 File Offset: 0x00008675
		// (set) Token: 0x0600013E RID: 318 RVA: 0x0000A47D File Offset: 0x0000867D
		internal int DaysInWindowForward
		{
			get
			{
				return this.daysInWindowForward;
			}
			set
			{
				if (value >= 0)
				{
					this.daysInWindowForward = value;
					return;
				}
				throw new ArgumentOutOfRangeException("DaysInWindowForward");
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600013F RID: 319 RVA: 0x0000A495 File Offset: 0x00008695
		// (set) Token: 0x06000140 RID: 320 RVA: 0x0000A49D File Offset: 0x0000869D
		internal int DaysInWindowBackward
		{
			get
			{
				return this.daysInWindowBackward;
			}
			set
			{
				if (value >= 0)
				{
					this.daysInWindowBackward = value;
					return;
				}
				throw new ArgumentOutOfRangeException("DaysInWindowBackward");
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000141 RID: 321 RVA: 0x0000A4B5 File Offset: 0x000086B5
		// (set) Token: 0x06000142 RID: 322 RVA: 0x0000A4BD File Offset: 0x000086BD
		internal CalendarRepairType RepairMode { get; set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000143 RID: 323 RVA: 0x0000A4C6 File Offset: 0x000086C6
		// (set) Token: 0x06000144 RID: 324 RVA: 0x0000A4CE File Offset: 0x000086CE
		internal int MaxThreads
		{
			get
			{
				return this.maxThreads;
			}
			set
			{
				this.maxThreads = value;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000145 RID: 325 RVA: 0x0000A4D7 File Offset: 0x000086D7
		// (set) Token: 0x06000146 RID: 326 RVA: 0x0000A4DF File Offset: 0x000086DF
		internal bool CopyRumsToSentItems { get; set; }

		// Token: 0x040000AC RID: 172
		public const int DefaultDaysForward = 45;

		// Token: 0x040000AD RID: 173
		private string name;

		// Token: 0x040000AE RID: 174
		private int daysInWindowForward;

		// Token: 0x040000AF RID: 175
		private int daysInWindowBackward;

		// Token: 0x040000B0 RID: 176
		private int maxThreads;

		// Token: 0x040000B1 RID: 177
		private HashSet<CalendarInconsistencyFlag> repairFlags;
	}
}
