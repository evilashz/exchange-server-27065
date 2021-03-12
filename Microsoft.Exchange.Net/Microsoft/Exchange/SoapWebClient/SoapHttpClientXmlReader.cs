using System;
using System.Xml;
using System.Xml.Schema;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient
{
	// Token: 0x020006DF RID: 1759
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SoapHttpClientXmlReader : XmlReader
	{
		// Token: 0x060020EC RID: 8428 RVA: 0x0004148B File Offset: 0x0003F68B
		public SoapHttpClientXmlReader(XmlReader reader)
		{
			this.innerReader = reader;
		}

		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x060020ED RID: 8429 RVA: 0x0004149A File Offset: 0x0003F69A
		public override int AttributeCount
		{
			get
			{
				return this.innerReader.AttributeCount;
			}
		}

		// Token: 0x1700088E RID: 2190
		// (get) Token: 0x060020EE RID: 8430 RVA: 0x000414A7 File Offset: 0x0003F6A7
		public override string BaseURI
		{
			get
			{
				return this.innerReader.BaseURI;
			}
		}

		// Token: 0x1700088F RID: 2191
		// (get) Token: 0x060020EF RID: 8431 RVA: 0x000414B4 File Offset: 0x0003F6B4
		public bool SupportsNormalization
		{
			get
			{
				return this.innerReader is XmlTextReader;
			}
		}

		// Token: 0x17000890 RID: 2192
		// (set) Token: 0x060020F0 RID: 8432 RVA: 0x000414C4 File Offset: 0x0003F6C4
		public bool Normalization
		{
			set
			{
				XmlTextReader xmlTextReader = this.innerReader as XmlTextReader;
				if (xmlTextReader != null)
				{
					xmlTextReader.Normalization = value;
					return;
				}
				throw new InvalidOperationException("This instance of SoapHttpClientXmlReader doesn't support normalization.");
			}
		}

		// Token: 0x060020F1 RID: 8433 RVA: 0x000414F2 File Offset: 0x0003F6F2
		public override void Close()
		{
			this.innerReader.Close();
		}

		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x060020F2 RID: 8434 RVA: 0x000414FF File Offset: 0x0003F6FF
		public override int Depth
		{
			get
			{
				return this.innerReader.Depth;
			}
		}

		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x060020F3 RID: 8435 RVA: 0x0004150C File Offset: 0x0003F70C
		public override bool EOF
		{
			get
			{
				return this.innerReader.EOF;
			}
		}

		// Token: 0x060020F4 RID: 8436 RVA: 0x00041519 File Offset: 0x0003F719
		public override string GetAttribute(int i)
		{
			return this.innerReader.GetAttribute(i);
		}

		// Token: 0x060020F5 RID: 8437 RVA: 0x00041527 File Offset: 0x0003F727
		public override string GetAttribute(string name, string namespaceURI)
		{
			return this.innerReader.GetAttribute(name, namespaceURI);
		}

		// Token: 0x060020F6 RID: 8438 RVA: 0x00041536 File Offset: 0x0003F736
		public override string GetAttribute(string name)
		{
			return this.innerReader.GetAttribute(name);
		}

		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x060020F7 RID: 8439 RVA: 0x00041544 File Offset: 0x0003F744
		public override bool HasValue
		{
			get
			{
				return this.innerReader.HasValue;
			}
		}

		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x060020F8 RID: 8440 RVA: 0x00041551 File Offset: 0x0003F751
		public override bool IsEmptyElement
		{
			get
			{
				return this.innerReader.IsEmptyElement;
			}
		}

		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x060020F9 RID: 8441 RVA: 0x0004155E File Offset: 0x0003F75E
		public override string LocalName
		{
			get
			{
				return this.innerReader.LocalName;
			}
		}

		// Token: 0x060020FA RID: 8442 RVA: 0x0004156B File Offset: 0x0003F76B
		public override string LookupNamespace(string prefix)
		{
			return this.innerReader.LookupNamespace(prefix);
		}

		// Token: 0x060020FB RID: 8443 RVA: 0x00041579 File Offset: 0x0003F779
		public override bool MoveToAttribute(string name, string ns)
		{
			return this.innerReader.MoveToAttribute(name, ns);
		}

		// Token: 0x060020FC RID: 8444 RVA: 0x00041588 File Offset: 0x0003F788
		public override bool MoveToAttribute(string name)
		{
			return this.innerReader.MoveToAttribute(name);
		}

		// Token: 0x060020FD RID: 8445 RVA: 0x00041596 File Offset: 0x0003F796
		public override bool MoveToElement()
		{
			return this.innerReader.MoveToElement();
		}

		// Token: 0x060020FE RID: 8446 RVA: 0x000415A4 File Offset: 0x0003F7A4
		public override bool MoveToFirstAttribute()
		{
			bool flag = this.innerReader.MoveToFirstAttribute();
			if (flag && this.innerReader.NodeType == XmlNodeType.Attribute && this.innerReader.NamespaceURI == "http://schemas.xmlsoap.org/soap/envelope/" && this.innerReader.LocalName == "mustUnderstand")
			{
				return this.innerReader.MoveToNextAttribute();
			}
			return flag;
		}

		// Token: 0x060020FF RID: 8447 RVA: 0x00041609 File Offset: 0x0003F809
		public override bool MoveToNextAttribute()
		{
			return this.innerReader.MoveToNextAttribute();
		}

		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x06002100 RID: 8448 RVA: 0x00041616 File Offset: 0x0003F816
		public override XmlNameTable NameTable
		{
			get
			{
				return this.innerReader.NameTable;
			}
		}

		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x06002101 RID: 8449 RVA: 0x00041623 File Offset: 0x0003F823
		public override string NamespaceURI
		{
			get
			{
				return this.innerReader.NamespaceURI;
			}
		}

		// Token: 0x17000898 RID: 2200
		// (get) Token: 0x06002102 RID: 8450 RVA: 0x00041630 File Offset: 0x0003F830
		public override XmlNodeType NodeType
		{
			get
			{
				return this.innerReader.NodeType;
			}
		}

		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x06002103 RID: 8451 RVA: 0x0004163D File Offset: 0x0003F83D
		public override string Prefix
		{
			get
			{
				return this.innerReader.Prefix;
			}
		}

		// Token: 0x06002104 RID: 8452 RVA: 0x0004164A File Offset: 0x0003F84A
		public override bool Read()
		{
			return this.innerReader.Read();
		}

		// Token: 0x06002105 RID: 8453 RVA: 0x00041657 File Offset: 0x0003F857
		public override bool ReadAttributeValue()
		{
			return this.innerReader.ReadAttributeValue();
		}

		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x06002106 RID: 8454 RVA: 0x00041664 File Offset: 0x0003F864
		public override ReadState ReadState
		{
			get
			{
				return this.innerReader.ReadState;
			}
		}

		// Token: 0x06002107 RID: 8455 RVA: 0x00041671 File Offset: 0x0003F871
		public override void ResolveEntity()
		{
			this.innerReader.ResolveEntity();
		}

		// Token: 0x1700089B RID: 2203
		// (get) Token: 0x06002108 RID: 8456 RVA: 0x0004167E File Offset: 0x0003F87E
		public override string Value
		{
			get
			{
				return this.innerReader.Value;
			}
		}

		// Token: 0x1700089C RID: 2204
		// (get) Token: 0x06002109 RID: 8457 RVA: 0x0004168B File Offset: 0x0003F88B
		public override bool CanReadBinaryContent
		{
			get
			{
				return this.innerReader.CanReadBinaryContent;
			}
		}

		// Token: 0x1700089D RID: 2205
		// (get) Token: 0x0600210A RID: 8458 RVA: 0x00041698 File Offset: 0x0003F898
		public override bool CanReadValueChunk
		{
			get
			{
				return this.innerReader.CanReadValueChunk;
			}
		}

		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x0600210B RID: 8459 RVA: 0x000416A5 File Offset: 0x0003F8A5
		public override bool CanResolveEntity
		{
			get
			{
				return this.innerReader.CanResolveEntity;
			}
		}

		// Token: 0x1700089F RID: 2207
		// (get) Token: 0x0600210C RID: 8460 RVA: 0x000416B2 File Offset: 0x0003F8B2
		public override bool HasAttributes
		{
			get
			{
				return this.innerReader.HasAttributes;
			}
		}

		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x0600210D RID: 8461 RVA: 0x000416BF File Offset: 0x0003F8BF
		public override bool IsDefault
		{
			get
			{
				return this.innerReader.IsDefault;
			}
		}

		// Token: 0x0600210E RID: 8462 RVA: 0x000416CC File Offset: 0x0003F8CC
		public override bool IsStartElement()
		{
			return this.innerReader.IsStartElement();
		}

		// Token: 0x0600210F RID: 8463 RVA: 0x000416D9 File Offset: 0x0003F8D9
		public override bool IsStartElement(string localname, string ns)
		{
			return this.innerReader.IsStartElement(localname, ns);
		}

		// Token: 0x06002110 RID: 8464 RVA: 0x000416E8 File Offset: 0x0003F8E8
		public override bool IsStartElement(string name)
		{
			return this.innerReader.IsStartElement(name);
		}

		// Token: 0x06002111 RID: 8465 RVA: 0x000416F6 File Offset: 0x0003F8F6
		public override void MoveToAttribute(int i)
		{
			this.innerReader.MoveToAttribute(i);
		}

		// Token: 0x06002112 RID: 8466 RVA: 0x00041704 File Offset: 0x0003F904
		public override XmlNodeType MoveToContent()
		{
			return this.innerReader.MoveToContent();
		}

		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x06002113 RID: 8467 RVA: 0x00041711 File Offset: 0x0003F911
		public override string Name
		{
			get
			{
				return this.innerReader.Name;
			}
		}

		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x06002114 RID: 8468 RVA: 0x0004171E File Offset: 0x0003F91E
		public override char QuoteChar
		{
			get
			{
				return this.innerReader.QuoteChar;
			}
		}

		// Token: 0x06002115 RID: 8469 RVA: 0x0004172B File Offset: 0x0003F92B
		public override object ReadContentAs(Type returnType, IXmlNamespaceResolver namespaceResolver)
		{
			return this.innerReader.ReadContentAs(returnType, namespaceResolver);
		}

		// Token: 0x06002116 RID: 8470 RVA: 0x0004173A File Offset: 0x0003F93A
		public override int ReadContentAsBase64(byte[] buffer, int index, int count)
		{
			return this.innerReader.ReadContentAsBase64(buffer, index, count);
		}

		// Token: 0x06002117 RID: 8471 RVA: 0x0004174A File Offset: 0x0003F94A
		public override int ReadContentAsBinHex(byte[] buffer, int index, int count)
		{
			return this.innerReader.ReadContentAsBinHex(buffer, index, count);
		}

		// Token: 0x06002118 RID: 8472 RVA: 0x0004175A File Offset: 0x0003F95A
		public override bool ReadContentAsBoolean()
		{
			return this.innerReader.ReadContentAsBoolean();
		}

		// Token: 0x06002119 RID: 8473 RVA: 0x00041767 File Offset: 0x0003F967
		public override DateTime ReadContentAsDateTime()
		{
			return this.innerReader.ReadContentAsDateTime();
		}

		// Token: 0x0600211A RID: 8474 RVA: 0x00041774 File Offset: 0x0003F974
		public override decimal ReadContentAsDecimal()
		{
			return this.innerReader.ReadContentAsDecimal();
		}

		// Token: 0x0600211B RID: 8475 RVA: 0x00041781 File Offset: 0x0003F981
		public override double ReadContentAsDouble()
		{
			return this.innerReader.ReadContentAsDouble();
		}

		// Token: 0x0600211C RID: 8476 RVA: 0x0004178E File Offset: 0x0003F98E
		public override float ReadContentAsFloat()
		{
			return this.innerReader.ReadContentAsFloat();
		}

		// Token: 0x0600211D RID: 8477 RVA: 0x0004179B File Offset: 0x0003F99B
		public override int ReadContentAsInt()
		{
			return this.innerReader.ReadContentAsInt();
		}

		// Token: 0x0600211E RID: 8478 RVA: 0x000417A8 File Offset: 0x0003F9A8
		public override long ReadContentAsLong()
		{
			return this.innerReader.ReadContentAsLong();
		}

		// Token: 0x0600211F RID: 8479 RVA: 0x000417B5 File Offset: 0x0003F9B5
		public override object ReadContentAsObject()
		{
			return this.innerReader.ReadContentAsObject();
		}

		// Token: 0x06002120 RID: 8480 RVA: 0x000417C2 File Offset: 0x0003F9C2
		public override string ReadContentAsString()
		{
			return this.innerReader.ReadContentAsString();
		}

		// Token: 0x06002121 RID: 8481 RVA: 0x000417CF File Offset: 0x0003F9CF
		public override object ReadElementContentAs(Type returnType, IXmlNamespaceResolver namespaceResolver)
		{
			return this.innerReader.ReadElementContentAs(returnType, namespaceResolver);
		}

		// Token: 0x06002122 RID: 8482 RVA: 0x000417DE File Offset: 0x0003F9DE
		public override object ReadElementContentAs(Type returnType, IXmlNamespaceResolver namespaceResolver, string localName, string namespaceURI)
		{
			return this.innerReader.ReadElementContentAs(returnType, namespaceResolver, localName, namespaceURI);
		}

		// Token: 0x06002123 RID: 8483 RVA: 0x000417F0 File Offset: 0x0003F9F0
		public override int ReadElementContentAsBase64(byte[] buffer, int index, int count)
		{
			return this.innerReader.ReadElementContentAsBase64(buffer, index, count);
		}

		// Token: 0x06002124 RID: 8484 RVA: 0x00041800 File Offset: 0x0003FA00
		public override int ReadElementContentAsBinHex(byte[] buffer, int index, int count)
		{
			return this.innerReader.ReadElementContentAsBinHex(buffer, index, count);
		}

		// Token: 0x06002125 RID: 8485 RVA: 0x00041810 File Offset: 0x0003FA10
		public override bool ReadElementContentAsBoolean()
		{
			return this.innerReader.ReadElementContentAsBoolean();
		}

		// Token: 0x06002126 RID: 8486 RVA: 0x0004181D File Offset: 0x0003FA1D
		public override bool ReadElementContentAsBoolean(string localName, string namespaceURI)
		{
			return this.innerReader.ReadElementContentAsBoolean(localName, namespaceURI);
		}

		// Token: 0x06002127 RID: 8487 RVA: 0x0004182C File Offset: 0x0003FA2C
		public override DateTime ReadElementContentAsDateTime()
		{
			return this.innerReader.ReadElementContentAsDateTime();
		}

		// Token: 0x06002128 RID: 8488 RVA: 0x00041839 File Offset: 0x0003FA39
		public override DateTime ReadElementContentAsDateTime(string localName, string namespaceURI)
		{
			return this.innerReader.ReadElementContentAsDateTime(localName, namespaceURI);
		}

		// Token: 0x06002129 RID: 8489 RVA: 0x00041848 File Offset: 0x0003FA48
		public override decimal ReadElementContentAsDecimal()
		{
			return this.innerReader.ReadElementContentAsDecimal();
		}

		// Token: 0x0600212A RID: 8490 RVA: 0x00041855 File Offset: 0x0003FA55
		public override decimal ReadElementContentAsDecimal(string localName, string namespaceURI)
		{
			return this.innerReader.ReadElementContentAsDecimal(localName, namespaceURI);
		}

		// Token: 0x0600212B RID: 8491 RVA: 0x00041864 File Offset: 0x0003FA64
		public override double ReadElementContentAsDouble()
		{
			return this.innerReader.ReadElementContentAsDouble();
		}

		// Token: 0x0600212C RID: 8492 RVA: 0x00041871 File Offset: 0x0003FA71
		public override double ReadElementContentAsDouble(string localName, string namespaceURI)
		{
			return this.innerReader.ReadElementContentAsDouble(localName, namespaceURI);
		}

		// Token: 0x0600212D RID: 8493 RVA: 0x00041880 File Offset: 0x0003FA80
		public override float ReadElementContentAsFloat()
		{
			return this.innerReader.ReadElementContentAsFloat();
		}

		// Token: 0x0600212E RID: 8494 RVA: 0x0004188D File Offset: 0x0003FA8D
		public override float ReadElementContentAsFloat(string localName, string namespaceURI)
		{
			return this.innerReader.ReadElementContentAsFloat(localName, namespaceURI);
		}

		// Token: 0x0600212F RID: 8495 RVA: 0x0004189C File Offset: 0x0003FA9C
		public override int ReadElementContentAsInt()
		{
			return this.innerReader.ReadElementContentAsInt();
		}

		// Token: 0x06002130 RID: 8496 RVA: 0x000418A9 File Offset: 0x0003FAA9
		public override int ReadElementContentAsInt(string localName, string namespaceURI)
		{
			return this.innerReader.ReadElementContentAsInt(localName, namespaceURI);
		}

		// Token: 0x06002131 RID: 8497 RVA: 0x000418B8 File Offset: 0x0003FAB8
		public override long ReadElementContentAsLong()
		{
			return this.innerReader.ReadElementContentAsLong();
		}

		// Token: 0x06002132 RID: 8498 RVA: 0x000418C5 File Offset: 0x0003FAC5
		public override long ReadElementContentAsLong(string localName, string namespaceURI)
		{
			return this.innerReader.ReadElementContentAsLong(localName, namespaceURI);
		}

		// Token: 0x06002133 RID: 8499 RVA: 0x000418D4 File Offset: 0x0003FAD4
		public override object ReadElementContentAsObject()
		{
			return this.innerReader.ReadElementContentAsObject();
		}

		// Token: 0x06002134 RID: 8500 RVA: 0x000418E1 File Offset: 0x0003FAE1
		public override object ReadElementContentAsObject(string localName, string namespaceURI)
		{
			return this.innerReader.ReadElementContentAsObject(localName, namespaceURI);
		}

		// Token: 0x06002135 RID: 8501 RVA: 0x000418F0 File Offset: 0x0003FAF0
		public override string ReadElementContentAsString()
		{
			return this.innerReader.ReadElementContentAsString();
		}

		// Token: 0x06002136 RID: 8502 RVA: 0x000418FD File Offset: 0x0003FAFD
		public override string ReadElementContentAsString(string localName, string namespaceURI)
		{
			return this.innerReader.ReadElementContentAsString(localName, namespaceURI);
		}

		// Token: 0x06002137 RID: 8503 RVA: 0x0004190C File Offset: 0x0003FB0C
		public override string ReadElementString()
		{
			return this.innerReader.ReadElementString();
		}

		// Token: 0x06002138 RID: 8504 RVA: 0x00041919 File Offset: 0x0003FB19
		public override string ReadElementString(string localname, string ns)
		{
			return this.innerReader.ReadElementString(localname, ns);
		}

		// Token: 0x06002139 RID: 8505 RVA: 0x00041928 File Offset: 0x0003FB28
		public override string ReadElementString(string name)
		{
			return this.innerReader.ReadElementString(name);
		}

		// Token: 0x0600213A RID: 8506 RVA: 0x00041936 File Offset: 0x0003FB36
		public override void ReadEndElement()
		{
			this.innerReader.ReadEndElement();
		}

		// Token: 0x0600213B RID: 8507 RVA: 0x00041943 File Offset: 0x0003FB43
		public override string ReadInnerXml()
		{
			return this.innerReader.ReadInnerXml();
		}

		// Token: 0x0600213C RID: 8508 RVA: 0x00041950 File Offset: 0x0003FB50
		public override string ReadOuterXml()
		{
			return this.innerReader.ReadOuterXml();
		}

		// Token: 0x0600213D RID: 8509 RVA: 0x0004195D File Offset: 0x0003FB5D
		public override void ReadStartElement()
		{
			this.innerReader.ReadStartElement();
		}

		// Token: 0x0600213E RID: 8510 RVA: 0x0004196A File Offset: 0x0003FB6A
		public override void ReadStartElement(string localname, string ns)
		{
			this.innerReader.ReadStartElement(localname, ns);
		}

		// Token: 0x0600213F RID: 8511 RVA: 0x00041979 File Offset: 0x0003FB79
		public override void ReadStartElement(string name)
		{
			this.innerReader.ReadStartElement(name);
		}

		// Token: 0x06002140 RID: 8512 RVA: 0x00041987 File Offset: 0x0003FB87
		public override string ReadString()
		{
			return this.innerReader.ReadString();
		}

		// Token: 0x06002141 RID: 8513 RVA: 0x00041994 File Offset: 0x0003FB94
		public override XmlReader ReadSubtree()
		{
			return this.innerReader.ReadSubtree();
		}

		// Token: 0x06002142 RID: 8514 RVA: 0x000419A1 File Offset: 0x0003FBA1
		public override bool ReadToDescendant(string localName, string namespaceURI)
		{
			return this.innerReader.ReadToDescendant(localName, namespaceURI);
		}

		// Token: 0x06002143 RID: 8515 RVA: 0x000419B0 File Offset: 0x0003FBB0
		public override bool ReadToDescendant(string name)
		{
			return this.innerReader.ReadToDescendant(name);
		}

		// Token: 0x06002144 RID: 8516 RVA: 0x000419BE File Offset: 0x0003FBBE
		public override bool ReadToFollowing(string localName, string namespaceURI)
		{
			return this.innerReader.ReadToFollowing(localName, namespaceURI);
		}

		// Token: 0x06002145 RID: 8517 RVA: 0x000419CD File Offset: 0x0003FBCD
		public override bool ReadToFollowing(string name)
		{
			return this.innerReader.ReadToFollowing(name);
		}

		// Token: 0x06002146 RID: 8518 RVA: 0x000419DB File Offset: 0x0003FBDB
		public override bool ReadToNextSibling(string localName, string namespaceURI)
		{
			return this.innerReader.ReadToNextSibling(localName, namespaceURI);
		}

		// Token: 0x06002147 RID: 8519 RVA: 0x000419EA File Offset: 0x0003FBEA
		public override bool ReadToNextSibling(string name)
		{
			return this.innerReader.ReadToNextSibling(name);
		}

		// Token: 0x06002148 RID: 8520 RVA: 0x000419F8 File Offset: 0x0003FBF8
		public override int ReadValueChunk(char[] buffer, int index, int count)
		{
			return this.innerReader.ReadValueChunk(buffer, index, count);
		}

		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x06002149 RID: 8521 RVA: 0x00041A08 File Offset: 0x0003FC08
		public override IXmlSchemaInfo SchemaInfo
		{
			get
			{
				return this.innerReader.SchemaInfo;
			}
		}

		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x0600214A RID: 8522 RVA: 0x00041A15 File Offset: 0x0003FC15
		public override XmlReaderSettings Settings
		{
			get
			{
				return this.innerReader.Settings;
			}
		}

		// Token: 0x0600214B RID: 8523 RVA: 0x00041A22 File Offset: 0x0003FC22
		public override void Skip()
		{
			this.innerReader.Skip();
		}

		// Token: 0x170008A5 RID: 2213
		public override string this[int i]
		{
			get
			{
				return base[i];
			}
		}

		// Token: 0x170008A6 RID: 2214
		public override string this[string name, string namespaceURI]
		{
			get
			{
				return base[name, namespaceURI];
			}
		}

		// Token: 0x170008A7 RID: 2215
		public override string this[string name]
		{
			get
			{
				return base[name];
			}
		}

		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x0600214F RID: 8527 RVA: 0x00041A4B File Offset: 0x0003FC4B
		public override Type ValueType
		{
			get
			{
				return this.innerReader.ValueType;
			}
		}

		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x06002150 RID: 8528 RVA: 0x00041A58 File Offset: 0x0003FC58
		public override string XmlLang
		{
			get
			{
				return this.innerReader.XmlLang;
			}
		}

		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x06002151 RID: 8529 RVA: 0x00041A65 File Offset: 0x0003FC65
		public override XmlSpace XmlSpace
		{
			get
			{
				return this.innerReader.XmlSpace;
			}
		}

		// Token: 0x04001F79 RID: 8057
		private XmlReader innerReader;
	}
}
