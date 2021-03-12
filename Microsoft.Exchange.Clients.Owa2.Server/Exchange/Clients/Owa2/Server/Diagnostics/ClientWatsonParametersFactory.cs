using System;
using System.Collections.Concurrent;

namespace Microsoft.Exchange.Clients.Owa2.Server.Diagnostics
{
	// Token: 0x02000435 RID: 1077
	internal class ClientWatsonParametersFactory
	{
		// Token: 0x060024C4 RID: 9412 RVA: 0x00085514 File Offset: 0x00083714
		public static ClientWatsonParameters GetInstance(string owaVersion)
		{
			if (!ClientWatsonParametersFactory.clientWatsonParametersCollection.ContainsKey(owaVersion))
			{
				ClientWatsonParameters value = new ClientWatsonParameters(owaVersion);
				ClientWatsonParametersFactory.clientWatsonParametersCollection.TryAdd(owaVersion, value);
			}
			return ClientWatsonParametersFactory.clientWatsonParametersCollection[owaVersion];
		}

		// Token: 0x060024C5 RID: 9413 RVA: 0x00085550 File Offset: 0x00083750
		public static void Shutdown()
		{
			foreach (string key in ClientWatsonParametersFactory.clientWatsonParametersCollection.Keys)
			{
				ClientWatsonParameters clientWatsonParameters;
				if (ClientWatsonParametersFactory.clientWatsonParametersCollection.ContainsKey(key) && ClientWatsonParametersFactory.clientWatsonParametersCollection.TryGetValue(key, out clientWatsonParameters) && clientWatsonParameters != null)
				{
					clientWatsonParameters.Dispose();
				}
			}
			ClientWatsonParametersFactory.clientWatsonParametersCollection.Clear();
		}

		// Token: 0x04001438 RID: 5176
		private static ConcurrentDictionary<string, ClientWatsonParameters> clientWatsonParametersCollection = new ConcurrentDictionary<string, ClientWatsonParameters>();
	}
}
