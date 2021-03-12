using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Principal;
using System.ServiceModel;
using System.Threading;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics.Components.Management.ControlPanel;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.PowerShell.RbacHostingTools
{
	// Token: 0x02000006 RID: 6
	internal class RbacPrincipal : IPrincipal, IIdentity, IExtension<OperationContext>, IIsInRole
	{
		// Token: 0x06000016 RID: 22 RVA: 0x00002E88 File Offset: 0x00001088
		public RbacPrincipal(ExchangeRunspaceConfiguration roles, string cacheKey)
		{
			if (roles == null)
			{
				throw new ArgumentNullException("roles");
			}
			if (string.IsNullOrEmpty(cacheKey))
			{
				throw new ArgumentNullException("cacheKey");
			}
			this.RbacConfiguration = roles;
			ADObjectId executingUserId;
			roles.TryGetExecutingUserId(out executingUserId);
			this.ExecutingUserId = executingUserId;
			MultiValuedProperty<CultureInfo> executingUserLanguages = this.RbacConfiguration.ExecutingUserLanguages;
			if (executingUserLanguages.Count > 0)
			{
				this.UserCulture = executingUserLanguages[0];
			}
			this.cacheKeys = new string[]
			{
				cacheKey
			};
			this.Name = this.RbacConfiguration.ExecutingUserDisplayName;
			if (string.IsNullOrEmpty(this.Name))
			{
				this.Name = this.ExecutingUserId.Name;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002F5C File Offset: 0x0000115C
		public static RbacPrincipal Current
		{
			get
			{
				return RbacPrincipal.GetCurrent(true);
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002F64 File Offset: 0x00001164
		public static RbacPrincipal GetCurrent(bool throwOnInvalidValue)
		{
			OperationContext operationContext = OperationContext.Current;
			if (operationContext != null)
			{
				return operationContext.GetRbacPrincipal();
			}
			RbacPrincipal.PrincipalWrapper principalWrapper = Thread.CurrentPrincipal as RbacPrincipal.PrincipalWrapper;
			if (throwOnInvalidValue)
			{
				return (Thread.CurrentPrincipal as RbacPrincipal) ?? ((RbacPrincipal)((RbacPrincipal.PrincipalWrapper)Thread.CurrentPrincipal).Principal);
			}
			if (principalWrapper != null)
			{
				return principalWrapper.Principal as RbacPrincipal;
			}
			return Thread.CurrentPrincipal as RbacPrincipal;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002FCB File Offset: 0x000011CB
		public static string[] SplitRoles(string roleString)
		{
			return roleString.Split(RbacPrincipal.andSeparatorList, StringSplitOptions.RemoveEmptyEntries);
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002FD9 File Offset: 0x000011D9
		public IIdentity Identity
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002FDC File Offset: 0x000011DC
		// (set) Token: 0x0600001C RID: 28 RVA: 0x00002FE4 File Offset: 0x000011E4
		public string Name { get; internal set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002FED File Offset: 0x000011ED
		public string NameForEventLog
		{
			get
			{
				return this.RbacConfiguration.IdentityName;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002FFA File Offset: 0x000011FA
		public string UniqueName
		{
			get
			{
				return this.RbacConfiguration.IdentityName;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00003007 File Offset: 0x00001207
		// (set) Token: 0x06000020 RID: 32 RVA: 0x0000300F File Offset: 0x0000120F
		public ExchangeRunspaceConfiguration RbacConfiguration { get; private set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00003018 File Offset: 0x00001218
		// (set) Token: 0x06000022 RID: 34 RVA: 0x00003020 File Offset: 0x00001220
		public ADObjectId ExecutingUserId { get; private set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00003029 File Offset: 0x00001229
		// (set) Token: 0x06000024 RID: 36 RVA: 0x00003031 File Offset: 0x00001231
		public virtual string DateFormat { get; protected set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000025 RID: 37 RVA: 0x0000303A File Offset: 0x0000123A
		// (set) Token: 0x06000026 RID: 38 RVA: 0x00003042 File Offset: 0x00001242
		public virtual string TimeFormat { get; protected set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000027 RID: 39 RVA: 0x0000304B File Offset: 0x0000124B
		// (set) Token: 0x06000028 RID: 40 RVA: 0x00003053 File Offset: 0x00001253
		public virtual ExTimeZone UserTimeZone { get; protected set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000029 RID: 41 RVA: 0x0000305C File Offset: 0x0000125C
		// (set) Token: 0x0600002A RID: 42 RVA: 0x00003064 File Offset: 0x00001264
		protected CultureInfo UserCulture { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600002B RID: 43 RVA: 0x0000306D File Offset: 0x0000126D
		public object OwaOptionsLock
		{
			get
			{
				return this.owaOptionsLock;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00003075 File Offset: 0x00001275
		public string[] CacheKeys
		{
			get
			{
				return this.cacheKeys;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600002D RID: 45 RVA: 0x0000307D File Offset: 0x0000127D
		public bool IsAdmin
		{
			get
			{
				return this.RbacConfiguration.HasAdminRoles;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600002E RID: 46 RVA: 0x0000308A File Offset: 0x0000128A
		string IIdentity.AuthenticationType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600002F RID: 47 RVA: 0x0000308D File Offset: 0x0000128D
		bool IIdentity.IsAuthenticated
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00003090 File Offset: 0x00001290
		public bool IsInRole(string role)
		{
			return this.IsInRole(role, null);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000309C File Offset: 0x0000129C
		public bool IsInRole(string role, ADRawEntry adRawEntry)
		{
			if (string.IsNullOrEmpty(role))
			{
				throw new ArgumentNullException("role");
			}
			bool flag = false;
			string[] array = RbacPrincipal.SplitRoles(role);
			foreach (string text in array)
			{
				this.roleCacheLock.AcquireReaderLock(-1);
				try
				{
					if (adRawEntry != null || RbacQuery.LegacyIsScoped || !this.roleCache.TryGetValue(text, out flag))
					{
						bool flag2;
						flag = this.IsInRole(text, out flag2, adRawEntry);
						if (flag2)
						{
							LockCookie lockCookie = this.roleCacheLock.UpgradeToWriterLock(-1);
							try
							{
								this.roleCache[text] = flag;
							}
							finally
							{
								this.roleCacheLock.DowngradeFromWriterLock(ref lockCookie);
							}
						}
					}
				}
				finally
				{
					this.roleCacheLock.ReleaseReaderLock();
				}
				if (!flag)
				{
					ExTraceGlobals.RBACTracer.TraceInformation<string, string, string>(this.GetHashCode(), 0L, "RbacPrincipal({0}).IsInRole({1}) returning false because {2} is not granted.", this.NameForEventLog, role, text);
					break;
				}
			}
			return flag;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000319C File Offset: 0x0000139C
		public virtual void SetCurrentThreadPrincipal()
		{
			Thread.CurrentPrincipal = this.GetWrapperPrincipal();
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000031A9 File Offset: 0x000013A9
		internal IPrincipal GetWrapperPrincipal()
		{
			if (this.wrapper == null)
			{
				Interlocked.CompareExchange<RbacPrincipal.PrincipalWrapper>(ref this.wrapper, new RbacPrincipal.PrincipalWrapper(this), null);
			}
			return this.wrapper;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000031CC File Offset: 0x000013CC
		void IExtension<OperationContext>.Attach(OperationContext owner)
		{
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000031CE File Offset: 0x000013CE
		void IExtension<OperationContext>.Detach(OperationContext owner)
		{
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000031D0 File Offset: 0x000013D0
		protected virtual bool IsInRole(string rbacQuery, out bool canCache, ADRawEntry adRawEntry)
		{
			RbacQuery rbacQuery2 = new RbacQuery(rbacQuery, adRawEntry);
			bool result = rbacQuery2.IsInRole(this.RbacConfiguration);
			canCache = rbacQuery2.CanCache;
			return result;
		}

		// Token: 0x04000007 RID: 7
		public const string EnterpriseRole = "Enterprise";

		// Token: 0x04000008 RID: 8
		public const string LiveIDRole = "LiveID";

		// Token: 0x04000009 RID: 9
		public const string MultiTenantRole = "MultiTenant";

		// Token: 0x0400000A RID: 10
		private const char AndSeparator = '+';

		// Token: 0x0400000B RID: 11
		private static char[] andSeparatorList = new char[]
		{
			'+'
		};

		// Token: 0x0400000C RID: 12
		private object owaOptionsLock = new object();

		// Token: 0x0400000D RID: 13
		private Dictionary<string, bool> roleCache = new Dictionary<string, bool>(StringComparer.InvariantCultureIgnoreCase);

		// Token: 0x0400000E RID: 14
		private ReaderWriterLock roleCacheLock = new ReaderWriterLock();

		// Token: 0x0400000F RID: 15
		private string[] cacheKeys;

		// Token: 0x04000010 RID: 16
		private RbacPrincipal.PrincipalWrapper wrapper;

		// Token: 0x02000007 RID: 7
		private class PrincipalWrapper : IPrincipal
		{
			// Token: 0x06000038 RID: 56 RVA: 0x0000321F File Offset: 0x0000141F
			public PrincipalWrapper(IPrincipal principal)
			{
				this.refPricipal = new WeakReference(principal);
			}

			// Token: 0x17000013 RID: 19
			// (get) Token: 0x06000039 RID: 57 RVA: 0x00003234 File Offset: 0x00001434
			IIdentity IPrincipal.Identity
			{
				get
				{
					IPrincipal principal = this.Principal;
					if (principal != null)
					{
						return principal.Identity;
					}
					return null;
				}
			}

			// Token: 0x0600003A RID: 58 RVA: 0x00003254 File Offset: 0x00001454
			bool IPrincipal.IsInRole(string role)
			{
				IPrincipal principal = this.Principal;
				return principal != null && principal.IsInRole(role);
			}

			// Token: 0x17000014 RID: 20
			// (get) Token: 0x0600003B RID: 59 RVA: 0x00003274 File Offset: 0x00001474
			public IPrincipal Principal
			{
				get
				{
					return (IPrincipal)this.refPricipal.Target;
				}
			}

			// Token: 0x04000018 RID: 24
			private readonly WeakReference refPricipal;
		}
	}
}
