using System;
using System.Security.Principal;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000329 RID: 809
	[Serializable]
	public class ADComputer : ADNonExchangeObject
	{
		// Token: 0x06002577 RID: 9591 RVA: 0x0009F059 File Offset: 0x0009D259
		internal void DisableComputerAccount()
		{
			this.UserAccountControl |= UserAccountControlFlags.AccountDisabled;
		}

		// Token: 0x170009DA RID: 2522
		// (get) Token: 0x06002578 RID: 9592 RVA: 0x0009F069 File Offset: 0x0009D269
		internal override ADObjectSchema Schema
		{
			get
			{
				return ADComputer.schema;
			}
		}

		// Token: 0x170009DB RID: 2523
		// (get) Token: 0x06002579 RID: 9593 RVA: 0x0009F070 File Offset: 0x0009D270
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ADComputer.mostDerivedClass;
			}
		}

		// Token: 0x170009DC RID: 2524
		// (get) Token: 0x0600257B RID: 9595 RVA: 0x0009F07F File Offset: 0x0009D27F
		public MultiValuedProperty<string> ServicePrincipalName
		{
			get
			{
				return (MultiValuedProperty<string>)this.propertyBag[ADComputerSchema.ServicePrincipalName];
			}
		}

		// Token: 0x170009DD RID: 2525
		// (get) Token: 0x0600257C RID: 9596 RVA: 0x0009F096 File Offset: 0x0009D296
		public SecurityIdentifier Sid
		{
			get
			{
				return (SecurityIdentifier)this.propertyBag[ADComputerSchema.Sid];
			}
		}

		// Token: 0x170009DE RID: 2526
		// (get) Token: 0x0600257D RID: 9597 RVA: 0x0009F0AD File Offset: 0x0009D2AD
		public string OperatingSystemVersion
		{
			get
			{
				return (string)this.propertyBag[ADComputerSchema.OperatingSystemVersion];
			}
		}

		// Token: 0x170009DF RID: 2527
		// (get) Token: 0x0600257E RID: 9598 RVA: 0x0009F0C4 File Offset: 0x0009D2C4
		public string OperatingSystemServicePack
		{
			get
			{
				return (string)this.propertyBag[ADComputerSchema.OperatingSystemServicePack];
			}
		}

		// Token: 0x170009E0 RID: 2528
		// (get) Token: 0x0600257F RID: 9599 RVA: 0x0009F0DB File Offset: 0x0009D2DB
		// (set) Token: 0x06002580 RID: 9600 RVA: 0x0009F0ED File Offset: 0x0009D2ED
		public string DnsHostName
		{
			get
			{
				return (string)this[ADComputerSchema.DnsHostName];
			}
			internal set
			{
				this[ADComputerSchema.DnsHostName] = value;
			}
		}

		// Token: 0x170009E1 RID: 2529
		// (get) Token: 0x06002581 RID: 9601 RVA: 0x0009F0FB File Offset: 0x0009D2FB
		// (set) Token: 0x06002582 RID: 9602 RVA: 0x0009F10D File Offset: 0x0009D30D
		public ADObjectId ThrottlingPolicy
		{
			get
			{
				return (ADObjectId)this[ADComputerSchema.ThrottlingPolicy];
			}
			set
			{
				this[ADComputerSchema.ThrottlingPolicy] = value;
			}
		}

		// Token: 0x170009E2 RID: 2530
		// (get) Token: 0x06002583 RID: 9603 RVA: 0x0009F11B File Offset: 0x0009D31B
		// (set) Token: 0x06002584 RID: 9604 RVA: 0x0009F12D File Offset: 0x0009D32D
		public MultiValuedProperty<string> ComponentStates
		{
			get
			{
				return (MultiValuedProperty<string>)this[ADComputerSchema.ComponentStates];
			}
			internal set
			{
				this[ADComputerSchema.ComponentStates] = value;
			}
		}

		// Token: 0x170009E3 RID: 2531
		// (get) Token: 0x06002585 RID: 9605 RVA: 0x0009F13B File Offset: 0x0009D33B
		// (set) Token: 0x06002586 RID: 9606 RVA: 0x0009F14D File Offset: 0x0009D34D
		public string MonitoringGroup
		{
			get
			{
				return (string)this[ADComputerSchema.MonitoringGroup];
			}
			set
			{
				this[ADComputerSchema.MonitoringGroup] = value;
			}
		}

		// Token: 0x170009E4 RID: 2532
		// (get) Token: 0x06002587 RID: 9607 RVA: 0x0009F15B File Offset: 0x0009D35B
		// (set) Token: 0x06002588 RID: 9608 RVA: 0x0009F16D File Offset: 0x0009D36D
		public int MonitoringInstalled
		{
			get
			{
				return (int)this[ADComputerSchema.MonitoringInstalled];
			}
			set
			{
				this[ADComputerSchema.MonitoringInstalled] = value;
			}
		}

		// Token: 0x170009E5 RID: 2533
		// (get) Token: 0x06002589 RID: 9609 RVA: 0x0009F180 File Offset: 0x0009D380
		// (set) Token: 0x0600258A RID: 9610 RVA: 0x0009F192 File Offset: 0x0009D392
		internal bool IsOutOfService
		{
			get
			{
				return (bool)this[ADComputerSchema.IsOutOfService];
			}
			set
			{
				this[ADComputerSchema.IsOutOfService] = value;
			}
		}

		// Token: 0x170009E6 RID: 2534
		// (get) Token: 0x0600258B RID: 9611 RVA: 0x0009F1A5 File Offset: 0x0009D3A5
		// (set) Token: 0x0600258C RID: 9612 RVA: 0x0009F1B7 File Offset: 0x0009D3B7
		private UserAccountControlFlags UserAccountControl
		{
			get
			{
				return (UserAccountControlFlags)this[ADComputerSchema.UserAccountControl];
			}
			set
			{
				this[ADComputerSchema.UserAccountControl] = value;
			}
		}

		// Token: 0x04001701 RID: 5889
		private static ADComputerSchema schema = ObjectSchema.GetInstance<ADComputerSchema>();

		// Token: 0x04001702 RID: 5890
		private static string mostDerivedClass = "computer";
	}
}
