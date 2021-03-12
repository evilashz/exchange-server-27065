using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Xml;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Net;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Security.RightsManagement.StructuredStorage;
using Microsoft.Win32;

namespace Microsoft.Exchange.Security.RightsManagement.Protectors
{
	// Token: 0x020009B2 RID: 2482
	internal sealed class ProtectorsManager
	{
		// Token: 0x060035CC RID: 13772 RVA: 0x00087D0A File Offset: 0x00085F0A
		private ProtectorsManager()
		{
			this.LoadProtectors();
		}

		// Token: 0x17000DCB RID: 3531
		// (get) Token: 0x060035CD RID: 13773 RVA: 0x00087D28 File Offset: 0x00085F28
		public static ProtectorsManager Instance
		{
			get
			{
				if (ProtectorsManager.instance == null)
				{
					lock (ProtectorsManager.SyncLock)
					{
						if (ProtectorsManager.instance == null)
						{
							ProtectorsManager.Tracer.TraceDebug(0L, "Instantiating ProtectorsManager.");
							ProtectorsManager.instance = new ProtectorsManager();
						}
					}
				}
				return ProtectorsManager.instance;
			}
		}

		// Token: 0x060035CE RID: 13774 RVA: 0x00087D90 File Offset: 0x00085F90
		public bool Protect(string filename, Stream inputStream, IStream outputStream, SafeRightsManagementHandle encryptorHandle, SafeRightsManagementHandle decryptorHandle, string issuanceLicense)
		{
			if (string.IsNullOrEmpty(filename))
			{
				throw new ArgumentNullException("filename");
			}
			if (inputStream == null)
			{
				throw new ArgumentNullException("inputStream");
			}
			if (outputStream == null)
			{
				throw new ArgumentNullException("outputStream");
			}
			if (encryptorHandle == null)
			{
				throw new ArgumentNullException("encryptorHandle");
			}
			if (decryptorHandle == null)
			{
				throw new ArgumentNullException("decryptorHandle");
			}
			if (string.IsNullOrEmpty(issuanceLicense))
			{
				throw new ArgumentNullException("issuanceLicense");
			}
			if (encryptorHandle.IsInvalid)
			{
				throw new ArgumentException("handle is invalid", "encryptorHandle");
			}
			if (decryptorHandle.IsInvalid)
			{
				throw new ArgumentException("handle is invalid", "decryptorHandle");
			}
			I_IrmProtector protector = this.GetProtector(filename);
			if (protector == null)
			{
				ProtectorsManager.Tracer.TraceDebug<string>(0L, "Cannot protect file {0} because no protector is registered that can handle it.", filename);
				return false;
			}
			ILockBytesOverStream lockBytesOverStream = new ILockBytesOverStream(inputStream);
			MsoIpiResult arg = this.IsProtected(protector, lockBytesOverStream, filename);
			ProtectorsManager.Tracer.TraceDebug<string, MsoIpiResult>(0L, "Protection status of file {0} is {1}.", filename, arg);
			switch (arg)
			{
			case MsoIpiResult.Protected:
				return false;
			case MsoIpiResult.NotMyFile:
				return false;
			}
			if (!this.ShouldProtectOfcFile(filename, inputStream, lockBytesOverStream))
			{
				return false;
			}
			using (IrmPolicyInfoRms irmPolicyInfoRms = new IrmPolicyInfoRms(encryptorHandle, decryptorHandle, issuanceLicense))
			{
				MsoIpiStatus msoIpiStatus;
				int num = protector.HrProtectRMS(lockBytesOverStream, new ILockBytesOverIStream(outputStream), irmPolicyInfoRms, out msoIpiStatus);
				ProtectorsManager.ThrowAttachmentProtectionExceptionForHR(num, msoIpiStatus, DrmStrings.FailedToProtectFile(filename, num));
				ProtectorsManager.Tracer.TraceDebug<string, int>(0L, "Result of HrProtectRMS for file {0} is 0x{1:X8}", filename, num);
				switch (msoIpiStatus)
				{
				case MsoIpiStatus.Unknown:
					return false;
				case MsoIpiStatus.ProtectSuccess:
					return true;
				case MsoIpiStatus.AlreadyProtected:
					return false;
				case MsoIpiStatus.NotMyFile:
					return false;
				case MsoIpiStatus.FileCorrupt:
					return false;
				}
				ProtectorsManager.Tracer.TraceError<MsoIpiStatus>(0L, "Error protecting file.  HrProtectRMS status is: {0}.", msoIpiStatus);
				throw new AttachmentProtectionException(DrmStrings.ErrorProtectingFile(filename, msoIpiStatus));
			}
			bool result;
			return result;
		}

