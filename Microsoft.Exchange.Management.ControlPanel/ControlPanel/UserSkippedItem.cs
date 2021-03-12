using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200022B RID: 555
	[DataContract]
	public class UserSkippedItem
	{
		// Token: 0x060027B2 RID: 10162 RVA: 0x0007CD8B File Offset: 0x0007AF8B
		public UserSkippedItem(MigrationUserSkippedItem skippedItem)
		{
			this.skippedItem = skippedItem;
		}

		// Token: 0x17001C23 RID: 7203
		// (get) Token: 0x060027B3 RID: 10163 RVA: 0x0007CD9C File Offset: 0x0007AF9C
		// (set) Token: 0x060027B4 RID: 10164 RVA: 0x0007CE1E File Offset: 0x0007B01E
		[DataMember]
		public string Date
		{
			get
			{
				if (this.skippedItem.DateReceived != null)
				{
					return this.skippedItem.DateReceived.Value.ToString();
				}
				if (this.skippedItem.DateSent != null)
				{
					return this.skippedItem.DateSent.Value.ToString();
				}
				return string.Empty;
			}
			private set
			{
			}
		}

		// Token: 0x17001C24 RID: 7204
		// (get) Token: 0x060027B5 RID: 10165 RVA: 0x0007CE20 File Offset: 0x0007B020
		// (set) Token: 0x060027B6 RID: 10166 RVA: 0x0007CE2D File Offset: 0x0007B02D
		[DataMember]
		public string Subject
		{
			get
			{
				return this.skippedItem.Subject;
			}
			private set
			{
			}
		}

		// Token: 0x17001C25 RID: 7205
		// (get) Token: 0x060027B7 RID: 10167 RVA: 0x0007CE2F File Offset: 0x0007B02F
		// (set) Token: 0x060027B8 RID: 10168 RVA: 0x0007CE3C File Offset: 0x0007B03C
		[DataMember]
		public string Kind
		{
			get
			{
				return this.skippedItem.Kind;
			}
			private set
			{
			}
		}

		// Token: 0x17001C26 RID: 7206
		// (get) Token: 0x060027B9 RID: 10169 RVA: 0x0007CE3E File Offset: 0x0007B03E
		// (set) Token: 0x060027BA RID: 10170 RVA: 0x0007CE4B File Offset: 0x0007B04B
		[DataMember]
		public string FolderName
		{
			get
			{
				return this.skippedItem.FolderName;
			}
			private set
			{
			}
		}

		// Token: 0x0400201C RID: 8220
		private MigrationUserSkippedItem skippedItem;
	}
}
