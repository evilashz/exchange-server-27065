using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace schemas.microsoft.com.O365Filtering.GlobalLocatorService.Data
{
	// Token: 0x02000C32 RID: 3122
	[DataContract(Name = "SaveDomainResponse", Namespace = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/Data")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class SaveDomainResponse : ResponseBase
	{
		// Token: 0x1700113B RID: 4411
		// (get) Token: 0x06004475 RID: 17525 RVA: 0x000B6CFE File Offset: 0x000B4EFE
		// (set) Token: 0x06004476 RID: 17526 RVA: 0x000B6D06 File Offset: 0x000B4F06
		[DataMember]
		public string DomainKey
		{
			get
			{
				return this.DomainKeyField;
			}
			set
			{
				this.DomainKeyField = value;
			}
		}

		// Token: 0x040039FC RID: 14844
		private string DomainKeyField;
	}
}
