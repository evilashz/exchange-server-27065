using System;
using System.Runtime.InteropServices;

namespace System.Security.Claims
{
	// Token: 0x020002F2 RID: 754
	[ComVisible(false)]
	public static class ClaimTypes
	{
		// Token: 0x04000EF3 RID: 3827
		internal const string ClaimTypeNamespace = "http://schemas.microsoft.com/ws/2008/06/identity/claims";

		// Token: 0x04000EF4 RID: 3828
		public const string AuthenticationInstant = "http://schemas.microsoft.com/ws/2008/06/identity/claims/authenticationinstant";

		// Token: 0x04000EF5 RID: 3829
		public const string AuthenticationMethod = "http://schemas.microsoft.com/ws/2008/06/identity/claims/authenticationmethod";

		// Token: 0x04000EF6 RID: 3830
		public const string CookiePath = "http://schemas.microsoft.com/ws/2008/06/identity/claims/cookiepath";

		// Token: 0x04000EF7 RID: 3831
		public const string DenyOnlyPrimarySid = "http://schemas.microsoft.com/ws/2008/06/identity/claims/denyonlyprimarysid";

		// Token: 0x04000EF8 RID: 3832
		public const string DenyOnlyPrimaryGroupSid = "http://schemas.microsoft.com/ws/2008/06/identity/claims/denyonlyprimarygroupsid";

		// Token: 0x04000EF9 RID: 3833
		public const string DenyOnlyWindowsDeviceGroup = "http://schemas.microsoft.com/ws/2008/06/identity/claims/denyonlywindowsdevicegroup";

		// Token: 0x04000EFA RID: 3834
		public const string Dsa = "http://schemas.microsoft.com/ws/2008/06/identity/claims/dsa";

		// Token: 0x04000EFB RID: 3835
		public const string Expiration = "http://schemas.microsoft.com/ws/2008/06/identity/claims/expiration";

		// Token: 0x04000EFC RID: 3836
		public const string Expired = "http://schemas.microsoft.com/ws/2008/06/identity/claims/expired";

		// Token: 0x04000EFD RID: 3837
		public const string GroupSid = "http://schemas.microsoft.com/ws/2008/06/identity/claims/groupsid";

		// Token: 0x04000EFE RID: 3838
		public const string IsPersistent = "http://schemas.microsoft.com/ws/2008/06/identity/claims/ispersistent";

		// Token: 0x04000EFF RID: 3839
		public const string PrimaryGroupSid = "http://schemas.microsoft.com/ws/2008/06/identity/claims/primarygroupsid";

		// Token: 0x04000F00 RID: 3840
		public const string PrimarySid = "http://schemas.microsoft.com/ws/2008/06/identity/claims/primarysid";

		// Token: 0x04000F01 RID: 3841
		public const string Role = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";

		// Token: 0x04000F02 RID: 3842
		public const string SerialNumber = "http://schemas.microsoft.com/ws/2008/06/identity/claims/serialnumber";

		// Token: 0x04000F03 RID: 3843
		public const string UserData = "http://schemas.microsoft.com/ws/2008/06/identity/claims/userdata";

		// Token: 0x04000F04 RID: 3844
		public const string Version = "http://schemas.microsoft.com/ws/2008/06/identity/claims/version";

		// Token: 0x04000F05 RID: 3845
		public const string WindowsAccountName = "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsaccountname";

		// Token: 0x04000F06 RID: 3846
		public const string WindowsDeviceClaim = "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsdeviceclaim";

		// Token: 0x04000F07 RID: 3847
		public const string WindowsDeviceGroup = "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsdevicegroup";

		// Token: 0x04000F08 RID: 3848
		public const string WindowsUserClaim = "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsuserclaim";

		// Token: 0x04000F09 RID: 3849
		public const string WindowsFqbnVersion = "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsfqbnversion";

		// Token: 0x04000F0A RID: 3850
		public const string WindowsSubAuthority = "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowssubauthority";

		// Token: 0x04000F0B RID: 3851
		internal const string ClaimType2005Namespace = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims";

		// Token: 0x04000F0C RID: 3852
		public const string Anonymous = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/anonymous";

		// Token: 0x04000F0D RID: 3853
		public const string Authentication = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/authentication";

		// Token: 0x04000F0E RID: 3854
		public const string AuthorizationDecision = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/authorizationdecision";

		// Token: 0x04000F0F RID: 3855
		public const string Country = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/country";

		// Token: 0x04000F10 RID: 3856
		public const string DateOfBirth = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/dateofbirth";

		// Token: 0x04000F11 RID: 3857
		public const string Dns = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/dns";

		// Token: 0x04000F12 RID: 3858
		public const string DenyOnlySid = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/denyonlysid";

		// Token: 0x04000F13 RID: 3859
		public const string Email = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";

		// Token: 0x04000F14 RID: 3860
		public const string Gender = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/gender";

		// Token: 0x04000F15 RID: 3861
		public const string GivenName = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname";

		// Token: 0x04000F16 RID: 3862
		public const string Hash = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/hash";

		// Token: 0x04000F17 RID: 3863
		public const string HomePhone = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/homephone";

		// Token: 0x04000F18 RID: 3864
		public const string Locality = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/locality";

		// Token: 0x04000F19 RID: 3865
		public const string MobilePhone = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/mobilephone";

		// Token: 0x04000F1A RID: 3866
		public const string Name = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";

		// Token: 0x04000F1B RID: 3867
		public const string NameIdentifier = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";

		// Token: 0x04000F1C RID: 3868
		public const string OtherPhone = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/otherphone";

		// Token: 0x04000F1D RID: 3869
		public const string PostalCode = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/postalcode";

		// Token: 0x04000F1E RID: 3870
		public const string Rsa = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/rsa";

		// Token: 0x04000F1F RID: 3871
		public const string Sid = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid";

		// Token: 0x04000F20 RID: 3872
		public const string Spn = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/spn";

		// Token: 0x04000F21 RID: 3873
		public const string StateOrProvince = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/stateorprovince";

		// Token: 0x04000F22 RID: 3874
		public const string StreetAddress = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/streetaddress";

		// Token: 0x04000F23 RID: 3875
		public const string Surname = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname";

		// Token: 0x04000F24 RID: 3876
		public const string System = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/system";

		// Token: 0x04000F25 RID: 3877
		public const string Thumbprint = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/thumbprint";

		// Token: 0x04000F26 RID: 3878
		public const string Upn = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/upn";

		// Token: 0x04000F27 RID: 3879
		public const string Uri = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/uri";

		// Token: 0x04000F28 RID: 3880
		public const string Webpage = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/webpage";

		// Token: 0x04000F29 RID: 3881
		public const string X500DistinguishedName = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/x500distinguishedname";

		// Token: 0x04000F2A RID: 3882
		internal const string ClaimType2009Namespace = "http://schemas.xmlsoap.org/ws/2009/09/identity/claims";

		// Token: 0x04000F2B RID: 3883
		public const string Actor = "http://schemas.xmlsoap.org/ws/2009/09/identity/claims/actor";
	}
}
