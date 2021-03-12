using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000DFF RID: 3583
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class CommonNode
	{
		// Token: 0x06007B2E RID: 31534 RVA: 0x0021FE58 File Offset: 0x0021E058
		public CommonNode(string serverIdIn, string versionIdIn, byte sentToClientIn, byte itemIsEmailIn, byte readIn, byte itemIsCalendarIn, ExDateTime endTimeIn)
		{
			this.ServerId = serverIdIn;
			this.VersionId = versionIdIn;
			this.SentToClient = (sentToClientIn == 1);
			this.IsEmail = (itemIsEmailIn == 1);
			this.Read = (readIn == 43);
			this.IsCalendar = (itemIsCalendarIn == 1);
			this.endTime = endTimeIn;
		}

		// Token: 0x170020FB RID: 8443
		// (get) Token: 0x06007B2F RID: 31535 RVA: 0x0021FEB8 File Offset: 0x0021E0B8
		// (set) Token: 0x06007B30 RID: 31536 RVA: 0x0021FEC0 File Offset: 0x0021E0C0
		public ExDateTime EndTime
		{
			get
			{
				return this.endTime;
			}
			set
			{
				this.endTime = value;
			}
		}

		// Token: 0x170020FC RID: 8444
		// (get) Token: 0x06007B31 RID: 31537 RVA: 0x0021FEC9 File Offset: 0x0021E0C9
		// (set) Token: 0x06007B32 RID: 31538 RVA: 0x0021FED1 File Offset: 0x0021E0D1
		public bool IsCalendar
		{
			get
			{
				return this.itemIsCalendar;
			}
			set
			{
				this.itemIsCalendar = value;
			}
		}

		// Token: 0x170020FD RID: 8445
		// (get) Token: 0x06007B33 RID: 31539 RVA: 0x0021FEDA File Offset: 0x0021E0DA
		// (set) Token: 0x06007B34 RID: 31540 RVA: 0x0021FEE2 File Offset: 0x0021E0E2
		public bool IsEmail
		{
			get
			{
				return this.itemIsEmail;
			}
			set
			{
				this.itemIsEmail = value;
			}
		}

		// Token: 0x170020FE RID: 8446
		// (get) Token: 0x06007B35 RID: 31541 RVA: 0x0021FEEB File Offset: 0x0021E0EB
		// (set) Token: 0x06007B36 RID: 31542 RVA: 0x0021FEF3 File Offset: 0x0021E0F3
		public bool Read
		{
			get
			{
				return this.read;
			}
			set
			{
				this.read = value;
			}
		}

		// Token: 0x170020FF RID: 8447
		// (get) Token: 0x06007B37 RID: 31543 RVA: 0x0021FEFC File Offset: 0x0021E0FC
		// (set) Token: 0x06007B38 RID: 31544 RVA: 0x0021FF04 File Offset: 0x0021E104
		public bool SentToClient
		{
			get
			{
				return this.sentToClient;
			}
			set
			{
				this.sentToClient = value;
			}
		}

		// Token: 0x17002100 RID: 8448
		// (get) Token: 0x06007B39 RID: 31545 RVA: 0x0021FF0D File Offset: 0x0021E10D
		// (set) Token: 0x06007B3A RID: 31546 RVA: 0x0021FF15 File Offset: 0x0021E115
		public string ServerId
		{
			get
			{
				return this.serverId;
			}
			set
			{
				this.serverId = value;
			}
		}

		// Token: 0x17002101 RID: 8449
		// (get) Token: 0x06007B3B RID: 31547 RVA: 0x0021FF1E File Offset: 0x0021E11E
		// (set) Token: 0x06007B3C RID: 31548 RVA: 0x0021FF26 File Offset: 0x0021E126
		public string VersionId
		{
			get
			{
				return this.versionId;
			}
			set
			{
				this.versionId = value;
			}
		}

		// Token: 0x040054C1 RID: 21697
		private ExDateTime endTime = ExDateTime.MinValue;

		// Token: 0x040054C2 RID: 21698
		private bool itemIsCalendar;

		// Token: 0x040054C3 RID: 21699
		private bool itemIsEmail;

		// Token: 0x040054C4 RID: 21700
		private bool read;

		// Token: 0x040054C5 RID: 21701
		private bool sentToClient;

		// Token: 0x040054C6 RID: 21702
		private string serverId;

		// Token: 0x040054C7 RID: 21703
		private string versionId;
	}
}
