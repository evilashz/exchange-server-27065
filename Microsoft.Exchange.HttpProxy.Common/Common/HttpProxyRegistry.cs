using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.HttpProxy;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.HttpProxy.Common
{
	// Token: 0x02000015 RID: 21
	internal static class HttpProxyRegistry
	{
		// Token: 0x04000081 RID: 129
		public static readonly LazyMember<bool> AreGccStoredSecretKeysValid = new LazyMember<bool>(delegate()
		{
			bool result;
			try
			{
				result = GccUtils.AreStoredSecretKeysValid();
			}
			catch (InvalidDatacenterProxyKeyException)
			{
				ExTraceGlobals.VerboseTracer.TraceDebug(0L, "[HttpProxyRegistry::AreGccStoredSecretKeysValid] InvalidDatacenterProxyKeyException encountered when calling AreStoredSecretKeysValid");
				result = false;
			}
			return result;
		});
	}
}
