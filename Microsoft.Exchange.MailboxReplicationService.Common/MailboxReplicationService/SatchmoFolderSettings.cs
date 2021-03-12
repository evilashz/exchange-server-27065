using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020000BD RID: 189
	[DataContract]
	internal class SatchmoFolderSettings : ItemPropertiesBase
	{
		// Token: 0x04000479 RID: 1145
		public int FolderTypeInt;

		// Token: 0x0400047A RID: 1146
		public DateTime LastWrite;

		// Token: 0x0400047B RID: 1147
		public int? LatestFolderChangeSequenceNumber;

		// Token: 0x0400047C RID: 1148
		public int ViewTypeInt;

		// Token: 0x0400047D RID: 1149
		public bool IsArchive;

		// Token: 0x0400047E RID: 1150
		public byte[] UserPreference;

		// Token: 0x0400047F RID: 1151
		public int? UIDValidity;

		// Token: 0x04000480 RID: 1152
		public int ImapStateInt;

		// Token: 0x04000481 RID: 1153
		public int? LatestCreateSequenceNumberInFolder;

		// Token: 0x04000482 RID: 1154
		public DateTime? LastDateReceivedHeaderProcessedForIMAP;

		// Token: 0x04000483 RID: 1155
		public Guid? LastMessageIdHeaderProcessedForIMAP;

		// Token: 0x04000484 RID: 1156
		public int? LastCreateSequenceNumberInFolderProcessedForIMAP;
	}
}
