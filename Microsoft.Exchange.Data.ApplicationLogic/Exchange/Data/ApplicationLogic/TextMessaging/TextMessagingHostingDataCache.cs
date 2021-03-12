using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.ApplicationLogic.TextMessaging.HostingData.Site;
using Microsoft.Exchange.Data.ApplicationLogic.TextMessaging.HostingData.System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Xml.Serialization.TextMessagingHostingData;

namespace Microsoft.Exchange.Data.ApplicationLogic.TextMessaging
{
	// Token: 0x020001B4 RID: 436
	internal class TextMessagingHostingDataCache : DisposeTrackableBase
	{
		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x060010B0 RID: 4272 RVA: 0x00044A80 File Offset: 0x00042C80
		public static TextMessagingHostingDataCache Instance
		{
			get
			{
				return TextMessagingHostingDataCache.instance;
			}
		}

		// Token: 0x060010B1 RID: 4273 RVA: 0x00044A88 File Offset: 0x00042C88
		static TextMessagingHostingDataCache()
		{
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				TextMessagingHostingDataCache.processName = currentProcess.MainModule.FileName;
				TextMessagingHostingDataCache.processId = currentProcess.Id;
			}
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x00044BCC File Offset: 0x00042DCC
		public void Initialize()
		{
			base.CheckDisposed();
			lock (this)
			{
				if (!this.Initialized)
				{
					FileSystemEventHandler value = delegate(object sender, FileSystemEventArgs e)
					{
						if (string.Equals("TextMessagingHostingData-System.xml", e.Name, StringComparison.OrdinalIgnoreCase) || string.Equals("TextMessagingHostingData-Site.xml", e.Name, StringComparison.OrdinalIgnoreCase))
						{
							this.LoadFromFiles();
						}
					};
					RenamedEventHandler value2 = delegate(object sender, RenamedEventArgs e)
					{
						if (string.Equals("TextMessagingHostingData-System.xml", e.Name, StringComparison.OrdinalIgnoreCase) || string.Equals("TextMessagingHostingData-Site.xml", e.Name, StringComparison.OrdinalIgnoreCase) || string.Equals("TextMessagingHostingData-System.xml", e.OldName, StringComparison.OrdinalIgnoreCase) || string.Equals("TextMessagingHostingData-Site.xml", e.OldName, StringComparison.OrdinalIgnoreCase))
						{
							this.LoadFromFiles();
						}
					};
					Exception ex = null;
					try
					{
						this.fileWatcher = new FileSystemWatcher(TextMessagingHostingDataCache.ExecutingAssemblyLocation, "TextMessagingHostingData-*.xml");
						this.fileWatcher.Changed += value;
						this.fileWatcher.Created += value;
						this.fileWatcher.Deleted += value;
						this.fileWatcher.Renamed += value2;
						this.fileWatcher.EnableRaisingEvents = true;
					}
					catch (ArgumentException ex2)
					{
						ex = ex2;
					}
					catch (FileNotFoundException ex3)
					{
						ex = ex3;
					}
					finally
					{
						if (ex != null)
						{
							TextMessagingHostingDataCache.EventLog.LogEvent(ApplicationLogicEventLogConstants.Tuple_MonitorHostingDataFileFailure, null, new object[]
							{
								TextMessagingHostingDataCache.ExecutingAssemblyLocation,
								ex
							});
						}
					}
					this.LoadFromFiles();
					this.Initialized = true;
				}
			}
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x060010B3 RID: 4275 RVA: 0x00044D0C File Offset: 0x00042F0C
		// (set) Token: 0x060010B4 RID: 4276 RVA: 0x00044D14 File Offset: 0x00042F14
		private bool Initialized { get; set; }

		// Token: 0x14000066 RID: 102
		// (add) Token: 0x060010B5 RID: 4277 RVA: 0x00044D20 File Offset: 0x00042F20
		// (remove) Token: 0x060010B6 RID: 4278 RVA: 0x00044D58 File Offset: 0x00042F58
		public event TextMessagingHostingDataChanged Changed;

		// Token: 0x060010B7 RID: 4279 RVA: 0x00044D90 File Offset: 0x00042F90
		public Microsoft.Exchange.Data.ApplicationLogic.TextMessaging.TextMessagingHostingData GetHostingData()
		{
			if (!this.Initialized)
			{
				this.Initialize();
			}
			Microsoft.Exchange.Data.ApplicationLogic.TextMessaging.TextMessagingHostingData result;
			try
			{
				this.rwLock.EnterReadLock();
				result = this.data;
			}
			finally
			{
				try
				{
					this.rwLock.ExitReadLock();
				}
				catch (SynchronizationLockException)
				{
				}
			}
			return result;
		}

		// Token: 0x060010B8 RID: 4280 RVA: 0x00044DF0 File Offset: 0x00042FF0
		public TextMessagingHostingDataRegionsRegion GetRegion(string iso2)
		{
			Microsoft.Exchange.Data.ApplicationLogic.TextMessaging.TextMessagingHostingData hostingData = this.GetHostingData();
			if (hostingData == null || hostingData.Regions == null || hostingData.Regions.Region == null || hostingData.Regions.Region.Length == 0)
			{
				return null;
			}
			foreach (TextMessagingHostingDataRegionsRegion textMessagingHostingDataRegionsRegion in hostingData.Regions.Region)
			{
				if (string.Equals(textMessagingHostingDataRegionsRegion.Iso2, iso2, StringComparison.OrdinalIgnoreCase))
				{
					return textMessagingHostingDataRegionsRegion;
				}
			}
			return null;
		}

		// Token: 0x060010B9 RID: 4281 RVA: 0x00044E68 File Offset: 0x00043068
		public TextMessagingHostingDataCarriersCarrier GetCarrier(string id)
		{
			Microsoft.Exchange.Data.ApplicationLogic.TextMessaging.TextMessagingHostingData hostingData = this.GetHostingData();
			if (hostingData == null || hostingData.Carriers == null || hostingData.Carriers.Carrier == null || hostingData.Carriers.Carrier.Length == 0)
			{
				return null;
			}
			int num = -1;
			if (!int.TryParse(id, out num))
			{
				return null;
			}
			foreach (TextMessagingHostingDataCarriersCarrier textMessagingHostingDataCarriersCarrier in hostingData.Carriers.Carrier)
			{
				if (textMessagingHostingDataCarriersCarrier.Identity == num)
				{
					return textMessagingHostingDataCarriersCarrier;
				}
			}
			return null;
		}

		// Token: 0x060010BA RID: 4282 RVA: 0x00044EEC File Offset: 0x000430EC
		public TextMessagingHostingDataServicesService GetService(string regionIso2, string carrierId, TextMessagingHostingDataServicesServiceType type)
		{
			Microsoft.Exchange.Data.ApplicationLogic.TextMessaging.TextMessagingHostingData hostingData = this.GetHostingData();
			if (hostingData == null || hostingData.Services == null || hostingData.Services.Service == null || hostingData.Services.Service.Length == 0)
			{
				return null;
			}
			int num = -1;
			if (!int.TryParse(carrierId, out num))
			{
				return null;
			}
			foreach (TextMessagingHostingDataServicesService textMessagingHostingDataServicesService in hostingData.Services.Service)
			{
				if (string.Equals(textMessagingHostingDataServicesService.RegionIso2, regionIso2, StringComparison.OrdinalIgnoreCase) && textMessagingHostingDataServicesService.CarrierIdentity == num && textMessagingHostingDataServicesService.Type == type)
				{
					switch (textMessagingHostingDataServicesService.Type)
					{
					case TextMessagingHostingDataServicesServiceType.SmtpToSmsGateway:
						if (textMessagingHostingDataServicesService.SmtpToSmsGateway != null && textMessagingHostingDataServicesService.SmtpToSmsGateway.MessageRendering != null && textMessagingHostingDataServicesService.SmtpToSmsGateway.MessageRendering.Capacity != null && 0 < textMessagingHostingDataServicesService.SmtpToSmsGateway.MessageRendering.Capacity.Length && textMessagingHostingDataServicesService.SmtpToSmsGateway.RecipientAddressing != null && !string.IsNullOrEmpty(textMessagingHostingDataServicesService.SmtpToSmsGateway.RecipientAddressing.SmtpAddress))
						{
							return textMessagingHostingDataServicesService;
						}
						goto IL_104;
					}
					return textMessagingHostingDataServicesService;
				}
				IL_104:;
			}
			return null;
		}

		// Token: 0x060010BB RID: 4283 RVA: 0x00045011 File Offset: 0x00043211
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<TextMessagingHostingDataCache>(this);
		}

