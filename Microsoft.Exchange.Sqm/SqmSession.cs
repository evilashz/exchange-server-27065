using System;
using System.Diagnostics;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Common;
using Microsoft.Win32;

namespace Microsoft.Exchange.Sqm
{
	// Token: 0x02000006 RID: 6
	public class SqmSession
	{
		// Token: 0x0600002B RID: 43 RVA: 0x000020D0 File Offset: 0x000002D0
		static SqmSession()
		{
			ExWatson.TryRegistryKeyGetValue<string>("SOFTWARE\\Microsoft\\ExchangeServer\\v15", "LabName", string.Empty, out SqmSession.labName);
			if (string.IsNullOrEmpty(SqmSession.labName))
			{
				SqmSession.labName = (ExWatson.MsInternal ? "MSInternal" : "Customer");
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002138 File Offset: 0x00000338
		public SqmSession(SqmAppID sqmAppID, SqmSession.Scope scope) : this(sqmAppID, scope, 0U, 0U, 0U, 0U)
		{
			try
			{
				Version installedVersion = ExchangeSetupContext.InstalledVersion;
				if (installedVersion != null)
				{
					this.majorVersion = (uint)installedVersion.Major;
					this.minorVersion = (uint)installedVersion.Minor;
					this.majorBuild = (uint)installedVersion.Build;
					this.minorBuild = (uint)installedVersion.Revision;
				}
			}
			catch (SetupVersionInformationCorruptException)
			{
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000021A8 File Offset: 0x000003A8
		public SqmSession(SqmAppID sqmAppID, SqmSession.Scope scope, uint majorVersion, uint minorVersion) : this(sqmAppID, scope, majorVersion, minorVersion, 0U, 0U)
		{
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000021B8 File Offset: 0x000003B8
		public SqmSession(SqmAppID sqmAppID, SqmSession.Scope scope, uint majorVersion, uint minorVersion, uint majorBuild, uint minorBuild)
		{
			this.sqmAppID = sqmAppID;
			this.Name = (Enum.GetName(typeof(SqmAppID), sqmAppID) ?? ("Default" + (int)sqmAppID));
			switch (scope)
			{
			case SqmSession.Scope.AppDomain:
				using (Process currentProcess = Process.GetCurrentProcess())
				{
					this.physicalSessionName = string.Concat(new object[]
					{
						this.Name,
						currentProcess.Id,
						"_",
						AppDomain.CurrentDomain.Id.ToString()
					});
					goto IL_F0;
				}
				break;
			case SqmSession.Scope.Process:
				break;
			default:
				goto IL_E4;
			}
			using (Process currentProcess2 = Process.GetCurrentProcess())
			{
				this.physicalSessionName = this.Name + currentProcess2.Id;
				goto IL_F0;
			}
			IL_E4:
			this.physicalSessionName = this.Name;
			IL_F0:
			this.majorVersion = majorVersion;
			this.minorVersion = minorVersion;
			this.majorBuild = majorBuild;
			this.minorBuild = minorBuild;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000022F0 File Offset: 0x000004F0
		// (set) Token: 0x06000030 RID: 48 RVA: 0x000022F8 File Offset: 0x000004F8
		public uint SessionHandle { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002301 File Offset: 0x00000501
		public virtual uint MaximumDataFileSize
		{
			get
			{
				return 60000U;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002308 File Offset: 0x00000508
		public virtual uint MaximumApproachingLimit
		{
			get
			{
				return 15000U;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000033 RID: 51 RVA: 0x0000230F File Offset: 0x0000050F
		// (set) Token: 0x06000034 RID: 52 RVA: 0x00002317 File Offset: 0x00000517
		public string Name { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002320 File Offset: 0x00000520
		public string DataFileDirectory
		{
			get
			{
				if (string.IsNullOrEmpty(this.dataFileDirectory))
				{
					string text = null;
					string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Microsoft\\Exchange Server\\v15");
					try
					{
						Directory.CreateDirectory(path);
						this.dataFileDirectory = path;
					}
					catch (IOException ex)
					{
						text = ex.Message;
					}
					catch (UnauthorizedAccessException ex2)
					{
						text = ex2.Message;
					}
					if (!string.IsNullOrEmpty(text))
					{
						ExTraceGlobals.SqmTracer.TraceDebug<string>(60751, 0L, "Failed to create data file directory for SQM session. Error: {0}", text);
					}
					if (string.IsNullOrEmpty(this.dataFileDirectory))
					{
						this.Enabled = false;
					}
				}
				return this.dataFileDirectory;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000036 RID: 54 RVA: 0x000023C8 File Offset: 0x000005C8
		// (set) Token: 0x06000037 RID: 55 RVA: 0x000023DF File Offset: 0x000005DF
		public bool Enabled
		{
			get
			{
				return this.IsOpened && SqmLibWrap.SqmGetEnabled(this.SessionHandle);
			}
			set
			{
				if (this.IsOpened)
				{
					SqmLibWrap.SqmSetEnabled(this.SessionHandle, value);
				}
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000038 RID: 56 RVA: 0x000023F5 File Offset: 0x000005F5
		public bool IsOpened
		{
			get
			{
				return this.SessionHandle != 0U;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002403 File Offset: 0x00000603
		// (set) Token: 0x0600003A RID: 58 RVA: 0x0000240B File Offset: 0x0000060B
		internal uint SessionSize
		{
			get
			{
				return this.sessionSize;
			}
			set
			{
				this.sessionSize = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002414 File Offset: 0x00000614
		// (set) Token: 0x0600003C RID: 60 RVA: 0x0000241C File Offset: 0x0000061C
		protected uint MajorVersion
		{
			get
			{
				return this.majorVersion;
			}
			set
			{
				this.majorVersion = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002425 File Offset: 0x00000625
		// (set) Token: 0x0600003E RID: 62 RVA: 0x0000242D File Offset: 0x0000062D
		protected uint MinorVersion
		{
			get
			{
				return this.minorVersion;
			}
			set
			{
				this.minorVersion = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002438 File Offset: 0x00000638
		private string LdapHostName
		{
			get
			{
				if (this.ldapHostName == null)
				{
					string text = null;
					try
					{
						using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\EdgeTransportRole\\AdamSettings\\MSExchange"))
						{
							if (registryKey != null)
							{
								object value = registryKey.GetValue("LdapPort");
								if (value != null)
								{
									this.ldapHostName = Environment.MachineName + ":" + value.ToString() + "/";
								}
							}
							else
							{
								this.ldapHostName = string.Empty;
							}
						}
					}
					catch (SecurityException ex)
					{
						text = ex.Message;
					}
					catch (UnauthorizedAccessException ex2)
					{
						text = ex2.Message;
					}
					if (!string.IsNullOrEmpty(text))
					{
						ExTraceGlobals.SqmTracer.TraceDebug<string>(44367, 0L, "Failed to get Ldap host name. Error: {0}", text);
					}
				}
				return this.ldapHostName;
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002514 File Offset: 0x00000714
		public void Open()
		{
			if (!this.IsOpened)
			{
				this.SessionHandle = SqmLibWrap.SqmGetSession(this.physicalSessionName, this.MaximumDataFileSize, 0U);
				if (!this.IsOpened)
				{
					this.SessionHandle = SqmLibWrap.SqmGetSession(this.physicalSessionName, this.MaximumDataFileSize, 1U);
					if (this.IsOpened)
					{
						this.OnOpen();
						this.OnCreate();
					}
				}
				else
				{
					if (!this.Enabled)
					{
						ExTraceGlobals.SqmTracer.TraceDebug(60559, 0L, "Calling SqmStartSession from Open");
						SqmLibWrap.SqmStartSession(this.SessionHandle);
						this.Enabled = this.GetOptInStatus();
					}
					this.OnOpen();
				}
				this.SetCommonDataPoints();
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000025BB File Offset: 0x000007BB
		public void Close()
		{
			if (this.IsOpened)
			{
				this.OnClosing();
				this.UpdateData(true, true);
				SqmLibWrap.SqmWaitForUploadComplete(0U, 2U);
				this.SessionHandle = 0U;
				if (this.dataWriteMutex != null)
				{
					this.dataWriteMutex.Close();
				}
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x0000261C File Offset: 0x0000081C
		public void SetDataPoint(SqmDataID dataID, int value)
		{
			this.InternalSetDataPoint(delegate
			{
				SqmLibWrap.SqmSet(this.SessionHandle, (uint)dataID, (uint)value);
				return 4U;
			});
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002680 File Offset: 0x00000880
		public void SetDataPoint(SqmDataID dataID, uint value)
		{
			this.InternalSetDataPoint(delegate
			{
				SqmLibWrap.SqmSet(this.SessionHandle, (uint)dataID, value);
				return 4U;
			});
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000026E8 File Offset: 0x000008E8
		public void SetDataPoint(SqmDataID dataID, bool value)
		{
			this.InternalSetDataPoint(delegate
			{
				SqmLibWrap.SqmSetBool(this.SessionHandle, (uint)dataID, value ? 1U : 0U);
				return 4U;
			});
		}

		// Token: 0x06000045 RID: 69 RVA: 0x0000275C File Offset: 0x0000095C
		public void SetDataPoint(SqmDataID dataID, string value)
		{
			this.InternalSetDataPoint(delegate
			{
				SqmLibWrap.SqmSetString(this.SessionHandle, (uint)dataID, value);
				return (uint)Encoding.Unicode.GetByteCount(value);
			});
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000027C0 File Offset: 0x000009C0
		public void SetBitsDataPoint(SqmDataID dataID, uint value)
		{
			this.InternalSetDataPoint(delegate
			{
				SqmLibWrap.SqmSetBits(this.SessionHandle, (uint)dataID, value);
				return 4U;
			});
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002824 File Offset: 0x00000A24
		public void SetIfMaxDataPoint(SqmDataID dataID, uint value)
		{
			this.InternalSetDataPoint(delegate
			{
				SqmLibWrap.SqmSetIfMax(this.SessionHandle, (uint)dataID, value);
				return 4U;
			});
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002888 File Offset: 0x00000A88
		public void SetIfMinDataPoint(SqmDataID dataID, uint value)
		{
			this.InternalSetDataPoint(delegate
			{
				SqmLibWrap.SqmSetIfMin(this.SessionHandle, (uint)dataID, value);
				return 4U;
			});
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000028EC File Offset: 0x00000AEC
		public void IncrementDataPoint(SqmDataID dataID, uint value)
		{
			this.InternalSetDataPoint(delegate
			{
				SqmLibWrap.SqmIncrement(this.SessionHandle, (uint)dataID, value);
				return 4U;
			});
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002950 File Offset: 0x00000B50
		public void AddToAverageDataPoint(SqmDataID dataID, uint value)
		{
			this.InternalSetDataPoint(delegate
			{
				SqmLibWrap.SqmAddToAverage(this.SessionHandle, (uint)dataID, value);
				return 4U;
			});
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002ACC File Offset: 0x00000CCC
		public void AddToStreamDataPoint(SqmDataID dataID, params object[] list)
		{
			this.InternalSetDataPoint(delegate
			{
				uint num = 0U;
				foreach (object obj in list)
				{
					if (obj is bool)
					{
						SqmLibWrap.SqmAddToStreamDWord(this.SessionHandle, (uint)dataID, (uint)list.Length, ((bool)obj) ? 1U : 0U);
						num += 4U;
					}
					else if (obj is int)
					{
						SqmLibWrap.SqmAddToStreamDWord(this.SessionHandle, (uint)dataID, (uint)list.Length, (uint)((int)obj));
						num += 4U;
					}
					else if (obj is uint)
					{
						SqmLibWrap.SqmAddToStreamDWord(this.SessionHandle, (uint)dataID, (uint)list.Length, (uint)obj);
						num += 4U;
					}
					else if (obj is string)
					{
						SqmLibWrap.SqmAddToStreamString(this.SessionHandle, (uint)dataID, (uint)list.Length, (string)obj);
						num += (uint)Encoding.Unicode.GetByteCount((string)obj);
					}
					else
					{
						ExTraceGlobals.SqmTracer.TraceDebug<string>(62799, 0L, "Unexpected object type: {0}", obj.GetType().Name);
					}
				}
				return num;
			});
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002B06 File Offset: 0x00000D06
		protected virtual void OnOpen()
		{
			this.dataWriteMutex = new Mutex(false, this.physicalSessionName);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002B1C File Offset: 0x00000D1C
		protected virtual void OnCreate()
		{
			ExTraceGlobals.SqmTracer.TraceDebug(35983, 0L, "Calling SqmStartSession from OnCreate");
			SqmLibWrap.SqmStartSession(this.SessionHandle);
			this.UpdateData(false, false);
			Guid guid;
			if (!SqmLibWrap.SqmReadSharedMachineId(out guid))
			{
				SqmLibWrap.SqmCreateNewId(out guid);
				SqmLibWrap.SqmWriteSharedMachineId(ref guid);
				SqmLibWrap.SqmReadSharedMachineId(out guid);
			}
			SqmLibWrap.SqmSetMachineId(this.SessionHandle, ref guid);
			Guid guid2;
			if (!SqmLibWrap.SqmReadSharedUserId(out guid2))
			{
				SqmLibWrap.SqmCreateNewId(out guid2);
				SqmLibWrap.SqmWriteSharedUserId(ref guid2);
				SqmLibWrap.SqmReadSharedUserId(out guid2);
			}
			SqmLibWrap.SqmSetUserId(this.SessionHandle, ref guid2);
			SqmLibWrap.SqmSetAppId(this.SessionHandle, (uint)this.sqmAppID);
			SqmLibWrap.SqmSetAppVersion(this.SessionHandle, this.majorVersion, this.minorVersion);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002BD7 File Offset: 0x00000DD7
		protected virtual void OnClosing()
		{
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002BDC File Offset: 0x00000DDC
		protected virtual bool GetOptInStatus()
		{
			if (this.LdapHostName == null)
			{
				return false;
			}
			bool result = false;
			string text = null;
			try
			{
				string arg;
				using (DirectoryEntry directoryEntry = new DirectoryEntry(string.Format("LDAP://{0}RootDse", this.LdapHostName)))
				{
					arg = (string)directoryEntry.Properties["configurationNamingContext"].Value;
				}
				this.configNCUri = string.Format("LDAP://{0}{1}", this.LdapHostName, arg);
				using (DirectorySearcher directorySearcher = new DirectorySearcher(new DirectoryEntry(this.configNCUri)))
				{
					string machineName = Environment.MachineName;
					if (machineName != null)
					{
						directorySearcher.Filter = string.Format("(&(objectClass=msExchExchangeServer)(cn={0}))", machineName);
						directorySearcher.PropertiesToLoad.Clear();
						directorySearcher.PropertiesToLoad.Add("msExchCustomerFeedbackEnabled");
						SearchResult searchResult = directorySearcher.FindOne();
						if (searchResult != null)
						{
							ResultPropertyValueCollection resultPropertyValueCollection = searchResult.Properties["msExchCustomerFeedbackEnabled"];
							if (resultPropertyValueCollection != null && resultPropertyValueCollection.Count > 0 && resultPropertyValueCollection[0] is bool)
							{
								result = (bool)resultPropertyValueCollection[0];
							}
						}
					}
				}
			}
			catch (COMException ex)
			{
				text = ex.Message;
			}
			catch (ActiveDirectoryObjectNotFoundException ex2)
			{
				text = ex2.Message;
			}
			catch (ActiveDirectoryOperationException ex3)
			{
				text = ex3.Message;
			}
			catch (ActiveDirectoryServerDownException ex4)
			{
				text = ex4.Message;
			}
			if (!string.IsNullOrEmpty(text))
			{
				ExTraceGlobals.SqmTracer.TraceDebug<string>(52559, 0L, "Failed to get opt-in status. Error: {0}", text);
			}
			return result;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002D90 File Offset: 0x00000F90
		private static bool UploadCallback(uint hr, string filePath, uint httpResponse)
		{
			ExTraceGlobals.SqmTracer.TraceDebug<string, uint, uint>(36175, 0L, "Cmdlet SQM infra: Data file {0} was uploaded with result {1} and http response {2}", filePath, hr, httpResponse);
			return httpResponse == 200U;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002DB4 File Offset: 0x00000FB4
		private void UpdateData(bool flushToDisk, bool closing)
		{
			try
			{
				this.dataWriteMutex.WaitOne();
				if (this.IsOpened && !string.IsNullOrEmpty(this.DataFileDirectory))
				{
					if (flushToDisk && this.SessionSize > 0U)
					{
						ExTraceGlobals.SqmTracer.TraceDebug(46223, 0L, "Calling SqmEndSession");
						SqmLibWrap.SqmEndSession(this.SessionHandle, Path.Combine(this.DataFileDirectory, this.physicalSessionName + "%02d.sqm"), 10U, 2U);
						if (!closing)
						{
							ExTraceGlobals.SqmTracer.TraceDebug(52367, 0L, "Calling SqmStartSession from UpdateData");
							SqmLibWrap.SqmStartSession(this.SessionHandle);
							this.SessionSize = 0U;
							this.SetCommonDataPoints();
						}
					}
					if (!closing)
					{
						this.Enabled = this.GetOptInStatus();
						if (this.Enabled)
						{
							ExTraceGlobals.SqmTracer.TraceDebug(62607, 0L, "Calling SqmStartUpload");
							SqmLibWrap.SqmStartUpload(Path.Combine(this.DataFileDirectory, this.Name + "*.sqm"), null, "https://sqm.microsoft.com/sqm/exchange/sqmserver.dll", 6U, SqmSession.uploadCallback);
						}
					}
				}
			}
			finally
			{
				this.dataWriteMutex.ReleaseMutex();
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002EDC File Offset: 0x000010DC
		private Guid GetOrganization()
		{
			if (this.organizationGuid != Guid.Empty)
			{
				return this.organizationGuid;
			}
			if (this.LdapHostName == null)
			{
				return Guid.Empty;
			}
			string text = null;
			try
			{
				string arg;
				using (DirectoryEntry directoryEntry = new DirectoryEntry(string.Format("LDAP://{0}RootDse", this.LdapHostName)))
				{
					arg = (string)directoryEntry.Properties["configurationNamingContext"].Value;
				}
				this.configNCUri = string.Format("LDAP://{0}{1}", this.LdapHostName, arg);
				using (DirectorySearcher directorySearcher = new DirectorySearcher(new DirectoryEntry(this.configNCUri)))
				{
					directorySearcher.Filter = string.Format("(objectClass=msExchOrganizationContainer)", new object[0]);
					directorySearcher.PropertiesToLoad.Clear();
					directorySearcher.PropertiesToLoad.Add("objectGUID");
					SearchResult searchResult = directorySearcher.FindOne();
					if (searchResult != null)
					{
						ResultPropertyValueCollection resultPropertyValueCollection = searchResult.Properties["objectGUID"];
						if (resultPropertyValueCollection != null && resultPropertyValueCollection.Count > 0)
						{
							this.organizationGuid = new Guid((byte[])resultPropertyValueCollection[0]);
						}
					}
				}
			}
			catch (COMException ex)
			{
				text = ex.Message;
			}
			catch (ActiveDirectoryObjectNotFoundException ex2)
			{
				text = ex2.Message;
			}
			catch (ActiveDirectoryOperationException ex3)
			{
				text = ex3.Message;
			}
			catch (ActiveDirectoryServerDownException ex4)
			{
				text = ex4.Message;
			}
			catch (FormatException ex5)
			{
				text = ex5.Message;
			}
			catch (OverflowException ex6)
			{
				text = ex6.Message;
			}
			if (!string.IsNullOrEmpty(text))
			{
				ExTraceGlobals.SqmTracer.TraceDebug<string>(46415, 0L, "Failed to get the organization GUID. Error: {0}", text);
			}
			return this.organizationGuid;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000030D0 File Offset: 0x000012D0
		private void SetOrganizationDataPoint()
		{
			Guid organization = this.GetOrganization();
			if (organization != Guid.Empty)
			{
				this.SetDataPoint(SqmDataID.COMMON_DW_ORGID, organization.GetHashCode());
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003109 File Offset: 0x00001309
		private void SetVersionDataPoints()
		{
			this.SetDataPoint(SqmDataID.CMN_DYN_MAJORBUILD, this.majorBuild.ToString());
			this.SetDataPoint(SqmDataID.CMN_DYN_MINORBUILD, this.minorBuild.ToString());
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003137 File Offset: 0x00001337
		private void SetDeploymentTypeDataPoint()
		{
			this.SetDataPoint(SqmDataID.CMN_DYN_EXDEPLOYMENTTYPE, Datacenter.GetExchangeSku().ToString());
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003153 File Offset: 0x00001353
		private void SetLabNameDataPoint()
		{
			this.SetDataPoint(SqmDataID.CMN_DYN_LABNAME, SqmSession.labName);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003165 File Offset: 0x00001365
		private void SetCommonDataPoints()
		{
			this.SetOrganizationDataPoint();
			this.SetVersionDataPoints();
			this.SetDeploymentTypeDataPoint();
			this.SetLabNameDataPoint();
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003180 File Offset: 0x00001380
		private void InternalSetDataPoint(SqmSession.SetDataPointDelegate setDataPoint)
		{
			try
			{
				this.dataWriteMutex.WaitOne();
				if (this.Enabled)
				{
					uint size = setDataPoint();
					this.AddSessionSize(size);
				}
			}
			finally
			{
				this.dataWriteMutex.ReleaseMutex();
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000031D0 File Offset: 0x000013D0
		private void AddSessionSize(uint size)
		{
			this.SessionSize += size;
			if (this.SessionSize >= this.MaximumApproachingLimit)
			{
				ExTraceGlobals.SqmTracer.TraceDebug(38223, 0L, "Session size exceeds MaximumApproachingLimit, uploading data and starting a new session.");
				this.UpdateData(true, false);
			}
		}

		// Token: 0x04000191 RID: 401
		private const uint DefaultMaximumDataFileSize = 60000U;

		// Token: 0x04000192 RID: 402
		private const uint DefaultMaximumApproachingLimit = 15000U;

		// Token: 0x04000193 RID: 403
		private const string SqmUploadUrl = "https://sqm.microsoft.com/sqm/exchange/sqmserver.dll";

		// Token: 0x04000194 RID: 404
		private const string DefaultSessionName = "Default";

		// Token: 0x04000195 RID: 405
		private static SqmLibWrap.SqmUploadCallback uploadCallback = new SqmLibWrap.SqmUploadCallback(SqmSession.UploadCallback);

		// Token: 0x04000196 RID: 406
		private static string labName = string.Empty;

		// Token: 0x04000197 RID: 407
		private SqmAppID sqmAppID;

		// Token: 0x04000198 RID: 408
		private uint sessionSize;

		// Token: 0x04000199 RID: 409
		private uint majorVersion;

		// Token: 0x0400019A RID: 410
		private uint minorVersion;

		// Token: 0x0400019B RID: 411
		private uint majorBuild;

		// Token: 0x0400019C RID: 412
		private uint minorBuild;

		// Token: 0x0400019D RID: 413
		private Mutex dataWriteMutex;

		// Token: 0x0400019E RID: 414
		private string physicalSessionName;

		// Token: 0x0400019F RID: 415
		private Guid organizationGuid = Guid.Empty;

		// Token: 0x040001A0 RID: 416
		private string ldapHostName;

		// Token: 0x040001A1 RID: 417
		private string configNCUri;

		// Token: 0x040001A2 RID: 418
		private string dataFileDirectory;

		// Token: 0x02000007 RID: 7
		// (Invoke) Token: 0x0600005B RID: 91
		private delegate uint SetDataPointDelegate();

		// Token: 0x02000008 RID: 8
		public enum Scope
		{
			// Token: 0x040001A6 RID: 422
			AppDomain,
			// Token: 0x040001A7 RID: 423
			Process,
			// Token: 0x040001A8 RID: 424
			System
		}
	}
}
