using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000083 RID: 131
	[EditorBrowsable(EditorBrowsableState.Never)]
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	internal sealed class _DynamicStorageSelection_IMdmSupportedPlatformsSettings_Implementation_ : IMdmSupportedPlatformsSettings, ISettings, IDataAccessorBackedObject<_DynamicStorageSelection_IMdmSupportedPlatformsSettings_DataAccessor_>, IVariantObjectInstance
	{
		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000327 RID: 807 RVA: 0x00006109 File Offset: 0x00004309
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000328 RID: 808 RVA: 0x00006111 File Offset: 0x00004311
		_DynamicStorageSelection_IMdmSupportedPlatformsSettings_DataAccessor_ IDataAccessorBackedObject<_DynamicStorageSelection_IMdmSupportedPlatformsSettings_DataAccessor_>.DataAccessor
		{
			get
			{
				return this.dataAccessor;
			}
		}

		// Token: 0x06000329 RID: 809 RVA: 0x00006119 File Offset: 0x00004319
		void IDataAccessorBackedObject<_DynamicStorageSelection_IMdmSupportedPlatformsSettings_DataAccessor_>.Initialize(_DynamicStorageSelection_IMdmSupportedPlatformsSettings_DataAccessor_ dataAccessor, VariantContextSnapshot context)
		{
			this.dataAccessor = dataAccessor;
			this.context = context;
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x0600032A RID: 810 RVA: 0x00006129 File Offset: 0x00004329
		public string Name
		{
			get
			{
				return this.dataAccessor._Name_MaterializedValue_;
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x0600032B RID: 811 RVA: 0x00006136 File Offset: 0x00004336
		public string PlatformsSupported
		{
			get
			{
				if (this.dataAccessor._PlatformsSupported_ValueProvider_ != null)
				{
					return this.dataAccessor._PlatformsSupported_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._PlatformsSupported_MaterializedValue_;
			}
		}

		// Token: 0x04000268 RID: 616
		private _DynamicStorageSelection_IMdmSupportedPlatformsSettings_DataAccessor_ dataAccessor;

		// Token: 0x04000269 RID: 617
		private VariantContextSnapshot context;
	}
}
