using System;
using System.Collections;
using System.IO;
using Microsoft.Exchange.Diagnostics.Components.StsUpdate;
using Microsoft.Exchange.Transport.Agent.AntiSpam.Common;
using Microsoft.Exchange.Transport.Agent.ProtocolAnalysis.DbAccess;

namespace Microsoft.Exchange.Transport.Agent.ProtocolAnalysis.Update
{
	// Token: 0x02000066 RID: 102
	internal class VerifyContent
	{
		// Token: 0x060002C5 RID: 709 RVA: 0x0001280B File Offset: 0x00010A0B
		public VerifyContent()
		{
			this.recordsArray = new ArrayList();
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x0001281E File Offset: 0x00010A1E
		public bool IsFullDownload
		{
			get
			{
				return this.fileHeader.FileType == VerifyContent.UpdateFileType.FullUpdate;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x0001282E File Offset: 0x00010A2E
		public string MajorVersion
		{
			get
			{
				return this.fileHeader.MajorVersion;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x0001283B File Offset: 0x00010A3B
		public string MinorVersion
		{
			get
			{
				return this.fileHeader.MinorVersion;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x00012848 File Offset: 0x00010A48
		public DateTime ExpirationTime
		{
			get
			{
				return this.fileHeader.ExpTime;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060002CA RID: 714 RVA: 0x00012855 File Offset: 0x00010A55
		public ArrayList SenderRecords
		{
			get
			{
				return this.recordsArray;
			}
		}

		// Token: 0x060002CB RID: 715 RVA: 0x00012860 File Offset: 0x00010A60
		public bool Parse(byte[] data)
		{
			if (StsUpdateAgent.EndProcessing)
			{
				return true;
			}
			if (data == null)
			{
				ExTraceGlobals.OnDownloadTracer.TraceError((long)this.GetHashCode(), "Empty data bytes in VerifyContent.Parse");
				return false;
			}
			MemoryStream memoryStream = null;
			BinaryReader binaryReader = null;
			try
			{
				memoryStream = new MemoryStream(data);
				binaryReader = new BinaryReader(memoryStream);
				if (binaryReader.PeekChar() != -1)
				{
					VerifyContent.UpdateFileType updateFileType = (VerifyContent.UpdateFileType)binaryReader.ReadByte();
					if (!VerifyContent.ValidateUpdateFileType(updateFileType))
					{
						ExTraceGlobals.OnDownloadTracer.TraceError((long)this.GetHashCode(), "VerifyContent.Parse: corrupt data file.");
						StsUpdateAgent.EventLogger.LogEvent(AgentsEventLogConstants.Tuple_UpdateAgentFileNotLoaded, null, null);
						return false;
					}
					string text = binaryReader.ReadString();
					string text2 = binaryReader.ReadString();
					int num = binaryReader.ReadInt32();
					int nRecords = binaryReader.ReadInt32();
					if (StsUpdateAgent.CompareAndSetCurrentVersion(text, text2) == StsUpdateAgent.VersionComparison.Eq)
					{
						return false;
					}
					ExTraceGlobals.OnDownloadTracer.TraceDebug((long)this.GetHashCode(), "Download file header, type:{0}, majorVer:{1}, minorVer:{2}, expTime:{3}", new object[]
					{
						updateFileType,
						text,
						text2,
						num
					});
					this.fileHeader = new VerifyContent.Header(updateFileType, text, text2, num);
					if (this.ParseRecords(binaryReader, nRecords) && !StsUpdateAgent.EndProcessing)
					{
						this.StoreRecords();
					}
				}
			}
			catch (EndOfStreamException ex)
			{
				ExTraceGlobals.OnDownloadTracer.TraceError<string>((long)this.GetHashCode(), "EndOfStreamException in Parse, error: {0}", ex.Message);
			}
			catch (IOException ex2)
			{
				ExTraceGlobals.OnDownloadTracer.TraceError<string>((long)this.GetHashCode(), "IOException in Parse, error: {0}", ex2.Message);
			}
			finally
			{
				if (binaryReader != null)
				{
					binaryReader.Close();
				}
				if (memoryStream != null)
				{
					memoryStream.Close();
				}
			}
			return true;
		}

		// Token: 0x060002CC RID: 716 RVA: 0x00012A34 File Offset: 0x00010C34
		private void StoreRecords()
		{
			Database.TruncateSenderReputationTable(ExTraceGlobals.DatabaseTracer);
			foreach (object obj in this.SenderRecords)
			{
				VerifyContent.SenderReputationRecord senderReputationRecord = (VerifyContent.SenderReputationRecord)obj;
				if (StsUpdateAgent.EndProcessing)
				{
					return;
				}
				switch (senderReputationRecord.RecordType)
				{
				case VerifyContent.UpdateRecordType.Add:
				case VerifyContent.UpdateRecordType.Update:
					Database.UpdateSenderReputationData(senderReputationRecord.SenderAddrHash, senderReputationRecord.Srl, senderReputationRecord.IsOpenProxy, this.ExpirationTime, ExTraceGlobals.DatabaseTracer);
					break;
				case VerifyContent.UpdateRecordType.Delete:
					Database.DeleteSenderReputationData(senderReputationRecord.SenderAddrHash, ExTraceGlobals.DatabaseTracer);
					break;
				}
			}
			Database.UpdateConfiguration("majorversion", this.MajorVersion, ExTraceGlobals.DatabaseTracer);
			Database.UpdateConfiguration("minorversion", this.MinorVersion, ExTraceGlobals.DatabaseTracer);
			StsUpdateAgent.PerformanceCounters.TotalUpdate();
		}

		// Token: 0x060002CD RID: 717 RVA: 0x00012B1C File Offset: 0x00010D1C
		private bool ParseRecords(BinaryReader reader, int nRecords)
		{
			if (reader == null)
			{
				return false;
			}
			bool result;
			try
			{
				int num = 0;
				while (num < nRecords && !StsUpdateAgent.EndProcessing)
				{
					int num2 = 0;
					bool proxy = false;
					int num3 = (int)reader.ReadByte();
					if (num3 < 10)
					{
						ExTraceGlobals.OnDownloadTracer.TraceError<int>((long)this.GetHashCode(), "Invalid record length: {0}", num3);
						return false;
					}
					byte[] senderAddrHash = reader.ReadBytes(CMD5.HashSize());
					byte b = reader.ReadByte();
					VerifyContent.UpdateRecordType updateRecordType = (VerifyContent.UpdateRecordType)((b & 224) >> 5);
					if (!VerifyContent.ValidateUpdateRecordType(updateRecordType))
					{
						ExTraceGlobals.OnDownloadTracer.TraceError<int>((long)this.GetHashCode(), "Invalid record type: {0}", (b & 224) >> 5);
					}
					else
					{
						if (updateRecordType != VerifyContent.UpdateRecordType.Delete)
						{
							proxy = ((b & 1) == 1);
							num2 = (b & 30) >> 1;
							if (num2 == 15)
							{
								num2 = -1;
							}
							if (num2 < -1 || num2 > 9)
							{
								ExTraceGlobals.OnDownloadTracer.TraceError<int>((long)this.GetHashCode(), "Invalid srl value: {0}", num2);
								return false;
							}
						}
						if (this.IsFullDownload && updateRecordType != VerifyContent.UpdateRecordType.Add)
						{
							ExTraceGlobals.OnDownloadTracer.TraceError<int>((long)this.GetHashCode(), "Received record type: {0} in full download", num3);
							return false;
						}
						VerifyContent.SenderReputationRecord senderReputationRecord = null;
						if (StsUpdateAgent.EndProcessing)
						{
							return true;
						}
						switch (updateRecordType)
						{
						case VerifyContent.UpdateRecordType.Add:
						case VerifyContent.UpdateRecordType.Update:
							senderReputationRecord = new VerifyContent.SenderReputationRecord(updateRecordType, senderAddrHash, num2, proxy);
							break;
						case VerifyContent.UpdateRecordType.Delete:
							senderReputationRecord = new VerifyContent.SenderReputationRecord(senderAddrHash);
							break;
						}
						if (senderReputationRecord != null)
						{
							this.recordsArray.Add(senderReputationRecord);
						}
					}
					reader.ReadBytes(num3 - 1 - CMD5.HashSize() - 1);
					num++;
				}
				if (reader.PeekChar() != -1)
				{
					result = false;
				}
				else
				{
					result = true;
				}
			}
			catch (EndOfStreamException ex)
			{
				ExTraceGlobals.OnDownloadTracer.TraceError<string>((long)this.GetHashCode(), "EndOfStreamException in ParseRecords, error: {0}", ex.Message);
				result = false;
			}
			catch (IOException ex2)
			{
				ExTraceGlobals.OnDownloadTracer.TraceError<string>((long)this.GetHashCode(), "IOException in Parse, error: {0}", ex2.Message);
				result = false;
			}
			return result;
		}

		// Token: 0x060002CE RID: 718 RVA: 0x00012D34 File Offset: 0x00010F34
		private static bool ValidateUpdateFileType(VerifyContent.UpdateFileType updateFileType)
		{
			return updateFileType >= VerifyContent.UpdateFileType.Incremental && updateFileType <= VerifyContent.UpdateFileType.FullUpdate;
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00012D43 File Offset: 0x00010F43
		private static bool ValidateUpdateRecordType(VerifyContent.UpdateRecordType updateRecordType)
		{
			return updateRecordType >= VerifyContent.UpdateRecordType.Add && updateRecordType <= VerifyContent.UpdateRecordType.Update;
		}

		// Token: 0x04000248 RID: 584
		private VerifyContent.Header fileHeader;

		// Token: 0x04000249 RID: 585
		private ArrayList recordsArray;

		// Token: 0x02000067 RID: 103
		private struct Header
		{
			// Token: 0x060002D0 RID: 720 RVA: 0x00012D54 File Offset: 0x00010F54
			public Header(VerifyContent.UpdateFileType fileType, string majorVersion, string minorVersion, int expTimeInMinutes)
			{
				this.FileType = fileType;
				this.MajorVersion = majorVersion;
				this.MinorVersion = minorVersion;
				this.ExpTime = DateTime.UtcNow.AddMinutes((double)expTimeInMinutes);
			}

			// Token: 0x0400024A RID: 586
			public VerifyContent.UpdateFileType FileType;

			// Token: 0x0400024B RID: 587
			public string MajorVersion;

			// Token: 0x0400024C RID: 588
			public string MinorVersion;

			// Token: 0x0400024D RID: 589
			public DateTime ExpTime;
		}

		// Token: 0x02000068 RID: 104
		private class SenderReputationRecord
		{
			// Token: 0x060002D1 RID: 721 RVA: 0x00012D8C File Offset: 0x00010F8C
			public SenderReputationRecord(VerifyContent.UpdateRecordType recType, byte[] senderAddrHash, int srl, bool proxy)
			{
				this.recType = recType;
				this.srl = srl;
				this.proxy = proxy;
				this.senderAddrHash = senderAddrHash;
			}

			// Token: 0x060002D2 RID: 722 RVA: 0x00012DB1 File Offset: 0x00010FB1
			public SenderReputationRecord(byte[] senderAddrHash)
			{
				this.recType = VerifyContent.UpdateRecordType.Delete;
				this.senderAddrHash = senderAddrHash;
			}

			// Token: 0x17000076 RID: 118
			// (get) Token: 0x060002D3 RID: 723 RVA: 0x00012DC7 File Offset: 0x00010FC7
			public byte[] SenderAddrHash
			{
				get
				{
					return this.senderAddrHash;
				}
			}

			// Token: 0x17000077 RID: 119
			// (get) Token: 0x060002D4 RID: 724 RVA: 0x00012DCF File Offset: 0x00010FCF
			public VerifyContent.UpdateRecordType RecordType
			{
				get
				{
					return this.recType;
				}
			}

			// Token: 0x17000078 RID: 120
			// (get) Token: 0x060002D5 RID: 725 RVA: 0x00012DD7 File Offset: 0x00010FD7
			public bool IsOpenProxy
			{
				get
				{
					return this.proxy;
				}
			}

			// Token: 0x17000079 RID: 121
			// (get) Token: 0x060002D6 RID: 726 RVA: 0x00012DDF File Offset: 0x00010FDF
			public int Srl
			{
				get
				{
					return this.srl;
				}
			}

			// Token: 0x0400024E RID: 590
			private VerifyContent.UpdateRecordType recType;

			// Token: 0x0400024F RID: 591
			private byte[] senderAddrHash;

			// Token: 0x04000250 RID: 592
			private int srl;

			// Token: 0x04000251 RID: 593
			private bool proxy;
		}

		// Token: 0x02000069 RID: 105
		private enum UpdateFileType : byte
		{
			// Token: 0x04000253 RID: 595
			Incremental,
			// Token: 0x04000254 RID: 596
			FullUpdate
		}

		// Token: 0x0200006A RID: 106
		private enum UpdateRecordType : byte
		{
			// Token: 0x04000256 RID: 598
			Add = 1,
			// Token: 0x04000257 RID: 599
			Delete,
			// Token: 0x04000258 RID: 600
			Update
		}
	}
}
