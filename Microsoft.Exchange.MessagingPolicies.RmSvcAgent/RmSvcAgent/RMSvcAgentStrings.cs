using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MessagingPolicies.RmSvcAgent
{
	// Token: 0x02000006 RID: 6
	public static class RMSvcAgentStrings
	{
		// Token: 0x06000018 RID: 24 RVA: 0x00002CB0 File Offset: 0x00000EB0
		static RMSvcAgentStrings()
		{
			RMSvcAgentStrings.stringIDs.Add(2348409902U, "FailedToDetectMultiTenancy");
			RMSvcAgentStrings.stringIDs.Add(670208566U, "RmsAdName");
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002D14 File Offset: 0x00000F14
		public static LocalizedString RmsErrorTextFor401(string tenantId, string componentName, string objectName, string rmsName, string status)
		{
			return new LocalizedString("RmsErrorTextFor401", "Ex9D8436", false, true, RMSvcAgentStrings.ResourceManager, new object[]
			{
				tenantId,
				componentName,
				objectName,
				rmsName,
				status
			});
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002D54 File Offset: 0x00000F54
		public static LocalizedString RmsErrorTextFor403(string tenantId, string componentName, string objectName, string rmsName, string status)
		{
			return new LocalizedString("RmsErrorTextFor403", "Ex554C43", false, true, RMSvcAgentStrings.ResourceManager, new object[]
			{
				tenantId,
				componentName,
				objectName,
				rmsName,
				status
			});
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002D94 File Offset: 0x00000F94
		public static LocalizedString RmsErrorTextForNoServerPL(string messageId)
		{
			return new LocalizedString("RmsErrorTextForNoServerPL", "Ex5D5C04", false, true, RMSvcAgentStrings.ResourceManager, new object[]
			{
				messageId
			});
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002DC4 File Offset: 0x00000FC4
		public static LocalizedString RmsErrorTextForDefer(string messageId)
		{
			return new LocalizedString("RmsErrorTextForDefer", "Ex72ECEA", false, true, RMSvcAgentStrings.ResourceManager, new object[]
			{
				messageId
			});
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002DF4 File Offset: 0x00000FF4
		public static LocalizedString RmsErrorTextForNDR(string messageId)
		{
			return new LocalizedString("RmsErrorTextForNDR", "Ex8816EC", false, true, RMSvcAgentStrings.ResourceManager, new object[]
			{
				messageId
			});
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002E24 File Offset: 0x00001024
		public static LocalizedString TemplateDoesNotExist(Guid templateId)
		{
			return new LocalizedString("TemplateDoesNotExist", "Ex028EEA", false, true, RMSvcAgentStrings.ResourceManager, new object[]
			{
				templateId
			});
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002E58 File Offset: 0x00001058
		public static LocalizedString FailedToDetectMultiTenancy
		{
			get
			{
				return new LocalizedString("FailedToDetectMultiTenancy", "", false, false, RMSvcAgentStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002E78 File Offset: 0x00001078
		public static LocalizedString RmsErrorTextForNoPL(string messageId)
		{
			return new LocalizedString("RmsErrorTextForNoPL", "ExE9F0BF", false, true, RMSvcAgentStrings.ResourceManager, new object[]
			{
				messageId
			});
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002EA8 File Offset: 0x000010A8
		public static LocalizedString RmsErrorTextForConnectFailure(string tenantId, string componentName, string objectName, string rmsName, string status)
		{
			return new LocalizedString("RmsErrorTextForConnectFailure", "Ex1B2160", false, true, RMSvcAgentStrings.ResourceManager, new object[]
			{
				tenantId,
				componentName,
				objectName,
				rmsName,
				status
			});
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002EE8 File Offset: 0x000010E8
		public static LocalizedString RmsAdName
		{
			get
			{
				return new LocalizedString("RmsAdName", "Ex048E70", false, true, RMSvcAgentStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002F08 File Offset: 0x00001108
		public static LocalizedString RmsErrorTextFor404(string tenantId, string componentName, string objectName, string rmsName, string status)
		{
			return new LocalizedString("RmsErrorTextFor404", "ExB39734", false, true, RMSvcAgentStrings.ResourceManager, new object[]
			{
				tenantId,
				componentName,
				objectName,
				rmsName,
				status
			});
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002F48 File Offset: 0x00001148
		public static LocalizedString RmsErrorTextForNoRightException(string tenantId, string componentName, string objectName, string rmsName)
		{
			return new LocalizedString("RmsErrorTextForNoRightException", "Ex899654", false, true, RMSvcAgentStrings.ResourceManager, new object[]
			{
				tenantId,
				componentName,
				objectName,
				rmsName
			});
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002F84 File Offset: 0x00001184
		public static LocalizedString RmsErrorTextForDeferJR(string messageId)
		{
			return new LocalizedString("RmsErrorTextForDeferJR", "Ex62AD80", false, true, RMSvcAgentStrings.ResourceManager, new object[]
			{
				messageId
			});
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002FB4 File Offset: 0x000011B4
		public static LocalizedString RmsErrorTextForNoJR(string messageId)
		{
			return new LocalizedString("RmsErrorTextForNoJR", "ExA1F2BE", false, true, RMSvcAgentStrings.ResourceManager, new object[]
			{
				messageId
			});
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002FE4 File Offset: 0x000011E4
		public static LocalizedString RmsErrorTextForGeneralException(string tenantId, string componentName, string exceptionName)
		{
			return new LocalizedString("RmsErrorTextForGeneralException", "Ex58C567", false, true, RMSvcAgentStrings.ResourceManager, new object[]
			{
				tenantId,
				componentName,
				exceptionName
			});
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000301C File Offset: 0x0000121C
		public static LocalizedString RmsErrorTextForTrustFailure(string tenantId, string componentName, string objectName, string rmsName, string status)
		{
			return new LocalizedString("RmsErrorTextForTrustFailure", "Ex2C5B99", false, true, RMSvcAgentStrings.ResourceManager, new object[]
			{
				tenantId,
				componentName,
				objectName,
				rmsName,
				status
			});
		}

		// Token: 0x06000029 RID: 41 RVA: 0x0000305C File Offset: 0x0000125C
		public static LocalizedString RmsErrorTextForSpecialException(string tenantId, string componentName, string objectName, string rmsName, string exceptionName)
		{
			return new LocalizedString("RmsErrorTextForSpecialException", "Ex28AEC3", false, true, RMSvcAgentStrings.ResourceManager, new object[]
			{
				tenantId,
				componentName,
				objectName,
				rmsName,
				exceptionName
			});
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000309C File Offset: 0x0000129C
		public static LocalizedString FailedToAcquireTenantLicenses(Guid tenantId)
		{
			return new LocalizedString("FailedToAcquireTenantLicenses", "ExEEC3BC", false, true, RMSvcAgentStrings.ResourceManager, new object[]
			{
				tenantId
			});
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000030D0 File Offset: 0x000012D0
		public static LocalizedString RmsAdNameWithUrl(string url)
		{
			return new LocalizedString("RmsAdNameWithUrl", "ExE24BDD", false, true, RMSvcAgentStrings.ResourceManager, new object[]
			{
				url
			});
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000030FF File Offset: 0x000012FF
		public static LocalizedString GetLocalizedString(RMSvcAgentStrings.IDs key)
		{
			return new LocalizedString(RMSvcAgentStrings.stringIDs[(uint)key], RMSvcAgentStrings.ResourceManager, new object[0]);
		}

		// Token: 0x04000015 RID: 21
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(2);

		// Token: 0x04000016 RID: 22
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.MessagingPolicies.RmSvcAgent.RMSvcAgentStrings", typeof(RMSvcAgentStrings).GetTypeInfo().Assembly);

		// Token: 0x02000007 RID: 7
		public enum IDs : uint
		{
			// Token: 0x04000018 RID: 24
			FailedToDetectMultiTenancy = 2348409902U,
			// Token: 0x04000019 RID: 25
			RmsAdName = 670208566U
		}

		// Token: 0x02000008 RID: 8
		private enum ParamIDs
		{
			// Token: 0x0400001B RID: 27
			RmsErrorTextFor401,
			// Token: 0x0400001C RID: 28
			RmsErrorTextFor403,
			// Token: 0x0400001D RID: 29
			RmsErrorTextForNoServerPL,
			// Token: 0x0400001E RID: 30
			RmsErrorTextForDefer,
			// Token: 0x0400001F RID: 31
			RmsErrorTextForNDR,
			// Token: 0x04000020 RID: 32
			TemplateDoesNotExist,
			// Token: 0x04000021 RID: 33
			RmsErrorTextForNoPL,
			// Token: 0x04000022 RID: 34
			RmsErrorTextForConnectFailure,
			// Token: 0x04000023 RID: 35
			RmsErrorTextFor404,
			// Token: 0x04000024 RID: 36
			RmsErrorTextForNoRightException,
			// Token: 0x04000025 RID: 37
			RmsErrorTextForDeferJR,
			// Token: 0x04000026 RID: 38
			RmsErrorTextForNoJR,
			// Token: 0x04000027 RID: 39
			RmsErrorTextForGeneralException,
			// Token: 0x04000028 RID: 40
			RmsErrorTextForTrustFailure,
			// Token: 0x04000029 RID: 41
			RmsErrorTextForSpecialException,
			// Token: 0x0400002A RID: 42
			FailedToAcquireTenantLicenses,
			// Token: 0x0400002B RID: 43
			RmsAdNameWithUrl
		}
	}
}
