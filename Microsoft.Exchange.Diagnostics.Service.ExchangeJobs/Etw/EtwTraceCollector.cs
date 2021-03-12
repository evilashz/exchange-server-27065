using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Microsoft.ExLogAnalyzer;

namespace Microsoft.Exchange.Diagnostics.Service.ExchangeJobs.Etw
{
	// Token: 0x0200001D RID: 29
	internal class EtwTraceCollector
	{
		// Token: 0x06000071 RID: 113 RVA: 0x000058D0 File Offset: 0x00003AD0
		public EtwTraceCollector(string guidFileLocation, Dictionary<string, string> etlFilePaths, string traceLogFilePath)
		{
			if (string.IsNullOrEmpty(guidFileLocation))
			{
				throw new ArgumentException("guidfilelocation");
			}
			if (string.IsNullOrEmpty(traceLogFilePath))
			{
				throw new ArgumentException("traceLogFilePath");
			}
			this.guidFileLocation = guidFileLocation;
			this.etlFilePaths = etlFilePaths;
			this.traceLogFilePath = traceLogFilePath;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00005920 File Offset: 0x00003B20
		internal bool Initialize()
		{
			Log.LogInformationMessage("Starting ETW data collection for server {0}", new object[]
			{
				Environment.MachineName
			});
			using (Dictionary<string, string>.Enumerator enumerator = this.etlFilePaths.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					KeyValuePair<string, string> keyValuePair = enumerator.Current;
					if (!string.IsNullOrEmpty(this.traceLogFilePath) && !keyValuePair.Key.Equals("NT Kernel Logger"))
					{
						string defaultArgs = string.Format("-start \"{0}\" -guid \"{1}\" ", keyValuePair.Key, this.guidFileLocation);
						EtwTraceCollector.TraceLogCollection state = new EtwTraceCollector.TraceLogCollection(keyValuePair.Value, 15000, keyValuePair.Key, " -matchanykw 0x00000001", defaultArgs);
						this.RunTraceLog(state);
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000059F0 File Offset: 0x00003BF0
		private void RunTraceLog(EtwTraceCollector.TraceLogCollection state)
		{
			try
			{
				if (this.ExecuteTraceLogCommand(state.GetArguments()))
				{
					Thread.Sleep(state.SleepTimeInMs);
				}
			}
			finally
			{
				string arguments = string.Format("-stop \"{0}\"", state.TraceName);
				if (!this.ExecuteTraceLogCommand(arguments))
				{
					Log.LogErrorMessage("tracelog.exe exited with a non-zero error code for {0}", new object[]
					{
						state.TraceName
					});
				}
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00005A64 File Offset: 0x00003C64
		private bool ExecuteTraceLogCommand(string arguments)
		{
			Log.LogInformationMessage("ETW Calculated Counter: Starting tracelog.exe collection for server {0} using args {1}", new object[]
			{
				Environment.MachineName,
				arguments
			});
			using (Process process = Process.Start(new ProcessStartInfo
			{
				CreateNoWindow = true,
				UseShellExecute = false,
				RedirectStandardOutput = true,
				FileName = this.traceLogFilePath,
				Arguments = arguments
			}))
			{
				process.WaitForExit(30000);
				if (process.ExitCode != 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x040000EF RID: 239
		private readonly string traceLogFilePath;

		// Token: 0x040000F0 RID: 240
		private readonly string guidFileLocation;

		// Token: 0x040000F1 RID: 241
		private readonly Dictionary<string, string> etlFilePaths;

		// Token: 0x0200001E RID: 30
		private struct TraceLogCollection
		{
			// Token: 0x06000075 RID: 117 RVA: 0x00005AFC File Offset: 0x00003CFC
			public TraceLogCollection(string traceFilePath, int sleepTimeInMs, string traceName, string customArgs, string defaultArgs)
			{
				if (string.IsNullOrEmpty(traceFilePath))
				{
					throw new ArgumentNullException("traceFilePath");
				}
				if (string.IsNullOrEmpty(traceName))
				{
					throw new ArgumentNullException("traceName");
				}
				if (string.IsNullOrEmpty(defaultArgs))
				{
					throw new ArgumentNullException("defaultArgs");
				}
				if (sleepTimeInMs < 1)
				{
					sleepTimeInMs = 15000;
				}
				this.TraceFilePath = traceFilePath;
				this.SleepTimeInMs = sleepTimeInMs;
				this.TraceName = traceName;
				this.CustomArgs = customArgs;
				this.DefaultArgs = defaultArgs;
			}

			// Token: 0x06000076 RID: 118 RVA: 0x00005B74 File Offset: 0x00003D74
			public string GetArguments()
			{
				string text = string.Format("{0} -f \"{1}\" -seq {2} -min {3} -max {4}", new object[]
				{
					this.DefaultArgs,
					this.TraceFilePath,
					500.ToString(),
					2.ToString(),
					200.ToString()
				});
				if (!string.IsNullOrEmpty(this.CustomArgs))
				{
					string.Join(text, new string[]
					{
						this.CustomArgs
					});
				}
				return text;
			}

			// Token: 0x040000F2 RID: 242
			private const int MaximumFileSize = 500;

			// Token: 0x040000F3 RID: 243
			private const int MaximumBufferCount = 200;

			// Token: 0x040000F4 RID: 244
			private const int MinimumBufferCount = 2;

			// Token: 0x040000F5 RID: 245
			public readonly string TraceFilePath;

			// Token: 0x040000F6 RID: 246
			public readonly string TraceName;

			// Token: 0x040000F7 RID: 247
			public readonly int SleepTimeInMs;

			// Token: 0x040000F8 RID: 248
			public readonly string CustomArgs;

			// Token: 0x040000F9 RID: 249
			public readonly string DefaultArgs;
		}
	}
}
