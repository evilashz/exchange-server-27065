using System;
using System.IO;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Mime.Internal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000602 RID: 1538
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class MimeStreamWriter : IDisposeTrackable, IDisposable
	{
		// Token: 0x06003F5B RID: 16219 RVA: 0x001072E5 File Offset: 0x001054E5
		internal MimeStreamWriter(MimeStreamWriter.Flags flags, EncodingOptions encodingOptions) : this(null, null, encodingOptions, flags)
		{
		}

		// Token: 0x06003F5C RID: 16220 RVA: 0x001072F1 File Offset: 0x001054F1
		internal MimeStreamWriter(Stream mimeOut, EncodingOptions options, MimeStreamWriter.Flags flags) : this(mimeOut, null, options, flags)
		{
		}

		// Token: 0x06003F5D RID: 16221 RVA: 0x00107300 File Offset: 0x00105500
		internal MimeStreamWriter(Stream mimeOut, Stream mimeSkeletonOut, EncodingOptions options, MimeStreamWriter.Flags flags)
		{
			this.flags = flags;
			this.mimeWriter = null;
			this.encodingOptions = options;
			if (mimeOut != null)
			{
				if ((flags & MimeStreamWriter.Flags.SkipHeaders) == MimeStreamWriter.Flags.SkipHeaders)
				{
					this.mimeTextStream = new MimeStreamWriter.MimeTextStream(mimeOut);
					mimeOut = this.mimeTextStream;
				}
				this.mimeWriter = new MimeWriter(mimeOut, (flags & MimeStreamWriter.Flags.ForceMime) == MimeStreamWriter.Flags.ForceMime, options);
			}
			if (mimeSkeletonOut != null)
			{
				this.mimeSkeletonWriter = new MimeWriter(mimeSkeletonOut, (flags & MimeStreamWriter.Flags.ForceMime) == MimeStreamWriter.Flags.ForceMime, options);
			}
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x06003F5E RID: 16222 RVA: 0x0010737E File Offset: 0x0010557E
		public virtual DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<MimeStreamWriter>(this);
		}

		// Token: 0x06003F5F RID: 16223 RVA: 0x00107386 File Offset: 0x00105586
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x06003F60 RID: 16224 RVA: 0x0010739B File Offset: 0x0010559B
		internal void StartPart(MimePartInfo part)
		{
			this.StartPart(part, true);
		}

		// Token: 0x06003F61 RID: 16225 RVA: 0x001073A8 File Offset: 0x001055A8
		internal void StartPart(MimePartInfo part, bool outputToSkeleton)
		{
			this.FlushCachedHeader();
			if (this.currentPart != null)
			{
				this.StartWriting();
				if ((this.flags & MimeStreamWriter.Flags.ProduceMimeStructure) == MimeStreamWriter.Flags.ProduceMimeStructure && this.currentPart.IsMultipart)
				{
					this.currentPart.ChildrenWrittenOut();
				}
			}
			MimePartHeaders mimePartHeaders = (part == null) ? null : part.Headers;
			this.currentPart = part;
			this.hasAllHeaders = (mimePartHeaders != null);
			if (this.mimeWriter != null)
			{
				this.mimeWriter.StartPart();
				if (this.hasAllHeaders)
				{
					this.CopyHeadersToWriter(mimePartHeaders, this.mimeWriter);
				}
			}
			if (this.mimeSkeletonWriter != null && outputToSkeleton)
			{
				this.mimeSkeletonWriter.StartPart();
				if (mimePartHeaders != null)
				{
					this.CopyHeadersToWriter(mimePartHeaders, this.mimeSkeletonWriter);
				}
			}
			this.assembleHeaders = false;
			if (this.currentPart != null && (this.flags & MimeStreamWriter.Flags.ProduceMimeStructure) == MimeStreamWriter.Flags.ProduceMimeStructure)
			{
				if (mimePartHeaders == null)
				{
					this.assembleHeaders = true;
					if ((this.flags & MimeStreamWriter.Flags.ForceMime) == MimeStreamWriter.Flags.ForceMime)
					{
						Header header = Header.Create(HeaderId.MimeVersion);
						header.Value = "1.0";
						this.currentPart.AddHeader(header);
					}
				}
				this.flags &= (MimeStreamWriter.Flags)(-5);
			}
		}

		// Token: 0x06003F62 RID: 16226 RVA: 0x001074B8 File Offset: 0x001056B8
		private void CopyHeadersToWriter(MimePartHeaders headers, MimeWriter writer)
		{
			Header header = null;
			foreach (Header header2 in headers)
			{
				if (header2.HeaderId == HeaderId.MimeVersion)
				{
					header = header2;
				}
				else
				{
					header2.WriteTo(writer);
				}
			}
			if (header != null)
			{
				header.WriteTo(writer);
			}
		}

		// Token: 0x06003F63 RID: 16227 RVA: 0x0010751C File Offset: 0x0010571C
		internal void EndPart()
		{
			this.EndPart(true);
		}

		// Token: 0x06003F64 RID: 16228 RVA: 0x00107525 File Offset: 0x00105725
		internal void EndPart(bool outputToSkeleton)
		{
			this.FlushCachedHeader();
			if (this.mimeWriter != null)
			{
				this.mimeWriter.EndPart();
			}
			if (this.mimeSkeletonWriter != null && outputToSkeleton)
			{
				this.mimeSkeletonWriter.EndPart();
			}
			this.currentPart = null;
		}

		// Token: 0x06003F65 RID: 16229 RVA: 0x00107560 File Offset: 0x00105760
		private void FlushCachedHeader()
		{
			if (this.currentHeader == null || this.hasAllHeaders)
			{
				return;
			}
			if (!string.Equals(this.currentHeader.Name, "X-Exchange-Mime-Skeleton-Content-Id", StringComparison.OrdinalIgnoreCase))
			{
				if (this.mimeWriter != null)
				{
					this.currentHeader.WriteTo(this.mimeWriter);
				}
				if (this.assembleHeaders && this.currentPart != null)
				{
					this.currentPart.AddHeader(this.currentHeader);
				}
			}
			if (this.mimeSkeletonWriter != null)
			{
				this.currentHeader.WriteTo(this.mimeSkeletonWriter);
			}
			this.currentHeader = null;
		}

		// Token: 0x06003F66 RID: 16230 RVA: 0x001075F0 File Offset: 0x001057F0
		internal static void CalculateBodySize(MimePartInfo partInfo, MimePart part)
		{
			if (partInfo.IsBodySizeComputed)
			{
				return;
			}
			using (Stream stream = new MimeStreamWriter.MimeBodySizeCounter(null, partInfo))
			{
				using (Stream rawContentReadStream = part.GetRawContentReadStream())
				{
					Util.StreamHandler.CopyStreamData(rawContentReadStream, stream);
				}
			}
		}

		// Token: 0x06003F67 RID: 16231 RVA: 0x00107654 File Offset: 0x00105854
		internal void WritePartWithHeaders(MimePart part, bool copyBoundaryToSkeleton)
		{
			this.StartPart(null, copyBoundaryToSkeleton);
			if ((this.flags & MimeStreamWriter.Flags.SkipHeaders) == MimeStreamWriter.Flags.SkipHeaders && this.mimeTextStream != null)
			{
				this.mimeTextStream.StartWriting();
				this.flags &= (MimeStreamWriter.Flags)(-2);
				if (this.mimeWriter == null)
				{
					goto IL_98;
				}
				using (Stream rawContentWriteStream = this.mimeWriter.GetRawContentWriteStream())
				{
					using (Stream rawContentReadStream = part.GetRawContentReadStream())
					{
						Util.StreamHandler.CopyStreamData(rawContentReadStream, rawContentWriteStream);
					}
					goto IL_98;
				}
			}
			if (this.mimeWriter != null)
			{
				using (Stream rawContentWriteStream2 = this.mimeWriter.GetRawContentWriteStream())
				{
					part.WriteTo(rawContentWriteStream2);
				}
			}
			IL_98:
			this.EndPart(copyBoundaryToSkeleton);
		}

		// Token: 0x06003F68 RID: 16232 RVA: 0x00107728 File Offset: 0x00105928
		internal void WriteHeadersFromPart(MimePart part)
		{
			this.StartPart(null, false);
			if (this.mimeWriter != null)
			{
				foreach (Header header in part.Headers)
				{
					header.WriteTo(this.mimeWriter);
				}
			}
			this.EndPart(false);
		}

		// Token: 0x06003F69 RID: 16233 RVA: 0x00107798 File Offset: 0x00105998
		internal void WriteHeader(Header header)
		{
			if (this.hasAllHeaders)
			{
				return;
			}
			this.FlushCachedHeader();
			this.currentHeader = header;
		}

		// Token: 0x06003F6A RID: 16234 RVA: 0x001077B0 File Offset: 0x001059B0
		internal void WriteHeader(string name, string data)
		{
			if (this.hasAllHeaders)
			{
				return;
			}
			Header header = Header.Create(name);
			if (MimeStreamWriter.CheckHeaderValue(header, data))
			{
				header = MimeStreamWriter.CopyHeader(header, data);
				this.WriteHeader(header);
			}
		}

		// Token: 0x06003F6B RID: 16235 RVA: 0x001077E8 File Offset: 0x001059E8
		internal void WriteHeader(HeaderId type, string data)
		{
			if (this.hasAllHeaders)
			{
				return;
			}
			Header header = Header.Create(type);
			if (MimeStreamWriter.CheckHeaderValue(header, data))
			{
				header = MimeStreamWriter.CopyHeader(header, data);
				this.WriteHeader(header);
			}
		}

		// Token: 0x06003F6C RID: 16236 RVA: 0x00107820 File Offset: 0x00105A20
		internal void WriteHeader(HeaderId id, ExDateTime data)
		{
			if (this.hasAllHeaders)
			{
				return;
			}
			DateHeader header = (DateHeader)Header.Create(id);
			MimeInternalHelpers.SetDateHeaderValue(header, data.UniversalTime, data.Bias);
			this.WriteHeader(header);
		}

		// Token: 0x06003F6D RID: 16237 RVA: 0x00107860 File Offset: 0x00105A60
		internal void WriteHeaderParameter(string parameterName, string parameterValue)
		{
			if (this.hasAllHeaders)
			{
				return;
			}
			ComplexHeader complexHeader = (ComplexHeader)this.currentHeader;
			MimeParameter mimeParameter = complexHeader[parameterName];
			if (mimeParameter == null)
			{
				mimeParameter = new MimeParameter(parameterName);
				complexHeader.AppendChild(mimeParameter);
			}
			mimeParameter.Value = parameterValue;
		}

		// Token: 0x06003F6E RID: 16238 RVA: 0x001078A3 File Offset: 0x00105AA3
		private static Header CopyHeader(Header header, string data)
		{
			if (data != null)
			{
				if (header is AddressHeader)
				{
					header = AddressHeader.Parse(header.Name, data, AddressParserFlags.IgnoreComments);
				}
				else
				{
					header.Value = data;
				}
			}
			return header;
		}

		// Token: 0x06003F6F RID: 16239 RVA: 0x001078C9 File Offset: 0x00105AC9
		internal MimeStreamWriter GetEmbeddedWriter(EncodingOptions encodingOptions, Stream embeddedSkeleton, OutboundConversionOptions options)
		{
			return new MimeStreamWriter(this.GetContentStream(false), embeddedSkeleton, encodingOptions, (this.flags & MimeStreamWriter.Flags.ProduceMimeStructure) | MimeStreamWriter.Flags.ForceMime);
		}

		// Token: 0x06003F70 RID: 16240 RVA: 0x001078E4 File Offset: 0x00105AE4
		internal void WriteMailBox(string displayName, string address)
		{
			if (this.hasAllHeaders)
			{
				return;
			}
			if (address == null)
			{
				address = string.Empty;
			}
			AddressHeader addressHeader = (AddressHeader)this.currentHeader;
			MimeRecipient newChild = new MimeRecipient(displayName, address);
			addressHeader.AppendChild(newChild);
		}

		// Token: 0x06003F71 RID: 16241 RVA: 0x00107920 File Offset: 0x00105B20
		internal Stream GetContentStream(bool writeToSkeleton)
		{
			this.StartWriting();
			this.Flush();
			Stream stream = null;
			bool flag = false;
			try
			{
				if (this.mimeWriter != null)
				{
					stream = this.mimeWriter.GetRawContentWriteStream();
				}
				if (writeToSkeleton && this.mimeSkeletonWriter != null)
				{
					Stream rawContentWriteStream = this.mimeSkeletonWriter.GetRawContentWriteStream();
					stream = ((stream == null) ? rawContentWriteStream : new MimeStreamWriter.SplitterWriteStream(stream, rawContentWriteStream));
				}
				if (this.currentPart != null && (this.flags & MimeStreamWriter.Flags.ProduceMimeStructure) == MimeStreamWriter.Flags.ProduceMimeStructure && !this.currentPart.IsBodySizeComputed)
				{
					stream = new MimeStreamWriter.MimeBodySizeCounter(stream, this.currentPart);
				}
				if (stream == null)
				{
					stream = new MimeStreamWriter.MimeTextStream(null);
				}
				flag = true;
			}
			finally
			{
				if (!flag && stream != null)
				{
					stream.Dispose();
				}
			}
			return stream;
		}

		// Token: 0x06003F72 RID: 16242 RVA: 0x001079D0 File Offset: 0x00105BD0
		private void StartWriting()
		{
			this.FlushCachedHeader();
			this.Flush();
			if ((this.flags & MimeStreamWriter.Flags.SkipHeaders) == MimeStreamWriter.Flags.SkipHeaders && this.mimeTextStream != null)
			{
				this.flags &= (MimeStreamWriter.Flags)(-2);
				if (this.mimeWriter != null)
				{
					this.mimeTextStream.StartWriting();
				}
			}
		}

		// Token: 0x06003F73 RID: 16243 RVA: 0x00107A1E File Offset: 0x00105C1E
		internal void Flush()
		{
			this.FlushCachedHeader();
			if (this.mimeWriter != null)
			{
				this.mimeWriter.Flush();
			}
			if (this.mimeSkeletonWriter != null)
			{
				this.mimeSkeletonWriter.Flush();
			}
		}

		// Token: 0x06003F74 RID: 16244 RVA: 0x00107A4C File Offset: 0x00105C4C
		private void Dispose(bool isDisposing)
		{
			this.FlushCachedHeader();
			if (isDisposing)
			{
				if (this.mimeWriter != null)
				{
					this.mimeWriter.Close();
				}
				if (this.mimeSkeletonWriter != null)
				{
					this.mimeSkeletonWriter.Close();
				}
				if (this.disposeTracker != null)
				{
					this.disposeTracker.Dispose();
				}
			}
		}

		// Token: 0x06003F75 RID: 16245 RVA: 0x00107A9B File Offset: 0x00105C9B
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06003F76 RID: 16246 RVA: 0x00107AAC File Offset: 0x00105CAC
		internal static bool CheckAsciiHeaderValue(string value)
		{
			if (value == null)
			{
				return true;
			}
			for (int i = 0; i < value.Length; i++)
			{
				if (value[i] >= '\u0080')
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003F77 RID: 16247 RVA: 0x00107AE0 File Offset: 0x00105CE0
		internal static bool CheckHeaderValue(Header header, string value)
		{
			if (header is TextHeader)
			{
				return true;
			}
			if (header is AddressHeader)
			{
				return true;
			}
			if (header is AsciiTextHeader)
			{
				return MimeStreamWriter.CheckAsciiHeaderValue(value);
			}
			if (!(header is ComplexHeader))
			{
				return value == null;
			}
			if (!MimeStreamWriter.CheckAsciiHeaderValue(value))
			{
				throw new InvalidOperationException(string.Format("ComplexHeader {0} value {1} is not ASCII", header.Name, value));
			}
			return true;
		}

		// Token: 0x170012F4 RID: 4852
		// (get) Token: 0x06003F78 RID: 16248 RVA: 0x00107B3F File Offset: 0x00105D3F
		internal string WriterCharsetName
		{
			get
			{
				return this.encodingOptions.CharsetName;
			}
		}

		// Token: 0x170012F5 RID: 4853
		// (get) Token: 0x06003F79 RID: 16249 RVA: 0x00107B4C File Offset: 0x00105D4C
		internal Charset WriterCharset
		{
			get
			{
				return Charset.GetCharset(this.encodingOptions.CharsetName);
			}
		}

		// Token: 0x040022DA RID: 8922
		private MimeWriter mimeWriter;

		// Token: 0x040022DB RID: 8923
		private MimeWriter mimeSkeletonWriter;

		// Token: 0x040022DC RID: 8924
		private MimeStreamWriter.MimeTextStream mimeTextStream;

		// Token: 0x040022DD RID: 8925
		private EncodingOptions encodingOptions;

		// Token: 0x040022DE RID: 8926
		private MimeStreamWriter.Flags flags;

		// Token: 0x040022DF RID: 8927
		private bool assembleHeaders;

		// Token: 0x040022E0 RID: 8928
		private bool hasAllHeaders;

		// Token: 0x040022E1 RID: 8929
		private MimePartInfo currentPart;

		// Token: 0x040022E2 RID: 8930
		private Header currentHeader;

		// Token: 0x040022E3 RID: 8931
		private DisposeTracker disposeTracker;

		// Token: 0x02000603 RID: 1539
		internal enum Flags
		{
			// Token: 0x040022E5 RID: 8933
			None,
			// Token: 0x040022E6 RID: 8934
			SkipHeaders,
			// Token: 0x040022E7 RID: 8935
			ProduceMimeStructure,
			// Token: 0x040022E8 RID: 8936
			ForceMime = 4
		}

		// Token: 0x02000604 RID: 1540
		internal class MimeTextStream : StreamWrapper
		{
			// Token: 0x06003F7A RID: 16250 RVA: 0x00107B5E File Offset: 0x00105D5E
			internal MimeTextStream(Stream targetStream) : base(targetStream, false, StreamBase.Capabilities.Writable)
			{
				this.isWriting = false;
				this.skippedCR = false;
				this.skippedLF = false;
			}

			// Token: 0x06003F7B RID: 16251 RVA: 0x00107B80 File Offset: 0x00105D80
			public override void Write(byte[] buffer, int offset, int count)
			{
				if (this.isWriting && count > 0)
				{
					if (!this.skippedCR && buffer[offset] == 13)
					{
						this.skippedCR = true;
						this.Write(buffer, offset + 1, count - 1);
						return;
					}
					if (this.skippedCR && !this.skippedLF && buffer[offset] == 10)
					{
						this.skippedLF = true;
						this.Write(buffer, offset + 1, count - 1);
						return;
					}
					this.skippedCR = true;
					this.skippedLF = true;
					base.InternalStream.Write(buffer, offset, count);
				}
			}

			// Token: 0x06003F7C RID: 16252 RVA: 0x00107C05 File Offset: 0x00105E05
			public override void Flush()
			{
				if (this.isWriting)
				{
					base.InternalStream.Flush();
				}
			}

			// Token: 0x06003F7D RID: 16253 RVA: 0x00107C1A File Offset: 0x00105E1A
			internal void StartWriting()
			{
				this.isWriting = true;
			}

			// Token: 0x040022E9 RID: 8937
			private bool isWriting;

			// Token: 0x040022EA RID: 8938
			private bool skippedCR;

			// Token: 0x040022EB RID: 8939
			private bool skippedLF;
		}

		// Token: 0x02000605 RID: 1541
		internal class SplitterWriteStream : StreamWrapper
		{
			// Token: 0x06003F7E RID: 16254 RVA: 0x00107C23 File Offset: 0x00105E23
			internal SplitterWriteStream(Stream targetStream1, Stream targetStream2) : base(targetStream1, true, StreamBase.Capabilities.Writable)
			{
				this.targetStream2 = targetStream2;
			}

			// Token: 0x06003F7F RID: 16255 RVA: 0x00107C38 File Offset: 0x00105E38
			protected override void Dispose(bool disposing)
			{
				try
				{
					if (disposing && !base.IsClosed && this.targetStream2 != null)
					{
						this.targetStream2.Dispose();
						this.targetStream2 = null;
					}
				}
				finally
				{
					base.Dispose(disposing);
				}
			}

			// Token: 0x06003F80 RID: 16256 RVA: 0x00107C84 File Offset: 0x00105E84
			public override void Write(byte[] buffer, int offset, int count)
			{
				base.InternalStream.Write(buffer, offset, count);
				this.targetStream2.Write(buffer, offset, count);
			}

			// Token: 0x06003F81 RID: 16257 RVA: 0x00107CA2 File Offset: 0x00105EA2
			public override void Flush()
			{
				base.InternalStream.Flush();
				this.targetStream2.Flush();
			}

			// Token: 0x040022EC RID: 8940
			private Stream targetStream2;
		}

		// Token: 0x02000606 RID: 1542
		internal class MimeBodySizeCounter : StreamWrapper
		{
			// Token: 0x06003F82 RID: 16258 RVA: 0x00107CBA File Offset: 0x00105EBA
			internal MimeBodySizeCounter(Stream stream, MimePartInfo part) : base(stream, true, StreamBase.Capabilities.Writable)
			{
				this.part = part;
				this.lineCount = 0;
				this.byteCount = 0;
			}

			// Token: 0x06003F83 RID: 16259 RVA: 0x00107CDC File Offset: 0x00105EDC
			public override void Write(byte[] buffer, int offset, int count)
			{
				if (base.InternalStream != null)
				{
					base.InternalStream.Write(buffer, offset, count);
				}
				this.byteCount += count;
				for (int num = 0; num != count; num++)
				{
					if (buffer[offset + num] == 10)
					{
						this.lineCount++;
					}
				}
			}

			// Token: 0x06003F84 RID: 16260 RVA: 0x00107D30 File Offset: 0x00105F30
			public override void Flush()
			{
				if (base.InternalStream != null)
				{
					base.InternalStream.Flush();
				}
			}

			// Token: 0x06003F85 RID: 16261 RVA: 0x00107D48 File Offset: 0x00105F48
			protected override void Dispose(bool disposing)
			{
				try
				{
					if (disposing)
					{
						this.part.SetBodySize(this.byteCount, this.lineCount);
					}
				}
				finally
				{
					base.Dispose(disposing);
				}
			}

			// Token: 0x040022ED RID: 8941
			private MimePartInfo part;

			// Token: 0x040022EE RID: 8942
			private int lineCount;

			// Token: 0x040022EF RID: 8943
			private int byteCount;
		}
	}
}
