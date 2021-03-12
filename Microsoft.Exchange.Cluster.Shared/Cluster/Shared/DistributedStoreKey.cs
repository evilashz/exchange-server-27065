using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.DxStore.Common;
using Microsoft.Win32;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x0200009A RID: 154
	public class DistributedStoreKey : IDistributedStoreKey, IDisposable
	{
		// Token: 0x06000583 RID: 1411 RVA: 0x00014DC8 File Offset: 0x00012FC8
		public DistributedStoreKey(string parentKeyName, string subkeyName, DxStoreKeyAccessMode mode, DistributedStore.Context context = null)
		{
			this.InstanceId = Guid.NewGuid();
			if (string.IsNullOrEmpty(parentKeyName))
			{
				parentKeyName = string.Empty;
			}
			if (string.IsNullOrEmpty(subkeyName))
			{
				subkeyName = string.Empty;
			}
			this.FullKeyName = Path.Combine(parentKeyName, subkeyName);
			this.Mode = mode;
			this.Context = context;
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000584 RID: 1412 RVA: 0x00014E2B File Offset: 0x0001302B
		// (set) Token: 0x06000585 RID: 1413 RVA: 0x00014E33 File Offset: 0x00013033
		public string FullKeyName { get; private set; }

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000586 RID: 1414 RVA: 0x00014E3C File Offset: 0x0001303C
		// (set) Token: 0x06000587 RID: 1415 RVA: 0x00014E44 File Offset: 0x00013044
		public DxStoreKeyAccessMode Mode { get; private set; }

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000588 RID: 1416 RVA: 0x00014E4D File Offset: 0x0001304D
		// (set) Token: 0x06000589 RID: 1417 RVA: 0x00014E55 File Offset: 0x00013055
		public DistributedStore.Context Context { get; private set; }

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x0600058A RID: 1418 RVA: 0x00014E5E File Offset: 0x0001305E
		// (set) Token: 0x0600058B RID: 1419 RVA: 0x00014E66 File Offset: 0x00013066
		public bool IsClosed { get; set; }

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x0600058C RID: 1420 RVA: 0x00014E6F File Offset: 0x0001306F
		// (set) Token: 0x0600058D RID: 1421 RVA: 0x00014E77 File Offset: 0x00013077
		internal IDistributedStoreKey PrimaryStoreKey { get; set; }

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x0600058E RID: 1422 RVA: 0x00014E80 File Offset: 0x00013080
		// (set) Token: 0x0600058F RID: 1423 RVA: 0x00014E88 File Offset: 0x00013088
		internal IDistributedStoreKey ShadowStoreKey { get; set; }

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000590 RID: 1424 RVA: 0x00014E91 File Offset: 0x00013091
		// (set) Token: 0x06000591 RID: 1425 RVA: 0x00014E99 File Offset: 0x00013099
		internal Guid InstanceId { get; set; }

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000592 RID: 1426 RVA: 0x00014EA2 File Offset: 0x000130A2
		// (set) Token: 0x06000593 RID: 1427 RVA: 0x00014EAA File Offset: 0x000130AA
		internal bool IsBaseKey { get; set; }

		// Token: 0x06000594 RID: 1428 RVA: 0x00014EB3 File Offset: 0x000130B3
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x00014EC2 File Offset: 0x000130C2
		public IDistributedStoreKey OpenKey(string keyName, DxStoreKeyAccessMode mode, bool isIgnoreIfNotExist, ReadWriteConstraints constraints)
		{
			return this.OpenKeyInternal(keyName, mode, isIgnoreIfNotExist, constraints);
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x00014ED0 File Offset: 0x000130D0
		public void CloseKey()
		{
			lock (this.locker)
			{
				this.CloseKeyInternal();
			}
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x00014F10 File Offset: 0x00013110
		public bool DeleteKey(string keyName, bool isIgnoreIfNotExist, ReadWriteConstraints constraints)
		{
			return this.DeleteKeyInternal(keyName, isIgnoreIfNotExist, constraints);
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x00014F1B File Offset: 0x0001311B
		public bool SetValue(string propertyName, object propertyValue, RegistryValueKind valueKind, bool isBestEffort, ReadWriteConstraints constraints)
		{
			return this.SetValueInternal(propertyName, propertyValue, valueKind, isBestEffort, constraints);
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x00014F2A File Offset: 0x0001312A
		public object GetValue(string propertyName, out bool isValueExist, out RegistryValueKind valueKind, ReadWriteConstraints constraints)
		{
			return this.GetValueInternal(propertyName, out isValueExist, out valueKind, constraints);
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x00014F37 File Offset: 0x00013137
		public bool DeleteValue(string propertyName, bool isIgnoreIfNotExist, ReadWriteConstraints constraints)
		{
			return this.DeleteValueInternal(propertyName, isIgnoreIfNotExist, constraints);
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x00014F42 File Offset: 0x00013142
		public IEnumerable<string> GetSubkeyNames(ReadWriteConstraints constraints)
		{
			return this.GetSubkeyNamesInternal(constraints);
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x00014F4B File Offset: 0x0001314B
		public IEnumerable<string> GetValueNames(ReadWriteConstraints constraints)
		{
			return this.GetValueNamesInternal(constraints);
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x00014F54 File Offset: 0x00013154
		public IEnumerable<Tuple<string, RegistryValueKind>> GetValueInfos(ReadWriteConstraints constraints)
		{
			return this.GetValueInfosInternal(constraints);
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x00014F5D File Offset: 0x0001315D
		public IEnumerable<Tuple<string, object>> GetAllValues(ReadWriteConstraints constraints)
		{
			return this.GetAllValuesInternal(constraints);
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x00014F66 File Offset: 0x00013166
		public IDistributedStoreBatchRequest CreateBatchUpdateRequest()
		{
			return this.CreateBatchUpdateRequestInternal();
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x00014F6E File Offset: 0x0001316E
		public void ExecuteBatchRequest(List<DxStoreBatchCommand> commands, ReadWriteConstraints constraints)
		{
			this.ExecuteBatchRequestInternal(commands, constraints);
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x00014F78 File Offset: 0x00013178
		public IDistributedStoreChangeNotify CreateChangeNotify(ChangeNotificationFlags flags, object context, Action callback)
		{
			throw new NotImplementedException("CreateChangeNotity not implemented yet");
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x00014F84 File Offset: 0x00013184
		protected virtual void Dispose(bool isDisposing)
		{
			lock (this.locker)
			{
				if (!this.isDisposed)
				{
					if (isDisposing)
					{
						this.CloseKey();
					}
					this.isDisposed = true;
				}
			}
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x00014FD8 File Offset: 0x000131D8
		private void ThrowIfKeyIsInvalid(IDistributedStoreKey key)
		{
			if (key == null)
			{
				throw new DxStoreKeyInvalidKeyException(this.FullKeyName);
			}
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x00014FEC File Offset: 0x000131EC
		private IDistributedStoreKey OpenKeyInternal(string subKeyName, DxStoreKeyAccessMode mode, bool isIgnoreIfNotExist, ReadWriteConstraints constraints)
		{
			if (mode == DxStoreKeyAccessMode.CreateIfNotExist)
			{
				IDistributedStoreKey distributedStoreKey = this.OpenKeyFinal(subKeyName, DxStoreKeyAccessMode.Write, true, constraints);
				if (distributedStoreKey != null)
				{
					return distributedStoreKey;
				}
			}
			return this.OpenKeyFinal(subKeyName, mode, isIgnoreIfNotExist, constraints);
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x0001507C File Offset: 0x0001327C
		private IDistributedStoreKey OpenKeyFinal(string subKeyName, DxStoreKeyAccessMode mode, bool isIgnoreIfNotExist, ReadWriteConstraints constraints)
		{
			OperationCategory operationCategory = OperationCategory.OpenKey;
			OperationType operationType = OperationType.Read;
			if (mode == DxStoreKeyAccessMode.CreateIfNotExist)
			{
				operationCategory = OperationCategory.OpenOrCreateKey;
				operationType = OperationType.Write;
			}
			DistributedStoreKey compositeKey = new DistributedStoreKey(this.FullKeyName, subKeyName, mode, this.Context);
			IDistributedStoreKey result;
			try
			{
				result = (DistributedStore.Instance.ExecuteRequest<bool>(this, operationCategory, operationType, string.Format("SubKey: [{0}] Mode: [{1}] IsBestEffort: [{2}] IsConstrained: [{3}]", new object[]
				{
					subKeyName,
					mode,
					isIgnoreIfNotExist,
					constraints != null
				}), delegate(IDistributedStoreKey key, bool isPrimary, StoreKind storeKind)
				{
					this.ThrowIfKeyIsInvalid(key);
					IDistributedStoreKey distributedStoreKey = key.OpenKey(subKeyName, mode, isIgnoreIfNotExist, ReadWriteConstraints.Copy(constraints));
					if (distributedStoreKey != null)
					{
						DistributedStore.Instance.SetKeyByRole(compositeKey, isPrimary, distributedStoreKey);
						return true;
					}
					return false;
				}) ? compositeKey : null);
			}
			finally
			{
				if (compositeKey.PrimaryStoreKey == null)
				{
					compositeKey.CloseKey();
				}
			}
			return result;
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x000151B3 File Offset: 0x000133B3
		private void CloseKeyInternal()
		{
			if (!this.IsClosed)
			{
				this.IsClosed = true;
				DistributedStore.Instance.ExecuteRequest(this, OperationCategory.CloseKey, OperationType.Read, string.Empty, delegate(IDistributedStoreKey key, bool isPrimary, StoreKind storeKind)
				{
					if (key != null)
					{
						key.CloseKey();
					}
				});
			}
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x00015228 File Offset: 0x00013428
		private bool DeleteKeyInternal(string keyName, bool isIgnoreIfNotExist, ReadWriteConstraints constraints)
		{
			return DistributedStore.Instance.ExecuteRequest<bool>(this, OperationCategory.DeleteKey, OperationType.Write, string.Format("SubKey: [{0}] IsBestEffort: [{1}] IsConstrained: [{2}]", keyName, isIgnoreIfNotExist, constraints != null), delegate(IDistributedStoreKey key)
			{
				this.ThrowIfKeyIsInvalid(key);
				return key.DeleteKey(keyName, isIgnoreIfNotExist, ReadWriteConstraints.Copy(constraints));
			});
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x000152D0 File Offset: 0x000134D0
		private bool SetValueInternal(string propertyName, object propertyValue, RegistryValueKind valueKind, bool isBestEffort, ReadWriteConstraints constraints)
		{
			return DistributedStore.Instance.ExecuteRequest<bool>(this, OperationCategory.SetValue, OperationType.Write, string.Format("Property: [{0}] PropertyKind: [{1}] IsBestEffort: [{2}] IsConstrained: [{3}]", new object[]
			{
				propertyName,
				valueKind,
				isBestEffort,
				constraints != null
			}), delegate(IDistributedStoreKey key)
			{
				this.ThrowIfKeyIsInvalid(key);
				return key.SetValue(propertyName, propertyValue, isBestEffort, constraints);
			});
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x000153B4 File Offset: 0x000135B4
		private object GetValueInternal(string propertyName, out bool isValueExist, out RegistryValueKind valueKind, ReadWriteConstraints constraints)
		{
			Tuple<bool, RegistryValueKind, object> tuple = DistributedStore.Instance.ExecuteRequest<Tuple<bool, RegistryValueKind, object>>(this, OperationCategory.GetValue, OperationType.Read, string.Format("Property: [{0}]", propertyName), delegate(IDistributedStoreKey key)
			{
				this.ThrowIfKeyIsInvalid(key);
				bool item = false;
				RegistryValueKind item2 = RegistryValueKind.Unknown;
				object value = key.GetValue(propertyName, out item, out item2, ReadWriteConstraints.Copy(constraints));
				return new Tuple<bool, RegistryValueKind, object>(item, item2, value);
			});
			isValueExist = tuple.Item1;
			valueKind = tuple.Item2;
			return tuple.Item3;
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x00015444 File Offset: 0x00013644
		public IEnumerable<Tuple<string, object>> GetAllValuesInternal(ReadWriteConstraints constraints)
		{
			return DistributedStore.Instance.ExecuteRequest<IEnumerable<Tuple<string, object>>>(this, OperationCategory.GetAllValues, OperationType.Read, string.Empty, delegate(IDistributedStoreKey key)
			{
				this.ThrowIfKeyIsInvalid(key);
				return key.GetAllValues(ReadWriteConstraints.Copy(constraints));
			});
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x000154BC File Offset: 0x000136BC
		private bool DeleteValueInternal(string propertyName, bool isIgnoreIfNotExist, ReadWriteConstraints constraints)
		{
			return DistributedStore.Instance.ExecuteRequest<bool>(this, OperationCategory.DeleteValue, OperationType.Write, string.Format("Property: [{0}] IsBestEffort: [{1}] IsConstrained: [{2}]", propertyName, isIgnoreIfNotExist, constraints != null), delegate(IDistributedStoreKey key)
			{
				this.ThrowIfKeyIsInvalid(key);
				return key.DeleteValue(propertyName, isIgnoreIfNotExist, ReadWriteConstraints.Copy(constraints));
			});
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x00015554 File Offset: 0x00013754
		private IEnumerable<string> GetSubkeyNamesInternal(ReadWriteConstraints constraints)
		{
			return DistributedStore.Instance.ExecuteRequest<IEnumerable<string>>(this, OperationCategory.GetSubKeyNames, OperationType.Read, string.Empty, delegate(IDistributedStoreKey key)
			{
				this.ThrowIfKeyIsInvalid(key);
				return key.GetSubkeyNames(constraints);
			});
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x000155B8 File Offset: 0x000137B8
		private IEnumerable<string> GetValueNamesInternal(ReadWriteConstraints constraints)
		{
			return DistributedStore.Instance.ExecuteRequest<IEnumerable<string>>(this, OperationCategory.GetValueNames, OperationType.Read, string.Empty, delegate(IDistributedStoreKey key)
			{
				this.ThrowIfKeyIsInvalid(key);
				return key.GetValueNames(constraints);
			});
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x0001561C File Offset: 0x0001381C
		private IEnumerable<Tuple<string, RegistryValueKind>> GetValueInfosInternal(ReadWriteConstraints constraints)
		{
			return DistributedStore.Instance.ExecuteRequest<IEnumerable<Tuple<string, RegistryValueKind>>>(this, OperationCategory.GetValueInfos, OperationType.Read, string.Empty, delegate(IDistributedStoreKey key)
			{
				this.ThrowIfKeyIsInvalid(key);
				return key.GetValueInfos(constraints);
			});
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x0001565C File Offset: 0x0001385C
		private IDistributedStoreBatchRequest CreateBatchUpdateRequestInternal()
		{
			return new GenericBatchRequest(this);
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x0001568C File Offset: 0x0001388C
		private void ExecuteBatchRequestInternal(List<DxStoreBatchCommand> commands, ReadWriteConstraints constraints)
		{
			DistributedStore.Instance.ExecuteRequest(this, OperationCategory.ExecuteBatch, OperationType.Write, string.Format("TotalCommands: [{0}] IsConstrained: [{1}]", commands.Count, constraints != null), delegate(IDistributedStoreKey key, bool isPrimary, StoreKind storeKind)
			{
				this.ThrowIfKeyIsInvalid(key);
				key.ExecuteBatchRequest(commands, constraints);
			});
		}

		// Token: 0x04000308 RID: 776
		private object locker = new object();

		// Token: 0x04000309 RID: 777
		private bool isDisposed;
	}
}
