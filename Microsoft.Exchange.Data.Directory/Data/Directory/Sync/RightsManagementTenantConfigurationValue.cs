using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200092C RID: 2348
	[DebuggerStepThrough]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DesignerCategory("code")]
	[Serializable]
	public class RightsManagementTenantConfigurationValue
	{
		// Token: 0x170027AF RID: 10159
		// (get) Token: 0x06006FA3 RID: 28579 RVA: 0x00176D8B File Offset: 0x00174F8B
		// (set) Token: 0x06006FA4 RID: 28580 RVA: 0x00176D93 File Offset: 0x00174F93
		[XmlElement(DataType = "base64Binary", Order = 0)]
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

		// Token: 0x170027B0 RID: 10160
		// (get) Token: 0x06006FA5 RID: 28581 RVA: 0x00176D9C File Offset: 0x00174F9C
		// (set) Token: 0x06006FA6 RID: 28582 RVA: 0x00176DA4 File Offset: 0x00174FA4
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

		// Token: 0x04004873 RID: 18547
		private byte[] dataField;

		// Token: 0x04004874 RID: 18548
		private int versionField;
	}
}
