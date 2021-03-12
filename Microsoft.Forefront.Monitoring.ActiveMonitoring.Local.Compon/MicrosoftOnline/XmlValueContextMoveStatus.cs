using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x02000100 RID: 256
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[Serializable]
	public class XmlValueContextMoveStatus
	{
		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x060007BD RID: 1981 RVA: 0x0001FF0A File Offset: 0x0001E10A
		// (set) Token: 0x060007BE RID: 1982 RVA: 0x0001FF12 File Offset: 0x0001E112
		public ContextMoveStatusValue State
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

		// Token: 0x040003FC RID: 1020
		private ContextMoveStatusValue stateField;
	}
}
