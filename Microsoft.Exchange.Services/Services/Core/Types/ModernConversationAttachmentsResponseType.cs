using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000B3E RID: 2878
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "ModernConversationAttachments")]
	public class ModernConversationAttachmentsResponseType
	{
		// Token: 0x1700139E RID: 5022
		// (get) Token: 0x06005186 RID: 20870 RVA: 0x0010A8C7 File Offset: 0x00108AC7
		// (set) Token: 0x06005187 RID: 20871 RVA: 0x0010A8CF File Offset: 0x00108ACF
		[DataMember(Name = "ConversationId", IsRequired = true)]
		public ItemId ConversationId { get; set; }

		// Token: 0x1700139F RID: 5023
		// (get) Token: 0x06005188 RID: 20872 RVA: 0x0010A8D8 File Offset: 0x00108AD8
		// (set) Token: 0x06005189 RID: 20873 RVA: 0x0010A8E0 File Offset: 0x00108AE0
		public byte[] SyncState { get; set; }

		// Token: 0x170013A0 RID: 5024
		// (get) Token: 0x0600518A RID: 20874 RVA: 0x0010A8EC File Offset: 0x00108AEC
		// (set) Token: 0x0600518B RID: 20875 RVA: 0x0010A90B File Offset: 0x00108B0B
		[DataMember(Name = "SyncState", EmitDefaultValue = false)]
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

		// Token: 0x170013A1 RID: 5025
		// (get) Token: 0x0600518C RID: 20876 RVA: 0x0010A924 File Offset: 0x00108B24
		// (set) Token: 0x0600518D RID: 20877 RVA: 0x0010A92C File Offset: 0x00108B2C
		[DataMember(IsRequired = true)]
		public ItemType[] ItemsWithAttachments { get; set; }
	}
}
