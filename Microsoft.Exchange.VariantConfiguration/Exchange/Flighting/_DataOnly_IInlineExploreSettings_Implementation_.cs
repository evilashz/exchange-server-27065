using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.Flighting
{
	// Token: 0x0200006C RID: 108
	[EditorBrowsable(EditorBrowsableState.Never)]
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	internal sealed class _DataOnly_IInlineExploreSettings_Implementation_ : IInlineExploreSettings, ISettings, IVariantObjectInstance, IVariantObjectInstanceProvider
	{
		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x0600027F RID: 639 RVA: 0x000055B4 File Offset: 0x000037B4
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000280 RID: 640 RVA: 0x000055B7 File Offset: 0x000037B7
		IVariantObjectInstance IVariantObjectInstanceProvider.GetVariantObjectInstance(VariantContextSnapshot context)
		{
			return this;
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000281 RID: 641 RVA: 0x000055BA File Offset: 0x000037BA
		public string Name
		{
			get
			{
				return this._Name_MaterializedValue_;
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000282 RID: 642 RVA: 0x000055C2 File Offset: 0x000037C2
		public string Content
		{
			get
			{
				return this._Content_MaterializedValue_;
			}
		}

		// Token: 0x040001E0 RID: 480
		internal string _Name_MaterializedValue_;

		// Token: 0x040001E1 RID: 481
		internal string _Content_MaterializedValue_;
	}
}
