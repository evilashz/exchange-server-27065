using System;
using System.Collections.Generic;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.UM.ClientAccess;
using Microsoft.Exchange.UM.PersonalAutoAttendant;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x0200047E RID: 1150
	public class UnifiedMessagingOptions : OwaPage, IRegistryOnlyForm
	{
		// Token: 0x17000D13 RID: 3347
		// (get) Token: 0x06002C36 RID: 11318 RVA: 0x000F64D7 File Offset: 0x000F46D7
		protected Toolbar PersonalAutoAttendantToolBar
		{
			get
			{
				return this.personalAutoAttendantToolbar;
			}
		}

		// Token: 0x17000D14 RID: 3348
		// (get) Token: 0x06002C37 RID: 11319 RVA: 0x000F64DF File Offset: 0x000F46DF
		protected string TelephoneFolderId
		{
			get
			{
				return this.telephoneFolderId.ToBase64String();
			}
		}

		// Token: 0x17000D15 RID: 3349
		// (get) Token: 0x06002C38 RID: 11320 RVA: 0x000F64EC File Offset: 0x000F46EC
		// (set) Token: 0x06002C39 RID: 11321 RVA: 0x000F64F4 File Offset: 0x000F46F4
		protected string TelephoneFolderName
		{
			get
			{
				return this.telephoneFolderName;
			}
			set
			{
				this.telephoneFolderName = Utilities.HtmlEncode(value);
			}
		}

		// Token: 0x17000D16 RID: 3350
		// (get) Token: 0x06002C3A RID: 11322 RVA: 0x000F6502 File Offset: 0x000F4702
		protected string TelephoneNumber
		{
			get
			{
				return this.dialOnPhoneNumber;
			}
		}

		// Token: 0x17000D17 RID: 3351
		// (get) Token: 0x06002C3B RID: 11323 RVA: 0x000F650A File Offset: 0x000F470A
		protected bool IsOutOfOfficeGreeting
		{
			get
			{
				return this.isOutOfOffice;
			}
		}

		// Token: 0x17000D18 RID: 3352
		// (get) Token: 0x06002C3C RID: 11324 RVA: 0x000F6512 File Offset: 0x000F4712
		protected bool IsFIFOOrder
		{
			get
			{
				return this.isFIFOOrder;
			}
		}

		// Token: 0x17000D19 RID: 3353
		// (get) Token: 0x06002C3D RID: 11325 RVA: 0x000F651A File Offset: 0x000F471A
		protected UMSMSNotificationOptions SMSNotificationValue
		{
			get
			{
				return this.smsNotificationValue;
			}
		}

		// Token: 0x17000D1A RID: 3354
		// (get) Token: 0x06002C3E RID: 11326 RVA: 0x000F6522 File Offset: 0x000F4722
		protected bool IsPinlessVoicemail
		{
			get
			{
				return this.isPinlessVoicemail;
			}
		}

		// Token: 0x17000D1B RID: 3355
		// (get) Token: 0x06002C3F RID: 11327 RVA: 0x000F652A File Offset: 0x000F472A
		protected bool IsDialPlanEnterprise
		{
			get
			{
				return this.isDialPlanEnterprise;
			}
		}

		// Token: 0x17000D1C RID: 3356
		// (get) Token: 0x06002C40 RID: 11328 RVA: 0x000F6532 File Offset: 0x000F4732
		protected bool IsVoicemailPreviewReceive
		{
			get
			{
				return this.isVoicemailPreviewReceive;
			}
		}

		// Token: 0x17000D1D RID: 3357
		// (get) Token: 0x06002C41 RID: 11329 RVA: 0x000F653A File Offset: 0x000F473A
		protected bool IsVoicemailPreviewSend
		{
			get
			{
				return this.isVoicemailPreviewSend;
			}
		}

		// Token: 0x17000D1E RID: 3358
		// (get) Token: 0x06002C42 RID: 11330 RVA: 0x000F6542 File Offset: 0x000F4742
		protected bool IsMissedCallNotification
		{
			get
			{
				return this.isMissedCallNotification;
			}
		}

		// Token: 0x17000D1F RID: 3359
		// (get) Token: 0x06002C43 RID: 11331 RVA: 0x000F654A File Offset: 0x000F474A
		protected bool IsEnabledForVoicemailPreview
		{
			get
			{
				return this.enabledForVoicemailPreview;
			}
		}

		// Token: 0x17000D20 RID: 3360
		// (get) Token: 0x06002C44 RID: 11332 RVA: 0x000F6552 File Offset: 0x000F4752
		protected bool IsEnabledForPinlessVoicemail
		{
			get
			{
				return this.enabledForPinlessVoicemail;
			}
		}

		// Token: 0x17000D21 RID: 3361
		// (get) Token: 0x06002C45 RID: 11333 RVA: 0x000F655A File Offset: 0x000F475A
		protected bool IsEnabledForSMSNotification
		{
			get
			{
				return this.enabledForSMSNotification;
			}
		}

		// Token: 0x17000D22 RID: 3362
		// (get) Token: 0x06002C46 RID: 11334 RVA: 0x000F6562 File Offset: 0x000F4762
		protected bool IsEnabledForMissedCallNotification
		{
			get
			{
				return this.enabledForMissedCallNotification;
			}
		}

		// Token: 0x17000D23 RID: 3363
		// (get) Token: 0x06002C47 RID: 11335 RVA: 0x000F656A File Offset: 0x000F476A
		protected bool IsEnabledForPersonalAutoAttendant
		{
			get
			{
				return this.enabledForPersonalAutoAttendant;
			}
		}

		// Token: 0x17000D24 RID: 3364
		// (get) Token: 0x06002C48 RID: 11336 RVA: 0x000F6572 File Offset: 0x000F4772
		protected bool IsEnabledForOutDialing
		{
			get
			{
				return this.enabledForOutdialing;
			}
		}

		// Token: 0x17000D25 RID: 3365
		// (get) Token: 0x06002C49 RID: 11337 RVA: 0x000F657A File Offset: 0x000F477A
		protected bool IsPlayOnPhoneEnabled
		{
			get
			{
				return this.isPlayOnPhoneEnabled;
			}
		}

		// Token: 0x17000D26 RID: 3366
		// (get) Token: 0x06002C4A RID: 11338 RVA: 0x000F6582 File Offset: 0x000F4782
		protected Infobar PaaInfobar
		{
			get
			{
				return this.infobar;
			}
		}

		// Token: 0x06002C4B RID: 11339 RVA: 0x000F658C File Offset: 0x000F478C
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.infobar = new Infobar();
			using (UMClientCommon umclientCommon = new UMClientCommon(base.UserContext.ExchangePrincipal))
			{
				if (!umclientCommon.IsUMEnabled())
				{
					throw new OwaInvalidRequestException("User is not UM enabled");
				}
				this.isPlayOnPhoneEnabled = umclientCommon.IsPlayOnPhoneEnabled();
				this.enabledForPinlessVoicemail = umclientCommon.UMPolicy.AllowPinlessVoiceMailAccess;
				this.enabledForSMSNotification = umclientCommon.IsSmsNotificationsEnabled();
				this.enabledForVoicemailPreview = umclientCommon.UMPolicy.AllowVoiceMailPreview;
				this.enabledForMissedCallNotification = umclientCommon.UMPolicy.AllowMissedCallNotifications;
				this.isDialPlanEnterprise = (umclientCommon.DialPlan.SubscriberType == UMSubscriberType.Enterprise);
				UMPropertiesEx umproperties = umclientCommon.GetUMProperties();
				this.isOutOfOffice = umproperties.OofStatus;
				this.isFIFOOrder = umproperties.ReadUnreadVoicemailInFIFOOrder;
				this.isMissedCallNotification = umproperties.MissedCallNotificationEnabled;
				this.dialOnPhoneNumber = umproperties.PlayOnPhoneDialString;
				this.telephoneAccessNumbers = umproperties.TelephoneAccessNumbers;
				this.isPinlessVoicemail = umproperties.PinlessAccessToVoicemail;
				this.smsNotificationValue = umproperties.SMSNotificationOption;
				this.isVoicemailPreviewReceive = umproperties.ReceivedVoiceMailPreviewEnabled;
				this.isVoicemailPreviewSend = umproperties.SentVoiceMailPreviewEnabled;
				try
				{
					this.telephoneFolderId = StoreObjectId.FromProviderSpecificId(Convert.FromBase64String(umproperties.TelephoneAccessFolderEmail));
				}
				catch (CorruptDataException arg)
				{
					ExTraceGlobals.CoreCallTracer.TraceDebug<string, CorruptDataException>(0L, "Invalid format for Folder ID string: {0}. Exception: {1}", umproperties.TelephoneAccessFolderEmail, arg);
				}
				catch (FormatException arg2)
				{
					ExTraceGlobals.CoreCallTracer.TraceDebug<string, FormatException>(0L, "Invalid format for Folder ID string: {0}. Exception: {1}", umproperties.TelephoneAccessFolderEmail, arg2);
				}
			}
			if (this.telephoneFolderId == null)
			{
				this.telephoneFolderId = base.UserContext.InboxFolderId;
			}
			try
			{
				using (Folder folder = Folder.Bind(base.UserContext.MailboxSession, this.telephoneFolderId, new PropertyDefinition[]
				{
					FolderSchema.DisplayName
				}))
				{
					this.TelephoneFolderName = folder.DisplayName;
				}
			}
			catch (ObjectNotFoundException)
			{
				ExTraceGlobals.CoreCallTracer.TraceDebug<StoreObjectId>(0L, "The folder doesn't exist. Folder Id: {0}", this.telephoneFolderId);
				this.telephoneFolderId = base.UserContext.InboxFolderId;
				using (Folder folder2 = Folder.Bind(base.UserContext.MailboxSession, this.telephoneFolderId, new PropertyDefinition[]
				{
					FolderSchema.DisplayName
				}))
				{
					this.TelephoneFolderName = folder2.DisplayName;
				}
			}
			using (IPAAStore ipaastore = PAAStore.Create(base.UserContext.ExchangePrincipal))
			{
				ipaastore.GetUserPermissions(out this.enabledForPersonalAutoAttendant, out this.enabledForOutdialing);
				if (this.enabledForPersonalAutoAttendant)
				{
					this.personalAutoAttendantToolbar = new PersonalAutoAttendantListToolbar();
				}
			}
		}

		// Token: 0x06002C4C RID: 11340 RVA: 0x000F68C0 File Offset: 0x000F4AC0
		protected void RenderPersonalAutoAttendantList()
		{
			using (IPAAStore ipaastore = PAAStore.Create(base.UserContext.ExchangePrincipal))
			{
				IList<PersonalAutoAttendant> personalAutoAttendants = null;
				ipaastore.TryGetAutoAttendants(PAAValidationMode.StopOnFirstError, out personalAutoAttendants);
				PersonalAutoAttendantListView personalAutoAttendantListView = new PersonalAutoAttendantListView(base.UserContext, personalAutoAttendants);
				personalAutoAttendantListView.Render(base.Response.Output);
			}
		}

		// Token: 0x06002C4D RID: 11341 RVA: 0x000F6924 File Offset: 0x000F4B24
		protected void RenderTelephoneHeader()
		{
			if (string.IsNullOrEmpty(this.telephoneAccessNumbers))
			{
				base.Response.Output.Write(LocalizedStrings.GetHtmlEncoded(1121744548));
				return;
			}
			base.Response.Output.Write(LocalizedStrings.GetHtmlEncoded(1879630091), "<B>" + Utilities.HtmlEncode(this.telephoneAccessNumbers) + "</B>");
		}

		// Token: 0x06002C4E RID: 11342 RVA: 0x000F698D File Offset: 0x000F4B8D
		protected bool IsSMSConfigured()
		{
			if (this.isLazySMSConfigured == null)
			{
				this.isLazySMSConfigured = new bool?(UnifiedMessagingOptions.IsSMSConfigured(base.UserContext));
			}
			return this.isLazySMSConfigured.Value;
		}

		// Token: 0x06002C4F RID: 11343 RVA: 0x000F69C0 File Offset: 0x000F4BC0
		internal static bool IsSMSConfigured(UserContext context)
		{
			bool result;
			using (VersionedXmlDataProvider versionedXmlDataProvider = new VersionedXmlDataProvider(context.MailboxSession))
			{
				ADObjectId objectId = context.MailboxSession.MailboxOwner.ObjectId;
				TextMessagingAccount textMessagingAccount = (TextMessagingAccount)versionedXmlDataProvider.Read<TextMessagingAccount>(objectId);
				result = (textMessagingAccount != null && textMessagingAccount.NotificationPhoneNumber != null && textMessagingAccount.NotificationPhoneNumberVerified);
			}
			return result;
		}

		// Token: 0x04001D16 RID: 7446
		private bool isOutOfOffice;

		// Token: 0x04001D17 RID: 7447
		private bool isFIFOOrder;

		// Token: 0x04001D18 RID: 7448
		private string telephoneAccessNumbers;

		// Token: 0x04001D19 RID: 7449
		private string dialOnPhoneNumber;

		// Token: 0x04001D1A RID: 7450
		private StoreObjectId telephoneFolderId;

		// Token: 0x04001D1B RID: 7451
		private string telephoneFolderName;

		// Token: 0x04001D1C RID: 7452
		private bool isMissedCallNotification;

		// Token: 0x04001D1D RID: 7453
		private bool enabledForPersonalAutoAttendant;

		// Token: 0x04001D1E RID: 7454
		private bool enabledForOutdialing;

		// Token: 0x04001D1F RID: 7455
		private bool enabledForMissedCallNotification;

		// Token: 0x04001D20 RID: 7456
		private bool enabledForVoicemailPreview;

		// Token: 0x04001D21 RID: 7457
		private bool enabledForPinlessVoicemail;

		// Token: 0x04001D22 RID: 7458
		private bool enabledForSMSNotification;

		// Token: 0x04001D23 RID: 7459
		private bool isPlayOnPhoneEnabled;

		// Token: 0x04001D24 RID: 7460
		private bool isPinlessVoicemail;

		// Token: 0x04001D25 RID: 7461
		private bool isDialPlanEnterprise;

		// Token: 0x04001D26 RID: 7462
		private bool isVoicemailPreviewReceive;

		// Token: 0x04001D27 RID: 7463
		private bool isVoicemailPreviewSend;

		// Token: 0x04001D28 RID: 7464
		private Toolbar personalAutoAttendantToolbar;

		// Token: 0x04001D29 RID: 7465
		private UMSMSNotificationOptions smsNotificationValue;

		// Token: 0x04001D2A RID: 7466
		private bool? isLazySMSConfigured;

		// Token: 0x04001D2B RID: 7467
		private Infobar infobar;
	}
}
