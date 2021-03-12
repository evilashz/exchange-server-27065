using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.MailboxRules
{
	// Token: 0x02000BE9 RID: 3049
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class OofHistoryReader
	{
		// Token: 0x17001D62 RID: 7522
		// (get) Token: 0x06006C38 RID: 27704 RVA: 0x001D0751 File Offset: 0x001CE951
		public int EntryCount
		{
			get
			{
				this.ThrowIfNotInitialized();
				return this.entryCount;
			}
		}

		// Token: 0x17001D63 RID: 7523
		// (get) Token: 0x06006C39 RID: 27705 RVA: 0x001D075F File Offset: 0x001CE95F
		public bool HasMoreEntries
		{
			get
			{
				this.ThrowIfNotInitialized();
				return this.entriesRead < this.entryCount;
			}
		}

		// Token: 0x17001D64 RID: 7524
		// (get) Token: 0x06006C3A RID: 27706 RVA: 0x001D0775 File Offset: 0x001CE975
		public IList<byte> CurrentEntryAddressBytes
		{
			get
			{
				this.ThrowIfNotInitialized();
				if (this.currentEntryAddressBytes == null)
				{
					throw new InvalidOperationException("Read an entry first.");
				}
				return this.currentEntryAddressBytes;
			}
		}

		// Token: 0x17001D65 RID: 7525
		// (get) Token: 0x06006C3B RID: 27707 RVA: 0x001D0796 File Offset: 0x001CE996
		public IList<byte> CurrentEntryRuleIdBytes
		{
			get
			{
				this.ThrowIfNotInitialized();
				if (this.currentEntryRuleIdBytes == null)
				{
					throw new InvalidOperationException("Read an entry first.");
				}
				return this.currentEntryRuleIdBytes;
			}
		}

		// Token: 0x06006C3D RID: 27709 RVA: 0x001D07C0 File Offset: 0x001CE9C0
		public void Initialize(Stream oofHistoryStream)
		{
			this.oofHistoryStream = oofHistoryStream;
			if (this.data != null)
			{
				throw new InvalidOperationException("Initialize can only be called once.");
			}
			this.data = new byte[32768];
			this.dataLength = 0;
			this.dataPosition = 0;
			this.LoadBuffer();
			this.ReadHeader();
			this.initialized = true;
		}

		// Token: 0x06006C3E RID: 27710 RVA: 0x001D0818 File Offset: 0x001CEA18
		public void ReadEntry()
		{
			this.ThrowIfNotInitialized();
			if (!this.HasMoreEntries)
			{
				throw new InvalidOperationException("There are no more entries to read.");
			}
			if (this.currentEntryAddressBytes == null)
			{
				this.currentEntryAddressBytes = new VirtualList<byte>(true);
			}
			if (this.currentEntryRuleIdBytes == null)
			{
				this.currentEntryRuleIdBytes = new VirtualList<byte>(true);
			}
			if (!this.EnsureDataSufficiency(1))
			{
				throw new OofHistoryCorruptionException(string.Concat(new object[]
				{
					"Insufficient data in stream to read property count for entry ",
					this.entriesRead,
					" at buffer position ",
					this.dataPosition
				}));
			}
			byte b = this.data[this.dataPosition++];
			for (byte b2 = 0; b2 < b; b2 += 1)
			{
				if (!this.EnsureDataSufficiency(3))
				{
					throw new OofHistoryCorruptionException(string.Concat(new object[]
					{
						"Insufficient data in stream to read property id and size. Current entry ",
						this.entriesRead,
						", current property: ",
						b2,
						", total entries: ",
						this.entryCount
					}));
				}
				OofHistory.PropId propId = (OofHistory.PropId)this.data[this.dataPosition];
				ushort num = BitConverter.ToUInt16(this.data, this.dataPosition + 1);
				if (num > 1000)
				{
					throw new OofHistoryCorruptionException(string.Concat(new object[]
					{
						"Current entry: ",
						this.entriesRead,
						"Property with id ",
						propId,
						" has property size: ",
						num,
						", which is over the maximum allowed size of ",
						1000
					}));
				}
				this.dataPosition += 3;
				if (!this.EnsureDataSufficiency((int)num))
				{
					throw new OofHistoryCorruptionException(string.Concat(new object[]
					{
						"Insufficient data in stream to match property values.  Current entry: ",
						this.entriesRead,
						", property with id ",
						this.data[this.dataPosition],
						" has property size: ",
						num
					}));
				}
				switch (propId)
				{
				case OofHistory.PropId.SenderAddress:
					this.currentEntryAddressBytes.SetRange(this.data, this.dataPosition, (int)num);
					break;
				case OofHistory.PropId.GlobalRuleId:
					this.currentEntryRuleIdBytes.SetRange(this.data, this.dataPosition, (int)num);
					break;
				}
				this.dataPosition += (int)num;
			}
			this.entriesRead++;
		}

		// Token: 0x06006C3F RID: 27711 RVA: 0x001D0AB4 File Offset: 0x001CECB4
		private void ReadHeader()
		{
			if (this.dataLength < 6)
			{
				throw new OofHistoryCorruptionException("OOF history initial bytes corrupted.  Must have at least 6 bytes.");
			}
			this.entryCount = BitConverter.ToInt32(this.data, 2);
			if (this.entryCount < 0)
			{
				throw new OofHistoryCorruptionException("OOF history initial bytes corrupted.  Entry count is negative.");
			}
			if (10000 < this.entryCount)
			{
				this.entryCount = 10000;
			}
			this.dataPosition = 6;
		}

		// Token: 0x06006C40 RID: 27712 RVA: 0x001D0B1A File Offset: 0x001CED1A
		private bool EnsureDataSufficiency(int bytesRequired)
		{
			this.ThrowIfNotInitialized();
			if (this.dataLength < this.dataPosition + bytesRequired)
			{
				if (this.dataLength < 32768)
				{
					return false;
				}
				this.LoadBuffer();
				if (this.dataLength < bytesRequired)
				{
					return false;
				}
				this.dataPosition = 0;
			}
			return true;
		}

		// Token: 0x06006C41 RID: 27713 RVA: 0x001D0B5C File Offset: 0x001CED5C
		private void LoadBuffer()
		{
			this.dataLength -= this.dataPosition;
			if (this.dataPosition != 0 && this.dataLength != 0)
			{
				if (this.dataPosition < 16384)
				{
					throw new OofHistoryCorruptionException("Remaining valid data is more than half the buffer size.");
				}
				Buffer.BlockCopy(this.data, this.dataPosition, this.data, 0, this.dataLength);
			}
			this.dataLength = this.oofHistoryStream.Read(this.data, this.dataLength, 32768 - this.dataLength) + this.dataLength;
		}

		// Token: 0x06006C42 RID: 27714 RVA: 0x001D0BF2 File Offset: 0x001CEDF2
		private void ThrowIfNotInitialized()
		{
			if (!this.initialized)
			{
				throw new InvalidOperationException("Call the Initialize method first.");
			}
		}

		// Token: 0x04003DEA RID: 15850
		private const int DefaultBufferSize = 32768;

		// Token: 0x04003DEB RID: 15851
		private byte[] data;

		// Token: 0x04003DEC RID: 15852
		private int dataPosition;

		// Token: 0x04003DED RID: 15853
		private int dataLength;

		// Token: 0x04003DEE RID: 15854
		private Stream oofHistoryStream;

		// Token: 0x04003DEF RID: 15855
		private bool initialized;

		// Token: 0x04003DF0 RID: 15856
		private int entryCount;

		// Token: 0x04003DF1 RID: 15857
		private int entriesRead;

		// Token: 0x04003DF2 RID: 15858
		private VirtualList<byte> currentEntryAddressBytes;

		// Token: 0x04003DF3 RID: 15859
		private VirtualList<byte> currentEntryRuleIdBytes;
	}
}
