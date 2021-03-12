using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Timers;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020001CF RID: 463
	public class FileBasedDeserializedTypeGatherer : IDeserializedTypesGatherer
	{
		// Token: 0x06000D01 RID: 3329 RVA: 0x00036AE4 File Offset: 0x00034CE4
		public FileBasedDeserializedTypeGatherer(string logPath, TimeSpan dumpInterval)
		{
			try
			{
				this.filePath = logPath;
				this.dumpInterval = dumpInterval;
				this.dumpTimer = new Timer(this.dumpInterval.TotalMilliseconds);
				this.dumpTimer.Elapsed += this.FlushIfNecessary;
				this.dumpTimer.Start();
				using (Process currentProcess = Process.GetCurrentProcess())
				{
					this.ProcessId = currentProcess.Id;
				}
			}
			catch
			{
				this.filePath = null;
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000D02 RID: 3330 RVA: 0x00036BA4 File Offset: 0x00034DA4
		// (set) Token: 0x06000D03 RID: 3331 RVA: 0x00036BAC File Offset: 0x00034DAC
		public int ProcessId { get; private set; }

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000D04 RID: 3332 RVA: 0x00036BB5 File Offset: 0x00034DB5
		// (set) Token: 0x06000D05 RID: 3333 RVA: 0x00036BBD File Offset: 0x00034DBD
		public bool AddStackTrace { get; set; }

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000D06 RID: 3334 RVA: 0x00036BC6 File Offset: 0x00034DC6
		// (set) Token: 0x06000D07 RID: 3335 RVA: 0x00036BCE File Offset: 0x00034DCE
		public bool ClearAfterSave { get; set; }

		// Token: 0x06000D08 RID: 3336 RVA: 0x00036BD7 File Offset: 0x00034DD7
		public void Add(string typeName, string assemblyName)
		{
			if (ExchangeBinaryFormatterFactory.LoggingEnabled)
			{
				if (this.filePath == null || this.dumpTimer == null)
				{
					return;
				}
				this.map.TryAdd(this.BuildKey(typeName, assemblyName), 0);
			}
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x00036C08 File Offset: 0x00034E08
		private string BuildKey(string typeName, string assemblyName)
		{
			string text = null;
			if (this.AddStackTrace)
			{
				StackTrace stackTrace = new StackTrace(2);
				text = stackTrace.ToString();
			}
			return string.Concat(new string[]
			{
				typeName,
				"--",
				assemblyName,
				"--",
				text
			}) ?? "NoStack";
		}

		// Token: 0x06000D0A RID: 3338 RVA: 0x00036C60 File Offset: 0x00034E60
		private void FlushIfNecessary(object state, ElapsedEventArgs e)
		{
			try
			{
				if (this.filePath != null && this.map.Any<KeyValuePair<string, int>>())
				{
					ConcurrentDictionary<string, int> concurrentDictionary = null;
					if (DateTime.UtcNow - this.lastRefreshUtcTime >= this.dumpInterval)
					{
						lock (this.lockObj)
						{
							if (DateTime.UtcNow - this.lastRefreshUtcTime >= this.dumpInterval)
							{
								concurrentDictionary = this.map;
								if (this.ClearAfterSave)
								{
									this.map = new ConcurrentDictionary<string, int>();
								}
								this.lastRefreshUtcTime = DateTime.UtcNow;
							}
						}
					}
					if (concurrentDictionary != null)
					{
						if (!Directory.Exists(this.filePath))
						{
							Directory.CreateDirectory(this.filePath);
						}
						string path = Path.Combine(this.filePath, "DeserializedTypesEncountered_" + this.ProcessId + ".log");
						using (StreamWriter streamWriter = new StreamWriter(path))
						{
							foreach (KeyValuePair<string, int> keyValuePair in concurrentDictionary)
							{
								streamWriter.WriteLine(keyValuePair.Key);
							}
							streamWriter.Flush();
						}
					}
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x04000994 RID: 2452
		private TimeSpan dumpInterval;

		// Token: 0x04000995 RID: 2453
		private object lockObj = new object();

		// Token: 0x04000996 RID: 2454
		private DateTime lastRefreshUtcTime = DateTime.UtcNow;

		// Token: 0x04000997 RID: 2455
		private string filePath;

		// Token: 0x04000998 RID: 2456
		private Timer dumpTimer;

		// Token: 0x04000999 RID: 2457
		private ConcurrentDictionary<string, int> map = new ConcurrentDictionary<string, int>();
	}
}
