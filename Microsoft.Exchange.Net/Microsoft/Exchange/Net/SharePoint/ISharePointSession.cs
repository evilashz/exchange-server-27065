using System;
using System.Collections.Specialized;
using System.IO;

namespace Microsoft.Exchange.Net.SharePoint
{
	// Token: 0x0200092C RID: 2348
	public interface ISharePointSession
	{
		// Token: 0x06003255 RID: 12885
		bool DoesFileExist(string fileUrl);

		// Token: 0x06003256 RID: 12886
		string UploadFile(string fileUrl, Stream inStream, Action heartbeat, out NameValueCollection propertyBag);

		// Token: 0x06003257 RID: 12887
		void DownloadFile(string fileUrl, SharepointFileDownloadHelper writeStream);
	}
}
