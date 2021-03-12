using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Security.RightsManagement.SOAP.Template
{
	// Token: 0x020009F7 RID: 2551
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[XmlType(Namespace = "http://microsoft.com/DRM/TemplateDistributionService")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class TemplateInformation
	{
		// Token: 0x17000E29 RID: 3625
		// (get) Token: 0x060037AD RID: 14253 RVA: 0x0008D156 File Offset: 0x0008B356
		// (set) Token: 0x060037AE RID: 14254 RVA: 0x0008D15E File Offset: 0x0008B35E
		public string ServerPublicKey
		{
			get
			{
				return this.serverPublicKeyField;
			}
			set
			{
				this.serverPublicKeyField = value;
			}
		}

		// Token: 0x17000E2A RID: 3626
		// (get) Token: 0x060037AF RID: 14255 RVA: 0x0008D167 File Offset: 0x0008B367
		// (set) Token: 0x060037B0 RID: 14256 RVA: 0x0008D16F File Offset: 0x0008B36F
		public int GuidHashCount
		{
			get
			{
				return this.guidHashCountField;
			}
			set
			{
				this.guidHashCountField = value;
			}
		}

		// Token: 0x17000E2B RID: 3627
		// (get) Token: 0x060037B1 RID: 14257 RVA: 0x0008D178 File Offset: 0x0008B378
		// (set) Token: 0x060037B2 RID: 14258 RVA: 0x0008D180 File Offset: 0x0008B380
		[XmlElement("GuidHash")]
		public GuidHash[] GuidHash
		{
			get
			{
				return this.guidHashField;
			}
			set
			{
				this.guidHashField = value;
			}
		}

		// Token: 0x04002F35 RID: 12085
		private string serverPublicKeyField;

		// Token: 0x04002F36 RID: 12086
		private int guidHashCountField;

		// Token: 0x04002F37 RID: 12087
		private GuidHash[] guidHashField;
	}
}
