using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x020002FF RID: 767
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "SetCompanySettingsRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	public class SetCompanySettingsRequest : Request
	{
		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x060014D9 RID: 5337 RVA: 0x0008B312 File Offset: 0x00089512
		// (set) Token: 0x060014DA RID: 5338 RVA: 0x0008B31A File Offset: 0x0008951A
		[DataMember]
		public CompanySettings Settings
		{
			get
			{
				return this.SettingsField;
			}
			set
			{
				this.SettingsField = value;
			}
		}

		// Token: 0x04000F88 RID: 3976
		private CompanySettings SettingsField;
	}
}
