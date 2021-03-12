using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000903 RID: 2307
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[Serializable]
	public class XmlValueEncryptedExternalSecret
	{
		// Token: 0x17002767 RID: 10087
		// (get) Token: 0x06006EEE RID: 28398 RVA: 0x0017678D File Offset: 0x0017498D
		// (set) Token: 0x06006EEF RID: 28399 RVA: 0x00176795 File Offset: 0x00174995
		[XmlElement(Order = 0)]
		public EncryptedExternalSecretValue EncryptedExternalSecret
		{
			get
			{
				return this.encryptedExternalSecretField;
			}
			set
			{
				this.encryptedExternalSecretField = value;
			}
		}

		// Token: 0x04004813 RID: 18451
		private EncryptedExternalSecretValue encryptedExternalSecretField;
	}
}
