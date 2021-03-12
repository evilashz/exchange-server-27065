using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;

namespace Microsoft.Exchange.EDiscovery.MailboxSearch
{
	// Token: 0x02000002 RID: 2
	internal class AttachmentLog : IDisposable
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public AttachmentLog(string logFileName, string logHeader)
		{
			this.logFileName = logFileName;
			this.logFullPathFileName = Path.Combine(Path.GetTempPath(), logFileName);
			this.logWriter = new StreamWriter(this.logFullPathFileName);
			this.logWriter.AutoFlush = true;
			if (!string.IsNullOrEmpty(logHeader))
			{
				this.logWriter.WriteLine(logHeader);
			}
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000212C File Offset: 0x0000032C
		public void WriteLogs(IEnumerable<string> logEntries)
		{
			foreach (string value in logEntries)
			{
				this.logWriter.WriteLine(value);
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x0000217C File Offset: 0x0000037C
		public byte[] GetCompressedLogData()
		{
			return this.CreateZipFileAndGetTheContent();
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002184 File Offset: 0x00000384
		public void Dispose()
		{
			if (!this.disposed)
			{
				if (this.logWriter != null)
				{
					this.logWriter.Flush();
					this.logWriter.Dispose();
					this.logWriter = null;
				}
				if (File.Exists(this.logFullPathFileName))
				{
					File.Delete(this.logFullPathFileName);
				}
				this.disposed = true;
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000021E0 File Offset: 0x000003E0
		private byte[] CreateZipFileAndGetTheContent()
		{
			byte[] array = null;
			this.logWriter.Flush();
			this.logWriter.Dispose();
			this.logWriter = null;
			string path = Path.GetTempFileName() + ".zip";
			try
			{
				using (Package package = Package.Open(path, FileMode.CreateNew, FileAccess.ReadWrite))
				{
					PackagePart packagePart = package.CreatePart(new Uri(Uri.EscapeUriString("/" + this.logFileName), UriKind.Relative), "application/zip", CompressionOption.Maximum);
					using (StreamReader streamReader = new StreamReader(this.logFullPathFileName))
					{
						using (StreamWriter streamWriter = new StreamWriter(packagePart.GetStream(FileMode.Create, FileAccess.Write)))
						{
							char[] buffer = new char[1000];
							for (;;)
							{
								int num = streamReader.Read(buffer, 0, 1000);
								if (num <= 0)
								{
									break;
								}
								streamWriter.Write(buffer, 0, num);
							}
						}
					}
				}
				using (Stream stream = new FileStream(path, FileMode.Open))
				{
					array = new byte[stream.Length];
					stream.Seek(0L, SeekOrigin.Begin);
					stream.Read(array, 0, (int)stream.Length);
				}
			}
			finally
			{
				if (File.Exists(path))
				{
					File.Delete(path);
				}
			}
			this.logWriter = new StreamWriter(this.logFullPathFileName, true);
			return array;
		}

		// Token: 0x04000001 RID: 1
		internal const string CsvFileExtensionName = ".csv";

		// Token: 0x04000002 RID: 2
		internal const string ZipFileExtensionName = ".zip";

		// Token: 0x04000003 RID: 3
		internal const int BufferSize = 1000;

		// Token: 0x04000004 RID: 4
		private readonly string logFileName;

		// Token: 0x04000005 RID: 5
		private readonly string logFullPathFileName;

		// Token: 0x04000006 RID: 6
		private bool disposed;

		// Token: 0x04000007 RID: 7
		private StreamWriter logWriter;
	}
}
