using System;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000084 RID: 132
	internal sealed class NotificationManagerHandler : ExchangeDiagnosableWrapper<NotificationManagerResult>
	{
		// Token: 0x170002AE RID: 686
		// (get) Token: 0x060006E7 RID: 1767 RVA: 0x00026776 File Offset: 0x00024976
		protected override string UsageText
		{
			get
			{
				return "The Notification Manager handler is a static diagnostics handler that dumps information about the notification manager and cache for EAS running processes.  \r\n                        It is implemented by the NotificationManagerHandler class, responds to the NotificationManager component (method) in Get-ExchangeDiagnosticInfo and returns NotificationManagerResults. \r\n                        Below are examples for using this diagnostics handler: ";
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x060006E8 RID: 1768 RVA: 0x0002677D File Offset: 0x0002497D
		protected override string UsageSample
		{
			get
			{
				return "Example 1: Metadata only call\r\n                            Get-ExchangeDiagnosticInfo –Process MSExchangeSyncAppPool –Component NotificationManager\r\n                                            \r\n                            Example 2: Dump cache call\r\n                            Get-ExchangeDiagnosticInfo –Process MSExchangeSyncAppPool –Component NotificationManager –Argument “dumpcache” ";
			}
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x00026784 File Offset: 0x00024984
		public static NotificationManagerHandler GetInstance()
		{
			if (NotificationManagerHandler.instance == null)
			{
				lock (NotificationManagerHandler.lockObject)
				{
					if (NotificationManagerHandler.instance == null)
					{
						NotificationManagerHandler.instance = new NotificationManagerHandler();
					}
				}
			}
			return NotificationManagerHandler.instance;
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x000267DC File Offset: 0x000249DC
		private NotificationManagerHandler()
		{
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x060006EB RID: 1771 RVA: 0x000267E4 File Offset: 0x000249E4
		protected override string ComponentName
		{
			get
			{
				return "NotificationManager";
			}
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x000267EC File Offset: 0x000249EC
		internal override NotificationManagerResult GetExchangeDiagnosticsInfoData(DiagnosableParameters argument)
		{
			string argument2;
			CallType callType = DiagnosticsHelper.GetCallType(argument.Argument, out argument2);
			return NotificationManager.GetDiagnosticInfo(callType, argument2);
		}

		// Token: 0x040004CA RID: 1226
		private static NotificationManagerHandler instance;

		// Token: 0x040004CB RID: 1227
		private static object lockObject = new object();
	}
}
