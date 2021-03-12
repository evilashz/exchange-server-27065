using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.Search
{
	// Token: 0x02000058 RID: 88
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal sealed class _DataOnly_IFeederSettings_Implementation_ : IFeederSettings, ISettings, IVariantObjectInstance, IVariantObjectInstanceProvider
	{
		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060001CB RID: 459 RVA: 0x00004A6A File Offset: 0x00002C6A
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00004A6D File Offset: 0x00002C6D
		IVariantObjectInstance IVariantObjectInstanceProvider.GetVariantObjectInstance(VariantContextSnapshot context)
		{
			return this;
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060001CD RID: 461 RVA: 0x00004A70 File Offset: 0x00002C70
		public string Name
		{
			get
			{
				return this._Name_MaterializedValue_;
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060001CE RID: 462 RVA: 0x00004A78 File Offset: 0x00002C78
		public int QueueSize
		{
			get
			{
				return this._QueueSize_MaterializedValue_;
			}
		}

		// Token: 0x0400014B RID: 331
		internal string _Name_MaterializedValue_;

		// Token: 0x0400014C RID: 332
		internal int _QueueSize_MaterializedValue_;
	}
}
