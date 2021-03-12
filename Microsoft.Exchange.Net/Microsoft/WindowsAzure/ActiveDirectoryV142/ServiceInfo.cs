using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.Data.Services.Common;

namespace Microsoft.WindowsAzure.ActiveDirectoryV142
{
	// Token: 0x020005F9 RID: 1529
	[DataServiceKey("objectId")]
	public class ServiceInfo : DirectoryObject
	{
		// Token: 0x06001ABE RID: 6846 RVA: 0x000315E4 File Offset: 0x0002F7E4
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

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x06001ABF RID: 6847 RVA: 0x0003161B File Offset: 0x0002F81B
		// (set) Token: 0x06001AC0 RID: 6848 RVA: 0x00031623 File Offset: 0x0002F823
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

		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x06001AC1 RID: 6849 RVA: 0x0003162C File Offset: 0x0002F82C
		// (set) Token: 0x06001AC2 RID: 6850 RVA: 0x00031634 File Offset: 0x0002F834
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

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x06001AC3 RID: 6851 RVA: 0x0003163D File Offset: 0x0002F83D
		// (set) Token: 0x06001AC4 RID: 6852 RVA: 0x00031645 File Offset: 0x0002F845
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

		// Token: 0x04001C29 RID: 7209
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _serviceInstance;

		// Token: 0x04001C2A RID: 7210
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private int _version;

		// Token: 0x04001C2B RID: 7211
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<string> _serviceElements = new Collection<string>();
	}
}
