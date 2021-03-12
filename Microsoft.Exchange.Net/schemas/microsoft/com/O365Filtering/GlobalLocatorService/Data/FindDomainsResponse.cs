using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace schemas.microsoft.com.O365Filtering.GlobalLocatorService.Data
{
	// Token: 0x02000C35 RID: 3125
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "FindDomainsResponse", Namespace = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/Data")]
	public class FindDomainsResponse : ResponseBase
	{
		// Token: 0x1700113C RID: 4412
		// (get) Token: 0x0600447A RID: 17530 RVA: 0x000B6D27 File Offset: 0x000B4F27
		// (set) Token: 0x0600447B RID: 17531 RVA: 0x000B6D2F File Offset: 0x000B4F2F
		[DataMember]
		public FindDomainResponse[] DomainsResponse
		{
			get
			{
				return this.DomainsResponseField;
			}
			set
			{
				this.DomainsResponseField = value;
			}
		}

		// Token: 0x040039FD RID: 14845
		private FindDomainResponse[] DomainsResponseField;
	}
}
