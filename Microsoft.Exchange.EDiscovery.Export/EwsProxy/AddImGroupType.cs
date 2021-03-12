using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000327 RID: 807
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class AddImGroupType : BaseRequestType
	{
		// Token: 0x17000968 RID: 2408
		// (get) Token: 0x06001A36 RID: 6710 RVA: 0x00028CE0 File Offset: 0x00026EE0
		// (set) Token: 0x06001A37 RID: 6711 RVA: 0x00028CE8 File Offset: 0x00026EE8
		public string DisplayName
		{
			get
			{
				return this.displayNameField;
			}
			set
			{
				this.displayNameField = value;
			}
		}

		// Token: 0x04001197 RID: 4503
		private string displayNameField;
	}
}
