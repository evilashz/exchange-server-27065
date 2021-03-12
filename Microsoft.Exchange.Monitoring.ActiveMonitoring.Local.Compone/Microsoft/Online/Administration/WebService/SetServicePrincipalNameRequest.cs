using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x020002D1 RID: 721
	[DataContract(Name = "SetServicePrincipalNameRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	public class SetServicePrincipalNameRequest : Request
	{
		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x060013F3 RID: 5107 RVA: 0x0008AB86 File Offset: 0x00088D86
		// (set) Token: 0x060013F4 RID: 5108 RVA: 0x0008AB8E File Offset: 0x00088D8E
		[DataMember]
		public string[] AddSpn
		{
			get
			{
				return this.AddSpnField;
			}
			set
			{
				this.AddSpnField = value;
			}
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x060013F5 RID: 5109 RVA: 0x0008AB97 File Offset: 0x00088D97
		// (set) Token: 0x060013F6 RID: 5110 RVA: 0x0008AB9F File Offset: 0x00088D9F
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

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x060013F7 RID: 5111 RVA: 0x0008ABA8 File Offset: 0x00088DA8
		// (set) Token: 0x060013F8 RID: 5112 RVA: 0x0008ABB0 File Offset: 0x00088DB0
		[DataMember]
		public string[] RemoveSpn
		{
			get
			{
				return this.RemoveSpnField;
			}
			set
			{
				this.RemoveSpnField = value;
			}
		}

		// Token: 0x04000F2C RID: 3884
		private string[] AddSpnField;

		// Token: 0x04000F2D RID: 3885
		private Guid ObjectIdField;

		// Token: 0x04000F2E RID: 3886
		private string[] RemoveSpnField;
	}
}
