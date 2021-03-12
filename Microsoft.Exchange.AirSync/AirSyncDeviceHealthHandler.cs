using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000073 RID: 115
	internal sealed class AirSyncDeviceHealthHandler : ExchangeDiagnosableWrapper<List<AirSyncDeviceHealth>>
	{
		// Token: 0x1700026A RID: 618
		// (get) Token: 0x0600063E RID: 1598 RVA: 0x00025026 File Offset: 0x00023226
		protected override string UsageText
		{
			get
			{
				return "This diagnostics handler returns health of an EAS device or an user. The handler supports \"EmailAddress\" & \"DeviceID\" arguments. Below are examples for using this diagnostics handler: ";
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x0600063F RID: 1599 RVA: 0x0002502D File Offset: 0x0002322D
		protected override string UsageSample
		{
			get
			{
				return " Example 1: Returns health for all devices in cache\r\n                        Get-ExchangeDiagnosticInfo -Process MSExchangeSyncAppPool -Component AirSyncDeviceHealth\r\n\r\n                        Example 2: Returns health information about all devices/requests for specified user.\r\n                        Get-ExchangeDiagnosticInfo -Process MSExchangeSyncAppPool -Component AirSyncDeviceHealth -Argument \"EmailAddress=jondoe@contoso.com\";\r\n\r\n                        Example 2: Returns health information for specified device.\r\n                        Get-ExchangeDiagnosticInfo -Process MSExchangeSyncAppPool -Component AirSyncDeviceHealth -Argument \"DeviceID=WP986912973799292012\"";
			}
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x00025034 File Offset: 0x00023234
		public static AirSyncDeviceHealthHandler GetInstance()
		{
			if (AirSyncDeviceHealthHandler.instance == null)
			{
				lock (AirSyncDeviceHealthHandler.lockObject)
				{
					if (AirSyncDeviceHealthHandler.instance == null)
					{
						AirSyncDeviceHealthHandler.instance = new AirSyncDeviceHealthHandler();
					}
				}
			}
			return AirSyncDeviceHealthHandler.instance;
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x0002508C File Offset: 0x0002328C
		private AirSyncDeviceHealthHandler()
		{
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000642 RID: 1602 RVA: 0x00025094 File Offset: 0x00023294
		protected override string ComponentName
		{
			get
			{
				return "AirSyncDeviceHealth";
			}
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x000250E4 File Offset: 0x000232E4
		internal override List<AirSyncDeviceHealth> GetExchangeDiagnosticsInfoData(DiagnosableParameters arguments)
		{
			List<AirSyncDeviceHealth> list = new List<AirSyncDeviceHealth>();
			string parsedArgument;
			CallType callType = DiagnosticsHelper.GetCallType(arguments.Argument, out parsedArgument);
			List<ActiveSyncRequestData> list2;
			if (string.IsNullOrEmpty(arguments.Argument))
			{
				list2 = (from request in ActiveSyncRequestCache.Instance.Values
				where request.UserEmail != null && request.DeviceID != null
				select request).ToList<ActiveSyncRequestData>();
			}
			else if (callType == CallType.EmailAddress)
			{
				list2 = (from request in ActiveSyncRequestCache.Instance.Values
				where string.Equals(request.UserEmail, parsedArgument, StringComparison.InvariantCultureIgnoreCase)
				select request).ToList<ActiveSyncRequestData>();
			}
			else
			{
				if (callType != CallType.DeviceId)
				{
					throw new ArgumentException("Invalid value found in 'Argument' parameter. please use ? as argument to see proper usage.");
				}
				list2 = (from request in ActiveSyncRequestCache.Instance.Values
				where string.Equals(request.DeviceID, parsedArgument, StringComparison.InvariantCultureIgnoreCase)
				select request).ToList<ActiveSyncRequestData>();
			}
			foreach (ActiveSyncRequestData data in list2)
			{
				list.Add(new AirSyncDeviceHealth(data));
			}
			return list;
		}

		// Token: 0x04000472 RID: 1138
		private static AirSyncDeviceHealthHandler instance;

		// Token: 0x04000473 RID: 1139
		private static object lockObject = new object();
	}
}
