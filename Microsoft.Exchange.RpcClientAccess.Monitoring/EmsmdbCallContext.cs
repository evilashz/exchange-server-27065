using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MapiHttp;
using Microsoft.Exchange.Rpc;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x0200000D RID: 13
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class EmsmdbCallContext<TResult> : RpcCallContext<TResult> where TResult : EmsmdbCallResult
	{
		// Token: 0x06000050 RID: 80 RVA: 0x000028D9 File Offset: 0x00000AD9
		public EmsmdbCallContext(IExchangeAsyncDispatch protocolClient, TimeSpan timeout, AsyncCallback asyncCallback, object asyncState) : base(timeout, asyncCallback, asyncState)
		{
			Util.ThrowOnNullArgument(protocolClient, "protocolClient");
			this.protocolClient = protocolClient;
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000051 RID: 81 RVA: 0x000028F7 File Offset: 0x00000AF7
		protected IExchangeAsyncDispatch ProtocolClient
		{
			get
			{
				return this.protocolClient;
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002900 File Offset: 0x00000B00
		protected IPropertyBag GetHttpResponseInformation()
		{
			PropertyBag propertyBag = new PropertyBag();
			EmsmdbHttpClient emsmdbHttpClient = this.ProtocolClient as EmsmdbHttpClient;
			if (emsmdbHttpClient != null)
			{
				propertyBag.Set(ContextPropertySchema.ResponseHeaderCollection, emsmdbHttpClient.LastHttpResponseHeaders);
				propertyBag.Set(ContextPropertySchema.ResponseStatusCode, emsmdbHttpClient.LastResponseStatusCode);
				propertyBag.Set(ContextPropertySchema.ResponseStatusCodeDescription, emsmdbHttpClient.LastResponseStatusDescription);
				propertyBag.Set(ContextPropertySchema.RequestHeaderCollection, emsmdbHttpClient.LastHttpRequestHeaders);
				return propertyBag;
			}
			return null;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002970 File Offset: 0x00000B70
		protected IPropertyBag GetHttpInformationFromProtocolException(ProtocolException protocolException)
		{
			PropertyBag propertyBag = new PropertyBag();
			propertyBag.Set(ContextPropertySchema.RequestHeaderCollection, protocolException.HttpRequestHeaders);
			propertyBag.Set(ContextPropertySchema.ResponseHeaderCollection, protocolException.HttpResponseHeaders);
			ProtocolFailureException ex = protocolException as ProtocolFailureException;
			if (ex != null)
			{
				propertyBag.Set(ContextPropertySchema.ResponseStatusCode, ex.HttpStatusCode);
				propertyBag.Set(ContextPropertySchema.ResponseStatusCodeDescription, ex.HttpStatusDescription);
			}
			return propertyBag;
		}

		// Token: 0x04000020 RID: 32
		private readonly IExchangeAsyncDispatch protocolClient;
	}
}
