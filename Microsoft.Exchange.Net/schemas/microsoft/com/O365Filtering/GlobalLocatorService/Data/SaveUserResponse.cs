using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace schemas.microsoft.com.O365Filtering.GlobalLocatorService.Data
{
	// Token: 0x02000C36 RID: 3126
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	[DataContract(Name = "SaveUserResponse", Namespace = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/Data")]
	public class SaveUserResponse : ResponseBase
	{
		// Token: 0x1700113D RID: 4413
		// (get) Token: 0x0600447D RID: 17533 RVA: 0x000B6D40 File Offset: 0x000B4F40
		// (set) Token: 0x0600447E RID: 17534 RVA: 0x000B6D48 File Offset: 0x000B4F48
		[DataMember]
		public string UserKey
		{
			get
			{
				return this.UserKeyField;
			}
			set
			{
				this.UserKeyField = value;
			}
		}

		// Token: 0x040039FE RID: 14846
		private string UserKeyField;
	}
}
