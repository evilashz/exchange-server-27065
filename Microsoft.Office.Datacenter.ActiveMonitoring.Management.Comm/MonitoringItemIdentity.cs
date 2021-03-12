using System;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.ActiveMonitoring;

namespace Microsoft.Office.Datacenter.ActiveMonitoring.Management.Common
{
	// Token: 0x02000008 RID: 8
	[Serializable]
	public class MonitoringItemIdentity : ConfigurableObject
	{
		// Token: 0x06000077 RID: 119 RVA: 0x00003793 File Offset: 0x00001993
		public MonitoringItemIdentity() : base(new SimpleProviderPropertyBag())
		{
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000037A0 File Offset: 0x000019A0
		internal MonitoringItemIdentity(string server, RpcGetMonitoringItemIdentity.RpcMonitorItemIdentity rpcIdentity) : this()
		{
			this.Server = server;
			this.HealthSetName = rpcIdentity.HealthSetName;
			this.Name = rpcIdentity.Name;
			this.TargetResource = rpcIdentity.TargetResource;
			this.ItemType = this.ParseEnum<MonitorItemType>(rpcIdentity.ItemType, MonitorItemType.Unknown);
			this[SimpleProviderObjectSchema.Identity] = new MonitoringItemIdentity.MonitorIdentityId(this.HealthSetName, this.Name, this.TargetResource);
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00003813 File Offset: 0x00001A13
		// (set) Token: 0x0600007A RID: 122 RVA: 0x00003825 File Offset: 0x00001A25
		public string Server
		{
			get
			{
				return (string)this[MonitorIdentitySchema.Server];
			}
			private set
			{
				this[MonitorIdentitySchema.Server] = value;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00003833 File Offset: 0x00001A33
		// (set) Token: 0x0600007C RID: 124 RVA: 0x00003845 File Offset: 0x00001A45
		public string HealthSetName
		{
			get
			{
				return (string)this[MonitorIdentitySchema.HealthSetName];
			}
			private set
			{
				this[MonitorIdentitySchema.HealthSetName] = value;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00003853 File Offset: 0x00001A53
		// (set) Token: 0x0600007E RID: 126 RVA: 0x00003865 File Offset: 0x00001A65
		public string Name
		{
			get
			{
				return (string)this[MonitorIdentitySchema.Name];
			}
			private set
			{
				this[MonitorIdentitySchema.Name] = value;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00003873 File Offset: 0x00001A73
		// (set) Token: 0x06000080 RID: 128 RVA: 0x00003885 File Offset: 0x00001A85
		public string TargetResource
		{
			get
			{
				return (string)this[MonitorIdentitySchema.TargetResource];
			}
			private set
			{
				this[MonitorIdentitySchema.TargetResource] = value;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00003893 File Offset: 0x00001A93
		// (set) Token: 0x06000082 RID: 130 RVA: 0x000038A5 File Offset: 0x00001AA5
		public MonitorItemType ItemType
		{
			get
			{
				return (MonitorItemType)this[MonitorIdentitySchema.ItemType];
			}
			private set
			{
				this[MonitorIdentitySchema.ItemType] = value;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000083 RID: 131 RVA: 0x000038B8 File Offset: 0x00001AB8
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000084 RID: 132 RVA: 0x000038BF File Offset: 0x00001ABF
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return MonitoringItemIdentity.schema;
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000038C8 File Offset: 0x00001AC8
		internal T ParseEnum<T>(string strEnum, T defaultValue) where T : struct
		{
			T result = defaultValue;
			if (!string.IsNullOrEmpty(strEnum) && !Enum.TryParse<T>(strEnum, out result))
			{
				result = defaultValue;
			}
			return result;
		}

		// Token: 0x04000037 RID: 55
		private static MonitorIdentitySchema schema = ObjectSchema.GetInstance<MonitorIdentitySchema>();

		// Token: 0x02000009 RID: 9
		[Serializable]
		public class MonitorIdentityId : ObjectId
		{
			// Token: 0x06000087 RID: 135 RVA: 0x000038F8 File Offset: 0x00001AF8
			public MonitorIdentityId(string healthSetName, string monitorName, string targetResource)
			{
				this.identity = string.Format("{0}\\{1}{2}", healthSetName, monitorName, string.IsNullOrEmpty(targetResource) ? string.Empty : ("\\" + targetResource));
			}

			// Token: 0x06000088 RID: 136 RVA: 0x0000392C File Offset: 0x00001B2C
			public static bool IsValidFormat(string monitorIdentity)
			{
				if (string.IsNullOrEmpty(monitorIdentity))
				{
					return false;
				}
				int num = monitorIdentity.Split(new char[]
				{
					'\\'
				}).Length;
				return num >= 2 && num <= 3;
			}

			// Token: 0x06000089 RID: 137 RVA: 0x00003968 File Offset: 0x00001B68
			public static string GetHealthSet(string monitorIdentity)
			{
				if (!MonitoringItemIdentity.MonitorIdentityId.IsValidFormat(monitorIdentity))
				{
					return null;
				}
				string[] array = monitorIdentity.Split(new char[]
				{
					'\\'
				});
				return array[0];
			}

			// Token: 0x0600008A RID: 138 RVA: 0x00003998 File Offset: 0x00001B98
			public static string GetMonitor(string monitorIdentity)
			{
				if (!MonitoringItemIdentity.MonitorIdentityId.IsValidFormat(monitorIdentity))
				{
					return null;
				}
				string[] array = monitorIdentity.Split(new char[]
				{
					'\\'
				});
				return array[1];
			}

			// Token: 0x0600008B RID: 139 RVA: 0x000039C8 File Offset: 0x00001BC8
			public static string GetTargetResource(string monitorIdentity)
			{
				if (!MonitoringItemIdentity.MonitorIdentityId.IsValidFormat(monitorIdentity))
				{
					return null;
				}
				string[] array = monitorIdentity.Split(new char[]
				{
					'\\'
				});
				if (array.Length > 2)
				{
					return array[2];
				}
				return string.Empty;
			}

			// Token: 0x0600008C RID: 140 RVA: 0x00003A02 File Offset: 0x00001C02
			public override string ToString()
			{
				return this.identity;
			}

			// Token: 0x0600008D RID: 141 RVA: 0x00003A0A File Offset: 0x00001C0A
			public override byte[] GetBytes()
			{
				return Encoding.Unicode.GetBytes(this.ToString());
			}

			// Token: 0x04000038 RID: 56
			private readonly string identity;

			// Token: 0x0200000A RID: 10
			internal enum Components
			{
				// Token: 0x0400003A RID: 58
				HealthSetName,
				// Token: 0x0400003B RID: 59
				MonitorName,
				// Token: 0x0400003C RID: 60
				TargetResourceName,
				// Token: 0x0400003D RID: 61
				TotalCount
			}
		}
	}
}
