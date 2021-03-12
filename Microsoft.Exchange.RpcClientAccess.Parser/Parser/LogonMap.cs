using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020001B7 RID: 439
	internal class LogonMap : BaseObject, IParseLogonTracker
	{
		// Token: 0x06000947 RID: 2375 RVA: 0x0001DC9A File Offset: 0x0001BE9A
		internal LogonMap(IConnectionHandler handler)
		{
			this.handler = handler;
			this.logonMap = new Dictionary<byte, LogonMap.Logon>();
			this.parseLogonMap = new Dictionary<byte, LogonFlags>();
			this.parseHandleTable = new Dictionary<byte, byte>();
			this.parseTrackingEnabled = false;
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000948 RID: 2376 RVA: 0x0001DCD1 File Offset: 0x0001BED1
		internal int LogonCount
		{
			get
			{
				return this.logonMap.Count;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000949 RID: 2377 RVA: 0x0001DCDE File Offset: 0x0001BEDE
		private IRopHandler RopHandler
		{
			get
			{
				return this.handler.RopHandler;
			}
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x0001DCEC File Offset: 0x0001BEEC
		internal ServerObjectMap CreateLogon(byte logonIndex, LogonFlags logonFlags)
		{
			LogonMap.Logon logon = new LogonMap.Logon(logonIndex, logonFlags);
			if (this.logonMap.ContainsKey(logonIndex))
			{
				this.ReleaseLogon(logonIndex);
			}
			this.logonMap.Add(logonIndex, logon);
			return logon.ServerObjectMap;
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x0001DD2C File Offset: 0x0001BF2C
		internal void ReleaseLogon(byte logonIndex)
		{
			LogonMap.Logon logon;
			if (this.logonMap.TryGetValue(logonIndex, out logon))
			{
				logon.ServerObjectMap.ReleaseAll(this.RopHandler);
			}
			this.logonMap.Remove(logonIndex);
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x0001DD68 File Offset: 0x0001BF68
		internal void ReleaseHandle(byte logonIndex, ServerObjectHandle handleToRelease)
		{
			if (handleToRelease.IsLogonHandle(logonIndex))
			{
				this.ReleaseLogon(logonIndex);
				return;
			}
			LogonMap.Logon logon;
			if (!this.logonMap.TryGetValue(logonIndex, out logon))
			{
				return;
			}
			logon.ServerObjectMap.ReleaseAndRemove(this.RopHandler, handleToRelease);
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x0001DDAC File Offset: 0x0001BFAC
		internal bool TryGetServerObjectMap(byte logonIndex, out ServerObjectMap serverObjectMap, out ErrorCode errorCode)
		{
			LogonMap.Logon logon;
			if (this.logonMap.TryGetValue(logonIndex, out logon))
			{
				errorCode = ErrorCode.None;
				serverObjectMap = logon.ServerObjectMap;
				return true;
			}
			errorCode = ErrorCode.NoSuchLogon;
			serverObjectMap = null;
			return false;
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x0001DDE4 File Offset: 0x0001BFE4
		internal bool TryGetServerObject(byte logonIndex, ServerObjectHandle handle, out IServerObject serverObject, out ErrorCode errorCode)
		{
			serverObject = null;
			ServerObjectMap serverObjectMap;
			return this.TryGetServerObjectMap(logonIndex, out serverObjectMap, out errorCode) && serverObjectMap.TryGetValue(handle, out serverObject, out errorCode);
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x0001DE10 File Offset: 0x0001C010
		public void ParseBegin(ServerObjectHandleTable serverObjectHandleTable)
		{
			this.parseLogonMap.Clear();
			this.parseHandleTable.Clear();
			this.parseTrackingEnabled = true;
			Dictionary<ServerObjectHandle, byte> dictionary = new Dictionary<ServerObjectHandle, byte>(this.logonMap.Count);
			foreach (KeyValuePair<byte, LogonMap.Logon> keyValuePair in this.logonMap)
			{
				byte key = keyValuePair.Key;
				LogonMap.Logon value = keyValuePair.Value;
				dictionary.Add(new ServerObjectHandle(key, 0U), key);
				this.parseLogonMap.Add(key, value.LogonFlags);
			}
			int highestIndexAccessed = serverObjectHandleTable.HighestIndexAccessed;
			byte b = 0;
			while ((int)b < serverObjectHandleTable.LastIndex)
			{
				byte value2;
				if (dictionary.TryGetValue(serverObjectHandleTable[(int)b], out value2))
				{
					this.parseHandleTable.Add(b, value2);
				}
				b += 1;
			}
			serverObjectHandleTable.HighestIndexAccessed = highestIndexAccessed;
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x0001DF04 File Offset: 0x0001C104
		public void ParseEnd()
		{
			this.parseLogonMap.Clear();
			this.parseHandleTable.Clear();
			this.parseTrackingEnabled = false;
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x0001DF23 File Offset: 0x0001C123
		public void ParseRecordLogon(byte logonIndex, byte handleTableIndex, LogonFlags logonFlags)
		{
			if (!this.parseTrackingEnabled)
			{
				throw new InvalidOperationException("Not currently in parsing state");
			}
			this.parseHandleTable[handleTableIndex] = logonIndex;
			this.parseLogonMap[logonIndex] = logonFlags;
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x0001DF54 File Offset: 0x0001C154
		public void ParseRecordRelease(byte handleTableIndex)
		{
			if (!this.parseTrackingEnabled)
			{
				throw new InvalidOperationException("Not currently in parsing state");
			}
			byte key;
			if (this.parseHandleTable.TryGetValue(handleTableIndex, out key))
			{
				this.parseLogonMap.Remove(key);
				this.parseHandleTable.Remove(handleTableIndex);
			}
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x0001DF9E File Offset: 0x0001C19E
		public void ParseRecordInputOutput(byte handleTableIndex)
		{
			if (!this.parseTrackingEnabled)
			{
				throw new InvalidOperationException("Not currently in parsing state");
			}
			this.parseHandleTable.Remove(handleTableIndex);
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x0001DFC0 File Offset: 0x0001C1C0
		public bool ParseIsValidLogon(byte logonIndex)
		{
			if (!this.parseTrackingEnabled)
			{
				throw new InvalidOperationException("Not currently in parsing state");
			}
			return this.parseLogonMap.ContainsKey(logonIndex);
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x0001DFE4 File Offset: 0x0001C1E4
		public bool ParseIsPublicLogon(byte logonIndex)
		{
			if (!this.parseTrackingEnabled)
			{
				throw new InvalidOperationException("Not currently in parsing state");
			}
			LogonFlags logonFlags;
			return this.parseLogonMap.TryGetValue(logonIndex, out logonFlags) && (byte)(logonFlags & LogonFlags.Private) == 0;
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x0001E01D File Offset: 0x0001C21D
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<LogonMap>(this);
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x0001E028 File Offset: 0x0001C228
		protected override void InternalDispose()
		{
			foreach (byte key in this.logonMap.Keys)
			{
				LogonMap.Logon logon;
				if (this.logonMap.TryGetValue(key, out logon))
				{
					logon.ServerObjectMap.ReleaseAll(this.RopHandler);
				}
			}
			base.InternalDispose();
		}

		// Token: 0x04000410 RID: 1040
		private readonly IConnectionHandler handler;

		// Token: 0x04000411 RID: 1041
		private readonly Dictionary<byte, LogonMap.Logon> logonMap;

		// Token: 0x04000412 RID: 1042
		private readonly Dictionary<byte, LogonFlags> parseLogonMap;

		// Token: 0x04000413 RID: 1043
		private readonly Dictionary<byte, byte> parseHandleTable;

		// Token: 0x04000414 RID: 1044
		private bool parseTrackingEnabled;

		// Token: 0x020001B8 RID: 440
		private class Logon
		{
			// Token: 0x06000958 RID: 2392 RVA: 0x0001E0A0 File Offset: 0x0001C2A0
			internal Logon(byte logonIndex, LogonFlags logonFlags)
			{
				this.serverObjectMap = new ServerObjectMap(logonIndex);
				this.logonFlags = logonFlags;
			}

			// Token: 0x1700017C RID: 380
			// (get) Token: 0x06000959 RID: 2393 RVA: 0x0001E0BB File Offset: 0x0001C2BB
			internal ServerObjectMap ServerObjectMap
			{
				get
				{
					return this.serverObjectMap;
				}
			}

			// Token: 0x1700017D RID: 381
			// (get) Token: 0x0600095A RID: 2394 RVA: 0x0001E0C3 File Offset: 0x0001C2C3
			internal LogonFlags LogonFlags
			{
				get
				{
					return this.logonFlags;
				}
			}

			// Token: 0x04000415 RID: 1045
			private readonly ServerObjectMap serverObjectMap;

			// Token: 0x04000416 RID: 1046
			private readonly LogonFlags logonFlags;
		}
	}
}
