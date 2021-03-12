using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.Search
{
	// Token: 0x02000063 RID: 99
	[EditorBrowsable(EditorBrowsableState.Never)]
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	internal sealed class _DynamicStorageSelection_IIndexStatusSettings_Implementation_ : IIndexStatusSettings, ISettings, IDataAccessorBackedObject<_DynamicStorageSelection_IIndexStatusSettings_DataAccessor_>, IVariantObjectInstance
	{
		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x00004C98 File Offset: 0x00002E98
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060001FA RID: 506 RVA: 0x00004CA0 File Offset: 0x00002EA0
		_DynamicStorageSelection_IIndexStatusSettings_DataAccessor_ IDataAccessorBackedObject<_DynamicStorageSelection_IIndexStatusSettings_DataAccessor_>.DataAccessor
		{
			get
			{
				return this.dataAccessor;
			}
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00004CA8 File Offset: 0x00002EA8
		void IDataAccessorBackedObject<_DynamicStorageSelection_IIndexStatusSettings_DataAccessor_>.Initialize(_DynamicStorageSelection_IIndexStatusSettings_DataAccessor_ dataAccessor, VariantContextSnapshot context)
		{
			this.dataAccessor = dataAccessor;
			this.context = context;
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060001FC RID: 508 RVA: 0x00004CB8 File Offset: 0x00002EB8
		public string Name
		{
			get
			{
				return this.dataAccessor._Name_MaterializedValue_;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060001FD RID: 509 RVA: 0x00004CC5 File Offset: 0x00002EC5
		public TimeSpan InvalidateInterval
		{
			get
			{
				if (this.dataAccessor._InvalidateInterval_ValueProvider_ != null)
				{
					return this.dataAccessor._InvalidateInterval_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._InvalidateInterval_MaterializedValue_;
			}
		}

		// Token: 0x0400016A RID: 362
		private _DynamicStorageSelection_IIndexStatusSettings_DataAccessor_ dataAccessor;

		// Token: 0x0400016B RID: 363
		private VariantContextSnapshot context;
	}
}
