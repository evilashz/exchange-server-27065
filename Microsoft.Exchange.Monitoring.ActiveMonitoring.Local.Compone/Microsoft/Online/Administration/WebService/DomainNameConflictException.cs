using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x0200036A RID: 874
	[DebuggerStepThrough]
	[DataContract(Name = "DomainNameConflictException", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class DomainNameConflictException : ObjectAlreadyExistsException
	{
	}
}
