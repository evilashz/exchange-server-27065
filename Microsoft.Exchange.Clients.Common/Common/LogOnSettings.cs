using System;
using System.Configuration;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Common
{
	// Token: 0x02000021 RID: 33
	public static class LogOnSettings
	{
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00008064 File Offset: 0x00006264
		// (set) Token: 0x060000FC RID: 252 RVA: 0x0000806B File Offset: 0x0000626B
		public static LogOnSettings.SignOutKind SignOut { get; private set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00008073 File Offset: 0x00006273
		// (set) Token: 0x060000FE RID: 254 RVA: 0x0000807A File Offset: 0x0000627A
		public static string SignOutPageUrl { get; private set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00008082 File Offset: 0x00006282
		// (set) Token: 0x06000100 RID: 256 RVA: 0x00008089 File Offset: 0x00006289
		public static bool IsLegacyLogOff { get; private set; }

		// Token: 0x06000101 RID: 257 RVA: 0x00008094 File Offset: 0x00006294
		static LogOnSettings()
		{
			string text = null;
			LogOnSettings.SignOut = LogOnSettings.SignOutKind.DefaultSignOut;
			try
			{
				text = ConfigurationManager.AppSettings["LogonSettings.SignOutKind"];
				LogOnSettings.SignOut = (LogOnSettings.SignOutKind)Enum.Parse(typeof(LogOnSettings.SignOutKind), text, true);
			}
			catch (Exception arg)
			{
				ExTraceGlobals.CoreTracer.TraceDebug<Exception>(0L, "LogonSettings::LogonSettings() Exception='{0}'", arg);
			}
			LogOnSettings.SignOutKind signOut = LogOnSettings.SignOut;
			if (signOut == LogOnSettings.SignOutKind.LegacyLogOff)
			{
				LogOnSettings.IsLegacyLogOff = true;
				LogOnSettings.SignOutPageUrl = LogOnSettings.logOnPageUrl;
			}
			else
			{
				LogOnSettings.IsLegacyLogOff = false;
				LogOnSettings.SignOutPageUrl = LogOnSettings.signOutPageUrl;
			}
			ExTraceGlobals.CoreTracer.TraceDebug<string, LogOnSettings.SignOutKind, string>(0L, "LogonSettings::LogonSettings() web.config.SignOut='{0}'; SignOut='{1}',SignOutPageUrl='{2}'", text, LogOnSettings.SignOut, LogOnSettings.SignOutPageUrl);
		}

		// Token: 0x04000261 RID: 609
		private static string signOutPageUrl = "auth/signout.aspx";

		// Token: 0x04000262 RID: 610
		private static string logOnPageUrl = "auth/logoff.aspx";

		// Token: 0x02000022 RID: 34
		public enum SignOutKind
		{
			// Token: 0x04000267 RID: 615
			DefaultSignOut,
			// Token: 0x04000268 RID: 616
			LegacyLogOff
		}
	}
}
