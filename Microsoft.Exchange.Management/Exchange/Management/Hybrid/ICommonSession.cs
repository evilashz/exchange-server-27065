using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Hybrid
{
	// Token: 0x0200090B RID: 2315
	internal interface ICommonSession : IDisposable
	{
		// Token: 0x06005214 RID: 21012
		IEnumerable<IAcceptedDomain> GetAcceptedDomain();

		// Token: 0x06005215 RID: 21013
		IFederatedOrganizationIdentifier GetFederatedOrganizationIdentifier();

		// Token: 0x06005216 RID: 21014
		IFederationTrust GetFederationTrust(ObjectId identity);

		// Token: 0x06005217 RID: 21015
		IFederationInformation GetFederationInformation(string domainName);

		// Token: 0x06005218 RID: 21016
		IEnumerable<OrganizationRelationship> GetOrganizationRelationship();

		// Token: 0x06005219 RID: 21017
		IEnumerable<DomainContentConfig> GetRemoteDomain();

		// Token: 0x0600521A RID: 21018
		void RemoveRemoteDomain(ObjectId identity);

		// Token: 0x0600521B RID: 21019
		void NewOrganizationRelationship(string name, string targetApplicationUri, string targetAutodiscoverEpr, IEnumerable<SmtpDomain> domainNames);

		// Token: 0x0600521C RID: 21020
		void RemoveOrganizationRelationship(string identity);

		// Token: 0x0600521D RID: 21021
		void SetOrganizationRelationship(ObjectId identity, SessionParameters parameters);

		// Token: 0x0600521E RID: 21022
		IOrganizationConfig GetOrganizationConfig();

		// Token: 0x0600521F RID: 21023
		IFederationInformation BuildFederationInformation(string targetApplicationUri, string targetAutodiscoverEpr);
	}
}
