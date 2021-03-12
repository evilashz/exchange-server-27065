using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000B3 RID: 179
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public class XmlValueAsymmetricKey
	{
		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000609 RID: 1545 RVA: 0x0001F04C File Offset: 0x0001D24C
		// (set) Token: 0x0600060A RID: 1546 RVA: 0x0001F054 File Offset: 0x0001D254
		public AsymmetricKeyValue AsymmetricKey
		{
			get
			{
				return this.asymmetricKeyField;
			}
			set
			{
				this.asymmetricKeyField = value;
			}
		}

		// Token: 0x04000319 RID: 793
		private AsymmetricKeyValue asymmetricKeyField;
	}
}
