using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.Search
{
	// Token: 0x02000073 RID: 115
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal sealed class _DynamicStorageSelection_ILanguageDetection_Implementation_ : ILanguageDetection, ISettings, IDataAccessorBackedObject<_DynamicStorageSelection_ILanguageDetection_DataAccessor_>, IVariantObjectInstance
	{
		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000296 RID: 662 RVA: 0x00005666 File Offset: 0x00003866
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000297 RID: 663 RVA: 0x0000566E File Offset: 0x0000386E
		_DynamicStorageSelection_ILanguageDetection_DataAccessor_ IDataAccessorBackedObject<_DynamicStorageSelection_ILanguageDetection_DataAccessor_>.DataAccessor
		{
			get
			{
				return this.dataAccessor;
			}
		}

		// Token: 0x06000298 RID: 664 RVA: 0x00005676 File Offset: 0x00003876
		void IDataAccessorBackedObject<_DynamicStorageSelection_ILanguageDetection_DataAccessor_>.Initialize(_DynamicStorageSelection_ILanguageDetection_DataAccessor_ dataAccessor, VariantContextSnapshot context)
		{
			this.dataAccessor = dataAccessor;
			this.context = context;
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000299 RID: 665 RVA: 0x00005686 File Offset: 0x00003886
		public string Name
		{
			get
			{
				return this.dataAccessor._Name_MaterializedValue_;
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x0600029A RID: 666 RVA: 0x00005693 File Offset: 0x00003893
		public bool EnableLanguageDetectionLogging
		{
			get
			{
				if (this.dataAccessor._EnableLanguageDetectionLogging_ValueProvider_ != null)
				{
					return this.dataAccessor._EnableLanguageDetectionLogging_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._EnableLanguageDetectionLogging_MaterializedValue_;
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x0600029B RID: 667 RVA: 0x000056C4 File Offset: 0x000038C4
		public int SamplingFrequency
		{
			get
			{
				if (this.dataAccessor._SamplingFrequency_ValueProvider_ != null)
				{
					return this.dataAccessor._SamplingFrequency_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._SamplingFrequency_MaterializedValue_;
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x0600029C RID: 668 RVA: 0x000056F5 File Offset: 0x000038F5
		public bool EnableLanguageSelection
		{
			get
			{
				if (this.dataAccessor._EnableLanguageSelection_ValueProvider_ != null)
				{
					return this.dataAccessor._EnableLanguageSelection_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._EnableLanguageSelection_MaterializedValue_;
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x0600029D RID: 669 RVA: 0x00005726 File Offset: 0x00003926
		public string RegionDefaultLanguage
		{
			get
			{
				if (this.dataAccessor._RegionDefaultLanguage_ValueProvider_ != null)
				{
					return this.dataAccessor._RegionDefaultLanguage_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._RegionDefaultLanguage_MaterializedValue_;
			}
		}

		// Token: 0x040001F2 RID: 498
		private _DynamicStorageSelection_ILanguageDetection_DataAccessor_ dataAccessor;

		// Token: 0x040001F3 RID: 499
		private VariantContextSnapshot context;
	}
}
