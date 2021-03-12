using System;
using Microsoft.Exchange.Clients.Common;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000038 RID: 56
	internal interface IAttachmentWebOperationContext : IOutgoingWebResponseContext
	{
		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000138 RID: 312
		UserAgent UserAgent { get; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000139 RID: 313
		bool IsPublicLogon { get; }

		// Token: 0x0600013A RID: 314
		void SetNoCacheNoStore();

		// Token: 0x0600013B RID: 315
		string GetRequestHeader(string name);
	}
}
