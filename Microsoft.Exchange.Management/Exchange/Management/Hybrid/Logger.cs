using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Hybrid
{
	// Token: 0x020008F0 RID: 2288
	internal class Logger : ILogger, IDisposable
	{
		// Token: 0x06005119 RID: 20761 RVA: 0x00151D64 File Offset: 0x0014FF64
		private Logger(string prefix, string directory, Action<LocalizedString> writeVerbose)
		{
			this.writeVerbose = writeVerbose;
			if (Configuration.EnableLogging)
			{
				DateTime utcNow = DateTime.UtcNow;
				this.logFile = string.Format("{0}\\{1}_{2}_{3}_{4}_{5}_{6}_{7}_{8}.log", new object[]
				{
					directory,
					prefix,
					utcNow.Month,
					utcNow.Day,
					utcNow.Year,
					utcNow.Hour,
					utcNow.Minute,
					utcNow.Second,
					utcNow.Ticks
				});
				this.logFileForData = this.logFile.Replace(".log", ".extra.log");
			}
		}

		// Token: 0x0600511A RID: 20762 RVA: 0x00151E34 File Offset: 0x00150034
		public static ILogger Create(Action<LocalizedString> writeVerbose)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(Path.Combine(ConfigurationContext.Setup.LoggingPath, "Update-HybridConfiguration"));
			if (Configuration.EnableLogging && !directoryInfo.Exists)
			{
				directoryInfo.Create();
			}
			return new Logger("HybridConfiguration", directoryInfo.FullName, writeVerbose);
		}

		// Token: 0x0600511B RID: 20763 RVA: 0x00151E7C File Offset: 0x0015007C
		public void Log(LocalizedString text)
		{
			this.LogInformation(text.ToString());
		}

		// Token: 0x0600511C RID: 20764 RVA: 0x00151E91 File Offset: 0x00150091
		public void Log(Exception e)
		{
			this.LogError(e.Message);
		}

		// Token: 0x0600511D RID: 20765 RVA: 0x00151E9F File Offset: 0x0015009F
		public void LogError(string information)
		{
			this.LogMessage("  ERROR", information, null);
		}

		// Token: 0x0600511E RID: 20766 RVA: 0x00151EAE File Offset: 0x001500AE
		public void LogWarning(string information)
		{
			this.LogMessage("WARNING", information, null);
		}

		// Token: 0x0600511F RID: 20767 RVA: 0x00151EBD File Offset: 0x001500BD
		public void LogInformation(string information)
		{
			this.LogMessage("   INFO", information, null);
		}

		// Token: 0x06005120 RID: 20768 RVA: 0x00151ECC File Offset: 0x001500CC
		public void LogInformation(string information, string data)
		{
			this.LogMessage("   INFO", information, data);
		}

		// Token: 0x06005121 RID: 20769 RVA: 0x00151EDB File Offset: 0x001500DB
		public void Dispose()
		{
		}

		// Token: 0x06005122 RID: 20770 RVA: 0x00151EDD File Offset: 0x001500DD
		public override string ToString()
		{
			return this.logFile;
		}

		// Token: 0x06005123 RID: 20771 RVA: 0x00151EE8 File Offset: 0x001500E8
		private void LogMessage(string prefix, string information, string data = null)
		{
			if (this.writeVerbose != null)
			{
				this.writeVerbose(new LocalizedString(string.Format("{0}:{1}", prefix, information)));
			}
			if (Configuration.EnableLogging)
			{
				DateTime utcNow = DateTime.UtcNow;
				string lineHeader = string.Format("[{0:00}/{1:00}/{2} {3:00}:{4:00}:{5:00}] {6} : ", new object[]
				{
					utcNow.Month,
					utcNow.Day,
					utcNow.Year,
					utcNow.Hour,
					utcNow.Minute,
					utcNow.Second,
					prefix
				});
				string[] lines = information.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
				Logger.Write(this.logFile, lineHeader, lines, null);
				if (!string.IsNullOrEmpty(data))
				{
					Logger.Write(this.logFileForData, lineHeader, lines, data);
				}
			}
		}

		// Token: 0x06005124 RID: 20772 RVA: 0x00151FD0 File Offset: 0x001501D0
		private static void Write(string logFile, string lineHeader, string[] lines, string data)
		{
			using (StreamWriter streamWriter = new StreamWriter(logFile, true, Encoding.UTF8))
			{
				if (lines.Length == 0)
				{
					streamWriter.WriteLine(lineHeader);
				}
				else
				{
					streamWriter.WriteLine("{0}{1}", lineHeader, lines[0]);
				}
				if (lines.Length > 1)
				{
					string arg = new string(' ', lineHeader.Length);
					for (int i = 1; i < lines.Length; i++)
					{
						streamWriter.WriteLine("{0}{1}", arg, lines[i]);
					}
				}
				if (!string.IsNullOrEmpty(data))
				{
					streamWriter.WriteLine(data);
				}
			}
		}

		// Token: 0x04002F9B RID: 12187
		private const string LogFilePrefix = "HybridConfiguration";

		// Token: 0x04002F9C RID: 12188
		private const string LoggingSubDir = "Update-HybridConfiguration";

		// Token: 0x04002F9D RID: 12189
		private const string ErrorPrefix = "  ERROR";

		// Token: 0x04002F9E RID: 12190
		private const string WarningPrefix = "WARNING";

		// Token: 0x04002F9F RID: 12191
		private const string InformationPrefix = "   INFO";

		// Token: 0x04002FA0 RID: 12192
		private readonly string logFile;

		// Token: 0x04002FA1 RID: 12193
		private readonly string logFileForData;

		// Token: 0x04002FA2 RID: 12194
		private Action<LocalizedString> writeVerbose;
	}
}
