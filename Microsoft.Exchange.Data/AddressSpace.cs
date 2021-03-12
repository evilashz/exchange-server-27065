using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000117 RID: 279
	[Serializable]
	public class AddressSpace
	{
		// Token: 0x060009A9 RID: 2473 RVA: 0x0001E44F File Offset: 0x0001C64F
		public AddressSpace(string addressSpace)
		{
			this.Initialize(addressSpace, true);
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x0001E45F File Offset: 0x0001C65F
		public AddressSpace(string addressSpace, bool includeSubDomain) : this("smtp", addressSpace, 1, includeSubDomain)
		{
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x0001E46F File Offset: 0x0001C66F
		public AddressSpace(string addressType, string addressSpace, int cost) : this(addressType, addressSpace, cost, false)
		{
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x0001E47B File Offset: 0x0001C67B
		public AddressSpace(string addressType, string addressSpace, int cost, bool includeSubDomain)
		{
			this.Initialize(addressType, addressSpace, cost, includeSubDomain, true);
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x0001E48F File Offset: 0x0001C68F
		private AddressSpace(SmtpDomainWithSubdomains obj)
		{
			this.type = "smtp";
			this.cost = 1;
			this.smtpDomainWithSubdomains = obj;
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x0001E4B0 File Offset: 0x0001C6B0
		private AddressSpace()
		{
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x060009AF RID: 2479 RVA: 0x0001E4B8 File Offset: 0x0001C6B8
		public string Address
		{
			get
			{
				if (this.addressSpace == null)
				{
					return this.smtpDomainWithSubdomains.Address;
				}
				return this.addressSpace;
			}
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x060009B0 RID: 2480 RVA: 0x0001E4D4 File Offset: 0x0001C6D4
		public string Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x060009B1 RID: 2481 RVA: 0x0001E4DC File Offset: 0x0001C6DC
		// (set) Token: 0x060009B2 RID: 2482 RVA: 0x0001E4E4 File Offset: 0x0001C6E4
		public int Cost
		{
			get
			{
				return this.cost;
			}
			set
			{
				if (value < 1 || value > 100)
				{
					throw new ArgumentOutOfRangeException("Cost", value, DataStrings.AddressSpaceCostOutOfRange(1, 100));
				}
				this.cost = value;
			}
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x060009B3 RID: 2483 RVA: 0x0001E514 File Offset: 0x0001C714
		public bool IncludeSubDomains
		{
			get
			{
				return this.addressSpace == null && this.smtpDomainWithSubdomains.IncludeSubDomains;
			}
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x060009B4 RID: 2484 RVA: 0x0001E52B File Offset: 0x0001C72B
		public string Domain
		{
			get
			{
				if (this.addressSpace == null)
				{
					return this.smtpDomainWithSubdomains.Domain;
				}
				return this.addressSpace;
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x060009B5 RID: 2485 RVA: 0x0001E547 File Offset: 0x0001C747
		public bool IsSmtpType
		{
			get
			{
				return "smtp".Equals(this.Type, StringComparison.OrdinalIgnoreCase);
			}
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x060009B6 RID: 2486 RVA: 0x0001E55A File Offset: 0x0001C75A
		public bool IsX400Type
		{
			get
			{
				return AddressSpace.IsX400AddressType(this.Type);
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x060009B7 RID: 2487 RVA: 0x0001E567 File Offset: 0x0001C767
		public RoutingX400Address X400Address
		{
			get
			{
				return this.x400Address;
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x060009B8 RID: 2488 RVA: 0x0001E56F File Offset: 0x0001C76F
		internal bool IsLocal
		{
			get
			{
				return this.isLocal;
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x060009B9 RID: 2489 RVA: 0x0001E577 File Offset: 0x0001C777
		internal SmtpDomainWithSubdomains DomainWithSubdomains
		{
			get
			{
				if (this.addressSpace == null)
				{
					return this.smtpDomainWithSubdomains;
				}
				return null;
			}
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x0001E58C File Offset: 0x0001C78C
		public bool Equals(AddressSpace value)
		{
			if (object.ReferenceEquals(this, value))
			{
				return true;
			}
			bool flag = false;
			if (value != null)
			{
				flag = (this.cost == value.cost && this.isLocal == value.isLocal && string.Equals(this.type, value.type, StringComparison.OrdinalIgnoreCase));
				if (flag)
				{
					if (this.addressSpace != null)
					{
						flag = this.addressSpace.Equals(value.addressSpace, StringComparison.OrdinalIgnoreCase);
					}
					else
					{
						flag = this.smtpDomainWithSubdomains.Equals(value.smtpDomainWithSubdomains);
					}
				}
			}
			return flag;
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x0001E610 File Offset: 0x0001C810
		public override bool Equals(object comparand)
		{
			AddressSpace addressSpace = comparand as AddressSpace;
			return addressSpace != null && this.Equals(addressSpace);
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x0001E630 File Offset: 0x0001C830
		public override int GetHashCode()
		{
			if (this.Domain != null)
			{
				return this.Domain.GetHashCode();
			}
			return 0;
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x0001E647 File Offset: 0x0001C847
		public override string ToString()
		{
			return this.ToString(false);
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x0001E650 File Offset: 0x0001C850
		public static AddressSpace Parse(string addressSpace)
		{
			return AddressSpace.Parse(addressSpace, true);
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x0001E659 File Offset: 0x0001C859
		public static bool TryParse(string address, out AddressSpace addressSpace)
		{
			return AddressSpace.TryParse(address, out addressSpace, true);
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x0001E664 File Offset: 0x0001C864
		internal static bool TryParse(string address, out AddressSpace addressSpace, bool performFullValidation)
		{
			try
			{
				addressSpace = AddressSpace.Parse(address, performFullValidation);
			}
			catch (FormatException)
			{
				addressSpace = null;
				return false;
			}
			return true;
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x0001E698 File Offset: 0x0001C898
		internal static AddressSpace ADParse(string address)
		{
			bool flag = false;
			if (address.StartsWith("Local:", StringComparison.OrdinalIgnoreCase))
			{
				address = address.Substring("Local:".Length);
				flag = true;
			}
			AddressSpace addressSpace = AddressSpace.Parse(address, false);
			addressSpace.isLocal = flag;
			return addressSpace;
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x0001E6DC File Offset: 0x0001C8DC
		internal string ToString(bool includeScope)
		{
			string text = this.addressSpace;
			if (text != null && !this.IsSmtpType && !this.IsX400Type)
			{
				text = this.addressSpace.Replace("@", "(a)");
			}
			if (includeScope && this.isLocal)
			{
				return string.Format("{0}{1}:{2};{3}", new object[]
				{
					"Local:",
					this.type,
					(text != null) ? text : this.smtpDomainWithSubdomains.ToString(),
					this.cost
				});
			}
			return string.Format("{0}:{1};{2}", this.type, (text != null) ? text : this.smtpDomainWithSubdomains.ToString(), this.cost);
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x0001E796 File Offset: 0x0001C996
		internal string ADToString()
		{
			return this.ToString(true);
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x0001E7A0 File Offset: 0x0001C9A0
		internal AddressSpace ToggleScope()
		{
			return new AddressSpace
			{
				cost = this.cost,
				type = this.type,
				smtpDomainWithSubdomains = this.smtpDomainWithSubdomains,
				addressSpace = this.addressSpace,
				x400Address = this.x400Address,
				isLocal = !this.isLocal
			};
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x0001E800 File Offset: 0x0001CA00
		private void Initialize(string addressType, string addressSpace, int cost, bool includeSubDomain, bool performFullAddressTypeValidation)
		{
			if (string.IsNullOrEmpty(addressType))
			{
				throw new StrongTypeFormatException(DataStrings.InvalidAddressSpaceTypeNullOrEmpty, "Type");
			}
			this.type = addressType;
			this.Cost = cost;
			if (this.IsSmtpType)
			{
				this.smtpDomainWithSubdomains = new SmtpDomainWithSubdomains(addressSpace, includeSubDomain);
				return;
			}
			if (addressSpace != null)
			{
				addressSpace = addressSpace.ToLower();
			}
			if (this.IsX400Type)
			{
				if (!RoutingX400Address.TryParseAddressSpace(addressSpace, this.isLocal, out this.x400Address))
				{
					throw new StrongTypeFormatException(DataStrings.InvalidX400AddressSpace(addressSpace), "Domain");
				}
				this.addressSpace = addressSpace;
				return;
			}
			else
			{
				if (string.IsNullOrEmpty(addressSpace))
				{
					throw new StrongTypeFormatException(DataStrings.InvalidAddressSpaceAddress, "Domain");
				}
				if (performFullAddressTypeValidation && !ProxyAddressPrefix.IsPrefixStringValid(addressType))
				{
					throw new StrongTypeFormatException(DataStrings.InvalidAddressSpaceType(addressType), "Type");
				}
				this.addressSpace = addressSpace.Replace("(a)", "@");
				return;
			}
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x0001E8EC File Offset: 0x0001CAEC
		private void Initialize(string addressSpace, bool performFullAddressTypeValidation)
		{
			if (addressSpace.StartsWith("Local:", StringComparison.OrdinalIgnoreCase))
			{
				throw new FormatException(DataStrings.InvalidScopedAddressSpace(addressSpace));
			}
			int num = addressSpace.IndexOf(':');
			string addressType;
			if (num == -1 || num == 0)
			{
				addressType = "smtp";
			}
			else
			{
				addressType = addressSpace.Substring(0, num);
			}
			int num2 = addressSpace.LastIndexOf(';');
			if (AddressSpace.IsX400AddressType(addressType) && num2 == addressSpace.Length - 1)
			{
				num2 = -1;
			}
			string text;
			int num3;
			if (num2 == -1 || num2 <= num)
			{
				text = addressSpace.Substring(num + 1);
				num3 = 1;
			}
			else
			{
				text = addressSpace.Substring(num + 1, num2 - num - 1);
				if (num2 == addressSpace.Length - 1)
				{
					num3 = 1;
				}
				else if (!int.TryParse(addressSpace.Substring(num2 + 1), out num3))
				{
					throw new ArgumentException(DataStrings.InvalidAddressSpaceCostFormat(addressSpace.Substring(num2 + 1)), "Cost");
				}
			}
			this.Initialize(addressType, text, num3, false, performFullAddressTypeValidation);
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x0001E9D9 File Offset: 0x0001CBD9
		private static bool IsX400AddressType(string type)
		{
			return "x400".Equals(type, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x0001E9E8 File Offset: 0x0001CBE8
		private static AddressSpace Parse(string value, bool performFullValidation)
		{
			AddressSpace addressSpace = new AddressSpace();
			addressSpace.Initialize(value, performFullValidation);
			return addressSpace;
		}

		// Token: 0x04000611 RID: 1553
		public const string SmtpAddressType = "smtp";

		// Token: 0x04000612 RID: 1554
		public const string X400AddressType = "x400";

		// Token: 0x04000613 RID: 1555
		public const int MinCostValue = 1;

		// Token: 0x04000614 RID: 1556
		public const int MaxCostValue = 100;

		// Token: 0x04000615 RID: 1557
		private const string ScopePrefix = "Local:";

		// Token: 0x04000616 RID: 1558
		private const string E12At = "@";

		// Token: 0x04000617 RID: 1559
		private const string TiAt = "(a)";

		// Token: 0x04000618 RID: 1560
		private string type;

		// Token: 0x04000619 RID: 1561
		private string addressSpace;

		// Token: 0x0400061A RID: 1562
		private SmtpDomainWithSubdomains smtpDomainWithSubdomains;

		// Token: 0x0400061B RID: 1563
		private RoutingX400Address x400Address;

		// Token: 0x0400061C RID: 1564
		private int cost;

		// Token: 0x0400061D RID: 1565
		private bool isLocal;
	}
}
