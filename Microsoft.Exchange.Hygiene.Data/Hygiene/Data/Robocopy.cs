using System;
using System.Diagnostics;
using System.IO;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x020000A1 RID: 161
	internal static class Robocopy
	{
		// Token: 0x0600056E RID: 1390 RVA: 0x00012468 File Offset: 0x00010668
		internal static void Copy(string fromDir, string toDir, string fileName)
		{
			string arguments = string.Format(" \"{0}\" \"{1}\" \"{2}\" /NP /Z /R:10 /MT /XO /NJH /NJS /TS /FP /NDL", fromDir.TrimEnd(new char[]
			{
				'\\'
			}), toDir.TrimEnd(new char[]
			{
				'\\'
			}), fileName);
			string fileName2 = Path.Combine(Environment.SystemDirectory, "robocopy.exe");
			using (Process process = new Process())
			{
				process.StartInfo = new ProcessStartInfo(fileName2, arguments)
				{
					UseShellExecute = false,
					CreateNoWindow = true,
					RedirectStandardOutput = true,
					RedirectStandardError = true
				};
				process.Start();
				string text = process.StandardOutput.ReadToEnd();
				string text2 = process.StandardError.ReadToEnd();
				process.WaitForExit();
				int exitCode = process.ExitCode;
				int num = 28;
				if ((exitCode & num) != 0)
				{
					string message = string.Format("Unable to copy {0} to {1} for file {2} : robocopy errors, return code={3}\nstdout: {4}\nstderr: {5}", new object[]
					{
						fromDir,
						toDir,
						fileName,
						exitCode,
						text,
						text2
					});
					throw new RoboCopyException(message);
				}
			}
		}
	}
}
