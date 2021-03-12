using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.Common;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000ED9 RID: 3801
	[Serializable]
	public class ChangeDate
	{
		// Token: 0x170022D1 RID: 8913
		// (get) Token: 0x0600832C RID: 33580 RVA: 0x0023AA5C File Offset: 0x00238C5C
		// (set) Token: 0x0600832D RID: 33581 RVA: 0x0023AAD0 File Offset: 0x00238CD0
		[XmlElement]
		public string Time
		{
			get
			{
				return string.Concat(new string[]
				{
					this.systemTime.Hour.ToString("00"),
					":",
					this.systemTime.Minute.ToString("00"),
					":",
					this.systemTime.Second.ToString("00")
				});
			}
			set
			{
				if (value == null)
				{
					Microsoft.Exchange.Diagnostics.Components.Data.Storage.ExTraceGlobals.WorkHoursTracer.TraceError((long)this.GetHashCode(), "Invalid Time: <null>");
					throw new WorkingHoursXmlMalformedException(ServerStrings.NullTimeInChangeDate);
				}
				string[] array = value.Split(new char[]
				{
					':'
				});
				if (array == null || array.Length != 3)
				{
					Microsoft.Exchange.Diagnostics.Components.Data.Storage.ExTraceGlobals.WorkHoursTracer.TraceError<string>((long)this.GetHashCode(), "Invalid Time: {0}", value);
					throw new WorkingHoursXmlMalformedException(ServerStrings.BadTimeFormatInChangeDate(value));
				}
				Exception ex = null;
				try
				{
					this.systemTime.Hour = Convert.ToUInt16(array[0]);
					this.systemTime.Minute = Convert.ToUInt16(array[1]);
					this.systemTime.Second = Convert.ToUInt16(array[2]);
				}
				catch (ArgumentException ex2)
				{
					ex = ex2;
				}
				catch (OverflowException ex3)
				{
					ex = ex3;
				}
				catch (FormatException ex4)
				{
					ex = ex4;
				}
				if (ex != null)
				{
					Microsoft.Exchange.Diagnostics.Components.Data.Storage.ExTraceGlobals.WorkHoursTracer.TraceError<string, Exception>((long)this.GetHashCode(), "Invalid Time: {0}. Exception={1}", value, ex);
					throw new WorkingHoursXmlMalformedException(ServerStrings.BadDateTimeFormatInChangeDate, ex);
				}
			}
		}

		// Token: 0x170022D2 RID: 8914
		// (get) Token: 0x0600832E RID: 33582 RVA: 0x0023ABE4 File Offset: 0x00238DE4
		// (set) Token: 0x0600832F RID: 33583 RVA: 0x0023AC58 File Offset: 0x00238E58
		[XmlElement]
		public string Date
		{
			get
			{
				return string.Concat(new string[]
				{
					this.systemTime.Year.ToString("00"),
					"/",
					this.systemTime.Month.ToString("00"),
					"/",
					this.systemTime.Day.ToString("00")
				});
			}
			set
			{
				if (value == null)
				{
					Microsoft.Exchange.Diagnostics.Components.Data.Storage.ExTraceGlobals.WorkHoursTracer.TraceError((long)this.GetHashCode(), "Invalid Date: <null>");
					throw new WorkingHoursXmlMalformedException(ServerStrings.NullDateInChangeDate);
				}
				string[] array = value.Split(new char[]
				{
					'/'
				});
				if (array == null || array.Length != 3)
				{
					Microsoft.Exchange.Diagnostics.Components.Data.Storage.ExTraceGlobals.WorkHoursTracer.TraceError<string>((long)this.GetHashCode(), "Invalid Date: {0}", value);
					throw new WorkingHoursXmlMalformedException(ServerStrings.BadDateFormatInChangeDate);
				}
				Exception ex = null;
				try
				{
					this.systemTime.Year = Convert.ToUInt16(array[0]);
					this.systemTime.Month = Convert.ToUInt16(array[1]);
					this.systemTime.Day = Convert.ToUInt16(array[2]);
				}
				catch (ArgumentException ex2)
				{
					ex = ex2;
				}
				catch (OverflowException ex3)
				{
					ex = ex3;
				}
				catch (FormatException ex4)
				{
					ex = ex4;
				}
				if (ex != null)
				{
					Microsoft.Exchange.Diagnostics.Components.Data.Storage.ExTraceGlobals.WorkHoursTracer.TraceError<string, Exception>((long)this.GetHashCode(), "Invalid Date: {0}. Exception={1}", value, ex);
					throw new WorkingHoursXmlMalformedException(ServerStrings.BadDateTimeFormatInChangeDate, ex);
				}
			}
		}

		// Token: 0x170022D3 RID: 8915
		// (get) Token: 0x06008330 RID: 33584 RVA: 0x0023AD68 File Offset: 0x00238F68
		// (set) Token: 0x06008331 RID: 33585 RVA: 0x0023AD76 File Offset: 0x00238F76
		[XmlElement]
		public short DayOfWeek
		{
			get
			{
				return (short)this.systemTime.DayOfWeek;
			}
			set
			{
				this.systemTime.DayOfWeek = (ushort)value;
			}
		}

		// Token: 0x06008332 RID: 33586 RVA: 0x0023AD85 File Offset: 0x00238F85
		public ChangeDate()
		{
		}

		// Token: 0x06008333 RID: 33587 RVA: 0x0023AD90 File Offset: 0x00238F90
		public override string ToString()
		{
			if (this.toString == null)
			{
				this.toString = string.Concat(new object[]
				{
					"Time=",
					this.Time,
					", Date=",
					this.Date,
					", DayOfWeek=",
					this.DayOfWeek
				});
			}
			return this.toString;
		}

		// Token: 0x06008334 RID: 33588 RVA: 0x0023ADF6 File Offset: 0x00238FF6
		internal ChangeDate(NativeMethods.SystemTime systemTime)
		{
			this.systemTime = systemTime;
		}

		// Token: 0x170022D4 RID: 8916
		// (get) Token: 0x06008335 RID: 33589 RVA: 0x0023AE05 File Offset: 0x00239005
		// (set) Token: 0x06008336 RID: 33590 RVA: 0x0023AE0D File Offset: 0x0023900D
		[XmlIgnore]
		internal NativeMethods.SystemTime SystemTime
		{
			get
			{
				return this.systemTime;
			}
			set
			{
				this.systemTime = value;
			}
		}

		// Token: 0x040057E9 RID: 22505
		[NonSerialized]
		private NativeMethods.SystemTime systemTime;

		// Token: 0x040057EA RID: 22506
		private string toString;

		// Token: 0x040057EB RID: 22507
		private static readonly Trace Tracer = Microsoft.Exchange.Diagnostics.Components.InfoWorker.Common.ExTraceGlobals.WorkingHoursTracer;
	}
}
