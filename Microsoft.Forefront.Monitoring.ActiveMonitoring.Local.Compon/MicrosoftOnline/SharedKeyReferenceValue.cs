using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000AF RID: 175
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public class SharedKeyReferenceValue
	{
		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060005F6 RID: 1526 RVA: 0x0001EFAC File Offset: 0x0001D1AC
		// (set) Token: 0x060005F7 RID: 1527 RVA: 0x0001EFB4 File Offset: 0x0001D1B4
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

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060005F8 RID: 1528 RVA: 0x0001EFBD File Offset: 0x0001D1BD
		// (set) Token: 0x060005F9 RID: 1529 RVA: 0x0001EFC5 File Offset: 0x0001D1C5
		[XmlAttribute]
		public string KeyGroupId
		{
			get
			{
				return this.keyGroupIdField;
			}
			set
			{
				this.keyGroupIdField = value;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060005FA RID: 1530 RVA: 0x0001EFCE File Offset: 0x0001D1CE
		// (set) Token: 0x060005FB RID: 1531 RVA: 0x0001EFD6 File Offset: 0x0001D1D6
		[XmlAttribute]
		public string ContextId
		{
			get
			{
				return this.contextIdField;
			}
			set
			{
				this.contextIdField = value;
			}
		}

		// Token: 0x0400030E RID: 782
		private int versionField;

		// Token: 0x0400030F RID: 783
		private string keyGroupIdField;

		// Token: 0x04000310 RID: 784
		private string contextIdField;
	}
}
