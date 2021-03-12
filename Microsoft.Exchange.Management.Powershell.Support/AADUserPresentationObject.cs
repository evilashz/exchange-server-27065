using System;
using Microsoft.Exchange.Data;
using Microsoft.WindowsAzure.ActiveDirectory;

namespace Microsoft.Exchange.Management.Powershell.Support
{
	// Token: 0x0200001A RID: 26
	[Serializable]
	public sealed class AADUserPresentationObject : AADDirectoryObjectPresentationObject
	{
		// Token: 0x06000123 RID: 291 RVA: 0x00005B03 File Offset: 0x00003D03
		internal AADUserPresentationObject(User user) : base(user)
		{
			this.DisplayName = user.displayName;
			this.MailNickname = user.mailNickname;
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000124 RID: 292 RVA: 0x00005B24 File Offset: 0x00003D24
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return ObjectSchema.GetInstance<AADUserPresentationObjectSchema>();
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000125 RID: 293 RVA: 0x00005B2B File Offset: 0x00003D2B
		// (set) Token: 0x06000126 RID: 294 RVA: 0x00005B3D File Offset: 0x00003D3D
		public string DisplayName
		{
			get
			{
				return (string)this[AADUserPresentationObjectSchema.DisplayName];
			}
			set
			{
				this[AADUserPresentationObjectSchema.DisplayName] = value;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00005B4B File Offset: 0x00003D4B
		// (set) Token: 0x06000128 RID: 296 RVA: 0x00005B5D File Offset: 0x00003D5D
		public string MailNickname
		{
			get
			{
				return (string)this[AADUserPresentationObjectSchema.MailNickname];
			}
			set
			{
				this[AADUserPresentationObjectSchema.MailNickname] = value;
			}
		}
	}
}
