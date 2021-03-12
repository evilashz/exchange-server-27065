using System;
using System.Data;
using Microsoft.Exchange.Configuration.MonadDataProvider;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020000EC RID: 236
	internal class ProcedureDataFiller : MonadAdapterFiller
	{
		// Token: 0x060008F2 RID: 2290 RVA: 0x0001D348 File Offset: 0x0001B548
		public ProcedureDataFiller(string commandText, ProcedureBuilder procedureBuilder) : base(commandText, procedureBuilder)
		{
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x060008F3 RID: 2291 RVA: 0x0001D352 File Offset: 0x0001B552
		internal ProcedureBuilder ProcedureBuilder
		{
			get
			{
				return this.CommandBuilder as ProcedureBuilder;
			}
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x0001D360 File Offset: 0x0001B560
		protected override void OnFill(DataTable resultsTable)
		{
			base.Command.CommandType = CommandType.StoredProcedure;
			bool flag = this.RequireMatchFilter();
			bool flag2 = this.RequireMatchResolve();
			DataTable dataTable = resultsTable;
			if (flag || flag2)
			{
				dataTable = resultsTable.Clone();
			}
			using (MonadDataAdapter monadDataAdapter = new MonadDataAdapter(base.Command))
			{
				if (dataTable.Columns.Count != 0)
				{
					monadDataAdapter.MissingSchemaAction = MissingSchemaAction.Ignore;
					monadDataAdapter.EnforceDataSetSchema = true;
				}
				monadDataAdapter.Fill(dataTable);
			}
			if (flag || flag2)
			{
				resultsTable.BeginLoadData();
				foreach (object obj in dataTable.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					bool flag3 = true;
					if (flag)
					{
						flag3 = this.MatchFilter(dataRow);
					}
					if (flag2 && flag3)
					{
						flag3 = this.MatchResolveProperty(dataRow);
					}
					if (flag3)
					{
						resultsTable.Rows.Add(dataRow.ItemArray);
					}
				}
				resultsTable.EndLoadData();
			}
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x0001D478 File Offset: 0x0001B678
		private bool RequireMatchFilter()
		{
			return this.ProcedureBuilder.SearchType == 2 && !string.IsNullOrEmpty(this.searchText) && !string.IsNullOrEmpty(this.ProcedureBuilder.NamePropertyFilter);
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x0001D4AC File Offset: 0x0001B6AC
		private bool MatchFilter(DataRow row)
		{
			bool result = false;
			string text = string.Format("{0}", row[this.ProcedureBuilder.NamePropertyFilter]);
			if (text.IndexOf(this.searchText, 0, StringComparison.CurrentCultureIgnoreCase) >= 0)
			{
				result = true;
			}
			return result;
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x0001D4EB File Offset: 0x0001B6EB
		private bool RequireMatchResolve()
		{
			return base.IsResolving && !this.ProcedureBuilder.UseFilterToResolveNonId && !string.IsNullOrEmpty(this.ProcedureBuilder.ResolveProperty) && (this.pipeline != null || this.pipeline.Length > 0);
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x0001D52C File Offset: 0x0001B72C
		private bool MatchResolveProperty(DataRow row)
		{
			bool result = false;
			foreach (object objB in this.pipeline)
			{
				if (object.Equals(row[this.ProcedureBuilder.ResolveProperty], objB))
				{
					result = true;
					break;
				}
			}
			return result;
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x0001D572 File Offset: 0x0001B772
		public override void BuildCommand(string searchText, object[] pipeline, DataRow row)
		{
			this.searchText = searchText;
			this.pipeline = pipeline;
			base.Command = this.ProcedureBuilder.BuildProcedure(base.GetExecutingCommandText(), searchText, pipeline, row);
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x0001D59C File Offset: 0x0001B79C
		public override void BuildCommandWithScope(string searchText, object[] pipeline, DataRow row, object scope)
		{
			this.searchText = searchText;
			this.pipeline = pipeline;
			base.Command = this.ProcedureBuilder.BuildProcedureWithScope(base.GetExecutingCommandText(), searchText, pipeline, row, scope);
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x0001D5C8 File Offset: 0x0001B7C8
		public override object Clone()
		{
			return new ProcedureDataFiller(base.CommandText, this.ProcedureBuilder)
			{
				ResolveCommandText = base.ResolveCommandText,
				IsResolving = base.IsResolving
			};
		}

		// Token: 0x040003F1 RID: 1009
		private string searchText;

		// Token: 0x040003F2 RID: 1010
		private object[] pipeline;
	}
}
