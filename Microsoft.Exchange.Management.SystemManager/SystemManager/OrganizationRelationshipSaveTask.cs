using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Management.SnapIn;
using Microsoft.Exchange.Management.SystemManager.WinForms;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x020000ED RID: 237
	public class OrganizationRelationshipSaveTask : Saver
	{
		// Token: 0x060008FC RID: 2300 RVA: 0x0001D600 File Offset: 0x0001B800
		public OrganizationRelationshipSaveTask(string commandText, string dataObjectName, string workUnitTextColumn, string workUnitIconColumn) : base(workUnitTextColumn, workUnitIconColumn)
		{
			this.CommandText = commandText;
			this.dataObjectName = dataObjectName;
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x060008FD RID: 2301 RVA: 0x0001D650 File Offset: 0x0001B850
		// (set) Token: 0x060008FE RID: 2302 RVA: 0x0001D658 File Offset: 0x0001B858
		public string CommandText
		{
			get
			{
				return this.originalCommandText;
			}
			set
			{
				this.originalCommandText = value;
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x060008FF RID: 2303 RVA: 0x0001D664 File Offset: 0x0001B864
		private SingleTaskDataHandler DataHandler
		{
			get
			{
				if (this.dataHandler == null)
				{
					this.dataHandler = new SingleTaskDataHandler(this.CommandText, new MonadConnection("pooled=false", new CommandInteractionHandler(), ADServerSettingsSingleton.GetInstance().CreateRunspaceServerSettingsObject(), PSConnectionInfoSingleton.GetInstance().GetMonadConnectionInfo()));
					this.dataHandler.Command.CommandType = CommandType.Text;
				}
				return this.dataHandler;
			}
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x0001D6C4 File Offset: 0x0001B8C4
		public OrganizationRelationshipSaveTask() : this(null, null, null, null)
		{
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x0001D6D0 File Offset: 0x0001B8D0
		public override void UpdateWorkUnits(DataRow row)
		{
			this.DataHandler.UpdateWorkUnits();
			if (!string.IsNullOrEmpty(base.WorkUnitTextColumn))
			{
				this.DataHandler.WorkUnit.Text = row[base.WorkUnitTextColumn].ToString();
			}
			this.DataHandler.WorkUnit.Description = this.ModifiedParametersDescription;
			if (!string.IsNullOrEmpty(base.WorkUnitIconColumn))
			{
				this.DataHandler.WorkUnit.Icon = WinformsHelper.GetIconFromIconLibrary(row[base.WorkUnitIconColumn].ToString());
			}
			this.DataHandler.ResetCancel();
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x0001D76A File Offset: 0x0001B96A
		public override void Cancel()
		{
			this.DataHandler.Cancel();
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x0001D778 File Offset: 0x0001B978
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
				store.ClearModifiedColumns(row, this.dataObjectName);
			}
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x0001D808 File Offset: 0x0001BA08
		public override void BuildParameters(DataRow row, DataObjectStore store, IList<ParameterProfile> paramInfos)
		{
			this.DataHandler.Parameters.Clear();
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			if ((bool)row[this.AutoDiscovery])
			{
				string text = MonadCommand.FormatParameterValue(row[this.AutoDiscoveryDomain]);
				stringBuilder2.AppendFormat(this.AutoDiscoveryCommandText, text);
				stringBuilder.Append(Strings.NameValueFormat(this.AutoDiscoveryDomain, text));
			}
			stringBuilder2.Append(this.originalCommandText);
			stringBuilder2.Append(" ");
			foreach (ParameterProfile parameterProfile in paramInfos)
			{
				if ((store.ModifiedColumns.Contains(parameterProfile.Reference) || this.Identity.Equals(parameterProfile.Name)) && parameterProfile.IsRunnable(row))
				{
					switch (parameterProfile.ParameterType)
					{
					case ParameterType.Switched:
						stringBuilder2.AppendFormat("-{0} ", parameterProfile.Name);
						stringBuilder.AppendLine(parameterProfile.Name);
						break;
					case ParameterType.Column:
					case ParameterType.ModifiedColumn:
					{
						string text2 = MonadCommand.FormatParameterValue(MonadSaveTask.ConvertToParameterValue(row, parameterProfile.Reference));
						stringBuilder.Append(Strings.NameValueFormat(parameterProfile.Name, text2));
						if (this.Identity.Equals(parameterProfile.Name) && row[parameterProfile.Reference] is ADObjectId)
						{
							text2 = string.Format("'{0}'", ((ADObjectId)row[parameterProfile.Reference]).ObjectGuid.ToString());
						}
						stringBuilder2.AppendFormat("-{0} {1} ", parameterProfile.Name, text2);
						break;
					}
					}
				}
			}
			this.modifiedParametersDescription = stringBuilder.ToString();
			this.DataHandler.CommandText = stringBuilder2.ToString();
			this.DataHandler.ClearParameterNames();
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x0001DA1C File Offset: 0x0001BC1C
		public override bool IsRunnable(DataRow row, DataObjectStore store)
		{
			bool flag = (bool)row[this.AutoDiscovery];
			if (!flag)
			{
				int num = store.ModifiedColumns.Count;
				if (store.ModifiedColumns.Contains(this.AutoDiscovery))
				{
					num--;
				}
				if (store.ModifiedColumns.Contains(this.AutoDiscoveryDomain))
				{
					num--;
				}
				flag = (num > 0);
			}
			return flag;
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000906 RID: 2310 RVA: 0x0001DA7E File Offset: 0x0001BC7E
		public override object WorkUnits
		{
			get
			{
				return this.DataHandler.WorkUnits;
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000907 RID: 2311 RVA: 0x0001DA8B File Offset: 0x0001BC8B
		public override List<object> SavedResults
		{
			get
			{
				return this.DataHandler.SavedResults;
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000908 RID: 2312 RVA: 0x0001DA98 File Offset: 0x0001BC98
		public override string CommandToRun
		{
			get
			{
				return this.DataHandler.CommandText;
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000909 RID: 2313 RVA: 0x0001DAA5 File Offset: 0x0001BCA5
		public override string ModifiedParametersDescription
		{
			get
			{
				return this.modifiedParametersDescription;
			}
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x0001DAB0 File Offset: 0x0001BCB0
		public override bool HasPermission(string propertyName, IList<ParameterProfile> parameters)
		{
			return EMCRunspaceConfigurationSingleton.GetInstance().IsCmdletAllowedInScope(this.CommandText, new string[]
			{
				propertyName,
				"Identity"
			});
		}

		// Token: 0x040003F3 RID: 1011
		private readonly string AutoDiscovery = "AutoDiscovery";

		// Token: 0x040003F4 RID: 1012
		private readonly string Identity = "Identity";

		// Token: 0x040003F5 RID: 1013
		private readonly string AutoDiscoveryDomain = "AutoDiscoveryDomain";

		// Token: 0x040003F6 RID: 1014
		private readonly string AutoDiscoveryCommandText = "Get-FederationInformation -DomainName {0} | ";

		// Token: 0x040003F7 RID: 1015
		private SingleTaskDataHandler dataHandler;

		// Token: 0x040003F8 RID: 1016
		private string originalCommandText;

		// Token: 0x040003F9 RID: 1017
		private string dataObjectName;

		// Token: 0x040003FA RID: 1018
		private string modifiedParametersDescription;
	}
}
