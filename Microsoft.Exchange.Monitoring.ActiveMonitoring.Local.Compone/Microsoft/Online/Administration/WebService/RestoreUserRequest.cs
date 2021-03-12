using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x020002F2 RID: 754
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "RestoreUserRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[DebuggerStepThrough]
	public class RestoreUserRequest : Request
	{
		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06001498 RID: 5272 RVA: 0x0008B0F0 File Offset: 0x000892F0
		// (set) Token: 0x06001499 RID: 5273 RVA: 0x0008B0F8 File Offset: 0x000892F8
		[DataMember]
		public bool? AutoReconcileProxyConflicts
		{
			get
			{
				return this.AutoReconcileProxyConflictsField;
			}
			set
			{
				this.AutoReconcileProxyConflictsField = value;
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x0600149A RID: 5274 RVA: 0x0008B101 File Offset: 0x00089301
		// (set) Token: 0x0600149B RID: 5275 RVA: 0x0008B109 File Offset: 0x00089309
		[DataMember]
		public string NewUserPrincipalName
		{
			get
			{
				return this.NewUserPrincipalNameField;
			}
			set
			{
				this.NewUserPrincipalNameField = value;
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x0600149C RID: 5276 RVA: 0x0008B112 File Offset: 0x00089312
		// (set) Token: 0x0600149D RID: 5277 RVA: 0x0008B11A File Offset: 0x0008931A
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

		// Token: 0x04000F6E RID: 3950
		private bool? AutoReconcileProxyConflictsField;

		// Token: 0x04000F6F RID: 3951
		private string NewUserPrincipalNameField;

		// Token: 0x04000F70 RID: 3952
		private Guid ObjectIdField;
	}
}
