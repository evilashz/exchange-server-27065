using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Exchange.CtsResources;
using Microsoft.Exchange.Data.ContentTypes.Tnef;

namespace Microsoft.Exchange.Data.MsgStorage.Internal
{
	// Token: 0x020000BA RID: 186
	internal class NamedPropertyList
	{
		// Token: 0x06000617 RID: 1559 RVA: 0x0001B14F File Offset: 0x0001934F
		public NamedPropertyList()
		{
			this.Reset();
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x0001B168 File Offset: 0x00019368
		public static NamedPropertyList ReadNamedPropertyList(ComStorage messageStorage)
		{
			ComStorage comStorage = null;
			NamedPropertyList.NamedPropertyEntry[] array = null;
			byte[] array2 = null;
			List<Guid> list = null;
			NamedPropertyList namedPropertyList = new NamedPropertyList();
			NamedPropertyList result;
			try
			{
				try
				{
					comStorage = messageStorage.OpenStorage("__nameid_version1.0", ComStorage.OpenMode.Read);
					array = NamedPropertyList.ReadEntryList(comStorage);
				}
				catch (MsgStorageNotFoundException)
				{
					namedPropertyList.canModify = false;
					return namedPropertyList;
				}
				list = new List<Guid>(32);
				list.AddRange(NamedPropertyList.StandardGuids);
				try
				{
					NamedPropertyList.ReadGuidList(comStorage, list);
				}
				catch (MsgStorageNotFoundException)
				{
				}
				try
				{
					array2 = comStorage.ReadFromStreamMaxLength("__substg1.0_00040102", 262144);
				}
				catch (MsgStorageNotFoundException)
				{
					array2 = Util.EmptyByteArray;
				}
				foreach (NamedPropertyList.NamedPropertyEntry namedPropertyEntry in array)
				{
					ushort guidIndex = namedPropertyEntry.GuidIndex;
					if ((int)guidIndex > list.Count)
					{
						throw new MsgStorageException(MsgStorageErrorCode.CorruptNamedPropertyData, MsgStorageStrings.CorruptData);
					}
					Guid propertySetGuid = list[(int)guidIndex];
					TnefPropertyId tnefPropertyId = namedPropertyEntry.TnefPropertyId;
					TnefNameId namedProperty;
					if (namedPropertyEntry.IsString)
					{
						int num = namedPropertyEntry.NamedId;
						if (num + 4 > array2.Length)
						{
							throw new MsgStorageException(MsgStorageErrorCode.CorruptNamedPropertyData, MsgStorageStrings.CorruptData);
						}
						int num2 = BitConverter.ToInt32(array2, num);
						num += 4;
						if (num + num2 < num || num + num2 > array2.Length)
						{
							throw new MsgStorageException(MsgStorageErrorCode.CorruptNamedPropertyData, MsgStorageStrings.CorruptData);
						}
						string @string = Util.UnicodeEncoding.GetString(array2, num, num2);
						namedProperty = new TnefNameId(propertySetGuid, @string);
					}
					else
					{
						namedProperty = new TnefNameId(propertySetGuid, namedPropertyEntry.NamedId);
					}
					if (!namedPropertyList.TryAdd(tnefPropertyId, namedProperty))
					{
						throw new MsgStorageException(MsgStorageErrorCode.CorruptNamedPropertyData, MsgStorageStrings.CorruptData);
					}
				}
				namedPropertyList.canModify = false;
				result = namedPropertyList;
			}
			finally
			{
				if (comStorage != null)
				{
					comStorage.Dispose();
				}
			}
			return result;
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x0001B368 File Offset: 0x00019568
		public TnefPropertyId Add(TnefNameId namedProperty)
		{
			if (!this.canModify)
			{
				throw new InvalidOperationException("Cannot modify the property list");
			}
			TnefNameIdWrapper tnefNameIdWrapper = new TnefNameIdWrapper(namedProperty);
			TnefPropertyId tnefPropertyId;
			if (!this.nameToId.TryGetValue(tnefNameIdWrapper, out tnefPropertyId))
			{
				tnefPropertyId = (TnefPropertyId)(this.lastPropertyId++);
				this.idToName.Add(tnefPropertyId, tnefNameIdWrapper);
				this.nameToId.Add(tnefNameIdWrapper, tnefPropertyId);
			}
			return tnefPropertyId;
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x0001B3D0 File Offset: 0x000195D0
		public bool TryGetValue(TnefPropertyId propertyId, out TnefNameId namedId)
		{
			TnefNameIdWrapper tnefNameIdWrapper = null;
			if (!this.idToName.TryGetValue(propertyId, out tnefNameIdWrapper))
			{
				namedId = default(TnefNameId);
				return false;
			}
			namedId = tnefNameIdWrapper.TnefNameId;
			return true;
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x0001B408 File Offset: 0x00019608
		public void WriteTo(ComStorage messageStorage)
		{
			using (ComStorage comStorage = messageStorage.CreateStorage("__nameid_version1.0", ComStorage.OpenMode.CreateWrite))
			{
				List<Guid> list = new List<Guid>(32);
				list.AddRange(NamedPropertyList.StandardGuids);
				Dictionary<Guid, int> dictionary = new Dictionary<Guid, int>(32);
				int num = 0;
				foreach (Guid key in list)
				{
					dictionary[key] = num++;
				}
				using (BinaryWriter binaryWriter = new BinaryWriter(new BufferedStream(comStorage.CreateStream("__substg1.0_00030102", ComStorage.OpenMode.CreateWrite), 8192)))
				{
					using (BinaryWriter binaryWriter2 = new BinaryWriter(new BufferedStream(comStorage.CreateStream("__substg1.0_00040102", ComStorage.OpenMode.CreateWrite), 8192), new UnicodeEncoding(false, false)))
					{
						foreach (TnefPropertyId tnefPropertyId in this.idToName.Keys)
						{
							bool isString = false;
							TnefNameIdWrapper tnefNameIdWrapper = this.idToName[tnefPropertyId];
							int count;
							if (!dictionary.TryGetValue(tnefNameIdWrapper.PropertySetGuid, out count))
							{
								count = list.Count;
								list.Add(tnefNameIdWrapper.PropertySetGuid);
								dictionary.Add(tnefNameIdWrapper.PropertySetGuid, count);
							}
							int namedId;
							if (tnefNameIdWrapper.Kind == TnefNameIdKind.Id)
							{
								namedId = tnefNameIdWrapper.Id;
							}
							else
							{
								namedId = NamedPropertyList.WriteName(binaryWriter2, tnefNameIdWrapper.Name);
								isString = true;
							}
							NamedPropertyList.NamedPropertyEntry namedPropertyEntry = new NamedPropertyList.NamedPropertyEntry(tnefPropertyId, isString, (ushort)count, namedId);
							namedPropertyEntry.WriteEntry(binaryWriter);
						}
					}
				}
				using (Stream stream = new BufferedStream(comStorage.CreateStream("__substg1.0_00020102", ComStorage.OpenMode.CreateWrite), 8192))
				{
					for (int i = 3; i < list.Count; i++)
					{
						byte[] array = list[i].ToByteArray();
						stream.Write(array, 0, array.Length);
					}
				}
				comStorage.Flush();
			}
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x0001B6B0 File Offset: 0x000198B0
		private static int WriteName(BinaryWriter writer, string name)
		{
			int num = (int)writer.BaseStream.Position;
			if ((num & 3) != 0)
			{
				int num2 = 4 - (num & 3);
				writer.Write(NamedPropertyList.PaddingBytes, 0, num2);
				num += num2;
			}
			writer.Write((uint)(name.Length * 2));
			writer.Write(name.ToCharArray());
			return num;
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x0001B704 File Offset: 0x00019904
		private static void ReadGuidList(ComStorage namedPropertiesStorage, List<Guid> guids)
		{
			Stream stream = new BufferedStream(namedPropertiesStorage.OpenStream("__substg1.0_00020102", ComStorage.OpenMode.Read), 8192);
			byte[] array = new byte[16];
			for (;;)
			{
				int num = stream.Read(array, 0, 16);
				if (num < 16)
				{
					break;
				}
				guids.Add(new Guid(array));
				if (guids.Count > 8192)
				{
					goto Block_2;
				}
			}
			return;
			Block_2:
			throw new MsgStorageException(MsgStorageErrorCode.NamedPropertiesListTooLong, MsgStorageStrings.LargeNamedPropertyList);
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x0001B768 File Offset: 0x00019968
		private static NamedPropertyList.NamedPropertyEntry[] ReadEntryList(ComStorage namedPropertiesStorage)
		{
			byte[] array = namedPropertiesStorage.ReadFromStreamMaxLength("__substg1.0_00030102", 262144);
			int num = 0;
			int num2 = array.Length / 8;
			NamedPropertyList.NamedPropertyEntry[] array2 = new NamedPropertyList.NamedPropertyEntry[num2];
			for (int num3 = 0; num3 != num2; num3++)
			{
				array2[num3] = NamedPropertyList.NamedPropertyEntry.ReadEntry(array, ref num);
			}
			return array2;
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x0001B7BB File Offset: 0x000199BB
		private void Reset()
		{
			this.nameToId = new Dictionary<TnefNameIdWrapper, TnefPropertyId>();
			this.idToName = new SortedDictionary<TnefPropertyId, TnefNameIdWrapper>();
			this.canModify = true;
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x0001B7DC File Offset: 0x000199DC
		private bool TryAdd(TnefPropertyId id, TnefNameId namedProperty)
		{
			TnefNameIdWrapper tnefNameIdWrapper;
			if (this.idToName.TryGetValue(id, out tnefNameIdWrapper))
			{
				return false;
			}
			tnefNameIdWrapper = new TnefNameIdWrapper(namedProperty);
			this.idToName[id] = tnefNameIdWrapper;
			this.nameToId[tnefNameIdWrapper] = id;
			return true;
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x0001B820 File Offset: 0x00019A20
		// Note: this type is marked as 'beforefieldinit'.
		static NamedPropertyList()
		{
			byte[] paddingBytes = new byte[3];
			NamedPropertyList.PaddingBytes = paddingBytes;
			NamedPropertyList.StandardGuids = new Guid[]
			{
				default(Guid),
				new Guid("{00020328-0000-0000-C000-000000000046}"),
				new Guid("{00020329-0000-0000-C000-000000000046}")
			};
		}

		// Token: 0x040005BD RID: 1469
		private const int EntriesStreamLengthLimit = 262144;

		// Token: 0x040005BE RID: 1470
		private const int GuidNumberLimit = 8192;

		// Token: 0x040005BF RID: 1471
		private const int StringsStreamLengthLimit = 262144;

		// Token: 0x040005C0 RID: 1472
		private const int BufferSize = 8192;

		// Token: 0x040005C1 RID: 1473
		private static readonly byte[] PaddingBytes;

		// Token: 0x040005C2 RID: 1474
		private static readonly Guid[] StandardGuids;

		// Token: 0x040005C3 RID: 1475
		private Dictionary<TnefNameIdWrapper, TnefPropertyId> nameToId;

		// Token: 0x040005C4 RID: 1476
		private SortedDictionary<TnefPropertyId, TnefNameIdWrapper> idToName;

		// Token: 0x040005C5 RID: 1477
		private uint lastPropertyId = 32768U;

		// Token: 0x040005C6 RID: 1478
		private bool canModify;

		// Token: 0x020000BB RID: 187
		private struct NamedPropertyEntry
		{
			// Token: 0x06000622 RID: 1570 RVA: 0x0001B888 File Offset: 0x00019A88
			public NamedPropertyEntry(TnefPropertyId tnefPropertyId, bool isString, ushort guidIndex, int namedId)
			{
				uint num = (uint)(tnefPropertyId - (TnefPropertyId)32768);
				this.info = (num << 16 | (uint)((uint)guidIndex << 1) | (isString ? 1U : 0U));
				this.namedId = namedId;
			}

			// Token: 0x17000160 RID: 352
			// (get) Token: 0x06000623 RID: 1571 RVA: 0x0001B8BB File Offset: 0x00019ABB
			public bool IsString
			{
				get
				{
					return (this.info & 1U) != 0U;
				}
			}

			// Token: 0x17000161 RID: 353
			// (get) Token: 0x06000624 RID: 1572 RVA: 0x0001B8CB File Offset: 0x00019ACB
			public ushort GuidIndex
			{
				get
				{
					return (ushort)(this.info >> 1 & 32767U);
				}
			}

			// Token: 0x17000162 RID: 354
			// (get) Token: 0x06000625 RID: 1573 RVA: 0x0001B8DC File Offset: 0x00019ADC
			public TnefPropertyId TnefPropertyId
			{
				get
				{
					uint num = this.info >> 16;
					return (TnefPropertyId)(num + 32768U);
				}
			}

			// Token: 0x17000163 RID: 355
			// (get) Token: 0x06000626 RID: 1574 RVA: 0x0001B8FB File Offset: 0x00019AFB
			public int NamedId
			{
				get
				{
					return this.namedId;
				}
			}

			// Token: 0x06000627 RID: 1575 RVA: 0x0001B904 File Offset: 0x00019B04
			public static NamedPropertyList.NamedPropertyEntry ReadEntry(byte[] byteArray, ref int position)
			{
				NamedPropertyList.NamedPropertyEntry result = default(NamedPropertyList.NamedPropertyEntry);
				result.namedId = BitConverter.ToInt32(byteArray, position);
				position += 4;
				result.info = BitConverter.ToUInt32(byteArray, position);
				position += 4;
				if (((int)result.TnefPropertyId & 32768) == 0)
				{
					throw new MsgStorageException(MsgStorageErrorCode.CorruptNamedPropertyData, MsgStorageStrings.CorruptData);
				}
				return result;
			}

			// Token: 0x06000628 RID: 1576 RVA: 0x0001B960 File Offset: 0x00019B60
			public void WriteEntry(BinaryWriter writer)
			{
				writer.Write(this.namedId);
				writer.Write(this.info);
			}

			// Token: 0x040005C7 RID: 1479
			public const int EntrySize = 8;

			// Token: 0x040005C8 RID: 1480
			internal const uint PropertyIdBase = 32768U;

			// Token: 0x040005C9 RID: 1481
			private int namedId;

			// Token: 0x040005CA RID: 1482
			private uint info;
		}
	}
}
