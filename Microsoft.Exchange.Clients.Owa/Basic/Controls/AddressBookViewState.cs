using System;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Basic.Controls
{
	// Token: 0x02000011 RID: 17
	internal class AddressBookViewState : ClientViewState
	{
		// Token: 0x0600007A RID: 122 RVA: 0x00004F20 File Offset: 0x00003120
		public AddressBookViewState(ClientViewState lastClientViewState, AddressBook.Mode mode, int pageNumber, StoreObjectId itemId, string itemChangeKey, RecipientItemType recipientWell, ColumnId sortColumnId, SortOrder sortOrder)
		{
			this.mode = mode;
			this.pageNumber = pageNumber;
			this.itemId = itemId;
			this.itemChangeKey = itemChangeKey;
			this.recipientWell = recipientWell;
			this.sortColumnId = sortColumnId;
			this.sortOrder = sortOrder;
			AddressBookViewState addressBookViewState = lastClientViewState as AddressBookViewState;
			if (addressBookViewState != null)
			{
				this.previousViewState = addressBookViewState.PreviousViewState;
				return;
			}
			this.previousViewState = lastClientViewState;
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00004F87 File Offset: 0x00003187
		public AddressBook.Mode Mode
		{
			get
			{
				return this.mode;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00004F8F File Offset: 0x0000318F
		public RecipientItemType RecipientWell
		{
			get
			{
				return this.recipientWell;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00004F97 File Offset: 0x00003197
		public ClientViewState PreviousViewState
		{
			get
			{
				return this.previousViewState;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00004F9F File Offset: 0x0000319F
		public StoreObjectId ItemId
		{
			get
			{
				return this.itemId;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00004FA7 File Offset: 0x000031A7
		public string ItemChangeKey
		{
			get
			{
				return this.itemChangeKey;
			}
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00004FB0 File Offset: 0x000031B0
		public override PreFormActionResponse ToPreFormActionResponse()
		{
			PreFormActionResponse preFormActionResponse = new PreFormActionResponse();
			preFormActionResponse.ApplicationElement = ApplicationElement.Dialog;
			preFormActionResponse.Type = "AddressBook";
			PreFormActionResponse preFormActionResponse2 = preFormActionResponse;
			string name = "ctx";
			int num = (int)this.mode;
			preFormActionResponse2.AddParameter(name, num.ToString());
			if (this.pageNumber > 0)
			{
				preFormActionResponse.AddParameter("pg", this.pageNumber.ToString());
			}
			if (this.itemId != null)
			{
				preFormActionResponse.AddParameter("id", this.itemId.ToBase64String());
			}
			PreFormActionResponse preFormActionResponse3 = preFormActionResponse;
			string name2 = "rw";
			int num2 = (int)this.recipientWell;
			preFormActionResponse3.AddParameter(name2, num2.ToString());
			PreFormActionResponse preFormActionResponse4 = preFormActionResponse;
			string name3 = "cid";
			int num3 = (int)this.sortColumnId;
			preFormActionResponse4.AddParameter(name3, num3.ToString());
			PreFormActionResponse preFormActionResponse5 = preFormActionResponse;
			string name4 = "so";
			int num4 = (int)this.sortOrder;
			preFormActionResponse5.AddParameter(name4, num4.ToString());
			return preFormActionResponse;
		}

		// Token: 0x04000050 RID: 80
		private AddressBook.Mode mode;

		// Token: 0x04000051 RID: 81
		private int pageNumber;

		// Token: 0x04000052 RID: 82
		private StoreObjectId itemId;

		// Token: 0x04000053 RID: 83
		private string itemChangeKey;

		// Token: 0x04000054 RID: 84
		private RecipientItemType recipientWell;

		// Token: 0x04000055 RID: 85
		private ColumnId sortColumnId;

		// Token: 0x04000056 RID: 86
		private SortOrder sortOrder;

		// Token: 0x04000057 RID: 87
		private ClientViewState previousViewState;
	}
}
