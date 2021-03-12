using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.Search
{
	// Token: 0x0200004C RID: 76
	[EditorBrowsable(EditorBrowsableState.Never)]
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	internal sealed class _DataOnly_IDocumentFeederSettings_Implementation_ : IDocumentFeederSettings, ISettings, IVariantObjectInstance, IVariantObjectInstanceProvider
	{
		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x0000486D File Offset: 0x00002A6D
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00004870 File Offset: 0x00002A70
		IVariantObjectInstance IVariantObjectInstanceProvider.GetVariantObjectInstance(VariantContextSnapshot context)
		{
			return this;
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x00004873 File Offset: 0x00002A73
		public string Name
		{
			get
			{
				return this._Name_MaterializedValue_;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x0000487B File Offset: 0x00002A7B
		public TimeSpan BatchTimeout
		{
			get
			{
				return this._BatchTimeout_MaterializedValue_;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00004883 File Offset: 0x00002A83
		public TimeSpan LostCallbackTimeout
		{
			get
			{
				return this._LostCallbackTimeout_MaterializedValue_;
			}
		}

		// Token: 0x04000132 RID: 306
		internal string _Name_MaterializedValue_;

		// Token: 0x04000133 RID: 307
		internal TimeSpan _BatchTimeout_MaterializedValue_ = default(TimeSpan);

		// Token: 0x04000134 RID: 308
		internal TimeSpan _LostCallbackTimeout_MaterializedValue_ = default(TimeSpan);
	}
}
