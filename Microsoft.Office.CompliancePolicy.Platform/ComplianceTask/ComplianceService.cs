using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Office.CompliancePolicy.ComplianceData;
using Microsoft.Office.CompliancePolicy.Dar;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;
using Microsoft.Office.CompliancePolicy.PolicyEvaluation;

namespace Microsoft.Office.CompliancePolicy.ComplianceTask
{
	// Token: 0x0200005F RID: 95
	public class ComplianceService
	{
		// Token: 0x06000290 RID: 656 RVA: 0x00007E7C File Offset: 0x0000607C
		public ComplianceService(PeriodicPolicyTask darTask, DarTaskManager darTaskManager)
		{
			if (darTaskManager == null)
			{
				throw new ArgumentNullException("darTaskManager");
			}
			if (darTask == null)
			{
				throw new ArgumentNullException("darTask");
			}
			this.csp = darTask.ComplianceServiceProvider;
			this.darTaskManager = darTaskManager;
			this.darTask = darTask;
		}

		// Token: 0x06000291 RID: 657 RVA: 0x00007EDC File Offset: 0x000060DC
		public DarTaskExecutionResult ApplyPeriodicPolicies(ComplianceItemContainer root)
		{
			if (root == null)
			{
				this.csp.GetExecutionLog().LogWarnining("ComplianceService", null, null, "Skipping policy processing since root container is null ", new KeyValuePair<string, object>[]
				{
					new KeyValuePair<string, object>("Tag", DarExecutionLogClientIDs.ComplianceService0.ToString())
				});
				return DarTaskExecutionResult.Failed;
			}
			DarTaskExecutionResult result;
			using (PolicyConfigProvider policyStore = this.csp.GetPolicyStore(root))
			{
				if (policyStore != null)
				{
					ComplianceService.PolicyApplicationContext policyApplicationContext = new ComplianceService.PolicyApplicationContext
					{
						Root = root,
						PolicyStore = policyStore
					};
					policyApplicationContext.RootRules = ComplianceService.DetermineRootRules(policyApplicationContext, root);
					result = this.ApplyPolicyInContainer(policyApplicationContext, root);
				}
				else
				{
					this.csp.GetExecutionLog().LogWarnining("ComplianceService", null, null, string.Format(CultureInfo.InvariantCulture, "Skipping policy processing for {0} since Policy Store is null ", new object[]
					{
						root.Id
					}), new KeyValuePair<string, object>[]
					{
						new KeyValuePair<string, object>("Tag", DarExecutionLogClientIDs.ComplianceService1.ToString())
					});
					result = DarTaskExecutionResult.Failed;
				}
			}
			return result;
		}

		// Token: 0x06000292 RID: 658 RVA: 0x00007FFC File Offset: 0x000061FC
		private static ComplianceService.PolicyApplicationContext GetPolicyApplicationContextForChild(ComplianceItemContainer parent, ComplianceItemContainer child, ComplianceService.PolicyApplicationContext parentContext, ComplianceService.EffectiveRules parentRules)
		{
			parentContext.ParentRules = null;
			if (child.Ancestors != null && child.Ancestors[0].Id == parent.Id)
			{
				parentContext.ParentRules = parentRules;
			}
			return parentContext;
		}

		// Token: 0x06000293 RID: 659 RVA: 0x00008034 File Offset: 0x00006234
		private static Dictionary<string, int> GetAncestorContainerIds(ComplianceItemContainer container)
		{
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			List<ComplianceItemContainer> ancestors = container.Ancestors;
			int num = 1;
			if (ancestors != null)
			{
				foreach (ComplianceItemContainer complianceItemContainer in ancestors)
				{
					dictionary.Add(complianceItemContainer.Id, num++);
				}
			}
			return dictionary;
		}

