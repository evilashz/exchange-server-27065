using System;
using System.Collections.Generic;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000176 RID: 374
	internal class NamedPropMapper : LookupTable<NamedPropMapper.Mapping>
	{
		// Token: 0x06000E44 RID: 3652 RVA: 0x000208DC File Offset: 0x0001EADC
		public NamedPropMapper(IMailbox mailbox, bool createMappingsIfNeeded)
		{
			this.mailbox = mailbox;
			this.createMappingsIfNeeded = createMappingsIfNeeded;
			this.byId = new NamedPropMapper.PropIdIndex();
			this.byNamedProp = new NamedPropMapper.NamedPropIndex();
			base.RegisterIndex(this.byId);
			base.RegisterIndex(this.byNamedProp);
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06000E45 RID: 3653 RVA: 0x0002092B File Offset: 0x0001EB2B
		public NamedPropMapper.PropIdIndex ById
		{
			get
			{
				return this.byId;
			}
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06000E46 RID: 3654 RVA: 0x00020933 File Offset: 0x0001EB33
		public NamedPropMapper.NamedPropIndex ByNamedProp
		{
			get
			{
				return this.byNamedProp;
			}
		}

		// Token: 0x06000E47 RID: 3655 RVA: 0x0002093C File Offset: 0x0001EB3C
		public PropTag MapNamedProp(NamedPropData npData, PropType propType)
		{
			NamedPropMapper.Mapping mapping = this.ByNamedProp[npData];
			if (mapping != null)
			{
				return PropTagHelper.PropTagFromIdAndType(mapping.PropId, propType);
			}
			return PropTag.Unresolved;
		}

		// Token: 0x0400080C RID: 2060
		private IMailbox mailbox;

		// Token: 0x0400080D RID: 2061
		private bool createMappingsIfNeeded;

		// Token: 0x0400080E RID: 2062
		private NamedPropMapper.PropIdIndex byId;

		// Token: 0x0400080F RID: 2063
		private NamedPropMapper.NamedPropIndex byNamedProp;

		// Token: 0x02000177 RID: 375
		public class Mapping
		{
			// Token: 0x06000E48 RID: 3656 RVA: 0x00020968 File Offset: 0x0001EB68
			public Mapping(int propId, NamedPropData npData)
			{
				this.PropId = propId;
				this.NPData = npData;
			}

			// Token: 0x17000473 RID: 1139
			// (get) Token: 0x06000E49 RID: 3657 RVA: 0x0002097E File Offset: 0x0001EB7E
			// (set) Token: 0x06000E4A RID: 3658 RVA: 0x00020986 File Offset: 0x0001EB86
			public int PropId { get; set; }

			// Token: 0x17000474 RID: 1140
			// (get) Token: 0x06000E4B RID: 3659 RVA: 0x0002098F File Offset: 0x0001EB8F
			// (set) Token: 0x06000E4C RID: 3660 RVA: 0x00020997 File Offset: 0x0001EB97
			public NamedPropData NPData { get; set; }
		}

		// Token: 0x02000178 RID: 376
		public class PropIdIndex : LookupIndex<int, NamedPropMapper.Mapping>
		{
			// Token: 0x06000E4D RID: 3661 RVA: 0x000209A0 File Offset: 0x0001EBA0
			protected override ICollection<int> RetrieveKeys(NamedPropMapper.Mapping data)
			{
				return new int[]
				{
					data.PropId
				};
			}

			// Token: 0x06000E4E RID: 3662 RVA: 0x000209C0 File Offset: 0x0001EBC0
			protected override NamedPropMapper.Mapping[] LookupKeys(int[] keys)
			{
				PropTag[] array = new PropTag[keys.Length];
				for (int i = 0; i < keys.Length; i++)
				{
					array[i] = PropTagHelper.PropTagFromIdAndType(keys[i], PropType.Unspecified);
				}
				NamedPropData[] namesFromIDs = ((NamedPropMapper)base.Owner).mailbox.GetNamesFromIDs(array);
				NamedPropMapper.Mapping[] array2 = new NamedPropMapper.Mapping[namesFromIDs.Length];
				for (int j = 0; j < array.Length; j++)
				{
					if (namesFromIDs[j] == null)
					{
						array2[j] = null;
					}
					else
					{
						array2[j] = new NamedPropMapper.Mapping(keys[j], namesFromIDs[j]);
					}
				}
				return array2;
			}
		}

		// Token: 0x02000179 RID: 377
		public class NamedPropIndex : LookupIndex<NamedPropData, NamedPropMapper.Mapping>
		{
			// Token: 0x06000E50 RID: 3664 RVA: 0x00020A4C File Offset: 0x0001EC4C
			protected override ICollection<NamedPropData> RetrieveKeys(NamedPropMapper.Mapping data)
			{
				return new NamedPropData[]
				{
					data.NPData
				};
			}

			// Token: 0x06000E51 RID: 3665 RVA: 0x00020A6C File Offset: 0x0001EC6C
			protected override NamedPropMapper.Mapping[] LookupKeys(NamedPropData[] keys)
			{
				NamedPropMapper namedPropMapper = (NamedPropMapper)base.Owner;
				PropTag[] idsFromNames = namedPropMapper.mailbox.GetIDsFromNames(namedPropMapper.createMappingsIfNeeded, keys);
				NamedPropMapper.Mapping[] array = new NamedPropMapper.Mapping[idsFromNames.Length];
				for (int i = 0; i < idsFromNames.Length; i++)
				{
					int num = idsFromNames[i].Id();
					if (num == 10)
					{
						array[i] = null;
					}
					else
					{
						array[i] = new NamedPropMapper.Mapping(num, keys[i]);
					}
				}
				return array;
			}
		}
	}
}
