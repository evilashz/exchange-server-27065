using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.Office.CompliancePolicy.ComplianceData;
using Microsoft.Office.CompliancePolicy.Dar;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;
using Microsoft.Office.CompliancePolicy.PolicySync;

namespace Microsoft.Office.CompliancePolicy.ComplianceTask
{
	// Token: 0x0200005A RID: 90
	public class BindingTask : ComplianceTask
	{
		// Token: 0x0600024A RID: 586 RVA: 0x00006F9D File Offset: 0x0000519D
		public BindingTask()
		{
			this.Category = DarTaskCategory.Medium;
			base.TaskRetryTotalCount = 5;
			base.TaskRetryInterval = new TimeSpan(0, 0, 135);
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600024B RID: 587 RVA: 0x00006FC5 File Offset: 0x000051C5
		public override string TaskType
		{
			get
			{
				return "Common.BindingApplication";
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600024C RID: 588 RVA: 0x00006FCC File Offset: 0x000051CC
		// (set) Token: 0x0600024D RID: 589 RVA: 0x00006FD9 File Offset: 0x000051D9
		public override string WorkloadData
		{
			get
			{
				return this.Bindings.WorkloadData;
			}
			set
			{
				this.Bindings.WorkloadData = value;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600024E RID: 590 RVA: 0x00006FE8 File Offset: 0x000051E8
		public override string CorrelationId
		{
			get
			{
				if (this.Bindings.TriggerObjectStatus != null && this.Bindings.TriggerObjectStatus.Version != null)
				{
					return this.Bindings.TriggerObjectStatus.Version.InternalStorage.ToString();
				}
				return base.CorrelationId;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600024F RID: 591 RVA: 0x00007044 File Offset: 0x00005244
		// (set) Token: 0x06000250 RID: 592 RVA: 0x0000705F File Offset: 0x0000525F
		[SerializableTaskData]
		public BindingTask.StoredBindings Bindings
		{
			get
			{
				if (this.storedBindings == null)
				{
					this.storedBindings = new BindingTask.StoredBindings();
				}
				return this.storedBindings;
			}
			set
			{
				this.storedBindings = value;
			}
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00007068 File Offset: 0x00005268
		public void InsertBindingToAdd(PolicyDefinitionConfig definition, PolicyRuleConfig rule, PolicyBindingConfig binding)
		{
			this.InsertBinding(this.Bindings.BindingsToAdd, definition, rule, binding);
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000707E File Offset: 0x0000527E
		public void InsertBindingToRemove(PolicyDefinitionConfig definition, PolicyRuleConfig rule, PolicyBindingConfig binding)
		{
			this.InsertBinding(this.Bindings.BindingsToRemove, definition, rule, binding);
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00007094 File Offset: 0x00005294
		public void SetTriggerObject(PolicyConfigBase configObject, Guid? policyId, ConfigurationObjectType type, Mode mode)
		{
			if (this.Bindings.TriggerObjectStatus == null)
			{
				UnifiedPolicyStatus policyStatusFromPolicyConfig = this.GetPolicyStatusFromPolicyConfig(configObject, policyId, type, mode);
				this.Bindings.TriggerObjectStatus = policyStatusFromPolicyConfig;
				return;
			}
			throw new CompliancePolicyException("Cannot set the trigger object after status has already created");
		}

		// Token: 0x06000254 RID: 596 RVA: 0x000070D4 File Offset: 0x000052D4
		public void AddCompletedBinding(PolicyBindingConfig binding, Guid? policyId)
		{
			UnifiedPolicyStatus policyStatusFromPolicyConfig = this.GetPolicyStatusFromPolicyConfig(binding, policyId, ConfigurationObjectType.Scope, binding.Mode);
			this.Bindings.CompletedBindings[binding.Identity] = policyStatusFromPolicyConfig;
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00007108 File Offset: 0x00005308
		public IEnumerable<UnifiedPolicyStatus> GetCompletedBindings()
		{
			return this.Bindings.CompletedBindings.Values.Concat(new UnifiedPolicyStatus[]
			{
				this.Bindings.TriggerObjectStatus
			});
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00007150 File Offset: 0x00005350
		public override DarTaskExecutionResult Execute(DarTaskManager darTaskManager)
		{
			DarTaskExecutionResult darTaskExecutionResult = DarTaskExecutionResult.Yielded;
			int num = this.Bindings.BindingsToAdd.Count + this.Bindings.BindingsToRemove.Count;
			int num2 = num;
			DarTaskExecutionResult result;
			try
			{
				this.Log(darTaskManager, "Binding task executing", DarExecutionLogClientIDs.BindingTask0);
				if (this.Bindings.SkippedDueToRetryNeed >= num)
				{
					this.Bindings.SkippedDueToRetryNeed = 0;
				}
				int skippedDueToRetryNeed = this.Bindings.SkippedDueToRetryNeed;
				using (PolicyConfigProvider policyStore = this.ComplianceServiceProvider.GetPolicyStore(base.TenantId))
				{
					if (this.ForEachBindings(this.Bindings.BindingsToRemove, new Func<ComplianceItemContainer, BindingTask.StoredBinding, PolicyConfigProvider, DarTaskManager, bool>(this.RemoveBinding), darTaskManager, policyStore, ref skippedDueToRetryNeed))
					{
						this.ForEachBindings(this.Bindings.BindingsToAdd, new Func<ComplianceItemContainer, BindingTask.StoredBinding, PolicyConfigProvider, DarTaskManager, bool>(this.AddBinding), darTaskManager, policyStore, ref skippedDueToRetryNeed);
					}
				}
				num2 = this.Bindings.BindingsToAdd.Count + this.Bindings.BindingsToRemove.Count;
				if (num2 == 0)
				{
					darTaskExecutionResult = (result = (this.Bindings.CompletedBindings.Values.Any((UnifiedPolicyStatus status) => status.ErrorCode != UnifiedPolicyErrorCode.Success) ? DarTaskExecutionResult.Failed : DarTaskExecutionResult.Completed));
				}
				else if (skippedDueToRetryNeed > this.Bindings.SkippedDueToRetryNeed)
				{
					this.Bindings.SkippedDueToRetryNeed = skippedDueToRetryNeed;
					darTaskExecutionResult = (result = DarTaskExecutionResult.TransientError);
				}
				else
				{
					darTaskExecutionResult = (result = DarTaskExecutionResult.Yielded);
				}
			}
			finally
			{
				this.Log(darTaskManager, string.Format("Binding task exited with result {0}, Processed {1}/{2}, Need retry: {3}", new object[]
				{
					darTaskExecutionResult,
					num - num2,
					num,
					this.Bindings.SkippedDueToRetryNeed
				}), DarExecutionLogClientIDs.BindingTask1);
			}
			return result;
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00007334 File Offset: 0x00005534
		public override void CompleteTask(DarTaskManager darTaskManager)
		{
			try
			{
				this.PublishStatus();
			}
			catch (PolicyConfigProviderPermanentException ex)
			{
				this.Bindings.StatusPublishingError = "PolicyConfigProviderPermanentException occured: " + ex.Message;
				this.LogError(darTaskManager, ex, "Binding task failed during status publishing", DarExecutionLogClientIDs.BindingTask14);
				base.TaskState = DarTaskState.Failed;
			}
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00007390 File Offset: 0x00005590
		protected internal virtual bool ChangeAffectsWorkload(PolicyDefinitionConfig definition, ChangeType changeType, PolicyConfigProvider provider)
		{
			return definition.Scenario == PolicyScenario.Hold && (definition.IsModified(PolicyDefinitionConfigSchema.Enabled) || definition.IsModified(PolicyDefinitionConfigSchema.Mode) || changeType == ChangeType.Delete);
		}

		// Token: 0x06000259 RID: 601 RVA: 0x000073BC File Offset: 0x000055BC
		protected internal virtual bool ChangeAffectsWorkload(PolicyRuleConfig rule, ChangeType changeType, PolicyConfigProvider provider)
		{
			if (rule.IsModified(PolicyRuleConfigSchema.Enabled) || rule.IsModified(PolicyRuleConfigSchema.RuleBlob) || rule.IsModified(PolicyRuleConfigSchema.Mode) || changeType == ChangeType.Delete)
			{
				PolicyDefinitionConfig policyDefinitionConfig = provider.FindByIdentity<PolicyDefinitionConfig>(rule.PolicyDefinitionConfigId);
				if (policyDefinitionConfig != null && policyDefinitionConfig.Scenario == PolicyScenario.Hold)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00007410 File Offset: 0x00005610
		protected internal virtual bool ChangeAffectsWorkload(PolicyBindingConfig binding, ChangeType changeType, PolicyConfigProvider provider)
		{
			return false;
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00007413 File Offset: 0x00005613
		protected internal virtual bool ChangeAffectsWorkload(PolicyBindingSetConfig binding, ChangeType changeType, PolicyConfigProvider provider, out PolicyDefinitionConfig definition)
		{
			definition = null;
			if (binding.IsModified(PolicyBindingSetConfigSchema.AppliedScopes) || changeType == ChangeType.Delete)
			{
				definition = provider.FindByIdentity<PolicyDefinitionConfig>(binding.PolicyDefinitionConfigId);
				if (definition != null && definition.Scenario == PolicyScenario.Hold)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000744C File Offset: 0x0000564C
		protected override IEnumerable<Type> GetKnownTypes()
		{
			return base.GetKnownTypes().Concat(new Type[]
			{
				typeof(BindingTask.StoredBindings),
				typeof(BindingTask.StoredBinding),
				typeof(List<BindingTask.StoredBinding>),
				typeof(Dictionary<Guid, UnifiedPolicyStatus>)
			});
		}

		// Token: 0x0600025D RID: 605 RVA: 0x000074A0 File Offset: 0x000056A0
		private bool AddBinding(ComplianceItemContainer container, BindingTask.StoredBinding storedBinding, PolicyConfigProvider pcp, DarTaskManager darTaskManager)
		{
			bool result = false;
			string errorMessage = string.Empty;
			UnifiedPolicyErrorCode errorCode = UnifiedPolicyErrorCode.Success;
			if (container != null)
			{
				if (container.SupportsBinding)
				{
					PolicyDefinitionConfig policyDefinitionConfig = pcp.FindByIdentity<PolicyDefinitionConfig>(storedBinding.DefinitionId);
					if (policyDefinitionConfig != null)
					{
						PolicyRuleConfig policyRuleConfig = pcp.FindByIdentity<PolicyRuleConfig>(storedBinding.RuleId);
						if (policyRuleConfig != null)
						{
							if (storedBinding.RuleVersion == Guid.Empty || (policyRuleConfig.Version != null && policyRuleConfig.Version.CompareTo(PolicyVersion.Create(storedBinding.RuleVersion)) >= 0))
							{
								if (!(storedBinding.DefinitionVersion == Guid.Empty))
								{
									if (!(policyDefinitionConfig.Version != null) || policyDefinitionConfig.Version.CompareTo(PolicyVersion.Create(storedBinding.DefinitionVersion)) < 0)
									{
										goto IL_180;
									}
								}
								try
								{
									container.AddPolicy(policyDefinitionConfig, policyRuleConfig);
									this.Log(darTaskManager, string.Format("Policy application applied successfully.  Scope: {0}", storedBinding.Scope), DarExecutionLogClientIDs.BindingTask11);
									goto IL_24D;
								}
								catch (ComplianceTaskTransientException ex)
								{
									this.LogError(darTaskManager, ex, string.Format("Policy application failed with transient failure.  Scope: {0}", storedBinding.Scope), DarExecutionLogClientIDs.BindingTask2);
									result = true;
									errorMessage = ex.Message;
									errorCode = ex.ErrorCode;
									goto IL_24D;
								}
								catch (ComplianceTaskPermanentException ex2)
								{
									this.LogError(darTaskManager, ex2, string.Format("Policy application failed with permanent failure.  Scope: {0}", storedBinding.Scope), DarExecutionLogClientIDs.BindingTask3);
									errorMessage = ex2.Message;
									errorCode = ex2.ErrorCode;
									goto IL_24D;
								}
								catch (Exception ex3)
								{
									this.LogError(darTaskManager, ex3, string.Format("Policy application failed with unknown failure.  Scope: {0}", storedBinding.Scope), DarExecutionLogClientIDs.BindingTask13);
									result = true;
									errorMessage = ex3.Message;
									errorCode = UnifiedPolicyErrorCode.Unknown;
									goto IL_24D;
								}
							}
							IL_180:
							string text = "Policy store not sync to most recent changes";
							this.Log(darTaskManager, text, DarExecutionLogClientIDs.BindingTask4);
							result = true;
							errorMessage = text;
							errorCode = UnifiedPolicyErrorCode.InternalError;
						}
						else
						{
							string text2 = "Policy rule does not exist";
							this.Log(darTaskManager, string.Format("{0}: {1}", text2, storedBinding.RuleId), DarExecutionLogClientIDs.BindingTask5);
							result = true;
							errorMessage = text2;
							errorCode = UnifiedPolicyErrorCode.InternalError;
						}
					}
					else
					{
						string text3 = "Policy definition does not exist";
						this.Log(darTaskManager, string.Format("{0}: {1}", text3, storedBinding.DefinitionId.ToString()), DarExecutionLogClientIDs.BindingTask6);
						result = true;
						errorMessage = text3;
						errorCode = UnifiedPolicyErrorCode.InternalError;
					}
				}
				else
				{
					string text4 = "Container does not support binding so failing the application";
					this.Log(darTaskManager, text4, DarExecutionLogClientIDs.BindingTask7);
					errorMessage = text4;
					errorCode = UnifiedPolicyErrorCode.InternalError;
				}
			}
			else
			{
				this.Log(darTaskManager, string.Format("{0}: {1}", "Policy container does not exist", storedBinding.Scope), DarExecutionLogClientIDs.BindingTask8);
				result = true;
				errorMessage = "Policy container does not exist";
				errorCode = UnifiedPolicyErrorCode.FailedToOpenContainer;
			}
			IL_24D:
			this.HandleCompletedBinding(storedBinding, errorMessage, errorCode);
			return result;
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000772C File Offset: 0x0000592C
		private bool RemoveBinding(ComplianceItemContainer container, BindingTask.StoredBinding storedBinding, PolicyConfigProvider pcp, DarTaskManager darTaskManager)
		{
			bool result = false;
			string errorMessage = string.Empty;
			UnifiedPolicyErrorCode errorCode = UnifiedPolicyErrorCode.Success;
			if (container != null)
			{
				try
				{
					container.RemovePolicy(storedBinding.DefinitionId, storedBinding.Scenario);
					this.Log(darTaskManager, string.Format("Policy application removed successfully.  Scope: {0}", storedBinding.Scope), DarExecutionLogClientIDs.BindingTask12);
					goto IL_C7;
				}
				catch (PolicyConfigProviderTransientException ex)
				{
					result = true;
					errorMessage = ex.Message;
					errorCode = UnifiedPolicyErrorCode.Unknown;
					goto IL_C7;
				}
				catch (PolicyConfigProviderPermanentException ex2)
				{
					errorMessage = ex2.Message;
					errorCode = UnifiedPolicyErrorCode.Unknown;
					goto IL_C7;
				}
			}
			darTaskManager.ServiceProvider.ExecutionLog.LogInformation("BindingTask", null, this.CorrelationId, string.Format("{0}: {1}", "Policy container does not exist", storedBinding.Scope), new KeyValuePair<string, object>[]
			{
				new KeyValuePair<string, object>("Tag", DarExecutionLogClientIDs.BindingTask9.ToString())
			});
			result = true;
			errorMessage = "Policy container does not exist";
			errorCode = UnifiedPolicyErrorCode.FailedToOpenContainer;
			IL_C7:
			this.HandleCompletedBinding(storedBinding, errorMessage, errorCode);
			return result;
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00007828 File Offset: 0x00005A28
		private UnifiedPolicyStatus GetPolicyStatusFromPolicyConfig(PolicyConfigBase policyConfig, Guid? policyId, ConfigurationObjectType type, Mode mode)
		{
			return new UnifiedPolicyStatus
			{
				ErrorCode = UnifiedPolicyErrorCode.Success,
				ErrorMessage = string.Empty,
				ObjectId = policyConfig.Identity,
				ParentObjectId = policyId,
				ObjectType = type,
				Version = policyConfig.Version,
				WhenProcessedUTC = DateTime.UtcNow,
				Mode = mode
			};
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00007888 File Offset: 0x00005A88
		private bool ForEachBindings(List<BindingTask.StoredBinding> bindingList, Func<ComplianceItemContainer, BindingTask.StoredBinding, PolicyConfigProvider, DarTaskManager, bool> processPolicyDelegate, DarTaskManager darTaskManager, PolicyConfigProvider pcp, ref int skippedDueToNeedToRetry)
		{
			while (skippedDueToNeedToRetry < bindingList.Count)
			{
				if (!base.ShouldContinue(darTaskManager))
				{
					return false;
				}
				BindingTask.StoredBinding storedBinding = bindingList[skippedDueToNeedToRetry];
				ComplianceItemContainer complianceItemContainer = this.ComplianceServiceProvider.GetComplianceItemContainer(base.TenantId, storedBinding.Scope);
				bool flag = processPolicyDelegate(complianceItemContainer, storedBinding, pcp, darTaskManager);
				if (flag)
				{
					skippedDueToNeedToRetry++;
				}
				else
				{
					bindingList.RemoveAt(skippedDueToNeedToRetry);
				}
			}
			return true;
		}

		// Token: 0x06000261 RID: 609 RVA: 0x000078F4 File Offset: 0x00005AF4
		private void HandleCompletedBinding(BindingTask.StoredBinding storedBinding, string errorMessage, UnifiedPolicyErrorCode errorCode)
		{
			UnifiedPolicyStatus policyStatusFromBinding = this.GetPolicyStatusFromBinding(storedBinding);
			if (!string.IsNullOrEmpty(errorMessage))
			{
				policyStatusFromBinding.ErrorMessage = errorMessage;
				policyStatusFromBinding.ErrorCode = errorCode;
			}
			if (this.Bindings.CompletedBindings.ContainsKey(policyStatusFromBinding.ObjectId))
			{
				policyStatusFromBinding.Mode = this.Bindings.CompletedBindings[policyStatusFromBinding.ObjectId].Mode;
				if (policyStatusFromBinding.Mode == Mode.PendingDeletion && policyStatusFromBinding.ErrorCode == UnifiedPolicyErrorCode.Success)
				{
					policyStatusFromBinding.Mode = Mode.Deleted;
				}
			}
			this.Bindings.CompletedBindings[policyStatusFromBinding.ObjectId] = policyStatusFromBinding;
		}

		// Token: 0x06000262 RID: 610 RVA: 0x00007988 File Offset: 0x00005B88
		private UnifiedPolicyStatus GetPolicyStatusFromBinding(BindingTask.StoredBinding binding)
		{
			return new UnifiedPolicyStatus
			{
				ObjectId = binding.BindingId,
				ObjectType = ConfigurationObjectType.Scope,
				ParentObjectId = new Guid?(binding.DefinitionId),
				Version = binding.BindingVersion,
				WhenProcessedUTC = DateTime.UtcNow,
				ErrorCode = UnifiedPolicyErrorCode.Success,
				ErrorMessage = string.Empty
			};
		}

		// Token: 0x06000263 RID: 611 RVA: 0x000079F0 File Offset: 0x00005BF0
		private void InsertBinding(List<BindingTask.StoredBinding> bindings, PolicyDefinitionConfig definition, PolicyRuleConfig rule, PolicyBindingConfig binding)
		{
			if (definition == null)
			{
				throw new ArgumentNullException("definition");
			}
			if (rule == null)
			{
				throw new ArgumentNullException("rule");
			}
			DateTime minValue = DateTime.MinValue;
			DateTime minValue2 = DateTime.MinValue;
			Guid definitionVersion = Guid.Empty;
			Guid ruleVersion = Guid.Empty;
			Guid bindingVersion = Guid.Empty;
			if (definition.Version != null)
			{
				definitionVersion = definition.Version.InternalStorage;
			}
			if (rule.Version != null)
			{
				ruleVersion = rule.Version.InternalStorage;
			}
			if (binding.Version != null)
			{
				bindingVersion = binding.Version.InternalStorage;
			}
			bindings.Add(new BindingTask.StoredBinding(definition.Identity, definitionVersion, rule.Identity, ruleVersion, binding.Identity, bindingVersion, binding.Scope.ImmutableIdentity, definition.Scenario));
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00007ABC File Offset: 0x00005CBC
		private void PublishStatus()
		{
			bool flag = this.Bindings.TriggerObjectStatus.Mode == Mode.PendingDeletion;
			bool flag2 = true;
			foreach (UnifiedPolicyStatus unifiedPolicyStatus in this.Bindings.CompletedBindings.Values)
			{
				if (unifiedPolicyStatus.ErrorCode != UnifiedPolicyErrorCode.Success)
				{
					flag2 = false;
				}
				else if (unifiedPolicyStatus.Mode == Mode.PendingDeletion)
				{
					unifiedPolicyStatus.Mode = Mode.Deleted;
				}
			}
			if (flag && flag2)
			{
				this.Bindings.TriggerObjectStatus.Mode = Mode.Deleted;
			}
			using (PolicyConfigProvider policyStore = this.ComplianceServiceProvider.GetPolicyStore(base.TenantId))
			{
				policyStore.PublishStatus(this.GetCompletedBindings());
			}
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00007B94 File Offset: 0x00005D94
		private void Log(DarTaskManager darTaskManager, string msg, DarExecutionLogClientIDs tag)
		{
			darTaskManager.ServiceProvider.ExecutionLog.LogInformation("BindingTask", null, this.CorrelationId, msg, new KeyValuePair<string, object>[]
			{
				new KeyValuePair<string, object>("Tag", tag.ToString())
			});
		}

		// Token: 0x06000266 RID: 614 RVA: 0x00007BE8 File Offset: 0x00005DE8
		private void LogError(DarTaskManager darTaskManager, Exception ex, string msg, DarExecutionLogClientIDs tag)
		{
			darTaskManager.ServiceProvider.ExecutionLog.LogError("BindingTask", null, this.CorrelationId, ex, msg, new KeyValuePair<string, object>[]
			{
				new KeyValuePair<string, object>("Tag", tag.ToString())
			});
		}

		// Token: 0x04000126 RID: 294
		private const string PolicyContainerDoesNotExist = "Policy container does not exist";

		// Token: 0x04000127 RID: 295
		private const string LoggingClientId = "BindingTask";

		// Token: 0x04000128 RID: 296
		private const int DefaultRetryCount = 5;

		// Token: 0x04000129 RID: 297
		private const int DefaultRetryIntervalInSeconds = 135;

		// Token: 0x0400012A RID: 298
		private BindingTask.StoredBindings storedBindings;

		// Token: 0x0200005B RID: 91
		[DataContract]
		public class StoredBinding
		{
			// Token: 0x06000268 RID: 616 RVA: 0x00007C40 File Offset: 0x00005E40
			public StoredBinding(Guid definitionId, Guid definitionVersion, Guid ruleId, Guid ruleVersion, Guid bindingId, Guid bindingVersion, string scope, PolicyScenario scenario)
			{
				this.DefinitionId = definitionId;
				this.DefinitionVersion = definitionVersion;
				this.RuleId = ruleId;
				this.RuleVersion = ruleVersion;
				this.BindingId = bindingId;
				this.BindingVersion = bindingVersion;
				this.Scope = scope;
				this.Scenario = scenario;
			}

			// Token: 0x170000A7 RID: 167
			// (get) Token: 0x06000269 RID: 617 RVA: 0x00007C90 File Offset: 0x00005E90
			// (set) Token: 0x0600026A RID: 618 RVA: 0x00007C98 File Offset: 0x00005E98
			[DataMember]
			public Guid DefinitionId { get; set; }

			// Token: 0x170000A8 RID: 168
			// (get) Token: 0x0600026B RID: 619 RVA: 0x00007CA1 File Offset: 0x00005EA1
			// (set) Token: 0x0600026C RID: 620 RVA: 0x00007CA9 File Offset: 0x00005EA9
			[DataMember]
			public Guid DefinitionVersion { get; set; }

			// Token: 0x170000A9 RID: 169
			// (get) Token: 0x0600026D RID: 621 RVA: 0x00007CB2 File Offset: 0x00005EB2
			// (set) Token: 0x0600026E RID: 622 RVA: 0x00007CBA File Offset: 0x00005EBA
			[DataMember]
			public Guid RuleId { get; set; }

			// Token: 0x170000AA RID: 170
			// (get) Token: 0x0600026F RID: 623 RVA: 0x00007CC3 File Offset: 0x00005EC3
			// (set) Token: 0x06000270 RID: 624 RVA: 0x00007CCB File Offset: 0x00005ECB
			[DataMember]
			public Guid RuleVersion { get; set; }

			// Token: 0x170000AB RID: 171
			// (get) Token: 0x06000271 RID: 625 RVA: 0x00007CD4 File Offset: 0x00005ED4
			// (set) Token: 0x06000272 RID: 626 RVA: 0x00007CDC File Offset: 0x00005EDC
			[DataMember]
			public Guid BindingId { get; set; }

			// Token: 0x170000AC RID: 172
			// (get) Token: 0x06000273 RID: 627 RVA: 0x00007CE5 File Offset: 0x00005EE5
			// (set) Token: 0x06000274 RID: 628 RVA: 0x00007CED File Offset: 0x00005EED
			[DataMember]
			public Guid BindingVersion { get; set; }

			// Token: 0x170000AD RID: 173
			// (get) Token: 0x06000275 RID: 629 RVA: 0x00007CF6 File Offset: 0x00005EF6
			// (set) Token: 0x06000276 RID: 630 RVA: 0x00007CFE File Offset: 0x00005EFE
			[DataMember]
			public string Scope { get; set; }

			// Token: 0x170000AE RID: 174
			// (get) Token: 0x06000277 RID: 631 RVA: 0x00007D07 File Offset: 0x00005F07
			// (set) Token: 0x06000278 RID: 632 RVA: 0x00007D0F File Offset: 0x00005F0F
			[DataMember]
			public PolicyScenario Scenario { get; set; }
		}

		// Token: 0x0200005C RID: 92
		[DataContract]
		public class StoredBindings
		{
			// Token: 0x06000279 RID: 633 RVA: 0x00007D18 File Offset: 0x00005F18
			public StoredBindings()
			{
				this.Init(default(StreamingContext));
			}

			// Token: 0x170000AF RID: 175
			// (get) Token: 0x0600027A RID: 634 RVA: 0x00007D3A File Offset: 0x00005F3A
			[DataMember]
			public List<BindingTask.StoredBinding> BindingsToAdd
			{
				get
				{
					return this.bindingsToAdd;
				}
			}

			// Token: 0x170000B0 RID: 176
			// (get) Token: 0x0600027B RID: 635 RVA: 0x00007D42 File Offset: 0x00005F42
			[DataMember]
			public List<BindingTask.StoredBinding> BindingsToRemove
			{
				get
				{
					return this.bindingsToRemove;
				}
			}

			// Token: 0x170000B1 RID: 177
			// (get) Token: 0x0600027C RID: 636 RVA: 0x00007D4A File Offset: 0x00005F4A
			// (set) Token: 0x0600027D RID: 637 RVA: 0x00007D52 File Offset: 0x00005F52
			[DataMember]
			public UnifiedPolicyStatus TriggerObjectStatus { get; set; }

			// Token: 0x170000B2 RID: 178
			// (get) Token: 0x0600027E RID: 638 RVA: 0x00007D5B File Offset: 0x00005F5B
			[DataMember]
			public Dictionary<Guid, UnifiedPolicyStatus> CompletedBindings
			{
				get
				{
					return this.completedBindings;
				}
			}

			// Token: 0x170000B3 RID: 179
			// (get) Token: 0x0600027F RID: 639 RVA: 0x00007D63 File Offset: 0x00005F63
			// (set) Token: 0x06000280 RID: 640 RVA: 0x00007D6B File Offset: 0x00005F6B
			[DataMember]
			public string WorkloadData { get; set; }

			// Token: 0x170000B4 RID: 180
			// (get) Token: 0x06000281 RID: 641 RVA: 0x00007D74 File Offset: 0x00005F74
			// (set) Token: 0x06000282 RID: 642 RVA: 0x00007D7C File Offset: 0x00005F7C
			[DataMember]
			public int SkippedDueToRetryNeed { get; set; }

			// Token: 0x170000B5 RID: 181
			// (get) Token: 0x06000283 RID: 643 RVA: 0x00007D85 File Offset: 0x00005F85
			// (set) Token: 0x06000284 RID: 644 RVA: 0x00007D8D File Offset: 0x00005F8D
			[DataMember]
			public string StatusPublishingError { get; set; }

			// Token: 0x06000285 RID: 645 RVA: 0x00007D96 File Offset: 0x00005F96
			[OnDeserializing]
			private void Init(StreamingContext ctx)
			{
				this.bindingsToAdd = new List<BindingTask.StoredBinding>();
				this.bindingsToRemove = new List<BindingTask.StoredBinding>();
				this.completedBindings = new Dictionary<Guid, UnifiedPolicyStatus>();
			}

			// Token: 0x04000134 RID: 308
			private List<BindingTask.StoredBinding> bindingsToAdd;

			// Token: 0x04000135 RID: 309
			private List<BindingTask.StoredBinding> bindingsToRemove;

			// Token: 0x04000136 RID: 310
			private Dictionary<Guid, UnifiedPolicyStatus> completedBindings;
		}
	}
}
