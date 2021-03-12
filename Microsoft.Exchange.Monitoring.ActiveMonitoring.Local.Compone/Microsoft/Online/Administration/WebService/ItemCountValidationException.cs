using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x020003A0 RID: 928
	[KnownType(typeof(TooManyUnverifiedDomainException))]
	[KnownType(typeof(TooManySearchResultsException))]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "ItemCountValidationException", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	public class ItemCountValidationException : PropertyValidationException
	{
		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x060016A2 RID: 5794 RVA: 0x0008C1EE File Offset: 0x0008A3EE
		// (set) Token: 0x060016A3 RID: 5795 RVA: 0x0008C1F6 File Offset: 0x0008A3F6
		[DataMember]
		public int? MaxCount
		{
			get
			{
				return this.MaxCountField;
			}
			set
			{
				this.MaxCountField = value;
			}
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x060016A4 RID: 5796 RVA: 0x0008C1FF File Offset: 0x0008A3FF
		// (set) Token: 0x060016A5 RID: 5797 RVA: 0x0008C207 File Offset: 0x0008A407
		[DataMember]
		public int? MinCount
		{
			get
			{
				return this.MinCountField;
			}
			set
			{
				this.MinCountField = value;
			}
		}

		// Token: 0x0400101C RID: 4124
		private int? MaxCountField;

		// Token: 0x0400101D RID: 4125
		private int? MinCountField;
	}
}
