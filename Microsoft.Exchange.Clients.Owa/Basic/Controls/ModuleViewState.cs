using System;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Basic.Controls
{
	// Token: 0x0200001F RID: 31
	internal abstract class ModuleViewState : ClientViewState
	{
		// Token: 0x060000EA RID: 234 RVA: 0x00007B5F File Offset: 0x00005D5F
		public ModuleViewState(NavigationModule navigationModule, StoreObjectId folderId, string folderType)
		{
			this.navigationModule = navigationModule;
			this.folderId = folderId;
			this.folderType = folderType;
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00007B7C File Offset: 0x00005D7C
		public StoreObjectId FolderId
		{
			get
			{
				return this.folderId;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00007B84 File Offset: 0x00005D84
		public string FolderType
		{
			get
			{
				return this.folderType;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00007B8C File Offset: 0x00005D8C
		public NavigationModule NavigationModule
		{
			get
			{
				return this.navigationModule;
			}
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00007B94 File Offset: 0x00005D94
		public override PreFormActionResponse ToPreFormActionResponse()
		{
			PreFormActionResponse preFormActionResponse = new PreFormActionResponse();
			preFormActionResponse.ApplicationElement = ApplicationElement.Folder;
			preFormActionResponse.Type = this.FolderType;
			if (this.FolderId != null)
			{
				preFormActionResponse.AddParameter("id", this.FolderId.ToBase64String());
			}
			return preFormActionResponse;
		}

		// Token: 0x0400008F RID: 143
		private StoreObjectId folderId;

		// Token: 0x04000090 RID: 144
		private string folderType;

		// Token: 0x04000091 RID: 145
		private NavigationModule navigationModule;
	}
}