		// Token: 0x06000294 RID: 660 RVA: 0x000080A0 File Offset: 0x000062A0
		private static ComplianceService.EffectiveRules DetermineEffectiveRules(ComplianceService.PolicyApplicationContext policyApplicationContext, IEnumerable<PolicyBindingConfig> bindings, Dictionary<string, int> scopeAncestors)
		{
			ComplianceService.EffectiveRules effectiveRules = new ComplianceService.EffectiveRules();
			if (bindings != null)
			{
				foreach (PolicyBindingConfig policyBindingConfig in bindings)
				{
					int depth = 0;
					if (policyBindingConfig != null)
					{
						if (scopeAncestors != null && scopeAncestors.ContainsKey(policyBindingConfig.Scope.ImmutableIdentity))
						{
							depth = scopeAncestors[policyBindingConfig.Scope.ImmutableIdentity];
						}
						if (policyBindingConfig.IsExempt)
						{
							PolicyDefinitionConfig policyDefinitionConfig = policyApplicationContext.PolicyStore.FindByIdentity<PolicyDefinitionConfig>(policyBindingConfig.PolicyDefinitionConfigId);
							effectiveRules.Exempt(policyDefinitionConfig.Scenario);
						}
						else if (policyBindingConfig.PolicyRuleConfigId != null)
						{
							PolicyRuleConfig rule = policyApplicationContext.PolicyStore.FindByIdentity<PolicyRuleConfig>(policyBindingConfig.PolicyRuleConfigId.Value);
							effectiveRules.Add(rule, depth, policyBindingConfig.PolicyAssociationConfigId);
						}
						else
						{
							IEnumerable<PolicyRuleConfig> enumerable = policyApplicationContext.PolicyStore.FindByPolicyDefinitionConfigId<PolicyRuleConfig>(policyBindingConfig.PolicyDefinitionConfigId);
							foreach (PolicyRuleConfig rule2 in enumerable)
							{
								effectiveRules.Add(rule2, depth, policyBindingConfig.PolicyAssociationConfigId);
							}
						}
					}
				}
			}
			return effectiveRules;
		}

		// Token: 0x06000295 RID: 661 RVA: 0x00008204 File Offset: 0x00006404
		private static ComplianceService.EffectiveRules DetermineRootRules(ComplianceService.PolicyApplicationContext policyApplicationContext, ComplianceItemContainer rootContainer)
		{
			ComplianceService.EffectiveRules effectiveRules = null;
			if (rootContainer.SupportsAssociation)
			{
				PolicyAssociationConfig associationForContainerOrTemplate = ComplianceService.GetAssociationForContainerOrTemplate(policyApplicationContext, rootContainer);
				if (associationForContainerOrTemplate != null && associationForContainerOrTemplate.DefaultPolicyDefinitionConfigId != null)
				{
					PolicyDefinitionConfig policyDefinitionConfig = policyApplicationContext.PolicyStore.FindByIdentity<PolicyDefinitionConfig>(associationForContainerOrTemplate.DefaultPolicyDefinitionConfigId.Value);
					if (policyDefinitionConfig.DefaultPolicyRuleConfigId != null)
					{
						PolicyRuleConfig policyRuleConfig = policyApplicationContext.PolicyStore.FindByIdentity<PolicyRuleConfig>(policyDefinitionConfig.DefaultPolicyRuleConfigId.Value);
						if (policyRuleConfig != null)
						{
							effectiveRules = new ComplianceService.EffectiveRules();
							effectiveRules.Add(policyRuleConfig, int.MaxValue, new Guid?(associationForContainerOrTemplate.Identity));
						}
					}
				}
			}
			return effectiveRules;
		}

		// Token: 0x06000296 RID: 662 RVA: 0x000082A4 File Offset: 0x000064A4
		private static PolicyAssociationConfig GetAssociationForContainerOrTemplate(ComplianceService.PolicyApplicationContext context, ComplianceItemContainer container)
		{
			PolicyAssociationConfig policyAssociationConfig = context.PolicyStore.FindPolicyAssociationConfigByScope(container.Id);
			if (policyAssociationConfig != null)
			{
				return policyAssociationConfig;
			}
			return context.PolicyStore.FindPolicyAssociationConfigByScope(container.Template);
		}

