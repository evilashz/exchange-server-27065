using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x02000354 RID: 852
	[KnownType(typeof(InvalidContextException))]
	[KnownType(typeof(ObjectNotFoundException))]
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
	[KnownType(typeof(SetUnverifiedDomainAsDefaultException))]
	[KnownType(typeof(DefaultDomainUnsetException))]
	[KnownType(typeof(DefaultDomainInvalidAuthenticationException))]
	[KnownType(typeof(InitialDomainUpdateException))]
	[KnownType(typeof(DomainCapabilityUnavailableException))]
	[KnownType(typeof(DomainOverlappingOperationException))]
	[KnownType(typeof(BindingRedirectionException))]
	[KnownType(typeof(ThrottlingException))]
	[KnownType(typeof(ClientVersionException))]
	[KnownType(typeof(LiveTokenExpiredException))]
	[KnownType(typeof(InvalidHeaderException))]
	[KnownType(typeof(InvalidParameterException))]
	[KnownType(typeof(InvalidLicenseConfigurationException))]
	[KnownType(typeof(InvalidUserLicenseOptionException))]
	[KnownType(typeof(InvalidUserLicenseException))]
	[KnownType(typeof(InvalidSubscriptionStatusException))]
	[KnownType(typeof(LicenseQuotaExceededException))]
	[KnownType(typeof(TenantNotPartnerTypeException))]
	[KnownType(typeof(PropertyValidationException))]
	[KnownType(typeof(StringLengthValidationException))]
	[KnownType(typeof(StringSyntaxValidationException))]
	[KnownType(typeof(InvalidPasswordException))]
	[KnownType(typeof(InvalidPasswordWeakException))]
	[KnownType(typeof(InvalidPasswordContainMemberNameException))]
	[KnownType(typeof(IncorrectPasswordException))]
	[KnownType(typeof(ServicePrincipalCredentialNotSettableException))]
	[KnownType(typeof(ItemCountValidationException))]
	[KnownType(typeof(TooManySearchResultsException))]
	[KnownType(typeof(TooManyUnverifiedDomainException))]
	[KnownType(typeof(PropertyNotSettableException))]
	[KnownType(typeof(PropertyDomainValidationException))]
	[KnownType(typeof(UniquenessValidationException))]
	[KnownType(typeof(ServiceUnavailableException))]
	[KnownType(typeof(InternalServiceException))]
	[KnownType(typeof(DirectoryInternalServiceException))]
	[KnownType(typeof(IdentityInternalServiceException))]
	[KnownType(typeof(UserAccountDisabledException))]
	[KnownType(typeof(RequiredPropertyNotSetException))]
	[KnownType(typeof(QuotaExceededException))]
	[KnownType(typeof(UserAuthenticationUnchangedException))]
	[KnownType(typeof(MailEnabledGroupsNotSupportedException))]
	[KnownType(typeof(GroupDeletionNotAllowedException))]
	[KnownType(typeof(GroupUpdateNotAllowedException))]
	[KnownType(typeof(AccessDeniedException))]
	[KnownType(typeof(DomainDataOperationException))]
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
	[KnownType(typeof(GroupMemberNotFoundException))]
	[KnownType(typeof(PageNotAvailableException))]
	[KnownType(typeof(InvalidListContextException))]
	[KnownType(typeof(ObjectAlreadyExistsException))]
	[KnownType(typeof(MemberAlreadyExistsException))]
	[KnownType(typeof(UserAlreadyExistsException))]
	[KnownType(typeof(DomainAlreadyExistsException))]
	[KnownType(typeof(DomainAlreadyExistsInOldSystemException))]
	[KnownType(typeof(DomainNameConflictException))]
	[KnownType(typeof(GroupAlreadyExistsException))]
	[KnownType(typeof(RemoveSelfFromRoleException))]
	[KnownType(typeof(RemoveDirSyncObjectNotAllowedException))]
	[KnownType(typeof(UserRemoveSelfException))]
	[KnownType(typeof(UserConflictAuthenticationException))]
	[KnownType(typeof(RestoreUserLicenseErrorException))]
	[KnownType(typeof(RestoreUserErrorException))]
	[KnownType(typeof(RestoreUserNotAllowedException))]
	[KnownType(typeof(DirSyncStatusChangeNotAllowedException))]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "MsolAdministrationException", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[KnownType(typeof(DataOperationException))]
	public class MsolAdministrationException : IExtensibleDataObject
	{
		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x0600160C RID: 5644 RVA: 0x0008BD19 File Offset: 0x00089F19
		// (set) Token: 0x0600160D RID: 5645 RVA: 0x0008BD21 File Offset: 0x00089F21
		public ExtensionDataObject ExtensionData
		{
			get
			{
				return this.extensionDataField;
			}
			set
			{
				this.extensionDataField = value;
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x0600160E RID: 5646 RVA: 0x0008BD2A File Offset: 0x00089F2A
		// (set) Token: 0x0600160F RID: 5647 RVA: 0x0008BD32 File Offset: 0x00089F32
		[DataMember]
		public string HelpLink
		{
			get
			{
				return this.HelpLinkField;
			}
			set
			{
				this.HelpLinkField = value;
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06001610 RID: 5648 RVA: 0x0008BD3B File Offset: 0x00089F3B
		// (set) Token: 0x06001611 RID: 5649 RVA: 0x0008BD43 File Offset: 0x00089F43
		[DataMember]
		public string Message
		{
			get
			{
				return this.MessageField;
			}
			set
			{
				this.MessageField = value;
			}
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06001612 RID: 5650 RVA: 0x0008BD4C File Offset: 0x00089F4C
		// (set) Token: 0x06001613 RID: 5651 RVA: 0x0008BD54 File Offset: 0x00089F54
		[DataMember]
		public Guid? OperationId
		{
			get
			{
				return this.OperationIdField;
			}
			set
			{
				this.OperationIdField = value;
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06001614 RID: 5652 RVA: 0x0008BD5D File Offset: 0x00089F5D
		// (set) Token: 0x06001615 RID: 5653 RVA: 0x0008BD65 File Offset: 0x00089F65
		[DataMember]
		public string Source
		{
			get
			{
				return this.SourceField;
			}
			set
			{
				this.SourceField = value;
			}
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06001616 RID: 5654 RVA: 0x0008BD6E File Offset: 0x00089F6E
		// (set) Token: 0x06001617 RID: 5655 RVA: 0x0008BD76 File Offset: 0x00089F76
		[DataMember]
		public string StackTrace
		{
			get
			{
				return this.StackTraceField;
			}
			set
			{
				this.StackTraceField = value;
			}
		}

		// Token: 0x04000FF7 RID: 4087
		private ExtensionDataObject extensionDataField;

		// Token: 0x04000FF8 RID: 4088
		private string HelpLinkField;

		// Token: 0x04000FF9 RID: 4089
		private string MessageField;

		// Token: 0x04000FFA RID: 4090
		private Guid? OperationIdField;

		// Token: 0x04000FFB RID: 4091
		private string SourceField;

		// Token: 0x04000FFC RID: 4092
		private string StackTraceField;
	}
}
