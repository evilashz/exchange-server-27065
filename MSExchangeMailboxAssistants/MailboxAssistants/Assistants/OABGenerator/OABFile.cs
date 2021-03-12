using System;
using System.Diagnostics;
using System.IO;
using Microsoft.Exchange.OAB;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.OABGenerator
{
	// Token: 0x020001E4 RID: 484
	internal class OABFile
	{
		// Token: 0x060012CD RID: 4813 RVA: 0x0006DE7B File Offset: 0x0006C07B
		public OABFile(FileStream uncompressedFileStream, OABDataFileType oabDataFileType)
		{
			this.uncompressedFileStream = uncompressedFileStream;
			this.oabDataFileType = oabDataFileType;
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x060012CE RID: 4814 RVA: 0x0006DE98 File Offset: 0x0006C098
		// (set) Token: 0x060012CF RID: 4815 RVA: 0x0006DEA0 File Offset: 0x0006C0A0
		public bool IsContinuationOfSequence
		{
			get
			{
				return this.isContinuationOfSequence;
			}
			set
			{
				this.isContinuationOfSequence = value;
			}
		}

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x060012D0 RID: 4816 RVA: 0x0006DEA9 File Offset: 0x0006C0A9
		// (set) Token: 0x060012D1 RID: 4817 RVA: 0x0006DEB1 File Offset: 0x0006C0B1
		public FileStream UncompressedFileStream
		{
			get
			{
				return this.uncompressedFileStream;
			}
			set
			{
				this.uncompressedFileStream = value;
			}
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x060012D2 RID: 4818 RVA: 0x0006DEBA File Offset: 0x0006C0BA
		public FileStream CompressedFileStream
		{
			get
			{
				return this.compressedFileStream;
			}
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x060012D3 RID: 4819 RVA: 0x0006DEC2 File Offset: 0x0006C0C2
		// (set) Token: 0x060012D4 RID: 4820 RVA: 0x0006DECA File Offset: 0x0006C0CA
		public string CompressedFileHash
		{
			get
			{
				return this.compressedFileHash;
			}
			set
			{
				this.compressedFileHash = value;
			}
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x060012D5 RID: 4821 RVA: 0x0006DED3 File Offset: 0x0006C0D3
		// (set) Token: 0x060012D6 RID: 4822 RVA: 0x0006DEDB File Offset: 0x0006C0DB
		public string NameToUseInOABFile
		{
			get
			{
				return this.nameToUseInOABFile;
			}
			set
			{
				this.nameToUseInOABFile = value;
			}
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x060012D7 RID: 4823 RVA: 0x0006DEE4 File Offset: 0x0006C0E4
		// (set) Token: 0x060012D8 RID: 4824 RVA: 0x0006DEEC File Offset: 0x0006C0EC
		public string DnToUseInOABFile
		{
			get
			{
				return this.dnToUseInOABFile;
			}
			set
			{
				this.dnToUseInOABFile = value;
			}
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x060012D9 RID: 4825 RVA: 0x0006DEF5 File Offset: 0x0006C0F5
		// (set) Token: 0x060012DA RID: 4826 RVA: 0x0006DEFD File Offset: 0x0006C0FD
		public string DnOfTheHabRoot
		{
			get
			{
				return this.dnOfTheHabRoot;
			}
			set
			{
				this.dnOfTheHabRoot = value;
			}
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x060012DB RID: 4827 RVA: 0x0006DF06 File Offset: 0x0006C106
		// (set) Token: 0x060012DC RID: 4828 RVA: 0x0006DF0E File Offset: 0x0006C10E
		public Guid Guid
		{
			get
			{
				return this.guid;
			}
			set
			{
				this.guid = value;
			}
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x060012DD RID: 4829 RVA: 0x0006DF17 File Offset: 0x0006C117
		// (set) Token: 0x060012DE RID: 4830 RVA: 0x0006DF1F File Offset: 0x0006C11F
		public uint SequenceNumber
		{
			get
			{
				return this.sequenceNumber;
			}
			set
			{
				this.sequenceNumber = value;
			}
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x060012DF RID: 4831 RVA: 0x0006DF28 File Offset: 0x0006C128
		// (set) Token: 0x060012E0 RID: 4832 RVA: 0x0006DFF0 File Offset: 0x0006C1F0
		public string PublishedFileName
		{
			get
			{
				if (this.publishedFileName != null)
				{
					return this.publishedFileName;
				}
				if (this.oabDataFileType == OABDataFileType.Full || this.oabDataFileType == OABDataFileType.Diff)
				{
					return string.Format("{0}-{1}-{2}.lzx", this.Guid.ToString(), (this.oabDataFileType == OABDataFileType.Full) ? "data" : "binpatch", this.sequenceNumber);
				}
				return string.Format("{0}-{1}{2:x4}-{3}.lzx", new object[]
				{
					this.guid.ToString(),
					(this.oabDataFileType == OABDataFileType.TemplateWin) ? "lng" : "mac",
					this.lcid,
					this.sequenceNumber
				});
			}
			set
			{
				this.publishedFileName = value;
			}
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x060012E1 RID: 4833 RVA: 0x0006DFF9 File Offset: 0x0006C1F9
		// (set) Token: 0x060012E2 RID: 4834 RVA: 0x0006E001 File Offset: 0x0006C201
		public OABDataFileType OABDataFileType
		{
			get
			{
				return this.oabDataFileType;
			}
			set
			{
				this.oabDataFileType = value;
			}
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x060012E3 RID: 4835 RVA: 0x0006E00A File Offset: 0x0006C20A
		// (set) Token: 0x060012E4 RID: 4836 RVA: 0x0006E012 File Offset: 0x0006C212
		public uint CompressedFileSize
		{
			get
			{
				return this.compressedFileSize;
			}
			set
			{
				this.compressedFileSize = value;
			}
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x060012E5 RID: 4837 RVA: 0x0006E01B File Offset: 0x0006C21B
		// (set) Token: 0x060012E6 RID: 4838 RVA: 0x0006E023 File Offset: 0x0006C223
		public uint UncompressedFileSize
		{
			get
			{
				return this.uncompressedFileSize;
			}
			set
			{
				this.uncompressedFileSize = value;
			}
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x060012E7 RID: 4839 RVA: 0x0006E02C File Offset: 0x0006C22C
		// (set) Token: 0x060012E8 RID: 4840 RVA: 0x0006E034 File Offset: 0x0006C234
		public int Lcid
		{
			get
			{
				return this.lcid;
			}
			set
			{
				this.lcid = value;
			}
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x060012E9 RID: 4841 RVA: 0x0006E03D File Offset: 0x0006C23D
		public TimeSpan IODuration
		{
			get
			{
				return this.ioDuration;
			}
		}

		// Token: 0x060012EA RID: 4842 RVA: 0x0006E048 File Offset: 0x0006C248
		public bool IsPublished(string oabDirectory)
		{
			FileInfo fileInfo = new FileInfo(Path.Combine(oabDirectory, this.PublishedFileName));
			if (fileInfo.Exists && fileInfo.Length == (long)((ulong)this.CompressedFileSize))
			{
				using (Stream stream = File.Open(fileInfo.FullName, FileMode.Open, FileAccess.Read))
				{
					if (this.CompressedFileHash == OABFileHash.GetHash(stream))
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x060012EB RID: 4843 RVA: 0x0006E0C4 File Offset: 0x0006C2C4
		public void Compress(FileSet fileSet, GenerationStats stats, string marker)
		{
			this.compressedFileStream = fileSet.Create("LZX");
			long length = this.uncompressedFileStream.Length;
			ulong num = (ulong)-1;
			this.uncompressedFileSize = (uint)this.uncompressedFileStream.Length;
			this.uncompressedFileStream.Seek(0L, SeekOrigin.Begin);
			Stopwatch stopwatch = Stopwatch.StartNew();
			byte[] array = new byte[Globals.MaxCompressionBlockSize];
			using (IOCostStream iocostStream = new IOCostStream(new NoCloseStream(this.uncompressedFileStream)))
			{
				using (new FileSystemPerformanceTracker(marker, iocostStream, stats))
				{
					using (IOCostStream iocostStream2 = new IOCostStream(new OABCompressStream(this.compressedFileStream, Globals.MaxCompressionBlockSize)))
					{
						using (new FileSystemPerformanceTracker(marker, iocostStream2, stats))
						{
							for (;;)
							{
								int num2 = iocostStream.Read(array, 0, array.Length);
								if (num2 == 0)
								{
									break;
								}
								iocostStream2.Write(array, 0, num2);
							}
						}
					}
				}
			}
			stopwatch.Stop();
			this.ioDuration += stopwatch.Elapsed;
			this.compressedFileSize = (uint)this.compressedFileStream.Length;
			this.compressedFileHash = OABFileHash.GetHash(this.compressedFileStream);
		}

		// Token: 0x04000B61 RID: 2913
		private bool isContinuationOfSequence;

		// Token: 0x04000B62 RID: 2914
		private FileStream uncompressedFileStream;

		// Token: 0x04000B63 RID: 2915
		private FileStream compressedFileStream;

		// Token: 0x04000B64 RID: 2916
		private string compressedFileHash;

		// Token: 0x04000B65 RID: 2917
		private string nameToUseInOABFile;

		// Token: 0x04000B66 RID: 2918
		private string dnToUseInOABFile;

		// Token: 0x04000B67 RID: 2919
		private string dnOfTheHabRoot;

		// Token: 0x04000B68 RID: 2920
		private Guid guid;

		// Token: 0x04000B69 RID: 2921
		private int lcid;

		// Token: 0x04000B6A RID: 2922
		private OABDataFileType oabDataFileType;

		// Token: 0x04000B6B RID: 2923
		private uint compressedFileSize;

		// Token: 0x04000B6C RID: 2924
		private uint uncompressedFileSize;

		// Token: 0x04000B6D RID: 2925
		private uint sequenceNumber = 1U;

		// Token: 0x04000B6E RID: 2926
		private TimeSpan ioDuration;

		// Token: 0x04000B6F RID: 2927
		private string publishedFileName;
	}
}
