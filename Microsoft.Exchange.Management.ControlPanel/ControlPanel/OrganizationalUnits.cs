using System;
using System.Security;
using System.Security.Permissions;
using System.ServiceModel.Activation;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200052B RID: 1323
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public sealed class OrganizationalUnits : DataSourceService
	{
		// Token: 0x06003EEF RID: 16111 RVA: 0x000BD7C8 File Offset: 0x000BB9C8
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-OrganizationalUnit?Identity@R:Organization")]
		public PowerShellResults<ExtendedOrganizationalUnit> GetObject(Identity identity)
		{
			PowerShellResults<ExtendedOrganizationalUnit> result;
			try
			{
				result = base.GetObject<ExtendedOrganizationalUnit>("Get-OrganizationalUnit", identity);
			}
			catch (SecurityException innerException)
			{
				result = new PowerShellResults<ExtendedOrganizationalUnit>
				{
					ErrorRecords = new ErrorRecord[]
					{
						new ErrorRecord(new Exception(Strings.MultipleOrganizationalUnit, innerException))
					}
				};
			}
			return result;
		}

		// Token: 0x040028B7 RID: 10423
		private const string Noun = "OrganizationalUnit";

		// Token: 0x040028B8 RID: 10424
		internal const string GetCmdlet = "Get-OrganizationalUnit";

		// Token: 0x040028B9 RID: 10425
		internal const string ReadScope = "@R:Organization";

		// Token: 0x040028BA RID: 10426
		private const string GetObjectRole = "Get-OrganizationalUnit?Identity@R:Organization";
	}
}
