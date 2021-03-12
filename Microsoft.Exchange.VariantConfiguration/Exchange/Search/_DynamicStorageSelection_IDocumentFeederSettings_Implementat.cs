using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.Search
{
	// Token: 0x0200004B RID: 75
	[EditorBrowsable(EditorBrowsableState.Never)]
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	internal sealed class _DynamicStorageSelection_IDocumentFeederSettings_Implementation_ : IDocumentFeederSettings, ISettings, IDataAccessorBackedObject<_DynamicStorageSelection_IDocumentFeederSettings_DataAccessor_>, IVariantObjectInstance
	{
		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000199 RID: 409 RVA: 0x000047D6 File Offset: 0x000029D6
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600019A RID: 410 RVA: 0x000047DE File Offset: 0x000029DE
		_DynamicStorageSelection_IDocumentFeederSettings_DataAccessor_ IDataAccessorBackedObject<_DynamicStorageSelection_IDocumentFeederSettings_DataAccessor_>.DataAccessor
		{
			get
			{
				return this.dataAccessor;
			}
		}

		// Token: 0x0600019B RID: 411 RVA: 0x000047E6 File Offset: 0x000029E6
		void IDataAccessorBackedObject<_DynamicStorageSelection_IDocumentFeederSettings_DataAccessor_>.Initialize(_DynamicStorageSelection_IDocumentFeederSettings_DataAccessor_ dataAccessor, VariantContextSnapshot context)
		{
			this.dataAccessor = dataAccessor;
			this.context = context;
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x0600019C RID: 412 RVA: 0x000047F6 File Offset: 0x000029F6
		public string Name
		{
			get
			{
				return this.dataAccessor._Name_MaterializedValue_;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600019D RID: 413 RVA: 0x00004803 File Offset: 0x00002A03
		public TimeSpan BatchTimeout
		{
			get
			{
				if (this.dataAccessor._BatchTimeout_ValueProvider_ != null)
				{
					return this.dataAccessor._BatchTimeout_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._BatchTimeout_MaterializedValue_;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600019E RID: 414 RVA: 0x00004834 File Offset: 0x00002A34
		public TimeSpan LostCallbackTimeout
		{
			get
			{
				if (this.dataAccessor._LostCallbackTimeout_ValueProvider_ != null)
				{
					return this.dataAccessor._LostCallbackTimeout_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._LostCallbackTimeout_MaterializedValue_;
			}
		}

		// Token: 0x04000130 RID: 304
		private _DynamicStorageSelection_IDocumentFeederSettings_DataAccessor_ dataAccessor;

		// Token: 0x04000131 RID: 305
		private VariantContextSnapshot context;
	}
}
