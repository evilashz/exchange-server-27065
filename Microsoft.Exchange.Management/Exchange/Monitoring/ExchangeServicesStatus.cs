using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x020005E9 RID: 1513
	[Serializable]
	public class ExchangeServicesStatus : ConfigurableObject
	{
		// Token: 0x17001005 RID: 4101
		// (get) Token: 0x060035CE RID: 13774 RVA: 0x000DD82B File Offset: 0x000DBA2B
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return ExchangeServicesStatus.schema;
			}
		}

		// Token: 0x17001006 RID: 4102
		// (get) Token: 0x060035CF RID: 13775 RVA: 0x000DD832 File Offset: 0x000DBA32
		// (set) Token: 0x060035D0 RID: 13776 RVA: 0x000DD844 File Offset: 0x000DBA44
		public string Role
		{
			get
			{
				return (string)this[ExchangeServicesStatusSchema.Role];
			}
			internal set
			{
				this[ExchangeServicesStatusSchema.Role] = value;
			}
		}

		// Token: 0x17001007 RID: 4103
		// (get) Token: 0x060035D1 RID: 13777 RVA: 0x000DD852 File Offset: 0x000DBA52
		// (set) Token: 0x060035D2 RID: 13778 RVA: 0x000DD864 File Offset: 0x000DBA64
		public bool RequiredServicesRunning
		{
			get
			{
				return (bool)this[ExchangeServicesStatusSchema.RequiredServicesRunning];
			}
			internal set
			{
				this[ExchangeServicesStatusSchema.RequiredServicesRunning] = value;
			}
		}

		// Token: 0x17001008 RID: 4104
		// (get) Token: 0x060035D3 RID: 13779 RVA: 0x000DD877 File Offset: 0x000DBA77
		// (set) Token: 0x060035D4 RID: 13780 RVA: 0x000DD889 File Offset: 0x000DBA89
		public MultiValuedProperty<string> ServicesNotRunning
		{
			get
			{
				return (MultiValuedProperty<string>)this[ExchangeServicesStatusSchema.ServicesNotRunning];
			}
			internal set
			{
				this[ExchangeServicesStatusSchema.ServicesNotRunning] = value;
			}
		}

		// Token: 0x17001009 RID: 4105
		// (get) Token: 0x060035D5 RID: 13781 RVA: 0x000DD897 File Offset: 0x000DBA97
		// (set) Token: 0x060035D6 RID: 13782 RVA: 0x000DD8A9 File Offset: 0x000DBAA9
		public MultiValuedProperty<string> ServicesRunning
		{
			get
			{
				return (MultiValuedProperty<string>)this[ExchangeServicesStatusSchema.ServicesRunning];
			}
			internal set
			{
				this[ExchangeServicesStatusSchema.ServicesRunning] = value;
			}
		}

		// Token: 0x060035D7 RID: 13783 RVA: 0x000DD8B8 File Offset: 0x000DBAB8
		internal ExchangeServicesStatus(ServerRole roleBitfiedFlag, bool requiredServicesRunning, string[] servicesNotRunning, string[] servicesRunning) : base(new SimpleProviderPropertyBag())
		{
			if (servicesNotRunning == null)
			{
				throw new ArgumentNullException("servicesNotRunning");
			}
			if (servicesRunning == null)
			{
				throw new ArgumentNullException("servicesRunning");
			}
			this.Role = MpServerRoles.DisplayRoleName(roleBitfiedFlag);
			this.RequiredServicesRunning = requiredServicesRunning;
			this.ServicesNotRunning = new MultiValuedProperty<string>(servicesNotRunning);
			this.ServicesRunning = new MultiValuedProperty<string>(servicesRunning);
		}

		// Token: 0x040024E0 RID: 9440
		private static ExchangeServicesStatusSchema schema = ObjectSchema.GetInstance<ExchangeServicesStatusSchema>();
	}
}
