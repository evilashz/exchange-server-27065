using System;
using System.ServiceModel;

namespace Microsoft.Exchange.HA.SupportApi
{
	// Token: 0x0200037D RID: 893
	[ServiceContract(Namespace = "http://Microsoft.Exchange.HA.SupportApi")]
	public interface IInternalSupportApi
	{
		// Token: 0x060023C8 RID: 9160
		[OperationContract]
		void DisconnectCopier(Guid dbGuid);

		// Token: 0x060023C9 RID: 9161
		[OperationContract]
		void ConnectCopier(Guid dbGuid);

		// Token: 0x060023CA RID: 9162
		[OperationContract]
		void SetFailedAndSuspended(Guid dbGuid, bool fSuspendCopy, uint errorEventId, string failedMsg);

		// Token: 0x060023CB RID: 9163
		[OperationContract]
		void TriggerShutdownSwitchover();

		// Token: 0x060023CC RID: 9164
		[OperationContract]
		void IgnoreGranularCompletions(Guid dbGuid);

		// Token: 0x060023CD RID: 9165
		[OperationContract]
		bool Ping();

		// Token: 0x060023CE RID: 9166
		[OperationContract]
		void ReloadRegistryParameters();

		// Token: 0x060023CF RID: 9167
		[OperationContract]
		void TriggerLogSourceCorruption(Guid dbGuid, bool granular, bool granularRepairFails, int countOfLogsBeforeCorruption);

		// Token: 0x060023D0 RID: 9168
		[OperationContract]
		void SetCopyProperty(Guid dbGuid, string propName, string propVal);

		// Token: 0x060023D1 RID: 9169
		[OperationContract]
		void TriggerConfigUpdater();

		// Token: 0x060023D2 RID: 9170
		[OperationContract]
		void TriggerDumpster(Guid dbGuid, DateTime inspectorTime);

		// Token: 0x060023D3 RID: 9171
		[OperationContract]
		void TriggerDumpsterEx(Guid dbGuid, bool fTriggerSafetyNet, DateTime failoverTimeUtc, DateTime startTimeUtc, DateTime endTimeUtc, long lastLogGenBeforeActivation, long numLogsLost);

		// Token: 0x060023D4 RID: 9172
		[OperationContract]
		void DoDumpsterRedeliveryIfRequired(Guid dbGuid);

		// Token: 0x060023D5 RID: 9173
		[OperationContract]
		void TriggerServerLocatorRestart();

		// Token: 0x060023D6 RID: 9174
		[OperationContract]
		void TriggerTruncation(Guid dbGuid);
	}
}
