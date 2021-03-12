using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001C9 RID: 457
	internal class RequestJobLog : ObjectLog<RequestJobLogData>
	{
		// Token: 0x060012B2 RID: 4786 RVA: 0x0002AE5D File Offset: 0x0002905D
		private RequestJobLog() : base(new RequestJobLog.RequestJobLogSchema(), new SimpleObjectLogConfiguration("Request", "RequestLogEnabled", "RequestLogMaxDirSize", "RequestLogMaxFileSize"))
		{
		}

		// Token: 0x060012B3 RID: 4787 RVA: 0x0002AE83 File Offset: 0x00029083
		public static void Write(RequestJobBase request)
		{
			RequestJobLog.instance.LogObject(new RequestJobLogData(request));
		}

		// Token: 0x060012B4 RID: 4788 RVA: 0x0002AE95 File Offset: 0x00029095
		public static void Write(RequestJobBase request, RequestState statusDetail)
		{
			RequestJobLog.instance.LogObject(new RequestJobLogData(request, statusDetail));
		}

		// Token: 0x040009B4 RID: 2484
		private static RequestJobLog instance = new RequestJobLog();

		// Token: 0x020001CA RID: 458
		private class RequestJobLogSchema : ConfigurableObjectLogSchema<RequestJobLogData, RequestJobSchema>
		{
			// Token: 0x17000634 RID: 1588
			// (get) Token: 0x060012B6 RID: 4790 RVA: 0x0002AEB4 File Offset: 0x000290B4
			public override string Software
			{
				get
				{
					return "Microsoft Exchange Mailbox Replication Service";
				}
			}

			// Token: 0x17000635 RID: 1589
			// (get) Token: 0x060012B7 RID: 4791 RVA: 0x0002AEBB File Offset: 0x000290BB
			public override string LogType
			{
				get
				{
					return "Request Log";
				}
			}

			// Token: 0x17000636 RID: 1590
			// (get) Token: 0x060012B8 RID: 4792 RVA: 0x0002AEC2 File Offset: 0x000290C2
			public override HashSet<string> ExcludedProperties
			{
				get
				{
					return RequestJobLog.RequestJobLogSchema.excludedProperties;
				}
			}

			// Token: 0x060012B9 RID: 4793 RVA: 0x0002AECC File Offset: 0x000290CC
			private static IEnumerable<IObjectLogPropertyDefinition<RequestJobLogData>> GetTimeTrackerTimestamps()
			{
				List<IObjectLogPropertyDefinition<RequestJobLogData>> list = new List<IObjectLogPropertyDefinition<RequestJobLogData>>();
				foreach (object obj in Enum.GetValues(typeof(RequestJobTimestamp)))
				{
					RequestJobTimestamp requestJobTimestamp = (RequestJobTimestamp)obj;
					if (RequestJobTimeTracker.SupportTimestampTracking(requestJobTimestamp))
					{
						list.Add(new RequestJobLog.RequestJobLogSchema.TimeTrackerTimeStampProperty(requestJobTimestamp));
					}
				}
				return list;
			}

			// Token: 0x060012BA RID: 4794 RVA: 0x0002AF44 File Offset: 0x00029144
			private static IEnumerable<IObjectLogPropertyDefinition<RequestJobLogData>> GetTimeTrackerDurations()
			{
				List<IObjectLogPropertyDefinition<RequestJobLogData>> list = new List<IObjectLogPropertyDefinition<RequestJobLogData>>();
				foreach (object obj in Enum.GetValues(typeof(RequestState)))
				{
					RequestState requestState = (RequestState)obj;
					if (RequestJobTimeTracker.SupportDurationTracking(requestState))
					{
						list.Add(new RequestJobLog.RequestJobLogSchema.TimeTrackerDurationProperty(requestState));
					}
				}
				return list;
			}

			// Token: 0x040009B5 RID: 2485
			private static HashSet<string> excludedProperties = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase)
			{
				RequestJobSchema.RetryCount.Name,
				RequestJobSchema.UserId.Name,
				RequestJobSchema.DisplayName.Name,
				RequestJobSchema.Alias.Name,
				RequestJobSchema.SourceAlias.Name,
				RequestJobSchema.TargetAlias.Name,
				RequestJobSchema.SourceRootFolder.Name,
				RequestJobSchema.TargetRootFolder.Name,
				RequestJobSchema.IncludeFolders.Name,
				RequestJobSchema.ExcludeFolders.Name,
				RequestJobSchema.EmailAddress.Name,
				RequestJobSchema.RemoteCredentialUsername.Name,
				RequestJobSchema.Name.Name,
				RequestJobSchema.SourceUserId.Name,
				RequestJobSchema.TargetUserId.Name,
				RequestJobSchema.RemoteUserName.Name,
				RequestJobSchema.RemoteMailboxLegacyDN.Name,
				RequestJobSchema.RemoteUserLegacyDN.Name,
				RequestJobSchema.RequestCreator.Name
			};

			// Token: 0x040009B6 RID: 2486
			public static readonly ObjectLogSimplePropertyDefinition<RequestJobLogData> ExternalDirectoryOrganizationId = new ObjectLogSimplePropertyDefinition<RequestJobLogData>("ExternalDirectoryOrganizationId", (RequestJobLogData rj) => rj.Request.ExternalDirectoryOrganizationId);

			// Token: 0x040009B7 RID: 2487
			public static readonly ObjectLogSimplePropertyDefinition<RequestJobLogData> StatusDetail = new ObjectLogSimplePropertyDefinition<RequestJobLogData>("StatusDetail", delegate(RequestJobLogData rj)
			{
				string result;
				if (!rj.TryGetOverride("StatusDetail", out result))
				{
					return rj.Request.TimeTracker.CurrentState.ToString();
				}
				return result;
			});

			// Token: 0x040009B8 RID: 2488
			public static readonly IEnumerable<IObjectLogPropertyDefinition<RequestJobLogData>> TimeTrackerTimestamps = RequestJobLog.RequestJobLogSchema.GetTimeTrackerTimestamps();

			// Token: 0x040009B9 RID: 2489
			public static readonly IEnumerable<IObjectLogPropertyDefinition<RequestJobLogData>> TimeTrackerDurations = RequestJobLog.RequestJobLogSchema.GetTimeTrackerDurations();

			// Token: 0x020001CB RID: 459
			private class TimeTrackerTimeStampProperty : IObjectLogPropertyDefinition<RequestJobLogData>
			{
				// Token: 0x060012BF RID: 4799 RVA: 0x0002B1DD File Offset: 0x000293DD
				public TimeTrackerTimeStampProperty(RequestJobTimestamp rjts)
				{
					this.rjts = rjts;
				}

				// Token: 0x17000637 RID: 1591
				// (get) Token: 0x060012C0 RID: 4800 RVA: 0x0002B1EC File Offset: 0x000293EC
				string IObjectLogPropertyDefinition<RequestJobLogData>.FieldName
				{
					get
					{
						return string.Format("TS_{0}", this.rjts);
					}
				}

				// Token: 0x060012C1 RID: 4801 RVA: 0x0002B204 File Offset: 0x00029404
				object IObjectLogPropertyDefinition<RequestJobLogData>.GetValue(RequestJobLogData rj)
				{
					DateTime? timestamp = rj.Request.TimeTracker.GetTimestamp(this.rjts);
					if (timestamp != null)
					{
						return timestamp.Value;
					}
					return null;
				}

				// Token: 0x040009BC RID: 2492
				private RequestJobTimestamp rjts;
			}

			// Token: 0x020001CC RID: 460
			private class TimeTrackerDurationProperty : IObjectLogPropertyDefinition<RequestJobLogData>
			{
				// Token: 0x060012C2 RID: 4802 RVA: 0x0002B23F File Offset: 0x0002943F
				public TimeTrackerDurationProperty(RequestState rs)
				{
					this.rs = rs;
				}

				// Token: 0x17000638 RID: 1592
				// (get) Token: 0x060012C3 RID: 4803 RVA: 0x0002B24E File Offset: 0x0002944E
				string IObjectLogPropertyDefinition<RequestJobLogData>.FieldName
				{
					get
					{
						return string.Format("Duration_{0}", this.rs);
					}
				}

				// Token: 0x060012C4 RID: 4804 RVA: 0x0002B268 File Offset: 0x00029468
				object IObjectLogPropertyDefinition<RequestJobLogData>.GetValue(RequestJobLogData rj)
				{
					RequestJobDurationData duration = rj.Request.TimeTracker.GetDuration(this.rs);
					if (duration != null)
					{
						return (long)duration.Duration.TotalMilliseconds;
					}
					return null;
				}

				// Token: 0x040009BD RID: 2493
				private RequestState rs;
			}
		}
	}
}
