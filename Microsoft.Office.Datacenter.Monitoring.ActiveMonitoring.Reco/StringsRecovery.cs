using System;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery
{
	// Token: 0x0200004F RID: 79
	internal static class StringsRecovery
	{
		// Token: 0x06000342 RID: 834 RVA: 0x0000B7DC File Offset: 0x000099DC
		public static LocalizedString ArbitrationQuotaCalculationFailed(int exhaustedQuota, int allowedQuota, bool isConcluded, bool isInvokedTooSoon)
		{
			return new LocalizedString("ArbitrationQuotaCalculationFailed", StringsRecovery.ResourceManager, new object[]
			{
				exhaustedQuota,
				allowedQuota,
				isConcluded,
				isInvokedTooSoon
			});
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0000B824 File Offset: 0x00009A24
		public static LocalizedString RecoveryActionExceptionCommon(string recoveryActionMsg)
		{
			return new LocalizedString("RecoveryActionExceptionCommon", StringsRecovery.ResourceManager, new object[]
			{
				recoveryActionMsg
			});
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000B84C File Offset: 0x00009A4C
		public static LocalizedString ArbitrationMinimumRequiredReadyNotSatisfied(int totalReady, int minimumRequired)
		{
			return new LocalizedString("ArbitrationMinimumRequiredReadyNotSatisfied", StringsRecovery.ResourceManager, new object[]
			{
				totalReady,
				minimumRequired
			});
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0000B884 File Offset: 0x00009A84
		public static LocalizedString ThrottlingInProgressException(long instanceId, string actionId, string resourceName, string currentRequester, string inProgressRequester, DateTime operationStartTime, DateTime expectedEndTime)
		{
			return new LocalizedString("ThrottlingInProgressException", StringsRecovery.ResourceManager, new object[]
			{
				instanceId,
				actionId,
				resourceName,
				currentRequester,
				inProgressRequester,
				operationStartTime,
				expectedEndTime
			});
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000B8D8 File Offset: 0x00009AD8
		public static LocalizedString ThrottlingOverlapException(long currentInstanceId, long overlapInstanceId, string currentRequester, string overlapRequester, DateTime currentStartTime, DateTime overlapStartTime)
		{
			return new LocalizedString("ThrottlingOverlapException", StringsRecovery.ResourceManager, new object[]
			{
				currentInstanceId,
				overlapInstanceId,
				currentRequester,
				overlapRequester,
				currentStartTime,
				overlapStartTime
			});
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000B92C File Offset: 0x00009B2C
		public static LocalizedString ThrottlingRejectedOperationException(string rejectedOperationMsg)
		{
			return new LocalizedString("ThrottlingRejectedOperationException", StringsRecovery.ResourceManager, new object[]
			{
				rejectedOperationMsg
			});
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000B954 File Offset: 0x00009B54
		public static LocalizedString GroupThrottlingRejectedOperation(string actionId, string resourceName, string requester, string failedChecks)
		{
			return new LocalizedString("GroupThrottlingRejectedOperation", StringsRecovery.ResourceManager, new object[]
			{
				actionId,
				resourceName,
				requester,
				failedChecks
			});
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000B988 File Offset: 0x00009B88
		public static LocalizedString LocalThrottlingRejectedOperation(string actionId, string resourceName, string requester, string failedChecks)
		{
			return new LocalizedString("LocalThrottlingRejectedOperation", StringsRecovery.ResourceManager, new object[]
			{
				actionId,
				resourceName,
				requester,
				failedChecks
			});
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000B9BC File Offset: 0x00009BBC
		public static LocalizedString ArbitrationExceptionCommon(string arbitrationMsg)
		{
			return new LocalizedString("ArbitrationExceptionCommon", StringsRecovery.ResourceManager, new object[]
			{
				arbitrationMsg
			});
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0000B9E4 File Offset: 0x00009BE4
		public static LocalizedString ArbitrationMaximumAllowedConcurrentNotSatisfied(int totalReady, int minimumRequired)
		{
			return new LocalizedString("ArbitrationMaximumAllowedConcurrentNotSatisfied", StringsRecovery.ResourceManager, new object[]
			{
				totalReady,
				minimumRequired
			});
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000BA1C File Offset: 0x00009C1C
		public static LocalizedString DumpFreeSpaceRequirementNotSatisfied(string directory, double available, double required)
		{
			return new LocalizedString("DumpFreeSpaceRequirementNotSatisfied", StringsRecovery.ResourceManager, new object[]
			{
				directory,
				available,
				required
			});
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000BA58 File Offset: 0x00009C58
		public static LocalizedString DistributedThrottlingRejectedOperation(string actionId, string requester)
		{
			return new LocalizedString("DistributedThrottlingRejectedOperation", StringsRecovery.ResourceManager, new object[]
			{
				actionId,
				requester
			});
		}

		// Token: 0x040001F9 RID: 505
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery.StringsRecovery", typeof(StringsRecovery).GetTypeInfo().Assembly);

		// Token: 0x02000050 RID: 80
		private enum ParamIDs
		{
			// Token: 0x040001FB RID: 507
			ArbitrationQuotaCalculationFailed,
			// Token: 0x040001FC RID: 508
			RecoveryActionExceptionCommon,
			// Token: 0x040001FD RID: 509
			ArbitrationMinimumRequiredReadyNotSatisfied,
			// Token: 0x040001FE RID: 510
			ThrottlingInProgressException,
			// Token: 0x040001FF RID: 511
			ThrottlingOverlapException,
			// Token: 0x04000200 RID: 512
			ThrottlingRejectedOperationException,
			// Token: 0x04000201 RID: 513
			GroupThrottlingRejectedOperation,
			// Token: 0x04000202 RID: 514
			LocalThrottlingRejectedOperation,
			// Token: 0x04000203 RID: 515
			ArbitrationExceptionCommon,
			// Token: 0x04000204 RID: 516
			ArbitrationMaximumAllowedConcurrentNotSatisfied,
			// Token: 0x04000205 RID: 517
			DumpFreeSpaceRequirementNotSatisfied,
			// Token: 0x04000206 RID: 518
			DistributedThrottlingRejectedOperation
		}
	}
}
