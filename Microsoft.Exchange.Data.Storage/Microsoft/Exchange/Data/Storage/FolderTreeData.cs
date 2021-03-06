using System;
using System.Collections.Generic;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data.Storage.OutlookClassIds;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000397 RID: 919
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class FolderTreeData : MessageItem, IFolderTreeData, IMessageItem, IToDoItem, IItem, IStoreObject, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x060028A3 RID: 10403 RVA: 0x000A20F4 File Offset: 0x000A02F4
		protected static VersionedId FindFirstRowMatchingFilter(MailboxSession session, PropertyDefinition[] filterProperties, FolderTreeData.RowMatchesFilterDelegate rowMatchesFilter)
		{
			bool flag;
			return FolderTreeData.GetValueFromFirstMatchingRow<VersionedId>(session, null, rowMatchesFilter, filterProperties, ItemSchema.Id, out flag);
		}

		// Token: 0x060028A4 RID: 10404 RVA: 0x000A2111 File Offset: 0x000A0311
		protected static byte[] GetOrdinalValueOfFirstMatchingNode(MailboxSession session, SortBy[] sortOrder, FolderTreeData.RowMatchesFilterDelegate rowMatchesFilter, PropertyDefinition[] filterProperties, out bool noRowsFound)
		{
			return FolderTreeData.GetValueFromFirstMatchingRow<byte[]>(session, sortOrder, rowMatchesFilter, filterProperties, FolderTreeDataSchema.Ordinal, out noRowsFound);
		}

		// Token: 0x060028A5 RID: 10405 RVA: 0x000A2124 File Offset: 0x000A0324
		protected static T GetValueFromFirstMatchingRow<T>(MailboxSession session, SortBy[] sortOrder, FolderTreeData.RowMatchesFilterDelegate rowMatchesFilter, PropertyDefinition[] filterProperties, PropertyDefinition propertyToRetrieve, out bool noRowsFound)
		{
			noRowsFound = true;
			List<PropertyDefinition> list = new List<PropertyDefinition>();
			list.AddRange(filterProperties);
			list.Add(propertyToRetrieve);
			using (Folder folder = Folder.Bind(session, DefaultFolderType.CommonViews))
			{
				using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.Associated, null, sortOrder, list))
				{
					IStorePropertyBag[] propertyBags = queryResult.GetPropertyBags(10000);
					foreach (IStorePropertyBag storePropertyBag in propertyBags)
					{
						if (rowMatchesFilter(storePropertyBag))
						{
							noRowsFound = false;
							return storePropertyBag.GetValueOrDefault<T>(propertyToRetrieve, default(T));
						}
					}
				}
			}
			return default(T);
		}

		// Token: 0x060028A6 RID: 10406 RVA: 0x000A21F0 File Offset: 0x000A03F0
		protected static Guid GetSafeGuidFromByteArray(byte[] guid)
		{
			if (guid != null && guid.Length == 16)
			{
				return new Guid(guid);
			}
			return Guid.Empty;
		}

		// Token: 0x060028A7 RID: 10407 RVA: 0x000A2208 File Offset: 0x000A0408
		private static int GenerateNewOutlookTagId()
		{
			return FolderTreeData.outlookTagIdRandom.Next();
		}

		// Token: 0x060028A8 RID: 10408 RVA: 0x000A2214 File Offset: 0x000A0414
		public FolderTreeData(ICoreItem coreItem) : base(coreItem, false)
		{
		}

		// Token: 0x17000D5E RID: 3422
		// (get) Token: 0x060028A9 RID: 10409 RVA: 0x000A221E File Offset: 0x000A041E
		// (set) Token: 0x060028AA RID: 10410 RVA: 0x000A2236 File Offset: 0x000A0436
		public byte[] NodeOrdinal
		{
			get
			{
				this.CheckDisposed("NodeOrdinal::get");
				return base.GetValueOrDefault<byte[]>(FolderTreeDataSchema.Ordinal);
			}
			private set
			{
				this.CheckDisposed("NodeOrdinal::set");
				this[FolderTreeDataSchema.Ordinal] = value;
			}
		}

		// Token: 0x17000D5F RID: 3423
		// (get) Token: 0x060028AB RID: 10411 RVA: 0x000A224F File Offset: 0x000A044F
		// (set) Token: 0x060028AC RID: 10412 RVA: 0x000A2267 File Offset: 0x000A0467
		public int OutlookTagId
		{
			get
			{
				this.CheckDisposed("NodeOrdinal::get");
				return base.GetValueOrDefault<int>(FolderTreeDataSchema.OutlookTagId);
			}
			private set
			{
				this.CheckDisposed("NodeOrdinal::set");
				this[FolderTreeDataSchema.OutlookTagId] = value;
			}
		}

		// Token: 0x17000D60 RID: 3424
		// (get) Token: 0x060028AD RID: 10413 RVA: 0x000A2285 File Offset: 0x000A0485
		// (set) Token: 0x060028AE RID: 10414 RVA: 0x000A229E File Offset: 0x000A049E
		public FolderTreeDataType FolderTreeDataType
		{
			get
			{
				this.CheckDisposed("FolderTreeDataType::get");
				return base.GetValueOrDefault<FolderTreeDataType>(FolderTreeDataSchema.Type, FolderTreeDataType.Undefined);
			}
			protected set
			{
				this.CheckDisposed("FolderTreeDataType::set");
				this[FolderTreeDataSchema.Type] = value;
			}
		}

		// Token: 0x17000D61 RID: 3425
		// (get) Token: 0x060028AF RID: 10415 RVA: 0x000A22BC File Offset: 0x000A04BC
		// (set) Token: 0x060028B0 RID: 10416 RVA: 0x000A22D5 File Offset: 0x000A04D5
		public FolderTreeDataFlags FolderTreeDataFlags
		{
			get
			{
				this.CheckDisposed("FolderTreeDataFlags::get");
				return base.GetValueOrDefault<FolderTreeDataFlags>(FolderTreeDataSchema.FolderTreeDataFlags, FolderTreeDataFlags.None);
			}
			protected set
			{
				this.CheckDisposed("FolderTreeDataFlags::set");
				this[FolderTreeDataSchema.FolderTreeDataFlags] = value;
			}
		}

		// Token: 0x17000D62 RID: 3426
		// (get) Token: 0x060028B1 RID: 10417 RVA: 0x000A22F3 File Offset: 0x000A04F3
		// (set) Token: 0x060028B2 RID: 10418 RVA: 0x000A22FB File Offset: 0x000A04FB
		public MailboxSession MailboxSession { get; protected set; }

		// Token: 0x060028B3 RID: 10419 RVA: 0x000A2304 File Offset: 0x000A0504
		public virtual void SetNodeOrdinal(byte[] nodeBefore, byte[] nodeAfter)
		{
			this.CheckDisposed("SetNodeOrdinal");
			if (base.IsNew)
			{
				throw new NotSupportedException("SetNodeOrdinal cannot be used for new NavigationNodes.");
			}
			this.SetNodeOrdinalInternal(nodeBefore, nodeAfter);
		}

		// Token: 0x060028B4 RID: 10420 RVA: 0x000A232C File Offset: 0x000A052C
		protected void SetNodeOrdinalInternal(byte[] nodeBefore, byte[] nodeAfter)
		{
			this.NodeOrdinal = FolderTreeData.BinaryOrdinalGenerator.GetInbetweenOrdinalValue(nodeBefore, nodeAfter);
		}

		// Token: 0x060028B5 RID: 10421 RVA: 0x000A233B File Offset: 0x000A053B
		protected override void OnBeforeSave()
		{
			base.OnBeforeSave();
			this.OutlookTagId = FolderTreeData.GenerateNewOutlookTagId();
		}

		// Token: 0x040017CF RID: 6095
		protected static readonly Guid MyFoldersClassId = NavigationNodeParentGroup.MyFoldersClassId.AsGuid;

		// Token: 0x040017D0 RID: 6096
		protected static readonly Guid OtherFoldersClassId = NavigationNodeParentGroup.OtherFoldersClassId.AsGuid;

		// Token: 0x040017D1 RID: 6097
		protected static readonly Guid PeoplesFoldersClassId = NavigationNodeParentGroup.PeoplesFoldersClassId.AsGuid;

		// Token: 0x040017D2 RID: 6098
		private static readonly Random outlookTagIdRandom = new Random((int)ExDateTime.UtcNow.UtcTicks);

		// Token: 0x02000398 RID: 920
		// (Invoke) Token: 0x060028B8 RID: 10424
		protected delegate bool RowMatchesFilterDelegate(IStorePropertyBag row);

		// Token: 0x02000399 RID: 921
		private static class BinaryOrdinalGenerator
		{
			// Token: 0x060028BB RID: 10427 RVA: 0x000A23A4 File Offset: 0x000A05A4
			public static byte[] GetInbetweenOrdinalValue(byte[] beforeOrdinal, byte[] afterOrdinal)
			{
				int num = 0;
				int num2 = Math.Max((beforeOrdinal != null) ? beforeOrdinal.Length : 0, (afterOrdinal != null) ? afterOrdinal.Length : 0) + 1;
				byte[] array = new byte[num2];
				if (beforeOrdinal != null && afterOrdinal != null && ArrayComparer<byte>.Comparer.Compare(beforeOrdinal, afterOrdinal) >= 0)
				{
					throw new Exception("Previous ordinal value is greater then after ordinal value");
				}
				if (beforeOrdinal != null && FolderTreeData.BinaryOrdinalGenerator.CheckAllZero(beforeOrdinal))
				{
					beforeOrdinal = null;
				}
				if (afterOrdinal != null && FolderTreeData.BinaryOrdinalGenerator.CheckAllZero(afterOrdinal))
				{
					afterOrdinal = null;
				}
				byte beforeVal;
				byte afterVal;
				for (;;)
				{
					beforeVal = FolderTreeData.BinaryOrdinalGenerator.GetBeforeVal(num, beforeOrdinal);
					afterVal = FolderTreeData.BinaryOrdinalGenerator.GetAfterVal(num, afterOrdinal);
					if (afterVal > beforeVal + 1)
					{
						break;
					}
					array[num++] = beforeVal;
					if (beforeVal + 1 == afterVal)
					{
						afterOrdinal = null;
					}
				}
				array[num++] = (afterVal + beforeVal) / 2;
				byte[] array2 = new byte[num];
				Array.Copy(array, array2, num);
				return array2;
			}

			// Token: 0x060028BC RID: 10428 RVA: 0x000A2460 File Offset: 0x000A0660
			private static bool CheckAllZero(byte[] bytes)
			{
				if (bytes == null)
				{
					throw new ArgumentNullException("bytes");
				}
				foreach (byte b in bytes)
				{
					if (b != 0)
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x060028BD RID: 10429 RVA: 0x000A2499 File Offset: 0x000A0699
			private static byte GetValEx(int index, byte newValue, byte[] ordinal)
			{
				if (index >= ordinal.Length)
				{
					return newValue;
				}
				return ordinal[index];
			}

			// Token: 0x060028BE RID: 10430 RVA: 0x000A24A6 File Offset: 0x000A06A6
			private static byte GetBeforeVal(int index, byte[] beforeOrdinal)
			{
				if (beforeOrdinal == null)
				{
					return 0;
				}
				return FolderTreeData.BinaryOrdinalGenerator.GetValEx(index, 0, beforeOrdinal);
			}

			// Token: 0x060028BF RID: 10431 RVA: 0x000A24B5 File Offset: 0x000A06B5
			private static byte GetAfterVal(int index, byte[] afterOrdinal)
			{
				if (afterOrdinal == null)
				{
					return byte.MaxValue;
				}
				return FolderTreeData.BinaryOrdinalGenerator.GetValEx(index, byte.MaxValue, afterOrdinal);
			}

			// Token: 0x040017D4 RID: 6100
			private const byte MinValue = 0;

			// Token: 0x040017D5 RID: 6101
			private const byte MaxValue = 255;
		}
	}
}
