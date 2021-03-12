using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace Microsoft.Exchange.Security.RightsManagement
{
	// Token: 0x0200099D RID: 2461
	[SuppressUnmanagedCodeSecurity]
	[SecurityCritical(SecurityCriticalScope.Everything)]
	internal static class SafeNativeMethods
	{
		// Token: 0x06003532 RID: 13618
		[DllImport("msdrm.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		internal static extern int DRMCreateClientSession([MarshalAs(UnmanagedType.FunctionPtr)] [In] CallbackDelegate pfnCallback, [MarshalAs(UnmanagedType.U4)] [In] uint uCallbackVersion, [MarshalAs(UnmanagedType.LPWStr)] [In] string GroupIDProviderType, [MarshalAs(UnmanagedType.LPWStr)] [In] string GroupID, out SafeRightsManagementSessionHandle phSession);

		// Token: 0x06003533 RID: 13619
		[DllImport("msdrm.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		internal static extern int DRMCloseSession([MarshalAs(UnmanagedType.U4)] [In] uint sessionHandle);

		// Token: 0x06003534 RID: 13620
		[DllImport("msdrm.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		internal static extern int DRMCloseHandle([MarshalAs(UnmanagedType.U4)] [In] uint handle);

		// Token: 0x06003535 RID: 13621
		[DllImport("msdrm.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		internal static extern int DRMCloseQueryHandle([MarshalAs(UnmanagedType.U4)] [In] uint queryHandle);

		// Token: 0x06003536 RID: 13622
		[DllImport("msdrm.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		internal static extern int DRMCloseEnvironmentHandle([MarshalAs(UnmanagedType.U4)] [In] uint envHandle);

		// Token: 0x06003537 RID: 13623
		[DllImport("msdrm.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		internal static extern int DRMIsActivated([In] SafeRightsManagementSessionHandle hSession, [MarshalAs(UnmanagedType.U4)] [In] uint uFlags, [MarshalAs(UnmanagedType.LPStruct)] [In] ActivationServerInfo activationServerInfo);

		// Token: 0x06003538 RID: 13624
		[DllImport("msdrm.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		internal static extern int DRMActivate([In] SafeRightsManagementSessionHandle hSession, [MarshalAs(UnmanagedType.U4)] [In] uint uFlags, [MarshalAs(UnmanagedType.U4)] [In] uint uLangID, [MarshalAs(UnmanagedType.LPStruct)] [In] ActivationServerInfo activationServerInfo, IntPtr context, IntPtr parentWindowHandle);

		// Token: 0x06003539 RID: 13625
		[DllImport("msdrm.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		internal static extern int DRMCreateLicenseStorageSession([In] SafeRightsManagementEnvironmentHandle envHandle, [In] SafeRightsManagementHandle hDefLib, [In] SafeRightsManagementSessionHandle hClientSession, [MarshalAs(UnmanagedType.U4)] [In] uint uFlags, [MarshalAs(UnmanagedType.LPWStr)] [In] string IssuanceLicense, out SafeRightsManagementSessionHandle phLicenseStorageSession);

		// Token: 0x0600353A RID: 13626
		[DllImport("msdrm.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		internal static extern int DRMAcquireLicense([In] SafeRightsManagementSessionHandle hSession, [MarshalAs(UnmanagedType.U4)] [In] uint uFlags, [MarshalAs(UnmanagedType.LPWStr)] [In] string GroupIdentityCredential, [MarshalAs(UnmanagedType.LPWStr)] [In] string RequestedRights, [MarshalAs(UnmanagedType.LPWStr)] [In] string CustomData, [MarshalAs(UnmanagedType.LPWStr)] [In] string url, IntPtr context);

		// Token: 0x0600353B RID: 13627
		[DllImport("msdrm.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		internal static extern int DRMEnumerateLicense([In] SafeRightsManagementSessionHandle hSession, [MarshalAs(UnmanagedType.U4)] [In] EnumerateLicenseFlags uFlags, [MarshalAs(UnmanagedType.U4)] [In] uint uIndex, [MarshalAs(UnmanagedType.Bool)] [In] [Out] ref bool pfSharedFlag, [MarshalAs(UnmanagedType.U4)] [In] [Out] ref uint puCertDataLen, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder wszCertificateData);

		// Token: 0x0600353C RID: 13628
		[DllImport("msdrm.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		internal static extern int DRMGetServiceLocation([In] SafeRightsManagementSessionHandle clientSessionHandle, [MarshalAs(UnmanagedType.U4)] [In] ServiceType serviceType, [MarshalAs(UnmanagedType.U4)] [In] ServiceLocation serviceLocation, [MarshalAs(UnmanagedType.LPWStr)] [In] string issuanceLicense, [MarshalAs(UnmanagedType.U4)] [In] [Out] ref uint serviceUrlLength, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder serviceUrl);

		// Token: 0x0600353D RID: 13629
		[DllImport("msdrm.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		internal static extern int DRMDeconstructCertificateChain([MarshalAs(UnmanagedType.LPWStr)] [In] string chain, [MarshalAs(UnmanagedType.U4)] [In] uint index, [MarshalAs(UnmanagedType.U4)] [In] [Out] ref uint certificateLength, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder certificate);

		// Token: 0x0600353E RID: 13630
		[DllImport("msdrm.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		internal static extern int DRMGetCertificateChainCount([MarshalAs(UnmanagedType.LPWStr)] [In] string chain, [MarshalAs(UnmanagedType.U4)] out uint certCount);

		// Token: 0x0600353F RID: 13631
		[DllImport("msdrm.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		internal static extern int DRMParseUnboundLicense([MarshalAs(UnmanagedType.LPWStr)] [In] string certificate, out SafeRightsManagementQueryHandle queryRootHandle);

		// Token: 0x06003540 RID: 13632
		[DllImport("msdrm.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		internal static extern int DRMGetUnboundLicenseObjectCount([In] SafeRightsManagementQueryHandle queryRootHandle, [MarshalAs(UnmanagedType.LPWStr)] [In] string subObjectType, [MarshalAs(UnmanagedType.U4)] out uint objectCount);

		// Token: 0x06003541 RID: 13633
		[DllImport("msdrm.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		internal static extern int DRMGetUnboundLicenseAttributeCount([In] SafeRightsManagementQueryHandle queryRootHandle, [MarshalAs(UnmanagedType.LPWStr)] [In] string subAttributeType, [MarshalAs(UnmanagedType.U4)] out uint attributeCount);

		// Token: 0x06003542 RID: 13634
		[DllImport("msdrm.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		internal static extern int DRMGetBoundLicenseObject([In] SafeRightsManagementHandle queryRootHandle, [MarshalAs(UnmanagedType.LPWStr)] [In] string subObjectType, [MarshalAs(UnmanagedType.U4)] [In] uint index, out SafeRightsManagementHandle subQueryHandle);

		// Token: 0x06003543 RID: 13635
		[DllImport("msdrm.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		internal static extern int DRMGetUnboundLicenseObject([In] SafeRightsManagementQueryHandle queryRootHandle, [MarshalAs(UnmanagedType.LPWStr)] [In] string subObjectType, [MarshalAs(UnmanagedType.U4)] [In] uint index, out SafeRightsManagementQueryHandle subQueryHandle);

		// Token: 0x06003544 RID: 13636
		[DllImport("msdrm.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		internal static extern int DRMGetUnboundLicenseAttribute([In] SafeRightsManagementQueryHandle queryRootHandle, [MarshalAs(UnmanagedType.LPWStr)] [In] string attributeType, [MarshalAs(UnmanagedType.U4)] [In] uint index, [MarshalAs(UnmanagedType.U4)] out uint encodingType, [MarshalAs(UnmanagedType.U4)] [In] [Out] ref uint bufferSize, byte[] buffer);

		// Token: 0x06003545 RID: 13637
		[DllImport("msdrm.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		internal static extern int DRMGetBoundLicenseAttribute([In] SafeRightsManagementHandle queryRootHandle, [MarshalAs(UnmanagedType.LPWStr)] [In] string attributeType, [MarshalAs(UnmanagedType.U4)] [In] uint index, [MarshalAs(UnmanagedType.U4)] out uint encodingType, [MarshalAs(UnmanagedType.U4)] [In] [Out] ref uint bufferSize, byte[] buffer);

		// Token: 0x06003546 RID: 13638
		[DllImport("msdrm.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		internal static extern int DRMCreateIssuanceLicense([MarshalAs(UnmanagedType.LPStruct)] [In] SystemTime timeFrom, [MarshalAs(UnmanagedType.LPStruct)] [In] SystemTime timeUntil, [MarshalAs(UnmanagedType.LPWStr)] [In] string referralInfoName, [MarshalAs(UnmanagedType.LPWStr)] [In] string referralInfoUrl, [In] SafeRightsManagementPubHandle ownerUserHandle, [MarshalAs(UnmanagedType.LPWStr)] [In] string issuanceLicense, [In] SafeRightsManagementHandle boundLicenseHandle, out SafeRightsManagementPubHandle issuanceLicenseHandle);

		// Token: 0x06003547 RID: 13639
		[DllImport("msdrm.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		internal static extern int DRMCreateUser([MarshalAs(UnmanagedType.LPWStr)] [In] string userName, [MarshalAs(UnmanagedType.LPWStr)] [In] string userId, [MarshalAs(UnmanagedType.LPWStr)] [In] string userIdType, out SafeRightsManagementPubHandle userHandle);

		// Token: 0x06003548 RID: 13640
		[DllImport("msdrm.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		internal static extern int DRMGetUsers([In] SafeRightsManagementPubHandle issuanceLicenseHandle, [MarshalAs(UnmanagedType.U4)] [In] uint index, out SafeRightsManagementPubHandle userHandle);

		// Token: 0x06003549 RID: 13641
		[DllImport("msdrm.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		internal static extern int DRMGetUserRights([In] SafeRightsManagementPubHandle issuanceLicenseHandle, [In] SafeRightsManagementPubHandle userHandle, [MarshalAs(UnmanagedType.U4)] [In] uint index, out SafeRightsManagementPubHandle rightHandle);

		// Token: 0x0600354A RID: 13642
		[DllImport("msdrm.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		internal static extern int DRMGetUserInfo([In] SafeRightsManagementPubHandle userHandle, [MarshalAs(UnmanagedType.U4)] [In] [Out] ref uint userNameLength, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder userName, [MarshalAs(UnmanagedType.U4)] [In] [Out] ref uint userIdLength, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder userId, [MarshalAs(UnmanagedType.U4)] [In] [Out] ref uint userIdTypeLength, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder userIdType);

		// Token: 0x0600354B RID: 13643
		[DllImport("msdrm.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		internal static extern int DRMGetRightInfo([In] SafeRightsManagementPubHandle rightHandle, [MarshalAs(UnmanagedType.U4)] [In] [Out] ref uint rightNameLength, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder rightName, [MarshalAs(UnmanagedType.LPStruct)] SystemTime timeFrom, [MarshalAs(UnmanagedType.LPStruct)] SystemTime timeUntil);

		// Token: 0x0600354C RID: 13644
		[DllImport("msdrm.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		internal static extern int DRMCreateRight([MarshalAs(UnmanagedType.LPWStr)] [In] string rightName, [MarshalAs(UnmanagedType.LPStruct)] [In] SystemTime timeFrom, [MarshalAs(UnmanagedType.LPStruct)] [In] SystemTime timeUntil, [MarshalAs(UnmanagedType.U4)] [In] uint countExtendedInfo, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] [In] string[] extendedInfoNames, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] [In] string[] extendedInfoValues, out SafeRightsManagementPubHandle rightHandle);

		// Token: 0x0600354D RID: 13645
		[DllImport("msdrm.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		internal static extern int DRMGetIssuanceLicenseTemplate([In] SafeRightsManagementPubHandle issuanceLicenseHandle, [MarshalAs(UnmanagedType.U4)] [In] [Out] ref uint issuanceLicenseTemplateLength, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder issuanceLicenseTemplate);

		// Token: 0x0600354E RID: 13646
		[DllImport("msdrm.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		internal static extern int DRMClosePubHandle([MarshalAs(UnmanagedType.U4)] [In] uint pubHandle);

		// Token: 0x0600354F RID: 13647
		[DllImport("msdrm.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		internal static extern int DRMAddRightWithUser([In] SafeRightsManagementPubHandle issuanceLicenseHandle, [In] SafeRightsManagementPubHandle rightHandle, [In] SafeRightsManagementPubHandle userHandle);

		// Token: 0x06003550 RID: 13648
		[DllImport("msdrm.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		internal static extern int DRMSetMetaData([In] SafeRightsManagementPubHandle issuanceLicenseHandle, [MarshalAs(UnmanagedType.LPWStr)] [In] string contentId, [MarshalAs(UnmanagedType.LPWStr)] [In] string contentIdType, [MarshalAs(UnmanagedType.LPWStr)] [In] string SkuId, [MarshalAs(UnmanagedType.LPWStr)] [In] string SkuIdType, [MarshalAs(UnmanagedType.LPWStr)] [In] string contentType, [MarshalAs(UnmanagedType.LPWStr)] [In] string contentName);

		// Token: 0x06003551 RID: 13649
		[DllImport("msdrm.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		internal static extern int DRMGetIssuanceLicenseInfo([In] SafeRightsManagementPubHandle issuanceLicenseHandle, [MarshalAs(UnmanagedType.LPStruct)] SystemTime timeFrom, [MarshalAs(UnmanagedType.LPStruct)] SystemTime timeUntil, [MarshalAs(UnmanagedType.U4)] [In] uint flags, [MarshalAs(UnmanagedType.U4)] [In] [Out] ref uint distributionPointNameLength, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder DistributionPointName, [MarshalAs(UnmanagedType.U4)] [In] [Out] ref uint distributionPointUriLength, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder DistributionPointUri, out SafeRightsManagementPubHandle ownerHandle, [MarshalAs(UnmanagedType.Bool)] out bool officialFlag);

		// Token: 0x06003552 RID: 13650
		[DllImport("msdrm.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		internal static extern int DRMGetSecurityProvider([MarshalAs(UnmanagedType.U4)] [In] uint flags, [MarshalAs(UnmanagedType.U4)] [In] [Out] ref uint typeLength, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder type, [MarshalAs(UnmanagedType.U4)] [In] [Out] ref uint pathLength, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder path);

		// Token: 0x06003553 RID: 13651
		[DllImport("msdrm.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		internal static extern int DRMDeleteLicense([In] SafeRightsManagementSessionHandle hSession, [MarshalAs(UnmanagedType.LPWStr)] [In] string wszLicenseId);

		// Token: 0x06003554 RID: 13652
		[DllImport("msdrm.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		internal static extern int DRMSetNameAndDescription([In] SafeRightsManagementPubHandle issuanceLicenseHandle, [MarshalAs(UnmanagedType.Bool)] [In] bool flagDelete, [MarshalAs(UnmanagedType.U4)] [In] uint localeId, [MarshalAs(UnmanagedType.LPWStr)] [In] string name, [MarshalAs(UnmanagedType.LPWStr)] [In] string description);

		// Token: 0x06003555 RID: 13653
		[DllImport("msdrm.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		internal static extern int DRMGetNameAndDescription([In] SafeRightsManagementPubHandle issuanceLicenseHandle, [MarshalAs(UnmanagedType.U4)] [In] uint uIndex, [MarshalAs(UnmanagedType.U4)] out uint localeId, [MarshalAs(UnmanagedType.U4)] [In] [Out] ref uint nameLength, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder name, [MarshalAs(UnmanagedType.U4)] [In] [Out] ref uint descriptionLength, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder description);

		// Token: 0x06003556 RID: 13654
		[DllImport("msdrm.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		internal static extern int DRMGetSignedIssuanceLicense([In] SafeRightsManagementEnvironmentHandle environmentHandle, [In] SafeRightsManagementPubHandle issuanceLicenseHandle, [MarshalAs(UnmanagedType.U4)] [In] SignIssuanceLicenseFlags flags, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] [In] byte[] symmetricKey, [MarshalAs(UnmanagedType.U4)] [In] uint symmetricKeyByteCount, [MarshalAs(UnmanagedType.LPWStr)] [In] string symmetricKeyType, [MarshalAs(UnmanagedType.LPWStr)] [In] string clientLicensorCertificate, [MarshalAs(UnmanagedType.FunctionPtr)] [In] CallbackDelegate pfnCallback, [MarshalAs(UnmanagedType.LPWStr)] [In] string url, [In] IntPtr context);

		// Token: 0x06003557 RID: 13655
		[DllImport("msdrm.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		internal static extern int DRMGetSignedIssuanceLicenseEx([In] SafeRightsManagementEnvironmentHandle environmentHandle, [In] SafeRightsManagementPubHandle issuanceLicenseHandle, [MarshalAs(UnmanagedType.U4)] [In] SignIssuanceLicenseFlags flags, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] [In] byte[] symmetricKey, [MarshalAs(UnmanagedType.U4)] [In] uint symmetricKeyByteCount, [MarshalAs(UnmanagedType.LPWStr)] [In] string symmetricKeyType, [In] IntPtr pReserved, [In] SafeRightsManagementHandle enablingPrincipalHandle, [In] SafeRightsManagementHandle boundLicenseCLCHandle, [MarshalAs(UnmanagedType.FunctionPtr)] [In] CallbackDelegate pfnCallback, [In] IntPtr context);

		// Token: 0x06003558 RID: 13656
		[DllImport("msdrm.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		internal static extern int DRMGetOwnerLicense([In] SafeRightsManagementPubHandle issuanceLicenseHandle, [MarshalAs(UnmanagedType.U4)] [In] [Out] ref uint ownerLicenseLength, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder ownerLicense);

		// Token: 0x06003559 RID: 13657
		[DllImport("msdrm.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		internal static extern int DRMCreateBoundLicense([In] SafeRightsManagementEnvironmentHandle environmentHandle, [MarshalAs(UnmanagedType.LPStruct)] [In] BoundLicenseParams boundLicenseParams, [MarshalAs(UnmanagedType.LPWStr)] [In] string licenseChain, out SafeRightsManagementHandle boundLicenseHandle, [MarshalAs(UnmanagedType.U4)] out uint errorLogHandle);

		// Token: 0x0600355A RID: 13658
		[DllImport("msdrm.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		internal static extern int DRMCreateEnablingBitsDecryptor([In] SafeRightsManagementHandle boundLicenseHandle, [MarshalAs(UnmanagedType.LPWStr)] [In] string right, [MarshalAs(UnmanagedType.U4)] [In] uint auxLibrary, [MarshalAs(UnmanagedType.LPWStr)] [In] string auxPlugin, out SafeRightsManagementHandle decryptorHandle);

		// Token: 0x0600355B RID: 13659
		[DllImport("msdrm.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		internal static extern int DRMCreateEnablingBitsEncryptor([In] SafeRightsManagementHandle boundLicenseHandle, [MarshalAs(UnmanagedType.LPWStr)] [In] string right, [MarshalAs(UnmanagedType.U4)] [In] uint auxLibrary, [MarshalAs(UnmanagedType.LPWStr)] [In] string auxPlugin, out SafeRightsManagementHandle encryptorHandle);

		// Token: 0x0600355C RID: 13660
		[DllImport("msdrm.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		internal static extern int DRMDecrypt([In] SafeRightsManagementHandle cryptoProvHandle, [MarshalAs(UnmanagedType.U4)] [In] uint position, [MarshalAs(UnmanagedType.U4)] [In] uint inputByteCount, byte[] inputBuffer, [MarshalAs(UnmanagedType.U4)] [In] [Out] ref uint outputByteCount, byte[] outputBuffer);

		// Token: 0x0600355D RID: 13661
		[DllImport("msdrm.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		internal static extern int DRMEncrypt([In] SafeRightsManagementHandle cryptoProvHandle, [MarshalAs(UnmanagedType.U4)] [In] uint position, [MarshalAs(UnmanagedType.U4)] [In] uint inputByteCount, byte[] inputBuffer, [MarshalAs(UnmanagedType.U4)] [In] [Out] ref uint outputByteCount, byte[] outputBuffer);

		// Token: 0x0600355E RID: 13662
		[DllImport("msdrm.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		internal static extern int DRMGetInfo([In] SafeRightsManagementHandle handle, [MarshalAs(UnmanagedType.LPWStr)] [In] string attributeType, [MarshalAs(UnmanagedType.U4)] out uint encodingType, [MarshalAs(UnmanagedType.U4)] [In] [Out] ref uint outputByteCount, byte[] outputBuffer);

		// Token: 0x0600355F RID: 13663
		[DllImport("msdrm.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		internal static extern int DRMGetApplicationSpecificData([In] SafeRightsManagementPubHandle issuanceLicenseHandle, [MarshalAs(UnmanagedType.U4)] [In] uint index, [MarshalAs(UnmanagedType.U4)] [In] [Out] ref uint nameLength, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder name, [MarshalAs(UnmanagedType.U4)] [In] [Out] ref uint valueLength, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder value);

		// Token: 0x06003560 RID: 13664
		[DllImport("msdrm.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		internal static extern int DRMSetApplicationSpecificData([In] SafeRightsManagementPubHandle issuanceLicenseHandle, [MarshalAs(UnmanagedType.Bool)] [In] bool flagDelete, [MarshalAs(UnmanagedType.LPWStr)] [In] string name, [MarshalAs(UnmanagedType.LPWStr)] [In] string value);

		// Token: 0x06003561 RID: 13665
		[DllImport("msdrm.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		internal static extern int DRMGetIntervalTime([In] SafeRightsManagementPubHandle issuanceLicenseHandle, [MarshalAs(UnmanagedType.U4)] [In] [Out] ref uint days);

		// Token: 0x06003562 RID: 13666
		[DllImport("msdrm.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		internal static extern int DRMSetIntervalTime([In] SafeRightsManagementPubHandle issuanceLicenseHandle, [MarshalAs(UnmanagedType.U4)] [In] uint days);

		// Token: 0x06003563 RID: 13667
		[DllImport("msdrm.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		internal static extern int DRMGetRevocationPoint([In] SafeRightsManagementPubHandle issuanceLicenseHandle, [MarshalAs(UnmanagedType.U4)] [In] [Out] ref uint idLength, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder id, [MarshalAs(UnmanagedType.U4)] [In] [Out] ref uint idTypeLength, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder idType, [MarshalAs(UnmanagedType.U4)] [In] [Out] ref uint urlLength, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder url, [MarshalAs(UnmanagedType.LPStruct)] SystemTime frequency, [MarshalAs(UnmanagedType.U4)] [In] [Out] ref uint nameLength, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder name, [MarshalAs(UnmanagedType.U4)] [In] [Out] ref uint publicKeyLength, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder publicKey);

		// Token: 0x06003564 RID: 13668
		[DllImport("msdrm.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		internal static extern int DRMSetRevocationPoint([In] SafeRightsManagementPubHandle issuanceLicenseHandle, [MarshalAs(UnmanagedType.Bool)] [In] bool flagDelete, [MarshalAs(UnmanagedType.LPWStr)] [In] string id, [MarshalAs(UnmanagedType.LPWStr)] [In] string idType, [MarshalAs(UnmanagedType.LPWStr)] [In] string url, [MarshalAs(UnmanagedType.LPStruct)] [In] SystemTime frequency, [MarshalAs(UnmanagedType.LPWStr)] [In] string name, [MarshalAs(UnmanagedType.LPWStr)] [In] string publicKey);

		// Token: 0x06003565 RID: 13669
		[DllImport("msdrm.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		internal static extern int DRMSetGlobalOptions([MarshalAs(UnmanagedType.U4)] [In] GlobalOptions globalOptions, IntPtr dataPtr, [MarshalAs(UnmanagedType.U4)] [In] int length);

		// Token: 0x06003566 RID: 13670
		[DllImport("msdrm.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		internal static extern int DRMGetClientVersion([MarshalAs(UnmanagedType.LPStruct)] [In] [Out] DRMClientVersionInfo drmClientVersionInfo);

		// Token: 0x06003567 RID: 13671
		[DllImport("msdrm.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		internal static extern int DRMRegisterRevocationList([In] SafeRightsManagementEnvironmentHandle environmentHandle, [MarshalAs(UnmanagedType.LPWStr)] [In] string revocationList);

		// Token: 0x06003568 RID: 13672
		[DllImport("msdrm.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		internal static extern int DRMAcquireIssuanceLicenseTemplate([In] SafeRightsManagementSessionHandle hSession, [MarshalAs(UnmanagedType.U4)] [In] TemplateDistribution uFlags, [In] IntPtr pvReserved, [MarshalAs(UnmanagedType.U4)] [In] uint cReserved, [In] IntPtr pwszReserved, [MarshalAs(UnmanagedType.LPWStr)] [In] string wszURL, [In] IntPtr context);

		// Token: 0x06003569 RID: 13673
		[DllImport("msdrm.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		internal static extern int DRMCreateEnablingPrincipal([In] SafeRightsManagementEnvironmentHandle environmentHandle, [In] SafeRightsManagementHandle libraryHandle, [MarshalAs(UnmanagedType.LPWStr)] [In] string wszObject, [MarshalAs(UnmanagedType.LPStruct)] [In] [Out] DRMId idPrincipal, [MarshalAs(UnmanagedType.LPWStr)] [In] string wszCredentials, out SafeRightsManagementHandle enablingPrincipalHandle);

		// Token: 0x0600356A RID: 13674
		[DllImport("msdrm.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		internal static extern int DRMDuplicateHandle([In] SafeRightsManagementHandle drmHandle, out SafeRightsManagementHandle duplicateHandle);

		// Token: 0x04002D9E RID: 11678
		private const string MsDrm = "msdrm.dll";
	}
}
