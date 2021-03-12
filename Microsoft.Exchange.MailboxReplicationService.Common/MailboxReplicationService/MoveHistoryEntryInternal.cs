using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000160 RID: 352
	public sealed class MoveHistoryEntryInternal : XMLSerializableBase, IComparable<MoveHistoryEntryInternal>
	{
		// Token: 0x06000C62 RID: 3170 RVA: 0x0001D6EA File Offset: 0x0001B8EA
		public MoveHistoryEntryInternal()
		{
			this.hasReadCompressedEntries = false;
		}

		// Token: 0x06000C63 RID: 3171 RVA: 0x0001D6FC File Offset: 0x0001B8FC
		internal MoveHistoryEntryInternal(RequestJobBase requestJob, ReportData report)
		{
			this.hasReadCompressedEntries = false;
			this.Status = (int)requestJob.Status;
			this.Flags = (int)requestJob.Flags;
			this.SourceDatabase = ADObjectIdXML.Serialize(requestJob.SourceDatabase);
			this.SourceVersion = requestJob.SourceVersion;
			this.SourceServer = requestJob.SourceServer;
			this.SourceArchiveDatabase = ADObjectIdXML.Serialize(requestJob.SourceArchiveDatabase);
			this.SourceArchiveVersion = requestJob.SourceArchiveVersion;
			this.SourceArchiveServer = requestJob.SourceArchiveServer;
			this.DestinationDatabase = ADObjectIdXML.Serialize(requestJob.TargetDatabase);
			this.DestinationVersion = requestJob.TargetVersion;
			this.DestinationServer = requestJob.TargetServer;
			this.DestinationArchiveDatabase = ADObjectIdXML.Serialize(requestJob.TargetArchiveDatabase);
			this.DestinationArchiveVersion = requestJob.TargetArchiveVersion;
			this.DestinationArchiveServer = requestJob.TargetArchiveServer;
			this.RemoteHostName = requestJob.RemoteHostName;
			if (requestJob.RemoteCredential == null)
			{
				this.RemoteCredentialUserName = null;
			}
			else if (requestJob.RemoteCredential.Domain == null)
			{
				this.RemoteCredentialUserName = requestJob.RemoteCredential.UserName;
			}
			else
			{
				this.RemoteCredentialUserName = requestJob.RemoteCredential.Domain + "\\" + requestJob.RemoteCredential.UserName;
			}
			this.RemoteDatabaseName = requestJob.RemoteDatabaseName;
			this.RemoteArchiveDatabaseName = requestJob.RemoteArchiveDatabaseName;
			this.BadItemLimit = requestJob.BadItemLimit;
			this.BadItemsEncountered = requestJob.BadItemsEncountered;
			this.LargeItemLimit = requestJob.LargeItemLimit;
			this.LargeItemsEncountered = requestJob.LargeItemsEncountered;
			this.MissingItemsEncountered = requestJob.MissingItemsEncountered;
			this.TimeTracker = requestJob.TimeTracker;
			this.MRSServerName = requestJob.MRSServerName;
			this.TotalMailboxSize = requestJob.TotalMailboxSize;
			this.TotalMailboxItemCount = requestJob.TotalMailboxItemCount;
			this.TotalArchiveSize = requestJob.TotalArchiveSize;
			this.TotalArchiveItemCount = requestJob.TotalArchiveItemCount;
			this.TargetDeliveryDomain = requestJob.TargetDeliveryDomain;
			this.ArchiveDomain = requestJob.ArchiveDomain;
			this.FailureCode = requestJob.FailureCode;
			this.FailureType = requestJob.FailureType;
			this.MessageData = CommonUtils.ByteSerialize(requestJob.Message);
			this.report = report;
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06000C64 RID: 3172 RVA: 0x0001D919 File Offset: 0x0001BB19
		// (set) Token: 0x06000C65 RID: 3173 RVA: 0x0001D921 File Offset: 0x0001BB21
		[XmlElement(ElementName = "Status")]
		public int Status { get; set; }

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06000C66 RID: 3174 RVA: 0x0001D92A File Offset: 0x0001BB2A
		// (set) Token: 0x06000C67 RID: 3175 RVA: 0x0001D932 File Offset: 0x0001BB32
		[XmlElement(ElementName = "Flags")]
		public int Flags { get; set; }

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06000C68 RID: 3176 RVA: 0x0001D93B File Offset: 0x0001BB3B
		// (set) Token: 0x06000C69 RID: 3177 RVA: 0x0001D943 File Offset: 0x0001BB43
		[XmlElement(ElementName = "SourceDatabase")]
		public ADObjectIdXML SourceDatabase { get; set; }

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06000C6A RID: 3178 RVA: 0x0001D94C File Offset: 0x0001BB4C
		// (set) Token: 0x06000C6B RID: 3179 RVA: 0x0001D954 File Offset: 0x0001BB54
		[XmlElement(ElementName = "SourceVersion")]
		public int SourceVersion { get; set; }

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06000C6C RID: 3180 RVA: 0x0001D95D File Offset: 0x0001BB5D
		// (set) Token: 0x06000C6D RID: 3181 RVA: 0x0001D965 File Offset: 0x0001BB65
		[XmlElement(ElementName = "SourceServer")]
		public string SourceServer { get; set; }

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06000C6E RID: 3182 RVA: 0x0001D96E File Offset: 0x0001BB6E
		// (set) Token: 0x06000C6F RID: 3183 RVA: 0x0001D976 File Offset: 0x0001BB76
		[XmlElement(ElementName = "SourceArchiveDatabase")]
		public ADObjectIdXML SourceArchiveDatabase { get; set; }

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06000C70 RID: 3184 RVA: 0x0001D97F File Offset: 0x0001BB7F
		// (set) Token: 0x06000C71 RID: 3185 RVA: 0x0001D987 File Offset: 0x0001BB87
		[XmlElement(ElementName = "SourceArchiveVersion")]
		public int SourceArchiveVersion { get; set; }

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06000C72 RID: 3186 RVA: 0x0001D990 File Offset: 0x0001BB90
		// (set) Token: 0x06000C73 RID: 3187 RVA: 0x0001D998 File Offset: 0x0001BB98
		[XmlElement(ElementName = "SourceArchiveServer")]
		public string SourceArchiveServer { get; set; }

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06000C74 RID: 3188 RVA: 0x0001D9A1 File Offset: 0x0001BBA1
		// (set) Token: 0x06000C75 RID: 3189 RVA: 0x0001D9A9 File Offset: 0x0001BBA9
		[XmlElement(ElementName = "DestinationDatabase")]
		public ADObjectIdXML DestinationDatabase { get; set; }

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06000C76 RID: 3190 RVA: 0x0001D9B2 File Offset: 0x0001BBB2
		// (set) Token: 0x06000C77 RID: 3191 RVA: 0x0001D9BA File Offset: 0x0001BBBA
		[XmlElement(ElementName = "DestinationVersion")]
		public int DestinationVersion { get; set; }

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06000C78 RID: 3192 RVA: 0x0001D9C3 File Offset: 0x0001BBC3
		// (set) Token: 0x06000C79 RID: 3193 RVA: 0x0001D9CB File Offset: 0x0001BBCB
		[XmlElement(ElementName = "DestinationServer")]
		public string DestinationServer { get; set; }

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06000C7A RID: 3194 RVA: 0x0001D9D4 File Offset: 0x0001BBD4
		// (set) Token: 0x06000C7B RID: 3195 RVA: 0x0001D9DC File Offset: 0x0001BBDC
		[XmlElement(ElementName = "DestinationArchiveDatabase")]
		public ADObjectIdXML DestinationArchiveDatabase { get; set; }

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06000C7C RID: 3196 RVA: 0x0001D9E5 File Offset: 0x0001BBE5
		// (set) Token: 0x06000C7D RID: 3197 RVA: 0x0001D9ED File Offset: 0x0001BBED
		[XmlElement(ElementName = "DestinationArchiveVersion")]
		public int DestinationArchiveVersion { get; set; }

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06000C7E RID: 3198 RVA: 0x0001D9F6 File Offset: 0x0001BBF6
		// (set) Token: 0x06000C7F RID: 3199 RVA: 0x0001D9FE File Offset: 0x0001BBFE
		[XmlElement(ElementName = "DestinationArchiveServer")]
		public string DestinationArchiveServer { get; set; }

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06000C80 RID: 3200 RVA: 0x0001DA07 File Offset: 0x0001BC07
		// (set) Token: 0x06000C81 RID: 3201 RVA: 0x0001DA0F File Offset: 0x0001BC0F
		[XmlElement(ElementName = "RemoteHostName")]
		public string RemoteHostName { get; set; }

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06000C82 RID: 3202 RVA: 0x0001DA18 File Offset: 0x0001BC18
		// (set) Token: 0x06000C83 RID: 3203 RVA: 0x0001DA20 File Offset: 0x0001BC20
		[XmlElement(ElementName = "RemoteCredentialUserName")]
		public string RemoteCredentialUserName { get; set; }

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06000C84 RID: 3204 RVA: 0x0001DA29 File Offset: 0x0001BC29
		// (set) Token: 0x06000C85 RID: 3205 RVA: 0x0001DA31 File Offset: 0x0001BC31
		[XmlElement(ElementName = "RemoteDatabaseName")]
		public string RemoteDatabaseName { get; set; }

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06000C86 RID: 3206 RVA: 0x0001DA3A File Offset: 0x0001BC3A
		// (set) Token: 0x06000C87 RID: 3207 RVA: 0x0001DA42 File Offset: 0x0001BC42
		[XmlElement(ElementName = "RemoteArchiveDatabaseName")]
		public string RemoteArchiveDatabaseName { get; set; }

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06000C88 RID: 3208 RVA: 0x0001DA4C File Offset: 0x0001BC4C
		// (set) Token: 0x06000C89 RID: 3209 RVA: 0x0001DA79 File Offset: 0x0001BC79
		[XmlElement(ElementName = "BadItemLimit")]
		public int BadItemLimitInt
		{
			get
			{
				if (!this.BadItemLimit.IsUnlimited)
				{
					return this.BadItemLimit.Value;
				}
				return -1;
			}
			set
			{
				this.BadItemLimit = ((value < 0) ? Unlimited<int>.UnlimitedValue : new Unlimited<int>(value));
			}
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06000C8A RID: 3210 RVA: 0x0001DA92 File Offset: 0x0001BC92
		// (set) Token: 0x06000C8B RID: 3211 RVA: 0x0001DA9A File Offset: 0x0001BC9A
		[XmlIgnore]
		public Unlimited<int> BadItemLimit { get; set; }

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06000C8C RID: 3212 RVA: 0x0001DAA3 File Offset: 0x0001BCA3
		// (set) Token: 0x06000C8D RID: 3213 RVA: 0x0001DAAB File Offset: 0x0001BCAB
		[XmlElement(ElementName = "BadItemsEncountered")]
		public int BadItemsEncountered { get; set; }

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06000C8E RID: 3214 RVA: 0x0001DAB4 File Offset: 0x0001BCB4
		// (set) Token: 0x06000C8F RID: 3215 RVA: 0x0001DAE1 File Offset: 0x0001BCE1
		[XmlElement(ElementName = "LargeItemLimit")]
		public int LargeItemLimitInt
		{
			get
			{
				if (!this.LargeItemLimit.IsUnlimited)
				{
					return this.LargeItemLimit.Value;
				}
				return -1;
			}
			set
			{
				this.LargeItemLimit = ((value < 0) ? Unlimited<int>.UnlimitedValue : new Unlimited<int>(value));
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06000C90 RID: 3216 RVA: 0x0001DAFA File Offset: 0x0001BCFA
		// (set) Token: 0x06000C91 RID: 3217 RVA: 0x0001DB02 File Offset: 0x0001BD02
		[XmlIgnore]
		public Unlimited<int> LargeItemLimit { get; set; }

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06000C92 RID: 3218 RVA: 0x0001DB0B File Offset: 0x0001BD0B
		// (set) Token: 0x06000C93 RID: 3219 RVA: 0x0001DB13 File Offset: 0x0001BD13
		[XmlElement(ElementName = "LargeItemsEncountered")]
		public int LargeItemsEncountered { get; set; }

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06000C94 RID: 3220 RVA: 0x0001DB1C File Offset: 0x0001BD1C
		// (set) Token: 0x06000C95 RID: 3221 RVA: 0x0001DB24 File Offset: 0x0001BD24
		[XmlElement(ElementName = "MissingItemsEncountered")]
		public int MissingItemsEncountered { get; set; }

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06000C96 RID: 3222 RVA: 0x0001DB2D File Offset: 0x0001BD2D
		// (set) Token: 0x06000C97 RID: 3223 RVA: 0x0001DB35 File Offset: 0x0001BD35
		[XmlElement(ElementName = "TimeTracker")]
		public RequestJobTimeTracker TimeTracker { get; set; }

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06000C98 RID: 3224 RVA: 0x0001DB3E File Offset: 0x0001BD3E
		// (set) Token: 0x06000C99 RID: 3225 RVA: 0x0001DB46 File Offset: 0x0001BD46
		[XmlElement(ElementName = "MoveServerName")]
		public string MRSServerName { get; set; }

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06000C9A RID: 3226 RVA: 0x0001DB4F File Offset: 0x0001BD4F
		// (set) Token: 0x06000C9B RID: 3227 RVA: 0x0001DB57 File Offset: 0x0001BD57
		[XmlElement(ElementName = "TotalMailboxSize")]
		public ulong TotalMailboxSize { get; set; }

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06000C9C RID: 3228 RVA: 0x0001DB60 File Offset: 0x0001BD60
		// (set) Token: 0x06000C9D RID: 3229 RVA: 0x0001DB68 File Offset: 0x0001BD68
		[XmlElement(ElementName = "TotalMailboxItemCount")]
		public ulong TotalMailboxItemCount { get; set; }

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06000C9E RID: 3230 RVA: 0x0001DB71 File Offset: 0x0001BD71
		// (set) Token: 0x06000C9F RID: 3231 RVA: 0x0001DB79 File Offset: 0x0001BD79
		[XmlElement(ElementName = "TotalArchiveSize")]
		public ulong? TotalArchiveSize { get; set; }

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06000CA0 RID: 3232 RVA: 0x0001DB82 File Offset: 0x0001BD82
		// (set) Token: 0x06000CA1 RID: 3233 RVA: 0x0001DB8A File Offset: 0x0001BD8A
		[XmlElement(ElementName = "TotalArchiveItemCount")]
		public ulong? TotalArchiveItemCount { get; set; }

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06000CA2 RID: 3234 RVA: 0x0001DB93 File Offset: 0x0001BD93
		// (set) Token: 0x06000CA3 RID: 3235 RVA: 0x0001DB9B File Offset: 0x0001BD9B
		[XmlElement(ElementName = "TargetDeliveryDomain")]
		public string TargetDeliveryDomain { get; set; }

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06000CA4 RID: 3236 RVA: 0x0001DBA4 File Offset: 0x0001BDA4
		// (set) Token: 0x06000CA5 RID: 3237 RVA: 0x0001DBAC File Offset: 0x0001BDAC
		[XmlElement(ElementName = "ArchiveDomain")]
		public string ArchiveDomain { get; set; }

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06000CA6 RID: 3238 RVA: 0x0001DBB5 File Offset: 0x0001BDB5
		// (set) Token: 0x06000CA7 RID: 3239 RVA: 0x0001DBBD File Offset: 0x0001BDBD
		[XmlElement(ElementName = "FailureCode")]
		public int? FailureCode { get; set; }

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06000CA8 RID: 3240 RVA: 0x0001DBC6 File Offset: 0x0001BDC6
		// (set) Token: 0x06000CA9 RID: 3241 RVA: 0x0001DBCE File Offset: 0x0001BDCE
		[XmlElement(ElementName = "FailureType")]
		public string FailureType { get; set; }

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06000CAA RID: 3242 RVA: 0x0001DBD7 File Offset: 0x0001BDD7
		// (set) Token: 0x06000CAB RID: 3243 RVA: 0x0001DBDF File Offset: 0x0001BDDF
		[XmlElement(ElementName = "FailureMessageData")]
		public byte[] MessageData { get; set; }

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06000CAC RID: 3244 RVA: 0x0001DBE8 File Offset: 0x0001BDE8
		// (set) Token: 0x06000CAD RID: 3245 RVA: 0x0001DC0F File Offset: 0x0001BE0F
		[XmlElement(ElementName = "MoveReport")]
		public ReportEntry[] ReportData
		{
			get
			{
				return new ReportEntry[]
				{
					new ReportEntry(new LocalizedString("Your version of Exchange is unable to display Compressed Reports."))
				};
			}
			set
			{
				if (!this.hasReadCompressedEntries)
				{
					this.report = new ReportData(MoveHistoryEntryInternal.FakeReportGuid, ReportVersion.ReportE14R4);
					if (value != null)
					{
						this.report.Entries = new List<ReportEntry>(value);
					}
				}
			}
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06000CAE RID: 3246 RVA: 0x0001DC3E File Offset: 0x0001BE3E
		// (set) Token: 0x06000CAF RID: 3247 RVA: 0x0001DC5B File Offset: 0x0001BE5B
		[XmlElement(ElementName = "CompressedReport")]
		public CompressedReport CompressedReportData
		{
			get
			{
				return new CompressedReport((this.report != null) ? this.report.Entries : null);
			}
			set
			{
				this.report = new ReportData(MoveHistoryEntryInternal.FakeReportGuid, ReportVersion.ReportE14R6Compression);
				if (value != null)
				{
					this.report.Entries = value.Entries;
					this.hasReadCompressedEntries = true;
				}
			}
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06000CB0 RID: 3248 RVA: 0x0001DC89 File Offset: 0x0001BE89
		internal Report Report
		{
			get
			{
				return this.report.ToReport();
			}
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06000CB1 RID: 3249 RVA: 0x0001DC98 File Offset: 0x0001BE98
		private DateTime ComparisonTime
		{
			get
			{
				if (this.TimeTracker == null)
				{
					return DateTime.MinValue;
				}
				DateTime? timestamp = this.TimeTracker.GetTimestamp(RequestJobTimestamp.Completion);
				DateTime? timestamp2 = this.TimeTracker.GetTimestamp(RequestJobTimestamp.Failure);
				DateTime? timestamp3 = this.TimeTracker.GetTimestamp(RequestJobTimestamp.Creation);
				if (timestamp != null)
				{
					return timestamp.Value;
				}
				if (timestamp2 != null)
				{
					return timestamp2.Value;
				}
				DateTime? dateTime = timestamp3;
				if (dateTime == null)
				{
					return DateTime.MinValue;
				}
				return dateTime.GetValueOrDefault();
			}
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x0001DD18 File Offset: 0x0001BF18
		int IComparable<MoveHistoryEntryInternal>.CompareTo(MoveHistoryEntryInternal other)
		{
			return -this.ComparisonTime.CompareTo(other.ComparisonTime);
		}

		// Token: 0x06000CB3 RID: 3251 RVA: 0x0001DD3C File Offset: 0x0001BF3C
		internal static List<MoveHistoryEntryInternal> LoadMoveHistory(MapiStore mailbox)
		{
			MrsTracer.Common.Function("MoveHistoryEntryInternal.LoadMoveHistory", new object[0]);
			List<MoveHistoryEntryInternal> list = new List<MoveHistoryEntryInternal>();
			using (MapiFolder mapiFolder = MapiUtils.OpenFolderUnderRoot(mailbox, MoveHistoryEntryInternal.MHEFolderName, false))
			{
				if (mapiFolder == null)
				{
					return list;
				}
				using (MapiTable contentsTable = mapiFolder.GetContentsTable(ContentsTableFlags.DeferredErrors))
				{
					PropValue[][] array = MapiUtils.QueryAllRows(contentsTable, null, new PropTag[]
					{
						PropTag.EntryId
					});
					foreach (PropValue[] array3 in array)
					{
						byte[] bytes = array3[0].GetBytes();
						string subject = string.Format("MoveHistoryEntry {0}", TraceUtils.DumpEntryId(bytes));
						MoveObjectInfo<MoveHistoryEntryInternal> moveObjectInfo = new MoveObjectInfo<MoveHistoryEntryInternal>(Guid.Empty, mailbox, bytes, MoveHistoryEntryInternal.MHEFolderName, MoveHistoryEntryInternal.MHEMessageClass, subject, null);
						using (moveObjectInfo)
						{
							MoveHistoryEntryInternal moveHistoryEntryInternal = null;
							try
							{
								moveHistoryEntryInternal = moveObjectInfo.ReadObject(ReadObjectFlags.DontThrowOnCorruptData);
							}
							catch (MailboxReplicationPermanentException ex)
							{
								MrsTracer.Common.Warning("Failed to read move history entry: {0}", new object[]
								{
									ex.ToString()
								});
							}
							if (moveHistoryEntryInternal != null)
							{
								list.Add(moveHistoryEntryInternal);
							}
							else if (moveObjectInfo.CreationTimestamp < DateTime.UtcNow - TimeSpan.FromDays(365.0))
							{
								MrsTracer.Common.Warning("Removing old corrupt MHEI entry {0}", new object[]
								{
									TraceUtils.DumpEntryId(bytes)
								});
								moveObjectInfo.DeleteMessage();
							}
						}
					}
				}
			}
			list.Sort();
			return list;
		}

		// Token: 0x06000CB4 RID: 3252 RVA: 0x0001DF30 File Offset: 0x0001C130
		internal static List<MoveHistoryEntryInternal> LoadMoveHistory(Guid mailboxGuid, Guid mdbGuid, UserMailboxFlags flags)
		{
			List<MoveHistoryEntryInternal> result;
			using (MapiStore userMailbox = MapiUtils.GetUserMailbox(mailboxGuid, mdbGuid, flags))
			{
				if (userMailbox == null)
				{
					result = null;
				}
				else
				{
					result = MoveHistoryEntryInternal.LoadMoveHistory(userMailbox);
				}
			}
			return result;
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x0001DF8C File Offset: 0x0001C18C
		internal void SaveToMailbox(MapiStore mailbox, int maxMoveHistoryLength)
		{
			MrsTracer.Common.Function("MoveHistoryEntryInternal.SaveToMailbox(maxHistoryLength={0})", new object[]
			{
				maxMoveHistoryLength
			});
			List<byte[]> list = new List<byte[]>();
			using (MapiFolder folder = MapiUtils.OpenFolderUnderRoot(mailbox, MoveHistoryEntryInternal.MHEFolderName, true))
			{
				using (MapiTable contentsTable = folder.GetContentsTable(ContentsTableFlags.DeferredErrors))
				{
					contentsTable.SortTable(new SortOrder(PropTag.LastModificationTime, SortFlags.Ascend), SortTableFlags.None);
					PropValue[][] array = MapiUtils.QueryAllRows(contentsTable, null, new PropTag[]
					{
						PropTag.EntryId
					});
					foreach (PropValue[] array3 in array)
					{
						list.Add(array3[0].GetBytes());
					}
				}
				MrsTracer.Common.Debug("Move history contains {0} items.", new object[]
				{
					list.Count
				});
				List<byte[]> list2 = new List<byte[]>();
				while (list.Count >= maxMoveHistoryLength && list.Count > 0)
				{
					list2.Add(list[0]);
					list.RemoveAt(0);
				}
				if (list2.Count > 0)
				{
					MrsTracer.Common.Debug("Clearing {0} entries from move history", new object[]
					{
						list2.Count
					});
					MapiUtils.ProcessMapiCallInBatches<byte[]>(list2.ToArray(), delegate(byte[][] batch)
					{
						folder.DeleteMessages(DeleteMessagesFlags.ForceHardDelete, batch);
					});
				}
			}
			if (maxMoveHistoryLength <= 0)
			{
				MrsTracer.Common.Debug("Move history saving is disabled.", new object[0]);
				return;
			}
			DateTime dateTime = this.TimeTracker.GetTimestamp(RequestJobTimestamp.Creation) ?? DateTime.MinValue;
			string subject = string.Format("MoveHistoryEntry {0}", dateTime.ToString());
			byte[] bytes = BitConverter.GetBytes(dateTime.ToBinary());
			MoveObjectInfo<MoveHistoryEntryInternal> moveObjectInfo = new MoveObjectInfo<MoveHistoryEntryInternal>(Guid.Empty, mailbox, null, MoveHistoryEntryInternal.MHEFolderName, MoveHistoryEntryInternal.MHEMessageClass, subject, bytes);
			using (moveObjectInfo)
			{
				moveObjectInfo.SaveObject(this);
			}
		}

		// Token: 0x04000714 RID: 1812
		public static readonly string MHEFolderName = "MailboxMoveHistory";

		// Token: 0x04000715 RID: 1813
		private static readonly string MHEMessageClass = "IPM.MS-Exchange.MailboxMoveHistory";

		// Token: 0x04000716 RID: 1814
		private static readonly Guid FakeReportGuid = new Guid("d1df9b83-96bf-428b-8557-85c71cb1640f");

		// Token: 0x04000717 RID: 1815
		private bool hasReadCompressedEntries;

		// Token: 0x04000718 RID: 1816
		private ReportData report;
	}
}
