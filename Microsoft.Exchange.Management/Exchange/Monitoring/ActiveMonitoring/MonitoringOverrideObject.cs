using System;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring
{
	// Token: 0x020004F9 RID: 1273
	[Serializable]
	public class MonitoringOverrideObject : ConfigurableObject
	{
		// Token: 0x06002DB9 RID: 11705 RVA: 0x000B70DB File Offset: 0x000B52DB
		public MonitoringOverrideObject() : base(new SimpleProviderPropertyBag())
		{
		}

		// Token: 0x06002DBA RID: 11706 RVA: 0x000B70E8 File Offset: 0x000B52E8
		internal MonitoringOverrideObject(MonitoringOverride monitoringOverride, string workitemType) : this(monitoringOverride.HealthSet, monitoringOverride.MonitoringItemName, string.Empty, workitemType, monitoringOverride.PropertyName, monitoringOverride.PropertyValue, (monitoringOverride.ExpirationTime != null) ? monitoringOverride.ExpirationTime.Value.ToString() : string.Empty, (monitoringOverride.ApplyVersion == null) ? string.Empty : monitoringOverride.ApplyVersion.ToString(), monitoringOverride.CreatedBy, (monitoringOverride.WhenCreated != null) ? monitoringOverride.WhenCreatedUTC.Value.ToString() : string.Empty)
		{
		}

		// Token: 0x06002DBB RID: 11707 RVA: 0x000B71A8 File Offset: 0x000B53A8
		internal MonitoringOverrideObject(string healthSetName, string monitoringItemName, string targetResource, string itemType, string propertyName, string propertyValue, string expirationTime, string applyVersion, string createdBy, string createdTime) : this()
		{
			this[SimpleProviderObjectSchema.Identity] = new MonitoringOverrideObject.MonitoringItemIdentity(healthSetName, monitoringItemName, targetResource);
			this.ItemType = itemType;
			this.PropertyName = propertyName;
			this.PropertyValue = propertyValue;
			this.HealthSetName = healthSetName;
			this.MonitoringItemName = monitoringItemName;
			this.TargetResource = targetResource;
			this.ExpirationTime = expirationTime;
			this.ApplyVersion = applyVersion;
			this.CreatedBy = createdBy;
			this.CreatedTime = createdTime;
		}

		// Token: 0x17000DA4 RID: 3492
		// (get) Token: 0x06002DBC RID: 11708 RVA: 0x000B721B File Offset: 0x000B541B
		// (set) Token: 0x06002DBD RID: 11709 RVA: 0x000B722D File Offset: 0x000B542D
		public string ItemType
		{
			get
			{
				return (string)this[MonitoringOverrideSchema.ItemType];
			}
			private set
			{
				this[MonitoringOverrideSchema.ItemType] = value;
			}
		}

		// Token: 0x17000DA5 RID: 3493
		// (get) Token: 0x06002DBE RID: 11710 RVA: 0x000B723B File Offset: 0x000B543B
		// (set) Token: 0x06002DBF RID: 11711 RVA: 0x000B724D File Offset: 0x000B544D
		public string PropertyName
		{
			get
			{
				return (string)this[MonitoringOverrideSchema.PropertyName];
			}
			private set
			{
				this[MonitoringOverrideSchema.PropertyName] = value;
			}
		}

		// Token: 0x17000DA6 RID: 3494
		// (get) Token: 0x06002DC0 RID: 11712 RVA: 0x000B725B File Offset: 0x000B545B
		// (set) Token: 0x06002DC1 RID: 11713 RVA: 0x000B726D File Offset: 0x000B546D
		public string PropertyValue
		{
			get
			{
				return (string)this[MonitoringOverrideSchema.PropertyValue];
			}
			private set
			{
				this[MonitoringOverrideSchema.PropertyValue] = value;
			}
		}

		// Token: 0x17000DA7 RID: 3495
		// (get) Token: 0x06002DC2 RID: 11714 RVA: 0x000B727B File Offset: 0x000B547B
		// (set) Token: 0x06002DC3 RID: 11715 RVA: 0x000B728D File Offset: 0x000B548D
		public string HealthSetName
		{
			get
			{
				return (string)this[MonitoringOverrideSchema.HealthSetName];
			}
			private set
			{
				this[MonitoringOverrideSchema.HealthSetName] = value;
			}
		}

		// Token: 0x17000DA8 RID: 3496
		// (get) Token: 0x06002DC4 RID: 11716 RVA: 0x000B729B File Offset: 0x000B549B
		// (set) Token: 0x06002DC5 RID: 11717 RVA: 0x000B72AD File Offset: 0x000B54AD
		public string MonitoringItemName
		{
			get
			{
				return (string)this[MonitoringOverrideSchema.MonitoringItemName];
			}
			private set
			{
				this[MonitoringOverrideSchema.MonitoringItemName] = value;
			}
		}

		// Token: 0x17000DA9 RID: 3497
		// (get) Token: 0x06002DC6 RID: 11718 RVA: 0x000B72BB File Offset: 0x000B54BB
		// (set) Token: 0x06002DC7 RID: 11719 RVA: 0x000B72CD File Offset: 0x000B54CD
		public string TargetResource
		{
			get
			{
				return (string)this[MonitoringOverrideSchema.TargetResource];
			}
			private set
			{
				this[MonitoringOverrideSchema.TargetResource] = value;
			}
		}

		// Token: 0x17000DAA RID: 3498
		// (get) Token: 0x06002DC8 RID: 11720 RVA: 0x000B72DB File Offset: 0x000B54DB
		// (set) Token: 0x06002DC9 RID: 11721 RVA: 0x000B72ED File Offset: 0x000B54ED
		public string ExpirationTime
		{
			get
			{
				return (string)this[MonitoringOverrideSchema.ExpirationTime];
			}
			private set
			{
				this[MonitoringOverrideSchema.ExpirationTime] = value;
			}
		}

		// Token: 0x17000DAB RID: 3499
		// (get) Token: 0x06002DCA RID: 11722 RVA: 0x000B72FB File Offset: 0x000B54FB
		// (set) Token: 0x06002DCB RID: 11723 RVA: 0x000B730D File Offset: 0x000B550D
		public string ApplyVersion
		{
			get
			{
				return (string)this[MonitoringOverrideSchema.ApplyVersion];
			}
			private set
			{
				this[MonitoringOverrideSchema.ApplyVersion] = value;
			}
		}

		// Token: 0x17000DAC RID: 3500
		// (get) Token: 0x06002DCC RID: 11724 RVA: 0x000B731B File Offset: 0x000B551B
		// (set) Token: 0x06002DCD RID: 11725 RVA: 0x000B732D File Offset: 0x000B552D
		public string CreatedBy
		{
			get
			{
				return (string)this[MonitoringOverrideSchema.CreatedBy];
			}
			private set
			{
				this[MonitoringOverrideSchema.CreatedBy] = value;
			}
		}

		// Token: 0x17000DAD RID: 3501
		// (get) Token: 0x06002DCE RID: 11726 RVA: 0x000B733B File Offset: 0x000B553B
		// (set) Token: 0x06002DCF RID: 11727 RVA: 0x000B734D File Offset: 0x000B554D
		public string CreatedTime
		{
			get
			{
				return (string)this[MonitoringOverrideSchema.CreatedTime];
			}
			private set
			{
				this[MonitoringOverrideSchema.CreatedTime] = value;
			}
		}

		// Token: 0x17000DAE RID: 3502
		// (get) Token: 0x06002DD0 RID: 11728 RVA: 0x000B735B File Offset: 0x000B555B
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2012;
			}
		}

		// Token: 0x17000DAF RID: 3503
		// (get) Token: 0x06002DD1 RID: 11729 RVA: 0x000B7362 File Offset: 0x000B5562
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return MonitoringOverrideObject.schema;
			}
		}

		// Token: 0x040020C8 RID: 8392
		private static ObjectSchema schema = ObjectSchema.GetInstance<MonitoringOverrideSchema>();

		// Token: 0x020004FA RID: 1274
		[Serializable]
		public class MonitoringItemIdentity : ObjectId
		{
			// Token: 0x06002DD3 RID: 11731 RVA: 0x000B7375 File Offset: 0x000B5575
			public MonitoringItemIdentity(string healthSetName, string monitoringItemName, string targetResource)
			{
				if (string.IsNullOrWhiteSpace(targetResource))
				{
					this.identity = string.Format("{0}\\{1}", healthSetName, monitoringItemName);
					return;
				}
				this.identity = string.Format("{0}\\{1}\\{2}", healthSetName, monitoringItemName, targetResource);
			}

			// Token: 0x06002DD4 RID: 11732 RVA: 0x000B73AB File Offset: 0x000B55AB
			public override string ToString()
			{
				return this.identity;
			}

			// Token: 0x06002DD5 RID: 11733 RVA: 0x000B73B3 File Offset: 0x000B55B3
			public override byte[] GetBytes()
			{
				return Encoding.Unicode.GetBytes(this.ToString());
			}

			// Token: 0x040020C9 RID: 8393
			private readonly string identity;
		}
	}
}
