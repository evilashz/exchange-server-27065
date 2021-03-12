using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.Calendar
{
	// Token: 0x02000028 RID: 40
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal sealed class _DataOnly_ICalendarIcalConversionSettings_Implementation_ : ICalendarIcalConversionSettings, ISettings, IVariantObjectInstance, IVariantObjectInstanceProvider
	{
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x000039F0 File Offset: 0x00001BF0
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000039F3 File Offset: 0x00001BF3
		IVariantObjectInstance IVariantObjectInstanceProvider.GetVariantObjectInstance(VariantContextSnapshot context)
		{
			return this;
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x000039F6 File Offset: 0x00001BF6
		public string Name
		{
			get
			{
				return this._Name_MaterializedValue_;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x000039FE File Offset: 0x00001BFE
		public bool LocalTimeZoneReferenceForRecurrenceNeeded
		{
			get
			{
				return this._LocalTimeZoneReferenceForRecurrenceNeeded_MaterializedValue_;
			}
		}

		// Token: 0x04000079 RID: 121
		internal string _Name_MaterializedValue_;

		// Token: 0x0400007A RID: 122
		internal bool _LocalTimeZoneReferenceForRecurrenceNeeded_MaterializedValue_;
	}
}
