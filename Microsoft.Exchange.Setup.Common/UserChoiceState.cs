using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x02000071 RID: 113
	[ClassAccessLevel(AccessLevel.MSInternal)]
	public class UserChoiceState
	{
		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000508 RID: 1288 RVA: 0x00011D3C File Offset: 0x0000FF3C
		// (set) Token: 0x06000509 RID: 1289 RVA: 0x00011D44 File Offset: 0x0000FF44
		[XmlElement(IsNullable = true)]
		public string CurrentWizardPageName { get; set; }

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x0600050A RID: 1290 RVA: 0x00011D4D File Offset: 0x0000FF4D
		// (set) Token: 0x0600050B RID: 1291 RVA: 0x00011D55 File Offset: 0x0000FF55
		[XmlElement(IsNullable = true)]
		public string ExchangeVersionBeingInstalled { get; set; }

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x0600050C RID: 1292 RVA: 0x00011D5E File Offset: 0x0000FF5E
		// (set) Token: 0x0600050D RID: 1293 RVA: 0x00011D66 File Offset: 0x0000FF66
		public bool? CustomerFeedbackEnabled { get; set; }

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x0600050E RID: 1294 RVA: 0x00011D6F File Offset: 0x0000FF6F
		// (set) Token: 0x0600050F RID: 1295 RVA: 0x00011D77 File Offset: 0x0000FF77
		public bool ErrorReportingEnabled { get; set; }

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000510 RID: 1296 RVA: 0x00011D80 File Offset: 0x0000FF80
		// (set) Token: 0x06000511 RID: 1297 RVA: 0x00011D88 File Offset: 0x0000FF88
		public bool TypicalInstallation { get; set; }

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000512 RID: 1298 RVA: 0x00011D91 File Offset: 0x0000FF91
		// (set) Token: 0x06000513 RID: 1299 RVA: 0x00011D99 File Offset: 0x0000FF99
		public bool IsAdminToolsChecked { get; set; }

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000514 RID: 1300 RVA: 0x00011DA2 File Offset: 0x0000FFA2
		// (set) Token: 0x06000515 RID: 1301 RVA: 0x00011DAA File Offset: 0x0000FFAA
		public bool IsBridgeheadChecked { get; set; }

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000516 RID: 1302 RVA: 0x00011DB3 File Offset: 0x0000FFB3
		// (set) Token: 0x06000517 RID: 1303 RVA: 0x00011DBB File Offset: 0x0000FFBB
		public bool IsClientAccessChecked { get; set; }

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000518 RID: 1304 RVA: 0x00011DC4 File Offset: 0x0000FFC4
		// (set) Token: 0x06000519 RID: 1305 RVA: 0x00011DCC File Offset: 0x0000FFCC
		public bool IsGatewayChecked { get; set; }

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x0600051A RID: 1306 RVA: 0x00011DD5 File Offset: 0x0000FFD5
		// (set) Token: 0x0600051B RID: 1307 RVA: 0x00011DDD File Offset: 0x0000FFDD
		public bool IsMailboxChecked { get; set; }

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x0600051C RID: 1308 RVA: 0x00011DE6 File Offset: 0x0000FFE6
		// (set) Token: 0x0600051D RID: 1309 RVA: 0x00011DEE File Offset: 0x0000FFEE
		public bool IsUnifiedMessagingChecked { get; set; }

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x0600051E RID: 1310 RVA: 0x00011DF7 File Offset: 0x0000FFF7
		// (set) Token: 0x0600051F RID: 1311 RVA: 0x00011DFF File Offset: 0x0000FFFF
		public bool IsFrontendTransportChecked { get; set; }

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000520 RID: 1312 RVA: 0x00011E08 File Offset: 0x00010008
		// (set) Token: 0x06000521 RID: 1313 RVA: 0x00011E10 File Offset: 0x00010010
		public bool IsCentralAdminChecked { get; set; }

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000522 RID: 1314 RVA: 0x00011E19 File Offset: 0x00010019
		// (set) Token: 0x06000523 RID: 1315 RVA: 0x00011E21 File Offset: 0x00010021
		public bool IsCentralAdminDatabaseChecked { get; set; }

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000524 RID: 1316 RVA: 0x00011E2A File Offset: 0x0001002A
		// (set) Token: 0x06000525 RID: 1317 RVA: 0x00011E32 File Offset: 0x00010032
		public bool IsMonitoringChecked { get; set; }

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000526 RID: 1318 RVA: 0x00011E3B File Offset: 0x0001003B
		// (set) Token: 0x06000527 RID: 1319 RVA: 0x00011E43 File Offset: 0x00010043
		public bool IsLanguagePacksChecked { get; set; }

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000528 RID: 1320 RVA: 0x00011E4C File Offset: 0x0001004C
		// (set) Token: 0x06000529 RID: 1321 RVA: 0x00011E54 File Offset: 0x00010054
		public bool IsCafeChecked { get; set; }

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x0600052A RID: 1322 RVA: 0x00011E5D File Offset: 0x0001005D
		// (set) Token: 0x0600052B RID: 1323 RVA: 0x00011E65 File Offset: 0x00010065
		public bool IsOSPChecked { get; set; }

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x0600052C RID: 1324 RVA: 0x00011E6E File Offset: 0x0001006E
		// (set) Token: 0x0600052D RID: 1325 RVA: 0x00011E76 File Offset: 0x00010076
		[XmlElement(IsNullable = true)]
		public string ProgramFilesPath { get; set; }

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x0600052E RID: 1326 RVA: 0x00011E7F File Offset: 0x0001007F
		// (set) Token: 0x0600052F RID: 1327 RVA: 0x00011E87 File Offset: 0x00010087
		[XmlElement(IsNullable = true)]
		public string OrganizationName { get; set; }

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000530 RID: 1328 RVA: 0x00011E90 File Offset: 0x00010090
		// (set) Token: 0x06000531 RID: 1329 RVA: 0x00011E98 File Offset: 0x00010098
		[XmlElement(IsNullable = true)]
		public string ExternalCASServerDomain { get; set; }

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000532 RID: 1330 RVA: 0x00011EA1 File Offset: 0x000100A1
		// (set) Token: 0x06000533 RID: 1331 RVA: 0x00011EA9 File Offset: 0x000100A9
		public bool? ActiveDirectorySplitPermissions { get; set; }

		// Token: 0x06000534 RID: 1332 RVA: 0x00011EB4 File Offset: 0x000100B4
		public static void DeleteFile()
		{
			try
			{
				File.Delete(Path.Combine(ConfigurationContext.Setup.SetupLoggingPath, UserChoiceState.stateFileName));
			}
			catch (IOException e)
			{
				SetupLogger.Log(Strings.ExceptionWhenDeserializingStateFile(e));
			}
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x00011EF8 File Offset: 0x000100F8
		public void SaveToFile()
		{
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(UserChoiceState));
			TextWriter textWriter = new StreamWriter(Path.Combine(ConfigurationContext.Setup.SetupLoggingPath, UserChoiceState.stateFileName));
			xmlSerializer.Serialize(textWriter, this);
			textWriter.Close();
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x00011F38 File Offset: 0x00010138
		public static UserChoiceState LoadFromFile()
		{
			try
			{
				string text = File.ReadAllText(Path.Combine(ConfigurationContext.Setup.SetupLoggingPath, UserChoiceState.stateFileName));
				SetupLogger.Log(Strings.DeserializedStateXML(text));
				using (XmlReader xmlReader = XmlReader.Create(new StringReader(text)))
				{
					XmlSerializer xmlSerializer = new XmlSerializer(typeof(UserChoiceState));
					if (xmlSerializer.CanDeserialize(xmlReader))
					{
						return (UserChoiceState)xmlSerializer.Deserialize(xmlReader);
					}
					SetupLogger.Log(Strings.CouldNotDeserializeStateFile);
				}
			}
			catch (IOException e)
			{
				SetupLogger.Log(Strings.ExceptionWhenDeserializingStateFile(e));
			}
			catch (XmlException e2)
			{
				SetupLogger.Log(Strings.ExceptionWhenDeserializingStateFile(e2));
			}
			catch (InvalidOperationException e3)
			{
				SetupLogger.Log(Strings.ExceptionWhenDeserializingStateFile(e3));
			}
			return null;
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x0001201C File Offset: 0x0001021C
		internal void WriteToContext(ISetupContext setupContext, ModeDataHandler modeDataHandler)
		{
			if (this.ExchangeVersionBeingInstalled == null || !this.ExchangeVersionBeingInstalled.Equals(setupContext.RunningVersion.ToString()))
			{
				SetupLogger.Log(Strings.StateFileVersionMismatch(this.ExchangeVersionBeingInstalled, setupContext.RunningVersion.ToString()));
				return;
			}
			setupContext.IsRestoredFromPreviousState = true;
			setupContext.CurrentWizardPageName = this.CurrentWizardPageName;
			if (modeDataHandler is InstallModeDataHandler)
			{
				InstallModeDataHandler installModeDataHandler = modeDataHandler as InstallModeDataHandler;
				installModeDataHandler.CustomerFeedbackEnabled = this.CustomerFeedbackEnabled;
				if (setupContext.IsCleanMachine)
				{
					installModeDataHandler.TypicalInstallation = this.TypicalInstallation;
				}
			}
			setupContext.WatsonEnabled = this.ErrorReportingEnabled;
			modeDataHandler.IsAdminToolsChecked = this.IsAdminToolsChecked;
			modeDataHandler.IsBridgeheadChecked = this.IsBridgeheadChecked;
			modeDataHandler.IsClientAccessChecked = this.IsClientAccessChecked;
			modeDataHandler.IsGatewayChecked = this.IsGatewayChecked;
			modeDataHandler.IsMailboxChecked = this.IsMailboxChecked;
			modeDataHandler.IsUnifiedMessagingChecked = this.IsUnifiedMessagingChecked;
			modeDataHandler.IsFrontendTransportChecked = this.IsFrontendTransportChecked;
			modeDataHandler.IsCentralAdminChecked = this.IsCentralAdminChecked;
			modeDataHandler.IsCentralAdminDatabaseChecked = this.IsCentralAdminDatabaseChecked;
			modeDataHandler.IsMonitoringChecked = this.IsMonitoringChecked;
			modeDataHandler.IsLanguagePacksChecked = this.IsLanguagePacksChecked;
			modeDataHandler.IsCafeChecked = this.IsCafeChecked;
			modeDataHandler.IsOSPChecked = this.IsOSPChecked;
			setupContext.ActiveDirectorySplitPermissions = this.ActiveDirectorySplitPermissions;
			if (this.ProgramFilesPath != null)
			{
				setupContext.TargetDir = NonRootLocalLongFullPath.Parse(this.ProgramFilesPath);
			}
			if (this.OrganizationName != null)
			{
				setupContext.OrganizationName = new OrganizationName(this.OrganizationName);
			}
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x00012190 File Offset: 0x00010390
		internal void ReadFromContext(ISetupContext setupContext, ModeDataHandler modeDataHandler)
		{
			this.ExchangeVersionBeingInstalled = ((setupContext.RunningVersion == null) ? null : setupContext.RunningVersion.ToString());
			this.CurrentWizardPageName = setupContext.CurrentWizardPageName;
			if (modeDataHandler is InstallModeDataHandler)
			{
				InstallModeDataHandler installModeDataHandler = modeDataHandler as InstallModeDataHandler;
				this.CustomerFeedbackEnabled = installModeDataHandler.CustomerFeedbackEnabled;
				this.TypicalInstallation = installModeDataHandler.TypicalInstallation;
			}
			this.ErrorReportingEnabled = setupContext.WatsonEnabled;
			this.IsAdminToolsChecked = modeDataHandler.IsAdminToolsChecked;
			this.IsBridgeheadChecked = modeDataHandler.IsBridgeheadChecked;
			this.IsClientAccessChecked = modeDataHandler.IsClientAccessChecked;
			this.IsGatewayChecked = modeDataHandler.IsGatewayChecked;
			this.IsMailboxChecked = modeDataHandler.IsMailboxChecked;
			this.IsUnifiedMessagingChecked = modeDataHandler.IsUnifiedMessagingChecked;
			this.IsFrontendTransportChecked = modeDataHandler.IsFrontendTransportChecked;
			this.IsCentralAdminChecked = modeDataHandler.IsCentralAdminChecked;
			this.IsCentralAdminDatabaseChecked = modeDataHandler.IsCentralAdminDatabaseChecked;
			this.IsMonitoringChecked = modeDataHandler.IsMonitoringChecked;
			this.IsLanguagePacksChecked = modeDataHandler.IsLanguagePacksChecked;
			this.IsCafeChecked = modeDataHandler.IsCafeChecked;
			this.IsOSPChecked = modeDataHandler.IsOSPChecked;
			this.ActiveDirectorySplitPermissions = setupContext.ActiveDirectorySplitPermissions;
			this.ProgramFilesPath = ((setupContext.TargetDir == null) ? null : setupContext.TargetDir.ToString());
			this.OrganizationName = ((setupContext.OrganizationName == null) ? null : setupContext.OrganizationName.UnescapedName);
		}

		// Token: 0x040001A6 RID: 422
		private static readonly string stateFileName = "exchangeInstallState.xml";
	}
}
