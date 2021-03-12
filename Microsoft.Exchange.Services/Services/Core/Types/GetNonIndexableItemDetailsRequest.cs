using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000440 RID: 1088
	[XmlType(TypeName = "GetNonIndexableItemDetailsType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Name = "GetNonIndexableItemDetailsRequest", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class GetNonIndexableItemDetailsRequest : BaseRequest
	{
		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x06001FE6 RID: 8166 RVA: 0x000A156C File Offset: 0x0009F76C
		// (set) Token: 0x06001FE7 RID: 8167 RVA: 0x000A1574 File Offset: 0x0009F774
		[DataMember(Name = "Mailboxes", IsRequired = true)]
		[XmlArray(ElementName = "Mailboxes", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		[XmlArrayItem(ElementName = "LegacyDN", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", Type = typeof(string))]
		public string[] Mailboxes { get; set; }

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06001FE8 RID: 8168 RVA: 0x000A157D File Offset: 0x0009F77D
		// (set) Token: 0x06001FE9 RID: 8169 RVA: 0x000A1585 File Offset: 0x0009F785
		[XmlElement("PageSize")]
		[DataMember(Name = "PageSize", IsRequired = false)]
		public int? PageSize { get; set; }

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06001FEA RID: 8170 RVA: 0x000A158E File Offset: 0x0009F78E
		// (set) Token: 0x06001FEB RID: 8171 RVA: 0x000A1596 File Offset: 0x0009F796
		[DataMember(Name = "PageItemReference", IsRequired = false)]
		[XmlElement("PageItemReference")]
		public string PageItemReference { get; set; }

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06001FEC RID: 8172 RVA: 0x000A159F File Offset: 0x0009F79F
		// (set) Token: 0x06001FED RID: 8173 RVA: 0x000A15A7 File Offset: 0x0009F7A7
		[IgnoreDataMember]
		[XmlElement("PageDirection")]
		public SearchPageDirectionType? PageDirection { get; set; }

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06001FEE RID: 8174 RVA: 0x000A15B0 File Offset: 0x0009F7B0
		// (set) Token: 0x06001FEF RID: 8175 RVA: 0x000A15EA File Offset: 0x0009F7EA
		[XmlIgnore]
		[DataMember(Name = "PageDirection", IsRequired = false)]
		public string PageDirectionString
		{
			get
			{
				if (this.PageDirection == null || this.PageDirection == null)
				{
					return null;
				}
				return EnumUtilities.ToString<SearchPageDirectionType?>(this.PageDirection);
			}
			set
			{
				this.PageDirection = new SearchPageDirectionType?(EnumUtilities.Parse<SearchPageDirectionType>(value));
			}
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06001FF0 RID: 8176 RVA: 0x000A15FD File Offset: 0x0009F7FD
		// (set) Token: 0x06001FF1 RID: 8177 RVA: 0x000A1605 File Offset: 0x0009F805
		[XmlElement("SearchArchiveOnly")]
		[DataMember(Name = "SearchArchiveOnly", IsRequired = false)]
		public bool SearchArchiveOnly { get; set; }

		// Token: 0x06001FF2 RID: 8178 RVA: 0x000A160E File Offset: 0x0009F80E
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new GetNonIndexableItemDetails(callContext, this);
		}

		// Token: 0x06001FF3 RID: 8179 RVA: 0x000A1618 File Offset: 0x0009F818
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

		// Token: 0x06001FF4 RID: 8180 RVA: 0x000A16D2 File Offset: 0x0009F8D2
		internal override ResourceKey[] GetResources(CallContext callContext, int currentStep)
		{
			return null;
		}
	}
}
