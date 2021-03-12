using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000161 RID: 353
	public class SetCmdlet : PipelineCmdlet, IReadOnlyChecker
	{
		// Token: 0x060021DA RID: 8666 RVA: 0x00065DE8 File Offset: 0x00063FE8
		public SetCmdlet()
		{
		}

		// Token: 0x060021DB RID: 8667 RVA: 0x00065DF0 File Offset: 0x00063FF0
		protected SetCmdlet(SetCmdlet activity) : base(activity)
		{
			this.ForceExecution = activity.ForceExecution;
		}

		// Token: 0x060021DC RID: 8668 RVA: 0x00065E05 File Offset: 0x00064005
		public override Activity Clone()
		{
			return new SetCmdlet(this);
		}

		// Token: 0x17001A86 RID: 6790
		// (get) Token: 0x060021DD RID: 8669 RVA: 0x00065E0D File Offset: 0x0006400D
		// (set) Token: 0x060021DE RID: 8670 RVA: 0x00065E15 File Offset: 0x00064015
		[DefaultValue(false)]
		public bool ForceExecution { get; set; }

		// Token: 0x060021DF RID: 8671 RVA: 0x00065E1E File Offset: 0x0006401E
		protected override bool IsToExecuteCmdlet(DataRow input, DataTable dataTable, DataObjectStore store, List<Parameter> parameters)
		{
			return 1 != parameters.Count || !(parameters.First<Parameter>().Name == base.IdentityName) || this.ForceExecution;
		}

		// Token: 0x060021E0 RID: 8672 RVA: 0x00065E4C File Offset: 0x0006404C
		protected override List<Parameter> GetParametersToInvoke(DataRow input, DataTable dataTable, DataObjectStore store)
		{
			List<Parameter> parametersToInvoke = base.GetParametersToInvoke(input, dataTable, store);
			this.BuildParameters(input, store, parametersToInvoke);
			return parametersToInvoke;
		}

		// Token: 0x060021E1 RID: 8673 RVA: 0x00065EC0 File Offset: 0x000640C0
		internal override bool HasPermission(DataRow input, DataTable dataTable, DataObjectStore store, Variable var)
		{
			string updatingVariable = (var == null) ? null : var.MappingProperty;
			List<string> list;
			if (!string.IsNullOrWhiteSpace(updatingVariable))
			{
				IEnumerable<string> source = from c in base.Parameters
				where c.IsAccessingVariable(updatingVariable)
				select c.Name;
				string text = (source.Count<string>() > 0) ? source.First<string>() : updatingVariable;
				Collection<Parameter> collection = new Collection<Parameter>((from c in this.GetEffectiveParameters(input, dataTable, store)
				where c.Type != ParameterType.RunOnModified
				select c).ToList<Parameter>());
				if (!base.SingletonObject)
				{
					Parameter item = new Parameter
					{
						Name = base.IdentityName,
						Reference = base.IdentityVariable,
						Type = ParameterType.Mandatory
					};
					if (!collection.Contains(item))
					{
						collection.Add(item);
					}
				}
				list = (from c in collection
				select c.Name).ToList<string>();
				if (!list.Contains(text, StringComparer.OrdinalIgnoreCase))
				{
					list.Add(text);
				}
			}
			else
			{
				Collection<Parameter> collection2 = new Collection<Parameter>((from c in this.GetEffectiveParameters(input, dataTable, store)
				where c.IsRunnable(input, dataTable)
				select c).ToList<Parameter>());
				this.BuildParameters(input, store, collection2);
				list = (from c in collection2
				select c.Name).ToList<string>();
			}
			return this.CheckPermission(store, list, var);
		}

		// Token: 0x060021E2 RID: 8674 RVA: 0x000660B6 File Offset: 0x000642B6
		protected override string GetVerb()
		{
			return "Set-";
		}

		// Token: 0x060021E3 RID: 8675 RVA: 0x000660BD File Offset: 0x000642BD
		protected override void DoPreRunCore(DataRow input, DataTable dataTable, DataObjectStore store, Type codeBehind)
		{
			this.BuildParameters(input, store, base.Parameters);
			base.DoPreRunCore(input, dataTable, store, codeBehind);
		}

		// Token: 0x060021E4 RID: 8676 RVA: 0x000660D8 File Offset: 0x000642D8
		private void BuildParameters(DataRow input, DataObjectStore store, ICollection<Parameter> parameters)
		{
			if (!base.SingletonObject)
			{
				Parameter item = new Parameter
				{
					Name = base.IdentityName,
					Reference = base.IdentityVariable,
					Type = ParameterType.Mandatory
				};
				if (!parameters.Contains(item))
				{
					parameters.Add(item);
				}
			}
			List<string> modifiedPropertiesBasedOnDataObject = store.GetModifiedPropertiesBasedOnDataObject(input, base.DataObjectName);
			foreach (string text in modifiedPropertiesBasedOnDataObject)
			{
				if (!string.Equals(base.IdentityName, text, StringComparison.OrdinalIgnoreCase))
				{
					Parameter item2 = new Parameter
					{
						Name = text,
						Reference = text,
						Type = ParameterType.Mandatory
					};
					if (!parameters.Contains(item2))
					{
						parameters.Add(item2);
					}
				}
			}
		}

		// Token: 0x060021E5 RID: 8677 RVA: 0x000661D0 File Offset: 0x000643D0
		internal override bool? FindAndCheckPermission(Func<Activity, bool> predicate, DataRow input, DataTable dataTable, DataObjectStore store, Variable updatingVariable)
		{
			IEnumerable<Activity> enumerable = new List<Activity>
			{
				this
			}.Where(predicate);
			bool? result = null;
			bool flag = base.DataObjectName != null && base.DataObjectName == updatingVariable.DataObjectName;
			foreach (Activity activity in enumerable)
			{
				if (!flag)
				{
					if (!base.Parameters.Any((Parameter p) => p.IsAccessingVariable(updatingVariable.Name)))
					{
						continue;
					}
				}
				result = new bool?(activity.HasPermission(input, dataTable, store, updatingVariable));
			}
			return result;
		}
	}
}