		// Token: 0x060035CF RID: 13775 RVA: 0x00087F7C File Offset: 0x0008617C
		public bool Unprotect(string filename, Stream inputStream, Stream outputStream, string issuanceLicense, SafeRightsManagementHandle decryptorHandle)
		{
			if (string.IsNullOrEmpty(filename))
			{
				throw new ArgumentNullException("filename");
			}
			if (inputStream == null)
			{
				throw new ArgumentNullException("inputStream");
			}
			if (outputStream == null)
			{
				throw new ArgumentNullException("outputStream");
			}
			if (string.IsNullOrEmpty(issuanceLicense))
			{
				throw new ArgumentNullException("issuanceLicense");
			}
			if (decryptorHandle == null)
			{
				throw new ArgumentNullException("decryptorHandle");
			}
			if (decryptorHandle.IsInvalid)
			{
				throw new ArgumentException("handle is invalid", "decryptorHandle");
			}
			return this.UnprotectInternal(filename, inputStream, outputStream, issuanceLicense, decryptorHandle, null);
		}

		// Token: 0x060035D0 RID: 13776 RVA: 0x00088004 File Offset: 0x00086204
		public bool Unprotect(BindLicenseForDecrypt bindDelegate, string filename, Stream inputStream, Stream outputStream)
		{
			if (bindDelegate == null)
			{
				throw new ArgumentNullException("bindDelegate");
			}
			if (string.IsNullOrEmpty(filename))
			{
				throw new ArgumentNullException("filename");
			}
			if (inputStream == null)
			{
				throw new ArgumentNullException("inputStream");
			}
			if (outputStream == null)
			{
				throw new ArgumentNullException("outputStream");
			}
			return this.UnprotectInternal(filename, inputStream, outputStream, null, null, bindDelegate);
		}

		// Token: 0x060035D1 RID: 13777 RVA: 0x0008805C File Offset: 0x0008625C
		private bool UnprotectInternal(string filename, Stream inputStream, Stream outputStream, string issuanceLicense, SafeRightsManagementHandle decryptorHandle, BindLicenseForDecrypt bindDelegate)
		{
			I_IrmProtector protector = this.GetProtector(filename);
			if (protector == null)
			{
				ProtectorsManager.Tracer.TraceDebug<string>(0L, "Cannot unprotect file {0} because no protector is registered that can handle it.", filename);
				return false;
			}
			ILockBytesOverStream lockBytesOverStream = new ILockBytesOverStream(inputStream);
			MsoIpiResult arg = this.IsProtected(protector, lockBytesOverStream, filename);
			ProtectorsManager.Tracer.TraceDebug<string, MsoIpiResult>(0L, "Protection status of file {0} is {1}.", filename, arg);
			switch (arg)
			{
			case MsoIpiResult.Unprotected:
				return false;
			case MsoIpiResult.NotMyFile:
				return false;
			}
			using (IrmPolicyInfoRms irmPolicyInfoRms = (bindDelegate != null) ? new IrmPolicyInfoRms(bindDelegate) : new IrmPolicyInfoRms(decryptorHandle, issuanceLicense))
			{
				MsoIpiStatus msoIpiStatus;
				int num = protector.HrUnprotectRMS(lockBytesOverStream, new ILockBytesOverStream(outputStream), irmPolicyInfoRms, out msoIpiStatus);
				ProtectorsManager.ThrowAttachmentProtectionExceptionForHR(num, msoIpiStatus, DrmStrings.FailedToUnprotectFile(filename, num));
				ProtectorsManager.Tracer.TraceDebug<string, int>(0L, "Result of HrUnprotectRMS for file {0} is 0x{1:X8}", filename, num);
				switch (msoIpiStatus)
				{
				case MsoIpiStatus.Unknown:
					return false;
				case MsoIpiStatus.UnprotectSuccess:
					return true;
				case MsoIpiStatus.AlreadyUnprotected:
					return false;
				case MsoIpiStatus.NotMyFile:
					return false;
				case MsoIpiStatus.FileCorrupt:
					return false;
				}
				ProtectorsManager.Tracer.TraceError<MsoIpiStatus>(0L, "Error unprotecting file.  HrUnprotectRMS status is: {0}.", msoIpiStatus);
				throw new AttachmentProtectionException(DrmStrings.ErrorUnprotectingFile(filename, msoIpiStatus));
			}
			bool result;
			return result;
		}

