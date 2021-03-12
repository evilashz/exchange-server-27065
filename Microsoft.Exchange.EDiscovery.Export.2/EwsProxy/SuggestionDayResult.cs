using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000277 RID: 631
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class SuggestionDayResult
	{
		// Token: 0x1700084E RID: 2126
		// (get) Token: 0x06001768 RID: 5992 RVA: 0x00027556 File Offset: 0x00025756
		// (set) Token: 0x06001769 RID: 5993 RVA: 0x0002755E File Offset: 0x0002575E
		public DateTime Date
		{
			get
			{
				return this.dateField;
			}
			set
			{
				this.dateField = value;
			}
		}

		// Token: 0x1700084F RID: 2127
		// (get) Token: 0x0600176A RID: 5994 RVA: 0x00027567 File Offset: 0x00025767
		// (set) Token: 0x0600176B RID: 5995 RVA: 0x0002756F File Offset: 0x0002576F
		public SuggestionQuality DayQuality
		{
			get
			{
				return this.dayQualityField;
			}
			set
			{
				this.dayQualityField = value;
			}
		}

		// Token: 0x17000850 RID: 2128
		// (get) Token: 0x0600176C RID: 5996 RVA: 0x00027578 File Offset: 0x00025778
		// (set) Token: 0x0600176D RID: 5997 RVA: 0x00027580 File Offset: 0x00025780
		[XmlArrayItem(IsNullable = false)]
		public Suggestion[] SuggestionArray
		{
			get
			{
				return this.suggestionArrayField;
			}
			set
			{
				this.suggestionArrayField = value;
			}
		}

		// Token: 0x04000FD3 RID: 4051
		private DateTime dateField;

		// Token: 0x04000FD4 RID: 4052
		private SuggestionQuality dayQualityField;

		// Token: 0x04000FD5 RID: 4053
		private Suggestion[] suggestionArrayField;
	}
}
