using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Management.Automation;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring.Reporting
{
	// Token: 0x02000590 RID: 1424
	[Cmdlet("Get", "MessageLatencyReport", DefaultParameterSetName = "ReportingPeriod")]
	public sealed class GetMessageLatencyReport : TransportReportingTask
	{
		// Token: 0x17000EDF RID: 3807
		// (get) Token: 0x0600322F RID: 12847 RVA: 0x000CBB76 File Offset: 0x000C9D76
		// (set) Token: 0x06003230 RID: 12848 RVA: 0x000CBB8D File Offset: 0x000C9D8D
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan SlaTargetTimespan
		{
			get
			{
				return (EnhancedTimeSpan)base.Fields["SlaTargetTimespan"];
			}
			set
			{
				base.Fields["SlaTargetTimespan"] = value;
			}
		}

		// Token: 0x06003231 RID: 12849 RVA: 0x000CBBA8 File Offset: 0x000C9DA8
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			base.ValidateSProcExists("[Exchange2010].[TransportReporting_GetMessageLatency]");
			if (base.Fields.IsModified("SlaTargetTimespan") && (this.SlaTargetTimespan > EnhancedTimeSpan.FromMinutes(5.0) || this.SlaTargetTimespan < EnhancedTimeSpan.FromSeconds(1.0)))
			{
				base.WriteError(new ArgumentException(Strings.OutOfRangeSlaTaget(EnhancedTimeSpan.FromSeconds(1.0).ToString(), EnhancedTimeSpan.FromMinutes(5.0).ToString())), (ErrorCategory)1000, null);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06003232 RID: 12850 RVA: 0x000CBC70 File Offset: 0x000C9E70
		protected override void WriteStatistics(ADObjectId tenantId)
		{
			TaskLogger.LogEnter();
			SqlDataReader sqlDataReader = null;
			List<MessageLatencyReport> list = new List<MessageLatencyReport>(1);
			try
			{
				using (SqlConnection sqlConnection = new SqlConnection(base.GetSqlConnectionString()))
				{
					sqlConnection.Open();
					SqlCommand sqlCommand = new SqlCommand("[Exchange2010].[TransportReporting_GetMessageLatency]", sqlConnection);
					sqlCommand.CommandType = CommandType.StoredProcedure;
					if (tenantId != null && tenantId.ObjectGuid != Guid.Empty)
					{
						sqlCommand.Parameters.Add("@TenantGuid", SqlDbType.UniqueIdentifier).Value = tenantId.ObjectGuid;
					}
					else if (base.Fields.IsModified("AdSite"))
					{
						sqlCommand.Parameters.Add("@AdSite", SqlDbType.NVarChar).Value = base.AdSite.ToString();
					}
					sqlCommand.Parameters.Add("@DailyStatistics", SqlDbType.TinyInt).Value = (base.DailyStatistics ? 1 : 0);
					sqlCommand.Parameters.Add("@Target", SqlDbType.SmallInt).Value = this.GetSlaTargetTimeSpanInSeconds();
					if (base.ParameterSetName == "ReportingPeriod")
					{
						if (!base.Fields.IsModified("ReportingPeriod"))
						{
							base.ReportingPeriod = ReportingPeriod.LastMonth;
						}
						ExDateTime startDate;
						ExDateTime endDate;
						Utils.GetStartEndDateForReportingPeriod(base.ReportingPeriod, out startDate, out endDate);
						base.StartDate = startDate;
						base.EndDate = endDate;
					}
					sqlCommand.Parameters.Add("@IntervalStartDateTime", SqlDbType.DateTime).Value = (DateTime)base.StartDate;
					sqlCommand.Parameters.Add("@IntervalEndDateTime", SqlDbType.DateTime).Value = (DateTime)base.EndDate;
					sqlDataReader = sqlCommand.ExecuteReader();
					while (sqlDataReader.Read())
					{
						MessageLatencyReport messageLatencyReport = new MessageLatencyReport();
						if (tenantId != null)
						{
							messageLatencyReport.Identity = tenantId;
						}
						messageLatencyReport.SlaTargetInSeconds = this.GetSlaTargetTimeSpanInSeconds();
						if (base.DailyStatistics)
						{
							DateTime dateTime = (DateTime)sqlDataReader["AggregatedDateTime"];
							if (new ExDateTime(ExTimeZone.UtcTimeZone, dateTime).Date < base.StartDate)
							{
								base.WriteError(new ArgumentException(Strings.InvalidAggregatedDateTime), (ErrorCategory)1002, null);
							}
							messageLatencyReport.StartDate = new ExDateTime(ExTimeZone.UtcTimeZone, dateTime.Date);
							messageLatencyReport.EndDate = new ExDateTime(ExTimeZone.UtcTimeZone, dateTime.AddDays(1.0).Subtract(TimeSpan.FromMinutes(1.0)));
						}
						else
						{
							messageLatencyReport.StartDate = base.StartDate;
							messageLatencyReport.EndDate = base.EndDate;
						}
						if (DBNull.Value.Equals(sqlDataReader["PercentageOfMessagesMeetingTarget"]) || DBNull.Value.Equals(sqlDataReader["PercentageOfMessagesMeetingTarget"]))
						{
							messageLatencyReport.PercentOfMessageInGivenSla = 100m;
						}
						else
						{
							messageLatencyReport.PercentOfMessageInGivenSla = (decimal)sqlDataReader["PercentageOfMessagesMeetingTarget"];
						}
						list.Add(messageLatencyReport);
					}
				}
			}
			catch (SqlException ex)
			{
				if (!Datacenter.IsMultiTenancyEnabled() && ex.Number == 53)
				{
					base.WriteError(new InvalidOperationException(Strings.ScomMayNotBeInstalled(ex.Message)), (ErrorCategory)1001, null);
				}
				else
				{
					base.WriteError(ex, (ErrorCategory)1002, null);
				}
			}
			finally
			{
				if (sqlDataReader != null)
				{
					sqlDataReader.Close();
				}
			}
			if (base.DailyStatistics)
			{
				IEnumerable<StartEndDateTimePair> allDaysInGivenRange = Utils.GetAllDaysInGivenRange(base.StartDate, base.EndDate);
				int num = 0;
				using (IEnumerator<StartEndDateTimePair> enumerator = allDaysInGivenRange.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						StartEndDateTimePair startEndDateTimePair = enumerator.Current;
						if (num < list.Count && startEndDateTimePair.StartDate.Date == list[num].StartDate.Date)
						{
							base.WriteResult(list[num]);
							num++;
						}
						else
						{
							MessageLatencyReport messageLatencyReport2 = new MessageLatencyReport();
							messageLatencyReport2.StartDate = startEndDateTimePair.StartDate;
							messageLatencyReport2.EndDate = startEndDateTimePair.EndDate;
							messageLatencyReport2.PercentOfMessageInGivenSla = 100m;
							if (tenantId != null)
							{
								messageLatencyReport2.Identity = tenantId;
							}
							messageLatencyReport2.SlaTargetInSeconds = this.GetSlaTargetTimeSpanInSeconds();
							base.WriteResult(messageLatencyReport2);
						}
					}
					goto IL_470;
				}
			}
			foreach (MessageLatencyReport dataObject in list)
			{
				base.WriteResult(dataObject);
			}
			IL_470:
			TaskLogger.LogExit();
		}

		// Token: 0x06003233 RID: 12851 RVA: 0x000CC170 File Offset: 0x000CA370
		private short GetSlaTargetTimeSpanInSeconds()
		{
			if (base.Fields.IsModified("SlaTargetTimespan"))
			{
				return (short)this.SlaTargetTimespan.TotalSeconds;
			}
			return (short)GetMessageLatencyReport.DefaultSlaTargetTimeSpan.TotalSeconds;
		}

		// Token: 0x0400234A RID: 9034
		private const string CmdletNoun = "MessageLatencyReport";

		// Token: 0x0400234B RID: 9035
		private const string SPName = "[Exchange2010].[TransportReporting_GetMessageLatency]";

		// Token: 0x0400234C RID: 9036
		private const ReportingPeriod DefualtReportingPeriod = ReportingPeriod.LastMonth;

		// Token: 0x0400234D RID: 9037
		private static readonly EnhancedTimeSpan DefaultSlaTargetTimeSpan = EnhancedTimeSpan.FromMinutes(1.0) + EnhancedTimeSpan.FromSeconds(30.0);
	}
}
