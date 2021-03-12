using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Management.SnapIn;
using Microsoft.Exchange.Management.SystemManager.WinForms;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x020000B0 RID: 176
	public class MonadPipelineSaveTask : Saver
	{
		// Token: 0x06000594 RID: 1428 RVA: 0x000150EC File Offset: 0x000132EC
		public MonadPipelineSaveTask(string commandText, string dataObjectName, string workUnitTextColumn, string workUnitIconColumn) : base(workUnitTextColumn, workUnitIconColumn)
		{
			this.CommandText = commandText;
			this.dataObjectName = dataObjectName;
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x00015105 File Offset: 0x00013305
		public MonadPipelineSaveTask() : this(null, null, null, null)
		{
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000596 RID: 1430 RVA: 0x00015111 File Offset: 0x00013311
		// (set) Token: 0x06000597 RID: 1431 RVA: 0x0001511C File Offset: 0x0001331C
		public string CommandText
		{
			get
			{
				return this.originalCommandText;
			}
			set
			{
				this.originalCommandText = value;
				if (value != null)
				{
					this.dataHandler = new SingleTaskDataHandler(string.Empty, new MonadConnection(PSConnectionInfoSingleton.GetInstance().GetConnectionStringForScript(), new CommandInteractionHandler(), ADServerSettingsSingleton.GetInstance().CreateRunspaceServerSettingsObject(), PSConnectionInfoSingleton.GetInstance().GetMonadConnectionInfo()));
					this.dataHandler.Command.CommandType = CommandType.Text;
				}
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000598 RID: 1432 RVA: 0x0001517C File Offset: 0x0001337C
		internal MonadCommand Command
		{
			get
			{
				return this.dataHandler.Command;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000599 RID: 1433 RVA: 0x00015189 File Offset: 0x00013389
		public override string CommandToRun
		{
			get
			{
				return this.dataHandler.CommandText;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x0600059A RID: 1434 RVA: 0x00015196 File Offset: 0x00013396
		public override List<object> SavedResults
		{
			get
			{
				return this.dataHandler.SavedResults;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x0600059B RID: 1435 RVA: 0x000151A3 File Offset: 0x000133A3
		public override string ModifiedParametersDescription
		{
			get
			{
				return this.modifiedParametersDescription;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x0600059C RID: 1436 RVA: 0x000151AB File Offset: 0x000133AB
		public override object WorkUnits
		{
			get
			{
				return this.dataHandler.WorkUnits;
			}
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x000151B8 File Offset: 0x000133B8
		public override void UpdateWorkUnits(DataRow row)
		{
			this.dataHandler.UpdateWorkUnits();
			if (!string.IsNullOrEmpty(base.WorkUnitTextColumn))
			{
				this.dataHandler.WorkUnit.Text = row[base.WorkUnitTextColumn].ToString();
			}
			this.dataHandler.WorkUnit.Description = this.ModifiedParametersDescription;
			if (!string.IsNullOrEmpty(base.WorkUnitIconColumn))
			{
				this.dataHandler.WorkUnit.Icon = WinformsHelper.GetIconFromIconLibrary(row[base.WorkUnitIconColumn].ToString());
			}
			this.dataHandler.ResetCancel();
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x00015254 File Offset: 0x00013454
		public override void BuildParameters(DataRow row, DataObjectStore store, IList<ParameterProfile> paramInfos)
		{
			this.dataHandler.Parameters.Clear();
			this.dataHandler.ClearParameterNames();
			this.modifiedParametersDescription = MonadSaveTask.BuildParametersDescription(row, paramInfos);
			this.dataHandler.CommandText = MonadPipelineSaveTask.BuildCommandScript(this.originalCommandText, row, paramInfos);
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x000152A4 File Offset: 0x000134A4
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
			if (!this.dataHandler.HasWorkUnits || !this.dataHandler.WorkUnits.HasFailures)
			{
				store.ClearModifiedColumns(row, this.dataObjectName);
			}
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x00015334 File Offset: 0x00013534
		public override void Cancel()
		{
			this.dataHandler.Cancel();
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x00015344 File Offset: 0x00013544
		public static string BuildCommandScript(string rawScript, DataRow row, IList<ParameterProfile> paramInfos)
		{
			IList<string> paramStrings = MonadPipelineSaveTask.BuildParameterStrings(row, paramInfos);
			return MonadPipelineSaveTask.ReplaceParameterPlaceholder(rawScript, paramStrings, paramInfos);
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x00015364 File Offset: 0x00013564
		private static IList<string> BuildParameterStrings(DataRow row, IList<ParameterProfile> paramInfos)
		{
			List<string> list = new List<string>();
			foreach (ParameterProfile parameterProfile in paramInfos)
			{
				if (parameterProfile.IsRunnable(row))
				{
					switch (parameterProfile.ParameterType)
					{
					case ParameterType.Switched:
						list.Add(string.Format("-{0} ", parameterProfile.Name));
						break;
					case ParameterType.Column:
					case ParameterType.ModifiedColumn:
					{
						string arg = MonadCommand.FormatParameterValue(MonadSaveTask.ConvertToParameterValue(row, parameterProfile));
						list.Add(string.Format("-{0} {1} ", parameterProfile.Name, arg));
						break;
					}
					default:
						list.Add(string.Empty);
						break;
					}
				}
				else
				{
					list.Add(string.Empty);
				}
			}
			return list;
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x00015484 File Offset: 0x00013684
		private static string ReplaceParameterPlaceholder(string rawScript, IList<string> paramStrings, IList<ParameterProfile> paramInfos)
		{
			return MonadPipelineSaveTask.placeholderRegex.Replace(rawScript, delegate(Match match)
			{
				string text = match.Value.Substring(1, match.Value.Length - 2);
				int parameterIndex;
				if (!int.TryParse(text, out parameterIndex))
				{
					string paramName = text;
					parameterIndex = MonadPipelineSaveTask.GetParameterIndex(paramName, paramInfos);
				}
				return paramStrings[parameterIndex];
			});
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x000154BC File Offset: 0x000136BC
		private static int GetParameterIndex(string paramName, IList<ParameterProfile> paramInfos)
		{
			int num = -1;
			for (int i = 0; i < paramInfos.Count; i++)
			{
				if (paramInfos[i].Name == paramName)
				{
					if (num != -1)
					{
						throw new NotSupportedException(string.Format("Multiple parameters are found for '{0}'.", paramName));
					}
					num = i;
				}
			}
			if (num == -1)
			{
				throw new NotSupportedException(string.Format("Parameter '{0}' is not found.", paramName));
			}
			return num;
		}

		// Token: 0x040001D0 RID: 464
		private SingleTaskDataHandler dataHandler;

		// Token: 0x040001D1 RID: 465
		private string originalCommandText;

		// Token: 0x040001D2 RID: 466
		private string dataObjectName;

		// Token: 0x040001D3 RID: 467
		private string modifiedParametersDescription;

		// Token: 0x040001D4 RID: 468
		private static Regex placeholderRegex = new Regex("\\{\\w+\\}");
	}
}
