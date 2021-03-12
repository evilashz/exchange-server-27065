using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020002FF RID: 767
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class RecurringDateTransitionType : RecurringTimeTransitionType
	{
		// Token: 0x17000924 RID: 2340
		// (get) Token: 0x0600198B RID: 6539 RVA: 0x00028744 File Offset: 0x00026944
		// (set) Token: 0x0600198C RID: 6540 RVA: 0x0002874C File Offset: 0x0002694C
		public int Day
		{
			get
			{
				return this.dayField;
			}
			set
			{
				this.dayField = value;
			}
		}

		// Token: 0x0400113B RID: 4411
		private int dayField;
	}
}
