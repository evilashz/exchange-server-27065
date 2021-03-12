using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x0200000A RID: 10
	public class LogData
	{
		// Token: 0x06000029 RID: 41 RVA: 0x0000242C File Offset: 0x0000062C
		public LogData()
		{
			this.tracker.Start();
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002481 File Offset: 0x00000681
		internal static string[] LogColumnNames
		{
			get
			{
				return Enum.GetNames(typeof(LogKey));
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002492 File Offset: 0x00000692
		internal static LogKey[] LogKeys
		{
			get
			{
				return (LogKey[])Enum.GetValues(typeof(LogKey));
			}
		}

		// Token: 0x17000006 RID: 6
		internal object this[LogKey key]
		{
			get
			{
				if (key == LogKey.GenericInfo)
				{
					return this.genericInfo.ToString() + ";" + this.detailedLatencyInfo.ToString();
				}
				if (key == LogKey.GenericErrors)
				{
					return this.errorInfo.ToString();
				}
				if (this.data.Keys.Contains(key))
				{
					return this.data[key];
				}
				return null;
			}
			set
			{
				string text = string.Empty;
				if (value != null)
				{
					text = LogData.EscapeStringForCsvFormat(value.ToString());
				}
				if (key == LogKey.GenericInfo || key == LogKey.GenericErrors)
				{
					throw new InvalidOperationException("Cannot set GenericInfo or ErrorInfo directly. Use 'Append' methods.");
				}
				if (!this.data.ContainsKey(key))
				{
					this.data.Add(key, text);
					return;
				}
				string.Format("Attempting to overwrite logdata for key {0} with {1}.", key, text);
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002578 File Offset: 0x00000778
		public void AppendGenericInfo(string key, object value)
		{
			string text = LogData.EscapeStringForCsvFormat(value.ToString());
			if (!string.IsNullOrEmpty(key))
			{
				text = LogData.EscapeSeperatorsInGenericInfo(text);
				this.genericInfo.Append(key + "=" + text + ";");
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000025C0 File Offset: 0x000007C0
		public void AppendErrorInfo(string key, object value)
		{
			string text = LogData.EscapeStringForCsvFormat(value.ToString());
			if (!string.IsNullOrEmpty(key))
			{
				text = LogData.EscapeSeperatorsInGenericInfo(text);
				this.errorInfo.Append(key + "=" + text + ";");
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002608 File Offset: 0x00000808
		public void LogElapsedTimeInDetailedLatencyInfo(string key)
		{
			this.detailedLatencyInfo.AppendFormat("{0}{1}{2}{3}", new object[]
			{
				key,
				"=",
				this.GetElapsedTime(),
				";"
			});
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002650 File Offset: 0x00000850
		internal long GetElapsedTime()
		{
			return this.tracker.ElapsedMilliseconds;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000265D File Offset: 0x0000085D
		private static string EscapeStringForCsvFormat(string value)
		{
			if (value.Contains(","))
			{
				value = value.Replace(',', ' ');
			}
			if (value.Contains("\r\n"))
			{
				value = value.Replace("\r\n", " ");
			}
			return value;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002698 File Offset: 0x00000898
		private static string EscapeSeperatorsInGenericInfo(string value)
		{
			if (value.Contains("="))
			{
				value = value.Replace("=", " ");
			}
			if (value.Contains(";"))
			{
				value = value.Replace(";", " ");
			}
			return value;
		}

		// Token: 0x04000058 RID: 88
		public const string KeyValueSeparator = "=";

		// Token: 0x04000059 RID: 89
		public const string PairSeparator = ";";

		// Token: 0x0400005A RID: 90
		private Dictionary<LogKey, object> data = new Dictionary<LogKey, object>();

		// Token: 0x0400005B RID: 91
		private StringBuilder genericInfo = new StringBuilder();

		// Token: 0x0400005C RID: 92
		private StringBuilder errorInfo = new StringBuilder();

		// Token: 0x0400005D RID: 93
		private Stopwatch tracker = new Stopwatch();

		// Token: 0x0400005E RID: 94
		private StringBuilder detailedLatencyInfo = new StringBuilder();
	}
}
