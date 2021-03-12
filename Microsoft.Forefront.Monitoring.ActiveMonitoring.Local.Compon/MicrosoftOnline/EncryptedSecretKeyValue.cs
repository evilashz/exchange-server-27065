using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000B4 RID: 180
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[Serializable]
	public class EncryptedSecretKeyValue
	{
		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600060C RID: 1548 RVA: 0x0001F065 File Offset: 0x0001D265
		// (set) Token: 0x0600060D RID: 1549 RVA: 0x0001F06D File Offset: 0x0001D26D
		[XmlElement(DataType = "base64Binary")]
		public byte[] EncryptedSecret
		{
			get
			{
				return this.encryptedSecretField;
			}
			set
			{
				this.encryptedSecretField = value;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600060E RID: 1550 RVA: 0x0001F076 File Offset: 0x0001D276
		// (set) Token: 0x0600060F RID: 1551 RVA: 0x0001F07E File Offset: 0x0001D27E
		[XmlAttribute]
		public string KeyIdentifier
		{
			get
			{
				return this.keyIdentifierField;
			}
			set
			{
				this.keyIdentifierField = value;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000610 RID: 1552 RVA: 0x0001F087 File Offset: 0x0001D287
		// (set) Token: 0x06000611 RID: 1553 RVA: 0x0001F08F File Offset: 0x0001D28F
		[XmlAttribute]
		public int Version
		{
			get
			{
				return this.versionField;
			}
			set
			{
				this.versionField = value;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000612 RID: 1554 RVA: 0x0001F098 File Offset: 0x0001D298
		// (set) Token: 0x06000613 RID: 1555 RVA: 0x0001F0A0 File Offset: 0x0001D2A0
		[XmlAttribute]
		public SecretKeyType SecretKeyType
		{
			get
			{
				return this.secretKeyTypeField;
			}
			set
			{
				this.secretKeyTypeField = value;
			}
		}

		// Token: 0x0400031A RID: 794
		private byte[] encryptedSecretField;

		// Token: 0x0400031B RID: 795
		private string keyIdentifierField;

		// Token: 0x0400031C RID: 796
		private int versionField;

		// Token: 0x0400031D RID: 797
		private SecretKeyType secretKeyTypeField;
	}
}
