using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000BD RID: 189
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[Serializable]
	public class RightsManagementTenantKeyValue
	{
		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000632 RID: 1586 RVA: 0x0001F1A5 File Offset: 0x0001D3A5
		// (set) Token: 0x06000633 RID: 1587 RVA: 0x0001F1AD File Offset: 0x0001D3AD
		[XmlElement(DataType = "base64Binary")]
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

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000634 RID: 1588 RVA: 0x0001F1B6 File Offset: 0x0001D3B6
		// (set) Token: 0x06000635 RID: 1589 RVA: 0x0001F1BE File Offset: 0x0001D3BE
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

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000636 RID: 1590 RVA: 0x0001F1C7 File Offset: 0x0001D3C7
		// (set) Token: 0x06000637 RID: 1591 RVA: 0x0001F1CF File Offset: 0x0001D3CF
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

		// Token: 0x04000335 RID: 821
		private byte[] keyField;

		// Token: 0x04000336 RID: 822
		private string keyIdentifierField;

		// Token: 0x04000337 RID: 823
		private int versionField;
	}
}
