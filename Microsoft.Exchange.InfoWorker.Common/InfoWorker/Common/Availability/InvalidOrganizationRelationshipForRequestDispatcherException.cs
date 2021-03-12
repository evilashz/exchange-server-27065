using System;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x020000A2 RID: 162
	internal class InvalidOrganizationRelationshipForRequestDispatcherException : AvailabilityException
	{
		// Token: 0x0600036D RID: 877 RVA: 0x0000EA63 File Offset: 0x0000CC63
		public InvalidOrganizationRelationshipForRequestDispatcherException(string organizationRelationship) : base(ErrorConstants.InvalidOrganizationRelationshipForFreeBusy, Strings.descMisconfiguredOrganizationRelationship(organizationRelationship))
		{
		}
	}
}
