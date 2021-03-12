using System;
using System.Collections.Generic;
using System.IO;
using System.Management.Automation;
using System.Text;
using System.Web;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.UM.TroubleshootingTool.Shared;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCommon.CrossServerMailboxAccess;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x02000D15 RID: 3349
	[Cmdlet("Export", "UMCallDataRecord", SupportsShouldProcess = true)]
	public sealed class ExportUMCallDataRecord : UMReportsTaskBase<MailboxIdParameter>
	{
		// Token: 0x170027D4 RID: 10196
		// (get) Token: 0x06008082 RID: 32898 RVA: 0x0020D888 File Offset: 0x0020BA88
		// (set) Token: 0x06008083 RID: 32899 RVA: 0x0020D890 File Offset: 0x0020BA90
		private new MailboxIdParameter Identity
		{
			get
			{
				return base.Identity;
			}
			set
			{
				base.Identity = value;
			}
		}

		// Token: 0x170027D5 RID: 10197
		// (get) Token: 0x06008084 RID: 32900 RVA: 0x0020D899 File Offset: 0x0020BA99
		// (set) Token: 0x06008085 RID: 32901 RVA: 0x0020D8B0 File Offset: 0x0020BAB0
		[Parameter(Mandatory = false)]
		[ValidateNotNullOrEmpty]
		public UMDialPlanIdParameter UMDialPlan
		{
			get
			{
				return (UMDialPlanIdParameter)base.Fields["UMDialPlan"];
			}
			set
			{
				base.Fields["UMDialPlan"] = value;
			}
		}

		// Token: 0x170027D6 RID: 10198
		// (get) Token: 0x06008086 RID: 32902 RVA: 0x0020D8C3 File Offset: 0x0020BAC3
		// (set) Token: 0x06008087 RID: 32903 RVA: 0x0020D8DA File Offset: 0x0020BADA
		[Parameter(Mandatory = false)]
		[ValidateNotNullOrEmpty]
		public UMIPGatewayIdParameter UMIPGateway
		{
			get
			{
				return (UMIPGatewayIdParameter)base.Fields["UMIPGateway"];
			}
			set
			{
				base.Fields["UMIPGateway"] = value;
			}
		}

		// Token: 0x170027D7 RID: 10199
		// (get) Token: 0x06008088 RID: 32904 RVA: 0x0020D8ED File Offset: 0x0020BAED
		// (set) Token: 0x06008089 RID: 32905 RVA: 0x0020D904 File Offset: 0x0020BB04
		[Parameter(Mandatory = true)]
		[ValidateNotNullOrEmpty]
		public ExDateTime Date
		{
			get
			{
				return (ExDateTime)base.Fields["Date"];
			}
			set
			{
				base.Fields["Date"] = value;
			}
		}

		// Token: 0x170027D8 RID: 10200
		// (get) Token: 0x0600808A RID: 32906 RVA: 0x0020D91C File Offset: 0x0020BB1C
		// (set) Token: 0x0600808B RID: 32907 RVA: 0x0020D924 File Offset: 0x0020BB24
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = true)]
		public Stream ClientStream { get; set; }

		// Token: 0x170027D9 RID: 10201
		// (get) Token: 0x0600808C RID: 32908 RVA: 0x0020D930 File Offset: 0x0020BB30
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageExportUMCallDataRecord(this.Date.ToString());
			}
		}

		// Token: 0x0600808D RID: 32909 RVA: 0x0020D956 File Offset: 0x0020BB56
		protected override void InternalBeginProcessing()
		{
			base.InternalBeginProcessing();
			base.ValidateCommonParamsAndSetOrg(this.UMDialPlan, this.UMIPGateway, out this.dialPlanGuid, out this.gatewayGuid, out this.dialPlanName, out this.gatewayName);
		}

		// Token: 0x0600808E RID: 32910 RVA: 0x0020D988 File Offset: 0x0020BB88
		protected override void ProcessMailbox()
		{
			try
			{
				ExDateTime exDateTime = this.Date.ToUtc();
				ExDateTime startDateTime = new ExDateTime(ExTimeZone.UtcTimeZone, exDateTime.Year, exDateTime.Month, exDateTime.Day);
				ExDateTime endDateTime = startDateTime.AddDays(1.0);
				StreamWriter streamWriter = new StreamWriter(this.ClientStream, Encoding.UTF8);
				streamWriter.WriteCsvLine(this.csvRow.Keys);
				using (IUMCallDataRecordStorage umcallDataRecordsAcessor = InterServerMailboxAccessor.GetUMCallDataRecordsAcessor(this.DataObject))
				{
					int num = 0;
					int numberOfRecordsToRead = 5000;
					if (Utils.RunningInTestMode)
					{
						numberOfRecordsToRead = 1;
					}
					bool flag;
					do
					{
						flag = false;
						CDRData[] umcallDataRecords = umcallDataRecordsAcessor.GetUMCallDataRecords(startDateTime.Subtract(this.TimeDelta), endDateTime.Add(this.TimeDelta), num, numberOfRecordsToRead);
						if (umcallDataRecords != null && umcallDataRecords.Length > 0)
						{
							num += umcallDataRecords.Length;
							this.WriteToStream(umcallDataRecords, streamWriter, startDateTime, endDateTime);
							flag = true;
						}
						streamWriter.Flush();
					}
					while (flag);
				}
			}
			catch (ArgumentException exception)
			{
				base.WriteError(exception, ErrorCategory.InvalidArgument, null);
			}
			catch (ObjectDisposedException exception2)
			{
				base.WriteError(exception2, ErrorCategory.InvalidArgument, null);
			}
			catch (IOException exception3)
			{
				base.WriteError(exception3, ErrorCategory.WriteError, null);
			}
			catch (UnableToFindUMReportDataException exception4)
			{
				base.WriteError(exception4, ErrorCategory.ReadError, null);
			}
			catch (StorageTransientException exception5)
			{
				base.WriteError(exception5, ErrorCategory.ReadError, null);
			}
			catch (StoragePermanentException exception6)
			{
				base.WriteError(exception6, ErrorCategory.ReadError, null);
			}
			catch (HttpException exception7)
			{
				base.WriteError(exception7, ErrorCategory.WriteError, null);
			}
			catch (CDROperationException exception8)
			{
				base.WriteError(exception8, ErrorCategory.ReadError, null);
			}
			catch (EWSUMMailboxAccessException exception9)
			{
				base.WriteError(exception9, ErrorCategory.ReadError, null);
			}
		}

		// Token: 0x0600808F RID: 32911 RVA: 0x0020DB88 File Offset: 0x0020BD88
		private void WriteToStream(CDRData[] cdrDataArray, StreamWriter writer, ExDateTime startDateTime, ExDateTime endDateTime)
		{
			foreach (CDRData cdrdata in cdrDataArray)
			{
				if (!(cdrdata.CallStartTime < startDateTime.UniversalTime) && !(cdrdata.CallStartTime >= endDateTime.UniversalTime) && (!(this.dialPlanGuid != Guid.Empty) || !(cdrdata.DialPlanGuid != this.dialPlanGuid)) && (!(this.gatewayGuid != Guid.Empty) || !(cdrdata.GatewayGuid != this.gatewayGuid)))
				{
					this.FillOrCleanCSVRow(cdrdata, false);
					writer.WriteCsvLine(this.csvRow.Values);
					this.FillOrCleanCSVRow(cdrdata, true);
				}
			}
		}

		// Token: 0x06008090 RID: 32912 RVA: 0x0020DC44 File Offset: 0x0020BE44
		private void FillOrCleanCSVRow(CDRData cdrData, bool clean)
		{
			this.csvRow["CallStartTime"] = (clean ? string.Empty : cdrData.CallStartTime.ToString("u"));
			this.csvRow["CallType"] = (clean ? string.Empty : Utils.CheckString(cdrData.CallType));
			this.csvRow["CallIdentity"] = (clean ? string.Empty : Utils.CheckString(cdrData.CallIdentity));
			this.csvRow["ParentCallIdentity"] = (clean ? string.Empty : Utils.CheckString(cdrData.ParentCallIdentity));
			if (!CommonConstants.UseDataCenterLogging)
			{
				this.csvRow["UMServerName"] = (clean ? string.Empty : Utils.CheckString(cdrData.UMServerName));
			}
			this.csvRow["DialPlanName"] = (clean ? string.Empty : Utils.CheckString(cdrData.DialPlanName));
			this.csvRow["CallDuration"] = (clean ? string.Empty : TimeSpan.FromSeconds((double)cdrData.CallDuration).ToString());
			this.csvRow["IPGatewayAddress"] = (clean ? string.Empty : Utils.CheckString(cdrData.IPGatewayAddress));
			this.csvRow["IPGatewayName"] = (clean ? string.Empty : Utils.CheckString(cdrData.IPGatewayName));
			this.csvRow["CalledPhoneNumber"] = (clean ? string.Empty : Utils.CheckString(cdrData.CalledPhoneNumber));
			this.csvRow["CallerPhoneNumber"] = (clean ? string.Empty : Utils.CheckString(cdrData.CallerPhoneNumber));
			this.csvRow["OfferResult"] = (clean ? string.Empty : Utils.CheckString(cdrData.OfferResult));
			this.csvRow["DropCallReason"] = (clean ? string.Empty : Utils.CheckString(cdrData.DropCallReason));
			this.csvRow["ReasonForCall"] = (clean ? string.Empty : Utils.CheckString(cdrData.ReasonForCall));
			this.csvRow["TransferredNumber"] = (clean ? string.Empty : Utils.CheckString(cdrData.TransferredNumber));
			this.csvRow["DialedString"] = (clean ? string.Empty : Utils.CheckString(cdrData.DialedString));
			this.csvRow["CallerMailboxAlias"] = (clean ? string.Empty : Utils.CheckString(cdrData.CallerMailboxAlias));
			this.csvRow["CalleeMailboxAlias"] = (clean ? string.Empty : Utils.CheckString(cdrData.CalleeMailboxAlias));
			this.csvRow["AutoAttendantName"] = (clean ? string.Empty : Utils.CheckString(cdrData.AutoAttendantName));
			this.csvRow["NMOS"] = (clean ? string.Empty : this.CheckAudioMetricString(cdrData.AudioQualityMetrics.NMOS));
			this.csvRow["NMOSDegradation"] = (clean ? string.Empty : this.CheckAudioMetricString(cdrData.AudioQualityMetrics.NMOSDegradation));
			this.csvRow["NMOSDegradationPacketLoss"] = (clean ? string.Empty : this.CheckAudioMetricString(cdrData.AudioQualityMetrics.NMOSDegradationPacketLoss));
			this.csvRow["NMOSDegradationJitter"] = (clean ? string.Empty : this.CheckAudioMetricString(cdrData.AudioQualityMetrics.NMOSDegradationJitter));
			this.csvRow["Jitter"] = (clean ? string.Empty : this.CheckAudioMetricString(cdrData.AudioQualityMetrics.Jitter));
			this.csvRow["PacketLoss"] = (clean ? string.Empty : this.CheckAudioMetricString(cdrData.AudioQualityMetrics.PacketLoss));
			this.csvRow["RoundTrip"] = (clean ? string.Empty : this.CheckAudioMetricString(cdrData.AudioQualityMetrics.RoundTrip));
			this.csvRow["BurstDensity"] = (clean ? string.Empty : this.CheckAudioMetricString(cdrData.AudioQualityMetrics.BurstDensity));
			this.csvRow["BurstDuration"] = (clean ? string.Empty : this.CheckAudioMetricString(cdrData.AudioQualityMetrics.BurstDuration));
			this.csvRow["AudioCodec"] = (clean ? string.Empty : Utils.CheckString(cdrData.AudioQualityMetrics.AudioCodec));
		}

		// Token: 0x06008091 RID: 32913 RVA: 0x0020E0DD File Offset: 0x0020C2DD
		private string CheckAudioMetricString(float metric)
		{
			if (metric == AudioQuality.UnknownValue)
			{
				return string.Empty;
			}
			return metric.ToString("F1");
		}

		// Token: 0x04003EDB RID: 16091
		private const string FixedFormatString = "F1";

		// Token: 0x04003EDC RID: 16092
		private const string UTCFormatString = "u";

		// Token: 0x04003EDD RID: 16093
		private const int ChunkSize = 5000;

		// Token: 0x04003EDE RID: 16094
		private readonly TimeSpan TimeDelta = TimeSpan.FromHours(1.0);

		// Token: 0x04003EDF RID: 16095
		private Dictionary<string, string> csvRow = new Dictionary<string, string>
		{
			{
				"CallStartTime",
				string.Empty
			},
			{
				"CallType",
				string.Empty
			},
			{
				"CallIdentity",
				string.Empty
			},
			{
				"ParentCallIdentity",
				string.Empty
			},
			{
				"UMServerName",
				string.Empty
			},
			{
				"DialPlanName",
				string.Empty
			},
			{
				"CallDuration",
				string.Empty
			},
			{
				"IPGatewayAddress",
				string.Empty
			},
			{
				"IPGatewayName",
				string.Empty
			},
			{
				"CalledPhoneNumber",
				string.Empty
			},
			{
				"CallerPhoneNumber",
				string.Empty
			},
			{
				"OfferResult",
				string.Empty
			},
			{
				"DropCallReason",
				string.Empty
			},
			{
				"ReasonForCall",
				string.Empty
			},
			{
				"TransferredNumber",
				string.Empty
			},
			{
				"DialedString",
				string.Empty
			},
			{
				"CallerMailboxAlias",
				string.Empty
			},
			{
				"CalleeMailboxAlias",
				string.Empty
			},
			{
				"AutoAttendantName",
				string.Empty
			},
			{
				"NMOS",
				string.Empty
			},
			{
				"NMOSDegradation",
				string.Empty
			},
			{
				"NMOSDegradationJitter",
				string.Empty
			},
			{
				"NMOSDegradationPacketLoss",
				string.Empty
			},
			{
				"Jitter",
				string.Empty
			},
			{
				"PacketLoss",
				string.Empty
			},
			{
				"RoundTrip",
				string.Empty
			},
			{
				"BurstDensity",
				string.Empty
			},
			{
				"BurstDuration",
				string.Empty
			},
			{
				"AudioCodec",
				string.Empty
			}
		};

		// Token: 0x04003EE0 RID: 16096
		private Guid dialPlanGuid;

		// Token: 0x04003EE1 RID: 16097
		private Guid gatewayGuid;

		// Token: 0x04003EE2 RID: 16098
		private string dialPlanName;

		// Token: 0x04003EE3 RID: 16099
		private string gatewayName;

		// Token: 0x02000D16 RID: 3350
		internal abstract class CSVHeaders
		{
			// Token: 0x04003EE5 RID: 16101
			public const string CallStartTime = "CallStartTime";

			// Token: 0x04003EE6 RID: 16102
			public const string CallType = "CallType";

			// Token: 0x04003EE7 RID: 16103
			public const string CallIdentity = "CallIdentity";

			// Token: 0x04003EE8 RID: 16104
			public const string ParentCallIdentity = "ParentCallIdentity";

			// Token: 0x04003EE9 RID: 16105
			public const string UMServerName = "UMServerName";

			// Token: 0x04003EEA RID: 16106
			public const string DialPlanName = "DialPlanName";

			// Token: 0x04003EEB RID: 16107
			public const string CallDuration = "CallDuration";

			// Token: 0x04003EEC RID: 16108
			public const string IPGatewayAddress = "IPGatewayAddress";

			// Token: 0x04003EED RID: 16109
			public const string IPGatewayName = "IPGatewayName";

			// Token: 0x04003EEE RID: 16110
			public const string CalledPhoneNumber = "CalledPhoneNumber";

			// Token: 0x04003EEF RID: 16111
			public const string CallerPhoneNumber = "CallerPhoneNumber";

			// Token: 0x04003EF0 RID: 16112
			public const string OfferResult = "OfferResult";

			// Token: 0x04003EF1 RID: 16113
			public const string DropCallReason = "DropCallReason";

			// Token: 0x04003EF2 RID: 16114
			public const string ReasonForCall = "ReasonForCall";

			// Token: 0x04003EF3 RID: 16115
			public const string TransferredNumber = "TransferredNumber";

			// Token: 0x04003EF4 RID: 16116
			public const string DialedString = "DialedString";

			// Token: 0x04003EF5 RID: 16117
			public const string CallerMailboxAlias = "CallerMailboxAlias";

			// Token: 0x04003EF6 RID: 16118
			public const string CalleeMailboxAlias = "CalleeMailboxAlias";

			// Token: 0x04003EF7 RID: 16119
			public const string AutoAttendantName = "AutoAttendantName";

			// Token: 0x04003EF8 RID: 16120
			public const string NMOS = "NMOS";

			// Token: 0x04003EF9 RID: 16121
			public const string NMOSDegradation = "NMOSDegradation";

			// Token: 0x04003EFA RID: 16122
			public const string NMOSDegradationPacketLoss = "NMOSDegradationPacketLoss";

			// Token: 0x04003EFB RID: 16123
			public const string NMOSDegradationJitter = "NMOSDegradationJitter";

			// Token: 0x04003EFC RID: 16124
			public const string Jitter = "Jitter";

			// Token: 0x04003EFD RID: 16125
			public const string PacketLoss = "PacketLoss";

			// Token: 0x04003EFE RID: 16126
			public const string RoundTrip = "RoundTrip";

			// Token: 0x04003EFF RID: 16127
			public const string BurstDensity = "BurstDensity";

			// Token: 0x04003F00 RID: 16128
			public const string BurstDuration = "BurstDuration";

			// Token: 0x04003F01 RID: 16129
			public const string AudioCodec = "AudioCodec";
		}
	}
}
