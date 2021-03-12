using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Exchange.Configuration.ObjectModel.EventLog;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.ValidationRules;
using Microsoft.Exchange.Diagnostics.Components.Authorization;

namespace Microsoft.Exchange.Configuration.Authorization
{
	// Token: 0x0200023F RID: 575
	internal sealed class RBACValidationRulesList
	{
		// Token: 0x0600146B RID: 5227 RVA: 0x0004BADC File Offset: 0x00049CDC
		private RBACValidationRulesList()
		{
			string text = Path.Combine(ConfigurationContext.Setup.BinPath, "Microsoft.Exchange.Management.RbacDefinition.dll");
			try
			{
				Assembly assembly = Assembly.LoadFrom(text);
				MethodInfo method = assembly.GetType("Microsoft.Exchange.Management.Tasks.ValidationRuleDefinitions").GetMethod("GetRBACValidationRulesList", BindingFlags.Static | BindingFlags.Public);
				List<ValidationRuleDefinition> rules = (List<ValidationRuleDefinition>)method.Invoke(null, null);
				this.Initialize(rules);
			}
			catch (Exception ex)
			{
				ExTraceGlobals.RunspaceConfigTracer.TraceError<string, string>(0L, "RBACValidationRulesList.RBACValidationRulesList. Cannot initialize validation rules from assembly {0}: {1}", text, ex.ToString());
				TaskLogger.LogRbacEvent(TaskEventLogConstants.Tuple_FailedToLoadValidationRules, text, new object[]
				{
					text,
					ex.ToString()
				});
				throw;
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x0600146C RID: 5228 RVA: 0x0004BB94 File Offset: 0x00049D94
		public static RBACValidationRulesList Instance
		{
			get
			{
				return RBACValidationRulesList.Nested.instance;
			}
		}

		// Token: 0x0600146D RID: 5229 RVA: 0x0004BB9C File Offset: 0x00049D9C
		private void Initialize(IEnumerable<ValidationRuleDefinition> rules)
		{
			if (rules == null)
			{
				throw new ArgumentNullException("rules");
			}
			if (this.featuresToRules != null && this.cmdletToRules != null)
			{
				return;
			}
			lock (this.listLock)
			{
				if (this.featuresToRules == null || this.cmdletToRules == null)
				{
					this.cmdletToRules = new Dictionary<string, List<ValidationRuleDefinition>>();
					this.featuresToRules = new Dictionary<string, List<ValidationRuleDefinition>>();
					foreach (ValidationRuleDefinition validationRuleDefinition in rules)
					{
						if (validationRuleDefinition.Expressions != null && validationRuleDefinition.Expressions.Count > 0)
						{
							foreach (ValidationRuleExpression validationRuleExpression in validationRuleDefinition.Expressions)
							{
								MonadFilter monadFilter = new MonadFilter(validationRuleExpression.QueryString, null, validationRuleExpression.Schema);
								validationRuleExpression.QueryFilter = monadFilter.InnerFilter;
							}
						}
						OrganizationValidationRuleDefinition organizationValidationRuleDefinition = validationRuleDefinition as OrganizationValidationRuleDefinition;
						if (organizationValidationRuleDefinition != null)
						{
							if (organizationValidationRuleDefinition.OverridingAllowExpressions != null && organizationValidationRuleDefinition.OverridingAllowExpressions.Count > 0)
							{
								foreach (ValidationRuleExpression validationRuleExpression2 in organizationValidationRuleDefinition.OverridingAllowExpressions)
								{
									MonadFilter monadFilter2 = new MonadFilter(validationRuleExpression2.QueryString, null, validationRuleExpression2.Schema);
									validationRuleExpression2.QueryFilter = monadFilter2.InnerFilter;
								}
							}
							if (organizationValidationRuleDefinition.RestrictionExpressions != null && organizationValidationRuleDefinition.RestrictionExpressions.Count > 0)
							{
								foreach (ValidationRuleExpression validationRuleExpression3 in organizationValidationRuleDefinition.RestrictionExpressions)
								{
									MonadFilter monadFilter3 = new MonadFilter(validationRuleExpression3.QueryString, null, validationRuleExpression3.Schema);
									validationRuleExpression3.QueryFilter = monadFilter3.InnerFilter;
								}
							}
						}
						foreach (RoleEntry roleEntry in validationRuleDefinition.ApplicableRoleEntries)
						{
							string key = roleEntry.Name;
							if (roleEntry is CmdletRoleEntry)
							{
								key = ((CmdletRoleEntry)roleEntry).FullName;
							}
							RBACHelper.AddValueToDictionaryList<ValidationRuleDefinition>(this.cmdletToRules, key, validationRuleDefinition);
						}
						RBACHelper.AddValueToDictionaryList<ValidationRuleDefinition>(this.featuresToRules, validationRuleDefinition.Feature, validationRuleDefinition);
					}
				}
			}
		}

		// Token: 0x0600146E RID: 5230 RVA: 0x0004BE90 File Offset: 0x0004A090
		internal IList<RoleEntryValidationRuleTuple> GetApplicableRules(string cmdletFullName, IList<string> parameters, ValidationRuleSkus applicableSku)
		{
			if (string.IsNullOrEmpty(cmdletFullName))
			{
				throw new ArgumentNullException("cmdletFullName");
			}
			if (parameters == null)
			{
				throw new ArgumentNullException("parameters");
			}
			ExTraceGlobals.AccessCheckTracer.TraceDebug<string, string>((long)this.GetHashCode(), "Entering RBACValidationRulesList.GetApplicableRules({0}). Sku {1} ", cmdletFullName, applicableSku.ToString());
			if (!this.IsListInitialized())
			{
				return RBACValidationRulesList.Nested.TupleEmptyList;
			}
			List<ValidationRuleDefinition> list = null;
			IList<RoleEntryValidationRuleTuple> list2 = RBACValidationRulesList.Nested.TupleEmptyList;
			if (this.cmdletToRules.TryGetValue(cmdletFullName, out list))
			{
				RoleEntry targetRoleEntry = null;
				if (this.TryCreateRoleEntry(cmdletFullName, parameters, out targetRoleEntry))
				{
					RoleEntry matchingRoleEntry = null;
					list2 = new List<RoleEntryValidationRuleTuple>(list.Count);
					foreach (ValidationRuleDefinition validationRuleDefinition in list)
					{
						if ((byte)(validationRuleDefinition.ApplicableSku & applicableSku) != 0 && validationRuleDefinition.IsRuleApplicable(targetRoleEntry, out matchingRoleEntry))
						{
							list2.Add(new RoleEntryValidationRuleTuple(validationRuleDefinition, matchingRoleEntry));
						}
					}
				}
			}
			ExTraceGlobals.AccessCheckTracer.TraceDebug<string, string>((long)this.GetHashCode(), "RBACValidationRulesList.GetApplicableRules({0}). returns '{1}'", cmdletFullName, (list2.Count == 0) ? "<Empty>" : (list2.Count.ToString() + " validation rules."));
			return list2;
		}

		// Token: 0x0600146F RID: 5231 RVA: 0x0004BFC4 File Offset: 0x0004A1C4
		internal bool HasApplicableValidationRules(string cmdletFullName)
		{
			if (string.IsNullOrEmpty(cmdletFullName))
			{
				throw new ArgumentNullException("cmdletFullName");
			}
			ExTraceGlobals.AccessCheckTracer.TraceDebug<string>((long)this.GetHashCode(), "Entering RBACValidationRulesList.HasApplicableValidationRules({0}).", cmdletFullName);
			return this.IsListInitialized() && this.cmdletToRules.ContainsKey(cmdletFullName);
		}

		// Token: 0x06001470 RID: 5232 RVA: 0x0004C014 File Offset: 0x0004A214
		internal IList<ValidationRuleDefinition> GetApplicableRules(string featureName)
		{
			if (string.IsNullOrEmpty(featureName))
			{
				throw new ArgumentNullException("featureName");
			}
			ExTraceGlobals.AccessCheckTracer.TraceDebug<string>((long)this.GetHashCode(), "Entering RBACValidationRulesList.GetApplicableRules({0}).", featureName);
			if (!this.IsListInitialized())
			{
				ExTraceGlobals.AccessCheckTracer.TraceDebug<string>((long)this.GetHashCode(), "RBACValidationRulesList.GetApplicableRules({0}). returns '<Empty>'", featureName);
				return RBACValidationRulesList.Nested.RuleDefinitionEmptyList;
			}
			List<ValidationRuleDefinition> list = null;
			if (!this.featuresToRules.TryGetValue(featureName, out list))
			{
				ExTraceGlobals.AccessCheckTracer.TraceDebug<string>((long)this.GetHashCode(), "RBACValidationRulesList.GetApplicableRules({0}). returns '<Empty>'", featureName);
				return RBACValidationRulesList.Nested.RuleDefinitionEmptyList;
			}
			ExTraceGlobals.AccessCheckTracer.TraceDebug<string, string>((long)this.GetHashCode(), "RBACValidationRulesList.GetApplicableRules({0}). returns '{1}'", featureName, list.Count.ToString() + " validation rules.");
			return list;
		}

		// Token: 0x06001471 RID: 5233 RVA: 0x0004C0D0 File Offset: 0x0004A2D0
		private bool TryCreateRoleEntry(string cmdletFullName, IList<string> parameters, out RoleEntry roleEntry)
		{
			roleEntry = null;
			int num = cmdletFullName.IndexOf('\\');
			if (num > 0)
			{
				roleEntry = new CmdletRoleEntry(cmdletFullName.Substring(num + 1), cmdletFullName.Substring(0, num), parameters.ToArray<string>());
				return true;
			}
			return false;
		}

		// Token: 0x06001472 RID: 5234 RVA: 0x0004C10E File Offset: 0x0004A30E
		private bool IsListInitialized()
		{
			if (this.cmdletToRules == null || this.featuresToRules == null)
			{
				ExTraceGlobals.AccessCheckTracer.TraceDebug((long)this.GetHashCode(), "RBACValidationRulesList.IsListInitialized(). ValidationRules list is not initialized.");
				return false;
			}
			return true;
		}

		// Token: 0x040005DB RID: 1499
		private Dictionary<string, List<ValidationRuleDefinition>> cmdletToRules;

		// Token: 0x040005DC RID: 1500
		private Dictionary<string, List<ValidationRuleDefinition>> featuresToRules;

		// Token: 0x040005DD RID: 1501
		private object listLock = new object();

		// Token: 0x02000240 RID: 576
		private class Nested
		{
			// Token: 0x040005DE RID: 1502
			internal static readonly RBACValidationRulesList instance = new RBACValidationRulesList();

			// Token: 0x040005DF RID: 1503
			internal static readonly IList<RoleEntryValidationRuleTuple> TupleEmptyList = new ReadOnlyCollection<RoleEntryValidationRuleTuple>(new RoleEntryValidationRuleTuple[0]);

			// Token: 0x040005E0 RID: 1504
			internal static readonly IList<ValidationRuleDefinition> RuleDefinitionEmptyList = new ReadOnlyCollection<ValidationRuleDefinition>(new ValidationRuleDefinition[0]);
		}
	}
}
