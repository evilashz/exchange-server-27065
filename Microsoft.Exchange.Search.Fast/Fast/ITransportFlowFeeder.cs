using System;
using System.IO;
using Microsoft.Exchange.Search.Core.Common;

namespace Microsoft.Exchange.Search.Fast
{
	// Token: 0x0200001F RID: 31
	internal interface ITransportFlowFeeder
	{
		// Token: 0x060001CB RID: 459
		void ProcessMessage(Stream mimeStream, Stream propertyStream, TransportFlowMessageFlags transportFlowMessageFlags);
	}
}
