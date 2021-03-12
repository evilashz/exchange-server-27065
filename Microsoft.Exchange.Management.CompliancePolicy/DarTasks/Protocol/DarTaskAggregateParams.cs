using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.CompliancePolicy.DarTasks.Protocol
{
	// Token: 0x02000012 RID: 18
	[DataContract]
	public class DarTaskAggregateParams : DarTaskParamsBase
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600009D RID: 157 RVA: 0x000035EB File Offset: 0x000017EB
		// (set) Token: 0x0600009E RID: 158 RVA: 0x000035F3 File Offset: 0x000017F3
		[DataMember]
		public bool IsEnabled { get; set; }
	}
}
