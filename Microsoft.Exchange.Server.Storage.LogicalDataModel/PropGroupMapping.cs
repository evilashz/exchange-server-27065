using System;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x020000AC RID: 172
	public struct PropGroupMapping
	{
		// Token: 0x060009D8 RID: 2520 RVA: 0x00050330 File Offset: 0x0004E530
		public PropGroupMapping(Context context, Mailbox mailbox)
		{
			this.mappingId = MessagePropGroups.CurrentGroupMappingId;
			this.groups = new StorePropTag[MessagePropGroups.NumberedGroupLists.Length][];
			for (int i = 0; i < this.groups.Length; i++)
			{
				this.groups[i] = new StorePropTag[MessagePropGroups.NumberedGroupLists[i].Length];
				for (int j = 0; j < this.groups[i].Length; j++)
				{
					StorePropInfo storePropInfo = MessagePropGroups.NumberedGroupLists[i][j];
					ushort propId;
					if (storePropInfo.IsNamedProperty)
					{
						StoreNamedPropInfo objB;
						mailbox.NamedPropertyMap.GetNumberFromName(context, storePropInfo.PropName, true, mailbox.QuotaInfo, out propId, out objB);
						Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(object.ReferenceEquals(storePropInfo, objB), "unexpected scenario for a named props");
					}
					else
					{
						propId = storePropInfo.PropId;
					}
					this.groups[i][j] = new StorePropTag(propId, storePropInfo.PropType, storePropInfo, ObjectType.Message);
				}
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x060009D9 RID: 2521 RVA: 0x0005040A File Offset: 0x0004E60A
		public int MappingId
		{
			get
			{
				return this.mappingId;
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x060009DA RID: 2522 RVA: 0x00050412 File Offset: 0x0004E612
		public int Count
		{
			get
			{
				if (this.groups != null)
				{
					return this.groups.Length;
				}
				return 0;
			}
		}

		// Token: 0x17000220 RID: 544
		public StorePropTag[] this[int groupIndex]
		{
			get
			{
				return this.groups[groupIndex];
			}
		}

		// Token: 0x040004B2 RID: 1202
		private int mappingId;

		// Token: 0x040004B3 RID: 1203
		private StorePropTag[][] groups;
	}
}