		// Token: 0x06000297 RID: 663 RVA: 0x00008324 File Offset: 0x00006524
		private DarTaskExecutionResult ApplyPolicyInContainer(ComplianceService.PolicyApplicationContext policyApplicationContext, ComplianceItemContainer itemContainer)
		{
			this.csp.GetExecutionLog().LogVerbose("ComplianceService", null, null, "ComplianceService: Applying policy for container: " + itemContainer.Id, new KeyValuePair<string, object>[]
			{
				new KeyValuePair<string, object>("Tag", DarExecutionLogClientIDs.ComplianceService2.ToString())
			});
			if (!this.darTask.ShouldContinue(this.darTaskManager))
			{
				return DarTaskExecutionResult.Yielded;
			}
			try
			{
				IEnumerable<PolicyBindingConfig> bindings = policyApplicationContext.PolicyStore.FindPolicyBindingConfigsByScopes(new List<string>
				{
					itemContainer.Id
				});
				ComplianceService.EffectiveRules effectiveRules = ComplianceService.DetermineEffectiveRules(policyApplicationContext, bindings, null);
				if (policyApplicationContext.ParentRules != null)
				{
					effectiveRules.Merge(policyApplicationContext.ParentRules);
				}
				else
				{
					Dictionary<string, int> ancestorContainerIds = ComplianceService.GetAncestorContainerIds(itemContainer);
					if (ancestorContainerIds.Keys.Count > 0)
					{
						IEnumerable<PolicyBindingConfig> enumerable = policyApplicationContext.PolicyStore.FindPolicyBindingConfigsByScopes(ancestorContainerIds.Keys);
						if (enumerable.Count<PolicyBindingConfig>() != 0)
						{
							ComplianceService.EffectiveRules rulesToMerge = ComplianceService.DetermineEffectiveRules(policyApplicationContext, enumerable, ancestorContainerIds);
							effectiveRules.Merge(rulesToMerge);
						}
					}
					if (policyApplicationContext.RootRules != null)
					{
						effectiveRules.Merge(policyApplicationContext.RootRules);
					}
				}
				itemContainer.UpdatePolicy(effectiveRules.RulesForScenario);
				if (itemContainer.HasItems)
				{
					using (HashSet<PolicyScenario>.Enumerator enumerator = this.periodicProcessingScenarios.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							PolicyScenario policyScenario = enumerator.Current;
							if (itemContainer.SupportsPolicy(policyScenario))
							{
								if (effectiveRules.RulesForScenario.ContainsKey(policyScenario))
								{
									this.RunPeriodicRules(itemContainer, policyScenario, effectiveRules.RulesForScenario[policyScenario]);
								}
							}
							else
							{
								this.csp.GetExecutionLog().LogVerbose("ComplianceService", null, null, string.Format(CultureInfo.InvariantCulture, "ComplianceService: Not running retention rules for {0} as it does not support retention rules", new object[]
								{
									itemContainer.Id
								}), new KeyValuePair<string, object>[]
								{
									new KeyValuePair<string, object>("Tag", DarExecutionLogClientIDs.ComplianceService3.ToString())
								});
							}
						}
						goto IL_482;
					}
				}
				if (itemContainer.Level < this.darTask.MaxLevel)
				{
					try
					{
						this.csp.GetExecutionLog().LogVerbose("ComplianceService", null, null, string.Format(CultureInfo.InvariantCulture, "ComplianceService: Applying policy for children of container: {0}. Max Level: {1}; Container Level: {2} ", new object[]
						{
							itemContainer.Id,
							this.darTask.MaxLevel,
							itemContainer.Level
						}), new KeyValuePair<string, object>[]
						{
							new KeyValuePair<string, object>("Tag", DarExecutionLogClientIDs.ComplianceService4.ToString())
						});
						itemContainer.ForEachChildContainer(delegate(ComplianceItemContainer childContainer)
						{
							this.ApplyPolicyInContainer(ComplianceService.GetPolicyApplicationContextForChild(itemContainer, childContainer, policyApplicationContext, effectiveRules), childContainer);
						}, new Func<ComplianceItemContainer, Exception, bool>(this.HandlerContainerIterationError));
						goto IL_482;
					}
					catch (Exception exception)
					{
						this.csp.GetExecutionLog().LogError("ComplianceService", null, null, exception, string.Format("ComplianceService: Error when applying policies for child of {0}.", itemContainer.Id), new KeyValuePair<string, object>[]
						{
							new KeyValuePair<string, object>("Tag", DarExecutionLogClientIDs.ComplianceService5.ToString())
						});
						goto IL_482;
					}
				}
				this.csp.GetExecutionLog().LogVerbose("ComplianceService", null, null, string.Format(CultureInfo.InvariantCulture, "ComplianceService: Skipping Applying policy for children of container:{0}. Max Level: {1}; Container Level: {2} ", new object[]
				{
					itemContainer.Id,
					this.darTask.MaxLevel,
					itemContainer.Level
				}), new KeyValuePair<string, object>[]
				{
					new KeyValuePair<string, object>("Tag", DarExecutionLogClientIDs.ComplianceService12.ToString())
				});
				IL_482:;
			}
			catch (CompliancePolicyException exception2)
			{
				this.csp.GetExecutionLog().LogError("ComplianceService", null, null, exception2, string.Format("ComplianceService: Error when determining and applying policies for {0}.", itemContainer.Id), new KeyValuePair<string, object>[]
				{
					new KeyValuePair<string, object>("Tag", DarExecutionLogClientIDs.ComplianceService6.ToString())
				});
				return DarTaskExecutionResult.Failed;
			}
			return DarTaskExecutionResult.Completed;
		}