		// Token: 0x060035D2 RID: 13778 RVA: 0x000881B4 File Offset: 0x000863B4
		internal MsoIpiResult IsProtected(string filename, Stream stream)
		{
			if (string.IsNullOrEmpty(filename))
			{
				throw new ArgumentNullException("filename");
			}
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			I_IrmProtector protector = this.GetProtector(filename);
			if (protector == null)
			{
				return MsoIpiResult.Unknown;
			}
			return this.IsProtected(protector, new ILockBytesOverStream(stream), filename);
		}

		// Token: 0x17000DCC RID: 3532
		// (get) Token: 0x060035D3 RID: 13779 RVA: 0x000881FD File Offset: 0x000863FD
		internal IEnumerable<string> ProtectableFileExtensions
		{
			get
			{
				return this.map.Keys;
			}
		}

		// Token: 0x060035D4 RID: 13780 RVA: 0x0008820A File Offset: 0x0008640A
		internal bool IsProtectorRegistered(string extension)
		{
			return !string.IsNullOrEmpty(extension) && this.map.ContainsKey(extension);
		}

		// Token: 0x060035D5 RID: 13781 RVA: 0x00088222 File Offset: 0x00086422
		private static I_IrmProtector InstantiateProtector(Guid clsid)
		{
			return (I_IrmProtector)Activator.CreateInstance(Type.GetTypeFromCLSID(clsid));
		}

		// Token: 0x060035D6 RID: 13782 RVA: 0x00088234 File Offset: 0x00086434
		private static ushort GetLangIdFromLocaleId(int localeID)
		{
			return (ushort)(localeID & 65535);
		}

		// Token: 0x060035D7 RID: 13783 RVA: 0x00088240 File Offset: 0x00086440
		private static void ThrowAttachmentProtectionExceptionForHR(int errorCode, MsoIpiStatus status, LocalizedString contextMessage)
		{
			if (errorCode >= 0)
			{
				return;
			}
			Exception ex;
			if (status == MsoIpiStatus.PlatformIrmFailed && Enum.IsDefined(typeof(RightsManagementFailureCode), errorCode))
			{
				ex = new RightsManagementException((RightsManagementFailureCode)errorCode, contextMessage);
			}
			else
			{
				ex = Marshal.GetExceptionForHR(errorCode);
			}
			ProtectorsManager.Tracer.TraceError<LocalizedString, int, Exception>(0L, "Throwing AttachmentProtectionException.  Context: {0}.  Error code: 0x{1:X8}.  Inner exception: {2}", contextMessage, errorCode, ex);
			throw new AttachmentProtectionException(contextMessage, ex);
		}

