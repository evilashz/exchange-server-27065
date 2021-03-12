using System;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.WindowsAzure.ActiveDirectory;

namespace Microsoft.Exchange.Management.Powershell.Support
{
	// Token: 0x02000017 RID: 23
	[Serializable]
	public sealed class AADGroupPresentationObject : AADDirectoryObjectPresentationObject
	{
		// Token: 0x06000103 RID: 259 RVA: 0x000057B8 File Offset: 0x000039B8
		internal AADGroupPresentationObject(Group group) : base(group)
		{
			AADDirectoryObjectPresentationObject[] allowAccessTo;
			if (group.allowAccessTo == null)
			{
				allowAccessTo = null;
			}
			else
			{
				allowAccessTo = (from directoryObject in @group.allowAccessTo
				select AADPresentationObjectFactory.Create(directoryObject)).ToArray<AADDirectoryObjectPresentationObject>();
			}
			this.AllowAccessTo = allowAccessTo;
			this.Description = group.description;
			this.DisplayName = group.displayName;
			this.ExchangeResources = ((group.exchangeResources != null) ? group.exchangeResources.ToArray<string>() : null);
			this.GroupType = group.groupType;
			this.IsPublic = group.isPublic;
			this.Mail = group.mail;
			this.MailEnabled = group.mailEnabled;
			AADDirectoryObjectPresentationObject[] pendingMembers;
			if (group.pendingMembers == null)
			{
				pendingMembers = null;
			}
			else
			{
				pendingMembers = (from directoryObject in @group.pendingMembers
				select AADPresentationObjectFactory.Create(directoryObject)).ToArray<AADDirectoryObjectPresentationObject>();
			}
			this.PendingMembers = pendingMembers;
			this.ProxyAddresses = ((group.proxyAddresses != null) ? group.proxyAddresses.ToArray<string>() : null);
			this.SecurityEnabled = group.securityEnabled;
			this.SharePointResources = ((group.sharepointResources != null) ? group.sharepointResources.ToArray<string>() : null);
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000104 RID: 260 RVA: 0x000058F0 File Offset: 0x00003AF0
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return ObjectSchema.GetInstance<AADGroupPresentationObjectSchema>();
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000105 RID: 261 RVA: 0x000058F7 File Offset: 0x00003AF7
		// (set) Token: 0x06000106 RID: 262 RVA: 0x00005909 File Offset: 0x00003B09
		public AADDirectoryObjectPresentationObject[] AllowAccessTo
		{
			get
			{
				return (AADDirectoryObjectPresentationObject[])this[AADGroupPresentationObjectSchema.AllowAccessTo];
			}
			set
			{
				this[AADGroupPresentationObjectSchema.AllowAccessTo] = value;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00005917 File Offset: 0x00003B17
		// (set) Token: 0x06000108 RID: 264 RVA: 0x00005929 File Offset: 0x00003B29
		public string Description
		{
			get
			{
				return (string)this[AADGroupPresentationObjectSchema.Description];
			}
			set
			{
				this[AADGroupPresentationObjectSchema.Description] = value;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00005937 File Offset: 0x00003B37
		// (set) Token: 0x0600010A RID: 266 RVA: 0x00005949 File Offset: 0x00003B49
		public string DisplayName
		{
			get
			{
				return (string)this[AADGroupPresentationObjectSchema.DisplayName];
			}
			set
			{
				this[AADGroupPresentationObjectSchema.DisplayName] = value;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600010B RID: 267 RVA: 0x00005957 File Offset: 0x00003B57
		// (set) Token: 0x0600010C RID: 268 RVA: 0x00005969 File Offset: 0x00003B69
		public string[] ExchangeResources
		{
			get
			{
				return (string[])this[AADGroupPresentationObjectSchema.ExchangeResources];
			}
			set
			{
				this[AADGroupPresentationObjectSchema.ExchangeResources] = value;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600010D RID: 269 RVA: 0x00005977 File Offset: 0x00003B77
		// (set) Token: 0x0600010E RID: 270 RVA: 0x00005989 File Offset: 0x00003B89
		public string GroupType
		{
			get
			{
				return (string)this[AADGroupPresentationObjectSchema.GroupType];
			}
			set
			{
				this[AADGroupPresentationObjectSchema.GroupType] = value;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00005997 File Offset: 0x00003B97
		// (set) Token: 0x06000110 RID: 272 RVA: 0x000059A9 File Offset: 0x00003BA9
		public bool? IsPublic
		{
			get
			{
				return (bool?)this[AADGroupPresentationObjectSchema.IsPublic];
			}
			set
			{
				this[AADGroupPresentationObjectSchema.IsPublic] = value;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000111 RID: 273 RVA: 0x000059BC File Offset: 0x00003BBC
		// (set) Token: 0x06000112 RID: 274 RVA: 0x000059CE File Offset: 0x00003BCE
		public string Mail
		{
			get
			{
				return (string)this[AADGroupPresentationObjectSchema.Mail];
			}
			set
			{
				this[AADGroupPresentationObjectSchema.Mail] = value;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000113 RID: 275 RVA: 0x000059DC File Offset: 0x00003BDC
		// (set) Token: 0x06000114 RID: 276 RVA: 0x000059EE File Offset: 0x00003BEE
		public bool? MailEnabled
		{
			get
			{
				return (bool?)this[AADGroupPresentationObjectSchema.MailEnabled];
			}
			set
			{
				this[AADGroupPresentationObjectSchema.MailEnabled] = value;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000115 RID: 277 RVA: 0x00005A01 File Offset: 0x00003C01
		// (set) Token: 0x06000116 RID: 278 RVA: 0x00005A13 File Offset: 0x00003C13
		public AADDirectoryObjectPresentationObject[] PendingMembers
		{
			get
			{
				return (AADDirectoryObjectPresentationObject[])this[AADGroupPresentationObjectSchema.PendingMembers];
			}
			set
			{
				this[AADGroupPresentationObjectSchema.PendingMembers] = value;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000117 RID: 279 RVA: 0x00005A21 File Offset: 0x00003C21
		// (set) Token: 0x06000118 RID: 280 RVA: 0x00005A33 File Offset: 0x00003C33
		public string[] ProxyAddresses
		{
			get
			{
				return (string[])this[AADGroupPresentationObjectSchema.ProxyAddresses];
			}
			set
			{
				this[AADGroupPresentationObjectSchema.ProxyAddresses] = value;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000119 RID: 281 RVA: 0x00005A41 File Offset: 0x00003C41
		// (set) Token: 0x0600011A RID: 282 RVA: 0x00005A53 File Offset: 0x00003C53
		public bool? SecurityEnabled
		{
			get
			{
				return (bool?)this[AADGroupPresentationObjectSchema.SecurityEnabled];
			}
			set
			{
				this[AADGroupPresentationObjectSchema.SecurityEnabled] = value;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600011B RID: 283 RVA: 0x00005A66 File Offset: 0x00003C66
		// (set) Token: 0x0600011C RID: 284 RVA: 0x00005A78 File Offset: 0x00003C78
		public string[] SharePointResources
		{
			get
			{
				return (string[])this[AADGroupPresentationObjectSchema.SharePointResources];
			}
			set
			{
				this[AADGroupPresentationObjectSchema.SharePointResources] = value;
			}
		}
	}
}
