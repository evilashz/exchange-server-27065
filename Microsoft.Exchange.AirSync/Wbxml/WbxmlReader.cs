using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.AirSync.Wbxml
{
	// Token: 0x020002A5 RID: 677
	internal class WbxmlReader : WbxmlBase, IDisposeTrackable, IDisposable
	{
		// Token: 0x06001892 RID: 6290 RVA: 0x00091776 File Offset: 0x0008F976
		public WbxmlReader(Stream stream) : this(stream, false)
		{
		}

		// Token: 0x06001893 RID: 6291 RVA: 0x00091780 File Offset: 0x0008F980
		public WbxmlReader(Stream stream, bool parseBlobAsByteArray)
		{
			this.parseBlobAsByteArray = parseBlobAsByteArray;
			if (stream == null)
			{
				throw new WbxmlException("WbxmlReader cannot accept a null stream");
			}
			this.underlyingStream = stream;
			this.stream = new PooledBufferedStream(stream, WbxmlReader.readerPool, false);
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x17000840 RID: 2112
		// (get) Token: 0x06001894 RID: 6292 RVA: 0x000917D0 File Offset: 0x0008F9D0
		private static XmlDocument ErrorDocument
		{
			get
			{
				if (WbxmlReader.errorDocument == null)
				{
					string xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><WbxmlConversionError></WbxmlConversionError>";
					XmlDocument xmlDocument = new SafeXmlDocument();
					xmlDocument.LoadXml(xml);
					WbxmlReader.errorDocument = xmlDocument;
				}
				return WbxmlReader.errorDocument;
			}
		}

		// Token: 0x06001895 RID: 6293 RVA: 0x00091804 File Offset: 0x0008FA04
		public XmlDocument ReadXmlDocument()
		{
			this.ReadHeader();
			XmlDocument result;
			try
			{
				bool flag;
				bool flag2;
				int tag = this.ReadTag(out flag, out flag2);
				string name = WbxmlBase.Schema.GetName(tag);
				string nameSpace = WbxmlBase.Schema.GetNameSpace(tag);
				if (name == null || nameSpace == null)
				{
					result = WbxmlReader.ErrorDocument;
				}
				else
				{
					XmlDocument xmlDocument = new SafeXmlDocument();
					bool flag3 = WbxmlBase.Schema.IsTagSecure(tag);
					bool flag4 = WbxmlBase.Schema.IsTagAnOpaqueBlob(tag);
					XmlElement xmlElement;
					if (flag3)
					{
						xmlElement = new AirSyncSecureStringXmlNode(null, name, nameSpace, xmlDocument);
					}
					else if (flag4)
					{
						xmlElement = new AirSyncBlobXmlNode(null, name, nameSpace, xmlDocument);
					}
					else
					{
						xmlElement = xmlDocument.CreateElement(name, nameSpace);
					}
					xmlDocument.AppendChild(xmlElement);
					if (flag)
					{
						this.SkipAttributes();
					}
					if (flag2 && !this.FillXmlElement(xmlElement, 0, flag3, flag4))
					{
						result = WbxmlReader.ErrorDocument;
					}
					else
					{
						result = xmlDocument;
					}
				}
			}
			catch (IndexOutOfRangeException innerException)
			{
				throw new WbxmlException("Invalid WBXML code/codepage from client", innerException);
			}
			catch (NotImplementedException innerException2)
			{
				throw new WbxmlException("Invalid WBXML data from client (see inner exception).", innerException2);
			}
			catch (ArgumentOutOfRangeException innerException3)
			{
				throw new WbxmlException("Invalid WBXML code from client", innerException3);
			}
			return result;
		}

		// Token: 0x06001896 RID: 6294 RVA: 0x0009192C File Offset: 0x0008FB2C
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001897 RID: 6295 RVA: 0x0009193B File Offset: 0x0008FB3B
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<WbxmlReader>(this);
		}

		// Token: 0x06001898 RID: 6296 RVA: 0x00091943 File Offset: 0x0008FB43
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x06001899 RID: 6297 RVA: 0x00091958 File Offset: 0x0008FB58
		protected void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.underlyingStream = null;
				if (this.stream != null)
				{
					this.stream.Dispose();
					this.stream = null;
				}
				if (this.disposeTracker != null)
				{
					this.disposeTracker.Dispose();
				}
			}
		}

		// Token: 0x0600189A RID: 6298 RVA: 0x00091994 File Offset: 0x0008FB94
		private static SecureString ConvertToSecureString(byte[] buffer)
		{
			if (buffer.Length > WbxmlReader.maxSecureStringLength)
			{
				throw new WbxmlException("Buffer is too long");
			}
			GCHandle gchandle = default(GCHandle);
			SecureString result;
			try
			{
				char[] chars = Encoding.UTF8.GetChars(buffer);
				gchandle = GCHandle.Alloc(chars, GCHandleType.Pinned);
				Array.Clear(buffer, 0, buffer.Length);
				SecureString secureString = chars.ConvertToSecureString();
				Array.Clear(chars, 0, chars.Length);
				result = secureString;
			}
			finally
			{
				if (gchandle.IsAllocated)
				{
					gchandle.Free();
				}
			}
			return result;
		}

		// Token: 0x0600189B RID: 6299 RVA: 0x00091A14 File Offset: 0x0008FC14
		private bool FillXmlElement(XmlElement elem, int depth, bool elemIsSecureData, bool elemIsBlobData)
		{
			if (depth > 20)
			{
				throw new WbxmlException("Document nested too deep");
			}
			for (;;)
			{
				bool flag;
				bool flag2;
				int num = this.ReadTag(out flag, out flag2);
				if (num == 1)
				{
					return true;
				}
				if (num == 3)
				{
					if (elemIsSecureData)
					{
						AirSyncSecureStringXmlNode airSyncSecureStringXmlNode = (AirSyncSecureStringXmlNode)elem;
						airSyncSecureStringXmlNode.SecureData = this.ReadInlineSecureString();
					}
					else
					{
						string innerText = this.ReadInlineString();
						elem.InnerText = innerText;
					}
				}
				else if (num == 195)
				{
					if (elemIsSecureData)
					{
						AirSyncSecureStringXmlNode airSyncSecureStringXmlNode2 = (AirSyncSecureStringXmlNode)elem;
						airSyncSecureStringXmlNode2.SecureData = this.ReadOpaqueSecureString();
					}
					else if (elemIsBlobData)
					{
						AirSyncBlobXmlNode airSyncBlobXmlNode = elem as AirSyncBlobXmlNode;
						int num2 = this.ReadMultiByteInteger();
						if (this.stream.Position + (long)num2 > this.stream.Length)
						{
							break;
						}
						if (this.parseBlobAsByteArray)
						{
							airSyncBlobXmlNode.ByteArray = this.ReadBytes(num2);
						}
						else
						{
							airSyncBlobXmlNode.Stream = new SubStream(this.underlyingStream, this.stream.Position, (long)num2);
							this.stream.Seek((long)num2, SeekOrigin.Current);
						}
					}
					else
					{
						string innerText2 = this.ReadOpaqueString();
						elem.InnerText = innerText2;
					}
				}
				if ((num & 63) >= 5)
				{
					if (flag)
					{
						this.SkipAttributes();
					}
					string name = WbxmlBase.Schema.GetName(num);
					string nameSpace = WbxmlBase.Schema.GetNameSpace(num);
					if (name == null || nameSpace == null)
					{
						return false;
					}
					bool flag3 = WbxmlBase.Schema.IsTagSecure(num);
					bool flag4 = WbxmlBase.Schema.IsTagAnOpaqueBlob(num);
					XmlElement xmlElement;
					if (flag3)
					{
						xmlElement = new AirSyncSecureStringXmlNode(null, name, nameSpace, elem.OwnerDocument);
					}
					else if (flag4)
					{
						xmlElement = new AirSyncBlobXmlNode(null, name, nameSpace, elem.OwnerDocument);
					}
					else
					{
						xmlElement = elem.OwnerDocument.CreateElement(name, nameSpace);
					}
					if (flag2 && !this.FillXmlElement(xmlElement, depth + 1, flag3, flag4))
					{
						return false;
					}
					elem.AppendChild(xmlElement);
				}
			}
			return false;
		}

		// Token: 0x0600189C RID: 6300 RVA: 0x00091BE4 File Offset: 0x0008FDE4
		private byte ReadByte()
		{
			int num = this.stream.ReadByte();
			if (num == -1)
			{
				Exception innerException = new EndOfStreamException();
				throw new WbxmlException("End of stream reached by ReadByte before parsing was complete", innerException);
			}
			return (byte)num;
		}

		// Token: 0x0600189D RID: 6301 RVA: 0x00091C18 File Offset: 0x0008FE18
		private void ReadBytes(byte[] answer)
		{
			int num = answer.Length;
			int i;
			int num2;
			for (i = 0; i < num; i += num2)
			{
				num2 = this.stream.Read(answer, i, num - i);
				if (num2 == 0)
				{
					break;
				}
			}
			if (i != num)
			{
				Exception innerException = new EndOfStreamException();
				throw new WbxmlException("End of stream reached by ReadBytes before parsing was complete", innerException);
			}
		}

		// Token: 0x0600189E RID: 6302 RVA: 0x00091C60 File Offset: 0x0008FE60
		private byte[] ReadBytes(int count)
		{
			byte[] array = new byte[count];
			this.ReadBytes(array);
			return array;
		}

		// Token: 0x0600189F RID: 6303 RVA: 0x00091C7C File Offset: 0x0008FE7C
		private void SkipBytes(int count)
		{
			if (this.stream.CanSeek)
			{
				this.stream.Seek((long)count, SeekOrigin.Current);
				return;
			}
			for (int i = 0; i < count; i++)
			{
				this.ReadByte();
			}
		}

		// Token: 0x060018A0 RID: 6304 RVA: 0x00091CBC File Offset: 0x0008FEBC
		private void ReadHeader()
		{
			int num = (int)this.ReadByte();
			if (num != 3)
			{
				throw new WbxmlException("Unsupported Wbxml version");
			}
			int num2 = this.ReadMultiByteInteger();
			if (num2 != 1)
			{
				throw new WbxmlException("Unsupported PublicID: " + num2);
			}
			int num3 = this.ReadMultiByteInteger();
			if (num3 != 106)
			{
				throw new WbxmlException("Unsupported charset: 0x" + num3.ToString("X", CultureInfo.InvariantCulture));
			}
			int num4 = this.ReadMultiByteInteger();
			if (num4 > 0)
			{
				throw new WbxmlException(string.Format("stringTableLength {0} > 0 ", num4));
			}
		}

		// Token: 0x060018A1 RID: 6305 RVA: 0x00091D50 File Offset: 0x0008FF50
		private string ReadInlineString()
		{
			long position = this.stream.Position;
			int num = 0;
			int num2 = 0;
			byte[] array = null;
			string @string;
			try
			{
				byte[] array2;
				array = (array2 = WbxmlReader.readerPool.Acquire());
				for (;;)
				{
					int num3 = this.stream.Read(array2, num, array2.Length - num);
					if (num3 == 0)
					{
						break;
					}
					num2 += num3;
					while (num < num2 && array2[num] != 0)
					{
						num++;
					}
					if (num < num2)
					{
						goto Block_5;
					}
					Array.Resize<byte>(ref array2, array2.Length * 2);
				}
				Exception innerException = new EndOfStreamException();
				throw new WbxmlException("End of stream reached by ReadInlineString before parsing was complete", innerException);
				Block_5:
				this.stream.Seek(position + (long)num + 1L, SeekOrigin.Begin);
				@string = Encoding.UTF8.GetString(array2, 0, num);
			}
			finally
			{
				if (array != null)
				{
					WbxmlReader.readerPool.Release(array);
				}
			}
			return @string;
		}

		// Token: 0x060018A2 RID: 6306 RVA: 0x00091E1C File Offset: 0x0009001C
		private SecureString ReadInlineSecureString()
		{
			long position = this.stream.Position;
			long num = position;
			while (this.ReadByte() != 0)
			{
				num += 1L;
			}
			this.stream.Position = position;
			GCHandle gchandle = default(GCHandle);
			SecureString result;
			try
			{
				int num2 = (int)(num - position) + 1;
				if (num2 > WbxmlReader.maxSecureStringLength)
				{
					throw new WbxmlException("Inline secure string is too long");
				}
				byte[] array = new byte[num2];
				gchandle = GCHandle.Alloc(array, GCHandleType.Pinned);
				this.ReadBytes(array);
				result = WbxmlReader.ConvertToSecureString(array);
			}
			finally
			{
				if (gchandle.IsAllocated)
				{
					gchandle.Free();
				}
			}
			return result;
		}

		// Token: 0x060018A3 RID: 6307 RVA: 0x00091EBC File Offset: 0x000900BC
		private int ReadMultiByteInteger()
		{
			uint num = 0U;
			byte b = this.ReadByte();
			int i = 1;
			while (i <= 5)
			{
				num = (num << 7) + (uint)(b & 127);
				if ((b & 128) != 0)
				{
					b = this.ReadByte();
					i++;
				}
				else
				{
					if (num > 2147483647U)
					{
						throw new WbxmlException("Multi-byte integer is too large");
					}
					return (int)num;
				}
			}
			throw new WbxmlException("Multi-byte integer is too large");
		}

		// Token: 0x060018A4 RID: 6308 RVA: 0x00091F18 File Offset: 0x00090118
		private string ReadOpaqueString()
		{
			int count = this.ReadMultiByteInteger();
			byte[] bytes = this.ReadBytes(count);
			return Encoding.UTF8.GetString(bytes);
		}

		// Token: 0x060018A5 RID: 6309 RVA: 0x00091F40 File Offset: 0x00090140
		private SecureString ReadOpaqueSecureString()
		{
			GCHandle gchandle = default(GCHandle);
			SecureString result;
			try
			{
				int num = this.ReadMultiByteInteger();
				if (num > WbxmlReader.maxSecureStringLength)
				{
					throw new WbxmlException("Opaque secure string is too long");
				}
				byte[] array = new byte[num];
				gchandle = GCHandle.Alloc(array, GCHandleType.Pinned);
				this.ReadBytes(array);
				result = WbxmlReader.ConvertToSecureString(array);
			}
			finally
			{
				if (gchandle.IsAllocated)
				{
					gchandle.Free();
				}
			}
			return result;
		}

		// Token: 0x060018A6 RID: 6310 RVA: 0x00091FB0 File Offset: 0x000901B0
		private int ReadTag(out bool hasAttributes, out bool hasContent)
		{
			byte b = this.ReadByte();
			int num = 0;
			while (b == 0)
			{
				this.currentTagPage = this.ReadByte();
				if (num++ > 5)
				{
					throw new WbxmlException("Multiple consecutive page change codes not allowed");
				}
				b = this.ReadByte();
			}
			if ((b & 63) < 5)
			{
				hasAttributes = false;
				hasContent = ((b & 64) != 0);
				return (int)b;
			}
			hasAttributes = ((b & 128) != 0);
			hasContent = ((b & 64) != 0);
			b &= 63;
			return (int)b | (int)this.currentTagPage << 8;
		}

		// Token: 0x060018A7 RID: 6311 RVA: 0x00092034 File Offset: 0x00090234
		private void SkipAttributes()
		{
			byte b = this.ReadByte();
			while (b != 1)
			{
				if (b == 0)
				{
					this.ReadByte();
				}
				else if (b == 4)
				{
					this.ReadMultiByteInteger();
				}
				else if (b == 3)
				{
					for (b = this.ReadByte(); b != 0; b = this.ReadByte())
					{
					}
				}
				else if (b == 131)
				{
					this.ReadMultiByteInteger();
				}
				else if (b == 195)
				{
					int count = this.ReadMultiByteInteger();
					this.SkipBytes(count);
				}
				else
				{
					if ((b & 63) < 5)
					{
						throw new WbxmlException("Unsupported control code");
					}
					throw new WbxmlException("Invalid WBXML - Attributes (potentially malicious)");
				}
			}
		}

		// Token: 0x04001182 RID: 4482
		private static XmlDocument errorDocument;

		// Token: 0x04001183 RID: 4483
		private static BufferPool readerPool = new BufferPool(1024, true);

		// Token: 0x04001184 RID: 4484
		private static int maxSecureStringLength = 256;

		// Token: 0x04001185 RID: 4485
		private readonly bool parseBlobAsByteArray;

		// Token: 0x04001186 RID: 4486
		private readonly DisposeTracker disposeTracker;

		// Token: 0x04001187 RID: 4487
		private byte currentTagPage;

		// Token: 0x04001188 RID: 4488
		private Stream stream;

		// Token: 0x04001189 RID: 4489
		private Stream underlyingStream;
	}
}
