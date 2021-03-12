using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020002FC RID: 764
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[XmlInclude(typeof(RecurringDateTransitionType))]
	[DebuggerStepThrough]
	[XmlInclude(typeof(RecurringDayTransitionType))]
	[XmlInclude(typeof(RecurringTimeTransitionType))]
	[XmlInclude(typeof(AbsoluteDateTransitionType))]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class TransitionType
	{
		// Token: 0x1700091F RID: 2335
		// (get) Token: 0x0600197E RID: 6526 RVA: 0x000286D7 File Offset: 0x000268D7
		// (set) Token: 0x0600197F RID: 6527 RVA: 0x000286DF File Offset: 0x000268DF
		public TransitionTargetType To
		{
			get
			{
				return this.toField;
			}
			set
			{
				this.toField = value;
			}
		}

		// Token: 0x04001136 RID: 4406
		private TransitionTargetType toField;
	}
}
