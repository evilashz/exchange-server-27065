using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000AD RID: 173
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public class AlternativeSecurityIdValue
	{
		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060005EC RID: 1516 RVA: 0x0001EF58 File Offset: 0x0001D158
		// (set) Token: 0x060005ED RID: 1517 RVA: 0x0001EF60 File Offset: 0x0001D160
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

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060005EE RID: 1518 RVA: 0x0001EF69 File Offset: 0x0001D169
		// (set) Token: 0x060005EF RID: 1519 RVA: 0x0001EF71 File Offset: 0x0001D171
		[XmlAttribute]
		public int Type
		{
			get
			{
				return this.typeField;
			}
			set
			{
				this.typeField = value;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060005F0 RID: 1520 RVA: 0x0001EF7A File Offset: 0x0001D17A
		// (set) Token: 0x060005F1 RID: 1521 RVA: 0x0001EF82 File Offset: 0x0001D182
		[XmlAttribute]
		public string IdentityProvider
		{
			get
			{
				return this.identityProviderField;
			}
			set
			{
				this.identityProviderField = value;
			}
		}

		// Token: 0x0400030A RID: 778
		private byte[] keyField;

		// Token: 0x0400030B RID: 779
		private int typeField;

		// Token: 0x0400030C RID: 780
		private string identityProviderField;
	}
}
