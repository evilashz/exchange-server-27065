using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000944 RID: 2372
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[Serializable]
	public class XmlValueCredential
	{
		// Token: 0x170027D9 RID: 10201
		// (get) Token: 0x0600700B RID: 28683 RVA: 0x00177103 File Offset: 0x00175303
		// (set) Token: 0x0600700C RID: 28684 RVA: 0x0017710B File Offset: 0x0017530B
		[XmlElement(Order = 0)]
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

		// Token: 0x040048B5 RID: 18613
		private CredentialValue credentialField;
	}
}
