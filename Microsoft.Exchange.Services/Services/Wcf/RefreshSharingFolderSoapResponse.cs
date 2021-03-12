﻿using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D0D RID: 3341
	[MessageContract(IsWrapped = false)]
	public class RefreshSharingFolderSoapResponse : BaseSoapResponse
	{
		// Token: 0x0400309F RID: 12447
		[MessageBodyMember(Name = "RefreshSharingFolderResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public RefreshSharingFolderResponseMessage Body;
	}
}