		// Token: 0x060035D8 RID: 13784 RVA: 0x0008829C File Offset: 0x0008649C
		private I_IrmProtector GetProtector(string filename)
		{
			I_IrmProtector result;
			if (filename.IndexOfAny(Path.GetInvalidPathChars()) == -1 && this.map.TryGetValue(Path.GetExtension(filename), out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x060035D9 RID: 13785 RVA: 0x000882E8 File Offset: 0x000864E8
		private void LoadProtectors()
		{
			LinkedList<KeyValuePair<Guid, I_IrmProtector>> linkedList = new LinkedList<KeyValuePair<Guid, I_IrmProtector>>();
			bool flag = false;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Shared Tools\\Web Server Extensions\\12.0\\IrmProtectors"))
				{
					if (registryKey == null)
					{
						ProtectorsManager.Tracer.TraceError<string>(0L, "Protectors registry key {0} not found.", "Software\\Microsoft\\Shared Tools\\Web Server Extensions\\12.0\\IrmProtectors");
						return;
					}
					foreach (string text in registryKey.GetValueNames())
					{
						Guid guid;
						if (registryKey.GetValueKind(text) == RegistryValueKind.String && DrmClientUtils.TryParseGuid(text, out guid))
						{
							ProtectorsManager.Tracer.TraceDebug<Guid>(0L, "Instantiating protector {0}", guid);
							I_IrmProtector value = ProtectorsManager.InstantiateProtector(guid);
							linkedList.AddLast(new KeyValuePair<Guid, I_IrmProtector>(guid, value));
						}
						else
						{
							ProtectorsManager.Tracer.TraceDebug<string>(0L, "Skipping malformed CLSID {0} under protectors registry key.", text);
						}
					}
				}
				List<string> list = new List<string>();
				foreach (KeyValuePair<Guid, I_IrmProtector> keyValuePair in linkedList)
				{
					Guid key = keyValuePair.Key;
					I_IrmProtector value2 = keyValuePair.Value;
					int errorCode = value2.HrSetLangId(ProtectorsManager.GetLangIdFromLocaleId(CultureInfo.CurrentUICulture.LCID));
					ProtectorsManager.ThrowAttachmentProtectionExceptionForHR(errorCode, MsoIpiStatus.Unknown, DrmStrings.FailedAtSettingProtectorLanguageId(key, errorCode));
					ProtectorsManager.Tracer.TraceDebug<Guid>(0L, "Initializing protector {0}", key);
					string text2;
					int num;
					string text3;
					bool flag2;
					errorCode = value2.HrInit(out text2, out num, out text3, out flag2);
					ProtectorsManager.ThrowAttachmentProtectionExceptionForHR(errorCode, MsoIpiStatus.Unknown, DrmStrings.FailedAtInitializingProtector(key, errorCode));
					ProtectorsManager.Tracer.TraceDebug(0L, "Protector {0} has been initialized. Product: {1}, Version: {2}, Extensions: {3}, UseRms: {4}", new object[]
					{
						key,
						text2,
						num,
						text3,
						flag2
					});
					if (flag2 && !string.IsNullOrEmpty(text3))
					{
						string[] array = text3.Split(ProtectorsManager.ExtensionSeparator, StringSplitOptions.RemoveEmptyEntries);
						foreach (string text4 in array)
						{
							string text5 = text4.Trim();
							if (!string.IsNullOrEmpty(text5))
							{
								string extensionWithLeadingDot = (text5[0] == '.') ? text5 : ("." + text5);
								this.map[extensionWithLeadingDot] = value2;
								if (key == ProtectorsManager.MetroIrmProtectorGuid)
								{
									if (ProtectorsManager.ofcFileExtensions.Any((string ex) => extensionWithLeadingDot.Equals(ex, StringComparison.OrdinalIgnoreCase)))
									{
										list.Add(extensionWithLeadingDot);
									}
								}
							}
						}
						if (key == ProtectorsManager.MetroIrmProtectorGuid)
						{
							this.effectiveOfcFileExtensions = list.ToArray();
							this.SetFileFormatValidationOverrides();
							this.map[".umrmwav"] = value2;
							this.map[".umrmwma"] = value2;
							this.map[".umrmmp3"] = value2;
						}
					}
					else
					{
						ProtectorsManager.Tracer.TraceDebug<string, Guid>(0L, "Skipping protector {0} ('{1}') because it either is autonomous or its extensions list is empty", text2, key);
					}
				}
				flag = true;
			}
			catch (SecurityException ex)
			{
				SecurityException ex5;
				ProtectorsManager.Tracer.TraceError<Exception>(0L, "Failed to enumerate protectors.  Exception: {0}", ex5);
				throw new AttachmentProtectionException(DrmStrings.FailedToEnumerateProtectors("Software\\Microsoft\\Shared Tools\\Web Server Extensions\\12.0\\IrmProtectors"), ex5);
			}
			catch (UnauthorizedAccessException ex2)
			{
				ProtectorsManager.Tracer.TraceError<Exception>(0L, "Failed to enumerate protectors.  Exception: {0}", ex2);
				throw new AttachmentProtectionException(DrmStrings.FailedToEnumerateProtectors("Software\\Microsoft\\Shared Tools\\Web Server Extensions\\12.0\\IrmProtectors"), ex2);
			}
			catch (COMException ex3)
			{
				ProtectorsManager.Tracer.TraceError<Exception>(0L, "Failed to instantiate protectors.  Exception: {0}", ex3);
				throw new AttachmentProtectionException(DrmStrings.FailedToInstantiateProtectors, ex3);
			}
			catch (FileNotFoundException ex4)
			{
				ProtectorsManager.Tracer.TraceError<Exception>(0L, "Failed to instantiate protectors.  Exception: {0}", ex4);
				throw new AttachmentProtectionException(DrmStrings.FailedToInstantiateProtectors, ex4);
			}
			finally
			{
				if (!flag)
				{
					foreach (KeyValuePair<Guid, I_IrmProtector> keyValuePair2 in linkedList)
					{
						I_IrmProtector value3 = keyValuePair2.Value;
						if (value3 != null)
						{
							Marshal.ReleaseComObject(value3);
						}
					}
				}
			}
		}

		// Token: 0x060035DA RID: 13786 RVA: 0x00088788 File Offset: 0x00086988
		[HandleProcessCorruptedStateExceptions]
		private MsoIpiResult IsProtected(I_IrmProtector protector, ILockBytes stream, string filename)
		{
			MsoIpiResult result = MsoIpiResult.Unknown;
			int num;
			try
			{
				num = protector.HrIsProtected(stream, out result);
			}
			catch (AccessViolationException ex)
			{
				ExWatson.SendGenericWatsonReport("E12", ExWatson.RealApplicationVersion.ToString(), ExWatson.RealAppName, "15.00.1497.010", Assembly.GetExecutingAssembly().GetName().Name, ex.GetType().Name, ex.StackTrace, ex.StackTrace.GetHashCode().ToString("x"), ex.TargetSite.Name, ex.ToString());
				ProtectorsManager.Tracer.TraceError<Exception>(0L, "Protector caused AccessViolationException: {0}", ex);
				num = -1;
			}
			if (num == -2147287038)
			{
				return MsoIpiResult.Unknown;
			}
			if (filename.EndsWith(".xml", StringComparison.OrdinalIgnoreCase) && Enum.IsDefined(typeof(NativeMethods.XmlError), (uint)num))
			{
				return MsoIpiResult.Unknown;
			}
			ProtectorsManager.ThrowAttachmentProtectionExceptionForHR(num, MsoIpiStatus.Unknown, DrmStrings.FailedAtGettingStatusOfProtection(filename, num));
			return result;
		}

		// Token: 0x060035DB RID: 13787 RVA: 0x0008888C File Offset: 0x00086A8C
		private bool ShouldProtectOfcFile(string fileName, Stream inputStream, ILockBytes inputLockBytes)
		{
			if (this.skipOfcFileFormatValidation || this.effectiveOfcFileExtensions == null || this.effectiveOfcFileExtensions.Length == 0)
			{
				return true;
			}
			if (!this.effectiveOfcFileExtensions.Any((string ex) => fileName.EndsWith(ex, StringComparison.OrdinalIgnoreCase)))
			{
				return true;
			}
			bool result;
			try
			{
				using (Package.Open(inputStream, FileMode.Open, FileAccess.Read))
				{
				}
				result = true;
			}
			catch (Exception ex)
			{
				Exception ex2;
				if (ex2 is FileFormatException || ex2 is XmlException)
				{
					ProtectorsManager.Tracer.TraceDebug<string, Exception>(0L, "The file {0} is not valid OFC file. Exception: {1}", fileName, ex2);
					if (SafeNativeMethods.StgIsStorageILockBytes(inputLockBytes) == 0)
					{
						result = true;
					}
					else
					{
						ProtectorsManager.Tracer.TraceDebug<string>(0L, "The file {0} is not a valid Office file. It will not be protected.", fileName);
						result = false;
					}
				}
				else
				{
					if (!(ex2 is IOException))
					{
						throw;
					}
					ProtectorsManager.Tracer.TraceDebug<string, Exception>(0L, "There is an error when checking file {0} and should not protect. Exception: {1}", fileName, ex2);
					result = false;
				}
			}
			return result;
		}

		// Token: 0x060035DC RID: 13788 RVA: 0x000889A8 File Offset: 0x00086BA8
		private void SetFileFormatValidationOverrides()
		{
			this.skipOfcFileFormatValidation = false;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Shared Tools\\Web Server Extensions\\12.0\\IrmProtectors\\OfcIrmProtector"))
			{
				if (registryKey == null)
				{
					ProtectorsManager.Tracer.TraceError<string>(0L, "Metro Protector (OfcIrmProtector) registry key {0} not found. Use default value for OFC file format validation.", "Software\\Microsoft\\Shared Tools\\Web Server Extensions\\12.0\\IrmProtectors\\OfcIrmProtector");
				}
				else
				{
					try
					{
						this.skipOfcFileFormatValidation = ((int)registryKey.GetValue("SkipFormatValidation", 0) == 1);
					}
					catch (Exception arg)
					{
						ProtectorsManager.Tracer.TraceError<string, Exception>(0L, "Failed to get value of OfcIrmProtector {0}. Use default value for OFC file format validation. Exception: {1}.", "SkipFormatValidation", arg);
					}
					if (!this.skipOfcFileFormatValidation)
					{
						try
						{
							string text = (string)registryKey.GetValue("FileFormatValidationList", "");
							if (!string.IsNullOrEmpty(text))
							{
								string[] array = text.Split(ProtectorsManager.ExtensionSeparator, StringSplitOptions.RemoveEmptyEntries);
								List<string> list = new List<string>();
								foreach (string text2 in array)
								{
									string text3 = text2.Trim();
									if (!string.IsNullOrEmpty(text3))
									{
										string extensionWithLeadingDot = (text3[0] == '.') ? text3 : ("." + text3);
										if (this.effectiveOfcFileExtensions.Any((string ex) => extensionWithLeadingDot.Equals(ex, StringComparison.OrdinalIgnoreCase)))
										{
											list.Add(extensionWithLeadingDot);
										}
									}
								}
								this.effectiveOfcFileExtensions = list.ToArray();
							}
						}
						catch (Exception arg2)
						{
							ProtectorsManager.Tracer.TraceError<string, Exception>(0L, "Failed to get value of OfcIrmProtector {0}. Use default value for OFC file format validation. Exception: {1}.", "SkipFormatValidation", arg2);
						}
					}
				}
			}
		}

		// Token: 0x04002E41 RID: 11841
		private const string IrmProtectorsKeyPath = "Software\\Microsoft\\Shared Tools\\Web Server Extensions\\12.0\\IrmProtectors";

		// Token: 0x04002E42 RID: 11842
		private const string MetroIrmProtectorKeyPath = "Software\\Microsoft\\Shared Tools\\Web Server Extensions\\12.0\\IrmProtectors\\OfcIrmProtector";

		// Token: 0x04002E43 RID: 11843
		private const string SkipOfcFileFormatValidationKeyName = "SkipFormatValidation";

		// Token: 0x04002E44 RID: 11844
		private const string OfcFileFormatValidationListKeyName = "FileFormatValidationList";

		// Token: 0x04002E45 RID: 11845
		private static readonly object SyncLock = new object();

		// Token: 0x04002E46 RID: 11846
		private static readonly Guid MetroIrmProtectorGuid = new Guid("{0D231213-1E3B-4940-82C6-2BC8B93EE8E3}");

		// Token: 0x04002E47 RID: 11847
		private static readonly char[] ExtensionSeparator = new char[]
		{
			','
		};

		// Token: 0x04002E48 RID: 11848
		private static readonly string[] ofcFileExtensions = new string[]
		{
			".xps",
			".docx",
			".docm",
			".dotx",
			".dotm",
			".xlsx",
			".xlsm",
			".xlsb",
			".xltx",
			".xltm",
			".xlam",
			".pptx",
			".pptm",
			".potx",
			".potm",
			".thmx",
			".ppsx",
			".ppsm"
		};

		// Token: 0x04002E49 RID: 11849
		private static readonly Trace Tracer = ExTraceGlobals.RightsManagementTracer;

		// Token: 0x04002E4A RID: 11850
		private static ProtectorsManager instance;

		// Token: 0x04002E4B RID: 11851
		private bool skipOfcFileFormatValidation;

		// Token: 0x04002E4C RID: 11852
		private string[] effectiveOfcFileExtensions;

		// Token: 0x04002E4D RID: 11853
		private Dictionary<string, I_IrmProtector> map = new Dictionary<string, I_IrmProtector>(StringComparer.OrdinalIgnoreCase);
	}
}
