using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000168 RID: 360
	[AttributeUsage(AttributeTargets.Class)]
	public class DDIAllVariableHasRoles : DDIValidateAttribute, IDDIHasArgumentValidator
	{
		// Token: 0x06002203 RID: 8707 RVA: 0x00066777 File Offset: 0x00064977
		public DDIAllVariableHasRoles() : base("DDIAllVariableHasRoles")
		{
		}

		// Token: 0x06002204 RID: 8708 RVA: 0x00066784 File Offset: 0x00064984
		public override List<string> Validate(object target, Service profile)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002205 RID: 8709 RVA: 0x000667A0 File Offset: 0x000649A0
		public List<string> ValidateWithArg(object target, Service profile, Dictionary<string, string> arguments)
		{
			List<string> list = new List<string>();
			Variable variable = target as Variable;
			arguments.TryGetValue("CodeBehind", out this.codeBehind);
			arguments.TryGetValue("Xaml", out this.xaml);
			string xamlName = arguments["SchemaName"];
			using (new DDIVMockRbacPrincipal())
			{
				Dictionary<string, List<string>> rbacMetaData = DDIVUtil.GetRbacMetaData(xamlName, profile);
				DataObjectStore store = DDIVUtil.GetStore(xamlName, profile);
				DataTable table = DDIVUtil.GetTable(xamlName, profile, rbacMetaData);
				DataRow dataRow = table.NewRow();
				DataColumn dataColumn = table.Columns[variable.Name];
				bool? flag = null;
				foreach (Workflow workflow in from x in profile.Workflows
				where x is GetObjectWorkflow || x is GetObjectForNewWorkflow
				select x)
				{
					List<string> list2 = new List<string>();
					if (rbacMetaData != null && rbacMetaData.ContainsKey(variable.Name))
					{
						list2.AddRange(rbacMetaData[variable.Name]);
					}
					MethodInfo method = typeof(MetaDataIncludeWorkflow).GetMethod("IsVariableSettable", BindingFlags.Instance | BindingFlags.NonPublic);
					if ((workflow is GetObjectWorkflow || workflow is GetObjectForNewWorkflow) && !(method.Invoke(workflow, new object[]
					{
						dataRow,
						table,
						store,
						variable,
						profile,
						list2
					}) is bool?))
					{
						string workflowName = (workflow is GetObjectWorkflow) ? "SetObjectWorkflow" : "NewObjectWorkflow";
						string code = DDIVUtil.RetrieveCodesInPostAndPreActions(workflowName, this.xaml, this.codeBehind);
						if (DDIVUtil.IsVariableUsedInCode(code, variable.Name))
						{
							list.Add(string.Format("Variable '{0}', Workflow {1}. Please register dependency via RbacDependenciesForNew/RbacDependenciesForSet or set the SetRoles to NA", variable.Name + " " + variable.DataObjectName, workflow.Name));
						}
					}
				}
			}
			return list;
		}

		// Token: 0x04001D5A RID: 7514
		private string codeBehind;

		// Token: 0x04001D5B RID: 7515
		private string xaml;
	}
}
