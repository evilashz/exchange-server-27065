using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Xml;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.CalendarRepair;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Infoworker.MeetingValidator;
using Microsoft.Win32;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.CalendarRepair
{
	// Token: 0x02000151 RID: 337
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class CalendarRepairLogger
	{
		// Token: 0x06000DCB RID: 3531 RVA: 0x00053660 File Offset: 0x00051860
		private CalendarRepairLogger()
		{
			this.settings = new XmlWriterSettings();
			this.settings.Indent = true;
		}

		// Token: 0x06000DCC RID: 3532 RVA: 0x000536B4 File Offset: 0x000518B4
		internal void UpdateConfigurationFromADSetting(Server adServer)
		{
			if (adServer == null)
			{
				return;
			}
			if (adServer.CalendarRepairLogPath == null)
			{
				this.logPath = this.GetDefaultLogPath();
				ExTraceGlobals.CalendarRepairAssistantTracer.Information<string>((long)this.GetHashCode(), "The log path is not set on the server. The default log path ({0}) is used.", this.logPath);
			}
			else
			{
				this.logPath = adServer.CalendarRepairLogPath.ToString();
			}
			this.logDirectorySizeLimit = (long)(adServer.CalendarRepairLogDirectorySizeLimit.IsUnlimited ? 0UL : adServer.CalendarRepairLogDirectorySizeLimit.Value.ToBytes());
			TimeSpan timeSpan = adServer.CalendarRepairLogFileAgeLimit;
			this.logFileAgeLimit = ((timeSpan == TimeSpan.Zero) ? TimeSpan.MaxValue : timeSpan);
			this.logSubject = adServer.CalendarRepairLogSubjectLoggingEnabled;
			this.logEnabled = adServer.CalendarRepairLogEnabled;
			ExTraceGlobals.CalendarRepairAssistantTracer.Information((long)this.GetHashCode(), "Configure logger from AD server setting. LogPath:{0}, DirectorySizeLimit:{1}, FileAgeLimit:{2}, SubjectLoggingEnabled:{3}, LogEnabled:{4}.", new object[]
			{
				this.logPath,
				this.logDirectorySizeLimit,
				this.logFileAgeLimit,
				this.logSubject,
				this.logEnabled
			});
		}

		// Token: 0x06000DCD RID: 3533 RVA: 0x000537E4 File Offset: 0x000519E4
		internal void Log(List<MeetingValidationResult> validationResults, IExchangePrincipal mailboxOwner, ExDateTime rangeStart, ExDateTime rangeEnd)
		{
			if (!this.logEnabled)
			{
				return;
			}
			LogDirectory.BufferedStream bufferedStream = null;
			try
			{
				lock (this.threadSafetyLock)
				{
					if (this.lastCRALogCleanupRun.AddDays(1.0) < ExDateTime.Now)
					{
						this.DeleteOldCRALogFiles();
						this.lastCRALogCleanupRun = ExDateTime.Now;
					}
				}
				CalendarRepairLogRootEntry calendarRepairLogRootEntry = CalendarRepairLogRootEntry.CreateInstance(mailboxOwner, rangeStart, rangeEnd, this.logSubject);
				validationResults.ForEach(new Action<MeetingValidationResult>(calendarRepairLogRootEntry.AddValidationResult));
				if (calendarRepairLogRootEntry.TotalInconsistentMeetings != 0)
				{
					if (!Directory.Exists(this.logPath))
					{
						Microsoft.Exchange.Diagnostics.Log.CreateLogDirectory(this.logPath);
					}
					string arg = LocalLongFullPath.ConvertInvalidCharactersInFileName(mailboxOwner.Alias);
					string fileName = string.Format("{0}-{1:MM-dd-yyyy}-{2}.log", arg, ExDateTime.UtcNow, Guid.NewGuid().ToString().Substring(0, 6));
					bufferedStream = new LogDirectory.BufferedStream(Path.Combine(this.logPath, LocalLongFullPath.ConvertInvalidCharactersInPathName(fileName)), 4096);
					try
					{
						using (XmlWriter xmlWriter = XmlWriter.Create(bufferedStream, this.settings))
						{
							calendarRepairLogRootEntry.WriteXml(xmlWriter);
						}
					}
					catch (ArgumentException arg2)
					{
						CalendarRepairLogger.Tracer.TraceDebug<ArgumentException>((long)this.GetHashCode(), "CRA log creation failure:{0}", arg2);
					}
				}
			}
			catch (IOException arg3)
			{
				CalendarRepairLogger.Tracer.TraceDebug<IOException>((long)this.GetHashCode(), "IOException:{0}", arg3);
			}
			finally
			{
				if (bufferedStream != null)
				{
					bufferedStream.Close();
					bufferedStream = null;
				}
			}
		}

		// Token: 0x06000DCE RID: 3534 RVA: 0x000539DC File Offset: 0x00051BDC
		private string GetDefaultLogPath()
		{
			string text = null;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Setup"))
				{
					text = (registryKey.GetValue("MsiInstallPath") as string);
				}
			}
			catch (SecurityException ex)
			{
				ExTraceGlobals.CalendarRepairAssistantTracer.TraceError((long)this.GetHashCode(), ex.Message);
			}
			catch (UnauthorizedAccessException ex2)
			{
				ExTraceGlobals.CalendarRepairAssistantTracer.TraceError((long)this.GetHashCode(), ex2.Message);
			}
			if (text == null)
			{
				text = "C:\\MSExchange\\";
			}
			return Path.Combine(text, "Logging\\Calendar Repair Assistant\\");
		}

		// Token: 0x06000DCF RID: 3535 RVA: 0x00053A8C File Offset: 0x00051C8C
		private void DeleteOldCRALogFiles()
		{
			if (TimeSpan.MaxValue == this.logFileAgeLimit || !Directory.Exists(this.logPath))
			{
				return;
			}
			try
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(this.logPath);
				foreach (FileInfo fileInfo in directoryInfo.GetFiles())
				{
					if (this.logFileAgeLimit < (DateTime)ExDateTime.Now - fileInfo.LastWriteTime)
					{
						fileInfo.Delete();
					}
				}
			}
			catch (IOException)
			{
			}
		}

		// Token: 0x040008A3 RID: 2211
		private const string CalendarRepairComponentName = "Calendar Repair Assistant";

		// Token: 0x040008A4 RID: 2212
		private const string InstallationPathRegistryKey = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Setup";

		// Token: 0x040008A5 RID: 2213
		private const string InstallationPathRegistryValueName = "MsiInstallPath";

		// Token: 0x040008A6 RID: 2214
		private const string LogFailoverPath = "C:\\MSExchange\\";

		// Token: 0x040008A7 RID: 2215
		private const string LogRelativePath = "Logging\\Calendar Repair Assistant\\";

		// Token: 0x040008A8 RID: 2216
		private const int bufferLength = 4096;

		// Token: 0x040008A9 RID: 2217
		internal static readonly CalendarRepairLogger Instance = new CalendarRepairLogger();

		// Token: 0x040008AA RID: 2218
		private readonly object threadSafetyLock = new object();

		// Token: 0x040008AB RID: 2219
		private XmlWriterSettings settings;

		// Token: 0x040008AC RID: 2220
		private string logPath = "C:\\MSExchange\\Logging\\Calendar Repair Assistant\\";

		// Token: 0x040008AD RID: 2221
		private long logDirectorySizeLimit;

		// Token: 0x040008AE RID: 2222
		private TimeSpan logFileAgeLimit = TimeSpan.MaxValue;

		// Token: 0x040008AF RID: 2223
		private bool logSubject;

		// Token: 0x040008B0 RID: 2224
		private bool logEnabled = true;

		// Token: 0x040008B1 RID: 2225
		private ExDateTime lastCRALogCleanupRun;

		// Token: 0x040008B2 RID: 2226
		private static readonly Trace Tracer = ExTraceGlobals.CalendarRepairAssistantTracer;
	}
}
