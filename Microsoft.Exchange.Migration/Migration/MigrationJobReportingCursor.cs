using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Xml;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Migration.Logging;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000130 RID: 304
	internal class MigrationJobReportingCursor
	{
		// Token: 0x06000F5A RID: 3930 RVA: 0x00041F8C File Offset: 0x0004018C
		public MigrationJobReportingCursor(ReportingStageEnum stage = ReportingStageEnum.Unknown)
		{
			this.reportingStage = stage;
			this.MigrationSuccessCount = new MigrationObjectsCount(new int?(0));
			this.MigrationErrorCount = new MigrationObjectsCount(new int?(0));
			this.PartialMigrationCounts = 0;
		}

		// Token: 0x06000F5B RID: 3931 RVA: 0x00041FC4 File Offset: 0x000401C4
		public MigrationJobReportingCursor(ReportingStageEnum stage, string cursorPosition, MigrationReportId successReportId, MigrationReportId failureReportId) : this(stage)
		{
			MigrationUtil.ThrowOnNullArgument(successReportId, "successReportId");
			MigrationUtil.ThrowOnNullArgument(failureReportId, "failureReportId");
			this.currentCursorPosition = cursorPosition;
			this.successReportId = successReportId;
			this.failureReportId = failureReportId;
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06000F5C RID: 3932 RVA: 0x00041FFA File Offset: 0x000401FA
		public static string MoacHelpUrlFormat
		{
			get
			{
				return "http://go.microsoft.com/fwlink/?LinkId=183883&clcid=0x{0:X4}";
			}
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x06000F5D RID: 3933 RVA: 0x00042001 File Offset: 0x00040201
		public MigrationReportId SuccessReportId
		{
			get
			{
				return this.successReportId;
			}
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x06000F5E RID: 3934 RVA: 0x00042009 File Offset: 0x00040209
		public MigrationReportId FailureReportId
		{
			get
			{
				return this.failureReportId;
			}
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x06000F5F RID: 3935 RVA: 0x00042011 File Offset: 0x00040211
		public ReportingStageEnum ReportingStage
		{
			get
			{
				return this.reportingStage;
			}
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06000F60 RID: 3936 RVA: 0x00042019 File Offset: 0x00040219
		public string CurrentCursorPosition
		{
			get
			{
				return this.currentCursorPosition;
			}
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06000F61 RID: 3937 RVA: 0x00042021 File Offset: 0x00040221
		// (set) Token: 0x06000F62 RID: 3938 RVA: 0x00042029 File Offset: 0x00040229
		public MigrationObjectsCount MigrationSuccessCount { get; set; }

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06000F63 RID: 3939 RVA: 0x00042032 File Offset: 0x00040232
		// (set) Token: 0x06000F64 RID: 3940 RVA: 0x0004203A File Offset: 0x0004023A
		public MigrationObjectsCount MigrationErrorCount { get; set; }

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06000F65 RID: 3941 RVA: 0x00042043 File Offset: 0x00040243
		// (set) Token: 0x06000F66 RID: 3942 RVA: 0x0004204B File Offset: 0x0004024B
		public int PartialMigrationCounts { get; set; }

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06000F67 RID: 3943 RVA: 0x00042054 File Offset: 0x00040254
		// (set) Token: 0x06000F68 RID: 3944 RVA: 0x0004205C File Offset: 0x0004025C
		public TimeSpan? SyncDuration { get; set; }

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06000F69 RID: 3945 RVA: 0x00042065 File Offset: 0x00040265
		public bool HasErrors
		{
			get
			{
				return this.MigrationErrorCount.GetTotal() > 0;
			}
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06000F6A RID: 3946 RVA: 0x00042075 File Offset: 0x00040275
		public bool AreSuccessfulMigrationsPresent
		{
			get
			{
				return this.MigrationSuccessCount.GetTotal() > 0;
			}
		}

		// Token: 0x06000F6B RID: 3947 RVA: 0x00042088 File Offset: 0x00040288
		public static string GetLicensingHtml(string moacHelpUrl)
		{
			if (string.IsNullOrEmpty(moacHelpUrl))
			{
				return string.Empty;
			}
			MigrationUtil.ThrowOnNullOrEmptyArgument(moacHelpUrl, "moacHelpUrl");
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("<tr>");
			stringBuilder.AppendLine("<td colspan=\"2\" class=\"spacer\">&nbsp;</td>");
			stringBuilder.AppendLine("</tr>");
			stringBuilder.AppendLine("<tr>");
			stringBuilder.AppendLine("<td width=\"16px\" valign=\"top\"><img width=\"16\" height=\"16\" src=\"cid:Information\" /></td>");
			stringBuilder.AppendLine(string.Format(CultureInfo.CurrentCulture, "<td>{0}</td>", new object[]
			{
				Strings.MoacWarningMessage(moacHelpUrl)
			}));
			stringBuilder.AppendLine("</tr>");
			return stringBuilder.ToString();
		}

		// Token: 0x06000F6C RID: 3948 RVA: 0x00042130 File Offset: 0x00040330
		public static MigrationJobReportingCursor Deserialize(string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				return null;
			}
			MigrationJobReportingCursor result;
			try
			{
				PersistableDictionary persistableDictionary = PersistableDictionary.Create(s);
				ReportingStageEnum stage = (ReportingStageEnum)persistableDictionary["ReportingStage"];
				result = new MigrationJobReportingCursor(stage, (string)persistableDictionary["CurrentCursorPosition"], new MigrationReportId((string)persistableDictionary["SuccessReportId"]), new MigrationReportId((string)persistableDictionary["FailureReportId"]))
				{
					MigrationSuccessCount = MigrationObjectsCount.FromValue((string)persistableDictionary["SuccessCount"]),
					MigrationErrorCount = MigrationObjectsCount.FromValue((string)persistableDictionary["FailureCount"]),
					PartialMigrationCounts = (int)persistableDictionary["PartialMigrationCounts"],
					SyncDuration = persistableDictionary.Get<TimeSpan?>("SyncDuration", null)
				};
			}
			catch (XmlException exception)
			{
				MigrationLogger.Log(MigrationEventType.Error, exception, "Xml Exception occured trying to parse deserialize MigrationJobReportingCursor. Data was {0}", new object[]
				{
					s
				});
				result = null;
			}
			return result;
		}

		// Token: 0x06000F6D RID: 3949 RVA: 0x00042240 File Offset: 0x00040440
		public string Serialize()
		{
			PersistableDictionary persistableDictionary = new PersistableDictionary();
			persistableDictionary.Add("ReportingStage", (int)this.ReportingStage);
			persistableDictionary.Add("CurrentCursorPosition", this.CurrentCursorPosition);
			persistableDictionary.Add("SuccessReportId", this.SuccessReportId.ToString());
			persistableDictionary.Add("FailureReportId", this.FailureReportId.ToString());
			persistableDictionary.Add("FailureCount", this.MigrationErrorCount.ToValue());
			persistableDictionary.Add("SuccessCount", this.MigrationSuccessCount.ToValue());
			persistableDictionary.Add("PartialMigrationCounts", this.PartialMigrationCounts);
			persistableDictionary.Set<TimeSpan?>("SyncDuration", this.SyncDuration);
			return persistableDictionary.Serialize();
		}

		// Token: 0x06000F6E RID: 3950 RVA: 0x00042300 File Offset: 0x00040500
		public MigrationJobReportingCursor GetNextCursor(ReportingStageEnum stage, string cursorPosition)
		{
			return new MigrationJobReportingCursor(stage, cursorPosition, this.SuccessReportId, this.FailureReportId)
			{
				MigrationErrorCount = this.MigrationErrorCount,
				MigrationSuccessCount = this.MigrationSuccessCount,
				PartialMigrationCounts = this.PartialMigrationCounts,
				SyncDuration = this.SyncDuration
			};
		}

		// Token: 0x0400054E RID: 1358
		private const string ReportingStageKey = "ReportingStage";

		// Token: 0x0400054F RID: 1359
		private const string CurrentCursorPositionKey = "CurrentCursorPosition";

		// Token: 0x04000550 RID: 1360
		private const string SuccessReportIdKey = "SuccessReportId";

		// Token: 0x04000551 RID: 1361
		private const string FailureReportIdKey = "FailureReportId";

		// Token: 0x04000552 RID: 1362
		private const string PartialMigrationCountsKey = "PartialMigrationCounts";

		// Token: 0x04000553 RID: 1363
		private const string FailureCountKey = "FailureCount";

		// Token: 0x04000554 RID: 1364
		private const string SuccessCountKey = "SuccessCount";

		// Token: 0x04000555 RID: 1365
		private const string SyncDurationKey = "SyncDuration";

		// Token: 0x04000556 RID: 1366
		internal static readonly HashSet<MigrationUserStatus> FailureStatuses = new HashSet<MigrationUserStatus>(MigrationJobItem.ErrorStatuses);

		// Token: 0x04000557 RID: 1367
		private readonly ReportingStageEnum reportingStage;

		// Token: 0x04000558 RID: 1368
		private readonly string currentCursorPosition;

		// Token: 0x04000559 RID: 1369
		private readonly MigrationReportId successReportId;

		// Token: 0x0400055A RID: 1370
		private readonly MigrationReportId failureReportId;
	}
}
