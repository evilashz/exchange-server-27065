using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200091D RID: 2333
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[Serializable]
	public class AsymmetricKeyValue
	{
		// Token: 0x17002794 RID: 10132
		// (get) Token: 0x06006F62 RID: 28514 RVA: 0x00176B68 File Offset: 0x00174D68
		// (set) Token: 0x06006F63 RID: 28515 RVA: 0x00176B70 File Offset: 0x00174D70
		[XmlElement("AsymmetricKeyValue", DataType = "base64Binary", Order = 0)]
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

		// Token: 0x17002795 RID: 10133
		// (get) Token: 0x06006F64 RID: 28516 RVA: 0x00176B79 File Offset: 0x00174D79
		// (set) Token: 0x06006F65 RID: 28517 RVA: 0x00176B81 File Offset: 0x00174D81
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

		// Token: 0x17002796 RID: 10134
		// (get) Token: 0x06006F66 RID: 28518 RVA: 0x00176B8A File Offset: 0x00174D8A
		// (set) Token: 0x06006F67 RID: 28519 RVA: 0x00176B92 File Offset: 0x00174D92
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

		// Token: 0x17002797 RID: 10135
		// (get) Token: 0x06006F68 RID: 28520 RVA: 0x00176B9B File Offset: 0x00174D9B
		// (set) Token: 0x06006F69 RID: 28521 RVA: 0x00176BA3 File Offset: 0x00174DA3
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

		// Token: 0x04004840 RID: 18496
		private byte[] asymmetricKeyValue1Field;

		// Token: 0x04004841 RID: 18497
		private string keyIdentifierField;

		// Token: 0x04004842 RID: 18498
		private int versionField;

		// Token: 0x04004843 RID: 18499
		private AsymmetricKeyType asymmetricKeyTypeField;
	}
}
