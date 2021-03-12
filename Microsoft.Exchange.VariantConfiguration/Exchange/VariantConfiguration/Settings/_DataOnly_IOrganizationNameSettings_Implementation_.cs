using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.VariantConfiguration.Settings
{
	// Token: 0x02000090 RID: 144
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal sealed class _DataOnly_IOrganizationNameSettings_Implementation_ : IOrganizationNameSettings, ISettings, IVariantObjectInstance, IVariantObjectInstanceProvider
	{
		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000386 RID: 902 RVA: 0x000066D4 File Offset: 0x000048D4
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000387 RID: 903 RVA: 0x000066D7 File Offset: 0x000048D7
		IVariantObjectInstance IVariantObjectInstanceProvider.GetVariantObjectInstance(VariantContextSnapshot context)
		{
			return this;
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000388 RID: 904 RVA: 0x000066DA File Offset: 0x000048DA
		public string Name
		{
			get
			{
				return this._Name_MaterializedValue_;
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000389 RID: 905 RVA: 0x000066E2 File Offset: 0x000048E2
		public IList<string> OrgNames
		{
			get
			{
				return this._OrgNames_MaterializedValue_;
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x0600038A RID: 906 RVA: 0x000066EA File Offset: 0x000048EA
		public IList<string> DogfoodOrgNames
		{
			get
			{
				return this._DogfoodOrgNames_MaterializedValue_;
			}
		}

		// Token: 0x040002B1 RID: 689
		internal string _Name_MaterializedValue_;

		// Token: 0x040002B2 RID: 690
		internal IList<string> _OrgNames_MaterializedValue_;

		// Token: 0x040002B3 RID: 691
		internal IList<string> _DogfoodOrgNames_MaterializedValue_;
	}
}
