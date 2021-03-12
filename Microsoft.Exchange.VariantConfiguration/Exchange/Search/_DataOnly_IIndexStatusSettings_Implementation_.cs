using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.Search
{
	// Token: 0x02000064 RID: 100
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal sealed class _DataOnly_IIndexStatusSettings_Implementation_ : IIndexStatusSettings, ISettings, IVariantObjectInstance, IVariantObjectInstanceProvider
	{
		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060001FF RID: 511 RVA: 0x00004CFE File Offset: 0x00002EFE
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00004D01 File Offset: 0x00002F01
		IVariantObjectInstance IVariantObjectInstanceProvider.GetVariantObjectInstance(VariantContextSnapshot context)
		{
			return this;
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000201 RID: 513 RVA: 0x00004D04 File Offset: 0x00002F04
		public string Name
		{
			get
			{
				return this._Name_MaterializedValue_;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000202 RID: 514 RVA: 0x00004D0C File Offset: 0x00002F0C
		public TimeSpan InvalidateInterval
		{
			get
			{
				return this._InvalidateInterval_MaterializedValue_;
			}
		}

		// Token: 0x0400016C RID: 364
		internal string _Name_MaterializedValue_;

		// Token: 0x0400016D RID: 365
		internal TimeSpan _InvalidateInterval_MaterializedValue_ = default(TimeSpan);
	}
}
