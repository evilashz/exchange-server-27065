using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.HolidayCalendars
{
	// Token: 0x02000060 RID: 96
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal sealed class _DataOnly_IHostSettings_Implementation_ : IHostSettings, ISettings, IVariantObjectInstance, IVariantObjectInstanceProvider
	{
		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x00004C5E File Offset: 0x00002E5E
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00004C61 File Offset: 0x00002E61
		IVariantObjectInstance IVariantObjectInstanceProvider.GetVariantObjectInstance(VariantContextSnapshot context)
		{
			return this;
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x00004C64 File Offset: 0x00002E64
		public string Name
		{
			get
			{
				return this._Name_MaterializedValue_;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x00004C6C File Offset: 0x00002E6C
		public string Endpoint
		{
			get
			{
				return this._Endpoint_MaterializedValue_;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x00004C74 File Offset: 0x00002E74
		public int Timeout
		{
			get
			{
				return this._Timeout_MaterializedValue_;
			}
		}

		// Token: 0x04000164 RID: 356
		internal string _Name_MaterializedValue_;

		// Token: 0x04000165 RID: 357
		internal string _Endpoint_MaterializedValue_;

		// Token: 0x04000166 RID: 358
		internal int _Timeout_MaterializedValue_;
	}
}
