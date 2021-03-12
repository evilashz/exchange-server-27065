using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Management.SnapIn;
using Microsoft.ManagementGUI;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000065 RID: 101
	public class MonadAdapterFiller : AbstractDataTableFiller, IHasPermission
	{
		// Token: 0x060003BE RID: 958 RVA: 0x0000D560 File Offset: 0x0000B760
		public MonadAdapterFiller(string commandText, ICommandBuilder builder)
		{
			this.commandText = commandText;
			this.commandBuilder = builder;
			this.FixedValues = new Dictionary<string, object>();
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060003BF RID: 959 RVA: 0x0000D597 File Offset: 0x0000B797
		// (set) Token: 0x060003C0 RID: 960 RVA: 0x0000D59F File Offset: 0x0000B79F
		public Dictionary<string, object> FixedValues { get; private set; }

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x0000D5A8 File Offset: 0x0000B7A8
		public override ICommandBuilder CommandBuilder
		{
			get
			{
				return this.commandBuilder;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x0000D5B0 File Offset: 0x0000B7B0
		// (set) Token: 0x060003C3 RID: 963 RVA: 0x0000D5B8 File Offset: 0x0000B7B8
		public Dictionary<string, string> Parameters
		{
			get
			{
				return this.parameters;
			}
			set
			{
				this.parameters = value;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060003C4 RID: 964 RVA: 0x0000D5C1 File Offset: 0x0000B7C1
		// (set) Token: 0x060003C5 RID: 965 RVA: 0x0000D5C9 File Offset: 0x0000B7C9
		public List<string> AddtionalParameters
		{
			get
			{
				return this.additionalParameters;
			}
			set
			{
				this.additionalParameters = value;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060003C6 RID: 966 RVA: 0x0000D5D2 File Offset: 0x0000B7D2
		// (set) Token: 0x060003C7 RID: 967 RVA: 0x0000D5DA File Offset: 0x0000B7DA
		public string ResolveCommandText
		{
			get
			{
				return this.resolveCommandText;
			}
			set
			{
				this.resolveCommandText = value;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x0000D5E3 File Offset: 0x0000B7E3
		// (set) Token: 0x060003C9 RID: 969 RVA: 0x0000D5EB File Offset: 0x0000B7EB
		public bool IsResolving
		{
			get
			{
				return this.isResolving;
			}
			set
			{
				this.isResolving = value;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060003CA RID: 970 RVA: 0x0000D5F4 File Offset: 0x0000B7F4
		public string CommandText
		{
			get
			{
				return this.commandText;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060003CB RID: 971 RVA: 0x0000D5FC File Offset: 0x0000B7FC
		// (set) Token: 0x060003CC RID: 972 RVA: 0x0000D604 File Offset: 0x0000B804
		internal MonadCommand Command { get; set; }

		// Token: 0x060003CD RID: 973 RVA: 0x0000D60D File Offset: 0x0000B80D
		public override void BuildCommand(string searchText, object[] pipeline, DataRow row)
		{
			this.Command = new LoggableMonadCommand();
			this.Command.CommandText = this.CommandBuilder.BuildCommand(this.GetExecutingCommandText(), searchText, pipeline, row);
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0000D639 File Offset: 0x0000B839
		public override void BuildCommandWithScope(string searchText, object[] pipeline, DataRow row, object scope)
		{
			this.Command = new LoggableMonadCommand();
			this.Command.CommandText = this.CommandBuilder.BuildCommandWithScope(this.GetExecutingCommandText(), searchText, pipeline, row, scope);
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0000D724 File Offset: 0x0000B924
		protected override void OnFill(DataTable table)
		{
			this.Command.CommandType = CommandType.Text;
			DataTable filledTable = table;
			if (this.FixedValues.Count != 0)
			{
				filledTable = table.Clone();
				filledTable.RowChanged += delegate(object sender, DataRowChangeEventArgs e)
				{
					if (e.Action == DataRowAction.Add)
					{
						foreach (string text in this.FixedValues.Keys)
						{
							e.Row[text] = this.FixedValues[text];
						}
						table.Rows.Add(e.Row.ItemArray);
						filledTable.Rows.Remove(e.Row);
					}
				};
			}
			using (MonadDataAdapter monadDataAdapter = new MonadDataAdapter(this.Command))
			{
				if (table.Columns.Count != 0)
				{
					monadDataAdapter.MissingSchemaAction = MissingSchemaAction.Ignore;
					monadDataAdapter.EnforceDataSetSchema = true;
				}
				monadDataAdapter.Fill(filledTable);
			}
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0000D7F0 File Offset: 0x0000B9F0
		protected string GetExecutingCommandText()
		{
			StringBuilder stringBuilder = new StringBuilder(this.commandText);
			foreach (string text in this.parameters.Keys)
			{
				stringBuilder.AppendFormat(" -{0} {1}", text, this.parameters[text]);
			}
			string result = stringBuilder.ToString();
			if (this.isResolving && !string.IsNullOrEmpty(this.resolveCommandText))
			{
				if (!this.resolveCommandText.Equals(this.commandText, StringComparison.OrdinalIgnoreCase))
				{
					throw new NotSupportedException();
				}
				result = this.resolveCommandText;
			}
			return result;
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0000D8A4 File Offset: 0x0000BAA4
		public override object Clone()
		{
			MonadAdapterFiller monadAdapterFiller = new MonadAdapterFiller(this.commandText, this.commandBuilder)
			{
				ResolveCommandText = this.resolveCommandText,
				IsResolving = this.isResolving,
				Parameters = this.parameters,
				AddtionalParameters = this.additionalParameters
			};
			foreach (string key in this.FixedValues.Keys)
			{
				monadAdapterFiller.FixedValues[key] = this.FixedValues[key];
			}
			return monadAdapterFiller;
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0000D954 File Offset: 0x0000BB54
		public override void Cancel()
		{
			if (this.Command != null)
			{
				this.Command.Cancel();
			}
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0000D96C File Offset: 0x0000BB6C
		public bool HasPermission()
		{
			List<string> list = new List<string>(this.additionalParameters);
			list.AddRange(this.parameters.Keys);
			return EMCRunspaceConfigurationSingleton.GetInstance().IsCmdletAllowedInScope(this.CommandText, list.ToArray());
		}

		// Token: 0x040000FB RID: 251
		private ICommandBuilder commandBuilder;

		// Token: 0x040000FC RID: 252
		private string commandText;

		// Token: 0x040000FD RID: 253
		private string resolveCommandText;

		// Token: 0x040000FE RID: 254
		private bool isResolving;

		// Token: 0x040000FF RID: 255
		private Dictionary<string, string> parameters = new Dictionary<string, string>();

		// Token: 0x04000100 RID: 256
		private List<string> additionalParameters = new List<string>();
	}
}
