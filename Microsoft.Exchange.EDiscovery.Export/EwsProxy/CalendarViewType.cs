using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000305 RID: 773
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class CalendarViewType : BasePagingType
	{
		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x060019A5 RID: 6565 RVA: 0x0002881E File Offset: 0x00026A1E
		// (set) Token: 0x060019A6 RID: 6566 RVA: 0x00028826 File Offset: 0x00026A26
		[XmlAttribute]
		public DateTime StartDate
		{
			get
			{
				return this.startDateField;
			}
			set
			{
				this.startDateField = value;
			}
		}

		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x060019A7 RID: 6567 RVA: 0x0002882F File Offset: 0x00026A2F
		// (set) Token: 0x060019A8 RID: 6568 RVA: 0x00028837 File Offset: 0x00026A37
		[XmlAttribute]
		public DateTime EndDate
		{
			get
			{
				return this.endDateField;
			}
			set
			{
				this.endDateField = value;
			}
		}

		// Token: 0x04001145 RID: 4421
		private DateTime startDateField;

		// Token: 0x04001146 RID: 4422
		private DateTime endDateField;
	}
}
