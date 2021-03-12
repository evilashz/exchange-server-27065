using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200073F RID: 1855
	[XmlType(TypeName = "DelegateUserType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class DelegateUserType
	{
		// Token: 0x060037D0 RID: 14288 RVA: 0x000C5DAA File Offset: 0x000C3FAA
		public DelegateUserType()
		{
		}

		// Token: 0x060037D1 RID: 14289 RVA: 0x000C5DB2 File Offset: 0x000C3FB2
		internal DelegateUserType(UserId userId)
		{
			this.userId = userId;
		}

		// Token: 0x17000D30 RID: 3376
		// (get) Token: 0x060037D2 RID: 14290 RVA: 0x000C5DC1 File Offset: 0x000C3FC1
		// (set) Token: 0x060037D3 RID: 14291 RVA: 0x000C5DC9 File Offset: 0x000C3FC9
		[XmlElement("UserId")]
		public UserId UserId
		{
			get
			{
				return this.userId;
			}
			set
			{
				this.userId = value;
			}
		}

		// Token: 0x17000D31 RID: 3377
		// (get) Token: 0x060037D4 RID: 14292 RVA: 0x000C5DD2 File Offset: 0x000C3FD2
		// (set) Token: 0x060037D5 RID: 14293 RVA: 0x000C5DDA File Offset: 0x000C3FDA
		[XmlElement("DelegatePermissions")]
		public DelegatePermissionsType DelegatePermissions
		{
			get
			{
				return this.delegatePermissions;
			}
			set
			{
				this.delegatePermissions = value;
			}
		}

		// Token: 0x17000D32 RID: 3378
		// (get) Token: 0x060037D6 RID: 14294 RVA: 0x000C5DE3 File Offset: 0x000C3FE3
		// (set) Token: 0x060037D7 RID: 14295 RVA: 0x000C5DEB File Offset: 0x000C3FEB
		[XmlElement("ReceiveCopiesOfMeetingMessages")]
		public bool? ReceiveCopiesOfMeetingMessages
		{
			get
			{
				return this.receiveCopiesOfMeetingMessages;
			}
			set
			{
				this.receiveCopiesOfMeetingMessages = value;
			}
		}

		// Token: 0x17000D33 RID: 3379
		// (get) Token: 0x060037D8 RID: 14296 RVA: 0x000C5DF4 File Offset: 0x000C3FF4
		// (set) Token: 0x060037D9 RID: 14297 RVA: 0x000C5DFC File Offset: 0x000C3FFC
		[XmlElement("ViewPrivateItems")]
		public bool? ViewPrivateItems
		{
			get
			{
				return this.viewPrivateItems;
			}
			set
			{
				this.viewPrivateItems = value;
			}
		}

		// Token: 0x04001F0A RID: 7946
		private UserId userId;

		// Token: 0x04001F0B RID: 7947
		private DelegatePermissionsType delegatePermissions;

		// Token: 0x04001F0C RID: 7948
		private bool? receiveCopiesOfMeetingMessages;

		// Token: 0x04001F0D RID: 7949
		private bool? viewPrivateItems;
	}
}
