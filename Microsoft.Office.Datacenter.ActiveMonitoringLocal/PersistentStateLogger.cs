using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x020000A4 RID: 164
	internal class PersistentStateLogger : DisposeTrackableBase
	{
		// Token: 0x060007D7 RID: 2007 RVA: 0x000209C8 File Offset: 0x0001EBC8
		internal PersistentStateLogger(ILogConfiguration configuration)
		{
			if (configuration == null)
			{
				throw new ArgumentNullException("configuration");
			}
			this.configuration = configuration;
			if (this.configuration.IsLoggingEnabled)
			{
				string version = PersistentStateLogger.assembly.GetName().Version.ToString();
				this.logSchema = new LogSchema("Microsoft Exchange Server", version, this.configuration.LogType, PersistentStateLogger.Fields);
				this.log = new Log(this.configuration.LogPrefix, null, this.configuration.LogComponent);
				this.log.Configure(this.configuration.LogPath, this.configuration.MaxLogAge, this.configuration.MaxLogDirectorySizeInBytes, this.configuration.MaxLogFileSizeInBytes, 0, new TimeSpan(0, 1, 0));
			}
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x00020A98 File Offset: 0x0001EC98
		public void SetForeLogFileRollOver(bool flag)
		{
			if (!this.configuration.IsLoggingEnabled)
			{
				return;
			}
			this.log.TestHelper_ForceLogFileRollOver = flag;
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x00020ABC File Offset: 0x0001ECBC
		public string GetLogFile()
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(this.configuration.LogPath);
			IEnumerable<FileInfo> enumerable = (from f in directoryInfo.GetFiles()
			orderby f.LastWriteTime descending
			select f).Take(3);
			int num = 0;
			int num2 = 0;
			if (enumerable.Count<FileInfo>() <= 0)
			{
				return null;
			}
			foreach (FileInfo fileInfo in enumerable)
			{
				using (StreamReader streamReader = new StreamReader(fileInfo.FullName))
				{
					string text = string.Empty;
					while ((text = streamReader.ReadLine()) != null)
					{
						if (text.Contains(LocalDataAccess.PersistentStateIdentity))
						{
							bool resultSize = this.GetResultSize(text, out num2);
							if (resultSize && num2 != 0 && num == num2)
							{
								return fileInfo.FullName;
							}
							break;
						}
						else
						{
							num++;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x00020BC8 File Offset: 0x0001EDC8
		public void LogEvent(string tempResult)
		{
			if (!this.configuration.IsLoggingEnabled)
			{
				return;
			}
			LogRowFormatter row = this.CreateRow(tempResult);
			this.log.Append(row, -1);
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x00020BF8 File Offset: 0x0001EDF8
		public bool GetResultSize(string str, out int resultSize)
		{
			string[] array = str.Split(new char[]
			{
				'|'
			});
			return int.TryParse(array[11], out resultSize);
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x00020C23 File Offset: 0x0001EE23
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<PersistentStateLogger>(this);
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x00020C2B File Offset: 0x0001EE2B
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.log != null)
			{
				this.log.Flush();
				this.log.Close();
			}
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x00020C50 File Offset: 0x0001EE50
		private LogRowFormatter CreateRow(string data)
		{
			LogRowFormatter logRowFormatter = new LogRowFormatter(this.logSchema, true);
			logRowFormatter[0] = data;
			return logRowFormatter;
		}

		// Token: 0x040005FF RID: 1535
		private const string SoftwareName = "Microsoft Exchange Server";

		// Token: 0x04000600 RID: 1536
		private static readonly string[] Fields = Enum.GetNames(typeof(PersistentStateLogger.Field));

		// Token: 0x04000601 RID: 1537
		private static readonly Assembly assembly = Assembly.GetExecutingAssembly();

		// Token: 0x04000602 RID: 1538
		private readonly LogSchema logSchema;

		// Token: 0x04000603 RID: 1539
		private readonly Log log;

		// Token: 0x04000604 RID: 1540
		private readonly ILogConfiguration configuration;

		// Token: 0x020000A5 RID: 165
		private enum Field
		{
			// Token: 0x04000607 RID: 1543
			Data
		}
	}
}
