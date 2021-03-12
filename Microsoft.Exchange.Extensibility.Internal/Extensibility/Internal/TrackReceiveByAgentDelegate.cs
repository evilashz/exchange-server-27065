using System;

namespace Microsoft.Exchange.Extensibility.Internal
{
	// Token: 0x0200005D RID: 93
	// (Invoke) Token: 0x0600032F RID: 815
	internal delegate void TrackReceiveByAgentDelegate(ITransportMailItemFacade mailItem, string sourceContext, string connectorId, long? relatedMailItemId);
}
