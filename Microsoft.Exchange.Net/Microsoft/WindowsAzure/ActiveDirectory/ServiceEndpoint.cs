using System;
using System.CodeDom.Compiler;
using System.Data.Services.Common;

namespace Microsoft.WindowsAzure.ActiveDirectory
{
	// Token: 0x020005A4 RID: 1444
	[DataServiceKey("objectId")]
	public class ServiceEndpoint : DirectoryObject
	{
		// Token: 0x060014AF RID: 5295 RVA: 0x0002CA28 File Offset: 0x0002AC28
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public static ServiceEndpoint CreateServiceEndpoint(string objectId)
		{
			return new ServiceEndpoint
			{
				objectId = objectId
			};
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x060014B0 RID: 5296 RVA: 0x0002CA43 File Offset: 0x0002AC43
		// (set) Token: 0x060014B1 RID: 5297 RVA: 0x0002CA4B File Offset: 0x0002AC4B
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

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x060014B2 RID: 5298 RVA: 0x0002CA54 File Offset: 0x0002AC54
		// (set) Token: 0x060014B3 RID: 5299 RVA: 0x0002CA5C File Offset: 0x0002AC5C
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

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x060014B4 RID: 5300 RVA: 0x0002CA65 File Offset: 0x0002AC65
		// (set) Token: 0x060014B5 RID: 5301 RVA: 0x0002CA6D File Offset: 0x0002AC6D
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

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x060014B6 RID: 5302 RVA: 0x0002CA76 File Offset: 0x0002AC76
		// (set) Token: 0x060014B7 RID: 5303 RVA: 0x0002CA7E File Offset: 0x0002AC7E
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

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x060014B8 RID: 5304 RVA: 0x0002CA87 File Offset: 0x0002AC87
		// (set) Token: 0x060014B9 RID: 5305 RVA: 0x0002CA8F File Offset: 0x0002AC8F
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

		// Token: 0x0400195E RID: 6494
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _capability;

		// Token: 0x0400195F RID: 6495
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _serviceId;

		// Token: 0x04001960 RID: 6496
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _serviceName;

		// Token: 0x04001961 RID: 6497
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _serviceEndpointUri;

		// Token: 0x04001962 RID: 6498
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _serviceResourceId;
	}
}
