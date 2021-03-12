using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000B6 RID: 182
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public class XmlValueEncryptedSecretKey
	{
		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000615 RID: 1557 RVA: 0x0001F0B1 File Offset: 0x0001D2B1
		// (set) Token: 0x06000616 RID: 1558 RVA: 0x0001F0B9 File Offset: 0x0001D2B9
		public EncryptedSecretKeyValue EncryptedSecretKey
		{
			get
			{
				return this.encryptedSecretKeyField;
			}
			set
			{
				this.encryptedSecretKeyField = value;
			}
		}

		// Token: 0x04000321 RID: 801
		private EncryptedSecretKeyValue encryptedSecretKeyField;
	}
}
