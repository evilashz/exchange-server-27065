using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x0200030E RID: 782
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "ListDomainsRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[DebuggerStepThrough]
	public class ListDomainsRequest : Request
	{
		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06001512 RID: 5394 RVA: 0x0008B4EF File Offset: 0x000896EF
		// (set) Token: 0x06001513 RID: 5395 RVA: 0x0008B4F7 File Offset: 0x000896F7
		[DataMember]
		public DomainSearchFilter SearchFilter
		{
			get
			{
				return this.SearchFilterField;
			}
			set
			{
				this.SearchFilterField = value;
			}
		}

		// Token: 0x04000F9D RID: 3997
		private DomainSearchFilter SearchFilterField;
	}
}
