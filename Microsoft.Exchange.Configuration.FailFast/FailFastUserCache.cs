using System;
using System.Diagnostics;
using System.Text;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Diagnostics.Components.FailFast;

namespace Microsoft.Exchange.Configuration.FailFast
{
	// Token: 0x02000008 RID: 8
	internal abstract class FailFastUserCache : TimeoutCache<string, FailFastUserCacheValue>
	{
		// Token: 0x06000018 RID: 24 RVA: 0x00002A38 File Offset: 0x00000C38
		static FailFastUserCache()
		{
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				FailFastUserCache.pipeNameOfThisProcess = "M.E.C.FailFast.FailFastUserCache.NamedPipe." + currentProcess.Id;
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002A94 File Offset: 0x00000C94
		protected FailFastUserCache(int numberOfBuckets, int maxSizeForBuckets) : base(numberOfBuckets, maxSizeForBuckets, false)
		{
			AppDomain.CurrentDomain.DomainUnload += this.CurrentDomainDomainUnload;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002AB5 File Offset: 0x00000CB5
		// (set) Token: 0x0600001B RID: 27 RVA: 0x00002AD0 File Offset: 0x00000CD0
		internal static bool IsPrimaryUserCache
		{
			get
			{
				return FailFastUserCache.isPrimaryUserCache != null && FailFastUserCache.isPrimaryUserCache.Value;
			}
			set
			{
				Logger.TraceDebug(ExTraceGlobals.FailFastCacheTracer, "Set IsPrimaryUserCache to be {0}.", new object[]
				{
					value
				});
				FailFastUserCache.isPrimaryUserCache = new bool?(value);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002B08 File Offset: 0x00000D08
		internal static FailFastUserCache Instance
		{
			get
			{
				if (!FailFastUserCache.IsPrimaryUserCache)
				{
					return PassiveFailFastUserCache.Singleton;
				}
				return PrimaryFailFastUserCache.Singleton;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002B1C File Offset: 0x00000D1C
		// (set) Token: 0x0600001E RID: 30 RVA: 0x00002B24 File Offset: 0x00000D24
		internal static bool FailFastEnabled
		{
			get
			{
				return FailFastUserCache.failFastEnabled;
			}
			set
			{
				Logger.TraceDebug(ExTraceGlobals.FailFastCacheTracer, "Set FailFastEnabled to be {0}.", new object[]
				{
					value
				});
				FailFastUserCache.failFastEnabled = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002B57 File Offset: 0x00000D57
		protected static string PipeNameOfThisProcess
		{
			get
			{
				return FailFastUserCache.pipeNameOfThisProcess;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002B5E File Offset: 0x00000D5E
		protected static Encoding Encoding
		{
			get
			{
				return FailFastUserCache.encoding;
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002B65 File Offset: 0x00000D65
		internal void AddUserToCache(string userToken, BlockedType blockedType, TimeSpan blockedTime)
		{
			if (!FailFastUserCache.FailFastEnabled)
			{
				return;
			}
			Logger.EnterFunction(ExTraceGlobals.FailFastCacheTracer, "FailFastUserCache.BlockUser");
			this.InsertValueToCache(userToken, blockedType, blockedTime);
			Logger.ExitFunction(ExTraceGlobals.FailFastCacheTracer, "FailFastUserCache.BlockUser");
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002B96 File Offset: 0x00000D96
		internal void AddTenantToCache(string tenant, BlockedType blockedType, TimeSpan blockedTime)
		{
			if (!FailFastUserCache.FailFastEnabled)
			{
				return;
			}
			Logger.EnterFunction(ExTraceGlobals.FailFastCacheTracer, "FailFastUserCache.BlockTenant");
			this.InsertValueToCache("Tenant:C8E2A9F6-0E7A-4bcc-95A0-9CE1BCA7EE68:" + tenant, blockedType, blockedTime);
			Logger.ExitFunction(ExTraceGlobals.FailFastCacheTracer, "FailFastUserCache.BlockTenant");
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002BD1 File Offset: 0x00000DD1
		internal void AddAllUsersToCache(BlockedType blockedType, TimeSpan blockedTime)
		{
			if (!FailFastUserCache.FailFastEnabled)
			{
				return;
			}
			Logger.EnterFunction(ExTraceGlobals.FailFastCacheTracer, "FailFastUserCache.BlockAllUsers");
			this.InsertValueToCache("AllUsers:D3511BCA-379C-4a38-97E5-0FDA0C231C33", blockedType, blockedTime);
			Logger.ExitFunction(ExTraceGlobals.FailFastCacheTracer, "FailFastUserCache.BlockAllUsers");
		}

		// Token: 0x06000024 RID: 36
		internal abstract bool IsUserInCache(string userToken, string userTenant, out string cacheKey, out FailFastUserCacheValue cacheValue, out BlockedReason blockedReason);

		// Token: 0x06000025 RID: 37
		protected abstract void InsertValueToCache(string key, BlockedType blockedType, TimeSpan blockedTime);

		// Token: 0x06000026 RID: 38 RVA: 0x00002C06 File Offset: 0x00000E06
		private void CurrentDomainDomainUnload(object sender, EventArgs e)
		{
			base.Dispose();
		}

		// Token: 0x04000014 RID: 20
		protected const char BlockedInfoSeparator = ';';

		// Token: 0x04000015 RID: 21
		protected const string TenantCacheKeyPrefix = "Tenant:C8E2A9F6-0E7A-4bcc-95A0-9CE1BCA7EE68:";

		// Token: 0x04000016 RID: 22
		protected const string AllUsersKey = "AllUsers:D3511BCA-379C-4a38-97E5-0FDA0C231C33";

		// Token: 0x04000017 RID: 23
		private static readonly string pipeNameOfThisProcess;

		// Token: 0x04000018 RID: 24
		private static readonly Encoding encoding = Encoding.UTF8;

		// Token: 0x04000019 RID: 25
		private static bool? isPrimaryUserCache;

		// Token: 0x0400001A RID: 26
		private static bool failFastEnabled = false;
	}
}
