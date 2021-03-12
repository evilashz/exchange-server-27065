using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Common.Sniff;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.Internal;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200005F RID: 95
	internal sealed class ZipEntryAttachment
	{
		// Token: 0x06000307 RID: 775 RVA: 0x0000BBC4 File Offset: 0x00009DC4
		internal ZipEntryAttachment(string fileName, Attachment attachment, bool doNeedToFilterHtml, bool doNotSniff, bool isNotHtmlandNotXml)
		{
			this.fileName = fileName;
			this.encodedfileNameBytes = ZipEntryAttachment.GetEncodedString(fileName);
			this.attachmentId = attachment.Id;
			this.doNeedToFilterHtml = doNeedToFilterHtml;
			this.doNotSniff = doNotSniff;
			this.isNotHtmlandNotXml = isNotHtmlandNotXml;
			if (this.NeedsCompression(attachment))
			{
				this.CompressionMethod = 8;
				this.GeneralPurposeBitFlag = 2056;
			}
			else
			{
				this.CompressionMethod = 0;
				this.GeneralPurposeBitFlag = 2048;
			}
			this.CurrentDateTime = this.CurrentDosDateTime();
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000BC48 File Offset: 0x00009E48
		internal long WriteToStream(ConfigurationContextBase configurationContext, Stream outputStream, OutboundConversionOptions outboundConversionOptions, BlockStatus blockStatus, long outputStreamPosition, IList<string> fileNames, AttachmentCollection attachmentCollection)
		{
			this.headerOffset = outputStreamPosition;
			Stream stream = null;
			uint num = 0U;
			using (Attachment attachment = attachmentCollection.Open(this.attachmentId))
			{
				try
				{
					if (attachment.AttachmentType == AttachmentType.EmbeddedMessage)
					{
						this.fileNames = fileNames;
						stream = this.GetItemAttachmentStream(attachment, outboundConversionOptions);
					}
					else
					{
						this.fileNames = null;
						stream = ZipEntryAttachment.GetStreamAttachmentStream(attachment);
					}
					if (stream.Length > 0L)
					{
						Stream stream2;
						if (this.doNeedToFilterHtml)
						{
							if (this.CompressionMethod != 0)
							{
								stream2 = AttachmentUtilities.GetFilteredStream(configurationContext, stream, attachment.TextCharset, blockStatus);
							}
							else
							{
								stream2 = this.GetFilteredResponseStream(configurationContext, stream, attachment.TextCharset, blockStatus);
							}
						}
						else
						{
							stream2 = this.GetUnfilteredStream(stream);
						}
						if (stream2 != stream)
						{
							stream.Close();
							stream = stream2;
						}
					}
					if (this.CompressionMethod == 0)
					{
						if (!stream.CanSeek)
						{
							throw new ArgumentException("stream", "Stream is required to support Seek operations, and does not");
						}
						this.attachmentSize = stream.Length;
					}
					this.WriteZipFileHeader(stream, outputStream);
					this.WriteFileData(stream, outputStream, blockStatus);
					if (this.CompressionMethod != 0)
					{
						num = this.WriteZipFileDescriptor(outputStream);
					}
				}
				finally
				{
					if (stream != null)
					{
						stream.Close();
						stream = null;
					}
				}
			}
			return (long)((ulong)(this.headerBytesWritten + this.attachmentBytesWritten + num));
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000BD84 File Offset: 0x00009F84
		internal void WriteZipFileHeader(Stream inputStream, Stream outputStream)
		{
			ByteBuffer byteBuffer = new ByteBuffer(30);
			byteBuffer.WriteUInt32(67324752U);
			byteBuffer.WriteUInt16(20);
			byteBuffer.WriteUInt16(this.GeneralPurposeBitFlag);
			byteBuffer.WriteUInt16(this.CompressionMethod);
			byteBuffer.WriteUInt32(this.CurrentDateTime);
			if (this.CompressionMethod != 0)
			{
				byteBuffer.WriteUInt32(0U);
				byteBuffer.WriteUInt32(0U);
				byteBuffer.WriteUInt32(0U);
			}
			else
			{
				this.ComputeCrc32(inputStream);
				byteBuffer.WriteUInt32(this.CheckSum);
				byteBuffer.WriteUInt32((uint)this.attachmentSize);
				byteBuffer.WriteUInt32((uint)this.attachmentSize);
			}
			byteBuffer.WriteUInt16((ushort)this.encodedfileNameBytes.Length);
			byteBuffer.WriteUInt16(0);
			byteBuffer.WriteContentsTo(outputStream);
			outputStream.Write(this.encodedfileNameBytes, 0, this.encodedfileNameBytes.Length);
			this.headerBytesWritten = (uint)(byteBuffer.Length + this.encodedfileNameBytes.Length);
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000BE74 File Offset: 0x0000A074
		internal uint WriteCentralDirectoryStructure(Stream outputStream)
		{
			ByteBuffer byteBuffer = new ByteBuffer(46);
			byteBuffer.WriteUInt32(33639248U);
			byteBuffer.WriteUInt16(20);
			byteBuffer.WriteUInt16(20);
			byteBuffer.WriteUInt16(this.GeneralPurposeBitFlag);
			byteBuffer.WriteUInt16(this.CompressionMethod);
			byteBuffer.WriteUInt32(this.CurrentDateTime);
			byteBuffer.WriteUInt32(this.CheckSum);
			byteBuffer.WriteUInt32(this.attachmentBytesWritten);
			byteBuffer.WriteUInt32((uint)this.attachmentSize);
			byteBuffer.WriteUInt16((ushort)this.encodedfileNameBytes.Length);
			byteBuffer.WriteUInt16(0);
			byteBuffer.WriteUInt16(0);
			byteBuffer.WriteUInt16(0);
			byteBuffer.WriteUInt16(0);
			byteBuffer.WriteUInt32(0U);
			byteBuffer.WriteUInt32((uint)this.headerOffset);
			byteBuffer.WriteContentsTo(outputStream);
			outputStream.Write(this.encodedfileNameBytes, 0, this.encodedfileNameBytes.Length);
			this.directoryBytesWritten = (uint)(byteBuffer.Length + this.encodedfileNameBytes.Length);
			return this.directoryBytesWritten;
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600030B RID: 779 RVA: 0x0000BF76 File Offset: 0x0000A176
		// (set) Token: 0x0600030C RID: 780 RVA: 0x0000BF7E File Offset: 0x0000A17E
		private ushort CompressionMethod
		{
			get
			{
				return this.compressionMethod;
			}
			set
			{
				this.compressionMethod = value;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600030D RID: 781 RVA: 0x0000BF87 File Offset: 0x0000A187
		// (set) Token: 0x0600030E RID: 782 RVA: 0x0000BF8F File Offset: 0x0000A18F
		private ushort GeneralPurposeBitFlag
		{
			get
			{
				return this.generalPurposeFlag;
			}
			set
			{
				this.generalPurposeFlag = value;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600030F RID: 783 RVA: 0x0000BF98 File Offset: 0x0000A198
		// (set) Token: 0x06000310 RID: 784 RVA: 0x0000BFA0 File Offset: 0x0000A1A0
		private uint CheckSum
		{
			get
			{
				return this.checkSum;
			}
			set
			{
				this.checkSum = value;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000311 RID: 785 RVA: 0x0000BFA9 File Offset: 0x0000A1A9
		// (set) Token: 0x06000312 RID: 786 RVA: 0x0000BFB1 File Offset: 0x0000A1B1
		private uint CurrentDateTime
		{
			get
			{
				return this.currentDateTime;
			}
			set
			{
				this.currentDateTime = value;
			}
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000BFBC File Offset: 0x0000A1BC
		private static bool CheckShouldRemoveContents(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (!stream.CanSeek)
			{
				throw new ArgumentException("Stream is required to support Seek operations, and does not");
			}
			byte[] array = new byte[512];
			int bytesToRead = stream.Read(array, 0, 512);
			bool result = ZipEntryAttachment.CheckShouldRemoveContents(array, bytesToRead);
			stream.Seek(0L, SeekOrigin.Begin);
			return result;
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000C018 File Offset: 0x0000A218
		private static bool CheckShouldRemoveContents(byte[] bytesToSniff, int bytesToRead)
		{
			bool result;
			using (MemoryStream memoryStream = new MemoryStream(bytesToSniff, 0, bytesToRead))
			{
				DataSniff dataSniff = new DataSniff(256);
				string x = dataSniff.FindMimeFromData(memoryStream);
				result = (StringComparer.OrdinalIgnoreCase.Compare(x, "text/xml") == 0 || 0 == StringComparer.OrdinalIgnoreCase.Compare(x, "text/html"));
			}
			return result;
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000C088 File Offset: 0x0000A288
		private static Stream GetContentsReplacementStream(Strings.IDs resource)
		{
			byte[] encodedString = ZipEntryAttachment.GetEncodedString(Strings.GetLocalizedString(resource));
			return new MemoryStream(encodedString, 0, encodedString.Length);
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000C0AD File Offset: 0x0000A2AD
		private static byte[] GetEncodedString(string stringToEncode)
		{
			return Encoding.UTF8.GetBytes(stringToEncode);
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000C0BA File Offset: 0x0000A2BA
		private bool NeedsCompression(Attachment attachment)
		{
			return !ZipEntryAttachment.alreadyCompressedRegex.IsMatch(attachment.FileExtension) && attachment.Size >= 1000L;
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000C0E4 File Offset: 0x0000A2E4
		private void ComputeCrc32(Stream stream)
		{
			uint num = ZipEntryAttachment.ComputeCrc32FromStream(stream);
			this.CheckSum = num;
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000C100 File Offset: 0x0000A300
		private static uint ComputeCrc32FromStream(Stream stream)
		{
			if (!stream.CanSeek)
			{
				throw new ArgumentException("Stream is required to support Seek operations, and does not");
			}
			BufferPool bufferPool = BufferPoolCollection.AutoCleanupCollection.Acquire(ZipEntryAttachment.CopyBufferSize);
			byte[] array = bufferPool.Acquire();
			uint num = 0U;
			try
			{
				int bytesToRead;
				while ((bytesToRead = stream.Read(array, 0, array.Length)) > 0)
				{
					num = ZipEntryAttachment.ComputeCrc32FromBytes(array, bytesToRead, num);
				}
			}
			finally
			{
				if (array != null)
				{
					bufferPool.Release(array);
				}
			}
			stream.Seek(0L, SeekOrigin.Begin);
			return num;
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000C17C File Offset: 0x0000A37C
		private static uint ComputeCrc32FromBytes(byte[] data, int bytesToRead, uint seed)
		{
			uint num = seed ^ uint.MaxValue;
			for (int i = 0; i < bytesToRead; i++)
			{
				num = (ZipEntryAttachment.CrcTable[(int)((UIntPtr)((num ^ (uint)data[i]) & 255U))] ^ num >> 8);
			}
			return num ^ uint.MaxValue;
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000C1B8 File Offset: 0x0000A3B8
		private static uint[] GenerateCrc32Table()
		{
			int num = 256;
			uint[] array = new uint[num];
			for (int i = 0; i < num; i++)
			{
				uint num2 = (uint)i;
				for (int j = 0; j < 8; j++)
				{
					if ((num2 & 1U) != 0U)
					{
						num2 = (3988292384U ^ num2 >> 1);
					}
					else
					{
						num2 >>= 1;
					}
				}
				array[i] = num2;
			}
			return array;
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000C20C File Offset: 0x0000A40C
		private void WriteFileData(Stream inputStream, Stream outputStream, BlockStatus blockStatus)
		{
			uint[] array = new uint[3];
			if (this.CompressionMethod == 8)
			{
				array = ZipEntryAttachment.CompressAndWriteOutputStream(outputStream, inputStream, true);
				this.attachmentBytesWritten = array[0];
				this.CheckSum = array[1];
				this.attachmentSize = (long)((ulong)array[2]);
				return;
			}
			this.attachmentBytesWritten = ZipEntryAttachment.WriteOutputStream(outputStream, inputStream);
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0000C25C File Offset: 0x0000A45C
		private static uint[] CompressAndWriteOutputStream(Stream outputStream, Stream inputStream, bool doComputeCrc)
		{
			if (outputStream == null)
			{
				throw new ArgumentNullException("outputStream");
			}
			if (inputStream == null)
			{
				throw new ArgumentNullException("inputStream");
			}
			uint num = 0U;
			int num2 = 0;
			BufferPool bufferPool = BufferPoolCollection.AutoCleanupCollection.Acquire(ZipEntryAttachment.CopyBufferSize);
			byte[] array = bufferPool.Acquire();
			uint num3 = 0U;
			using (Stream stream = Streams.CreateTemporaryStorageStream())
			{
				try
				{
					int num4;
					using (Stream stream2 = new DeflateStream(stream, CompressionMode.Compress, true))
					{
						while ((num4 = inputStream.Read(array, 0, array.Length)) > 0)
						{
							if (doComputeCrc)
							{
								num3 = ZipEntryAttachment.ComputeCrc32FromBytes(array, num4, num3);
							}
							num2 += num4;
							stream2.Write(array, 0, num4);
						}
						stream2.Flush();
					}
					stream.Seek(0L, SeekOrigin.Begin);
					while ((num4 = stream.Read(array, 0, array.Length)) > 0)
					{
						outputStream.Write(array, 0, num4);
						num += (uint)num4;
					}
				}
				finally
				{
					if (array != null)
					{
						bufferPool.Release(array);
					}
				}
			}
			return new uint[]
			{
				num,
				num3,
				(uint)num2
			};
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0000C388 File Offset: 0x0000A588
		private static uint WriteOutputStream(Stream outputStream, Stream inputStream)
		{
			if (outputStream == null)
			{
				throw new ArgumentNullException("outputStream");
			}
			if (inputStream == null)
			{
				throw new ArgumentNullException("inputStream");
			}
			uint num = 0U;
			BufferPool bufferPool = BufferPoolCollection.AutoCleanupCollection.Acquire(ZipEntryAttachment.CopyBufferSize);
			byte[] array = bufferPool.Acquire();
			try
			{
				int num2;
				while ((num2 = inputStream.Read(array, 0, array.Length)) > 0)
				{
					outputStream.Write(array, 0, num2);
					num += (uint)num2;
				}
			}
			finally
			{
				if (array != null)
				{
					bufferPool.Release(array);
				}
			}
			return num;
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000C40C File Offset: 0x0000A60C
		private static Stream GetStreamAttachmentStream(Attachment attachment)
		{
			StreamAttachmentBase streamAttachmentBase = attachment as StreamAttachmentBase;
			if (streamAttachmentBase == null)
			{
				throw new ArgumentNullException("stream", "Attachment is not a stream attachment");
			}
			OleAttachment oleAttachment = streamAttachmentBase as OleAttachment;
			Stream stream;
			if (oleAttachment != null)
			{
				stream = oleAttachment.TryConvertToImage(ImageFormat.Jpeg);
				if (stream == null)
				{
					stream = new MemoryStream();
				}
			}
			else
			{
				stream = streamAttachmentBase.GetContentStream(PropertyOpenMode.ReadOnly);
			}
			return stream;
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000C45C File Offset: 0x0000A65C
		private Stream GetItemAttachmentStream(Attachment attachment, OutboundConversionOptions outboundConversionOptions)
		{
			Stream stream = Streams.CreateTemporaryStorageStream();
			string text = null;
			ItemAttachment itemAttachment = attachment as ItemAttachment;
			using (Item item = itemAttachment.GetItem(StoreObjectSchema.ContentConversionProperties))
			{
				try
				{
					if (ItemConversion.IsItemClassConvertibleToMime(item.ClassName))
					{
						ItemConversion.ConvertItemToMime(item, stream, outboundConversionOptions);
						text = ".eml";
					}
					else if (ObjectClass.IsCalendarItemCalendarItemOccurrenceOrRecurrenceException(item.ClassName))
					{
						(item as CalendarItemBase).ExportAsICAL(stream, "UTF-8", outboundConversionOptions);
						text = ".ics";
					}
					else if (ObjectClass.IsContact(item.ClassName))
					{
						Contact.ExportVCard(item as Contact, stream, outboundConversionOptions);
						text = ".vcf";
					}
					else
					{
						ItemConversion.ConvertItemToMsgStorage(item, stream, outboundConversionOptions);
					}
				}
				catch (Exception)
				{
					stream = ZipEntryAttachment.GetContentsReplacementStream(-1706159495);
					text = ".txt";
				}
			}
			if (text != null)
			{
				this.fileName = this.GetConvertedItemFileName(this.fileName, text);
				this.encodedfileNameBytes = ZipEntryAttachment.GetEncodedString(this.fileName);
			}
			stream.Position = 0L;
			return stream;
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000C564 File Offset: 0x0000A764
		private string GetConvertedItemFileName(string fileName, string newfileExtension)
		{
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
			int num = 1;
			string text = fileNameWithoutExtension + newfileExtension;
			while (this.fileNames.Contains(text))
			{
				text = string.Concat(new string[]
				{
					fileNameWithoutExtension,
					"[",
					num.ToString(),
					"]",
					newfileExtension
				});
				num++;
			}
			return text;
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000C5C8 File Offset: 0x0000A7C8
		private uint WriteZipFileDescriptor(Stream outputStream)
		{
			ByteBuffer byteBuffer = new ByteBuffer(16);
			byteBuffer.WriteUInt32(134695760U);
			byteBuffer.WriteUInt32(this.CheckSum);
			byteBuffer.WriteUInt32(this.attachmentBytesWritten);
			byteBuffer.WriteUInt32((uint)this.attachmentSize);
			byteBuffer.WriteContentsTo(outputStream);
			return (uint)byteBuffer.Length;
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000C624 File Offset: 0x0000A824
		private Stream GetFilteredResponseStream(ConfigurationContextBase configurationContext, Stream inputStream, Charset charset, BlockStatus blockStatus)
		{
			BufferPool bufferPool = BufferPoolCollection.AutoCleanupCollection.Acquire(ZipEntryAttachment.CopyBufferSize);
			byte[] array = bufferPool.Acquire();
			Stream stream = new MemoryStream();
			try
			{
				using (Stream filteredStream = AttachmentUtilities.GetFilteredStream(configurationContext, inputStream, charset, blockStatus))
				{
					try
					{
						int count;
						while ((count = filteredStream.Read(array, 0, array.Length)) > 0)
						{
							stream.Write(array, 0, count);
						}
					}
					finally
					{
						if (array != null)
						{
							bufferPool.Release(array);
						}
					}
				}
				stream.Seek(0L, SeekOrigin.Begin);
			}
			catch (Exception ex)
			{
				ExTraceGlobals.AttachmentHandlingTracer.TraceError<string>((long)this.GetHashCode(), "ZipEntryAttachment.GetfilteredStream: Safe HTML converter failed with exception {0}", ex.Message);
				stream = new MemoryStream();
			}
			return stream;
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0000C6F0 File Offset: 0x0000A8F0
		private Stream GetUnfilteredStream(Stream stream)
		{
			if (!stream.CanSeek)
			{
				throw new ArgumentException("stream", "Stream is required to support Seek operations, and does not");
			}
			if (!this.doNotSniff && this.isNotHtmlandNotXml && ZipEntryAttachment.CheckShouldRemoveContents(stream))
			{
				if (ExTraceGlobals.AttachmentHandlingTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.AttachmentHandlingTracer.TraceDebug<string, string>((long)this.GetHashCode(), "ZipEntryAttachment.GetUnfilteredStream: Return contents removed for attachment {0}: mailbox {1}", this.fileName, AttachmentUtilities.TryGetMailboxIdentityName());
				}
				return ZipEntryAttachment.GetContentsReplacementStream(-1868113279);
			}
			if (ExTraceGlobals.AttachmentHandlingTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.AttachmentHandlingTracer.TraceDebug<string, string>((long)this.GetHashCode(), "ZipEntryAttachment.GetUnfilteredStream: Return original contents for attachment {0}: mailbox {1}", this.fileName, AttachmentUtilities.TryGetMailboxIdentityName());
			}
			return stream;
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0000C798 File Offset: 0x0000A998
		private uint CurrentDosDateTime()
		{
			ExDateTime now = ExDateTime.Now;
			int num = now.Year - 1980 & 127;
			num = (num << 4) + now.Month;
			num = (num << 5) + now.Day;
			num = (num << 5) + now.Hour;
			num = (num << 6) + now.Minute;
			return (uint)((num << 5) + now.Second / 2);
		}

		// Token: 0x04000169 RID: 361
		private const int MinimumSizeForCompression = 1000;

		// Token: 0x0400016A RID: 362
		private const ushort VersionNeededToExtract = 20;

		// Token: 0x0400016B RID: 363
		private const ushort GeneralPurposeBitFlagDescriptor = 8;

		// Token: 0x0400016C RID: 364
		private const ushort GeneralPurposeBitFlagUtf8Encoded = 2048;

		// Token: 0x0400016D RID: 365
		private const ushort CompressionMethodStore = 0;

		// Token: 0x0400016E RID: 366
		private const ushort CompressionMethodDeflate = 8;

		// Token: 0x0400016F RID: 367
		private const ushort ExtraFieldLength = 0;

		// Token: 0x04000170 RID: 368
		private const string CompressedExts = "(?i)^\\.(mp3|png|docx|xlsx|pptx|jpg|zip|pdf|gif|mpg|aac|wma|wmv|mov)$";

		// Token: 0x04000171 RID: 369
		private const int DataSniffByteCount = 512;

		// Token: 0x04000172 RID: 370
		private static BufferPoolCollection.BufferSize CopyBufferSize = BufferPoolCollection.BufferSize.Size2K;

		// Token: 0x04000173 RID: 371
		private static Regex alreadyCompressedRegex = new Regex("(?i)^\\.(mp3|png|docx|xlsx|pptx|jpg|zip|pdf|gif|mpg|aac|wma|wmv|mov)$");

		// Token: 0x04000174 RID: 372
		private static uint[] CrcTable = ZipEntryAttachment.GenerateCrc32Table();

		// Token: 0x04000175 RID: 373
		private readonly bool doNeedToFilterHtml;

		// Token: 0x04000176 RID: 374
		private readonly bool doNotSniff;

		// Token: 0x04000177 RID: 375
		private readonly bool isNotHtmlandNotXml;

		// Token: 0x04000178 RID: 376
		private readonly AttachmentId attachmentId;

		// Token: 0x04000179 RID: 377
		private string fileName;

		// Token: 0x0400017A RID: 378
		private long headerOffset;

		// Token: 0x0400017B RID: 379
		private uint headerBytesWritten;

		// Token: 0x0400017C RID: 380
		private uint directoryBytesWritten;

		// Token: 0x0400017D RID: 381
		private uint attachmentBytesWritten;

		// Token: 0x0400017E RID: 382
		private long attachmentSize;

		// Token: 0x0400017F RID: 383
		private uint checkSum;

		// Token: 0x04000180 RID: 384
		private ushort compressionMethod;

		// Token: 0x04000181 RID: 385
		private ushort generalPurposeFlag;

		// Token: 0x04000182 RID: 386
		private uint currentDateTime;

		// Token: 0x04000183 RID: 387
		private IList<string> fileNames;

		// Token: 0x04000184 RID: 388
		private byte[] encodedfileNameBytes;
	}
}
