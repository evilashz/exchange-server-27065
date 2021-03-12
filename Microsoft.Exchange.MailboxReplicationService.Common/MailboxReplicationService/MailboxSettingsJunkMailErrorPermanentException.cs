using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200031B RID: 795
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MailboxSettingsJunkMailErrorPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x0600253C RID: 9532 RVA: 0x00051261 File Offset: 0x0004F461
		public MailboxSettingsJunkMailErrorPermanentException(string collectionName, string itemList, string validationError) : base(MrsStrings.MailboxSettingsJunkMailError(collectionName, itemList, validationError))
		{
			this.collectionName = collectionName;
			this.itemList = itemList;
			this.validationError = validationError;
		}

		// Token: 0x0600253D RID: 9533 RVA: 0x00051286 File Offset: 0x0004F486
		public MailboxSettingsJunkMailErrorPermanentException(string collectionName, string itemList, string validationError, Exception innerException) : base(MrsStrings.MailboxSettingsJunkMailError(collectionName, itemList, validationError), innerException)
		{
			this.collectionName = collectionName;
			this.itemList = itemList;
			this.validationError = validationError;
		}

		// Token: 0x0600253E RID: 9534 RVA: 0x000512B0 File Offset: 0x0004F4B0
		protected MailboxSettingsJunkMailErrorPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.collectionName = (string)info.GetValue("collectionName", typeof(string));
			this.itemList = (string)info.GetValue("itemList", typeof(string));
			this.validationError = (string)info.GetValue("validationError", typeof(string));
		}

		// Token: 0x0600253F RID: 9535 RVA: 0x00051325 File Offset: 0x0004F525
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("collectionName", this.collectionName);
			info.AddValue("itemList", this.itemList);
			info.AddValue("validationError", this.validationError);
		}

		// Token: 0x17000D66 RID: 3430
		// (get) Token: 0x06002540 RID: 9536 RVA: 0x00051362 File Offset: 0x0004F562
		public string CollectionName
		{
			get
			{
				return this.collectionName;
			}
		}

		// Token: 0x17000D67 RID: 3431
		// (get) Token: 0x06002541 RID: 9537 RVA: 0x0005136A File Offset: 0x0004F56A
		public string ItemList
		{
			get
			{
				return this.itemList;
			}
		}

		// Token: 0x17000D68 RID: 3432
		// (get) Token: 0x06002542 RID: 9538 RVA: 0x00051372 File Offset: 0x0004F572
		public string ValidationError
		{
			get
			{
				return this.validationError;
			}
		}

		// Token: 0x04001019 RID: 4121
		private readonly string collectionName;

		// Token: 0x0400101A RID: 4122
		private readonly string itemList;

		// Token: 0x0400101B RID: 4123
		private readonly string validationError;
	}
}
