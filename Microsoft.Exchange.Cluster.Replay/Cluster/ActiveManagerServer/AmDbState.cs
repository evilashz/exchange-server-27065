using System;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x02000060 RID: 96
	internal abstract class AmDbState : IAmDbState, IDisposable
	{
		// Token: 0x06000410 RID: 1040 RVA: 0x00015F40 File Offset: 0x00014140
		internal static string ConstructLastLogTimeStampProperty(string prefix)
		{
			return prefix + "_Time";
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x00015F4D File Offset: 0x0001414D
		internal AmDbState()
		{
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x00015F55 File Offset: 0x00014155
		public void Dispose()
		{
			if (!this.m_fDisposed)
			{
				this.Dispose(true);
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x00015F6C File Offset: 0x0001416C
		public void Dispose(bool disposing)
		{
			lock (this)
			{
				if (!this.m_fDisposed)
				{
					if (disposing)
					{
						this.CloseHandles();
					}
					this.m_fDisposed = true;
				}
			}
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x00015FBC File Offset: 0x000141BC
		public void Write(AmDbStateInfo state)
		{
			this.Write(state, false);
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x00015FC8 File Offset: 0x000141C8
		internal bool Write(AmDbStateInfo state, bool isBestEffort)
		{
			bool result = false;
			try
			{
				ActiveManagerServerPerfmon.DatabaseStateInfoWrites.Increment();
				ActiveManagerServerPerfmon.DatabaseStateInfoWritesPerSec.Increment();
				this.WriteInternal(state.DatabaseGuid.ToString(), state.ToString(), state.ActiveServer);
				result = true;
			}
			catch (ClusterException ex)
			{
				AmTrace.Error("Attempt write persistent database state for Database '{0}' failed with error: {1}", new object[]
				{
					state.DatabaseGuid,
					ex
				});
				if (!isBestEffort)
				{
					throw;
				}
			}
			return result;
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x00016054 File Offset: 0x00014254
		public AmDbStateInfo Read(Guid dbGuid)
		{
			return this.Read(dbGuid, false);
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0001605E File Offset: 0x0001425E
		public AmDbStateInfo[] ReadAll()
		{
			return this.ReadAll(false);
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x00016068 File Offset: 0x00014268
		public string ReadStateString(Guid dbGuid)
		{
			string result;
			this.ReadInternal(dbGuid.ToString(), out result);
			return result;
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0001608C File Offset: 0x0001428C
		internal AmDbStateInfo[] ReadAll(bool isBestEffort)
		{
			return this.ReadAllInternal(isBestEffort);
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x00016098 File Offset: 0x00014298
		internal AmDbStateInfo Read(Guid dbGuid, bool isBestEffort)
		{
			AmDbStateInfo result = new AmDbStateInfo(dbGuid);
			string empty = string.Empty;
			try
			{
				bool flag = this.ReadInternal(dbGuid.ToString(), out empty);
				if (flag && !string.IsNullOrEmpty(empty))
				{
					result = AmDbStateInfo.Parse(dbGuid, empty);
				}
			}
			catch (FormatException ex)
			{
				AmTrace.Error("Failed to parse the state info string '{1}' for Database '{0}'. Error: {2}", new object[]
				{
					dbGuid,
					empty,
					ex
				});
				if (!isBestEffort)
				{
					throw new AmInvalidDbStateException(dbGuid, empty, ex);
				}
			}
			catch (ClusterException ex2)
			{
				AmTrace.Error("Attempt read persistent database state for Database '{0}' failed with error: {1}", new object[]
				{
					dbGuid,
					ex2
				});
				if (!isBestEffort)
				{
					throw;
				}
			}
			return result;
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x00016160 File Offset: 0x00014360
		internal void Delete(Guid dbGuid)
		{
			this.DeleteInternal(dbGuid.ToString());
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x00016175 File Offset: 0x00014375
		public void SetLastLogGenerationNumber(Guid dbGuid, long generation)
		{
			this.SetLastLogPropertyInternal(dbGuid.ToString(), generation.ToString());
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x00016194 File Offset: 0x00014394
		public bool GetLastLogGenerationNumber(Guid dbGuid, out long lastLogGenNumber)
		{
			lastLogGenNumber = long.MaxValue;
			string text = dbGuid.ToString();
			string text2;
			bool lastLogPropertyInternal = this.GetLastLogPropertyInternal(text, out text2);
			if (lastLogPropertyInternal && !long.TryParse(text2, out lastLogGenNumber))
			{
				AmTrace.Error("GetLastLogPropertyInternal() returned a value that could not be parsed. DB: '{0}'; Value: {1}", new object[]
				{
					text,
					lastLogGenNumber
				});
				throw new AmLastLogPropertyCorruptedException(text, text2);
			}
			return lastLogPropertyInternal;
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x000161FC File Offset: 0x000143FC
		public void SetLastLogGenerationTimeStamp(Guid dbGuid, ExDateTime timeStamp)
		{
			string name = AmDbState.ConstructLastLogTimeStampProperty(dbGuid.ToString());
			this.SetLastLogPropertyInternal(name, timeStamp.ToString("s"));
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x00016230 File Offset: 0x00014430
		public bool GetLastLogGenerationTimeStamp(Guid dbGuid, out ExDateTime lastLogGenTimeStamp)
		{
			string text = AmDbState.ConstructLastLogTimeStampProperty(dbGuid.ToString());
			string empty = string.Empty;
			lastLogGenTimeStamp = ExDateTime.MinValue;
			bool lastLogPropertyInternal = this.GetLastLogPropertyInternal(text, out empty);
			if (lastLogPropertyInternal && !ExDateTime.TryParse(empty, out lastLogGenTimeStamp))
			{
				AmTrace.Error("GetLastLogPropertyInternal() returned a value that could not be parsed. DB: '{0}'; Value: {1}", new object[]
				{
					text,
					empty
				});
				throw new AmLastLogPropertyCorruptedException(text, empty);
			}
			return lastLogPropertyInternal;
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0001629C File Offset: 0x0001449C
		public bool GetLastServerTimeStamp(string serverName, out ExDateTime lastServerTimeStamp)
		{
			string empty = string.Empty;
			lastServerTimeStamp = ExDateTime.MinValue;
			bool lastLogPropertyInternal = this.GetLastLogPropertyInternal(serverName, out empty);
			if (lastLogPropertyInternal && !ExDateTime.TryParse(empty, out lastServerTimeStamp))
			{
				AmTrace.Error("GetLastLogPropertyInternal() returned a value that could not be parsed. serverName: '{0}'; Value: {1}", new object[]
				{
					serverName,
					empty
				});
				throw new AmLastServerTimeStampCorruptedException(serverName, empty);
			}
			return lastLogPropertyInternal;
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x000162F4 File Offset: 0x000144F4
		public T GetDebugOption<T>(AmServerName serverName, string propertyName, T defaultValue)
		{
			bool flag = false;
			return this.GetDebugOption<T>(serverName, propertyName, defaultValue, out flag);
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x00016310 File Offset: 0x00014510
		public T GetDebugOption<T>(AmServerName serverName, AmDebugOptions dbgOption, T defaultValue)
		{
			bool flag = false;
			return this.GetDebugOption<T>(serverName, dbgOption.ToString(), defaultValue, out flag);
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x00016334 File Offset: 0x00014534
		public T GetDebugOption<T>(AmServerName serverName, string propertyName, T defaultValue, out bool doesValueExist)
		{
			string serverName2 = (serverName != null) ? serverName.NetbiosName : null;
			if (defaultValue is bool)
			{
				int debugOptionInternal = this.GetDebugOptionInternal<int>(serverName2, propertyName, Convert.ToInt32(defaultValue), out doesValueExist);
				return (T)((object)(debugOptionInternal > 0));
			}
			return this.GetDebugOptionInternal<T>(serverName2, propertyName, defaultValue, out doesValueExist);
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x00016390 File Offset: 0x00014590
		internal bool SetDebugOption<T>(AmServerName serverName, string propertyName, T propertyValue)
		{
			string serverName2 = (serverName != null) ? serverName.NetbiosName : null;
			if (propertyValue is bool)
			{
				return this.SetDebugOptionInternal<int>(serverName2, propertyName, Convert.ToInt32(propertyValue));
			}
			return this.SetDebugOptionInternal<T>(serverName2, propertyName, propertyValue);
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x000163D4 File Offset: 0x000145D4
		internal Guid[] ConvertGuidStringsToGuids(string[] dbGuidStrings)
		{
			Guid[] array = null;
			if (dbGuidStrings != null)
			{
				array = new Guid[dbGuidStrings.Length];
				for (int i = 0; i < dbGuidStrings.Length; i++)
				{
					array[i] = new Guid(dbGuidStrings[i]);
				}
			}
			return array;
		}

		// Token: 0x06000426 RID: 1062
		protected abstract void InitializeHandles();

		// Token: 0x06000427 RID: 1063
		protected abstract void CloseHandles();

		// Token: 0x06000428 RID: 1064
		protected abstract void WriteInternal(string guidStr, string stateInfoStr, AmServerName activeServerName);

		// Token: 0x06000429 RID: 1065
		protected abstract bool ReadInternal(string guidStr, out string stateInfoStr);

		// Token: 0x0600042A RID: 1066
		protected abstract Guid[] ReadDatabaseGuids(bool isBestEffort);

		// Token: 0x0600042B RID: 1067
		protected abstract void DeleteInternal(string guidStr);

		// Token: 0x0600042C RID: 1068
		protected abstract void SetLastLogPropertyInternal(string name, string value);

		// Token: 0x0600042D RID: 1069
		protected abstract bool GetLastLogPropertyInternal(string name, out string value);

		// Token: 0x0600042E RID: 1070
		protected abstract T GetDebugOptionInternal<T>(string serverName, string propertyName, T defaultValue, out bool doesValueExist);

		// Token: 0x0600042F RID: 1071
		protected abstract bool SetDebugOptionInternal<T>(string serverName, string propertyName, T propertyValue);

		// Token: 0x06000430 RID: 1072
		protected abstract AmDbStateInfo[] ReadAllInternal(bool isBestEffort);

		// Token: 0x040001D4 RID: 468
		protected const string DbStateKeyName = "DbState";

		// Token: 0x040001D5 RID: 469
		protected const string DebugOptionKeyName = "DebugOption";

		// Token: 0x040001D6 RID: 470
		private bool m_fDisposed;
	}
}
