using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.Flighting
{
	// Token: 0x020000B4 RID: 180
	[EditorBrowsable(EditorBrowsableState.Never)]
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	internal sealed class _DataOnly_IUrlMapping_Implementation_ : IUrlMapping, ISettings, IVariantObjectInstance, IVariantObjectInstanceProvider
	{
		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000433 RID: 1075 RVA: 0x00006FF2 File Offset: 0x000051F2
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x00006FF5 File Offset: 0x000051F5
		IVariantObjectInstance IVariantObjectInstanceProvider.GetVariantObjectInstance(VariantContextSnapshot context)
		{
			return this;
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000435 RID: 1077 RVA: 0x00006FF8 File Offset: 0x000051F8
		public string Name
		{
			get
			{
				return this._Name_MaterializedValue_;
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000436 RID: 1078 RVA: 0x00007000 File Offset: 0x00005200
		public string Url
		{
			get
			{
				return this._Url_MaterializedValue_;
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000437 RID: 1079 RVA: 0x00007008 File Offset: 0x00005208
		public string RemapTo
		{
			get
			{
				return this._RemapTo_MaterializedValue_;
			}
		}

		// Token: 0x04000326 RID: 806
		internal string _Name_MaterializedValue_;

		// Token: 0x04000327 RID: 807
		internal string _Url_MaterializedValue_;

		// Token: 0x04000328 RID: 808
		internal string _RemapTo_MaterializedValue_;
	}
}
