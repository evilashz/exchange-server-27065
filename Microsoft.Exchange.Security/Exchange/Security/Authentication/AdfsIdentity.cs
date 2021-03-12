using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Security.Authentication
{
	// Token: 0x02000018 RID: 24
	[Serializable]
	public class AdfsIdentity : SidBasedIdentity
	{
		// Token: 0x060000A7 RID: 167 RVA: 0x000075B4 File Offset: 0x000057B4
		public AdfsIdentity(string userPrincipalName, string userSid, OrganizationId orgId, string PartitionId, IEnumerable<string> groupSidIds, bool isPublicSession) : base(userPrincipalName, userSid, userPrincipalName, "ADFS", null)
		{
			if (groupSidIds == null)
			{
				throw new ArgumentNullException("groupSidIds");
			}
			if (!VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).OwaServer.ShouldSkipAdfsGroupReadOnFrontend.Enabled)
			{
				this.prepopulatedGroupSidIds = groupSidIds;
			}
			base.UserOrganizationId = orgId;
			this.IsPublicSession = isPublicSession;
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x00007616 File Offset: 0x00005816
		// (set) Token: 0x060000A9 RID: 169 RVA: 0x0000761E File Offset: 0x0000581E
		public bool IsPublicSession { get; private set; }

		// Token: 0x04000142 RID: 322
		private const string AdfsAuthType = "ADFS";
	}
}
