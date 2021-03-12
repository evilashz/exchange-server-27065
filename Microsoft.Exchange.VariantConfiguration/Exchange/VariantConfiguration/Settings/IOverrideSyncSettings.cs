using System;
using System.CodeDom.Compiler;

namespace Microsoft.Exchange.VariantConfiguration.Settings
{
	// Token: 0x02000091 RID: 145
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	public interface IOverrideSyncSettings : IFeature, ISettings
	{
		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x0600038C RID: 908
		TimeSpan RefreshInterval { get; }
	}
}
