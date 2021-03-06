using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020000EA RID: 234
	internal sealed class PolicyConfiguration : ConfigurationBase
	{
		// Token: 0x06000878 RID: 2168 RVA: 0x0001BB87 File Offset: 0x00019D87
		private PolicyConfiguration()
		{
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x0001BB90 File Offset: 0x00019D90
		internal static PolicyConfiguration GetPolicyConfigurationFromAD(IConfigurationSession session, ADObjectId owaMailboxPolicyId)
		{
			OwaMailboxPolicy[] array = session.Find<OwaMailboxPolicy>(owaMailboxPolicyId, QueryScope.Base, null, null, 1);
			OwaMailboxPolicy owaMailboxPolicy = null;
			if (array != null && array.Length == 1)
			{
				owaMailboxPolicy = array[0];
			}
			if (owaMailboxPolicy == null)
			{
				ExTraceGlobals.PolicyConfigurationTracer.TraceError<ADObjectId>(0L, "The mailbox policy {0} couldn't be found in Active Directory.", owaMailboxPolicyId);
				return null;
			}
			return PolicyConfiguration.CreatePolicyConfigurationFromOwaMailboxPolicy(owaMailboxPolicy);
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x0001BBD8 File Offset: 0x00019DD8
		internal static PolicyConfiguration CreatePolicyConfigurationFromOwaMailboxPolicy(OwaMailboxPolicy owaMailboxPolicy)
		{
			if (owaMailboxPolicy == null)
			{
				return null;
			}
			PolicyConfiguration policyConfiguration = new PolicyConfiguration();
			AttachmentPolicyLevel treatUnknownTypeAs = ConfigurationBase.AttachmentActionToPolicyLevel(new AttachmentBlockingActions?(owaMailboxPolicy.ActionForUnknownFileAndMIMETypes));
			AttachmentPolicy attachmentPolicy = new AttachmentPolicy(owaMailboxPolicy.BlockedFileTypes.ToArray(), owaMailboxPolicy.BlockedMimeTypes.ToArray(), owaMailboxPolicy.ForceSaveFileTypes.ToArray(), owaMailboxPolicy.ForceSaveMimeTypes.ToArray(), owaMailboxPolicy.AllowedFileTypes.ToArray(), owaMailboxPolicy.AllowedMimeTypes.ToArray(), treatUnknownTypeAs, owaMailboxPolicy.DirectFileAccessOnPublicComputersEnabled, owaMailboxPolicy.DirectFileAccessOnPrivateComputersEnabled, owaMailboxPolicy.ForceWacViewingFirstOnPublicComputers, owaMailboxPolicy.ForceWacViewingFirstOnPrivateComputers, owaMailboxPolicy.WacViewingOnPublicComputersEnabled, owaMailboxPolicy.WacViewingOnPrivateComputersEnabled, owaMailboxPolicy.ForceWebReadyDocumentViewingFirstOnPublicComputers, owaMailboxPolicy.ForceWebReadyDocumentViewingFirstOnPrivateComputers, owaMailboxPolicy.WebReadyDocumentViewingOnPublicComputersEnabled, owaMailboxPolicy.WebReadyDocumentViewingOnPrivateComputersEnabled, owaMailboxPolicy.WebReadyFileTypes.ToArray(), owaMailboxPolicy.WebReadyMimeTypes.ToArray(), owaMailboxPolicy.WebReadyDocumentViewingSupportedFileTypes.ToArray(), owaMailboxPolicy.WebReadyDocumentViewingSupportedMimeTypes.ToArray(), owaMailboxPolicy.WebReadyDocumentViewingForAllSupportedTypes);
			policyConfiguration.AttachmentPolicy = attachmentPolicy;
			int segmentationBits = (int)owaMailboxPolicy[OwaMailboxPolicySchema.ADMailboxFolderSet];
			int segmentationBits2 = (int)owaMailboxPolicy[OwaMailboxPolicySchema.ADMailboxFolderSet2];
			policyConfiguration.SegmentationFlags = ConfigurationBase.SetSegmentationFlags(segmentationBits, segmentationBits2);
			policyConfiguration.OutboundCharset = owaMailboxPolicy.OutboundCharset;
			policyConfiguration.UseGB18030 = owaMailboxPolicy.UseGB18030;
			policyConfiguration.UseISO885915 = owaMailboxPolicy.UseISO885915;
			policyConfiguration.InstantMessagingType = ((owaMailboxPolicy.InstantMessagingType != null) ? owaMailboxPolicy.InstantMessagingType.Value : InstantMessagingTypeOptions.None);
			policyConfiguration.DefaultTheme = owaMailboxPolicy.DefaultTheme;
			policyConfiguration.PlacesEnabled = owaMailboxPolicy.PlacesEnabled;
			policyConfiguration.WeatherEnabled = owaMailboxPolicy.WeatherEnabled;
			policyConfiguration.AllowCopyContactsToDeviceAddressBook = owaMailboxPolicy.AllowCopyContactsToDeviceAddressBook;
			policyConfiguration.AllowOfflineOn = owaMailboxPolicy.AllowOfflineOn;
			policyConfiguration.RecoverDeletedItemsEnabled = owaMailboxPolicy.RecoverDeletedItemsEnabled;
			policyConfiguration.GroupCreationEnabled = owaMailboxPolicy.GroupCreationEnabled;
			return policyConfiguration;
		}
	}
}
