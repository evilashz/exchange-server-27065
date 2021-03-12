using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.Flighting
{
	// Token: 0x020000AB RID: 171
	[EditorBrowsable(EditorBrowsableState.Never)]
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	internal sealed class _DataOnly_ITeam_Implementation_ : ITeam, IVariantObjectInstance, IVariantObjectInstanceProvider
	{
		// Token: 0x17000306 RID: 774
		// (get) Token: 0x0600040F RID: 1039 RVA: 0x00006E37 File Offset: 0x00005037
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x00006E3A File Offset: 0x0000503A
		IVariantObjectInstance IVariantObjectInstanceProvider.GetVariantObjectInstance(VariantContextSnapshot context)
		{
			return this;
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000411 RID: 1041 RVA: 0x00006E3D File Offset: 0x0000503D
		public string Name
		{
			get
			{
				return this._Name_MaterializedValue_;
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x00006E45 File Offset: 0x00005045
		public bool IsMember
		{
			get
			{
				return this._IsMember_MaterializedValue_;
			}
		}

		// Token: 0x04000310 RID: 784
		internal string _Name_MaterializedValue_;

		// Token: 0x04000311 RID: 785
		internal bool _IsMember_MaterializedValue_;
	}
}
