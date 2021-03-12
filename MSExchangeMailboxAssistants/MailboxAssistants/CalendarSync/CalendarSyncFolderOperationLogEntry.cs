using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.MailboxAssistants.CalendarSync
{
	// Token: 0x020000CA RID: 202
	internal class CalendarSyncFolderOperationLogEntry
	{
		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000885 RID: 2181 RVA: 0x0003B227 File Offset: 0x00039427
		// (set) Token: 0x06000886 RID: 2182 RVA: 0x0003B22F File Offset: 0x0003942F
		private Dictionary<string, int> Exceptions { get; set; }

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000887 RID: 2183 RVA: 0x0003B238 File Offset: 0x00039438
		// (set) Token: 0x06000888 RID: 2184 RVA: 0x0003B240 File Offset: 0x00039440
		private Dictionary<string, int> Errors { get; set; }

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000889 RID: 2185 RVA: 0x0003B249 File Offset: 0x00039449
		// (set) Token: 0x0600088A RID: 2186 RVA: 0x0003B251 File Offset: 0x00039451
		internal List<List<KeyValuePair<string, object>>> SanitizedStackTraces { get; set; }

		// Token: 0x0600088B RID: 2187 RVA: 0x0003B25A File Offset: 0x0003945A
		internal CalendarSyncFolderOperationLogEntry()
		{
			this.Exceptions = new Dictionary<string, int>();
			this.Errors = new Dictionary<string, int>();
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x0600088C RID: 2188 RVA: 0x0003B278 File Offset: 0x00039478
		// (set) Token: 0x0600088D RID: 2189 RVA: 0x0003B280 File Offset: 0x00039480
		internal Guid MailboxGuid { get; set; }

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x0600088E RID: 2190 RVA: 0x0003B289 File Offset: 0x00039489
		// (set) Token: 0x0600088F RID: 2191 RVA: 0x0003B291 File Offset: 0x00039491
		internal string FolderId { get; set; }

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000890 RID: 2192 RVA: 0x0003B29A File Offset: 0x0003949A
		// (set) Token: 0x06000891 RID: 2193 RVA: 0x0003B2A2 File Offset: 0x000394A2
		internal string FolderUrl { get; set; }

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000892 RID: 2194 RVA: 0x0003B2AB File Offset: 0x000394AB
		// (set) Token: 0x06000893 RID: 2195 RVA: 0x0003B2B3 File Offset: 0x000394B3
		internal string FolderType { get; set; }

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000894 RID: 2196 RVA: 0x0003B2BC File Offset: 0x000394BC
		// (set) Token: 0x06000895 RID: 2197 RVA: 0x0003B2C4 File Offset: 0x000394C4
		internal string DisplayName { get; set; }

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000896 RID: 2198 RVA: 0x0003B2CD File Offset: 0x000394CD
		// (set) Token: 0x06000897 RID: 2199 RVA: 0x0003B2D5 File Offset: 0x000394D5
		internal bool IsOnDemandJob { get; set; }

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000898 RID: 2200 RVA: 0x0003B2DE File Offset: 0x000394DE
		// (set) Token: 0x06000899 RID: 2201 RVA: 0x0003B2E6 File Offset: 0x000394E6
		internal bool IsSyncSuccess { get; set; }

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x0600089A RID: 2202 RVA: 0x0003B2EF File Offset: 0x000394EF
		// (set) Token: 0x0600089B RID: 2203 RVA: 0x0003B2F7 File Offset: 0x000394F7
		internal ExDateTime LastAttemptedSyncTime { get; set; }

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x0600089C RID: 2204 RVA: 0x0003B300 File Offset: 0x00039500
		// (set) Token: 0x0600089D RID: 2205 RVA: 0x0003B308 File Offset: 0x00039508
		internal ExDateTime LastSyncSuccessTime { get; set; }

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x0600089E RID: 2206 RVA: 0x0003B311 File Offset: 0x00039511
		// (set) Token: 0x0600089F RID: 2207 RVA: 0x0003B319 File Offset: 0x00039519
		internal Guid TenantGuid { get; set; }

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x060008A0 RID: 2208 RVA: 0x0003B322 File Offset: 0x00039522
		// (set) Token: 0x060008A1 RID: 2209 RVA: 0x0003B32A File Offset: 0x0003952A
		internal Guid ActivityId { get; set; }

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x060008A2 RID: 2210 RVA: 0x0003B333 File Offset: 0x00039533
		// (set) Token: 0x060008A3 RID: 2211 RVA: 0x0003B33B File Offset: 0x0003953B
		internal PublishingSubscriptionData SubscriptionData { get; set; }

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x060008A4 RID: 2212 RVA: 0x0003B344 File Offset: 0x00039544
		// (set) Token: 0x060008A5 RID: 2213 RVA: 0x0003B34C File Offset: 0x0003954C
		public bool IsDeadlineExpired { get; set; }

		// Token: 0x060008A6 RID: 2214 RVA: 0x0003B358 File Offset: 0x00039558
		internal void AddExceptionToLog(Exception ex)
		{
			if (ex != null)
			{
				string text = ex.GetType().Name;
				if (ex.InnerException != null)
				{
					text = text + "_" + ex.InnerException.GetType().Name;
				}
				else
				{
					text += "_NoInnerException";
				}
				this.AddToStackTraceList(ex, text);
				this.AddToFailuresDictionary(this.Exceptions, text);
			}
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x0003B3BC File Offset: 0x000395BC
		internal void AddErrorToLog(string errorStr, string innerErrorStr)
		{
			if (!string.IsNullOrEmpty(errorStr))
			{
				string key;
				if (!string.IsNullOrEmpty(innerErrorStr))
				{
					key = errorStr + "_" + innerErrorStr;
				}
				else
				{
					key = errorStr + "_NoInnerException";
				}
				this.AddToFailuresDictionary(this.Errors, key);
			}
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x0003B404 File Offset: 0x00039604
		internal List<KeyValuePair<string, object>> FormatCustomData()
		{
			return new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("MGUID", string.Format("{0}", this.MailboxGuid)),
				new KeyValuePair<string, object>("FID", string.Format("{0}", this.FolderId)),
				new KeyValuePair<string, object>("FURL", string.Format("{0}", this.FolderUrl)),
				new KeyValuePair<string, object>("FTYP", string.Format("{0}", this.FolderType)),
				new KeyValuePair<string, object>("ODMD", string.Format("{0}", this.IsOnDemandJob)),
				new KeyValuePair<string, object>("SRES", string.Format("{0}", this.IsSyncSuccess)),
				new KeyValuePair<string, object>("LAST", string.Format("{0}", this.LastAttemptedSyncTime)),
				new KeyValuePair<string, object>("LSST", string.Format("{0}", this.LastSyncSuccessTime)),
				new KeyValuePair<string, object>("TGUID", string.Format("{0}", this.TenantGuid)),
				new KeyValuePair<string, object>("EX", this.BuildFailureString(this.Exceptions)),
				new KeyValuePair<string, object>("ERR", this.BuildFailureString(this.Errors))
			};
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x0003B590 File Offset: 0x00039790
		private void AddToFailuresDictionary(Dictionary<string, int> failures, string key)
		{
			if (failures.ContainsKey(key))
			{
				int num = failures[key];
				failures[key] = num++;
				return;
			}
			failures.Add(key, 1);
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x0003B5C4 File Offset: 0x000397C4
		private void AddToStackTraceList(Exception ex, string key)
		{
			if (!this.Exceptions.ContainsKey(key))
			{
				if (this.SanitizedStackTraces == null)
				{
					this.SanitizedStackTraces = new List<List<KeyValuePair<string, object>>>();
				}
				List<KeyValuePair<string, object>> list = new List<KeyValuePair<string, object>>();
				list.Add(new KeyValuePair<string, object>("MGUID", string.Format("{0}", this.MailboxGuid)));
				list.Add(new KeyValuePair<string, object>("FID", string.Format("{0}", this.FolderId)));
				list.Add(new KeyValuePair<string, object>("EX", string.Format("{0}", key)));
				list.Add(new KeyValuePair<string, object>("MSG", string.Format("{0}", SpecialCharacters.SanitizeForLogging(ex.Message))));
				list.Add(new KeyValuePair<string, object>("EXTRACE", string.Format("{0}", SpecialCharacters.SanitizeForLogging(ex.StackTrace))));
				this.SanitizedStackTraces.Add(list);
			}
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x0003B6B4 File Offset: 0x000398B4
		private string BuildFailureString(Dictionary<string, int> failures)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (KeyValuePair<string, int> keyValuePair in failures)
			{
				stringBuilder.Append(string.Format("{0}_{1}|", keyValuePair.Key, keyValuePair.Value));
			}
			return stringBuilder.ToString().TrimEnd(new char[]
			{
				'|'
			});
		}

		// Token: 0x040005F2 RID: 1522
		private const string noInnerExceptionStr = "NoInnerException";
	}
}
