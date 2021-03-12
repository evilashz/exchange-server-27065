using System;
using System.Xml;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000DA0 RID: 3488
	internal class SoapSavvyReader : XmlReader
	{
		// Token: 0x06005886 RID: 22662 RVA: 0x001140CF File Offset: 0x001122CF
		public SoapSavvyReader(XmlReader innerReader, SoapSavvyReader.OnSoapSectionChange soapSectionChangeHandler)
		{
			this.innerReader = innerReader;
			this.onSoapSectionChange = soapSectionChangeHandler;
		}

		// Token: 0x1700144B RID: 5195
		// (get) Token: 0x06005887 RID: 22663 RVA: 0x001140E5 File Offset: 0x001122E5
		public override int AttributeCount
		{
			get
			{
				return this.innerReader.AttributeCount;
			}
		}

		// Token: 0x1700144C RID: 5196
		// (get) Token: 0x06005888 RID: 22664 RVA: 0x001140F2 File Offset: 0x001122F2
		public override string BaseURI
		{
			get
			{
				return this.innerReader.BaseURI;
			}
		}

		// Token: 0x06005889 RID: 22665 RVA: 0x001140FF File Offset: 0x001122FF
		public override void Close()
		{
			this.innerReader.Close();
		}

		// Token: 0x1700144D RID: 5197
		// (get) Token: 0x0600588A RID: 22666 RVA: 0x0011410C File Offset: 0x0011230C
		public override int Depth
		{
			get
			{
				return this.innerReader.Depth;
			}
		}

		// Token: 0x1700144E RID: 5198
		// (get) Token: 0x0600588B RID: 22667 RVA: 0x00114119 File Offset: 0x00112319
		public override bool EOF
		{
			get
			{
				return this.innerReader.EOF;
			}
		}

		// Token: 0x0600588C RID: 22668 RVA: 0x00114126 File Offset: 0x00112326
		public override string GetAttribute(int i)
		{
			return this.innerReader.GetAttribute(i);
		}

		// Token: 0x0600588D RID: 22669 RVA: 0x00114134 File Offset: 0x00112334
		public override string GetAttribute(string name, string namespaceURI)
		{
			return this.innerReader.GetAttribute(name, namespaceURI);
		}

		// Token: 0x0600588E RID: 22670 RVA: 0x00114143 File Offset: 0x00112343
		public override string GetAttribute(string name)
		{
			return this.innerReader.GetAttribute(name);
		}

		// Token: 0x1700144F RID: 5199
		// (get) Token: 0x0600588F RID: 22671 RVA: 0x00114151 File Offset: 0x00112351
		public override bool HasValue
		{
			get
			{
				return this.innerReader.HasValue;
			}
		}

		// Token: 0x17001450 RID: 5200
		// (get) Token: 0x06005890 RID: 22672 RVA: 0x0011415E File Offset: 0x0011235E
		public override bool IsEmptyElement
		{
			get
			{
				return this.innerReader.IsEmptyElement;
			}
		}

		// Token: 0x17001451 RID: 5201
		// (get) Token: 0x06005891 RID: 22673 RVA: 0x0011416B File Offset: 0x0011236B
		public override string LocalName
		{
			get
			{
				return this.innerReader.LocalName;
			}
		}

		// Token: 0x06005892 RID: 22674 RVA: 0x00114178 File Offset: 0x00112378
		public override string LookupNamespace(string prefix)
		{
			return this.innerReader.LookupNamespace(prefix);
		}

		// Token: 0x06005893 RID: 22675 RVA: 0x00114186 File Offset: 0x00112386
		public override bool MoveToAttribute(string name, string ns)
		{
			return this.innerReader.MoveToAttribute(name, ns);
		}

		// Token: 0x06005894 RID: 22676 RVA: 0x00114195 File Offset: 0x00112395
		public override bool MoveToAttribute(string name)
		{
			return this.innerReader.MoveToAttribute(name);
		}

		// Token: 0x06005895 RID: 22677 RVA: 0x001141A3 File Offset: 0x001123A3
		public override bool MoveToElement()
		{
			return this.innerReader.MoveToElement();
		}

		// Token: 0x06005896 RID: 22678 RVA: 0x001141B0 File Offset: 0x001123B0
		public override bool MoveToFirstAttribute()
		{
			return this.innerReader.MoveToFirstAttribute();
		}

		// Token: 0x06005897 RID: 22679 RVA: 0x001141BD File Offset: 0x001123BD
		public override bool MoveToNextAttribute()
		{
			return this.innerReader.MoveToNextAttribute();
		}

		// Token: 0x17001452 RID: 5202
		// (get) Token: 0x06005898 RID: 22680 RVA: 0x001141CA File Offset: 0x001123CA
		public override XmlNameTable NameTable
		{
			get
			{
				return this.innerReader.NameTable;
			}
		}

		// Token: 0x17001453 RID: 5203
		// (get) Token: 0x06005899 RID: 22681 RVA: 0x001141D7 File Offset: 0x001123D7
		public override string NamespaceURI
		{
			get
			{
				return this.innerReader.NamespaceURI;
			}
		}

		// Token: 0x17001454 RID: 5204
		// (get) Token: 0x0600589A RID: 22682 RVA: 0x001141E4 File Offset: 0x001123E4
		public override XmlNodeType NodeType
		{
			get
			{
				return this.innerReader.NodeType;
			}
		}

		// Token: 0x17001455 RID: 5205
		// (get) Token: 0x0600589B RID: 22683 RVA: 0x001141F1 File Offset: 0x001123F1
		public override string Prefix
		{
			get
			{
				return this.innerReader.Prefix;
			}
		}

		// Token: 0x0600589C RID: 22684 RVA: 0x00114200 File Offset: 0x00112400
		public override bool Read()
		{
			switch (this.soapSection)
			{
			case SoapSavvyReader.SoapSection.Unknown:
				if (this.LocalName == "Envelope" && this.IsSoapElement())
				{
					this.SetSoapSection(SoapSavvyReader.SoapSection.Envelope);
				}
				break;
			case SoapSavvyReader.SoapSection.Envelope:
				if (this.LocalName == "Header" && this.IsSoapElement())
				{
					this.SetSoapSection(SoapSavvyReader.SoapSection.Header);
				}
				else if (this.LocalName == "Body" && this.IsSoapElement())
				{
					this.SetSoapSection(SoapSavvyReader.SoapSection.Body);
				}
				break;
			case SoapSavvyReader.SoapSection.Header:
				if (this.LocalName == "Body" && this.IsSoapElement())
				{
					this.SetSoapSection(SoapSavvyReader.SoapSection.Body);
				}
				break;
			}
			return this.innerReader.Read();
		}

		// Token: 0x0600589D RID: 22685 RVA: 0x001142C0 File Offset: 0x001124C0
		private bool IsSoapElement()
		{
			return this.NamespaceURI.StartsWith("http://schemas.xmlsoap.org/soap/envelope/", StringComparison.OrdinalIgnoreCase) || this.NamespaceURI.StartsWith("http://www.w3.org/2003/05/soap-envelope", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0600589E RID: 22686 RVA: 0x001142E8 File Offset: 0x001124E8
		public override bool ReadAttributeValue()
		{
			return this.innerReader.ReadAttributeValue();
		}

		// Token: 0x17001456 RID: 5206
		// (get) Token: 0x0600589F RID: 22687 RVA: 0x001142F5 File Offset: 0x001124F5
		public override ReadState ReadState
		{
			get
			{
				return this.innerReader.ReadState;
			}
		}

		// Token: 0x060058A0 RID: 22688 RVA: 0x00114302 File Offset: 0x00112502
		public override void ResolveEntity()
		{
			this.innerReader.ResolveEntity();
		}

		// Token: 0x17001457 RID: 5207
		// (get) Token: 0x060058A1 RID: 22689 RVA: 0x0011430F File Offset: 0x0011250F
		public override string Value
		{
			get
			{
				return this.innerReader.Value;
			}
		}

		// Token: 0x17001458 RID: 5208
		// (get) Token: 0x060058A2 RID: 22690 RVA: 0x0011431C File Offset: 0x0011251C
		internal SoapSavvyReader.SoapSection Section
		{
			get
			{
				return this.soapSection;
			}
		}

		// Token: 0x060058A3 RID: 22691 RVA: 0x00114324 File Offset: 0x00112524
		private void SetSoapSection(SoapSavvyReader.SoapSection section)
		{
			this.soapSection = section;
			if (this.onSoapSectionChange != null)
			{
				this.onSoapSectionChange(this, this.soapSection);
			}
		}

		// Token: 0x04003132 RID: 12594
		private const string EnvelopeElementName = "Envelope";

		// Token: 0x04003133 RID: 12595
		private const string BodyElementName = "Body";

		// Token: 0x04003134 RID: 12596
		private const string HeaderElementName = "Header";

		// Token: 0x04003135 RID: 12597
		private XmlReader innerReader;

		// Token: 0x04003136 RID: 12598
		private SoapSavvyReader.SoapSection soapSection;

		// Token: 0x04003137 RID: 12599
		private SoapSavvyReader.OnSoapSectionChange onSoapSectionChange;

		// Token: 0x02000DA1 RID: 3489
		// (Invoke) Token: 0x060058A5 RID: 22693
		internal delegate void OnSoapSectionChange(SoapSavvyReader reader, SoapSavvyReader.SoapSection section);

		// Token: 0x02000DA2 RID: 3490
		internal enum SoapSection
		{
			// Token: 0x04003139 RID: 12601
			Unknown,
			// Token: 0x0400313A RID: 12602
			Envelope,
			// Token: 0x0400313B RID: 12603
			Header,
			// Token: 0x0400313C RID: 12604
			Body
		}
	}
}
