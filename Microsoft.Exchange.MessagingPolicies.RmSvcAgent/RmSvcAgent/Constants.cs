using System;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.MessagingPolicies.RmSvcAgent
{
	// Token: 0x0200001A RID: 26
	internal static class Constants
	{
		// Token: 0x040000BA RID: 186
		public const string RecipientListToGeneratePL = "Microsoft.Exchange.RMSEncryptionAgent.RecipientListForPL";

		// Token: 0x040000BB RID: 187
		public const string CacheKeySeparator = "|";

		// Token: 0x040000BC RID: 188
		public const string HtmlBegin = "<HTML><HEAD></HEAD><BODY>";

		// Token: 0x040000BD RID: 189
		public const string HtmlBR = "<BR>";

		// Token: 0x040000BE RID: 190
		public const string HtmlEnd = "</BODY></HTML>";

		// Token: 0x040000BF RID: 191
		public const string NDRStatusCode = "550";

		// Token: 0x040000C0 RID: 192
		public const string NDRStatusEnhancedCode = "5.7.1";

		// Token: 0x040000C1 RID: 193
		public const string RetryStatusCode = "451";

		// Token: 0x040000C2 RID: 194
		public const string RetryStatusEnhancedCode = "4.3.2";

		// Token: 0x040000C3 RID: 195
		public const string ActiveAgentsCapReached = "Already processing maximum number of messages.";

		// Token: 0x040000C4 RID: 196
		public const string ContactSystemAdministrator = "Please contact your system administrator for more information.";

		// Token: 0x040000C5 RID: 197
		public const string DeliveryNotAuthorized = "Delivery not authorized, message refused.";

		// Token: 0x040000C6 RID: 198
		public const string EncryptionDisabled = "Cannot RMS protect the message because Encryption is disabled in Microsoft Exchange Transport.";

		// Token: 0x040000C7 RID: 199
		public const string EncryptionDisabledOME = "Cannot OME protect the message because Encryption is disabled in Microsoft Exchange Transport.";

		// Token: 0x040000C8 RID: 200
		public const string NDRGenericOME = "Cannot OME protect the message. Error Code: {0}.";

		// Token: 0x040000C9 RID: 201
		public const string NDRPublishLicenseLimitExceededOME = "Cannot OME protect the message because there are too many recipients.";

		// Token: 0x040000CA RID: 202
		public const string Enterprise = "Enterprise";

		// Token: 0x040000CB RID: 203
		public const string FailedToFindAddressEntry = "Failed to find AddressEntry.";

		// Token: 0x040000CC RID: 204
		public const string FailedToFindTemplate = "A failure occurred when trying to look up Rights Management Server template '{0}'.";

		// Token: 0x040000CD RID: 205
		public const string FailedToReadOrganizationConfiguration = "A transient error occurred when reading configuration for {0} from AD.";

		// Token: 0x040000CE RID: 206
		public const string FailedToReadUserConfiguration = "A transient error occurred when reading user configuration from AD.";

		// Token: 0x040000CF RID: 207
		public const string JournalReportDecryptionTransientError = "A transient error occurred during journal decryption when communicating with RMS server {0}.";

		// Token: 0x040000D0 RID: 208
		public const string NoValidRecipients = "No valid recipients.";

		// Token: 0x040000D1 RID: 209
		public const string PrelicenseTransientError = "A transient error occurred during prelicensing when communicating with RMS server {0}.";

		// Token: 0x040000D2 RID: 210
		public const string SenderNotAuthorized = "The sender is not authorized to send e-mail messages to this e-mail address.";

		// Token: 0x040000D3 RID: 211
		public const string Tenant = "Tenant '{0}'";

		// Token: 0x040000D4 RID: 212
		public const string UnableToObtainPLForDLUnderLiveRMS = "Cannot RMS protect a recipient that is a distribution group under Live RMS.";

		// Token: 0x040000D5 RID: 213
		public const string UnableToPipelineDecrypt = "Microsoft Exchange Transport cannot RMS decrypt the message.";

		// Token: 0x040000D6 RID: 214
		public const string UnableToPipelineReEncryptNoPL = "Microsoft Exchange Transport cannot RMS re-encrypt the message (ATTR1).";

		// Token: 0x040000D7 RID: 215
		public const string UnableToPipelineReEncryptNoUL = "Microsoft Exchange Transport cannot RMS re-encrypt the message (ATTR2).";

		// Token: 0x040000D8 RID: 216
		public const string UnableToPipelineReEncryptNoLicenseUri = "Microsoft Exchange Transport cannot RMS re-encrypt the message (ATTR4).";

		// Token: 0x040000D9 RID: 217
		public const string FailedToPipelineReEncrypt = "Microsoft Exchange Transport cannot RMS re-encrypt the message (ATTR3).";

		// Token: 0x040000DA RID: 218
		public const string PrelicenseTransientErrorWithFailureCodesSame = "A transient error occurred during prelicensing when communicating with RMS server {0}. Failure Code {1}";

		// Token: 0x040000DB RID: 219
		public const string PrelicenseTransientErrorWithFailureCodesDifferent = "A transient error occurred during prelicensing when communicating with RMS server {0}. First recipient {1} failure code is {2}";

		// Token: 0x040000DC RID: 220
		public const string ExceptionType = "Exception encountered: {0}.";

		// Token: 0x040000DD RID: 221
		public const string FailureCode = "Failure Code: {0}.";

		// Token: 0x040000DE RID: 222
		public const int DefaultANSICodePage = 1252;

		// Token: 0x040000DF RID: 223
		public static readonly string SupportedMapiMessageClassForDrm = "IPM.Note";

		// Token: 0x040000E0 RID: 224
		public static readonly SmtpResponse NDRResponse = new SmtpResponse("550", "5.7.1", Constants.NDRTexts);

		// Token: 0x040000E1 RID: 225
		private static readonly string[] NDRTexts = new string[]
		{
			"Delivery not authorized, message refused.",
			"The sender is not authorized to send e-mail messages to this e-mail address.",
			"Please contact your system administrator for more information."
		};
	}
}
