using System;
using System.Data;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Hygiene.Data.DataProvider;
using Microsoft.Exchange.Net.DiagnosticsAggregation;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x020001A0 RID: 416
	internal class TransportQueueSession : HygieneSession
	{
		// Token: 0x06001182 RID: 4482 RVA: 0x00035B09 File Offset: 0x00033D09
		public TransportQueueSession()
		{
			this.dataProviderMtrt = ConfigDataProviderFactory.Default.Create(DatabaseType.Mtrt);
		}

		// Token: 0x06001183 RID: 4483 RVA: 0x00035B24 File Offset: 0x00033D24
		public void Save(TransportQueueLogBatch serverQueueData)
		{
			if (serverQueueData == null)
			{
				throw new ArgumentNullException("serverQueueData");
			}
			TransportQueueSession.CheckInputType(serverQueueData);
			TransportQueueLogSaveDataSet instance = TransportQueueLogSaveDataSet.CreateDataSet(serverQueueData);
			this.dataProviderMtrt.Save(instance);
		}

		// Token: 0x06001184 RID: 4484 RVA: 0x00035B59 File Offset: 0x00033D59
		public IPagedReader<TransportQueueStatistics> FindTransportQueueInfo(QueryFilter filter, int pageSize = 1000)
		{
			return new ConfigDataProviderPagedReader<TransportQueueStatistics>(this.dataProviderMtrt, null, filter, null, pageSize);
		}

		// Token: 0x06001185 RID: 4485 RVA: 0x00035B6C File Offset: 0x00033D6C
		public IPagedReader<TransportQueueStatistics> FindTransportQueueInfo(Guid forestId, string aggregatedBy, TimeSpan freshnessCutoffTime, MultiValuedProperty<Guid> adSiteIdList = null, MultiValuedProperty<Guid> dagIdList = null, MultiValuedProperty<Guid> serverIdList = null, MultiValuedProperty<ComparisonFilter> dataFilter = null, DetailsLevel detailsLevel = DetailsLevel.None, int pageSize = 100, int detailsResultsSize = 20)
		{
			if (forestId == Guid.Empty)
			{
				throw new ArgumentNullException("forestId");
			}
			if (string.IsNullOrWhiteSpace(aggregatedBy))
			{
				throw new ArgumentNullException("aggregatedBy");
			}
			QueryFilter filter = new AndFilter(new ComparisonFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, TransportQueueStatisticsSchema.ForestIdQueryProperty, forestId),
				new ComparisonFilter(ComparisonOperator.Equal, TransportQueueStatisticsSchema.AggregatedByQueryProperty, aggregatedBy),
				new ComparisonFilter(ComparisonOperator.Equal, TransportQueueStatisticsSchema.FreshnessCutoffTimeSecondsProperty, freshnessCutoffTime.TotalSeconds),
				new ComparisonFilter(ComparisonOperator.Equal, TransportQueueStatisticsSchema.DetailsLevelQueryProperty, detailsLevel),
				new ComparisonFilter(ComparisonOperator.Equal, TransportQueueStatisticsSchema.PageSizeQueryProperty, pageSize),
				new ComparisonFilter(ComparisonOperator.Equal, TransportQueueStatisticsSchema.DetailsResultSizeQueryProperty, detailsResultsSize),
				new ComparisonFilter(ComparisonOperator.Equal, TransportQueueStatisticsSchema.ADSiteQueryProperty, TransportQueueSession.GetFilter(adSiteIdList)),
				new ComparisonFilter(ComparisonOperator.Equal, TransportQueueStatisticsSchema.DagQueryProperty, TransportQueueSession.GetFilter(dagIdList)),
				new ComparisonFilter(ComparisonOperator.Equal, TransportQueueStatisticsSchema.ServerQueryProperty, TransportQueueSession.GetFilter(serverIdList)),
				new ComparisonFilter(ComparisonOperator.Equal, TransportQueueStatisticsSchema.DataFilterQueryProperty, TransportQueueSession.GetDataFilter(dataFilter))
			});
			return this.FindTransportQueueInfo(filter, pageSize);
		}

		// Token: 0x06001186 RID: 4486 RVA: 0x00035C90 File Offset: 0x00033E90
		private static string CheckInputType(IConfigurable obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("TransportQueueSession input");
			}
			string name = obj.GetType().Name;
			if (!(obj is TransportQueueLogBatch))
			{
				throw new InvalidObjectTypeForSessionException(HygieneDataStrings.ErrorInvalidObjectTypeForSession("TransportQueueSession", name));
			}
			return name;
		}

		// Token: 0x06001187 RID: 4487 RVA: 0x00035CD4 File Offset: 0x00033ED4
		private static DataTable GetFilter(MultiValuedProperty<Guid> guidList)
		{
			DataTable dataTable = new DataTable("ReportFilterTable");
			DataColumn column = new DataColumn("ReportFilter", typeof(string));
			dataTable.Columns.Add(column);
			if (guidList != null)
			{
				foreach (Guid guid in guidList)
				{
					TransportQueueSession.AddReportFilterRow(dataTable, guid.ToString());
				}
			}
			return dataTable;
		}

		// Token: 0x06001188 RID: 4488 RVA: 0x00035D60 File Offset: 0x00033F60
		private static void AddReportFilterRow(DataTable table, string value)
		{
			DataRow dataRow = table.NewRow();
			dataRow[0] = value;
			table.Rows.Add(dataRow);
		}

		// Token: 0x06001189 RID: 4489 RVA: 0x00035D88 File Offset: 0x00033F88
		private static DataTable GetDataFilter(MultiValuedProperty<ComparisonFilter> dataFilterList)
		{
			BatchPropertyTable batchPropertyTable = new BatchPropertyTable();
			if (dataFilterList != null)
			{
				foreach (ComparisonFilter filter in dataFilterList)
				{
					TransportQueueSession.AddDataFilterRow(batchPropertyTable, filter);
				}
			}
			return batchPropertyTable;
		}

		// Token: 0x0600118A RID: 4490 RVA: 0x00035DE0 File Offset: 0x00033FE0
		private static void AddDataFilterRow(BatchPropertyTable table, ComparisonFilter filter)
		{
			if (filter != null)
			{
				Guid identity = CombGuidGenerator.NewGuid();
				table.AddPropertyValue(identity, TransportQueueSchema.FilterNameProperty, filter.Property.Name);
				table.AddPropertyValue(identity, TransportQueueSchema.OperatorProperty, filter.ComparisonOperator.ToString());
				table.AddPropertyValue(identity, TransportQueueSchema.FilterValueProperty, filter.PropertyValue.ToString());
			}
		}

		// Token: 0x04000859 RID: 2137
		private readonly IConfigDataProvider dataProviderMtrt;
	}
}
