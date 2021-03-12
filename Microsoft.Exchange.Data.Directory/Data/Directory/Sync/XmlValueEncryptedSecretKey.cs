using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200091F RID: 2335
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class XmlValueEncryptedSecretKey
	{
		// Token: 0x17002798 RID: 10136
		// (get) Token: 0x06006F6B RID: 28523 RVA: 0x00176BB4 File Offset: 0x00174DB4
		// (set) Token: 0x06006F6C RID: 28524 RVA: 0x00176BBC File Offset: 0x00174DBC
		[XmlElement(Order = 0)]
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

		// Token: 0x04004848 RID: 18504
		private EncryptedSecretKeyValue encryptedSecretKeyField;
	}
}
