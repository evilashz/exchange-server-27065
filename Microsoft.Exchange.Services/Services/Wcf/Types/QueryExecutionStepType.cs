using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Search.Query;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B32 RID: 2866
	[XmlType(TypeName = "QueryExecutionStepType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class QueryExecutionStepType
	{
		// Token: 0x06005141 RID: 20801 RVA: 0x0010A436 File Offset: 0x00108636
		internal QueryExecutionStepType()
		{
		}

		// Token: 0x06005142 RID: 20802 RVA: 0x0010A440 File Offset: 0x00108640
		internal QueryExecutionStepType(QueryExecutionStep queryExecutionStep)
		{
			this.StartTime = queryExecutionStep.StartTime.ToString("MM/dd/yyyy hh:mm:ss.fff tt", CultureInfo.InvariantCulture);
			this.StepTime = (double)((queryExecutionStep.EndTime.Ticks - queryExecutionStep.StartTime.Ticks) / 10000L);
			this.StepType = queryExecutionStep.StepType.ToString();
			if (queryExecutionStep.AdditionalStatistics != null)
			{
				List<string> list = new List<string>(queryExecutionStep.AdditionalStatistics.Count);
				foreach (KeyValuePair<string, object> keyValuePair in queryExecutionStep.AdditionalStatistics)
				{
					if (keyValuePair.Value != null)
					{
						list.Add(string.Format("{0}=>{1}", keyValuePair.Key, keyValuePair.Value));
					}
				}
				this.AdditionalEntries = list.ToArray();
			}
		}

		// Token: 0x17001385 RID: 4997
		// (get) Token: 0x06005143 RID: 20803 RVA: 0x0010A540 File Offset: 0x00108740
		// (set) Token: 0x06005144 RID: 20804 RVA: 0x0010A548 File Offset: 0x00108748
		[DataMember]
		public string StartTime { get; set; }

		// Token: 0x17001386 RID: 4998
		// (get) Token: 0x06005145 RID: 20805 RVA: 0x0010A551 File Offset: 0x00108751
		// (set) Token: 0x06005146 RID: 20806 RVA: 0x0010A559 File Offset: 0x00108759
		[DataMember]
		public double StepTime { get; set; }

		// Token: 0x17001387 RID: 4999
		// (get) Token: 0x06005147 RID: 20807 RVA: 0x0010A562 File Offset: 0x00108762
		// (set) Token: 0x06005148 RID: 20808 RVA: 0x0010A56A File Offset: 0x0010876A
		[DataMember]
		public string StepType { get; set; }

		// Token: 0x17001388 RID: 5000
		// (get) Token: 0x06005149 RID: 20809 RVA: 0x0010A573 File Offset: 0x00108773
		// (set) Token: 0x0600514A RID: 20810 RVA: 0x0010A57B File Offset: 0x0010877B
		[DataMember]
		public string[] AdditionalEntries { get; set; }
	}
}
