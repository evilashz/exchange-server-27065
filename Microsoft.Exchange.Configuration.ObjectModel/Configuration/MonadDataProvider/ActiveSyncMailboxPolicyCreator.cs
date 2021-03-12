using System;
using System.Collections;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.MonadDataProvider
{
	// Token: 0x020001BB RID: 443
	internal class ActiveSyncMailboxPolicyCreator : ConfigurableObjectCreator
	{
		// Token: 0x06000FB7 RID: 4023 RVA: 0x0002FB84 File Offset: 0x0002DD84
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
				"DevicePasswordEnabled",
				"AlphanumericDevicePasswordRequired",
				"MinDevicePasswordComplexCharacters",
				"PasswordRecoveryEnabled",
				"RequireStorageCardEncryption",
				"RequireDeviceEncryption",
				"MaxInactivityTimeDeviceLock",
				"MaxDevicePasswordFailedAttempts",
				"MinDevicePasswordLength",
				"AllowSimpleDevicePassword",
				"DevicePasswordExpiration",
				"DevicePasswordHistory",
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

		// Token: 0x06000FB8 RID: 4024 RVA: 0x0002FCF0 File Offset: 0x0002DEF0
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
