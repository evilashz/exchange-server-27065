using System;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000204 RID: 516
	internal class XmlElementInformation
	{
		// Token: 0x06000D68 RID: 3432 RVA: 0x000436F4 File Offset: 0x000418F4
		public XmlElementInformation(string localName, string xmlPath, string namespaceUri, ExchangeVersion effectiveVersion)
		{
			this.localName = localName;
			this.xmlPath = xmlPath;
			this.namespaceUri = namespaceUri;
			this.effectiveVersion = effectiveVersion;
		}

		// Token: 0x06000D69 RID: 3433 RVA: 0x00043745 File Offset: 0x00041945
		public XmlElementInformation(string localName, string xmlPath, ExchangeVersion effectiveVersion) : this(localName, xmlPath, ServiceXml.DefaultNamespaceUri, effectiveVersion)
		{
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000D6A RID: 3434 RVA: 0x00043755 File Offset: 0x00041955
		public string NamespaceUri
		{
			get
			{
				return this.namespaceUri;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000D6B RID: 3435 RVA: 0x0004375D File Offset: 0x0004195D
		public string LocalName
		{
			get
			{
				return this.localName;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000D6C RID: 3436 RVA: 0x00043765 File Offset: 0x00041965
		public string Path
		{
			get
			{
				return this.xmlPath;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000D6D RID: 3437 RVA: 0x0004376D File Offset: 0x0004196D
		public ExchangeVersion EffectiveVersion
		{
			get
			{
				return this.effectiveVersion;
			}
		}

		// Token: 0x06000D6E RID: 3438 RVA: 0x00043775 File Offset: 0x00041975
		public override string ToString()
		{
			return this.localName;
		}

		// Token: 0x04000A95 RID: 2709
		private string namespaceUri = string.Empty;

		// Token: 0x04000A96 RID: 2710
		private string localName = string.Empty;

		// Token: 0x04000A97 RID: 2711
		private string xmlPath = string.Empty;

		// Token: 0x04000A98 RID: 2712
		protected ExchangeVersion effectiveVersion;
	}
}
