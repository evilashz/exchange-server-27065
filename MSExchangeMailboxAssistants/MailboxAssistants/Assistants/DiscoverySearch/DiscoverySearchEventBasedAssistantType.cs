using System;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.DiscoverySearch
{
	// Token: 0x020001FF RID: 511
	internal sealed class DiscoverySearchEventBasedAssistantType : IEventBasedAssistantType, IAssistantType, IMailboxFilter
	{
		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x0600139D RID: 5021 RVA: 0x00072F5D File Offset: 0x0007115D
		public LocalizedString Name
		{
			get
			{
				return Strings.discoverySearchAssistantName;
			}
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x0600139E RID: 5022 RVA: 0x00072F64 File Offset: 0x00071164
		public string NonLocalizedName
		{
			get
			{
				return "DiscoverySearchEventBasedAssistant";
			}
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x0600139F RID: 5023 RVA: 0x00072F6B File Offset: 0x0007116B
		public MapiEventTypeFlags EventMask
		{
			get
			{
				return MapiEventTypeFlags.ObjectCreated;
			}
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x060013A0 RID: 5024 RVA: 0x00072F6E File Offset: 0x0007116E
		public bool NeedsMailboxSession
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x060013A1 RID: 5025 RVA: 0x00072F71 File Offset: 0x00071171
		public bool ProcessesPublicDatabases
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x060013A2 RID: 5026 RVA: 0x00072F74 File Offset: 0x00071174
		public PropertyDefinition[] PreloadItemProperties
		{
			get
			{
				return DiscoverySearchEventBasedAssistantType.InternalPreloadItemProperties;
			}
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x060013A3 RID: 5027 RVA: 0x00072F7B File Offset: 0x0007117B
		public Guid Identity
		{
			get
			{
				return AssistantIdentity.DiscoverySearchEventBasedAssistant;
			}
		}

		// Token: 0x060013A4 RID: 5028 RVA: 0x00072F82 File Offset: 0x00071182
		public IEventBasedAssistant CreateInstance(DatabaseInfo databaseInfo)
		{
			return new DiscoverySearchEventBasedAssistant(databaseInfo, this.Name, this.NonLocalizedName);
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x060013A5 RID: 5029 RVA: 0x00072F96 File Offset: 0x00071196
		public MailboxType MailboxType
		{
			get
			{
				return MailboxType.Arbitration;
			}
		}

		// Token: 0x04000BFF RID: 3071
		internal const string AssistantName = "DiscoverySearchEventBasedAssistant";

		// Token: 0x04000C00 RID: 3072
		internal static readonly PropertyDefinition[] InternalPreloadItemProperties = new PropertyDefinition[]
		{
			DiscoverySearchEventBasedAssistant.AlternativeId,
			DiscoverySearchEventBasedAssistant.AsynchronousActionRequest,
			DiscoverySearchEventBasedAssistant.AsynchronousActionRbacContext,
			DiscoverySearchEventBasedAssistant.Status
		};
	}
}
