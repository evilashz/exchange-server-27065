using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.Calendar
{
	// Token: 0x02000027 RID: 39
	[EditorBrowsable(EditorBrowsableState.Never)]
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	internal sealed class _DynamicStorageSelection_ICalendarIcalConversionSettings_Implementation_ : ICalendarIcalConversionSettings, ISettings, IDataAccessorBackedObject<_DynamicStorageSelection_ICalendarIcalConversionSettings_DataAccessor_>, IVariantObjectInstance
	{
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000AB RID: 171 RVA: 0x0000398A File Offset: 0x00001B8A
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00003992 File Offset: 0x00001B92
		_DynamicStorageSelection_ICalendarIcalConversionSettings_DataAccessor_ IDataAccessorBackedObject<_DynamicStorageSelection_ICalendarIcalConversionSettings_DataAccessor_>.DataAccessor
		{
			get
			{
				return this.dataAccessor;
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x0000399A File Offset: 0x00001B9A
		void IDataAccessorBackedObject<_DynamicStorageSelection_ICalendarIcalConversionSettings_DataAccessor_>.Initialize(_DynamicStorageSelection_ICalendarIcalConversionSettings_DataAccessor_ dataAccessor, VariantContextSnapshot context)
		{
			this.dataAccessor = dataAccessor;
			this.context = context;
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000AE RID: 174 RVA: 0x000039AA File Offset: 0x00001BAA
		public string Name
		{
			get
			{
				return this.dataAccessor._Name_MaterializedValue_;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000AF RID: 175 RVA: 0x000039B7 File Offset: 0x00001BB7
		public bool LocalTimeZoneReferenceForRecurrenceNeeded
		{
			get
			{
				if (this.dataAccessor._LocalTimeZoneReferenceForRecurrenceNeeded_ValueProvider_ != null)
				{
					return this.dataAccessor._LocalTimeZoneReferenceForRecurrenceNeeded_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._LocalTimeZoneReferenceForRecurrenceNeeded_MaterializedValue_;
			}
		}

		// Token: 0x04000077 RID: 119
		private _DynamicStorageSelection_ICalendarIcalConversionSettings_DataAccessor_ dataAccessor;

		// Token: 0x04000078 RID: 120
		private VariantContextSnapshot context;
	}
}
