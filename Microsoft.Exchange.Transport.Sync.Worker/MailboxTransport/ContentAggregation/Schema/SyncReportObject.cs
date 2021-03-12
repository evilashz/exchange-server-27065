using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Net.Protocols.DeltaSync;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation.Schema
{
	// Token: 0x02000226 RID: 550
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SyncReportObject : ISyncReportObject
	{
		// Token: 0x060013B6 RID: 5046 RVA: 0x00042FD1 File Offset: 0x000411D1
		internal SyncReportObject(ISyncObject syncObject, SchemaType schema)
		{
			if (syncObject != null)
			{
				this.Initialize(syncObject, schema);
			}
		}

		// Token: 0x060013B7 RID: 5047 RVA: 0x00042FF0 File Offset: 0x000411F0
		internal SyncReportObject(DeltaSyncObject deltaSyncObject, SchemaType schema)
		{
			if (deltaSyncObject != null)
			{
				this.Initialize(deltaSyncObject, schema);
			}
		}

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x060013B8 RID: 5048 RVA: 0x0004300F File Offset: 0x0004120F
		public string FolderName
		{
			get
			{
				return this.folderName;
			}
		}

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x060013B9 RID: 5049 RVA: 0x00043017 File Offset: 0x00041217
		public string Sender
		{
			get
			{
				return this.sender;
			}
		}

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x060013BA RID: 5050 RVA: 0x0004301F File Offset: 0x0004121F
		public string Subject
		{
			get
			{
				return this.subject;
			}
		}

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x060013BB RID: 5051 RVA: 0x00043027 File Offset: 0x00041227
		public string MessageClass
		{
			get
			{
				return this.messageClass;
			}
		}

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x060013BC RID: 5052 RVA: 0x0004302F File Offset: 0x0004122F
		public int? MessageSize
		{
			get
			{
				return this.messageSize;
			}
		}

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x060013BD RID: 5053 RVA: 0x00043037 File Offset: 0x00041237
		public ExDateTime? DateSent
		{
			get
			{
				return this.dateSent;
			}
		}

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x060013BE RID: 5054 RVA: 0x0004303F File Offset: 0x0004123F
		public ExDateTime? DateReceived
		{
			get
			{
				return this.dateReceived;
			}
		}

		// Token: 0x060013BF RID: 5055 RVA: 0x00043048 File Offset: 0x00041248
		private void Initialize(ISyncObject syncObject, SchemaType schema)
		{
			switch (schema)
			{
			case SchemaType.Email:
			{
				ISyncEmail syncEmail = (ISyncEmail)syncObject;
				if (syncEmail != null)
				{
					this.sender = syncEmail.From;
					this.subject = syncEmail.Subject;
					this.messageClass = syncEmail.MessageClass;
					this.messageSize = syncEmail.Size;
					this.dateReceived = syncEmail.ReceivedTime;
					return;
				}
				break;
			}
			case SchemaType.Contact:
			{
				ISyncContact syncContact = (ISyncContact)syncObject;
				if (syncContact != null)
				{
					this.subject = string.Format("{0}_{1}_{2}", syncContact.FirstName, syncContact.MiddleName, syncContact.LastName);
					return;
				}
				break;
			}
			case SchemaType.Folder:
			{
				SyncFolder syncFolder = (SyncFolder)syncObject;
				if (syncFolder != null)
				{
					this.folderName = syncFolder.DisplayName;
					return;
				}
				break;
			}
			default:
				throw new ArgumentException("Unknown schema {0} detected ", schema.ToString());
			}
		}

		// Token: 0x060013C0 RID: 5056 RVA: 0x00043114 File Offset: 0x00041314
		private void Initialize(DeltaSyncObject deltaSyncObject, SchemaType schema)
		{
			switch (schema)
			{
			case SchemaType.Email:
			{
				DeltaSyncMail deltaSyncMail = (DeltaSyncMail)deltaSyncObject;
				if (deltaSyncMail != null)
				{
					this.sender = deltaSyncMail.From;
					this.subject = deltaSyncMail.Subject;
					this.messageClass = deltaSyncMail.MessageClass;
					this.messageSize = new int?(deltaSyncMail.Size);
					this.dateReceived = new ExDateTime?(deltaSyncMail.DateReceived);
					return;
				}
				return;
			}
			case SchemaType.Folder:
			{
				DeltaSyncFolder deltaSyncFolder = (DeltaSyncFolder)deltaSyncObject;
				if (deltaSyncFolder != null)
				{
					this.folderName = deltaSyncFolder.DisplayName;
					return;
				}
				return;
			}
			}
			throw new ArgumentException("Unknown schema {0} detected ", schema.ToString());
		}

		// Token: 0x04000A74 RID: 2676
		private string folderName;

		// Token: 0x04000A75 RID: 2677
		private string sender;

		// Token: 0x04000A76 RID: 2678
		private string subject;

		// Token: 0x04000A77 RID: 2679
		private string messageClass;

		// Token: 0x04000A78 RID: 2680
		private int? messageSize;

		// Token: 0x04000A79 RID: 2681
		private ExDateTime? dateSent = null;

		// Token: 0x04000A7A RID: 2682
		private ExDateTime? dateReceived;
	}
}
