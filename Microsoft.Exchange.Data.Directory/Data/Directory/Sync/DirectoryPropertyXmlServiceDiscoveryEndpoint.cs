using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200088E RID: 2190
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DesignerCategory("code")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class DirectoryPropertyXmlServiceDiscoveryEndpoint : DirectoryPropertyXml
	{
		// Token: 0x06006D8A RID: 28042 RVA: 0x001759DA File Offset: 0x00173BDA
		public override IList GetValues()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06006D8B RID: 28043 RVA: 0x001759E1 File Offset: 0x00173BE1
		public sealed override void SetValues(IList values)
		{
			throw new NotImplementedException();
		}

		// Token: 0x1700270E RID: 9998
		// (get) Token: 0x06006D8C RID: 28044 RVA: 0x001759E8 File Offset: 0x00173BE8
		// (set) Token: 0x06006D8D RID: 28045 RVA: 0x001759F0 File Offset: 0x00173BF0
		[XmlElement("Value", Order = 0)]
		public XmlValueServiceDiscoveryEndpoint[] Value
		{
			get
			{
				return this.valueField;
			}
			set
			{
				this.valueField = value;
			}
		}

		// Token: 0x04004780 RID: 18304
		private XmlValueServiceDiscoveryEndpoint[] valueField;
	}
}
