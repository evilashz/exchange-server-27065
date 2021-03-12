using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.AddressBook.Nspi
{
	// Token: 0x02000028 RID: 40
	internal class EphemeralIdTable
	{
		// Token: 0x06000155 RID: 341 RVA: 0x00006B6B File Offset: 0x00004D6B
		internal static bool IsActiveDirectoryEphemeralId(int mid)
		{
			return mid >= 16;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00006B75 File Offset: 0x00004D75
		internal static EphemeralIdTable.NamingContext GetNamingContext(ADRawEntry rawEntry)
		{
			return EphemeralIdTable.GetNamingContext((ADObjectId)rawEntry[ADObjectSchema.Id]);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00006B8C File Offset: 0x00004D8C
		internal static EphemeralIdTable.NamingContext GetNamingContext(ADObjectId id)
		{
			if (id.IsDescendantOf(ADSession.GetConfigurationNamingContext(id.GetPartitionId().ForestFQDN)))
			{
				if (!ADSession.IsTenantIdentity(id, id.GetPartitionId().ForestFQDN))
				{
					return EphemeralIdTable.NamingContext.Config;
				}
				return EphemeralIdTable.NamingContext.TenantConfig;
			}
			else
			{
				if (!ADSession.IsTenantIdentity(id, id.GetPartitionId().ForestFQDN))
				{
					return EphemeralIdTable.NamingContext.Domain;
				}
				if (!id.IsDescendantOf(ADSession.GetConfigurationUnitsRoot(id.GetPartitionId().ForestFQDN)))
				{
					return EphemeralIdTable.NamingContext.Domain;
				}
				return EphemeralIdTable.NamingContext.TenantConfig;
			}
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00006BF8 File Offset: 0x00004DF8
		internal int CreateEphemeralId(Guid guid, EphemeralIdTable.NamingContext namingContext)
		{
			int count;
			if (!this.guidToEphemeralId.TryGetValue(guid, out count))
			{
				count = this.ephemeralIdToEntry.Count;
				this.ephemeralIdToEntry.Add(new EphemeralIdTable.Entry(guid, namingContext));
				this.guidToEphemeralId.Add(guid, count);
			}
			return -(count + 16);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00006C48 File Offset: 0x00004E48
		internal bool GetGuid(int id, out Guid guid, out EphemeralIdTable.NamingContext namingContext)
		{
			if (!this.IsAddressBookEphemeralId(id))
			{
				guid = EphemeralIdTable.InvalidGuid;
				namingContext = EphemeralIdTable.NamingContext.Invalid;
				return false;
			}
			id = -id;
			EphemeralIdTable.Entry entry = this.ephemeralIdToEntry[id - 16];
			guid = entry.Guid;
			namingContext = entry.NamingContext;
			return true;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00006C98 File Offset: 0x00004E98
		internal void ConvertIdsToGuids(int[] ids, out Guid[] guids, out EphemeralIdTable.NamingContext[] contexts)
		{
			guids = new Guid[ids.Length];
			contexts = new EphemeralIdTable.NamingContext[ids.Length];
			for (int i = 0; i < ids.Length; i++)
			{
				Guid guid;
				EphemeralIdTable.NamingContext namingContext;
				this.GetGuid(ids[i], out guid, out namingContext);
				guids[i] = guid;
				contexts[i] = namingContext;
			}
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00006CE8 File Offset: 0x00004EE8
		internal bool IsAddressBookEphemeralId(int mid)
		{
			if (mid > -16)
			{
				return false;
			}
			mid = -mid;
			return mid >= 16 && mid < 16 + this.ephemeralIdToEntry.Count;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00006D0D File Offset: 0x00004F0D
		internal void AddIdMapping(int mid1, int mid2)
		{
			if (this.midMappingTable == null)
			{
				this.midMappingTable = new Dictionary<int, int>(4);
			}
			this.midMappingTable[mid1] = mid2;
			this.midMappingTable[mid2] = mid1;
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00006D3D File Offset: 0x00004F3D
		internal bool TryGetMapping(int inputMid, out int outputMid)
		{
			if (this.midMappingTable == null || !this.midMappingTable.TryGetValue(inputMid, out outputMid))
			{
				outputMid = -1;
				return false;
			}
			return true;
		}

		// Token: 0x040000DA RID: 218
		internal const int InvalidId = -1;

		// Token: 0x040000DB RID: 219
		internal const int NoGalId = -2;

		// Token: 0x040000DC RID: 220
		internal const int GlobalAddressList = 0;

		// Token: 0x040000DD RID: 221
		internal const int Beginning = 0;

		// Token: 0x040000DE RID: 222
		internal const int Current = 1;

		// Token: 0x040000DF RID: 223
		internal const int End = 2;

		// Token: 0x040000E0 RID: 224
		private const int InitialSize = 20;

		// Token: 0x040000E1 RID: 225
		private const int FirstEphemeralId = 16;

		// Token: 0x040000E2 RID: 226
		private const int FirstActiveDirectoryEphemeralId = 16;

		// Token: 0x040000E3 RID: 227
		private const int FirstAddressBookEphemeralId = -16;

		// Token: 0x040000E4 RID: 228
		internal static readonly Guid InvalidGuid = new Guid(uint.MaxValue, ushort.MaxValue, ushort.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);

		// Token: 0x040000E5 RID: 229
		private Dictionary<Guid, int> guidToEphemeralId = new Dictionary<Guid, int>(20);

		// Token: 0x040000E6 RID: 230
		private List<EphemeralIdTable.Entry> ephemeralIdToEntry = new List<EphemeralIdTable.Entry>(20);

		// Token: 0x040000E7 RID: 231
		private Dictionary<int, int> midMappingTable;

		// Token: 0x02000029 RID: 41
		internal enum NamingContext
		{
			// Token: 0x040000E9 RID: 233
			Invalid,
			// Token: 0x040000EA RID: 234
			Domain,
			// Token: 0x040000EB RID: 235
			Config,
			// Token: 0x040000EC RID: 236
			TenantDomain,
			// Token: 0x040000ED RID: 237
			TenantConfig
		}

		// Token: 0x0200002A RID: 42
		private class Entry
		{
			// Token: 0x17000050 RID: 80
			// (get) Token: 0x06000160 RID: 352 RVA: 0x00006DC8 File Offset: 0x00004FC8
			// (set) Token: 0x06000161 RID: 353 RVA: 0x00006DD0 File Offset: 0x00004FD0
			internal Guid Guid { get; set; }

			// Token: 0x17000051 RID: 81
			// (get) Token: 0x06000162 RID: 354 RVA: 0x00006DD9 File Offset: 0x00004FD9
			// (set) Token: 0x06000163 RID: 355 RVA: 0x00006DE1 File Offset: 0x00004FE1
			internal EphemeralIdTable.NamingContext NamingContext { get; set; }

			// Token: 0x06000164 RID: 356 RVA: 0x00006DEA File Offset: 0x00004FEA
			internal Entry(Guid guid, EphemeralIdTable.NamingContext namingContext)
			{
				this.Guid = guid;
				this.NamingContext = namingContext;
			}
		}
	}
}
