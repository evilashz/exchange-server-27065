using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200032E RID: 814
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class UninstallAppType : BaseRequestType
	{
		// Token: 0x17000976 RID: 2422
		// (get) Token: 0x06001A58 RID: 6744 RVA: 0x00028DFE File Offset: 0x00026FFE
		// (set) Token: 0x06001A59 RID: 6745 RVA: 0x00028E06 File Offset: 0x00027006
		public string ID
		{
			get
			{
				return this.idField;
			}
			set
			{
				this.idField = value;
			}
		}

		// Token: 0x040011AA RID: 4522
		private string idField;
	}
}
