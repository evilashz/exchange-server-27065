using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Management.SnapIn;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020000AE RID: 174
	public class MonadSaveTask : Saver
	{
		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000573 RID: 1395 RVA: 0x00014A93 File Offset: 0x00012C93
		internal MonadCommand Command
		{
			get
			{
				return this.DataHandler.Command;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000574 RID: 1396 RVA: 0x00014AA0 File Offset: 0x00012CA0
		// (set) Token: 0x06000575 RID: 1397 RVA: 0x00014AA8 File Offset: 0x00012CA8
		[DDIDataObjectNameExist]
		public string DataObjectName { get; set; }

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000576 RID: 1398 RVA: 0x00014AB1 File Offset: 0x00012CB1
		// (set) Token: 0x06000577 RID: 1399 RVA: 0x00014AB9 File Offset: 0x00012CB9
		[DDIValidLambdaExpression]
		public string CommandTextLambdaExpression
		{
			get
			{
				return this.commandTextLambdaExpression;
			}
			set
			{
				this.commandTextLambdaExpression = value;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000578 RID: 1400 RVA: 0x00014AC2 File Offset: 0x00012CC2
		// (set) Token: 0x06000579 RID: 1401 RVA: 0x00014ACA File Offset: 0x00012CCA
		[DDIValidCommandText]
		public string CommandText
		{
			get
			{
				return this.commandText;
			}
			set
			{
				this.commandText = value;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600057A RID: 1402 RVA: 0x00014AD3 File Offset: 0x00012CD3
		private SingleTaskDataHandler DataHandler
		{
			get
			{
				if (this.dataHandler == null)
				{
					this.dataHandler = new SingleTaskDataHandler(this.CommandText);
				}
				return this.dataHandler;
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x0600057B RID: 1403 RVA: 0x00014AF4 File Offset: 0x00012CF4
		// (set) Token: 0x0600057C RID: 1404 RVA: 0x00014AFC File Offset: 0x00012CFC
		[DefaultValue("")]
		[DDIDataColumnExist]
		public string WorkUnitDescriptionColumn { get; set; }

		// Token: 0x0600057D RID: 1405 RVA: 0x00014B05 File Offset: 0x00012D05
		public MonadSaveTask() : base(null, null)
		{
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x00014B0F File Offset: 0x00012D0F
		public MonadSaveTask(string commandText, string dataObjectName, string workUnitTextColumn, string workUnitIconColumn) : base(workUnitTextColumn, workUnitIconColumn)
		{
			this.CommandText = commandText;
			this.DataObjectName = dataObjectName;
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x00014B28 File Offset: 0x00012D28
		public override void Cancel()
		{
			this.DataHandler.Cancel();
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x00014B38 File Offset: 0x00012D38
		public override void Run(object interactionHandler, DataRow row, DataObjectStore store)
		{
			this.DataHandler.ProgressReport += base.OnProgressReport;
			try
			{
				this.DataHandler.Save(interactionHandler as CommandInteractionHandler);
			}
			finally
			{
				this.DataHandler.ProgressReport -= base.OnProgressReport;
			}
			if (!this.DataHandler.HasWorkUnits || !this.DataHandler.WorkUnits.HasFailures)
			{
				store.ClearModifiedColumns(row, this.DataObjectName);
			}
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x00014BC8 File Offset: 0x00012DC8
		public override bool IsRunnable(DataRow row, DataObjectStore store)
		{
			bool flag = true;
			if (!string.IsNullOrEmpty(this.DataObjectName))
			{
				flag = (store.GetModifiedPropertiesBasedOnDataObject(row, this.DataObjectName).Count > 0);
			}
			return flag && base.IsRunnable(row, store);
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000582 RID: 1410 RVA: 0x00014C07 File Offset: 0x00012E07
		public override object WorkUnits
		{
			get
			{
				return this.DataHandler.WorkUnits;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000583 RID: 1411 RVA: 0x00014C14 File Offset: 0x00012E14
		public override List<object> SavedResults
		{
			get
			{
				return this.DataHandler.SavedResults;
			}
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x00014C24 File Offset: 0x00012E24
		public override void UpdateWorkUnits(DataRow row)
		{
			this.DataHandler.UpdateWorkUnits();
			if (!string.IsNullOrEmpty(base.WorkUnitTextColumn))
			{
				this.DataHandler.WorkUnit.Text = row[base.WorkUnitTextColumn].ToString();
			}
			else if (!string.IsNullOrEmpty(this.CommandTextLambdaExpression))
			{
				this.DataHandler.WorkUnit.Text = this.DataHandler.CommandText;
			}
			if (string.IsNullOrEmpty(this.WorkUnitDescriptionColumn))
			{
				this.DataHandler.WorkUnit.Description = this.workUnitDescription;
			}
			else
			{
				this.DataHandler.WorkUnit.Description = row[this.WorkUnitDescriptionColumn].ToString();
			}
			if (!string.IsNullOrEmpty(base.WorkUnitIconColumn))
			{
				this.DataHandler.WorkUnit.Icon = WinformsHelper.GetIconFromIconLibrary(row[base.WorkUnitIconColumn].ToString());
			}
			this.DataHandler.ResetCancel();
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000585 RID: 1413 RVA: 0x00014D18 File Offset: 0x00012F18
		public override string CommandToRun
		{
			get
			{
				return this.DataHandler.CommandToRun;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000586 RID: 1414 RVA: 0x00014D25 File Offset: 0x00012F25
		public override string ModifiedParametersDescription
		{
			get
			{
				return this.DataHandler.ModifiedParametersDescription;
			}
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x00014D34 File Offset: 0x00012F34
		public override void BuildParameters(DataRow row, DataObjectStore store, IList<ParameterProfile> paramInfos)
		{
			if (!string.IsNullOrEmpty(this.CommandTextLambdaExpression))
			{
				this.DataHandler.CommandText = (string)ExpressionCalculator.CalculateLambdaExpression(ExpressionCalculator.BuildColumnExpression(this.CommandTextLambdaExpression), typeof(string), row, null);
			}
			this.DataHandler.Parameters.Clear();
			if (!string.IsNullOrEmpty(this.DataObjectName))
			{
				this.DataHandler.Parameters.AddWithValue("Instance", store.GetDataObject(this.DataObjectName));
			}
			else
			{
				this.DataHandler.KeepInstanceParamerter = true;
			}
			MonadSaveTask.BuildParametersCore(row, paramInfos, this.DataHandler.Parameters);
			this.workUnitDescription = MonadSaveTask.BuildParametersDescription(row, paramInfos);
			this.DataHandler.ClearParameterNames();
			this.DataHandler.SpecifyParameterNames(store.GetModifiedPropertiesBasedOnDataObject(row, this.DataObjectName));
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x00014E0C File Offset: 0x0001300C
		public static string BuildParametersDescription(DataRow row, IList<ParameterProfile> paramInfos)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (ParameterProfile parameterProfile in paramInfos)
			{
				if (parameterProfile.IsRunnable(row) && !parameterProfile.HideDisplay)
				{
					switch (parameterProfile.ParameterType)
					{
					case ParameterType.Switched:
						stringBuilder.Append(Strings.NameValueFormat(parameterProfile.Name, string.Empty));
						break;
					case ParameterType.Column:
					case ParameterType.ModifiedColumn:
					{
						object value = MonadSaveTask.ConvertToParameterValue(row, parameterProfile);
						stringBuilder.Append(Strings.NameValueFormat(parameterProfile.Name, MonadCommand.FormatParameterValue(value)));
						break;
					}
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x00014ED0 File Offset: 0x000130D0
		public static void BuildParametersCore(DataRow row, IList<ParameterProfile> paramInfos, object parameters)
		{
			MonadParameterCollection monadParameterCollection = parameters as MonadParameterCollection;
			foreach (ParameterProfile parameterProfile in paramInfos)
			{
				if (parameterProfile.IsRunnable(row))
				{
					switch (parameterProfile.ParameterType)
					{
					case ParameterType.Switched:
						monadParameterCollection.AddSwitch(parameterProfile.Name);
						break;
					case ParameterType.Column:
					case ParameterType.ModifiedColumn:
					{
						object value = MonadSaveTask.ConvertToParameterValue(row, parameterProfile);
						monadParameterCollection.AddWithValue(parameterProfile.Name, value);
						break;
					}
					}
				}
			}
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x00014F68 File Offset: 0x00013168
		public static object ConvertToParameterValue(DataRow row, string parameterName)
		{
			object obj = row[parameterName];
			if (DBNull.Value.Equals(obj) && row.Table != null && !typeof(string).Equals(row.Table.Columns[parameterName].GetType()))
			{
				obj = null;
			}
			return obj;
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x00014FBC File Offset: 0x000131BC
		public static object ConvertToParameterValue(DataRow row, ParameterProfile paramInfo)
		{
			if (string.IsNullOrEmpty(paramInfo.LambdaExpression))
			{
				return MonadSaveTask.ConvertToParameterValue(row, paramInfo.Reference);
			}
			return ExpressionCalculator.CalculateLambdaExpression(ExpressionCalculator.BuildColumnExpression(paramInfo.LambdaExpression), typeof(object), row, null);
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x00014FF4 File Offset: 0x000131F4
		public override Saver CreateBulkSaver(WorkUnit[] workunits)
		{
			this.dataHandler = new BulkSaveDataHandler(workunits, this.DataHandler.CommandText);
			return this;
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x0600058D RID: 1421 RVA: 0x0001500E File Offset: 0x0001320E
		// (set) Token: 0x0600058E RID: 1422 RVA: 0x00015016 File Offset: 0x00013216
		public bool IgnoreIdentityParameter { get; set; }

		// Token: 0x0600058F RID: 1423 RVA: 0x00015040 File Offset: 0x00013240
		public override bool HasPermission(string propertyName, IList<ParameterProfile> parameters)
		{
			ICollection<string> parameterList = EMCRunspaceConfigurationSingleton.GetInstance().GetAllowedParamsForSetCmdlet(this.commandText, null);
			return parameterList != null && parameterList.Contains(propertyName, new CaseInSensitveComparer()) && (from c in parameters
			where parameterList.Contains(c.Name, new CaseInSensitveComparer())
			select c).Count<ParameterProfile>() == parameters.Count && (this.IgnoreIdentityParameter || parameterList.Contains("Identity", new CaseInSensitveComparer()));
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x000150C5 File Offset: 0x000132C5
		public override string GetConsumedDataObjectName()
		{
			return this.DataObjectName;
		}

		// Token: 0x040001C9 RID: 457
		private SingleTaskDataHandler dataHandler;

		// Token: 0x040001CA RID: 458
		private string workUnitDescription;

		// Token: 0x040001CB RID: 459
		private string commandText;

		// Token: 0x040001CC RID: 460
		private string commandTextLambdaExpression;
	}
}
