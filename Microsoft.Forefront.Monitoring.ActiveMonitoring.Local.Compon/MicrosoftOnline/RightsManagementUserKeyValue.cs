using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000BB RID: 187
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DesignerCategory("code")]
	[Serializable]
	public class RightsManagementUserKeyValue
	{
		// Token: 0x1700010C RID: 268
		// (get) Token: 0x0600062A RID: 1578 RVA: 0x0001F162 File Offset: 0x0001D362
		// (set) Token: 0x0600062B RID: 1579 RVA: 0x0001F16A File Offset: 0x0001D36A
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

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600062C RID: 1580 RVA: 0x0001F173 File Offset: 0x0001D373
		// (set) Token: 0x0600062D RID: 1581 RVA: 0x0001F17B File Offset: 0x0001D37B
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

		// Token: 0x04000332 RID: 818
		private byte[] keyField;

		// Token: 0x04000333 RID: 819
		private int versionField;
	}
}
