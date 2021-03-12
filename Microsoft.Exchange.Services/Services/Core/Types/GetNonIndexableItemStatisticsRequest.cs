using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000441 RID: 1089
	[DataContract(Name = "GetNonIndexableItemStatisticsRequest", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "GetNonIndexableItemStatisticsType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class GetNonIndexableItemStatisticsRequest : BaseRequest
	{
		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06001FF6 RID: 8182 RVA: 0x000A16DD File Offset: 0x0009F8DD
		// (set) Token: 0x06001FF7 RID: 8183 RVA: 0x000A16E5 File Offset: 0x0009F8E5
		[XmlArrayItem(ElementName = "LegacyDN", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", Type = typeof(string))]
		[DataMember(Name = "Mailboxes", IsRequired = true)]
		[XmlArray(ElementName = "Mailboxes", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public string[] Mailboxes { get; set; }

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06001FF8 RID: 8184 RVA: 0x000A16EE File Offset: 0x0009F8EE
		// (set) Token: 0x06001FF9 RID: 8185 RVA: 0x000A16F6 File Offset: 0x0009F8F6
		[XmlElement("SearchArchiveOnly")]
		[DataMember(Name = "SearchArchiveOnly", IsRequired = false)]
		public bool SearchArchiveOnly { get; set; }

		// Token: 0x06001FFA RID: 8186 RVA: 0x000A16FF File Offset: 0x0009F8FF
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new GetNonIndexableItemStatistics(callContext, this);
		}

		// Token: 0x06001FFB RID: 8187 RVA: 0x000A1708 File Offset: 0x0009F908
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			if (this.SearchArchiveOnly || this.Mailboxes == null || this.Mailboxes.Length <= 0)
			{
				return null;
			}
			if (this.Mailboxes[0].StartsWith("\\"))
			{
				throw FaultExceptionUtilities.CreateFault(new NonExistentMailboxException(CoreResources.IDs.MessagePublicFoldersNotSupportedForNonIndexable, this.Mailboxes[0]), FaultParty.Sender);
			}
			IRecipientSession adrecipientSession = callContext.ADRecipientSessionContext.GetADRecipientSession();
			ADRecipient adrecipient = adrecipientSession.FindByLegacyExchangeDN(this.Mailboxes[0]);
			if (adrecipient == null)
			{
				throw FaultExceptionUtilities.CreateFault(new NonExistentMailboxException(CoreResources.IDs.MessageNonExistentMailboxLegacyDN, this.Mailboxes[0]), FaultParty.Sender);
			}
			return MailboxIdServerInfo.Create(adrecipient.PrimarySmtpAddress.ToString());
		}

		// Token: 0x06001FFC RID: 8188 RVA: 0x000A17C2 File Offset: 0x0009F9C2
		internal override ResourceKey[] GetResources(CallContext callContext, int currentStep)
		{
			return null;
		}
	}
}
