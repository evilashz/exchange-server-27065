using System;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.UM;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.Mwi
{
	// Token: 0x02000105 RID: 261
	internal sealed class MwiAssistantType : IEventBasedAssistantType, IAssistantType
	{
		// Token: 0x06000AC9 RID: 2761 RVA: 0x00046C9D File Offset: 0x00044E9D
		public MwiAssistantType() : this(UMServerMwiTargetPicker.Instance)
		{
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x00046CAA File Offset: 0x00044EAA
		public MwiAssistantType(ServerPickerBase<IMwiTarget, Guid> picker)
		{
			this.serverPicker = picker;
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000ACB RID: 2763 RVA: 0x00046CB9 File Offset: 0x00044EB9
		public LocalizedString Name
		{
			get
			{
				return Strings.mwiName;
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000ACC RID: 2764 RVA: 0x00046CC0 File Offset: 0x00044EC0
		public string NonLocalizedName
		{
			get
			{
				return "MessageWaitingIndicatorAssistant";
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000ACD RID: 2765 RVA: 0x00046CC7 File Offset: 0x00044EC7
		public MapiEventTypeFlags EventMask
		{
			get
			{
				return (MapiEventTypeFlags)(-1);
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000ACE RID: 2766 RVA: 0x00046CCA File Offset: 0x00044ECA
		public bool NeedsMailboxSession
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000ACF RID: 2767 RVA: 0x00046CCD File Offset: 0x00044ECD
		public bool ProcessesPublicDatabases
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000AD0 RID: 2768 RVA: 0x00046CD0 File Offset: 0x00044ED0
		public Guid Identity
		{
			get
			{
				return AssistantIdentity.MwiAssistant;
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000AD1 RID: 2769 RVA: 0x00046CD8 File Offset: 0x00044ED8
		public PropertyDefinition[] PreloadItemProperties
		{
			get
			{
				return new PropertyDefinition[]
				{
					MessageItemSchema.VoiceMessageSenderName,
					MessageItemSchema.SenderTelephoneNumber,
					MessageItemSchema.VoiceMessageDuration,
					MessageItemSchema.PstnCallbackTelephoneNumber
				};
			}
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x00046D0D File Offset: 0x00044F0D
		public IEventBasedAssistant CreateInstance(DatabaseInfo databaseInfo)
		{
			if (!MwiAssistantType.initialized)
			{
				MwiAssistantType.initialized = true;
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMMwiAssistantStarted, null, new object[0]);
			}
			return new MwiAssistant(databaseInfo, this.serverPicker, this.Name, this.NonLocalizedName);
		}

		// Token: 0x040006E7 RID: 1767
		internal const string AssistantName = "MessageWaitingIndicatorAssistant";

		// Token: 0x040006E8 RID: 1768
		private static bool initialized;

		// Token: 0x040006E9 RID: 1769
		private ServerPickerBase<IMwiTarget, Guid> serverPicker;
	}
}
