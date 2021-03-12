using System;
using System.Collections;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.MonadDataProvider
{
	// Token: 0x020001BA RID: 442
	internal class MobileMailboxPolicyCreator : ConfigurableObjectCreator
	{
		// Token: 0x06000FB4 RID: 4020 RVA: 0x0002F95C File Offset: 0x0002DB5C
		internal override IList<string> GetProperties(string fullName)
		{
			return new string[]
			{
				"Identity",
				"Name",
				"WhenChanged",
				"DevicePolicyRefreshInterval",
				"AllowNonProvisionableDevices",
				"IsDefaultPolicy",
				"PasswordEnabled",
				"AlphanumericPasswordRequired",
				"MinPasswordComplexCharacters",
				"PasswordRecoveryEnabled",
				"RequireStorageCardEncryption",
				"RequireDeviceEncryption",
				"MaxInactivityTimeLock",
				"MaxPasswordFailedAttempts",
				"MinPasswordLength",
				"AllowSimplePassword",
				"PasswordExpiration",
				"PasswordHistory",
				"MaxCalendarAgeFilter",
				"MaxEmailAgeFilter",
				"MaxEmailBodyTruncationSize",
				"RequireManualSyncWhenRoaming",
				"AllowHTMLEmail",
				"AttachmentsEnabled",
				"MaxAttachmentSize",
				"AllowStorageCard",
				"AllowCamera",
				"AllowWiFi",
				"AllowIrDA",
				"AllowInternetSharing",
				"AllowRemoteDesktop",
				"AllowDesktopSync",
				"AllowBluetooth",
				"AllowBrowser",
				"AllowConsumerEmail",
				"AllowUnsignedApplications",
				"AllowUnsignedInstallationPackages",
				"ApprovedApplicationList",
				"UnapprovedInROMApplicationList"
			};
		}

		// Token: 0x06000FB5 RID: 4021 RVA: 0x0002FAC8 File Offset: 0x0002DCC8
		protected override void FillProperty(Type type, PSObject psObject, ConfigurableObject configObject, string propertyName)
		{
			if (propertyName == "ApprovedApplicationList")
			{
				ApprovedApplicationCollection approvedApplicationCollection = new ApprovedApplicationCollection();
				IList list = ((PSObject)psObject.Members[propertyName].Value).BaseObject as IList;
				foreach (object prop in list)
				{
					approvedApplicationCollection.Add(MockObjectCreator.GetSingleProperty(prop, typeof(ApprovedApplication)));
				}
				configObject.propertyBag[MobileMailboxPolicySchema.ADApprovedApplicationList] = approvedApplicationCollection;
				return;
			}
			base.FillProperty(type, psObject, configObject, propertyName);
		}
	}
}
