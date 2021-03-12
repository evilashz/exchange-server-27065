using System;
using System.ComponentModel;

namespace AjaxControlToolkit
{
	// Token: 0x02000015 RID: 21
	public class ProfilePropertyBinding
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000090 RID: 144 RVA: 0x000031DE File Offset: 0x000013DE
		// (set) Token: 0x06000091 RID: 145 RVA: 0x000031E6 File Offset: 0x000013E6
		[NotifyParentProperty(true)]
		public string ExtenderPropertyName
		{
			get
			{
				return this.extenderPropertyName;
			}
			set
			{
				this.extenderPropertyName = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000092 RID: 146 RVA: 0x000031EF File Offset: 0x000013EF
		// (set) Token: 0x06000093 RID: 147 RVA: 0x000031F7 File Offset: 0x000013F7
		[NotifyParentProperty(true)]
		public string ProfilePropertyName
		{
			get
			{
				return this.profilePropertyName;
			}
			set
			{
				this.profilePropertyName = value;
			}
		}

		// Token: 0x04000025 RID: 37
		private string extenderPropertyName;

		// Token: 0x04000026 RID: 38
		private string profilePropertyName;
	}
}
