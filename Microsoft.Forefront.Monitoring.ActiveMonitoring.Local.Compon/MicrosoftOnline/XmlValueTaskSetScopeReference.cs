using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000CA RID: 202
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[Serializable]
	public class XmlValueTaskSetScopeReference
	{
		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000668 RID: 1640 RVA: 0x0001F36A File Offset: 0x0001D56A
		// (set) Token: 0x06000669 RID: 1641 RVA: 0x0001F372 File Offset: 0x0001D572
		public TaskSetScopeReferenceValue TaskSetScopeReference
		{
			get
			{
				return this.taskSetScopeReferenceField;
			}
			set
			{
				this.taskSetScopeReferenceField = value;
			}
		}

		// Token: 0x0400034E RID: 846
		private TaskSetScopeReferenceValue taskSetScopeReferenceField;
	}
}
