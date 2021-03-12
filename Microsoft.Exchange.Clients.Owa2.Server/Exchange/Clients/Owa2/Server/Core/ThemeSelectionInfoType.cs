using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000410 RID: 1040
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class ThemeSelectionInfoType
	{
		// Token: 0x1700089C RID: 2204
		// (get) Token: 0x06002294 RID: 8852 RVA: 0x0007F206 File Offset: 0x0007D406
		// (set) Token: 0x06002295 RID: 8853 RVA: 0x0007F20E File Offset: 0x0007D40E
		[DataMember]
		public Theme[] Themes
		{
			get
			{
				return this.themes;
			}
			set
			{
				this.themes = value;
			}
		}

		// Token: 0x1700089D RID: 2205
		// (get) Token: 0x06002296 RID: 8854 RVA: 0x0007F217 File Offset: 0x0007D417
		// (set) Token: 0x06002297 RID: 8855 RVA: 0x0007F21F File Offset: 0x0007D41F
		[DataMember]
		public string[] CssPaths
		{
			get
			{
				return this.cssPaths;
			}
			set
			{
				this.cssPaths = value;
			}
		}

		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x06002298 RID: 8856 RVA: 0x0007F228 File Offset: 0x0007D428
		// (set) Token: 0x06002299 RID: 8857 RVA: 0x0007F230 File Offset: 0x0007D430
		[DataMember]
		public string ThemePath
		{
			get
			{
				return this.themePath;
			}
			set
			{
				this.themePath = value;
			}
		}

		// Token: 0x0400132A RID: 4906
		private Theme[] themes;

		// Token: 0x0400132B RID: 4907
		private string[] cssPaths;

		// Token: 0x0400132C RID: 4908
		private string themePath;
	}
}
