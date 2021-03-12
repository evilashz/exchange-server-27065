using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Exchange.Cluster.ClusApi;
using Microsoft.Exchange.DxStore.Common;
using Microsoft.Win32;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x0200008E RID: 142
	public class ClusterDbKey : IDistributedStoreKey, IDisposable
	{
		// Token: 0x06000520 RID: 1312 RVA: 0x0001341D File Offset: 0x0001161D
		internal ClusterDbKey(AmClusterRegkeyHandle keyHandle, AmClusterHandle clusterHandle)
		{
			this.KeyHandle = keyHandle;
			this.ClusterHandle = clusterHandle;
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000521 RID: 1313 RVA: 0x00013433 File Offset: 0x00011633
		// (set) Token: 0x06000522 RID: 1314 RVA: 0x0001343B File Offset: 0x0001163B
		internal AmClusterRegkeyHandle KeyHandle { get; set; }

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000523 RID: 1315 RVA: 0x00013444 File Offset: 0x00011644
		// (set) Token: 0x06000524 RID: 1316 RVA: 0x0001344C File Offset: 0x0001164C
		internal AmClusterHandle ClusterHandle { get; private set; }

		// Token: 0x06000525 RID: 1317 RVA: 0x00013455 File Offset: 0x00011655
		public IDistributedStoreKey OpenKey(string subKeyName, DxStoreKeyAccessMode mode, bool isIgnoreIfNotExist, ReadWriteConstraints constraints)
		{
			return this.OpenKeyInternal(subKeyName, mode, isIgnoreIfNotExist);
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x00013460 File Offset: 0x00011660
		public void CloseKey()
		{
			this.Dispose();
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x00013468 File Offset: 0x00011668
		public bool DeleteKey(string subKeyName, bool isIgnoreIfNotExist, ReadWriteConstraints constraints)
		{
			return this.IsKeyExists(subKeyName, constraints) && this.DeleteKeyInternal(subKeyName, constraints);
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x00013480 File Offset: 0x00011680
		public bool SetValue(string propertyName, object propertyValue, RegistryValueKind valueKind, bool isBestEffort, ReadWriteConstraints constraints)
		{
			bool result = false;
			using (ClusdbMarshalledProperty clusdbMarshalledProperty = ClusdbMarshalledProperty.Create(propertyName, propertyValue, valueKind))
			{
				if (clusdbMarshalledProperty != null)
				{
					int num = ClusapiMethods.ClusterRegSetValue(this.KeyHandle, clusdbMarshalledProperty.PropertyName, clusdbMarshalledProperty.ValueKind, clusdbMarshalledProperty.PropertyValueIntPtr, clusdbMarshalledProperty.PropertyValueSize);
					if (num != 0)
					{
						if (!isBestEffort)
						{
							throw AmExceptionHelper.ConstructClusterApiException(num, "ClusterRegSetValue()", new object[0]);
						}
					}
					else
					{
						result = true;
					}
				}
				else if (!isBestEffort)
				{
					string typeName = (propertyValue != null) ? propertyValue.GetType().Name : "<null>";
					throw new ClusterApiException("ClusterRegSetValue(unsupported registry type)", new ClusterUnsupportedRegistryTypeException(typeName));
				}
			}
			return result;
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x00013524 File Offset: 0x00011724
		public object GetValue(string propertyName, out bool isValueExist, out RegistryValueKind valueKind, ReadWriteConstraints constraints)
		{
			return this.GetRaw(this.KeyHandle, propertyName, out isValueExist, out valueKind);
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x00013538 File Offset: 0x00011738
		public IEnumerable<Tuple<string, object>> GetAllValues(ReadWriteConstraints constraints)
		{
			List<Tuple<string, object>> list = new List<Tuple<string, object>>();
			IEnumerable<string> valueNames = this.GetValueNames(constraints);
			if (valueNames != null)
			{
				foreach (string text in valueNames)
				{
					bool flag = false;
					RegistryValueKind registryValueKind = RegistryValueKind.Unknown;
					object value = this.GetValue(text, out flag, out registryValueKind, constraints);
					if (flag)
					{
						list.Add(new Tuple<string, object>(text, value));
					}
				}
			}
			return list;
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x000135B4 File Offset: 0x000117B4
		public bool DeleteValue(string propertyName, bool isIgnoreIfNotExist, ReadWriteConstraints constraints)
		{
			if (isIgnoreIfNotExist && !this.IsPropertyExists(propertyName, constraints))
			{
				return false;
			}
			int num = ClusapiMethods.ClusterRegDeleteValue(this.KeyHandle, propertyName);
			if (num != 0)
			{
				throw AmExceptionHelper.ConstructClusterApiException(num, "ClusterRegDeleteValue()", new object[0]);
			}
			return true;
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x000135F4 File Offset: 0x000117F4
		public IEnumerable<string> GetSubkeyNames(ReadWriteConstraints constraints)
		{
			List<string> list = new List<string>();
			int num = 128;
			int num2 = 0;
			int num4;
			for (;;)
			{
				StringBuilder stringBuilder = new StringBuilder(num);
				int num3 = num;
				num4 = ClusapiMethods.ClusterRegEnumKey(this.KeyHandle, num2, stringBuilder, ref num3, IntPtr.Zero);
				if (234 == num4)
				{
					num = num3 + 1;
					stringBuilder = new StringBuilder(num);
					num4 = ClusapiMethods.ClusterRegEnumKey(this.KeyHandle, num2, stringBuilder, ref num3, IntPtr.Zero);
				}
				if (num4 != 0)
				{
					break;
				}
				list.Add(stringBuilder.ToString());
				num2++;
			}
			if (259 != num4)
			{
				throw AmExceptionHelper.ConstructClusterApiException(num4, "ClusterRegEnumKey()", new object[0]);
			}
			return list.ToArray();
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x00013694 File Offset: 0x00011894
		public IEnumerable<Tuple<string, RegistryValueKind>> GetValueInfos(ReadWriteConstraints constraints)
		{
			List<Tuple<string, RegistryValueKind>> list = new List<Tuple<string, RegistryValueKind>>();
			int num = 128;
			int num2 = 0;
			int item = 0;
			int num5;
			for (;;)
			{
				StringBuilder stringBuilder = new StringBuilder(num);
				int num3 = num;
				int num4 = 0;
				num5 = ClusapiMethods.ClusterRegEnumValue(this.KeyHandle, num2, stringBuilder, ref num3, ref item, IntPtr.Zero, ref num4);
				if (259 == num5)
				{
					return list;
				}
				if (234 != num5 && num5 != 0)
				{
					break;
				}
				if (num3 > num)
				{
					num = num3 + 1;
					num4 = 0;
					stringBuilder = new StringBuilder(num);
					num5 = ClusapiMethods.ClusterRegEnumValue(this.KeyHandle, num2, stringBuilder, ref num3, ref item, IntPtr.Zero, ref num4);
				}
				if (234 != num5 && num5 != 0)
				{
					goto Block_6;
				}
				list.Add(new Tuple<string, RegistryValueKind>(stringBuilder.ToString(), (RegistryValueKind)item));
				num2++;
			}
			throw AmExceptionHelper.ConstructClusterApiException(num5, "ClusterRegEnumValue(first)", new object[0]);
			Block_6:
			throw AmExceptionHelper.ConstructClusterApiException(num5, "ClusterRegEnumValue(second)", new object[0]);
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x0001377B File Offset: 0x0001197B
		public IEnumerable<string> GetValueNames(ReadWriteConstraints constraints)
		{
			return from vi in this.GetValueInfos(constraints)
			select vi.Item1;
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x000137A6 File Offset: 0x000119A6
		public IDistributedStoreBatchRequest CreateBatchUpdateRequest()
		{
			return new GenericBatchRequest(this);
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x000137B0 File Offset: 0x000119B0
		public void ExecuteBatchRequest(List<DxStoreBatchCommand> commands, ReadWriteConstraints constraints)
		{
			using (ClusdbBatchRequest clusdbBatchRequest = new ClusdbBatchRequest(this))
			{
				foreach (DxStoreBatchCommand dxStoreBatchCommand in commands)
				{
					DxStoreBatchCommand.CreateKey createKey = dxStoreBatchCommand as DxStoreBatchCommand.CreateKey;
					if (createKey != null)
					{
						clusdbBatchRequest.CreateKey(createKey.Name);
					}
					else
					{
						DxStoreBatchCommand.DeleteKey deleteKey = dxStoreBatchCommand as DxStoreBatchCommand.DeleteKey;
						if (deleteKey != null)
						{
							clusdbBatchRequest.DeleteKey(deleteKey.Name);
						}
						else
						{
							DxStoreBatchCommand.SetProperty setProperty = dxStoreBatchCommand as DxStoreBatchCommand.SetProperty;
							if (setProperty != null)
							{
								clusdbBatchRequest.SetValue(setProperty.Name, setProperty.Value.Value, (RegistryValueKind)setProperty.Value.Kind);
							}
							else
							{
								DxStoreBatchCommand.DeleteProperty deleteProperty = dxStoreBatchCommand as DxStoreBatchCommand.DeleteProperty;
								if (deleteProperty != null)
								{
									clusdbBatchRequest.DeleteValue(deleteProperty.Name);
								}
							}
						}
					}
				}
				clusdbBatchRequest.Execute(constraints);
			}
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x000138A0 File Offset: 0x00011AA0
		public IDistributedStoreChangeNotify CreateChangeNotify(ChangeNotificationFlags flags, object context, Action callback)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x000138A7 File Offset: 0x00011AA7
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x000138B8 File Offset: 0x00011AB8
		internal static IDistributedStoreKey GetBaseKey(AmClusterHandle clusterHandle, DxStoreKeyAccessMode mode)
		{
			RegSAM regSam = ClusterDbKey.GetRegSam(mode);
			AmClusterRegkeyHandle clusterKey = ClusapiMethods.GetClusterKey(clusterHandle, regSam);
			if (clusterKey == null || clusterKey.IsInvalid)
			{
				throw AmExceptionHelper.ConstructClusterApiExceptionNoErr("GetClusterKey", new object[0]);
			}
			return new ClusterDbKey(clusterKey, clusterHandle);
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x000138F7 File Offset: 0x00011AF7
		protected virtual void Dispose(bool disposing)
		{
			if (!this.isDisposed)
			{
				if (disposing)
				{
					this.KeyHandle.Close();
				}
				this.isDisposed = true;
			}
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x00013918 File Offset: 0x00011B18
		private static RegSAM GetRegSam(DxStoreKeyAccessMode mode)
		{
			RegSAM result = RegSAM.Read;
			if (mode == DxStoreKeyAccessMode.Write || mode == DxStoreKeyAccessMode.CreateIfNotExist)
			{
				result = RegSAM.AllAccess;
			}
			return result;
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x0001393C File Offset: 0x00011B3C
		private IDistributedStoreKey OpenKeyInternal(string subKeyName, DxStoreKeyAccessMode mode, bool isIgnoreIfNotExist)
		{
			AmClusterRegkeyHandle amClusterRegkeyHandle = null;
			AmClusterRegkeyHandle keyHandle = this.KeyHandle;
			RegSAM regSam = ClusterDbKey.GetRegSam(mode);
			int num = ClusapiMethods.ClusterRegOpenKey(keyHandle, subKeyName, regSam, out amClusterRegkeyHandle);
			if (mode == DxStoreKeyAccessMode.CreateIfNotExist && (num == 2 || num == 3))
			{
				uint num2;
				num = ClusapiMethods.ClusterRegCreateKey(keyHandle, subKeyName, 0U, regSam, IntPtr.Zero, out amClusterRegkeyHandle, out num2);
			}
			if (num == 0)
			{
				return new ClusterDbKey(amClusterRegkeyHandle, this.ClusterHandle);
			}
			if (amClusterRegkeyHandle != null && !amClusterRegkeyHandle.IsInvalid)
			{
				amClusterRegkeyHandle.Dispose();
			}
			amClusterRegkeyHandle = null;
			if (!isIgnoreIfNotExist)
			{
				throw new ClusterApiException("ClusterRegOpenKey", new Win32Exception(num));
			}
			return null;
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x000139C4 File Offset: 0x00011BC4
		private bool DeleteKeyInternal(string keyName, ReadWriteConstraints constraints)
		{
			IEnumerable<string> subkeyNames = this.GetSubkeyNames(keyName, constraints);
			foreach (string path in subkeyNames)
			{
				this.DeleteKeyInternal(Path.Combine(keyName, path), constraints);
			}
			int num = ClusapiMethods.ClusterRegDeleteKey(this.KeyHandle, keyName);
			if (num != 2 && num != 0)
			{
				throw new ClusterApiException("ClusterRegDeleteKey", new Win32Exception(num));
			}
			return true;
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x00013A44 File Offset: 0x00011C44
		private object GetRaw(AmClusterRegkeyHandle hKey, string valueName, out bool doesValueExist, out RegistryValueKind valueKind)
		{
			IntPtr intPtr = IntPtr.Zero;
			int num = 1024;
			object result;
			try
			{
				intPtr = Marshal.AllocHGlobal(num);
				int num2 = ClusapiMethods.ClusterRegQueryValue(hKey, valueName, out valueKind, intPtr, ref num);
				if (num2 == 234)
				{
					int num3 = 0;
					do
					{
						Marshal.FreeHGlobal(intPtr);
						intPtr = Marshal.AllocHGlobal(num);
						num2 = ClusapiMethods.ClusterRegQueryValue(hKey, valueName, out valueKind, intPtr, ref num);
					}
					while (num2 == 234 && num3++ < 3);
				}
				if (num2 == 2 || num2 == 1018)
				{
					doesValueExist = false;
					result = null;
				}
				else
				{
					if (num2 != 0)
					{
						throw new ClusterApiException("GetRaw()", new Win32Exception(num2));
					}
					doesValueExist = true;
					result = this.ParseRegistryValue(valueKind, intPtr, num);
				}
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
			}
			return result;
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x00013B08 File Offset: 0x00011D08
		private object ParseRegistryValue(RegistryValueKind valueType, IntPtr value, int valueSize)
		{
			switch (valueType)
			{
			case RegistryValueKind.String:
				return Marshal.PtrToStringUni(value);
			case RegistryValueKind.ExpandString:
			case (RegistryValueKind)5:
			case (RegistryValueKind)6:
				break;
			case RegistryValueKind.Binary:
			{
				byte[] array = new byte[valueSize];
				Marshal.Copy(value, array, 0, valueSize);
				return array;
			}
			case RegistryValueKind.DWord:
				return Marshal.ReadInt32(value);
			case RegistryValueKind.MultiString:
				return ClusdbMarshalledProperty.FromIntPtrToStringArray(value, valueSize);
			default:
				if (valueType == RegistryValueKind.QWord)
				{
					return Marshal.ReadInt64(value);
				}
				break;
			}
			throw new ClusterApiException("ParseRegistryValue", new ClusterUnsupportedRegistryTypeException(valueType.ToString()));
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x00013B98 File Offset: 0x00011D98
		private bool IsKeyExists(string subKeyName, ReadWriteConstraints constraints)
		{
			using (IDistributedStoreKey distributedStoreKey = this.OpenKey(subKeyName, DxStoreKeyAccessMode.Read, true, constraints))
			{
				if (distributedStoreKey != null)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x00013BD8 File Offset: 0x00011DD8
		private bool IsPropertyExists(string propertyName, ReadWriteConstraints constraints)
		{
			bool result = false;
			RegistryValueKind registryValueKind;
			this.GetValue(propertyName, out result, out registryValueKind, constraints);
			return result;
		}

		// Token: 0x040002C9 RID: 713
		private bool isDisposed;
	}
}
