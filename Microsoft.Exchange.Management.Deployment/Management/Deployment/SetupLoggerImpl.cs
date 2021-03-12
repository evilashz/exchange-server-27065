using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Security;
using System.Security.Principal;
using System.Text;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Setup;
using Microsoft.Exchange.Setup.Common;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000029 RID: 41
	internal class SetupLoggerImpl : ISetupLogger
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000091 RID: 145 RVA: 0x0000351E File Offset: 0x0000171E
		// (set) Token: 0x06000092 RID: 146 RVA: 0x00003525 File Offset: 0x00001725
		public bool IsPrereqLogging
		{
			get
			{
				return TaskLogger.IsPrereqLogging;
			}
			set
			{
				TaskLogger.IsPrereqLogging = value;
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003530 File Offset: 0x00001730
		public void StartLogging()
		{
			if (!Directory.Exists(SetupLoggerImpl.setupLogDirectory))
			{
				Directory.CreateDirectory(SetupLoggerImpl.setupLogDirectory);
			}
			string filename = Path.Combine(SetupLoggerImpl.setupLogDirectory, SetupLoggerImpl.setupLogFileNameForWatson);
			string filename2 = Path.Combine(SetupLoggerImpl.setupLogDirectory, "ExchangeSetup.msilog");
			string dataMiningPath = null;
			if (DatacenterRegistry.IsMicrosoftHostedOnly())
			{
				string text = "d:\\ExchangeSetupLogs";
				if (!Directory.Exists(text))
				{
					Directory.CreateDirectory(text);
				}
				dataMiningPath = Path.Combine(text, SetupLoggerImpl.setupLogFileName);
			}
			ExWatson.TryAddExtraFile(filename);
			ExWatson.TryAddExtraFile(filename2);
			try
			{
				TaskLogger.IsSetupLogging = true;
				TaskLogger.StartFileLogging(SetupLoggerImpl.setupLogFilePath, dataMiningPath);
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
			this.Log(SetupLoggerImpl.AsterixLine);
			this.Log(Strings.SetupLogStarted);
			this.Log(SetupLoggerImpl.AsterixLine);
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
			this.TaskStartTime = DateTime.UtcNow;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003688 File Offset: 0x00001888
		public void StopLogging()
		{
			if (this.isLoggingStarted)
			{
				this.Log(Strings.SetupLogEnd);
				this.Log(SetupLoggerImpl.AsterixLine);
				this.LogForDataMining(Strings.SetupLogStarted, this.TaskStartTime);
				TaskLogger.StopFileLogging();
				this.isLoggingStarted = false;
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000036D5 File Offset: 0x000018D5
		public void Log(LocalizedString localizedString)
		{
			TaskLogger.Log(localizedString);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000036DD File Offset: 0x000018DD
		public void LogWarning(LocalizedString localizedString)
		{
			TaskLogger.LogWarning(localizedString);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000036E5 File Offset: 0x000018E5
		public void LogError(Exception e)
		{
			TaskLogger.LogError(e);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000036F0 File Offset: 0x000018F0
		public void TraceEnter(params object[] arguments)
		{
			if (!ExTraceGlobals.TraceTracer.IsTraceEnabled(TraceType.InfoTrace))
			{
				return;
			}
			StackTrace stackTrace = new StackTrace();
			MethodBase method = stackTrace.GetFrame(1).GetMethod();
			StringBuilder stringBuilder = new StringBuilder();
			string argumentList = string.Empty;
			if (arguments != null)
			{
				for (int i = 0; i < arguments.Length; i++)
				{
					stringBuilder.Append((arguments[i] != null) ? arguments[i].ToString() : "null");
					if (i + 1 < arguments.Length)
					{
						stringBuilder.Append(", ");
					}
				}
				argumentList = stringBuilder.ToString();
			}
			ExTraceGlobals.TraceTracer.Information(0L, Strings.TraceFunctionEnter(method.ReflectedType, method.Name, argumentList));
		}

		// Token: 0x06000099 RID: 153 RVA: 0x0000379C File Offset: 0x0000199C
		public void TraceExit()
		{
			if (!ExTraceGlobals.TraceTracer.IsTraceEnabled(TraceType.InfoTrace))
			{
				return;
			}
			StackTrace stackTrace = new StackTrace();
			MethodBase method = stackTrace.GetFrame(1).GetMethod();
			ExTraceGlobals.TraceTracer.Information(0L, Strings.TraceFunctionExit(method.ReflectedType, method.Name));
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000037EC File Offset: 0x000019EC
		public void IncreaseIndentation(LocalizedString tag)
		{
			TaskLogger.IncreaseIndentation(tag);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000037F4 File Offset: 0x000019F4
		public void DecreaseIndentation()
		{
			TaskLogger.DecreaseIndentation();
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000037FB File Offset: 0x000019FB
		public void LogForDataMining(string task, DateTime startTime)
		{
			TaskLogger.LogDataMiningMessage(SetupLoggerImpl.BuildVersion, task, startTime);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003809 File Offset: 0x00001A09
		private void LogAssemblyVersion()
		{
			SetupLoggerImpl.BuildVersion = ConfigurationContext.Setup.GetExecutingVersion().ToString();
			SetupLogger.Log(Strings.AssemblyVersion(SetupLoggerImpl.BuildVersion));
		}

		// Token: 0x0600009E RID: 158 RVA: 0x0000382C File Offset: 0x00001A2C
		private void LogUserName()
		{
			try
			{
				WindowsPrincipal windowsPrincipal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
				string name = windowsPrincipal.Identity.Name;
				SetupLogger.Log(Strings.UserName(name));
			}
			catch (SecurityException ex)
			{
				this.LogWarning(Strings.UserNameError(ex.Message));
			}
		}

		// Token: 0x0400008A RID: 138
		private const string MsiLogFileName = "ExchangeSetup.msilog";

		// Token: 0x0400008B RID: 139
		private static readonly LocalizedString AsterixLine = new LocalizedString("**********************************************");

		// Token: 0x0400008C RID: 140
		private static readonly string setupLogDirectory = ConfigurationContext.Setup.SetupLoggingPath;

		// Token: 0x0400008D RID: 141
		private static readonly string setupLogFileName = ConfigurationContext.Setup.SetupLogFileName;

		// Token: 0x0400008E RID: 142
		private static readonly string setupLogFileNameForWatson = ConfigurationContext.Setup.SetupLogFileNameForWatson;

		// Token: 0x0400008F RID: 143
		private static readonly string setupLogFilePath = Path.Combine(SetupLoggerImpl.setupLogDirectory, SetupLoggerImpl.setupLogFileName);

		// Token: 0x04000090 RID: 144
		private static string BuildVersion;

		// Token: 0x04000091 RID: 145
		private bool isLoggingStarted;

		// Token: 0x04000092 RID: 146
		private DateTime TaskStartTime;
	}
}
