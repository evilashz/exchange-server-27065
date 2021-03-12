using System;
using System.Data;
using System.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Monad;

namespace Microsoft.Exchange.Configuration.MonadDataProvider
{
	// Token: 0x020001CA RID: 458
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MonadDataAdapter : DbDataAdapter
	{
		// Token: 0x0600103F RID: 4159 RVA: 0x0003183D File Offset: 0x0002FA3D
		public MonadDataAdapter()
		{
			ExTraceGlobals.IntegrationTracer.Information((long)this.GetHashCode(), "new MonadDataAdapter()");
		}

		// Token: 0x06001040 RID: 4160 RVA: 0x0003185B File Offset: 0x0002FA5B
		public MonadDataAdapter(MonadCommand selectCommand) : this()
		{
			this.SelectCommand = selectCommand;
		}

		// Token: 0x06001041 RID: 4161 RVA: 0x0003186A File Offset: 0x0002FA6A
		public MonadDataAdapter(string selectCommandText) : this(new MonadCommand(selectCommandText, new MonadConnection()))
		{
		}

		// Token: 0x06001042 RID: 4162 RVA: 0x0003187D File Offset: 0x0002FA7D
		public MonadDataAdapter(string selectCommandText, string selectConnectionString) : this(new MonadCommand(selectCommandText, new MonadConnection(selectConnectionString)))
		{
		}

		// Token: 0x06001043 RID: 4163 RVA: 0x00031891 File Offset: 0x0002FA91
		public MonadDataAdapter(string selectCommandText, MonadConnection selectConnection) : this(new MonadCommand(selectCommandText, selectConnection))
		{
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06001044 RID: 4164 RVA: 0x000318A0 File Offset: 0x0002FAA0
		// (set) Token: 0x06001045 RID: 4165 RVA: 0x000318A8 File Offset: 0x0002FAA8
		public bool EnforceDataSetSchema
		{
			get
			{
				return this.enforceDataSetSchema;
			}
			set
			{
				this.enforceDataSetSchema = value;
			}
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06001046 RID: 4166 RVA: 0x000318B1 File Offset: 0x0002FAB1
		// (set) Token: 0x06001047 RID: 4167 RVA: 0x000318BE File Offset: 0x0002FABE
		public new MonadCommand UpdateCommand
		{
			get
			{
				return (MonadCommand)base.UpdateCommand;
			}
			set
			{
				base.UpdateCommand = value;
			}
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06001048 RID: 4168 RVA: 0x000318C7 File Offset: 0x0002FAC7
		// (set) Token: 0x06001049 RID: 4169 RVA: 0x000318D4 File Offset: 0x0002FAD4
		public new MonadCommand SelectCommand
		{
			get
			{
				return (MonadCommand)base.SelectCommand;
			}
			set
			{
				base.SelectCommand = value;
			}
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x0600104A RID: 4170 RVA: 0x000318DD File Offset: 0x0002FADD
		// (set) Token: 0x0600104B RID: 4171 RVA: 0x000318EA File Offset: 0x0002FAEA
		public new MonadCommand DeleteCommand
		{
			get
			{
				return (MonadCommand)base.DeleteCommand;
			}
			set
			{
				base.DeleteCommand = value;
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x0600104C RID: 4172 RVA: 0x000318F3 File Offset: 0x0002FAF3
		// (set) Token: 0x0600104D RID: 4173 RVA: 0x00031900 File Offset: 0x0002FB00
		public new MonadCommand InsertCommand
		{
			get
			{
				return (MonadCommand)base.InsertCommand;
			}
			set
			{
				base.InsertCommand = value;
			}
		}

		// Token: 0x0600104E RID: 4174 RVA: 0x0003190C File Offset: 0x0002FB0C
		protected override int Fill(DataSet dataSet, string srcTable, IDataReader dataReader, int startRecord, int maxRecords)
		{
			ExTraceGlobals.IntegrationTracer.Information<string>((long)this.GetHashCode(), "-->MonadDataAdapter.Fill({0})", srcTable);
			ExTraceGlobals.IntegrationTracer.Information<string>((long)this.GetHashCode(), "\tSelectCommand={0}", this.SelectCommand.CommandText);
			DataTable dataTable = dataSet.Tables[srcTable];
			int num;
			if (dataTable != null)
			{
				num = this.Fill(new DataTable[]
				{
					dataTable
				}, dataReader, startRecord, maxRecords);
			}
			else
			{
				if (this.enforceDataSetSchema)
				{
					throw new InvalidOperationException("EnforceDataSetSchema cannot be used if the data table is not already present in the dataset.");
				}
				num = base.Fill(dataSet, srcTable, dataReader, startRecord, maxRecords);
			}
			ExTraceGlobals.IntegrationTracer.Information<int>((long)this.GetHashCode(), "<--MonadDataAdapter.Fill(), {0}", num);
			return num;
		}

		// Token: 0x0600104F RID: 4175 RVA: 0x000319B8 File Offset: 0x0002FBB8
		protected override int Fill(DataTable[] dataTables, IDataReader dataReader, int startRecord, int maxRecords)
		{
			ExTraceGlobals.IntegrationTracer.Information<int, string>((long)this.GetHashCode(), "-->MonadDataAdapter.Fill({0} (first of {1}))", dataTables.Length, dataTables[0].TableName);
			ExTraceGlobals.IntegrationTracer.Information<string>((long)this.GetHashCode(), "\tSelectCommand={0}", this.SelectCommand.CommandText);
			MonadDataReader monadDataReader = dataReader as MonadDataReader;
			if (dataTables == null || dataTables[0] == null)
			{
				throw new ArgumentNullException("dataTables");
			}
			DataTable dataTable = dataTables[0];
			if (this.enforceDataSetSchema)
			{
				DataColumnMappingCollection mappings = null;
				if (base.TableMappings.Contains(dataTable.TableName))
				{
					mappings = base.TableMappings[dataTable.TableName].ColumnMappings;
				}
				monadDataReader.EnforceSchema(dataTable.Columns, mappings);
			}
			if (monadDataReader.PositionInfo != null)
			{
				dataTable.ExtendedProperties["Position"] = monadDataReader.PositionInfo.PageOffset;
				dataTable.ExtendedProperties["TotalCount"] = monadDataReader.PositionInfo.TotalCount;
			}
			int num = base.Fill(dataTables, dataReader, startRecord, maxRecords);
			if (monadDataReader.PositionInfo != null)
			{
				dataTable.ExtendedProperties["BookmarkPrevious"] = monadDataReader.FirstResult;
				dataTable.ExtendedProperties["BookmarkNext"] = monadDataReader.LastResult;
			}
			ExTraceGlobals.IntegrationTracer.Information<int>((long)this.GetHashCode(), "<--MonadDataAdapter.Fill(), {0}", num);
			return num;
		}

		// Token: 0x06001050 RID: 4176 RVA: 0x00031B08 File Offset: 0x0002FD08
		public virtual object[] GetObjects()
		{
			object[] result;
			using (new OpenConnection(this.SelectCommand.Connection))
			{
				result = this.SelectCommand.Execute();
			}
			return result;
		}

		// Token: 0x0400037D RID: 893
		private bool enforceDataSetSchema;
	}
}
