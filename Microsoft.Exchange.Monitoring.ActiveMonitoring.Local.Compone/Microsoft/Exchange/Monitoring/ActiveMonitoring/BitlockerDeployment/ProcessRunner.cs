using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.BitlockerDeployment
{
	// Token: 0x02000028 RID: 40
	internal static class ProcessRunner
	{
		// Token: 0x0600014E RID: 334 RVA: 0x00009DE1 File Offset: 0x00007FE1
		public static int Run(string executableFilename, string arguments, int timeout, string workingDirectory, out string outputString, out string errorString)
		{
			return ProcessRunner.Run(executableFilename, arguments, null, timeout, workingDirectory, out outputString, out errorString);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00009E8C File Offset: 0x0000808C
		public static int Run(string executableFilename, string arguments, Dictionary<string, string> environmentVariables, int timeout, string workingDirectory, out string outputString, out string errorString)
		{
			if (executableFilename == null)
			{
				throw new ArgumentNullException("executableFileName");
			}
			int result = 0;
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
						process.Start();
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
							throw new TimeoutException(string.Format("Process took more than {0} seconds to complete", 100000));
						}
						stdOutputEvent.WaitOne(100000);
						stdErrorEvent.WaitOne(100000);
						result = process.ExitCode;
					}
				}
			}
			try
			{
				outputString = standardOutput.ToString();
			}
			catch (ArgumentOutOfRangeException)
			{
				outputString = null;
			}
			try
			{
				errorString = standardError.ToString();
			}
			catch (ArgumentOutOfRangeException)
			{
				errorString = null;
			}
			return result;
		}

		// Token: 0x04000114 RID: 276
		public const int NoTimeout = -1;

		// Token: 0x04000115 RID: 277
		public const int StdOutputErrorEventTimeout = 100000;

		// Token: 0x02000029 RID: 41
		public class ExchangeProcessTimeoutException : Exception
		{
		}
	}
}
