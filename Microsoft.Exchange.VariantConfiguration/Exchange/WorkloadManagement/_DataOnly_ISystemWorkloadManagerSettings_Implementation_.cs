using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x020000A7 RID: 167
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal sealed class _DataOnly_ISystemWorkloadManagerSettings_Implementation_ : ISystemWorkloadManagerSettings, ISettings, IVariantObjectInstance, IVariantObjectInstanceProvider
	{
		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000400 RID: 1024 RVA: 0x00006D97 File Offset: 0x00004F97
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x00006D9A File Offset: 0x00004F9A
		IVariantObjectInstance IVariantObjectInstanceProvider.GetVariantObjectInstance(VariantContextSnapshot context)
		{
			return this;
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000402 RID: 1026 RVA: 0x00006D9D File Offset: 0x00004F9D
		public string Name
		{
			get
			{
				return this._Name_MaterializedValue_;
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000403 RID: 1027 RVA: 0x00006DA5 File Offset: 0x00004FA5
		public int MaxConcurrency
		{
			get
			{
				return this._MaxConcurrency_MaterializedValue_;
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000404 RID: 1028 RVA: 0x00006DAD File Offset: 0x00004FAD
		public TimeSpan RefreshCycle
		{
			get
			{
				return this._RefreshCycle_MaterializedValue_;
			}
		}

		// Token: 0x04000308 RID: 776
		internal string _Name_MaterializedValue_;

		// Token: 0x04000309 RID: 777
		internal int _MaxConcurrency_MaterializedValue_;

		// Token: 0x0400030A RID: 778
		internal TimeSpan _RefreshCycle_MaterializedValue_ = default(TimeSpan);
	}
}
