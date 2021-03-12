using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000193 RID: 403
	[ClassAccessLevel(AccessLevel.Consumer)]
	[XmlInclude(typeof(SetupComponentInfoReference))]
	public class SetupComponentInfoReference
	{
		// Token: 0x040006E3 RID: 1763
		public string RelativeFileLocation;
	}
}
