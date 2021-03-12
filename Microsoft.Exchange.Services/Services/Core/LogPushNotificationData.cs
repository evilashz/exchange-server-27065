using System;
using System.Collections.Generic;
using Microsoft.Exchange.PushNotifications.CrimsonEvents;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000347 RID: 839
	internal class LogPushNotificationData : SingleStepServiceCommand<LogPushNotificationDataRequest, ServiceResultNone>
	{
		// Token: 0x060017AC RID: 6060 RVA: 0x0007EBA7 File Offset: 0x0007CDA7
		internal LogPushNotificationData(CallContext callContext, LogPushNotificationDataRequest request) : base(callContext, request)
		{
		}

		// Token: 0x060017AD RID: 6061 RVA: 0x0007EBB4 File Offset: 0x0007CDB4
		internal override ServiceResult<ServiceResultNone> Execute()
		{
			this.ValidateRequest();
			Dictionary<string, string> dictionary = this.CreateDictionary();
			string owaDeviceId = base.CallContext.OwaDeviceId;
			string text = base.MailboxIdentityMailboxSession.MailboxGuid.ToString();
			if (string.Compare(base.Request.DataType, "BackgroundPushNotificationEvent", StringComparison.OrdinalIgnoreCase) == 0)
			{
				if (string.Compare(dictionary["BackgroundPushNotificationResult"], "Succeed", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(dictionary["BackgroundPushNotificationResult"], "SyncSucceededWithNoItem", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(dictionary["BackgroundPushNotificationResult"], "RichMailNotificationIsNotEnabled", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(dictionary["BackgroundPushNotificationResult"], "ForegroundNotification", StringComparison.OrdinalIgnoreCase) == 0)
				{
					PushNotificationsCrimsonEvents.BackgroundPushNotificationSuccess.Log<string, string, string, string, string, string, string, string, string, string, string>(base.Request.AppId, dictionary["ClientId"], dictionary["ServerId"], owaDeviceId, dictionary["StartTime"], text, dictionary["DeviceModel"], dictionary["PalVersion"], dictionary["OwaVersion"], dictionary["BackgroundPushNotificationResult"], dictionary["BackgroundSyncResult"]);
				}
				else
				{
					PushNotificationsCrimsonEvents.BackgroundPushNotificationWarning.LogPeriodic<string, string, string, string, string, string, string, string, string, string, string>(owaDeviceId + dictionary["BackgroundPushNotificationResult"], TimeSpan.FromMinutes(4.0), base.Request.AppId, dictionary["ClientId"], dictionary["ServerId"], owaDeviceId, dictionary["StartTime"], text, dictionary["DeviceModel"], dictionary["PalVersion"], dictionary["OwaVersion"], dictionary["BackgroundPushNotificationResult"], dictionary["BackgroundSyncResult"]);
				}
			}
			else if (string.Compare(base.Request.DataType, PushNotificationsCrimsonEvents.ContinuousRegistrationError.GetType().Name, StringComparison.OrdinalIgnoreCase) == 0)
			{
				PushNotificationsCrimsonEvents.ContinuousRegistrationError.LogPeriodic<string, string, string, string>(owaDeviceId, TimeSpan.FromMinutes(1440.0), base.Request.AppId, owaDeviceId, text, dictionary["Error"]);
			}
			else
			{
				if (string.Compare(base.Request.DataType, PushNotificationsCrimsonEvents.RegistrationError.GetType().Name, StringComparison.OrdinalIgnoreCase) != 0)
				{
					throw new InvalidRequestException();
				}
				PushNotificationsCrimsonEvents.RegistrationError.LogPeriodic<string, string, string, string>(owaDeviceId, TimeSpan.FromMinutes(1440.0), base.Request.AppId, owaDeviceId, text, dictionary["Error"]);
			}
			return new ServiceResult<ServiceResultNone>(new ServiceResultNone());
		}

		// Token: 0x060017AE RID: 6062 RVA: 0x0007EE34 File Offset: 0x0007D034
		internal override IExchangeWebMethodResponse GetResponse()
		{
			LogPushNotificationDataResponse logPushNotificationDataResponse = new LogPushNotificationDataResponse();
			logPushNotificationDataResponse.ProcessServiceResult(base.Result);
			return logPushNotificationDataResponse;
		}

		// Token: 0x060017AF RID: 6063 RVA: 0x0007EE54 File Offset: 0x0007D054
		private void ValidateRequest()
		{
			if (string.IsNullOrEmpty(base.Request.AppId) || string.IsNullOrEmpty(base.Request.DataType))
			{
				throw new InvalidRequestException();
			}
			if (base.Request.KeyValuePairs == null || base.Request.KeyValuePairs.Length % 2 == 1)
			{
				throw new InvalidRequestException();
			}
		}

		// Token: 0x060017B0 RID: 6064 RVA: 0x0007EEB0 File Offset: 0x0007D0B0
		private Dictionary<string, string> CreateDictionary()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			for (int i = 0; i < base.Request.KeyValuePairs.Length; i += 2)
			{
				dictionary.Add(base.Request.KeyValuePairs[i], base.Request.KeyValuePairs[i + 1]);
			}
			return dictionary;
		}

		// Token: 0x04000FE2 RID: 4066
		private const int LogPeriodicSuppressionInMinutes = 1440;

		// Token: 0x04000FE3 RID: 4067
		private const string ErrorKey = "Error";

		// Token: 0x04000FE4 RID: 4068
		private const string ServerIdName = "ServerId";

		// Token: 0x04000FE5 RID: 4069
		private const string BackgroundPushNotificationResultName = "BackgroundPushNotificationResult";

		// Token: 0x04000FE6 RID: 4070
		private const string BackgroundSyncResultName = "BackgroundSyncResult";

		// Token: 0x04000FE7 RID: 4071
		private const string StartTimeName = "StartTime";

		// Token: 0x04000FE8 RID: 4072
		private const string DeviceModelName = "DeviceModel";

		// Token: 0x04000FE9 RID: 4073
		private const string PalVersionName = "PalVersion";

		// Token: 0x04000FEA RID: 4074
		private const string OwaVersionName = "OwaVersion";

		// Token: 0x04000FEB RID: 4075
		private const string ClientIdName = "ClientId";

		// Token: 0x04000FEC RID: 4076
		private const string BackgroundPushNotificationEventName = "BackgroundPushNotificationEvent";
	}
}
