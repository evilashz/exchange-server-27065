using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.Search
{
	// Token: 0x02000074 RID: 116
	[EditorBrowsable(EditorBrowsableState.Never)]
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	internal sealed class _DataOnly_ILanguageDetection_Implementation_ : ILanguageDetection, ISettings, IVariantObjectInstance, IVariantObjectInstanceProvider
	{
		// Token: 0x170001DC RID: 476
		// (get) Token: 0x0600029F RID: 671 RVA: 0x0000575F File Offset: 0x0000395F
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x00005762 File Offset: 0x00003962
		IVariantObjectInstance IVariantObjectInstanceProvider.GetVariantObjectInstance(VariantContextSnapshot context)
		{
			return this;
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x00005765 File Offset: 0x00003965
		public string Name
		{
			get
			{
				return this._Name_MaterializedValue_;
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x0000576D File Offset: 0x0000396D
		public bool EnableLanguageDetectionLogging
		{
			get
			{
				return this._EnableLanguageDetectionLogging_MaterializedValue_;
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x00005775 File Offset: 0x00003975
		public int SamplingFrequency
		{
			get
			{
				return this._SamplingFrequency_MaterializedValue_;
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x0000577D File Offset: 0x0000397D
		public bool EnableLanguageSelection
		{
			get
			{
				return this._EnableLanguageSelection_MaterializedValue_;
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x00005785 File Offset: 0x00003985
		public string RegionDefaultLanguage
		{
			get
			{
				return this._RegionDefaultLanguage_MaterializedValue_;
			}
		}

		// Token: 0x040001F4 RID: 500
		internal string _Name_MaterializedValue_;

		// Token: 0x040001F5 RID: 501
		internal bool _EnableLanguageDetectionLogging_MaterializedValue_;

		// Token: 0x040001F6 RID: 502
		internal int _SamplingFrequency_MaterializedValue_;

		// Token: 0x040001F7 RID: 503
		internal bool _EnableLanguageSelection_MaterializedValue_;

		// Token: 0x040001F8 RID: 504
		internal string _RegionDefaultLanguage_MaterializedValue_;
	}
}
