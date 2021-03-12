using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020002FE RID: 766
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class RecurringDayTransitionType : RecurringTimeTransitionType
	{
		// Token: 0x17000922 RID: 2338
		// (get) Token: 0x06001986 RID: 6534 RVA: 0x0002871A File Offset: 0x0002691A
		// (set) Token: 0x06001987 RID: 6535 RVA: 0x00028722 File Offset: 0x00026922
		public string DayOfWeek
		{
			get
			{
				return this.dayOfWeekField;
			}
			set
			{
				this.dayOfWeekField = value;
			}
		}

		// Token: 0x17000923 RID: 2339
		// (get) Token: 0x06001988 RID: 6536 RVA: 0x0002872B File Offset: 0x0002692B
		// (set) Token: 0x06001989 RID: 6537 RVA: 0x00028733 File Offset: 0x00026933
		public int Occurrence
		{
			get
			{
				return this.occurrenceField;
			}
			set
			{
				this.occurrenceField = value;
			}
		}

		// Token: 0x04001139 RID: 4409
		private string dayOfWeekField;

		// Token: 0x0400113A RID: 4410
		private int occurrenceField;
	}
}
