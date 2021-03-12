using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.Flighting
{
	// Token: 0x02000050 RID: 80
	[EditorBrowsable(EditorBrowsableState.Never)]
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	internal sealed class _DataOnly_IEacWebRequest_Implementation_ : IEacWebRequest, ISettings, IVariantObjectInstance, IVariantObjectInstanceProvider
	{
		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x0000494A File Offset: 0x00002B4A
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000494D File Offset: 0x00002B4D
		IVariantObjectInstance IVariantObjectInstanceProvider.GetVariantObjectInstance(VariantContextSnapshot context)
		{
			return this;
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x00004950 File Offset: 0x00002B50
		public string Name
		{
			get
			{
				return this._Name_MaterializedValue_;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x00004958 File Offset: 0x00002B58
		public string Request
		{
			get
			{
				return this._Request_MaterializedValue_;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x00004960 File Offset: 0x00002B60
		public bool Enabled
		{
			get
			{
				return this._Enabled_MaterializedValue_;
			}
		}

		// Token: 0x0400013C RID: 316
		internal string _Name_MaterializedValue_;

		// Token: 0x0400013D RID: 317
		internal string _Request_MaterializedValue_;

		// Token: 0x0400013E RID: 318
		internal bool _Enabled_MaterializedValue_;
	}
}
