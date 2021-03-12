using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200038D RID: 909
	internal class ServerObjectMap
	{
		// Token: 0x060015F0 RID: 5616 RVA: 0x00038AB1 File Offset: 0x00036CB1
		public ServerObjectMap(byte logonIndex)
		{
			this.objectMap = new Dictionary<ServerObjectHandle, IServerObject>();
			this.logonIndex = logonIndex;
			this.counter = 0U;
		}

		// Token: 0x060015F1 RID: 5617 RVA: 0x00038AD4 File Offset: 0x00036CD4
		public ServerObjectHandle Add(IServerObject serverObject)
		{
			if (serverObject == null)
			{
				throw new ArgumentNullException("serverObject");
			}
			ServerObjectHandle nextFreeHandle = this.GetNextFreeHandle();
			this.objectMap.Add(nextFreeHandle, serverObject);
			return nextFreeHandle;
		}

		// Token: 0x060015F2 RID: 5618 RVA: 0x00038B04 File Offset: 0x00036D04
		public void Add(ServerObjectHandle handle, IServerObject serverObject)
		{
			if (serverObject == null)
			{
				throw new ArgumentNullException("serverObject");
			}
			if ((handle.HandleValue & 16777215U) == 16777215U)
			{
				throw new ArgumentOutOfRangeException("handle");
			}
			this.objectMap.Add(handle, serverObject);
		}

		// Token: 0x060015F3 RID: 5619 RVA: 0x00038B40 File Offset: 0x00036D40
		public bool TryGetValue(ServerObjectHandle handle, out IServerObject serverObject, out ErrorCode errorCode)
		{
			if (handle == ServerObjectHandle.None)
			{
				errorCode = ErrorCode.NullObject;
				serverObject = null;
				return false;
			}
			if (handle.LogonIndex != this.logonIndex)
			{
				errorCode = (ErrorCode)2147942405U;
				serverObject = null;
				return false;
			}
			if (!this.objectMap.TryGetValue(handle, out serverObject) || serverObject == null)
			{
				errorCode = ErrorCode.NullObject;
				return false;
			}
			errorCode = ErrorCode.None;
			return true;
		}

		// Token: 0x060015F4 RID: 5620 RVA: 0x00038BA4 File Offset: 0x00036DA4
		public void ReleaseAndRemove(IRopHandler handler, ServerObjectHandle handleToRemove)
		{
			IServerObject serverObject;
			if (this.objectMap.TryGetValue(handleToRemove, out serverObject))
			{
				if (handler != null)
				{
					handler.Release(serverObject);
				}
				this.objectMap.Remove(handleToRemove);
			}
		}

		// Token: 0x060015F5 RID: 5621 RVA: 0x00038BD8 File Offset: 0x00036DD8
		public void ReleaseAll(IRopHandler handler)
		{
			ServerObjectHandle key = new ServerObjectHandle(this.logonIndex, 0U);
			IServerObject serverObject = null;
			if (this.objectMap.TryGetValue(key, out serverObject))
			{
				this.objectMap.Remove(key);
			}
			foreach (IServerObject serverObject2 in this.objectMap.Values)
			{
				handler.Release(serverObject2);
			}
			if (serverObject != null)
			{
				handler.Release(serverObject);
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x060015F6 RID: 5622 RVA: 0x00038C64 File Offset: 0x00036E64
		internal int HandleCount
		{
			get
			{
				return this.objectMap.Count;
			}
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x060015F7 RID: 5623 RVA: 0x00038C71 File Offset: 0x00036E71
		internal IDictionary<ServerObjectHandle, IServerObject> ObjectMap
		{
			get
			{
				return this.objectMap;
			}
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x060015F8 RID: 5624 RVA: 0x00038C79 File Offset: 0x00036E79
		public ServerObjectHandle NextHandleValue
		{
			get
			{
				return new ServerObjectHandle(this.logonIndex, this.counter);
			}
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x060015F9 RID: 5625 RVA: 0x00038C8C File Offset: 0x00036E8C
		public object LogonObject
		{
			get
			{
				return this.objectMap[this.LogonServerObjectHandle];
			}
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x060015FA RID: 5626 RVA: 0x00038C9F File Offset: 0x00036E9F
		private ServerObjectHandle LogonServerObjectHandle
		{
			get
			{
				if (this.logonServerObjectHandle == null)
				{
					this.logonServerObjectHandle = new ServerObjectHandle?(ServerObjectHandle.CreateLogonHandle(this.logonIndex));
				}
				return this.logonServerObjectHandle.Value;
			}
		}

		// Token: 0x060015FB RID: 5627 RVA: 0x00038CCF File Offset: 0x00036ECF
		private void IncrementCount()
		{
			this.counter = (this.counter + 1U) % 16777214U;
		}

		// Token: 0x060015FC RID: 5628 RVA: 0x00038CE8 File Offset: 0x00036EE8
		private ServerObjectHandle GetNextFreeHandle()
		{
			for (uint num = 0U; num < 16777214U; num += 1U)
			{
				ServerObjectHandle nextHandleValue = this.NextHandleValue;
				this.IncrementCount();
				if (!this.objectMap.ContainsKey(nextHandleValue))
				{
					return nextHandleValue;
				}
			}
			throw new RopExecutionException("No more handles in the ServerObjectMap.", ErrorCode.Memory);
		}

		// Token: 0x04000B6E RID: 2926
		private const int InvalidCounter = 16777215;

		// Token: 0x04000B6F RID: 2927
		internal const int MaxCounter = 16777214;

		// Token: 0x04000B70 RID: 2928
		private readonly IDictionary<ServerObjectHandle, IServerObject> objectMap;

		// Token: 0x04000B71 RID: 2929
		private readonly byte logonIndex;

		// Token: 0x04000B72 RID: 2930
		private uint counter;

		// Token: 0x04000B73 RID: 2931
		private ServerObjectHandle? logonServerObjectHandle;
	}
}
