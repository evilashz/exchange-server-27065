using System;
using System.Security.Principal;
using Microsoft.Exchange.Diagnostics.Components.ObjectModel;

namespace Microsoft.Exchange.Configuration.ObjectModel
{
	// Token: 0x0200001C RID: 28
	[Serializable]
	public class DataSourceInfo
	{
		// Token: 0x06000100 RID: 256 RVA: 0x000051C2 File Offset: 0x000033C2
		public DataSourceInfo()
		{
			ExTraceGlobals.DataSourceInfoTracer.Information((long)this.GetHashCode(), "DataSourceInfo::DataSourceInfo - initializing data source info.");
		}

		// Token: 0x06000101 RID: 257 RVA: 0x000051EB File Offset: 0x000033EB
		public DataSourceInfo(string path)
		{
			ExTraceGlobals.DataSourceInfoTracer.Information((long)this.GetHashCode(), "DataSourceInfo::DataSourceInfo - initializing data source info.");
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00005214 File Offset: 0x00003414
		public DataSourceInfo(DataSourceInfo template)
		{
			ExTraceGlobals.DataSourceInfoTracer.Information((long)this.GetHashCode(), "DataSourceInfo::DataSourceInfo - initializing data source info from template.");
			if (template != null)
			{
				this.managementServer = template.ManagementServer;
				this.connectionString = template.ConnectionString;
				this.userName = template.UserName;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000103 RID: 259 RVA: 0x0000526F File Offset: 0x0000346F
		// (set) Token: 0x06000104 RID: 260 RVA: 0x00005276 File Offset: 0x00003476
		public static string DefaultManagementServer
		{
			get
			{
				return DataSourceInfo.defaultManagementServer;
			}
			set
			{
				DataSourceInfo.defaultManagementServer = value;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000105 RID: 261 RVA: 0x0000527E File Offset: 0x0000347E
		public string ConnectionString
		{
			get
			{
				return this.connectionString;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00005286 File Offset: 0x00003486
		// (set) Token: 0x06000107 RID: 263 RVA: 0x0000528E File Offset: 0x0000348E
		public string ManagementServer
		{
			get
			{
				return this.managementServer;
			}
			set
			{
				ExTraceGlobals.DataSourceInfoTracer.Information<string>((long)this.GetHashCode(), "DataSourceInfo::ManagementServer - setting ManagementServer to {0}.", (this.managementServer == null) ? "null" : this.ManagementServer);
				this.managementServer = value;
				this.OnInstanceDataChanged();
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000108 RID: 264 RVA: 0x000052C8 File Offset: 0x000034C8
		public string ManagementEndpointUrl
		{
			get
			{
				if (this.managementServer != null)
				{
					return "tcp://" + this.managementServer + ":8085/ExchangeAdministration";
				}
				return null;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000109 RID: 265 RVA: 0x000052EC File Offset: 0x000034EC
		public string UserName
		{
			get
			{
				if (this.userName == null)
				{
					using (WindowsIdentity current = WindowsIdentity.GetCurrent())
					{
						this.userName = current.Name;
					}
				}
				return this.userName;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00005338 File Offset: 0x00003538
		// (set) Token: 0x0600010B RID: 267 RVA: 0x00005340 File Offset: 0x00003540
		protected string ConnectionStringInternal
		{
			get
			{
				return this.connectionString;
			}
			set
			{
				this.connectionString = value;
			}
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00005349 File Offset: 0x00003549
		public DataSourceInfo Duplicate()
		{
			return (DataSourceInfo)base.MemberwiseClone();
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00005356 File Offset: 0x00003556
		protected virtual void OnInstanceDataChanged()
		{
		}

		// Token: 0x04000056 RID: 86
		private static string defaultManagementServer = "localhost";

		// Token: 0x04000057 RID: 87
		private string connectionString;

		// Token: 0x04000058 RID: 88
		private string managementServer = DataSourceInfo.DefaultManagementServer;

		// Token: 0x04000059 RID: 89
		private string userName;
	}
}
