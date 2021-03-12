using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.Flighting
{
	// Token: 0x02000023 RID: 35
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal sealed class _DynamicStorageSelection_ICacheExpiryTimeInMinutes_Implementation_ : ICacheExpiryTimeInMinutes, ISettings, IDataAccessorBackedObject<_DynamicStorageSelection_ICacheExpiryTimeInMinutes_DataAccessor_>, IVariantObjectInstance
	{
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600009E RID: 158 RVA: 0x000038FE File Offset: 0x00001AFE
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00003906 File Offset: 0x00001B06
		_DynamicStorageSelection_ICacheExpiryTimeInMinutes_DataAccessor_ IDataAccessorBackedObject<_DynamicStorageSelection_ICacheExpiryTimeInMinutes_DataAccessor_>.DataAccessor
		{
			get
			{
				return this.dataAccessor;
			}
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x0000390E File Offset: 0x00001B0E
		void IDataAccessorBackedObject<_DynamicStorageSelection_ICacheExpiryTimeInMinutes_DataAccessor_>.Initialize(_DynamicStorageSelection_ICacheExpiryTimeInMinutes_DataAccessor_ dataAccessor, VariantContextSnapshot context)
		{
			this.dataAccessor = dataAccessor;
			this.context = context;
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x0000391E File Offset: 0x00001B1E
		public string Name
		{
			get
			{
				return this.dataAccessor._Name_MaterializedValue_;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x0000392B File Offset: 0x00001B2B
		public int Value
		{
			get
			{
				if (this.dataAccessor._Value_ValueProvider_ != null)
				{
					return this.dataAccessor._Value_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._Value_MaterializedValue_;
			}
		}

		// Token: 0x04000070 RID: 112
		private _DynamicStorageSelection_ICacheExpiryTimeInMinutes_DataAccessor_ dataAccessor;

		// Token: 0x04000071 RID: 113
		private VariantContextSnapshot context;
	}
}
