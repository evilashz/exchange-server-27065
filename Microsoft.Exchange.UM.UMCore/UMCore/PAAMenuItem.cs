using System;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020002A2 RID: 674
	internal class PAAMenuItem
	{
		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x0600147F RID: 5247 RVA: 0x000591D6 File Offset: 0x000573D6
		// (set) Token: 0x06001480 RID: 5248 RVA: 0x000591DE File Offset: 0x000573DE
		internal bool Enabled
		{
			get
			{
				return this.enabled;
			}
			set
			{
				this.enabled = value;
			}
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x06001481 RID: 5249 RVA: 0x000591E7 File Offset: 0x000573E7
		// (set) Token: 0x06001482 RID: 5250 RVA: 0x000591EF File Offset: 0x000573EF
		internal int MenuKey
		{
			get
			{
				return this.menuKey;
			}
			set
			{
				this.menuKey = value;
			}
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x06001483 RID: 5251 RVA: 0x000591F8 File Offset: 0x000573F8
		// (set) Token: 0x06001484 RID: 5252 RVA: 0x00059200 File Offset: 0x00057400
		internal string MenuType
		{
			get
			{
				return this.menuType;
			}
			set
			{
				this.menuType = value;
			}
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x06001485 RID: 5253 RVA: 0x00059209 File Offset: 0x00057409
		// (set) Token: 0x06001486 RID: 5254 RVA: 0x00059211 File Offset: 0x00057411
		internal string Context
		{
			get
			{
				return this.context;
			}
			set
			{
				this.context = value;
			}
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x06001487 RID: 5255 RVA: 0x0005921A File Offset: 0x0005741A
		// (set) Token: 0x06001488 RID: 5256 RVA: 0x00059222 File Offset: 0x00057422
		internal object TargetName
		{
			get
			{
				return this.targetName;
			}
			set
			{
				this.targetName = value;
			}
		}

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x06001489 RID: 5257 RVA: 0x0005922B File Offset: 0x0005742B
		// (set) Token: 0x0600148A RID: 5258 RVA: 0x00059233 File Offset: 0x00057433
		internal PhoneNumber TargetPhone
		{
			get
			{
				return this.targetPhone;
			}
			set
			{
				this.targetPhone = value;
			}
		}

		// Token: 0x04000CA0 RID: 3232
		private bool enabled;

		// Token: 0x04000CA1 RID: 3233
		private int menuKey;

		// Token: 0x04000CA2 RID: 3234
		private string menuType;

		// Token: 0x04000CA3 RID: 3235
		private string context;

		// Token: 0x04000CA4 RID: 3236
		private object targetName;

		// Token: 0x04000CA5 RID: 3237
		private PhoneNumber targetPhone;
	}
}
