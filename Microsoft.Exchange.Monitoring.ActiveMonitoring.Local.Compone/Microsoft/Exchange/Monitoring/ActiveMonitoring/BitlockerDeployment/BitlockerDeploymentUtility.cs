using System;
using System.Collections.Generic;
using System.IO;
using System.Management;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.Exchange.Cluster.Common;
using Microsoft.Exchange.Cluster.Common.Extensions;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.HighAvailability;
using Microsoft.Office.Datacenter.WorkerTaskFramework;
using Microsoft.Win32;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.BitlockerDeployment
{
	// Token: 0x0200002B RID: 43
	internal static class BitlockerDeploymentUtility
	{
		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000151 RID: 337 RVA: 0x0000A188 File Offset: 0x00008388
		public static IRegistryReader RegReader
		{
			get
			{
				return CachedRegistryReader.Instance;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000152 RID: 338 RVA: 0x0000A18F File Offset: 0x0000838F
		public static IRegistryReader NonCachedRegReader
		{
			get
			{
				return RegistryReader.Instance;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000153 RID: 339 RVA: 0x0000A196 File Offset: 0x00008396
		public static IRegistryWriter RegWriter
		{
			get
			{
				return RegistryWriter.Instance;
			}
		}

		// Token: 0x06000154 RID: 340 RVA: 0x0000A19D File Offset: 0x0000839D
		public static string ConstructProbeName(string mask, string categoryName)
		{
			return string.Format("{0}/{1}", mask, categoryName);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x0000A1AC File Offset: 0x000083AC
		public static ManagementObjectCollection GetEncryptableVolumes()
		{
			ManagementObjectCollection result;
			try
			{
				using (ManagementClass managementClass = new ManagementClass(new ManagementPath
				{
					NamespacePath = "\\ROOT\\CIMV2\\Security\\MicrosoftVolumeEncryption",
					ClassName = "Win32_EncryptableVolume"
				}))
				{
					managementClass.Get();
					ManagementObjectCollection instances = managementClass.GetInstances();
					result = instances;
				}
			}
			catch (Exception ex)
			{
				WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.HighAvailabilityTracer, BitlockerDeploymentUtility.traceContext, string.Format("Exception {0} in {1} GetEncryptableVolumes", ex.Message, ex.StackTrace), Environment.MachineName, null, "GetEncryptableVolumes", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\BitlockerDeployment\\Common\\BitlockerDeploymentUtility.cs", 122);
				result = null;
			}
			return result;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0000A254 File Offset: 0x00008454
		public static ManagementObjectCollection GetEncryptableDataVolumes()
		{
			ManagementObjectCollection result;
			try
			{
				SelectQuery selectQuery = new SelectQuery("SELECT * FROM Win32_EncryptableVolume");
				selectQuery.Condition = string.Format("DeviceID!='{0}'", BitlockerDeploymentUtility.GetBootVolumeDeviceID().Replace("\\", "\\\\"));
				string path = "\\ROOT\\CIMV2\\Security\\MicrosoftVolumeEncryption";
				using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(new ManagementScope(path), selectQuery))
				{
					result = managementObjectSearcher.Get();
				}
			}
			catch (Exception ex)
			{
				WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.HighAvailabilityTracer, BitlockerDeploymentUtility.traceContext, string.Format("Exception {0} in {1} GetEncryptableDataVolumes", ex.Message, ex.StackTrace), Environment.MachineName, null, "GetEncryptableDataVolumes", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\BitlockerDeployment\\Common\\BitlockerDeploymentUtility.cs", 152);
				result = null;
			}
			return result;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x0000A318 File Offset: 0x00008518
		public static ManagementObjectCollection GetTPM()
		{
			ManagementObjectCollection result;
			try
			{
				using (ManagementClass managementClass = new ManagementClass(new ManagementPath
				{
					NamespacePath = "\\ROOT\\CIMV2\\Security\\MicrosoftTpm",
					ClassName = "Win32_TPM"
				}))
				{
					managementClass.Get();
					ManagementObjectCollection instances = managementClass.GetInstances();
					result = instances;
				}
			}
			catch (Exception ex)
			{
				WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.HighAvailabilityTracer, BitlockerDeploymentUtility.traceContext, string.Format("Exception {0} in {1} GetTPM", ex.Message, ex.StackTrace), Environment.MachineName, null, "GetTPM", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\BitlockerDeployment\\Common\\BitlockerDeploymentUtility.cs", 183);
				result = null;
			}
			return result;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x0000A3C4 File Offset: 0x000085C4
		public static string GetUnencryptedEncryptableVolumes()
		{
			string result;
			try
			{
				StringBuilder stringBuilder = new StringBuilder();
				ManagementObjectCollection encryptableVolumes = BitlockerDeploymentUtility.GetEncryptableVolumes();
				using (encryptableVolumes)
				{
					if (encryptableVolumes != null)
					{
						foreach (ManagementBaseObject managementBaseObject in encryptableVolumes)
						{
							ManagementObject managementObject = (ManagementObject)managementBaseObject;
							using (managementObject)
							{
								ManagementBaseObject managementBaseObject2 = managementObject.InvokeMethod("GetLockStatus", managementObject, null);
								using (managementBaseObject2)
								{
									if (Convert.ToInt32(managementBaseObject2.GetPropertyValue("LockStatus")) == 0)
									{
										string arg = managementObject.GetPropertyValue("DeviceID").ToString();
										ManagementBaseObject managementBaseObject4 = managementObject.InvokeMethod("GetConversionStatus", managementObject, null);
										using (managementBaseObject4)
										{
											BitlockerDeploymentConstants.BitlockerEncryptionStates bitlockerEncryptionStates = (BitlockerDeploymentConstants.BitlockerEncryptionStates)Convert.ToInt32(managementBaseObject4.GetPropertyValue("ConversionStatus"));
											if (bitlockerEncryptionStates == BitlockerDeploymentConstants.BitlockerEncryptionStates.FullyDecrypted || bitlockerEncryptionStates == BitlockerDeploymentConstants.BitlockerEncryptionStates.DecryptionInProgress || bitlockerEncryptionStates == BitlockerDeploymentConstants.BitlockerEncryptionStates.DecryptionPaused)
											{
												stringBuilder.AppendLine(string.Format("{0}{1}", arg, Environment.NewLine));
											}
										}
									}
								}
							}
						}
					}
					result = stringBuilder.ToString();
				}
			}
			catch (Exception ex)
			{
				WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.HighAvailabilityTracer, BitlockerDeploymentUtility.traceContext, string.Format("Exception {0} in {1} GetUnencryptedEncryptableVolumes", ex.Message, ex.StackTrace), Environment.MachineName, null, "GetUnencryptedEncryptableVolumes", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\BitlockerDeployment\\Common\\BitlockerDeploymentUtility.cs", 245);
				result = string.Empty;
			}
			return result;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x0000A5C0 File Offset: 0x000087C0
		public static string GetUnencryptedEncryptableDataVolumes()
		{
			string result;
			try
			{
				StringBuilder stringBuilder = new StringBuilder();
				ManagementObjectCollection encryptableDataVolumes = BitlockerDeploymentUtility.GetEncryptableDataVolumes();
				using (encryptableDataVolumes)
				{
					if (encryptableDataVolumes != null)
					{
						foreach (ManagementBaseObject managementBaseObject in encryptableDataVolumes)
						{
							ManagementObject managementObject = (ManagementObject)managementBaseObject;
							using (managementObject)
							{
								ManagementBaseObject managementBaseObject2 = managementObject.InvokeMethod("GetLockStatus", managementObject, null);
								using (managementBaseObject2)
								{
									if (Convert.ToInt32(managementBaseObject2.GetPropertyValue("LockStatus")) == 0 && !managementObject.GetPropertyValue("DeviceID").Equals(BitlockerDeploymentUtility.GetBootVolumeDeviceID()))
									{
										string arg = managementObject.GetPropertyValue("DeviceID").ToString();
										ManagementBaseObject managementBaseObject4 = managementObject.InvokeMethod("GetConversionStatus", managementObject, null);
										using (managementBaseObject4)
										{
											BitlockerDeploymentConstants.BitlockerEncryptionStates bitlockerEncryptionStates = (BitlockerDeploymentConstants.BitlockerEncryptionStates)Convert.ToInt32(managementBaseObject4.GetPropertyValue("ConversionStatus"));
											if (bitlockerEncryptionStates == BitlockerDeploymentConstants.BitlockerEncryptionStates.FullyDecrypted || bitlockerEncryptionStates == BitlockerDeploymentConstants.BitlockerEncryptionStates.DecryptionInProgress || bitlockerEncryptionStates == BitlockerDeploymentConstants.BitlockerEncryptionStates.DecryptionPaused)
											{
												stringBuilder.AppendLine(string.Format("{0}{1}", arg, Environment.NewLine));
											}
										}
									}
								}
							}
						}
					}
					result = stringBuilder.ToString();
				}
			}
			catch (Exception ex)
			{
				WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.HighAvailabilityTracer, BitlockerDeploymentUtility.traceContext, string.Format("Exception {0} in {1} GetUnencryptedEncryptableVolumes", ex.Message, ex.StackTrace), Environment.MachineName, null, "GetUnencryptedEncryptableDataVolumes", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\BitlockerDeployment\\Common\\BitlockerDeploymentUtility.cs", 307);
				result = string.Empty;
			}
			return result;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0000A7D8 File Offset: 0x000089D8
		public static string GetEncryptedEncryptableVolumes()
		{
			string result;
			try
			{
				StringBuilder stringBuilder = new StringBuilder();
				ManagementObjectCollection encryptableVolumes = BitlockerDeploymentUtility.GetEncryptableVolumes();
				using (encryptableVolumes)
				{
					if (encryptableVolumes != null)
					{
						foreach (ManagementBaseObject managementBaseObject in encryptableVolumes)
						{
							ManagementObject managementObject = (ManagementObject)managementBaseObject;
							using (managementObject)
							{
								ManagementBaseObject managementBaseObject2 = managementObject.InvokeMethod("GetLockStatus", managementObject, null);
								using (managementBaseObject2)
								{
									BitlockerDeploymentConstants.BitlokerEncryptionLockStates bitlokerEncryptionLockStates = (BitlockerDeploymentConstants.BitlokerEncryptionLockStates)Convert.ToInt32(managementBaseObject2.GetPropertyValue("LockStatus"));
									if (BitlockerDeploymentConstants.BitlokerEncryptionLockStates.Locked != bitlokerEncryptionLockStates)
									{
										string arg = managementObject.GetPropertyValue("DeviceID").ToString();
										ManagementBaseObject managementBaseObject4 = managementObject.InvokeMethod("GetConversionStatus", managementObject, null);
										using (managementBaseObject4)
										{
											BitlockerDeploymentConstants.BitlockerEncryptionStates bitlockerEncryptionStates = (BitlockerDeploymentConstants.BitlockerEncryptionStates)Convert.ToInt32(managementBaseObject4.GetPropertyValue("ConversionStatus"));
											if (bitlockerEncryptionStates == BitlockerDeploymentConstants.BitlockerEncryptionStates.FullyEncrypted || bitlockerEncryptionStates == BitlockerDeploymentConstants.BitlockerEncryptionStates.EncryptionPaused || bitlockerEncryptionStates == BitlockerDeploymentConstants.BitlockerEncryptionStates.EncryptionInProgress)
											{
												stringBuilder.AppendLine(string.Format("{0}{1}", arg, Environment.NewLine));
											}
										}
									}
								}
							}
						}
					}
					result = stringBuilder.ToString();
				}
			}
			catch (Exception ex)
			{
				WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.HighAvailabilityTracer, BitlockerDeploymentUtility.traceContext, string.Format("Exception {0} in {1} GetEncryptedEncryptableVolumes", ex.Message, ex.StackTrace), Environment.MachineName, null, "GetEncryptedEncryptableVolumes", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\BitlockerDeployment\\Common\\BitlockerDeploymentUtility.cs", 368);
				result = string.Empty;
			}
			return result;
		}

		// Token: 0x0600015B RID: 347 RVA: 0x0000A9D8 File Offset: 0x00008BD8
		public static string GetEncryptedEncryptableDataVolumes()
		{
			string result;
			try
			{
				StringBuilder stringBuilder = new StringBuilder();
				ManagementObjectCollection encryptableVolumes = BitlockerDeploymentUtility.GetEncryptableVolumes();
				using (encryptableVolumes)
				{
					if (encryptableVolumes != null)
					{
						foreach (ManagementBaseObject managementBaseObject in encryptableVolumes)
						{
							ManagementObject managementObject = (ManagementObject)managementBaseObject;
							using (managementObject)
							{
								ManagementBaseObject managementBaseObject2 = managementObject.InvokeMethod("GetLockStatus", managementObject, null);
								using (managementBaseObject2)
								{
									BitlockerDeploymentConstants.BitlokerEncryptionLockStates bitlokerEncryptionLockStates = (BitlockerDeploymentConstants.BitlokerEncryptionLockStates)Convert.ToInt32(managementBaseObject2.GetPropertyValue("LockStatus"));
									if (BitlockerDeploymentConstants.BitlokerEncryptionLockStates.Locked != bitlokerEncryptionLockStates && !managementObject.GetPropertyValue("DeviceID").Equals(BitlockerDeploymentUtility.GetBootVolumeDeviceID()))
									{
										string arg = managementObject.GetPropertyValue("DeviceID").ToString();
										ManagementBaseObject managementBaseObject4 = managementObject.InvokeMethod("GetConversionStatus", managementObject, null);
										using (managementBaseObject4)
										{
											BitlockerDeploymentConstants.BitlockerEncryptionStates bitlockerEncryptionStates = (BitlockerDeploymentConstants.BitlockerEncryptionStates)Convert.ToInt32(managementBaseObject4.GetPropertyValue("ConversionStatus"));
											if (bitlockerEncryptionStates == BitlockerDeploymentConstants.BitlockerEncryptionStates.FullyEncrypted || bitlockerEncryptionStates == BitlockerDeploymentConstants.BitlockerEncryptionStates.EncryptionPaused || bitlockerEncryptionStates == BitlockerDeploymentConstants.BitlockerEncryptionStates.EncryptionInProgress)
											{
												stringBuilder.AppendLine(string.Format("{0}{1}", arg, Environment.NewLine));
											}
										}
									}
								}
							}
						}
					}
					result = stringBuilder.ToString();
				}
			}
			catch (Exception ex)
			{
				WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.HighAvailabilityTracer, BitlockerDeploymentUtility.traceContext, string.Format("Exception {0} in {1} GetEncryptedEncryptableVolumes", ex.Message, ex.StackTrace), Environment.MachineName, null, "GetEncryptedEncryptableDataVolumes", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\BitlockerDeployment\\Common\\BitlockerDeploymentUtility.cs", 429);
				result = string.Empty;
			}
			return result;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000ABF0 File Offset: 0x00008DF0
		public static string GetLockedVolumes()
		{
			string result;
			try
			{
				StringBuilder stringBuilder = new StringBuilder();
				ManagementObjectCollection encryptableVolumes = BitlockerDeploymentUtility.GetEncryptableVolumes();
				using (encryptableVolumes)
				{
					if (encryptableVolumes != null)
					{
						foreach (ManagementBaseObject managementBaseObject in encryptableVolumes)
						{
							ManagementObject managementObject = (ManagementObject)managementBaseObject;
							using (managementObject)
							{
								string arg = managementObject.GetPropertyValue("DeviceID").ToString();
								ManagementBaseObject managementBaseObject2 = managementObject.InvokeMethod("GetLockStatus", managementObject, null);
								BitlockerDeploymentConstants.BitlokerEncryptionLockStates bitlokerEncryptionLockStates = (BitlockerDeploymentConstants.BitlokerEncryptionLockStates)Convert.ToInt32(managementBaseObject2.GetPropertyValue("LockStatus"));
								if (bitlokerEncryptionLockStates == BitlockerDeploymentConstants.BitlokerEncryptionLockStates.Locked)
								{
									stringBuilder.AppendLine(string.Format("{0}{1}", arg, Environment.NewLine));
								}
							}
						}
					}
					result = stringBuilder.ToString();
				}
			}
			catch (Exception ex)
			{
				WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.HighAvailabilityTracer, BitlockerDeploymentUtility.traceContext, string.Format("Exception {0} in {1} GetLockedVolumes", ex.Message, ex.StackTrace), Environment.MachineName, null, "GetLockedVolumes", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\BitlockerDeployment\\Common\\BitlockerDeploymentUtility.cs", 474);
				result = string.Empty;
			}
			return result;
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000AD34 File Offset: 0x00008F34
		public static string GetSuspendedVolumes()
		{
			string result;
			try
			{
				StringBuilder stringBuilder = new StringBuilder();
				ManagementObjectCollection encryptableVolumes = BitlockerDeploymentUtility.GetEncryptableVolumes();
				using (encryptableVolumes)
				{
					if (encryptableVolumes != null)
					{
						foreach (ManagementBaseObject managementBaseObject in encryptableVolumes)
						{
							ManagementObject managementObject = (ManagementObject)managementBaseObject;
							using (managementObject)
							{
								string arg = managementObject.GetPropertyValue("DeviceID").ToString();
								if (BitlockerDeploymentUtility.IsVolumeEncryptionSuspended(managementObject))
								{
									stringBuilder.AppendLine(string.Format("{0}{1}", arg, Environment.NewLine));
								}
							}
						}
					}
					result = stringBuilder.ToString();
				}
			}
			catch (Exception ex)
			{
				WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.HighAvailabilityTracer, BitlockerDeploymentUtility.traceContext, string.Format("Exception {0} in {1} GetLockedVolumes", ex.Message, ex.StackTrace), Environment.MachineName, null, "GetSuspendedVolumes", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\BitlockerDeployment\\Common\\BitlockerDeploymentUtility.cs", 518);
				result = string.Empty;
			}
			return result;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000AE58 File Offset: 0x00009058
		public static string GetUnlockedVolumes()
		{
			string result;
			try
			{
				StringBuilder stringBuilder = new StringBuilder();
				ManagementObjectCollection encryptableVolumes = BitlockerDeploymentUtility.GetEncryptableVolumes();
				using (encryptableVolumes)
				{
					if (encryptableVolumes != null)
					{
						foreach (ManagementBaseObject managementBaseObject in encryptableVolumes)
						{
							ManagementObject managementObject = (ManagementObject)managementBaseObject;
							using (managementObject)
							{
								string arg = managementObject.GetPropertyValue("DeviceID").ToString();
								ManagementBaseObject managementBaseObject2 = managementObject.InvokeMethod("GetLockStatus", managementObject, null);
								using (managementBaseObject2)
								{
									if (Convert.ToInt32(managementBaseObject2.GetPropertyValue("LockStatus")) == 0)
									{
										stringBuilder.AppendLine(string.Format("{0}{1}", arg, Environment.NewLine));
									}
								}
							}
						}
					}
					result = stringBuilder.ToString();
				}
			}
			catch (Exception ex)
			{
				WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.HighAvailabilityTracer, BitlockerDeploymentUtility.traceContext, string.Format("Exception {0} in {1} GetUnlockedVolumes", ex.StackTrace), Environment.MachineName, null, "GetUnlockedVolumes", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\BitlockerDeployment\\Common\\BitlockerDeploymentUtility.cs", 567);
				result = string.Empty;
			}
			return result;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x0000AFB4 File Offset: 0x000091B4
		public static bool Unlock(ManagementObject encryptableVolume, out string outputString, out string errorString)
		{
			return ProcessRunner.Run(Path.Combine(BitlockerDeploymentConstants.DatacenterFolderPath, "BitlockerDataVolumeUnlocker.exe"), null, 100000, null, out outputString, out errorString) == 0;
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000AFE8 File Offset: 0x000091E8
		public static bool Resume(ManagementObject encryptableVolume)
		{
			object obj = encryptableVolume.InvokeMethod("EnableKeyProtectors", null);
			return obj.ToString().Equals("0");
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0000B018 File Offset: 0x00009218
		public static bool AutoUnlockDataVolume(ManagementObject encryptableVolume)
		{
			string arg = encryptableVolume.GetPropertyValue("DeviceID").ToString();
			try
			{
				encryptableVolume.InvokeMethod("ProtectKeyWithExternalKey", new object[0]);
				ManagementBaseObject methodParameters = encryptableVolume.GetMethodParameters("GetKeyProtectors");
				using (methodParameters)
				{
					methodParameters["KeyProtectorType"] = 2;
					ManagementBaseObject managementBaseObject2 = encryptableVolume.InvokeMethod("GetKeyProtectors", methodParameters, null);
					using (managementBaseObject2)
					{
						string[] array = (string[])managementBaseObject2.GetPropertyValue("VolumeKeyProtectorID");
						if (array == null || array.Length == 0)
						{
							return false;
						}
						ManagementBaseObject methodParameters2 = encryptableVolume.GetMethodParameters("EnableAutoUnlock");
						using (methodParameters2)
						{
							methodParameters2["VolumeKeyProtectorID"] = array[0];
							ManagementBaseObject managementBaseObject5 = encryptableVolume.InvokeMethod("EnableAutoUnlock", methodParameters2, null);
							using (managementBaseObject5)
							{
								if (managementBaseObject5.GetPropertyValue("ReturnValue").ToString().Equals("0"))
								{
									return true;
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.HighAvailabilityTracer, BitlockerDeploymentUtility.traceContext, string.Format("Exception {0} in {1} AutoUnlockDataVolume", ex.StackTrace), Environment.MachineName, null, "AutoUnlockDataVolume", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\BitlockerDeployment\\Common\\BitlockerDeploymentUtility.cs", 655);
				throw new Exception(string.Format("AutoUnlock on Volume {0} unsuccessfull.", arg));
			}
			return false;
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000B1BC File Offset: 0x000093BC
		public static string GetBootVolumeDeviceID()
		{
			SelectQuery query = new SelectQuery("SELECT * FROM Win32_Volume Where BootVolume=True");
			using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(query))
			{
				foreach (ManagementBaseObject managementBaseObject in managementObjectSearcher.Get())
				{
					ManagementObject managementObject = (ManagementObject)managementBaseObject;
					using (managementObject)
					{
						return managementObject.GetPropertyValue("DeviceID").ToString();
					}
				}
			}
			return null;
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000B268 File Offset: 0x00009468
		public static string GetSystemVolumeDeviceID()
		{
			SelectQuery query = new SelectQuery("SELECT * FROM Win32_Volume Where SystemVolume=True");
			using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(query))
			{
				foreach (ManagementBaseObject managementBaseObject in managementObjectSearcher.Get())
				{
					ManagementObject managementObject = (ManagementObject)managementBaseObject;
					using (managementObject)
					{
						return managementObject.GetPropertyValue("DeviceID").ToString();
					}
				}
			}
			return null;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000B318 File Offset: 0x00009518
		public static bool IsBootVolume(ManagementObject encryptableVolume)
		{
			string value = encryptableVolume.GetPropertyValue("DeviceID").ToString();
			return BitlockerDeploymentUtility.GetBootVolumeDeviceID().Equals(value);
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000B344 File Offset: 0x00009544
		public static ManagementObject GetBootEncryptableVolumeObject()
		{
			foreach (ManagementBaseObject managementBaseObject in BitlockerDeploymentUtility.GetEncryptableVolumes())
			{
				ManagementObject managementObject = (ManagementObject)managementBaseObject;
				using (managementObject)
				{
					if (BitlockerDeploymentUtility.IsBootVolume(managementObject))
					{
						return managementObject;
					}
				}
			}
			return null;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x0000B3B8 File Offset: 0x000095B8
		public static bool IsBootVolumeEncrypted()
		{
			ManagementObject bootEncryptableVolumeObject = BitlockerDeploymentUtility.GetBootEncryptableVolumeObject();
			using (bootEncryptableVolumeObject)
			{
				if (bootEncryptableVolumeObject == null && BitlockerDeploymentUtility.GetSystemVolumeDeviceID() == BitlockerDeploymentUtility.GetBootVolumeDeviceID())
				{
					return false;
				}
				bootEncryptableVolumeObject.GetPropertyValue("DeviceID").ToString();
				ManagementBaseObject managementBaseObject = bootEncryptableVolumeObject.InvokeMethod("GetConversionStatus", bootEncryptableVolumeObject, null);
				BitlockerDeploymentConstants.BitlockerEncryptionStates bitlockerEncryptionStates = (BitlockerDeploymentConstants.BitlockerEncryptionStates)Convert.ToInt32(managementBaseObject.GetPropertyValue("ConversionStatus"));
				if (bitlockerEncryptionStates == BitlockerDeploymentConstants.BitlockerEncryptionStates.FullyEncrypted || bitlockerEncryptionStates == BitlockerDeploymentConstants.BitlockerEncryptionStates.EncryptionPaused || bitlockerEncryptionStates == BitlockerDeploymentConstants.BitlockerEncryptionStates.EncryptionInProgress)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0000B448 File Offset: 0x00009648
		public static bool IsVolumeLocked(ManagementObject encryptableVolume)
		{
			ManagementBaseObject managementBaseObject = encryptableVolume.InvokeMethod("GetLockStatus", encryptableVolume, null);
			using (managementBaseObject)
			{
				BitlockerDeploymentConstants.BitlokerEncryptionLockStates bitlokerEncryptionLockStates = (BitlockerDeploymentConstants.BitlokerEncryptionLockStates)Convert.ToInt32(managementBaseObject.GetPropertyValue("LockStatus"));
				if (BitlockerDeploymentConstants.BitlokerEncryptionLockStates.Locked == bitlokerEncryptionLockStates)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000B4A0 File Offset: 0x000096A0
		public static bool IsVolumeEncryptionSuspended(ManagementObject encryptableVolume)
		{
			ManagementBaseObject managementBaseObject = encryptableVolume.InvokeMethod("GetProtectionStatus", encryptableVolume, null);
			using (managementBaseObject)
			{
				ManagementBaseObject managementBaseObject3 = encryptableVolume.InvokeMethod("GetConversionStatus", encryptableVolume, null);
				using (managementBaseObject3)
				{
					BitlockerDeploymentConstants.BitlokerEncryptionProtectionStates bitlokerEncryptionProtectionStates = (BitlockerDeploymentConstants.BitlokerEncryptionProtectionStates)Convert.ToInt32(managementBaseObject.GetPropertyValue("ProtectionStatus"));
					BitlockerDeploymentConstants.BitlockerEncryptionStates bitlockerEncryptionStates = (BitlockerDeploymentConstants.BitlockerEncryptionStates)Convert.ToInt32(managementBaseObject3.GetPropertyValue("ConversionStatus"));
					if (bitlokerEncryptionProtectionStates == BitlockerDeploymentConstants.BitlokerEncryptionProtectionStates.Unprotected && BitlockerDeploymentConstants.BitlockerEncryptionStates.FullyEncrypted == bitlockerEncryptionStates)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0000B53C File Offset: 0x0000973C
		public static BitlockerDeploymentConstants.BitlockerEncryptionStates GetConversionStatus(ManagementObject encryptableVolume)
		{
			ManagementBaseObject managementBaseObject = encryptableVolume.InvokeMethod("GetConversionStatus", encryptableVolume, null);
			return (BitlockerDeploymentConstants.BitlockerEncryptionStates)Convert.ToInt32(managementBaseObject.GetPropertyValue("ConversionStatus"));
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000B568 File Offset: 0x00009768
		public static int EncryptionPercentage(ManagementObject encryptableVolume)
		{
			int result = 0;
			if (BitlockerDeploymentUtility.IsVolumeLocked(encryptableVolume))
			{
				return result;
			}
			ManagementBaseObject managementBaseObject = encryptableVolume.InvokeMethod("GetConversionStatus", encryptableVolume, null);
			int.TryParse(managementBaseObject.GetPropertyValue("EncryptionPercentage").ToString(), out result);
			return result;
		}

		// Token: 0x0600016B RID: 363 RVA: 0x0000B5A8 File Offset: 0x000097A8
		public static string GetDeviceID(ManagementObject encryptableVolume)
		{
			return encryptableVolume.GetPropertyValue("DeviceID").ToString();
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0000B5BC File Offset: 0x000097BC
		public static string GetRegistryValue(string registryKey, string registryParam)
		{
			RegistryKey registryKey2 = Registry.LocalMachine.OpenSubKey(registryKey);
			if (registryKey2 != null)
			{
				object value = registryKey2.GetValue(registryParam);
				if (value != null)
				{
					return value.ToString();
				}
			}
			return string.Empty;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x0000B5F0 File Offset: 0x000097F0
		public static string GetVolumesNotProtectedWithDra()
		{
			ManagementObjectCollection encryptableDataVolumes = BitlockerDeploymentUtility.GetEncryptableDataVolumes();
			StringBuilder stringBuilder = new StringBuilder();
			if (encryptableDataVolumes != null)
			{
				foreach (ManagementBaseObject managementBaseObject in encryptableDataVolumes)
				{
					ManagementObject managementObject = (ManagementObject)managementBaseObject;
					if (BitlockerDeploymentUtility.IsVolumeProtected(managementObject) && !BitlockerDeploymentUtility.IsVolumeProtectedWithDra(managementObject))
					{
						stringBuilder.AppendLine(managementObject.GetPropertyValue("DeviceID").ToString());
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600016E RID: 366 RVA: 0x0000B674 File Offset: 0x00009874
		public static bool IsVolumeProtected(ManagementObject encryptableVolume)
		{
			ManagementBaseObject managementBaseObject = encryptableVolume.InvokeMethod("GetProtectionStatus", encryptableVolume, null);
			return Convert.ToInt32(managementBaseObject.GetPropertyValue("ProtectionStatus")) != 0;
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0000B6A8 File Offset: 0x000098A8
		private static bool IsVolumeProtectedWithDra(ManagementObject encryptableVolume)
		{
			bool result = false;
			if (!BitlockerDeploymentUtility.IsVolumeProtected(encryptableVolume))
			{
				return false;
			}
			ManagementBaseObject managementBaseObject = null;
			int num = BitlockerDeploymentUtility.CallVolumeWmiMethod(encryptableVolume, "GetKeyProtectors", new string[0], new object[0], out managementBaseObject);
			if (num != 0)
			{
				return false;
			}
			string[] array = (string[])managementBaseObject["VolumeKeyProtectorID"];
			foreach (string text in array)
			{
				if (BitlockerDeploymentUtility.CallVolumeWmiMethod(encryptableVolume, "GetKeyProtectorType", new string[]
				{
					"VolumeKeyProtectorID"
				}, new object[]
				{
					text
				}, out managementBaseObject) == 0)
				{
					object value = managementBaseObject["KeyProtectorType"];
					if (Convert.ToInt32(value) == 7 && BitlockerDeploymentUtility.CallVolumeWmiMethod(encryptableVolume, "GetKeyProtectorCertificate", new string[]
					{
						"VolumeKeyProtectorID"
					}, new object[]
					{
						text
					}, out managementBaseObject) == 0)
					{
						object value2 = managementBaseObject["CertType"];
						if (Convert.ToInt32(value2) == 1)
						{
							result = true;
							break;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x0000B7B4 File Offset: 0x000099B4
		public static List<string> GetCertificateList(ManagementObject encryptableVolume)
		{
			List<string> list = new List<string>();
			if (!BitlockerDeploymentUtility.IsVolumeProtected(encryptableVolume))
			{
				return list;
			}
			ManagementBaseObject managementBaseObject = null;
			int num = BitlockerDeploymentUtility.CallVolumeWmiMethod(encryptableVolume, "GetKeyProtectors", new string[0], new object[0], out managementBaseObject);
			if (num != 0)
			{
				return list;
			}
			string[] array = (string[])managementBaseObject["VolumeKeyProtectorID"];
			foreach (string text in array)
			{
				if (BitlockerDeploymentUtility.CallVolumeWmiMethod(encryptableVolume, "GetKeyProtectorType", new string[]
				{
					"VolumeKeyProtectorID"
				}, new object[]
				{
					text
				}, out managementBaseObject) == 0)
				{
					object value = managementBaseObject["KeyProtectorType"];
					if (Convert.ToInt32(value) == 7 && BitlockerDeploymentUtility.CallVolumeWmiMethod(encryptableVolume, "GetKeyProtectorCertificate", new string[]
					{
						"VolumeKeyProtectorID"
					}, new object[]
					{
						text
					}, out managementBaseObject) == 0)
					{
						object value2 = managementBaseObject["CertType"];
						if (Convert.ToInt32(value2) == 1)
						{
							string item = Convert.ToString(managementBaseObject["CertThumbprint"]);
							list.Add(item);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000B8DC File Offset: 0x00009ADC
		public static Dictionary<string, string> GetVolumesWithoutDraDecryptor()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			X509Store x509Store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
			x509Store.Open(OpenFlags.ReadOnly);
			try
			{
				X509Certificate2Collection certificates = x509Store.Certificates;
				ManagementObjectCollection encryptableDataVolumes = BitlockerDeploymentUtility.GetEncryptableDataVolumes();
				foreach (ManagementBaseObject managementBaseObject in encryptableDataVolumes)
				{
					ManagementObject encryptableVolume = (ManagementObject)managementBaseObject;
					List<string> certificateList = BitlockerDeploymentUtility.GetCertificateList(encryptableVolume);
					bool flag = certificateList.Count <= 0;
					StringBuilder stringBuilder = new StringBuilder();
					foreach (string text in certificateList)
					{
						X509Certificate2Collection x509Certificate2Collection = certificates.Find(X509FindType.FindByThumbprint, text, false);
						if (x509Certificate2Collection.Count >= 1)
						{
							flag = true;
							break;
						}
						stringBuilder.AppendLine(text);
					}
					if (!flag)
					{
						string deviceID = BitlockerDeploymentUtility.GetDeviceID(encryptableVolume);
						dictionary[deviceID] = stringBuilder.ToString();
					}
				}
			}
			finally
			{
				x509Store.Close();
			}
			return dictionary;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000BA00 File Offset: 0x00009C00
		private static int CallVolumeWmiMethod(ManagementObject encryptableVolume, string methodName, string[] inParamNameList, object[] inParamValueList, out ManagementBaseObject outParams)
		{
			if (inParamNameList == null || inParamValueList == null || inParamNameList.Length != inParamValueList.Length)
			{
				throw new ApplicationException("Wrong usage of internal function CallVolumeMethod, inParamNameList and inParamValueList length doesn't match");
			}
			ManagementBaseObject methodParameters = encryptableVolume.GetMethodParameters(methodName);
			for (int i = 0; i < inParamNameList.Length; i++)
			{
				methodParameters[inParamNameList[i]] = inParamValueList[i];
			}
			outParams = encryptableVolume.InvokeMethod(methodName, methodParameters, null);
			return Convert.ToInt32(outParams["ReturnValue"]);
		}

		// Token: 0x04000118 RID: 280
		private static TracingContext traceContext = TracingContext.Default;
	}
}
