using System;

namespace AjaxControlToolkit
{
	// Token: 0x02000026 RID: 38
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = true)]
	public sealed class ScriptCombineAttribute : Attribute
	{
		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00004C00 File Offset: 0x00002E00
		// (set) Token: 0x06000153 RID: 339 RVA: 0x00004C08 File Offset: 0x00002E08
		public string ExcludeScripts
		{
			get
			{
				return this.excludeScripts;
			}
			set
			{
				this.excludeScripts = value;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00004C11 File Offset: 0x00002E11
		// (set) Token: 0x06000155 RID: 341 RVA: 0x00004C19 File Offset: 0x00002E19
		public string IncludeScripts
		{
			get
			{
				return this.includeScripts;
			}
			set
			{
				this.includeScripts = value;
			}
		}

		// Token: 0x04000049 RID: 73
		private string excludeScripts;

		// Token: 0x0400004A RID: 74
		private string includeScripts;
	}
}
