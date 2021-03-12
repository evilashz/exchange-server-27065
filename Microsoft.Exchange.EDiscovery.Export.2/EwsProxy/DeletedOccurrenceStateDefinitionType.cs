using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020002BD RID: 701
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class DeletedOccurrenceStateDefinitionType : BaseCalendarItemStateDefinitionType
	{
		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x06001804 RID: 6148 RVA: 0x00027A62 File Offset: 0x00025C62
		// (set) Token: 0x06001805 RID: 6149 RVA: 0x00027A6A File Offset: 0x00025C6A
		public DateTime OccurrenceDate
		{
			get
			{
				return this.occurrenceDateField;
			}
			set
			{
				this.occurrenceDateField = value;
			}
		}

		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x06001806 RID: 6150 RVA: 0x00027A73 File Offset: 0x00025C73
		// (set) Token: 0x06001807 RID: 6151 RVA: 0x00027A7B File Offset: 0x00025C7B
		public bool IsOccurrencePresent
		{
			get
			{
				return this.isOccurrencePresentField;
			}
			set
			{
				this.isOccurrencePresentField = value;
			}
		}

		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x06001808 RID: 6152 RVA: 0x00027A84 File Offset: 0x00025C84
		// (set) Token: 0x06001809 RID: 6153 RVA: 0x00027A8C File Offset: 0x00025C8C
		[XmlIgnore]
		public bool IsOccurrencePresentSpecified
		{
			get
			{
				return this.isOccurrencePresentFieldSpecified;
			}
			set
			{
				this.isOccurrencePresentFieldSpecified = value;
			}
		}

		// Token: 0x04001050 RID: 4176
		private DateTime occurrenceDateField;

		// Token: 0x04001051 RID: 4177
		private bool isOccurrencePresentField;

		// Token: 0x04001052 RID: 4178
		private bool isOccurrencePresentFieldSpecified;
	}
}
