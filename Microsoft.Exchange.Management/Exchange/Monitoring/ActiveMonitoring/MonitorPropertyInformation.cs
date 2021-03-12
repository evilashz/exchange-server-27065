using System;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring
{
	// Token: 0x020004FC RID: 1276
	[Serializable]
	public class MonitorPropertyInformation : ConfigurableObject
	{
		// Token: 0x06002DD8 RID: 11736 RVA: 0x000B75A9 File Offset: 0x000B57A9
		public MonitorPropertyInformation() : base(new SimpleProviderPropertyBag())
		{
		}

		// Token: 0x06002DD9 RID: 11737 RVA: 0x000B75B8 File Offset: 0x000B57B8
		internal MonitorPropertyInformation(string server, PropertyInformation propertyInfo) : this()
		{
			this.Server = server;
			this.PropertyName = propertyInfo.Name;
			this.Description = propertyInfo.Description;
			this.IsMandatory = propertyInfo.IsMandatory;
			this[SimpleProviderObjectSchema.Identity] = new MonitorPropertyInformation.MonitorPropertyInformationId(this.PropertyName, this.Description, this.IsMandatory);
		}

		// Token: 0x17000DB0 RID: 3504
		// (get) Token: 0x06002DDA RID: 11738 RVA: 0x000B7618 File Offset: 0x000B5818
		// (set) Token: 0x06002DDB RID: 11739 RVA: 0x000B762A File Offset: 0x000B582A
		public string Server
		{
			get
			{
				return (string)this[MonitorPropertyInformationSchema.Server];
			}
			private set
			{
				this[MonitorPropertyInformationSchema.Server] = value;
			}
		}

		// Token: 0x17000DB1 RID: 3505
		// (get) Token: 0x06002DDC RID: 11740 RVA: 0x000B7638 File Offset: 0x000B5838
		// (set) Token: 0x06002DDD RID: 11741 RVA: 0x000B764A File Offset: 0x000B584A
		public string PropertyName
		{
			get
			{
				return (string)this[MonitorPropertyInformationSchema.PropertyName];
			}
			private set
			{
				this[MonitorPropertyInformationSchema.PropertyName] = value;
			}
		}

		// Token: 0x17000DB2 RID: 3506
		// (get) Token: 0x06002DDE RID: 11742 RVA: 0x000B7658 File Offset: 0x000B5858
		// (set) Token: 0x06002DDF RID: 11743 RVA: 0x000B766A File Offset: 0x000B586A
		public string Description
		{
			get
			{
				return (string)this[MonitorPropertyInformationSchema.Description];
			}
			private set
			{
				this[MonitorPropertyInformationSchema.Description] = value;
			}
		}

		// Token: 0x17000DB3 RID: 3507
		// (get) Token: 0x06002DE0 RID: 11744 RVA: 0x000B7678 File Offset: 0x000B5878
		// (set) Token: 0x06002DE1 RID: 11745 RVA: 0x000B768A File Offset: 0x000B588A
		public bool IsMandatory
		{
			get
			{
				return (bool)this[MonitorPropertyInformationSchema.IsMandatory];
			}
			private set
			{
				this[MonitorPropertyInformationSchema.IsMandatory] = value;
			}
		}

		// Token: 0x17000DB4 RID: 3508
		// (get) Token: 0x06002DE2 RID: 11746 RVA: 0x000B769D File Offset: 0x000B589D
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x17000DB5 RID: 3509
		// (get) Token: 0x06002DE3 RID: 11747 RVA: 0x000B76A4 File Offset: 0x000B58A4
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return MonitorPropertyInformation.schema;
			}
		}

		// Token: 0x040020D4 RID: 8404
		private static MonitorPropertyInformationSchema schema = ObjectSchema.GetInstance<MonitorPropertyInformationSchema>();

		// Token: 0x020004FD RID: 1277
		[Serializable]
		public class MonitorPropertyInformationId : ObjectId
		{
			// Token: 0x06002DE5 RID: 11749 RVA: 0x000B76B7 File Offset: 0x000B58B7
			public MonitorPropertyInformationId(string propertyName, string propertyDescription, bool isMandatory)
			{
				this.identity = string.Format("{0}\\{1}\\{2}", propertyName, propertyDescription, isMandatory.ToString());
			}

			// Token: 0x06002DE6 RID: 11750 RVA: 0x000B76D8 File Offset: 0x000B58D8
			public override string ToString()
			{
				return this.identity;
			}

			// Token: 0x06002DE7 RID: 11751 RVA: 0x000B76E0 File Offset: 0x000B58E0
			public override byte[] GetBytes()
			{
				return Encoding.Unicode.GetBytes(this.ToString());
			}

			// Token: 0x040020D5 RID: 8405
			private readonly string identity;
		}
	}
}
