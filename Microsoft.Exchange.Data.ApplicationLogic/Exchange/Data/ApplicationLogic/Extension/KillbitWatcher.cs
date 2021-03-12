using System;
using System.ComponentModel;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.ApplicationLogic;

namespace Microsoft.Exchange.Data.ApplicationLogic.Extension
{
	// Token: 0x02000107 RID: 263
	public class KillbitWatcher : DisposeTrackableBase
	{
		// Token: 0x06000B36 RID: 2870 RVA: 0x0002D4F0 File Offset: 0x0002B6F0
		private static void OnError(object source, ErrorEventArgs e)
		{
			Exception ex = (e != null) ? e.GetException() : null;
			if (ex != null)
			{
				KillbitWatcher.HandleFileWatcherException(ex);
			}
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x0002D514 File Offset: 0x0002B714
		private static void HandleFileWatcherException(Exception e)
		{
			KillbitWatcher.Tracer.TraceError<string, Exception>(0L, "killbit file system watcher failed because of exception {0}.", KillBitHelper.KillBitDirectory, e);
			ExtensionDiagnostics.Logger.LogEvent(ApplicationLogicEventLogConstants.Tuple_KillbitFileWatcherFailed, null, new object[]
			{
				"ProcessKillBit",
				ExtensionDiagnostics.GetLoggedExceptionString(e)
			});
			Win32Exception ex = e as Win32Exception;
			if (ex != null && ex.NativeErrorCode == 5)
			{
				return;
			}
			ExWatson.HandleException(new UnhandledExceptionEventArgs(e, false), ReportOptions.None);
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x0002D584 File Offset: 0x0002B784
		private static void Watch(KillbitWatcher.ReadKillBitFromFileCallback readKillBitFromFileCallback)
		{
			KillbitWatcher.watcher = new FileSystemWatcher();
			KillbitWatcher.watcher.Error += KillbitWatcher.OnError;
			KillbitWatcher.watcher.Path = KillBitHelper.KillBitDirectory;
			KillbitWatcher.watcher.Filter = "killbit.xml";
			KillbitWatcher.watcher.NotifyFilter = NotifyFilters.LastWrite;
			KillbitWatcher.watcher.Changed += readKillBitFromFileCallback.Invoke;
			KillbitWatcher.watcher.Created += readKillBitFromFileCallback.Invoke;
			KillbitWatcher.watcher.EnableRaisingEvents = true;
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x0002D614 File Offset: 0x0002B814
		public static void TryWatch(KillbitWatcher.ReadKillBitFromFileCallback readKillBitFromFileCallback)
		{
			if (!Directory.Exists(KillBitHelper.KillBitDirectory))
			{
				Exception ex = null;
				try
				{
					KillbitWatcher.Tracer.TraceInformation<string>(0, 0L, "Killbit folder {0} is not there. Creating it.", KillBitHelper.KillBitDirectory);
					ExtensionDiagnostics.Logger.LogEvent(ApplicationLogicEventLogConstants.Tuple_KillbitFolderNotExist, null, new object[]
					{
						"ProcessKillBit"
					});
					Directory.CreateDirectory(KillBitHelper.KillBitDirectory);
				}
				catch (DirectoryNotFoundException ex2)
				{
					ex = ex2;
				}
				catch (IOException ex3)
				{
					ex = ex3;
				}
				catch (UnauthorizedAccessException ex4)
				{
					ex = ex4;
				}
				if (ex != null)
				{
					KillbitWatcher.Tracer.TraceError<string, Exception>(0L, "Cannot created killbit folder {0} due to Exception {1}.", KillBitHelper.KillBitDirectory, ex);
					ExtensionDiagnostics.Logger.LogEvent(ApplicationLogicEventLogConstants.Tuple_CanNotCreateKillbitFolder, null, new object[]
					{
						"ProcessKillBit",
						ExtensionDiagnostics.GetLoggedExceptionString(ex)
					});
					return;
				}
			}
			try
			{
				KillbitWatcher.Watch(readKillBitFromFileCallback);
			}
			catch (Exception e)
			{
				KillbitWatcher.HandleFileWatcherException(e);
			}
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x0002D71C File Offset: 0x0002B91C
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<KillbitWatcher>(this);
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x0002D724 File Offset: 0x0002B924
		protected override void InternalDispose(bool disposing)
		{
			if (!disposing)
			{
				return;
			}
			if (KillbitWatcher.watcher == null)
			{
				return;
			}
			KillbitWatcher.watcher.Dispose();
		}

		// Token: 0x040005A8 RID: 1448
		private static readonly Trace Tracer = ExTraceGlobals.ExtensionTracer;

		// Token: 0x040005A9 RID: 1449
		private static FileSystemWatcher watcher;

		// Token: 0x02000108 RID: 264
		// (Invoke) Token: 0x06000B3F RID: 2879
		public delegate void ReadKillBitFromFileCallback(object source, FileSystemEventArgs e);
	}
}
