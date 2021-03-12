using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch
{
	// Token: 0x02000D16 RID: 3350
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class MailboxDiscoverySearchRequest : DiscoverySearchBase
	{
		// Token: 0x17001EEC RID: 7916
		// (get) Token: 0x060073DB RID: 29659 RVA: 0x002022EB File Offset: 0x002004EB
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return MailboxDiscoverySearchRequest.schema;
			}
		}

		// Token: 0x17001EED RID: 7917
		// (get) Token: 0x060073DC RID: 29660 RVA: 0x002022F2 File Offset: 0x002004F2
		internal override string ItemClass
		{
			get
			{
				return "IPM.Configuration.MailboxDiscoverySearchRequest";
			}
		}

		// Token: 0x17001EEE RID: 7918
		// (get) Token: 0x060073DD RID: 29661 RVA: 0x002022F9 File Offset: 0x002004F9
		// (set) Token: 0x060073DE RID: 29662 RVA: 0x0020230B File Offset: 0x0020050B
		public ActionRequestType AsynchronousActionRequest
		{
			get
			{
				return (ActionRequestType)this[MailboxDiscoverySearchRequestSchema.AsynchronousActionRequest];
			}
			set
			{
				this[MailboxDiscoverySearchRequestSchema.AsynchronousActionRequest] = value;
			}
		}

		// Token: 0x17001EEF RID: 7919
		// (get) Token: 0x060073DF RID: 29663 RVA: 0x0020231E File Offset: 0x0020051E
		// (set) Token: 0x060073E0 RID: 29664 RVA: 0x00202330 File Offset: 0x00200530
		public string AsynchronousActionRbacContext
		{
			get
			{
				return (string)this[MailboxDiscoverySearchRequestSchema.AsynchronousActionRbacContext];
			}
			set
			{
				this[MailboxDiscoverySearchRequestSchema.AsynchronousActionRbacContext] = value;
			}
		}

		// Token: 0x040050E9 RID: 20713
		private static ObjectSchema schema = new MailboxDiscoverySearchRequestSchema();
	}
}
