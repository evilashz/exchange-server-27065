using System;
using System.Security.Permissions;
using System.ServiceModel.Activation;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002BE RID: 702
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public class ImportContactList : DataSourceService, IImportContactList, IImportObjectService<ImportContactsResult, ImportContactListParameters>
	{
		// Token: 0x06002C15 RID: 11285 RVA: 0x00088CF8 File Offset: 0x00086EF8
		[PrincipalPermission(SecurityAction.Demand, Role = "MultiTenant+Import-ContactList?Identity&CSV&CSVStream@W:Self|Organization")]
		public PowerShellResults<ImportContactsResult> ImportObject(Identity identity, ImportContactListParameters properties)
		{
			PowerShellResults<ImportContactsResult> powerShellResults = base.SetObject<ImportContactsResult, ImportContactListParameters>("Import-ContactList", identity, properties);
			bool succeeded = powerShellResults.Succeeded;
			return powerShellResults;
		}

		// Token: 0x040021D7 RID: 8663
		internal const string ImportCmdlet = "Import-ContactList";

		// Token: 0x040021D8 RID: 8664
		internal const string WriteScope = "@W:Self|Organization";

		// Token: 0x040021D9 RID: 8665
		private const string Noun = "ContactList";

		// Token: 0x040021DA RID: 8666
		private const string ImportObjectRole = "MultiTenant+Import-ContactList?Identity&CSV&CSVStream@W:Self|Organization";
	}
}
