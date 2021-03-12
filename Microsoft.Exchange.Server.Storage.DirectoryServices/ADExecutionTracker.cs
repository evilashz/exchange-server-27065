using System;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.DirectoryServices
{
	// Token: 0x02000010 RID: 16
	public class ADExecutionTracker
	{
		// Token: 0x06000053 RID: 83 RVA: 0x00002436 File Offset: 0x00000636
		public static void Initialize()
		{
			ADExecutionTracker.adOperationTimeoutDefinition = new ThreadManager.TimeoutDefinition(ConfigurationSchema.ADOperationTimeout.Value, new Action<ThreadManager.ThreadInfo>(ADExecutionTracker.CrashOnTimeout));
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002458 File Offset: 0x00000658
		public static ADExecutionTracker.DirectoryExecutionTrackingFrame TrackCall(IExecutionContext context, string operationName)
		{
			return new ADExecutionTracker.DirectoryExecutionTrackingFrame(context, operationName);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002461 File Offset: 0x00000661
		private static void CrashOnTimeout(ThreadManager.ThreadInfo threadInfo)
		{
			throw new InvalidOperationException(string.Format("Possible hang detected. Operation: {0}. Client: {2}. MailboxGuid: {3}", threadInfo.MethodName, threadInfo.Client, threadInfo.MailboxGuid));
		}

		// Token: 0x0400000F RID: 15
		private static ThreadManager.TimeoutDefinition adOperationTimeoutDefinition;

		// Token: 0x02000011 RID: 17
		public struct DirectoryExecutionTrackingFrame : IDisposable
		{
			// Token: 0x06000057 RID: 87 RVA: 0x00002494 File Offset: 0x00000694
			internal DirectoryExecutionTrackingFrame(IExecutionContext context, string operationName)
			{
				ADExecutionTracker.OperationExecutionTrackableWrapper<string> operation = new ADExecutionTracker.OperationExecutionTrackableWrapper<string>(operationName);
				this.operationData = context.RecordOperation<ExecutionDiagnostics.DirectoryTrackingData>(operation);
				if (this.operationData != null)
				{
					this.operationData.Count++;
				}
				this.startTimeStamp = StopwatchStamp.GetStamp();
				this.threadManagerMethodFrame = ThreadManager.NewMethodFrame(operationName, ADExecutionTracker.adOperationTimeoutDefinition);
			}

			// Token: 0x06000058 RID: 88 RVA: 0x000024EC File Offset: 0x000006EC
			public void Dispose()
			{
				if (this.operationData != null)
				{
					this.operationData.ExecutionTime += this.startTimeStamp.ElapsedTime;
				}
				this.threadManagerMethodFrame.Dispose();
			}

			// Token: 0x04000010 RID: 16
			private ExecutionDiagnostics.DirectoryTrackingData operationData;

			// Token: 0x04000011 RID: 17
			private StopwatchStamp startTimeStamp;

			// Token: 0x04000012 RID: 18
			private ThreadManager.MethodFrame threadManagerMethodFrame;
		}

		// Token: 0x02000012 RID: 18
		internal class OperationExecutionTrackableWrapper<TOperation> : IOperationExecutionTrackable, IOperationExecutionTrackingKey
		{
			// Token: 0x06000059 RID: 89 RVA: 0x00002522 File Offset: 0x00000722
			internal OperationExecutionTrackableWrapper(TOperation operation)
			{
				this.operation = operation;
			}

			// Token: 0x0600005A RID: 90 RVA: 0x00002531 File Offset: 0x00000731
			IOperationExecutionTrackingKey IOperationExecutionTrackable.GetTrackingKey()
			{
				return this;
			}

			// Token: 0x0600005B RID: 91 RVA: 0x00002534 File Offset: 0x00000734
			int IOperationExecutionTrackingKey.GetTrackingKeyHashValue()
			{
				return this.operation.GetHashCode();
			}

			// Token: 0x0600005C RID: 92 RVA: 0x00002547 File Offset: 0x00000747
			public int GetSimpleHashValue()
			{
				return this.operation.GetHashCode();
			}

			// Token: 0x0600005D RID: 93 RVA: 0x0000255A File Offset: 0x0000075A
			bool IOperationExecutionTrackingKey.IsTrackingKeyEqualTo(IOperationExecutionTrackingKey other)
			{
				return other != null && other is ADExecutionTracker.OperationExecutionTrackableWrapper<TOperation> && this.operation.Equals(((ADExecutionTracker.OperationExecutionTrackableWrapper<TOperation>)other).operation);
			}

			// Token: 0x0600005E RID: 94 RVA: 0x0000258A File Offset: 0x0000078A
			string IOperationExecutionTrackingKey.TrackingKeyToString()
			{
				return this.operation.ToString();
			}

			// Token: 0x04000013 RID: 19
			private TOperation operation;
		}
	}
}
