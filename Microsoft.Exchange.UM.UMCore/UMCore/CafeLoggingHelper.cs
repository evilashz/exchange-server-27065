using System;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200024C RID: 588
	internal class CafeLoggingHelper
	{
		// Token: 0x06001140 RID: 4416 RVA: 0x0004CDEC File Offset: 0x0004AFEC
		internal static void LogCallStatistics(CafeRoutingContext cafeRoutingContext)
		{
			CafeCallStatisticsLogger.CafeCallStatisticsLogRow cafeCallStatisticsLogRow = new CafeCallStatisticsLogger.CafeCallStatisticsLogRow();
			cafeCallStatisticsLogRow.CallStartTime = cafeRoutingContext.CallReceivedTime.UniversalTime;
			cafeCallStatisticsLogRow.CallLatency = cafeRoutingContext.CallLatency;
			cafeCallStatisticsLogRow.CallType = string.Empty;
			cafeCallStatisticsLogRow.CallIdentity = Utils.CheckString(cafeRoutingContext.CallId);
			cafeCallStatisticsLogRow.CafeServerName = Utils.GetLocalHostName();
			cafeCallStatisticsLogRow.DialPlanGuid = CafeLoggingHelper.GetDialPlanId(cafeRoutingContext);
			cafeCallStatisticsLogRow.DialPlanType = CafeLoggingHelper.GetDialPlanType(cafeRoutingContext);
			cafeCallStatisticsLogRow.CalledPhoneNumber = CafeLoggingHelper.GetCalledPhoneNumber(cafeRoutingContext);
			cafeCallStatisticsLogRow.CallerPhoneNumber = CafeLoggingHelper.GetCallerPhoneNumber(cafeRoutingContext);
			cafeCallStatisticsLogRow.OfferResult = CafeLoggingHelper.GetOfferResultDescription(cafeRoutingContext);
			cafeCallStatisticsLogRow.OrganizationId = Util.GetTenantName(cafeRoutingContext.DialPlan);
			CafeCallStatisticsLogger.Instance.Append(cafeCallStatisticsLogRow);
		}

		// Token: 0x06001141 RID: 4417 RVA: 0x0004CE9E File Offset: 0x0004B09E
		private static string GetCalledPhoneNumber(CafeRoutingContext cafeRoutingContext)
		{
			if (cafeRoutingContext.CalledParty != null)
			{
				return cafeRoutingContext.CalledParty.ToString();
			}
			return string.Empty;
		}

		// Token: 0x06001142 RID: 4418 RVA: 0x0004CEB9 File Offset: 0x0004B0B9
		private static string GetCallerPhoneNumber(CafeRoutingContext cafeRoutingContext)
		{
			if (cafeRoutingContext.CallingParty != null)
			{
				return cafeRoutingContext.CallingParty.ToString();
			}
			return string.Empty;
		}

		// Token: 0x06001143 RID: 4419 RVA: 0x0004CED4 File Offset: 0x0004B0D4
		private static Guid GetDialPlanId(CafeRoutingContext cafeRoutingContext)
		{
			if (cafeRoutingContext.DialPlan != null)
			{
				return cafeRoutingContext.DialPlan.Guid;
			}
			return Guid.Empty;
		}

		// Token: 0x06001144 RID: 4420 RVA: 0x0004CEEF File Offset: 0x0004B0EF
		private static string GetDialPlanType(CafeRoutingContext cafeRoutingContext)
		{
			if (cafeRoutingContext.DialPlan != null)
			{
				return cafeRoutingContext.DialPlan.URIType.ToString();
			}
			return string.Empty;
		}

		// Token: 0x06001145 RID: 4421 RVA: 0x0004CF14 File Offset: 0x0004B114
		private static string GetOfferResultDescription(CafeRoutingContext cafeRoutingContext)
		{
			if (cafeRoutingContext.RedirectUri != null)
			{
				return "Redirect";
			}
			return "Reject";
		}
	}
}
