using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x02000303 RID: 771
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "GetContactRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	public class GetContactRequest : Request
	{
		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x060014E7 RID: 5351 RVA: 0x0008B387 File Offset: 0x00089587
		// (set) Token: 0x060014E8 RID: 5352 RVA: 0x0008B38F File Offset: 0x0008958F
		[DataMember]
		public Guid ObjectId
		{
			get
			{
				return this.ObjectIdField;
			}
			set
			{
				this.ObjectIdField = value;
			}
		}

		// Token: 0x04000F8D RID: 3981
		private Guid ObjectIdField;
	}
}
