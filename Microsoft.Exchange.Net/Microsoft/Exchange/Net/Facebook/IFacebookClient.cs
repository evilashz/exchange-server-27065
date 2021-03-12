using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net.Facebook
{
	// Token: 0x02000717 RID: 1815
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IFacebookClient : IDisposable
	{
		// Token: 0x0600225A RID: 8794
		IAsyncResult BeginGetFriends(string accessToken, string fields, string limit, string offset, AsyncCallback callback, object state);

		// Token: 0x0600225B RID: 8795
		FacebookUsersList EndGetFriends(IAsyncResult ar);

		// Token: 0x0600225C RID: 8796
		IAsyncResult BeginGetUsers(string accessToken, string userIds, string fields, AsyncCallback callback, object state);

		// Token: 0x0600225D RID: 8797
		FacebookUsersList EndGetUsers(IAsyncResult ar);

		// Token: 0x0600225E RID: 8798
		FacebookUser GetProfile(string accessToken, string fields);

		// Token: 0x0600225F RID: 8799
		FacebookImportContactsResult UploadContacts(string accessToken, bool continuous, bool async, string source, string contactsFormat, string contactsStreamContentType, Stream contacts);

		// Token: 0x06002260 RID: 8800
		void RemoveApplication(string accessToken);

		// Token: 0x06002261 RID: 8801
		void Cancel();

		// Token: 0x06002262 RID: 8802
		void SubscribeDownloadCompletedEvent(EventHandler<DownloadCompleteEventArgs> eventHandler);
	}
}
