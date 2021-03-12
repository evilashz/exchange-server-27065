using System;
using System.Collections.Generic;
using System.Web;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x0200003C RID: 60
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class DictionaryBasedOperationFactory : IAsyncOperationFactory
	{
		// Token: 0x06000219 RID: 537 RVA: 0x0000C8CC File Offset: 0x0000AACC
		public DictionaryBasedOperationFactory(IDictionary<string, Func<HttpContextBase, AsyncOperation>> operationFactoryMethods)
		{
			foreach (string text in operationFactoryMethods.Keys)
			{
				this.operationFactoryMethods[text.ToLowerInvariant()] = operationFactoryMethods[text];
			}
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000C93C File Offset: 0x0000AB3C
		public AsyncOperation Create(string requestType, HttpContextBase context)
		{
			Func<HttpContextBase, AsyncOperation> func;
			if (this.operationFactoryMethods.TryGetValue(requestType.ToLowerInvariant(), out func))
			{
				return func(context);
			}
			throw ProtocolException.FromResponseCode((LID)64032, string.Format("Unknown request type {0}", requestType), ResponseCode.InvalidRequestType, null);
		}

		// Token: 0x040000F4 RID: 244
		private readonly IDictionary<string, Func<HttpContextBase, AsyncOperation>> operationFactoryMethods = new Dictionary<string, Func<HttpContextBase, AsyncOperation>>();
	}
}
