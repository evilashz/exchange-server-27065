using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.Search
{
	// Token: 0x02000070 RID: 112
	[EditorBrowsable(EditorBrowsableState.Never)]
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	internal sealed class _DataOnly_IInstantSearch_Implementation_ : IInstantSearch, ISettings, IVariantObjectInstance, IVariantObjectInstanceProvider
	{
		// Token: 0x170001CE RID: 462
		// (get) Token: 0x0600028C RID: 652 RVA: 0x00005640 File Offset: 0x00003840
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00005643 File Offset: 0x00003843
		IVariantObjectInstance IVariantObjectInstanceProvider.GetVariantObjectInstance(VariantContextSnapshot context)
		{
			return this;
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x0600028E RID: 654 RVA: 0x00005646 File Offset: 0x00003846
		public string Name
		{
			get
			{
				return this._Name_MaterializedValue_;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x0600028F RID: 655 RVA: 0x0000564E File Offset: 0x0000384E
		public int FastQueryPath
		{
			get
			{
				return this._FastQueryPath_MaterializedValue_;
			}
		}

		// Token: 0x040001E7 RID: 487
		internal string _Name_MaterializedValue_;

		// Token: 0x040001E8 RID: 488
		internal int _FastQueryPath_MaterializedValue_;
	}
}
