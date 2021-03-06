using System;
using System.Web;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Diagnostics.Components.Data.ApplicationLogic;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Net.Protocols;

namespace Microsoft.Exchange.Data.ApplicationLogic.Cafe
{
	// Token: 0x020000B7 RID: 183
	public static class HttpDatabaseValidationHelper
	{
		// Token: 0x060007CC RID: 1996 RVA: 0x0001ED2C File Offset: 0x0001CF2C
		public static void ValidateHttpDatabaseHeader(HttpContextBase context, Action onSuccess, Action<string> onError, Action onBadRequest)
		{
			string text = context.Request.Headers[WellKnownHeader.MailboxDatabaseGuid];
			ExTraceGlobals.FaultInjectionTracer.TraceTest<string>(2307271997U, ref text);
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			Guid guid;
			if (!Guid.TryParse(text, out guid))
			{
				onBadRequest();
				return;
			}
			bool flag = false;
			DatabaseLocationInfo databaseLocationInfo;
			Exception ex;
			bool flag2 = HttpDatabaseValidationHelper.TryGetActiveServerForDatabase(guid, GetServerForDatabaseFlags.BasicQuery, out databaseLocationInfo, out ex);
			if (flag2)
			{
				bool flag3 = !HttpDatabaseValidationHelper.IsCurrentServer(databaseLocationInfo.ServerFqdn);
				ExTraceGlobals.FaultInjectionTracer.TraceTest<bool>(3649449277U, ref flag3);
				if (flag3)
				{
					flag2 = HttpDatabaseValidationHelper.TryGetActiveServerForDatabase(guid, GetServerForDatabaseFlags.ReadThrough, out databaseLocationInfo, out ex);
					if (flag2)
					{
						flag = !HttpDatabaseValidationHelper.IsCurrentServer(databaseLocationInfo.ServerFqdn);
						ExTraceGlobals.FaultInjectionTracer.TraceTest<bool>(2844142909U, ref flag);
					}
				}
			}
			if (ex != null)
			{
				string obj = string.Format("Failed to lookup database guid [{0}]: {1}", guid, ex.GetType().ToString());
				onError(obj);
				return;
			}
			if (flag)
			{
				string obj2 = string.Format("Request routed to {0} but database [{1}] is mounted on {2}", ComputerInformation.DnsFullyQualifiedDomainName, guid, databaseLocationInfo.ServerFqdn);
				if (databaseLocationInfo.ServerVersion != 0)
				{
					context.Response.Headers[WellKnownHeader.XDBMountedOnServer] = string.Format("{0}~{1}~{2}", guid, databaseLocationInfo.ServerFqdn, databaseLocationInfo.ServerVersion);
				}
				onError(obj2);
				return;
			}
			onSuccess();
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x0001EE80 File Offset: 0x0001D080
		private static bool TryGetActiveServerForDatabase(Guid databaseGuid, GetServerForDatabaseFlags flag, out DatabaseLocationInfo databaseLocationInfo, out Exception activeManagerOperationException)
		{
			databaseLocationInfo = null;
			activeManagerOperationException = null;
			ActiveManagerOperationResult activeManagerOperationResult = ActiveManager.TryGetCachedServerForDatabaseBasic(databaseGuid, flag, out databaseLocationInfo);
			if (activeManagerOperationResult.Succeeded)
			{
				return true;
			}
			activeManagerOperationException = activeManagerOperationResult.Exception;
			return false;
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x0001EEAF File Offset: 0x0001D0AF
		private static bool IsCurrentServer(string serverFqdn)
		{
			return string.Equals(serverFqdn, ComputerInformation.DnsFullyQualifiedDomainName, StringComparison.OrdinalIgnoreCase) || string.Equals(serverFqdn, ComputerInformation.DnsHostName, StringComparison.OrdinalIgnoreCase) || string.Equals(serverFqdn, "localhost", StringComparison.OrdinalIgnoreCase) || string.Equals(serverFqdn, Environment.MachineName, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0400037A RID: 890
		private const uint IsWrongServerCachedLid = 3649449277U;

		// Token: 0x0400037B RID: 891
		private const uint IsWrongServerUncachedLid = 2844142909U;

		// Token: 0x0400037C RID: 892
		private const uint DatabaseGuidHeaderLid = 2307271997U;
	}
}
