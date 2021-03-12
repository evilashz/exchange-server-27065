using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.Flighting
{
	// Token: 0x0200004F RID: 79
	[EditorBrowsable(EditorBrowsableState.Never)]
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	internal sealed class _DynamicStorageSelection_IEacWebRequest_Implementation_ : IEacWebRequest, ISettings, IDataAccessorBackedObject<_DynamicStorageSelection_IEacWebRequest_DataAccessor_>, IVariantObjectInstance
	{
		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x000048B3 File Offset: 0x00002AB3
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060001AA RID: 426 RVA: 0x000048BB File Offset: 0x00002ABB
		_DynamicStorageSelection_IEacWebRequest_DataAccessor_ IDataAccessorBackedObject<_DynamicStorageSelection_IEacWebRequest_DataAccessor_>.DataAccessor
		{
			get
			{
				return this.dataAccessor;
			}
		}

		// Token: 0x060001AB RID: 427 RVA: 0x000048C3 File Offset: 0x00002AC3
		void IDataAccessorBackedObject<_DynamicStorageSelection_IEacWebRequest_DataAccessor_>.Initialize(_DynamicStorageSelection_IEacWebRequest_DataAccessor_ dataAccessor, VariantContextSnapshot context)
		{
			this.dataAccessor = dataAccessor;
			this.context = context;
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060001AC RID: 428 RVA: 0x000048D3 File Offset: 0x00002AD3
		public string Name
		{
			get
			{
				return this.dataAccessor._Name_MaterializedValue_;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060001AD RID: 429 RVA: 0x000048E0 File Offset: 0x00002AE0
		public string Request
		{
			get
			{
				if (this.dataAccessor._Request_ValueProvider_ != null)
				{
					return this.dataAccessor._Request_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._Request_MaterializedValue_;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00004911 File Offset: 0x00002B11
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

		// Token: 0x0400013A RID: 314
		private _DynamicStorageSelection_IEacWebRequest_DataAccessor_ dataAccessor;

		// Token: 0x0400013B RID: 315
		private VariantContextSnapshot context;
	}
}
