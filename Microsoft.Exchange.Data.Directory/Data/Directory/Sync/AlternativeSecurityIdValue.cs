using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000919 RID: 2329
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[Serializable]
	public class AlternativeSecurityIdValue
	{
		// Token: 0x1700278C RID: 10124
		// (get) Token: 0x06006F4E RID: 28494 RVA: 0x00176AC0 File Offset: 0x00174CC0
		// (set) Token: 0x06006F4F RID: 28495 RVA: 0x00176AC8 File Offset: 0x00174CC8
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

		// Token: 0x1700278D RID: 10125
		// (get) Token: 0x06006F50 RID: 28496 RVA: 0x00176AD1 File Offset: 0x00174CD1
		// (set) Token: 0x06006F51 RID: 28497 RVA: 0x00176AD9 File Offset: 0x00174CD9
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

		// Token: 0x1700278E RID: 10126
		// (get) Token: 0x06006F52 RID: 28498 RVA: 0x00176AE2 File Offset: 0x00174CE2
		// (set) Token: 0x06006F53 RID: 28499 RVA: 0x00176AEA File Offset: 0x00174CEA
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

		// Token: 0x04004838 RID: 18488
		private byte[] keyField;

		// Token: 0x04004839 RID: 18489
		private int typeField;

		// Token: 0x0400483A RID: 18490
		private string identityProviderField;
	}
}
