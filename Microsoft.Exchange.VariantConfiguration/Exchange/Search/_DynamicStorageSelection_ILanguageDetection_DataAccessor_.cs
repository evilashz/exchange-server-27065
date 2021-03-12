using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.Search
{
	// Token: 0x02000072 RID: 114
	[EditorBrowsable(EditorBrowsableState.Never)]
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	internal sealed class _DynamicStorageSelection_ILanguageDetection_DataAccessor_ : VariantObjectDataAccessorBase<ILanguageDetection, _DynamicStorageSelection_ILanguageDetection_Implementation_, _DynamicStorageSelection_ILanguageDetection_DataAccessor_>
	{
		// Token: 0x040001E9 RID: 489
		internal string _Name_MaterializedValue_;

		// Token: 0x040001EA RID: 490
		internal bool _EnableLanguageDetectionLogging_MaterializedValue_;

		// Token: 0x040001EB RID: 491
		internal ValueProvider<bool> _EnableLanguageDetectionLogging_ValueProvider_;

		// Token: 0x040001EC RID: 492
		internal int _SamplingFrequency_MaterializedValue_;

		// Token: 0x040001ED RID: 493
		internal ValueProvider<int> _SamplingFrequency_ValueProvider_;

		// Token: 0x040001EE RID: 494
		internal bool _EnableLanguageSelection_MaterializedValue_;

		// Token: 0x040001EF RID: 495
		internal ValueProvider<bool> _EnableLanguageSelection_ValueProvider_;

		// Token: 0x040001F0 RID: 496
		internal string _RegionDefaultLanguage_MaterializedValue_;

		// Token: 0x040001F1 RID: 497
		internal ValueProvider<string> _RegionDefaultLanguage_ValueProvider_;
	}
}
