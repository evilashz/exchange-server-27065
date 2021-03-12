using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.AutoDiscover
{
	// Token: 0x02000098 RID: 152
	[EditorBrowsable(EditorBrowsableState.Never)]
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	internal sealed class _DataOnly_IOWAUrl_Implementation_ : IOWAUrl, ISettings, IVariantObjectInstance, IVariantObjectInstanceProvider
	{
		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x00006876 File Offset: 0x00004A76
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x00006879 File Offset: 0x00004A79
		IVariantObjectInstance IVariantObjectInstanceProvider.GetVariantObjectInstance(VariantContextSnapshot context)
		{
			return this;
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x060003A7 RID: 935 RVA: 0x0000687C File Offset: 0x00004A7C
		public string Name
		{
			get
			{
				return this._Name_MaterializedValue_;
			}
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x00006884 File Offset: 0x00004A84
		public string OwaInternalAuthMethods
		{
			get
			{
				return this._OwaInternalAuthMethods_MaterializedValue_;
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x060003A9 RID: 937 RVA: 0x0000688C File Offset: 0x00004A8C
		public string OwaExternalAuthMethods
		{
			get
			{
				return this._OwaExternalAuthMethods_MaterializedValue_;
			}
		}

		// Token: 0x040002C5 RID: 709
		internal string _Name_MaterializedValue_;

		// Token: 0x040002C6 RID: 710
		internal string _OwaInternalAuthMethods_MaterializedValue_;

		// Token: 0x040002C7 RID: 711
		internal string _OwaExternalAuthMethods_MaterializedValue_;
	}
}