		// Token: 0x06000298 RID: 664 RVA: 0x00008868 File Offset: 0x00006A68
		private void RunPeriodicRules(ComplianceItemContainer itemContainer, PolicyScenario scenario, List<PolicyRuleConfig> rules)
		{
			if (scenario != PolicyScenario.Retention)
			{
				return;
			}
			foreach (PolicyRuleConfig ruleConfig in rules)
			{
				PolicyRule policyRule = this.GetPolicyRule(ruleConfig);
				PolicyEvaluationContext policyEvaluationContext = null;
				try
				{
					ComplianceItemPagedReader pagedReader = this.csp.GetPagedReader(itemContainer);
					QueryPredicate queryPredicate = policyRule.Condition as QueryPredicate;
					if (queryPredicate == null)
					{
						this.csp.GetExecutionLog().LogError("ComplianceService", null, null, null, string.Format(CultureInfo.InvariantCulture, "Skipping policy processing for container since QueryPredicate is null. Container Id: {0}", new object[]
						{
							itemContainer.Id
						}), new KeyValuePair<string, object>[]
						{
							new KeyValuePair<string, object>("Tag", DarExecutionLogClientIDs.ComplianceService7.ToString())
						});
						break;
					}
					pagedReader.Condition = queryPredicate;
					policyEvaluationContext = PolicyEvaluationContext.Create(pagedReader, Guid.NewGuid().ToString(), this.csp.GetExecutionLog(), this.csp.GetAuditor());
					PolicyEngine.Instance.Execute(policyEvaluationContext, new List<PolicyRule>
					{
						policyRule
					});
				}
				catch (CompliancePolicyException exception)
				{
					this.csp.GetExecutionLog().LogError("ComplianceService", null, null, exception, "Compliance Policy Exception when executing policy rules", new KeyValuePair<string, object>[]
					{
						new KeyValuePair<string, object>("Tag", DarExecutionLogClientIDs.ComplianceService8.ToString())
					});
				}
				finally
				{
					if (policyEvaluationContext != null && policyEvaluationContext.RulesEvaluationHistory != null)
					{
						this.csp.GetExecutionLog().LogVerbose("ComplianceService", null, null, string.Format(CultureInfo.InvariantCulture, "Time spent executing policy rules: {0}", new object[]
						{
							policyEvaluationContext.RulesEvaluationHistory.TimeSpent
						}), new KeyValuePair<string, object>[]
						{
							new KeyValuePair<string, object>("Tag", DarExecutionLogClientIDs.ComplianceService9.ToString())
						});
					}
					else
					{
						this.csp.GetExecutionLog().LogError("ComplianceService", null, null, null, "Rules evaluation history is null", new KeyValuePair<string, object>[]
						{
							new KeyValuePair<string, object>("Tag", DarExecutionLogClientIDs.ComplianceService10.ToString())
						});
					}
				}
			}
		}

