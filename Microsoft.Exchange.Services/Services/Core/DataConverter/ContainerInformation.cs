using System;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000208 RID: 520
	internal sealed class ContainerInformation : XmlElementInformation
	{
		// Token: 0x06000D92 RID: 3474 RVA: 0x00043C08 File Offset: 0x00041E08
		public ContainerInformation(string localName, string path, string namespaceUri, ExchangeVersion effectiveVersion) : base(localName, path, namespaceUri, effectiveVersion)
		{
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x00043C15 File Offset: 0x00041E15
		public ContainerInformation(string localName, string path, ExchangeVersion effectiveVersion) : this(localName, path, ServiceXml.DefaultNamespaceUri, effectiveVersion)
		{
		}
	}
}
