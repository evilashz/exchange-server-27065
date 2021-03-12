using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000B1 RID: 177
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class AsymmetricKeyValue
	{
		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000600 RID: 1536 RVA: 0x0001F000 File Offset: 0x0001D200
		// (set) Token: 0x06000601 RID: 1537 RVA: 0x0001F008 File Offset: 0x0001D208
		[XmlElement("AsymmetricKeyValue", DataType = "base64Binary")]
		public byte[] AsymmetricKeyValue1
		{
			get
			{
				return this.asymmetricKeyValue1Field;
			}
			set
			{
				this.asymmetricKeyValue1Field = value;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000602 RID: 1538 RVA: 0x0001F011 File Offset: 0x0001D211
		// (set) Token: 0x06000603 RID: 1539 RVA: 0x0001F019 File Offset: 0x0001D219
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

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000604 RID: 1540 RVA: 0x0001F022 File Offset: 0x0001D222
		// (set) Token: 0x06000605 RID: 1541 RVA: 0x0001F02A File Offset: 0x0001D22A
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

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000606 RID: 1542 RVA: 0x0001F033 File Offset: 0x0001D233
		// (set) Token: 0x06000607 RID: 1543 RVA: 0x0001F03B File Offset: 0x0001D23B
		[XmlAttribute]
		public AsymmetricKeyType AsymmetricKeyType
		{
			get
			{
				return this.asymmetricKeyTypeField;
			}
			set
			{
				this.asymmetricKeyTypeField = value;
			}
		}

		// Token: 0x04000312 RID: 786
		private byte[] asymmetricKeyValue1Field;

		// Token: 0x04000313 RID: 787
		private string keyIdentifierField;

		// Token: 0x04000314 RID: 788
		private int versionField;

		// Token: 0x04000315 RID: 789
		private AsymmetricKeyType asymmetricKeyTypeField;
	}
}
