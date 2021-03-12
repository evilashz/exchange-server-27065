using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000303 RID: 771
	[XmlInclude(typeof(FractionalPageViewType))]
	[XmlInclude(typeof(ContactsViewType))]
	[XmlInclude(typeof(CalendarViewType))]
	[XmlInclude(typeof(SeekToConditionPageViewType))]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[XmlInclude(typeof(IndexedPageViewType))]
	[Serializable]
	public abstract class BasePagingType
	{
		// Token: 0x1700092A RID: 2346
		// (get) Token: 0x0600199B RID: 6555 RVA: 0x000287CA File Offset: 0x000269CA
		// (set) Token: 0x0600199C RID: 6556 RVA: 0x000287D2 File Offset: 0x000269D2
		[XmlAttribute]
		public int MaxEntriesReturned
		{
			get
			{
				return this.maxEntriesReturnedField;
			}
			set
			{
				this.maxEntriesReturnedField = value;
			}
		}

		// Token: 0x1700092B RID: 2347
		// (get) Token: 0x0600199D RID: 6557 RVA: 0x000287DB File Offset: 0x000269DB
		// (set) Token: 0x0600199E RID: 6558 RVA: 0x000287E3 File Offset: 0x000269E3
		[XmlIgnore]
		public bool MaxEntriesReturnedSpecified
		{
			get
			{
				return this.maxEntriesReturnedFieldSpecified;
			}
			set
			{
				this.maxEntriesReturnedFieldSpecified = value;
			}
		}

		// Token: 0x04001141 RID: 4417
		private int maxEntriesReturnedField;

		// Token: 0x04001142 RID: 4418
		private bool maxEntriesReturnedFieldSpecified;
	}
}
