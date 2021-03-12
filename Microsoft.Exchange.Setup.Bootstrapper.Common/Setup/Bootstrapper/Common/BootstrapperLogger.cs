using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Security;
using System.Security.Principal;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Setup.CommonBase;

namespace Microsoft.Exchange.Setup.Bootstrapper.Common
{
	// Token: 0x02000003 RID: 3
	internal class BootstrapperLogger : IBootstrapperLogger
	{
		// Token: 0x06000008 RID: 8 RVA: 0x000020D0 File Offset: 0x000002D0
		public void StartLogging()
		{
			if (!Directory.Exists(BootstrapperLogger.setupLogDirectory))
			{
				Directory.CreateDirectory(BootstrapperLogger.setupLogDirectory);
			}
			try
			{
				BootstrapperLogger.indentationLevel = 0;
				FileStream stream = new FileStream(BootstrapperLogger.setupLogFilePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
				BootstrapperLogger.sw = new StreamWriter(stream);
				BootstrapperLogger.sw.AutoFlush = true;
				this.isLoggingStarted = true;
			}
			catch (IOException ex)
			{
				throw new SetupLogInitializeException(ex.Message, ex);
			}
			catch (UnauthorizedAccessException ex2)
			{
				throw new SetupLogInitializeException(ex2.Message, ex2);
			}
			this.Log(Strings.AsterixLine);
			this.Log(Strings.SetupLogStarted);
			this.Log(Strings.AsterixLine);
			this.Log(Strings.LocalTimeZone(TimeZoneInfo.Local.DisplayName));
			this.Log(Strings.OSVersion(Environment.OSVersion.ToString()));
			try
			{
				this.LogAssemblyVersion();
			}
			catch (FileVersionNotFoundException ex3)
			{
				throw new SetupLogInitializeException(ex3.Message, ex3);
			}
			this.LogUserName();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021D4 File Offset: 0x000003D4
		public void StopLogging()
		{
			if (this.isLoggingStarted)
			{
				this.Log(Strings.SetupLogEnd);
				this.Log(Strings.AsterixLine);
				this.isLoggingStarted = false;
				BootstrapperLogger.sw.Close();
				BootstrapperLogger.sw = null;
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000220B File Offset: 0x0000040B
		public void Log(LocalizedString localizedString)
		{
			if (this.isLoggingStarted)
			{
				BootstrapperLogger.LogMessageString(localizedString.ToString());
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002227 File Offset: 0x00000427
		public void LogWarning(LocalizedString localizedString)
		{
			if (this.isLoggingStarted)
			{
				BootstrapperLogger.LogWarningString(localizedString.ToString());
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002244 File Offset: 0x00000444
		public void LogError(Exception e)
		{
			while (e != null)
			{
				LocalizedException ex = e as LocalizedException;
				string message;
				if (ex != null)
				{
					ex.FormatProvider = new CultureInfo("en-US");
					message = ex.Message;
				}
				else
				{
					message = e.Message;
				}
				if (this.isLoggingStarted)
				{
					BootstrapperLogger.LogErrorString(message);
				}
				e = e.InnerException;
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002298 File Offset: 0x00000498
		public void IncreaseIndentation(LocalizedString tag)
		{
			if (this.isLoggingStarted)
			{
				BootstrapperLogger.indentationLevel++;
				if (!string.IsNullOrEmpty(tag))
				{
					this.Log(tag);
				}
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000022C2 File Offset: 0x000004C2
		public void DecreaseIndentation()
		{
			if (this.isLoggingStarted)
			{
				BootstrapperLogger.indentationLevel--;
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000022D8 File Offset: 0x000004D8
		private static void LogErrorString(string message)
		{
			BootstrapperLogger.LogMessageString("[ERROR] " + message);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000022EC File Offset: 0x000004EC
		private static void LogMessageString(string message)
		{
			try
			{
				DateTime utcNow = DateTime.UtcNow;
				BootstrapperLogger.sw.WriteLine(string.Format("[{0}.{1:0000}] [{2}] {3}", new object[]
				{
					utcNow.ToString("MM/dd/yyyy HH:mm:ss"),
					utcNow.Millisecond,
					BootstrapperLogger.indentationLevel,
					message
				}));
			}
			catch (IOException)
			{
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002360 File Offset: 0x00000560
		private static void LogWarningString(string message)
		{
			BootstrapperLogger.LogMessageString("[WARNING] " + message);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002374 File Offset: 0x00000574
		private static string GetExecutingVersion()
		{
			string result = string.Empty;
			string text = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Setup\\ServerRoles\\Common");
			if (!Directory.Exists(text))
			{
				text = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			}
			string text2 = Path.Combine(text, "ExSetup.exe");
			if (File.Exists(text2))
			{
				string versionOfApplication = SetupHelper.GetVersionOfApplication(text2);
				if (!string.IsNullOrEmpty(versionOfApplication))
				{
					result = new Version(versionOfApplication).ToString();
				}
			}
			return result;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000023E9 File Offset: 0x000005E9
		private void LogAssemblyVersion()
		{
			this.Log(Strings.AssemblyVersion(BootstrapperLogger.GetExecutingVersion()));
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000023FC File Offset: 0x000005FC
		private void LogUserName()
		{
			try
			{
				WindowsPrincipal windowsPrincipal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
				string name = windowsPrincipal.Identity.Name;
				this.Log(Strings.UserName(name));
			}
			catch (SecurityException ex)
			{
				this.LogWarning(Strings.UserNameError(ex.Message));
			}
		}

		// Token: 0x04000001 RID: 1
		private const int MinimumIndentationLevel = 0;

		// Token: 0x04000002 RID: 2
		private const int MaximumIndentationLevel = 2;

		// Token: 0x04000003 RID: 3
		private const string ErrorTag = "[ERROR] ";

		// Token: 0x04000004 RID: 4
		private const string WarningTag = "[WARNING] ";

		// Token: 0x04000005 RID: 5
		private static readonly string setupLogDirectory = Environment.ExpandEnvironmentVariables("%systemdrive%\\ExchangeSetupLogs");

		// Token: 0x04000006 RID: 6
		private static readonly string setupLogFileName = "ExchangeSetupBootStrapper.log";

		// Token: 0x04000007 RID: 7
		private static readonly string setupLogFilePath = Path.Combine(BootstrapperLogger.setupLogDirectory, BootstrapperLogger.setupLogFileName);

		// Token: 0x04000008 RID: 8
		private static StreamWriter sw;

		// Token: 0x04000009 RID: 9
		private static int indentationLevel;

		// Token: 0x0400000A RID: 10
		private bool isLoggingStarted;
	}
}
