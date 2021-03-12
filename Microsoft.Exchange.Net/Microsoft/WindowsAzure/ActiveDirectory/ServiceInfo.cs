using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.Data.Services.Common;

namespace Microsoft.WindowsAzure.ActiveDirectory
{
	// Token: 0x020005AE RID: 1454
	[DataServiceKey("objectId")]
	public class ServiceInfo : DirectoryObject
	{
		// Token: 0x06001579 RID: 5497 RVA: 0x0002D36C File Offset: 0x0002B56C
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public static ServiceInfo CreateServiceInfo(string objectId, int version, Collection<string> serviceElements)
		{
			ServiceInfo serviceInfo = new ServiceInfo();
			serviceInfo.objectId = objectId;
			serviceInfo.version = version;
			if (serviceElements == null)
			{
				throw new ArgumentNullException("serviceElements");
			}
			serviceInfo.serviceElements = serviceElements;
			return serviceInfo;
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x0600157A RID: 5498 RVA: 0x0002D3A3 File Offset: 0x0002B5A3
		// (set) Token: 0x0600157B RID: 5499 RVA: 0x0002D3AB File Offset: 0x0002B5AB
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string serviceInstance
		{
			get
			{
				return this._serviceInstance;
			}
			set
			{
				this._serviceInstance = value;
			}
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x0600157C RID: 5500 RVA: 0x0002D3B4 File Offset: 0x0002B5B4
		// (set) Token: 0x0600157D RID: 5501 RVA: 0x0002D3BC File Offset: 0x0002B5BC
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public int version
		{
			get
			{
				return this._version;
			}
			set
			{
				this._version = value;
			}
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x0600157E RID: 5502 RVA: 0x0002D3C5 File Offset: 0x0002B5C5
		// (set) Token: 0x0600157F RID: 5503 RVA: 0x0002D3CD File Offset: 0x0002B5CD
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<string> serviceElements
		{
			get
			{
				return this._serviceElements;
			}
			set
			{
				this._serviceElements = value;
			}
		}

		// Token: 0x040019B9 RID: 6585
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _serviceInstance;

		// Token: 0x040019BA RID: 6586
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private int _version;

		// Token: 0x040019BB RID: 6587
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<string> _serviceElements = new Collection<string>();
	}
}
