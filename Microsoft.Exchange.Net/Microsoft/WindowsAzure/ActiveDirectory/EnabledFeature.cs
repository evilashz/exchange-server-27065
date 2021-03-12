using System;
using System.CodeDom.Compiler;
using System.Data.Services.Common;

namespace Microsoft.WindowsAzure.ActiveDirectory
{
	// Token: 0x020005B3 RID: 1459
	[DataServiceKey("featureId")]
	public class EnabledFeature
	{
		// Token: 0x060015F2 RID: 5618 RVA: 0x0002DA68 File Offset: 0x0002BC68
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public static EnabledFeature CreateEnabledFeature(string featureId)
		{
			return new EnabledFeature
			{
				featureId = featureId
			};
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x060015F3 RID: 5619 RVA: 0x0002DA83 File Offset: 0x0002BC83
		// (set) Token: 0x060015F4 RID: 5620 RVA: 0x0002DA8B File Offset: 0x0002BC8B
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string featureId
		{
			get
			{
				return this._featureId;
			}
			set
			{
				this._featureId = value;
			}
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x060015F5 RID: 5621 RVA: 0x0002DA94 File Offset: 0x0002BC94
		// (set) Token: 0x060015F6 RID: 5622 RVA: 0x0002DA9C File Offset: 0x0002BC9C
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string featureName
		{
			get
			{
				return this._featureName;
			}
			set
			{
				this._featureName = value;
			}
		}

		// Token: 0x040019F3 RID: 6643
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _featureId;

		// Token: 0x040019F4 RID: 6644
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _featureName;
	}
}
