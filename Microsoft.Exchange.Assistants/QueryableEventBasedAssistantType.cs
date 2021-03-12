using System;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x020000A6 RID: 166
	internal class QueryableEventBasedAssistantType : QueryableObjectImplBase<QueryableEventBasedAssistantTypeObjectSchema>
	{
		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060004F4 RID: 1268 RVA: 0x0001A288 File Offset: 0x00018488
		// (set) Token: 0x060004F5 RID: 1269 RVA: 0x0001A29A File Offset: 0x0001849A
		public Guid AssistantGuid
		{
			get
			{
				return (Guid)this[QueryableEventBasedAssistantTypeObjectSchema.AssistantGuid];
			}
			set
			{
				this[QueryableEventBasedAssistantTypeObjectSchema.AssistantGuid] = value;
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060004F6 RID: 1270 RVA: 0x0001A2AD File Offset: 0x000184AD
		// (set) Token: 0x060004F7 RID: 1271 RVA: 0x0001A2BF File Offset: 0x000184BF
		public string AssistantName
		{
			get
			{
				return (string)this[QueryableEventBasedAssistantTypeObjectSchema.AssistantName];
			}
			set
			{
				this[QueryableEventBasedAssistantTypeObjectSchema.AssistantName] = value;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060004F8 RID: 1272 RVA: 0x0001A2CD File Offset: 0x000184CD
		// (set) Token: 0x060004F9 RID: 1273 RVA: 0x0001A2DF File Offset: 0x000184DF
		public string MailboxType
		{
			get
			{
				return (string)this[QueryableEventBasedAssistantTypeObjectSchema.MailboxType];
			}
			set
			{
				this[QueryableEventBasedAssistantTypeObjectSchema.MailboxType] = value;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060004FA RID: 1274 RVA: 0x0001A2ED File Offset: 0x000184ED
		// (set) Token: 0x060004FB RID: 1275 RVA: 0x0001A2FF File Offset: 0x000184FF
		public string MapiEventType
		{
			get
			{
				return (string)this[QueryableEventBasedAssistantTypeObjectSchema.MapiEventType];
			}
			set
			{
				this[QueryableEventBasedAssistantTypeObjectSchema.MapiEventType] = value;
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060004FC RID: 1276 RVA: 0x0001A30D File Offset: 0x0001850D
		// (set) Token: 0x060004FD RID: 1277 RVA: 0x0001A31F File Offset: 0x0001851F
		public bool NeedMailboxSession
		{
			get
			{
				return (bool)this[QueryableEventBasedAssistantTypeObjectSchema.NeedMailboxSession];
			}
			set
			{
				this[QueryableEventBasedAssistantTypeObjectSchema.NeedMailboxSession] = value;
			}
		}
	}
}
