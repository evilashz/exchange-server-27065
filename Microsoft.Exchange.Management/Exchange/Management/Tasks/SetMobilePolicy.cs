using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000444 RID: 1092
	[Cmdlet("set", "MobileDeviceMailboxPolicy", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public class SetMobilePolicy : SetMailboxPolicyBase<MobileMailboxPolicy>
	{
		// Token: 0x17000B8F RID: 2959
		// (get) Token: 0x060026A7 RID: 9895 RVA: 0x00098ED4 File Offset: 0x000970D4
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				if (this.updateOtherDefaultPolicies)
				{
					return Strings.ConfirmationMessageSwitchMobileMailboxDefaultPolicy(this.Identity.ToString());
				}
				return base.ConfirmationMessage;
			}
		}

		// Token: 0x060026A8 RID: 9896 RVA: 0x00098EF8 File Offset: 0x000970F8
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (this.DataObject.IsDefault)
			{
				QueryFilter extraFilter = new ComparisonFilter(ComparisonOperator.NotEqual, ADObjectSchema.Guid, this.DataObject.Id.ObjectGuid);
				this.otherDefaultPolicies = DefaultMobileMailboxPolicyUtility<MobileMailboxPolicy>.GetDefaultPolicies((IConfigurationSession)base.DataSession, extraFilter);
				if (this.otherDefaultPolicies.Count > 0)
				{
					this.updateOtherDefaultPolicies = true;
				}
			}
			if (!DefaultMobileMailboxPolicyUtility<MobileMailboxPolicy>.ValidateLength(this.DataObject.UnapprovedInROMApplicationList, 5120, 2048))
			{
				base.WriteError(new ArgumentException(Strings.MobileDevicePolicyApplicationListTooLong(5120, 2048), "UnapprovedInROMApplicationList"), ErrorCategory.InvalidArgument, this.DataObject.Identity);
			}
			if (!DefaultMobileMailboxPolicyUtility<MobileMailboxPolicy>.ValidateLength(this.DataObject.ApprovedApplicationList, 7168, 2048))
			{
				base.WriteError(new ArgumentException(Strings.MobileDevicePolicyApplicationListTooLong(7168, 2048), "ApprovedApplicationList"), ErrorCategory.InvalidArgument, this.DataObject.Identity);
			}
		}

		// Token: 0x060026A9 RID: 9897 RVA: 0x00099000 File Offset: 0x00097200
		protected override void InternalProcessRecord()
		{
			if (!this.DataObject.PasswordEnabled && base.UserSpecifiedParameters.Contains(MobileMailboxPolicySchema.PasswordEnabled.Name) && !base.UserSpecifiedParameters.Contains(MobileMailboxPolicySchema.AllowSimplePassword.Name) && !base.UserSpecifiedParameters.Contains(MobileMailboxPolicySchema.AlphanumericPasswordRequired.Name) && !base.UserSpecifiedParameters.Contains(MobileMailboxPolicySchema.MinPasswordComplexCharacters.Name) && !base.UserSpecifiedParameters.Contains(MobileMailboxPolicySchema.MinPasswordLength.Name) && !base.UserSpecifiedParameters.Contains(MobileMailboxPolicySchema.MaxPasswordFailedAttempts.Name) && !base.UserSpecifiedParameters.Contains(MobileMailboxPolicySchema.MaxInactivityTimeLock.Name) && !base.UserSpecifiedParameters.Contains(MobileMailboxPolicySchema.PasswordRecoveryEnabled.Name) && !base.UserSpecifiedParameters.Contains(MobileMailboxPolicySchema.PasswordExpiration.Name) && !base.UserSpecifiedParameters.Contains(MobileMailboxPolicySchema.PasswordHistory.Name) && !base.UserSpecifiedParameters.Contains(MobileMailboxPolicySchema.RequireDeviceEncryption.Name) && !base.UserSpecifiedParameters.Contains(MobileMailboxPolicySchema.RequireStorageCardEncryption.Name) && !base.UserSpecifiedParameters.Contains("DeviceEncryptionEnabled"))
			{
				this.DataObject.AllowSimplePassword = (bool)MobileMailboxPolicySchema.AllowSimplePassword.DefaultValue;
				this.DataObject.AlphanumericPasswordRequired = (bool)MobileMailboxPolicySchema.AlphanumericPasswordRequired.DefaultValue;
				this.DataObject.MinPasswordComplexCharacters = (int)MobileMailboxPolicySchema.MinPasswordComplexCharacters.DefaultValue;
				this.DataObject.MinPasswordLength = (int?)MobileMailboxPolicySchema.MinPasswordLength.DefaultValue;
				this.DataObject.MaxPasswordFailedAttempts = (Unlimited<int>)MobileMailboxPolicySchema.MaxPasswordFailedAttempts.DefaultValue;
				this.DataObject.MaxInactivityTimeLock = (Unlimited<EnhancedTimeSpan>)MobileMailboxPolicySchema.MaxInactivityTimeLock.DefaultValue;
				this.DataObject.PasswordRecoveryEnabled = (bool)MobileMailboxPolicySchema.PasswordRecoveryEnabled.DefaultValue;
				this.DataObject.PasswordExpiration = (Unlimited<EnhancedTimeSpan>)MobileMailboxPolicySchema.PasswordExpiration.DefaultValue;
				this.DataObject.PasswordHistory = (int)MobileMailboxPolicySchema.PasswordHistory.DefaultValue;
				this.DataObject.RequireDeviceEncryption = (bool)MobileMailboxPolicySchema.RequireDeviceEncryption.DefaultValue;
				this.DataObject.DeviceEncryptionEnabled = (bool)MobileMailboxPolicySchema.RequireStorageCardEncryption.DefaultValue;
				this.DataObject.RequireStorageCardEncryption = (bool)MobileMailboxPolicySchema.RequireStorageCardEncryption.DefaultValue;
			}
			base.InternalProcessRecord();
			if (this.updateOtherDefaultPolicies)
			{
				try
				{
					DefaultMailboxPolicyUtility<MobileMailboxPolicy>.ClearDefaultPolicies((IConfigurationSession)base.DataSession, this.otherDefaultPolicies);
				}
				catch (DataSourceTransientException exception)
				{
					base.WriteError(exception, ErrorCategory.ReadError, null);
				}
			}
		}
	}
}
