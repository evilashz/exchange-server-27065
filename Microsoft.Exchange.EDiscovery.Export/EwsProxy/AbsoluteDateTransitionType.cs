using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000300 RID: 768
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class AbsoluteDateTransitionType : TransitionType
	{
		// Token: 0x17000925 RID: 2341
		// (get) Token: 0x0600198E RID: 6542 RVA: 0x0002875D File Offset: 0x0002695D
		// (set) Token: 0x0600198F RID: 6543 RVA: 0x00028765 File Offset: 0x00026965
		public DateTime DateTime
		{
			get
			{
				return this.dateTimeField;
			}
			set
			{
				this.dateTimeField = value;
			}
		}

		// Token: 0x0400113C RID: 4412
		private DateTime dateTimeField;
	}
}
