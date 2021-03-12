using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x020002F1 RID: 753
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "ConvertFederatedUserToManagedRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[DebuggerStepThrough]
	public class ConvertFederatedUserToManagedRequest : Request
	{
		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06001493 RID: 5267 RVA: 0x0008B0C6 File Offset: 0x000892C6
		// (set) Token: 0x06001494 RID: 5268 RVA: 0x0008B0CE File Offset: 0x000892CE
		[DataMember]
		public string NewPassword
		{
			get
			{
				return this.NewPasswordField;
			}
			set
			{
				this.NewPasswordField = value;
			}
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06001495 RID: 5269 RVA: 0x0008B0D7 File Offset: 0x000892D7
		// (set) Token: 0x06001496 RID: 5270 RVA: 0x0008B0DF File Offset: 0x000892DF
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

		// Token: 0x04000F6C RID: 3948
		private string NewPasswordField;

		// Token: 0x04000F6D RID: 3949
		private string UserPrincipalNameField;
	}
}
