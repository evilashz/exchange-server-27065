using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.Search
{
	// Token: 0x020000AF RID: 175
	[EditorBrowsable(EditorBrowsableState.Never)]
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	internal sealed class _DataOnly_ITransportFlowSettings_Implementation_ : ITransportFlowSettings, ISettings, IVariantObjectInstance, IVariantObjectInstanceProvider
	{
		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000420 RID: 1056 RVA: 0x00006F25 File Offset: 0x00005125
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x00006F28 File Offset: 0x00005128
		IVariantObjectInstance IVariantObjectInstanceProvider.GetVariantObjectInstance(VariantContextSnapshot context)
		{
			return this;
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000422 RID: 1058 RVA: 0x00006F2B File Offset: 0x0000512B
		public string Name
		{
			get
			{
				return this._Name_MaterializedValue_;
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000423 RID: 1059 RVA: 0x00006F33 File Offset: 0x00005133
		public bool SkipTokenInfoGeneration
		{
			get
			{
				return this._SkipTokenInfoGeneration_MaterializedValue_;
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000424 RID: 1060 RVA: 0x00006F3B File Offset: 0x0000513B
		public bool SkipMdmGeneration
		{
			get
			{
				return this._SkipMdmGeneration_MaterializedValue_;
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x00006F43 File Offset: 0x00005143
		public bool UseMdmFlow
		{
			get
			{
				return this._UseMdmFlow_MaterializedValue_;
			}
		}

		// Token: 0x0400031B RID: 795
		internal string _Name_MaterializedValue_;

		// Token: 0x0400031C RID: 796
		internal bool _SkipTokenInfoGeneration_MaterializedValue_;

		// Token: 0x0400031D RID: 797
		internal bool _SkipMdmGeneration_MaterializedValue_;

		// Token: 0x0400031E RID: 798
		internal bool _UseMdmFlow_MaterializedValue_;
	}
}
