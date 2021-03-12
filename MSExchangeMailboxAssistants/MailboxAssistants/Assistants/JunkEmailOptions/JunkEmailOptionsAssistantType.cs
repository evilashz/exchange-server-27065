using System;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.JunkEmailOptions
{
	// Token: 0x02000113 RID: 275
	internal sealed class JunkEmailOptionsAssistantType : IEventBasedAssistantType, IAssistantType, IMailboxFilter
	{
		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000B3B RID: 2875 RVA: 0x0004869D File Offset: 0x0004689D
		public LocalizedString Name
		{
			get
			{
				return Strings.jeoName;
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000B3C RID: 2876 RVA: 0x000486A4 File Offset: 0x000468A4
		public string NonLocalizedName
		{
			get
			{
				return "JunkEmailOptionsAssistant";
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000B3D RID: 2877 RVA: 0x000486AB File Offset: 0x000468AB
		public MapiEventTypeFlags EventMask
		{
			get
			{
				return (MapiEventTypeFlags)(-1);
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000B3E RID: 2878 RVA: 0x000486AE File Offset: 0x000468AE
		public bool NeedsMailboxSession
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000B3F RID: 2879 RVA: 0x000486B1 File Offset: 0x000468B1
		public bool ProcessesPublicDatabases
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000B40 RID: 2880 RVA: 0x000486B4 File Offset: 0x000468B4
		public PropertyDefinition[] PreloadItemProperties
		{
			get
			{
				return JunkEmailOptionsAssistantType.EmptyPreloadItemProperties;
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000B41 RID: 2881 RVA: 0x000486BB File Offset: 0x000468BB
		public Guid Identity
		{
			get
			{
				return AssistantIdentity.JunkEmailOptionsAssistant;
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000B42 RID: 2882 RVA: 0x000486C2 File Offset: 0x000468C2
		public MailboxType MailboxType
		{
			get
			{
				return MailboxType.User;
			}
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x000486C5 File Offset: 0x000468C5
		public IEventBasedAssistant CreateInstance(DatabaseInfo databaseInfo)
		{
			return new JunkEmailOptionsAssistant(databaseInfo, this.Name, this.NonLocalizedName);
		}

		// Token: 0x04000709 RID: 1801
		internal const string AssistantName = "JunkEmailOptionsAssistant";

		// Token: 0x0400070A RID: 1802
		private static readonly PropertyDefinition[] EmptyPreloadItemProperties = new PropertyDefinition[0];
	}
}
