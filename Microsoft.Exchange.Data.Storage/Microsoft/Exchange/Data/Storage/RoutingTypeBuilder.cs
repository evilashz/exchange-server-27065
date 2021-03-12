using System;
using System.Linq;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020002F7 RID: 759
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class RoutingTypeBuilder : ADConsumer
	{
		// Token: 0x06002271 RID: 8817 RVA: 0x0008A597 File Offset: 0x00088797
		private RoutingTypeBuilder(ITopologyConfigurationSession configSession, OrganizationCache cache) : base(configSession.GetRoutingGroupId(), configSession, cache)
		{
			ADNotificationAdapter.RegisterChangeNotification<MailGateway>(base.ConfigSession.GetOrgContainerId(), new ADNotificationCallback(base.OnChange), null);
		}

		// Token: 0x17000BA0 RID: 2976
		// (get) Token: 0x06002272 RID: 8818 RVA: 0x0008A5DC File Offset: 0x000887DC
		public static RoutingTypeBuilder Instance
		{
			get
			{
				if (RoutingTypeBuilder.instance != null)
				{
					return RoutingTypeBuilder.instance;
				}
				RoutingTypeBuilder result;
				lock (RoutingTypeBuilder.lockInstance)
				{
					if (RoutingTypeBuilder.instance == null)
					{
						DirectoryHelper.DoAdCallAndTranslateExceptions(delegate
						{
							RoutingTypeBuilder.instance = new RoutingTypeBuilder(ADConsumer.ADSystemConfigurationSessionInstance, new OrganizationCache());
						}, "get_Instance");
					}
					result = RoutingTypeBuilder.instance;
				}
				return result;
			}
		}

		// Token: 0x06002273 RID: 8819 RVA: 0x0008A658 File Offset: 0x00088858
		public string[] GetRoutingTypes()
		{
			return base.Cache.Get<string[]>(this, RoutingTypeBuilder.RoutingTypes, new Func<string[]>(this.QueryRoutingTypes));
		}

		// Token: 0x06002274 RID: 8820 RVA: 0x0008A690 File Offset: 0x00088890
		private string[] QueryRoutingTypes()
		{
			return (from type in (from addressSpace in base.ConfigSession.FindPaged<MailGateway>(base.ConfigSession.GetOrgContainerId().GetChildId("Administrative Groups"), QueryScope.SubTree, null, null, ADGenericPagedReader<Microsoft.Exchange.Data.Directory.SystemConfiguration.MailGateway>.DefaultPageSize).SelectMany((MailGateway mailGateway) => mailGateway.AddressSpaces)
			select Participant.NormalizeRoutingType(addressSpace.Type)).Concat(RoutingTypeBuilder.DefaultAddressTypes).Distinct<string>()
			orderby type
			select type).ToArray<string>();
		}

		// Token: 0x040013FF RID: 5119
		private const string MailGateway = "mailGateway";

		// Token: 0x04001400 RID: 5120
		private static readonly object lockInstance = new object();

		// Token: 0x04001401 RID: 5121
		private static readonly string RoutingTypes = "RoutingTypes";

		// Token: 0x04001402 RID: 5122
		private static readonly string[] DefaultAddressTypes = new string[]
		{
			"EX",
			"SMTP",
			"X400"
		};

		// Token: 0x04001403 RID: 5123
		private static RoutingTypeBuilder instance = null;
	}
}
