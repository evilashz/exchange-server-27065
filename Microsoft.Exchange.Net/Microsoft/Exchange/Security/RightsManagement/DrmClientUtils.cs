using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Text;
using System.Threading;
using System.Xml;
using Microsoft.com.IPC.WSService;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Net.WSTrust;
using Microsoft.Exchange.Security.RightsManagement.StructuredStorage;
using Microsoft.Exchange.Win32;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Exchange.Security.RightsManagement
{
	// Token: 0x02000975 RID: 2421
	internal static class DrmClientUtils
	{
		// Token: 0x0600346D RID: 13421 RVA: 0x000810F8 File Offset: 0x0007F2F8
		public static bool TryParseGuid(string s, out Guid guid)
		{
			guid = Guid.Empty;
			if (string.IsNullOrEmpty(s))
			{
				return false;
			}
			bool result;
			try
			{
				guid = new Guid(s);
				result = true;
			}
			catch (FormatException)
			{
				result = false;
			}
			catch (OverflowException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0600346E RID: 13422 RVA: 0x00081154 File Offset: 0x0007F354
		public static string GetLicenseOnSession(SafeRightsManagementSessionHandle sessionHandle, EnumerateLicenseFlags enumerateLicenseFlags, int index)
		{
			bool flag = false;
			uint num = 0U;
			int num2 = SafeNativeMethods.DRMEnumerateLicense(sessionHandle, enumerateLicenseFlags, (uint)index, ref flag, ref num, null);
			if (num2 == -2147168461)
			{
				return null;
			}
			Errors.ThrowOnErrorCode(num2);
			if (num > 2147483647U)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder(checked((int)num));
			num2 = SafeNativeMethods.DRMEnumerateLicense(sessionHandle, enumerateLicenseFlags, (uint)index, ref flag, ref num, stringBuilder);
			Errors.ThrowOnErrorCode(num2);
			return stringBuilder.ToString();
		}

		// Token: 0x0600346F RID: 13423 RVA: 0x000811B0 File Offset: 0x0007F3B0
		public static Uri GetServiceLocation(SafeRightsManagementSessionHandle sessionHandle, ServiceType serviceType, ServiceLocation serviceLocation, string issuanceLicense)
		{
			uint num = 0U;
			int num2;
			try
			{
				num2 = SafeNativeMethods.DRMGetServiceLocation(sessionHandle, serviceType, serviceLocation, issuanceLicense, ref num, null);
			}
			catch (ThreadAbortException)
			{
				num2 = -2147168447;
			}
			if (num2 == -2147168439)
			{
				return null;
			}
			Errors.ThrowOnErrorCode(num2);
			checked
			{
				StringBuilder stringBuilder = new StringBuilder((int)num);
				try
				{
					num2 = SafeNativeMethods.DRMGetServiceLocation(sessionHandle, serviceType, serviceLocation, issuanceLicense, ref num, stringBuilder);
					if (num2 == -2147024774)
					{
						stringBuilder = new StringBuilder((int)num);
						num2 = SafeNativeMethods.DRMGetServiceLocation(sessionHandle, serviceType, serviceLocation, issuanceLicense, ref num, stringBuilder);
					}
				}
				catch (ThreadAbortException)
				{
					num2 = -2147168447;
				}
				Errors.ThrowOnErrorCode(num2);
				return new Uri(stringBuilder.ToString());
			}
		}

		// Token: 0x06003470 RID: 13424 RVA: 0x00081254 File Offset: 0x0007F454
		public static bool GetNameAndDescription(SafeRightsManagementPubHandle issuanceLicenseHandle, uint index, out uint localeId, out string name, out string description)
		{
			uint num = 0U;
			uint num2 = 0U;
			int num3 = SafeNativeMethods.DRMGetNameAndDescription(issuanceLicenseHandle, index, out localeId, ref num, null, ref num2, null);
			if (num3 == -2147168461)
			{
				localeId = 0U;
				name = null;
				description = null;
				return false;
			}
			Errors.ThrowOnErrorCode(num3);
			checked
			{
				StringBuilder stringBuilder = new StringBuilder((int)num);
				StringBuilder stringBuilder2 = new StringBuilder((int)num2);
				num3 = SafeNativeMethods.DRMGetNameAndDescription(issuanceLicenseHandle, index, out localeId, ref num, stringBuilder, ref num2, stringBuilder2);
				Errors.ThrowOnErrorCode(num3);
				name = stringBuilder.ToString();
				description = stringBuilder2.ToString();
				return true;
			}
		}

		// Token: 0x06003471 RID: 13425 RVA: 0x000812C8 File Offset: 0x0007F4C8
		public static void SetServerLockboxOptions()
		{
			int num = SafeNativeMethods.DRMSetGlobalOptions(GlobalOptions.UseWinHttp, IntPtr.Zero, 0);
			if (num != -2147168396)
			{
				Errors.ThrowOnErrorCode(num);
			}
			num = SafeNativeMethods.DRMSetGlobalOptions(GlobalOptions.UseServerSecurityProcessor, IntPtr.Zero, 0);
			if (num != -2147168396)
			{
				Errors.ThrowOnErrorCode(num);
			}
		}

		// Token: 0x06003472 RID: 13426 RVA: 0x0008130C File Offset: 0x0007F50C
		public static string Activate(SafeRightsManagementSessionHandle sessionHandle, CallbackHandler callbackHandler, ActivationFlags activationFlags, Uri url)
		{
			ActivationServerInfo activationServerInfo = null;
			if (url != null)
			{
				activationServerInfo = new ActivationServerInfo();
				activationServerInfo.PubKey = null;
				activationServerInfo.Url = url.AbsoluteUri;
				activationServerInfo.Version = 1U;
			}
			int num = SafeNativeMethods.DRMActivate(sessionHandle, (uint)activationFlags, 0U, activationServerInfo, IntPtr.Zero, IntPtr.Zero);
			if (num == -2147168456)
			{
				for (int i = 0; i <= 3; i++)
				{
					num = SafeNativeMethods.DRMIsActivated(sessionHandle, 1U, null);
					if (num != -2147168451)
					{
						Errors.ThrowOnErrorCode(num);
						break;
					}
					if (i == 3)
					{
						throw new RightsManagementException(RightsManagementFailureCode.UnknownDRMFailure, DrmStrings.RmExceptionActivationGenericMessage);
					}
					if (callbackHandler.WaitForCompletion(DrmClientUtils.ActivationWaitTimeout))
					{
						break;
					}
				}
			}
			else
			{
				Errors.ThrowOnErrorCode(num);
				callbackHandler.WaitForCompletion();
			}
			return callbackHandler.CallbackData;
		}

		// Token: 0x06003473 RID: 13427 RVA: 0x000813C0 File Offset: 0x0007F5C0
		public static string GetSecurityProviderPath()
		{
			uint num = 0U;
			uint num2 = 0U;
			int hr = SafeNativeMethods.DRMGetSecurityProvider(0U, ref num, null, ref num2, null);
			Errors.ThrowOnErrorCode(hr);
			checked
			{
				StringBuilder type = new StringBuilder((int)num);
				StringBuilder stringBuilder = new StringBuilder((int)num2);
				hr = SafeNativeMethods.DRMGetSecurityProvider(0U, ref num, type, ref num2, stringBuilder);
				Errors.ThrowOnErrorCode(hr);
				return stringBuilder.ToString();
			}
		}

		// Token: 0x06003474 RID: 13428 RVA: 0x00081410 File Offset: 0x0007F610
		public static void VerifyDRMClientVersion(DRMClientVersionInfo versionInfo)
		{
			if (versionInfo == null)
			{
				throw new ArgumentNullException("versionInfo");
			}
			int hr = SafeNativeMethods.DRMGetClientVersion(versionInfo);
			Errors.ThrowOnErrorCode(hr);
			if (versionInfo.V1 < 5U || (versionInfo.V1 == 5U && versionInfo.V2 < 2U) || (versionInfo.V1 == 5U && versionInfo.V2 == 2U && versionInfo.V3 < 3790U) || (versionInfo.V1 == 5U && versionInfo.V2 == 2U && versionInfo.V3 == 3790U && versionInfo.V4 < 222U))
			{
				Errors.ThrowOnErrorCode(-2147168506);
			}
		}

		// Token: 0x06003475 RID: 13429 RVA: 0x000814A8 File Offset: 0x0007F6A8
		public static void ActivateMachine()
		{
			string text = null;
			using (WindowsIdentity current = WindowsIdentity.GetCurrent(TokenAccessLevels.Query))
			{
				text = current.User.ToString();
			}
			using (CallbackHandler callbackHandler = new CallbackHandler())
			{
				SafeRightsManagementSessionHandle safeRightsManagementSessionHandle = null;
				try
				{
					int num = SafeNativeMethods.DRMCreateClientSession(callbackHandler.CallbackDelegate, 1U, "WindowsAuthProvider", string.IsNullOrEmpty(text) ? null : text, out safeRightsManagementSessionHandle);
					Errors.ThrowOnErrorCode(num);
					num = SafeNativeMethods.DRMIsActivated(safeRightsManagementSessionHandle, 1U, null);
					if (num == -2147168451)
					{
						DrmClientUtils.Activate(safeRightsManagementSessionHandle, callbackHandler, ActivationFlags.Machine | ActivationFlags.Silent, null);
					}
					else
					{
						Errors.ThrowOnErrorCode(num);
					}
				}
				finally
				{
					if (safeRightsManagementSessionHandle != null)
					{
						safeRightsManagementSessionHandle.Close();
					}
				}
			}
		}

		// Token: 0x06003476 RID: 13430 RVA: 0x00081570 File Offset: 0x0007F770
		public static List<XmlNode[]> ConvertMachineCertsToXmlNodeArrays(List<string> machineCertificates)
		{
			if (machineCertificates == null)
			{
				throw new ArgumentNullException("machineCertificates");
			}
			List<XmlNode[]> list = new List<XmlNode[]>(machineCertificates.Count);
			foreach (string certificateChain in machineCertificates)
			{
				list.Add(DrmClientUtils.ConvertCertificateChainToXmlNodeArray(certificateChain));
			}
			return list;
		}

		// Token: 0x06003477 RID: 13431 RVA: 0x000815E0 File Offset: 0x0007F7E0
		public static List<string> LoadAllMachineCertificates()
		{
			List<string> list = new List<string>(2);
			for (int i = 0; i < 2147483647; i++)
			{
				string text = DrmClientUtils.LoadMachineCertificate(i);
				if (text == null)
				{
					break;
				}
				list.Add(text);
			}
			return list;
		}

		// Token: 0x06003478 RID: 13432 RVA: 0x00081618 File Offset: 0x0007F818
		public static string LoadMachineCertificate(int machineCertIndex)
		{
			if (machineCertIndex < 0)
			{
				throw new ArgumentException("machineCertIndex");
			}
			string text = null;
			using (WindowsIdentity current = WindowsIdentity.GetCurrent(TokenAccessLevels.Query))
			{
				text = current.User.ToString();
			}
			string licenseOnSession;
			using (CallbackHandler callbackHandler = new CallbackHandler())
			{
				SafeRightsManagementSessionHandle safeRightsManagementSessionHandle = null;
				try
				{
					int hr = SafeNativeMethods.DRMCreateClientSession(callbackHandler.CallbackDelegate, 1U, "WindowsAuthProvider", string.IsNullOrEmpty(text) ? null : text, out safeRightsManagementSessionHandle);
					Errors.ThrowOnErrorCode(hr);
					licenseOnSession = DrmClientUtils.GetLicenseOnSession(safeRightsManagementSessionHandle, EnumerateLicenseFlags.Machine, machineCertIndex);
				}
				finally
				{
					if (safeRightsManagementSessionHandle != null)
					{
						safeRightsManagementSessionHandle.Close();
					}
				}
			}
			return licenseOnSession;
		}

		// Token: 0x06003479 RID: 13433 RVA: 0x000816D0 File Offset: 0x0007F8D0
		public static bool DoesMsDrmSupportMode2Crypto()
		{
			bool result = false;
			string text = null;
			using (WindowsIdentity current = WindowsIdentity.GetCurrent(TokenAccessLevels.Query))
			{
				text = current.User.ToString();
			}
			using (CallbackHandler callbackHandler = new CallbackHandler())
			{
				SafeRightsManagementSessionHandle safeRightsManagementSessionHandle = null;
				try
				{
					int hr = SafeNativeMethods.DRMCreateClientSession(callbackHandler.CallbackDelegate, 1U, "WindowsAuthProvider", string.IsNullOrEmpty(text) ? null : text, out safeRightsManagementSessionHandle);
					Errors.ThrowOnErrorCode(hr);
					string licenseOnSession = DrmClientUtils.GetLicenseOnSession(safeRightsManagementSessionHandle, EnumerateLicenseFlags.Machine, 1);
					if (!string.IsNullOrEmpty(licenseOnSession))
					{
						result = true;
					}
				}
				finally
				{
					if (safeRightsManagementSessionHandle != null)
					{
						safeRightsManagementSessionHandle.Close();
					}
				}
			}
			return result;
		}

		// Token: 0x0600347A RID: 13434 RVA: 0x0008178C File Offset: 0x0007F98C
		public static string GetOwnerLicense(SafeRightsManagementPubHandle issuanceLicenseHandle)
		{
			uint num = 0U;
			int hr = SafeNativeMethods.DRMGetOwnerLicense(issuanceLicenseHandle, ref num, null);
			Errors.ThrowOnErrorCode(hr);
			StringBuilder stringBuilder = new StringBuilder(checked((int)num));
			hr = SafeNativeMethods.DRMGetOwnerLicense(issuanceLicenseHandle, ref num, stringBuilder);
			Errors.ThrowOnErrorCode(hr);
			return stringBuilder.ToString();
		}

		// Token: 0x0600347B RID: 13435 RVA: 0x000817C9 File Offset: 0x0007F9C9
		public static void ParsePublishLicense(string publishLicense, out Uri intranetUri, out Uri extranetUri)
		{
			DrmClientUtils.ParseLicense(publishLicense, out intranetUri, out extranetUri, DrmClientUtils.LicenseType.PublishLicense);
		}

		// Token: 0x0600347C RID: 13436 RVA: 0x000817D4 File Offset: 0x0007F9D4
		public static void ParseGic(string gic, out Uri intranetUri, out Uri extranetUri)
		{
			DrmClientUtils.ParseLicense(gic, out intranetUri, out extranetUri, DrmClientUtils.LicenseType.Gic);
		}

		// Token: 0x0600347D RID: 13437 RVA: 0x000817DF File Offset: 0x0007F9DF
		public static void ParseClc(string clc, out Uri intranetUri, out Uri extranetUri)
		{
			DrmClientUtils.ParseLicense(clc, out intranetUri, out extranetUri, DrmClientUtils.LicenseType.Clc);
		}

		// Token: 0x0600347E RID: 13438 RVA: 0x000817EA File Offset: 0x0007F9EA
		public static void ParseTemplate(string template, out Uri intranetUri, out Uri extranetUri, out Guid templateGuid)
		{
			DrmClientUtils.ParseLicense(template, out intranetUri, out extranetUri, DrmClientUtils.LicenseType.Template);
			templateGuid = DrmClientUtils.GetTemplateGuidFromLicense(template);
		}

		// Token: 0x0600347F RID: 13439 RVA: 0x00081804 File Offset: 0x0007FA04
		public static Guid GetTemplateGuidFromLicense(string license)
		{
			if (string.IsNullOrEmpty(license))
			{
				throw new ArgumentNullException("license");
			}
			SafeRightsManagementQueryHandle safeRightsManagementQueryHandle = null;
			Guid result;
			try
			{
				int hr = SafeNativeMethods.DRMParseUnboundLicense(license, out safeRightsManagementQueryHandle);
				Errors.ThrowOnErrorCode(hr);
				string unboundLicenseStringAttribute = DrmClientUtils.GetUnboundLicenseStringAttribute(safeRightsManagementQueryHandle, "id-value", 0U);
				if (string.IsNullOrEmpty(unboundLicenseStringAttribute))
				{
					result = Guid.Empty;
				}
				else
				{
					Guid guid;
					if (!DrmClientUtils.TryParseGuid(unboundLicenseStringAttribute, out guid))
					{
						throw new RightsManagementException(RightsManagementFailureCode.InvalidLicense, DrmStrings.FailedToGetTemplateIdFromLicense);
					}
					result = guid;
				}
			}
			finally
			{
				if (safeRightsManagementQueryHandle != null)
				{
					safeRightsManagementQueryHandle.Close();
				}
			}
			return result;
		}

		// Token: 0x06003480 RID: 13440 RVA: 0x00081890 File Offset: 0x0007FA90
		private static void ParseLicense(string license, out Uri intranetUri, out Uri extranetUri, DrmClientUtils.LicenseType licenseType)
		{
			if (string.IsNullOrEmpty(license))
			{
				throw new ArgumentNullException("license");
			}
			intranetUri = null;
			extranetUri = null;
			string strB;
			string strB2;
			if (licenseType == DrmClientUtils.LicenseType.Gic)
			{
				strB = "Activation";
				strB2 = "Extranet-Activation";
			}
			else if (licenseType == DrmClientUtils.LicenseType.Template)
			{
				strB = "Publishing-URL";
				strB2 = "Extranet-Publishing-URL";
			}
			else
			{
				strB = "License-Acquisition-URL";
				strB2 = "Extranet-License-Acquisition-URL";
			}
			SafeRightsManagementQueryHandle safeRightsManagementQueryHandle = null;
			try
			{
				int num = SafeNativeMethods.DRMParseUnboundLicense(license, out safeRightsManagementQueryHandle);
				Errors.ThrowOnErrorCode(num);
				uint num2 = 0U;
				for (;;)
				{
					SafeRightsManagementQueryHandle safeRightsManagementQueryHandle2 = null;
					try
					{
						num = SafeNativeMethods.DRMGetUnboundLicenseObject(safeRightsManagementQueryHandle, "distribution-point", num2, out safeRightsManagementQueryHandle2);
						if (num != -2147168490)
						{
							Errors.ThrowOnErrorCode(num);
							string unboundLicenseStringAttribute = DrmClientUtils.GetUnboundLicenseStringAttribute(safeRightsManagementQueryHandle2, "object-type", 0U);
							if (intranetUri == null && string.CompareOrdinal(unboundLicenseStringAttribute, strB) == 0)
							{
								string unboundLicenseStringAttribute2 = DrmClientUtils.GetUnboundLicenseStringAttribute(safeRightsManagementQueryHandle2, "address-value", 0U);
								Uri.TryCreate(unboundLicenseStringAttribute2, UriKind.Absolute, out intranetUri);
							}
							else if (extranetUri == null && string.CompareOrdinal(unboundLicenseStringAttribute, strB2) == 0)
							{
								string unboundLicenseStringAttribute3 = DrmClientUtils.GetUnboundLicenseStringAttribute(safeRightsManagementQueryHandle2, "address-value", 0U);
								Uri.TryCreate(unboundLicenseStringAttribute3, UriKind.Absolute, out extranetUri);
							}
							if (!(intranetUri != null) || !(extranetUri != null))
							{
								num2 += 1U;
								continue;
							}
						}
					}
					finally
					{
						if (safeRightsManagementQueryHandle2 != null)
						{
							safeRightsManagementQueryHandle2.Close();
						}
					}
					break;
				}
			}
			finally
			{
				if (safeRightsManagementQueryHandle != null)
				{
					safeRightsManagementQueryHandle.Close();
				}
			}
		}

		// Token: 0x06003481 RID: 13441 RVA: 0x000819E0 File Offset: 0x0007FBE0
		public static void BindUseLicense(string useLicense, string publishLicense, string rightsRequested, bool createEncryptor, SafeRightsManagementHandle racEnablingPrincipal, SafeRightsManagementEnvironmentHandle envHandle, out SafeRightsManagementHandle encryptorHandle, out SafeRightsManagementHandle decryptorHandle)
		{
			if (string.IsNullOrEmpty(publishLicense))
			{
				throw new ArgumentNullException("publishLicense");
			}
			string contentId;
			string contentIdType;
			DrmClientUtils.GetContentIdFromLicense(publishLicense, out contentId, out contentIdType);
			DrmClientUtils.BindUseLicense(useLicense, contentId, contentIdType, rightsRequested, createEncryptor, racEnablingPrincipal, envHandle, out encryptorHandle, out decryptorHandle);
		}

		// Token: 0x06003482 RID: 13442 RVA: 0x00081A1C File Offset: 0x0007FC1C
		public static void BindUseLicense(string useLicense, string contentId, string contentIdType, string rightsRequested, bool createEncryptor, SafeRightsManagementHandle racEnablingPrincipal, SafeRightsManagementEnvironmentHandle envHandle, out SafeRightsManagementHandle encryptorHandle, out SafeRightsManagementHandle decryptorHandle)
		{
			if (string.IsNullOrEmpty(useLicense))
			{
				throw new ArgumentNullException("useLicense");
			}
			if (string.IsNullOrEmpty(contentId))
			{
				throw new ArgumentNullException("contentId");
			}
			if (string.IsNullOrEmpty(contentIdType))
			{
				throw new ArgumentNullException("contentIdType");
			}
			if (string.IsNullOrEmpty(rightsRequested))
			{
				throw new ArgumentNullException("rightsRequested");
			}
			if (racEnablingPrincipal == null)
			{
				throw new ArgumentNullException("racEnablingPrincipal");
			}
			if (envHandle == null)
			{
				throw new ArgumentNullException("envHandle");
			}
			ContentRight rightFromName = RightUtils.GetRightFromName(rightsRequested);
			if (createEncryptor && !rightFromName.IsUsageRightGranted(ContentRight.Edit))
			{
				throw new RightsManagementException(RightsManagementFailureCode.BindInsufficientRightsToCreateEncryptor, DrmStrings.UnableToCreateEncryptorHandleWithoutEditRight);
			}
			bool flag = false;
			SafeRightsManagementHandle safeRightsManagementHandle = null;
			encryptorHandle = null;
			decryptorHandle = null;
			try
			{
				BoundLicenseParams boundLicenseParams = DrmClientUtils.GetBoundLicenseParams(racEnablingPrincipal, rightsRequested, null, contentId, contentIdType);
				uint num;
				int hr = SafeNativeMethods.DRMCreateBoundLicense(envHandle, boundLicenseParams, useLicense, out safeRightsManagementHandle, out num);
				Errors.ThrowOnErrorCode(hr);
				if (createEncryptor)
				{
					hr = SafeNativeMethods.DRMCreateEnablingBitsEncryptor(safeRightsManagementHandle, boundLicenseParams.WszRightsRequested, 0U, null, out encryptorHandle);
					Errors.ThrowOnErrorCode(hr);
				}
				hr = SafeNativeMethods.DRMCreateEnablingBitsDecryptor(safeRightsManagementHandle, boundLicenseParams.WszRightsRequested, 0U, null, out decryptorHandle);
				Errors.ThrowOnErrorCode(hr);
				if ((createEncryptor && DrmClientUtils.GetSymmetricKeyTypeFromHandle(encryptorHandle) != "AES") || DrmClientUtils.GetSymmetricKeyTypeFromHandle(decryptorHandle) != "AES")
				{
					throw new RightsManagementException(RightsManagementFailureCode.InvalidAlgorithmType, DrmStrings.AlgorithmNotSupported);
				}
				flag = true;
			}
			finally
			{
				if (safeRightsManagementHandle != null)
				{
					safeRightsManagementHandle.Close();
					safeRightsManagementHandle = null;
				}
				if (!flag)
				{
					if (encryptorHandle != null)
					{
						encryptorHandle.Close();
						encryptorHandle = null;
					}
					if (decryptorHandle != null)
					{
						decryptorHandle.Close();
						decryptorHandle = null;
					}
				}
			}
		}

		// Token: 0x06003483 RID: 13443 RVA: 0x00081B9C File Offset: 0x0007FD9C
		public static string GetSymmetricKeyTypeFromHandle(SafeRightsManagementHandle handle)
		{
			if (handle == null)
			{
				throw new ArgumentNullException("handle");
			}
			if (handle.IsInvalid)
			{
				throw new ArgumentException("handle");
			}
			uint num = 0U;
			uint num2;
			int hr = SafeNativeMethods.DRMGetInfo(handle, "symmetric-key-type", out num2, ref num, null);
			Errors.ThrowOnErrorCode(hr);
			if (num <= 0U)
			{
				return string.Empty;
			}
			byte[] array = new byte[checked((int)num)];
			hr = SafeNativeMethods.DRMGetInfo(handle, "symmetric-key-type", out num2, ref num, array);
			Errors.ThrowOnErrorCode(hr);
			string @string = Encoding.Unicode.GetString(array);
			char[] trimChars = new char[1];
			return @string.TrimEnd(trimChars);
		}

		// Token: 0x06003484 RID: 13444 RVA: 0x00081C28 File Offset: 0x0007FE28
		public static SafeRightsManagementHandle CreateEnablingPrincipal(string rightsAccountCertificate, SafeRightsManagementEnvironmentHandle envHandle, SafeRightsManagementHandle libHandle)
		{
			if (envHandle == null)
			{
				throw new ArgumentNullException("envHandle");
			}
			if (libHandle == null)
			{
				throw new ArgumentNullException("libHandle");
			}
			if (string.IsNullOrEmpty(rightsAccountCertificate))
			{
				throw new ArgumentNullException("rightsAccountCertificate");
			}
			SafeRightsManagementHandle result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				SafeRightsManagementHandle safeRightsManagementHandle;
				int hr = SafeNativeMethods.DRMCreateEnablingPrincipal(envHandle, libHandle, "UDStdPlg Enabling Principal", DrmClientUtils.NullDRMID, rightsAccountCertificate, out safeRightsManagementHandle);
				disposeGuard.Add<SafeRightsManagementHandle>(safeRightsManagementHandle);
				Errors.ThrowOnErrorCode(hr);
				disposeGuard.Success();
				result = safeRightsManagementHandle;
			}
			return result;
		}

		// Token: 0x06003485 RID: 13445 RVA: 0x00081CBC File Offset: 0x0007FEBC
		public static SafeRightsManagementHandle CreateClcBoundLicense(SafeRightsManagementHandle enablingPrincipalHandle, SafeRightsManagementEnvironmentHandle envHandle, string clcCertChain)
		{
			if (envHandle == null)
			{
				throw new ArgumentNullException("envHandle");
			}
			if (enablingPrincipalHandle == null)
			{
				throw new ArgumentNullException("enablingPrincipalHandle");
			}
			if (string.IsNullOrEmpty(clcCertChain))
			{
				throw new ArgumentNullException("clcCertChain");
			}
			string contentId;
			string contentIdType;
			DrmClientUtils.GetContentIdFromLicense(DrmClientUtils.GetElementFromCertificateChain(clcCertChain, 0), out contentId, out contentIdType);
			BoundLicenseParams boundLicenseParams = DrmClientUtils.GetBoundLicenseParams(enablingPrincipalHandle, "ISSUE", "Main-Rights", contentId, contentIdType);
			SafeRightsManagementHandle result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				SafeRightsManagementHandle safeRightsManagementHandle;
				uint num;
				int hr = SafeNativeMethods.DRMCreateBoundLicense(envHandle, boundLicenseParams, clcCertChain, out safeRightsManagementHandle, out num);
				disposeGuard.Add<SafeRightsManagementHandle>(safeRightsManagementHandle);
				Errors.ThrowOnErrorCode(hr);
				disposeGuard.Success();
				result = safeRightsManagementHandle;
			}
			return result;
		}

		// Token: 0x06003486 RID: 13446 RVA: 0x00081D70 File Offset: 0x0007FF70
		public static string GetDefaultManifestFilePath()
		{
			int num;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\uDRM"))
			{
				if (registryKey == null || registryKey.GetValueKind("Hierarchy") != RegistryValueKind.DWord)
				{
					num = 0;
				}
				else
				{
					num = (int)registryKey.GetValue("Hierarchy");
					if (num < 0 || num > 2)
					{
						num = 0;
					}
				}
			}
			string directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			if (num == 0)
			{
				return Path.Combine(directoryName, "rmmanifest_production.xml");
			}
			if (num != 2)
			{
				return null;
			}
			string text = (string)Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Exchange_Test\\v15\\Setup", "MsiInstallPath", null);
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			return Path.Combine(text, "TransportRoles\\rmmanifest_preproduction.xml");
		}

		// Token: 0x06003487 RID: 13447 RVA: 0x00081E2C File Offset: 0x0008002C
		public static void GetContentIdFromLicense(string publishLicense, out string contentId, out string contentIdType)
		{
			if (string.IsNullOrEmpty(publishLicense))
			{
				throw new ArgumentNullException("publishLicense");
			}
			SafeRightsManagementQueryHandle safeRightsManagementQueryHandle = null;
			SafeRightsManagementQueryHandle safeRightsManagementQueryHandle2 = null;
			try
			{
				int hr = SafeNativeMethods.DRMParseUnboundLicense(publishLicense, out safeRightsManagementQueryHandle);
				Errors.ThrowOnErrorCode(hr, DrmStrings.FailedToParseUnboundLicense(publishLicense));
				hr = SafeNativeMethods.DRMGetUnboundLicenseObject(safeRightsManagementQueryHandle, "work", 0U, out safeRightsManagementQueryHandle2);
				Errors.ThrowOnErrorCode(hr, DrmStrings.FailedToGetUnboundLicenseObject(publishLicense));
				contentIdType = DrmClientUtils.GetUnboundLicenseStringAttribute(safeRightsManagementQueryHandle2, "id-type", 0U);
				contentId = DrmClientUtils.GetUnboundLicenseStringAttribute(safeRightsManagementQueryHandle2, "id-value", 0U);
			}
			finally
			{
				if (safeRightsManagementQueryHandle2 != null)
				{
					safeRightsManagementQueryHandle2.Close();
				}
				if (safeRightsManagementQueryHandle != null)
				{
					safeRightsManagementQueryHandle.Close();
				}
			}
		}

		// Token: 0x06003488 RID: 13448 RVA: 0x00081EC4 File Offset: 0x000800C4
		public static string GetElementFromCertificateChain(string certificateChain, int index)
		{
			uint num = 0U;
			int hr = SafeNativeMethods.DRMDeconstructCertificateChain(certificateChain, (uint)index, ref num, null);
			Errors.ThrowOnErrorCode(hr);
			StringBuilder stringBuilder = new StringBuilder(checked((int)num));
			hr = SafeNativeMethods.DRMDeconstructCertificateChain(certificateChain, (uint)index, ref num, stringBuilder);
			Errors.ThrowOnErrorCode(hr);
			return stringBuilder.ToString();
		}

		// Token: 0x06003489 RID: 13449 RVA: 0x00081F04 File Offset: 0x00080104
		public static ContentRight GetUsageRightsFromLicense(string drmLicense)
		{
			SafeRightsManagementQueryHandle safeRightsManagementQueryHandle = null;
			ContentRight usageRightsFromLicense;
			try
			{
				int hr = SafeNativeMethods.DRMParseUnboundLicense(drmLicense, out safeRightsManagementQueryHandle);
				Errors.ThrowOnErrorCode(hr);
				usageRightsFromLicense = DrmClientUtils.GetUsageRightsFromLicense(safeRightsManagementQueryHandle);
			}
			finally
			{
				if (safeRightsManagementQueryHandle != null)
				{
					safeRightsManagementQueryHandle.Close();
				}
			}
			return usageRightsFromLicense;
		}

		// Token: 0x0600348A RID: 13450 RVA: 0x00081F48 File Offset: 0x00080148
		public static ContentRight GetUsageRightsFromLicense(SafeRightsManagementQueryHandle queryRootHandle)
		{
			if (queryRootHandle == null)
			{
				throw new ArgumentNullException("queryRootHandle");
			}
			ContentRight contentRight = ContentRight.None;
			SafeRightsManagementQueryHandle safeRightsManagementQueryHandle = null;
			try
			{
				int hr = SafeNativeMethods.DRMGetUnboundLicenseObject(queryRootHandle, "work", 0U, out safeRightsManagementQueryHandle);
				Errors.ThrowOnErrorCode(hr);
				SafeRightsManagementQueryHandle safeRightsManagementQueryHandle2 = null;
				try
				{
					hr = SafeNativeMethods.DRMGetUnboundLicenseObject(safeRightsManagementQueryHandle, "rights-group", 0U, out safeRightsManagementQueryHandle2);
					uint num = 0U;
					if (SafeNativeMethods.DRMGetUnboundLicenseObjectCount(safeRightsManagementQueryHandle2, "right", out num) == 0 && num > 0U)
					{
						int num2 = 0;
						while ((long)num2 < (long)((ulong)num))
						{
							SafeRightsManagementQueryHandle safeRightsManagementQueryHandle3 = null;
							try
							{
								hr = SafeNativeMethods.DRMGetUnboundLicenseObject(safeRightsManagementQueryHandle2, "right", (uint)num2, out safeRightsManagementQueryHandle3);
								Errors.ThrowOnErrorCode(hr);
								string unboundLicenseStringAttribute = DrmClientUtils.GetUnboundLicenseStringAttribute(safeRightsManagementQueryHandle3, "name", 0U);
								contentRight |= RightUtils.GetRightFromName(unboundLicenseStringAttribute);
							}
							finally
							{
								if (safeRightsManagementQueryHandle3 != null)
								{
									safeRightsManagementQueryHandle3.Close();
								}
							}
							num2++;
						}
					}
				}
				finally
				{
					if (safeRightsManagementQueryHandle2 != null)
					{
						safeRightsManagementQueryHandle2.Close();
					}
				}
			}
			finally
			{
				if (safeRightsManagementQueryHandle != null)
				{
					safeRightsManagementQueryHandle.Close();
				}
			}
			return contentRight;
		}

		// Token: 0x0600348B RID: 13451 RVA: 0x0008203C File Offset: 0x0008023C
		public static bool IsExchangeRecipientOrganizationExtractAllowed(string useLicense)
		{
			return DrmClientUtils.IsAppSpecificKeySet(useLicense, "ExchangeRecipientOrganizationExtractAllowed", "true");
		}

		// Token: 0x0600348C RID: 13452 RVA: 0x0008204E File Offset: 0x0008024E
		public static bool IsExchangeRecipientOrganizationExtractAllowed(SafeRightsManagementQueryHandle queryRootHandle)
		{
			return DrmClientUtils.IsAppSpecificKeySet(queryRootHandle, "ExchangeRecipientOrganizationExtractAllowed", "true");
		}

		// Token: 0x0600348D RID: 13453 RVA: 0x00082060 File Offset: 0x00080260
		public static bool IsCachingOfLicenseDisabled(string license)
		{
			return DrmClientUtils.IsAppSpecificKeySet(license, "NOLICCACHE", "1");
		}

		// Token: 0x0600348E RID: 13454 RVA: 0x00082072 File Offset: 0x00080272
		public static bool IsCachingOfLicenseDisabled(SafeRightsManagementQueryHandle queryRootHandle)
		{
			return DrmClientUtils.IsAppSpecificKeySet(queryRootHandle, "NOLICCACHE", "1");
		}

		// Token: 0x0600348F RID: 13455 RVA: 0x00082084 File Offset: 0x00080284
		public static bool IsAppSpecificKeySet(string license, string name, string value)
		{
			if (string.IsNullOrEmpty(license))
			{
				throw new ArgumentNullException("license");
			}
			SafeRightsManagementQueryHandle safeRightsManagementQueryHandle = null;
			bool result;
			try
			{
				int hr = SafeNativeMethods.DRMParseUnboundLicense(license, out safeRightsManagementQueryHandle);
				Errors.ThrowOnErrorCode(hr);
				result = DrmClientUtils.IsAppSpecificKeySet(safeRightsManagementQueryHandle, name, value);
			}
			finally
			{
				if (safeRightsManagementQueryHandle != null)
				{
					safeRightsManagementQueryHandle.Close();
				}
			}
			return result;
		}

		// Token: 0x06003490 RID: 13456 RVA: 0x000820DC File Offset: 0x000802DC
		public static bool IsAppSpecificKeySet(SafeRightsManagementQueryHandle queryRootHandle, string name, string value)
		{
			if (queryRootHandle == null)
			{
				throw new ArgumentNullException("queryRootHandle");
			}
			if (queryRootHandle.IsInvalid)
			{
				throw new ArgumentException("queryRootHandle is invalid", "queryRootHandle");
			}
			uint num;
			if (SafeNativeMethods.DRMGetUnboundLicenseAttributeCount(queryRootHandle, "appdata-name", out num) == 0 && num > 0U)
			{
				for (uint num2 = 0U; num2 < num; num2 += 1U)
				{
					string unboundLicenseStringAttribute = DrmClientUtils.GetUnboundLicenseStringAttribute(queryRootHandle, "appdata-name", num2);
					string unboundLicenseStringAttribute2 = DrmClientUtils.GetUnboundLicenseStringAttribute(queryRootHandle, "appdata-value", num2);
					if (string.Equals(unboundLicenseStringAttribute, name, StringComparison.OrdinalIgnoreCase) && string.Equals(unboundLicenseStringAttribute2, value, StringComparison.OrdinalIgnoreCase))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06003491 RID: 13457 RVA: 0x00082164 File Offset: 0x00080364
		public static string GetConversationOwnerFromPublishLicense(string publishLicense)
		{
			uint num = 0U;
			uint num2 = 0U;
			SafeRightsManagementPubHandle safeRightsManagementPubHandle = null;
			SafeRightsManagementPubHandle safeRightsManagementPubHandle2 = null;
			string result;
			try
			{
				int hr = SafeNativeMethods.DRMCreateIssuanceLicense(null, null, null, null, SafeRightsManagementPubHandle.InvalidHandle, publishLicense, SafeRightsManagementHandle.InvalidHandle, out safeRightsManagementPubHandle);
				Errors.ThrowOnErrorCode(hr);
				bool flag;
				hr = SafeNativeMethods.DRMGetIssuanceLicenseInfo(safeRightsManagementPubHandle, null, null, 2U, ref num, null, ref num2, null, out safeRightsManagementPubHandle2, out flag);
				Errors.ThrowOnErrorCode(hr);
				if (safeRightsManagementPubHandle2.IsInvalid)
				{
					result = string.Empty;
				}
				else
				{
					result = DrmClientUtils.GetUserFromHandle(safeRightsManagementPubHandle2);
				}
			}
			finally
			{
				if (safeRightsManagementPubHandle2 != null)
				{
					safeRightsManagementPubHandle2.Close();
				}
				if (safeRightsManagementPubHandle != null)
				{
					safeRightsManagementPubHandle.Close();
				}
			}
			return result;
		}

		// Token: 0x06003492 RID: 13458 RVA: 0x000821F4 File Offset: 0x000803F4
		public static string GetUserFromHandle(SafeRightsManagementPubHandle userHandle)
		{
			if (userHandle == null)
			{
				throw new ArgumentNullException("userHandle");
			}
			if (userHandle.IsInvalid)
			{
				throw new ArgumentException("userHandle");
			}
			uint num = 0U;
			uint num2 = 0U;
			uint num3 = 0U;
			int hr = SafeNativeMethods.DRMGetUserInfo(userHandle, ref num, null, ref num2, null, ref num3, null);
			Errors.ThrowOnErrorCode(hr);
			if (num <= 0U)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder(checked((int)num));
			hr = SafeNativeMethods.DRMGetUserInfo(userHandle, ref num, stringBuilder, ref num2, null, ref num3, null);
			Errors.ThrowOnErrorCode(hr);
			return stringBuilder.ToString();
		}

		// Token: 0x06003493 RID: 13459 RVA: 0x00082270 File Offset: 0x00080470
		public static string GetRightFromHandle(SafeRightsManagementPubHandle rightHandle)
		{
			uint num = 0U;
			string result = null;
			int hr = SafeNativeMethods.DRMGetRightInfo(rightHandle, ref num, null, null, null);
			Errors.ThrowOnErrorCode(hr);
			if (num > 0U)
			{
				StringBuilder stringBuilder = new StringBuilder((int)num);
				hr = SafeNativeMethods.DRMGetRightInfo(rightHandle, ref num, stringBuilder, null, null);
				Errors.ThrowOnErrorCode(hr);
				result = stringBuilder.ToString();
			}
			return result;
		}

		// Token: 0x06003494 RID: 13460 RVA: 0x000822B8 File Offset: 0x000804B8
		public static string GetCertFromLicenseStore(string filename)
		{
			string path = null;
			using (WindowsIdentity current = WindowsIdentity.GetCurrent(TokenAccessLevels.Query))
			{
				path = current.User.ToString();
			}
			string text = Path.Combine(DrmEmailConstants.LicenseStorePath, path);
			string text2 = Path.Combine(text, filename);
			if (!Directory.Exists(text) || !File.Exists(text2))
			{
				return null;
			}
			string @string;
			using (FileStream fileStream = DrmClientUtils.OpenLongFileNameFile(text2, FileAccess.Read, FileMode.Open))
			{
				int num = (int)fileStream.Length;
				byte[] array = new byte[num];
				int num2 = 0;
				int num3;
				while ((num3 = fileStream.Read(array, num2, num - num2)) > 0)
				{
					num2 += num3;
				}
				fileStream.SafeFileHandle.Close();
				UnicodeEncoding unicodeEncoding = new UnicodeEncoding();
				@string = unicodeEncoding.GetString(array);
			}
			return @string;
		}

		// Token: 0x06003495 RID: 13461 RVA: 0x00082398 File Offset: 0x00080598
		public static string AddCertToLicenseStore(string license, bool isRAC)
		{
			if (string.IsNullOrEmpty(license))
			{
				throw new ArgumentNullException("license");
			}
			string text = isRAC ? "GIC" : "CLC";
			string text2;
			string text3;
			DrmClientUtils.GetGroupIdentityAndLicenseIdFromLicense(DrmClientUtils.GetElementFromCertificateChain(license, 0), out text2, out text3);
			if (string.IsNullOrEmpty(text2) || string.IsNullOrEmpty(text3))
			{
				return null;
			}
			string path = null;
			using (WindowsIdentity current = WindowsIdentity.GetCurrent(TokenAccessLevels.Query))
			{
				path = current.User.ToString();
			}
			string text4 = Path.Combine(DrmEmailConstants.LicenseStorePath, path);
			string text5 = string.Format(CultureInfo.InvariantCulture, "{0}-{1}-{2}.{3}", new object[]
			{
				text,
				DrmClientUtils.MaskStringForLicenseStore(text2),
				DrmClientUtils.MaskStringForLicenseStore(text3),
				"drm"
			});
			string filePath = Path.Combine(text4, text5);
			if (!Directory.Exists(text4))
			{
				Directory.CreateDirectory(text4);
			}
			uint num;
			int hr = SafeNativeMethods.DRMGetCertificateChainCount(license, out num);
			Errors.ThrowOnErrorCode(hr);
			using (FileStream fileStream = DrmClientUtils.OpenLongFileNameFile(filePath, FileAccess.Write, FileMode.Create))
			{
				UnicodeEncoding unicodeEncoding = new UnicodeEncoding();
				StringBuilder stringBuilder = new StringBuilder();
				int num2 = 0;
				while ((long)num2 < (long)((ulong)num))
				{
					stringBuilder.Append(DrmClientUtils.GetElementFromCertificateChain(license, num2));
					num2++;
				}
				byte[] bytes = unicodeEncoding.GetBytes(stringBuilder.ToString());
				fileStream.Write(bytes, 0, bytes.Length);
				fileStream.SafeFileHandle.Close();
			}
			return text5;
		}

		// Token: 0x06003496 RID: 13462 RVA: 0x00082518 File Offset: 0x00080718
		private static FileStream OpenLongFileNameFile(string filePath, FileAccess access, FileMode fileMode)
		{
			NativeMethods.SECURITY_ATTRIBUTES security_ATTRIBUTES = new NativeMethods.SECURITY_ATTRIBUTES(SafeHGlobalHandle.InvalidHandle);
			NativeMethods.CreateFileAccess dwDesiredAccess = (NativeMethods.CreateFileAccess)2147483648U;
			NativeMethods.CreateFileShare dwShareMode = NativeMethods.CreateFileShare.None;
			if (access == FileAccess.Write)
			{
				dwDesiredAccess = (NativeMethods.CreateFileAccess.GenericWrite | NativeMethods.CreateFileAccess.FileWriteAttributes);
				dwShareMode = NativeMethods.CreateFileShare.None;
			}
			else if (access == FileAccess.Read)
			{
				dwDesiredAccess = (NativeMethods.CreateFileAccess)2147483648U;
				dwShareMode = NativeMethods.CreateFileShare.Read;
			}
			filePath = "\\\\?\\" + filePath;
			SafeFileHandle safeFileHandle = NativeMethods.CreateFile(filePath, dwDesiredAccess, dwShareMode, ref security_ATTRIBUTES, fileMode, NativeMethods.CreateFileFileAttributes.Normal, IntPtr.Zero);
			if (safeFileHandle.IsInvalid)
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				Errors.ThrowOnErrorCode(lastWin32Error);
			}
			return new FileStream(safeFileHandle, access);
		}

		// Token: 0x06003497 RID: 13463 RVA: 0x0008259C File Offset: 0x0008079C
		public static string ConvertXmlNodeArrayToCertificateChain(XmlNode[] xmlNodeArray)
		{
			string[] array = DrmClientUtils.ConvertXmlNodeArrayToStringArray(xmlNodeArray);
			string result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8))
				{
					xmlTextWriter.WriteStartElement("CERTIFICATECHAIN");
					foreach (string s in array)
					{
						xmlTextWriter.WriteStartElement("CERTIFICATE");
						xmlTextWriter.WriteBase64(Encoding.Unicode.GetBytes(s), 0, Encoding.Unicode.GetByteCount(s));
						xmlTextWriter.WriteEndElement();
					}
					xmlTextWriter.WriteEndElement();
					xmlTextWriter.Flush();
					int num = Encoding.UTF8.GetPreamble().Length;
					result = string.Format(CultureInfo.InvariantCulture, "{0}{1}", new object[]
					{
						"<?xml version=\"1.0\"?>",
						Encoding.UTF8.GetString(memoryStream.ToArray(), num, (int)memoryStream.Length - num)
					});
				}
			}
			return result;
		}

		// Token: 0x06003498 RID: 13464 RVA: 0x000826AC File Offset: 0x000808AC
		internal static XmlNode[] ConvertCertificateChainToXmlNodeArray(string certificateChain)
		{
			XmlDocument xmlDocument = new SafeXmlDocument();
			xmlDocument.LoadXml(certificateChain);
			XmlNode[] array;
			using (XmlNodeList xmlNodeList = xmlDocument.SelectNodes("/CERTIFICATECHAIN/CERTIFICATE"))
			{
				array = new XmlNode[xmlNodeList.Count];
				XmlDocument xmlDocument2 = new SafeXmlDocument();
				for (int i = 0; i < xmlNodeList.Count; i++)
				{
					string @string = Encoding.Unicode.GetString(Convert.FromBase64String(xmlNodeList[i].InnerText));
					xmlDocument2.LoadXml(@string);
					array[i] = xmlDocument2.DocumentElement;
				}
			}
			return array;
		}

		// Token: 0x06003499 RID: 13465 RVA: 0x00082748 File Offset: 0x00080948
		internal unsafe static byte[] SignDRMProps(ContentRight usageRights, ExDateTime expiryTime, SecurityIdentifier userSid, SafeRightsManagementHandle encryptor, SafeRightsManagementHandle decryptor)
		{
			byte[] bytes = BitConverter.GetBytes((int)usageRights);
			byte[] bytes2 = BitConverter.GetBytes(expiryTime.UtcTicks);
			byte[] array = new byte[userSid.BinaryLength + bytes.Length + bytes2.Length];
			userSid.GetBinaryForm(array, 0);
			Array.Copy(bytes, 0, array, userSid.BinaryLength, bytes.Length);
			Array.Copy(bytes2, 0, array, userSid.BinaryLength + bytes.Length, bytes2.Length);
			byte[] result;
			using (SHA256Cng sha256Cng = new SHA256Cng())
			{
				byte[] array2 = sha256Cng.ComputeHash(array);
				using (MemoryStream memoryStream = new MemoryStream(2 * array2.Length))
				{
					IStream stream;
					int errorCode = SafeNativeMethods.WrapStreamWithEncryptingStream(new IStreamOverStream(memoryStream), encryptor, decryptor, out stream);
					Marshal.ThrowExceptionForHR(errorCode);
					try
					{
						try
						{
							fixed (byte* ptr = array2)
							{
								int num = stream.Write(new IntPtr((void*)ptr), array2.Length);
								if (num != array2.Length)
								{
									throw new InvalidOperationException("failed to write to IStream.");
								}
							}
						}
						finally
						{
							byte* ptr = null;
						}
						stream.Commit(STGC.STGC_DEFAULT);
						memoryStream.Seek(0L, SeekOrigin.Begin);
						using (BinaryReader binaryReader = new BinaryReader(memoryStream))
						{
							result = binaryReader.ReadBytes(checked((int)memoryStream.Length));
						}
					}
					finally
					{
						if (stream != null)
						{
							Marshal.ReleaseComObject(stream);
							stream = null;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0600349A RID: 13466 RVA: 0x000828D0 File Offset: 0x00080AD0
		internal static string GetUnboundLicenseStringAttribute(SafeRightsManagementQueryHandle queryHandle, string attributeType, uint attributeIndex)
		{
			uint num = 0U;
			uint num3;
			int num2 = SafeNativeMethods.DRMGetUnboundLicenseAttribute(queryHandle, attributeType, attributeIndex, out num3, ref num, null);
			if (num2 == -2147168490)
			{
				return null;
			}
			Errors.ThrowOnErrorCode(num2);
			if (num < 2U)
			{
				return null;
			}
			byte[] array = new byte[checked((int)num)];
			num2 = SafeNativeMethods.DRMGetUnboundLicenseAttribute(queryHandle, attributeType, attributeIndex, out num3, ref num, array);
			Errors.ThrowOnErrorCode(num2);
			return Encoding.Unicode.GetString(array, 0, array.Length - 2);
		}

		// Token: 0x0600349B RID: 13467 RVA: 0x00082930 File Offset: 0x00080B30
		private static BoundLicenseParams GetBoundLicenseParams(SafeRightsManagementHandle racEnablingPrincipal, string requestedRights, string rightsGroup, string contentId, string contentIdType)
		{
			return new BoundLicenseParams
			{
				Version = 0U,
				EnablingPrincipalHandle = (uint)((int)racEnablingPrincipal.DangerousGetHandle()),
				SecureStoreHandle = 0U,
				WszRightsRequested = requestedRights,
				WszRightsGroup = rightsGroup,
				DRMIDuVersion = 0U,
				DRMIDIdType = contentIdType,
				DRMIDId = contentId,
				AuthenticatorCount = 0U,
				RghAuthenticators = IntPtr.Zero,
				WszDefaultEnablingPrincipalCredentials = null,
				DwFlags = 0U
			};
		}

		// Token: 0x0600349C RID: 13468 RVA: 0x000829A8 File Offset: 0x00080BA8
		public static void GetGroupIdentityAndLicenseIdFromLicense(string license, out string groupIdentity, out string licenseId)
		{
			groupIdentity = null;
			licenseId = null;
			if (string.IsNullOrEmpty(license))
			{
				throw new ArgumentNullException("license");
			}
			SafeRightsManagementQueryHandle safeRightsManagementQueryHandle = null;
			try
			{
				int hr = SafeNativeMethods.DRMParseUnboundLicense(license, out safeRightsManagementQueryHandle);
				Errors.ThrowOnErrorCode(hr);
				licenseId = DrmClientUtils.GetUnboundLicenseStringAttribute(safeRightsManagementQueryHandle, "id-value", 0U);
				SafeRightsManagementQueryHandle safeRightsManagementQueryHandle2 = null;
				try
				{
					hr = SafeNativeMethods.DRMGetUnboundLicenseObject(safeRightsManagementQueryHandle, "issued-principal", 0U, out safeRightsManagementQueryHandle2);
					Errors.ThrowOnErrorCode(hr);
					groupIdentity = DrmClientUtils.GetUnboundLicenseStringAttribute(safeRightsManagementQueryHandle2, "name", 0U);
				}
				finally
				{
					if (safeRightsManagementQueryHandle2 != null)
					{
						safeRightsManagementQueryHandle2.Close();
					}
				}
			}
			finally
			{
				if (safeRightsManagementQueryHandle != null)
				{
					safeRightsManagementQueryHandle.Close();
				}
			}
		}

		// Token: 0x0600349D RID: 13469 RVA: 0x00082A60 File Offset: 0x00080C60
		private static string MaskStringForLicenseStore(string input)
		{
			char[] array = input.ToCharArray();
			char[] anyOf = new char[]
			{
				'<',
				'>',
				':',
				'"',
				'/',
				'\\',
				'|',
				'?',
				'*',
				';',
				'-'
			};
			if (input.IndexOfAny(anyOf) == -1)
			{
				return input;
			}
			char[] array2 = new char[2 * array.Length + 1];
			int num = 0;
			int i = 0;
			while (i < array.Length)
			{
				char c = array[i];
				if (c <= '/')
				{
					if (c != '"')
					{
						if (c != '*')
						{
							switch (c)
							{
							case '-':
								array2[num++] = ';';
								array2[num] = 'k';
								break;
							case '.':
								goto IL_162;
							case '/':
								array2[num++] = ';';
								array2[num] = 'e';
								break;
							default:
								goto IL_162;
							}
						}
						else
						{
							array2[num++] = ';';
							array2[num] = 'i';
						}
					}
					else
					{
						array2[num++] = ';';
						array2[num] = 'd';
					}
				}
				else
				{
					switch (c)
					{
					case ':':
						array2[num++] = ';';
						array2[num] = 'c';
						break;
					case ';':
						array2[num++] = ';';
						array2[num] = 'j';
						break;
					case '<':
						array2[num++] = ';';
						array2[num] = 'a';
						break;
					case '=':
						goto IL_162;
					case '>':
						array2[num++] = ';';
						array2[num] = 'b';
						break;
					case '?':
						array2[num++] = ';';
						array2[num] = 'h';
						break;
					default:
						if (c != '\\')
						{
							if (c != '|')
							{
								goto IL_162;
							}
							array2[num++] = ';';
							array2[num] = 'g';
						}
						else
						{
							array2[num++] = ';';
							array2[num] = 'f';
						}
						break;
					}
				}
				IL_169:
				num++;
				i++;
				continue;
				IL_162:
				array2[num] = array[i];
				goto IL_169;
			}
			return new string(array2, 0, num);
		}

		// Token: 0x0600349E RID: 13470 RVA: 0x00082BF4 File Offset: 0x00080DF4
		public static string[] ConvertXmlNodeArrayToStringArray(XmlNode[] xmlNodeArray)
		{
			if (xmlNodeArray == null || xmlNodeArray.Length < 1)
			{
				throw new ArgumentNullException("xmlNodeArray");
			}
			string[] array = new string[xmlNodeArray.Length];
			for (int i = 0; i < xmlNodeArray.Length; i++)
			{
				if (xmlNodeArray[i] == null)
				{
					throw new ArgumentNullException("xmlNodeArray[" + i + "]");
				}
				array[i] = xmlNodeArray[i].OuterXml;
			}
			return array;
		}

		// Token: 0x0600349F RID: 13471 RVA: 0x00082C58 File Offset: 0x00080E58
		public static RightsManagementException RunWCFOperation(DrmClientUtils.WCFMethod wcfCall, Uri wcfUri, Uri targetUri)
		{
			RightsManagementException result;
			try
			{
				wcfCall();
				result = null;
			}
			catch (WSTrustException innerException)
			{
				result = new RightsManagementException(RightsManagementFailureCode.FailedToRequestDelegationToken, DrmStrings.FailedToRequestDelegationToken(wcfUri, targetUri), innerException);
			}
			catch (CryptographicException innerException2)
			{
				result = new RightsManagementException(RightsManagementFailureCode.FederationCertificateAccessFailure, DrmStrings.FederationCertificateAccessFailure, innerException2);
			}
			catch (FaultException<ActiveFederationFault> faultException)
			{
				if (faultException.Detail != null)
				{
					result = new RightsManagementException(RightsManagementFailureCode.ActiveFederationFault, DrmStrings.ExternalRmsServerFailure(wcfUri, faultException.Detail.ErrorCode.ToString()), faultException);
				}
				else
				{
					result = new RightsManagementException(RightsManagementFailureCode.ActiveFederationFault, DrmStrings.ExternalRmsServerFailure(wcfUri, RightsManagementFailureCode.UnknownFailure.ToString()), faultException);
				}
			}
			catch (EndpointNotFoundException innerException3)
			{
				result = new RightsManagementException(RightsManagementFailureCode.ServiceNotFound, DrmStrings.EndpointNotFound(wcfUri), innerException3);
			}
			catch (ActionNotSupportedException innerException4)
			{
				result = new RightsManagementException(RightsManagementFailureCode.ActionNotSupported, DrmStrings.ActionNotSupported(wcfUri), innerException4);
			}
			catch (MessageSecurityException innerException5)
			{
				result = new RightsManagementException(RightsManagementFailureCode.MessageSecurityError, DrmStrings.MessageSecurityError(wcfUri), innerException5);
			}
			catch (CommunicationException innerException6)
			{
				result = new RightsManagementException(RightsManagementFailureCode.CommunicationError, DrmStrings.CommunicationError(wcfUri), innerException6);
			}
			catch (TimeoutException innerException7)
			{
				result = new RightsManagementException(RightsManagementFailureCode.OperationTimeout, DrmStrings.TimeoutError(wcfUri), innerException7);
			}
			return result;
		}

		// Token: 0x04002C69 RID: 11369
		private const string DpIntranetLicenseAcquisitionUrl = "License-Acquisition-URL";

		// Token: 0x04002C6A RID: 11370
		private const string DpExtranetLicenseAcquisitionUrl = "Extranet-License-Acquisition-URL";

		// Token: 0x04002C6B RID: 11371
		private const string DpIntranetCertificationUrl = "Activation";

		// Token: 0x04002C6C RID: 11372
		private const string DpExtranetCertificationUrl = "Extranet-Activation";

		// Token: 0x04002C6D RID: 11373
		private const string DpIntranetTemplateUrl = "Publishing-URL";

		// Token: 0x04002C6E RID: 11374
		private const string DpExtranetTemplateUrl = "Extranet-Publishing-URL";

		// Token: 0x04002C6F RID: 11375
		public const string UDRMKeyPath = "SOFTWARE\\Microsoft\\uDRM";

		// Token: 0x04002C70 RID: 11376
		public const string UDRMHierarchyKeyName = "Hierarchy";

		// Token: 0x04002C71 RID: 11377
		public const int HierarchyProduction = 0;

		// Token: 0x04002C72 RID: 11378
		public const int HierarchyIsv = 1;

		// Token: 0x04002C73 RID: 11379
		public const int HierarchyPreProduction = 2;

		// Token: 0x04002C74 RID: 11380
		public const string ProductionManifestFile = "rmmanifest_production.xml";

		// Token: 0x04002C75 RID: 11381
		public const string PreProductionManifestFile = "TransportRoles\\rmmanifest_preproduction.xml";

		// Token: 0x04002C76 RID: 11382
		private const int ActivationWaitCountCap = 3;

		// Token: 0x04002C77 RID: 11383
		private static readonly DRMId NullDRMID = new DRMId();

		// Token: 0x04002C78 RID: 11384
		private static readonly TimeSpan ActivationWaitTimeout = TimeSpan.FromSeconds(30.0);

		// Token: 0x02000976 RID: 2422
		private enum LicenseType
		{
			// Token: 0x04002C7A RID: 11386
			Gic,
			// Token: 0x04002C7B RID: 11387
			Clc,
			// Token: 0x04002C7C RID: 11388
			PublishLicense,
			// Token: 0x04002C7D RID: 11389
			Template
		}

		// Token: 0x02000977 RID: 2423
		// (Invoke) Token: 0x060034A2 RID: 13474
		public delegate void WCFMethod();
	}
}
