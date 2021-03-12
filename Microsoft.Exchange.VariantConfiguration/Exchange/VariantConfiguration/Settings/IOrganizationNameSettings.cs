using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace Microsoft.Exchange.VariantConfiguration.Settings
{
	// Token: 0x0200008D RID: 141
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	public interface IOrganizationNameSettings : ISettings
	{
		// Token: 0x17000299 RID: 665
		// (get) Token: 0x0600037C RID: 892
		IList<string> OrgNames { get; }

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x0600037D RID: 893
		IList<string> DogfoodOrgNames { get; }
	}
}
