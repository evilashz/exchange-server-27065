using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x02000374 RID: 884
	[DebuggerStepThrough]
	[KnownType(typeof(SetUnverifiedDomainAsDefaultException))]
	[KnownType(typeof(DomainOverlappingOperationException))]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "DomainDataOperationException", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[KnownType(typeof(DomainUnexpectedAuthenticationException))]
	[KnownType(typeof(DomainCapabilitySetException))]
	[KnownType(typeof(DomainLiveNamespaceExistsException))]
	[KnownType(typeof(DomainNotRootException))]
	[KnownType(typeof(DomainLiveNamespaceAuthenticationException))]
	[KnownType(typeof(DomainLiveNamespaceUriConflictException))]
	[KnownType(typeof(DomainNameForbiddenWordException))]
	[KnownType(typeof(DomainPendingDeletionException))]
	[KnownType(typeof(DomainVerificationMissingCnameException))]
	[KnownType(typeof(DomainVerificationWrongCnameTargetException))]
	[KnownType(typeof(DomainVerificationMissingDnsRecordException))]
	[KnownType(typeof(DomainLiveNamespaceUnmanagedException))]
	[KnownType(typeof(DomainCapabilityUnsetException))]
	[KnownType(typeof(DefaultDomainDeletionException))]
	[KnownType(typeof(InitialDomainDeletionException))]
	[KnownType(typeof(DomainHasChildDomainException))]
	[KnownType(typeof(DomainNotEmptyException))]
	[KnownType(typeof(DefaultDomainUnsetException))]
	[KnownType(typeof(DefaultDomainInvalidAuthenticationException))]
	[KnownType(typeof(InitialDomainUpdateException))]
	[KnownType(typeof(DomainCapabilityUnavailableException))]
	public class DomainDataOperationException : DataOperationException
	{
		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06001646 RID: 5702 RVA: 0x0008BEF6 File Offset: 0x0008A0F6
		// (set) Token: 0x06001647 RID: 5703 RVA: 0x0008BEFE File Offset: 0x0008A0FE
		[DataMember]
		public string DomainName
		{
			get
			{
				return this.DomainNameField;
			}
			set
			{
				this.DomainNameField = value;
			}
		}

		// Token: 0x04001004 RID: 4100
		private string DomainNameField;
	}
}
