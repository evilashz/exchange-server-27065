using System;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Win32;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000370 RID: 880
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class QuickLog
	{
		// Token: 0x17000CEC RID: 3308
		// (get) Token: 0x060026D8 RID: 9944 RVA: 0x0009BAB6 File Offset: 0x00099CB6
		// (set) Token: 0x060026D9 RID: 9945 RVA: 0x0009BABD File Offset: 0x00099CBD
		private static bool IsDisabled { get; set; }

		// Token: 0x17000CED RID: 3309
		// (get) Token: 0x060026DA RID: 9946 RVA: 0x0009BAC5 File Offset: 0x00099CC5
		// (set) Token: 0x060026DB RID: 9947 RVA: 0x0009BACD File Offset: 0x00099CCD
		private int MaxLogEntries { get; set; }

		// Token: 0x060026DC RID: 9948 RVA: 0x0009BAD6 File Offset: 0x00099CD6
		public QuickLog() : this(40)
		{
		}

		// Token: 0x060026DD RID: 9949 RVA: 0x0009BAE0 File Offset: 0x00099CE0
		public QuickLog(int maxLogEntries)
		{
			this.LogStore = new SingleInstanceItemHandler(this.LogMessageClass, DefaultFolderType.Configuration);
			this.MaxLogEntries = maxLogEntries;
		}

		// Token: 0x060026DE RID: 9950 RVA: 0x0009BB04 File Offset: 0x00099D04
		private static void CheckLoggingDisabled(object ignored)
		{
			int num = 0;
			try
			{
				num = (int)Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeServer\\v15", "DisableQuickLog", 0);
			}
			catch
			{
			}
			QuickLog.IsDisabled = (num == 1);
		}

		// Token: 0x060026DF RID: 9951 RVA: 0x0009BB4C File Offset: 0x00099D4C
		protected void AppendFormatLogEntry(MailboxSession session, string entry, params object[] args)
		{
			this.AppendFormatLogEntry(session, null, false, entry, args);
		}

		// Token: 0x060026E0 RID: 9952 RVA: 0x0009BB5C File Offset: 0x00099D5C
		protected void AppendFormatLogEntry(MailboxSession session, Exception e, bool logWatsonReport, string entry, params object[] args)
		{
			if (QuickLog.IsDisabled)
			{
				return;
			}
			StringBuilder stringBuilder = new StringBuilder(1024);
			try
			{
				stringBuilder.AppendFormat("{0}, Mailbox: '{1}', Entry ", DateTime.UtcNow.ToString(), session.DisplayName);
				stringBuilder.AppendFormat(entry, args);
				stringBuilder.AppendLine();
				if (e != null)
				{
					stringBuilder.Append(e);
				}
				else
				{
					logWatsonReport = false;
				}
			}
			catch
			{
				stringBuilder.AppendFormat("Failed to format string: {0}", entry);
				logWatsonReport = false;
			}
			try
			{
				this.WriteLogEntry(session, stringBuilder.ToString());
			}
			catch (LocalizedException ex)
			{
				if (ex.InnerException == null || (!(ex.InnerException is QuotaExceededException) && !(ex.InnerException is StorageTransientException)))
				{
					throw;
				}
				logWatsonReport = false;
			}
			if (logWatsonReport)
			{
				ExWatson.SendReport(e, ReportOptions.DoNotCollectDumps | ReportOptions.DoNotLogProcessAndThreadIds | ReportOptions.DoNotFreezeThreads, stringBuilder.ToString());
			}
		}

		// Token: 0x17000CEE RID: 3310
		// (get) Token: 0x060026E1 RID: 9953
		protected abstract string LogMessageClass { get; }

		// Token: 0x060026E2 RID: 9954 RVA: 0x0009BC44 File Offset: 0x00099E44
		protected void WriteUniqueLogEntry(MailboxSession mailboxSession, string entry, string uniqueKey)
		{
			this.WriteLogEntry(mailboxSession, entry, uniqueKey);
		}

		// Token: 0x060026E3 RID: 9955 RVA: 0x0009BC4F File Offset: 0x00099E4F
		protected void WriteLogEntry(MailboxSession session, string entry)
		{
			this.WriteLogEntry(session, entry, null);
		}

		// Token: 0x060026E4 RID: 9956 RVA: 0x0009BC5C File Offset: 0x00099E5C
		private void WriteLogEntry(MailboxSession session, string entry, string uniqueKey)
		{
			if (QuickLog.IsDisabled)
			{
				return;
			}
			int i = 0;
			while (i < 2)
			{
				i++;
				try
				{
					string itemContent = this.LogStore.GetItemContent(session);
					if (uniqueKey != null && !string.IsNullOrEmpty(itemContent))
					{
						int num = itemContent.IndexOf('\n');
						if (num == -1)
						{
							num = itemContent.Length - 1;
						}
						if (itemContent.IndexOf(uniqueKey, 0, num) != -1)
						{
							break;
						}
					}
					StringBuilder stringBuilder = new StringBuilder(6000);
					stringBuilder.AppendLine(entry);
					if (!string.IsNullOrEmpty(itemContent))
					{
						int num2 = 1;
						int j;
						for (j = 0; j < entry.Length; j++)
						{
							if (entry[j] == '\n')
							{
								num2++;
							}
						}
						for (j = 0; j < itemContent.Length; j++)
						{
							if (itemContent[j] == '\n')
							{
								num2++;
								if (num2 >= this.MaxLogEntries)
								{
									break;
								}
							}
						}
						j++;
						if (j >= itemContent.Length)
						{
							j = itemContent.Length;
						}
						stringBuilder.Append(itemContent, 0, j);
					}
					this.LogStore.SetItemContent(session, stringBuilder.ToString());
					break;
				}
				catch (ObjectNotFoundException)
				{
					if (i == 2)
					{
						break;
					}
				}
				catch (VirusException)
				{
					this.LogStore.Delete(session);
					if (i == 2)
					{
						break;
					}
				}
			}
		}

		// Token: 0x060026E5 RID: 9957 RVA: 0x0009BDB0 File Offset: 0x00099FB0
		private string[] GetContent(MailboxSession session)
		{
			string itemContent = this.LogStore.GetItemContent(session);
			string[] result = null;
			if (!string.IsNullOrEmpty(itemContent))
			{
				char[] separator = new char[]
				{
					'\n'
				};
				result = itemContent.Split(separator, this.MaxLogEntries, StringSplitOptions.RemoveEmptyEntries);
			}
			return result;
		}

		// Token: 0x04001714 RID: 5908
		private const int DefaultMaxLogEntries = 40;

		// Token: 0x04001715 RID: 5909
		private const int MaxRetry = 2;

		// Token: 0x04001716 RID: 5910
		private const ReportOptions WatsonReportOptions = ReportOptions.DoNotCollectDumps | ReportOptions.DoNotLogProcessAndThreadIds | ReportOptions.DoNotFreezeThreads;

		// Token: 0x04001717 RID: 5911
		private const string DisableLoggingKey = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeServer\\v15";

		// Token: 0x04001718 RID: 5912
		private const string DisableQuickLogValue = "DisableQuickLog";

		// Token: 0x04001719 RID: 5913
		private static readonly TimeSpan RegistryTimerFrequency = TimeSpan.FromMinutes(5.0);

		// Token: 0x0400171A RID: 5914
		private static Timer registryTimer = new Timer(new TimerCallback(QuickLog.CheckLoggingDisabled), null, TimeSpan.Zero, QuickLog.RegistryTimerFrequency);

		// Token: 0x0400171B RID: 5915
		private readonly SingleInstanceItemHandler LogStore;
	}
}
