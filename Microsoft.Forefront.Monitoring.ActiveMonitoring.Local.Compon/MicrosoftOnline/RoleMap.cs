using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x0200019E RID: 414
	[XmlInclude(typeof(PartnerRoleMap))]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://www.ccs.com/TestServices/")]
	[Serializable]
	public class RoleMap
	{
		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06000D68 RID: 3432 RVA: 0x00022F3D File Offset: 0x0002113D
		// (set) Token: 0x06000D69 RID: 3433 RVA: 0x00022F45 File Offset: 0x00021145
		public string PartnerGroup
		{
			get
			{
				return this.partnerGroupField;
			}
			set
			{
				this.partnerGroupField = value;
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06000D6A RID: 3434 RVA: 0x00022F4E File Offset: 0x0002114E
		// (set) Token: 0x06000D6B RID: 3435 RVA: 0x00022F56 File Offset: 0x00021156
		public string OnBehalfRole
		{
			get
			{
				return this.onBehalfRoleField;
			}
			set
			{
				this.onBehalfRoleField = value;
			}
		}

		// Token: 0x040006A5 RID: 1701
		private string partnerGroupField;

		// Token: 0x040006A6 RID: 1702
		private string onBehalfRoleField;
	}
}
