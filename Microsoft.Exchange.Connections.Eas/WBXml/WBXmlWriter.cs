using System;
using System.IO;
using System.Text;
using System.Xml;
using Microsoft.Exchange.Connections.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.WBXml
{
	// Token: 0x02000083 RID: 131
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class WBXmlWriter : WBXmlBase
	{
		// Token: 0x06000287 RID: 647 RVA: 0x00009141 File Offset: 0x00007341
		internal WBXmlWriter(Stream stream)
		{
			if (stream == null)
			{
				throw new EasWBXmlTransientException("WBXmlWriter cannot take a null stream parameter");
			}
			this.stream = stream;
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000915E File Offset: 0x0000735E
		internal void WriteXmlDocument(XmlDocument doc)
		{
			if (doc == null)
			{
				throw new EasWBXmlTransientException("WBXmlWriter cannot take a null XmlDocument parameter");
			}
			this.WriteHeader();
			this.WriteXmlElement(doc.DocumentElement);
		}

		// Token: 0x06000289 RID: 649 RVA: 0x00009180 File Offset: 0x00007380
		internal void WriteXmlDocumentFromElement(XmlElement elem)
		{
			if (elem == null)
			{
				throw new EasWBXmlTransientException("WBXmlWriter cannot take a null XmlElement parameter");
			}
			this.WriteHeader();
			this.WriteXmlElement(elem);
		}

		// Token: 0x0600028A RID: 650 RVA: 0x000091A0 File Offset: 0x000073A0
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

		// Token: 0x0600028B RID: 651 RVA: 0x00009238 File Offset: 0x00007438
		private void WriteEndTag()
		{
			this.stream.WriteByte(1);
		}

		// Token: 0x0600028C RID: 652 RVA: 0x00009246 File Offset: 0x00007446
		private void WriteHeader()
		{
			this.currentTagPage = 0;
			this.WriteMultiByteInteger(3);
			this.WriteMultiByteInteger(1);
			this.WriteMultiByteInteger(106);
			this.WriteMultiByteInteger(0);
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000926C File Offset: 0x0000746C
		private void WriteMultiByteInteger(int value)
		{
			if (value > 127)
			{
				this.WriteMultiByteIntegerRecursive(value >> 7);
			}
			this.stream.WriteByte((byte)(value & 127));
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000928C File Offset: 0x0000748C
		private void WriteMultiByteIntegerRecursive(int value)
		{
			if (value > 127)
			{
				this.WriteMultiByteIntegerRecursive(value >> 7);
			}
			this.stream.WriteByte((byte)((value & 127) | 128));
		}

		// Token: 0x0600028F RID: 655 RVA: 0x000092B4 File Offset: 0x000074B4
		private void WriteOpaqueString(string str)
		{
			this.stream.WriteByte(195);
			byte[] bytes = Encoding.UTF8.GetBytes(str);
			this.WriteMultiByteInteger(bytes.Length);
			this.stream.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x06000290 RID: 656 RVA: 0x000092F6 File Offset: 0x000074F6
		private void WriteByteArray(byte[] data)
		{
			this.stream.WriteByte(195);
			this.WriteMultiByteInteger(data.Length);
			this.stream.Write(data, 0, data.Length);
		}

		// Token: 0x06000291 RID: 657 RVA: 0x00009324 File Offset: 0x00007524
		private void WriteString(string str)
		{
			this.stream.WriteByte(3);
			byte[] bytes = Encoding.UTF8.GetBytes(str);
			this.stream.Write(bytes, 0, bytes.Length);
			this.stream.WriteByte(0);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x00009368 File Offset: 0x00007568
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

		// Token: 0x06000293 RID: 659 RVA: 0x000093D0 File Offset: 0x000075D0
		private void WriteXmlElement(XmlElement elem)
		{
			int tag = WBXmlBase.Schema.GetTag(elem.NamespaceURI, elem.LocalName);
			bool flag = WBXmlWriter.ElementHasContent(elem);
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
						WBxmlBlobNode wbxmlBlobNode = xmlNode as WBxmlBlobNode;
						if (wbxmlBlobNode != null)
						{
							int tag2 = WBXmlBase.Schema.GetTag(wbxmlBlobNode.NamespaceURI, wbxmlBlobNode.LocalName);
							this.WriteTag(tag2, true, false);
							if (wbxmlBlobNode.ByteArray != null)
							{
								this.WriteByteArray(wbxmlBlobNode.ByteArray);
							}
							if (wbxmlBlobNode.Stream != null)
							{
								switch (wbxmlBlobNode.OriginalNodeType)
								{
								case XmlNodeType.Text:
									this.stream.WriteByte(3);
									break;
								case XmlNodeType.CDATA:
									this.stream.WriteByte(195);
									this.WriteMultiByteInteger((int)wbxmlBlobNode.StreamDataSize);
									break;
								}
								wbxmlBlobNode.CopyStream(this.stream);
								if (wbxmlBlobNode.OriginalNodeType == XmlNodeType.Text)
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
						throw new EasWBXmlTransientException(string.Format("WriteXmlElement encountered an invalid XmlNodeType of '{0}'", xmlNode.NodeType));
					}
				}
				this.WriteEndTag();
			}
		}

		// Token: 0x0400041C RID: 1052
		private const byte WBXmlVersion = 3;

		// Token: 0x0400041D RID: 1053
		private byte currentTagPage;

		// Token: 0x0400041E RID: 1054
		private Stream stream;
	}
}
