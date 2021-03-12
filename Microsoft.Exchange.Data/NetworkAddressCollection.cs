using System;
using System.Collections;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200025C RID: 604
	[Serializable]
	public class NetworkAddressCollection : ProtocolAddressCollection<NetworkAddress>
	{
		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x06001456 RID: 5206 RVA: 0x0003FF0B File Offset: 0x0003E10B
		public new static NetworkAddressCollection Empty
		{
			get
			{
				return NetworkAddressCollection.empty;
			}
		}

		// Token: 0x06001457 RID: 5207 RVA: 0x0003FF12 File Offset: 0x0003E112
		public NetworkAddressCollection()
		{
		}

		// Token: 0x06001458 RID: 5208 RVA: 0x0003FF1A File Offset: 0x0003E11A
		public NetworkAddressCollection(object value) : base(value)
		{
		}

		// Token: 0x06001459 RID: 5209 RVA: 0x0003FF23 File Offset: 0x0003E123
		public NetworkAddressCollection(ICollection values) : base(values)
		{
		}

		// Token: 0x0600145A RID: 5210 RVA: 0x0003FF2C File Offset: 0x0003E12C
		public NetworkAddressCollection(Hashtable table) : base(table)
		{
		}

		// Token: 0x0600145B RID: 5211 RVA: 0x0003FF35 File Offset: 0x0003E135
		internal NetworkAddressCollection(bool readOnly, ProviderPropertyDefinition propertyDefinition, ICollection values) : base(readOnly, propertyDefinition, values)
		{
		}

		// Token: 0x0600145C RID: 5212 RVA: 0x0003FF40 File Offset: 0x0003E140
		internal NetworkAddressCollection(bool readOnly, ProviderPropertyDefinition propertyDefinition, ICollection values, ICollection invalidValues, LocalizedString? readOnlyErrorMessage) : base(readOnly, propertyDefinition, values, invalidValues, readOnlyErrorMessage)
		{
		}

		// Token: 0x0600145D RID: 5213 RVA: 0x0003FF4F File Offset: 0x0003E14F
		public new static implicit operator NetworkAddressCollection(object[] array)
		{
			return new NetworkAddressCollection(false, null, array);
		}

		// Token: 0x0600145E RID: 5214 RVA: 0x0003FF59 File Offset: 0x0003E159
		protected override NetworkAddress ConvertInput(object item)
		{
			if (item is string)
			{
				return NetworkAddress.Parse((string)item);
			}
			return base.ConvertInput(item);
		}

		// Token: 0x0600145F RID: 5215 RVA: 0x0003FF78 File Offset: 0x0003E178
		protected override bool TryAddInternal(NetworkAddress item, out Exception error)
		{
			if (null != item && null != this[item.ProtocolType as NetworkProtocol])
			{
				error = new ArgumentException(DataStrings.ExceptionNetworkProtocolDuplicate, "item");
				return false;
			}
			return base.TryAddInternal(item, out error);
		}

		// Token: 0x06001460 RID: 5216 RVA: 0x0003FFC8 File Offset: 0x0003E1C8
		protected override void SetAt(int index, NetworkAddress item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			if (base[index] == item)
			{
				return;
			}
			if (base[index].ProtocolType == item.ProtocolType)
			{
				base.SetAt(index, item);
				return;
			}
			NetworkAddress a = this[item.ProtocolType as NetworkProtocol];
			if (a == null)
			{
				base.SetAt(index, item);
				return;
			}
			throw new ArgumentException(DataStrings.ExceptionNetworkProtocolDuplicate, "item");
		}

		// Token: 0x1700061A RID: 1562
		public NetworkAddress this[NetworkProtocol protocol]
		{
			get
			{
				NetworkAddress result = null;
				foreach (NetworkAddress networkAddress in this)
				{
					if (networkAddress.ProtocolType == protocol)
					{
						result = networkAddress;
						break;
					}
				}
				return result;
			}
		}

		// Token: 0x04000BFC RID: 3068
		private static NetworkAddressCollection empty = new NetworkAddressCollection(true, null, new NetworkAddress[0]);
	}
}
