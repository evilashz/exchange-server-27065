using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000F8 RID: 248
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class CredentialValue
	{
		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000797 RID: 1943 RVA: 0x0001FDCA File Offset: 0x0001DFCA
		// (set) Token: 0x06000798 RID: 1944 RVA: 0x0001FDD2 File Offset: 0x0001DFD2
		[XmlAttribute]
		public CredentialType CredentialType
		{
			get
			{
				return this.credentialTypeField;
			}
			set
			{
				this.credentialTypeField = value;
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000799 RID: 1945 RVA: 0x0001FDDB File Offset: 0x0001DFDB
		// (set) Token: 0x0600079A RID: 1946 RVA: 0x0001FDE3 File Offset: 0x0001DFE3
		[XmlAttribute]
		public string KeyStoreId
		{
			get
			{
				return this.keyStoreIdField;
			}
			set
			{
				this.keyStoreIdField = value;
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x0600079B RID: 1947 RVA: 0x0001FDEC File Offset: 0x0001DFEC
		// (set) Token: 0x0600079C RID: 1948 RVA: 0x0001FDF4 File Offset: 0x0001DFF4
		[XmlAttribute]
		public string KeyGroupId
		{
			get
			{
				return this.keyGroupIdField;
			}
			set
			{
				this.keyGroupIdField = value;
			}
		}

		// Token: 0x040003E5 RID: 997
		private CredentialType credentialTypeField;

		// Token: 0x040003E6 RID: 998
		private string keyStoreIdField;

		// Token: 0x040003E7 RID: 999
		private string keyGroupIdField;
	}
}
