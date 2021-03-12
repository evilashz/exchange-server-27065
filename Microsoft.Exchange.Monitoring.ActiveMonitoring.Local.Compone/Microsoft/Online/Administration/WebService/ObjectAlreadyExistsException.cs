using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x02000365 RID: 869
	[KnownType(typeof(MemberAlreadyExistsException))]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "ObjectAlreadyExistsException", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[KnownType(typeof(GroupAlreadyExistsException))]
	[KnownType(typeof(DomainNameConflictException))]
	[KnownType(typeof(UserAlreadyExistsException))]
	[KnownType(typeof(DomainAlreadyExistsInOldSystemException))]
	[KnownType(typeof(DomainAlreadyExistsException))]
	public class ObjectAlreadyExistsException : DataOperationException
	{
		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x0600162B RID: 5675 RVA: 0x0008BE18 File Offset: 0x0008A018
		// (set) Token: 0x0600162C RID: 5676 RVA: 0x0008BE20 File Offset: 0x0008A020
		[DataMember]
		public string ObjectKey
		{
			get
			{
				return this.ObjectKeyField;
			}
			set
			{
				this.ObjectKeyField = value;
			}
		}

		// Token: 0x04000FFE RID: 4094
		private string ObjectKeyField;
	}
}
