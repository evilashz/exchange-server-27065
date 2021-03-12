using System;
using Microsoft.Exchange.Autodiscover.Providers;

namespace Microsoft.Exchange.Autodiscover
{
	// Token: 0x02000004 RID: 4
	internal class ProviderInfo
	{
		// Token: 0x06000018 RID: 24 RVA: 0x000027A4 File Offset: 0x000009A4
		internal ProviderInfo(Type systemType, ProviderAttribute attributes, CreateProviderDelegate createProvider)
		{
			this.systemType = systemType;
			this.attributes = attributes;
			this.createProvider = createProvider;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000027C4 File Offset: 0x000009C4
		internal bool Match(RequestData requestData)
		{
			return requestData.ResponseSchemas.Count > 0 && requestData.ResponseSchemas[0] == this.Attributes.ResponseSchema && requestData.RequestSchemas.Count > 0 && requestData.RequestSchemas[0] == this.Attributes.RequestSchema;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002829 File Offset: 0x00000A29
		internal Type SystemType
		{
			get
			{
				return this.systemType;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002831 File Offset: 0x00000A31
		internal ProviderAttribute Attributes
		{
			get
			{
				return this.attributes;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002839 File Offset: 0x00000A39
		internal CreateProviderDelegate CreateProvider
		{
			get
			{
				return this.createProvider;
			}
		}

		// Token: 0x04000011 RID: 17
		private Type systemType;

		// Token: 0x04000012 RID: 18
		private ProviderAttribute attributes;

		// Token: 0x04000013 RID: 19
		private CreateProviderDelegate createProvider;
	}
}
