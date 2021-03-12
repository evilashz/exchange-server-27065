using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.Internal;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000299 RID: 665
	internal sealed class ZipEntryAttachment
	{
		// Token: 0x06001980 RID: 6528 RVA: 0x00095038 File Offset: 0x00093238
		internal ZipEntryAttachment(string fileName, AttachmentWellInfo attachmentWellInfo, bool doNeedToFilterHtml, bool doNotSniff, bool isNotHtmlandNotXml)
		{
			this.fileName = fileName;
			this.encodedfileNameBytes = this.GetEncodedString(fileName);
			this.attachmentWellInfo = attachmentWellInfo;
			this.doNeedToFilterHtml = doNeedToFilterHtml;
			this.doNotSniff = doNotSniff;
			this.isNotHtmlandNotXml = isNotHtmlandNotXml;
			if (this.NeedsCompression(attachmentWellInfo))
			{
				this.compressionMethod = 8;
				this.generalPurposeFlag = 2056;
			}
			else
			{
				this.compressionMethod = 0;
				this.generalPurposeFlag = 2048;
			}
			this.currentDateTime = this.CurrentDosDateTime();
		}

		// Token: 0x06001981 RID: 6529 RVA: 0x000950B8 File Offset: 0x000932B8
		internal long WriteToStream(Stream outputStream, HttpContext httpContext, BlockStatus blockStatus, long outputStreamPosition, IList<string> fileNames)
		{
			this.fileNames = fileNames;
			this.headerOffset = outputStreamPosition;
			Stream stream = null;
			using (Attachment attachment = this.attachmentWellInfo.OpenAttachment())
			{
				try
				{
					if (attachment.AttachmentType == AttachmentType.EmbeddedMessage)
					{
						stream = this.GetItemAttachmentStream(attachment, httpContext);
					}
					else
					{
						stream = this.GetStreamAttachmentStream(attachment);
					}
					if (stream.Length > 0L)
					{
						if (this.doNeedToFilterHtml)
						{
							if (this.CompressionMethod != 0)
							{
								stream = this.GetfilteredStreamForCompression(httpContext, stream, this.attachmentWellInfo.TextCharset, blockStatus);
							}
							else
							{
								stream = this.GetfilteredStream(httpContext, stream, this.attachmentWellInfo.TextCharset, blockStatus);
							}
						}
						else
						{
							stream = this.GetUnfilteredStream(httpContext, stream);
						}
					}
					if (this.CompressionMethod == 0)
					{
						if (!stream.CanSeek)
						{
							throw new OwaInvalidInputException("Stream is required to support Seek operations, and does not");
						}
						this.attachmentSize = stream.Length;
					}
					this.WriteZipFileHeader(stream, outputStream);
					this.WriteFileData(stream, outputStream, httpContext, blockStatus);
					if (this.CompressionMethod != 0)
					{
						this.WriteZipFileDescriptor(outputStream);
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
			return (long)((ulong)(this.headerBytesWritten + this.attachmentBytesWritten + this.descriptorBytesWritten));
		}

		// Token: 0x06001982 RID: 6530 RVA: 0x000951E4 File Offset: 0x000933E4
		private void WriteZipFileHeader(Stream inputStream, Stream outputStream)
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

		// Token: 0x06001983 RID: 6531 RVA: 0x000952D3 File Offset: 0x000934D3
		private byte[] GetEncodedString(string stringToEncode)
		{
			return Encoding.UTF8.GetBytes(stringToEncode);
		}

		// Token: 0x06001984 RID: 6532 RVA: 0x000952E0 File Offset: 0x000934E0
		private bool NeedsCompression(AttachmentWellInfo attachmentWellInfo)
		{
			return !ZipEntryAttachment.alreadyCompressedRegex.IsMatch(attachmentWellInfo.FileExtension) && attachmentWellInfo.Size >= 1000L;
		}

		// Token: 0x06001985 RID: 6533 RVA: 0x00095308 File Offset: 0x00093508
		private void ComputeCrc32(Stream stream)
		{
			uint num = AttachmentUtility.ComputeCrc32FromStream(stream);
			this.checkSum = num;
		}

		// Token: 0x06001986 RID: 6534 RVA: 0x00095324 File Offset: 0x00093524
		private void WriteFileData(Stream inputStream, Stream outputStream, HttpContext httpContext, BlockStatus blockStatus)
		{
			uint[] array = new uint[3];
			if (this.CompressionMethod == 8)
			{
				array = AttachmentUtility.CompressAndWriteOutputStream(httpContext.Response.OutputStream, inputStream, true);
				this.attachmentBytesWritten = array[0];
				this.checkSum = array[1];
				this.attachmentSize = (long)((ulong)array[2]);
				return;
			}
			this.attachmentBytesWritten = AttachmentUtility.WriteOutputStream(httpContext.Response.OutputStream, inputStream);
		}

		// Token: 0x06001987 RID: 6535 RVA: 0x00095388 File Offset: 0x00093588
		internal Stream GetStreamAttachmentStream(Attachment attachment)
		{
			StreamAttachmentBase streamAttachmentBase = attachment as StreamAttachmentBase;
			if (streamAttachmentBase == null)
			{
				throw new OwaInvalidRequestException("Attachment is not a stream attachment");
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

		// Token: 0x06001988 RID: 6536 RVA: 0x000953D4 File Offset: 0x000935D4
		internal Stream GetItemAttachmentStream(Attachment attachment, HttpContext httpContext)
		{
			OwaContext owaContext = OwaContext.Get(httpContext);
			UserContext userContext = owaContext.UserContext;
			OutboundConversionOptions outboundConversionOptions = Utilities.CreateOutboundConversionOptions(userContext);
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
					stream = this.GetContentsReplacementStream(-1706159495);
					text = ".txt";
				}
			}
			if (text != null)
			{
				this.fileName = this.GetConvertedItemFileName(this.fileName, text);
				this.encodedfileNameBytes = this.GetEncodedString(this.fileName);
			}
			stream.Position = 0L;
			return stream;
		}

		// Token: 0x06001989 RID: 6537 RVA: 0x00095504 File Offset: 0x00093704
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

		// Token: 0x0600198A RID: 6538 RVA: 0x00095568 File Offset: 0x00093768
		private void WriteZipFileDescriptor(Stream outputStream)
		{
			ByteBuffer byteBuffer = new ByteBuffer(16);
			byteBuffer.WriteUInt32(134695760U);
			byteBuffer.WriteUInt32(this.CheckSum);
			byteBuffer.WriteUInt32(this.attachmentBytesWritten);
			byteBuffer.WriteUInt32((uint)this.attachmentSize);
			byteBuffer.WriteContentsTo(outputStream);
			this.descriptorBytesWritten = (uint)byteBuffer.Length;
		}

		// Token: 0x0600198B RID: 6539 RVA: 0x000955C8 File Offset: 0x000937C8
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

		// Token: 0x0600198C RID: 6540 RVA: 0x000956CA File Offset: 0x000938CA
		public Stream GetfilteredStreamForCompression(HttpContext httpContext, Stream stream, Charset charset, BlockStatus blockStatus)
		{
			return AttachmentUtility.GetFilteredStream(httpContext, stream, charset, blockStatus);
		}

		// Token: 0x0600198D RID: 6541 RVA: 0x000956D8 File Offset: 0x000938D8
		internal Stream GetfilteredStream(HttpContext httpContext, Stream inputStream, Charset charset, BlockStatus blockStatus)
		{
			BufferPool bufferPool = BufferPoolCollection.AutoCleanupCollection.Acquire(AttachmentUtility.CopyBufferSize);
			byte[] array = bufferPool.Acquire();
			Stream stream = new MemoryStream();
			try
			{
				using (Stream filteredStream = AttachmentUtility.GetFilteredStream(httpContext, inputStream, charset, blockStatus))
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

		// Token: 0x0600198E RID: 6542 RVA: 0x000957A4 File Offset: 0x000939A4
		public Stream GetUnfilteredStream(HttpContext httpContext, Stream stream)
		{
			if (!stream.CanSeek)
			{
				throw new OwaInvalidInputException("Stream is required to support Seek operations, and does not");
			}
			if (!this.doNotSniff && this.isNotHtmlandNotXml && AttachmentUtility.CheckShouldRemoveContents(stream))
			{
				if (ExTraceGlobals.AttachmentHandlingTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.AttachmentHandlingTracer.TraceDebug<string, string>((long)this.GetHashCode(), "ZipEntryAttachment.GetUnfilteredStream: Return contents removed for attachment {0}: mailbox {1}", this.fileName, AttachmentUtility.TryGetMailboxIdentityName(httpContext));
				}
				return this.GetContentsReplacementStream(-1868113279);
			}
			if (ExTraceGlobals.AttachmentHandlingTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.AttachmentHandlingTracer.TraceDebug<string, string>((long)this.GetHashCode(), "ZipEntryAttachment.GetUnfilteredStream: Return original contents for attachment {0}: mailbox {1}", this.fileName, AttachmentUtility.TryGetMailboxIdentityName(httpContext));
			}
			return stream;
		}

		// Token: 0x0600198F RID: 6543 RVA: 0x00095848 File Offset: 0x00093A48
		private Stream GetContentsReplacementStream(Strings.IDs resource)
		{
			byte[] encodedString = this.GetEncodedString(LocalizedStrings.GetNonEncoded(resource));
			return new MemoryStream(encodedString, 0, encodedString.Length);
		}

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x06001990 RID: 6544 RVA: 0x0009586E File Offset: 0x00093A6E
		public AttachmentWellInfo AttachmentWellInfo
		{
			get
			{
				return this.attachmentWellInfo;
			}
		}

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x06001991 RID: 6545 RVA: 0x00095876 File Offset: 0x00093A76
		// (set) Token: 0x06001992 RID: 6546 RVA: 0x0009587E File Offset: 0x00093A7E
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

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x06001993 RID: 6547 RVA: 0x00095887 File Offset: 0x00093A87
		// (set) Token: 0x06001994 RID: 6548 RVA: 0x0009588F File Offset: 0x00093A8F
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

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x06001995 RID: 6549 RVA: 0x00095898 File Offset: 0x00093A98
		// (set) Token: 0x06001996 RID: 6550 RVA: 0x000958A0 File Offset: 0x00093AA0
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

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x06001997 RID: 6551 RVA: 0x000958A9 File Offset: 0x00093AA9
		// (set) Token: 0x06001998 RID: 6552 RVA: 0x000958B1 File Offset: 0x00093AB1
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

		// Token: 0x06001999 RID: 6553 RVA: 0x000958BC File Offset: 0x00093ABC
		private uint CurrentDosDateTime()
		{
			ExDateTime localTime = DateTimeUtilities.GetLocalTime();
			int num = localTime.Year - 1980 & 127;
			num = (num << 4) + localTime.Month;
			num = (num << 5) + localTime.Day;
			num = (num << 5) + localTime.Hour;
			num = (num << 6) + localTime.Minute;
			return (uint)((num << 5) + localTime.Second / 2);
		}

		// Token: 0x040012A6 RID: 4774
		private const int MinimumSizeForCompression = 1000;

		// Token: 0x040012A7 RID: 4775
		private const ushort VersionNeededToExtract = 20;

		// Token: 0x040012A8 RID: 4776
		private const ushort GeneralPurposeBitFlagDescriptor = 8;

		// Token: 0x040012A9 RID: 4777
		private const ushort GeneralPurposeBitFlagUtf8Encoded = 2048;

		// Token: 0x040012AA RID: 4778
		private const ushort CompressionMethodStore = 0;

		// Token: 0x040012AB RID: 4779
		private const ushort CompressionMethodDeflate = 8;

		// Token: 0x040012AC RID: 4780
		private const ushort ExtraFieldLength = 0;

		// Token: 0x040012AD RID: 4781
		private const string CompressedExts = "(?i)^\\.(mp3|png|docx|xlsx|pptx|jpg|zip|pdf|gif|mpg|aac|wma|wmv|mov)$";

		// Token: 0x040012AE RID: 4782
		private static Regex alreadyCompressedRegex = new Regex("(?i)^\\.(mp3|png|docx|xlsx|pptx|jpg|zip|pdf|gif|mpg|aac|wma|wmv|mov)$");

		// Token: 0x040012AF RID: 4783
		private string fileName;

		// Token: 0x040012B0 RID: 4784
		private long headerOffset;

		// Token: 0x040012B1 RID: 4785
		private uint headerBytesWritten;

		// Token: 0x040012B2 RID: 4786
		private uint directoryBytesWritten;

		// Token: 0x040012B3 RID: 4787
		private uint attachmentBytesWritten;

		// Token: 0x040012B4 RID: 4788
		private uint descriptorBytesWritten;

		// Token: 0x040012B5 RID: 4789
		private AttachmentWellInfo attachmentWellInfo;

		// Token: 0x040012B6 RID: 4790
		private bool doNeedToFilterHtml;

		// Token: 0x040012B7 RID: 4791
		private bool doNotSniff;

		// Token: 0x040012B8 RID: 4792
		private bool isNotHtmlandNotXml;

		// Token: 0x040012B9 RID: 4793
		private long attachmentSize;

		// Token: 0x040012BA RID: 4794
		private uint checkSum;

		// Token: 0x040012BB RID: 4795
		private ushort compressionMethod;

		// Token: 0x040012BC RID: 4796
		private ushort generalPurposeFlag;

		// Token: 0x040012BD RID: 4797
		private uint currentDateTime;

		// Token: 0x040012BE RID: 4798
		private IList<string> fileNames;

		// Token: 0x040012BF RID: 4799
		private byte[] encodedfileNameBytes;
	}
}
