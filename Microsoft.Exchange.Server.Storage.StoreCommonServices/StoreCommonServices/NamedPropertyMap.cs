using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.StoreCommonServices;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropTags;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x0200006F RID: 111
	public class NamedPropertyMap : LockableMailboxComponent, IStateObject
	{
		// Token: 0x0600044F RID: 1103 RVA: 0x0001BB86 File Offset: 0x00019D86
		public NamedPropertyMap(MailboxState mailboxState)
		{
			this.mailboxLockName = mailboxState;
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x0001BBA8 File Offset: 0x00019DA8
		private NamedPropertyMap(Context context, MailboxState mailboxState, Dictionary<ushort, StoreNamedPropInfo> numberToNameMap)
		{
			this.mailboxLockName = mailboxState;
			using (MailboxComponentOperationFrame mailboxComponentOperationFrame = context.MailboxComponentWriteOperation(this))
			{
				if (!context.IsStateObjectRegistered(this))
				{
					context.RegisterStateObject(this);
				}
				this.numberToNameMap = new Dictionary<ushort, StoreNamedPropInfo>(numberToNameMap.Count);
				this.nameToNumberMap = new Dictionary<StorePropName, ushort>(this.numberToNameMap.Count);
				foreach (KeyValuePair<ushort, StoreNamedPropInfo> keyValuePair in numberToNameMap)
				{
					if (this.nameToNumberMap.ContainsKey(keyValuePair.Value.PropName))
					{
						throw new CorruptDataException((LID)62288U, "Duplicate property name.");
					}
					this.AddPropNameInternal(context, keyValuePair.Value.PropName, keyValuePair.Key);
					this.maxPropNumber = Math.Max(this.maxPropNumber, keyValuePair.Key);
				}
				this.shouldReloadMapping = false;
				mailboxComponentOperationFrame.Success();
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000451 RID: 1105 RVA: 0x0001BCD8 File Offset: 0x00019ED8
		public override MailboxComponentId MailboxComponentId
		{
			get
			{
				return NamedPropertyMap.ComponentId;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000452 RID: 1106 RVA: 0x0001BCDF File Offset: 0x00019EDF
		public override Guid DatabaseGuid
		{
			get
			{
				return this.mailboxLockName.DatabaseGuid;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000453 RID: 1107 RVA: 0x0001BCEC File Offset: 0x00019EEC
		public override int MailboxPartitionNumber
		{
			get
			{
				return this.mailboxLockName.MailboxPartitionNumber;
			}
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x0001BCF9 File Offset: 0x00019EF9
		public static void Initialize()
		{
			if (NamedPropertyMap.namedPropertyMappingCacheDataSlot == -1)
			{
				NamedPropertyMap.namedPropertyMappingCacheDataSlot = MailboxState.AllocateComponentDataSlot(false);
			}
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x0001BD10 File Offset: 0x00019F10
		public static void CreateCacheForNewMailbox(Context context, MailboxState mailboxState, Dictionary<ushort, StoreNamedPropInfo> numberToNameMap)
		{
			NamedPropertyMap value = new NamedPropertyMap(context, mailboxState, numberToNameMap);
			mailboxState.SetComponentData(NamedPropertyMap.namedPropertyMappingCacheDataSlot, value);
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x0001BD34 File Offset: 0x00019F34
		public static NamedPropertyMap GetCacheForMailbox(Context context, MailboxState mailboxState)
		{
			NamedPropertyMap namedPropertyMap = (NamedPropertyMap)mailboxState.GetComponentData(NamedPropertyMap.namedPropertyMappingCacheDataSlot);
			if (namedPropertyMap == null)
			{
				namedPropertyMap = new NamedPropertyMap(mailboxState);
				using (context.MailboxComponentReadOperation(namedPropertyMap))
				{
					namedPropertyMap.FetchMapping(context);
				}
				NamedPropertyMap namedPropertyMap2 = (NamedPropertyMap)mailboxState.CompareExchangeComponentData(NamedPropertyMap.namedPropertyMappingCacheDataSlot, null, namedPropertyMap);
				if (namedPropertyMap2 != null)
				{
					namedPropertyMap = namedPropertyMap2;
				}
			}
			return namedPropertyMap;
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x0001BDA4 File Offset: 0x00019FA4
		public static void DiscardCacheForMailbox(MailboxState mailboxState)
		{
			mailboxState.SetComponentData(NamedPropertyMap.namedPropertyMappingCacheDataSlot, null);
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x0001BDB4 File Offset: 0x00019FB4
		public override bool IsValidTableOperation(Context context, Connection.OperationType operationType, Table table, IList<object> partitionValues)
		{
			if (operationType == Connection.OperationType.CreateTable || operationType == Connection.OperationType.DeleteTable)
			{
				return this.TestExclusiveLock();
			}
			if (table.Equals(DatabaseSchema.ExtendedPropertyNameMappingTable(context.Database).Table))
			{
				if (operationType == Connection.OperationType.Query)
				{
					return this.TestSharedLock() || this.TestExclusiveLock();
				}
				if (operationType == Connection.OperationType.Insert)
				{
					return this.TestExclusiveLock();
				}
			}
			return false;
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x0001BE0C File Offset: 0x0001A00C
		public int GetNamedPropertyCount(Context context)
		{
			int result;
			using (context.MailboxComponentReadOperation(this))
			{
				this.FetchMapping(context);
				int count = this.nameToNumberMap.Count;
				result = count;
			}
			return result;
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x0001BE58 File Offset: 0x0001A058
		public StoreNamedPropInfo RetrieveOrAddNamedProperyToTheGlobalMap(Context context, StorePropName propName)
		{
			StoreNamedPropInfo namedPropInfo;
			if (!GlobalNamedPropertyMap.GetDefinitionFromName(context, propName, out namedPropInfo))
			{
				namedPropInfo = WellKnownProperties.GetNamedPropInfo(propName);
				GlobalNamedPropertyMap.AddPropDefinition(context, ref namedPropInfo);
			}
			return namedPropInfo;
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x0001BE80 File Offset: 0x0001A080
		public bool GetNumberFromName(Context context, StorePropName propName, bool create, QuotaInfo quotaInfo, out ushort propId, out StoreNamedPropInfo propInfo)
		{
			propId = 0;
			propInfo = null;
			using (context.MailboxComponentReadOperation(this))
			{
				this.FetchMapping(context);
				if (this.nameToNumberMap.TryGetValue(propName, out propId))
				{
					if (!this.numberToNameMap.TryGetValue(propId, out propInfo))
					{
						Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(false, "nameToNumber and numberToName maps are not in sync!");
					}
					return true;
				}
			}
			if (create)
			{
				using (MailboxComponentOperationFrame mailboxComponentOperationFrame2 = context.MailboxComponentWriteOperation(this))
				{
					this.FetchMapping(context);
					if (this.nameToNumberMap.TryGetValue(propName, out propId))
					{
						if (!this.numberToNameMap.TryGetValue(propId, out propInfo))
						{
							Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(false, "nameToNumber and numberToName maps are not in sync!");
						}
						return true;
					}
					if (!context.IsStateObjectRegistered(this))
					{
						context.RegisterStateObject(this);
					}
					propId = this.GetNextPropNumber(context, propName, quotaInfo, new Func<string>(this.mailboxLockName.GetFriendlyNameForLogging));
					this.AddPropNameInternal(context, propName, propId);
					if (!this.numberToNameMap.TryGetValue(propId, out propInfo))
					{
						Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(false, "nameToNumber and numberToName maps are not in sync after adding property!");
					}
					mailboxComponentOperationFrame2.Success();
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x0001BFC0 File Offset: 0x0001A1C0
		public StorePropInfo GetNameFromNumber(Context context, ushort propId)
		{
			using (context.MailboxComponentReadOperation(this))
			{
				this.FetchMapping(context);
				StoreNamedPropInfo result;
				if (this.numberToNameMap.TryGetValue(propId, out result))
				{
					return result;
				}
			}
			return null;
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x0001C014 File Offset: 0x0001A214
		public void ForEachElement(Context context, NamedPropertyMap.Callback callback)
		{
			using (context.MailboxComponentReadOperation(this))
			{
				foreach (KeyValuePair<ushort, StoreNamedPropInfo> propidNamePair in this.numberToNameMap)
				{
					callback(propidNamePair);
				}
			}
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x0001C08C File Offset: 0x0001A28C
		public void Process(Context context, Dictionary<ushort, StoreNamedPropInfo> specifiedNumberToNameMap)
		{
			ushort num = 0;
			uint num2;
			using (context.MailboxComponentReadOperation(this))
			{
				this.ValidateNamedPropertyMappingAgainstSpecifiedNamedPropertyMapping(context, specifiedNumberToNameMap, out num2);
				if (num2 > 0U)
				{
					num = this.maxPropNumber;
				}
			}
			if (num2 == 0U)
			{
				return;
			}
			using (MailboxComponentOperationFrame mailboxComponentOperationFrame2 = context.MailboxComponentWriteOperation(this))
			{
				if (!context.IsStateObjectRegistered(this))
				{
					context.RegisterStateObject(this);
				}
				for (uint num3 = 0U; num3 < num2; num3 += 1U)
				{
					Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(num >= 32768, "Invalid value for next ID.");
					num += 1;
					StoreNamedPropInfo storeNamedPropInfo;
					if (this.numberToNameMap.TryGetValue(num, out storeNamedPropInfo))
					{
						uint num4;
						this.ValidateNamedPropertyMappingAgainstSpecifiedNamedPropertyMapping(context, specifiedNumberToNameMap, out num4);
						num2 = num4;
						break;
					}
				}
				int num5 = 0;
				while ((long)num5 < (long)((ulong)num2))
				{
					num = (this.maxPropNumber += 1);
					StoreNamedPropInfo storeNamedPropInfo2;
					if (specifiedNumberToNameMap.TryGetValue(num, out storeNamedPropInfo2))
					{
						this.AddPropNameInternal(context, storeNamedPropInfo2.PropName, num);
					}
					else
					{
						Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(false, "This cannot be happening!");
					}
					num5++;
				}
				mailboxComponentOperationFrame2.Success();
			}
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x0001C1AC File Offset: 0x0001A3AC
		void IStateObject.OnBeforeCommit(Context context)
		{
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x0001C1AE File Offset: 0x0001A3AE
		void IStateObject.OnCommit(Context context)
		{
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x0001C1B0 File Offset: 0x0001A3B0
		void IStateObject.OnAbort(Context context)
		{
			this.ResetInMemoryCache();
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x0001C1B8 File Offset: 0x0001A3B8
		internal void GetInternalsForPropName_TestOnly(Context context, StorePropName propName, out ushort propId, out StorePropName keyPropName, out StoreNamedPropInfo propInfo)
		{
			using (context.MailboxComponentReadOperation(this))
			{
				this.FetchMapping(context);
				propId = this.nameToNumberMap[propName];
				propInfo = this.numberToNameMap[propId];
				keyPropName = null;
				foreach (StorePropName storePropName in this.nameToNumberMap.Keys)
				{
					if (storePropName == propName)
					{
						keyPropName = storePropName;
					}
				}
			}
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x0001C264 File Offset: 0x0001A464
		private void AddPropNameInternal(Context context, StorePropName propName, ushort propId)
		{
			if (ExTraceGlobals.ExtendedPropsTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.ExtendedPropsTracer.TraceDebug(0L, "NamedPropertyMap::Adding mapping for mailbox partition {0} from {1} to {2}:{3}", new object[]
				{
					this.MailboxPartitionNumber,
					propId,
					propName.Guid,
					propName.Name ?? propName.DispId.ToString()
				});
			}
			ExtendedPropertyNameMappingTable extendedPropertyNameMappingTable = DatabaseSchema.ExtendedPropertyNameMappingTable(context.Database);
			using (DataRow dataRow = Factory.CreateDataRow(context.Culture, context, extendedPropertyNameMappingTable.Table, true, new ColumnValue[]
			{
				new ColumnValue(extendedPropertyNameMappingTable.MailboxPartitionNumber, this.MailboxPartitionNumber),
				new ColumnValue(extendedPropertyNameMappingTable.PropNumber, (int)propId),
				new ColumnValue(extendedPropertyNameMappingTable.PropGuid, propName.Guid),
				new ColumnValue(extendedPropertyNameMappingTable.PropName, propName.Name),
				new ColumnValue(extendedPropertyNameMappingTable.PropDispId, (propName.Name != null) ? null : ((int)propName.DispId))
			}))
			{
				dataRow.Flush(context);
			}
			StoreNamedPropInfo storeNamedPropInfo = this.RetrieveOrAddNamedProperyToTheGlobalMap(context, propName);
			this.numberToNameMap.Add(propId, storeNamedPropInfo);
			this.nameToNumberMap.Add(storeNamedPropInfo.PropName, propId);
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x0001C400 File Offset: 0x0001A600
		private void ValidateNamedPropertyMappingAgainstSpecifiedNamedPropertyMapping(Context context, Dictionary<ushort, StoreNamedPropInfo> specifiedNumberToNameMap, out uint newNamedPropertiesCounter)
		{
			newNamedPropertiesCounter = 0U;
			foreach (KeyValuePair<ushort, StoreNamedPropInfo> keyValuePair in specifiedNumberToNameMap)
			{
				bool flag = false;
				bool flag2 = false;
				StoreNamedPropInfo storeNamedPropInfo;
				ushort num;
				if (this.numberToNameMap.TryGetValue(keyValuePair.Key, out storeNamedPropInfo))
				{
					flag2 = true;
					if (!storeNamedPropInfo.PropName.Equals(keyValuePair.Value.PropName))
					{
						flag = true;
					}
				}
				else if (this.nameToNumberMap.TryGetValue(keyValuePair.Value.PropName, out num))
				{
					flag2 = true;
					flag = true;
				}
				if (!flag2)
				{
					newNamedPropertiesCounter += 1U;
				}
				if (flag)
				{
					string friendlyNameForLogging = this.mailboxLockName.GetFriendlyNameForLogging();
					string text = keyValuePair.Value.PropName.ToString();
					string message = string.Format("On database {0} the signature of the mailbox {1} was changed by the new named property: {2}", context.Database.MdbName, friendlyNameForLogging, text);
					if (ExTraceGlobals.MailboxSignatureTracer.IsTraceEnabled(TraceType.ErrorTrace))
					{
						ExTraceGlobals.MailboxSignatureTracer.TraceError(56504L, message);
					}
					Microsoft.Exchange.Server.Storage.Common.Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_NamedPropertyMappingChange, new object[]
					{
						context.Database.MdbName,
						friendlyNameForLogging,
						text
					});
					throw new CannotRegisterNewNamedPropertyMapping((LID)44216U, message);
				}
			}
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x0001C560 File Offset: 0x0001A760
		private ushort GetNextPropNumber(Context context, StorePropName propName, QuotaInfo quotaInfo, Func<string> mailboxLoggingName)
		{
			long num = quotaInfo.NamedPropertiesCountQuota.IsUnlimited ? 32767L : Math.Min(quotaInfo.NamedPropertiesCountQuota.Value, 32767L);
			if (num < (long)(this.nameToNumberMap.Count + 1))
			{
				Microsoft.Exchange.Server.Storage.Common.Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_NamedPropsQuotaError, new object[]
				{
					mailboxLoggingName(),
					this.nameToNumberMap.Count,
					context.ClientType,
					propName.Guid,
					propName.Name
				});
				if (ExTraceGlobals.ExtendedPropsTracer.IsTraceEnabled(TraceType.ErrorTrace))
				{
					ExTraceGlobals.ExtendedPropsTracer.TraceError(0L, "Client {0} failed to register named property GUID: {1} name/id: {2} for mailbox {3} because the number of named properties reached {4}.", new object[]
					{
						context.ClientType,
						propName.Guid,
						propName.Name,
						mailboxLoggingName(),
						quotaInfo.NamedPropertiesCountQuota.Value
					});
				}
				throw new StoreException((LID)34588U, ErrorCodeValue.NamedPropQuotaExceeded);
			}
			return this.maxPropNumber += 1;
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x0001C690 File Offset: 0x0001A890
		private void FetchMapping(Context context)
		{
			if (!this.shouldReloadMapping)
			{
				return;
			}
			Dictionary<StorePropName, ushort> dictionary = new Dictionary<StorePropName, ushort>(1000);
			Dictionary<ushort, StoreNamedPropInfo> dictionary2 = new Dictionary<ushort, StoreNamedPropInfo>(1000);
			if (ExTraceGlobals.ExtendedPropsTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.ExtendedPropsTracer.TraceDebug<int>(0L, "NamedPropertyMap::Fetching name mapping for mailbox partition {0}", this.mailboxLockName.MailboxPartitionNumber);
			}
			uint num = 0U;
			ushort num2 = 0;
			ushort val = 32768;
			ushort val2 = ushort.MaxValue;
			ExtendedPropertyNameMappingTable extendedPropertyNameMappingTable = DatabaseSchema.ExtendedPropertyNameMappingTable(context.Database);
			Column[] columnsToFetch = new Column[]
			{
				extendedPropertyNameMappingTable.PropGuid,
				extendedPropertyNameMappingTable.PropName,
				extendedPropertyNameMappingTable.PropDispId,
				extendedPropertyNameMappingTable.PropNumber
			};
			StartStopKey startStopKey = new StartStopKey(true, new object[]
			{
				this.mailboxLockName.MailboxPartitionNumber
			});
			using (TableOperator tableOperator = Factory.CreateTableOperator(context.Culture, context, extendedPropertyNameMappingTable.Table, extendedPropertyNameMappingTable.Table.PrimaryKeyIndex, columnsToFetch, null, null, 0, 0, new KeyRange(startStopKey, startStopKey), false, true))
			{
				using (Reader reader = tableOperator.ExecuteReader(false))
				{
					while (reader.Read())
					{
						ushort num3 = (ushort)reader.GetInt32(extendedPropertyNameMappingTable.PropNumber);
						if (num3 <= 32768 || num3 > 65535)
						{
							throw new CorruptDataException((LID)48700U, "Invalid named property Id value.");
						}
						Guid guid = reader.GetGuid(extendedPropertyNameMappingTable.PropGuid);
						string @string = reader.GetString(extendedPropertyNameMappingTable.PropName);
						StorePropName storePropName;
						if (@string != null)
						{
							storePropName = new StorePropName(guid, @string);
						}
						else
						{
							uint @int = (uint)reader.GetInt32(extendedPropertyNameMappingTable.PropDispId);
							storePropName = new StorePropName(guid, @int);
						}
						StoreNamedPropInfo storeNamedPropInfo = this.RetrieveOrAddNamedProperyToTheGlobalMap(context, storePropName);
						Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(storePropName.Equals(storeNamedPropInfo.PropName), "FetchMapping: PropName is not the same after going through WellKnownProperties.GetNamedPropInfo");
						if (dictionary2.ContainsKey(num3))
						{
							throw new CorruptDataException((LID)60240U, "Duplicate property ID.");
						}
						if (dictionary.ContainsKey(storeNamedPropInfo.PropName))
						{
							throw new CorruptDataException((LID)35664U, "Duplicate property name.");
						}
						dictionary.Add(storeNamedPropInfo.PropName, num3);
						dictionary2.Add(num3, storeNamedPropInfo);
						val = Math.Max(val, num3);
						val2 = Math.Min(val2, num3);
						num += (uint)(num3 - 32768);
						num2 += 1;
						if (ExTraceGlobals.ExtendedPropsTracer.IsTraceEnabled(TraceType.DebugTrace))
						{
							ExTraceGlobals.ExtendedPropsTracer.TraceDebug<int, ushort, StorePropName>(0L, "NamedPropertyMap::Read mapping for mailbox partition {0} from {1} to {2}", this.mailboxLockName.MailboxPartitionNumber, num3, storeNamedPropInfo.PropName);
						}
					}
				}
			}
			using (LockManager.Lock(this, context.Diagnostics))
			{
				if (this.nameToNumberMap == null)
				{
					this.nameToNumberMap = dictionary;
					this.maxPropNumber = val;
					this.numberToNameMap = dictionary2;
					this.shouldReloadMapping = false;
				}
			}
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x0001C9B8 File Offset: 0x0001ABB8
		private void ResetInMemoryCache()
		{
			this.nameToNumberMap = null;
			this.numberToNameMap = null;
			this.maxPropNumber = 32768;
			this.shouldReloadMapping = true;
		}

		// Token: 0x040003A0 RID: 928
		public const ushort MaxNamedPropertyNumber = 32767;

		// Token: 0x040003A1 RID: 929
		public const ushort NamedPropertyPropBaseId = 32768;

		// Token: 0x040003A2 RID: 930
		private const int AveragePropertyNames = 1000;

		// Token: 0x040003A3 RID: 931
		private static readonly MailboxComponentId ComponentId = MailboxComponentId.NamedPropertyMap;

		// Token: 0x040003A4 RID: 932
		private static int namedPropertyMappingCacheDataSlot = -1;

		// Token: 0x040003A5 RID: 933
		private MailboxLockNameBase mailboxLockName;

		// Token: 0x040003A6 RID: 934
		private Dictionary<ushort, StoreNamedPropInfo> numberToNameMap;

		// Token: 0x040003A7 RID: 935
		private Dictionary<StorePropName, ushort> nameToNumberMap;

		// Token: 0x040003A8 RID: 936
		private ushort maxPropNumber = 32768;

		// Token: 0x040003A9 RID: 937
		private bool shouldReloadMapping = true;

		// Token: 0x02000070 RID: 112
		// (Invoke) Token: 0x0600046A RID: 1130
		public delegate void Callback(KeyValuePair<ushort, StoreNamedPropInfo> propidNamePair);
	}
}
