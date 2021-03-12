using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage.MailboxRules
{
	// Token: 0x02000BDE RID: 3038
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class FolderEvaluationResult
	{
		// Token: 0x06006BE8 RID: 27624 RVA: 0x001CEA02 File Offset: 0x001CCC02
		public FolderEvaluationResult(IRuleEvaluationContext context, List<Rule> rules)
		{
			this.context = context;
			this.targetFolder = context.CurrentFolder;
			this.hasOofRules = RuleLoader.HasOofRule(rules);
			this.ruleSet = new FolderEvaluationResult.RuleSet(this, rules);
			this.workItems = new List<WorkItem>();
		}

		// Token: 0x17001D4D RID: 7501
		// (get) Token: 0x06006BE9 RID: 27625 RVA: 0x001CEA41 File Offset: 0x001CCC41
		// (set) Token: 0x06006BEA RID: 27626 RVA: 0x001CEA49 File Offset: 0x001CCC49
		public RuleAction.Bounce.BounceCode? BounceCode
		{
			get
			{
				return this.bounceCode;
			}
			set
			{
				this.bounceCode = value;
			}
		}

		// Token: 0x17001D4E RID: 7502
		// (get) Token: 0x06006BEB RID: 27627 RVA: 0x001CEA52 File Offset: 0x001CCC52
		// (set) Token: 0x06006BEC RID: 27628 RVA: 0x001CEA5A File Offset: 0x001CCC5A
		public bool ExitExecution
		{
			get
			{
				return this.exitExecution;
			}
			set
			{
				this.exitExecution = value;
			}
		}

		// Token: 0x17001D4F RID: 7503
		// (get) Token: 0x06006BED RID: 27629 RVA: 0x001CEA64 File Offset: 0x001CCC64
		public bool HasDeferredMoveOrCopy
		{
			get
			{
				foreach (WorkItem workItem in this.workItems)
				{
					DeferredActionWorkItem deferredActionWorkItem = workItem as DeferredActionWorkItem;
					if (deferredActionWorkItem != null && deferredActionWorkItem.HasMoveOrCopy)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x17001D50 RID: 7504
		// (get) Token: 0x06006BEE RID: 27630 RVA: 0x001CEACC File Offset: 0x001CCCCC
		public bool IsMessageDelegated
		{
			get
			{
				return (this.status & FolderEvaluationResult.FolderEvaluationStatus.IsMessageDelegated) != (FolderEvaluationResult.FolderEvaluationStatus)0;
			}
		}

		// Token: 0x17001D51 RID: 7505
		// (get) Token: 0x06006BEF RID: 27631 RVA: 0x001CEADC File Offset: 0x001CCCDC
		// (set) Token: 0x06006BF0 RID: 27632 RVA: 0x001CEAEC File Offset: 0x001CCCEC
		public bool IsMessageMoved
		{
			get
			{
				return (this.status & FolderEvaluationResult.FolderEvaluationStatus.IsMessageMoved) != (FolderEvaluationResult.FolderEvaluationStatus)0;
			}
			set
			{
				if (value)
				{
					this.status |= FolderEvaluationResult.FolderEvaluationStatus.IsMessageMoved;
					return;
				}
				this.status &= ~FolderEvaluationResult.FolderEvaluationStatus.IsMessageMoved;
			}
		}

		// Token: 0x17001D52 RID: 7506
		// (get) Token: 0x06006BF1 RID: 27633 RVA: 0x001CEB0F File Offset: 0x001CCD0F
		public bool HasOofRules
		{
			get
			{
				return this.hasOofRules;
			}
		}

		// Token: 0x17001D53 RID: 7507
		// (get) Token: 0x06006BF2 RID: 27634 RVA: 0x001CEB17 File Offset: 0x001CCD17
		public IEnumerable<Rule> Rules
		{
			get
			{
				return this.ruleSet;
			}
		}

		// Token: 0x17001D54 RID: 7508
		// (get) Token: 0x06006BF3 RID: 27635 RVA: 0x001CEB20 File Offset: 0x001CCD20
		public bool ShouldContinue
		{
			get
			{
				return this.BounceCode == null && (!this.ExitExecution || this.HasOofRules);
			}
		}

		// Token: 0x17001D55 RID: 7509
		// (get) Token: 0x06006BF4 RID: 27636 RVA: 0x001CEB4F File Offset: 0x001CCD4F
		// (set) Token: 0x06006BF5 RID: 27637 RVA: 0x001CEB57 File Offset: 0x001CCD57
		public Folder TargetFolder
		{
			get
			{
				return this.targetFolder;
			}
			set
			{
				this.targetFolder = value;
			}
		}

		// Token: 0x17001D56 RID: 7510
		// (get) Token: 0x06006BF6 RID: 27638 RVA: 0x001CEB60 File Offset: 0x001CCD60
		internal IList<WorkItem> WorkItems
		{
			get
			{
				return new ReadOnlyCollection<WorkItem>(this.workItems);
			}
		}

		// Token: 0x06006BF7 RID: 27639 RVA: 0x001CEB6D File Offset: 0x001CCD6D
		public void AddWorkItem(WorkItem workItem)
		{
			this.workItems.Add(workItem);
			if (workItem is DelegateWorkItem)
			{
				this.status |= FolderEvaluationResult.FolderEvaluationStatus.IsMessageDelegated;
			}
		}

		// Token: 0x06006BF8 RID: 27640 RVA: 0x001CEB91 File Offset: 0x001CCD91
		public void ClearWorkItems()
		{
			this.workItems.Clear();
		}

		// Token: 0x06006BF9 RID: 27641 RVA: 0x001CEBA0 File Offset: 0x001CCDA0
		public void Execute(ExecutionStage stage)
		{
			foreach (WorkItem workItem in this.workItems)
			{
				if ((stage & workItem.Stage) != (ExecutionStage)0)
				{
					string stageDescription = FolderEvaluationResult.GetStageDescription(stage);
					try
					{
						this.context.CurrentRule = workItem.Rule;
						RuleUtil.FaultInjection((FaultInjectionLid)4175834429U);
						workItem.Execute();
						this.context.LogWorkItemExecution(workItem);
					}
					catch (InvalidRuleException exception)
					{
						this.context.RecordError(exception, stageDescription);
						this.context.DisableAndMarkRuleInError(workItem.Rule, workItem.Rule.Actions[workItem.ActionIndex].ActionType, workItem.ActionIndex, DeferredError.RuleError.Execution);
					}
					catch (StoragePermanentException exception2)
					{
						this.context.RecordError(exception2, stageDescription);
					}
					catch (ExchangeDataException exception3)
					{
						this.context.RecordError(exception3, stageDescription);
					}
					catch (MapiPermanentException exception4)
					{
						this.context.RecordError(exception4, stageDescription);
					}
					catch (DataValidationException exception5)
					{
						this.context.RecordError(exception5, stageDescription);
					}
					finally
					{
						this.context.CurrentRule = null;
					}
				}
			}
		}

		// Token: 0x06006BFA RID: 27642 RVA: 0x001CED18 File Offset: 0x001CCF18
		public DeferredActionWorkItem GetDeferredActionWorkItem(string provider)
		{
			foreach (WorkItem workItem in this.workItems)
			{
				DeferredActionWorkItem deferredActionWorkItem = workItem as DeferredActionWorkItem;
				if (deferredActionWorkItem != null && string.Equals(deferredActionWorkItem.Provider, provider, StringComparison.OrdinalIgnoreCase))
				{
					return deferredActionWorkItem;
				}
			}
			return null;
		}

		// Token: 0x06006BFB RID: 27643 RVA: 0x001CED84 File Offset: 0x001CCF84
		public bool HasCopyWorkItemTo(StoreId folderId)
		{
			foreach (WorkItem workItem in this.workItems)
			{
				CopyWorkItem copyWorkItem = workItem as CopyWorkItem;
				if (copyWorkItem != null && copyWorkItem.TargetFolderId.Equals(folderId))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06006BFC RID: 27644 RVA: 0x001CEDF0 File Offset: 0x001CCFF0
		private static string GetStageDescription(ExecutionStage stage)
		{
			string result = null;
			if (stage == ExecutionStage.OnPromotedMessage)
			{
				result = ServerStrings.FolderRuleStageOnPromotedMessage;
			}
			else if (stage == ExecutionStage.OnCreatedMessage)
			{
				result = ServerStrings.FolderRuleStageOnCreatedMessage;
			}
			else if (stage == ExecutionStage.OnDeliveredMessage)
			{
				result = ServerStrings.FolderRuleStageOnDeliveredMessage;
			}
			else if (stage == ExecutionStage.OnPublicFolderAfter)
			{
				result = ServerStrings.FolderRuleStageOnPublicFolderAfter;
			}
			else if (stage == ExecutionStage.OnPublicFolderBefore)
			{
				result = ServerStrings.FolderRuleStageOnPublicFolderBefore;
			}
			return result;
		}

		// Token: 0x04003DB5 RID: 15797
		private RuleAction.Bounce.BounceCode? bounceCode;

		// Token: 0x04003DB6 RID: 15798
		private IRuleEvaluationContext context;

		// Token: 0x04003DB7 RID: 15799
		private bool exitExecution;

		// Token: 0x04003DB8 RID: 15800
		private bool hasOofRules;

		// Token: 0x04003DB9 RID: 15801
		private FolderEvaluationResult.RuleSet ruleSet;

		// Token: 0x04003DBA RID: 15802
		private FolderEvaluationResult.FolderEvaluationStatus status;

		// Token: 0x04003DBB RID: 15803
		private Folder targetFolder;

		// Token: 0x04003DBC RID: 15804
		private List<WorkItem> workItems;

		// Token: 0x02000BDF RID: 3039
		[Flags]
		private enum FolderEvaluationStatus
		{
			// Token: 0x04003DBE RID: 15806
			IsMessageDelegated = 1,
			// Token: 0x04003DBF RID: 15807
			IsMessageMoved = 2
		}

		// Token: 0x02000BE0 RID: 3040
		private class RuleSet : IEnumerable<Rule>, IEnumerable
		{
			// Token: 0x06006BFD RID: 27645 RVA: 0x001CEE54 File Offset: 0x001CD054
			public RuleSet(FolderEvaluationResult evaluationResult, List<Rule> rules)
			{
				this.evaluationResult = evaluationResult;
				this.rules = rules;
			}

			// Token: 0x06006BFE RID: 27646 RVA: 0x001CEFE8 File Offset: 0x001CD1E8
			public IEnumerator<Rule> GetEnumerator()
			{
				foreach (Rule rule in this.rules)
				{
					if (this.evaluationResult.ExitExecution)
					{
						if (RuleLoader.IsOofRule(rule))
						{
							yield return rule;
						}
					}
					else
					{
						yield return rule;
					}
				}
				yield break;
			}

			// Token: 0x06006BFF RID: 27647 RVA: 0x001CF004 File Offset: 0x001CD204
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x04003DC0 RID: 15808
			private FolderEvaluationResult evaluationResult;

			// Token: 0x04003DC1 RID: 15809
			private List<Rule> rules;
		}
	}
}
