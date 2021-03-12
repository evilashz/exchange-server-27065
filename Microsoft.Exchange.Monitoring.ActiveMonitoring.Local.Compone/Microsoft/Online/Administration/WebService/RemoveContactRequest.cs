using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x02000302 RID: 770
	[DataContract(Name = "RemoveContactRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class RemoveContactRequest : Request
	{
		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x060014E4 RID: 5348 RVA: 0x0008B36E File Offset: 0x0008956E
		// (set) Token: 0x060014E5 RID: 5349 RVA: 0x0008B376 File Offset: 0x00089576
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

		// Token: 0x04000F8C RID: 3980
		private Guid ObjectIdField;
	}
}
