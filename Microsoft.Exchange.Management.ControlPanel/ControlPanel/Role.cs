using System;
using System.ComponentModel;
using System.Web;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000649 RID: 1609
	public class Role : ValuePair
	{
		// Token: 0x17002718 RID: 10008
		// (get) Token: 0x06004650 RID: 18000 RVA: 0x000D4B47 File Offset: 0x000D2D47
		// (set) Token: 0x06004651 RID: 18001 RVA: 0x000D4B54 File Offset: 0x000D2D54
		[DefaultValue("")]
		public override object Value
		{
			get
			{
				return this.IsInRoles;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002719 RID: 10009
		// (get) Token: 0x06004652 RID: 18002 RVA: 0x000D4B5B File Offset: 0x000D2D5B
		// (set) Token: 0x06004653 RID: 18003 RVA: 0x000D4B63 File Offset: 0x000D2D63
		[DefaultValue("")]
		public string Rbac { get; set; }

		// Token: 0x1700271A RID: 10010
		// (get) Token: 0x06004654 RID: 18004 RVA: 0x000D4B6C File Offset: 0x000D2D6C
		public bool IsInRoles
		{
			get
			{
				return LoginUtil.IsInRoles(HttpContext.Current.User, this.Rbac.Split(new char[]
				{
					','
				}));
			}
		}
	}
}