		// Token: 0x06000299 RID: 665 RVA: 0x00008B04 File Offset: 0x00006D04
		private PolicyRule GetPolicyRule(PolicyRuleConfig ruleConfig)
		{
			return this.csp.GetRuleParser().GetRule(ruleConfig.RuleBlob);
		}

		// Token: 0x0600029A RID: 666 RVA: 0x00008B1C File Offset: 0x00006D1C
		private bool HandlerContainerIterationError(ComplianceItemContainer container, Exception exception)
		{
			return false;
		}

		// Token: 0x04000141 RID: 321
		private const string LoggingClientId = "ComplianceService";

		// Token: 0x04000142 RID: 322
		private readonly ComplianceServiceProvider csp;

		// Token: 0x04000143 RID: 323
		private readonly DarTaskManager darTaskManager;

		// Token: 0x04000144 RID: 324
		private readonly PeriodicPolicyTask darTask;

		// Token: 0x04000145 RID: 325
		private HashSet<PolicyScenario> periodicProcessingScenarios = new HashSet<PolicyScenario>
		{
			PolicyScenario.Retention
		};

		// Token: 0x02000060 RID: 96
		internal class PolicyApplicationContext
		{
			// Token: 0x170000BA RID: 186
			// (get) Token: 0x0600029B RID: 667 RVA: 0x00008B1F File Offset: 0x00006D1F
			// (set) Token: 0x0600029C RID: 668 RVA: 0x00008B27 File Offset: 0x00006D27
			internal ComplianceItemContainer Root { get; set; }

			// Token: 0x170000BB RID: 187
			// (get) Token: 0x0600029D RID: 669 RVA: 0x00008B30 File Offset: 0x00006D30
			// (set) Token: 0x0600029E RID: 670 RVA: 0x00008B38 File Offset: 0x00006D38
			internal PolicyConfigProvider PolicyStore { get; set; }

			// Token: 0x170000BC RID: 188
			// (get) Token: 0x0600029F RID: 671 RVA: 0x00008B41 File Offset: 0x00006D41
			// (set) Token: 0x060002A0 RID: 672 RVA: 0x00008B49 File Offset: 0x00006D49
			internal ComplianceService.EffectiveRules ParentRules { get; set; }

			// Token: 0x170000BD RID: 189
			// (get) Token: 0x060002A1 RID: 673 RVA: 0x00008B52 File Offset: 0x00006D52
			// (set) Token: 0x060002A2 RID: 674 RVA: 0x00008B5A File Offset: 0x00006D5A
			internal ComplianceService.EffectiveRules RootRules { get; set; }
		}

		// Token: 0x02000061 RID: 97
		internal class EffectiveRules
		{
			// Token: 0x060002A4 RID: 676 RVA: 0x00008B6B File Offset: 0x00006D6B
			internal EffectiveRules()
			{
				this.RulesForAssociation = new Dictionary<Guid, Tuple<int, List<PolicyRuleConfig>>>();
				this.ScenariosExempted = new HashSet<PolicyScenario>();
			}

