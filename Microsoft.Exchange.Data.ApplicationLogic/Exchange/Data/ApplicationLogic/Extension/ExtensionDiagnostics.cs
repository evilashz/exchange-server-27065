using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic.Extension
{
	// Token: 0x020000F7 RID: 247
	internal static class ExtensionDiagnostics
	{
		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000A6C RID: 2668 RVA: 0x00028BB5 File Offset: 0x00026DB5
		internal static ExEventLog Logger
		{
			get
			{
				return ExtensionDiagnostics.logger;
			}
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x00028BBC File Offset: 0x00026DBC
		internal static string GetLoggedExceptionString(Exception exception)
		{
			string text = exception.ToString();
			if (text.Length > 32000)
			{
				text = text.Substring(0, 2000) + "...\n" + text.Substring(text.Length - 20000, 20000);
			}
			return text;
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x00028C0C File Offset: 0x00026E0C
		internal static object HandleNullObjectTrace(object objectToDisplay)
		{
			return objectToDisplay ?? "Object is null";
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x00028C18 File Offset: 0x00026E18
		internal static string GetLoggedMailboxIdentifier(IExchangePrincipal exchangePrincipal)
		{
			return exchangePrincipal.MailboxInfo.PrimarySmtpAddress.ToString();
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x00028C3E File Offset: 0x00026E3E
		internal static void LogToDatacenterOnly(ExEventLog.EventTuple tuple, string periodicKey, params object[] messageArgs)
		{
			if (Globals.IsDatacenter)
			{
				ExtensionDiagnostics.Logger.LogEvent(tuple, periodicKey, messageArgs);
			}
		}

		// Token: 0x04000521 RID: 1313
		private static Guid eventLogComponentGuid = new Guid("{2C1EB772-38A2-4812-903B-244EAB5169A6}");

		// Token: 0x04000522 RID: 1314
		private static readonly ExEventLog logger = new ExEventLog(ExtensionDiagnostics.eventLogComponentGuid, "MSExchangeApplicationLogic");
	}
}
