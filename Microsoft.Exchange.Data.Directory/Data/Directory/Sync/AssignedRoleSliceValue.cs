using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200093D RID: 2365
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class AssignedRoleSliceValue
	{
		// Token: 0x170027CA RID: 10186
		// (get) Token: 0x06006FE8 RID: 28648 RVA: 0x00176FD5 File Offset: 0x001751D5
		// (set) Token: 0x06006FE9 RID: 28649 RVA: 0x00176FDD File Offset: 0x001751DD
		[XmlAttribute]
		public string RoleName
		{
			get
			{
				return this.roleNameField;
			}
			set
			{
				this.roleNameField = value;
			}
		}

		// Token: 0x170027CB RID: 10187
		// (get) Token: 0x06006FEA RID: 28650 RVA: 0x00176FE6 File Offset: 0x001751E6
		// (set) Token: 0x06006FEB RID: 28651 RVA: 0x00176FEE File Offset: 0x001751EE
		[XmlAttribute]
		public int SliceId
		{
			get
			{
				return this.sliceIdField;
			}
			set
			{
				this.sliceIdField = value;
			}
		}

		// Token: 0x0400489C RID: 18588
		private string roleNameField;

		// Token: 0x0400489D RID: 18589
		private int sliceIdField;
	}
}
