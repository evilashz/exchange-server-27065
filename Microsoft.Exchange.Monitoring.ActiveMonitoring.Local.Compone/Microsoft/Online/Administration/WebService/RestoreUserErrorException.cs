using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x02000371 RID: 881
	[DebuggerStepThrough]
	[DataContract(Name = "RestoreUserErrorException", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class RestoreUserErrorException : DataOperationException
	{
		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x0600163F RID: 5695 RVA: 0x0008BEBC File Offset: 0x0008A0BC
		// (set) Token: 0x06001640 RID: 5696 RVA: 0x0008BEC4 File Offset: 0x0008A0C4
		[DataMember]
		public RestoreUserError[] RestoreUserErrors
		{
			get
			{
				return this.RestoreUserErrorsField;
			}
			set
			{
				this.RestoreUserErrorsField = value;
			}
		}

		// Token: 0x04001002 RID: 4098
		private RestoreUserError[] RestoreUserErrorsField;
	}
}
