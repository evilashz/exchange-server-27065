using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Management.SystemManager.WinForms;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x020000D3 RID: 211
	public class GroupSaveTask : Saver
	{
		// Token: 0x0600075D RID: 1885 RVA: 0x00018DA4 File Offset: 0x00016FA4
		public GroupSaveTask(string commandText, string workUnitTextColumn, string workUnitIconColumn, string commandParam, string groupColumnName, string groupLambdaExpression) : base(workUnitTextColumn, workUnitIconColumn)
		{
			this.CommandText = commandText;
			this.CommandParam = commandParam;
			this.GroupColumnName = groupColumnName;
			this.GroupLambdaExpression = groupLambdaExpression;
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x00018DF0 File Offset: 0x00016FF0
		public GroupSaveTask() : base(null, null)
		{
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x0600075F RID: 1887 RVA: 0x00018E12 File Offset: 0x00017012
		// (set) Token: 0x06000760 RID: 1888 RVA: 0x00018E1A File Offset: 0x0001701A
		[DefaultValue(null)]
		public string CommandText { get; set; }

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000761 RID: 1889 RVA: 0x00018E23 File Offset: 0x00017023
		// (set) Token: 0x06000762 RID: 1890 RVA: 0x00018E2B File Offset: 0x0001702B
		[DefaultValue(null)]
		public string CommandParam { get; set; }

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000763 RID: 1891 RVA: 0x00018E34 File Offset: 0x00017034
		// (set) Token: 0x06000764 RID: 1892 RVA: 0x00018E3C File Offset: 0x0001703C
		[DefaultValue(null)]
		[DDIDataColumnExist]
		public string GroupColumnName { get; set; }

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000765 RID: 1893 RVA: 0x00018E45 File Offset: 0x00017045
		// (set) Token: 0x06000766 RID: 1894 RVA: 0x00018E4D File Offset: 0x0001704D
		[DDIValidLambdaExpression]
		public string GroupLambdaExpression
		{
			get
			{
				return this.groupLambdaExpression;
			}
			set
			{
				this.groupLambdaExpression = value;
				this.groupColumnExpression = ExpressionCalculator.BuildColumnExpression(value);
			}
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x00018E62 File Offset: 0x00017062
		public override void Cancel()
		{
			this.dataHandler.Cancel();
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x00018E70 File Offset: 0x00017070
		public override void Run(object interactionHandler, DataRow row, DataObjectStore store)
		{
			this.dataHandler.ProgressReport += base.OnProgressReport;
			try
			{
				this.dataHandler.Save(interactionHandler as CommandInteractionHandler);
			}
			finally
			{
				this.dataHandler.ProgressReport -= base.OnProgressReport;
			}
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x00018ED0 File Offset: 0x000170D0
		public override bool IsRunnable(DataRow row, DataObjectStore store)
		{
			IList values = this.GetValues(row);
			return values != null && values.Count > 0 && base.IsRunnable(row, store);
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x00018F04 File Offset: 0x00017104
		private IList GetValues(DataRow row)
		{
			IList result;
			if (!string.IsNullOrEmpty(this.GroupColumnName))
			{
				result = (row[this.GroupColumnName] as IList);
			}
			else
			{
				result = (ExpressionCalculator.CalculateLambdaExpression(this.groupColumnExpression, typeof(IList), row, null) as IList);
			}
			return result;
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x0600076B RID: 1899 RVA: 0x00018F52 File Offset: 0x00017152
		public override object WorkUnits
		{
			get
			{
				return this.dataHandler.WorkUnits;
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x0600076C RID: 1900 RVA: 0x00018F5F File Offset: 0x0001715F
		public override List<object> SavedResults
		{
			get
			{
				return this.dataHandler.SavedResults;
			}
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x00018F6C File Offset: 0x0001716C
		public override void UpdateWorkUnits(DataRow row)
		{
			this.groupValues = this.GetValues(row);
			if (!string.IsNullOrEmpty(base.WorkUnitTextColumn))
			{
				this.workUnitsTextList = (row[base.WorkUnitTextColumn] as IList);
			}
			if (this.groupValues != null && this.groupValues.Count > 0)
			{
				this.dataHandler.DataHandlers.Clear();
				if (string.IsNullOrEmpty(this.CommandParam))
				{
					WorkUnit[] array = new WorkUnit[this.groupValues.Count];
					for (int i = 0; i < this.groupValues.Count; i++)
					{
						array[i] = new WorkUnit(this.GetDisplayText(i), this.GetDisplayIcon(row), this.groupValues[i]);
					}
					this.dataHandler.DataHandlers.Add(new BulkSaveDataHandler(array.DeepCopy(), this.CommandText));
				}
				else
				{
					for (int j = 0; j < this.groupValues.Count; j++)
					{
						this.dataHandler.DataHandlers.Add(this.CreateDataHandler(this.CommandText, j, this.GetDisplayIcon(row)));
					}
				}
				this.dataHandler.UpdateWorkUnits();
				foreach (WorkUnit workUnit in this.dataHandler.WorkUnits)
				{
					workUnit.Description = this.ModifiedParametersDescription;
				}
				this.dataHandler.ResetCancel();
			}
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x000190F0 File Offset: 0x000172F0
		internal void UpdateConnection(MonadConnection connection)
		{
			foreach (DataHandler dataHandler in this.dataHandler.DataHandlers)
			{
				SingleTaskDataHandler singleTaskDataHandler = (SingleTaskDataHandler)dataHandler;
				singleTaskDataHandler.Command.Connection = connection;
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x0600076F RID: 1903 RVA: 0x0001914C File Offset: 0x0001734C
		public override string CommandToRun
		{
			get
			{
				return this.dataHandler.CommandToRun;
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000770 RID: 1904 RVA: 0x00019159 File Offset: 0x00017359
		public override string ModifiedParametersDescription
		{
			get
			{
				return this.modifiedParametersDescription;
			}
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x00019164 File Offset: 0x00017364
		public override void BuildParameters(DataRow row, DataObjectStore store, IList<ParameterProfile> paramInfos)
		{
			foreach (DataHandler dataHandler in this.dataHandler.DataHandlers)
			{
				SingleTaskDataHandler singleTaskDataHandler = (SingleTaskDataHandler)dataHandler;
				foreach (ParameterProfile parameterProfile in paramInfos)
				{
					singleTaskDataHandler.Parameters.Remove(parameterProfile.Name);
				}
				MonadSaveTask.BuildParametersCore(row, paramInfos, singleTaskDataHandler.Parameters);
			}
			this.modifiedParametersDescription = MonadSaveTask.BuildParametersDescription(row, paramInfos);
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x00019210 File Offset: 0x00017410
		private SingleTaskDataHandler CreateDataHandler(string commandText, int index, Icon displayIcon)
		{
			SingleTaskDataHandler singleTaskDataHandler;
			if (this.workUnits.Length > 0)
			{
				singleTaskDataHandler = new BulkSaveDataHandler(this.workUnits.DeepCopy(), commandText);
			}
			else
			{
				singleTaskDataHandler = new SingleTaskDataHandler(commandText);
				singleTaskDataHandler.WorkUnit.Text = this.GetDisplayText(index);
				singleTaskDataHandler.WorkUnit.Icon = displayIcon;
			}
			singleTaskDataHandler.Parameters.AddWithValue(this.CommandParam, this.groupValues[index]);
			return singleTaskDataHandler;
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x00019280 File Offset: 0x00017480
		private string GetDisplayText(int index)
		{
			object obj = string.IsNullOrEmpty(base.WorkUnitTextColumn) ? this.groupValues[index] : this.workUnitsTextList[index];
			ADObjectId adobjectId = obj as ADObjectId;
			if (adobjectId != null)
			{
				return adobjectId.Name;
			}
			if (obj != null)
			{
				return obj.ToString();
			}
			return string.Empty;
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x000192D5 File Offset: 0x000174D5
		private Icon GetDisplayIcon(DataRow row)
		{
			if (string.IsNullOrEmpty(base.WorkUnitIconColumn))
			{
				return null;
			}
			return WinformsHelper.GetIconFromIconLibrary(row[base.WorkUnitIconColumn].ToString());
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x000192FC File Offset: 0x000174FC
		public override Saver CreateBulkSaver(WorkUnit[] workUnits)
		{
			this.workUnits = workUnits;
			return this;
		}

		// Token: 0x04000383 RID: 899
		private DataHandler dataHandler = new DataHandler(false);

		// Token: 0x04000384 RID: 900
		private WorkUnit[] workUnits = new WorkUnit[0];

		// Token: 0x04000385 RID: 901
		private ColumnExpression groupColumnExpression;

		// Token: 0x04000386 RID: 902
		private string modifiedParametersDescription;

		// Token: 0x04000387 RID: 903
		private string groupLambdaExpression;

		// Token: 0x04000388 RID: 904
		private IList workUnitsTextList;

		// Token: 0x04000389 RID: 905
		private IList groupValues;
	}
}
