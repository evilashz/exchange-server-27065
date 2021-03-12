using System;
using System.Xml;
using Microsoft.IdentityModel.Configuration;

namespace Microsoft.Exchange.Security.Authentication
{
	// Token: 0x02000015 RID: 21
	internal class AdfsIdentifyModelSection : MicrosoftIdentityModelSection
	{
		// Token: 0x06000098 RID: 152 RVA: 0x00007472 File Offset: 0x00005672
		internal void Deserialize(XmlReader reader)
		{
			this.DeserializeElement(reader, false);
		}
	}
}
