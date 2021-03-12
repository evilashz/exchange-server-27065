using System;
using System.CodeDom.Compiler;
using System.Data.Services.Common;

namespace Microsoft.WindowsAzure.ActiveDirectoryV142
{
	// Token: 0x020005ED RID: 1517
	[DataServiceKey("objectId")]
	public class ServiceEndpoint : DirectoryObject
	{
		// Token: 0x060019DD RID: 6621 RVA: 0x00030B3C File Offset: 0x0002ED3C
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public static ServiceEndpoint CreateServiceEndpoint(string objectId)
		{
			return new ServiceEndpoint
			{
				objectId = objectId
			};
		}

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x060019DE RID: 6622 RVA: 0x00030B57 File Offset: 0x0002ED57
		// (set) Token: 0x060019DF RID: 6623 RVA: 0x00030B5F File Offset: 0x0002ED5F
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string capability
		{
			get
			{
				return this._capability;
			}
			set
			{
				this._capability = value;
			}
		}

		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x060019E0 RID: 6624 RVA: 0x00030B68 File Offset: 0x0002ED68
		// (set) Token: 0x060019E1 RID: 6625 RVA: 0x00030B70 File Offset: 0x0002ED70
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string serviceId
		{
			get
			{
				return this._serviceId;
			}
			set
			{
				this._serviceId = value;
			}
		}

		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x060019E2 RID: 6626 RVA: 0x00030B79 File Offset: 0x0002ED79
		// (set) Token: 0x060019E3 RID: 6627 RVA: 0x00030B81 File Offset: 0x0002ED81
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string serviceName
		{
			get
			{
				return this._serviceName;
			}
			set
			{
				this._serviceName = value;
			}
		}

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x060019E4 RID: 6628 RVA: 0x00030B8A File Offset: 0x0002ED8A
		// (set) Token: 0x060019E5 RID: 6629 RVA: 0x00030B92 File Offset: 0x0002ED92
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string serviceEndpointUri
		{
			get
			{
				return this._serviceEndpointUri;
			}
			set
			{
				this._serviceEndpointUri = value;
			}
		}

		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x060019E6 RID: 6630 RVA: 0x00030B9B File Offset: 0x0002ED9B
		// (set) Token: 0x060019E7 RID: 6631 RVA: 0x00030BA3 File Offset: 0x0002EDA3
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string serviceResourceId
		{
			get
			{
				return this._serviceResourceId;
			}
			set
			{
				this._serviceResourceId = value;
			}
		}

		// Token: 0x04001BC3 RID: 7107
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _capability;

		// Token: 0x04001BC4 RID: 7108
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _serviceId;

		// Token: 0x04001BC5 RID: 7109
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _serviceName;

		// Token: 0x04001BC6 RID: 7110
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _serviceEndpointUri;

		// Token: 0x04001BC7 RID: 7111
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _serviceResourceId;
	}
}
