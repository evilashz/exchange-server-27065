using System;
using System.IO;
using System.ServiceModel.Channels;
using System.Web;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000B6C RID: 2924
	internal abstract class BaseResponseRenderer
	{
		// Token: 0x060052C7 RID: 21191
		internal abstract void Render(Message message, Stream stream);

		// Token: 0x060052C8 RID: 21192
		internal abstract void Render(Message message, Stream stream, HttpResponse response);
	}
}
