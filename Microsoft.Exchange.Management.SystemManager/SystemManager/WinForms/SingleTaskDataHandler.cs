using System;
using System.Management.Automation;
using System.Reflection;
using System.Text;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.SnapIn;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000068 RID: 104
	public class SingleTaskDataHandler : ExchangeDataHandler
	{
		// Token: 0x060003E2 RID: 994 RVA: 0x0000E840 File Offset: 0x0000CA40
		public SingleTaskDataHandler() : this("")
		{
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0000E84D File Offset: 0x0000CA4D
		public SingleTaskDataHandler(string saveCommandText) : this(saveCommandText, new MonadConnection("timeout=30", new CommandInteractionHandler(), ADServerSettingsSingleton.GetInstance().CreateRunspaceServerSettingsObject(), PSConnectionInfoSingleton.GetInstance().GetMonadConnectionInfo()))
		{
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0000E879 File Offset: 0x0000CA79
		internal SingleTaskDataHandler(string saveCommandText, MonadConnection connection) : this(new LoggableMonadCommand(saveCommandText, connection))
		{
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0000E888 File Offset: 0x0000CA88
		internal SingleTaskDataHandler(MonadCommand saveCommand)
		{
			this.saveCommand = saveCommand;
			if (this.saveCommand == null)
			{
				throw new ArgumentNullException("saveCommand");
			}
			this.connection = saveCommand.Connection;
			if (this.connection == null)
			{
				throw new ArgumentNullException("connection");
			}
			this.fields = new ADPropertyBag();
			base.DataSource = this;
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060003E6 RID: 998 RVA: 0x0000E8E6 File Offset: 0x0000CAE6
		internal WorkUnit WorkUnit
		{
			get
			{
				if (base.WorkUnits.Count == 0)
				{
					base.WorkUnits.Add(new WorkUnit());
				}
				return base.WorkUnits[0];
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x0000E911 File Offset: 0x0000CB11
		internal MonadConnection Connection
		{
			get
			{
				return this.connection;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060003E8 RID: 1000 RVA: 0x0000E919 File Offset: 0x0000CB19
		internal MonadCommand Command
		{
			get
			{
				return this.saveCommand;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x0000E921 File Offset: 0x0000CB21
		internal MonadParameterCollection Parameters
		{
			get
			{
				return this.saveCommand.Parameters;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060003EA RID: 1002 RVA: 0x0000E92E File Offset: 0x0000CB2E
		// (set) Token: 0x060003EB RID: 1003 RVA: 0x0000E93B File Offset: 0x0000CB3B
		public string CommandText
		{
			get
			{
				return this.saveCommand.CommandText;
			}
			set
			{
				this.saveCommand.CommandText = value;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060003EC RID: 1004 RVA: 0x0000E949 File Offset: 0x0000CB49
		// (set) Token: 0x060003ED RID: 1005 RVA: 0x0000E951 File Offset: 0x0000CB51
		internal bool KeepInstanceParamerter { get; set; }

		// Token: 0x060003EE RID: 1006 RVA: 0x0000E95A File Offset: 0x0000CB5A
		protected virtual void AdjustParameters()
		{
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x0000E95C File Offset: 0x0000CB5C
		protected virtual void HandleIdentityParameter()
		{
			IConfigurable configurable = this.Parameters["Instance"].Value as IConfigurable;
			if (configurable != null && !this.Parameters.Contains("Identity") && !(configurable is TransportConfigContainer) && !(configurable is PopImapAdConfiguration))
			{
				this.Parameters.AddWithValue("Identity", configurable.Identity);
			}
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0000E9C0 File Offset: 0x0000CBC0
		internal MonadParameter[] PrepareParameters()
		{
			MonadParameter[] array = new MonadParameter[this.Parameters.Count];
			this.Parameters.CopyTo(array, 0);
			this.AddParameters();
			if (!this.KeepInstanceParamerter && this.Parameters.Contains("Instance"))
			{
				object value = this.Parameters["Instance"].Value;
				BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public;
				foreach (PropertyInfo propertyInfo in value.GetType().GetProperties(bindingAttr))
				{
					if ((propertyInfo.GetCustomAttributes(typeof(ParameterAttribute), true).Length != 0 || (propertyInfo.GetSetMethod(true) != null && !propertyInfo.GetSetMethod(true).IsPrivate)) && base.ParameterNames.Contains(propertyInfo.Name) && !this.Parameters.Contains(propertyInfo.Name))
					{
						this.Parameters.AddWithValue(propertyInfo.Name, propertyInfo.GetValue(value, null));
					}
				}
				this.HandleIdentityParameter();
				this.Parameters.Remove("Instance");
			}
			this.AdjustParameters();
			MonadParameter[] array2 = new MonadParameter[this.Parameters.Count];
			this.Parameters.CopyTo(array2, 0);
			this.Parameters.Clear();
			this.Parameters.AddRange(array);
			return array2;
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0000EB1C File Offset: 0x0000CD1C
		internal sealed override void OnReadData(CommandInteractionHandler interactionHandler, string pageName)
		{
			this.Connection.InteractionHandler = interactionHandler;
			using (new OpenConnection(this.Connection))
			{
				this.OnReadData();
				object dataSource = base.DataSource;
				try
				{
					base.OnReadData(interactionHandler, pageName);
				}
				finally
				{
					base.DataSource = dataSource;
				}
			}
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x0000EB88 File Offset: 0x0000CD88
		protected virtual void OnReadData()
		{
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x0000EB8C File Offset: 0x0000CD8C
		internal sealed override void OnSaveData(CommandInteractionHandler interactionHandler)
		{
			this.Connection.InteractionHandler = interactionHandler;
			using (new OpenConnection(this.Connection))
			{
				MonadParameter[] array = new MonadParameter[this.Parameters.Count];
				this.Parameters.CopyTo(array, 0);
				MonadParameter[] values = this.PrepareParameters();
				this.Parameters.Clear();
				this.Parameters.AddRange(values);
				this.OnSaveData();
				base.OnSaveData(interactionHandler);
				this.Parameters.Clear();
				this.Parameters.AddRange(array);
			}
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0000EC30 File Offset: 0x0000CE30
		protected virtual void OnSaveData()
		{
			if (!string.IsNullOrEmpty(this.saveCommand.CommandText))
			{
				base.SavedResults.Clear();
				this.saveCommand.ProgressReport += base.OnProgressReport;
				try
				{
					object[] array = this.saveCommand.Execute(base.WorkUnits.ToArray());
					if (array != null)
					{
						base.SavedResults.AddRange(array);
					}
				}
				finally
				{
					this.saveCommand.ProgressReport -= base.OnProgressReport;
				}
			}
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0000ECC4 File Offset: 0x0000CEC4
		protected virtual void AddParameters()
		{
			if (this.fields.Count > 0)
			{
				this.Parameters.Clear();
				foreach (object obj in this.Fields.Keys)
				{
					PropertyDefinition propertyDefinition = (PropertyDefinition)obj;
					this.Parameters.AddWithValue(propertyDefinition.Name, this.Fields[propertyDefinition]);
				}
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x0000ED54 File Offset: 0x0000CF54
		internal ADPropertyBag Fields
		{
			get
			{
				return this.fields;
			}
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0000ED5C File Offset: 0x0000CF5C
		public override void UpdateWorkUnits()
		{
			if (base.WorkUnits.Count == 0 && !base.ReadOnly)
			{
				StringBuilder stringBuilder = new StringBuilder();
				if (this.Fields.Count > 0)
				{
					stringBuilder.AppendLine(Strings.ParametersForTheTaskTitle);
				}
				foreach (object obj in this.Fields.Keys)
				{
					PropertyDefinition propertyDefinition = (PropertyDefinition)obj;
					object obj2 = this.Fields[propertyDefinition];
					stringBuilder.Append(Strings.NameValueFormat(propertyDefinition.Name, obj2.ToString()));
				}
				this.WorkUnit.Text = this.saveCommand.CommandText;
				this.WorkUnit.Description = stringBuilder.ToString();
			}
			base.UpdateWorkUnits();
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0000EE50 File Offset: 0x0000D050
		public override void Cancel()
		{
			base.Cancel();
			this.saveCommand.Cancel();
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0000EE63 File Offset: 0x0000D063
		public string GetCommandString()
		{
			if (this.saveCommand == null)
			{
				return null;
			}
			return this.saveCommand.ToString();
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x0000EE7A File Offset: 0x0000D07A
		internal override string CommandToRun
		{
			get
			{
				return this.ClonePreparedCommand().ToString() + "\r\n";
			}
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0000EE94 File Offset: 0x0000D094
		private MonadCommand ClonePreparedCommand()
		{
			MonadParameter[] array = new MonadParameter[this.Parameters.Count];
			this.Parameters.CopyTo(array, 0);
			MonadParameter[] values = this.PrepareParameters();
			this.Parameters.Clear();
			this.Parameters.AddRange(values);
			MonadCommand result = this.saveCommand.Clone();
			this.Parameters.Clear();
			this.Parameters.AddRange(array);
			return result;
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x0000EF04 File Offset: 0x0000D104
		public override string ModifiedParametersDescription
		{
			get
			{
				string result = string.Empty;
				if (this.IsModified())
				{
					StringBuilder stringBuilder = new StringBuilder();
					MonadCommand monadCommand = this.ClonePreparedCommand();
					foreach (object obj in monadCommand.Parameters)
					{
						MonadParameter monadParameter = (MonadParameter)obj;
						if (monadParameter.IsSwitch)
						{
							stringBuilder.Append(Strings.NameValueFormat(monadParameter.ParameterName, string.Empty));
						}
						else
						{
							stringBuilder.Append(Strings.NameValueFormat(monadParameter.ParameterName, MonadCommand.FormatParameterValue(monadParameter.Value)));
						}
					}
					result = stringBuilder.ToString();
				}
				return result;
			}
		}

		// Token: 0x0400010A RID: 266
		private MonadConnection connection;

		// Token: 0x0400010B RID: 267
		private MonadCommand saveCommand;

		// Token: 0x0400010C RID: 268
		private ADPropertyBag fields;
	}
}
