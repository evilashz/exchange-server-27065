using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200093E RID: 2366
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public class XmlValueProvisionedPlan
	{
		// Token: 0x170027CC RID: 10188
		// (get) Token: 0x06006FED RID: 28653 RVA: 0x00176FFF File Offset: 0x001751FF
		// (set) Token: 0x06006FEE RID: 28654 RVA: 0x00177007 File Offset: 0x00175207
		[XmlElement(Order = 0)]
		public ProvisionedPlanValue Plan
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

		// Token: 0x0400489E RID: 18590
		private ProvisionedPlanValue planField;
	}
}
