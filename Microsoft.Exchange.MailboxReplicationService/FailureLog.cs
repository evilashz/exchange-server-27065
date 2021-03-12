using System;
using Microsoft.Exchange.Data.ConfigurationSettings;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200007D RID: 125
	internal class FailureLog : ObjectLog<FailureData>
	{
		// Token: 0x0600056A RID: 1386 RVA: 0x0001F04F File Offset: 0x0001D24F
		private FailureLog() : base(new FailureLog.FailureLogSchema(), new SimpleObjectLogConfiguration("Failure", "FailureLogEnabled", "FailureLogMaxDirSize", "FailureLogMaxFileSize"))
		{
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x0001F075 File Offset: 0x0001D275
		public static string GetDataContextToPersist(string dataContext)
		{
			if (dataContext.Length > 1000)
			{
				dataContext = dataContext.Substring(0, 997) + "...";
			}
			return dataContext;
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x0001F0A0 File Offset: 0x0001D2A0
		public static void Write(Guid requestGuid, Exception failure, bool isFatal, RequestState requestState, SyncStage syncStage, string folderName = null, string operationType = null)
		{
			FailureLog.WriteInternal(requestGuid, failure, isFatal, requestState, syncStage, folderName, operationType, Guid.NewGuid(), 0);
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x0001F0C4 File Offset: 0x0001D2C4
		private static void WriteInternal(Guid requestGuid, Exception failure, bool isFatal, RequestState requestState, SyncStage syncStage, string folderName, string operationType, Guid failureGuid, int failureLevel)
		{
			FailureData objectToLog = default(FailureData);
			objectToLog.FailureGuid = failureGuid;
			objectToLog.RequestGuid = requestGuid;
			objectToLog.Failure = failure;
			objectToLog.FailureLevel = failureLevel;
			objectToLog.IsFatal = isFatal;
			objectToLog.RequestState = requestState;
			objectToLog.SyncStage = syncStage;
			objectToLog.FolderName = folderName;
			objectToLog.OperationType = operationType;
			if (objectToLog.OperationType == null && objectToLog.Failure != null)
			{
				string dataContext = ExecutionContext.GetDataContext(failure);
				objectToLog.OperationType = FailureLog.GetDataContextToPersist(dataContext);
			}
			GenericSettingsContext genericSettingsContext = new GenericSettingsContext("FailureType", CommonUtils.GetFailureType(failure), null);
			using (genericSettingsContext.Activate())
			{
				if (ConfigBase<MRSConfigSchema>.GetConfig<bool>("SendGenericWatson"))
				{
					string watsonHash;
					CommonUtils.SendGenericWatson(failure, CommonUtils.FullFailureMessageWithCallStack(failure, 5), out watsonHash);
					objectToLog.WatsonHash = watsonHash;
				}
				else
				{
					objectToLog.WatsonHash = CommonUtils.ComputeCallStackHash(failure, 5);
				}
			}
			FailureLog.instance.LogObject(objectToLog);
			if (failure.InnerException != null)
			{
				FailureLog.WriteInternal(requestGuid, failure.InnerException, isFatal, requestState, syncStage, folderName, operationType, failureGuid, failureLevel + 1);
			}
		}

		// Token: 0x04000248 RID: 584
		public const int MaxDataContextLength = 1000;

		// Token: 0x04000249 RID: 585
		private static FailureLog instance = new FailureLog();

		// Token: 0x0200007E RID: 126
		private class FailureLogSchema : ObjectLogSchema
		{
			// Token: 0x17000154 RID: 340
			// (get) Token: 0x0600056F RID: 1391 RVA: 0x0001F1F0 File Offset: 0x0001D3F0
			public override string Software
			{
				get
				{
					return "Microsoft Exchange Mailbox Replication Service";
				}
			}

			// Token: 0x17000155 RID: 341
			// (get) Token: 0x06000570 RID: 1392 RVA: 0x0001F1F7 File Offset: 0x0001D3F7
			public override string LogType
			{
				get
				{
					return "Failure Log";
				}
			}

			// Token: 0x0400024A RID: 586
			public static readonly ObjectLogSimplePropertyDefinition<FailureData> RequestGuid = new ObjectLogSimplePropertyDefinition<FailureData>("RequestGuid", (FailureData d) => d.RequestGuid);

			// Token: 0x0400024B RID: 587
			public static readonly ObjectLogSimplePropertyDefinition<FailureData> IsFatal = new ObjectLogSimplePropertyDefinition<FailureData>("IsFatal", (FailureData d) => d.IsFatal.ToString());

			// Token: 0x0400024C RID: 588
			public static readonly ObjectLogSimplePropertyDefinition<FailureData> RequestState = new ObjectLogSimplePropertyDefinition<FailureData>("RequestState", (FailureData d) => d.RequestState.ToString());

			// Token: 0x0400024D RID: 589
			public static readonly ObjectLogSimplePropertyDefinition<FailureData> SyncStage = new ObjectLogSimplePropertyDefinition<FailureData>("SyncStage", (FailureData d) => d.SyncStage.ToString());

			// Token: 0x0400024E RID: 590
			public static readonly ObjectLogSimplePropertyDefinition<FailureData> FailureSide = new ObjectLogSimplePropertyDefinition<FailureData>("FailureSide", (FailureData d) => (CommonUtils.GetExceptionSide(d.Failure) ?? ExceptionSide.None).ToString());

			// Token: 0x0400024F RID: 591
			public static readonly ObjectLogSimplePropertyDefinition<FailureData> FailureType = new ObjectLogSimplePropertyDefinition<FailureData>("FailureType", (FailureData d) => CommonUtils.GetFailureType(d.Failure));

			// Token: 0x04000250 RID: 592
			public static readonly ObjectLogSimplePropertyDefinition<FailureData> FailureCode = new ObjectLogSimplePropertyDefinition<FailureData>("FailureCode", (FailureData d) => CommonUtils.HrFromException(d.Failure));

			// Token: 0x04000251 RID: 593
			public static readonly ObjectLogSimplePropertyDefinition<FailureData> MapiLowLevelError = new ObjectLogSimplePropertyDefinition<FailureData>("MapiLowLevelError", (FailureData d) => CommonUtils.GetMapiLowLevelError(d.Failure));

			// Token: 0x04000252 RID: 594
			public static readonly ObjectLogSimplePropertyDefinition<FailureData> FolderName = new ObjectLogSimplePropertyDefinition<FailureData>("FolderName", (FailureData d) => d.FolderName);

			// Token: 0x04000253 RID: 595
			public static readonly ObjectLogSimplePropertyDefinition<FailureData> OperationType = new ObjectLogSimplePropertyDefinition<FailureData>("OperationType", (FailureData d) => d.OperationType);

			// Token: 0x04000254 RID: 596
			public static readonly ObjectLogSimplePropertyDefinition<FailureData> WatsonHash = new ObjectLogSimplePropertyDefinition<FailureData>("WatsonHash", (FailureData d) => d.WatsonHash);

			// Token: 0x04000255 RID: 597
			public static readonly ObjectLogSimplePropertyDefinition<FailureData> StackTrace = new ObjectLogSimplePropertyDefinition<FailureData>("StackTrace", (FailureData d) => CommonUtils.FullFailureMessageWithCallStack(d.Failure, 5));

			// Token: 0x04000256 RID: 598
			public static readonly ObjectLogSimplePropertyDefinition<FailureData> AppVersion = new ObjectLogSimplePropertyDefinition<FailureData>("AppVersion", (FailureData d) => ExWatson.RealApplicationVersion.ToString());

			// Token: 0x04000257 RID: 599
			public static readonly ObjectLogSimplePropertyDefinition<FailureData> FailureGuid = new ObjectLogSimplePropertyDefinition<FailureData>("FailureGuid", (FailureData d) => d.FailureGuid);

			// Token: 0x04000258 RID: 600
			public static readonly ObjectLogSimplePropertyDefinition<FailureData> FailureLevel = new ObjectLogSimplePropertyDefinition<FailureData>("FailureLevel", (FailureData d) => d.FailureLevel);
		}
	}
}
