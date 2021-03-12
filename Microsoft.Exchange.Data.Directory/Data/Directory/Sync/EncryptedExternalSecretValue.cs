using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000904 RID: 2308
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DesignerCategory("code")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[Serializable]
	public class EncryptedExternalSecretValue
	{
		// Token: 0x17002768 RID: 10088
		// (get) Token: 0x06006EF1 RID: 28401 RVA: 0x001767A6 File Offset: 0x001749A6
		// (set) Token: 0x06006EF2 RID: 28402 RVA: 0x001767AE File Offset: 0x001749AE
		[XmlElement(DataType = "base64Binary", Order = 0)]
		public byte[] EncryptedExternalSecret
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

		// Token: 0x04004814 RID: 18452
		private byte[] encryptedExternalSecretField;
	}
}
