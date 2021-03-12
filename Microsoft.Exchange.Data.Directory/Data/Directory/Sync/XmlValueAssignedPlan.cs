using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200094C RID: 2380
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[Serializable]
	public class XmlValueAssignedPlan
	{
		// Token: 0x170027E3 RID: 10211
		// (get) Token: 0x06007025 RID: 28709 RVA: 0x001771DD File Offset: 0x001753DD
		// (set) Token: 0x06007026 RID: 28710 RVA: 0x001771E5 File Offset: 0x001753E5
		[XmlElement(Order = 0)]
		public AssignedPlanValue Plan
		{
			get
			{
				return this.planField;
			}
			set
			{
				this.planField = value;
			}
		}

		// Token: 0x040048C6 RID: 18630
		private AssignedPlanValue planField;
	}
}
