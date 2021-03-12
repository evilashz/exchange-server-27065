using System;
using System.Xml;

namespace Microsoft.Exchange.Compliance.Xml
{
	// Token: 0x0200000F RID: 15
	internal class XmlDtdException : XmlException
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00004715 File Offset: 0x00002915
		public override string Message
		{
			get
			{
				return "For security reasons DTD is prohibited in this XML document.";
			}
		}
	}
}
