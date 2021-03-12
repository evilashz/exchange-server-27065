using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using Microsoft.Exchange.Management.SnapIn;
using Microsoft.Exchange.Management.SystemManager.WinForms;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000151 RID: 337
	[DDIParameter]
	public class Parameter : ICloneable
	{
		// Token: 0x06002169 RID: 8553 RVA: 0x0006493C File Offset: 0x00062B3C
		public Parameter()
		{
		}

		// Token: 0x0600216A RID: 8554 RVA: 0x0006494B File Offset: 0x00062B4B
		public Parameter(string name, string reference, ParameterType type, IRunnable runnableTester)
		{
			this.name = name;
			this.reference = reference;
			this.parameterType = type;
			this.runnableTester = runnableTester;
		}

		// Token: 0x17001A6B RID: 6763
		// (get) Token: 0x0600216B RID: 8555 RVA: 0x00064977 File Offset: 0x00062B77
		// (set) Token: 0x0600216C RID: 8556 RVA: 0x0006497F File Offset: 0x00062B7F
		[DDIMandatoryValue]
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x17001A6C RID: 6764
		// (get) Token: 0x0600216D RID: 8557 RVA: 0x00064988 File Offset: 0x00062B88
		// (set) Token: 0x0600216E RID: 8558 RVA: 0x00064990 File Offset: 0x00062B90
		[DefaultValue(null)]
		[DDIVariableNameExist]
		public string Reference
		{
			get
			{
				return this.reference;
			}
			set
			{
				this.reference = value;
			}
		}

		// Token: 0x17001A6D RID: 6765
		// (get) Token: 0x0600216F RID: 8559 RVA: 0x00064999 File Offset: 0x00062B99
		// (set) Token: 0x06002170 RID: 8560 RVA: 0x000649A1 File Offset: 0x00062BA1
		[DDIValidLambdaExpression]
		[DefaultValue(null)]
		public object Value { get; set; }

		// Token: 0x17001A6E RID: 6766
		// (get) Token: 0x06002171 RID: 8561 RVA: 0x000649AA File Offset: 0x00062BAA
		// (set) Token: 0x06002172 RID: 8562 RVA: 0x000649B2 File Offset: 0x00062BB2
		[DefaultValue(ParameterType.Mandatory)]
		public ParameterType Type
		{
			get
			{
				return this.parameterType;
			}
			set
			{
				this.parameterType = value;
			}
		}

		// Token: 0x17001A6F RID: 6767
		// (get) Token: 0x06002173 RID: 8563 RVA: 0x000649BB File Offset: 0x00062BBB
		// (set) Token: 0x06002174 RID: 8564 RVA: 0x000649C3 File Offset: 0x00062BC3
		[DefaultValue(null)]
		public IRunnable RunnableTester
		{
			get
			{
				return this.runnableTester;
			}
			set
			{
				this.runnableTester = value;
			}
		}

		// Token: 0x17001A70 RID: 6768
		// (get) Token: 0x06002175 RID: 8565 RVA: 0x000649CC File Offset: 0x00062BCC
		// (set) Token: 0x06002176 RID: 8566 RVA: 0x000649D4 File Offset: 0x00062BD4
		[DDIValidLambdaExpression]
		[DefaultValue(null)]
		public string Condition { get; set; }

		// Token: 0x17001A71 RID: 6769
		// (get) Token: 0x06002177 RID: 8567 RVA: 0x000649DD File Offset: 0x00062BDD
		// (set) Token: 0x06002178 RID: 8568 RVA: 0x000649E5 File Offset: 0x00062BE5
		[DefaultValue(null)]
		[TypeConverter(typeof(OrganizationTypesConverter))]
		public OrganizationType[] OrganizationTypes { get; set; }

		// Token: 0x06002179 RID: 8569 RVA: 0x000649F0 File Offset: 0x00062BF0
		public static object ConvertToParameterValue(DataRow input, DataTable dataTable, Parameter paramInfo, DataObjectStore store)
		{
			string variableName = paramInfo.Reference ?? paramInfo.Name;
			object obj;
			if (paramInfo.Value == null)
			{
				obj = DDIHelper.GetVariableValue(store.ModifiedColumns, variableName, input, dataTable, store.IsGetListWorkflow);
			}
			else
			{
				string text = paramInfo.Value as string;
				if (DDIHelper.IsLambdaExpression(text))
				{
					obj = ExpressionCalculator.CalculateLambdaExpression(ExpressionCalculator.BuildColumnExpression(text), typeof(object), DDIHelper.GetLambdaExpressionDataRow(dataTable), input);
				}
				else
				{
					VariableReference variableReference = paramInfo.Value as VariableReference;
					if (variableReference != null)
					{
						obj = DDIHelper.GetVariableValue(variableReference, input, dataTable);
					}
					else
					{
						obj = paramInfo.Value;
					}
				}
			}
			if (obj == DBNull.Value)
			{
				return null;
			}
			return obj;
		}

		// Token: 0x0600217A RID: 8570 RVA: 0x00064A90 File Offset: 0x00062C90
		public object Clone()
		{
			return new Parameter(this.name, this.reference, this.parameterType, this.runnableTester)
			{
				Value = this.Value,
				Condition = this.Condition,
				OrganizationTypes = this.OrganizationTypes
			};
		}

		// Token: 0x0600217B RID: 8571 RVA: 0x00064AE0 File Offset: 0x00062CE0
		public override bool Equals(object obj)
		{
			Parameter parameter = obj as Parameter;
			return parameter != null && parameter.Name == this.Name;
		}

		// Token: 0x0600217C RID: 8572 RVA: 0x00064B0A File Offset: 0x00062D0A
		public override int GetHashCode()
		{
			if (this.Name == null)
			{
				return 0;
			}
			return this.Name.GetHashCode();
		}

		// Token: 0x0600217D RID: 8573 RVA: 0x00064B24 File Offset: 0x00062D24
		public bool IsRunnable(DataRow input, DataTable dataTable)
		{
			DataObjectStore dataObjectStore = dataTable.ExtendedProperties["DataSourceStore"] as DataObjectStore;
			if (this.Type == ParameterType.RunOnModified)
			{
				if (this.Value != null)
				{
					VariableReference variableReference = this.Value as VariableReference;
					if (variableReference != null && !dataObjectStore.ModifiedColumns.Contains(variableReference.Variable))
					{
						return false;
					}
					string text = this.Value as string;
					if (DDIHelper.IsLambdaExpression(text))
					{
						List<string> dependentColumns = ExpressionCalculator.BuildColumnExpression(text).DependentColumns;
						bool flag = false;
						foreach (string item in dependentColumns)
						{
							if (dataObjectStore != null && dataObjectStore.ModifiedColumns.Contains(item))
							{
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							return false;
						}
					}
				}
				else if (!dataObjectStore.ModifiedColumns.Contains(this.Reference ?? this.Name))
				{
					return false;
				}
			}
			if (!string.IsNullOrEmpty(this.Condition))
			{
				return (bool)ExpressionCalculator.CalculateLambdaExpression(ExpressionCalculator.BuildColumnExpression(this.Condition), typeof(bool), DDIHelper.GetLambdaExpressionDataRow(dataTable), input);
			}
			return this.runnableTester == null || this.runnableTester.IsRunnable(input, dataTable);
		}

		// Token: 0x0600217E RID: 8574 RVA: 0x00064C80 File Offset: 0x00062E80
		internal bool IsAccessingVariable(string accessingVariable)
		{
			List<string> list = new List<string>();
			if (this.Value != null)
			{
				VariableReference variableReference = this.Value as VariableReference;
				if (variableReference != null)
				{
					list.Add(variableReference.Variable);
				}
				else
				{
					string text = this.Value as string;
					if (DDIHelper.IsLambdaExpression(text))
					{
						list.AddRange(ExpressionCalculator.BuildColumnExpression(text).DependentColumns);
					}
				}
			}
			else
			{
				list.Add(this.Reference ?? this.Name);
			}
			return list.Any((string c) => string.Equals(c, accessingVariable, StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x04001D2A RID: 7466
		private string name;

		// Token: 0x04001D2B RID: 7467
		private string reference;

		// Token: 0x04001D2C RID: 7468
		private ParameterType parameterType = ParameterType.Mandatory;

		// Token: 0x04001D2D RID: 7469
		private IRunnable runnableTester;
	}
}
