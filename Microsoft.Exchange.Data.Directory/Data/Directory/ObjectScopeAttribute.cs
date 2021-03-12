using System;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000172 RID: 370
	[AttributeUsage(AttributeTargets.Class)]
	internal sealed class ObjectScopeAttribute : Attribute
	{
		// Token: 0x06000F9B RID: 3995 RVA: 0x0004A784 File Offset: 0x00048984
		public ObjectScopeAttribute(params ConfigScopes[] applicableScopes)
		{
			this.mainConfigScope = ConfigScopes.None;
			for (int i = 0; i < applicableScopes.Length; i++)
			{
				if (applicableScopes[i] == ConfigScopes.None)
				{
					throw new ArgumentOutOfRangeException("configScopes", "'None' is not a valid ConfigScope.");
				}
			}
			this.applicableScopes = applicableScopes;
		}

		// Token: 0x06000F9C RID: 3996 RVA: 0x0004A7CC File Offset: 0x000489CC
		public ObjectScopeAttribute(ConfigScopes configScope)
		{
			if (configScope == ConfigScopes.None)
			{
				throw new ArgumentOutOfRangeException("configScope", "'None' is not a valid ConfigScope.");
			}
			this.mainConfigScope = configScope;
			this.applicableScopes = new ConfigScopes[]
			{
				configScope
			};
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000F9D RID: 3997 RVA: 0x0004A80B File Offset: 0x00048A0B
		public ConfigScopes ConfigScope
		{
			get
			{
				return this.mainConfigScope;
			}
		}

		// Token: 0x06000F9E RID: 3998 RVA: 0x0004A828 File Offset: 0x00048A28
		public bool HasApplicableConfigScope(ConfigScopes configScope)
		{
			return Array.Exists<ConfigScopes>(this.applicableScopes, (ConfigScopes s) => s == configScope);
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000F9F RID: 3999 RVA: 0x0004A859 File Offset: 0x00048A59
		public bool IsTenant
		{
			get
			{
				return this.HasApplicableConfigScope(ConfigScopes.TenantSubTree);
			}
		}

		// Token: 0x04000921 RID: 2337
		private ConfigScopes mainConfigScope;

		// Token: 0x04000922 RID: 2338
		private ConfigScopes[] applicableScopes;
	}
}
