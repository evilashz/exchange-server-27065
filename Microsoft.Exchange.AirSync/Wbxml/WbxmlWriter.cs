using System;
using System.IO;
using System.Text;
using System.Xml;

namespace Microsoft.Exchange.AirSync.Wbxml
{
	// Token: 0x020002A9 RID: 681
	internal class WbxmlWriter : WbxmlBase
	{
		// Token: 0x060018BC RID: 6332 RVA: 0x000925F9 File Offset: 0x000907F9
		public WbxmlWriter(Stream stream)
		{
			if (stream == null)
			{
				throw new WbxmlException("WbxmlWriter cannot take a null stream parameter");
			}
			this.stream = stream;
		}

		// Token: 0x060018BD RID: 6333 RVA: 0x00092616 File Offset: 0x00090816
		public void WriteXmlDocument(XmlDocument doc)
		{
			if (doc == null)
			{
				throw new WbxmlException("WbxmlWriter cannot take a null XmlDocument parameter");
			}
			this.WriteHeader();
			this.WriteXmlElement(doc.DocumentElement);
		}

		// Token: 0x060018BE RID: 6334 RVA: 0x00092638 File Offset: 0x00090838
		public void WriteXmlDocumentFromElement(XmlElement elem)
		{
			if (elem == null)
			{
				throw new WbxmlException("WbxmlWriter cannot take a null XmlElement parameter");
			}
			this.WriteHeader();
			this.WriteXmlElement(elem);
		}

		// Token: 0x060018BF RID: 6335 RVA: 0x00092658 File Offset: 0x00090858
		private static bool ElementHasContent(XmlElement elem)
		{
			foreach (object obj in elem.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.NodeType == XmlNodeType.Element || (xmlNode.NodeType == XmlNodeType.Text && !string.IsNullOrEmpty(xmlNode.Value)) || (xmlNode.NodeType == XmlNodeType.CDATA && !string.IsNullOrEmpty(xmlNode.Value)) || xmlNode.NodeType == XmlNodeType.Document)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060018C0 RID: 6336 RVA: 0x000926F0 File Offset: 0x000908F0
		private void WriteEndTag()
		{
			this.stream.WriteByte(1);
		}

		// Token: 0x060018C1 RID: 6337 RVA: 0x000926FE File Offset: 0x000908FE
		private void WriteHeader()
		{
			this.currentTagPage = 0;
			this.WriteMultiByteInteger(3);
			this.WriteMultiByteInteger(1);
			this.WriteMultiByteInteger(106);
			this.WriteMultiByteInteger(0);
		}

		// Token: 0x060018C2 RID: 6338 RVA: 0x00092724 File Offset: 0x00090924
		private void WriteMultiByteInteger(int value)
		{
			if (value > 127)
			{
				this.WriteMultiByteIntegerRecursive(value >> 7);
			}
			this.stream.WriteByte((byte)(value & 127));
		}

		// Token: 0x060018C3 RID: 6339 RVA: 0x00092744 File Offset: 0x00090944
		private void WriteMultiByteIntegerRecursive(int value)
		{
			if (value > 127)
			{
				this.WriteMultiByteIntegerRecursive(value >> 7);
			}
			this.stream.WriteByte((byte)((value & 127) | 128));
		}

		// Token: 0x060018C4 RID: 6340 RVA: 0x0009276C File Offset: 0x0009096C
		private void WriteOpaqueString(string str)
		{
			this.stream.WriteByte(195);
			byte[] bytes = Encoding.UTF8.GetBytes(str);
			this.WriteMultiByteInteger(bytes.Length);
			this.stream.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x060018C5 RID: 6341 RVA: 0x000927AE File Offset: 0x000909AE
		private void WriteByteArray(byte[] data)
		{
			this.stream.WriteByte(195);
			this.WriteMultiByteInteger(data.Length);
			this.stream.Write(data, 0, data.Length);
		}

		// Token: 0x060018C6 RID: 6342 RVA: 0x000927DC File Offset: 0x000909DC
		private void WriteString(string str)
		{
			this.stream.WriteByte(3);
			byte[] bytes = Encoding.UTF8.GetBytes(str);
			this.stream.Write(bytes, 0, bytes.Length);
			this.stream.WriteByte(0);
		}

		// Token: 0x060018C7 RID: 6343 RVA: 0x00092820 File Offset: 0x00090A20
		private void WriteTag(int tag, bool hasContent, bool hasAttributes)
		{
			byte b = (byte)((tag & 65280) >> 8);
			byte b2 = (byte)(tag & 63);
			if (hasContent)
			{
				b2 += 64;
			}
			if (hasAttributes)
			{
				b2 += 128;
			}
			if (b != this.currentTagPage)
			{
				this.stream.WriteByte(0);
				this.stream.WriteByte(b);
				this.currentTagPage = b;
			}
			this.stream.WriteByte(b2);
		}

		// Token: 0x060018C8 RID: 6344 RVA: 0x00092888 File Offset: 0x00090A88
		private void WriteXmlElement(XmlElement elem)
		{
			int tag = WbxmlBase.Schema.GetTag(elem.NamespaceURI, elem.LocalName);
			bool flag = WbxmlWriter.ElementHasContent(elem);
			this.WriteTag(tag, flag, false);
			if (flag)
			{
				foreach (object obj in elem.ChildNodes)
				{
					XmlNode xmlNode = (XmlNode)obj;
					switch (xmlNode.NodeType)
					{
					case XmlNodeType.Element:
					{
						AirSyncBlobXmlNode airSyncBlobXmlNode = xmlNode as AirSyncBlobXmlNode;
						if (airSyncBlobXmlNode != null)
						{
							int tag2 = WbxmlBase.Schema.GetTag(airSyncBlobXmlNode.NamespaceURI, airSyncBlobXmlNode.LocalName);
							this.WriteTag(tag2, true, false);
							if (airSyncBlobXmlNode.ByteArray != null)
							{
								this.WriteByteArray(airSyncBlobXmlNode.ByteArray);
							}
							if (airSyncBlobXmlNode.Stream != null)
							{
								switch (airSyncBlobXmlNode.OriginalNodeType)
								{
								case XmlNodeType.Text:
									this.stream.WriteByte(3);
									break;
								case XmlNodeType.CDATA:
									this.stream.WriteByte(195);
									this.WriteMultiByteInteger((int)airSyncBlobXmlNode.StreamDataSize);
									break;
								}
								airSyncBlobXmlNode.CopyStream(this.stream);
								if (airSyncBlobXmlNode.OriginalNodeType == XmlNodeType.Text)
								{
									this.stream.WriteByte(0);
								}
							}
							this.WriteEndTag();
						}
						else
						{
							this.WriteXmlElement((XmlElement)xmlNode);
						}
						break;
					}
					case XmlNodeType.Attribute:
						break;
					case XmlNodeType.Text:
						this.WriteString(xmlNode.Value);
						break;
					case XmlNodeType.CDATA:
						this.WriteOpaqueString(xmlNode.Value);
						break;
					default:
						throw new AirSyncPermanentException(false);
					}
				}
				this.WriteEndTag();
			}
		}

		// Token: 0x04001192 RID: 4498
		private const byte WbxmlVersion = 3;

		// Token: 0x04001193 RID: 4499
		private byte currentTagPage;

		// Token: 0x04001194 RID: 4500
		private Stream stream;
	}
}
