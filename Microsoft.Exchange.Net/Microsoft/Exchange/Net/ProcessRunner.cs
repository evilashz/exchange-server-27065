using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000C61 RID: 3169
	internal static class ProcessRunner
	{
		// Token: 0x06004640 RID: 17984 RVA: 0x000BBBE6 File Offset: 0x000B9DE6
		public static int Run(string executableFilename, string arguments, int timeout, string workingDirectory, out string outputString, out string errorString)
		{
			return ProcessRunner.Run(executableFilename, arguments, null, timeout, workingDirectory, out outputString, out errorString);
		}

		// Token: 0x06004641 RID: 17985 RVA: 0x000BBC90 File Offset: 0x000B9E90
		public static int Run(string executableFilename, string arguments, Dictionary<string, string> environmentVariables, int timeout, string workingDirectory, out string outputString, out string errorString)
		{
			if (executableFilename == null)
			{
				throw new ArgumentNullException("executableFileName");
			}
			int num = 0;
			StringBuilder standardOutput = new StringBuilder();
			StringBuilder standardError = new StringBuilder();
			using (Process process = new Process())
			{
				using (ManualResetEvent stdOutputEvent = new ManualResetEvent(false))
				{
					using (ManualResetEvent stdErrorEvent = new ManualResetEvent(false))
					{
						process.StartInfo = new ProcessStartInfo();
						process.StartInfo.FileName = executableFilename;
						if (workingDirectory != null)
						{
							process.StartInfo.WorkingDirectory = workingDirectory;
						}
						if (environmentVariables != null)
						{
							foreach (KeyValuePair<string, string> keyValuePair in environmentVariables)
							{
								process.StartInfo.EnvironmentVariables[keyValuePair.Key] = keyValuePair.Value;
							}
						}
						process.StartInfo.CreateNoWindow = true;
						process.StartInfo.UseShellExecute = false;
						process.StartInfo.Arguments = arguments;
						if (timeout != 0)
						{
							process.StartInfo.RedirectStandardOutput = true;
							process.StartInfo.RedirectStandardError = true;
							process.OutputDataReceived += delegate(object sendingProcess, DataReceivedEventArgs outLine)
							{
								if (outLine.Data == null)
								{
									stdOutputEvent.Set();
									return;
								}
								if (outLine.Data != string.Empty)
								{
									standardOutput.AppendLine(outLine.Data);
								}
							};
							process.ErrorDataReceived += delegate(object sendingProcess, DataReceivedEventArgs outLine)
							{
								if (outLine.Data == null)
								{
									stdErrorEvent.Set();
									return;
								}
								if (outLine.Data != string.Empty)
								{
									standardError.AppendLine(outLine.Data);
								}
							};
						}
						process.Start();
						if (timeout != 0)
						{
							process.BeginOutputReadLine();
							process.BeginErrorReadLine();
							if (timeout != -1)
							{
								process.WaitForExit(timeout);
							}
							else
							{
								process.WaitForExit();
							}
							if (!process.HasExited)
							{
								ExWatson.SendHangWatsonReport(new ProcessRunner.ExchangeProcessTimeoutException(), process);
								process.Close();
								throw new TimeoutException(NetException.ProcessRunnerTimeout);
							}
							stdOutputEvent.WaitOne(10000);
							stdErrorEvent.WaitOne(10000);
							num = process.ExitCode;
						}
					}
				}
			}
			try
			{
				outputString = standardOutput.ToString();
			}
			catch (ArgumentOutOfRangeException ex)
			{
				outputString = NetServerException.ProcessRunnerToStringArgumentOutOfRangeException("standardOutput", ex.Message ?? string.Empty, executableFilename ?? string.Empty, num);
			}
			try
			{
				errorString = standardError.ToString();
			}
			catch (ArgumentOutOfRangeException ex2)
			{
				errorString = NetServerException.ProcessRunnerToStringArgumentOutOfRangeException("standardError", ex2.Message ?? string.Empty, executableFilename ?? string.Empty, num);
			}
			return num;
		}

		// Token: 0x04003AA5 RID: 15013
		public const int NoTimeout = -1;

		// Token: 0x04003AA6 RID: 15014
		public const int NoWait = 0;

		// Token: 0x04003AA7 RID: 15015
		public const int StdOutputErrorEventTimeout = 10000;

		// Token: 0x02000C62 RID: 3170
		public class ExchangeProcessTimeoutException : Exception
		{
		}
	}
}
