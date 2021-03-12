using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000F3 RID: 243
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[Serializable]
	public class DirSyncStatusValue
	{
		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000785 RID: 1925 RVA: 0x0001FD33 File Offset: 0x0001DF33
		// (set) Token: 0x06000786 RID: 1926 RVA: 0x0001FD3B File Offset: 0x0001DF3B
		[XmlAttribute]
		public string AttributeSetName
		{
			get
			{
				return this.attributeSetNameField;
			}
			set
			{
				this.attributeSetNameField = value;
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000787 RID: 1927 RVA: 0x0001FD44 File Offset: 0x0001DF44
		// (set) Token: 0x06000788 RID: 1928 RVA: 0x0001FD4C File Offset: 0x0001DF4C
		[XmlAttribute]
		public DirSyncState State
		{
			get
			{
				return this.stateField;
			}
			set
			{
				this.stateField = value;
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000789 RID: 1929 RVA: 0x0001FD55 File Offset: 0x0001DF55
		// (set) Token: 0x0600078A RID: 1930 RVA: 0x0001FD5D File Offset: 0x0001DF5D
		[XmlAttribute(DataType = "positiveInteger")]
		public string Version
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

		// Token: 0x040003D8 RID: 984
		private string attributeSetNameField;

		// Token: 0x040003D9 RID: 985
		private DirSyncState stateField;

		// Token: 0x040003DA RID: 986
		private string versionField;
	}
}
