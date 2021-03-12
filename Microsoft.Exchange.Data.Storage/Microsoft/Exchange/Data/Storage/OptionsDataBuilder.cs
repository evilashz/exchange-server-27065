using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020002F4 RID: 756
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class OptionsDataBuilder : ADConsumer
	{
		// Token: 0x06002261 RID: 8801 RVA: 0x00089FC4 File Offset: 0x000881C4
		private OptionsDataBuilder(ITopologyConfigurationSession configSession, OrganizationCache cache) : base(new ADObjectId("CN=Addressing," + configSession.GetOrgContainerId().ToDNString()), configSession, cache)
		{
			ADNotificationAdapter.RegisterChangeNotification<AddressTemplate>(base.ConfigSession.GetOrgContainerId(), new ADNotificationCallback(base.OnChange), null);
		}

		// Token: 0x17000B9D RID: 2973
		// (get) Token: 0x06002262 RID: 8802 RVA: 0x0008A014 File Offset: 0x00088214
		internal static OptionsDataBuilder Instance
		{
			get
			{
				if (OptionsDataBuilder.instance != null)
				{
					return OptionsDataBuilder.instance;
				}
				OptionsDataBuilder result;
				lock (OptionsDataBuilder.lockInstance)
				{
					if (OptionsDataBuilder.instance == null)
					{
						OptionsDataBuilder.instance = new OptionsDataBuilder(ADConsumer.ADSystemConfigurationSessionInstance, new OrganizationCache());
					}
					result = OptionsDataBuilder.instance;
				}
				return result;
			}
		}

		// Token: 0x06002263 RID: 8803 RVA: 0x0008A0A0 File Offset: 0x000882A0
		internal RoutingTypeOptionsData GetOptionsData(MailboxSession session, string routingType)
		{
			string attribute = OptionsDataBuilder.MakeCannonicRoutingTypeKey(routingType, session.PreferedCulture.LCID);
			return base.Cache.Get<RoutingTypeOptionsData>(this, attribute, () => this.QueryOptionsData(session, routingType));
		}

		// Token: 0x06002264 RID: 8804 RVA: 0x0008A0FD File Offset: 0x000882FD
		private static string MakeCannonicRoutingTypeKey(string routingType, int localeId)
		{
			return string.Format("{0}:{1}", routingType.ToUpperInvariant(), localeId);
		}

		// Token: 0x06002265 RID: 8805 RVA: 0x0008A118 File Offset: 0x00088318
		private RoutingTypeOptionsData QueryOptionsData(MailboxSession session, string routingType)
		{
			Util.ThrowOnNullArgument(routingType, "routingType");
			int num = routingType.IndexOf('=');
			if (num >= 0)
			{
				routingType = routingType.Substring(0, num);
			}
			if (routingType == string.Empty)
			{
				throw new ArgumentException("routingType");
			}
			int lcid = session.PreferedCulture.LCID;
			ADObjectId descendantId;
			if (string.CompareOrdinal(routingType, "EX") == 0)
			{
				descendantId = base.Id.GetDescendantId(new ADObjectId(string.Format("CN=Exchange,CN={0:X},CN={1}", lcid, "Display-Templates")));
			}
			else
			{
				descendantId = base.Id.GetDescendantId(new ADObjectId(string.Format("CN={0},CN={1:X},CN={2}", routingType, lcid, "Address-Templates")));
			}
			AddressTemplate addressTemplate = base.ConfigSession.Read<AddressTemplate>(descendantId);
			RoutingTypeOptionsData result;
			if (addressTemplate != null)
			{
				ADRawEntry adrawEntry = base.ConfigSession.ReadADRawEntry(addressTemplate.Id, new ADPropertyDefinition[]
				{
					AddressTemplateSchema.PerMsgDialogDisplayTable,
					AddressTemplateSchema.PerRecipDialogDisplayTable,
					DetailsTemplateSchema.HelpFileName,
					DetailsTemplateSchema.HelpData32
				});
				byte[] messageData = adrawEntry[AddressTemplateSchema.PerMsgDialogDisplayTable] as byte[];
				byte[] recipientData = adrawEntry[AddressTemplateSchema.PerRecipDialogDisplayTable] as byte[];
				byte[] helpFileName = adrawEntry[DetailsTemplateSchema.HelpFileName] as byte[];
				byte[] helpFileData = adrawEntry[DetailsTemplateSchema.HelpData32] as byte[];
				result = new RoutingTypeOptionsData(messageData, recipientData, helpFileName, helpFileData);
			}
			else
			{
				result = default(RoutingTypeOptionsData);
			}
			return result;
		}

		// Token: 0x040013F5 RID: 5109
		private const string DisplayTemplates = "Display-Templates";

		// Token: 0x040013F6 RID: 5110
		private const string AddressTemplates = "Address-Templates";

		// Token: 0x040013F7 RID: 5111
		private static readonly object lockInstance = new object();

		// Token: 0x040013F8 RID: 5112
		private static OptionsDataBuilder instance = null;
	}
}
