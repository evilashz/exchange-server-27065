using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Transport;

namespace Microsoft.Exchange.Transport.RecipientAPI
{
	// Token: 0x020001BD RID: 445
	internal sealed class AddressHashes
	{
		// Token: 0x06001257 RID: 4695 RVA: 0x00058988 File Offset: 0x00056B88
		public AddressHashes(byte[] hashArray)
		{
			if (hashArray == null)
			{
				throw new ArgumentNullException("hashArray");
			}
			this.List = new SortedList<uint, byte>(hashArray.Length / 4);
			for (int i = 0; i < hashArray.Length / 4; i++)
			{
				int num = i * 4;
				uint num2 = (uint)hashArray[num + 3];
				num2 <<= 8;
				num2 |= (uint)hashArray[num + 2];
				num2 <<= 8;
				num2 |= (uint)hashArray[num + 1];
				num2 <<= 8;
				num2 |= (uint)hashArray[num];
				this.List.Add(num2, 0);
			}
		}

		// Token: 0x06001258 RID: 4696 RVA: 0x00058A0D File Offset: 0x00056C0D
		public AddressHashes()
		{
			this.List = new SortedList<uint, byte>();
		}

		// Token: 0x06001259 RID: 4697 RVA: 0x00058A2C File Offset: 0x00056C2C
		public void Add(string addressOrDomain)
		{
			if (string.IsNullOrEmpty(addressOrDomain))
			{
				return;
			}
			if (addressOrDomain[0] == '@')
			{
				addressOrDomain = addressOrDomain.Substring(1);
				if (!SmtpAddress.IsValidDomain(addressOrDomain))
				{
					return;
				}
			}
			else if (!RoutingAddress.IsValidAddress(addressOrDomain))
			{
				return;
			}
			uint key = (uint)this.hasher.GetHash(addressOrDomain);
			if (this.List.ContainsKey(key))
			{
				return;
			}
			this.List.Add(key, 0);
		}

		// Token: 0x0600125A RID: 4698 RVA: 0x00058A94 File Offset: 0x00056C94
		public byte[] GetBytes()
		{
			byte[] array = new byte[this.List.Count * 4];
			for (int i = 0; i < this.List.Count; i++)
			{
				int num = i * 4;
				uint num2 = this.List.Keys[i];
				array[num] = (byte)(num2 & 255U);
				array[num + 1] = (byte)((num2 & 65280U) >> 8);
				array[num + 2] = (byte)((num2 & 16711680U) >> 16);
				array[num + 3] = (byte)((num2 & 4278190080U) >> 24);
			}
			return array;
		}

		// Token: 0x0600125B RID: 4699 RVA: 0x00058B1C File Offset: 0x00056D1C
		public uint[] GetHashArray()
		{
			uint[] array = new uint[this.List.Count];
			this.List.Keys.CopyTo(array, 0);
			return array;
		}

		// Token: 0x0600125C RID: 4700 RVA: 0x00058B50 File Offset: 0x00056D50
		public bool Contains(RoutingAddress address)
		{
			if (!AddressHashes.IsValidAddress(address))
			{
				return false;
			}
			uint key = (uint)this.hasher.GetHash((string)address);
			if (this.List.ContainsKey(key))
			{
				return true;
			}
			string domainPart = address.DomainPart;
			key = (uint)this.hasher.GetHash(domainPart);
			return this.List.ContainsKey(key);
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x0600125D RID: 4701 RVA: 0x00058BB1 File Offset: 0x00056DB1
		public int Count
		{
			get
			{
				if (this.List != null)
				{
					return this.List.Count;
				}
				return 0;
			}
		}

		// Token: 0x0600125E RID: 4702 RVA: 0x00058BC8 File Offset: 0x00056DC8
		private static bool IsValidAddress(RoutingAddress address)
		{
			return address.IsValid && !(address == RoutingAddress.NullReversePath);
		}

		// Token: 0x04000A97 RID: 2711
		internal SortedList<uint, byte> List;

		// Token: 0x04000A98 RID: 2712
		private StringHasher hasher = new StringHasher();
	}
}
