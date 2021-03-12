using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.PublicFolder;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.PublicFolder
{
	// Token: 0x02000169 RID: 361
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class SplitOperationBase : ISplitOperation
	{
		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06000E8A RID: 3722 RVA: 0x00057432 File Offset: 0x00055632
		protected virtual int MaxRetryCount
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x06000E8B RID: 3723 RVA: 0x00057438 File Offset: 0x00055638
		internal SplitOperationBase(string name, IPublicFolderSession publicFolderSession, IPublicFolderSplitState splitState, IPublicFolderMailboxLoggerBase logger, IAssistantRunspaceFactory powershellFactory, ISplitOperationState splitOperationState, SplitProgressState startState, SplitProgressState completionState)
		{
			this.name = name;
			this.publicFolderSession = publicFolderSession;
			this.splitState = splitState;
			this.logger = logger;
			this.powershellFactory = powershellFactory;
			this.startState = startState;
			this.completionState = completionState;
			this.splitOperationState = splitOperationState;
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06000E8C RID: 3724 RVA: 0x00057488 File Offset: 0x00055688
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06000E8D RID: 3725 RVA: 0x00057490 File Offset: 0x00055690
		public ISplitOperationState OperationState
		{
			get
			{
				return this.splitOperationState;
			}
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06000E8E RID: 3726 RVA: 0x00057498 File Offset: 0x00055698
		public IPublicFolderSession CurrentPublicFolderSession
		{
			get
			{
				return this.publicFolderSession;
			}
		}

		// Token: 0x06000E8F RID: 3727 RVA: 0x000574A0 File Offset: 0x000556A0
		public void Invoke()
		{
			this.StartInvoke();
			this.InvokeInternal();
			this.EndInvoke();
		}

		// Token: 0x06000E90 RID: 3728
		protected abstract void InvokeInternal();

		// Token: 0x06000E91 RID: 3729 RVA: 0x000574B4 File Offset: 0x000556B4
		private void StartInvoke()
		{
			if (this.splitState.ProgressState != this.startState)
			{
				this.splitState.ProgressState = this.startState;
				this.splitOperationState.StartTime = DateTime.UtcNow;
			}
			ISplitOperationState splitOperationState = this.splitOperationState;
			splitOperationState.RetryCount += 1;
			this.splitOperationState.Error = null;
			this.splitOperationState.ErrorDetails = null;
			this.splitOperationState.PartialStep = false;
		}

		// Token: 0x06000E92 RID: 3730 RVA: 0x00057530 File Offset: 0x00055730
		private void EndInvoke()
		{
			bool flag = false;
			if (this.splitOperationState.PartialStep)
			{
				ISplitOperationState splitOperationState = this.splitOperationState;
				splitOperationState.PartialStepCount += 1;
			}
			if (this.splitOperationState.Error == null)
			{
				if (this.splitOperationState.PartialStep)
				{
					this.splitOperationState.RetryCount = 0;
					this.logger.LogEvent(LogEventType.Verbose, string.Format("SplitOperationBase::{0}::EndInvoke - Operation {1} successfully executed partial step number {2}.", this.GetHashCode(), this.name, this.splitOperationState.PartialStepCount));
				}
				else
				{
					flag = true;
					this.logger.LogEvent(LogEventType.Verbose, string.Format("SplitOperationBase::{0}::EndInvoke - Operation {1} successfully completed.", this.GetHashCode(), this.name));
				}
			}
			else if (this.splitOperationState.Error is TransientException)
			{
				if ((int)this.splitOperationState.RetryCount < this.MaxRetryCount)
				{
					this.logger.LogEvent(LogEventType.Verbose, string.Format("SplitOperationBase::{0}::EndInvoke - Operation {1} executed with retryable transient error {2}. OptionalErrorDetails: {3}", new object[]
					{
						this.GetHashCode(),
						this.name,
						this.splitOperationState.Error,
						this.splitOperationState.ErrorDetails
					}));
				}
				else
				{
					flag = true;
					this.logger.LogEvent(LogEventType.Verbose, string.Format("SplitOperationBase::{0}::EndInvoke - Operation {1} completed all retries for transient error {2}. OptionalErrorDetails: {3}", new object[]
					{
						this.GetHashCode(),
						this.name,
						this.splitOperationState.Error,
						this.splitOperationState.ErrorDetails
					}));
				}
			}
			else
			{
				flag = true;
				this.logger.LogEvent(LogEventType.Verbose, string.Format("SplitOperationBase::{0}::EndInvoke - Operation {1} completed with permanent failure {2}. OptionalErrorDetails: {3}", new object[]
				{
					this.GetHashCode(),
					this.name,
					this.splitOperationState.Error,
					this.splitOperationState.ErrorDetails
				}));
			}
			if (flag)
			{
				this.splitOperationState.CompletedTime = DateTime.UtcNow;
				this.splitState.ProgressState = this.completionState;
			}
		}

		// Token: 0x04000949 RID: 2377
		private const int MaxRetryCountConst = 3;

		// Token: 0x0400094A RID: 2378
		protected IPublicFolderSplitState splitState;

		// Token: 0x0400094B RID: 2379
		private readonly IPublicFolderSession publicFolderSession;

		// Token: 0x0400094C RID: 2380
		protected readonly ISplitOperationState splitOperationState;

		// Token: 0x0400094D RID: 2381
		protected readonly IPublicFolderMailboxLoggerBase logger;

		// Token: 0x0400094E RID: 2382
		protected readonly IAssistantRunspaceFactory powershellFactory;

		// Token: 0x0400094F RID: 2383
		protected readonly SplitProgressState startState;

		// Token: 0x04000950 RID: 2384
		protected readonly SplitProgressState completionState;

		// Token: 0x04000951 RID: 2385
		protected readonly string name;
	}
}
