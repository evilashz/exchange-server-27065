using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Connections.Eas.Model.Request.Move;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Commands.MoveItems
{
	// Token: 0x02000057 RID: 87
	[XmlRoot(ElementName = "MoveItems", Namespace = "Move", IsNullable = false)]
	[ClassAccessLevel(AccessLevel.Implementation)]
	public class MoveItemsRequest : MoveItems
	{
	}
}
