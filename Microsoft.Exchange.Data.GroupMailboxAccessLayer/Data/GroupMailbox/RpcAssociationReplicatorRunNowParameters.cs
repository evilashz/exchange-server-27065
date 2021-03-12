using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.GroupMailbox;
using Microsoft.Exchange.SoapWebClient.EWS;

namespace Microsoft.Exchange.Data.GroupMailbox
{
	// Token: 0x02000034 RID: 52
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class RpcAssociationReplicatorRunNowParameters
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000188 RID: 392 RVA: 0x0000BD61 File Offset: 0x00009F61
		// (set) Token: 0x06000189 RID: 393 RVA: 0x0000BD69 File Offset: 0x00009F69
		public ICollection<IMailboxLocator> SlaveMailboxes { get; set; }

		// Token: 0x0600018A RID: 394 RVA: 0x0000BD74 File Offset: 0x00009F74
		public static RpcAssociationReplicatorRunNowParameters Parse(string input, IRecipientSession adSession)
		{
			SafeXmlSerializer safeXmlSerializer = new SafeXmlSerializer(typeof(List<MailboxLocatorType>));
			RpcAssociationReplicatorRunNowParameters result;
			using (StringReader stringReader = new StringReader(input))
			{
				List<MailboxLocatorType> locators = safeXmlSerializer.Deserialize(stringReader) as List<MailboxLocatorType>;
				result = RpcAssociationReplicatorRunNowParameters.Instantiate(locators, adSession);
			}
			return result;
		}

		// Token: 0x0600018B RID: 395 RVA: 0x0000BDCC File Offset: 0x00009FCC
		public override string ToString()
		{
			List<MailboxLocatorType> list = new List<MailboxLocatorType>(this.SlaveMailboxes.Count);
			foreach (IMailboxLocator locator in this.SlaveMailboxes)
			{
				list.Add(EwsAssociationDataConverter.Convert(locator));
			}
			SafeXmlSerializer safeXmlSerializer = new SafeXmlSerializer(list.GetType());
			string result;
			using (StringWriter stringWriter = new StringWriter())
			{
				safeXmlSerializer.Serialize(stringWriter, list);
				result = stringWriter.ToString();
			}
			return result;
		}

		// Token: 0x0600018C RID: 396 RVA: 0x0000BE70 File Offset: 0x0000A070
		private static RpcAssociationReplicatorRunNowParameters Instantiate(List<MailboxLocatorType> locators, IRecipientSession adSession)
		{
			ArgumentValidator.ThrowIfNull("adSession", adSession);
			if (locators != null && locators.Count > 0)
			{
				List<IMailboxLocator> list = new List<IMailboxLocator>(locators.Count);
				for (int i = 0; i < locators.Count; i++)
				{
					list.Add(EwsAssociationDataConverter.Convert(locators[i], adSession));
				}
				return new RpcAssociationReplicatorRunNowParameters
				{
					SlaveMailboxes = list
				};
			}
			return new RpcAssociationReplicatorRunNowParameters();
		}

		// Token: 0x040000C7 RID: 199
		private static readonly Trace Tracer = ExTraceGlobals.AssociationReplicationTracer;
	}
}
