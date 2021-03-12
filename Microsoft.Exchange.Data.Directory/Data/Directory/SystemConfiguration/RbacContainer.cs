using System;
using System.Diagnostics;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200055B RID: 1371
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public class RbacContainer : Container
	{
		// Token: 0x06003DD3 RID: 15827 RVA: 0x000EB748 File Offset: 0x000E9948
		internal void StampExchangeObjectVersion(FileVersionInfo managementDllVersion)
		{
			ExchangeObjectVersion version = new ExchangeObjectVersion(base.ExchangeVersion.Major, base.ExchangeVersion.Minor, (byte)managementDllVersion.FileMajorPart, (byte)managementDllVersion.FileMinorPart, (ushort)managementDllVersion.FileBuildPart, (ushort)managementDllVersion.FilePrivatePart);
			this.StampExchangeObjectVersion(version);
		}

		// Token: 0x06003DD4 RID: 15828 RVA: 0x000EB794 File Offset: 0x000E9994
		internal void StampExchangeObjectVersion(ExchangeObjectVersion version)
		{
			base.SetExchangeVersion(version);
			this.MinAdminVersion = new int?(base.ExchangeVersion.ExchangeBuild.ToExchange2003FormatInt32());
		}

		// Token: 0x040029DC RID: 10716
		internal const string RdnString = "CN=RBAC";

		// Token: 0x040029DD RID: 10717
		internal static readonly ExchangeBuild InitialRBACBuild = ExchangeObjectVersion.Exchange2010.ExchangeBuild;

		// Token: 0x040029DE RID: 10718
		internal static readonly ExchangeBuild E14RTMBuild = new ExchangeBuild(14, 0, 639, 20);

		// Token: 0x040029DF RID: 10719
		internal static readonly ExchangeBuild FirstRGRABuild = new ExchangeBuild(14, 0, 582, 0);
	}
}
