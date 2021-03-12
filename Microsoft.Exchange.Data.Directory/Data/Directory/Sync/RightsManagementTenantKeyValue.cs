using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200092A RID: 2346
	[DesignerCategory("code")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public class RightsManagementTenantKeyValue
	{
		// Token: 0x170027AB RID: 10155
		// (get) Token: 0x06006F99 RID: 28569 RVA: 0x00176D37 File Offset: 0x00174F37
		// (set) Token: 0x06006F9A RID: 28570 RVA: 0x00176D3F File Offset: 0x00174F3F
		[XmlElement(DataType = "base64Binary", Order = 0)]
		public byte[] Key
		{
			get
			{
				return this.keyField;
			}
			set
			{
				this.keyField = value;
			}
		}

		// Token: 0x170027AC RID: 10156
		// (get) Token: 0x06006F9B RID: 28571 RVA: 0x00176D48 File Offset: 0x00174F48
		// (set) Token: 0x06006F9C RID: 28572 RVA: 0x00176D50 File Offset: 0x00174F50
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

		// Token: 0x170027AD RID: 10157
		// (get) Token: 0x06006F9D RID: 28573 RVA: 0x00176D59 File Offset: 0x00174F59
		// (set) Token: 0x06006F9E RID: 28574 RVA: 0x00176D61 File Offset: 0x00174F61
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

		// Token: 0x0400486F RID: 18543
		private byte[] keyField;

		// Token: 0x04004870 RID: 18544
		private string keyIdentifierField;

		// Token: 0x04004871 RID: 18545
		private int versionField;
	}
}
