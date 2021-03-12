using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.Flighting
{
	// Token: 0x02000024 RID: 36
	[EditorBrowsable(EditorBrowsableState.Never)]
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	internal sealed class _DataOnly_ICacheExpiryTimeInMinutes_Implementation_ : ICacheExpiryTimeInMinutes, ISettings, IVariantObjectInstance, IVariantObjectInstanceProvider
	{
		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x00003964 File Offset: 0x00001B64
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003967 File Offset: 0x00001B67
		IVariantObjectInstance IVariantObjectInstanceProvider.GetVariantObjectInstance(VariantContextSnapshot context)
		{
			return this;
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x0000396A File Offset: 0x00001B6A
		public string Name
		{
			get
			{
				return this._Name_MaterializedValue_;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00003972 File Offset: 0x00001B72
		public int Value
		{
			get
			{
				return this._Value_MaterializedValue_;
			}
		}

		// Token: 0x04000072 RID: 114
		internal string _Name_MaterializedValue_;

		// Token: 0x04000073 RID: 115
		internal int _Value_MaterializedValue_;
	}
}
