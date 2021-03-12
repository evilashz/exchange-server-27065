using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Security.RightsManagement.SOAP.Server
{
	// Token: 0x020009E7 RID: 2535
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[XmlType(Namespace = "http://microsoft.com/DRM/ServerService")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class ServerInfoRequest
	{
		// Token: 0x17000E17 RID: 3607
		// (get) Token: 0x06003764 RID: 14180 RVA: 0x0008CCBD File Offset: 0x0008AEBD
		// (set) Token: 0x06003765 RID: 14181 RVA: 0x0008CCC5 File Offset: 0x0008AEC5
		public ServerInfoType Type
		{
			get
			{
				return this.typeField;
			}
			set
			{
				this.typeField = value;
			}
		}

		// Token: 0x17000E18 RID: 3608
		// (get) Token: 0x06003766 RID: 14182 RVA: 0x0008CCCE File Offset: 0x0008AECE
		// (set) Token: 0x06003767 RID: 14183 RVA: 0x0008CCD6 File Offset: 0x0008AED6
		public string AdditionalInfo
		{
			get
			{
				return this.additionalInfoField;
			}
			set
			{
				this.additionalInfoField = value;
			}
		}

		// Token: 0x04002F04 RID: 12036
		private ServerInfoType typeField;

		// Token: 0x04002F05 RID: 12037
		private string additionalInfoField;
	}
}
