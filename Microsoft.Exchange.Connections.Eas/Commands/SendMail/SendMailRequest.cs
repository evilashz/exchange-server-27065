using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Connections.Eas.Model.Request.ComposeMail;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Commands.SendMail
{
	// Token: 0x02000061 RID: 97
	[XmlRoot(ElementName = "SendMail", Namespace = "ComposeMail", IsNullable = false)]
	[ClassAccessLevel(AccessLevel.Implementation)]
	public class SendMailRequest : SendMail
	{
	}
}
