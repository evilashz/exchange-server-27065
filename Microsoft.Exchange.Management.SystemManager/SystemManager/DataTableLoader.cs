using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Mapi.Common;
using Microsoft.Exchange.Diagnostics.Components.Management.SystemManager;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.SnapIn;
using Microsoft.Exchange.Management.SystemManager.WinForms;
using Microsoft.ManagementGUI;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x0200000A RID: 10
	[DefaultEvent("RefreshingChanged")]
	public class DataTableLoader : RefreshableComponent, ISupportFastRefresh, IAcceptExternalInput
	{
		// Token: 0x06000066 RID: 102 RVA: 0x0000319B File Offset: 0x0000139B
		public DataTableLoader() : this(null)
		{
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000031A4 File Offset: 0x000013A4
		public DataTableLoader(ObjectPickerProfileLoader profileLoader, string profileName) : this(profileLoader.GetProfile(profileName))
		{
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000031B3 File Offset: 0x000013B3
		public DataTableLoader(IResultsLoaderConfiguration config) : this(config.BuildResultsLoaderProfile())
		{
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000031C4 File Offset: 0x000013C4
		public DataTableLoader(ResultsLoaderProfile profile)
		{
			this.expectedResultSize = this.DefaultExpectedResultSize;
			this.ResultsLoaderProfile = profile;
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600006A RID: 106 RVA: 0x0000321B File Offset: 0x0000141B
		// (set) Token: 0x0600006B RID: 107 RVA: 0x00003234 File Offset: 0x00001434
		public ResultsLoaderProfile ResultsLoaderProfile
		{
			get
			{
				return this.resultsLoaderProfile;
			}
			private set
			{
				if (value != null)
				{
					this.resultsLoaderProfile = value;
					this.Table = this.ResultsLoaderProfile.CreateResultsDataTable();
					this.Table.Columns.CollectionChanged += delegate(object param0, CollectionChangeEventArgs param1)
					{
						if (this.expressionCalculator != null)
						{
							this.expressionCalculator = null;
						}
					};
					this.BatchSize = this.ResultsLoaderProfile.BatchSize;
					base.RefreshArgument = this.ResultsLoaderProfile;
				}
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600006C RID: 108 RVA: 0x0000329C File Offset: 0x0000149C
		// (set) Token: 0x0600006D RID: 109 RVA: 0x000032A4 File Offset: 0x000014A4
		[DefaultValue(false)]
		public bool EnforeViewEntireForest { get; set; }

		// Token: 0x0600006E RID: 110 RVA: 0x000032AD File Offset: 0x000014AD
		public virtual void InputValue(string columnName, object value)
		{
			if (this.ResultsLoaderProfile == null)
			{
				throw new InvalidOperationException("ResultsLoaderProfile is null");
			}
			this.ResultsLoaderProfile.InputValue(columnName, value);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000032CF File Offset: 0x000014CF
		public virtual void RemoveValue(string columnName)
		{
			if (this.ResultsLoaderProfile == null)
			{
				throw new InvalidOperationException("ResultsLoaderProfile is null");
			}
			this.ResultsLoaderProfile.InputValue(columnName, null);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000032F1 File Offset: 0x000014F1
		public virtual object GetValue(string columnName)
		{
			if (this.ResultsLoaderProfile == null)
			{
				throw new InvalidOperationException("ResultsLoaderProfile is null");
			}
			return this.ResultsLoaderProfile.GetValue(columnName);
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00003312 File Offset: 0x00001512
		// (set) Token: 0x06000072 RID: 114 RVA: 0x0000331A File Offset: 0x0000151A
		[DefaultValue(100)]
		public virtual int BatchSize
		{
			get
			{
				return this.batchSize;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException("value", value, "value <= 0");
				}
				this.batchSize = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000073 RID: 115 RVA: 0x0000333D File Offset: 0x0000153D
		// (set) Token: 0x06000074 RID: 116 RVA: 0x00003345 File Offset: 0x00001545
		[DefaultValue(100)]
		public virtual int DefaultExpectedResultSize
		{
			get
			{
				return this.defaultExpectedResultSize;
			}
			set
			{
				this.defaultExpectedResultSize = value;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000075 RID: 117 RVA: 0x0000334E File Offset: 0x0000154E
		// (set) Token: 0x06000076 RID: 118 RVA: 0x00003356 File Offset: 0x00001556
		public int ExpectedResultSize
		{
			get
			{
				return this.expectedResultSize;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentException();
				}
				if (this.ExpectedResultSize != value)
				{
					this.expectedResultSize = value;
					this.lastRowCount = value;
					this.OnExpectedResultSizeChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003384 File Offset: 0x00001584
		private bool ShouldSerializeExpectedResultSize()
		{
			return this.ExpectedResultSize != this.DefaultExpectedResultSize;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003397 File Offset: 0x00001597
		private void ResetExpectedResultSize()
		{
			this.ExpectedResultSize = this.DefaultExpectedResultSize;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000033A8 File Offset: 0x000015A8
		protected virtual void OnExpectedResultSizeChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DataTableLoader.EventExpectedResultSizeChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x0600007A RID: 122 RVA: 0x000033D6 File Offset: 0x000015D6
		// (remove) Token: 0x0600007B RID: 123 RVA: 0x000033E9 File Offset: 0x000015E9
		public event EventHandler ExpectedResultSizeChanged
		{
			add
			{
				base.Events.AddHandler(DataTableLoader.EventExpectedResultSizeChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataTableLoader.EventExpectedResultSizeChanged, value);
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600007C RID: 124 RVA: 0x000033FC File Offset: 0x000015FC
		// (set) Token: 0x0600007D RID: 125 RVA: 0x00003404 File Offset: 0x00001604
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public LocalizedString ProgressText
		{
			get
			{
				return this.progressText;
			}
			set
			{
				this.progressText = value;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600007E RID: 126 RVA: 0x0000340D File Offset: 0x0000160D
		// (set) Token: 0x0600007F RID: 127 RVA: 0x00003415 File Offset: 0x00001615
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public LocalizedString RefreshCommandText
		{
			get
			{
				return this.refreshCommandText;
			}
			set
			{
				this.refreshCommandText = value;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000080 RID: 128 RVA: 0x0000341E File Offset: 0x0000161E
		// (set) Token: 0x06000081 RID: 129 RVA: 0x00003428 File Offset: 0x00001628
		public DataTable Table
		{
			get
			{
				return this.table;
			}
			set
			{
				if (this.Table != value)
				{
					if (this.Table != null)
					{
						this.Table.RowChanged -= this.Table_RowChanged;
					}
					if (base.Refreshing)
					{
						throw new InvalidOperationException();
					}
					this.table = value;
					if (this.Table != null)
					{
						this.Table.RowChanged += this.Table_RowChanged;
					}
					this.OnTableChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x06000082 RID: 130 RVA: 0x0000349C File Offset: 0x0000169C
		private void Table_RowChanged(object sender, DataRowChangeEventArgs e)
		{
			if (!this.isCalculatingColumn)
			{
				if (e.Action != DataRowAction.Add)
				{
					if (e.Action != DataRowAction.Change)
					{
						return;
					}
				}
				try
				{
					this.isCalculatingColumn = true;
					if (this.ResultsLoaderProfile != null && this.ResultsLoaderProfile.DataColumnsCalculator != null)
					{
						this.ResultsLoaderProfile.DataColumnsCalculator.Calculate(this.ResultsLoaderProfile, this.Table, e.Row);
					}
				}
				finally
				{
					this.isCalculatingColumn = false;
				}
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000083 RID: 131 RVA: 0x0000351C File Offset: 0x0000171C
		internal WorkUnitCollection WorkUnits
		{
			get
			{
				if (this.workUnits == null)
				{
					this.workUnits = new WorkUnitCollection();
				}
				return this.workUnits;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00003537 File Offset: 0x00001737
		protected virtual DataTable DefaultTable
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x0000353A File Offset: 0x0000173A
		private bool ShouldSerializeTable()
		{
			return this.Table != this.DefaultTable;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x0000354D File Offset: 0x0000174D
		private void ResetTable()
		{
			this.Table = this.DefaultTable;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x0000355C File Offset: 0x0000175C
		protected virtual void OnTableChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DataTableLoader.EventTableChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06000088 RID: 136 RVA: 0x0000358A File Offset: 0x0000178A
		// (remove) Token: 0x06000089 RID: 137 RVA: 0x0000359D File Offset: 0x0000179D
		public event EventHandler TableChanged
		{
			add
			{
				base.Events.AddHandler(DataTableLoader.EventTableChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataTableLoader.EventTableChanged, value);
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600008A RID: 138 RVA: 0x000035B0 File Offset: 0x000017B0
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DataColumnCollection Columns
		{
			get
			{
				if (this.Table == null)
				{
					return null;
				}
				return this.Table.Columns;
			}
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000035C8 File Offset: 0x000017C8
		protected override void OnRefreshStarting(CancelEventArgs e)
		{
			ExTraceGlobals.ProgramFlowTracer.TraceFunction<DataTableLoader>((long)this.GetHashCode(), "-->DataTableLoader.OnRefreshStarting: {0}", this);
			lock (this.WorkUnits)
			{
				this.WorkUnits.Clear();
			}
			if (this.Table == null)
			{
				ExTraceGlobals.ProgramFlowTracer.TraceDebug((long)this.GetHashCode(), "DataTableLoader.OnRefreshStarting: cancelling since we don't have a table set.");
				e.Cancel = true;
			}
			else
			{
				base.OnRefreshStarting(e);
			}
			ExTraceGlobals.ProgramFlowTracer.TraceFunction<DataTableLoader>((long)this.GetHashCode(), "<--DataTableLoader.OnRefreshStarting: {0}", this);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x0000366C File Offset: 0x0000186C
		protected sealed override RefreshRequestEventArgs CreateFullRefreshRequest(IProgress progress)
		{
			return new DataTableLoader.DataTableLoaderRefreshRequestEventArgs(this.Table.Clone(), true, progress, base.CloneRefreshArgument());
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003688 File Offset: 0x00001888
		protected override PartialOrder ComparePartialOrder(RefreshRequestEventArgs leftValue, RefreshRequestEventArgs rightValue)
		{
			if (this.ResultsLoaderProfile != null && this.ResultsLoaderProfile.InputTablePartialOrderComparer != null)
			{
				return this.ResultsLoaderProfile.InputTablePartialOrderComparer.Compare((leftValue.Argument as ResultsLoaderProfile).InputTable, (rightValue.Argument as ResultsLoaderProfile).InputTable);
			}
			return base.ComparePartialOrder(leftValue, rightValue);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000036E4 File Offset: 0x000018E4
		private void ReportDataTable(int rowCount, int expectedRowCount, DataTable dataTable, RefreshRequestEventArgs e, bool resolved, TimeSpan elapsed)
		{
			ResultsLoaderProfile resultsLoaderProfile = e.Argument as ResultsLoaderProfile;
			this.FillColumnsBasedOnLambdaExpression(dataTable, resultsLoaderProfile);
			ColumnValueCalculator.CalculateAll(dataTable);
			DataTable dataTable2 = this.MoveRows(dataTable, null, false);
			if (resultsLoaderProfile != null && resolved && resultsLoaderProfile.IsResolving)
			{
				foreach (object obj in resultsLoaderProfile.PipelineObjects)
				{
					if (this.FindRowByIdentity(dataTable2, obj) != null)
					{
						resultsLoaderProfile.ResolvedObjects.Add(obj);
					}
				}
			}
			ExTraceGlobals.ProgramFlowTracer.TraceFunction((long)this.GetHashCode(), "-->DataTableLoader.ReportDataTable: report batch: {0}. progressReportTable.Rows.Count:{1}, this.BatchSize:{2}, rowCount:{3}, expectedRowCount:{4}. ElapsedTime:{5}", new object[]
			{
				this,
				dataTable2.Rows.Count,
				this.BatchSize,
				rowCount,
				expectedRowCount,
				elapsed
			});
			if (!e.ReportedProgress)
			{
				ExTraceGlobals.ProgramFlowTracer.TracePerformance<DataTableLoader, string, string>(0L, "Time:{1}. {2} First batch data arrived in worker thread. {0}", this, ExDateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff"), (this != null) ? this.Table.TableName : string.Empty);
			}
			e.ReportProgress(rowCount, expectedRowCount, this.ProgressText, dataTable2);
			ExTraceGlobals.ProgramFlowTracer.TraceFunction<DataTableLoader>((long)this.GetHashCode(), "<--DataTableLoader.ReportDataTable: report batch: {0}", this);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x0000382F File Offset: 0x00001A2F
		private bool IsPreFillForResolving(AbstractDataTableFiller filler)
		{
			return filler is PreFillADObjectIdFiller;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003AE4 File Offset: 0x00001CE4
		protected override void OnDoRefreshWork(RefreshRequestEventArgs e)
		{
			Stopwatch sw = Stopwatch.StartNew();
			ExTraceGlobals.ProgramFlowTracer.TraceFunction<DataTableLoader>((long)this.GetHashCode(), "-->DataTableLoader.OnDoRefreshWork: {0}", this);
			this.expressionCalculator = null;
			DataTable dataTable = (e as DataTableLoader.IDataTableLoaderRefreshRequest).DataTable.Clone();
			e.Result = dataTable;
			dataTable.RowChanging += delegate(object param0, DataRowChangeEventArgs param1)
			{
				if (e.CancellationPending)
				{
					e.Cancel = true;
					this.Cancel(e);
					e.Result = dataTable;
				}
			};
			int rowCount = 0;
			int expectedRowCount = Math.Max(this.lastRowCount, this.BatchSize);
			ResultsLoaderProfile profile = e.Argument as ResultsLoaderProfile;
			if (profile != null)
			{
				foreach (AbstractDataTableFiller abstractDataTableFiller in profile.TableFillers)
				{
					abstractDataTableFiller.FillCompleted += delegate(object sender, FillCompletedEventArgs args)
					{
						AbstractDataTableFiller filler = sender as AbstractDataTableFiller;
						if (this.IsPreFillForResolving(filler) || profile.FillType == null)
						{
							expectedRowCount = Math.Max(expectedRowCount, rowCount + this.BatchSize + 1);
							bool flag = !this.IsPreFillForResolving(filler) && profile.FillType == 0;
							this.ReportDataTable(rowCount, expectedRowCount, args.DataTable, e, flag, sw.Elapsed);
							if (profile.IsResolving && profile.PipelineObjects != null && flag)
							{
								foreach (object obj in profile.PipelineObjects)
								{
									if (this.FindRowByIdentity(args.DataTable, obj) != null)
									{
										profile.ResolvedObjects.Add(obj);
									}
								}
								IEnumerable<object> source = from id in profile.PipelineObjects
								where !profile.ResolvedObjects.Contains(id)
								select id;
								profile.PipelineObjects = source.ToArray<object>();
								profile.ResolvedObjects.Clear();
							}
						}
					};
				}
			}
			dataTable.RowChanged += delegate(object sender, DataRowChangeEventArgs rowChangedEvent)
			{
				if (rowChangedEvent.Action == DataRowAction.Add)
				{
					rowCount++;
					ExTraceGlobals.DataFlowTracer.Information<DataTableLoader, int, TimeSpan>((long)this.GetHashCode(), "DataTableLoader.OnDoRefreshWork: {0}, rowCount:{1}. ElapsedTime:{2}", this, rowCount, sw.Elapsed);
					ConvertTypeCalculator.Convert(rowChangedEvent.Row);
					if (dataTable.Rows.Count >= this.BatchSize && (profile == null || profile.FillType == null))
					{
						expectedRowCount = Math.Max(expectedRowCount, rowCount + this.BatchSize + 1);
						this.ReportDataTable(rowCount, expectedRowCount, dataTable, e, true, sw.Elapsed);
					}
				}
			};
			try
			{
				ExTraceGlobals.ProgramFlowTracer.TraceFunction<DataTableLoader, TimeSpan>((long)this.GetHashCode(), "-->DataTableLoader.OnDoRefreshWork: Fill: {0}. ElapsedTime:{1}", this, sw.Elapsed);
				this.Fill(e);
			}
			catch (MonadDataAdapterInvocationException ex)
			{
				if (!(ex.InnerException is ManagementObjectNotFoundException) && !(ex.InnerException is MapiObjectNotFoundException))
				{
					throw;
				}
			}
			finally
			{
				ExTraceGlobals.ProgramFlowTracer.TraceFunction<DataTableLoader, TimeSpan>((long)this.GetHashCode(), "<--DataTableLoader.OnDoRefreshWork: Fill: {0}. ElapsedTime:{1}", this, sw.Elapsed);
				e.Result = dataTable;
				ExTraceGlobals.ProgramFlowTracer.TraceFunction((long)this.GetHashCode(), "-->DataTableLoader.OnDoRefreshWork: report last batch: {0}. dataTable.Rows.Count:{1}, this.BatchSize:{2}, rowCount:{3}, expectedRowCount:{4}", new object[]
				{
					this,
					dataTable.Rows.Count,
					this.BatchSize,
					rowCount,
					rowCount + 1
				});
				this.FillColumnsBasedOnLambdaExpression(dataTable, e.Argument as ResultsLoaderProfile);
				ColumnValueCalculator.CalculateAll(dataTable);
				if (!e.ReportedProgress)
				{
					ExTraceGlobals.ProgramFlowTracer.TracePerformance<DataTableLoader, string, string>(0L, "Time:{1}. {2} First batch data arrived in worker thread. {0}", this, ExDateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff"), (this != null) ? this.Table.TableName : string.Empty);
				}
				e.ReportProgress(rowCount, rowCount + 1, this.ProgressText, this.MoveRows(dataTable, null, false));
				ExTraceGlobals.ProgramFlowTracer.TraceFunction<DataTableLoader>((long)this.GetHashCode(), "<--DataTableLoader.OnDoRefreshWork: report last batch: {0}", this);
			}
			base.OnDoRefreshWork(e);
			e.Result = dataTable;
			ExTraceGlobals.ProgramFlowTracer.TraceFunction<DataTableLoader, TimeSpan>((long)this.GetHashCode(), "<--DataTableLoader.OnDoRefreshWork: {0}. Total ElapsedTime:{1}", this, sw.Elapsed);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003E48 File Offset: 0x00002048
		private ExpressionCalculator GetExpressionCalculator()
		{
			if (this.Table != null && this.expressionCalculator == null)
			{
				this.expressionCalculator = ExpressionCalculator.Parse(this.Table);
			}
			return this.expressionCalculator;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003E74 File Offset: 0x00002074
		private void FillColumnsBasedOnLambdaExpression(DataTable dataTable, ResultsLoaderProfile profile)
		{
			if (profile == null)
			{
				return;
			}
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				IList<KeyValuePair<string, object>> list = this.GetExpressionCalculator().CalculateAll(dataRow, profile.InputTable.Rows[0]);
				foreach (KeyValuePair<string, object> keyValuePair in list)
				{
					dataRow[keyValuePair.Key] = keyValuePair.Value;
				}
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003F84 File Offset: 0x00002184
		private void Fill(RefreshRequestEventArgs e)
		{
			ExTraceGlobals.ProgramFlowTracer.TraceFunction<DataTableLoader>((long)this.GetHashCode(), "-->DataTableLoader.Fill: {0}", this);
			ResultsLoaderProfile profile = e.Argument as ResultsLoaderProfile;
			if (profile != null)
			{
				DataTable dataTable = e.Result as DataTable;
				dataTable.RowChanged += delegate(object sender, DataRowChangeEventArgs eventArgs)
				{
					if (eventArgs.Action == DataRowAction.Add)
					{
						this.FillPrimaryKeysBasedOnLambdaExpression(eventArgs.Row, profile);
					}
				};
				DataTable dataTable2 = dataTable.Clone();
				dataTable2.RowChanged += delegate(object sender, DataRowChangeEventArgs eventArgs)
				{
					if (eventArgs.Action == DataRowAction.Add)
					{
						this.FillPrimaryKeysBasedOnLambdaExpression(eventArgs.Row, profile);
					}
				};
				if (!this.EnforeViewEntireForest && !profile.HasPermission())
				{
					goto IL_26F;
				}
				using (DataAdapterExecutionContext dataAdapterExecutionContext = this.executionContextFactory.CreateExecutionContext())
				{
					dataAdapterExecutionContext.Open(base.UIService, this.WorkUnits, this.EnforeViewEntireForest, profile);
					foreach (AbstractDataTableFiller filler in profile.TableFillers)
					{
						if (profile.IsRunnable(filler))
						{
							if (e.CancellationPending)
							{
								break;
							}
							profile.BuildCommand(filler);
							if (profile.FillType == 1 || this.IsPreFillForResolving(filler))
							{
								dataAdapterExecutionContext.Execute(filler, dataTable2, profile);
								this.MergeChanges(dataTable2, dataTable);
								dataTable2.Clear();
							}
							else
							{
								dataAdapterExecutionContext.Execute(filler, dataTable, profile);
							}
						}
					}
					goto IL_26F;
				}
			}
			MonadCommand monadCommand = e.Argument as MonadCommand;
			if (monadCommand != null)
			{
				this.AttachCommandToMonitorWarnings(monadCommand);
				using (MonadConnection monadConnection = new MonadConnection(PSConnectionInfoSingleton.GetInstance().GetConnectionStringForScript(), new CommandInteractionHandler(), ADServerSettingsSingleton.GetInstance().CreateRunspaceServerSettingsObject(), PSConnectionInfoSingleton.GetInstance().GetMonadConnectionInfo(ExchangeRunspaceConfigurationSettings.SerializationLevel.Full)))
				{
					monadConnection.Open();
					monadCommand.Connection = monadConnection;
					using (MonadDataAdapter monadDataAdapter = new MonadDataAdapter(monadCommand))
					{
						DataTable dataTable3 = (DataTable)e.Result;
						if (dataTable3.Columns.Count != 0)
						{
							monadDataAdapter.MissingSchemaAction = MissingSchemaAction.Ignore;
							monadDataAdapter.EnforceDataSetSchema = true;
						}
						ExTraceGlobals.ProgramFlowTracer.TraceFunction<DataTableLoader, MonadCommand>((long)this.GetHashCode(), "-->DataTableLoader.Fill: calling dataAdapter.Fill: {0}. Command:{1}", this, monadCommand);
						monadDataAdapter.Fill(dataTable3);
						ExTraceGlobals.ProgramFlowTracer.TraceFunction<DataTableLoader, MonadCommand>((long)this.GetHashCode(), "<--DataTableLoader.Fill: calling dataAdaptr.Fill: {0}. Command:{1}", this, monadCommand);
					}
				}
				this.DetachCommandFromMonitorWarnings(monadCommand);
			}
			IL_26F:
			this.OnFillTable(e);
			ExTraceGlobals.ProgramFlowTracer.TraceFunction<DataTableLoader>((long)this.GetHashCode(), "<--DataTableLoader.Fill: {0}", this);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00004254 File Offset: 0x00002454
		private void FillPrimaryKeysBasedOnLambdaExpression(DataRow dataRow, ResultsLoaderProfile profile)
		{
			ConvertTypeCalculator.Convert(dataRow);
			if (profile == null)
			{
				return;
			}
			foreach (DataColumn dataColumn in dataRow.Table.PrimaryKey)
			{
				IList<KeyValuePair<string, object>> list = this.GetExpressionCalculator().CalculateSpecifiedColumn(dataColumn.ColumnName, dataRow, profile.InputTable.Rows[0]);
				foreach (KeyValuePair<string, object> keyValuePair in list)
				{
					dataRow[keyValuePair.Key] = keyValuePair.Value;
				}
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00004300 File Offset: 0x00002500
		private void MergeChanges(DataTable sourceTable, DataTable destinationTable)
		{
			foreach (object obj in sourceTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				DataRow dataRow2 = null;
				if (sourceTable.PrimaryKey != null && sourceTable.PrimaryKey.Length != 0)
				{
					List<object> list = new List<object>();
					foreach (DataColumn column in sourceTable.PrimaryKey)
					{
						list.Add(dataRow[column]);
					}
					dataRow2 = destinationTable.Rows.Find(list.ToArray());
				}
				if (dataRow2 != null)
				{
					using (IEnumerator enumerator2 = sourceTable.Columns.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							object obj2 = enumerator2.Current;
							DataColumn dataColumn = (DataColumn)obj2;
							if (!dataColumn.DefaultValue.Equals(dataRow[dataColumn.ColumnName]))
							{
								dataRow2[dataColumn.ColumnName] = dataRow[dataColumn.ColumnName];
							}
						}
						continue;
					}
				}
				destinationTable.Rows.Add(dataRow.ItemArray);
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00004464 File Offset: 0x00002664
		internal void AttachCommandToMonitorWarnings(MonadCommand command)
		{
			lock (this.WorkUnits)
			{
				WorkUnit workUnit;
				if (!this.TryGetWorkUnit(command.CommandText, out workUnit))
				{
					workUnit = new WorkUnit();
					workUnit.Target = command;
					workUnit.Text = command.CommandText;
					this.WorkUnits.Add(workUnit);
					command.WarningReport += this.command_WarningReport;
				}
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000044E8 File Offset: 0x000026E8
		internal void DetachCommandFromMonitorWarnings(MonadCommand command)
		{
			lock (this.WorkUnits)
			{
				WorkUnit workUnit;
				if (this.TryGetWorkUnit(command.CommandText, out workUnit))
				{
					workUnit.Status = WorkUnitStatus.Completed;
					command.WarningReport -= this.command_WarningReport;
				}
			}
		}

		// Token: 0x06000098 RID: 152 RVA: 0x0000454C File Offset: 0x0000274C
		private void command_WarningReport(object sender, WarningReportEventArgs e)
		{
			lock (this.WorkUnits)
			{
				WorkUnit workUnit;
				if (this.TryGetWorkUnit(e.Command.CommandText, out workUnit) && workUnit.Target == e.Command)
				{
					workUnit.Warnings.Add(e.WarningMessage);
				}
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000045BC File Offset: 0x000027BC
		private bool TryGetWorkUnit(string text, out WorkUnit workUnit)
		{
			workUnit = null;
			for (int i = 0; i < this.WorkUnits.Count; i++)
			{
				if (this.WorkUnits[i].Text == text)
				{
					workUnit = this.WorkUnits[i];
					break;
				}
			}
			return null != workUnit;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00004614 File Offset: 0x00002814
		protected virtual void OnFillTable(RefreshRequestEventArgs e)
		{
			RefreshRequestEventHandler refreshRequestEventHandler = (RefreshRequestEventHandler)base.Events[DataTableLoader.EventFillTable];
			if (refreshRequestEventHandler != null)
			{
				refreshRequestEventHandler(this, e);
			}
		}

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x0600009B RID: 155 RVA: 0x00004642 File Offset: 0x00002842
		// (remove) Token: 0x0600009C RID: 156 RVA: 0x00004655 File Offset: 0x00002855
		public event RefreshRequestEventHandler FillTable
		{
			add
			{
				base.Events.AddHandler(DataTableLoader.EventFillTable, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataTableLoader.EventFillTable, value);
			}
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00004668 File Offset: 0x00002868
		private void Cancel(RefreshRequestEventArgs e)
		{
			ExTraceGlobals.ProgramFlowTracer.TraceFunction<DataTableLoader>((long)this.GetHashCode(), "-->DataTableLoader.Cancel: {0}", this);
			IDbCommand dbCommand = e.Argument as IDbCommand;
			if (dbCommand != null)
			{
				ExTraceGlobals.ProgramFlowTracer.TraceDebug<DataTableLoader, IDbCommand>((long)this.GetHashCode(), "DataTableLoader.Cancel: {0} cancelling IDbCommand: {1}", this, dbCommand);
				dbCommand.Cancel();
			}
			ResultsLoaderProfile resultsLoaderProfile = e.Argument as ResultsLoaderProfile;
			if (resultsLoaderProfile != null)
			{
				foreach (AbstractDataTableFiller abstractDataTableFiller in resultsLoaderProfile.TableFillers)
				{
					abstractDataTableFiller.Cancel();
				}
			}
			this.OnCancelFill(e);
			ExTraceGlobals.ProgramFlowTracer.TraceFunction<DataTableLoader>((long)this.GetHashCode(), "<--DataTableLoader.Cancel: {0}", this);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00004728 File Offset: 0x00002928
		protected virtual void OnCancelFill(RefreshRequestEventArgs e)
		{
			RefreshRequestEventHandler refreshRequestEventHandler = (RefreshRequestEventHandler)base.Events[DataTableLoader.EventCancelFill];
			if (refreshRequestEventHandler != null)
			{
				refreshRequestEventHandler(this, e);
			}
		}

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x0600009F RID: 159 RVA: 0x00004756 File Offset: 0x00002956
		// (remove) Token: 0x060000A0 RID: 160 RVA: 0x00004769 File Offset: 0x00002969
		public event RefreshRequestEventHandler CancelFill
		{
			add
			{
				base.Events.AddHandler(DataTableLoader.EventCancelFill, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataTableLoader.EventCancelFill, value);
			}
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x0000477C File Offset: 0x0000297C
		private DataTable MoveRows(DataTable sourceTable, DataTable destinationTable, bool forceUseMergeTable)
		{
			if (sourceTable != null)
			{
				int count = sourceTable.Rows.Count;
				Stopwatch stopwatch = Stopwatch.StartNew();
				ExTraceGlobals.ProgramFlowTracer.TraceFunction<DataTableLoader, bool>((long)this.GetHashCode(), "-->DataTableLoader.MoveRows: {0}. IsBackgroundThread:{1}", this, Thread.CurrentThread.IsBackground);
				if (forceUseMergeTable || (destinationTable != null && destinationTable.Columns.Count == 0))
				{
					destinationTable = ((destinationTable == null) ? sourceTable.Clone() : destinationTable);
					ExTraceGlobals.ProgramFlowTracer.TraceDebug<DataTableLoader>((long)this.GetHashCode(), "DataTableLoader.MoveRows: {0} merging tables.", this);
					destinationTable.Merge(sourceTable);
				}
				else
				{
					ExTraceGlobals.ProgramFlowTracer.TraceDebug<DataTableLoader>((long)this.GetHashCode(), "DataTableLoader.MoveRows: {0} BeginLoadData.", this);
					destinationTable = ((destinationTable == null) ? sourceTable.Clone() : destinationTable);
					destinationTable.BeginLoadData();
					ExTraceGlobals.ProgramFlowTracer.TraceDebug<DataTableLoader>((long)this.GetHashCode(), "DataTableLoader.MoveRows: {0} Moving Rows.", this);
					DataRowCollection rows = destinationTable.Rows;
					DataRowCollection rows2 = sourceTable.Rows;
					for (int i = 0; i < count; i++)
					{
						rows.Add(rows2[i].ItemArray);
					}
					ExTraceGlobals.ProgramFlowTracer.TraceDebug<DataTableLoader>((long)this.GetHashCode(), "DataTableLoader.MoveRows: {0} Moving Extended Properties.", this);
					foreach (object key in sourceTable.ExtendedProperties.Keys)
					{
						destinationTable.ExtendedProperties[key] = sourceTable.ExtendedProperties[key];
					}
					ExTraceGlobals.ProgramFlowTracer.TraceDebug<DataTableLoader>((long)this.GetHashCode(), "DataTableLoader.MoveRows: {0} EndLoadData.", this);
					try
					{
						destinationTable.EndLoadData();
					}
					catch (ConstraintException ex)
					{
						List<string> list = new List<string>();
						foreach (object obj in destinationTable.Columns)
						{
							DataColumn dataColumn = (DataColumn)obj;
							list.Add(dataColumn.ColumnName);
						}
						throw new ConstraintException(ex.Message + "\r\nAll columns of this data table are:" + string.Join(" ", list.ToArray()), ex);
					}
					ExTraceGlobals.ProgramFlowTracer.TraceDebug<DataTableLoader>((long)this.GetHashCode(), "DataTableLoader.MoveRows: {0} Clearing source table.", this);
					sourceTable.Clear();
				}
				ExTraceGlobals.ProgramFlowTracer.TraceFunction<DataTableLoader, int, TimeSpan>((long)this.GetHashCode(), "<--DataTableLoader.MoveRows: {0}. sourceRowsCount:{1}, Elapsed Time: {2}", this, count, stopwatch.Elapsed);
			}
			return destinationTable;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000049EC File Offset: 0x00002BEC
		protected override void OnProgressChanged(RefreshProgressChangedEventArgs e)
		{
			ExTraceGlobals.ProgramFlowTracer.TraceFunction<DataTableLoader>((long)this.GetHashCode(), "-->DataTableLoader.OnProgressChanged: {0}", this);
			if (!e.CancellationPending)
			{
				this.RemoveExistingRows(e);
				DataTable dataTable = (DataTable)e.UserState;
				if (dataTable != null)
				{
					this.MoveRows(dataTable, this.Table, true);
				}
				base.OnProgressChanged(e);
			}
			ExTraceGlobals.ProgramFlowTracer.TraceFunction<DataTableLoader>((long)this.GetHashCode(), "<--DataTableLoader.OnProgressChanged: {0}", this);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00004A5C File Offset: 0x00002C5C
		protected virtual void RemoveExistingRows(RefreshProgressChangedEventArgs e)
		{
			if (e.IsFirstProgressReport)
			{
				ResultsLoaderProfile resultsLoaderProfile = e.RequestArgument as ResultsLoaderProfile;
				if (resultsLoaderProfile != null && resultsLoaderProfile.LoadableFromProfilePredicate != null)
				{
					for (int i = this.Table.Rows.Count - 1; i >= 0; i--)
					{
						DataRow row = this.Table.Rows[i];
						if (resultsLoaderProfile.IsLoadable(row))
						{
							this.Table.Rows.Remove(row);
						}
					}
					return;
				}
				if (e.IsFullRefresh)
				{
					ExTraceGlobals.ProgramFlowTracer.TraceDebug<DataTableLoader>((long)this.GetHashCode(), "DataTableLoader.OnProgressChanged: clearing table as this is the first progress report of this refresh. {0}", this);
					this.Table.Clear();
					return;
				}
				PartialRefreshRequestEventArgs partialRefreshRequestEventArgs = e.Request as PartialRefreshRequestEventArgs;
				if (partialRefreshRequestEventArgs != null)
				{
					DataTable dataTable = (DataTable)e.UserState;
					foreach (object identity in partialRefreshRequestEventArgs.Identities)
					{
						if (this.FindRowByIdentity(dataTable, identity) == null)
						{
							DataRow dataRow = this.FindRowByIdentity(this.Table, identity);
							if (dataRow != null)
							{
								this.Table.Rows.Remove(dataRow);
							}
						}
					}
				}
			}
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00004B74 File Offset: 0x00002D74
		internal DataRow FindRowByIdentity(DataTable table, object identity)
		{
			DataRow result = null;
			if (table != null && identity != null)
			{
				object[] array = this.ConvertToPrimaryKeysFromIdentity(identity);
				if (array != null)
				{
					result = table.Rows.Find(array);
				}
			}
			return result;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00004BA2 File Offset: 0x00002DA2
		protected override void OnRefreshCompleted(RunWorkerCompletedEventArgs e)
		{
			this.lastRowCount = this.Table.Rows.Count;
			ExTraceGlobals.ProgramFlowTracer.TraceFunction<DataTableLoader, int>((long)this.GetHashCode(), "*--DataTableLoader.OnRefreshCompleted: {0}. lastRowCount:{1}", this, this.lastRowCount);
			base.OnRefreshCompleted(e);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00004BE0 File Offset: 0x00002DE0
		void ISupportFastRefresh.Refresh(IProgress progress, object id)
		{
			ExTraceGlobals.ProgramFlowTracer.TraceFunction<DataTableLoader, object>((long)this.GetHashCode(), "-->DataTableLoader.RefreshSingleRow: {0}. ID:{1}", this, id);
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			this.Refresh(progress, new object[]
			{
				id
			}, 0);
			ExTraceGlobals.ProgramFlowTracer.TraceFunction<DataTableLoader>((long)this.GetHashCode(), "<--DataTableLoader.RefreshSingleRow: {0}", this);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00004C40 File Offset: 0x00002E40
		void ISupportFastRefresh.Refresh(IProgress progress, object[] identities, RefreshRequestPriority priority)
		{
			if (identities == null)
			{
				throw new ArgumentNullException("identities");
			}
			ExTraceGlobals.ProgramFlowTracer.TraceFunction<DataTableLoader, int>((long)this.GetHashCode(), "-->DataTableLoader.PartialRefresh: {0}. ID Count:{1}", this, identities.Length);
			if (this.table.PrimaryKey.Length == 0)
			{
				throw new InvalidOperationException("Must specify PrimaryKey of DataTable");
			}
			base.RefreshCore(progress, this.CreatePartialRowRefreshRequest(identities, progress, priority));
			ExTraceGlobals.ProgramFlowTracer.TraceFunction<DataTableLoader>((long)this.GetHashCode(), "<--DataTableLoader.PartialRefresh: {0}", this);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00004CB6 File Offset: 0x00002EB6
		void ISupportFastRefresh.Remove(object identity)
		{
			this.FastRemoveImplement(identity);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00004CC0 File Offset: 0x00002EC0
		protected virtual void FastRemoveImplement(object identity)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identities");
			}
			if (this.Table.PrimaryKey.Length == 0)
			{
				throw new InvalidOperationException();
			}
			object[] array = this.ConvertToPrimaryKeysFromIdentity(identity);
			if (array != null)
			{
				DataRow dataRow = this.Table.Rows.Find(array);
				if (dataRow != null)
				{
					this.Table.Rows.Remove(dataRow);
				}
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00004D24 File Offset: 0x00002F24
		protected virtual object[] ConvertToPrimaryKeysFromIdentity(object identity)
		{
			if (identity is ADObjectId)
			{
				return new object[]
				{
					(identity as ADObjectId).ObjectGuid.ToString()
				};
			}
			if (this.ResultsLoaderProfile == null || string.IsNullOrEmpty(this.ResultsLoaderProfile.DistinguishIdentity))
			{
				return new object[]
				{
					identity
				};
			}
			DataRow dataRow = null;
			foreach (object obj in this.Table.Rows)
			{
				DataRow dataRow2 = (DataRow)obj;
				if (dataRow2[this.ResultsLoaderProfile.DistinguishIdentity].Equals(identity))
				{
					dataRow = dataRow2;
					break;
				}
			}
			if (dataRow != null)
			{
				List<object> list = new List<object>();
				for (int i = 0; i < this.Table.PrimaryKey.Length; i++)
				{
					list.Add(dataRow[this.Table.PrimaryKey[i]]);
				}
				return list.ToArray();
			}
			return null;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00004E44 File Offset: 0x00003044
		private RefreshRequestEventArgs CreatePartialRowRefreshRequest(object[] ids, IProgress progress, RefreshRequestPriority priority)
		{
			object argument;
			if (this.TryToGetPartialRefreshArgument(ids, out argument))
			{
				return new DataTableLoader.DataTableLoaderPartialRefreshRequestEventArgs(this.Table.Clone(), progress, argument, ids, priority);
			}
			return this.CreateFullRefreshRequest(progress);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00004E78 File Offset: 0x00003078
		protected virtual bool TryToGetPartialRefreshArgument(object[] ids, out object partialRefreshArgument)
		{
			partialRefreshArgument = null;
			if (ids != null)
			{
				if (this.ResultsLoaderProfile != null)
				{
					ResultsLoaderProfile resultsLoaderProfile = null;
					if (1 != ids.Length || this.ResultsLoaderProfile.InputTable.Columns.Contains("Identity"))
					{
						resultsLoaderProfile = (this.ResultsLoaderProfile.Clone() as ResultsLoaderProfile);
						resultsLoaderProfile.TryInputValue("IsFullRefresh", false);
						if (1 == ids.Length)
						{
							resultsLoaderProfile.InputValue("Identity", ids[0]);
						}
						else if (this.ResultsLoaderProfile.InputTable.Columns.Contains("IdentityList"))
						{
							resultsLoaderProfile.InputValue("IdentityList", ids);
						}
						else
						{
							resultsLoaderProfile.PipelineObjects = ids;
						}
					}
					partialRefreshArgument = resultsLoaderProfile;
				}
				else
				{
					partialRefreshArgument = ids;
				}
			}
			return null != partialRefreshArgument;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00004F38 File Offset: 0x00003138
		protected override void DoPostRefreshAction(RefreshRequestEventArgs refreshRequest)
		{
			ResultsLoaderProfile resultsLoaderProfile = refreshRequest.Argument as ResultsLoaderProfile;
			if (resultsLoaderProfile != null && resultsLoaderProfile.PostRefreshAction != null)
			{
				resultsLoaderProfile.PostRefreshAction.DoPostRefreshAction(this, refreshRequest);
			}
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00004F6C File Offset: 0x0000316C
		public IProgress CreateProgress(string operationName)
		{
			IProgressProvider progressProvider = (IProgressProvider)this.GetService(typeof(IProgressProvider));
			if (progressProvider != null)
			{
				return progressProvider.CreateProgress(operationName);
			}
			return NullProgress.Value;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00004F9F File Offset: 0x0000319F
		public override string ToString()
		{
			return base.GetType().Name;
		}

		// Token: 0x0400001D RID: 29
		private const int DefaultBatchSize = 100;

		// Token: 0x0400001E RID: 30
		private int batchSize = 100;

		// Token: 0x0400001F RID: 31
		private int expectedResultSize;

		// Token: 0x04000020 RID: 32
		private int lastRowCount;

		// Token: 0x04000021 RID: 33
		private LocalizedString progressText = LocalizedString.Empty;

		// Token: 0x04000022 RID: 34
		private LocalizedString refreshCommandText = LocalizedString.Empty;

		// Token: 0x04000023 RID: 35
		private DataTable table;

		// Token: 0x04000024 RID: 36
		private WorkUnitCollection workUnits;

		// Token: 0x04000025 RID: 37
		private ExpressionCalculator expressionCalculator;

		// Token: 0x04000026 RID: 38
		private IDataAdapterExecutionContextFactory executionContextFactory = new MonadDataAdapterExecutionContextFactory();

		// Token: 0x04000027 RID: 39
		private ResultsLoaderProfile resultsLoaderProfile;

		// Token: 0x04000028 RID: 40
		private int defaultExpectedResultSize = 100;

		// Token: 0x04000029 RID: 41
		private static readonly object EventExpectedResultSizeChanged = new object();

		// Token: 0x0400002A RID: 42
		private bool isCalculatingColumn;

		// Token: 0x0400002B RID: 43
		private static readonly object EventTableChanged = new object();

		// Token: 0x0400002C RID: 44
		private static readonly object EventFillTable = new object();

		// Token: 0x0400002D RID: 45
		private static readonly object EventCancelFill = new object();

		// Token: 0x0200000B RID: 11
		private interface IDataTableLoaderRefreshRequest
		{
			// Token: 0x17000025 RID: 37
			// (get) Token: 0x060000B2 RID: 178
			DataTable DataTable { get; }
		}

		// Token: 0x0200000D RID: 13
		private class DataTableLoaderRefreshRequestEventArgs : RefreshRequestEventArgs, DataTableLoader.IDataTableLoaderRefreshRequest
		{
			// Token: 0x060000BC RID: 188 RVA: 0x000050A0 File Offset: 0x000032A0
			public DataTableLoaderRefreshRequestEventArgs(DataTable dataTable, bool isFullRefresh, IProgress progress, object argument) : base(isFullRefresh, progress, argument)
			{
				this.DataTable = dataTable;
			}

			// Token: 0x1700002C RID: 44
			// (get) Token: 0x060000BD RID: 189 RVA: 0x000050B3 File Offset: 0x000032B3
			// (set) Token: 0x060000BE RID: 190 RVA: 0x000050BB File Offset: 0x000032BB
			public DataTable DataTable
			{
				get
				{
					return this.dataTable;
				}
				private set
				{
					this.dataTable = value;
				}
			}

			// Token: 0x04000034 RID: 52
			private DataTable dataTable;
		}

		// Token: 0x0200000F RID: 15
		private class DataTableLoaderPartialRefreshRequestEventArgs : PartialRefreshRequestEventArgs, DataTableLoader.IDataTableLoaderRefreshRequest
		{
			// Token: 0x060000C3 RID: 195 RVA: 0x000050F5 File Offset: 0x000032F5
			public DataTableLoaderPartialRefreshRequestEventArgs(DataTable dataTable, IProgress progress, object argument, object[] ids) : this(dataTable, progress, argument, ids, 0)
			{
			}

			// Token: 0x060000C4 RID: 196 RVA: 0x00005103 File Offset: 0x00003303
			public DataTableLoaderPartialRefreshRequestEventArgs(DataTable dataTable, IProgress progress, object argument, object[] ids, RefreshRequestPriority priority) : base(progress, argument, ids, priority)
			{
				this.DataTable = dataTable;
			}

			// Token: 0x1700002E RID: 46
			// (get) Token: 0x060000C5 RID: 197 RVA: 0x00005118 File Offset: 0x00003318
			// (set) Token: 0x060000C6 RID: 198 RVA: 0x00005120 File Offset: 0x00003320
			public DataTable DataTable
			{
				get
				{
					return this.dataTable;
				}
				private set
				{
					this.dataTable = value;
				}
			}

			// Token: 0x04000036 RID: 54
			private DataTable dataTable;
		}
	}
}
