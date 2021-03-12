using System;
using System.Globalization;
using System.IO;
using System.Threading;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics.Components.MessagingPolicies;

namespace Microsoft.Exchange.Transport.Agent.InterceptorAgent
{
	// Token: 0x02000002 RID: 2
	internal class Archiver : IDisposable
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		private Archiver(string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				throw new ArgumentNullException("path");
			}
			if (!Path.IsPathRooted(path))
			{
				throw new ArgumentException("Use absolute path");
			}
			this.archivePath = path;
			this.archiveCleaner = new ArchiveCleaner(this.archivePath, Archiver.MaximumArchivedItemAge, 0, 0L);
			this.archiveCleaner.StartMonitoring(Archiver.ArchiveCleanupInterval);
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002139 File Offset: 0x00000339
		public static Archiver Instance
		{
			get
			{
				if (Archiver.instance == null)
				{
					throw new InvalidOperationException();
				}
				return Archiver.instance;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002150 File Offset: 0x00000350
		public string ArchivedMessagesPath
		{
			get
			{
				string result;
				if ((result = this.archivedMessagesPath) == null)
				{
					result = (this.archivedMessagesPath = Path.Combine(this.archivePath, InterceptorAgentSettings.ArchivedMessagesDirectory));
				}
				return result;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002180 File Offset: 0x00000380
		public string ArchivedMessageHeadersPath
		{
			get
			{
				string result;
				if ((result = this.archivedMessageHeadersPath) == null)
				{
					result = (this.archivedMessageHeadersPath = Path.Combine(this.archivePath, InterceptorAgentSettings.ArchivedMessageHeadersDirectory));
				}
				return result;
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000021B0 File Offset: 0x000003B0
		public static void CreateArchiver(string directory)
		{
			if (Archiver.instance == null)
			{
				lock (Archiver.syncRoot)
				{
					if (Archiver.instance == null)
					{
						Archiver archiver = new Archiver(directory);
						Thread.MemoryBarrier();
						Archiver.instance = archiver;
					}
				}
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000220C File Offset: 0x0000040C
		public void Dispose()
		{
			this.archiveCleaner.Dispose();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002219 File Offset: 0x00000419
		public bool TryArchiveHeaders(MailItem mail, string relativePath)
		{
			return this.TryArchive(mail, relativePath, true);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002224 File Offset: 0x00000424
		public bool TryArchiveMessage(MailItem mail, string relativePath)
		{
			return this.TryArchive(mail, relativePath, false);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002230 File Offset: 0x00000430
		private static bool WriteMessage(FileStream stream, TransportMailItem tmi)
		{
			byte[] buffer = new byte[65536];
			Stream stream2;
			if (!ExportStream.TryCreate(tmi, tmi.Recipients, true, out stream2) || stream2 == null)
			{
				throw new InvalidOperationException("Failed to create an export stream because there were no ready recipients");
			}
			using (stream2)
			{
				stream2.Position = 0L;
				for (;;)
				{
					int num = stream2.Read(buffer, 0, 65536);
					if (num == 0)
					{
						break;
					}
					stream.Write(buffer, 0, num);
				}
			}
			return true;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000022AC File Offset: 0x000004AC
		private static bool WriteMessageHeaders(FileStream stream, TransportMailItem tmi)
		{
			if (!ExportStream.TryWriteReplayXHeaders(stream, tmi, tmi.Recipients, false))
			{
				throw new InvalidOperationException("Recipient cannot be written");
			}
			return true;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000022CC File Offset: 0x000004CC
		private static string CreateFolder(string root, string relativePath)
		{
			string text = Path.Combine(root, relativePath);
			try
			{
				if (!Directory.Exists(text))
				{
					Directory.CreateDirectory(text);
				}
				return text;
			}
			catch (UnauthorizedAccessException arg)
			{
				ExTraceGlobals.InterceptorAgentTracer.TraceError<string, UnauthorizedAccessException>((long)typeof(Archiver).GetHashCode(), "Interceptor agent does not have permissions to write to this directory '{0}': {1}", text, arg);
				Util.EventLog.LogEvent(TransportEventLogConstants.Tuple_InterceptorAgentAccessDenied, null, new object[]
				{
					text
				});
			}
			catch (Exception arg2)
			{
				ExTraceGlobals.InterceptorAgentTracer.TraceInformation<string, Exception>(0, (long)typeof(Archiver).GetHashCode(), "Exception when trying to create directory '{0}': {1}", text, arg2);
			}
			return null;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000237C File Offset: 0x0000057C
		private static TransportMailItem GetTransportMailItem(MailItem mailItem)
		{
			TransportMailItemWrapper transportMailItemWrapper = mailItem as TransportMailItemWrapper;
			if (transportMailItemWrapper == null)
			{
				return null;
			}
			return transportMailItemWrapper.TransportMailItem;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000239C File Offset: 0x0000059C
		private bool TryArchive(MailItem mail, string relativePath, bool headersOnly)
		{
			TransportMailItem transportMailItem = Archiver.GetTransportMailItem(mail);
			if (transportMailItem == null)
			{
				return false;
			}
			bool result = false;
			Exception ex = null;
			string text = Archiver.CreateFolder(headersOnly ? this.ArchivedMessageHeadersPath : this.ArchivedMessagesPath, relativePath);
			if (text == null)
			{
				return false;
			}
			string path = string.Format("{0}-{1}-{2}.{3}.eml", new object[]
			{
				Environment.MachineName,
				transportMailItem.RecordId,
				DateTime.UtcNow.ToString("yyyyMMddHHmmssZ", DateTimeFormatInfo.InvariantInfo),
				((IQueueItem)transportMailItem).Priority
			});
			string path2 = Path.Combine(text, path);
			try
			{
				using (FileStream fileStream = new FileStream(path2, FileMode.CreateNew, FileAccess.Write, FileShare.None))
				{
					result = (headersOnly ? Archiver.WriteMessageHeaders(fileStream, transportMailItem) : Archiver.WriteMessage(fileStream, transportMailItem));
				}
			}
			catch (UnauthorizedAccessException ex2)
			{
				Util.EventLog.LogEvent(TransportEventLogConstants.Tuple_InterceptorAgentAccessDenied, null, new object[]
				{
					Path.GetDirectoryName(path2)
				});
				ex = ex2;
			}
			catch (Exception ex3)
			{
				ex = ex3;
			}
			if (ex != null)
			{
				ExTraceGlobals.InterceptorAgentTracer.TraceError<string>((long)this.GetHashCode(), "Interceptor agent encountered an error: {0}", ex.Message);
			}
			return result;
		}

		// Token: 0x04000001 RID: 1
		private const int BlockSize = 65536;

		// Token: 0x04000002 RID: 2
		private static readonly TimeSpan MaximumArchivedItemAge = TimeSpan.FromDays(14.0);

		// Token: 0x04000003 RID: 3
		private static readonly TimeSpan ArchiveCleanupInterval = TimeSpan.FromHours(12.0);

		// Token: 0x04000004 RID: 4
		private static readonly object syncRoot = new object();

		// Token: 0x04000005 RID: 5
		private static Archiver instance;

		// Token: 0x04000006 RID: 6
		private readonly string archivePath;

		// Token: 0x04000007 RID: 7
		private readonly ArchiveCleaner archiveCleaner;

		// Token: 0x04000008 RID: 8
		private string archivedMessagesPath;

		// Token: 0x04000009 RID: 9
		private string archivedMessageHeadersPath;
	}
}
