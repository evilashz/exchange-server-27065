using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Exchange.DxStore.Common;
using Microsoft.Win32;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x0200009C RID: 156
	public class DxStoreKey : IDistributedStoreKey, IDisposable
	{
		// Token: 0x060005B2 RID: 1458 RVA: 0x000156F9 File Offset: 0x000138F9
		public DxStoreKey(string keyFullName, DxStoreKeyAccessMode mode, DxStoreKey.BaseKeyParameters baseParameters)
		{
			this.FullKeyName = keyFullName;
			this.Mode = mode;
			this.BaseParameters = baseParameters;
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x060005B3 RID: 1459 RVA: 0x00015716 File Offset: 0x00013916
		// (set) Token: 0x060005B4 RID: 1460 RVA: 0x0001571E File Offset: 0x0001391E
		public string FullKeyName { get; set; }

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x060005B5 RID: 1461 RVA: 0x00015727 File Offset: 0x00013927
		// (set) Token: 0x060005B6 RID: 1462 RVA: 0x0001572F File Offset: 0x0001392F
		public DxStoreKeyAccessMode Mode { get; set; }

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x060005B7 RID: 1463 RVA: 0x00015738 File Offset: 0x00013938
		// (set) Token: 0x060005B8 RID: 1464 RVA: 0x00015740 File Offset: 0x00013940
		public DxStoreKey.BaseKeyParameters BaseParameters { get; set; }

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x060005B9 RID: 1465 RVA: 0x00015749 File Offset: 0x00013949
		public bool IsBaseKey
		{
			get
			{
				return string.IsNullOrEmpty(this.FullKeyName);
			}
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x00015758 File Offset: 0x00013958
		public T CreateRequest<T>() where T : DxStoreAccessRequest, new()
		{
			T result = Activator.CreateInstance<T>();
			result.Initialize(this.FullKeyName, this.BaseParameters.IsPrivate, this.BaseParameters.Self);
			return result;
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x000158A4 File Offset: 0x00013AA4
		public IDistributedStoreKey OpenKey(string subkeyName, DxStoreKeyAccessMode mode, bool isIgnoreIfNotExist, ReadWriteConstraints constraints)
		{
			return this.BaseParameters.KeyFactory.RunOperationAndTranslateException<IDistributedStoreKey>(OperationCategory.OpenKey, Path.Combine(this.FullKeyName, subkeyName), delegate()
			{
				ReadOptions readOptions = this.GetReadOptions(constraints);
				WriteOptions writeOptions = this.GetWriteOptions(constraints);
				DxStoreAccessRequest.CheckKey checkKey = this.CreateRequest<DxStoreAccessRequest.CheckKey>();
				checkKey.IsCreateIfNotExist = (mode == DxStoreKeyAccessMode.CreateIfNotExist);
				checkKey.SubkeyName = subkeyName;
				checkKey.ReadOptions = readOptions;
				checkKey.WriteOptions = writeOptions;
				DxStoreAccessReply.CheckKey checkKey2 = this.BaseParameters.Client.CheckKey(checkKey, null);
				this.SetReadResult(constraints, checkKey2.ReadResult);
				this.SetWriteResult(constraints, checkKey2.WriteResult);
				IDistributedStoreKey result = null;
				if (!checkKey2.IsExist)
				{
					if (!isIgnoreIfNotExist)
					{
						throw new DxStoreKeyNotFoundException(subkeyName);
					}
				}
				else
				{
					result = new DxStoreKey(Path.Combine(this.FullKeyName, subkeyName), mode, this.BaseParameters);
				}
				return result;
			}, false);
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x0001590A File Offset: 0x00013B0A
		public void CloseKey()
		{
			this.Dispose();
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x000159CC File Offset: 0x00013BCC
		public bool DeleteKey(string keyName, bool isIgnoreIfNotExist, ReadWriteConstraints constraints)
		{
			ReadOptions readOptions = this.GetReadOptions(constraints);
			WriteOptions writeOptions = this.GetWriteOptions(constraints);
			return this.BaseParameters.KeyFactory.RunOperationAndTranslateException<bool>(OperationCategory.DeleteKey, this.FullKeyName, delegate()
			{
				DxStoreAccessRequest.DeleteKey deleteKey = this.CreateRequest<DxStoreAccessRequest.DeleteKey>();
				deleteKey.SubkeyName = keyName;
				deleteKey.ReadOptions = readOptions;
				deleteKey.WriteOptions = writeOptions;
				DxStoreAccessReply.DeleteKey deleteKey2 = this.BaseParameters.Client.DeleteKey(deleteKey, null);
				if (!deleteKey2.IsExist && !isIgnoreIfNotExist)
				{
					throw new DxStoreKeyNotFoundException(keyName);
				}
				this.SetReadResult(constraints, deleteKey2.ReadResult);
				this.SetWriteResult(constraints, deleteKey2.WriteResult);
				return deleteKey2.IsExist;
			}, false);
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x00015AF4 File Offset: 0x00013CF4
		public bool SetValue(string propertyName, object propertyValue, RegistryValueKind valueKind, bool isBestEffort, ReadWriteConstraints constraints)
		{
			WriteOptions writeOptions = this.GetWriteOptions(constraints);
			return this.BaseParameters.KeyFactory.RunOperationAndTranslateException<bool>(OperationCategory.SetValue, this.FullKeyName, delegate()
			{
				PropertyValue value = new PropertyValue(propertyValue, valueKind);
				bool result = false;
				try
				{
					DxStoreAccessRequest.SetProperty setProperty = this.CreateRequest<DxStoreAccessRequest.SetProperty>();
					setProperty.Name = propertyName;
					setProperty.Value = value;
					setProperty.WriteOptions = writeOptions;
					DxStoreAccessReply.SetProperty setProperty2 = this.BaseParameters.Client.SetProperty(setProperty, null);
					this.SetWriteResult(constraints, setProperty2.WriteResult);
					result = true;
				}
				catch
				{
					if (!isBestEffort)
					{
						throw;
					}
				}
				return result;
			}, false);
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x00015BE4 File Offset: 0x00013DE4
		public object GetValue(string propertyName, out bool isValueExist, out RegistryValueKind valueKind, ReadWriteConstraints constraints)
		{
			ReadOptions readOptions = this.GetReadOptions(constraints);
			isValueExist = false;
			valueKind = RegistryValueKind.Unknown;
			PropertyValue propertyValue = this.BaseParameters.KeyFactory.RunOperationAndTranslateException<PropertyValue>(OperationCategory.GetValue, this.FullKeyName, delegate()
			{
				DxStoreAccessRequest.GetProperty getProperty = this.CreateRequest<DxStoreAccessRequest.GetProperty>();
				getProperty.Name = propertyName;
				getProperty.ReadOptions = readOptions;
				DxStoreAccessReply.GetProperty property = this.BaseParameters.Client.GetProperty(getProperty, null);
				this.SetReadResult(constraints, property.ReadResult);
				return property.Value;
			}, false);
			object result = null;
			if (propertyValue != null)
			{
				isValueExist = true;
				result = propertyValue.Value;
				valueKind = (RegistryValueKind)propertyValue.Kind;
			}
			return result;
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x00015D34 File Offset: 0x00013F34
		public bool DeleteValue(string propertyName, bool isIgnoreIfNotExist, ReadWriteConstraints constraints)
		{
			return this.BaseParameters.KeyFactory.RunOperationAndTranslateException<bool>(OperationCategory.DeleteValue, this.FullKeyName, delegate()
			{
				ReadOptions readOptions = this.GetReadOptions(constraints);
				WriteOptions writeOptions = this.GetWriteOptions(constraints);
				DxStoreAccessRequest.DeleteProperty deleteProperty = this.CreateRequest<DxStoreAccessRequest.DeleteProperty>();
				deleteProperty.Name = propertyName;
				deleteProperty.ReadOptions = readOptions;
				deleteProperty.WriteOptions = writeOptions;
				DxStoreAccessReply.DeleteProperty deleteProperty2 = this.BaseParameters.Client.DeleteProperty(deleteProperty, null);
				this.SetReadResult(constraints, deleteProperty2.ReadResult);
				this.SetWriteResult(constraints, deleteProperty2.WriteResult);
				if (!deleteProperty2.IsExist && !isIgnoreIfNotExist)
				{
					throw new DxStorePropertyNotFoundException(propertyName);
				}
				return deleteProperty2.IsExist;
			}, false);
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x00015DF4 File Offset: 0x00013FF4
		public IEnumerable<string> GetSubkeyNames(ReadWriteConstraints constraints)
		{
			ReadOptions readOptions = this.GetReadOptions(constraints);
			return this.BaseParameters.KeyFactory.RunOperationAndTranslateException<string[]>(OperationCategory.GetSubKeyNames, this.FullKeyName, delegate()
			{
				DxStoreAccessRequest.GetSubkeyNames getSubkeyNames = this.CreateRequest<DxStoreAccessRequest.GetSubkeyNames>();
				getSubkeyNames.ReadOptions = readOptions;
				DxStoreAccessReply.GetSubkeyNames subkeyNames = this.BaseParameters.Client.GetSubkeyNames(getSubkeyNames, null);
				this.SetReadResult(constraints, subkeyNames.ReadResult);
				return subkeyNames.Keys;
			}, false);
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x00015EB8 File Offset: 0x000140B8
		public PropertyNameInfo[] GetPropertyNameInfos(ReadWriteConstraints constraints)
		{
			ReadOptions readOptions = this.GetReadOptions(constraints);
			return this.BaseParameters.KeyFactory.RunOperationAndTranslateException<PropertyNameInfo[]>(OperationCategory.GetSubKeyNames, this.FullKeyName, delegate()
			{
				DxStoreAccessRequest.GetPropertyNames getPropertyNames = this.CreateRequest<DxStoreAccessRequest.GetPropertyNames>();
				getPropertyNames.ReadOptions = readOptions;
				DxStoreAccessReply.GetPropertyNames propertyNames = this.BaseParameters.Client.GetPropertyNames(getPropertyNames, null);
				this.SetReadResult(constraints, propertyNames.ReadResult);
				return propertyNames.Infos;
			}, false);
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x00015F18 File Offset: 0x00014118
		public IEnumerable<string> GetValueNames(ReadWriteConstraints constraints)
		{
			PropertyNameInfo[] propertyNameInfos = this.GetPropertyNameInfos(constraints);
			if (propertyNameInfos != null)
			{
				return (from pni in propertyNameInfos
				select pni.Name).ToArray<string>();
			}
			return Utils.EmptyArray<string>();
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x00015F74 File Offset: 0x00014174
		public IEnumerable<Tuple<string, RegistryValueKind>> GetValueInfos(ReadWriteConstraints constraints)
		{
			PropertyNameInfo[] propertyNameInfos = this.GetPropertyNameInfos(constraints);
			if (propertyNameInfos != null)
			{
				return (from pni in propertyNameInfos
				select new Tuple<string, RegistryValueKind>(pni.Name, (RegistryValueKind)pni.Kind)).ToArray<Tuple<string, RegistryValueKind>>();
			}
			return Utils.EmptyArray<Tuple<string, RegistryValueKind>>();
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x0001607C File Offset: 0x0001427C
		public IEnumerable<Tuple<string, object>> GetAllValues(ReadWriteConstraints constraints)
		{
			List<Tuple<string, object>> list = new List<Tuple<string, object>>();
			ReadOptions readOptions = this.GetReadOptions(constraints);
			return this.BaseParameters.KeyFactory.RunOperationAndTranslateException<List<Tuple<string, object>>>(OperationCategory.GetAllValues, this.FullKeyName, delegate()
			{
				DxStoreAccessRequest.GetAllProperties getAllProperties = this.CreateRequest<DxStoreAccessRequest.GetAllProperties>();
				getAllProperties.ReadOptions = readOptions;
				DxStoreAccessReply.GetAllProperties allProperties = this.BaseParameters.Client.GetAllProperties(getAllProperties, null);
				this.SetReadResult(constraints, allProperties.ReadResult);
				foreach (Tuple<string, PropertyValue> tuple in allProperties.Values)
				{
					string item = tuple.Item1;
					object item2 = (tuple.Item2 != null) ? tuple.Item2.Value : null;
					list.Add(new Tuple<string, object>(item, item2));
				}
				return list;
			}, false);
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x000160DF File Offset: 0x000142DF
		public IDistributedStoreBatchRequest CreateBatchUpdateRequest()
		{
			return new GenericBatchRequest(this);
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x0001618C File Offset: 0x0001438C
		public void ExecuteBatchRequest(List<DxStoreBatchCommand> commands, ReadWriteConstraints constraints)
		{
			WriteOptions writeOptions = this.GetWriteOptions(constraints);
			this.BaseParameters.KeyFactory.RunOperationAndTranslateException(OperationCategory.ExecuteBatch, this.FullKeyName, delegate()
			{
				DxStoreAccessRequest.ExecuteBatch executeBatch = new DxStoreAccessRequest.ExecuteBatch
				{
					Commands = commands.ToArray(),
					WriteOptions = writeOptions
				};
				executeBatch.Initialize(this.FullKeyName, this.BaseParameters.IsPrivate, this.BaseParameters.Self);
				DxStoreAccessReply.ExecuteBatch executeBatch2 = this.BaseParameters.Client.ExecuteBatch(executeBatch, null);
				this.SetWriteResult(constraints, executeBatch2.WriteResult);
			});
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x000161F1 File Offset: 0x000143F1
		public IDistributedStoreChangeNotify CreateChangeNotify(ChangeNotificationFlags flags, object context, Action callback)
		{
			this.BaseParameters.KeyFactory.RunOperationAndTranslateException(OperationCategory.CreateChangeNotify, this.FullKeyName, delegate()
			{
				throw new NotImplementedException();
			});
			return null;
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x00016229 File Offset: 0x00014429
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x00016238 File Offset: 0x00014438
		protected virtual void Dispose(bool isDisposing)
		{
			if (!this.isDisposed)
			{
				this.isDisposed = true;
			}
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x00016249 File Offset: 0x00014449
		private ReadOptions GetReadOptions(ReadWriteConstraints rwc)
		{
			if (rwc == null || rwc.ReadOptions == null)
			{
				return this.BaseParameters.DefaultReadOptions;
			}
			return rwc.ReadOptions;
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x00016268 File Offset: 0x00014468
		private WriteOptions GetWriteOptions(ReadWriteConstraints rwc)
		{
			if (rwc == null || rwc.WriteOptions == null)
			{
				return this.BaseParameters.DefaultWriteOptions;
			}
			return rwc.WriteOptions;
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x00016287 File Offset: 0x00014487
		private void SetReadResult(ReadWriteConstraints rwc, ReadResult result)
		{
			if (rwc != null)
			{
				rwc.ReadResult = result;
			}
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x00016293 File Offset: 0x00014493
		private void SetWriteResult(ReadWriteConstraints rwc, WriteResult result)
		{
			if (rwc != null)
			{
				rwc.WriteResult = result;
			}
		}

		// Token: 0x04000318 RID: 792
		private bool isDisposed;

		// Token: 0x0200009D RID: 157
		public class BaseKeyParameters
		{
			// Token: 0x170001F1 RID: 497
			// (get) Token: 0x060005D2 RID: 1490 RVA: 0x0001629F File Offset: 0x0001449F
			// (set) Token: 0x060005D3 RID: 1491 RVA: 0x000162A7 File Offset: 0x000144A7
			public IDxStoreAccessClient Client { get; set; }

			// Token: 0x170001F2 RID: 498
			// (get) Token: 0x060005D4 RID: 1492 RVA: 0x000162B0 File Offset: 0x000144B0
			// (set) Token: 0x060005D5 RID: 1493 RVA: 0x000162B8 File Offset: 0x000144B8
			public DxStoreKeyFactory KeyFactory { get; set; }

			// Token: 0x170001F3 RID: 499
			// (get) Token: 0x060005D6 RID: 1494 RVA: 0x000162C1 File Offset: 0x000144C1
			// (set) Token: 0x060005D7 RID: 1495 RVA: 0x000162C9 File Offset: 0x000144C9
			public ReadOptions DefaultReadOptions { get; set; }

			// Token: 0x170001F4 RID: 500
			// (get) Token: 0x060005D8 RID: 1496 RVA: 0x000162D2 File Offset: 0x000144D2
			// (set) Token: 0x060005D9 RID: 1497 RVA: 0x000162DA File Offset: 0x000144DA
			public WriteOptions DefaultWriteOptions { get; set; }

			// Token: 0x170001F5 RID: 501
			// (get) Token: 0x060005DA RID: 1498 RVA: 0x000162E3 File Offset: 0x000144E3
			// (set) Token: 0x060005DB RID: 1499 RVA: 0x000162EB File Offset: 0x000144EB
			public string Self { get; set; }

			// Token: 0x170001F6 RID: 502
			// (get) Token: 0x060005DC RID: 1500 RVA: 0x000162F4 File Offset: 0x000144F4
			// (set) Token: 0x060005DD RID: 1501 RVA: 0x000162FC File Offset: 0x000144FC
			public bool IsPrivate { get; set; }
		}
	}
}
