using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net.WSTrust;

namespace Microsoft.Exchange.SoapWebClient
{
	// Token: 0x020006E0 RID: 1760
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SoapHttpClientXmlWriter : XmlWriter
	{
		// Token: 0x06002152 RID: 8530 RVA: 0x00041A72 File Offset: 0x0003FC72
		public SoapHttpClientXmlWriter(XmlWriter writer, IEnumerable<XmlNamespaceDefinition> namespaceDefinitions)
		{
			if (namespaceDefinitions == null)
			{
				throw new ArgumentNullException("namespaceDefinitions");
			}
			this.innerWriter = writer;
			this.namespaceDefinitions = namespaceDefinitions;
		}

		// Token: 0x06002153 RID: 8531 RVA: 0x00041A96 File Offset: 0x0003FC96
		public override void Close()
		{
			this.innerWriter.Close();
		}

		// Token: 0x06002154 RID: 8532 RVA: 0x00041AA3 File Offset: 0x0003FCA3
		public override void Flush()
		{
			this.innerWriter.Flush();
		}

		// Token: 0x06002155 RID: 8533 RVA: 0x00041AB0 File Offset: 0x0003FCB0
		public override string LookupPrefix(string ns)
		{
			return this.innerWriter.LookupPrefix(ns);
		}

		// Token: 0x06002156 RID: 8534 RVA: 0x00041ABE File Offset: 0x0003FCBE
		public override void WriteBase64(byte[] buffer, int index, int count)
		{
			this.innerWriter.WriteBase64(buffer, index, count);
		}

		// Token: 0x06002157 RID: 8535 RVA: 0x00041ACE File Offset: 0x0003FCCE
		public override void WriteCData(string text)
		{
			this.innerWriter.WriteCData(text);
		}

		// Token: 0x06002158 RID: 8536 RVA: 0x00041ADC File Offset: 0x0003FCDC
		public override void WriteCharEntity(char ch)
		{
			this.innerWriter.WriteCharEntity(ch);
		}

		// Token: 0x06002159 RID: 8537 RVA: 0x00041AEA File Offset: 0x0003FCEA
		public override void WriteChars(char[] buffer, int index, int count)
		{
			this.innerWriter.WriteChars(buffer, index, count);
		}

		// Token: 0x0600215A RID: 8538 RVA: 0x00041AFA File Offset: 0x0003FCFA
		public override void WriteComment(string text)
		{
			this.innerWriter.WriteComment(text);
		}

		// Token: 0x0600215B RID: 8539 RVA: 0x00041B08 File Offset: 0x0003FD08
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			this.innerWriter.WriteDocType(name, pubid, sysid, subset);
		}

		// Token: 0x0600215C RID: 8540 RVA: 0x00041B1A File Offset: 0x0003FD1A
		public override void WriteEndAttribute()
		{
			this.innerWriter.WriteEndAttribute();
		}

		// Token: 0x0600215D RID: 8541 RVA: 0x00041B27 File Offset: 0x0003FD27
		public override void WriteEndDocument()
		{
			this.innerWriter.WriteEndDocument();
		}

		// Token: 0x0600215E RID: 8542 RVA: 0x00041B34 File Offset: 0x0003FD34
		public override void WriteEndElement()
		{
			this.innerWriter.WriteEndElement();
		}

		// Token: 0x0600215F RID: 8543 RVA: 0x00041B41 File Offset: 0x0003FD41
		public override void WriteEntityRef(string name)
		{
			this.innerWriter.WriteEntityRef(name);
		}

		// Token: 0x06002160 RID: 8544 RVA: 0x00041B4F File Offset: 0x0003FD4F
		public override void WriteFullEndElement()
		{
			this.innerWriter.WriteFullEndElement();
		}

		// Token: 0x06002161 RID: 8545 RVA: 0x00041B5C File Offset: 0x0003FD5C
		public override void WriteProcessingInstruction(string name, string text)
		{
			this.innerWriter.WriteProcessingInstruction(name, text);
		}

		// Token: 0x06002162 RID: 8546 RVA: 0x00041B6B File Offset: 0x0003FD6B
		public override void WriteRaw(string data)
		{
			this.innerWriter.WriteRaw(data);
		}

		// Token: 0x06002163 RID: 8547 RVA: 0x00041B79 File Offset: 0x0003FD79
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			this.innerWriter.WriteRaw(buffer, index, count);
		}

		// Token: 0x06002164 RID: 8548 RVA: 0x00041B89 File Offset: 0x0003FD89
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			this.innerWriter.WriteStartAttribute(prefix, localName, ns);
		}

		// Token: 0x06002165 RID: 8549 RVA: 0x00041B99 File Offset: 0x0003FD99
		public override void WriteStartDocument(bool standalone)
		{
			this.innerWriter.WriteStartDocument(standalone);
		}

		// Token: 0x06002166 RID: 8550 RVA: 0x00041BA7 File Offset: 0x0003FDA7
		public override void WriteStartDocument()
		{
			this.innerWriter.WriteStartDocument();
		}

		// Token: 0x06002167 RID: 8551 RVA: 0x00041BB4 File Offset: 0x0003FDB4
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			this.innerWriter.WriteStartElement(prefix, localName, ns);
			if (!this.haveWrittenFirstElement)
			{
				foreach (XmlNamespaceDefinition xmlNamespaceDefinition in this.namespaceDefinitions)
				{
					xmlNamespaceDefinition.WriteAttribute(this);
				}
				this.haveWrittenFirstElement = true;
			}
		}

		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x06002168 RID: 8552 RVA: 0x00041C20 File Offset: 0x0003FE20
		public override WriteState WriteState
		{
			get
			{
				return this.innerWriter.WriteState;
			}
		}

		// Token: 0x06002169 RID: 8553 RVA: 0x00041C2D File Offset: 0x0003FE2D
		public override void WriteString(string text)
		{
			this.innerWriter.WriteString(text);
		}

		// Token: 0x0600216A RID: 8554 RVA: 0x00041C3B File Offset: 0x0003FE3B
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			this.innerWriter.WriteSurrogateCharEntity(lowChar, highChar);
		}

		// Token: 0x0600216B RID: 8555 RVA: 0x00041C4A File Offset: 0x0003FE4A
		public override void WriteWhitespace(string ws)
		{
			this.innerWriter.WriteWhitespace(ws);
		}

		// Token: 0x170008AC RID: 2220
		// (get) Token: 0x0600216C RID: 8556 RVA: 0x00041C58 File Offset: 0x0003FE58
		public override XmlWriterSettings Settings
		{
			get
			{
				return this.innerWriter.Settings;
			}
		}

		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x0600216D RID: 8557 RVA: 0x00041C65 File Offset: 0x0003FE65
		public override string XmlLang
		{
			get
			{
				return this.innerWriter.XmlLang;
			}
		}

		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x0600216E RID: 8558 RVA: 0x00041C72 File Offset: 0x0003FE72
		public override XmlSpace XmlSpace
		{
			get
			{
				return this.innerWriter.XmlSpace;
			}
		}

		// Token: 0x04001F7A RID: 8058
		private IEnumerable<XmlNamespaceDefinition> namespaceDefinitions;

		// Token: 0x04001F7B RID: 8059
		private XmlWriter innerWriter;

		// Token: 0x04001F7C RID: 8060
		private bool haveWrittenFirstElement;
	}
}
