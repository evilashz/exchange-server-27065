using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200093C RID: 2364
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class XmlValueAssignedRoleSlice
	{
		// Token: 0x170027C9 RID: 10185
		// (get) Token: 0x06006FE5 RID: 28645 RVA: 0x00176FBC File Offset: 0x001751BC
		// (set) Token: 0x06006FE6 RID: 28646 RVA: 0x00176FC4 File Offset: 0x001751C4
		[XmlElement(Order = 0)]
		public AssignedRoleSliceValue AssignedRoleSlice
		{
			get
			{
				return this.assignedRoleSliceField;
			}
			set
			{
				this.assignedRoleSliceField = value;
			}
		}

		// Token: 0x0400489B RID: 18587
		private AssignedRoleSliceValue assignedRoleSliceField;
	}
}
