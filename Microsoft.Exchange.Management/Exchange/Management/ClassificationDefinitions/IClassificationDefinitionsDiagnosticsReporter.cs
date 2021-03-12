using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.ClassificationDefinitions
{
	// Token: 0x02000823 RID: 2083
	internal interface IClassificationDefinitionsDiagnosticsReporter
	{
		// Token: 0x06004816 RID: 18454
		void WriteCorruptRulePackageDiagnosticsInformation(int traceSourceHashCode, OrganizationId currentOrganizationId, string offendingRulePackageObjectDn, Exception underlyingException);

		// Token: 0x06004817 RID: 18455
		void WriteDuplicateRuleIdAcrossRulePacksDiagnosticsInformation(int traceSourceHashCode, OrganizationId currentOrganizationId, string offendingRulePackageObjectDn1, string offendingRulePackageObjectDn2, string duplicateRuleId);

		// Token: 0x06004818 RID: 18456
		void WriteClassificationEngineConfigurationErrorInformation(int traceSourceHashCode, Exception underlyingException);

		// Token: 0x06004819 RID: 18457
		void WriteClassificationEngineUnexpectedFailureInValidation(int traceSourceHashCode, OrganizationId currentOrganizationId, int engineErrorCode);

		// Token: 0x0600481A RID: 18458
		void WriteClassificationEngineTimeoutInValidation(int traceSourceHashCode, OrganizationId currentOrganizationId, int validationTimeout);

		// Token: 0x0600481B RID: 18459
		void WriteInvalidObjectInformation(int traceSourceHashCode, OrganizationId currentOrganizationId, string offendingRulePackageObjectDn);
	}
}
