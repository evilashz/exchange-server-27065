using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x0200039B RID: 923
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[KnownType(typeof(IncorrectPasswordException))]
	[KnownType(typeof(InvalidPasswordWeakException))]
	[KnownType(typeof(InvalidPasswordContainMemberNameException))]
	[DebuggerStepThrough]
	[DataContract(Name = "InvalidPasswordException", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	public class InvalidPasswordException : StringSyntaxValidationException
	{
	}
}
