using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Management.ControlPanel;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x0200018E RID: 398
	public abstract class MetaDataIncludeWorkflow : Workflow
	{
		// Token: 0x060022BB RID: 8891 RVA: 0x00068BD6 File Offset: 0x00066DD6
		public MetaDataIncludeWorkflow()
		{
			this.IncludeNotAccessProperty = true;
			this.IncludeReadOnlyProperty = true;
			this.IncludeValidator = true;
		}

		// Token: 0x060022BC RID: 8892 RVA: 0x00068BF3 File Offset: 0x00066DF3
		protected MetaDataIncludeWorkflow(MetaDataIncludeWorkflow workflow) : base(workflow)
		{
			this.IncludeNotAccessProperty = workflow.IncludeNotAccessProperty;
			this.IncludeReadOnlyProperty = workflow.IncludeReadOnlyProperty;
			this.IncludeValidator = workflow.IncludeValidator;
		}

		// Token: 0x17001AB0 RID: 6832
		// (get) Token: 0x060022BD RID: 8893 RVA: 0x00068C20 File Offset: 0x00066E20
		// (set) Token: 0x060022BE RID: 8894 RVA: 0x00068C28 File Offset: 0x00066E28
		[DefaultValue(true)]
		protected bool IncludeValidator { get; set; }

		// Token: 0x17001AB1 RID: 6833
		// (get) Token: 0x060022BF RID: 8895 RVA: 0x00068C31 File Offset: 0x00066E31
		// (set) Token: 0x060022C0 RID: 8896 RVA: 0x00068C42 File Offset: 0x00066E42
		internal IIsInRole RbacChecker
		{
			get
			{
				return this.rbacPrincipal ?? RbacCheckerWrapper.RbacChecker;
			}
			set
			{
				this.rbacPrincipal = value;
			}
		}

		// Token: 0x17001AB2 RID: 6834
		// (get) Token: 0x060022C1 RID: 8897 RVA: 0x00068C4B File Offset: 0x00066E4B
		// (set) Token: 0x060022C2 RID: 8898 RVA: 0x00068C53 File Offset: 0x00066E53
		[DefaultValue(true)]
		protected bool IncludeReadOnlyProperty { get; set; }

		// Token: 0x17001AB3 RID: 6835
		// (get) Token: 0x060022C3 RID: 8899 RVA: 0x00068C5C File Offset: 0x00066E5C
		// (set) Token: 0x060022C4 RID: 8900 RVA: 0x00068C64 File Offset: 0x00066E64
		[DefaultValue(true)]
		protected bool IncludeNotAccessProperty { get; set; }

		// Token: 0x060022C5 RID: 8901 RVA: 0x00068C70 File Offset: 0x00066E70
		internal override void LoadMetaData(DataRow input, DataTable dataTable, DataObjectStore store, IList<string> outputVariables, out Dictionary<string, ValidatorInfo[]> validators, out IList<string> readOnlyProperties, out IList<string> notAccessProperties, Service service)
		{
			base.LoadMetaData(input, dataTable, store, outputVariables, out validators, out readOnlyProperties, out notAccessProperties, service);
			bool flag = true.Equals(dataTable.Rows[0]["IsReadOnly"]);
			foreach (object obj in dataTable.Columns)
			{
				DataColumn dataColumn = (DataColumn)obj;
				Variable variable = dataColumn.ExtendedProperties["Variable"] as Variable;
				if (variable != null)
				{
					if (outputVariables == null || outputVariables.Contains(dataColumn.ColumnName, StringComparer.OrdinalIgnoreCase))
					{
						if (this.IncludeValidator)
						{
							ValidatorInfo[] array = ValidatorHelper.ValidatorsFromPropertyDefinition(dataColumn.ExtendedProperties["PropertyDefinition"] as ProviderPropertyDefinition);
							if (array.Length != 0)
							{
								validators[dataColumn.ColumnName] = array;
							}
						}
						if (this.IncludeNotAccessProperty)
						{
							bool? flag2 = this.IsVariableAccessible(input, dataTable, store, variable, DDIHelper.GetOutputDepVariables(dataColumn));
							if (flag2.IsFalse())
							{
								notAccessProperties.Add(variable.Name);
							}
						}
					}
					if (this.IncludeReadOnlyProperty)
					{
						bool? flag3 = this.IsVariableSettable(input, dataTable, store, variable, service, DDIHelper.GetCodeBehindRegisteredDepVariables(dataColumn));
						if (flag3.IsFalse() || flag)
						{
							readOnlyProperties.Add(variable.Name);
						}
					}
				}
			}
		}

		// Token: 0x060022C6 RID: 8902 RVA: 0x00068DE0 File Offset: 0x00066FE0
		private bool CheckPredefinedPermission(string role, DataObjectStore store, Variable variable)
		{
			return role.Equals("NA", StringComparison.OrdinalIgnoreCase) || this.RbacChecker.IsInRole(role, (variable.RbacDataObjectName != null) ? (store.GetDataObject(variable.RbacDataObjectName) as ADRawEntry) : null);
		}

		// Token: 0x060022C7 RID: 8903 RVA: 0x00068E2C File Offset: 0x0006702C
		private bool? IsVariableAccessible(DataRow input, DataTable dataTable, DataObjectStore store, Variable variable, List<string> dependencies)
		{
			bool? flag = null;
			return this.CheckPermission(null, input, dataTable, store, variable, dependencies, new MetaDataIncludeWorkflow.CheckPermissionDelegate(this.CheckRbacForNotAccessPermission));
		}

		// Token: 0x060022C8 RID: 8904 RVA: 0x00068E74 File Offset: 0x00067074
		private bool? IsVariableSettable(DataRow input, DataTable dataTable, DataObjectStore store, Variable variable, Service service, List<string> dependencies)
		{
			bool flag = this is GetObjectWorkflow;
			bool flag2 = this is GetObjectForNewWorkflow;
			bool? result = null;
			if (flag || flag2)
			{
				if (flag && variable.SetRoles != null)
				{
					result = new bool?(this.CheckPredefinedPermission(variable.SetRoles, store, variable));
				}
				else if (flag2 && variable.NewRoles != null)
				{
					result = new bool?(this.CheckPredefinedPermission(variable.NewRoles, store, variable));
				}
				else
				{
					IEnumerable<Workflow> enumerable = null;
					if (flag)
					{
						enumerable = from c in service.Workflows
						where c is SetObjectWorkflow
						select c;
						if (variable.RbacDependenciesForSet != null)
						{
							dependencies.AddRange(variable.RbacDependenciesForSet);
						}
					}
					else if (flag2)
					{
						enumerable = from c in service.Workflows
						where c is NewObjectWorkflow
						select c;
						if (variable.RbacDependenciesForNew != null)
						{
							dependencies.AddRange(variable.RbacDependenciesForNew);
						}
					}
					if (enumerable.Count<Workflow>() > 0)
					{
						result = this.CheckPermission(enumerable, input, dataTable, store, variable, dependencies, new MetaDataIncludeWorkflow.CheckPermissionDelegate(this.CheckRbacForReadOnlyPermission));
					}
				}
			}
			return result;
		}

		// Token: 0x060022C9 RID: 8905 RVA: 0x00068FB4 File Offset: 0x000671B4
		private bool? CheckPermission(IEnumerable<Workflow> sets, DataRow input, DataTable dataTable, DataObjectStore store, Variable variable, IList<string> dependencies, MetaDataIncludeWorkflow.CheckPermissionDelegate checkPermissionDelegate)
		{
			bool? flag = null;
			if (dependencies != null && dependencies.Count > 0)
			{
				using (IEnumerator<string> enumerator = (from c in dependencies
				where !string.IsNullOrWhiteSpace(c)
				select c).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string name = enumerator.Current;
						Variable variable2 = dataTable.Columns[name].ExtendedProperties["Variable"] as Variable;
						if (variable2 != null)
						{
							flag = flag.And(checkPermissionDelegate(input, dataTable, store, variable2, sets));
							if (flag.IsFalse())
							{
								break;
							}
						}
					}
					return flag;
				}
			}
			flag = checkPermissionDelegate(input, dataTable, store, variable, sets);
			return flag;
		}

		// Token: 0x060022CA RID: 8906 RVA: 0x000690E4 File Offset: 0x000672E4
		private bool? CheckRbacForReadOnlyPermission(DataRow input, DataTable dataTable, DataObjectStore store, Variable variable, IEnumerable<Workflow> sets)
		{
			bool? flag = null;
			Collection<Activity> activities = sets.First<Workflow>().Activities;
			bool isSetCmdletDynamicParameter = !string.IsNullOrWhiteSpace(variable.DataObjectName) && !variable.IgnoreChangeTracking;
			foreach (Activity activity in activities)
			{
				flag = flag.Or(activity.FindAndCheckPermission((Activity a) => (isSetCmdletDynamicParameter && a is SetCmdlet && (a as SetCmdlet).DataObjectName == variable.DataObjectName) || (a is IReadOnlyChecker && a is CmdletActivity), input, dataTable, store, variable));
				if (flag.IsTrue())
				{
					break;
				}
			}
			return flag;
		}

		// Token: 0x060022CB RID: 8907 RVA: 0x0006920C File Offset: 0x0006740C
		private bool? CheckRbacForNotAccessPermission(DataRow input, DataTable dataTable, DataObjectStore store, Variable variable, IEnumerable<Workflow> sets)
		{
			bool? flag = null;
			Collection<Activity> activities = base.Activities;
			bool hasDataObject = !string.IsNullOrWhiteSpace(variable.DataObjectName);
			foreach (Activity activity in activities)
			{
				flag = flag.Or(activity.FindAndCheckPermission((Activity a) => (hasDataObject && a is GetCmdlet && (a as GetCmdlet).DataObjectName == variable.DataObjectName) || a.HasOutputVariable(variable.Name), input, dataTable, store, variable));
				if (flag.IsTrue())
				{
					break;
				}
			}
			if (hasDataObject)
			{
				IVersionable versionable = store.GetDataObject(variable.DataObjectName) as IVersionable;
				if (versionable != null && versionable.ExchangeVersion != null)
				{
					PropertyDefinition propertyDefinition = versionable.ObjectSchema[variable.MappingProperty];
					if (propertyDefinition != null && !versionable.IsPropertyAccessible(propertyDefinition))
					{
						flag = new bool?(false);
					}
				}
			}
			return flag;
		}

		// Token: 0x04001D9B RID: 7579
		internal const string RBAC_NotApplicable = "NA";

		// Token: 0x04001D9C RID: 7580
		private IIsInRole rbacPrincipal;

		// Token: 0x0200018F RID: 399
		// (Invoke) Token: 0x060022D0 RID: 8912
		internal delegate bool? CheckPermissionDelegate(DataRow input, DataTable dataTable, DataObjectStore store, Variable variable, IEnumerable<Workflow> sets);
	}
}