			// Token: 0x170000BE RID: 190
			// (get) Token: 0x060002A5 RID: 677 RVA: 0x00008B8C File Offset: 0x00006D8C
			internal Dictionary<PolicyScenario, List<PolicyRuleConfig>> RulesForScenario
			{
				get
				{
					if (this.rulesForScenario == null)
					{
						this.rulesForScenario = new Dictionary<PolicyScenario, List<PolicyRuleConfig>>();
						foreach (Tuple<int, List<PolicyRuleConfig>> tuple in this.RulesForAssociation.Values)
						{
							foreach (PolicyRuleConfig policyRuleConfig in tuple.Item2)
							{
								PolicyScenario scenario = policyRuleConfig.Scenario;
								if (!this.ScenariosExempted.Contains(scenario))
								{
									if (!this.rulesForScenario.ContainsKey(scenario))
									{
										this.rulesForScenario[scenario] = new List<PolicyRuleConfig>();
									}
									this.rulesForScenario[scenario].Add(policyRuleConfig);
								}
							}
						}
					}
					return this.rulesForScenario;
				}
			}

			// Token: 0x170000BF RID: 191
			// (get) Token: 0x060002A6 RID: 678 RVA: 0x00008C80 File Offset: 0x00006E80
			// (set) Token: 0x060002A7 RID: 679 RVA: 0x00008C88 File Offset: 0x00006E88
			internal HashSet<PolicyScenario> ScenariosExempted { get; set; }

			// Token: 0x170000C0 RID: 192
			// (get) Token: 0x060002A8 RID: 680 RVA: 0x00008C91 File Offset: 0x00006E91
			// (set) Token: 0x060002A9 RID: 681 RVA: 0x00008C99 File Offset: 0x00006E99
			private Dictionary<Guid, Tuple<int, List<PolicyRuleConfig>>> RulesForAssociation { get; set; }

			// Token: 0x060002AA RID: 682 RVA: 0x00008CA4 File Offset: 0x00006EA4
			internal void Add(PolicyRuleConfig rule, int depth, Guid? policyAssociationId)
			{
				Guid key = (policyAssociationId != null) ? policyAssociationId.Value : Guid.Empty;
				if (!this.RulesForAssociation.ContainsKey(key) || this.RulesForAssociation[key].Item1 > depth)
				{
					this.RulesForAssociation[key] = new Tuple<int, List<PolicyRuleConfig>>(depth, new List<PolicyRuleConfig>());
				}
				if (this.RulesForAssociation[key].Item1 == depth)
				{
					this.RulesForAssociation[key].Item2.Add(rule);
					this.rulesForScenario = null;
				}
			}

			// Token: 0x060002AB RID: 683 RVA: 0x00008D34 File Offset: 0x00006F34
			internal void Exempt(PolicyScenario scenario)
			{
				if (!this.ScenariosExempted.Contains(scenario))
				{
					this.ScenariosExempted.Add(scenario);
					this.rulesForScenario = null;
				}
			}

			// Token: 0x060002AC RID: 684 RVA: 0x00008D58 File Offset: 0x00006F58
			internal void Merge(ComplianceService.EffectiveRules rulesToMerge)
			{
				foreach (Guid key in rulesToMerge.RulesForAssociation.Keys)
				{
					if (this.RulesForAssociation.ContainsKey(key))
					{
						if (this.RulesForAssociation[key].Item1 > rulesToMerge.RulesForAssociation[key].Item1)
						{
							this.RulesForAssociation[key] = rulesToMerge.RulesForAssociation[key];
							this.rulesForScenario = null;
						}
					}
					else
					{
						this.RulesForAssociation.Add(key, rulesToMerge.RulesForAssociation[key]);
						this.rulesForScenario = null;
					}
				}
				this.ScenariosExempted.UnionWith(rulesToMerge.ScenariosExempted);
			}

			// Token: 0x0400014A RID: 330
			private Dictionary<PolicyScenario, List<PolicyRuleConfig>> rulesForScenario;
		}
	}
}
