using System;
using System.CodeDom.Compiler;
using System.Data.Services.Common;

namespace Microsoft.WindowsAzure.ActiveDirectoryV142
{
	// Token: 0x02000600 RID: 1536
	[DataServiceKey("featureId")]
	public class EnabledFeature
	{
		// Token: 0x06001B4E RID: 6990 RVA: 0x00031DF4 File Offset: 0x0002FFF4
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public static EnabledFeature CreateEnabledFeature(string featureId)
		{
			return new EnabledFeature
			{
				featureId = featureId
			};
		}

		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x06001B4F RID: 6991 RVA: 0x00031E0F File Offset: 0x0003000F
		// (set) Token: 0x06001B50 RID: 6992 RVA: 0x00031E17 File Offset: 0x00030017
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

		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x06001B51 RID: 6993 RVA: 0x00031E20 File Offset: 0x00030020
		// (set) Token: 0x06001B52 RID: 6994 RVA: 0x00031E28 File Offset: 0x00030028
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

		// Token: 0x04001C6E RID: 7278
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _featureId;

		// Token: 0x04001C6F RID: 7279
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _featureName;
	}
}
