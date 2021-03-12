using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x020000BF RID: 191
	[EditorBrowsable(EditorBrowsableState.Never)]
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	internal sealed class _DataOnly_IWorkloadSettings_Implementation_ : IWorkloadSettings, ISettings, IVariantObjectInstance, IVariantObjectInstanceProvider
	{
		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x0000726A File Offset: 0x0000546A
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x0000726D File Offset: 0x0000546D
		IVariantObjectInstance IVariantObjectInstanceProvider.GetVariantObjectInstance(VariantContextSnapshot context)
		{
			return this;
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000464 RID: 1124 RVA: 0x00007270 File Offset: 0x00005470
		public string Name
		{
			get
			{
				return this._Name_MaterializedValue_;
			}
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x00007278 File Offset: 0x00005478
		public WorkloadClassification Classification
		{
			get
			{
				return this._Classification_MaterializedValue_;
			}
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000466 RID: 1126 RVA: 0x00007280 File Offset: 0x00005480
		public int MaxConcurrency
		{
			get
			{
				return this._MaxConcurrency_MaterializedValue_;
			}
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x00007288 File Offset: 0x00005488
		public bool Enabled
		{
			get
			{
				return this._Enabled_MaterializedValue_;
			}
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000468 RID: 1128 RVA: 0x00007290 File Offset: 0x00005490
		public bool EnabledDuringBlackout
		{
			get
			{
				return this._EnabledDuringBlackout_MaterializedValue_;
			}
		}

		// Token: 0x04000345 RID: 837
		internal string _Name_MaterializedValue_;

		// Token: 0x04000346 RID: 838
		internal WorkloadClassification _Classification_MaterializedValue_;

		// Token: 0x04000347 RID: 839
		internal int _MaxConcurrency_MaterializedValue_;

		// Token: 0x04000348 RID: 840
		internal bool _Enabled_MaterializedValue_;

		// Token: 0x04000349 RID: 841
		internal bool _EnabledDuringBlackout_MaterializedValue_;
	}
}
