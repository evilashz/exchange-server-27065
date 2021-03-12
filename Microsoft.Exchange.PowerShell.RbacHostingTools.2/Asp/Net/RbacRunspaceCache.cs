using System;
using System.Management.Automation.Runspaces;
using System.Web;
using System.Web.Caching;
using Microsoft.PowerShell.HostingTools;

namespace Microsoft.Exchange.PowerShell.RbacHostingTools.Asp.Net
{
	// Token: 0x0200000A RID: 10
	internal class RbacRunspaceCache : RunspaceCache
	{
		// Token: 0x06000043 RID: 67 RVA: 0x00003320 File Offset: 0x00001520
		protected virtual string GetSessionKey()
		{
			HttpContext.Current.Session["RunspaceWebCache"] = true;
			return "Exchange" + HttpContext.Current.Session.SessionID;
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00003355 File Offset: 0x00001555
		protected virtual DateTime AbsoluteExpiration
		{
			get
			{
				return Cache.NoAbsoluteExpiration;
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x0000335C File Offset: 0x0000155C
		protected override bool AddRunspace(Runspace runspace)
		{
			CacheDependency dependencies = null;
			string[] dependencyCacheKeys = this.GetDependencyCacheKeys();
			if (dependencyCacheKeys != null)
			{
				dependencies = new CacheDependency(null, dependencyCacheKeys);
			}
			return null == HttpRuntime.Cache.Add(this.GetSessionKey(), runspace, dependencies, this.AbsoluteExpiration, this.SlidingExpiration, CacheItemPriority.High, new CacheItemRemovedCallback(this.Cache_ItemRemoved));
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000033AB File Offset: 0x000015AB
		protected override Runspace RemoveRunspace()
		{
			return (Runspace)HttpRuntime.Cache.Remove(this.GetSessionKey());
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000033C4 File Offset: 0x000015C4
		private void Cache_ItemRemoved(string key, object value, CacheItemRemovedReason reason)
		{
			if (reason != CacheItemRemovedReason.Removed)
			{
				Runspace runspace = (Runspace)value;
				this.DisposeRunspace(runspace);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000048 RID: 72 RVA: 0x000033E5 File Offset: 0x000015E5
		protected virtual TimeSpan SlidingExpiration
		{
			get
			{
				return RbacSection.Instance.RbacRunspaceSlidingExpiration;
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000033F1 File Offset: 0x000015F1
		protected virtual string[] GetDependencyCacheKeys()
		{
			return RbacPrincipal.Current.CacheKeys;
		}

		// Token: 0x0400001D RID: 29
		protected const string RunspaceName = "Exchange";
	}
}