		// Token: 0x060010BC RID: 4284 RVA: 0x00045019 File Offset: 0x00043219
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				if (this.fileWatcher != null)
				{
					this.fileWatcher.Dispose();
					this.fileWatcher = null;
				}
				if (this.rwLock != null)
				{
					this.rwLock.Dispose();
					this.rwLock = null;
				}
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x060010BD RID: 4285 RVA: 0x00045058 File Offset: 0x00043258
		private void LoadFromFiles()
		{
			TimeSpan timeout = TimeSpan.FromSeconds(1.0);
			Microsoft.Exchange.Data.ApplicationLogic.TextMessaging.HostingData.Site.TextMessagingHostingData textMessagingHostingData = new Microsoft.Exchange.Data.ApplicationLogic.TextMessaging.HostingData.Site.TextMessagingHostingData();
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
			using (Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("TextMessagingHostingData-Site.xsd"))
			{
				xmlReaderSettings.ValidationType = ValidationType.Schema;
				xmlReaderSettings.ValidationFlags = (XmlSchemaValidationFlags.ProcessInlineSchema | XmlSchemaValidationFlags.ProcessSchemaLocation | XmlSchemaValidationFlags.ReportValidationWarnings | XmlSchemaValidationFlags.ProcessIdentityConstraints | XmlSchemaValidationFlags.AllowXmlAttributes);
				xmlReaderSettings.Schemas.Add(XmlSchema.Read(manifestResourceStream, null));
			}
			int num = 0;
			while (2 > num)
			{
				try
				{
					using (XmlReader xmlReader = XmlReader.Create(TextMessagingHostingDataCache.SiteHostingDataFilePath, xmlReaderSettings))
					{
						textMessagingHostingData.ReadXml(xmlReader);
					}
					break;
				}
				catch (FileNotFoundException)
				{
					break;
				}
				catch (SystemException ex)
				{
					if (!(ex is IOException) || 1 <= num)
					{
						this.IssueRedEvent(ex, TextMessagingHostingDataCache.SiteHostingDataFilePath);
						textMessagingHostingData = null;
						break;
					}
					Thread.Sleep(timeout);
				}
				num++;
			}
			XmlReaderSettings xmlReaderSettings2 = new XmlReaderSettings();
			using (Stream manifestResourceStream2 = Assembly.GetExecutingAssembly().GetManifestResourceStream("TextMessagingHostingData-System.xsd"))
			{
				xmlReaderSettings2.ValidationType = ValidationType.Schema;
				xmlReaderSettings2.ValidationFlags = (XmlSchemaValidationFlags.ProcessInlineSchema | XmlSchemaValidationFlags.ProcessSchemaLocation | XmlSchemaValidationFlags.ReportValidationWarnings | XmlSchemaValidationFlags.ProcessIdentityConstraints | XmlSchemaValidationFlags.AllowXmlAttributes);
				xmlReaderSettings2.Schemas.Add(XmlSchema.Read(manifestResourceStream2, null));
			}
			int num2 = 0;
			while (2 > num2)
			{
				try
				{
					Microsoft.Exchange.Data.ApplicationLogic.TextMessaging.HostingData.System.TextMessagingHostingData textMessagingHostingData2 = new Microsoft.Exchange.Data.ApplicationLogic.TextMessaging.HostingData.System.TextMessagingHostingData();
					Microsoft.Exchange.Data.ApplicationLogic.TextMessaging.HostingData.System.TextMessagingHostingData textMessagingHostingData3 = new Microsoft.Exchange.Data.ApplicationLogic.TextMessaging.HostingData.System.TextMessagingHostingData();
					using (FileStream fileStream = new FileStream(TextMessagingHostingDataCache.SystemHostingDataFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
					{
						using (XmlReader xmlReader2 = XmlReader.Create(fileStream, xmlReaderSettings2))
						{
							textMessagingHostingData2.ReadXml(xmlReader2);
						}
						textMessagingHostingData2.AcceptChanges();
						if (textMessagingHostingData != null)
						{
							fileStream.Seek(0L, SeekOrigin.Begin);
							using (XmlReader xmlReader3 = XmlReader.Create(fileStream, xmlReaderSettings2))
							{
								textMessagingHostingData3.ReadXml(xmlReader3);
							}
						}
						textMessagingHostingData3.AcceptChanges();
					}
					if (textMessagingHostingData != null)
					{
						try
						{
							textMessagingHostingData2.Merge(textMessagingHostingData);
							textMessagingHostingData2.AcceptChanges();
							this.IssueGreenEvent();
						}
						catch (ConstraintException e)
						{
							this.IssueRedEvent(e, TextMessagingHostingDataCache.SiteHostingDataFilePath);
							textMessagingHostingData2 = textMessagingHostingData3;
						}
					}
					this.AssignHostingData(TextMessagingHostingDataCache.Convert(textMessagingHostingData2));
					break;
				}
				catch (SystemException ex2)
				{
					if (!(ex2 is IOException) || 1 <= num2)
					{
						this.IssueRedEvent(ex2, TextMessagingHostingDataCache.SystemHostingDataFilePath);
						this.AssignHostingData(null);
						break;
					}
					Thread.Sleep(timeout);
				}
				num2++;
			}
			if (this.Changed != null)
			{
				this.Changed(this.data);
			}
		}

		// Token: 0x060010BE RID: 4286 RVA: 0x00045310 File Offset: 0x00043510
		private void IssueRedEvent(SystemException e, string path)
		{
			TextMessagingHostingDataCache.EventLog.LogEvent(ApplicationLogicEventLogConstants.Tuple_LoadHostingDataFileFailure, null, new object[]
			{
				TextMessagingHostingDataCache.processName,
				TextMessagingHostingDataCache.processId,
				path,
				e
			});
		}

		// Token: 0x060010BF RID: 4287 RVA: 0x00045354 File Offset: 0x00043554
		private void IssueGreenEvent()
		{
			TextMessagingHostingDataCache.EventLog.LogEvent(ApplicationLogicEventLogConstants.Tuple_LoadHostingDataFilesSuccess, null, new object[]
			{
				TextMessagingHostingDataCache.processName,
				TextMessagingHostingDataCache.processId
			});
		}

		// Token: 0x060010C0 RID: 4288 RVA: 0x00045390 File Offset: 0x00043590
		private void AssignHostingData(Microsoft.Exchange.Data.ApplicationLogic.TextMessaging.TextMessagingHostingData data)
		{
			if (data == null)
			{
				data = new Microsoft.Exchange.Data.ApplicationLogic.TextMessaging.TextMessagingHostingData();
			}
			TextMessagingHostingDataCache.SetNullFieldsEmpty(data);
			try
			{
				this.rwLock.EnterWriteLock();
				this.data = data;
			}
			finally
			{
				try
				{
					this.rwLock.ExitWriteLock();
				}
				catch (SynchronizationLockException)
				{
				}
			}
		}

		// Token: 0x060010C1 RID: 4289 RVA: 0x000453F0 File Offset: 0x000435F0
		private static void SetNullFieldsEmpty(Microsoft.Exchange.Data.ApplicationLogic.TextMessaging.TextMessagingHostingData data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (data.Regions == null)
			{
				data.Regions = new TextMessagingHostingDataRegions();
			}
			if (data.Regions.Region == null)
			{
				data.Regions.Region = new TextMessagingHostingDataRegionsRegion[0];
			}
			if (data.Carriers == null)
			{
				data.Carriers = new TextMessagingHostingDataCarriers();
			}
			if (data.Carriers.Carrier == null)
			{
				data.Carriers.Carrier = new TextMessagingHostingDataCarriersCarrier[0];
			}
			if (data.Services == null)
			{
				data.Services = new TextMessagingHostingDataServices();
			}
			if (data.Services.Service == null)
			{
				data.Services.Service = new TextMessagingHostingDataServicesService[0];
			}
		}

		// Token: 0x060010C2 RID: 4290 RVA: 0x000454A0 File Offset: 0x000436A0
		private static Microsoft.Exchange.Data.ApplicationLogic.TextMessaging.TextMessagingHostingData Convert(Microsoft.Exchange.Data.ApplicationLogic.TextMessaging.HostingData.System.TextMessagingHostingData dataSet)
		{
			if (dataSet == null)
			{
				throw new ArgumentNullException("dataSet");
			}
			Microsoft.Exchange.Data.ApplicationLogic.TextMessaging.TextMessagingHostingData result = null;
			XmlSerializer xmlSerializer = new TextMessagingHostingDataSerializer();
			using (MemoryStream memoryStream = new MemoryStream())
			{
				dataSet.WriteXml(memoryStream);
				memoryStream.Seek(0L, SeekOrigin.Begin);
				result = (Microsoft.Exchange.Data.ApplicationLogic.TextMessaging.TextMessagingHostingData)xmlSerializer.Deserialize(memoryStream);
			}
			return result;
		}

		// Token: 0x040008D4 RID: 2260
		private const string SystemXmlSchema = "TextMessagingHostingData-System.xsd";

		// Token: 0x040008D5 RID: 2261
		private const string SiteXmlSchema = "TextMessagingHostingData-Site.xsd";

		// Token: 0x040008D6 RID: 2262
		private const string SystemHostingDataFileName = "TextMessagingHostingData-System.xml";

		// Token: 0x040008D7 RID: 2263
		private const string SiteHostingDataFileName = "TextMessagingHostingData-Site.xml";

		// Token: 0x040008D8 RID: 2264
		private const string HostingDataFileNamePattern = "TextMessagingHostingData-*.xml";

		// Token: 0x040008D9 RID: 2265
		private static TextMessagingHostingDataCache instance = new TextMessagingHostingDataCache();

		// Token: 0x040008DA RID: 2266
		private static readonly string ExecutingAssemblyLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

		// Token: 0x040008DB RID: 2267
		private static readonly string SystemHostingDataFilePath = Path.Combine(TextMessagingHostingDataCache.ExecutingAssemblyLocation, "TextMessagingHostingData-System.xml");

		// Token: 0x040008DC RID: 2268
		private static readonly string SiteHostingDataFilePath = Path.Combine(TextMessagingHostingDataCache.ExecutingAssemblyLocation, "TextMessagingHostingData-Site.xml");

		// Token: 0x040008DD RID: 2269
		private static Guid EventLogComponentGuid = new Guid("{88861F2E-22BA-4a1e-8B48-02C180528957}");

		// Token: 0x040008DE RID: 2270
		public static readonly ExEventLog EventLog = new ExEventLog(TextMessagingHostingDataCache.EventLogComponentGuid, "MSExchangeApplicationLogic");

		// Token: 0x040008DF RID: 2271
		private FileSystemWatcher fileWatcher;

		// Token: 0x040008E0 RID: 2272
		private ReaderWriterLockSlim rwLock = new ReaderWriterLockSlim();

		// Token: 0x040008E1 RID: 2273
		private Microsoft.Exchange.Data.ApplicationLogic.TextMessaging.TextMessagingHostingData data;

		// Token: 0x040008E2 RID: 2274
		private static readonly string processName;

		// Token: 0x040008E3 RID: 2275
		private static readonly int processId;
	}
}
