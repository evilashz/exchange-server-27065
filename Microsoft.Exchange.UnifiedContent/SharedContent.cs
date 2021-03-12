using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Security.Cryptography;

namespace Microsoft.Exchange.UnifiedContent
{
	// Token: 0x02000009 RID: 9
	internal class SharedContent : IExtractedContent, IRawContent
	{
		// Token: 0x06000033 RID: 51 RVA: 0x00002944 File Offset: 0x00000B44
		private SharedContent(Stream sharedStream)
		{
			this.NeedsUpdate = true;
			this.sharedStream = sharedStream;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x0000296C File Offset: 0x00000B6C
		private SharedContent(Stream sharedStream, Stream dataStream)
		{
			this.NeedsUpdate = true;
			this.sharedStream = sharedStream;
			long value = 12L + SharedContentWriter.ComputeLength(dataStream);
			this.rawDataEntryPosition = sharedStream.Length;
			sharedStream.Position = this.rawDataEntryPosition;
			using (SharedContentWriter sharedContentWriter = new SharedContentWriter(sharedStream))
			{
				sharedContentWriter.Write(value);
				sharedContentWriter.Write(286331153U);
				sharedContentWriter.Write(0UL);
				this.rawDataPosition = sharedStream.Position + 8L;
				sharedContentWriter.Write(dataStream);
				this.rawSize = sharedStream.Position - this.rawDataPosition;
				this.ComputeHashes(this.rawDataPosition);
				sharedContentWriter.ValidateAtEndOfEntry();
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002A3C File Offset: 0x00000C3C
		// (set) Token: 0x06000036 RID: 54 RVA: 0x00002A4A File Offset: 0x00000C4A
		public string FileName
		{
			get
			{
				this.Update();
				return this.fileName;
			}
			set
			{
				this.fileName = value;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002A53 File Offset: 0x00000C53
		public long Rawsize
		{
			get
			{
				return this.rawSize;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002A5B File Offset: 0x00000C5B
		public string RawFileName
		{
			get
			{
				return this.rawFileName;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002A63 File Offset: 0x00000C63
		// (set) Token: 0x0600003A RID: 58 RVA: 0x00002A6B File Offset: 0x00000C6B
		public bool NeedsUpdate { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002A74 File Offset: 0x00000C74
		public TextExtractionStatus TextExtractionStatus
		{
			get
			{
				this.Update();
				return this.textExtractionStatus;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002A82 File Offset: 0x00000C82
		public int RefId
		{
			get
			{
				this.Update();
				return this.refId;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002A90 File Offset: 0x00000C90
		public Dictionary<string, object> Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002A98 File Offset: 0x00000C98
		public byte[] LowFidelityHash
		{
			get
			{
				return this.lowFidelitySHA256Hash;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002AA0 File Offset: 0x00000CA0
		public byte[] HighFidelityHash
		{
			get
			{
				if (this.highFidelitySHA256Hash == null)
				{
					this.highFidelitySHA256Hash = this.CalculateHighFidelityHash();
				}
				return this.highFidelitySHA256Hash;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002ABC File Offset: 0x00000CBC
		public long StreamOffset
		{
			get
			{
				return this.rawDataPosition + 24L;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002AC8 File Offset: 0x00000CC8
		internal long EntryPosition
		{
			get
			{
				return this.entryPosition;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002AD0 File Offset: 0x00000CD0
		internal long RawDataPosition
		{
			get
			{
				return this.rawDataPosition;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002AD8 File Offset: 0x00000CD8
		internal long RawDataEntryPosition
		{
			get
			{
				return this.rawDataEntryPosition;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002AE0 File Offset: 0x00000CE0
		// (set) Token: 0x06000045 RID: 69 RVA: 0x00002AE8 File Offset: 0x00000CE8
		internal uint Crc32Hash
		{
			get
			{
				return this.crc32Hash;
			}
			set
			{
				this.crc32Hash = value;
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002AF1 File Offset: 0x00000CF1
		public Stream GetContentReadStream()
		{
			this.Update();
			if (this.textStream != null)
			{
				this.textStream.Position = 0L;
			}
			return this.textStream;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002B14 File Offset: 0x00000D14
		public bool IsModified(Stream rawStream)
		{
			uint hash = Crc32.ComputeHash(rawStream, 0L);
			return this.IsModified(hash);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002B31 File Offset: 0x00000D31
		public bool IsModified(uint hash)
		{
			return hash != this.crc32Hash;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002B40 File Offset: 0x00000D40
		public IList<IExtractedContent> GetChildren()
		{
			this.Update();
			if (this.children == null && !this.NeedsUpdate)
			{
				this.children = new List<IExtractedContent>();
				if (this.firstChildEntryPos != 0L)
				{
					SharedContent sharedContent = SharedContent.Open(this.sharedStream, this.firstChildEntryPos, this);
					this.children.Add(sharedContent);
					while (sharedContent.nextSiblingEntryPos != 0L)
					{
						sharedContent = SharedContent.Open(this.sharedStream, sharedContent.nextSiblingEntryPos, this);
						this.children.Add(sharedContent);
					}
				}
			}
			return this.children;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002BC8 File Offset: 0x00000DC8
		internal static SharedContent Create(SharedStream sharedStream, Stream contentStream, string contentName = null)
		{
			return new SharedContent(sharedStream, contentStream)
			{
				rawFileName = contentName
			};
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002BE8 File Offset: 0x00000DE8
		internal static SharedContent Open(Stream sharedStream, long entryPosition, SharedContent parent)
		{
			SharedContent sharedContent = new SharedContent(sharedStream);
			sharedContent.entryPosition = entryPosition;
			sharedContent.parent = parent;
			sharedContent.Update();
			return sharedContent;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002C14 File Offset: 0x00000E14
		internal static Stream OpenEntryStream(Stream sharedStream, long entryPosition)
		{
			long length = new BinaryReader(sharedStream)
			{
				BaseStream = 
				{
					Position = entryPosition
				}
			}.ReadInt64();
			return new StreamOnStream(sharedStream, entryPosition + 8L, length);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002C48 File Offset: 0x00000E48
		private void Update()
		{
			if (this.NeedsUpdate)
			{
				if (this.entryPosition == 0L)
				{
					RawDataEntry rawDataEntry = new RawDataEntry(SharedContent.OpenEntryStream(this.sharedStream, this.rawDataEntryPosition), this.rawDataEntryPosition);
					this.entryPosition = rawDataEntry.ExtractedContentEntryPosition;
				}
				if (this.entryPosition != 0L)
				{
					this.NeedsUpdate = false;
					ExtractedContentEntry extractedContentEntry = new ExtractedContentEntry(SharedContent.OpenEntryStream(this.sharedStream, this.entryPosition), this.entryPosition);
					this.parentEntryPos = extractedContentEntry.ParentPos;
					if (this.parent == null)
					{
						if (extractedContentEntry.ParentPos != 0L)
						{
							throw new InvalidDataException();
						}
					}
					else if (this.parent.entryPosition != this.parentEntryPos)
					{
						throw new InvalidDataException();
					}
					this.firstChildEntryPos = extractedContentEntry.FirstChildPos;
					this.nextSiblingEntryPos = extractedContentEntry.NextSiblingPos;
					this.fileName = extractedContentEntry.FileName;
					this.textExtractionStatus = (TextExtractionStatus)extractedContentEntry.TextExtractionStatus;
					this.textStream = extractedContentEntry.TextStream;
					this.refId = (int)extractedContentEntry.RefId;
					foreach (KeyValuePair<string, object> keyValuePair in extractedContentEntry.Properties)
					{
						this.properties[keyValuePair.Key] = keyValuePair.Value;
					}
				}
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002DA4 File Offset: 0x00000FA4
		private void ComputeHashes(long start = 0L)
		{
			byte[] array = new byte[16384];
			long position = -1L;
			if (this.sharedStream.CanSeek)
			{
				position = this.sharedStream.Position;
				this.sharedStream.Position = start;
			}
			int num;
			if ((num = this.sharedStream.Read(array, 0, 16384)) > 0)
			{
				this.crc32Hash = Crc32.ComputeHash(array, num);
				using (SHA256CryptoServiceProvider sha256CryptoServiceProvider = new SHA256CryptoServiceProvider())
				{
					this.lowFidelitySHA256Hash = sha256CryptoServiceProvider.ComputeHash(array, 0, Math.Min(num, 4096));
				}
			}
			if (this.sharedStream.CanSeek)
			{
				this.sharedStream.Position = position;
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002E5C File Offset: 0x0000105C
		private byte[] CalculateHighFidelityHash()
		{
			if (this.rawSize <= 4096L)
			{
				return this.lowFidelitySHA256Hash;
			}
			RawDataEntry rawDataEntry = new RawDataEntry(SharedContent.OpenEntryStream(this.sharedStream, this.rawDataEntryPosition), this.rawDataEntryPosition);
			byte[] result;
			using (SHA256CryptoServiceProvider sha256CryptoServiceProvider = new SHA256CryptoServiceProvider())
			{
				result = sha256CryptoServiceProvider.ComputeHash(rawDataEntry.DataStream);
			}
			return result;
		}

		// Token: 0x0400001B RID: 27
		private const uint EntryId = 286331153U;

		// Token: 0x0400001C RID: 28
		private const int MaxBytesToCalculateCrc32Hash = 16384;

		// Token: 0x0400001D RID: 29
		private const int MaxBytesToCalculateLowFidelitySHA256Hash = 4096;

		// Token: 0x0400001E RID: 30
		private const int MaxBufferLengthToCalculateHash = 16384;

		// Token: 0x0400001F RID: 31
		private readonly long rawDataEntryPosition;

		// Token: 0x04000020 RID: 32
		private readonly long rawDataPosition;

		// Token: 0x04000021 RID: 33
		private readonly long rawSize;

		// Token: 0x04000022 RID: 34
		private readonly Stream sharedStream;

		// Token: 0x04000023 RID: 35
		private readonly Dictionary<string, object> properties = new Dictionary<string, object>();

		// Token: 0x04000024 RID: 36
		private long entryPosition;

		// Token: 0x04000025 RID: 37
		private Stream textStream;

		// Token: 0x04000026 RID: 38
		private string fileName;

		// Token: 0x04000027 RID: 39
		private IList<IExtractedContent> children;

		// Token: 0x04000028 RID: 40
		private SharedContent parent;

		// Token: 0x04000029 RID: 41
		private long parentEntryPos;

		// Token: 0x0400002A RID: 42
		private long nextSiblingEntryPos;

		// Token: 0x0400002B RID: 43
		private long firstChildEntryPos;

		// Token: 0x0400002C RID: 44
		private TextExtractionStatus textExtractionStatus;

		// Token: 0x0400002D RID: 45
		private string rawFileName;

		// Token: 0x0400002E RID: 46
		private uint crc32Hash;

		// Token: 0x0400002F RID: 47
		private byte[] lowFidelitySHA256Hash;

		// Token: 0x04000030 RID: 48
		private byte[] highFidelitySHA256Hash;

		// Token: 0x04000031 RID: 49
		private int refId = -1;
	}
}
