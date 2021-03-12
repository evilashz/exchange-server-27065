using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Exchange.Services.OnlineMeetings
{
	// Token: 0x02000025 RID: 37
	internal class LogEntry
	{
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000FF RID: 255 RVA: 0x000054FF File Offset: 0x000036FF
		// (set) Token: 0x06000100 RID: 256 RVA: 0x00005507 File Offset: 0x00003707
		private Dictionary<string, int> exceptions { get; set; }

		// Token: 0x06000101 RID: 257 RVA: 0x00005510 File Offset: 0x00003710
		internal LogEntry()
		{
			this.exceptions = new Dictionary<string, int>();
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00005523 File Offset: 0x00003723
		// (set) Token: 0x06000103 RID: 259 RVA: 0x0000552B File Offset: 0x0000372B
		public bool IsDeadlineExpired { get; set; }

		// Token: 0x06000104 RID: 260 RVA: 0x00005534 File Offset: 0x00003734
		internal void AddExceptionToLog(Exception ex)
		{
			if (ex != null)
			{
				StringBuilder stringBuilder = new StringBuilder(ex.GetType().Name);
				if (ex.InnerException != null)
				{
					stringBuilder.AppendFormat("_{0}_{1}", ex.InnerException.GetType().Name, ex.InnerException.Message);
				}
				else
				{
					stringBuilder.AppendFormat("_{0}_{1}", "NoInnerException", ex.Message);
				}
				this.AddToFailuresDictionary(stringBuilder.ToString());
			}
		}

		// Token: 0x06000105 RID: 261 RVA: 0x000055AC File Offset: 0x000037AC
		private void AddToFailuresDictionary(string key)
		{
			if (this.exceptions.ContainsKey(key))
			{
				int num = this.exceptions[key];
				this.exceptions[key] = num++;
				return;
			}
			this.exceptions.Add(key, 1);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x000055F4 File Offset: 0x000037F4
		internal string BuildFailureString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (KeyValuePair<string, int> keyValuePair in this.exceptions)
			{
				stringBuilder.Append(string.Format("{0}_{1}|", keyValuePair.Key, keyValuePair.Value));
			}
			return stringBuilder.ToString().TrimEnd(new char[]
			{
				'|'
			});
		}

		// Token: 0x04000105 RID: 261
		private const string noInnerExceptionStr = "NoInnerException";
	}
}
