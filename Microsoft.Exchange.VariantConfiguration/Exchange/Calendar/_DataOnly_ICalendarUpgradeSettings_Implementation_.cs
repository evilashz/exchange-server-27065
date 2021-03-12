using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.Calendar
{
	// Token: 0x02000030 RID: 48
	[EditorBrowsable(EditorBrowsableState.Never)]
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	internal sealed class _DataOnly_ICalendarUpgradeSettings_Implementation_ : ICalendarUpgradeSettings, ISettings, IVariantObjectInstance, IVariantObjectInstanceProvider
	{
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060000DD RID: 221 RVA: 0x00003C8E File Offset: 0x00001E8E
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00003C91 File Offset: 0x00001E91
		IVariantObjectInstance IVariantObjectInstanceProvider.GetVariantObjectInstance(VariantContextSnapshot context)
		{
			return this;
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060000DF RID: 223 RVA: 0x00003C94 File Offset: 0x00001E94
		public string Name
		{
			get
			{
				return this._Name_MaterializedValue_;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00003C9C File Offset: 0x00001E9C
		public int MinCalendarItemsForUpgrade
		{
			get
			{
				return this._MinCalendarItemsForUpgrade_MaterializedValue_;
			}
		}

		// Token: 0x04000099 RID: 153
		internal string _Name_MaterializedValue_;

		// Token: 0x0400009A RID: 154
		internal int _MinCalendarItemsForUpgrade_MaterializedValue_;
	}
}
