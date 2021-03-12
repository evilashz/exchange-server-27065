using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.MessageDepot
{
	// Token: 0x0200008C RID: 140
	[EditorBrowsable(EditorBrowsableState.Never)]
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	internal sealed class _DataOnly_IMessageDepotSettings_Implementation_ : IMessageDepotSettings, ISettings, IVariantObjectInstance, IVariantObjectInstanceProvider
	{
		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000376 RID: 886 RVA: 0x0000660F File Offset: 0x0000480F
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000377 RID: 887 RVA: 0x00006612 File Offset: 0x00004812
		IVariantObjectInstance IVariantObjectInstanceProvider.GetVariantObjectInstance(VariantContextSnapshot context)
		{
			return this;
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000378 RID: 888 RVA: 0x00006615 File Offset: 0x00004815
		public string Name
		{
			get
			{
				return this._Name_MaterializedValue_;
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000379 RID: 889 RVA: 0x0000661D File Offset: 0x0000481D
		public bool Enabled
		{
			get
			{
				return this._Enabled_MaterializedValue_;
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x0600037A RID: 890 RVA: 0x00006625 File Offset: 0x00004825
		public IList<DayOfWeek> EnabledOnDaysOfWeek
		{
			get
			{
				return this._EnabledOnDaysOfWeek_MaterializedValue_;
			}
		}

		// Token: 0x040002A7 RID: 679
		internal string _Name_MaterializedValue_;

		// Token: 0x040002A8 RID: 680
		internal bool _Enabled_MaterializedValue_;

		// Token: 0x040002A9 RID: 681
		internal IList<DayOfWeek> _EnabledOnDaysOfWeek_MaterializedValue_;
	}
}
