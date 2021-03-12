using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.Provisioning;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000744 RID: 1860
	[ProvisioningObjectTag("RemoteMailbox")]
	[Serializable]
	public class RemoteMailbox : MailUser
	{
		// Token: 0x06005A98 RID: 23192 RVA: 0x0013DDCD File Offset: 0x0013BFCD
		internal static bool IsRemoteMailbox(RecipientTypeDetails recipientTypeDetails)
		{
			return recipientTypeDetails == (RecipientTypeDetails)((ulong)int.MinValue) || recipientTypeDetails == RecipientTypeDetails.RemoteRoomMailbox || recipientTypeDetails == RecipientTypeDetails.RemoteEquipmentMailbox || recipientTypeDetails == RecipientTypeDetails.RemoteTeamMailbox || recipientTypeDetails == RecipientTypeDetails.RemoteSharedMailbox;
		}

		// Token: 0x06005A99 RID: 23193 RVA: 0x0013DE0A File Offset: 0x0013C00A
		public RemoteMailbox()
		{
		}

		// Token: 0x06005A9A RID: 23194 RVA: 0x0013DE12 File Offset: 0x0013C012
		public RemoteMailbox(ADUser dataObject) : base(dataObject)
		{
		}

		// Token: 0x17001F64 RID: 8036
		// (get) Token: 0x06005A9B RID: 23195 RVA: 0x0013DE1B File Offset: 0x0013C01B
		// (set) Token: 0x06005A9C RID: 23196 RVA: 0x0013DE23 File Offset: 0x0013C023
		[Parameter(Mandatory = false)]
		public ProxyAddress RemoteRoutingAddress
		{
			get
			{
				return this.ExternalEmailAddress;
			}
			set
			{
				this.ExternalEmailAddress = value;
			}
		}

		// Token: 0x17001F65 RID: 8037
		// (get) Token: 0x06005A9D RID: 23197 RVA: 0x0013DE2C File Offset: 0x0013C02C
		// (set) Token: 0x06005A9E RID: 23198 RVA: 0x0013DE3E File Offset: 0x0013C03E
		public RemoteRecipientType RemoteRecipientType
		{
			get
			{
				return (RemoteRecipientType)this[RemoteMailboxSchema.RemoteRecipientType];
			}
			internal set
			{
				this[RemoteMailboxSchema.RemoteRecipientType] = value;
			}
		}

		// Token: 0x17001F66 RID: 8038
		// (get) Token: 0x06005A9F RID: 23199 RVA: 0x0013DE51 File Offset: 0x0013C051
		public string OnPremisesOrganizationalUnit
		{
			get
			{
				return this.OrganizationalUnit;
			}
		}

		// Token: 0x17001F67 RID: 8039
		// (get) Token: 0x06005AA0 RID: 23200 RVA: 0x0013DE59 File Offset: 0x0013C059
		public ArchiveState ArchiveState
		{
			get
			{
				return (ArchiveState)this[RemoteMailboxSchema.ArchiveState];
			}
		}

		// Token: 0x17001F68 RID: 8040
		// (get) Token: 0x06005AA1 RID: 23201 RVA: 0x0013DE6B File Offset: 0x0013C06B
		internal override ADPresentationSchema PresentationSchema
		{
			get
			{
				return RemoteMailbox.schema;
			}
		}

		// Token: 0x17001F69 RID: 8041
		// (get) Token: 0x06005AA2 RID: 23202 RVA: 0x0013DE72 File Offset: 0x0013C072
		// (set) Token: 0x06005AA3 RID: 23203 RVA: 0x0013DE7A File Offset: 0x0013C07A
		private new ProxyAddress ExternalEmailAddress
		{
			get
			{
				return base.ExternalEmailAddress;
			}
			set
			{
				base.ExternalEmailAddress = value;
			}
		}

		// Token: 0x17001F6A RID: 8042
		// (get) Token: 0x06005AA4 RID: 23204 RVA: 0x0013DE83 File Offset: 0x0013C083
		private new string OrganizationalUnit
		{
			get
			{
				return base.OrganizationalUnit;
			}
		}

		// Token: 0x17001F6B RID: 8043
		// (get) Token: 0x06005AA5 RID: 23205 RVA: 0x0013DE8B File Offset: 0x0013C08B
		// (set) Token: 0x06005AA6 RID: 23206 RVA: 0x0013DE93 File Offset: 0x0013C093
		private new MacAttachmentFormat MacAttachmentFormat
		{
			get
			{
				return base.MacAttachmentFormat;
			}
			set
			{
				base.MacAttachmentFormat = value;
			}
		}

		// Token: 0x17001F6C RID: 8044
		// (get) Token: 0x06005AA7 RID: 23207 RVA: 0x0013DE9C File Offset: 0x0013C09C
		// (set) Token: 0x06005AA8 RID: 23208 RVA: 0x0013DEA4 File Offset: 0x0013C0A4
		private new MessageBodyFormat MessageBodyFormat
		{
			get
			{
				return base.MessageBodyFormat;
			}
			set
			{
				base.MessageBodyFormat = value;
			}
		}

		// Token: 0x17001F6D RID: 8045
		// (get) Token: 0x06005AA9 RID: 23209 RVA: 0x0013DEAD File Offset: 0x0013C0AD
		// (set) Token: 0x06005AAA RID: 23210 RVA: 0x0013DEB5 File Offset: 0x0013C0B5
		private new MessageFormat MessageFormat
		{
			get
			{
				return base.MessageFormat;
			}
			set
			{
				base.MessageFormat = value;
			}
		}

		// Token: 0x17001F6E RID: 8046
		// (get) Token: 0x06005AAB RID: 23211 RVA: 0x0013DEBE File Offset: 0x0013C0BE
		// (set) Token: 0x06005AAC RID: 23212 RVA: 0x0013DEC6 File Offset: 0x0013C0C6
		private new bool? SKUAssigned
		{
			get
			{
				return base.SKUAssigned;
			}
			set
			{
				base.SKUAssigned = value;
			}
		}

		// Token: 0x17001F6F RID: 8047
		// (get) Token: 0x06005AAD RID: 23213 RVA: 0x0013DECF File Offset: 0x0013C0CF
		// (set) Token: 0x06005AAE RID: 23214 RVA: 0x0013DED7 File Offset: 0x0013C0D7
		private new bool UsePreferMessageFormat
		{
			get
			{
				return base.UsePreferMessageFormat;
			}
			set
			{
				base.UsePreferMessageFormat = value;
			}
		}

		// Token: 0x17001F70 RID: 8048
		// (get) Token: 0x06005AAF RID: 23215 RVA: 0x0013DEE0 File Offset: 0x0013C0E0
		// (set) Token: 0x06005AB0 RID: 23216 RVA: 0x0013DEE8 File Offset: 0x0013C0E8
		private new UseMapiRichTextFormat UseMapiRichTextFormat
		{
			get
			{
				return base.UseMapiRichTextFormat;
			}
			set
			{
				base.UseMapiRichTextFormat = value;
			}
		}

		// Token: 0x17001F71 RID: 8049
		// (get) Token: 0x06005AB1 RID: 23217 RVA: 0x0013DEF1 File Offset: 0x0013C0F1
		// (set) Token: 0x06005AB2 RID: 23218 RVA: 0x0013DEF9 File Offset: 0x0013C0F9
		private new SmtpAddress WindowsLiveID
		{
			get
			{
				return base.WindowsLiveID;
			}
			set
			{
				base.WindowsLiveID = value;
			}
		}

		// Token: 0x17001F72 RID: 8050
		// (get) Token: 0x06005AB3 RID: 23219 RVA: 0x0013DF02 File Offset: 0x0013C102
		// (set) Token: 0x06005AB4 RID: 23220 RVA: 0x0013DF0A File Offset: 0x0013C10A
		private new SmtpAddress MicrosoftOnlineServicesID
		{
			get
			{
				return base.MicrosoftOnlineServicesID;
			}
			set
			{
				base.MicrosoftOnlineServicesID = value;
			}
		}

		// Token: 0x17001F73 RID: 8051
		// (get) Token: 0x06005AB5 RID: 23221 RVA: 0x0013DF13 File Offset: 0x0013C113
		// (set) Token: 0x06005AB6 RID: 23222 RVA: 0x0013DF1B File Offset: 0x0013C11B
		private new CountryInfo UsageLocation
		{
			get
			{
				return base.UsageLocation;
			}
			set
			{
				base.UsageLocation = value;
			}
		}

		// Token: 0x06005AB7 RID: 23223 RVA: 0x0013DF24 File Offset: 0x0013C124
		internal new static RemoteMailbox FromDataObject(ADUser dataObject)
		{
			if (dataObject == null)
			{
				return null;
			}
			return new RemoteMailbox(dataObject);
		}

		// Token: 0x04003CCD RID: 15565
		private static RemoteMailboxSchema schema = ObjectSchema.GetInstance<RemoteMailboxSchema>();
	}
}
