using System;
using System.Linq;
using Microsoft.Exchange.Cluster.ClusApi;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Win32;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020000C2 RID: 194
	internal class ClusterRegistry : RegistryManipulator
	{
		// Token: 0x060007F0 RID: 2032 RVA: 0x00026BDC File Offset: 0x00024DDC
		public ClusterRegistry(string root, AmClusterHandle handle) : base(root, handle)
		{
			bool flag = true;
			try
			{
				this.SetClusterHandle(handle);
				flag = false;
			}
			finally
			{
				if (flag)
				{
					base.SuppressDisposeTracker();
				}
			}
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x00026C18 File Offset: 0x00024E18
		public void SetClusterHandle(AmClusterHandle handle)
		{
			if (this.rootKey != null)
			{
				this.rootKey.Dispose();
				this.rootKey = null;
			}
			this.Handle = null;
			using (IDistributedStoreKey clusterKey = DistributedStore.Instance.GetClusterKey(handle, null, null, DxStoreKeyAccessMode.Write, false))
			{
				this.rootKey = clusterKey.OpenKey(this.Root, DxStoreKeyAccessMode.CreateIfNotExist, false, null);
				this.Handle = handle;
			}
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x00026C90 File Offset: 0x00024E90
		public T RunActionWithKey<T>(string keyName, Func<IDistributedStoreKey, T> action, DxStoreKeyAccessMode mode = DxStoreKeyAccessMode.Read)
		{
			IDistributedStoreKey distributedStoreKey = string.IsNullOrEmpty(keyName) ? this.rootKey : this.rootKey.OpenKey(keyName, mode, false, null);
			T result;
			try
			{
				T t = action(distributedStoreKey);
				result = t;
			}
			finally
			{
				if (distributedStoreKey != null && !object.ReferenceEquals(distributedStoreKey, this.rootKey))
				{
					distributedStoreKey.Dispose();
				}
			}
			return result;
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x00026D02 File Offset: 0x00024F02
		public override string[] GetSubKeyNames(string keyName)
		{
			return this.RunActionWithKey<string[]>(keyName, (IDistributedStoreKey key) => key.GetSubkeyNames(null).ToArray<string>(), DxStoreKeyAccessMode.Read);
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x00026D37 File Offset: 0x00024F37
		public override string[] GetValueNames(string keyName)
		{
			return this.RunActionWithKey<string[]>(keyName, (IDistributedStoreKey key) => key.GetValueNames(null).ToArray<string>(), DxStoreKeyAccessMode.Read);
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x00026D94 File Offset: 0x00024F94
		public override void SetValue(string keyName, RegistryValue value)
		{
			this.RunActionWithKey<bool>(keyName, (IDistributedStoreKey regKey) => regKey.SetValue(value.Name, value.Value, value.Kind, false, null), DxStoreKeyAccessMode.Write);
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x00026E04 File Offset: 0x00025004
		public override RegistryValue GetValue(string keyName, string valueName)
		{
			return this.RunActionWithKey<RegistryValue>(keyName, delegate(IDistributedStoreKey regKey)
			{
				RegistryValue result = null;
				bool flag = false;
				RegistryValueKind kind;
				object value = regKey.GetValue(valueName, out flag, out kind, null);
				if (flag && value != null)
				{
					result = new RegistryValue(valueName, value, kind);
				}
				return result;
			}, DxStoreKeyAccessMode.Read);
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x00026E4C File Offset: 0x0002504C
		public override void DeleteValue(string keyName, string valueName)
		{
			this.RunActionWithKey<bool>(keyName, (IDistributedStoreKey regKey) => regKey.DeleteValue(valueName, true, null), DxStoreKeyAccessMode.Write);
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x00026E94 File Offset: 0x00025094
		public override void DeleteKey(string keyName)
		{
			this.RunActionWithKey<bool>(string.Empty, (IDistributedStoreKey regKey) => regKey.DeleteKey(keyName, true, null), DxStoreKeyAccessMode.Write);
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x00026EC8 File Offset: 0x000250C8
		public override void CreateKey(string keyName)
		{
			using (this.rootKey.OpenKey(keyName, DxStoreKeyAccessMode.CreateIfNotExist, false, null))
			{
			}
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x00026F04 File Offset: 0x00025104
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<ClusterRegistry>(this);
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x00026F0C File Offset: 0x0002510C
		public override void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x00026F1B File Offset: 0x0002511B
		private void Dispose(bool disposing)
		{
			if (disposing && this.rootKey != null)
			{
				this.rootKey.Dispose();
				this.rootKey = null;
			}
			base.Dispose();
		}

		// Token: 0x04000381 RID: 897
		private const int BufferSize = 128;

		// Token: 0x04000382 RID: 898
		private IDistributedStoreKey rootKey;
	}
}
