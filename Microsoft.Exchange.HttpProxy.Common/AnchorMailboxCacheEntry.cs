using System;
using System.Linq;
using System.Text;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.HttpProxy.Common;
using Microsoft.Exchange.SharedCache.Client;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x0200000E RID: 14
	public class AnchorMailboxCacheEntry : ISharedCacheEntry
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002FA2 File Offset: 0x000011A2
		// (set) Token: 0x06000033 RID: 51 RVA: 0x00002FAA File Offset: 0x000011AA
		public ADObjectId Database { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002FB3 File Offset: 0x000011B3
		// (set) Token: 0x06000035 RID: 53 RVA: 0x00002FBB File Offset: 0x000011BB
		public string DomainName { get; set; }

		// Token: 0x06000036 RID: 54 RVA: 0x00002FC4 File Offset: 0x000011C4
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				this.Database,
				'~',
				this.DomainName,
				'~',
				(this.Database == null) ? string.Empty : this.Database.PartitionFQDN
			});
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00003024 File Offset: 0x00001224
		public void FromByteArray(byte[] bytes)
		{
			if (bytes != null)
			{
				if (bytes.Length == 0)
				{
					throw new ArgumentException("Empty byte array for AnchorMailboxCacheEntry!");
				}
				int num = (bytes[0] == 1) ? 17 : 1;
				byte[] array = null;
				if (bytes[0] == 1)
				{
					if (bytes.Length < 17)
					{
						throw new ArgumentException(string.Format("There should be at least {0} bytes for the database GUID", 16));
					}
					array = new byte[16];
					Array.Copy(bytes, 1, array, 0, 16);
				}
				string domainName = null;
				string partitionFQDN = null;
				if (bytes.Length > num)
				{
					string @string = Encoding.ASCII.GetString(bytes, num, bytes.Length - num);
					Utilities.GetTwoSubstrings(@string, '~', out domainName, out partitionFQDN);
					this.DomainName = domainName;
				}
				if (array != null)
				{
					this.Database = new ADObjectId(new Guid(array), partitionFQDN);
				}
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000030D4 File Offset: 0x000012D4
		public byte[] ToByteArray()
		{
			byte[] array = new byte[]
			{
				(this.Database == null) ? 0 : 1
			};
			if (this.Database != null)
			{
				Guid objectGuid = this.Database.ObjectGuid;
				array = array.Concat(this.Database.ObjectGuid.ToByteArray()).ToArray<byte>();
			}
			if (!string.IsNullOrEmpty(this.DomainName))
			{
				array = array.Concat(Encoding.ASCII.GetBytes(this.DomainName)).ToArray<byte>();
			}
			if (this.Database != null && !string.IsNullOrEmpty(this.Database.PartitionFQDN))
			{
				array = array.Concat(Encoding.ASCII.GetBytes(new char[]
				{
					'~'
				})).ToArray<byte>();
				array = array.Concat(Encoding.ASCII.GetBytes(this.Database.PartitionFQDN)).ToArray<byte>();
			}
			return array;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000031B4 File Offset: 0x000013B4
		public override int GetHashCode()
		{
			int num = 0;
			if (!string.IsNullOrEmpty(this.DomainName))
			{
				num ^= this.DomainName.GetHashCode();
			}
			if (this.Database != null)
			{
				num ^= this.Database.ObjectGuid.GetHashCode();
				if (!string.IsNullOrEmpty(this.Database.PartitionFQDN))
				{
					num ^= this.Database.PartitionFQDN.GetHashCode();
				}
			}
			return num;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00003228 File Offset: 0x00001428
		public override bool Equals(object obj)
		{
			AnchorMailboxCacheEntry anchorMailboxCacheEntry = obj as AnchorMailboxCacheEntry;
			if (anchorMailboxCacheEntry != null && ((string.IsNullOrEmpty(this.DomainName) && string.IsNullOrEmpty(anchorMailboxCacheEntry.DomainName)) || string.Equals(this.DomainName, anchorMailboxCacheEntry.DomainName)))
			{
				if (this.Database == null)
				{
					return anchorMailboxCacheEntry.Database == null;
				}
				if (anchorMailboxCacheEntry.Database != null && this.Database.ObjectGuid == anchorMailboxCacheEntry.Database.ObjectGuid)
				{
					return (string.IsNullOrEmpty(this.Database.PartitionFQDN) && string.IsNullOrEmpty(anchorMailboxCacheEntry.Database.PartitionFQDN)) || string.Equals(this.Database.PartitionFQDN, anchorMailboxCacheEntry.Database.PartitionFQDN);
				}
			}
			return false;
		}

		// Token: 0x04000054 RID: 84
		private const int GuidLengthInBytes = 16;

		// Token: 0x04000055 RID: 85
		private const char Separator = '~';
	}
}
