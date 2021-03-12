using System;
using System.Xml;

namespace Microsoft.Exchange.Net.WSTrust
{
	// Token: 0x02000B5F RID: 2911
	internal class SoapXmlMalformedException : WSTrustException
	{
		// Token: 0x06003E69 RID: 15977 RVA: 0x000A2ECE File Offset: 0x000A10CE
		public SoapXmlMalformedException(XmlElement context) : base(WSTrustStrings.SoapXmlMalformedException)
		{
			this.context = context;
		}

		// Token: 0x06003E6A RID: 15978 RVA: 0x000A2EE2 File Offset: 0x000A10E2
		public SoapXmlMalformedException(XmlElement context, XmlNodeDefinition expectedNode) : base(WSTrustStrings.SoapXmlMalformedException)
		{
			this.context = context;
			this.expectedNode = expectedNode;
		}

		// Token: 0x17000F5A RID: 3930
		// (get) Token: 0x06003E6B RID: 15979 RVA: 0x000A2EFD File Offset: 0x000A10FD
		public XmlElement Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x17000F5B RID: 3931
		// (get) Token: 0x06003E6C RID: 15980 RVA: 0x000A2F05 File Offset: 0x000A1105
		public XmlNodeDefinition ExpectedNode
		{
			get
			{
				return this.expectedNode;
			}
		}

		// Token: 0x06003E6D RID: 15981 RVA: 0x000A2F10 File Offset: 0x000A1110
		public override string ToString()
		{
			if (" Context=" + this.context == null)
			{
				return "<null>";
			}
			if (this.context.OuterXml + " ExpectedNode=" + this.expectedNode != null)
			{
				return this.expectedNode.ToString() + Environment.NewLine + base.ToString();
			}
			return "<null>";
		}

		// Token: 0x04003660 RID: 13920
		private XmlElement context;

		// Token: 0x04003661 RID: 13921
		private XmlNodeDefinition expectedNode;
	}
}
