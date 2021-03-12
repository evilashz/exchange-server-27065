using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Windows.Markup;
using Microsoft.Exchange.Management.SystemManager.WinForms;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x0200014E RID: 334
	[ContentProperty("DefaultValues")]
	public class NewDefaultObject : Activity
	{
		// Token: 0x06002157 RID: 8535 RVA: 0x0006464B File Offset: 0x0006284B
		public NewDefaultObject()
		{
		}

		// Token: 0x06002158 RID: 8536 RVA: 0x0006466C File Offset: 0x0006286C
		protected NewDefaultObject(NewDefaultObject activity) : base(activity)
		{
			this.DataObjectNames = activity.DataObjectNames;
			this.DefaultValues = new Collection<Set>((from c in activity.DefaultValues
			select c.Clone() as Set).ToList<Set>());
		}

		// Token: 0x06002159 RID: 8537 RVA: 0x000646CF File Offset: 0x000628CF
		public override Activity Clone()
		{
			return new NewDefaultObject(this);
		}

		// Token: 0x17001A67 RID: 6759
		// (get) Token: 0x0600215A RID: 8538 RVA: 0x000646D7 File Offset: 0x000628D7
		// (set) Token: 0x0600215B RID: 8539 RVA: 0x000646DF File Offset: 0x000628DF
		[DDICollectionDecorator(AttributeType = typeof(DDIVariableNameExistAttribute), ObjectConverter = typeof(DDISetToVariableConverter))]
		public Collection<Set> DefaultValues
		{
			get
			{
				return this.sets;
			}
			set
			{
				this.sets = value;
			}
		}

		// Token: 0x17001A68 RID: 6760
		// (get) Token: 0x0600215C RID: 8540 RVA: 0x000646E8 File Offset: 0x000628E8
		// (set) Token: 0x0600215D RID: 8541 RVA: 0x000646F0 File Offset: 0x000628F0
		public string DataObjectNames { get; set; }

		// Token: 0x0600215E RID: 8542 RVA: 0x000646FC File Offset: 0x000628FC
		public override RunResult Run(DataRow input, DataTable dataTable, DataObjectStore store, Type codeBehind, Workflow.UpdateTableDelegate updateTableDelegate)
		{
			RunResult runResult = new RunResult();
			foreach (string text in this.GetDataObjectKeys(store))
			{
				object obj = null;
				IDataObjectCreator dataObjectCreator = store.GetDataObjectCreator(text);
				if (dataObjectCreator != null)
				{
					obj = dataObjectCreator.Create(dataTable);
				}
				else
				{
					Type dataObjectType = store.GetDataObjectType(text);
					if (null != dataObjectType)
					{
						obj = dataObjectType.GetConstructor(new Type[0]).Invoke(new object[0]);
					}
				}
				if (obj != null)
				{
					store.UpdateDataObject(text, obj, true);
					updateTableDelegate(text, false);
				}
			}
			runResult.DataObjectes.AddRange(store.GetKeys());
			return runResult;
		}

		// Token: 0x0600215F RID: 8543 RVA: 0x000647B8 File Offset: 0x000629B8
		private IEnumerable<string> GetDataObjectKeys(DataObjectStore store)
		{
			if (string.IsNullOrEmpty(this.DataObjectNames))
			{
				return store.GetKeys();
			}
			IList<string> list = new List<string>();
			foreach (string text in this.DataObjectNames.Split(new char[]
			{
				','
			}))
			{
				list.Add(text.Trim());
			}
			return list;
		}

		// Token: 0x06002160 RID: 8544 RVA: 0x0006481C File Offset: 0x00062A1C
		protected override void DoPostRunCore(DataRow input, DataTable dataTable, DataObjectStore store, Type codeBehind)
		{
			Collection<Set> defaultValues = this.DefaultValues;
			NewDefaultObject.SetDefaultValues(input, dataTable.Rows[0], dataTable, defaultValues);
		}

		// Token: 0x06002161 RID: 8545 RVA: 0x00064844 File Offset: 0x00062A44
		internal static void SetDefaultValues(DataRow input, DataRow row, DataTable dataTable, Collection<Set> defaultValues)
		{
			if (defaultValues != null)
			{
				foreach (Set set in defaultValues)
				{
					object obj = set.Value;
					string text = obj as string;
					if (DDIHelper.IsLambdaExpression(text))
					{
						obj = ExpressionCalculator.CalculateLambdaExpression(ExpressionCalculator.BuildColumnExpression(text), typeof(object), DDIHelper.GetLambdaExpressionDataRow(dataTable), input);
					}
					else
					{
						VariableReference variableReference = obj as VariableReference;
						if (variableReference != null)
						{
							obj = DDIHelper.GetVariableValue(variableReference, input, dataTable);
						}
					}
					row[set.Variable] = obj;
				}
			}
		}

		// Token: 0x04001D21 RID: 7457
		private Collection<Set> sets = new Collection<Set>();
	}
}
