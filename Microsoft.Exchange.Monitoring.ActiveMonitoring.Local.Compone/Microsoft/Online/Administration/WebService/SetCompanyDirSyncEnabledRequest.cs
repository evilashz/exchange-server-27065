using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x020002FE RID: 766
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	[DataContract(Name = "SetCompanyDirSyncEnabledRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	public class SetCompanyDirSyncEnabledRequest : Request
	{
		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x060014D6 RID: 5334 RVA: 0x0008B2F9 File Offset: 0x000894F9
		// (set) Token: 0x060014D7 RID: 5335 RVA: 0x0008B301 File Offset: 0x00089501
		[DataMember]
		public bool EnableDirSync
		{
			get
			{
				return this.EnableDirSyncField;
			}
			set
			{
				this.EnableDirSyncField = value;
			}
		}

		// Token: 0x04000F87 RID: 3975
		private bool EnableDirSyncField;
	}
}
