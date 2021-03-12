using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000703 RID: 1795
	[Serializable]
	public class ExchangeRoleEntryPresentation : ADPresentationObject, IConfigurable
	{
		// Token: 0x0600547C RID: 21628 RVA: 0x00131F27 File Offset: 0x00130127
		public ExchangeRoleEntryPresentation()
		{
		}

		// Token: 0x0600547D RID: 21629 RVA: 0x00131F30 File Offset: 0x00130130
		public ExchangeRoleEntryPresentation(ExchangeRole role, RoleEntry roleEntry) : base(role)
		{
			if (roleEntry is CmdletRoleEntry)
			{
				this.Type = ManagementRoleEntryType.Cmdlet;
				this.PSSnapinName = ((CmdletRoleEntry)roleEntry).PSSnapinName;
			}
			else if (roleEntry is ScriptRoleEntry)
			{
				this.Type = ManagementRoleEntryType.Script;
			}
			else if (roleEntry is ApplicationPermissionRoleEntry)
			{
				this.Type = ManagementRoleEntryType.ApplicationPermission;
			}
			else if (roleEntry is WebServiceRoleEntry)
			{
				this.Type = ManagementRoleEntryType.WebService;
			}
			this.Name = roleEntry.Name;
			this.identity = string.Format("{0}\\{1}", role.Id.ToString(), this.Name);
			this.Parameters = roleEntry.Parameters;
		}

		// Token: 0x17001C14 RID: 7188
		// (get) Token: 0x0600547E RID: 21630 RVA: 0x00131FD0 File Offset: 0x001301D0
		// (set) Token: 0x0600547F RID: 21631 RVA: 0x00131FD8 File Offset: 0x001301D8
		public new string Name
		{
			get
			{
				return this.name;
			}
			internal set
			{
				this.name = value;
			}
		}

		// Token: 0x17001C15 RID: 7189
		// (get) Token: 0x06005480 RID: 21632 RVA: 0x00131FE1 File Offset: 0x001301E1
		// (set) Token: 0x06005481 RID: 21633 RVA: 0x00131FF3 File Offset: 0x001301F3
		public ADObjectId Role
		{
			get
			{
				return (ADObjectId)this[ExchangeRoleEntryPresentationSchema.Role];
			}
			internal set
			{
				this[ExchangeRoleEntryPresentationSchema.Role] = value;
			}
		}

		// Token: 0x17001C16 RID: 7190
		// (get) Token: 0x06005482 RID: 21634 RVA: 0x00132001 File Offset: 0x00130201
		// (set) Token: 0x06005483 RID: 21635 RVA: 0x00132009 File Offset: 0x00130209
		public ManagementRoleEntryType Type
		{
			get
			{
				return this.roleEntryType;
			}
			internal set
			{
				this.roleEntryType = value;
			}
		}

		// Token: 0x17001C17 RID: 7191
		// (get) Token: 0x06005484 RID: 21636 RVA: 0x00132012 File Offset: 0x00130212
		// (set) Token: 0x06005485 RID: 21637 RVA: 0x0013201A File Offset: 0x0013021A
		public ICollection<string> Parameters
		{
			get
			{
				return this.parameters;
			}
			internal set
			{
				this.parameters = value;
			}
		}

		// Token: 0x17001C18 RID: 7192
		// (get) Token: 0x06005486 RID: 21638 RVA: 0x00132023 File Offset: 0x00130223
		// (set) Token: 0x06005487 RID: 21639 RVA: 0x0013202B File Offset: 0x0013022B
		public string PSSnapinName
		{
			get
			{
				return this.snapinName;
			}
			internal set
			{
				this.snapinName = value;
			}
		}

		// Token: 0x17001C19 RID: 7193
		// (get) Token: 0x06005488 RID: 21640 RVA: 0x00132034 File Offset: 0x00130234
		ObjectId IConfigurable.Identity
		{
			get
			{
				return new ConfigObjectId(this.identity);
			}
		}

		// Token: 0x17001C1A RID: 7194
		// (get) Token: 0x06005489 RID: 21641 RVA: 0x00132041 File Offset: 0x00130241
		internal override ADPresentationSchema PresentationSchema
		{
			get
			{
				return ExchangeRoleEntryPresentation.schema;
			}
		}

		// Token: 0x040038BB RID: 14523
		private static ExchangeRoleEntryPresentationSchema schema = ObjectSchema.GetInstance<ExchangeRoleEntryPresentationSchema>();

		// Token: 0x040038BC RID: 14524
		private string identity;

		// Token: 0x040038BD RID: 14525
		private ManagementRoleEntryType roleEntryType;

		// Token: 0x040038BE RID: 14526
		private string name;

		// Token: 0x040038BF RID: 14527
		private ICollection<string> parameters;

		// Token: 0x040038C0 RID: 14528
		private string snapinName;
	}
}
