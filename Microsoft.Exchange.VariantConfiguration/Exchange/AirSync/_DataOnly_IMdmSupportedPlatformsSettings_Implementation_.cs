using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000084 RID: 132
	[EditorBrowsable(EditorBrowsableState.Never)]
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	internal sealed class _DataOnly_IMdmSupportedPlatformsSettings_Implementation_ : IMdmSupportedPlatformsSettings, ISettings, IVariantObjectInstance, IVariantObjectInstanceProvider
	{
		// Token: 0x17000256 RID: 598
		// (get) Token: 0x0600032D RID: 813 RVA: 0x0000616F File Offset: 0x0000436F
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600032E RID: 814 RVA: 0x00006172 File Offset: 0x00004372
		IVariantObjectInstance IVariantObjectInstanceProvider.GetVariantObjectInstance(VariantContextSnapshot context)
		{
			return this;
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x0600032F RID: 815 RVA: 0x00006175 File Offset: 0x00004375
		public string Name
		{
			get
			{
				return this._Name_MaterializedValue_;
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000330 RID: 816 RVA: 0x0000617D File Offset: 0x0000437D
		public string PlatformsSupported
		{
			get
			{
				return this._PlatformsSupported_MaterializedValue_;
			}
		}

		// Token: 0x0400026A RID: 618
		internal string _Name_MaterializedValue_;

		// Token: 0x0400026B RID: 619
		internal string _PlatformsSupported_MaterializedValue_;
	}
}
