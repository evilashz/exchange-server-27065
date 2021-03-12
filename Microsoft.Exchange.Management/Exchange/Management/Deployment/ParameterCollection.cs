using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000219 RID: 537
	[XmlRoot(ElementName = "Parameters")]
	[ClassAccessLevel(AccessLevel.Consumer)]
	public class ParameterCollection : List<Parameter>
	{
	}
}
