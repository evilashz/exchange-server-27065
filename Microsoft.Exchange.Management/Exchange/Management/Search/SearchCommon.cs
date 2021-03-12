using System;
using System.ServiceProcess;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Management.Search
{
	// Token: 0x0200015B RID: 347
	internal static class SearchCommon
	{
		// Token: 0x06000CA0 RID: 3232 RVA: 0x0003A304 File Offset: 0x00038504
		internal static bool IsServiceRunning(string serviceName, string serverName, out bool scError)
		{
			bool result = true;
			scError = false;
			using (ServiceController serviceController = new ServiceController(serviceName, serverName))
			{
				try
				{
					result = (serviceController.Status == ServiceControllerStatus.Running);
				}
				catch (InvalidOperationException)
				{
					result = false;
					scError = true;
				}
			}
			return result;
		}

		// Token: 0x06000CA1 RID: 3233 RVA: 0x0003A35C File Offset: 0x0003855C
		private static LocalizedString ShortErrorMsgFromException(Exception exception)
		{
			if (exception.InnerException != null)
			{
				return Strings.MapiTransactionShortErrorMsgFromExceptionWithInnerException(exception.GetType().ToString(), exception.Message, exception.InnerException.GetType().ToString(), exception.InnerException.Message);
			}
			return Strings.MapiTransactionShortErrorMsgFromException(exception.GetType().ToString(), exception.Message);
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x0003A3BC File Offset: 0x000385BC
		internal static LocalizedString DiagnoseException(string serverFqdn, Guid mdbGuid, Exception exception)
		{
			try
			{
				bool flag = false;
				using (ExRpcAdmin exRpcAdmin = ExRpcAdmin.Create("Client=Management", serverFqdn, null, null, null))
				{
					MdbStatus[] array = exRpcAdmin.ListMdbStatus(new Guid[]
					{
						mdbGuid
					});
					if (array.Length != 0)
					{
						flag = ((array[0].Status & MdbStatusFlags.Online) == MdbStatusFlags.Online);
					}
				}
				if (!flag)
				{
					return Strings.MapiTransactionDiagnosticTargetDatabaseDismounted;
				}
			}
			catch (MapiPermanentException exception2)
			{
				return Strings.MapiTransactionDiagnosticStoreStateCheckFailure(SearchCommon.ShortErrorMsgFromException(exception2));
			}
			catch (MapiRetryableException exception3)
			{
				return Strings.MapiTransactionDiagnosticStoreStateCheckFailure(SearchCommon.ShortErrorMsgFromException(exception3));
			}
			return SearchCommon.ShortErrorMsgFromException(exception);
		}

		// Token: 0x04000628 RID: 1576
		internal const uint INDEX_PARTIAL = 1U;
	}
}
