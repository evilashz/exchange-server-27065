using System;
using System.Xml;
using System.Xml.Schema;

namespace Microsoft.Exchange.EDiscovery.Export.AutoDiscoverProxy
{
	// Token: 0x02000039 RID: 57
	internal class AutoDiscoverClientXmlReader : XmlReader
	{
		// Token: 0x060001D7 RID: 471 RVA: 0x0000790F File Offset: 0x00005B0F
		public AutoDiscoverClientXmlReader(XmlReader reader)
		{
			this.innerReader = reader;
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x0000791E File Offset: 0x00005B1E
		public override int AttributeCount
		{
			get
			{
				return this.innerReader.AttributeCount;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x0000792B File Offset: 0x00005B2B
		public override string BaseURI
		{
			get
			{
				return this.innerReader.BaseURI;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060001DA RID: 474 RVA: 0x00007938 File Offset: 0x00005B38
		public override int Depth
		{
			get
			{
				return this.innerReader.Depth;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060001DB RID: 475 RVA: 0x00007945 File Offset: 0x00005B45
		public override bool EOF
		{
			get
			{
				return this.innerReader.EOF;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060001DC RID: 476 RVA: 0x00007952 File Offset: 0x00005B52
		public override bool HasValue
		{
			get
			{
				return this.innerReader.HasValue;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060001DD RID: 477 RVA: 0x0000795F File Offset: 0x00005B5F
		public override bool IsEmptyElement
		{
			get
			{
				return this.innerReader.IsEmptyElement;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060001DE RID: 478 RVA: 0x0000796C File Offset: 0x00005B6C
		public override string LocalName
		{
			get
			{
				return this.innerReader.LocalName;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060001DF RID: 479 RVA: 0x00007979 File Offset: 0x00005B79
		public override XmlNameTable NameTable
		{
			get
			{
				return this.innerReader.NameTable;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x00007986 File Offset: 0x00005B86
		public override string NamespaceURI
		{
			get
			{
				return this.innerReader.NamespaceURI;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x00007993 File Offset: 0x00005B93
		public override XmlNodeType NodeType
		{
			get
			{
				return this.innerReader.NodeType;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x000079A0 File Offset: 0x00005BA0
		public override string Prefix
		{
			get
			{
				return this.innerReader.Prefix;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x000079AD File Offset: 0x00005BAD
		public override ReadState ReadState
		{
			get
			{
				return this.innerReader.ReadState;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x000079BA File Offset: 0x00005BBA
		public override string Value
		{
			get
			{
				return this.innerReader.Value;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x000079C7 File Offset: 0x00005BC7
		public override bool CanReadBinaryContent
		{
			get
			{
				return this.innerReader.CanReadBinaryContent;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x000079D4 File Offset: 0x00005BD4
		public override bool CanReadValueChunk
		{
			get
			{
				return this.innerReader.CanReadValueChunk;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x000079E1 File Offset: 0x00005BE1
		public override bool CanResolveEntity
		{
			get
			{
				return this.innerReader.CanResolveEntity;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x000079EE File Offset: 0x00005BEE
		public override bool HasAttributes
		{
			get
			{
				return this.innerReader.HasAttributes;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x000079FB File Offset: 0x00005BFB
		public override bool IsDefault
		{
			get
			{
				return this.innerReader.IsDefault;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060001EA RID: 490 RVA: 0x00007A08 File Offset: 0x00005C08
		public override Type ValueType
		{
			get
			{
				return this.innerReader.ValueType;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060001EB RID: 491 RVA: 0x00007A15 File Offset: 0x00005C15
		public override string XmlLang
		{
			get
			{
				return this.innerReader.XmlLang;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060001EC RID: 492 RVA: 0x00007A22 File Offset: 0x00005C22
		public override XmlSpace XmlSpace
		{
			get
			{
				return this.innerReader.XmlSpace;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060001ED RID: 493 RVA: 0x00007A2F File Offset: 0x00005C2F
		public override string Name
		{
			get
			{
				return this.innerReader.Name;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060001EE RID: 494 RVA: 0x00007A3C File Offset: 0x00005C3C
		public override char QuoteChar
		{
			get
			{
				return this.innerReader.QuoteChar;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060001EF RID: 495 RVA: 0x00007A49 File Offset: 0x00005C49
		public override IXmlSchemaInfo SchemaInfo
		{
			get
			{
				return this.innerReader.SchemaInfo;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x00007A56 File Offset: 0x00005C56
		public override XmlReaderSettings Settings
		{
			get
			{
				return this.innerReader.Settings;
			}
		}

		// Token: 0x170000BE RID: 190
		public override string this[int i]
		{
			get
			{
				return base[i];
			}
		}

		// Token: 0x170000BF RID: 191
		public override string this[string name, string namespaceURI]
		{
			get
			{
				return base[name, namespaceURI];
			}
		}

		// Token: 0x170000C0 RID: 192
		public override string this[string name]
		{
			get
			{
				return base[name];
			}
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00007A7F File Offset: 0x00005C7F
		public override object ReadContentAs(Type returnType, IXmlNamespaceResolver namespaceResolver)
		{
			return this.innerReader.ReadContentAs(returnType, namespaceResolver);
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00007A8E File Offset: 0x00005C8E
		public override int ReadContentAsBase64(byte[] buffer, int index, int count)
		{
			return this.innerReader.ReadContentAsBase64(buffer, index, count);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00007A9E File Offset: 0x00005C9E
		public override int ReadContentAsBinHex(byte[] buffer, int index, int count)
		{
			return this.innerReader.ReadContentAsBinHex(buffer, index, count);
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00007AAE File Offset: 0x00005CAE
		public override bool ReadContentAsBoolean()
		{
			return this.innerReader.ReadContentAsBoolean();
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00007ABB File Offset: 0x00005CBB
		public override DateTime ReadContentAsDateTime()
		{
			return this.innerReader.ReadContentAsDateTime();
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00007AC8 File Offset: 0x00005CC8
		public override decimal ReadContentAsDecimal()
		{
			return this.innerReader.ReadContentAsDecimal();
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00007AD5 File Offset: 0x00005CD5
		public override double ReadContentAsDouble()
		{
			return this.innerReader.ReadContentAsDouble();
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00007AE2 File Offset: 0x00005CE2
		public override float ReadContentAsFloat()
		{
			return this.innerReader.ReadContentAsFloat();
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00007AEF File Offset: 0x00005CEF
		public override int ReadContentAsInt()
		{
			return this.innerReader.ReadContentAsInt();
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00007AFC File Offset: 0x00005CFC
		public override long ReadContentAsLong()
		{
			return this.innerReader.ReadContentAsLong();
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00007B09 File Offset: 0x00005D09
		public override object ReadContentAsObject()
		{
			return this.innerReader.ReadContentAsObject();
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00007B16 File Offset: 0x00005D16
		public override string ReadContentAsString()
		{
			return this.innerReader.ReadContentAsString();
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00007B23 File Offset: 0x00005D23
		public override object ReadElementContentAs(Type returnType, IXmlNamespaceResolver namespaceResolver)
		{
			return this.innerReader.ReadElementContentAs(returnType, namespaceResolver);
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00007B32 File Offset: 0x00005D32
		public override object ReadElementContentAs(Type returnType, IXmlNamespaceResolver namespaceResolver, string localName, string namespaceURI)
		{
			return this.innerReader.ReadElementContentAs(returnType, namespaceResolver, localName, namespaceURI);
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00007B44 File Offset: 0x00005D44
		public override int ReadElementContentAsBase64(byte[] buffer, int index, int count)
		{
			return this.innerReader.ReadElementContentAsBase64(buffer, index, count);
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00007B54 File Offset: 0x00005D54
		public override int ReadElementContentAsBinHex(byte[] buffer, int index, int count)
		{
			return this.innerReader.ReadElementContentAsBinHex(buffer, index, count);
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00007B64 File Offset: 0x00005D64
		public override bool ReadElementContentAsBoolean()
		{
			return this.innerReader.ReadElementContentAsBoolean();
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00007B71 File Offset: 0x00005D71
		public override bool ReadElementContentAsBoolean(string localName, string namespaceURI)
		{
			return this.innerReader.ReadElementContentAsBoolean(localName, namespaceURI);
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00007B80 File Offset: 0x00005D80
		public override DateTime ReadElementContentAsDateTime()
		{
			return this.innerReader.ReadElementContentAsDateTime();
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00007B8D File Offset: 0x00005D8D
		public override DateTime ReadElementContentAsDateTime(string localName, string namespaceURI)
		{
			return this.innerReader.ReadElementContentAsDateTime(localName, namespaceURI);
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00007B9C File Offset: 0x00005D9C
		public override decimal ReadElementContentAsDecimal()
		{
			return this.innerReader.ReadElementContentAsDecimal();
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00007BA9 File Offset: 0x00005DA9
		public override decimal ReadElementContentAsDecimal(string localName, string namespaceURI)
		{
			return this.innerReader.ReadElementContentAsDecimal(localName, namespaceURI);
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00007BB8 File Offset: 0x00005DB8
		public override double ReadElementContentAsDouble()
		{
			return this.innerReader.ReadElementContentAsDouble();
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00007BC5 File Offset: 0x00005DC5
		public override double ReadElementContentAsDouble(string localName, string namespaceURI)
		{
			return this.innerReader.ReadElementContentAsDouble(localName, namespaceURI);
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00007BD4 File Offset: 0x00005DD4
		public override float ReadElementContentAsFloat()
		{
			return this.innerReader.ReadElementContentAsFloat();
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00007BE1 File Offset: 0x00005DE1
		public override float ReadElementContentAsFloat(string localName, string namespaceURI)
		{
			return this.innerReader.ReadElementContentAsFloat(localName, namespaceURI);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00007BF0 File Offset: 0x00005DF0
		public override int ReadElementContentAsInt()
		{
			return this.innerReader.ReadElementContentAsInt();
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00007BFD File Offset: 0x00005DFD
		public override int ReadElementContentAsInt(string localName, string namespaceURI)
		{
			return this.innerReader.ReadElementContentAsInt(localName, namespaceURI);
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00007C0C File Offset: 0x00005E0C
		public override long ReadElementContentAsLong()
		{
			return this.innerReader.ReadElementContentAsLong();
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00007C19 File Offset: 0x00005E19
		public override long ReadElementContentAsLong(string localName, string namespaceURI)
		{
			return this.innerReader.ReadElementContentAsLong(localName, namespaceURI);
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00007C28 File Offset: 0x00005E28
		public override object ReadElementContentAsObject()
		{
			return this.innerReader.ReadElementContentAsObject();
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00007C35 File Offset: 0x00005E35
		public override object ReadElementContentAsObject(string localName, string namespaceURI)
		{
			return this.innerReader.ReadElementContentAsObject(localName, namespaceURI);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00007C44 File Offset: 0x00005E44
		public override string ReadElementContentAsString()
		{
			return this.innerReader.ReadElementContentAsString();
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00007C51 File Offset: 0x00005E51
		public override string ReadElementContentAsString(string localName, string namespaceURI)
		{
			return this.innerReader.ReadElementContentAsString(localName, namespaceURI);
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00007C60 File Offset: 0x00005E60
		public override string ReadElementString()
		{
			return this.innerReader.ReadElementString();
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00007C6D File Offset: 0x00005E6D
		public override string ReadElementString(string localname, string ns)
		{
			return this.innerReader.ReadElementString(localname, ns);
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00007C7C File Offset: 0x00005E7C
		public override string ReadElementString(string name)
		{
			return this.innerReader.ReadElementString(name);
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00007C8A File Offset: 0x00005E8A
		public override void ReadEndElement()
		{
			this.innerReader.ReadEndElement();
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00007C97 File Offset: 0x00005E97
		public override string ReadInnerXml()
		{
			return this.innerReader.ReadInnerXml();
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00007CA4 File Offset: 0x00005EA4
		public override string ReadOuterXml()
		{
			return this.innerReader.ReadOuterXml();
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00007CB1 File Offset: 0x00005EB1
		public override void ReadStartElement()
		{
			this.innerReader.ReadStartElement();
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00007CBE File Offset: 0x00005EBE
		public override void ReadStartElement(string localname, string ns)
		{
			this.innerReader.ReadStartElement(localname, ns);
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00007CCD File Offset: 0x00005ECD
		public override void ReadStartElement(string name)
		{
			this.innerReader.ReadStartElement(name);
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00007CDB File Offset: 0x00005EDB
		public override string ReadString()
		{
			return this.innerReader.ReadString();
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00007CE8 File Offset: 0x00005EE8
		public override XmlReader ReadSubtree()
		{
			return this.innerReader.ReadSubtree();
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00007CF5 File Offset: 0x00005EF5
		public override bool ReadToDescendant(string localName, string namespaceURI)
		{
			return this.innerReader.ReadToDescendant(localName, namespaceURI);
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00007D04 File Offset: 0x00005F04
		public override bool ReadToDescendant(string name)
		{
			return this.innerReader.ReadToDescendant(name);
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00007D12 File Offset: 0x00005F12
		public override bool ReadToFollowing(string localName, string namespaceURI)
		{
			return this.innerReader.ReadToFollowing(localName, namespaceURI);
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00007D21 File Offset: 0x00005F21
		public override bool ReadToFollowing(string name)
		{
			return this.innerReader.ReadToFollowing(name);
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00007D2F File Offset: 0x00005F2F
		public override bool ReadToNextSibling(string localName, string namespaceURI)
		{
			return this.innerReader.ReadToNextSibling(localName, namespaceURI);
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00007D3E File Offset: 0x00005F3E
		public override bool ReadToNextSibling(string name)
		{
			return this.innerReader.ReadToNextSibling(name);
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00007D4C File Offset: 0x00005F4C
		public override int ReadValueChunk(char[] buffer, int index, int count)
		{
			return this.innerReader.ReadValueChunk(buffer, index, count);
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00007D5C File Offset: 0x00005F5C
		public override void Skip()
		{
			this.innerReader.Skip();
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00007D69 File Offset: 0x00005F69
		public override string LookupNamespace(string prefix)
		{
			return this.innerReader.LookupNamespace(prefix);
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00007D77 File Offset: 0x00005F77
		public override bool MoveToAttribute(string name, string ns)
		{
			return this.innerReader.MoveToAttribute(name, ns);
		}

		// Token: 0x0600022B RID: 555 RVA: 0x00007D86 File Offset: 0x00005F86
		public override bool MoveToAttribute(string name)
		{
			return this.innerReader.MoveToAttribute(name);
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00007D94 File Offset: 0x00005F94
		public override bool MoveToElement()
		{
			return this.innerReader.MoveToElement();
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00007DA4 File Offset: 0x00005FA4
		public override bool MoveToFirstAttribute()
		{
			bool flag = this.innerReader.MoveToFirstAttribute();
			if (flag && this.innerReader.NodeType == XmlNodeType.Attribute && this.innerReader.NamespaceURI == "http://schemas.xmlsoap.org/soap/envelope/" && this.innerReader.LocalName == "mustUnderstand")
			{
				return this.innerReader.MoveToNextAttribute();
			}
			return flag;
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00007E09 File Offset: 0x00006009
		public override bool MoveToNextAttribute()
		{
			return this.innerReader.MoveToNextAttribute();
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00007E16 File Offset: 0x00006016
		public override void Close()
		{
			this.innerReader.Close();
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00007E23 File Offset: 0x00006023
		public override string GetAttribute(int i)
		{
			return this.innerReader.GetAttribute(i);
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00007E31 File Offset: 0x00006031
		public override string GetAttribute(string name, string namespaceURI)
		{
			return this.innerReader.GetAttribute(name, namespaceURI);
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00007E40 File Offset: 0x00006040
		public override string GetAttribute(string name)
		{
			return this.innerReader.GetAttribute(name);
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00007E4E File Offset: 0x0000604E
		public override bool Read()
		{
			return this.innerReader.Read();
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00007E5B File Offset: 0x0000605B
		public override bool ReadAttributeValue()
		{
			return this.innerReader.ReadAttributeValue();
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00007E68 File Offset: 0x00006068
		public override void ResolveEntity()
		{
			this.innerReader.ResolveEntity();
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00007E75 File Offset: 0x00006075
		public override bool IsStartElement()
		{
			return this.innerReader.IsStartElement();
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00007E82 File Offset: 0x00006082
		public override bool IsStartElement(string localname, string ns)
		{
			return this.innerReader.IsStartElement(localname, ns);
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00007E91 File Offset: 0x00006091
		public override bool IsStartElement(string name)
		{
			return this.innerReader.IsStartElement(name);
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00007E9F File Offset: 0x0000609F
		public override void MoveToAttribute(int i)
		{
			this.innerReader.MoveToAttribute(i);
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00007EAD File Offset: 0x000060AD
		public override XmlNodeType MoveToContent()
		{
			return this.innerReader.MoveToContent();
		}

		// Token: 0x040000C0 RID: 192
		private XmlReader innerReader;
	}
}
