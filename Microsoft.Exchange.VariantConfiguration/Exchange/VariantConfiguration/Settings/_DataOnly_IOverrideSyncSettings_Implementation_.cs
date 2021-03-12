using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.VariantConfiguration.Settings
{
	// Token: 0x02000094 RID: 148
	[EditorBrowsable(EditorBrowsableState.Never)]
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	internal sealed class _DataOnly_IOverrideSyncSettings_Implementation_ : IOverrideSyncSettings, IFeature, ISettings, IVariantObjectInstance, IVariantObjectInstanceProvider
	{
		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000395 RID: 917 RVA: 0x000067A5 File Offset: 0x000049A5
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000396 RID: 918 RVA: 0x000067A8 File Offset: 0x000049A8
		IVariantObjectInstance IVariantObjectInstanceProvider.GetVariantObjectInstance(VariantContextSnapshot context)
		{
			return this;
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000397 RID: 919 RVA: 0x000067AB File Offset: 0x000049AB
		public string Name
		{
			get
			{
				return this._Name_MaterializedValue_;
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000398 RID: 920 RVA: 0x000067B3 File Offset: 0x000049B3
		public bool Enabled
		{
			get
			{
				return this._Enabled_MaterializedValue_;
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000399 RID: 921 RVA: 0x000067BB File Offset: 0x000049BB
		public TimeSpan RefreshInterval
		{
			get
			{
				return this._RefreshInterval_MaterializedValue_;
			}
		}

		// Token: 0x040002BB RID: 699
		internal string _Name_MaterializedValue_;

		// Token: 0x040002BC RID: 700
		internal bool _Enabled_MaterializedValue_;

		// Token: 0x040002BD RID: 701
		internal TimeSpan _RefreshInterval_MaterializedValue_ = default(TimeSpan);
	}
}
