using System;

namespace Microsoft.Exchange.Management.Sharing
{
	// Token: 0x02000A00 RID: 2560
	public enum TestOrganizationRelationshipResultId
	{
		// Token: 0x04003424 RID: 13348
		Unknown,
		// Token: 0x04003425 RID: 13349
		FailureToGetDelegationToken,
		// Token: 0x04003426 RID: 13350
		AutodiscoverUrlsDiffer,
		// Token: 0x04003427 RID: 13351
		AutodiscoverServiceCallFailed,
		// Token: 0x04003428 RID: 13352
		NoOrganizationRelationshipInstancesWereReturnedByTheRemoteParty,
		// Token: 0x04003429 RID: 13353
		MultipleOrganizationRelationshipInstancesReturnedByTheRemoteParty,
		// Token: 0x0400342A RID: 13354
		VerificationOfRemoteOrganizationRelationshipFailed,
		// Token: 0x0400342B RID: 13355
		ApplicationUrisDiffer,
		// Token: 0x0400342C RID: 13356
		AccessMismatchLocalRemote,
		// Token: 0x0400342D RID: 13357
		CouldNotParseRemoteValue,
		// Token: 0x0400342E RID: 13358
		AccessMismatchRemoteLocal,
		// Token: 0x0400342F RID: 13359
		PropertiesDiffer,
		// Token: 0x04003430 RID: 13360
		NoDomainInTheRemoteOrganizationRelationshipIsFederatedLocally,
		// Token: 0x04003431 RID: 13361
		LocalFederatedDomainsAreMissingFromTheRemoteOrganizationRelationsipDomains,
		// Token: 0x04003432 RID: 13362
		MismatchedSTS,
		// Token: 0x04003433 RID: 13363
		NoLocalDomainIsFederatedRemotely,
		// Token: 0x04003434 RID: 13364
		TargetSharingEprDoesNotMatchAnyExternalURI,
		// Token: 0x04003435 RID: 13365
		RemoteOrgRelationshipHasNoDomainsDefined,
		// Token: 0x04003436 RID: 13366
		UserFedDomainNotInRemoteOrgRelationship,
		// Token: 0x04003437 RID: 13367
		UserFedDomainInMultipleRemoteOrgRelationship,
		// Token: 0x04003438 RID: 13368
		NoRemoteFederatedDomainInLocalOrgRelationship,
		// Token: 0x04003439 RID: 13369
		FederatedOrganizationIdNotEnabled,
		// Token: 0x0400343A RID: 13370
		ApplicationUriMissing,
		// Token: 0x0400343B RID: 13371
		MismatchedFederation,
		// Token: 0x0400343C RID: 13372
		UnknownFederationDomainAuthenticationType,
		// Token: 0x0400343D RID: 13373
		FederatedIdentityTypeMismatch,
		// Token: 0x0400343E RID: 13374
		RequiredIdentityInformationNotSet,
		// Token: 0x0400343F RID: 13375
		UserFederatedDomainDoesNotMatchAccountNamespace,
		// Token: 0x04003440 RID: 13376
		UserFederatedIdentityIsNotFederatedDomain,
		// Token: 0x04003441 RID: 13377
		AutoDiscoverNotSetInOrgRelationship
	}
}
