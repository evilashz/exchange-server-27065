using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Filtering;
using Microsoft.Exchange.Net;
using Microsoft.Win32;

namespace Microsoft.Filtering
{
	// Token: 0x02000015 RID: 21
	internal class TextExtractionLog
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002CCC File Offset: 0x00000ECC
		public string LogPath
		{
			get
			{
				if (this.logPath == null)
				{
					this.logPath = string.Empty;
					using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeLabs"))
					{
						if (registryKey != null)
						{
							object value = registryKey.GetValue("TextExtractionLogPath");
							if (value != null)
							{
								this.logPath = value.ToString();
							}
						}
					}
				}
				return this.logPath;
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002D3C File Offset: 0x00000F3C
		public void Start()
		{
			if (string.IsNullOrEmpty(this.LogPath))
			{
				this.enabled = false;
				return;
			}
			this.CreateLog();
			this.ConfigureLog();
			ExTraceGlobals.FilteringServiceApiTracer.TraceDebug((long)this.GetHashCode(), "Text Extraction Information logging has been started.");
			this.enabled = true;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002D7C File Offset: 0x00000F7C
		public void Stop()
		{
			if (this.log != null)
			{
				this.FlushBuffer();
				this.log.Close();
				this.log = null;
			}
			this.enabled = false;
			ExTraceGlobals.FilteringServiceApiTracer.TraceDebug((long)this.GetHashCode(), "Text Extraction Information logging has been stopped.");
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002DBB File Offset: 0x00000FBB
		public void FlushBuffer()
		{
			if (this.log != null)
			{
				this.log.Flush();
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002DD0 File Offset: 0x00000FD0
		public void Trace(string exMessageId, TextExtractionData teData)
		{
			if (!this.enabled || this.log == null)
			{
				return;
			}
			LogRowFormatter logRowFormatter = new LogRowFormatter(this.textExtractionLogSchemaMapping);
			logRowFormatter[1] = exMessageId;
			logRowFormatter[2] = teData.StreamId;
			logRowFormatter[3] = teData.StreamSize;
			logRowFormatter[4] = teData.ParentId;
			logRowFormatter[5] = teData.Types;
			logRowFormatter[6] = teData.ModuleUsed;
			logRowFormatter[7] = teData.TextExtractionResult;
			logRowFormatter[8] = teData.SkippedModules;
			logRowFormatter[9] = teData.FailedModules;
			logRowFormatter[10] = teData.DisabledModules;
			logRowFormatter[11] = teData.AdditionalInformation;
			this.Append(logRowFormatter);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002EAC File Offset: 0x000010AC
		private void Append(LogRowFormatter row)
		{
			try
			{
				if (this.log != null)
				{
					this.log.Append(row, TextExtractionSchema.TimeStampFieldIndex);
				}
			}
			catch (Exception ex)
			{
				if (ex is OutOfMemoryException || ex is ThreadAbortException || ex is AccessViolationException || ex is SEHException)
				{
					throw;
				}
				ExTraceGlobals.FilteringServiceApiTracer.TraceError<string>((long)this.GetHashCode(), "Failed to append a row in the text extraction log. Error: {0}", ex.Message);
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002F28 File Offset: 0x00001128
		private void ConfigureLog()
		{
			long num = 10485760L;
			long maxDirectorySize = 50L * num;
			int bufferSize = 4096;
			TimeSpan maxAge = TimeSpan.FromDays(4.0);
			TimeSpan streamFlushInterval = TimeSpan.FromMinutes(1.0);
			TimeSpan backgroundWriteInterval = TimeSpan.FromSeconds(1.0);
			this.log.Configure(this.LogPath, maxAge, maxDirectorySize, num, bufferSize, streamFlushInterval, backgroundWriteInterval);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002F90 File Offset: 0x00001190
		private void CreateLog()
		{
			this.log = new AsyncLog("TELOG", new LogHeaderFormatter(this.textExtractionLogSchemaMapping), "TextExtractionLog");
		}

		// Token: 0x04000024 RID: 36
		private const string LogComponentName = "TextExtractionLog";

		// Token: 0x04000025 RID: 37
		private const string LogPathRegistryKey = "SOFTWARE\\Microsoft\\ExchangeLabs";

		// Token: 0x04000026 RID: 38
		private const string LogPathRegistryValue = "TextExtractionLogPath";

		// Token: 0x04000027 RID: 39
		private AsyncLog log;

		// Token: 0x04000028 RID: 40
		private bool enabled;

		// Token: 0x04000029 RID: 41
		private string logPath;

		// Token: 0x0400002A RID: 42
		private LogSchema textExtractionLogSchemaMapping = new LogSchema("Microsoft Exchange Server", TextExtractionSchema.DefaultVersion.ToString(), "Text Extraction Log", (from csvfield in TextExtractionSchema.DefaultSchema.Fields
		select csvfield.Name).ToArray<string>());
	}
}
