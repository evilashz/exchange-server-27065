using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x02000032 RID: 50
	internal class EmergencyKit
	{
		// Token: 0x0600019C RID: 412 RVA: 0x00008930 File Offset: 0x00006B30
		public EmergencyKit(MapiEvent mapiEvent)
		{
			this.mapiEvent = mapiEvent;
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000893F File Offset: 0x00006B3F
		public EmergencyKit(Guid mailboxGuid)
		{
			this.mailboxGuid = mailboxGuid;
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600019E RID: 414 RVA: 0x0000894E File Offset: 0x00006B4E
		public Guid MailboxGuid
		{
			get
			{
				return this.mailboxGuid;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600019F RID: 415 RVA: 0x00008956 File Offset: 0x00006B56
		public string MailboxDisplayName
		{
			get
			{
				if (this.eventDispatcher != null)
				{
					return this.eventDispatcher.MailboxDisplayName;
				}
				if (this.mailboxData != null)
				{
					return this.mailboxData.DisplayName;
				}
				return "<unknown>";
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x00008985 File Offset: 0x00006B85
		public MapiEvent MapiEvent
		{
			get
			{
				return this.mapiEvent;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x0000898D File Offset: 0x00006B8D
		public LocalizedString AssistantName
		{
			get
			{
				if (this.assistant != null)
				{
					return this.assistant.Name;
				}
				return Strings.driverName;
			}
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x000089A8 File Offset: 0x00006BA8
		public void SetContext(IAssistantBase assistant, EventDispatcher eventDispatcher)
		{
			this.assistant = assistant;
			this.eventDispatcher = eventDispatcher;
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x000089B8 File Offset: 0x00006BB8
		public void SetContext(IAssistantBase assistant, MailboxData mailboxData)
		{
			this.assistant = assistant;
			this.mailboxData = mailboxData;
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x000089C8 File Offset: 0x00006BC8
		public void SetContext(IAssistantBase assistant)
		{
			this.assistant = assistant;
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x000089D1 File Offset: 0x00006BD1
		public void UnsetContext()
		{
			this.assistant = null;
			this.eventDispatcher = null;
			this.mailboxData = null;
		}

		// Token: 0x0400015D RID: 349
		private Guid mailboxGuid;

		// Token: 0x0400015E RID: 350
		private MapiEvent mapiEvent;

		// Token: 0x0400015F RID: 351
		private IAssistantBase assistant;

		// Token: 0x04000160 RID: 352
		private EventDispatcher eventDispatcher;

		// Token: 0x04000161 RID: 353
		private MailboxData mailboxData;
	}
}
