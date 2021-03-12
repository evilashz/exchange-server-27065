using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Management.Automation;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000062 RID: 98
	[Cmdlet("Export", "ActiveSyncLog", SupportsShouldProcess = true)]
	public sealed class ExportMobileSyncLog : Task
	{
		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060002CA RID: 714 RVA: 0x0000B948 File Offset: 0x00009B48
		// (set) Token: 0x060002CB RID: 715 RVA: 0x0000B95F File Offset: 0x00009B5F
		[Parameter(Mandatory = true, ValueFromPipeline = true)]
		public string Filename
		{
			get
			{
				if (this.currentFile != null)
				{
					return this.currentFile.FullName;
				}
				return null;
			}
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					this.currentFile = new FileInfo(value);
					return;
				}
				this.currentFile = null;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060002CC RID: 716 RVA: 0x0000B97D File Offset: 0x00009B7D
		// (set) Token: 0x060002CD RID: 717 RVA: 0x0000B985 File Offset: 0x00009B85
		[Parameter(Mandatory = false)]
		public DateTime StartDate
		{
			get
			{
				return this.startTime;
			}
			set
			{
				this.startTime = value;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060002CE RID: 718 RVA: 0x0000B98E File Offset: 0x00009B8E
		// (set) Token: 0x060002CF RID: 719 RVA: 0x0000B996 File Offset: 0x00009B96
		[Parameter(Mandatory = false)]
		public DateTime EndDate
		{
			get
			{
				return this.endTime;
			}
			set
			{
				this.endTime = value;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x0000B99F File Offset: 0x00009B9F
		// (set) Token: 0x060002D1 RID: 721 RVA: 0x0000B9A7 File Offset: 0x00009BA7
		[Parameter(Mandatory = false)]
		public SwitchParameter UseGMT
		{
			get
			{
				return this.useGMT;
			}
			set
			{
				this.useGMT = value;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x0000B9B0 File Offset: 0x00009BB0
		// (set) Token: 0x060002D3 RID: 723 RVA: 0x0000B9B8 File Offset: 0x00009BB8
		[Parameter(Mandatory = false)]
		public string OutputPrefix
		{
			get
			{
				return this.outputPrefix;
			}
			set
			{
				this.outputPrefix = value;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x0000B9C1 File Offset: 0x00009BC1
		// (set) Token: 0x060002D5 RID: 725 RVA: 0x0000B9D8 File Offset: 0x00009BD8
		[Parameter(Mandatory = false)]
		public string OutputPath
		{
			get
			{
				if (this.outputPath != null)
				{
					return this.outputPath.FullName;
				}
				return null;
			}
			set
			{
				this.outputPath = new DirectoryInfo(value);
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x0000B9E6 File Offset: 0x00009BE6
		// (set) Token: 0x060002D7 RID: 727 RVA: 0x0000B9EE File Offset: 0x00009BEE
		[Parameter(Mandatory = false)]
		public SwitchParameter Force
		{
			get
			{
				return this.force;
			}
			set
			{
				this.force = value;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060002D8 RID: 728 RVA: 0x0000B9F7 File Offset: 0x00009BF7
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationProcessInputLog(this.currentFile.FullName);
			}
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000BA09 File Offset: 0x00009C09
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (this.outputPath != null && !Directory.Exists(this.outputPath.FullName))
			{
				base.WriteError(new OutputDirectoryNotExist(this.outputPath.FullName), ErrorCategory.InvalidArgument, null);
			}
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000BA44 File Offset: 0x00009C44
		protected override void InternalBeginProcessing()
		{
			base.InternalBeginProcessing();
			if (this.startTime > DateTime.MinValue && this.endTime < DateTime.MaxValue && this.startTime > this.endTime)
			{
				base.WriteError(new ArgumentException(Strings.InvalidTimeRange, "StartDate"), ErrorCategory.InvalidArgument, null);
			}
			this.parser = new ExportMobileSyncLog.AirSyncLogParser();
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000BAB8 File Offset: 0x00009CB8
		protected override void InternalProcessRecord()
		{
			base.InternalProcessRecord();
			if (this.useGMT)
			{
				this.parser.StartTime = this.startTime;
				this.parser.EndTime = this.endTime;
			}
			else
			{
				if (this.startTime != DateTime.MinValue)
				{
					this.parser.StartTime = this.startTime.ToUniversalTime();
				}
				if (this.endTime != DateTime.MaxValue)
				{
					this.parser.EndTime = this.endTime.ToUniversalTime();
				}
			}
			base.WriteProgress(Strings.AirSyncReportingProgressActivity, Strings.AirSyncReportingProgressParsing(this.currentFile.FullName), 0);
			base.WriteVerbose(Strings.AirSyncReportingProgressParsing(this.currentFile.FullName));
			this.parser.StartNewFile();
			int num = 0;
			StreamReader streamReader = null;
			try
			{
				FileStream stream = File.Open(this.currentFile.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
				streamReader = new StreamReader(stream);
				long length = streamReader.BaseStream.Length;
				string line;
				while ((line = streamReader.ReadLine()) != null)
				{
					int num2 = (int)((double)streamReader.BaseStream.Position / (double)length * 100.0);
					if (num2 != num)
					{
						base.WriteProgress(Strings.AirSyncReportingProgressActivity, Strings.AirSyncReportingProgressParsing(this.currentFile.FullName), num2);
						num = num2;
					}
					this.parser.ParseLine(line);
				}
			}
			catch (IOException ex)
			{
				base.WriteError(new ExceptionWhileReadingInputFile(this.currentFile.FullName, ex.Message, ex), ErrorCategory.ReadError, this.currentFile);
			}
			catch (UnauthorizedAccessException ex2)
			{
				base.WriteError(new ExceptionWhileReadingInputFile(this.currentFile.FullName, ex2.Message, ex2), ErrorCategory.ReadError, this.currentFile);
			}
			finally
			{
				if (streamReader != null)
				{
					streamReader.Close();
				}
				streamReader = null;
			}
			base.WriteProgress(Strings.AirSyncReportingProgressActivity, Strings.AirSyncReportingProgressParsing(this.currentFile.FullName), 100);
			this.fileCount++;
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000BCC8 File Offset: 0x00009EC8
		protected override void InternalEndProcessing()
		{
			base.InternalEndProcessing();
			if (this.fileCount == 0)
			{
				return;
			}
			string[] array = new string[]
			{
				"Users.csv",
				"Servers.csv",
				"Hourly.csv",
				"StatusCodes.csv",
				"PolicyCompliance.csv",
				"UserAgents.csv"
			};
			string text = "";
			if (this.outputPath != null)
			{
				text = this.outputPath.FullName;
				if (!Directory.Exists(text))
				{
					base.WriteError(new OutputDirectoryNotExist(text), ErrorCategory.InvalidArgument, null);
					return;
				}
			}
			for (int i = 0; i < array.Length; i++)
			{
				FileInfo fileInfo = new FileInfo(Path.Combine(text, this.outputPrefix + array[i]));
				if (!fileInfo.Exists || !(this.Force == false) || base.ShouldContinue(Strings.OutputFileExists(fileInfo.FullName)))
				{
					StreamWriter streamWriter = null;
					try
					{
						streamWriter = new StreamWriter(fileInfo.FullName, false, Encoding.UTF8);
						switch (i)
						{
						case 0:
							this.parser.WriteUserResults(streamWriter);
							break;
						case 1:
							this.parser.WriteServerResults(streamWriter);
							break;
						case 2:
							this.parser.WriteHourlyResults(streamWriter);
							break;
						case 3:
							this.parser.WriteStatusCodeResults(streamWriter);
							break;
						case 4:
							this.parser.WritePolicyComplianceResults(streamWriter);
							break;
						case 5:
							this.parser.WriteUserAgentResults(streamWriter);
							break;
						}
					}
					catch (IOException ex)
					{
						base.WriteError(new ExceptionWhileWritingOutputFile(fileInfo.FullName, ex.Message, ex), ErrorCategory.WriteError, this.currentFile);
					}
					catch (UnauthorizedAccessException ex2)
					{
						base.WriteError(new ExceptionWhileWritingOutputFile(fileInfo.FullName, ex2.Message, ex2), ErrorCategory.WriteError, this.currentFile);
					}
					finally
					{
						if (streamWriter != null)
						{
							streamWriter.Close();
						}
						streamWriter = null;
					}
					base.WriteObject(fileInfo);
				}
			}
		}

		// Token: 0x04000191 RID: 401
		private ExportMobileSyncLog.AirSyncLogParser parser;

		// Token: 0x04000192 RID: 402
		private FileInfo currentFile;

		// Token: 0x04000193 RID: 403
		private DateTime startTime = DateTime.MinValue;

		// Token: 0x04000194 RID: 404
		private DateTime endTime = DateTime.MaxValue;

		// Token: 0x04000195 RID: 405
		private SwitchParameter useGMT;

		// Token: 0x04000196 RID: 406
		private string outputPrefix = string.Empty;

		// Token: 0x04000197 RID: 407
		private DirectoryInfo outputPath;

		// Token: 0x04000198 RID: 408
		private SwitchParameter force;

		// Token: 0x04000199 RID: 409
		private int fileCount;

		// Token: 0x02000063 RID: 99
		internal enum PolicyCompliance
		{
			// Token: 0x0400019B RID: 411
			Unknown,
			// Token: 0x0400019C RID: 412
			Full,
			// Token: 0x0400019D RID: 413
			Partial,
			// Token: 0x0400019E RID: 414
			NotCompliant
		}

		// Token: 0x02000064 RID: 100
		internal enum ColumnID
		{
			// Token: 0x040001A0 RID: 416
			dateColumn,
			// Token: 0x040001A1 RID: 417
			timeColumn,
			// Token: 0x040001A2 RID: 418
			userNameColumn,
			// Token: 0x040001A3 RID: 419
			uriQueryColumn,
			// Token: 0x040001A4 RID: 420
			bytesSentColumn,
			// Token: 0x040001A5 RID: 421
			bytesReceivedColumn,
			// Token: 0x040001A6 RID: 422
			computerNameColumn,
			// Token: 0x040001A7 RID: 423
			uriStemColumn,
			// Token: 0x040001A8 RID: 424
			hostColumn,
			// Token: 0x040001A9 RID: 425
			ipAddressColumn,
			// Token: 0x040001AA RID: 426
			userAgentColumn,
			// Token: 0x040001AB RID: 427
			statusColumn,
			// Token: 0x040001AC RID: 428
			subStatusColumn,
			// Token: 0x040001AD RID: 429
			maxColumn
		}

		// Token: 0x02000065 RID: 101
		private enum QueryPatternGroup
		{
			// Token: 0x040001AF RID: 431
			Cmd = 1,
			// Token: 0x040001B0 RID: 432
			DeviceId,
			// Token: 0x040001B1 RID: 433
			DeviceType,
			// Token: 0x040001B2 RID: 434
			Ty,
			// Token: 0x040001B3 RID: 435
			CliA,
			// Token: 0x040001B4 RID: 436
			CliC,
			// Token: 0x040001B5 RID: 437
			SrvA = 10,
			// Token: 0x040001B6 RID: 438
			SrvC,
			// Token: 0x040001B7 RID: 439
			SrvAA = 16,
			// Token: 0x040001B8 RID: 440
			Oof = 18,
			// Token: 0x040001B9 RID: 441
			Ssp,
			// Token: 0x040001BA RID: 442
			Unc,
			// Token: 0x040001BB RID: 443
			Att,
			// Token: 0x040001BC RID: 444
			Attb,
			// Token: 0x040001BD RID: 445
			Pa
		}

		// Token: 0x02000066 RID: 102
		internal class UserRow
		{
			// Token: 0x17000123 RID: 291
			// (get) Token: 0x060002DD RID: 733 RVA: 0x0000BED0 File Offset: 0x0000A0D0
			// (set) Token: 0x060002DE RID: 734 RVA: 0x0000BED8 File Offset: 0x0000A0D8
			public string Alias { get; set; }

			// Token: 0x17000124 RID: 292
			// (get) Token: 0x060002DF RID: 735 RVA: 0x0000BEE1 File Offset: 0x0000A0E1
			// (set) Token: 0x060002E0 RID: 736 RVA: 0x0000BEE9 File Offset: 0x0000A0E9
			public string DeviceID { get; set; }

			// Token: 0x17000125 RID: 293
			// (get) Token: 0x060002E1 RID: 737 RVA: 0x0000BEF2 File Offset: 0x0000A0F2
			// (set) Token: 0x060002E2 RID: 738 RVA: 0x0000BEFA File Offset: 0x0000A0FA
			public string DeviceType { get; set; }

			// Token: 0x17000126 RID: 294
			// (get) Token: 0x060002E3 RID: 739 RVA: 0x0000BF03 File Offset: 0x0000A103
			// (set) Token: 0x060002E4 RID: 740 RVA: 0x0000BF0B File Offset: 0x0000A10B
			public ulong ItemsSent { get; set; }

			// Token: 0x17000127 RID: 295
			// (get) Token: 0x060002E5 RID: 741 RVA: 0x0000BF14 File Offset: 0x0000A114
			// (set) Token: 0x060002E6 RID: 742 RVA: 0x0000BF1C File Offset: 0x0000A11C
			public ulong ItemsReceived { get; set; }

			// Token: 0x17000128 RID: 296
			// (get) Token: 0x060002E7 RID: 743 RVA: 0x0000BF25 File Offset: 0x0000A125
			// (set) Token: 0x060002E8 RID: 744 RVA: 0x0000BF2D File Offset: 0x0000A12D
			public ulong Hits { get; set; }

			// Token: 0x17000129 RID: 297
			// (get) Token: 0x060002E9 RID: 745 RVA: 0x0000BF36 File Offset: 0x0000A136
			// (set) Token: 0x060002EA RID: 746 RVA: 0x0000BF3E File Offset: 0x0000A13E
			public ulong BytesSent { get; set; }

			// Token: 0x1700012A RID: 298
			// (get) Token: 0x060002EB RID: 747 RVA: 0x0000BF47 File Offset: 0x0000A147
			// (set) Token: 0x060002EC RID: 748 RVA: 0x0000BF4F File Offset: 0x0000A14F
			public ulong BytesReceived { get; set; }

			// Token: 0x1700012B RID: 299
			// (get) Token: 0x060002ED RID: 749 RVA: 0x0000BF58 File Offset: 0x0000A158
			// (set) Token: 0x060002EE RID: 750 RVA: 0x0000BF60 File Offset: 0x0000A160
			public ulong EmailsSent { get; set; }

			// Token: 0x1700012C RID: 300
			// (get) Token: 0x060002EF RID: 751 RVA: 0x0000BF69 File Offset: 0x0000A169
			// (set) Token: 0x060002F0 RID: 752 RVA: 0x0000BF71 File Offset: 0x0000A171
			public ulong EmailsReceived { get; set; }

			// Token: 0x1700012D RID: 301
			// (get) Token: 0x060002F1 RID: 753 RVA: 0x0000BF7A File Offset: 0x0000A17A
			// (set) Token: 0x060002F2 RID: 754 RVA: 0x0000BF82 File Offset: 0x0000A182
			public ulong CalendarsSent { get; set; }

			// Token: 0x1700012E RID: 302
			// (get) Token: 0x060002F3 RID: 755 RVA: 0x0000BF8B File Offset: 0x0000A18B
			// (set) Token: 0x060002F4 RID: 756 RVA: 0x0000BF93 File Offset: 0x0000A193
			public ulong CalendarsReceived { get; set; }

			// Token: 0x1700012F RID: 303
			// (get) Token: 0x060002F5 RID: 757 RVA: 0x0000BF9C File Offset: 0x0000A19C
			// (set) Token: 0x060002F6 RID: 758 RVA: 0x0000BFA4 File Offset: 0x0000A1A4
			public ulong ContactsSent { get; set; }

			// Token: 0x17000130 RID: 304
			// (get) Token: 0x060002F7 RID: 759 RVA: 0x0000BFAD File Offset: 0x0000A1AD
			// (set) Token: 0x060002F8 RID: 760 RVA: 0x0000BFB5 File Offset: 0x0000A1B5
			public ulong ContactsReceived { get; set; }

			// Token: 0x17000131 RID: 305
			// (get) Token: 0x060002F9 RID: 761 RVA: 0x0000BFBE File Offset: 0x0000A1BE
			// (set) Token: 0x060002FA RID: 762 RVA: 0x0000BFC6 File Offset: 0x0000A1C6
			public ulong TasksSent { get; set; }

			// Token: 0x17000132 RID: 306
			// (get) Token: 0x060002FB RID: 763 RVA: 0x0000BFCF File Offset: 0x0000A1CF
			// (set) Token: 0x060002FC RID: 764 RVA: 0x0000BFD7 File Offset: 0x0000A1D7
			public ulong TasksReceived { get; set; }

			// Token: 0x17000133 RID: 307
			// (get) Token: 0x060002FD RID: 765 RVA: 0x0000BFE0 File Offset: 0x0000A1E0
			// (set) Token: 0x060002FE RID: 766 RVA: 0x0000BFE8 File Offset: 0x0000A1E8
			public ulong NotesSent { get; set; }

			// Token: 0x17000134 RID: 308
			// (get) Token: 0x060002FF RID: 767 RVA: 0x0000BFF1 File Offset: 0x0000A1F1
			// (set) Token: 0x06000300 RID: 768 RVA: 0x0000BFF9 File Offset: 0x0000A1F9
			public ulong NotesReceived { get; set; }

			// Token: 0x17000135 RID: 309
			// (get) Token: 0x06000301 RID: 769 RVA: 0x0000C002 File Offset: 0x0000A202
			// (set) Token: 0x06000302 RID: 770 RVA: 0x0000C00A File Offset: 0x0000A20A
			public ulong NumberOfOOFSet { get; set; }

			// Token: 0x17000136 RID: 310
			// (get) Token: 0x06000303 RID: 771 RVA: 0x0000C013 File Offset: 0x0000A213
			// (set) Token: 0x06000304 RID: 772 RVA: 0x0000C01B File Offset: 0x0000A21B
			public ulong NumberOfOOFGet { get; set; }

			// Token: 0x17000137 RID: 311
			// (get) Token: 0x06000305 RID: 773 RVA: 0x0000C024 File Offset: 0x0000A224
			// (set) Token: 0x06000306 RID: 774 RVA: 0x0000C02C File Offset: 0x0000A22C
			public ulong SearchRequests { get; set; }

			// Token: 0x17000138 RID: 312
			// (get) Token: 0x06000307 RID: 775 RVA: 0x0000C035 File Offset: 0x0000A235
			// (set) Token: 0x06000308 RID: 776 RVA: 0x0000C03D File Offset: 0x0000A23D
			public ulong SharePointHits { get; set; }

			// Token: 0x17000139 RID: 313
			// (get) Token: 0x06000309 RID: 777 RVA: 0x0000C046 File Offset: 0x0000A246
			// (set) Token: 0x0600030A RID: 778 RVA: 0x0000C04E File Offset: 0x0000A24E
			public ulong UncHits { get; set; }

			// Token: 0x1700013A RID: 314
			// (get) Token: 0x0600030B RID: 779 RVA: 0x0000C057 File Offset: 0x0000A257
			// (set) Token: 0x0600030C RID: 780 RVA: 0x0000C05F File Offset: 0x0000A25F
			public ulong AttachmentHits { get; set; }

			// Token: 0x1700013B RID: 315
			// (get) Token: 0x0600030D RID: 781 RVA: 0x0000C068 File Offset: 0x0000A268
			// (set) Token: 0x0600030E RID: 782 RVA: 0x0000C070 File Offset: 0x0000A270
			public ulong AttachmentBytes { get; set; }

			// Token: 0x1700013C RID: 316
			// (get) Token: 0x0600030F RID: 783 RVA: 0x0000C079 File Offset: 0x0000A279
			// (set) Token: 0x06000310 RID: 784 RVA: 0x0000C081 File Offset: 0x0000A281
			public DateTime LastPolicyTime { get; set; }

			// Token: 0x1700013D RID: 317
			// (get) Token: 0x06000311 RID: 785 RVA: 0x0000C08A File Offset: 0x0000A28A
			// (set) Token: 0x06000312 RID: 786 RVA: 0x0000C092 File Offset: 0x0000A292
			public ExportMobileSyncLog.PolicyCompliance PolicyCompliance { get; set; }
		}

		// Token: 0x02000067 RID: 103
		internal class ServerRow
		{
			// Token: 0x1700013E RID: 318
			// (get) Token: 0x06000314 RID: 788 RVA: 0x0000C0A3 File Offset: 0x0000A2A3
			// (set) Token: 0x06000315 RID: 789 RVA: 0x0000C0AB File Offset: 0x0000A2AB
			public string ComputerName { get; set; }

			// Token: 0x1700013F RID: 319
			// (get) Token: 0x06000316 RID: 790 RVA: 0x0000C0B4 File Offset: 0x0000A2B4
			// (set) Token: 0x06000317 RID: 791 RVA: 0x0000C0BC File Offset: 0x0000A2BC
			public string HostName { get; set; }

			// Token: 0x17000140 RID: 320
			// (get) Token: 0x06000318 RID: 792 RVA: 0x0000C0C5 File Offset: 0x0000A2C5
			// (set) Token: 0x06000319 RID: 793 RVA: 0x0000C0CD File Offset: 0x0000A2CD
			public string IPAddress { get; set; }

			// Token: 0x17000141 RID: 321
			// (get) Token: 0x0600031A RID: 794 RVA: 0x0000C0D6 File Offset: 0x0000A2D6
			// (set) Token: 0x0600031B RID: 795 RVA: 0x0000C0DE File Offset: 0x0000A2DE
			public Dictionary<string, Dictionary<string, bool>> DevicesPerDay { get; set; }

			// Token: 0x17000142 RID: 322
			// (get) Token: 0x0600031C RID: 796 RVA: 0x0000C0E7 File Offset: 0x0000A2E7
			// (set) Token: 0x0600031D RID: 797 RVA: 0x0000C0EF File Offset: 0x0000A2EF
			public ulong Hits { get; set; }

			// Token: 0x17000143 RID: 323
			// (get) Token: 0x0600031E RID: 798 RVA: 0x0000C0F8 File Offset: 0x0000A2F8
			// (set) Token: 0x0600031F RID: 799 RVA: 0x0000C100 File Offset: 0x0000A300
			public ulong BytesSent { get; set; }

			// Token: 0x17000144 RID: 324
			// (get) Token: 0x06000320 RID: 800 RVA: 0x0000C109 File Offset: 0x0000A309
			// (set) Token: 0x06000321 RID: 801 RVA: 0x0000C111 File Offset: 0x0000A311
			public ulong BytesReceived { get; set; }
		}

		// Token: 0x02000068 RID: 104
		internal class HourlyRow
		{
			// Token: 0x17000145 RID: 325
			// (get) Token: 0x06000323 RID: 803 RVA: 0x0000C122 File Offset: 0x0000A322
			// (set) Token: 0x06000324 RID: 804 RVA: 0x0000C12A File Offset: 0x0000A32A
			public DayOfWeek Day { get; set; }

			// Token: 0x17000146 RID: 326
			// (get) Token: 0x06000325 RID: 805 RVA: 0x0000C133 File Offset: 0x0000A333
			// (set) Token: 0x06000326 RID: 806 RVA: 0x0000C13B File Offset: 0x0000A33B
			public uint Hour { get; set; }

			// Token: 0x17000147 RID: 327
			// (get) Token: 0x06000327 RID: 807 RVA: 0x0000C144 File Offset: 0x0000A344
			// (set) Token: 0x06000328 RID: 808 RVA: 0x0000C14C File Offset: 0x0000A34C
			public Dictionary<string, bool> Devices { get; set; }

			// Token: 0x17000148 RID: 328
			// (get) Token: 0x06000329 RID: 809 RVA: 0x0000C155 File Offset: 0x0000A355
			// (set) Token: 0x0600032A RID: 810 RVA: 0x0000C15D File Offset: 0x0000A35D
			public ulong SyncCount { get; set; }
		}

		// Token: 0x02000069 RID: 105
		internal class StatusRow
		{
			// Token: 0x17000149 RID: 329
			// (get) Token: 0x0600032C RID: 812 RVA: 0x0000C16E File Offset: 0x0000A36E
			// (set) Token: 0x0600032D RID: 813 RVA: 0x0000C176 File Offset: 0x0000A376
			public string Status { get; set; }

			// Token: 0x1700014A RID: 330
			// (get) Token: 0x0600032E RID: 814 RVA: 0x0000C17F File Offset: 0x0000A37F
			// (set) Token: 0x0600032F RID: 815 RVA: 0x0000C187 File Offset: 0x0000A387
			public string SubStatus { get; set; }

			// Token: 0x1700014B RID: 331
			// (get) Token: 0x06000330 RID: 816 RVA: 0x0000C190 File Offset: 0x0000A390
			// (set) Token: 0x06000331 RID: 817 RVA: 0x0000C198 File Offset: 0x0000A398
			public ulong Hits { get; set; }
		}

		// Token: 0x0200006A RID: 106
		internal class UserAgentRow
		{
			// Token: 0x1700014C RID: 332
			// (get) Token: 0x06000333 RID: 819 RVA: 0x0000C1A9 File Offset: 0x0000A3A9
			// (set) Token: 0x06000334 RID: 820 RVA: 0x0000C1B1 File Offset: 0x0000A3B1
			public string UserAgent { get; set; }

			// Token: 0x1700014D RID: 333
			// (get) Token: 0x06000335 RID: 821 RVA: 0x0000C1BA File Offset: 0x0000A3BA
			// (set) Token: 0x06000336 RID: 822 RVA: 0x0000C1C2 File Offset: 0x0000A3C2
			public ulong Hits { get; set; }

			// Token: 0x1700014E RID: 334
			// (get) Token: 0x06000337 RID: 823 RVA: 0x0000C1CB File Offset: 0x0000A3CB
			// (set) Token: 0x06000338 RID: 824 RVA: 0x0000C1D3 File Offset: 0x0000A3D3
			public Dictionary<string, bool> Devices { get; set; }
		}

		// Token: 0x0200006B RID: 107
		internal class AirSyncLogParser
		{
			// Token: 0x0600033A RID: 826 RVA: 0x0000C1E4 File Offset: 0x0000A3E4
			public AirSyncLogParser()
			{
				this.startTime = DateTime.MinValue;
				this.endTime = DateTime.MaxValue;
				this.hourlyTable = new ExportMobileSyncLog.HourlyRow[168];
				for (uint num = 0U; num < 7U; num += 1U)
				{
					for (uint num2 = 0U; num2 < 24U; num2 += 1U)
					{
						ExportMobileSyncLog.HourlyRow hourlyRow = new ExportMobileSyncLog.HourlyRow();
						hourlyRow.Devices = new Dictionary<string, bool>();
						hourlyRow.Day = (DayOfWeek)num;
						hourlyRow.Hour = num2;
						this.hourlyTable[(int)((UIntPtr)(num * 24U + num2))] = hourlyRow;
					}
				}
				this.fieldsPattern = new Regex("^#\\s*Fields\\s*:\\s*(.*?)\\s*$", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);
				this.valuesPattern = new Regex("(\\S+)(?:\\s+|$)", RegexOptions.Compiled | RegexOptions.CultureInvariant);
				this.queryPattern = new Regex("^(?:(?:^|&)(?:(?i:Cmd)=(?<Cmd>[^&]*)|(?i:DeviceId)=(?<DeviceId>[^&]*)|(?i:DeviceType)=(?<DeviceType>[^&]*)|(?i:Log)=(?:(?:Ty:(?<Ty>[^_]+)|Cli:(?<CliA>[0-9]+)a(?<CliC>[0-9]+)c(?<CliD>[0-9]+)d(?<CliF>[0-9]+)f(?<CliE>[0-9]+)e|Srv:(?<SrvA>[0-9]+)a(?<SrvC>[0-9]+)c(?<SrvD>[0-9]+)d(?<SrvS>[0-9]+)s(?<SrvE>[0-9]+)e(?<SrvR>[0-9]+)r(?<SrvAA>[0-9]+)A(?<SrvSD>[0-9]+)sd|Oof:(?<Oof>[^_]*)|Ssp(?<Ssp>[0-9]+)|Unc(?<Unc>[0-9]+)|Att(?<Att>[0-9]+)|Attb(?<Attb>[0-9]+)|Pa(?<Pa>[0-9]+)|[a-zA-Z]+(?:[0-9]+|:[^_]*))(?:_|$|(?=&)))*|[^=]+=[^&]*))*$", RegexOptions.Compiled | RegexOptions.CultureInvariant);
				this.columnNameToID = new Dictionary<string, ExportMobileSyncLog.ColumnID>();
				this.columnNameToID.Add("date", ExportMobileSyncLog.ColumnID.dateColumn);
				this.columnNameToID.Add("time", ExportMobileSyncLog.ColumnID.timeColumn);
				this.columnNameToID.Add("cs-username", ExportMobileSyncLog.ColumnID.userNameColumn);
				this.columnNameToID.Add("cs-uri-query", ExportMobileSyncLog.ColumnID.uriQueryColumn);
				this.columnNameToID.Add("sc-bytes", ExportMobileSyncLog.ColumnID.bytesSentColumn);
				this.columnNameToID.Add("cs-bytes", ExportMobileSyncLog.ColumnID.bytesReceivedColumn);
				this.columnNameToID.Add("s-computername", ExportMobileSyncLog.ColumnID.computerNameColumn);
				this.columnNameToID.Add("cs-uri-stem", ExportMobileSyncLog.ColumnID.uriStemColumn);
				this.columnNameToID.Add("cs-host", ExportMobileSyncLog.ColumnID.hostColumn);
				this.columnNameToID.Add("s-ip", ExportMobileSyncLog.ColumnID.ipAddressColumn);
				this.columnNameToID.Add("cs(user-agent)", ExportMobileSyncLog.ColumnID.userAgentColumn);
				this.columnNameToID.Add("sc-status", ExportMobileSyncLog.ColumnID.statusColumn);
				this.columnNameToID.Add("sc-substatus", ExportMobileSyncLog.ColumnID.subStatusColumn);
			}

			// Token: 0x1700014F RID: 335
			// (get) Token: 0x0600033B RID: 827 RVA: 0x0000C3E9 File Offset: 0x0000A5E9
			// (set) Token: 0x0600033C RID: 828 RVA: 0x0000C3F1 File Offset: 0x0000A5F1
			public DateTime StartTime
			{
				get
				{
					return this.startTime;
				}
				set
				{
					this.startTime = value;
				}
			}

			// Token: 0x17000150 RID: 336
			// (get) Token: 0x0600033D RID: 829 RVA: 0x0000C3FA File Offset: 0x0000A5FA
			// (set) Token: 0x0600033E RID: 830 RVA: 0x0000C402 File Offset: 0x0000A602
			public DateTime EndTime
			{
				get
				{
					return this.endTime;
				}
				set
				{
					this.endTime = value;
				}
			}

			// Token: 0x0600033F RID: 831 RVA: 0x0000C40B File Offset: 0x0000A60B
			public void StartNewFile()
			{
				this.columnCount = 0;
			}

			// Token: 0x06000340 RID: 832 RVA: 0x0000C414 File Offset: 0x0000A614
			public void ParseLine(string line)
			{
				Match match = null;
				if (line.Length == 0)
				{
					return;
				}
				if (line[0] == '#')
				{
					match = this.fieldsPattern.Match(line);
					if (match.Success)
					{
						string value = match.Groups[1].Value;
						MatchCollection matchCollection = this.valuesPattern.Matches(value);
						this.columnCount = matchCollection.Count;
						this.columnIndexToID = new ExportMobileSyncLog.ColumnID[this.columnCount];
						for (int i = 0; i < this.columnCount; i++)
						{
							string key = matchCollection[i].Groups[1].Value.ToLower(CultureInfo.InvariantCulture);
							ExportMobileSyncLog.ColumnID columnID;
							if (this.columnNameToID.TryGetValue(key, out columnID))
							{
								this.columnIndexToID[i] = columnID;
							}
							else
							{
								this.columnIndexToID[i] = ExportMobileSyncLog.ColumnID.maxColumn;
							}
						}
					}
					return;
				}
				if (this.columnCount == 0)
				{
					return;
				}
				this.columnValues = new string[13];
				int num = 0;
				int num2 = 0;
				int j = 0;
				while (j < line.Length)
				{
					while (num2 < line.Length && line[num2] == ' ')
					{
						num2++;
					}
					j = num2;
					while (j < line.Length && line[j] != ' ')
					{
						j++;
					}
					if (j == num2)
					{
						break;
					}
					if (num >= this.columnCount)
					{
						return;
					}
					ExportMobileSyncLog.ColumnID columnID2 = this.columnIndexToID[num];
					if (columnID2 != ExportMobileSyncLog.ColumnID.maxColumn)
					{
						string text = line.Substring(num2, j - num2);
						if (text.Length == 1 && text[0] == '-')
						{
							text = null;
						}
						this.columnValues[(int)columnID2] = text;
					}
					num++;
					num2 = j;
				}
				if (num != this.columnCount)
				{
					return;
				}
				string strA = this.columnValues[7];
				if (string.Compare(strA, "/Microsoft-Server-ActiveSync", true, CultureInfo.InvariantCulture) != 0 && string.Compare(strA, "/Microsoft-Server-ActiveSync/default.eas", true, CultureInfo.InvariantCulture) != 0)
				{
					return;
				}
				string text2 = this.columnValues[0];
				string text3 = this.columnValues[1];
				if (text2 == null || text3 == null)
				{
					return;
				}
				DateTime dateTime;
				if (!DateTime.TryParse(text2 + " " + text3, out dateTime))
				{
					return;
				}
				if (dateTime < this.startTime || dateTime > this.endTime)
				{
					return;
				}
				string text4 = this.columnValues[3];
				string text5 = this.columnValues[2];
				string text6 = this.columnValues[6];
				string text7 = this.columnValues[8];
				string text8 = this.columnValues[9];
				string text9 = this.columnValues[11];
				string text10 = this.columnValues[12];
				string text11 = this.columnValues[10];
				ulong num3 = 0UL;
				ulong num4 = 0UL;
				ulong.TryParse(this.columnValues[4], out num3);
				ulong.TryParse(this.columnValues[5], out num4);
				string strA2 = null;
				string text12 = null;
				string deviceType = null;
				uint num5 = 0U;
				uint num6 = 0U;
				uint num7 = 0U;
				uint num8 = 0U;
				string strA3 = null;
				uint num9 = 0U;
				uint num10 = 0U;
				uint num11 = 0U;
				uint num12 = 0U;
				uint num13 = 0U;
				uint num14 = 0U;
				if (text4 != null)
				{
					match = this.queryPattern.Match(text4);
					if (!match.Success)
					{
						return;
					}
					strA2 = match.Groups[1].Value;
					text12 = match.Groups[2].Value;
					deviceType = match.Groups[3].Value;
					strA3 = match.Groups[18].Value;
					uint.TryParse(match.Groups[19].Value, out num9);
					uint.TryParse(match.Groups[20].Value, out num10);
					uint.TryParse(match.Groups[21].Value, out num11);
					uint.TryParse(match.Groups[22].Value, out num12);
					uint.TryParse(match.Groups[23].Value, out num13);
				}
				string text13 = "";
				if (!string.IsNullOrEmpty(text5))
				{
					text13 += text5.ToLower(CultureInfo.InvariantCulture);
				}
				text13 += "_";
				if (!string.IsNullOrEmpty(text12))
				{
					text13 += text12.ToLower(CultureInfo.InvariantCulture);
				}
				if (text13.Length > 1)
				{
					ExportMobileSyncLog.UserRow userRow;
					if (!this.userTable.TryGetValue(text13, out userRow))
					{
						userRow = new ExportMobileSyncLog.UserRow();
						userRow.Alias = text5;
						userRow.DeviceID = text12;
						userRow.DeviceType = deviceType;
						this.userTable.Add(text13, userRow);
					}
					if (text4 != null)
					{
						int count = match.Groups[4].Captures.Count;
						if (count > 0)
						{
							int num15 = match.Groups[10].Captures.Count - 1;
							int num16 = match.Groups[5].Captures.Count - 1;
							for (int k = count - 1; k >= 0; k--)
							{
								num6 = (num5 = (num7 = (num8 = (num14 = 0U))));
								int index = match.Groups[4].Captures[k].Index;
								string value2 = match.Groups[4].Captures[k].Value;
								if (num16 >= 0 && match.Groups[5].Captures.Count > num16 && index < match.Groups[5].Captures[num16].Index)
								{
									uint.TryParse(match.Groups[5].Captures[num16].Value, out num5);
									uint.TryParse(match.Groups[6].Captures[num16].Value, out num6);
									num16--;
								}
								if (num15 >= 0 && match.Groups[10].Captures.Count > num15 && index < match.Groups[10].Captures[num15].Index)
								{
									uint.TryParse(match.Groups[10].Captures[num15].Value, out num7);
									uint.TryParse(match.Groups[11].Captures[num15].Value, out num8);
									uint.TryParse(match.Groups[16].Captures[num15].Value, out num14);
									num15--;
								}
								userRow.ItemsSent += (ulong)(num7 + num8 + num14);
								userRow.ItemsReceived += (ulong)(num5 + num6);
								if (string.Compare(value2, "Em", false, CultureInfo.InvariantCulture) == 0)
								{
									userRow.EmailsSent += (ulong)(num7 + num14 + num8);
								}
								if (string.Compare(value2, "Ca", false, CultureInfo.InvariantCulture) == 0)
								{
									userRow.CalendarsSent += (ulong)(num7 + num8);
									userRow.CalendarsReceived += (ulong)(num5 + num6);
								}
								if (string.Compare(value2, "Co", false, CultureInfo.InvariantCulture) == 0)
								{
									userRow.ContactsSent += (ulong)(num7 + num8);
									userRow.ContactsReceived += (ulong)(num5 + num6);
								}
								if (string.Compare(value2, "Ta", false, CultureInfo.InvariantCulture) == 0)
								{
									userRow.TasksSent += (ulong)(num7 + num8);
									userRow.TasksReceived += (ulong)(num5 + num6);
								}
								if (string.Compare(value2, "Nt", false, CultureInfo.InvariantCulture) == 0)
								{
									userRow.NotesSent += (ulong)(num7 + num8);
									userRow.NotesReceived += (ulong)(num5 + num6);
								}
							}
						}
						else
						{
							uint.TryParse(match.Groups[5].Value, out num5);
							uint.TryParse(match.Groups[6].Value, out num6);
							uint.TryParse(match.Groups[10].Value, out num7);
							uint.TryParse(match.Groups[11].Value, out num8);
							userRow.ItemsSent += (ulong)(num7 + num8);
							userRow.ItemsReceived += (ulong)(num5 + num6);
						}
					}
					if (string.Compare(strA2, "SendMail", true, CultureInfo.InvariantCulture) == 0 || string.Compare(strA2, "SmartReply", true, CultureInfo.InvariantCulture) == 0 || string.Compare(strA2, "SmartForward", true, CultureInfo.InvariantCulture) == 0)
					{
						userRow.EmailsReceived += 1UL;
					}
					if (string.Compare(strA2, "Settings", true, CultureInfo.InvariantCulture) == 0)
					{
						if (string.Compare(strA3, "Set", true, CultureInfo.InvariantCulture) == 0)
						{
							userRow.NumberOfOOFSet += 1UL;
						}
						if (string.Compare(strA3, "Get", true, CultureInfo.InvariantCulture) == 0)
						{
							userRow.NumberOfOOFGet += 1UL;
						}
					}
					if (string.Compare(strA2, "Search", true, CultureInfo.InvariantCulture) == 0)
					{
						userRow.SearchRequests += 1UL;
					}
					userRow.Hits += 1UL;
					userRow.BytesSent += num3;
					userRow.BytesReceived += num4;
					userRow.SharePointHits += (ulong)num9;
					userRow.UncHits += (ulong)num10;
					userRow.AttachmentHits += (ulong)num11;
					userRow.AttachmentBytes += (ulong)num12;
					if (num13 > 0U && dateTime > userRow.LastPolicyTime)
					{
						userRow.LastPolicyTime = dateTime;
						userRow.PolicyCompliance = (ExportMobileSyncLog.PolicyCompliance)num13;
					}
				}
				StringBuilder stringBuilder = new StringBuilder();
				if (text6 != null)
				{
					stringBuilder.Append(text6.ToLower(CultureInfo.InvariantCulture));
				}
				stringBuilder.Append('_');
				if (text7 != null)
				{
					stringBuilder.Append(text7.ToLower(CultureInfo.InvariantCulture));
				}
				stringBuilder.Append('_');
				if (text8 != null)
				{
					stringBuilder.Append(text8.ToLower(CultureInfo.InvariantCulture));
				}
				if (stringBuilder.Length > 2)
				{
					string key2 = stringBuilder.ToString();
					ExportMobileSyncLog.ServerRow serverRow;
					if (!this.serverTable.TryGetValue(key2, out serverRow))
					{
						serverRow = new ExportMobileSyncLog.ServerRow();
						serverRow.DevicesPerDay = new Dictionary<string, Dictionary<string, bool>>();
						serverRow.ComputerName = text6;
						serverRow.HostName = text7;
						serverRow.IPAddress = text8;
						this.serverTable.Add(key2, serverRow);
					}
					if (text12 != null)
					{
						Dictionary<string, bool> dictionary;
						if (!serverRow.DevicesPerDay.TryGetValue(text2, out dictionary))
						{
							dictionary = new Dictionary<string, bool>();
							serverRow.DevicesPerDay.Add(text2, dictionary);
						}
						dictionary[text12] = true;
					}
					serverRow.Hits += 1UL;
					serverRow.BytesSent += num3;
					serverRow.BytesReceived += num4;
				}
				ExportMobileSyncLog.HourlyRow hourlyRow = this.hourlyTable[(int)(dateTime.DayOfWeek * (DayOfWeek)24 + dateTime.Hour)];
				if (text12 != null)
				{
					hourlyRow.Devices[text12] = true;
				}
				if (string.Compare(strA2, "Sync", true, CultureInfo.InvariantCulture) == 0 || string.Compare(strA2, "GetItemEstimate", true, CultureInfo.InvariantCulture) == 0)
				{
					hourlyRow.SyncCount += 1UL;
				}
				string text14 = "";
				if (text9 != null)
				{
					text14 = text9.PadLeft(3, '0');
				}
				text14 += "_";
				if (text10 != null)
				{
					text14 += text10.PadLeft(11, '0');
				}
				if (text14.Length > 1)
				{
					ExportMobileSyncLog.StatusRow statusRow;
					if (!this.statusTable.TryGetValue(text14, out statusRow))
					{
						statusRow = new ExportMobileSyncLog.StatusRow();
						statusRow.Status = text9;
						statusRow.SubStatus = text10;
						this.statusTable.Add(text14, statusRow);
					}
					statusRow.Hits += 1UL;
				}
				if (text11 != null)
				{
					ExportMobileSyncLog.UserAgentRow userAgentRow;
					if (!this.userAgentTable.TryGetValue(text11, out userAgentRow))
					{
						userAgentRow = new ExportMobileSyncLog.UserAgentRow();
						userAgentRow.Devices = new Dictionary<string, bool>();
						userAgentRow.UserAgent = text11;
						this.userAgentTable.Add(text11, userAgentRow);
					}
					userAgentRow.Hits += 1UL;
					if (text12 != null)
					{
						userAgentRow.Devices[text12] = true;
					}
				}
			}

			// Token: 0x06000341 RID: 833 RVA: 0x0000D05C File Offset: 0x0000B25C
			public void WriteUserResults(StreamWriter writer)
			{
				string[] array = new string[]
				{
					"User Name",
					"Device ID",
					"Device Type",
					"Items Sent",
					"Items Received",
					"Hits",
					"Total Bytes Sent",
					"Total Bytes Received",
					"Total Emails Sent",
					"Total Emails Received",
					"Total Calendar Sent",
					"Total Calendar Received",
					"Total Contacts Sent",
					"Total Contacts Received",
					"Total Tasks Sent",
					"Total Tasks Received",
					"Total Notes Sent",
					"Total Notes Received",
					"Total OOF Messages Set by Client",
					"Total OOF Messages Retrieved from Server",
					"Total Searches requested",
					"Total SharePoint Access",
					"Total UNC Access",
					"Total Attachment Downloads",
					"Total Attachment Size",
					"Currently Compliant with Policy"
				};
				string[] array2 = new string[]
				{
					"Unknown",
					"Compliant",
					"Partially Compliant",
					"Not Compliant"
				};
				ExportMobileSyncLog.AirSyncLogParser.WriteCSVLine(writer, array);
				string[] array3 = new string[array.Length];
				ulong num = 0UL;
				ulong num2 = 0UL;
				ulong num3 = 0UL;
				ulong num4 = 0UL;
				ulong num5 = 0UL;
				string[] array4 = new string[this.userTable.Count];
				this.userTable.Keys.CopyTo(array4, 0);
				Array.Sort<string>(array4);
				foreach (string key in array4)
				{
					ExportMobileSyncLog.UserRow userRow = this.userTable[key];
					Array.Clear(array3, 0, array3.Length);
					array3[0] = userRow.Alias;
					array3[1] = userRow.DeviceID;
					array3[2] = userRow.DeviceType;
					array3[3] = userRow.ItemsSent.ToString();
					array3[4] = userRow.ItemsReceived.ToString();
					array3[5] = userRow.Hits.ToString();
					array3[6] = userRow.BytesSent.ToString();
					array3[7] = userRow.BytesReceived.ToString();
					array3[8] = userRow.EmailsSent.ToString();
					array3[9] = userRow.EmailsReceived.ToString();
					array3[10] = userRow.CalendarsSent.ToString();
					array3[11] = userRow.CalendarsReceived.ToString();
					array3[12] = userRow.ContactsSent.ToString();
					array3[13] = userRow.ContactsReceived.ToString();
					array3[14] = userRow.TasksSent.ToString();
					array3[15] = userRow.TasksReceived.ToString();
					array3[16] = userRow.NotesSent.ToString();
					array3[17] = userRow.NotesReceived.ToString();
					array3[18] = userRow.NumberOfOOFSet.ToString();
					array3[19] = userRow.NumberOfOOFGet.ToString();
					array3[20] = userRow.SearchRequests.ToString();
					array3[21] = userRow.SharePointHits.ToString();
					array3[22] = userRow.UncHits.ToString();
					array3[23] = userRow.AttachmentHits.ToString();
					array3[24] = userRow.AttachmentBytes.ToString();
					array3[25] = array2[(int)userRow.PolicyCompliance];
					ExportMobileSyncLog.AirSyncLogParser.WriteCSVLine(writer, array3);
					num += userRow.ItemsSent;
					num2 += userRow.ItemsReceived;
					num3 += userRow.Hits;
					num4 += userRow.BytesSent;
					num5 += userRow.BytesReceived;
				}
				Array.Clear(array3, 0, array3.Length);
				array3[0] = "*** Total ***";
				array3[3] = num.ToString();
				array3[4] = num2.ToString();
				array3[5] = num3.ToString();
				array3[6] = num4.ToString();
				array3[7] = num5.ToString();
				ExportMobileSyncLog.AirSyncLogParser.WriteCSVLine(writer, array3);
			}

			// Token: 0x06000342 RID: 834 RVA: 0x0000D494 File Offset: 0x0000B694
			public void WriteServerResults(StreamWriter writer)
			{
				string[] array = new string[]
				{
					"Server",
					"Host",
					"IP Address",
					"Average Unique Devices",
					"Hits",
					"Total Bytes Sent",
					"Total Bytes Received"
				};
				ExportMobileSyncLog.AirSyncLogParser.WriteCSVLine(writer, array);
				string[] array2 = new string[array.Length];
				string[] array3 = new string[this.serverTable.Count];
				this.serverTable.Keys.CopyTo(array3, 0);
				Array.Sort<string>(array3);
				foreach (string key in array3)
				{
					ExportMobileSyncLog.ServerRow serverRow = this.serverTable[key];
					Array.Clear(array2, 0, array2.Length);
					uint num = 0U;
					uint num2 = 0U;
					foreach (Dictionary<string, bool> dictionary in serverRow.DevicesPerDay.Values)
					{
						num += 1U;
						num2 += (uint)dictionary.Count;
					}
					uint num3 = 0U;
					if (num > 0U)
					{
						num3 = num2 / num;
					}
					array2[0] = serverRow.ComputerName;
					array2[1] = serverRow.HostName;
					array2[2] = serverRow.IPAddress;
					array2[3] = num3.ToString();
					array2[4] = serverRow.Hits.ToString();
					array2[5] = serverRow.BytesSent.ToString();
					array2[6] = serverRow.BytesReceived.ToString();
					ExportMobileSyncLog.AirSyncLogParser.WriteCSVLine(writer, array2);
				}
			}

			// Token: 0x06000343 RID: 835 RVA: 0x0000D634 File Offset: 0x0000B834
			public void WriteHourlyResults(StreamWriter writer)
			{
				string[] array = new string[]
				{
					"Day",
					"Hour",
					"Unique Devices",
					"Sync Related Requests"
				};
				ExportMobileSyncLog.AirSyncLogParser.WriteCSVLine(writer, array);
				string[] array2 = new string[array.Length];
				foreach (ExportMobileSyncLog.HourlyRow hourlyRow in this.hourlyTable)
				{
					Array.Clear(array2, 0, array2.Length);
					array2[0] = hourlyRow.Day.ToString();
					array2[1] = hourlyRow.Hour.ToString();
					array2[2] = hourlyRow.Devices.Count.ToString();
					array2[3] = hourlyRow.SyncCount.ToString();
					ExportMobileSyncLog.AirSyncLogParser.WriteCSVLine(writer, array2);
				}
			}

			// Token: 0x06000344 RID: 836 RVA: 0x0000D6FC File Offset: 0x0000B8FC
			public void WriteStatusCodeResults(StreamWriter writer)
			{
				string[] array = new string[]
				{
					"Status",
					"SubStatus",
					"Hits",
					"Ratio"
				};
				ExportMobileSyncLog.AirSyncLogParser.WriteCSVLine(writer, array);
				ulong num = 0UL;
				foreach (ExportMobileSyncLog.StatusRow statusRow in this.statusTable.Values)
				{
					num += statusRow.Hits;
				}
				string[] array2 = new string[array.Length];
				string[] array3 = new string[this.statusTable.Count];
				this.statusTable.Keys.CopyTo(array3, 0);
				Array.Sort<string>(array3);
				foreach (string key in array3)
				{
					ExportMobileSyncLog.StatusRow statusRow2 = this.statusTable[key];
					Array.Clear(array2, 0, array2.Length);
					array2[0] = statusRow2.Status;
					array2[1] = statusRow2.SubStatus;
					array2[2] = statusRow2.Hits.ToString();
					if (num > 0UL)
					{
						array2[3] = (statusRow2.Hits / num).ToString();
					}
					ExportMobileSyncLog.AirSyncLogParser.WriteCSVLine(writer, array2);
				}
			}

			// Token: 0x06000345 RID: 837 RVA: 0x0000D848 File Offset: 0x0000BA48
			public void WritePolicyComplianceResults(StreamWriter writer)
			{
				string[] array = new string[]
				{
					"Compliance Type",
					"Total",
					"Ratio"
				};
				ExportMobileSyncLog.AirSyncLogParser.WriteCSVLine(writer, array);
				ulong[] array2 = new ulong[this.policyComplianceStrings.Length];
				foreach (ExportMobileSyncLog.UserRow userRow in this.userTable.Values)
				{
					array2[(int)userRow.PolicyCompliance] += 1UL;
				}
				string[] array3 = new string[array.Length];
				for (int i = 0; i < this.policyComplianceStrings.Length; i++)
				{
					Array.Clear(array3, 0, array3.Length);
					array3[0] = this.policyComplianceStrings[i];
					array3[1] = array2[i].ToString();
					if (this.userTable.Count > 0)
					{
						array3[2] = (array2[i] / (float)this.userTable.Count).ToString();
					}
					ExportMobileSyncLog.AirSyncLogParser.WriteCSVLine(writer, array3);
				}
			}

			// Token: 0x06000346 RID: 838 RVA: 0x0000D96C File Offset: 0x0000BB6C
			public void WriteUserAgentResults(StreamWriter writer)
			{
				string[] array = new string[]
				{
					"User Agent",
					"Hits",
					"Unique Devices"
				};
				ExportMobileSyncLog.AirSyncLogParser.WriteCSVLine(writer, array);
				string[] array2 = new string[array.Length];
				string[] array3 = new string[this.userAgentTable.Count];
				this.userAgentTable.Keys.CopyTo(array3, 0);
				Array.Sort<string>(array3);
				foreach (string key in array3)
				{
					ExportMobileSyncLog.UserAgentRow userAgentRow = this.userAgentTable[key];
					Array.Clear(array2, 0, array2.Length);
					array2[0] = userAgentRow.UserAgent;
					array2[1] = userAgentRow.Hits.ToString();
					array2[2] = userAgentRow.Devices.Count.ToString();
					ExportMobileSyncLog.AirSyncLogParser.WriteCSVLine(writer, array2);
				}
			}

			// Token: 0x06000347 RID: 839 RVA: 0x0000DA48 File Offset: 0x0000BC48
			private static void WriteCSVLine(StreamWriter writer, string[] values)
			{
				for (int i = 0; i < values.Length; i++)
				{
					if (i > 0)
					{
						writer.Write(',');
					}
					if (values[i] != null)
					{
						if (values[i].IndexOf(',') == -1)
						{
							writer.Write(values[i]);
						}
						else
						{
							writer.Write('"');
							writer.Write(values[i].Replace("\"", "\"\""));
							writer.Write('"');
						}
					}
				}
				writer.WriteLine();
			}

			// Token: 0x040001EA RID: 490
			private DateTime startTime;

			// Token: 0x040001EB RID: 491
			private DateTime endTime;

			// Token: 0x040001EC RID: 492
			private Dictionary<string, ExportMobileSyncLog.UserRow> userTable = new Dictionary<string, ExportMobileSyncLog.UserRow>();

			// Token: 0x040001ED RID: 493
			private ExportMobileSyncLog.HourlyRow[] hourlyTable;

			// Token: 0x040001EE RID: 494
			private Dictionary<string, ExportMobileSyncLog.ServerRow> serverTable = new Dictionary<string, ExportMobileSyncLog.ServerRow>();

			// Token: 0x040001EF RID: 495
			private Dictionary<string, ExportMobileSyncLog.StatusRow> statusTable = new Dictionary<string, ExportMobileSyncLog.StatusRow>();

			// Token: 0x040001F0 RID: 496
			private Dictionary<string, ExportMobileSyncLog.UserAgentRow> userAgentTable = new Dictionary<string, ExportMobileSyncLog.UserAgentRow>();

			// Token: 0x040001F1 RID: 497
			private Regex fieldsPattern;

			// Token: 0x040001F2 RID: 498
			private Regex valuesPattern;

			// Token: 0x040001F3 RID: 499
			private Regex queryPattern;

			// Token: 0x040001F4 RID: 500
			private int columnCount;

			// Token: 0x040001F5 RID: 501
			private ExportMobileSyncLog.ColumnID[] columnIndexToID;

			// Token: 0x040001F6 RID: 502
			private Dictionary<string, ExportMobileSyncLog.ColumnID> columnNameToID;

			// Token: 0x040001F7 RID: 503
			private string[] columnValues;

			// Token: 0x040001F8 RID: 504
			private string[] policyComplianceStrings = new string[]
			{
				"Unknown",
				"Compliant",
				"Partially Compliant",
				"Not Compliant"
			};
		}
	}
}
