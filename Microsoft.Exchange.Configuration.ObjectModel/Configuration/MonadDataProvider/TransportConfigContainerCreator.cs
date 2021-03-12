using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Configuration.MonadDataProvider
{
	// Token: 0x020001BC RID: 444
	internal class TransportConfigContainerCreator : ConfigurableObjectCreator
	{
		// Token: 0x06000FBA RID: 4026 RVA: 0x0002FDAC File Offset: 0x0002DFAC
		internal override IList<string> GetProperties(string fullName)
		{
			return new string[]
			{
				"Name",
				"WhenChanged",
				"MaxSendSize",
				"MaxReceiveSize",
				"MaxRecipientEnvelopeLimit",
				"MaxDumpsterSizePerDatabase",
				"MaxDumpsterTime",
				"ExternalPostmasterAddress",
				"GenerateCopyOfDSNFor",
				"InternalSMTPServers"
			};
		}
	}
}
