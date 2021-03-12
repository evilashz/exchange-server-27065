using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000556 RID: 1366
	internal class ReplicationCheckResultInfo
	{
		// Token: 0x060030AF RID: 12463 RVA: 0x000C4B16 File Offset: 0x000C2D16
		public ReplicationCheckResultInfo(ReplicationCheck check)
		{
			this.m_check = check;
			this.m_WarningMessages = new List<MessageInfo>();
			this.m_FailedMessages = new List<MessageInfo>();
			this.m_PassedMessages = new List<MessageInfo>();
		}

		// Token: 0x17000E74 RID: 3700
		// (get) Token: 0x060030B0 RID: 12464 RVA: 0x000C4B46 File Offset: 0x000C2D46
		// (set) Token: 0x060030B1 RID: 12465 RVA: 0x000C4B4E File Offset: 0x000C2D4E
		public bool HasTransientError
		{
			get
			{
				return this.m_hasTransientError;
			}
			set
			{
				this.m_hasTransientError = value;
			}
		}

		// Token: 0x17000E75 RID: 3701
		// (get) Token: 0x060030B2 RID: 12466 RVA: 0x000C4B57 File Offset: 0x000C2D57
		// (set) Token: 0x060030B3 RID: 12467 RVA: 0x000C4B5F File Offset: 0x000C2D5F
		public bool HasRun
		{
			get
			{
				return this.m_hasRun;
			}
			set
			{
				this.m_hasRun = value;
			}
		}

		// Token: 0x17000E76 RID: 3702
		// (get) Token: 0x060030B4 RID: 12468 RVA: 0x000C4B68 File Offset: 0x000C2D68
		public bool HasPassed
		{
			get
			{
				return this.m_hasPassed;
			}
		}

		// Token: 0x17000E77 RID: 3703
		// (get) Token: 0x060030B5 RID: 12469 RVA: 0x000C4B70 File Offset: 0x000C2D70
		public bool HasFailures
		{
			get
			{
				return this.NumberOfFailures > 0;
			}
		}

		// Token: 0x17000E78 RID: 3704
		// (get) Token: 0x060030B6 RID: 12470 RVA: 0x000C4B7B File Offset: 0x000C2D7B
		public bool HasWarnings
		{
			get
			{
				return this.NumberOfWarnings > 0;
			}
		}

		// Token: 0x17000E79 RID: 3705
		// (get) Token: 0x060030B7 RID: 12471 RVA: 0x000C4B86 File Offset: 0x000C2D86
		public bool HasPasses
		{
			get
			{
				return this.NumberOfPasses > 0;
			}
		}

		// Token: 0x17000E7A RID: 3706
		// (get) Token: 0x060030B8 RID: 12472 RVA: 0x000C4B91 File Offset: 0x000C2D91
		public int NumberOfFailures
		{
			get
			{
				return this.m_FailedMessages.Count;
			}
		}

		// Token: 0x17000E7B RID: 3707
		// (get) Token: 0x060030B9 RID: 12473 RVA: 0x000C4B9E File Offset: 0x000C2D9E
		public int NumberOfWarnings
		{
			get
			{
				return this.m_WarningMessages.Count;
			}
		}

		// Token: 0x17000E7C RID: 3708
		// (get) Token: 0x060030BA RID: 12474 RVA: 0x000C4BAB File Offset: 0x000C2DAB
		public int NumberOfPasses
		{
			get
			{
				return this.m_PassedMessages.Count;
			}
		}

		// Token: 0x17000E7D RID: 3709
		// (get) Token: 0x060030BB RID: 12475 RVA: 0x000C4BB8 File Offset: 0x000C2DB8
		public int TotalNumberOfRecords
		{
			get
			{
				return this.NumberOfFailures + this.NumberOfWarnings + this.NumberOfPasses;
			}
		}

		// Token: 0x060030BC RID: 12476 RVA: 0x000C4BD0 File Offset: 0x000C2DD0
		public void AddFailure(string identity, string message, bool isTransitioningState)
		{
			this.AddFailure(identity, message, false, null, isTransitioningState);
		}

		// Token: 0x060030BD RID: 12477 RVA: 0x000C4BF0 File Offset: 0x000C2DF0
		public void AddFailure(string identity, string message, bool isException, uint? dbFailureEventId, bool isTransitioningState)
		{
			this.m_FailedMessages.Add(new MessageInfo(this.m_check.Title, identity, message, isException, dbFailureEventId, isTransitioningState));
		}

		// Token: 0x060030BE RID: 12478 RVA: 0x000C4C14 File Offset: 0x000C2E14
		public void AddWarning(string identity, string message, bool isTransitioningState)
		{
			this.m_WarningMessages.Add(new MessageInfo(this.m_check.Title, identity, message, false, null, isTransitioningState));
		}

		// Token: 0x060030BF RID: 12479 RVA: 0x000C4C4C File Offset: 0x000C2E4C
		public void AddSuccess(string identity, bool isTransitioningState)
		{
			this.m_PassedMessages.Add(new MessageInfo(this.m_check.Title, identity, string.Empty, false, null, isTransitioningState));
		}

		// Token: 0x060030C0 RID: 12480 RVA: 0x000C4C85 File Offset: 0x000C2E85
		public void SetPassed()
		{
			this.SetPassed(null, false);
		}

		// Token: 0x060030C1 RID: 12481 RVA: 0x000C4C90 File Offset: 0x000C2E90
		public void SetPassed(string identity, bool isTransitioningState)
		{
			if (!this.m_hasPassed)
			{
				if (!string.IsNullOrEmpty(identity) && this.NumberOfPasses == 0)
				{
					this.AddSuccess(identity, isTransitioningState);
					ExTraceGlobals.HealthChecksTracer.TraceDebug<string>((long)this.GetHashCode(), "Check {0} is recording a pass entry for overall check.", this.m_check.Title);
				}
				this.m_hasPassed = true;
			}
		}

		// Token: 0x060030C2 RID: 12482 RVA: 0x000C4CE5 File Offset: 0x000C2EE5
		private ReplicationCheckResultEnum GetCheckResultEnum()
		{
			if (!this.m_check.HasRun)
			{
				return ReplicationCheckResultEnum.Undefined;
			}
			if (this.m_check.HasPassed)
			{
				return ReplicationCheckResultEnum.Passed;
			}
			if (this.HasFailures)
			{
				return ReplicationCheckResultEnum.Failed;
			}
			if (this.HasWarnings)
			{
				return ReplicationCheckResultEnum.Warning;
			}
			return ReplicationCheckResultEnum.Undefined;
		}

		// Token: 0x060030C3 RID: 12483 RVA: 0x000C4D1C File Offset: 0x000C2F1C
		public string BuildErrorMessageForOutcome()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.HasFailures)
			{
				if (this.HasWarnings || this.NumberOfFailures > 1)
				{
					stringBuilder.AppendLine(Strings.ReplicationCheckFailuresLabel);
				}
				foreach (MessageInfo messageInfo in this.m_FailedMessages)
				{
					string text = messageInfo.IsException ? Strings.ReplicationCheckGeneralFail(this.m_check.Title, messageInfo.Message) : messageInfo.Message;
					if (this.HasWarnings || this.NumberOfFailures > 1)
					{
						stringBuilder.AppendFormat("\t{0}{1}", text, Environment.NewLine);
					}
					else
					{
						stringBuilder.AppendLine(text);
					}
				}
			}
			if (this.HasWarnings)
			{
				if (this.HasFailures || this.NumberOfWarnings > 1)
				{
					stringBuilder.AppendLine(Strings.ReplicationCheckWarningsLabel);
				}
				foreach (MessageInfo messageInfo2 in this.m_WarningMessages)
				{
					if (this.HasFailures || this.NumberOfWarnings > 1)
					{
						stringBuilder.AppendFormat("\t{0}{1}", messageInfo2.Message, Environment.NewLine);
					}
					else
					{
						stringBuilder.AppendLine(messageInfo2.Message);
					}
				}
			}
			string text2 = stringBuilder.ToString();
			ExTraceGlobals.HealthChecksTracer.TraceDebug<string, string>((long)this.GetHashCode(), "BuildErrorMessageForOutcome(): Message after running check '{0}' is:{1}", this.m_check.Title, (!string.IsNullOrEmpty(text2)) ? text2.Replace(Environment.NewLine, "; ") : "<blank>");
			return text2;
		}

		// Token: 0x060030C4 RID: 12484 RVA: 0x000C4EE4 File Offset: 0x000C30E4
		public ReplicationCheckOutcome GetCheckOutcome()
		{
			ReplicationCheckResultEnum checkResultEnum = this.GetCheckResultEnum();
			ReplicationCheckOutcome replicationCheckOutcome = new ReplicationCheckOutcome(this.m_check);
			if (checkResultEnum == ReplicationCheckResultEnum.Passed)
			{
				replicationCheckOutcome.Update(new ReplicationCheckResult(checkResultEnum), null);
				return replicationCheckOutcome;
			}
			string newErrorMessage = this.BuildErrorMessageForOutcome();
			replicationCheckOutcome.Update(new ReplicationCheckResult(checkResultEnum), newErrorMessage);
			return replicationCheckOutcome;
		}

		// Token: 0x060030C5 RID: 12485 RVA: 0x000C4F2C File Offset: 0x000C312C
		public List<ReplicationCheckOutputObject> GetCheckOutputObjects()
		{
			int totalNumberOfRecords = this.TotalNumberOfRecords;
			List<ReplicationCheckOutputObject> list = new List<ReplicationCheckOutputObject>(totalNumberOfRecords);
			foreach (MessageInfo messageInfo in this.m_FailedMessages)
			{
				ReplicationCheckOutputObject replicationCheckOutputObject = new ReplicationCheckOutputObject(this.m_check);
				replicationCheckOutputObject.Update(messageInfo.InstanceIdentity, new ReplicationCheckResult(ReplicationCheckResultEnum.Failed), messageInfo.Message, messageInfo.DbFailureEventId);
				list.Add(replicationCheckOutputObject);
			}
			foreach (MessageInfo messageInfo2 in this.m_WarningMessages)
			{
				ReplicationCheckOutputObject replicationCheckOutputObject = new ReplicationCheckOutputObject(this.m_check);
				replicationCheckOutputObject.Update(messageInfo2.InstanceIdentity, new ReplicationCheckResult(ReplicationCheckResultEnum.Warning), messageInfo2.Message, messageInfo2.DbFailureEventId);
				list.Add(replicationCheckOutputObject);
			}
			foreach (MessageInfo messageInfo3 in this.m_PassedMessages)
			{
				ReplicationCheckOutputObject replicationCheckOutputObject = new ReplicationCheckOutputObject(this.m_check);
				replicationCheckOutputObject.Update(messageInfo3.InstanceIdentity, new ReplicationCheckResult(ReplicationCheckResultEnum.Passed), string.Empty, null);
				list.Add(replicationCheckOutputObject);
			}
			return list;
		}

		// Token: 0x060030C6 RID: 12486 RVA: 0x000C509C File Offset: 0x000C329C
		public void LogEvents(IEventManager eventManager)
		{
			if (this.HasFailures)
			{
				eventManager.LogEvents(this.m_check.CheckId, ReplicationCheckResultEnum.Failed, this.m_FailedMessages);
			}
			if (this.HasWarnings)
			{
				eventManager.LogEvents(this.m_check.CheckId, ReplicationCheckResultEnum.Warning, this.m_WarningMessages);
			}
			if (this.HasPasses)
			{
				eventManager.LogEvents(this.m_check.CheckId, ReplicationCheckResultEnum.Passed, this.m_PassedMessages);
			}
		}

		// Token: 0x0400228E RID: 8846
		private readonly ReplicationCheck m_check;

		// Token: 0x0400228F RID: 8847
		private List<MessageInfo> m_WarningMessages;

		// Token: 0x04002290 RID: 8848
		private List<MessageInfo> m_FailedMessages;

		// Token: 0x04002291 RID: 8849
		private List<MessageInfo> m_PassedMessages;

		// Token: 0x04002292 RID: 8850
		private bool m_hasTransientError;

		// Token: 0x04002293 RID: 8851
		private bool m_hasRun;

		// Token: 0x04002294 RID: 8852
		private bool m_hasPassed;
	}
}
