using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x020008AD RID: 2221
	[DesignerCategory("code")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[Serializable]
	public class ServiceEndpointValue
	{
		// Token: 0x1700272E RID: 10030
		// (get) Token: 0x06006E2C RID: 28204 RVA: 0x00176144 File Offset: 0x00174344
		// (set) Token: 0x06006E2D RID: 28205 RVA: 0x0017614C File Offset: 0x0017434C
		[XmlAnyElement(Order = 0)]
		public XmlElement[] Any
		{
			get
			{
				return this.anyField;
			}
			set
			{
				this.anyField = value;
			}
		}

		// Token: 0x1700272F RID: 10031
		// (get) Token: 0x06006E2E RID: 28206 RVA: 0x00176155 File Offset: 0x00174355
		// (set) Token: 0x06006E2F RID: 28207 RVA: 0x0017615D File Offset: 0x0017435D
		[XmlAttribute]
		public string Name
		{
			get
			{
				return this.nameField;
			}
			set
			{
				this.nameField = value;
			}
		}

		// Token: 0x17002730 RID: 10032
		// (get) Token: 0x06006E30 RID: 28208 RVA: 0x00176166 File Offset: 0x00174366
		// (set) Token: 0x06006E31 RID: 28209 RVA: 0x0017616E File Offset: 0x0017436E
		[XmlAttribute]
		public string Address
		{
			get
			{
				return this.addressField;
			}
			set
			{
				this.addressField = value;
			}
		}

		// Token: 0x040047B7 RID: 18359
		private XmlElement[] anyField;

		// Token: 0x040047B8 RID: 18360
		private string nameField;

		// Token: 0x040047B9 RID: 18361
		private string addressField;
	}
}
