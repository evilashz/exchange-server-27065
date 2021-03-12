using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x0200039A RID: 922
	[KnownType(typeof(InvalidPasswordContainMemberNameException))]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "StringSyntaxValidationException", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[KnownType(typeof(IncorrectPasswordException))]
	[DebuggerStepThrough]
	[KnownType(typeof(InvalidPasswordException))]
	[KnownType(typeof(InvalidPasswordWeakException))]
	public class StringSyntaxValidationException : PropertyValidationException
	{
		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x0600169A RID: 5786 RVA: 0x0008C1AD File Offset: 0x0008A3AD
		// (set) Token: 0x0600169B RID: 5787 RVA: 0x0008C1B5 File Offset: 0x0008A3B5
		[DataMember]
		public string RegularExpression
		{
			get
			{
				return this.RegularExpressionField;
			}
			set
			{
				this.RegularExpressionField = value;
			}
		}

		// Token: 0x0400101B RID: 4123
		private string RegularExpressionField;
	}
}
