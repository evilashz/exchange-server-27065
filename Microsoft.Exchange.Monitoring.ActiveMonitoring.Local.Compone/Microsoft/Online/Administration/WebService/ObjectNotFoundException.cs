using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x02000356 RID: 854
	[KnownType(typeof(InvalidContextException))]
	[KnownType(typeof(GroupMemberNotFoundException))]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "ObjectNotFoundException", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[KnownType(typeof(ServicePrincipalNotFoundException))]
	[KnownType(typeof(UserNotFoundException))]
	[KnownType(typeof(CompanyNotFoundException))]
	[KnownType(typeof(RoleNotFoundException))]
	[KnownType(typeof(RoleMemberNotFoundException))]
	[KnownType(typeof(SubscriptionNotFoundException))]
	[KnownType(typeof(DomainNotFoundException))]
	[KnownType(typeof(ContractNotFoundException))]
	[KnownType(typeof(ContactNotFoundException))]
	[KnownType(typeof(GroupNotFoundException))]
	public class ObjectNotFoundException : DataOperationException
	{
		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x0600161A RID: 5658 RVA: 0x0008BD8F File Offset: 0x00089F8F
		// (set) Token: 0x0600161B RID: 5659 RVA: 0x0008BD97 File Offset: 0x00089F97
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

		// Token: 0x04000FFD RID: 4093
		private string ObjectKeyField;
	}
}
