using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.MessageDepot
{
	// Token: 0x0200008B RID: 139
	[EditorBrowsable(EditorBrowsableState.Never)]
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	internal sealed class _DynamicStorageSelection_IMessageDepotSettings_Implementation_ : IMessageDepotSettings, ISettings, IDataAccessorBackedObject<_DynamicStorageSelection_IMessageDepotSettings_DataAccessor_>, IVariantObjectInstance
	{
		// Token: 0x17000290 RID: 656
		// (get) Token: 0x0600036F RID: 879 RVA: 0x00006578 File Offset: 0x00004778
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000370 RID: 880 RVA: 0x00006580 File Offset: 0x00004780
		_DynamicStorageSelection_IMessageDepotSettings_DataAccessor_ IDataAccessorBackedObject<_DynamicStorageSelection_IMessageDepotSettings_DataAccessor_>.DataAccessor
		{
			get
			{
				return this.dataAccessor;
			}
		}

		// Token: 0x06000371 RID: 881 RVA: 0x00006588 File Offset: 0x00004788
		void IDataAccessorBackedObject<_DynamicStorageSelection_IMessageDepotSettings_DataAccessor_>.Initialize(_DynamicStorageSelection_IMessageDepotSettings_DataAccessor_ dataAccessor, VariantContextSnapshot context)
		{
			this.dataAccessor = dataAccessor;
			this.context = context;
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000372 RID: 882 RVA: 0x00006598 File Offset: 0x00004798
		public string Name
		{
			get
			{
				return this.dataAccessor._Name_MaterializedValue_;
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000373 RID: 883 RVA: 0x000065A5 File Offset: 0x000047A5
		public bool Enabled
		{
			get
			{
				if (this.dataAccessor._Enabled_ValueProvider_ != null)
				{
					return this.dataAccessor._Enabled_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._Enabled_MaterializedValue_;
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000374 RID: 884 RVA: 0x000065D6 File Offset: 0x000047D6
		public IList<DayOfWeek> EnabledOnDaysOfWeek
		{
			get
			{
				if (this.dataAccessor._EnabledOnDaysOfWeek_ValueProvider_ != null)
				{
					return this.dataAccessor._EnabledOnDaysOfWeek_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._EnabledOnDaysOfWeek_MaterializedValue_;
			}
		}

		// Token: 0x040002A5 RID: 677
		private _DynamicStorageSelection_IMessageDepotSettings_DataAccessor_ dataAccessor;

		// Token: 0x040002A6 RID: 678
		private VariantContextSnapshot context;
	}
}
