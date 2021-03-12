using System;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Basic.Controls
{
	// Token: 0x02000067 RID: 103
	internal class MessageModuleViewState : ListViewViewState
	{
		// Token: 0x060002DC RID: 732 RVA: 0x00018DE9 File Offset: 0x00016FE9
		public MessageModuleViewState(StoreObjectId folderId, string folderType, SecondaryNavigationArea selectedUsing, int pageNumber) : base(NavigationModule.Mail, folderId, folderType, pageNumber)
		{
			this.selectedUsing = selectedUsing;
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060002DD RID: 733 RVA: 0x00018DFD File Offset: 0x00016FFD
		public SecondaryNavigationArea SelectedUsing
		{
			get
			{
				return this.selectedUsing;
			}
		}

		// Token: 0x060002DE RID: 734 RVA: 0x00018E08 File Offset: 0x00017008
		public override PreFormActionResponse ToPreFormActionResponse()
		{
			PreFormActionResponse preFormActionResponse = base.ToPreFormActionResponse();
			PreFormActionResponse preFormActionResponse2 = preFormActionResponse;
			string name = "slUsng";
			int num = (int)this.selectedUsing;
			preFormActionResponse2.AddParameter(name, num.ToString());
			return preFormActionResponse;
		}

		// Token: 0x04000215 RID: 533
		private SecondaryNavigationArea selectedUsing;
	}
}
