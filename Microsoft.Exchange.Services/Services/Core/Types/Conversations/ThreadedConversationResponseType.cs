using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Core.Types.Conversations
{
	// Token: 0x0200068F RID: 1679
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "ThreadedConversation")]
	[Serializable]
	public class ThreadedConversationResponseType : IConversationResponseType
	{
		// Token: 0x17000BE0 RID: 3040
		// (get) Token: 0x0600337D RID: 13181 RVA: 0x000B889A File Offset: 0x000B6A9A
		// (set) Token: 0x0600337E RID: 13182 RVA: 0x000B88A2 File Offset: 0x000B6AA2
		[DataMember(Name = "ConversationId", IsRequired = true, Order = 1)]
		public ItemId ConversationId { get; set; }

		// Token: 0x17000BE1 RID: 3041
		// (get) Token: 0x0600337F RID: 13183 RVA: 0x000B88AB File Offset: 0x000B6AAB
		// (set) Token: 0x06003380 RID: 13184 RVA: 0x000B88B3 File Offset: 0x000B6AB3
		[IgnoreDataMember]
		public byte[] SyncState { get; set; }

		// Token: 0x17000BE2 RID: 3042
		// (get) Token: 0x06003381 RID: 13185 RVA: 0x000B88BC File Offset: 0x000B6ABC
		// (set) Token: 0x06003382 RID: 13186 RVA: 0x000B88DB File Offset: 0x000B6ADB
		[DataMember(Name = "SyncState", EmitDefaultValue = false, Order = 2)]
		public string SyncStateString
		{
			get
			{
				byte[] syncState = this.SyncState;
				if (syncState == null)
				{
					return null;
				}
				return Convert.ToBase64String(syncState);
			}
			set
			{
				this.SyncState = (string.IsNullOrEmpty(value) ? null : Convert.FromBase64String(value));
			}
		}

		// Token: 0x17000BE3 RID: 3043
		// (get) Token: 0x06003383 RID: 13187 RVA: 0x000B88F4 File Offset: 0x000B6AF4
		// (set) Token: 0x06003384 RID: 13188 RVA: 0x000B8919 File Offset: 0x000B6B19
		[DataMember(Name = "ConversationThreads", EmitDefaultValue = false, Order = 3)]
		public ConversationThreadType[] ConversationThreads
		{
			get
			{
				if (this.threads != null && this.threads.Count > 0)
				{
					return this.threads.ToArray();
				}
				return null;
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					this.threads = null;
					return;
				}
				this.threads = new List<ConversationThreadType>(value);
			}
		}

		// Token: 0x17000BE4 RID: 3044
		// (get) Token: 0x06003385 RID: 13189 RVA: 0x000B8937 File Offset: 0x000B6B37
		// (set) Token: 0x06003386 RID: 13190 RVA: 0x000B893F File Offset: 0x000B6B3F
		[DataMember(Name = "TotalThreadCount", IsRequired = false, Order = 4)]
		public int TotalThreadCount { get; set; }

		// Token: 0x04001D1A RID: 7450
		private List<ConversationThreadType> threads;
	}
}
