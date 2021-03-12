using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000986 RID: 2438
	internal interface IPFTreeTask
	{
		// Token: 0x170019E2 RID: 6626
		// (get) Token: 0x060056F3 RID: 22259
		// (set) Token: 0x060056F4 RID: 22260
		OrganizationIdParameter Organization { get; set; }

		// Token: 0x170019E3 RID: 6627
		// (get) Token: 0x060056F5 RID: 22261
		ADObjectId RootOrgContainerId { get; }

		// Token: 0x170019E4 RID: 6628
		// (get) Token: 0x060056F6 RID: 22262
		Fqdn DomainController { get; }

		// Token: 0x170019E5 RID: 6629
		// (get) Token: 0x060056F7 RID: 22263
		OrganizationId CurrentOrganizationId { get; }

		// Token: 0x170019E6 RID: 6630
		// (get) Token: 0x060056F8 RID: 22264
		OrganizationId ExecutingUserOrganizationId { get; }

		// Token: 0x170019E7 RID: 6631
		// (get) Token: 0x060056F9 RID: 22265
		IConfigDataProvider DataSession { get; }

		// Token: 0x170019E8 RID: 6632
		// (get) Token: 0x060056FA RID: 22266
		ITopologyConfigurationSession GlobalConfigSession { get; }

		// Token: 0x060056FB RID: 22267
		OrganizationId ResolveCurrentOrganization();

		// Token: 0x060056FC RID: 22268
		TObject GetDataObject<TObject>(IIdentityParameter id, IConfigDataProvider session, ObjectId rootID, LocalizedString? notFoundError, LocalizedString? multipleFoundError) where TObject : IConfigurable, new();

		// Token: 0x060056FD RID: 22269
		void WriteVerbose(LocalizedString text);

		// Token: 0x060056FE RID: 22270
		void WriteWarning(LocalizedString text);

		// Token: 0x060056FF RID: 22271
		void WriteError(Exception exception, ErrorCategory category, object target);
	}
}
