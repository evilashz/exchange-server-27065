using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.Flighting
{
	// Token: 0x020000BB RID: 187
	[EditorBrowsable(EditorBrowsableState.Never)]
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	internal sealed class _DataOnly_IVersion_Implementation_ : IVersion, ISettings, IVariantObjectInstance, IVariantObjectInstanceProvider
	{
		// Token: 0x17000332 RID: 818
		// (get) Token: 0x0600044F RID: 1103 RVA: 0x0000714B File Offset: 0x0000534B
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x0000714E File Offset: 0x0000534E
		IVariantObjectInstance IVariantObjectInstanceProvider.GetVariantObjectInstance(VariantContextSnapshot context)
		{
			return this;
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000451 RID: 1105 RVA: 0x00007151 File Offset: 0x00005351
		public string Name
		{
			get
			{
				return this._Name_MaterializedValue_;
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000452 RID: 1106 RVA: 0x00007159 File Offset: 0x00005359
		public string VersionNum
		{
			get
			{
				return this._VersionNum_MaterializedValue_;
			}
		}

		// Token: 0x04000338 RID: 824
		internal string _Name_MaterializedValue_;

		// Token: 0x04000339 RID: 825
		internal string _VersionNum_MaterializedValue_;
	}
}
