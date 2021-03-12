using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x020002F3 RID: 755
	[DebuggerStepThrough]
	[DataContract(Name = "RestoreUserByUpnRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class RestoreUserByUpnRequest : Request
	{
		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x0600149F RID: 5279 RVA: 0x0008B12B File Offset: 0x0008932B
		// (set) Token: 0x060014A0 RID: 5280 RVA: 0x0008B133 File Offset: 0x00089333
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

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x060014A1 RID: 5281 RVA: 0x0008B13C File Offset: 0x0008933C
		// (set) Token: 0x060014A2 RID: 5282 RVA: 0x0008B144 File Offset: 0x00089344
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

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x060014A3 RID: 5283 RVA: 0x0008B14D File Offset: 0x0008934D
		// (set) Token: 0x060014A4 RID: 5284 RVA: 0x0008B155 File Offset: 0x00089355
		[DataMember]
		public string UserPrincipalName
		{
			get
			{
				return this.UserPrincipalNameField;
			}
			set
			{
				this.UserPrincipalNameField = value;
			}
		}

		// Token: 0x04000F71 RID: 3953
		private bool? AutoReconcileProxyConflictsField;

		// Token: 0x04000F72 RID: 3954
		private string NewUserPrincipalNameField;

		// Token: 0x04000F73 RID: 3955
		private string UserPrincipalNameField;
	}
}
