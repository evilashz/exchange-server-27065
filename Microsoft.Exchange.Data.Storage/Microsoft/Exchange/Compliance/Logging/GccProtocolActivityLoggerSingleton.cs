using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Compliance.Logging
{
	// Token: 0x020007D1 RID: 2001
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class GccProtocolActivityLoggerSingleton
	{
		// Token: 0x06004B10 RID: 19216 RVA: 0x0013A054 File Offset: 0x00138254
		public static GccProtocolActivityLogger Get(string clientInfoString)
		{
			string protocolName = GccProtocolActivityLoggerSingleton.GetProtocolName(clientInfoString);
			if (!GccProtocolActivityLoggerSingleton.DoWeNeedToLog(protocolName))
			{
				return null;
			}
			if (Interlocked.Exchange(ref GccProtocolActivityLoggerSingleton.creationNotMyJob, 1) == 0)
			{
				GccProtocolActivityLogger gccProtocolActivityLogger = new GccProtocolActivityLogger(protocolName);
				gccProtocolActivityLogger.Initialize();
				AppDomain.CurrentDomain.DomainUnload += GccProtocolActivityLoggerSingleton.CloseLogSingleton;
				GccProtocolActivityLoggerSingleton.instance = gccProtocolActivityLogger;
				GccProtocolActivityLoggerSingleton.busyCreating.Set();
			}
			else if (GccProtocolActivityLoggerSingleton.instance == null)
			{
				GccProtocolActivityLoggerSingleton.busyCreating.WaitOne();
			}
			return GccProtocolActivityLoggerSingleton.instance;
		}

		// Token: 0x06004B11 RID: 19217 RVA: 0x0013A0CC File Offset: 0x001382CC
		private static void CloseLogSingleton(object sender, EventArgs e)
		{
			if (GccProtocolActivityLoggerSingleton.instance != null)
			{
				GccProtocolActivityLogger gccProtocolActivityLogger = Interlocked.Exchange<GccProtocolActivityLogger>(ref GccProtocolActivityLoggerSingleton.instance, null);
				GccProtocolActivityLoggerSingleton.creationNotMyJob = 0;
				gccProtocolActivityLogger.Close();
			}
		}

		// Token: 0x06004B12 RID: 19218 RVA: 0x0013A0F8 File Offset: 0x001382F8
		private static string GetProtocolName(string clientInfoString)
		{
			int num = clientInfoString.IndexOf(';');
			if (num >= 0)
			{
				clientInfoString = clientInfoString.Substring(0, num);
			}
			if (clientInfoString.StartsWith("Client=", StringComparison.OrdinalIgnoreCase))
			{
				clientInfoString = clientInfoString.Substring("Client=".Length);
			}
			else if (clientInfoString.StartsWith("MSExchangeRPC", StringComparison.OrdinalIgnoreCase))
			{
				clientInfoString = "MSExchangeRPC";
			}
			return clientInfoString;
		}

		// Token: 0x06004B13 RID: 19219 RVA: 0x0013A154 File Offset: 0x00138354
		private static bool DoWeNeedToLog(string protocol)
		{
			if (GccProtocolActivityLogger.Config.Disabled)
			{
				return false;
			}
			int num = GccProtocolActivityLoggerSingleton.loggedProtocols.Length;
			for (int i = 0; i < num; i++)
			{
				if (GccProtocolActivityLoggerSingleton.loggedProtocols[i].Equals(protocol, StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x040028E6 RID: 10470
		private static readonly string[] loggedProtocols = new string[]
		{
			"activesync",
			"owa",
			"pop3/imap4",
			"webservices",
			"msexchangerpc",
			"mailboxsessionwrapper"
		};

		// Token: 0x040028E7 RID: 10471
		private static int creationNotMyJob;

		// Token: 0x040028E8 RID: 10472
		private static ManualResetEvent busyCreating = new ManualResetEvent(false);

		// Token: 0x040028E9 RID: 10473
		private static GccProtocolActivityLogger instance;
	}
}
