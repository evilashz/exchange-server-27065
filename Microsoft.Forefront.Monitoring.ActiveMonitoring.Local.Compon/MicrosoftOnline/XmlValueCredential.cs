using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000FA RID: 250
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[Serializable]
	public class XmlValueCredential
	{
		// Token: 0x170001AA RID: 426
		// (get) Token: 0x0600079E RID: 1950 RVA: 0x0001FE05 File Offset: 0x0001E005
		// (set) Token: 0x0600079F RID: 1951 RVA: 0x0001FE0D File Offset: 0x0001E00D
		public CredentialValue Credential
		{
			get
			{
				return this.credentialField;
			}
			set
			{
				this.credentialField = value;
			}
		}

		// Token: 0x040003EB RID: 1003
		private CredentialValue credentialField;
	}
}
