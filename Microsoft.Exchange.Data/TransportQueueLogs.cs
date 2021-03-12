using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200028F RID: 655
	[XmlRoot(Namespace = "", ElementName = "TransportQueueLogs")]
	[Serializable]
	public class TransportQueueLogs : List<TransportQueueLog>
	{
		// Token: 0x04000DC7 RID: 3527
		internal static readonly XmlSerializer Serializer = new XmlSerializer(typeof(TransportQueueLogs));
	}
}
