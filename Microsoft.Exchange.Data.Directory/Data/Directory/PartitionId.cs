using System;
using System.Threading;
using Microsoft.Exchange.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000031 RID: 49
	[Serializable]
	internal class PartitionId : IEquatable<PartitionId>
	{
		// Token: 0x06000301 RID: 769 RVA: 0x0001068E File Offset: 0x0000E88E
		internal PartitionId(string fqdn)
		{
			this.ValidatePartitionFqdn(fqdn);
			this.forestFQDN = fqdn;
		}

		// Token: 0x06000302 RID: 770 RVA: 0x000106A4 File Offset: 0x0000E8A4
		internal PartitionId(Guid partitionObjectId)
		{
			if (partitionObjectId == Guid.Empty)
			{
				throw new ArgumentNullException("partitionObjectId");
			}
			this.forestFQDN = ADAccountPartitionLocator.GetAccountPartitionFqdnByPartitionGuid(partitionObjectId);
			this.ValidatePartitionFqdn(this.forestFQDN);
			this.partitionObjectId = new Guid?(partitionObjectId);
		}

		// Token: 0x06000303 RID: 771 RVA: 0x000106F3 File Offset: 0x0000E8F3
		internal PartitionId(string fqdn, Guid partitionObjectId) : this(fqdn)
		{
			if (partitionObjectId == Guid.Empty)
			{
				throw new ArgumentNullException("partitionObjectId");
			}
			this.partitionObjectId = new Guid?(partitionObjectId);
		}

		// Token: 0x06000304 RID: 772 RVA: 0x00010720 File Offset: 0x0000E920
		internal PartitionId(ADObjectId adObjectId)
		{
			if (string.IsNullOrEmpty(adObjectId.PartitionFQDN))
			{
				throw new ArgumentException("adObjectId");
			}
			this.forestFQDN = adObjectId.PartitionFQDN;
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0001074C File Offset: 0x0000E94C
		internal static bool TryParse(string input, out PartitionId partitionId)
		{
			partitionId = null;
			if (string.IsNullOrEmpty(input))
			{
				return false;
			}
			string[] array = input.Split(new char[]
			{
				':'
			});
			if (array.Length != 2)
			{
				return false;
			}
			Guid? guid = null;
			string text = null;
			Guid value;
			if (!array[0].Equals("<null>", StringComparison.OrdinalIgnoreCase) && Guid.TryParse(array[0], out value))
			{
				guid = new Guid?(value);
			}
			if (!array[1].Equals("<null>", StringComparison.OrdinalIgnoreCase))
			{
				text = array[1];
			}
			try
			{
				if (guid != null && text != null)
				{
					partitionId = new PartitionId(text, guid.Value);
				}
				else if (guid != null)
				{
					partitionId = new PartitionId(guid.Value);
				}
				else
				{
					if (text == null)
					{
						return false;
					}
					partitionId = new PartitionId(text);
				}
				return true;
			}
			catch (ArgumentException)
			{
			}
			return false;
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0001082C File Offset: 0x0000EA2C
		public static bool operator ==(PartitionId partition1, PartitionId partition2)
		{
			return (partition1 == null && partition2 == null) || (partition1 != null && partition2 != null && string.Equals(partition1.ForestFQDN.Trim(), partition2.ForestFQDN.Trim(), StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x06000307 RID: 775 RVA: 0x00010869 File Offset: 0x0000EA69
		public static bool IsLocalForestPartition(string partitionFqdn)
		{
			return partitionFqdn.Equals(TopologyProvider.LocalForestFqdn, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000308 RID: 776 RVA: 0x00010877 File Offset: 0x0000EA77
		public static bool operator !=(PartitionId partition1, PartitionId partition2)
		{
			return !(partition1 == partition2);
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000309 RID: 777 RVA: 0x00010883 File Offset: 0x0000EA83
		public static PartitionId LocalForest
		{
			get
			{
				if (PartitionId.localForest == null)
				{
					Interlocked.CompareExchange<PartitionId>(ref PartitionId.localForest, new PartitionId(TopologyProvider.LocalForestFqdn), null);
				}
				return PartitionId.localForest;
			}
		}

		// Token: 0x0600030A RID: 778 RVA: 0x000108B0 File Offset: 0x0000EAB0
		public static bool TryParse(string fqdn, out PartitionId partitionId, out Exception ex)
		{
			ex = null;
			partitionId = null;
			bool result;
			try
			{
				partitionId = new PartitionId(fqdn);
				result = true;
			}
			catch (Exception ex2)
			{
				ex = ex2;
				result = false;
			}
			return result;
		}

		// Token: 0x0600030B RID: 779 RVA: 0x000108E8 File Offset: 0x0000EAE8
		public bool IsLocalForestPartition()
		{
			return PartitionId.IsLocalForestPartition(this.ForestFQDN);
		}

		// Token: 0x0600030C RID: 780 RVA: 0x000108F8 File Offset: 0x0000EAF8
		public override string ToString()
		{
			return string.Format("{0}:{1}", (this.PartitionObjectId != null) ? this.PartitionObjectId.Value.ToString() : "<null>", this.ForestFQDN ?? "<null>");
		}

		// Token: 0x0600030D RID: 781 RVA: 0x00010951 File Offset: 0x0000EB51
		public bool Equals(PartitionId partitionId)
		{
			return partitionId != null && partitionId == this;
		}

		// Token: 0x0600030E RID: 782 RVA: 0x00010960 File Offset: 0x0000EB60
		public override bool Equals(object obj)
		{
			PartitionId partitionId = obj as PartitionId;
			return partitionId != null && this.Equals(partitionId);
		}

		// Token: 0x0600030F RID: 783 RVA: 0x00010986 File Offset: 0x0000EB86
		public override int GetHashCode()
		{
			return this.forestFQDN.GetHashCode();
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000310 RID: 784 RVA: 0x00010993 File Offset: 0x0000EB93
		internal string ForestFQDN
		{
			get
			{
				return this.forestFQDN;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000311 RID: 785 RVA: 0x0001099B File Offset: 0x0000EB9B
		internal Guid? PartitionObjectId
		{
			get
			{
				return this.partitionObjectId;
			}
		}

		// Token: 0x06000312 RID: 786 RVA: 0x000109A4 File Offset: 0x0000EBA4
		internal void ValidatePartitionFqdn(string fqdn)
		{
			if (string.IsNullOrEmpty(fqdn))
			{
				throw new ArgumentNullException("fqdn");
			}
			if (!Fqdn.IsValidFqdn(fqdn.Trim()))
			{
				throw new ArgumentException(string.Format("Invalid fqdn parameter value: '{0}'", fqdn.Trim()));
			}
			if (Datacenter.IsMicrosoftHostedOnly(true) && !Datacenter.IsDatacenterDedicated(true) && !PartitionId.IsLocalForestPartition(fqdn) && !fqdn.EndsWith("outlook.com", StringComparison.OrdinalIgnoreCase) && !fqdn.EndsWith("exchangelabs.com", StringComparison.OrdinalIgnoreCase) && !fqdn.EndsWith("outlook.cn", StringComparison.OrdinalIgnoreCase) && !fqdn.EndsWith("extest.microsoft.com", StringComparison.OrdinalIgnoreCase))
			{
				throw new ArgumentException(DirectoryStrings.InvalidPartitionFqdn(fqdn));
			}
		}

		// Token: 0x040000CF RID: 207
		private readonly string forestFQDN;

		// Token: 0x040000D0 RID: 208
		private readonly Guid? partitionObjectId;

		// Token: 0x040000D1 RID: 209
		private static PartitionId localForest;
	}
}
