using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.HolidayCalendars
{
	// Token: 0x0200005F RID: 95
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal sealed class _DynamicStorageSelection_IHostSettings_Implementation_ : IHostSettings, ISettings, IDataAccessorBackedObject<_DynamicStorageSelection_IHostSettings_DataAccessor_>, IVariantObjectInstance
	{
		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060001EA RID: 490 RVA: 0x00004BC7 File Offset: 0x00002DC7
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060001EB RID: 491 RVA: 0x00004BCF File Offset: 0x00002DCF
		_DynamicStorageSelection_IHostSettings_DataAccessor_ IDataAccessorBackedObject<_DynamicStorageSelection_IHostSettings_DataAccessor_>.DataAccessor
		{
			get
			{
				return this.dataAccessor;
			}
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00004BD7 File Offset: 0x00002DD7
		void IDataAccessorBackedObject<_DynamicStorageSelection_IHostSettings_DataAccessor_>.Initialize(_DynamicStorageSelection_IHostSettings_DataAccessor_ dataAccessor, VariantContextSnapshot context)
		{
			this.dataAccessor = dataAccessor;
			this.context = context;
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060001ED RID: 493 RVA: 0x00004BE7 File Offset: 0x00002DE7
		public string Name
		{
			get
			{
				return this.dataAccessor._Name_MaterializedValue_;
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060001EE RID: 494 RVA: 0x00004BF4 File Offset: 0x00002DF4
		public string Endpoint
		{
			get
			{
				if (this.dataAccessor._Endpoint_ValueProvider_ != null)
				{
					return this.dataAccessor._Endpoint_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._Endpoint_MaterializedValue_;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060001EF RID: 495 RVA: 0x00004C25 File Offset: 0x00002E25
		public int Timeout
		{
			get
			{
				if (this.dataAccessor._Timeout_ValueProvider_ != null)
				{
					return this.dataAccessor._Timeout_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._Timeout_MaterializedValue_;
			}
		}

		// Token: 0x04000162 RID: 354
		private _DynamicStorageSelection_IHostSettings_DataAccessor_ dataAccessor;

		// Token: 0x04000163 RID: 355
		private VariantContextSnapshot context;
	}
}
