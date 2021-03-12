using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AjaxControlToolkit
{
	// Token: 0x02000029 RID: 41
	public class CombinableScripts
	{
		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000175 RID: 373 RVA: 0x000059B2 File Offset: 0x00003BB2
		// (set) Token: 0x06000176 RID: 374 RVA: 0x000059BA File Offset: 0x00003BBA
		public ScriptsEntries Scripts { get; set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000177 RID: 375 RVA: 0x000059C3 File Offset: 0x00003BC3
		// (set) Token: 0x06000178 RID: 376 RVA: 0x000059CB File Offset: 0x00003BCB
		public string Alias { get; set; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000179 RID: 377 RVA: 0x000059D4 File Offset: 0x00003BD4
		// (set) Token: 0x0600017A RID: 378 RVA: 0x000059DC File Offset: 0x00003BDC
		public int Rank { get; set; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600017B RID: 379 RVA: 0x000059E8 File Offset: 0x00003BE8
		public bool HasScriptResources
		{
			get
			{
				foreach (ScriptEntry scriptEntry in this.Scripts)
				{
					if (!scriptEntry.SkipScriptResources && scriptEntry.HasScriptResources)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600017C RID: 380 RVA: 0x00005A48 File Offset: 0x00003C48
		// (set) Token: 0x0600017D RID: 381 RVA: 0x00005A50 File Offset: 0x00003C50
		[TypeConverter(typeof(StringArrayConverter))]
		public string[] DependsOn { get; set; }

		// Token: 0x0600017E RID: 382 RVA: 0x00005A59 File Offset: 0x00003C59
		public ScriptReference ToScriptReference()
		{
			return new ScriptReference(this.Scripts[0].Name, this.Scripts[0].Assembly);
		}
	}
}
