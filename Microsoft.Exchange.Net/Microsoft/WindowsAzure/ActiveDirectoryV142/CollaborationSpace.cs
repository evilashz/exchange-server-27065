using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.Data.Services.Common;

namespace Microsoft.WindowsAzure.ActiveDirectoryV142
{
	// Token: 0x020005EE RID: 1518
	[DataServiceKey("objectId")]
	public class CollaborationSpace : DirectoryObject
	{
		// Token: 0x060019E9 RID: 6633 RVA: 0x00030BB4 File Offset: 0x0002EDB4
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

		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x060019EA RID: 6634 RVA: 0x00030BE4 File Offset: 0x0002EDE4
		// (set) Token: 0x060019EB RID: 6635 RVA: 0x00030BEC File Offset: 0x0002EDEC
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

		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x060019EC RID: 6636 RVA: 0x00030BF5 File Offset: 0x0002EDF5
		// (set) Token: 0x060019ED RID: 6637 RVA: 0x00030BFD File Offset: 0x0002EDFD
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

		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x060019EE RID: 6638 RVA: 0x00030C06 File Offset: 0x0002EE06
		// (set) Token: 0x060019EF RID: 6639 RVA: 0x00030C0E File Offset: 0x0002EE0E
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

		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x060019F0 RID: 6640 RVA: 0x00030C17 File Offset: 0x0002EE17
		// (set) Token: 0x060019F1 RID: 6641 RVA: 0x00030C1F File Offset: 0x0002EE1F
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

		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x060019F2 RID: 6642 RVA: 0x00030C28 File Offset: 0x0002EE28
		// (set) Token: 0x060019F3 RID: 6643 RVA: 0x00030C30 File Offset: 0x0002EE30
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

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x060019F4 RID: 6644 RVA: 0x00030C39 File Offset: 0x0002EE39
		// (set) Token: 0x060019F5 RID: 6645 RVA: 0x00030C41 File Offset: 0x0002EE41
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

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x060019F6 RID: 6646 RVA: 0x00030C4A File Offset: 0x0002EE4A
		// (set) Token: 0x060019F7 RID: 6647 RVA: 0x00030C52 File Offset: 0x0002EE52
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

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x060019F8 RID: 6648 RVA: 0x00030C5B File Offset: 0x0002EE5B
		// (set) Token: 0x060019F9 RID: 6649 RVA: 0x00030C63 File Offset: 0x0002EE63
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

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x060019FA RID: 6650 RVA: 0x00030C6C File Offset: 0x0002EE6C
		// (set) Token: 0x060019FB RID: 6651 RVA: 0x00030C74 File Offset: 0x0002EE74
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

		// Token: 0x04001BC8 RID: 7112
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool? _accountEnabled;

		// Token: 0x04001BC9 RID: 7113
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<string> _allowAccessTo = new Collection<string>();

		// Token: 0x04001BCA RID: 7114
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _displayName;

		// Token: 0x04001BCB RID: 7115
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _description;

		// Token: 0x04001BCC RID: 7116
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _mail;

		// Token: 0x04001BCD RID: 7117
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _mailNickname;

		// Token: 0x04001BCE RID: 7118
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _userPrincipalName;

		// Token: 0x04001BCF RID: 7119
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Guid? _changeMarker;

		// Token: 0x04001BD0 RID: 7120
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DateTime? _provisioningSince;
	}
}
