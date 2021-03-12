using System;
using System.Globalization;
using System.IO;
using System.Threading;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Extensibility.Internal;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x0200025F RID: 607
	internal static class RoutingTableLogFileManager
	{
		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x06001A32 RID: 6706 RVA: 0x0006B0A5 File Offset: 0x000692A5
		internal static string LogFilePath
		{
			get
			{
				if (RoutingTableLogFileManager.directory == null)
				{
					return RoutingTableLogFileManager.directoryPath;
				}
				return RoutingTableLogFileManager.directory.FullName;
			}
		}

		// Token: 0x06001A33 RID: 6707 RVA: 0x0006B0C0 File Offset: 0x000692C0
		public static string CleanupLogsAndGetLogFileName(DateTime time, RoutingContextCore context)
		{
			if (!context.Dependencies.IsProcessShuttingDown)
			{
				RoutingTableLogFileManager.CleanupLogFiles(context);
			}
			int num = Interlocked.Increment(ref RoutingTableLogFileManager.sequenceNumber) % 256;
			string text = time.ToString(CultureInfo.InvariantCulture).Replace(':', '_');
			text = text.Replace('/', '_');
			return string.Concat(new string[]
			{
				RoutingTableLogFileManager.LogFilePath,
				"\\",
				RoutingTableLogFileManager.GetProcessRolePrefix(context.GetProcessRoleForDiagnostics()),
				"RoutingConfig#",
				num.ToString(),
				"@",
				text,
				".xml"
			});
		}

		// Token: 0x06001A34 RID: 6708 RVA: 0x0006B163 File Offset: 0x00069363
		public static void HandleTransportServerConfigChange(TransportServerConfiguration transportServerConfiguration)
		{
			RoutingDiag.Tracer.TraceDebug(0L, "Transport server config change detected");
			RoutingTableLogFileManager.CreateLogDirectory(transportServerConfiguration.TransportServer.RoutingTableLogPath);
		}

		// Token: 0x06001A35 RID: 6709 RVA: 0x0006B188 File Offset: 0x00069388
		private static void CleanupLogFiles(RoutingContextCore context)
		{
			FileInfo[] array = null;
			bool flag = false;
			if (RoutingTableLogFileManager.directory != null)
			{
				try
				{
					array = RoutingTableLogFileManager.directory.GetFiles(RoutingTableLogFileManager.GetProcessRolePrefix(context.GetProcessRoleForDiagnostics()) + "RoutingConfig#*.xml");
					goto IL_45;
				}
				catch (DirectoryNotFoundException)
				{
					RoutingDiag.Tracer.TraceError(0L, "Directory does not exist; attempting to create it");
					flag = true;
					goto IL_45;
				}
			}
			flag = true;
			IL_45:
			if (flag)
			{
				RoutingTableLogFileManager.CreateLogDirectory(context.Dependencies.LogPath);
				return;
			}
			Array.Sort<FileInfo>(array, RoutingTableLogFileManager.compareFileCreationTimeDelegate);
			int num = 0;
			while (num < array.Length && RoutingTableLogFileManager.IsFileTooOld(array[num], context.Dependencies.MaxLogFileAge))
			{
				RoutingTableLogFileManager.DeleteFile(array[num]);
				num++;
			}
			if (context.Dependencies.MaxLogDirectorySize.IsUnlimited)
			{
				return;
			}
			ulong num2 = 0UL;
			int i;
			for (i = array.Length - 1; i >= num; i--)
			{
				num2 += (ulong)array[i].Length;
				if (num2 >= context.Dependencies.MaxLogDirectorySize.Value.ToBytes())
				{
					break;
				}
			}
			for (int j = num; j <= i; j++)
			{
				RoutingTableLogFileManager.DeleteFile(array[j]);
			}
		}

		// Token: 0x06001A36 RID: 6710 RVA: 0x0006B2A8 File Offset: 0x000694A8
		private static bool IsFileTooOld(FileInfo file, EnhancedTimeSpan maxLogFileAge)
		{
			TimeSpan t = DateTime.UtcNow.Subtract(file.CreationTimeUtc);
			return t > maxLogFileAge;
		}

		// Token: 0x06001A37 RID: 6711 RVA: 0x0006B2D0 File Offset: 0x000694D0
		private static void DeleteFile(FileInfo file)
		{
			try
			{
				file.Delete();
			}
			catch (UnauthorizedAccessException ex)
			{
				RoutingDiag.Tracer.TraceError<string, UnauthorizedAccessException>(0L, "Can't delete routing config file {0}, Exception: {1}", file.FullName, ex);
				RoutingDiag.EventLogger.LogEvent(TransportEventLogConstants.Tuple_RoutingTableLogDeletionFailure, null, new object[]
				{
					file.FullName,
					ex.ToString(),
					ex
				});
			}
			catch (IOException ex2)
			{
				RoutingDiag.Tracer.TraceError<string, IOException>(0L, "Can't delete routing config file {0}, Exception: {1}", file.FullName, ex2);
				RoutingDiag.EventLogger.LogEvent(TransportEventLogConstants.Tuple_RoutingTableLogDeletionFailure, null, new object[]
				{
					file.FullName,
					ex2.ToString(),
					ex2
				});
			}
		}

		// Token: 0x06001A38 RID: 6712 RVA: 0x0006B398 File Offset: 0x00069598
		private static void CreateLogDirectory(LocalLongFullPath logPath)
		{
			string text;
			if (logPath == null || string.IsNullOrEmpty(logPath.PathName))
			{
				text = ConfigurationContext.Setup.InstallPath + "\\TransportRoles\\Logs\\Routing";
			}
			else
			{
				text = logPath.PathName;
			}
			RoutingTableLogFileManager.directoryPath = text;
			RoutingDiag.Tracer.TraceDebug<string>(0L, "Creating log file directory {0}", text);
			try
			{
				RoutingTableLogFileManager.directory = Log.CreateLogDirectory(text);
			}
			catch (UnauthorizedAccessException ex)
			{
				RoutingDiag.Tracer.TraceError<string, UnauthorizedAccessException>(0L, "Can't create routing log dir {0}, Exception: {1}", text, ex);
				RoutingDiag.EventLogger.LogEvent(TransportEventLogConstants.Tuple_RoutingTableLogCreationFailure, null, new object[]
				{
					text,
					ex.ToString(),
					ex
				});
			}
			catch (IOException ex2)
			{
				RoutingDiag.Tracer.TraceError<string, IOException>(0L, "Can't create routing log dir {0}, Exception: {1}", text, ex2);
				RoutingDiag.EventLogger.LogEvent(TransportEventLogConstants.Tuple_RoutingTableLogCreationFailure, null, new object[]
				{
					text,
					ex2.ToString(),
					ex2
				});
			}
		}

		// Token: 0x06001A39 RID: 6713 RVA: 0x0006B4A0 File Offset: 0x000696A0
		private static int CompareFileCreationTime(FileInfo x, FileInfo y)
		{
			return DateTime.Compare(x.CreationTimeUtc, y.CreationTimeUtc);
		}

		// Token: 0x06001A3A RID: 6714 RVA: 0x0006B4B4 File Offset: 0x000696B4
		private static string GetProcessRolePrefix(ProcessTransportRole role)
		{
			switch (role)
			{
			case ProcessTransportRole.Hub:
				return "H";
			case ProcessTransportRole.Edge:
				return string.Empty;
			case ProcessTransportRole.FrontEnd:
				return "F";
			case ProcessTransportRole.MailboxSubmission:
				return "MS";
			case ProcessTransportRole.MailboxDelivery:
				return "MD";
			default:
				throw new ArgumentOutOfRangeException("role", role, "Unexpected role value: " + role);
			}
		}

		// Token: 0x04000C7D RID: 3197
		private const string LogFilePrefix = "RoutingConfig#";

		// Token: 0x04000C7E RID: 3198
		private const string LogFilePattern = "RoutingConfig#*.xml";

		// Token: 0x04000C7F RID: 3199
		private const int MaxLogFileNumber = 256;

		// Token: 0x04000C80 RID: 3200
		private static int sequenceNumber;

		// Token: 0x04000C81 RID: 3201
		private static DirectoryInfo directory;

		// Token: 0x04000C82 RID: 3202
		private static string directoryPath = string.Empty;

		// Token: 0x04000C83 RID: 3203
		private static Comparison<FileInfo> compareFileCreationTimeDelegate = new Comparison<FileInfo>(RoutingTableLogFileManager.CompareFileCreationTime);
	}
}
