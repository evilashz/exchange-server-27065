using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x0200001C RID: 28
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal sealed class _DataOnly_IBlackoutSettings_Implementation_ : IBlackoutSettings, ISettings, IVariantObjectInstance, IVariantObjectInstanceProvider
	{
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00003781 File Offset: 0x00001981
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003784 File Offset: 0x00001984
		IVariantObjectInstance IVariantObjectInstanceProvider.GetVariantObjectInstance(VariantContextSnapshot context)
		{
			return this;
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00003787 File Offset: 0x00001987
		public string Name
		{
			get
			{
				return this._Name_MaterializedValue_;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000083 RID: 131 RVA: 0x0000378F File Offset: 0x0000198F
		public TimeSpan StartTime
		{
			get
			{
				return this._StartTime_MaterializedValue_;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00003797 File Offset: 0x00001997
		public TimeSpan EndTime
		{
			get
			{
				return this._EndTime_MaterializedValue_;
			}
		}

		// Token: 0x0400005A RID: 90
		internal string _Name_MaterializedValue_;

		// Token: 0x0400005B RID: 91
		internal TimeSpan _StartTime_MaterializedValue_ = default(TimeSpan);

		// Token: 0x0400005C RID: 92
		internal TimeSpan _EndTime_MaterializedValue_ = default(TimeSpan);
	}
}
