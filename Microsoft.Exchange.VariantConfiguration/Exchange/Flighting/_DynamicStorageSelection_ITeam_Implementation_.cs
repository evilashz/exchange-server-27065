using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.Flighting
{
	// Token: 0x020000AA RID: 170
	[EditorBrowsable(EditorBrowsableState.Never)]
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	internal sealed class _DynamicStorageSelection_ITeam_Implementation_ : ITeam, IDataAccessorBackedObject<_DynamicStorageSelection_ITeam_DataAccessor_>, IVariantObjectInstance
	{
		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000409 RID: 1033 RVA: 0x00006DD1 File Offset: 0x00004FD1
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x0600040A RID: 1034 RVA: 0x00006DD9 File Offset: 0x00004FD9
		_DynamicStorageSelection_ITeam_DataAccessor_ IDataAccessorBackedObject<_DynamicStorageSelection_ITeam_DataAccessor_>.DataAccessor
		{
			get
			{
				return this.dataAccessor;
			}
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x00006DE1 File Offset: 0x00004FE1
		void IDataAccessorBackedObject<_DynamicStorageSelection_ITeam_DataAccessor_>.Initialize(_DynamicStorageSelection_ITeam_DataAccessor_ dataAccessor, VariantContextSnapshot context)
		{
			this.dataAccessor = dataAccessor;
			this.context = context;
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x0600040C RID: 1036 RVA: 0x00006DF1 File Offset: 0x00004FF1
		public string Name
		{
			get
			{
				return this.dataAccessor._Name_MaterializedValue_;
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x00006DFE File Offset: 0x00004FFE
		public bool IsMember
		{
			get
			{
				if (this.dataAccessor._IsMember_ValueProvider_ != null)
				{
					return this.dataAccessor._IsMember_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._IsMember_MaterializedValue_;
			}
		}

		// Token: 0x0400030E RID: 782
		private _DynamicStorageSelection_ITeam_DataAccessor_ dataAccessor;

		// Token: 0x0400030F RID: 783
		private VariantContextSnapshot context;
	}
}
