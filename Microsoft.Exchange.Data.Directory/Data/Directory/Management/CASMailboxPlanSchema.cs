using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x020006E6 RID: 1766
	internal class CASMailboxPlanSchema : ADPresentationSchema
	{
		// Token: 0x06005264 RID: 21092 RVA: 0x0012F410 File Offset: 0x0012D610
		internal override ADObjectSchema GetParentSchema()
		{
			return ObjectSchema.GetInstance<ADUserSchema>();
		}

		// Token: 0x040037B8 RID: 14264
		public static readonly ADPropertyDefinition ActiveSyncDebugLogging = ADUserSchema.ActiveSyncDebugLogging;

		// Token: 0x040037B9 RID: 14265
		public static readonly ADPropertyDefinition ActiveSyncEnabled = ADUserSchema.ActiveSyncEnabled;

		// Token: 0x040037BA RID: 14266
		public static readonly ADPropertyDefinition ActiveSyncMailboxPolicy = ADUserSchema.ActiveSyncMailboxPolicy;

		// Token: 0x040037BB RID: 14267
		public static readonly ADPropertyDefinition DisplayName = ADRecipientSchema.DisplayName;

		// Token: 0x040037BC RID: 14268
		public static readonly ADPropertyDefinition ECPEnabled = ADRecipientSchema.ECPEnabled;

		// Token: 0x040037BD RID: 14269
		public static readonly ADPropertyDefinition ImapEnabled = ADRecipientSchema.ImapEnabled;

		// Token: 0x040037BE RID: 14270
		public static readonly ADPropertyDefinition ImapUseProtocolDefaults = ADRecipientSchema.ImapUseProtocolDefaults;

		// Token: 0x040037BF RID: 14271
		public static readonly ADPropertyDefinition ImapMessagesRetrievalMimeFormat = ADRecipientSchema.ImapMessagesRetrievalMimeFormat;

		// Token: 0x040037C0 RID: 14272
		public static readonly ADPropertyDefinition ImapEnableExactRFC822Size = ADRecipientSchema.ImapEnableExactRFC822Size;

		// Token: 0x040037C1 RID: 14273
		public static readonly ADPropertyDefinition ImapProtocolLoggingEnabled = ADRecipientSchema.ImapProtocolLoggingEnabled;

		// Token: 0x040037C2 RID: 14274
		public static readonly ADPropertyDefinition ImapSuppressReadReceipt = ADRecipientSchema.ImapSuppressReadReceipt;

		// Token: 0x040037C3 RID: 14275
		public static readonly ADPropertyDefinition ImapForceICalForCalendarRetrievalOption = ADRecipientSchema.ImapForceICalForCalendarRetrievalOption;

		// Token: 0x040037C4 RID: 14276
		public static readonly ADPropertyDefinition MAPIEnabled = ADRecipientSchema.MAPIEnabled;

		// Token: 0x040037C5 RID: 14277
		public static readonly ADPropertyDefinition MapiHttpEnabled = ADRecipientSchema.MapiHttpEnabled;

		// Token: 0x040037C6 RID: 14278
		public static readonly ADPropertyDefinition MAPIBlockOutlookNonCachedMode = ADRecipientSchema.MAPIBlockOutlookNonCachedMode;

		// Token: 0x040037C7 RID: 14279
		public static readonly ADPropertyDefinition MAPIBlockOutlookVersions = ADRecipientSchema.MAPIBlockOutlookVersions;

		// Token: 0x040037C8 RID: 14280
		public static readonly ADPropertyDefinition MAPIBlockOutlookRpcHttp = ADRecipientSchema.MAPIBlockOutlookRpcHttp;

		// Token: 0x040037C9 RID: 14281
		public static readonly ADPropertyDefinition MAPIBlockOutlookExternalConnectivity = ADRecipientSchema.MAPIBlockOutlookExternalConnectivity;

		// Token: 0x040037CA RID: 14282
		public static readonly ADPropertyDefinition OwaMailboxPolicy = ADUserSchema.OwaMailboxPolicy;

		// Token: 0x040037CB RID: 14283
		public static readonly ADPropertyDefinition OWAEnabled = ADRecipientSchema.OWAEnabled;

		// Token: 0x040037CC RID: 14284
		public static readonly ADPropertyDefinition OWAforDevicesEnabled = ADUserSchema.OWAforDevicesEnabled;

		// Token: 0x040037CD RID: 14285
		public static readonly ADPropertyDefinition PopEnabled = ADRecipientSchema.PopEnabled;

		// Token: 0x040037CE RID: 14286
		public static readonly ADPropertyDefinition PopUseProtocolDefaults = ADRecipientSchema.PopUseProtocolDefaults;

		// Token: 0x040037CF RID: 14287
		public static readonly ADPropertyDefinition PopMessagesRetrievalMimeFormat = ADRecipientSchema.PopMessagesRetrievalMimeFormat;

		// Token: 0x040037D0 RID: 14288
		public static readonly ADPropertyDefinition PopEnableExactRFC822Size = ADRecipientSchema.PopEnableExactRFC822Size;

		// Token: 0x040037D1 RID: 14289
		public static readonly ADPropertyDefinition PopProtocolLoggingEnabled = ADRecipientSchema.PopProtocolLoggingEnabled;

		// Token: 0x040037D2 RID: 14290
		public static readonly ADPropertyDefinition PopSuppressReadReceipt = ADRecipientSchema.PopSuppressReadReceipt;

		// Token: 0x040037D3 RID: 14291
		public static readonly ADPropertyDefinition PopForceICalForCalendarRetrievalOption = ADRecipientSchema.PopForceICalForCalendarRetrievalOption;

		// Token: 0x040037D4 RID: 14292
		public static readonly ADPropertyDefinition RemotePowerShellEnabled = ADRecipientSchema.RemotePowerShellEnabled;

		// Token: 0x040037D5 RID: 14293
		public static readonly ADPropertyDefinition EwsEnabled = ADRecipientSchema.EwsEnabled;

		// Token: 0x040037D6 RID: 14294
		public static readonly ADPropertyDefinition EwsAllowOutlook = ADRecipientSchema.EwsAllowOutlook;

		// Token: 0x040037D7 RID: 14295
		public static readonly ADPropertyDefinition EwsAllowMacOutlook = ADRecipientSchema.EwsAllowMacOutlook;

		// Token: 0x040037D8 RID: 14296
		public static readonly ADPropertyDefinition EwsAllowEntourage = ADRecipientSchema.EwsAllowEntourage;

		// Token: 0x040037D9 RID: 14297
		public static readonly ADPropertyDefinition EwsApplicationAccessPolicy = ADRecipientSchema.EwsApplicationAccessPolicy;

		// Token: 0x040037DA RID: 14298
		public static readonly ADPropertyDefinition EwsExceptions = ADRecipientSchema.EwsExceptions;
	}
}
