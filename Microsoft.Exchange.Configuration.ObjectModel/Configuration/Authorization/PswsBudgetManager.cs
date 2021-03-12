using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics.CmdletInfra;
using Microsoft.Exchange.Diagnostics.Components.Authorization;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Configuration.Authorization
{
	// Token: 0x02000217 RID: 535
	internal class PswsBudgetManager : BudgetManager
	{
		// Token: 0x060012AF RID: 4783 RVA: 0x0003C6E4 File Offset: 0x0003A8E4
		private PswsBudgetManager()
		{
			int num;
			if (!int.TryParse(ConfigurationManager.AppSettings["RunspaceCacheTimeoutSec"], out num))
			{
				num = 600;
			}
			this.pswsRunspaceCacheTimeout = TimeSpan.FromSeconds((double)(num + 60));
			this.budgetTimeout = this.pswsRunspaceCacheTimeout.Add(TimeSpan.FromSeconds(10.0));
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x060012B0 RID: 4784 RVA: 0x0003C762 File Offset: 0x0003A962
		internal static PswsBudgetManager Instance
		{
			get
			{
				return PswsBudgetManager.instance;
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x060012B1 RID: 4785 RVA: 0x0003C769 File Offset: 0x0003A969
		protected override TimeSpan BudgetTimeout
		{
			get
			{
				return this.budgetTimeout;
			}
		}

		// Token: 0x060012B2 RID: 4786 RVA: 0x0003C778 File Offset: 0x0003A978
		internal void StartRunspace(AuthZPluginUserToken userToken)
		{
			string runspaceCacheKey = this.GetRunspaceCacheKey(userToken);
			if (string.IsNullOrEmpty(runspaceCacheKey))
			{
				AuthZLogger.SafeAppendGenericError("NullOrEmptyRunspaceCacheKey", "User token have an empty ExecutingUserName", false);
				return;
			}
			lock (base.InstanceLock)
			{
				RunspaceCacheValue runspaceCacheValue;
				if (this.runspaceCache.TryGetValue(runspaceCacheKey, out runspaceCacheValue))
				{
					ExTraceGlobals.PublicPluginAPITracer.TraceDebug<string>((long)this.GetHashCode(), "[PswsBudgetManager.StartRunspace] item {0} is removed explicitly", runspaceCacheKey);
					if (runspaceCacheValue != null && runspaceCacheValue.CostHandle != null)
					{
						runspaceCacheValue.CostHandle.Dispose();
					}
					this.runspaceCache.Remove(runspaceCacheKey);
				}
				CostHandle costHandle = this.StartRunspaceImpl(userToken);
				RunspaceCacheValue value2 = new RunspaceCacheValue
				{
					CostHandle = costHandle,
					UserToken = (PswsAuthZUserToken)userToken
				};
				ExTraceGlobals.PublicPluginAPITracer.TraceDebug<string, TimeSpan>((long)this.GetHashCode(), "[PswsBudgetManager.StartRunspace] Add value {0} to runspace cache. Expired time = {1}.", runspaceCacheKey, this.pswsRunspaceCacheTimeout);
				this.runspaceCache.InsertAbsolute(runspaceCacheKey, value2, this.pswsRunspaceCacheTimeout, new RemoveItemDelegate<string, RunspaceCacheValue>(this.OnRunspaceCacheItemExpired));
			}
			ExTraceGlobals.PublicPluginAPITracer.TraceDebug<string>((long)this.GetHashCode(), "[PswsBudgetManager.StartRunspace] Add/Update value {0} to connectedUser cache.", runspaceCacheKey);
			this.connectedUsers.AddOrUpdate(runspaceCacheKey, ExDateTime.Now, (string key, ExDateTime value) => ExDateTime.Now);
			AuthZPluginHelper.UpdateAuthZPluginPerfCounters(this);
		}

		// Token: 0x060012B3 RID: 4787 RVA: 0x0003C8D0 File Offset: 0x0003AAD0
		internal string GetConnectedUsers()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (KeyValuePair<string, ExDateTime> keyValuePair in this.connectedUsers)
			{
				stringBuilder.Append(keyValuePair.Key);
				stringBuilder.Append(',');
				stringBuilder.Append(keyValuePair.Value);
				stringBuilder.Append(',');
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060012B4 RID: 4788 RVA: 0x0003C958 File Offset: 0x0003AB58
		protected override void HeartBeatImpl(AuthZPluginUserToken userToken)
		{
			base.HeartBeatImpl(userToken);
			string runspaceCacheKey = this.GetRunspaceCacheKey(userToken);
			lock (base.InstanceLock)
			{
				RunspaceCacheValue value;
				if (!this.runspaceCache.TryGetValue(runspaceCacheKey, out value))
				{
					ExTraceGlobals.PublicPluginAPITracer.TraceError<string>((long)this.GetHashCode(), "[PswsBudgetManager.HeartBeatImpl] Unexpected: User {0} is not in the runspace cache in heart-beat.", runspaceCacheKey);
				}
				else
				{
					this.runspaceCache.InsertAbsolute(runspaceCacheKey, value, this.pswsRunspaceCacheTimeout, new RemoveItemDelegate<string, RunspaceCacheValue>(this.OnRunspaceCacheItemExpired));
				}
			}
		}

		// Token: 0x060012B5 RID: 4789 RVA: 0x0003C9EC File Offset: 0x0003ABEC
		private string GetRunspaceCacheKey(AuthZPluginUserToken userToken)
		{
			return ((PswsAuthZUserToken)userToken).ExecutingUserName;
		}

		// Token: 0x060012B6 RID: 4790 RVA: 0x0003C9FC File Offset: 0x0003ABFC
		private void OnRunspaceCacheItemExpired(string key, RunspaceCacheValue value, RemoveReason reason)
		{
			ExTraceGlobals.PublicPluginAPITracer.TraceDebug<string, RemoveReason>((long)this.GetHashCode(), "[PswsBudgetManager.OnRunspaceCacheItemExpired] item {0} is removed. Reason = {1}", key, reason);
			if (reason != RemoveReason.Removed)
			{
				try
				{
					if (value != null)
					{
						if (value.CostHandle != null)
						{
							value.CostHandle.Dispose();
						}
						else
						{
							ExTraceGlobals.PublicPluginAPITracer.TraceDebug((long)this.GetHashCode(), "[PswsBudgetManager.OnRunspaceCacheItemExpired] value.CostHandle = null");
						}
						if (value.UserToken == null)
						{
							ExTraceGlobals.PublicPluginAPITracer.TraceDebug((long)this.GetHashCode(), "[PswsBudgetManager.OnRunspaceCacheItemExpired] value.UserToken = null");
						}
						else
						{
							base.RemoveBudgetIfNoActiveRunspace(value.UserToken);
							string runspaceCacheKey = this.GetRunspaceCacheKey(value.UserToken);
							ExDateTime exDateTime;
							if (runspaceCacheKey != null && this.connectedUsers.TryRemove(runspaceCacheKey, out exDateTime))
							{
								ExTraceGlobals.PublicPluginAPITracer.TraceDebug<string>((long)this.GetHashCode(), "[PswsBudgetManager.OnRunspaceCacheItemExpired] User {0} is removed from connectedUsers cache.", runspaceCacheKey);
							}
						}
					}
				}
				finally
				{
					AuthZPluginHelper.UpdateAuthZPluginPerfCounters(this);
				}
			}
		}

		// Token: 0x04000491 RID: 1169
		private readonly TimeSpan pswsRunspaceCacheTimeout;

		// Token: 0x04000492 RID: 1170
		private readonly TimeSpan budgetTimeout;

		// Token: 0x04000493 RID: 1171
		private readonly TimeoutCache<string, RunspaceCacheValue> runspaceCache = new TimeoutCache<string, RunspaceCacheValue>(20, 5000, false);

		// Token: 0x04000494 RID: 1172
		private readonly ConcurrentDictionary<string, ExDateTime> connectedUsers = new ConcurrentDictionary<string, ExDateTime>();

		// Token: 0x04000495 RID: 1173
		private static readonly PswsBudgetManager instance = new PswsBudgetManager();
	}
}
