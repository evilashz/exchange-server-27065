using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A5C RID: 2652
	[DataContract]
	internal class PhonebookServicesResponse : IBingResultSet
	{
		// Token: 0x17001134 RID: 4404
		// (get) Token: 0x06004B43 RID: 19267 RVA: 0x0010563C File Offset: 0x0010383C
		// (set) Token: 0x06004B44 RID: 19268 RVA: 0x00105644 File Offset: 0x00103844
		[DataMember(Name = "SearchResponse")]
		public PhonebookServicesWebResponse Response { get; set; }

		// Token: 0x17001135 RID: 4405
		// (get) Token: 0x06004B45 RID: 19269 RVA: 0x0010564D File Offset: 0x0010384D
		public IBingResult[] Results
		{
			get
			{
				if (this.Response != null && this.Response.Phonebook != null && this.Response.Phonebook.Results != null)
				{
					return this.Response.Phonebook.Results;
				}
				return null;
			}
		}

		// Token: 0x17001136 RID: 4406
		// (get) Token: 0x06004B46 RID: 19270 RVA: 0x00105688 File Offset: 0x00103888
		public IBingError[] Errors
		{
			get
			{
				if (this.Response != null && this.Response.Errors != null)
				{
					return this.Response.Errors;
				}
				return null;
			}
		}
	}
}
