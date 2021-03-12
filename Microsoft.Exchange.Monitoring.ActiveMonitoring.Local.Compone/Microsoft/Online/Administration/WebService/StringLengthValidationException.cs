using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x02000399 RID: 921
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "StringLengthValidationException", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	public class StringLengthValidationException : PropertyValidationException
	{
		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06001695 RID: 5781 RVA: 0x0008C183 File Offset: 0x0008A383
		// (set) Token: 0x06001696 RID: 5782 RVA: 0x0008C18B File Offset: 0x0008A38B
		[DataMember]
		public int? MaxLength
		{
			get
			{
				return this.MaxLengthField;
			}
			set
			{
				this.MaxLengthField = value;
			}
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06001697 RID: 5783 RVA: 0x0008C194 File Offset: 0x0008A394
		// (set) Token: 0x06001698 RID: 5784 RVA: 0x0008C19C File Offset: 0x0008A39C
		[DataMember]
		public int? MinLength
		{
			get
			{
				return this.MinLengthField;
			}
			set
			{
				this.MinLengthField = value;
			}
		}

		// Token: 0x04001019 RID: 4121
		private int? MaxLengthField;

		// Token: 0x0400101A RID: 4122
		private int? MinLengthField;
	}
}
