using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.Flighting
{
	// Token: 0x020000BA RID: 186
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal sealed class _DynamicStorageSelection_IVersion_Implementation_ : IVersion, ISettings, IDataAccessorBackedObject<_DynamicStorageSelection_IVersion_DataAccessor_>, IVariantObjectInstance
	{
		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000449 RID: 1097 RVA: 0x000070E5 File Offset: 0x000052E5
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x0600044A RID: 1098 RVA: 0x000070ED File Offset: 0x000052ED
		_DynamicStorageSelection_IVersion_DataAccessor_ IDataAccessorBackedObject<_DynamicStorageSelection_IVersion_DataAccessor_>.DataAccessor
		{
			get
			{
				return this.dataAccessor;
			}
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x000070F5 File Offset: 0x000052F5
		void IDataAccessorBackedObject<_DynamicStorageSelection_IVersion_DataAccessor_>.Initialize(_DynamicStorageSelection_IVersion_DataAccessor_ dataAccessor, VariantContextSnapshot context)
		{
			this.dataAccessor = dataAccessor;
			this.context = context;
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x0600044C RID: 1100 RVA: 0x00007105 File Offset: 0x00005305
		public string Name
		{
			get
			{
				return this.dataAccessor._Name_MaterializedValue_;
			}
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x0600044D RID: 1101 RVA: 0x00007112 File Offset: 0x00005312
		public string VersionNum
		{
			get
			{
				if (this.dataAccessor._VersionNum_ValueProvider_ != null)
				{
					return this.dataAccessor._VersionNum_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._VersionNum_MaterializedValue_;
			}
		}

		// Token: 0x04000336 RID: 822
		private _DynamicStorageSelection_IVersion_DataAccessor_ dataAccessor;

		// Token: 0x04000337 RID: 823
		private VariantContextSnapshot context;
	}
}
