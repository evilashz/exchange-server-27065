using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web;
using System.Xml.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200007D RID: 125
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MigrationReportItem : IMigrationSerializable
	{
		// Token: 0x060006E0 RID: 1760 RVA: 0x0001F3E8 File Offset: 0x0001D5E8
		private MigrationReportItem()
		{
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x0001F3F0 File Offset: 0x0001D5F0
		protected MigrationReportItem(string reportName, Guid? jobId, MigrationType migrationType, MigrationReportType reportType, bool isStaged)
		{
			this.ReportName = reportName;
			this.JobId = jobId;
			this.MigrationType = migrationType;
			this.ReportType = reportType;
			this.IsStagedMigration = isStaged;
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x060006E2 RID: 1762 RVA: 0x0001F41D File Offset: 0x0001D61D
		// (set) Token: 0x060006E3 RID: 1763 RVA: 0x0001F425 File Offset: 0x0001D625
		public long Version { get; protected set; }

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x060006E4 RID: 1764 RVA: 0x0001F42E File Offset: 0x0001D62E
		// (set) Token: 0x060006E5 RID: 1765 RVA: 0x0001F436 File Offset: 0x0001D636
		public StoreObjectId MessageId { get; protected set; }

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x060006E6 RID: 1766 RVA: 0x0001F43F File Offset: 0x0001D63F
		// (set) Token: 0x060006E7 RID: 1767 RVA: 0x0001F447 File Offset: 0x0001D647
		public ExDateTime CreationTime { get; protected set; }

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x060006E8 RID: 1768 RVA: 0x0001F450 File Offset: 0x0001D650
		// (set) Token: 0x060006E9 RID: 1769 RVA: 0x0001F458 File Offset: 0x0001D658
		public ExDateTime? ReportedTime { get; protected set; }

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x060006EA RID: 1770 RVA: 0x0001F461 File Offset: 0x0001D661
		public MigrationReportId Identifier
		{
			get
			{
				return new MigrationReportId(this.MessageId);
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x060006EB RID: 1771 RVA: 0x0001F46E File Offset: 0x0001D66E
		// (set) Token: 0x060006EC RID: 1772 RVA: 0x0001F476 File Offset: 0x0001D676
		public string ReportName { get; private set; }

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x060006ED RID: 1773 RVA: 0x0001F47F File Offset: 0x0001D67F
		// (set) Token: 0x060006EE RID: 1774 RVA: 0x0001F487 File Offset: 0x0001D687
		public Guid? JobId { get; private set; }

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x060006EF RID: 1775 RVA: 0x0001F490 File Offset: 0x0001D690
		// (set) Token: 0x060006F0 RID: 1776 RVA: 0x0001F498 File Offset: 0x0001D698
		public MigrationType MigrationType { get; private set; }

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x060006F1 RID: 1777 RVA: 0x0001F4A1 File Offset: 0x0001D6A1
		// (set) Token: 0x060006F2 RID: 1778 RVA: 0x0001F4A9 File Offset: 0x0001D6A9
		public MigrationReportType ReportType { get; private set; }

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x060006F3 RID: 1779 RVA: 0x0001F4B2 File Offset: 0x0001D6B2
		// (set) Token: 0x060006F4 RID: 1780 RVA: 0x0001F4BA File Offset: 0x0001D6BA
		public bool IsStagedMigration { get; private set; }

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x060006F5 RID: 1781 RVA: 0x0001F4C3 File Offset: 0x0001D6C3
		public PropertyDefinition[] PropertyDefinitions
		{
			get
			{
				return MigrationReportItem.MigrationReportItemColumnsIndex;
			}
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x0001F684 File Offset: 0x0001D884
		public static IEnumerable<MigrationReportItem> GetAll(IMigrationDataProvider dataProvider)
		{
			IEnumerable<StoreObjectId> messageIds = MigrationHelper.FindMessageIds(dataProvider, MigrationReportItem.MessageClassEqualityFilter, null, null, null);
			foreach (StoreObjectId messageId in messageIds)
			{
				yield return MigrationReportItem.Get(dataProvider, new MigrationReportId(messageId));
			}
			yield break;
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x0001F6A4 File Offset: 0x0001D8A4
		public static int GetCount(IMigrationDataProvider dataProvider, Guid jobId)
		{
			MigrationUtil.ThrowOnNullArgument(dataProvider, "dataProvider");
			MigrationUtil.ThrowOnGuidEmptyArgument(jobId, "jobId");
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, MigrationBatchMessageSchema.MigrationJobId, jobId);
			return dataProvider.CountMessages(filter, null);
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x0001F938 File Offset: 0x0001DB38
		public static IEnumerable<MigrationReportItem> GetByJobId(IMigrationDataProvider dataProvider, Guid? jobId, int maxCount)
		{
			MigrationEqualityFilter[] secondaryFilters = null;
			if (jobId != null)
			{
				secondaryFilters = new MigrationEqualityFilter[]
				{
					new MigrationEqualityFilter(MigrationBatchMessageSchema.MigrationJobId, jobId.Value)
				};
			}
			SortBy[] sortBy = new SortBy[]
			{
				new SortBy(InternalSchema.MigrationJobItemStateLastUpdated, SortOrder.Ascending),
				new SortBy(InternalSchema.CreationTime, SortOrder.Ascending)
			};
			IEnumerable<StoreObjectId> messageIds = MigrationHelper.FindMessageIds(dataProvider, MigrationReportItem.MessageClassEqualityFilter, secondaryFilters, sortBy, new int?(maxCount));
			foreach (StoreObjectId messageId in messageIds)
			{
				yield return MigrationReportItem.Get(dataProvider, new MigrationReportId(messageId));
			}
			yield break;
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x0001F964 File Offset: 0x0001DB64
		public static MigrationReportItem Get(IMigrationDataProvider dataProvider, MigrationReportId reportId)
		{
			MigrationReportItem result;
			using (IMigrationMessageItem migrationMessageItem = dataProvider.FindMessage(reportId.Id, MigrationReportItem.MigrationReportItemColumnsIndex))
			{
				MigrationReportItem migrationReportItem = new MigrationReportItem();
				migrationReportItem.ReadFromMessageItem(migrationMessageItem);
				result = migrationReportItem;
			}
			return result;
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x0001F9B0 File Offset: 0x0001DBB0
		public static MigrationReportItem Create(IMigrationDataProvider dataProvider, Guid? jobId, MigrationType migrationType, bool isStaged, MigrationReportType reportType, string reportName)
		{
			MigrationUtil.ThrowOnNullArgument(dataProvider, "dataProvider");
			MigrationUtil.ThrowOnNullOrEmptyArgument(reportName, "reportName");
			MigrationReportItem migrationReportItem = new MigrationReportItem(reportName, jobId, migrationType, reportType, isStaged);
			migrationReportItem.Version = 2L;
			using (IMigrationMessageItem migrationMessageItem = dataProvider.CreateMessage())
			{
				using (IMigrationAttachment migrationAttachment = migrationMessageItem.CreateAttachment(reportName))
				{
					migrationAttachment.Save(null);
				}
				migrationReportItem.WriteToMessageItem(migrationMessageItem, true);
				migrationMessageItem.Save(SaveMode.NoConflictResolution);
				migrationMessageItem.Load(MigrationHelper.ItemIdProperties);
				migrationReportItem.MessageId = migrationMessageItem.Id;
			}
			return migrationReportItem;
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x0001FACC File Offset: 0x0001DCCC
		public MigrationReport GetMigrationReport(IMigrationDataProvider dataProvider, Stream csvStream, int startingRowIndex, int rowCount, bool revealPassword)
		{
			MigrationUtil.ThrowOnNullArgument(dataProvider, "provider");
			MigrationReport migrationReport = new MigrationReport();
			migrationReport.Identity = this.Identifier;
			migrationReport.ReportName = this.ReportName;
			migrationReport.JobId = (this.JobId ?? Guid.Empty);
			migrationReport.MigrationType = this.MigrationType;
			migrationReport.ReportType = this.ReportType;
			using (IMigrationMessageItem migrationMessageItem = dataProvider.FindMessage(this.MessageId, MigrationReportItem.MigrationReportItemColumnsIndex))
			{
				using (IMigrationAttachment attachment = migrationMessageItem.GetAttachment(this.ReportName, PropertyOpenMode.ReadOnly))
				{
					CsvSchema csvSchema = null;
					switch (this.ReportType)
					{
					case MigrationReportType.BatchSuccessReport:
						csvSchema = MigrationSuccessReportCsvSchema.GetSchema(this.MigrationType, true, this.IsStagedMigration);
						break;
					case MigrationReportType.BatchFailureReport:
						csvSchema = MigrationFailureReportCsvSchema.GetSchema(this.MigrationType, true);
						break;
					case MigrationReportType.FinalizationSuccessReport:
						csvSchema = MigrationSuccessReportCsvSchema.GetSchema(this.MigrationType, false, this.IsStagedMigration);
						break;
					case MigrationReportType.FinalizationFailureReport:
						csvSchema = MigrationFailureReportCsvSchema.GetSchema(this.MigrationType, false);
						break;
					case MigrationReportType.BatchReport:
						csvSchema = new MigrationReportCsvSchema(MigrationJob.MigrationTypeSupportsProvisioning(this.MigrationType));
						break;
					}
					bool containsPasswordColumn = (this.MigrationType == MigrationType.ExchangeOutlookAnywhere && !this.IsStagedMigration && this.ReportType == MigrationReportType.BatchSuccessReport) || (this.ReportType == MigrationReportType.BatchReport && MigrationJob.MigrationTypeSupportsProvisioning(this.MigrationType));
					bool flag = false;
					Stream stream = null;
					Stream stream2 = csvStream;
					if (stream2 == null)
					{
						flag = true;
						stream = new MemoryStream();
						stream2 = stream;
					}
					using (stream)
					{
						if (csvSchema != null)
						{
							try
							{
								csvSchema.Copy(attachment.Stream, new StreamWriter(stream2), delegate(CsvRow source)
								{
									if (containsPasswordColumn && source.Index != 0)
									{
										if (revealPassword)
										{
											string encryptedString = source["Password"];
											string value = MigrationUtil.EncryptedStringToClearText(encryptedString);
											source["Password"] = value;
										}
										else
										{
											source["Password"] = "password hidden from DC admins";
										}
									}
									return source;
								});
								goto IL_1C4;
							}
							catch (CsvFileIsEmptyException)
							{
								goto IL_1C4;
							}
						}
						Util.StreamHandler.CopyStreamData(attachment.Stream, stream2);
						IL_1C4:
						if (flag)
						{
							stream.Seek(0L, SeekOrigin.Begin);
							migrationReport.Rows = MigrationReportItem.GetRows(stream2, startingRowIndex, rowCount);
						}
						else
						{
							migrationReport.Rows = new MultiValuedProperty<string>();
						}
					}
				}
			}
			return migrationReport;
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x0001FD54 File Offset: 0x0001DF54
		public string GetUrl(IMigrationDataProvider dataProvider)
		{
			Uri ecpUrl = dataProvider.GetEcpUrl();
			if (ecpUrl == null)
			{
				return null;
			}
			UriBuilder uriBuilder = new UriBuilder(ecpUrl);
			if (!uriBuilder.Path.EndsWith("/", StringComparison.OrdinalIgnoreCase))
			{
				UriBuilder uriBuilder2 = uriBuilder;
				uriBuilder2.Path += "/";
			}
			UriBuilder uriBuilder3 = uriBuilder;
			uriBuilder3.Path += "Migration/DownloadReport.aspx";
			string text = "OrganizationContext";
			if (dataProvider.ADProvider.IsMSOSyncEnabled)
			{
				text = "DelegatedOrg";
			}
			uriBuilder.Query = string.Format(CultureInfo.InvariantCulture, "HandlerClass=MigrationReportHandler&Name={0}&{1}={2}&realm={2}&exsvurl=1&Identity={3}", new object[]
			{
				HttpUtility.UrlEncode(this.ReportName),
				text,
				HttpUtility.UrlEncode(dataProvider.ADProvider.TenantOrganizationName),
				HttpUtility.UrlEncode(this.Identifier.ToString())
			});
			return uriBuilder.Uri.AbsoluteUri;
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x0001FE34 File Offset: 0x0001E034
		public void WriteStream(IMigrationDataProvider dataProvider, Action<StreamWriter> writer)
		{
			MigrationUtil.ThrowOnNullArgument(dataProvider, "dataProvider");
			MigrationUtil.ThrowOnNullArgument(writer, "writer");
			using (IMigrationMessageItem migrationMessageItem = dataProvider.FindMessage(this.MessageId, MigrationReportItem.MigrationReportItemColumnsIndex))
			{
				migrationMessageItem.OpenAsReadWrite();
				using (IMigrationAttachment attachment = migrationMessageItem.GetAttachment(this.ReportName, PropertyOpenMode.Modify))
				{
					attachment.Stream.Seek(0L, SeekOrigin.End);
					using (StreamWriter streamWriter = new StreamWriter(attachment.Stream, Encoding.UTF8))
					{
						writer(streamWriter);
					}
					attachment.Save(null);
					this.ReportedTime = new ExDateTime?(ExDateTime.UtcNow);
					migrationMessageItem[MigrationBatchMessageSchema.MigrationJobItemStateLastUpdated] = this.ReportedTime;
				}
				migrationMessageItem.Save(SaveMode.NoConflictResolution);
			}
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x0001FF24 File Offset: 0x0001E124
		public void UpdateReportItem(IMigrationDataProvider dataProvider, Guid jobId)
		{
			MigrationUtil.ThrowOnNullArgument(dataProvider, "dataProvider");
			using (IMigrationMessageItem migrationMessageItem = dataProvider.FindMessage(this.MessageId, MigrationReportItem.MigrationReportItemUpdate))
			{
				migrationMessageItem.OpenAsReadWrite();
				migrationMessageItem[MigrationBatchMessageSchema.MigrationJobId] = jobId;
				migrationMessageItem.Save(SaveMode.NoConflictResolution);
			}
			this.JobId = new Guid?(jobId);
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x0001FF94 File Offset: 0x0001E194
		public void WriteToMessageItem(IMigrationStoreObject message, bool loaded)
		{
			MigrationUtil.ThrowOnNullArgument(message, "message");
			message[MigrationBatchMessageSchema.MigrationVersion] = this.Version;
			message[StoreObjectSchema.ItemClass] = MigrationBatchMessageSchema.MigrationReportItemClass;
			message[MigrationBatchMessageSchema.MigrationReportName] = this.ReportName;
			if (this.Version == 1L)
			{
				return;
			}
			if (this.JobId != null)
			{
				message[MigrationBatchMessageSchema.MigrationJobId] = this.JobId;
			}
			message[MigrationBatchMessageSchema.MigrationType] = (int)this.MigrationType;
			message[MigrationBatchMessageSchema.MigrationReportType] = (int)this.ReportType;
			message[MigrationBatchMessageSchema.MigrationJobIsStaged] = this.IsStagedMigration;
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x00020058 File Offset: 0x0001E258
		public bool ReadFromMessageItem(IMigrationStoreObject message)
		{
			MigrationUtil.ThrowOnNullArgument(message, "message");
			this.MessageId = message.Id;
			this.CreationTime = message.CreationTime;
			this.Version = (long)message[MigrationBatchMessageSchema.MigrationVersion];
			if (this.Version > 3L)
			{
				throw new MigrationVersionMismatchException(this.Version, 3L);
			}
			this.ReportName = (string)message[MigrationBatchMessageSchema.MigrationReportName];
			if (this.Version > 1L)
			{
				Guid guidProperty = MigrationHelper.GetGuidProperty(message, MigrationBatchMessageSchema.MigrationJobId, false);
				if (guidProperty != Guid.Empty)
				{
					this.JobId = new Guid?(guidProperty);
				}
				this.MigrationType = (MigrationType)message[MigrationBatchMessageSchema.MigrationType];
				this.ReportType = (MigrationReportType)message[MigrationBatchMessageSchema.MigrationReportType];
				this.IsStagedMigration = message.GetValueOrDefault<bool>(MigrationBatchMessageSchema.MigrationJobIsStaged, false);
			}
			this.ReportedTime = MigrationHelper.GetExDateTimePropertyOrNull(message, MigrationBatchMessageSchema.MigrationJobItemStateLastUpdated);
			return true;
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x0002014C File Offset: 0x0001E34C
		public void Delete(IMigrationDataProvider provider)
		{
			MigrationUtil.ThrowOnNullArgument(provider, "provider");
			provider.RemoveMessage(this.MessageId);
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x00020168 File Offset: 0x0001E368
		public XElement GetDiagnosticInfo(IMigrationDataProvider dataProvider, MigrationDiagnosticArgument argument)
		{
			XElement xelement = new XElement("MigrationReportItem", new object[]
			{
				new XElement("ReportName", this.ReportName),
				new XElement("CreationTime", this.CreationTime),
				new XElement("ReportedTime", this.ReportedTime),
				new XElement("MessageId", this.MessageId),
				new XElement("Version", this.Version),
				new XElement("JobID", this.JobId),
				new XElement("MigrationType", this.MigrationType),
				new XElement("MigrationReportType", this.ReportType)
			});
			if (dataProvider != null && argument.HasArgument("storage"))
			{
				xelement.Add(this.GetStorageDiagnosticInfo(dataProvider, argument));
			}
			return xelement;
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x0002028C File Offset: 0x0001E48C
		private static MultiValuedProperty<string> GetRows(Stream sourceStream, int startingRowIndex, int rowCount)
		{
			if (rowCount < 0)
			{
				throw new ArgumentOutOfRangeException("rowCount", rowCount, "rowCount must be greater than 0.");
			}
			if (startingRowIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startingRowIndex", startingRowIndex, "startingRowIndex must be greater than 0.");
			}
			MultiValuedProperty<string> multiValuedProperty = new MultiValuedProperty<string>();
			using (StreamReader streamReader = new StreamReader(sourceStream, true))
			{
				int num = 0;
				string text = streamReader.ReadLine();
				if (text == null)
				{
					return multiValuedProperty;
				}
				multiValuedProperty.Add(text);
				while (multiValuedProperty.Count < rowCount + 1)
				{
					num++;
					text = streamReader.ReadLine();
					if (text == null)
					{
						break;
					}
					if (num > startingRowIndex)
					{
						multiValuedProperty.Add(text);
					}
				}
			}
			return multiValuedProperty;
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x0002033C File Offset: 0x0001E53C
		private XElement GetStorageDiagnosticInfo(IMigrationDataProvider dataProvider, MigrationDiagnosticArgument argument)
		{
			MigrationUtil.ThrowOnNullArgument(dataProvider, "dataProvider");
			ExAssert.RetailAssert(this.MessageId != null, "Need to persist the objects before trying to retrieve their diagnostics");
			XElement diagnosticInfo;
			using (IMigrationMessageItem migrationMessageItem = dataProvider.FindMessage(this.MessageId, MigrationReportItem.MigrationReportItemColumnsIndex))
			{
				diagnosticInfo = migrationMessageItem.GetDiagnosticInfo(this.PropertyDefinitions, argument);
			}
			return diagnosticInfo;
		}

		// Token: 0x040002E8 RID: 744
		private const string DownloadReportPagePath = "Migration/DownloadReport.aspx";

		// Token: 0x040002E9 RID: 745
		private const string MigrationReportHandlerQueryFormat = "HandlerClass=MigrationReportHandler&Name={0}&{1}={2}&realm={2}&exsvurl=1&Identity={3}";

		// Token: 0x040002EA RID: 746
		private const string BposOrgContextParameter = "DelegatedOrg";

		// Token: 0x040002EB RID: 747
		private const string EduOrgContextParameter = "OrganizationContext";

		// Token: 0x040002EC RID: 748
		protected const long MigrationReportItemLegacySupportedVersion = 2L;

		// Token: 0x040002ED RID: 749
		protected const long MigrationReportItemCurrentSupportedVersion = 3L;

		// Token: 0x040002EE RID: 750
		private static readonly MigrationEqualityFilter MessageClassEqualityFilter = new MigrationEqualityFilter(StoreObjectSchema.ItemClass, MigrationBatchMessageSchema.MigrationReportItemClass);

		// Token: 0x040002EF RID: 751
		internal static readonly PropertyDefinition[] MigrationReportItemColumnsIndex = MigrationHelper.AggregateProperties(new IList<PropertyDefinition>[]
		{
			new PropertyDefinition[]
			{
				StoreObjectSchema.ItemClass,
				MigrationBatchMessageSchema.MigrationVersion,
				MigrationBatchMessageSchema.MigrationReportName,
				MigrationBatchMessageSchema.MigrationJobId,
				MigrationBatchMessageSchema.MigrationType,
				MigrationBatchMessageSchema.MigrationReportType,
				MigrationBatchMessageSchema.MigrationJobIsStaged,
				MigrationBatchMessageSchema.MigrationJobItemStateLastUpdated
			},
			MigrationStoreObject.IdPropertyDefinition
		});

		// Token: 0x040002F0 RID: 752
		private static readonly PropertyDefinition[] MigrationReportItemUpdate = new PropertyDefinition[]
		{
			MigrationBatchMessageSchema.MigrationJobId
		};
	}
}
