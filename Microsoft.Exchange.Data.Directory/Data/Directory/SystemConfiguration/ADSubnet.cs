using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Net;
using System.Net.Sockets;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000394 RID: 916
	[ObjectScope(ConfigScopes.Global)]
	[Serializable]
	public sealed class ADSubnet : ADNonExchangeObject, IComparable<ADSubnet>
	{
		// Token: 0x17000B97 RID: 2967
		// (get) Token: 0x060029C1 RID: 10689 RVA: 0x000AF13F File Offset: 0x000AD33F
		internal override ADObjectSchema Schema
		{
			get
			{
				return ADSubnet.schema;
			}
		}

		// Token: 0x17000B98 RID: 2968
		// (get) Token: 0x060029C2 RID: 10690 RVA: 0x000AF146 File Offset: 0x000AD346
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ADSubnet.mostDerivedClass;
			}
		}

		// Token: 0x17000B99 RID: 2969
		// (get) Token: 0x060029C4 RID: 10692 RVA: 0x000AF155 File Offset: 0x000AD355
		// (set) Token: 0x060029C5 RID: 10693 RVA: 0x000AF167 File Offset: 0x000AD367
		public ADObjectId Site
		{
			get
			{
				return (ADObjectId)this[ADSubnetSchema.Site];
			}
			set
			{
				this[ADSubnetSchema.Site] = value;
			}
		}

		// Token: 0x17000B9A RID: 2970
		// (get) Token: 0x060029C6 RID: 10694 RVA: 0x000AF175 File Offset: 0x000AD375
		// (set) Token: 0x060029C7 RID: 10695 RVA: 0x000AF187 File Offset: 0x000AD387
		[Parameter]
		public IPAddress IPAddress
		{
			get
			{
				return (IPAddress)this[ADSubnetSchema.IPAddress];
			}
			set
			{
				this[ADSubnetSchema.IPAddress] = value;
			}
		}

		// Token: 0x17000B9B RID: 2971
		// (get) Token: 0x060029C8 RID: 10696 RVA: 0x000AF195 File Offset: 0x000AD395
		// (set) Token: 0x060029C9 RID: 10697 RVA: 0x000AF1A7 File Offset: 0x000AD3A7
		[Parameter]
		public int MaskBits
		{
			get
			{
				return (int)this[ADSubnetSchema.MaskBits];
			}
			set
			{
				this[ADSubnetSchema.MaskBits] = value;
			}
		}

		// Token: 0x060029CA RID: 10698 RVA: 0x000AF1BC File Offset: 0x000AD3BC
		internal static object IPAddressGetter(IPropertyBag propertyBag)
		{
			object result;
			try
			{
				string[] nameParts = ADSubnet.GetNameParts(propertyBag);
				IPAddress ipaddress = IPAddress.Parse(nameParts[0]);
				result = ipaddress;
			}
			catch (FormatException ex)
			{
				throw new DataValidationException(new PropertyValidationError(DirectoryStrings.CannotCalculateProperty("IPAddress", ex.Message), ADSubnetSchema.IPAddress, propertyBag[ADObjectSchema.Id]), ex);
			}
			return result;
		}

		// Token: 0x060029CB RID: 10699 RVA: 0x000AF21C File Offset: 0x000AD41C
		internal static void IPAddressSetter(object value, IPropertyBag propertyBag)
		{
			string[] nameParts = ADSubnet.GetNameParts(propertyBag);
			string value2 = value.ToString() + "/" + nameParts[1];
			propertyBag[ADObjectSchema.RawName] = value2;
		}

		// Token: 0x060029CC RID: 10700 RVA: 0x000AF250 File Offset: 0x000AD450
		internal static object MaskBitsGetter(IPropertyBag propertyBag)
		{
			object result;
			try
			{
				string[] nameParts = ADSubnet.GetNameParts(propertyBag);
				result = int.Parse(nameParts[1]);
			}
			catch (FormatException ex)
			{
				throw new DataValidationException(new PropertyValidationError(DirectoryStrings.CannotCalculateProperty("MaskBits", ex.Message), ADSubnetSchema.MaskBits, propertyBag[ADObjectSchema.Id]), ex);
			}
			return result;
		}

		// Token: 0x060029CD RID: 10701 RVA: 0x000AF2B4 File Offset: 0x000AD4B4
		internal static void MaskBitsSetter(object value, IPropertyBag propertyBag)
		{
			try
			{
				string[] nameParts = ADSubnet.GetNameParts(propertyBag);
				string value2 = nameParts[0] + "/" + value.ToString();
				propertyBag[ADObjectSchema.RawName] = value2;
			}
			catch (FormatException ex)
			{
				throw new DataValidationException(new PropertyValidationError(DirectoryStrings.CannotCalculateProperty("MaskBits", ex.Message), ADObjectSchema.Id, propertyBag[ADObjectSchema.Id]), ex);
			}
		}

		// Token: 0x060029CE RID: 10702 RVA: 0x000AF328 File Offset: 0x000AD528
		private static string[] GetNameParts(IPropertyBag propertyBag)
		{
			string text = (string)propertyBag[ADObjectSchema.RawName];
			string[] array = text.Split(new char[]
			{
				'/'
			});
			if (array.Length != 2 || string.IsNullOrEmpty(array[0]) || string.IsNullOrEmpty(array[1]))
			{
				throw new FormatException(DirectoryStrings.InvalidSubnetNameFormat(text));
			}
			return array;
		}

		// Token: 0x060029CF RID: 10703 RVA: 0x000AF388 File Offset: 0x000AD588
		public int CompareTo(ADSubnet rhs)
		{
			return this.MaskBits.CompareTo(rhs.MaskBits);
		}

		// Token: 0x060029D0 RID: 10704 RVA: 0x000AF3AC File Offset: 0x000AD5AC
		internal bool Match(IPAddress iPAddress)
		{
			if (this.subnetIPRange == null || !this.subnetIPRange.LowerBound.Equals(this.IPAddress) || (this.subnetIPRange.RangeFormat == IPRange.Format.CIDR && (int)this.subnetIPRange.CIDRLength != this.MaskBits))
			{
				this.subnetIPRange = IPRange.CreateIPAndCIDR(this.IPAddress, (short)this.MaskBits);
			}
			return this.subnetIPRange.Contains(iPAddress);
		}

		// Token: 0x060029D1 RID: 10705 RVA: 0x000AF428 File Offset: 0x000AD628
		private static IPAddress Mask(IPAddress iPAddress, int maskBits)
		{
			IPvxAddress v = new IPvxAddress(iPAddress);
			IPvxAddress mask = ~(~IPvxAddress.Zero >> maskBits);
			return v & mask;
		}

		// Token: 0x060029D2 RID: 10706 RVA: 0x000AF460 File Offset: 0x000AD660
		protected override void ValidateRead(List<ValidationError> errors)
		{
			base.ValidateRead(errors);
			int num = 32;
			if (this.IPAddress.AddressFamily == AddressFamily.InterNetworkV6)
			{
				num = 128;
			}
			if (this.MaskBits < 2 || this.MaskBits > num)
			{
				errors.Add(new ObjectValidationError(DirectoryStrings.ErrorSubnetMaskOutOfRange(this.MaskBits, this.IPAddress.ToString(), 2, num), base.Id, string.Empty));
			}
		}

		// Token: 0x060029D3 RID: 10707 RVA: 0x000AF4CC File Offset: 0x000AD6CC
		protected override void ValidateWrite(List<ValidationError> errors)
		{
			base.ValidateWrite(errors);
			if (this.MaskBits < 2)
			{
				errors.Add(new ObjectValidationError(DirectoryStrings.ErrorSubnetMaskLessThanMinRange(this.MaskBits, 2), base.Id, string.Empty));
				return;
			}
			if (this.MaskBits > this.IPAddress.GetAddressBytes().Length * 8)
			{
				errors.Add(new ObjectValidationError(DirectoryStrings.ErrorSubnetMaskGreaterThanAddress(this.MaskBits, this.IPAddress.ToString()), base.Id, string.Empty));
				return;
			}
			if (!this.Match(this.IPAddress))
			{
				errors.Add(new ObjectValidationError(DirectoryStrings.ErrorSubnetAddressDoesNotMatchMask(this.IPAddress.ToString(), this.MaskBits, ADSubnet.Mask(this.IPAddress, this.MaskBits).ToString()), base.Id, string.Empty));
				return;
			}
			if (new IPvxAddress(this.IPAddress) != this.subnetIPRange.LowerBound)
			{
				errors.Add(new ObjectValidationError(DirectoryStrings.ErrorSubnetAddressDoesNotMatchMask(this.IPAddress.ToString(), this.MaskBits, this.subnetIPRange.LowerBound.ToString()), base.Id, string.Empty));
			}
		}

		// Token: 0x04001985 RID: 6533
		private const int IpMaskMinRange = 2;

		// Token: 0x04001986 RID: 6534
		private const int Ipv4MaskMaxRange = 32;

		// Token: 0x04001987 RID: 6535
		private const int Ipv6MaskMaxRange = 128;

		// Token: 0x04001988 RID: 6536
		private static ADSubnetSchema schema = ObjectSchema.GetInstance<ADSubnetSchema>();

		// Token: 0x04001989 RID: 6537
		private static string mostDerivedClass = "subnet";

		// Token: 0x0400198A RID: 6538
		private IPRange subnetIPRange;
	}
}
