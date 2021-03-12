using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E43 RID: 3651
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SyncStateTypeFactory
	{
		// Token: 0x06007EA2 RID: 32418 RVA: 0x0022C96C File Offset: 0x0022AB6C
		private SyncStateTypeFactory()
		{
			this.tables = new List<ICustomSerializableBuilder>[]
			{
				this.internalBuilderTable,
				this.externalBuilderTable
			};
		}

		// Token: 0x170021D5 RID: 8661
		// (get) Token: 0x06007EA3 RID: 32419 RVA: 0x0022C9C0 File Offset: 0x0022ABC0
		internal static string ExternalSignature
		{
			get
			{
				return "External";
			}
		}

		// Token: 0x170021D6 RID: 8662
		// (get) Token: 0x06007EA4 RID: 32420 RVA: 0x0022C9C7 File Offset: 0x0022ABC7
		internal static string InternalSignature
		{
			get
			{
				return "{A54F86CF-51AE-457b-90F3-1FCD0683C433}";
			}
		}

		// Token: 0x170021D7 RID: 8663
		// (get) Token: 0x06007EA5 RID: 32421 RVA: 0x0022C9CE File Offset: 0x0022ABCE
		// (set) Token: 0x06007EA6 RID: 32422 RVA: 0x0022C9D5 File Offset: 0x0022ABD5
		internal static int ExternalVersion
		{
			get
			{
				return SyncStateTypeFactory.externalVersion;
			}
			set
			{
				SyncStateTypeFactory.externalVersion = value;
			}
		}

		// Token: 0x170021D8 RID: 8664
		// (get) Token: 0x06007EA7 RID: 32423 RVA: 0x0022C9DD File Offset: 0x0022ABDD
		internal static int InternalVersion
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x06007EA8 RID: 32424 RVA: 0x0022C9E0 File Offset: 0x0022ABE0
		public static bool DoesTypeExistWithThisId(ushort typeId)
		{
			List<ICustomSerializableBuilder> builderTableFromTypeId = SyncStateTypeFactory.GetBuilderTableFromTypeId(typeId);
			ushort num = SyncStateTypeFactory.TypeIndexFromTypeId(typeId);
			return (int)num < builderTableFromTypeId.Count && builderTableFromTypeId[(int)num].TypeId == typeId;
		}

		// Token: 0x06007EA9 RID: 32425 RVA: 0x0022CA18 File Offset: 0x0022AC18
		public static SyncStateTypeFactory GetInstance()
		{
			return SyncStateTypeFactory.typeFactory;
		}

		// Token: 0x06007EAA RID: 32426 RVA: 0x0022CA1F File Offset: 0x0022AC1F
		public static bool IsTypeRegistered(ICustomSerializableBuilder typeBuilder)
		{
			return SyncStateTypeFactory.DoesTypeExistWithThisId(typeBuilder.TypeId);
		}

		// Token: 0x06007EAB RID: 32427 RVA: 0x0022CA2C File Offset: 0x0022AC2C
		public ICustomSerializable BuildObject(ushort typeId)
		{
			if (!SyncStateTypeFactory.DoesTypeExistWithThisId(typeId))
			{
				throw new CustomSerializationException(ServerStrings.ExSyncStateCorrupted("typeId " + typeId));
			}
			return SyncStateTypeFactory.GetBuilderTableFromTypeId(typeId)[(int)SyncStateTypeFactory.TypeIndexFromTypeId(typeId)].BuildObject();
		}

		// Token: 0x06007EAC RID: 32428 RVA: 0x0022CA67 File Offset: 0x0022AC67
		public void RegisterBuilder(ICustomSerializableBuilder typeBuilder)
		{
			this.RegisterInternalBuilders();
			this.InternalRegisterBuilder(typeBuilder, 32768);
		}

		// Token: 0x06007EAD RID: 32429 RVA: 0x0022CA7C File Offset: 0x0022AC7C
		public void RegisterInternalBuilders()
		{
			if (this.initialized)
			{
				return;
			}
			lock (this.thisLock)
			{
				if (!this.initialized)
				{
					SyncStateTypeFactory.GetInstance().InternalRegisterBuilder(new NullableData<Int32Data, int>(), 0);
					SyncStateTypeFactory.GetInstance().InternalRegisterBuilder(new GenericDictionaryData<StringData, string, BooleanData, bool>(), 0);
					SyncStateTypeFactory.GetInstance().InternalRegisterBuilder(new GenericDictionaryData<DerivedData<ISyncItemId>, ISyncItemId, FolderSync.ClientStateInformation>(), 0);
					SyncStateTypeFactory.GetInstance().InternalRegisterBuilder(new GenericDictionaryData<DerivedData<ISyncItemId>, ISyncItemId, ServerManifestEntry>(), 0);
					SyncStateTypeFactory.GetInstance().InternalRegisterBuilder(new GenericDictionaryData<DerivedData<ISyncItemId>, ISyncItemId, ClientManifestEntry>(), 0);
					SyncStateTypeFactory.GetInstance().InternalRegisterBuilder(new MailboxSyncItemId(), 0);
					SyncStateTypeFactory.GetInstance().InternalRegisterBuilder(new MailboxSyncWatermark(), 0);
					SyncStateTypeFactory.GetInstance().InternalRegisterBuilder(new GenericDictionaryData<StoreObjectIdData, StoreObjectId, FolderStateEntry>(), 0);
					SyncStateTypeFactory.GetInstance().InternalRegisterBuilder(new GenericDictionaryData<StoreObjectIdData, StoreObjectId, FolderManifestEntry>(), 0);
					SyncStateTypeFactory.GetInstance().InternalRegisterBuilder(new StringData(), 0);
					SyncStateTypeFactory.GetInstance().InternalRegisterBuilder(new DateTimeData(), 0);
					SyncStateTypeFactory.GetInstance().InternalRegisterBuilder(new ByteData(), 0);
					SyncStateTypeFactory.GetInstance().InternalRegisterBuilder(new BooleanData(), 0);
					SyncStateTypeFactory.GetInstance().InternalRegisterBuilder(new Int32Data(), 0);
					SyncStateTypeFactory.GetInstance().InternalRegisterBuilder(new UInt32Data(), 0);
					SyncStateTypeFactory.GetInstance().InternalRegisterBuilder(new Int64Data(), 0);
					SyncStateTypeFactory.GetInstance().InternalRegisterBuilder(new StoreObjectIdData(), 0);
					SyncStateTypeFactory.GetInstance().InternalRegisterBuilder(new ByteArrayData(), 0);
					SyncStateTypeFactory.GetInstance().InternalRegisterBuilder(new ConstStringData(), 0);
					SyncStateTypeFactory.GetInstance().InternalRegisterBuilder(new NullableDateTimeData(), 0);
					SyncStateTypeFactory.GetInstance().InternalRegisterBuilder(new ConversationIdData(), 0);
					SyncStateTypeFactory.GetInstance().InternalRegisterBuilder(new ADObjectIdData(), 0);
					this.initialized = true;
				}
			}
		}

		// Token: 0x06007EAE RID: 32430 RVA: 0x0022CC40 File Offset: 0x0022AE40
		private static List<ICustomSerializableBuilder> GetBuilderTableFromTypeId(ushort typeId)
		{
			if (!SyncStateTypeFactory.IsExternalType(typeId))
			{
				return SyncStateTypeFactory.GetInstance().internalBuilderTable;
			}
			return SyncStateTypeFactory.GetInstance().externalBuilderTable;
		}

		// Token: 0x06007EAF RID: 32431 RVA: 0x0022CC5F File Offset: 0x0022AE5F
		private static bool IsExternalType(ushort typeId)
		{
			return (typeId & 32768) != 0;
		}

		// Token: 0x06007EB0 RID: 32432 RVA: 0x0022CC6E File Offset: 0x0022AE6E
		private static ushort TypeIndexFromTypeId(ushort typeIndex)
		{
			return (ushort)(((int)typeIndex & -32769) - 1);
		}

		// Token: 0x06007EB1 RID: 32433 RVA: 0x0022CC7C File Offset: 0x0022AE7C
		private bool IsTypeRegistered(Type type)
		{
			foreach (List<ICustomSerializableBuilder> list in this.tables)
			{
				foreach (ICustomSerializableBuilder customSerializableBuilder in list)
				{
					if (customSerializableBuilder.GetType() == type)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06007EB2 RID: 32434 RVA: 0x0022CCF8 File Offset: 0x0022AEF8
		private void InternalRegisterBuilder(ICustomSerializableBuilder typeBuilder, ushort typeMask)
		{
			List<ICustomSerializableBuilder> builderTableFromTypeId = SyncStateTypeFactory.GetBuilderTableFromTypeId(typeMask);
			builderTableFromTypeId.Add(typeBuilder);
			typeBuilder.TypeId = (ushort)(builderTableFromTypeId.Count | (int)typeMask);
		}

		// Token: 0x04005609 RID: 22025
		private const string ExternalSignatureValue = "External";

		// Token: 0x0400560A RID: 22026
		private const ushort ExternalTypeMask = 32768;

		// Token: 0x0400560B RID: 22027
		private const string InternalSignatureValue = "{A54F86CF-51AE-457b-90F3-1FCD0683C433}";

		// Token: 0x0400560C RID: 22028
		private const int InternalVersionValue = 3;

		// Token: 0x0400560D RID: 22029
		private const ushort InternalTypeMask = 0;

		// Token: 0x0400560E RID: 22030
		private readonly object thisLock = new object();

		// Token: 0x0400560F RID: 22031
		private static int externalVersion = 1;

		// Token: 0x04005610 RID: 22032
		private static SyncStateTypeFactory typeFactory = new SyncStateTypeFactory();

		// Token: 0x04005611 RID: 22033
		private List<ICustomSerializableBuilder> externalBuilderTable = new List<ICustomSerializableBuilder>();

		// Token: 0x04005612 RID: 22034
		private List<ICustomSerializableBuilder> internalBuilderTable = new List<ICustomSerializableBuilder>();

		// Token: 0x04005613 RID: 22035
		private List<ICustomSerializableBuilder>[] tables;

		// Token: 0x04005614 RID: 22036
		private bool initialized;
	}
}
