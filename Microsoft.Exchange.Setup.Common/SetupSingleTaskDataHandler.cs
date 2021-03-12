using System;
using System.Management.Automation;
using System.Reflection;
using System.Text;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x02000005 RID: 5
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SetupSingleTaskDataHandler : ExchangeDataHandler
	{
		// Token: 0x0600004C RID: 76 RVA: 0x00002E6C File Offset: 0x0000106C
		public SetupSingleTaskDataHandler(ISetupContext context, MonadCommand command) : base(command)
		{
			this.SetupContext = context;
			this.WorkUnit.CanShowExecutedCommand = false;
			this.ImplementsDatacenterMode = false;
			this.ImplementsDatacenterDedicatedMode = false;
			this.ImplementsPartnerHostedMode = false;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002EA0 File Offset: 0x000010A0
		public SetupSingleTaskDataHandler(ISetupContext context, string commandText, MonadConnection connection)
		{
			this.Command = new LoggableMonadCommand(commandText, connection);
			this.Connection = this.Command.Connection;
			if (this.Connection == null)
			{
				throw new ArgumentNullException("connection");
			}
			this.Fields = new ADPropertyBag();
			base.DataSource = this;
			this.SetupContext = context;
			this.WorkUnit.CanShowExecutedCommand = false;
			this.ImplementsDatacenterMode = false;
			this.ImplementsDatacenterDedicatedMode = false;
			this.ImplementsPartnerHostedMode = false;
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00002F1E File Offset: 0x0000111E
		// (set) Token: 0x0600004F RID: 79 RVA: 0x00002F26 File Offset: 0x00001126
		protected ISetupContext SetupContext { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002F2F File Offset: 0x0000112F
		// (set) Token: 0x06000051 RID: 81 RVA: 0x00002F37 File Offset: 0x00001137
		public bool ImplementsDatacenterMode { get; protected set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002F40 File Offset: 0x00001140
		// (set) Token: 0x06000053 RID: 83 RVA: 0x00002F48 File Offset: 0x00001148
		public bool ImplementsDatacenterDedicatedMode { get; protected set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00002F51 File Offset: 0x00001151
		// (set) Token: 0x06000055 RID: 85 RVA: 0x00002F59 File Offset: 0x00001159
		public bool ImplementsPartnerHostedMode { get; protected set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002F62 File Offset: 0x00001162
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

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00002F8D File Offset: 0x0000118D
		// (set) Token: 0x06000058 RID: 88 RVA: 0x00002F95 File Offset: 0x00001195
		internal MonadConnection Connection { get; private set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00002F9E File Offset: 0x0000119E
		// (set) Token: 0x0600005A RID: 90 RVA: 0x00002FA6 File Offset: 0x000011A6
		internal MonadCommand Command { get; private set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002FAF File Offset: 0x000011AF
		internal MonadParameterCollection Parameters
		{
			get
			{
				return this.Command.Parameters;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00002FBC File Offset: 0x000011BC
		// (set) Token: 0x0600005D RID: 93 RVA: 0x00002FC9 File Offset: 0x000011C9
		public string CommandText
		{
			get
			{
				return this.Command.CommandText;
			}
			set
			{
				this.Command.CommandText = value;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00002FD7 File Offset: 0x000011D7
		// (set) Token: 0x0600005F RID: 95 RVA: 0x00002FDF File Offset: 0x000011DF
		internal bool KeepInstanceParamerter { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00002FE8 File Offset: 0x000011E8
		// (set) Token: 0x06000061 RID: 97 RVA: 0x00002FF0 File Offset: 0x000011F0
		internal ADPropertyBag Fields { get; private set; }

		// Token: 0x06000062 RID: 98 RVA: 0x00002FFC File Offset: 0x000011FC
		protected virtual void OnSaveData()
		{
			if (!string.IsNullOrEmpty(this.CommandText))
			{
				try
				{
					SetupLogger.Log(SetupLogger.HalfAsterixLine);
					SetupLogger.Log(Strings.WillExecuteHighLevelTask(this.CommandText));
					this.startTime = DateTime.UtcNow;
					SetupLogger.IncreaseIndentation(Strings.HighLevelTaskStarted(this.GetCommandString()));
					base.SavedResults.Clear();
					this.Command.ProgressReport += base.OnProgressReport;
					try
					{
						object[] array = this.Command.Execute(base.WorkUnits.ToArray());
						if (array != null)
						{
							base.SavedResults.AddRange(array);
						}
					}
					finally
					{
						this.Command.ProgressReport -= base.OnProgressReport;
					}
				}
				finally
				{
					SetupLogger.DecreaseIndentation();
					SetupLogger.LogForDataMining(this.GetCommandString(), this.startTime);
				}
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000030E8 File Offset: 0x000012E8
		protected virtual void AddParameters()
		{
			SetupLogger.TraceEnter(new object[0]);
			this.Parameters.Clear();
			if (this.ImplementsDatacenterMode && this.SetupContext.IsDatacenter)
			{
				this.Parameters.AddWithValue("IsDatacenter", true);
			}
			if (this.ImplementsDatacenterDedicatedMode && this.SetupContext.IsDatacenterDedicated)
			{
				this.Parameters.AddWithValue("IsDatacenterDedicated", true);
			}
			if (this.ImplementsPartnerHostedMode && this.SetupContext.IsPartnerHosted)
			{
				this.Parameters.AddWithValue("IsPartnerHosted", true);
			}
			SetupLogger.TraceExit();
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003194 File Offset: 0x00001394
		protected virtual void AdjustParameters()
		{
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003198 File Offset: 0x00001398
		protected virtual void HandleIdentityParameter()
		{
			IConfigurable configurable = this.Parameters["Instance"].Value as IConfigurable;
			if (configurable != null && !this.Parameters.Contains("Identity") && !(configurable is TransportConfigContainer) && !(configurable is PopImapAdConfiguration))
			{
				this.Parameters.AddWithValue("Identity", configurable.Identity);
			}
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000031FC File Offset: 0x000013FC
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

		// Token: 0x06000067 RID: 103 RVA: 0x00003358 File Offset: 0x00001558
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

		// Token: 0x06000068 RID: 104 RVA: 0x000033C4 File Offset: 0x000015C4
		protected virtual void OnReadData()
		{
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000033C8 File Offset: 0x000015C8
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

		// Token: 0x0600006A RID: 106 RVA: 0x0000346C File Offset: 0x0000166C
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
				this.WorkUnit.Text = this.Command.CommandText;
				this.WorkUnit.Description = stringBuilder.ToString();
			}
			base.UpdateWorkUnits();
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003560 File Offset: 0x00001760
		public override void Cancel()
		{
			base.Cancel();
			this.Command.Cancel();
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003573 File Offset: 0x00001773
		public string GetCommandString()
		{
			if (this.Command == null)
			{
				return null;
			}
			return this.Command.ToString();
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600006D RID: 109 RVA: 0x0000358A File Offset: 0x0000178A
		internal override string CommandToRun
		{
			get
			{
				return this.ClonePreparedCommand() + "\r\n";
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x0000359C File Offset: 0x0000179C
		private string ClonePreparedCommand()
		{
			MonadParameter[] array = new MonadParameter[this.Parameters.Count];
			this.Parameters.CopyTo(array, 0);
			MonadCommand monadCommand = null;
			string result;
			try
			{
				MonadParameter[] values = this.PrepareParameters();
				this.Parameters.Clear();
				this.Parameters.AddRange(values);
				monadCommand = this.Command.Clone();
				result = monadCommand + "\r\n";
				this.modifiedParametersDescription = string.Empty;
				if (this.IsModified())
				{
					StringBuilder stringBuilder = new StringBuilder();
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
					this.modifiedParametersDescription = stringBuilder.ToString();
				}
			}
			finally
			{
				this.Parameters.Clear();
				this.Parameters.AddRange(array);
				if (monadCommand != null)
				{
					monadCommand.Dispose();
				}
			}
			return result;
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600006F RID: 111 RVA: 0x000036F4 File Offset: 0x000018F4
		public override string ModifiedParametersDescription
		{
			get
			{
				this.ClonePreparedCommand();
				return this.modifiedParametersDescription;
			}
		}

		// Token: 0x04000010 RID: 16
		private string modifiedParametersDescription;

		// Token: 0x04000011 RID: 17
		private DateTime startTime;
	}
}
