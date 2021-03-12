using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000BF RID: 191
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class RightsManagementTenantConfigurationValue
	{
		// Token: 0x17000113 RID: 275
		// (get) Token: 0x0600063C RID: 1596 RVA: 0x0001F1F9 File Offset: 0x0001D3F9
		// (set) Token: 0x0600063D RID: 1597 RVA: 0x0001F201 File Offset: 0x0001D401
		[XmlElement(DataType = "base64Binary")]
		public byte[] Data
		{
			get
			{
				return this.dataField;
			}
			set
			{
				this.dataField = value;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x0600063E RID: 1598 RVA: 0x0001F20A File Offset: 0x0001D40A
		// (set) Token: 0x0600063F RID: 1599 RVA: 0x0001F212 File Offset: 0x0001D412
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

		// Token: 0x04000339 RID: 825
		private byte[] dataField;

		// Token: 0x0400033A RID: 826
		private int versionField;
	}
}
