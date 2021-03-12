using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x02000398 RID: 920
	[KnownType(typeof(TooManyUnverifiedDomainException))]
	[KnownType(typeof(UniquenessValidationException))]
	[KnownType(typeof(InvalidPasswordWeakException))]
	[KnownType(typeof(PropertyDomainValidationException))]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "PropertyValidationException", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[KnownType(typeof(StringLengthValidationException))]
	[KnownType(typeof(StringSyntaxValidationException))]
	[KnownType(typeof(InvalidPasswordException))]
	[DebuggerStepThrough]
	[KnownType(typeof(InvalidPasswordContainMemberNameException))]
	[KnownType(typeof(IncorrectPasswordException))]
	[KnownType(typeof(ServicePrincipalCredentialNotSettableException))]
	[KnownType(typeof(ItemCountValidationException))]
	[KnownType(typeof(TooManySearchResultsException))]
	[KnownType(typeof(PropertyNotSettableException))]
	public class PropertyValidationException : MsolAdministrationException
	{
		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06001690 RID: 5776 RVA: 0x0008C159 File Offset: 0x0008A359
		// (set) Token: 0x06001691 RID: 5777 RVA: 0x0008C161 File Offset: 0x0008A361
		[DataMember]
		public string ParentObjectType
		{
			get
			{
				return this.ParentObjectTypeField;
			}
			set
			{
				this.ParentObjectTypeField = value;
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06001692 RID: 5778 RVA: 0x0008C16A File Offset: 0x0008A36A
		// (set) Token: 0x06001693 RID: 5779 RVA: 0x0008C172 File Offset: 0x0008A372
		[DataMember]
		public string PropertyName
		{
			get
			{
				return this.PropertyNameField;
			}
			set
			{
				this.PropertyNameField = value;
			}
		}

		// Token: 0x04001017 RID: 4119
		private string ParentObjectTypeField;

		// Token: 0x04001018 RID: 4120
		private string PropertyNameField;
	}
}
