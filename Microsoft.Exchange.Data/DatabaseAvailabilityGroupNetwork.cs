using System;
using System.Collections.Generic;
using System.Management.Automation;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000219 RID: 537
	[Serializable]
	public class DatabaseAvailabilityGroupNetwork : ConfigurableObject
	{
		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x060012C2 RID: 4802 RVA: 0x00039944 File Offset: 0x00037B44
		// (set) Token: 0x060012C3 RID: 4803 RVA: 0x0003995B File Offset: 0x00037B5B
		[Parameter]
		public string Name
		{
			get
			{
				return (string)this.propertyBag[DatabaseAvailabilityGroupNetworkSchema.Name];
			}
			set
			{
				this.propertyBag[DatabaseAvailabilityGroupNetworkSchema.Name] = value;
			}
		}

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x060012C4 RID: 4804 RVA: 0x0003996E File Offset: 0x00037B6E
		internal static StringComparer NameComparer
		{
			get
			{
				return StringComparer.CurrentCultureIgnoreCase;
			}
		}

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x060012C5 RID: 4805 RVA: 0x00039975 File Offset: 0x00037B75
		internal static StringComparison NameComparison
		{
			get
			{
				return StringComparison.CurrentCultureIgnoreCase;
			}
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x060012C6 RID: 4806 RVA: 0x00039978 File Offset: 0x00037B78
		// (set) Token: 0x060012C7 RID: 4807 RVA: 0x0003998F File Offset: 0x00037B8F
		[Parameter]
		public string Description
		{
			get
			{
				return (string)this.propertyBag[DatabaseAvailabilityGroupNetworkSchema.Description];
			}
			set
			{
				this.propertyBag[DatabaseAvailabilityGroupNetworkSchema.Description] = value;
			}
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x060012C8 RID: 4808 RVA: 0x000399A2 File Offset: 0x00037BA2
		// (set) Token: 0x060012C9 RID: 4809 RVA: 0x000399B4 File Offset: 0x00037BB4
		public DagNetMultiValuedProperty<DatabaseAvailabilityGroupNetworkSubnet> Subnets
		{
			get
			{
				return (DagNetMultiValuedProperty<DatabaseAvailabilityGroupNetworkSubnet>)this[DatabaseAvailabilityGroupNetworkSchema.Subnets];
			}
			set
			{
				this[DatabaseAvailabilityGroupNetworkSchema.Subnets] = value;
			}
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x060012CA RID: 4810 RVA: 0x000399C2 File Offset: 0x00037BC2
		// (set) Token: 0x060012CB RID: 4811 RVA: 0x000399D4 File Offset: 0x00037BD4
		public DagNetMultiValuedProperty<DatabaseAvailabilityGroupNetworkInterface> Interfaces
		{
			get
			{
				return (DagNetMultiValuedProperty<DatabaseAvailabilityGroupNetworkInterface>)this[DatabaseAvailabilityGroupNetworkSchema.Interfaces];
			}
			set
			{
				this[DatabaseAvailabilityGroupNetworkSchema.Interfaces] = value;
			}
		}

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x060012CC RID: 4812 RVA: 0x000399E2 File Offset: 0x00037BE2
		// (set) Token: 0x060012CD RID: 4813 RVA: 0x000399F9 File Offset: 0x00037BF9
		public bool MapiAccessEnabled
		{
			get
			{
				return (bool)this.propertyBag[DatabaseAvailabilityGroupNetworkSchema.MapiAccessEnabled];
			}
			internal set
			{
				this.propertyBag[DatabaseAvailabilityGroupNetworkSchema.MapiAccessEnabled] = value;
			}
		}

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x060012CE RID: 4814 RVA: 0x00039A11 File Offset: 0x00037C11
		// (set) Token: 0x060012CF RID: 4815 RVA: 0x00039A28 File Offset: 0x00037C28
		[Parameter]
		public bool ReplicationEnabled
		{
			get
			{
				return (bool)this.propertyBag[DatabaseAvailabilityGroupNetworkSchema.ReplicationEnabled];
			}
			set
			{
				this.propertyBag[DatabaseAvailabilityGroupNetworkSchema.ReplicationEnabled] = value;
			}
		}

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x060012D0 RID: 4816 RVA: 0x00039A40 File Offset: 0x00037C40
		// (set) Token: 0x060012D1 RID: 4817 RVA: 0x00039A57 File Offset: 0x00037C57
		[Parameter]
		public bool IgnoreNetwork
		{
			get
			{
				return (bool)this.propertyBag[DatabaseAvailabilityGroupNetworkSchema.IgnoreNetwork];
			}
			set
			{
				this.propertyBag[DatabaseAvailabilityGroupNetworkSchema.IgnoreNetwork] = value;
			}
		}

		// Token: 0x060012D2 RID: 4818 RVA: 0x00039A6F File Offset: 0x00037C6F
		internal void SetIdentity(DagNetworkObjectId id)
		{
			this[this.propertyBag.ObjectIdentityPropertyDefinition] = id;
		}

		// Token: 0x060012D3 RID: 4819 RVA: 0x00039A83 File Offset: 0x00037C83
		public DatabaseAvailabilityGroupNetwork() : base(new DagNetPropertyBag())
		{
		}

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x060012D4 RID: 4820 RVA: 0x00039A90 File Offset: 0x00037C90
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return DatabaseAvailabilityGroupNetwork.s_schema;
			}
		}

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x060012D5 RID: 4821 RVA: 0x00039A97 File Offset: 0x00037C97
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x060012D6 RID: 4822 RVA: 0x00039AA0 File Offset: 0x00037CA0
		internal void ReplaceSubnets(IEnumerable<DatabaseAvailabilityGroupSubnetId> newSubnetIds)
		{
			this.Subnets.Clear();
			foreach (DatabaseAvailabilityGroupSubnetId netId in newSubnetIds)
			{
				DatabaseAvailabilityGroupNetworkSubnet item = new DatabaseAvailabilityGroupNetworkSubnet(netId);
				this.Subnets.Add(item);
			}
		}

		// Token: 0x04000B26 RID: 2854
		private static ObjectSchema s_schema = ObjectSchema.GetInstance<DatabaseAvailabilityGroupNetworkSchema>();
	}
}
