using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.UnifiedGroups;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000094 RID: 148
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class GroupsLogger : IGroupsLogger
	{
		// Token: 0x0600038C RID: 908 RVA: 0x00011AD0 File Offset: 0x0000FCD0
		public GroupsLogger(GroupTaskName taskName, Guid activityId)
		{
			ArgumentValidator.ThrowIfNull("activityId", activityId);
			this.taskName = taskName;
			this.activityId = activityId;
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x0600038D RID: 909 RVA: 0x00011AF6 File Offset: 0x0000FCF6
		// (set) Token: 0x0600038E RID: 910 RVA: 0x00011AFE File Offset: 0x0000FCFE
		public Enum CurrentAction { get; set; }

		// Token: 0x0600038F RID: 911 RVA: 0x00011B08 File Offset: 0x0000FD08
		public void LogTrace(string formatString, params object[] args)
		{
			string text = string.Format(formatString, args);
			GroupsLogger.Tracer.TraceDebug((long)this.GetHashCode(), "ActivityId={0}. TaskName={1}. CurrentAction={2}. Message={3}.", new object[]
			{
				this.activityId,
				this.taskName,
				this.CurrentAction,
				text
			});
			FederatedDirectoryLogger.AppendToLog(new SchemaBasedLogEvent<FederatedDirectoryLogSchema.TraceTag>
			{
				{
					FederatedDirectoryLogSchema.TraceTag.TaskName,
					this.taskName
				},
				{
					FederatedDirectoryLogSchema.TraceTag.ActivityId,
					this.activityId
				},
				{
					FederatedDirectoryLogSchema.TraceTag.CurrentAction,
					this.CurrentAction
				},
				{
					FederatedDirectoryLogSchema.TraceTag.Message,
					text
				}
			});
		}

		// Token: 0x06000390 RID: 912 RVA: 0x00011BAC File Offset: 0x0000FDAC
		public void LogException(Exception exception, string formatString, params object[] args)
		{
			string text = string.Format(formatString, args);
			GroupsLogger.Tracer.TraceError((long)this.GetHashCode(), "ActivityId={0}. TaskName={1}. CurrentAction={2}. Message={3}. Exception={4}", new object[]
			{
				this.activityId,
				this.taskName,
				this.CurrentAction,
				text,
				exception
			});
			FederatedDirectoryLogger.AppendToLog(new SchemaBasedLogEvent<FederatedDirectoryLogSchema.ExceptionTag>
			{
				{
					FederatedDirectoryLogSchema.ExceptionTag.TaskName,
					this.taskName
				},
				{
					FederatedDirectoryLogSchema.ExceptionTag.ActivityId,
					this.activityId
				},
				{
					FederatedDirectoryLogSchema.ExceptionTag.ExceptionType,
					exception.GetType()
				},
				{
					FederatedDirectoryLogSchema.ExceptionTag.ExceptionDetail,
					exception
				},
				{
					FederatedDirectoryLogSchema.ExceptionTag.CurrentAction,
					this.CurrentAction
				},
				{
					FederatedDirectoryLogSchema.ExceptionTag.Message,
					text
				}
			});
		}

		// Token: 0x04000603 RID: 1539
		private static readonly Trace Tracer = ExTraceGlobals.ModernGroupsTracer;

		// Token: 0x04000604 RID: 1540
		private readonly GroupTaskName taskName;

		// Token: 0x04000605 RID: 1541
		private readonly Guid activityId;
	}
}
