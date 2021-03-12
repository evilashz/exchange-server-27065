using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.CtsResources;

namespace Microsoft.Exchange.Data.Mime
{
	// Token: 0x02000032 RID: 50
	public class HeaderList : MimeNode, IEnumerable<Header>, IEnumerable
	{
		// Token: 0x06000227 RID: 551 RVA: 0x00009821 File Offset: 0x00007A21
		internal HeaderList(MimeNode parent) : base(parent)
		{
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000984C File Offset: 0x00007A4C
		private HeaderList()
		{
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00009876 File Offset: 0x00007A76
		public static HeaderList ReadFrom(MimeReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			return reader.ReadHeaderList();
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000988C File Offset: 0x00007A8C
		public Header FindFirst(string name)
		{
			if (name != null)
			{
				HeaderId headerId = Header.GetHeaderId(name, false);
				if (headerId != HeaderId.Unknown)
				{
					return this.FindFirst(headerId);
				}
				if (this.headerMap[(int)headerId] == 0)
				{
					return null;
				}
				Header header = base.FirstChild as Header;
				int num = 0;
				while (header != null)
				{
					if (header.IsName(name))
					{
						return header;
					}
					num++;
					this.CheckLoopCount(num);
					header = (header.NextSibling as Header);
				}
			}
			return null;
		}

		// Token: 0x0600022B RID: 555 RVA: 0x000098F4 File Offset: 0x00007AF4
		public Header FindFirst(HeaderId headerId)
		{
			if (headerId < HeaderId.Unknown || headerId > (HeaderId)MimeData.nameIndex.Length)
			{
				throw new ArgumentException(Strings.InvalidHeaderId, "headerId");
			}
			if (this.headerMap[(int)headerId] == 0)
			{
				return null;
			}
			Header header = base.FirstChild as Header;
			int num = 0;
			while (header != null)
			{
				if (headerId == header.HeaderId)
				{
					return header;
				}
				num++;
				this.CheckLoopCount(num);
				header = (header.NextSibling as Header);
			}
			this.headerMap[(int)headerId] = 0;
			return null;
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000996C File Offset: 0x00007B6C
		public Header FindNext(Header refHeader)
		{
			if (refHeader == null)
			{
				throw new ArgumentNullException("refHeader");
			}
			if (this != refHeader.Parent)
			{
				throw new ArgumentException(Strings.RefHeaderIsNotMyChild);
			}
			HeaderId headerId = refHeader.HeaderId;
			if (this.headerMap[(int)headerId] == 1)
			{
				return null;
			}
			Header header = refHeader.NextSibling as Header;
			int num = 0;
			if (headerId == HeaderId.Unknown)
			{
				string name = refHeader.Name;
				while (header != null)
				{
					if (header.IsName(name))
					{
						return header;
					}
					num++;
					this.CheckLoopCount(num);
					header = (header.NextSibling as Header);
				}
			}
			else
			{
				while (header != null)
				{
					if (headerId == header.HeaderId)
					{
						return header;
					}
					num++;
					this.CheckLoopCount(num);
					header = (header.NextSibling as Header);
				}
			}
			return null;
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00009A18 File Offset: 0x00007C18
		public Header[] FindAll(HeaderId headerId)
		{
			Header header = this.FindFirst(headerId);
			if (header == null)
			{
				return new Header[0];
			}
			int num = (int)this.headerMap[(int)headerId];
			Header header2;
			int num2;
			if (num == 255)
			{
				header2 = header;
				num = 1;
				num2 = 0;
				for (;;)
				{
					header2 = (header2.NextSibling as Header);
					if (header2 == null)
					{
						break;
					}
					num2++;
					this.CheckLoopCount(num2);
					if (headerId == header2.HeaderId)
					{
						num++;
					}
				}
				if (num < 255)
				{
					this.headerMap[(int)headerId] = (byte)num;
				}
			}
			Header[] array = new Header[num];
			header2 = header;
			array[0] = header2;
			num2 = 0;
			int i = 1;
			while (i < num)
			{
				header2 = (header2.NextSibling as Header);
				num2++;
				this.CheckLoopCount(num2);
				if (header2 == null)
				{
					break;
				}
				if (headerId == header2.HeaderId)
				{
					array[i++] = header2;
				}
			}
			return array;
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00009AD4 File Offset: 0x00007CD4
		public Header[] FindAll(string name)
		{
			HeaderId headerId = Header.GetHeaderId(name, false);
			if (headerId != HeaderId.Unknown)
			{
				return this.FindAll(headerId);
			}
			Header header = this.FindFirst(name);
			if (header == null)
			{
				return new Header[0];
			}
			Header header2 = header;
			int num = 1;
			int num2 = 0;
			for (;;)
			{
				header2 = (header2.NextSibling as Header);
				num2++;
				this.CheckLoopCount(num2);
				if (header2 == null)
				{
					break;
				}
				if (header2.IsName(name))
				{
					num++;
				}
			}
			Header[] array = new Header[num];
			header2 = header;
			array[0] = header2;
			num2 = 0;
			int i = 1;
			while (i < num)
			{
				header2 = (header2.NextSibling as Header);
				num2++;
				this.CheckLoopCount(num2);
				if (header2 == null)
				{
					break;
				}
				if (header2.IsName(name))
				{
					array[i++] = header2;
				}
			}
			return array;
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00009B88 File Offset: 0x00007D88
		public void RemoveAll(HeaderId headerId)
		{
			Header header = this.FindFirst(headerId);
			if (header != null)
			{
				if (this.headerMap[(int)headerId] == 1)
				{
					base.RemoveChild(header);
					return;
				}
				int num = 0;
				do
				{
					Header header2 = header.NextSibling as Header;
					num++;
					this.CheckLoopCount(num);
					if (header.HeaderId == headerId)
					{
						base.RemoveChild(header);
					}
					header = header2;
				}
				while (header != null);
				this.headerMap[(int)headerId] = 0;
			}
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00009BEC File Offset: 0x00007DEC
		public void RemoveAll(string name)
		{
			if (name != null)
			{
				HeaderId headerId = Header.GetHeaderId(name, false);
				if (headerId != HeaderId.Unknown)
				{
					this.RemoveAll(headerId);
					return;
				}
				Header header2;
				for (Header header = this.FindFirst(name); header != null; header = header2)
				{
					header2 = this.FindNext(header);
					base.RemoveChild(header);
				}
			}
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00009C2E File Offset: 0x00007E2E
		public new MimeNode.Enumerator<Header> GetEnumerator()
		{
			return new MimeNode.Enumerator<Header>(this);
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00009C36 File Offset: 0x00007E36
		IEnumerator<Header> IEnumerable<Header>.GetEnumerator()
		{
			return new MimeNode.Enumerator<Header>(this);
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00009C43 File Offset: 0x00007E43
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new MimeNode.Enumerator<Header>(this);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00009C50 File Offset: 0x00007E50
		public sealed override MimeNode Clone()
		{
			HeaderList headerList = new HeaderList();
			this.CopyTo(headerList);
			return headerList;
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00009C6C File Offset: 0x00007E6C
		public sealed override void CopyTo(object destination)
		{
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			if (destination == this)
			{
				return;
			}
			if (!(destination is HeaderList))
			{
				throw new ArgumentException(Strings.CantCopyToDifferentObjectType);
			}
			base.CopyTo(destination);
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00009CA8 File Offset: 0x00007EA8
		public void WriteTo(Stream stream, EncodingOptions encodingOptions, MimeOutputFilter filter)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (encodingOptions == null)
			{
				encodingOptions = base.GetDocumentEncodingOptions();
			}
			byte[] array = null;
			MimeStringLength mimeStringLength = new MimeStringLength(0);
			this.WriteTo(stream, encodingOptions, filter, ref mimeStringLength, ref array);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00009CEC File Offset: 0x00007EEC
		internal override long WriteTo(Stream stream, EncodingOptions encodingOptions, MimeOutputFilter filter, ref MimeStringLength currentLineLength, ref byte[] scratchBuffer)
		{
			MimePart.CountingWriteStream countingWriteStream = null;
			MimePart.CountingWriteStream countingWriteStream2 = null;
			long num = 0L;
			long num2 = 0L;
			if (filter != null)
			{
				countingWriteStream = (stream as MimePart.CountingWriteStream);
				if (countingWriteStream == null)
				{
					countingWriteStream2 = new MimePart.CountingWriteStream(stream);
					countingWriteStream = countingWriteStream2;
					stream = countingWriteStream;
				}
				num = countingWriteStream.Count;
			}
			for (MimeNode mimeNode = base.FirstChild; mimeNode != null; mimeNode = mimeNode.NextSibling)
			{
				if (filter == null || !filter.FilterHeader(mimeNode as Header, countingWriteStream))
				{
					num2 += mimeNode.WriteTo(stream, encodingOptions, filter, ref currentLineLength, ref scratchBuffer);
				}
			}
			if (countingWriteStream != null)
			{
				num2 = countingWriteStream.Count - num;
				if (countingWriteStream2 != null)
				{
					countingWriteStream2.Dispose();
				}
			}
			currentLineLength.SetAs(0);
			return num2;
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00009D80 File Offset: 0x00007F80
		internal override MimeNode ValidateNewChild(MimeNode newChild, MimeNode refChild)
		{
			Header header = newChild as Header;
			if (header == null)
			{
				throw new ArgumentException(Strings.NewChildNotMimeHeader, "newChild");
			}
			HeaderId headerId = header.HeaderId;
			if (Header.IsRestrictedHeader(headerId))
			{
				if (this.headerMap[(int)headerId] != 0)
				{
					Header header2 = this.FindFirst(headerId);
					if (header2 == refChild)
					{
						refChild = header2.PreviousSibling;
					}
					base.InternalRemoveChild(header2);
				}
				byte[] array = this.headerMap;
				HeaderId headerId2 = headerId;
				array[(int)headerId2] = array[(int)headerId2] + 1;
			}
			else if (this.headerMap[(int)headerId] != 255)
			{
				byte[] array2 = this.headerMap;
				HeaderId headerId3 = headerId;
				array2[(int)headerId3] = array2[(int)headerId3] + 1;
			}
			return refChild;
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00009E24 File Offset: 0x00008024
		internal override void ChildRemoved(MimeNode oldChild)
		{
			Header header = oldChild as Header;
			HeaderId headerId = header.HeaderId;
			if (this.headerMap[(int)headerId] != 255)
			{
				byte[] array = this.headerMap;
				HeaderId headerId2 = headerId;
				array[(int)headerId2] = array[(int)headerId2] - 1;
			}
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00009E68 File Offset: 0x00008068
		private void CheckLoopCount(int count)
		{
			if (!this.loopLimitInitialized)
			{
				MimeDocument mimeDocument;
				MimeNode mimeNode;
				base.GetMimeDocumentOrTreeRoot(out mimeDocument, out mimeNode);
				if (mimeDocument != null)
				{
					this.loopLimit = mimeDocument.MimeLimits.MaxHeaders;
				}
				this.loopLimitInitialized = true;
			}
			if (count > this.loopLimit)
			{
				string message = string.Format("Loop detected in headers collection. Loop count: {0}", this.loopLimit);
				throw new InvalidOperationException(message);
			}
		}

		// Token: 0x0400012A RID: 298
		private const string LoopLimitMessage = "Loop detected in headers collection. Loop count: {0}";

		// Token: 0x0400012B RID: 299
		private bool loopLimitInitialized;

		// Token: 0x0400012C RID: 300
		private int loopLimit = MimeLimits.Default.MaxHeaders;

		// Token: 0x0400012D RID: 301
		private byte[] headerMap = new byte[MimeData.nameIndex.Length];
	}
}
