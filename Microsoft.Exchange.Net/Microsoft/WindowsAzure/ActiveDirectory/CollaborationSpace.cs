using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.Data.Services.Common;

namespace Microsoft.WindowsAzure.ActiveDirectory
{
	// Token: 0x020005A5 RID: 1445
	[DataServiceKey("objectId")]
	public class CollaborationSpace : DirectoryObject
	{
		// Token: 0x060014BB RID: 5307 RVA: 0x0002CAA0 File Offset: 0x0002ACA0
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public static CollaborationSpace CreateCollaborationSpace(string objectId, Collection<string> allowAccessTo)
		{
			CollaborationSpace collaborationSpace = new CollaborationSpace();
			collaborationSpace.objectId = objectId;
			if (allowAccessTo == null)
			{
				throw new ArgumentNullException("allowAccessTo");
			}
			collaborationSpace.allowAccessTo = allowAccessTo;
			return collaborationSpace;
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x060014BC RID: 5308 RVA: 0x0002CAD0 File Offset: 0x0002ACD0
		// (set) Token: 0x060014BD RID: 5309 RVA: 0x0002CAD8 File Offset: 0x0002ACD8
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public bool? accountEnabled
		{
			get
			{
				return this._accountEnabled;
			}
			set
			{
				this._accountEnabled = value;
			}
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x060014BE RID: 5310 RVA: 0x0002CAE1 File Offset: 0x0002ACE1
		// (set) Token: 0x060014BF RID: 5311 RVA: 0x0002CAE9 File Offset: 0x0002ACE9
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<string> allowAccessTo
		{
			get
			{
				return this._allowAccessTo;
			}
			set
			{
				this._allowAccessTo = value;
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x060014C0 RID: 5312 RVA: 0x0002CAF2 File Offset: 0x0002ACF2
		// (set) Token: 0x060014C1 RID: 5313 RVA: 0x0002CAFA File Offset: 0x0002ACFA
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string displayName
		{
			get
			{
				return this._displayName;
			}
			set
			{
				this._displayName = value;
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x060014C2 RID: 5314 RVA: 0x0002CB03 File Offset: 0x0002AD03
		// (set) Token: 0x060014C3 RID: 5315 RVA: 0x0002CB0B File Offset: 0x0002AD0B
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string description
		{
			get
			{
				return this._description;
			}
			set
			{
				this._description = value;
			}
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x060014C4 RID: 5316 RVA: 0x0002CB14 File Offset: 0x0002AD14
		// (set) Token: 0x060014C5 RID: 5317 RVA: 0x0002CB1C File Offset: 0x0002AD1C
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string mail
		{
			get
			{
				return this._mail;
			}
			set
			{
				this._mail = value;
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x060014C6 RID: 5318 RVA: 0x0002CB25 File Offset: 0x0002AD25
		// (set) Token: 0x060014C7 RID: 5319 RVA: 0x0002CB2D File Offset: 0x0002AD2D
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string mailNickname
		{
			get
			{
				return this._mailNickname;
			}
			set
			{
				this._mailNickname = value;
			}
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x060014C8 RID: 5320 RVA: 0x0002CB36 File Offset: 0x0002AD36
		// (set) Token: 0x060014C9 RID: 5321 RVA: 0x0002CB3E File Offset: 0x0002AD3E
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string userPrincipalName
		{
			get
			{
				return this._userPrincipalName;
			}
			set
			{
				this._userPrincipalName = value;
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x060014CA RID: 5322 RVA: 0x0002CB47 File Offset: 0x0002AD47
		// (set) Token: 0x060014CB RID: 5323 RVA: 0x0002CB4F File Offset: 0x0002AD4F
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Guid? changeMarker
		{
			get
			{
				return this._changeMarker;
			}
			set
			{
				this._changeMarker = value;
			}
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x060014CC RID: 5324 RVA: 0x0002CB58 File Offset: 0x0002AD58
		// (set) Token: 0x060014CD RID: 5325 RVA: 0x0002CB60 File Offset: 0x0002AD60
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public DateTime? provisioningSince
		{
			get
			{
				return this._provisioningSince;
			}
			set
			{
				this._provisioningSince = value;
			}
		}

		// Token: 0x04001963 RID: 6499
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool? _accountEnabled;

		// Token: 0x04001964 RID: 6500
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<string> _allowAccessTo = new Collection<string>();

		// Token: 0x04001965 RID: 6501
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _displayName;

		// Token: 0x04001966 RID: 6502
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _description;

		// Token: 0x04001967 RID: 6503
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _mail;

		// Token: 0x04001968 RID: 6504
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _mailNickname;

		// Token: 0x04001969 RID: 6505
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _userPrincipalName;

		// Token: 0x0400196A RID: 6506
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Guid? _changeMarker;

		// Token: 0x0400196B RID: 6507
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DateTime? _provisioningSince;
	}
}
