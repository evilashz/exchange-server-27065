using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000926 RID: 2342
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[Serializable]
	public class XmlValueRightsManagementUserKey
	{
		// Token: 0x170027A7 RID: 10151
		// (get) Token: 0x06006F8D RID: 28557 RVA: 0x00176CD3 File Offset: 0x00174ED3
		// (set) Token: 0x06006F8E RID: 28558 RVA: 0x00176CDB File Offset: 0x00174EDB
		[XmlElement(Order = 0)]
		public RightsManagementUserKeyValue RightsManagementUserKey
		{
			get
			{
				return this.rightsManagementUserKeyField;
			}
			set
			{
				this.rightsManagementUserKeyField = value;
			}
		}

		// Token: 0x0400486B RID: 18539
		private RightsManagementUserKeyValue rightsManagementUserKeyField;
	}
}
