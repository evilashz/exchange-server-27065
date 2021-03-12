using System;
using System.Collections.Generic;
using Microsoft.Exchange.Connections.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas
{
	// Token: 0x0200000A RID: 10
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class EasServerCapabilities : ServerCapabilities
	{
		// Token: 0x06000058 RID: 88 RVA: 0x00002994 File Offset: 0x00000B94
		internal EasServerCapabilities()
		{
		}

		// Token: 0x06000059 RID: 89 RVA: 0x0000299C File Offset: 0x00000B9C
		internal EasServerCapabilities(IEnumerable<string> capabilities) : base(capabilities)
		{
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600005A RID: 90 RVA: 0x000029A5 File Offset: 0x00000BA5
		internal bool SupportsEasAutodiscover
		{
			get
			{
				return base.Supports("Autodiscover");
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600005B RID: 91 RVA: 0x000029B2 File Offset: 0x00000BB2
		internal bool SupportsEasFolderCreate
		{
			get
			{
				return base.Supports("FolderCreate");
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600005C RID: 92 RVA: 0x000029BF File Offset: 0x00000BBF
		internal bool SupportsEasFolderDelete
		{
			get
			{
				return base.Supports("FolderDelete");
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600005D RID: 93 RVA: 0x000029CC File Offset: 0x00000BCC
		internal bool SupportsEasFolderSync
		{
			get
			{
				return base.Supports("FolderSync");
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600005E RID: 94 RVA: 0x000029D9 File Offset: 0x00000BD9
		internal bool SupportsEasFolderUpdate
		{
			get
			{
				return base.Supports("FolderUpdate");
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600005F RID: 95 RVA: 0x000029E6 File Offset: 0x00000BE6
		internal bool SupportsEasGetAttachment
		{
			get
			{
				return base.Supports("GetAttachment");
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000060 RID: 96 RVA: 0x000029F3 File Offset: 0x00000BF3
		internal bool SupportsEasGetItemEstimate
		{
			get
			{
				return base.Supports("GetItemEstimate");
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00002A00 File Offset: 0x00000C00
		internal bool SupportsEasItemOperations
		{
			get
			{
				return base.Supports("ItemOperations");
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00002A0D File Offset: 0x00000C0D
		internal bool SupportsEasMeetingResponse
		{
			get
			{
				return base.Supports("MeetingResponse");
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00002A1A File Offset: 0x00000C1A
		internal bool SupportsEasMoveItems
		{
			get
			{
				return base.Supports("MoveItems");
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00002A27 File Offset: 0x00000C27
		internal bool SupportsEasPing
		{
			get
			{
				return base.Supports("Ping");
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00002A34 File Offset: 0x00000C34
		internal bool SupportsEasProvision
		{
			get
			{
				return base.Supports("Provision");
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002A41 File Offset: 0x00000C41
		internal bool SupportsEasResolveRecipients
		{
			get
			{
				return base.Supports("ResolveRecipients");
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00002A4E File Offset: 0x00000C4E
		internal bool SupportsEasSearch
		{
			get
			{
				return base.Supports("Search");
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00002A5B File Offset: 0x00000C5B
		internal bool SupportsEasSendMail
		{
			get
			{
				return base.Supports("SendMail");
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00002A68 File Offset: 0x00000C68
		internal bool SupportsEasSettings
		{
			get
			{
				return base.Supports("Settings");
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00002A75 File Offset: 0x00000C75
		internal bool SupportsEasSmartForward
		{
			get
			{
				return base.Supports("SmartForward");
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00002A82 File Offset: 0x00000C82
		internal bool SupportsEasSmartReply
		{
			get
			{
				return base.Supports("SmartReply");
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00002A8F File Offset: 0x00000C8F
		internal bool SupportsEasSync
		{
			get
			{
				return base.Supports("Sync");
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00002A9C File Offset: 0x00000C9C
		internal bool SupportsEasValidateCert
		{
			get
			{
				return base.Supports("ValidateCert");
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00002AA9 File Offset: 0x00000CA9
		internal bool SupportsEasVersion140
		{
			get
			{
				return base.Supports("14.0");
			}
		}

		// Token: 0x04000039 RID: 57
		internal const string EasAutodiscoverCapability = "Autodiscover";

		// Token: 0x0400003A RID: 58
		internal const string EasFolderCreateCapability = "FolderCreate";

		// Token: 0x0400003B RID: 59
		internal const string EasFolderDeleteCapability = "FolderDelete";

		// Token: 0x0400003C RID: 60
		internal const string EasFolderSyncCapability = "FolderSync";

		// Token: 0x0400003D RID: 61
		internal const string EasFolderUpdateCapability = "FolderUpdate";

		// Token: 0x0400003E RID: 62
		internal const string EasGetAttachmentCapability = "GetAttachment";

		// Token: 0x0400003F RID: 63
		internal const string EasGetItemEstimateCapability = "GetItemEstimate";

		// Token: 0x04000040 RID: 64
		internal const string EasItemOperationsCapability = "ItemOperations";

		// Token: 0x04000041 RID: 65
		internal const string EasMeetingResponseCapability = "MeetingResponse";

		// Token: 0x04000042 RID: 66
		internal const string EasMoveItemsCapability = "MoveItems";

		// Token: 0x04000043 RID: 67
		internal const string EasPingCapability = "Ping";

		// Token: 0x04000044 RID: 68
		internal const string EasProvisionCapability = "Provision";

		// Token: 0x04000045 RID: 69
		internal const string EasResolveRecipientsCapability = "ResolveRecipients";

		// Token: 0x04000046 RID: 70
		internal const string EasSearchCapability = "Search";

		// Token: 0x04000047 RID: 71
		internal const string EasSendMailCapability = "SendMail";

		// Token: 0x04000048 RID: 72
		internal const string EasSettingsCapability = "Settings";

		// Token: 0x04000049 RID: 73
		internal const string EasSmartForwardCapability = "SmartForward";

		// Token: 0x0400004A RID: 74
		internal const string EasSmartReplyCapability = "SmartReply";

		// Token: 0x0400004B RID: 75
		internal const string EasSyncCapability = "Sync";

		// Token: 0x0400004C RID: 76
		internal const string EasValidateCertCapability = "ValidateCert";

		// Token: 0x0400004D RID: 77
		internal const string EasVersion140Capability = "14.0";
	}
}
