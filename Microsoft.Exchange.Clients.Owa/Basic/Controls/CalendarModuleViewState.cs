using System;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Basic.Controls
{
	// Token: 0x02000020 RID: 32
	internal sealed class CalendarModuleViewState : ModuleViewState
	{
		// Token: 0x060000EF RID: 239 RVA: 0x00007BD9 File Offset: 0x00005DD9
		public CalendarModuleViewState(StoreObjectId folderId, string folderType, ExDateTime dateTime) : base(NavigationModule.Calendar, folderId, folderType)
		{
			this.dateTime = dateTime;
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00007BEB File Offset: 0x00005DEB
		public ExDateTime DateTime
		{
			get
			{
				return this.dateTime;
			}
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00007BF4 File Offset: 0x00005DF4
		public override PreFormActionResponse ToPreFormActionResponse()
		{
			PreFormActionResponse preFormActionResponse = base.ToPreFormActionResponse();
			preFormActionResponse.AddParameter("yr", this.dateTime.Year.ToString());
			preFormActionResponse.AddParameter("mn", this.dateTime.Month.ToString());
			preFormActionResponse.AddParameter("dy", this.dateTime.Day.ToString());
			return preFormActionResponse;
		}

		// Token: 0x04000092 RID: 146
		private ExDateTime dateTime;
	}
}
