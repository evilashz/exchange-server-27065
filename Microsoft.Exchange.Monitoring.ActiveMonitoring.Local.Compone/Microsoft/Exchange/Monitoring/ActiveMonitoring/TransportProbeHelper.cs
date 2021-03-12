using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Xml;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.GlobalLocatorService;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics.Components.ForefrontActiveMonitoring;
using Microsoft.Exchange.Hygiene.Data;
using Microsoft.Exchange.Hygiene.Data.Directory;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Transport;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring
{
	// Token: 0x02000092 RID: 146
	internal class TransportProbeHelper : ProbeDefinitionHelper
	{
		// Token: 0x17000136 RID: 310
		// (get) Token: 0x0600051E RID: 1310 RVA: 0x0001E7CD File Offset: 0x0001C9CD
		private XmlNode ExtensionNode
		{
			get
			{
				return this.extensionNode;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x0600051F RID: 1311 RVA: 0x0001E7D5 File Offset: 0x0001C9D5
		private XmlNode MailFrom
		{
			get
			{
				return this.mailFrom;
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000520 RID: 1312 RVA: 0x0001E7DD File Offset: 0x0001C9DD
		private XmlNode MailTo
		{
			get
			{
				return this.mailTo;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000521 RID: 1313 RVA: 0x0001E7E5 File Offset: 0x0001C9E5
		private XmlNode AuthenticationAccount
		{
			get
			{
				return this.authenticationAccount;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000522 RID: 1314 RVA: 0x0001E7ED File Offset: 0x0001C9ED
		private XmlNode CheckMail
		{
			get
			{
				return this.checkMail;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000523 RID: 1315 RVA: 0x0001E7F5 File Offset: 0x0001C9F5
		private bool PopulateMailFrom
		{
			get
			{
				return this.populateMailFrom;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000524 RID: 1316 RVA: 0x0001E7FD File Offset: 0x0001C9FD
		private bool PopulateMailTo
		{
			get
			{
				return this.populateMailTo;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000525 RID: 1317 RVA: 0x0001E805 File Offset: 0x0001CA05
		private bool PopulateBoth
		{
			get
			{
				return this.PopulateMailFrom && this.PopulateMailTo;
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000526 RID: 1318 RVA: 0x0001E817 File Offset: 0x0001CA17
		private bool PopulateSenderPassword
		{
			get
			{
				return this.populateSenderPassword;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000527 RID: 1319 RVA: 0x0001E81F File Offset: 0x0001CA1F
		private bool PopulateRecipientPassword
		{
			get
			{
				return this.populateRecipientPassword;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000528 RID: 1320 RVA: 0x0001E827 File Offset: 0x0001CA27
		private bool PopulateCheckMailCredential
		{
			get
			{
				return this.populateCheckMailCredential;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000529 RID: 1321 RVA: 0x0001E82F File Offset: 0x0001CA2F
		private bool PopulateAuthenticationAccount
		{
			get
			{
				return this.populateAuthenticationAccount;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x0600052A RID: 1322 RVA: 0x0001E837 File Offset: 0x0001CA37
		private TransportProbeHelper.MonitoringMailboxType MonitoringMailbox
		{
			get
			{
				return this.monitoringMailbox;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x0600052B RID: 1323 RVA: 0x0001E83F File Offset: 0x0001CA3F
		private bool CafeMailboxIsNeeded
		{
			get
			{
				return this.MonitoringMailbox == TransportProbeHelper.MonitoringMailboxType.Cafe || this.MonitoringMailbox == TransportProbeHelper.MonitoringMailboxType.Both || (this.MonitoringMailbox == TransportProbeHelper.MonitoringMailboxType.Either && DiscoveryContext.ExchangeInstalledRoles["Cafe"]);
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x0600052C RID: 1324 RVA: 0x0001E86F File Offset: 0x0001CA6F
		private bool BackendMailboxIsNeeded
		{
			get
			{
				return this.MonitoringMailbox == TransportProbeHelper.MonitoringMailboxType.Backend || this.MonitoringMailbox == TransportProbeHelper.MonitoringMailboxType.Both || (this.MonitoringMailbox == TransportProbeHelper.MonitoringMailboxType.Either && DiscoveryContext.ExchangeInstalledRoles["Mailbox"]);
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x0600052D RID: 1325 RVA: 0x0001E89F File Offset: 0x0001CA9F
		private TransportProbeHelper.SenderRecipientMatchType MatchType
		{
			get
			{
				return this.matchType;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x0600052E RID: 1326 RVA: 0x0001E8A7 File Offset: 0x0001CAA7
		private string SenderFeatureTag
		{
			get
			{
				return this.senderFeatureTag;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x0600052F RID: 1327 RVA: 0x0001E8AF File Offset: 0x0001CAAF
		private string RecipientFeatureTag
		{
			get
			{
				return this.recipientFeatureTag;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000530 RID: 1328 RVA: 0x0001E8B8 File Offset: 0x0001CAB8
		private ProbeOrganizationInfo SenderOrganizationInfo
		{
			get
			{
				if (this.senderOrganizationInfo == null)
				{
					if (string.IsNullOrWhiteSpace(this.SenderFeatureTag))
					{
						throw new XmlException("The required attribute SenderFeatureTag is missing.");
					}
					GlobalConfigSession globalConfigSession = new GlobalConfigSession();
					IEnumerable<ProbeOrganizationInfo> probeOrganizations = globalConfigSession.GetProbeOrganizations(this.SenderFeatureTag);
					if (probeOrganizations == null || probeOrganizations.Count<ProbeOrganizationInfo>() == 0)
					{
						throw new InvalidOperationException("Cannot find any test tenant with sender feature tag=" + this.SenderFeatureTag + ".");
					}
					this.senderOrganizationInfo = this.GetTenantOrg(probeOrganizations);
					if (this.senderOrganizationInfo == null)
					{
						throw new InvalidOperationException("Cannot find a test tenant with sender feature tag=" + this.SenderFeatureTag + ".");
					}
					this.senderTenantID = this.senderOrganizationInfo.ProbeOrganizationId.ObjectGuid;
					if (this.senderTenantID == Guid.Empty)
					{
						string message = "Failed to get TenantId using SenderFeatureTag.";
						WTFDiagnostics.TraceDebug(ExTraceGlobals.GenericHelperTracer, base.TraceContext, message, null, "SenderOrganizationInfo", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\GenericWorkItemHelper\\TransportProbeHelper.cs", 453);
						throw new InvalidOperationException(message);
					}
				}
				return this.senderOrganizationInfo;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000531 RID: 1329 RVA: 0x0001E9B0 File Offset: 0x0001CBB0
		private ProbeOrganizationInfo RecipientOrganizationInfo
		{
			get
			{
				if (this.recipientOrganizationInfo == null)
				{
					if (string.IsNullOrWhiteSpace(this.RecipientFeatureTag))
					{
						throw new XmlException("The required attribute RecipientFeatureTag is missing.");
					}
					GlobalConfigSession globalConfigSession = new GlobalConfigSession();
					IEnumerable<ProbeOrganizationInfo> probeOrganizations = globalConfigSession.GetProbeOrganizations(this.RecipientFeatureTag);
					if (probeOrganizations == null || probeOrganizations.Count<ProbeOrganizationInfo>() == 0)
					{
						throw new InvalidOperationException("Cannot find any test tenant with recipient feature tag=" + this.RecipientFeatureTag + ".");
					}
					this.recipientOrganizationInfo = this.GetTenantOrg(probeOrganizations);
					if (this.recipientOrganizationInfo == null)
					{
						throw new InvalidOperationException("Cannot find a test tenant with recipient feature tag=" + this.RecipientFeatureTag + ".");
					}
					this.recipientTenantID = this.recipientOrganizationInfo.ProbeOrganizationId.ObjectGuid;
					if (this.recipientTenantID == Guid.Empty)
					{
						string message = "Failed to get TenantId using RecipientFeatureTag.";
						WTFDiagnostics.TraceDebug(ExTraceGlobals.GenericHelperTracer, base.TraceContext, message, null, "RecipientOrganizationInfo", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\GenericWorkItemHelper\\TransportProbeHelper.cs", 500);
						throw new InvalidOperationException(message);
					}
				}
				return this.recipientOrganizationInfo;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000532 RID: 1330 RVA: 0x0001EAA5 File Offset: 0x0001CCA5
		private Guid SenderTenantID
		{
			get
			{
				if (this.senderTenantID == Guid.Empty && this.SenderOrganizationInfo != null)
				{
					this.senderTenantID = this.SenderOrganizationInfo.ProbeOrganizationId.ObjectGuid;
				}
				return this.senderTenantID;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000533 RID: 1331 RVA: 0x0001EADD File Offset: 0x0001CCDD
		private Guid RecipientTenantID
		{
			get
			{
				if (this.recipientTenantID == Guid.Empty && this.RecipientOrganizationInfo != null)
				{
					this.recipientTenantID = this.RecipientOrganizationInfo.ProbeOrganizationId.ObjectGuid;
				}
				return this.recipientTenantID;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000534 RID: 1332 RVA: 0x0001EB18 File Offset: 0x0001CD18
		private ITenantRecipientSession SenderTenantSession
		{
			get
			{
				if (this.senderTenantSession == null && this.SenderOrganizationInfo != null)
				{
					this.senderTenantSession = DirectorySessionFactory.Default.CreateTenantRecipientSession(null, null, CultureInfo.InvariantCulture.LCID, true, ConsistencyMode.IgnoreInvalid, null, ADSessionSettings.FromExternalDirectoryOrganizationId(this.SenderTenantID), 560, "SenderTenantSession", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\GenericWorkItemHelper\\TransportProbeHelper.cs");
					if (this.senderTenantSession == null)
					{
						throw new InvalidOperationException("Cannot get sender TenantConfigurationSession.");
					}
				}
				return this.senderTenantSession;
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000535 RID: 1333 RVA: 0x0001EB88 File Offset: 0x0001CD88
		private ITenantRecipientSession RecipientTenantSession
		{
			get
			{
				if (this.recipientTenantSession == null && this.RecipientOrganizationInfo != null)
				{
					this.recipientTenantSession = DirectorySessionFactory.Default.CreateTenantRecipientSession(null, null, CultureInfo.InvariantCulture.LCID, true, ConsistencyMode.IgnoreInvalid, null, ADSessionSettings.FromExternalDirectoryOrganizationId(this.RecipientTenantID), 591, "RecipientTenantSession", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\GenericWorkItemHelper\\TransportProbeHelper.cs");
					if (this.recipientTenantSession == null)
					{
						throw new InvalidOperationException("Cannot get recipient TenantConfigurationSession.");
					}
				}
				return this.recipientTenantSession;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000536 RID: 1334 RVA: 0x0001EBF8 File Offset: 0x0001CDF8
		private IEnumerable<ADUser> SenderUsers
		{
			get
			{
				if (this.SenderOrganizationInfo != null)
				{
					this.senderUsers = this.SenderTenantSession.FindADUser(null, QueryScope.SubTree, null, null, int.MaxValue);
					if (this.senderUsers == null || this.senderUsers.Count<ADUser>() == 0)
					{
						throw new InvalidOperationException("Cannot get sender users.");
					}
				}
				return this.senderUsers;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000537 RID: 1335 RVA: 0x0001EC50 File Offset: 0x0001CE50
		private IEnumerable<ADUser> RecipientUsers
		{
			get
			{
				if (this.RecipientOrganizationInfo != null)
				{
					this.recipientUsers = this.RecipientTenantSession.FindADUser(null, QueryScope.SubTree, null, null, int.MaxValue);
					if (this.recipientUsers == null || this.recipientUsers.Count<ADUser>() == 0)
					{
						throw new InvalidOperationException("Cannot get recipient users.");
					}
				}
				return this.recipientUsers;
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000538 RID: 1336 RVA: 0x0001ECA8 File Offset: 0x0001CEA8
		private string SenderPassword
		{
			get
			{
				SecureString loginPassword = this.SenderOrganizationInfo.LoginPassword;
				if (loginPassword == null)
				{
					throw new ArgumentException("The sender organization does not have a password specified.");
				}
				IntPtr ptr = Marshal.SecureStringToBSTR(loginPassword);
				string result;
				try
				{
					result = Marshal.PtrToStringBSTR(ptr);
				}
				finally
				{
					Marshal.FreeBSTR(ptr);
				}
				return result;
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000539 RID: 1337 RVA: 0x0001ECF8 File Offset: 0x0001CEF8
		private string RecipientPassword
		{
			get
			{
				SecureString loginPassword = this.RecipientOrganizationInfo.LoginPassword;
				if (loginPassword == null)
				{
					throw new ArgumentException("The recipient organization does not have a password specified.");
				}
				IntPtr ptr = Marshal.SecureStringToBSTR(loginPassword);
				string result;
				try
				{
					result = Marshal.PtrToStringBSTR(ptr);
				}
				finally
				{
					Marshal.FreeBSTR(ptr);
				}
				return result;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x0600053A RID: 1338 RVA: 0x0001ED48 File Offset: 0x0001CF48
		private ICollection<MailboxDatabaseInfo> MailboxCollectionForBackend
		{
			get
			{
				if (this.mailboxCollectionForBackend == null)
				{
					LocalEndpointManager instance = LocalEndpointManager.Instance;
					if (instance.MailboxDatabaseEndpoint == null)
					{
						throw new InvalidOperationException("No mailbox database for Backend found on this server.");
					}
					this.mailboxCollectionForBackend = instance.MailboxDatabaseEndpoint.MailboxDatabaseInfoCollectionForBackend;
				}
				return this.mailboxCollectionForBackend;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x0600053B RID: 1339 RVA: 0x0001ED90 File Offset: 0x0001CF90
		private ICollection<MailboxDatabaseInfo> MailboxCollectionForCafe
		{
			get
			{
				if (this.mailboxCollectionForCafe == null)
				{
					LocalEndpointManager instance = LocalEndpointManager.Instance;
					if (instance.MailboxDatabaseEndpoint == null)
					{
						throw new InvalidOperationException("No mailbox database for Cafe found on this server.");
					}
					this.mailboxCollectionForCafe = instance.MailboxDatabaseEndpoint.MailboxDatabaseInfoCollectionForCafe;
				}
				return this.mailboxCollectionForCafe;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x0600053C RID: 1340 RVA: 0x0001EDD5 File Offset: 0x0001CFD5
		private bool RecipientPasswordIsNeeded
		{
			get
			{
				return this.PopulateRecipientPassword || this.PopulateCheckMailCredential || this.PopulateAuthenticationAccount;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x0600053D RID: 1341 RVA: 0x0001EDF0 File Offset: 0x0001CFF0
		private bool IsExTest
		{
			get
			{
				NetworkInterface[] allNetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
				foreach (NetworkInterface networkInterface in allNetworkInterfaces)
				{
					IPInterfaceProperties ipproperties = networkInterface.GetIPProperties();
					if (!string.IsNullOrWhiteSpace(ipproperties.DnsSuffix) && ipproperties.DnsSuffix.ToLower().Contains("extest.microsoft.com"))
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x0001EE54 File Offset: 0x0001D054
		internal override List<ProbeDefinition> CreateDefinition()
		{
			List<ProbeDefinition> list = new List<ProbeDefinition>();
			if (!this.CheckEssentialInfo())
			{
				list.Add(base.CreateProbeDefinition());
				return list;
			}
			try
			{
				this.xmlNodeCollection = new List<XmlNode>();
				this.PopulateUsers();
				int num = this.xmlNodeCollection.Count;
				foreach (XmlNode xmlNode in this.xmlNodeCollection)
				{
					XmlElement xmlElement = (XmlElement)xmlNode;
					ProbeDefinition probeDefinition = base.CreateProbeDefinition(xmlElement);
					if (num > 1)
					{
						probeDefinition.Name = string.Format("{0}_{1}", probeDefinition.Name, num);
						num--;
					}
					list.Add(probeDefinition);
				}
			}
			catch (TransientDALException ex)
			{
				if (!this.IsExTest)
				{
					throw;
				}
				WTFDiagnostics.TraceDebug<string>(ExTraceGlobals.GenericHelperTracer, base.TraceContext, "This topology does not support feature tags. {0}", ex.ToString(), null, "CreateDefinition", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\GenericWorkItemHelper\\TransportProbeHelper.cs", 865);
			}
			return list;
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x0001EF60 File Offset: 0x0001D160
		private void PopulateUsers()
		{
			if (!this.PopulateMailFrom && !this.PopulateMailTo && !this.PopulateAuthenticationAccount)
			{
				this.xmlNodeCollection.Add(this.ExtensionNode);
				return;
			}
			string optionalXmlAttribute = DefinitionHelperBase.GetOptionalXmlAttribute<string>(this.MailFrom, "Select", "all", base.TraceContext);
			int num = (string.Compare(optionalXmlAttribute.ToLower(), "all", true) == 0) ? 0 : ((int)Convert.ChangeType(optionalXmlAttribute, typeof(int)));
			num = ((num < 0) ? 0 : num);
			optionalXmlAttribute = DefinitionHelperBase.GetOptionalXmlAttribute<string>(this.MailTo, "Select", "all", base.TraceContext);
			int num2 = (string.Compare(optionalXmlAttribute.ToLower(), "all", true) == 0) ? 0 : ((int)Convert.ChangeType(optionalXmlAttribute, typeof(int)));
			num2 = ((num2 < 0) ? 0 : num2);
			if (this.MonitoringMailbox != TransportProbeHelper.MonitoringMailboxType.None)
			{
				if (!this.CheckRoles())
				{
					return;
				}
				LocalEndpointManager instance = LocalEndpointManager.Instance;
				if (instance.MailboxDatabaseEndpoint == null)
				{
					WTFDiagnostics.TraceDebug(ExTraceGlobals.GenericHelperTracer, base.TraceContext, "TransportProbeHelper: No mailbox database found on this server", null, "PopulateUsers", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\GenericWorkItemHelper\\TransportProbeHelper.cs", 909);
					return;
				}
				if (this.CafeMailboxIsNeeded)
				{
					this.PopulateMailboxes(this.MailboxCollectionForCafe, num, num2);
				}
				if (this.BackendMailboxIsNeeded)
				{
					this.PopulateMailboxes(this.MailboxCollectionForBackend, num, num2);
					return;
				}
			}
			else
			{
				IEnumerable<ADUser> enumerable = null;
				if (this.PopulateMailFrom)
				{
					enumerable = this.SelectUsers(this.SenderUsers, num);
				}
				IEnumerable<ADUser> enumerable2 = null;
				if (this.PopulateMailTo || this.PopulateAuthenticationAccount)
				{
					enumerable2 = this.SelectUsers(this.RecipientUsers, num2);
				}
				if (this.PopulateBoth)
				{
					this.InsertSenderAndRecipient(enumerable, enumerable2);
					return;
				}
				if (this.PopulateMailFrom)
				{
					this.InsertSender(enumerable);
					return;
				}
				if (this.PopulateMailTo)
				{
					this.InsertRecipient(enumerable2);
					return;
				}
				if (this.PopulateAuthenticationAccount)
				{
					this.InsertAuthenticationAccount(enumerable2);
				}
			}
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x0001F130 File Offset: 0x0001D330
		private bool CheckRoles()
		{
			if (this.MonitoringMailbox == TransportProbeHelper.MonitoringMailboxType.Cafe && !DiscoveryContext.ExchangeInstalledRoles["Cafe"])
			{
				WTFDiagnostics.TraceDebug(ExTraceGlobals.GenericHelperTracer, base.TraceContext, "The Cafe role is not installed on this server.", null, "CheckRoles", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\GenericWorkItemHelper\\TransportProbeHelper.cs", 976);
				return false;
			}
			if (this.MonitoringMailbox == TransportProbeHelper.MonitoringMailboxType.Backend && !DiscoveryContext.ExchangeInstalledRoles["Mailbox"])
			{
				WTFDiagnostics.TraceDebug(ExTraceGlobals.GenericHelperTracer, base.TraceContext, "The Mailbox role is not installed on this server.", null, "CheckRoles", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\GenericWorkItemHelper\\TransportProbeHelper.cs", 982);
				return false;
			}
			if (this.MonitoringMailbox == TransportProbeHelper.MonitoringMailboxType.Both && (!DiscoveryContext.ExchangeInstalledRoles["Cafe"] || !DiscoveryContext.ExchangeInstalledRoles["Mailbox"]))
			{
				WTFDiagnostics.TraceDebug(ExTraceGlobals.GenericHelperTracer, base.TraceContext, "Either the Cafe or Mailbox role is not installed on this server.", null, "CheckRoles", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\GenericWorkItemHelper\\TransportProbeHelper.cs", 989);
				return false;
			}
			if (this.MonitoringMailbox == TransportProbeHelper.MonitoringMailboxType.Either && !DiscoveryContext.ExchangeInstalledRoles["Cafe"] && !DiscoveryContext.ExchangeInstalledRoles["Mailbox"])
			{
				WTFDiagnostics.TraceDebug(ExTraceGlobals.GenericHelperTracer, base.TraceContext, "Neither the Cafe nor the Mailbox role is installed on this server.", null, "CheckRoles", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\GenericWorkItemHelper\\TransportProbeHelper.cs", 996);
				return false;
			}
			return true;
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x0001F264 File Offset: 0x0001D464
		private void PopulateMailboxes(ICollection<MailboxDatabaseInfo> mailboxes, int senderRequested, int recipientRequested)
		{
			IEnumerable<MailboxDatabaseInfo> enumerable = null;
			if (this.PopulateMailFrom)
			{
				enumerable = this.SelectUsers(mailboxes, senderRequested, this.PopulateSenderPassword);
			}
			IEnumerable<MailboxDatabaseInfo> enumerable2 = null;
			if (this.PopulateMailTo || this.PopulateAuthenticationAccount)
			{
				enumerable2 = this.SelectUsers(mailboxes, recipientRequested, this.RecipientPasswordIsNeeded);
			}
			if (this.PopulateBoth)
			{
				this.InsertSenderAndRecipient(enumerable, enumerable2);
				return;
			}
			if (this.PopulateMailFrom)
			{
				this.InsertSender(enumerable);
				return;
			}
			if (this.PopulateMailTo)
			{
				this.InsertRecipient(enumerable2);
				return;
			}
			if (this.PopulateAuthenticationAccount)
			{
				this.InsertAuthenticationAccount(enumerable2);
			}
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x0001F338 File Offset: 0x0001D538
		private IEnumerable<MailboxDatabaseInfo> SelectUsers(IEnumerable<MailboxDatabaseInfo> mailboxes, int numberRequested, bool populatePassword)
		{
			IEnumerable<MailboxDatabaseInfo> enumerable;
			if (populatePassword)
			{
				enumerable = from m in mailboxes
				where !string.IsNullOrWhiteSpace(m.MonitoringAccount) && !string.IsNullOrWhiteSpace(m.MonitoringAccountDomain) && !string.IsNullOrWhiteSpace(m.MonitoringAccountPassword)
				select m;
			}
			else
			{
				enumerable = from m in mailboxes
				where !string.IsNullOrWhiteSpace(m.MonitoringAccount) && !string.IsNullOrWhiteSpace(m.MonitoringAccountDomain)
				select m;
			}
			if (enumerable == null || enumerable.Count<MailboxDatabaseInfo>() == 0)
			{
				string message = string.Format("Unable to populate users: # of users with email address {0}= 0.", populatePassword ? "and password " : string.Empty);
				WTFDiagnostics.TraceDebug(ExTraceGlobals.GenericHelperTracer, base.TraceContext, message, null, "SelectUsers", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\GenericWorkItemHelper\\TransportProbeHelper.cs", 1076);
				throw new InvalidOperationException(message);
			}
			int count;
			int count2;
			this.GetRandomNumbers(enumerable.Count<MailboxDatabaseInfo>(), numberRequested, out count, out count2);
			return enumerable.Skip(count2).Take(count);
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x0001F424 File Offset: 0x0001D624
		private IEnumerable<ADUser> SelectUsers(IEnumerable<ADUser> users, int numberRequested)
		{
			IEnumerable<ADUser> enumerable = users.Where(delegate(ADUser user)
			{
				SmtpAddress windowsLiveID = user.WindowsLiveID;
				return user.WindowsLiveID.IsValidAddress;
			});
			if (enumerable == null || enumerable.Count<ADUser>() == 0)
			{
				string message = "Unable to populate users: # of users with email address = 0.";
				WTFDiagnostics.TraceDebug(ExTraceGlobals.GenericHelperTracer, base.TraceContext, message, null, "SelectUsers", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\GenericWorkItemHelper\\TransportProbeHelper.cs", 1103);
				throw new InvalidOperationException(message);
			}
			int count;
			int count2;
			this.GetRandomNumbers(enumerable.Count<ADUser>(), numberRequested, out count, out count2);
			return enumerable.Skip(count2).Take(count);
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x0001F4AC File Offset: 0x0001D6AC
		private void InsertSenderAndRecipient(IEnumerable<MailboxDatabaseInfo> senderSelected, IEnumerable<MailboxDatabaseInfo> recipientSelected)
		{
			if (senderSelected == null || recipientSelected == null)
			{
				WTFDiagnostics.TraceDebug(ExTraceGlobals.GenericHelperTracer, base.TraceContext, "Empty sender and/or recipient list", null, "InsertSenderAndRecipient", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\GenericWorkItemHelper\\TransportProbeHelper.cs", 1126);
				return;
			}
			if (this.MatchType == TransportProbeHelper.SenderRecipientMatchType.OneToMany)
			{
				using (IEnumerator<MailboxDatabaseInfo> enumerator = senderSelected.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						MailboxDatabaseInfo sender = enumerator.Current;
						foreach (MailboxDatabaseInfo recipient in recipientSelected)
						{
							XmlNode item = this.InsertSenderAndRecipient(sender, recipient);
							this.xmlNodeCollection.Add(item);
						}
					}
					return;
				}
			}
			if (this.MatchType == TransportProbeHelper.SenderRecipientMatchType.ToOneself)
			{
				using (IEnumerator<MailboxDatabaseInfo> enumerator3 = senderSelected.GetEnumerator())
				{
					while (enumerator3.MoveNext())
					{
						MailboxDatabaseInfo mailboxDatabaseInfo = enumerator3.Current;
						XmlNode item2 = this.InsertSenderAndRecipient(mailboxDatabaseInfo, mailboxDatabaseInfo);
						this.xmlNodeCollection.Add(item2);
					}
					return;
				}
			}
			if (senderSelected.Count<MailboxDatabaseInfo>() >= recipientSelected.Count<MailboxDatabaseInfo>())
			{
				int num = 0;
				List<MailboxDatabaseInfo> list = recipientSelected.ToList<MailboxDatabaseInfo>();
				using (IEnumerator<MailboxDatabaseInfo> enumerator4 = senderSelected.GetEnumerator())
				{
					while (enumerator4.MoveNext())
					{
						MailboxDatabaseInfo sender2 = enumerator4.Current;
						XmlNode item3 = this.InsertSenderAndRecipient(sender2, list[num]);
						this.xmlNodeCollection.Add(item3);
						if (++num == list.Count)
						{
							num = 0;
						}
					}
					return;
				}
			}
			int num2 = 0;
			List<MailboxDatabaseInfo> list2 = senderSelected.ToList<MailboxDatabaseInfo>();
			foreach (MailboxDatabaseInfo recipient2 in recipientSelected)
			{
				XmlNode item4 = this.InsertSenderAndRecipient(list2[num2], recipient2);
				this.xmlNodeCollection.Add(item4);
				if (++num2 == list2.Count)
				{
					num2 = 0;
				}
			}
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x0001F6C4 File Offset: 0x0001D8C4
		private void InsertSenderAndRecipient(IEnumerable<ADUser> senderSelected, IEnumerable<ADUser> recipientSelected)
		{
			if (senderSelected == null || recipientSelected == null)
			{
				WTFDiagnostics.TraceDebug(ExTraceGlobals.GenericHelperTracer, base.TraceContext, "Empty sender and/or recipient list", null, "InsertSenderAndRecipient", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\GenericWorkItemHelper\\TransportProbeHelper.cs", 1199);
				return;
			}
			if (this.MatchType == TransportProbeHelper.SenderRecipientMatchType.OneToMany)
			{
				using (IEnumerator<ADUser> enumerator = senderSelected.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ADUser sender = enumerator.Current;
						foreach (ADUser recipient in recipientSelected)
						{
							XmlNode item = this.InsertSenderAndRecipient(sender, recipient);
							this.xmlNodeCollection.Add(item);
						}
					}
					return;
				}
			}
			if (this.MatchType == TransportProbeHelper.SenderRecipientMatchType.ToOneself)
			{
				using (IEnumerator<ADUser> enumerator3 = senderSelected.GetEnumerator())
				{
					while (enumerator3.MoveNext())
					{
						ADUser aduser = enumerator3.Current;
						XmlNode item2 = this.InsertSenderAndRecipient(aduser, aduser);
						this.xmlNodeCollection.Add(item2);
					}
					return;
				}
			}
			if (senderSelected.Count<ADUser>() >= recipientSelected.Count<ADUser>())
			{
				int num = 0;
				List<ADUser> list = recipientSelected.ToList<ADUser>();
				using (IEnumerator<ADUser> enumerator4 = senderSelected.GetEnumerator())
				{
					while (enumerator4.MoveNext())
					{
						ADUser sender2 = enumerator4.Current;
						XmlNode item3 = this.InsertSenderAndRecipient(sender2, list[num]);
						this.xmlNodeCollection.Add(item3);
						if (++num == list.Count)
						{
							num = 0;
						}
					}
					return;
				}
			}
			int num2 = 0;
			List<ADUser> list2 = senderSelected.ToList<ADUser>();
			foreach (ADUser recipient2 in recipientSelected)
			{
				XmlNode item4 = this.InsertSenderAndRecipient(list2[num2], recipient2);
				this.xmlNodeCollection.Add(item4);
				if (++num2 == list2.Count)
				{
					num2 = 0;
				}
			}
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x0001F8DC File Offset: 0x0001DADC
		private XmlNode InsertSenderAndRecipient(MailboxDatabaseInfo sender, MailboxDatabaseInfo recipient)
		{
			XmlNode xmlNode = this.ExtensionNode.CloneNode(true);
			string user = sender.MonitoringAccount + "@" + sender.MonitoringAccountDomain;
			string user2 = recipient.MonitoringAccount + "@" + recipient.MonitoringAccountDomain;
			this.InsertSenderUsername(xmlNode, user);
			this.InsertRecipientUsername(xmlNode, user2);
			this.InsertSenderPassword(xmlNode, sender.MonitoringAccountPassword);
			this.InsertRecipientPassword(xmlNode, recipient.MonitoringAccountPassword);
			this.InsertAuthenticationAccount(xmlNode, user2, recipient.MonitoringAccountPassword);
			this.InsertCheckMailCredentials(xmlNode, user2, recipient.MonitoringAccountPassword);
			this.InsertSenderTenantId(xmlNode, sender);
			this.InsertRecipientTenantId(xmlNode, recipient);
			return xmlNode;
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x0001F97C File Offset: 0x0001DB7C
		private XmlNode InsertSenderAndRecipient(ADUser sender, ADUser recipient)
		{
			XmlNode xmlNode = this.ExtensionNode.CloneNode(true);
			this.InsertSenderUsername(xmlNode, sender.WindowsLiveID.ToString());
			this.InsertRecipientUsername(xmlNode, recipient.WindowsLiveID.ToString());
			if (this.PopulateSenderPassword)
			{
				this.InsertSenderPassword(xmlNode, this.SenderPassword);
			}
			string passwd = this.RecipientPasswordIsNeeded ? this.RecipientPassword : string.Empty;
			this.InsertRecipientPassword(xmlNode, passwd);
			this.InsertAuthenticationAccount(xmlNode, recipient.WindowsLiveID.ToString(), passwd);
			this.InsertCheckMailCredentials(xmlNode, recipient.WindowsLiveID.ToString(), passwd);
			this.InsertTenantId(xmlNode);
			return xmlNode;
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x0001FA44 File Offset: 0x0001DC44
		private void InsertSender(IEnumerable<MailboxDatabaseInfo> selected)
		{
			if (selected == null)
			{
				WTFDiagnostics.TraceDebug(ExTraceGlobals.GenericHelperTracer, base.TraceContext, "Empty sender list", null, "InsertSender", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\GenericWorkItemHelper\\TransportProbeHelper.cs", 1337);
				return;
			}
			foreach (MailboxDatabaseInfo mailboxDatabaseInfo in selected)
			{
				XmlNode xmlNode = this.ExtensionNode.CloneNode(true);
				string user = mailboxDatabaseInfo.MonitoringAccount + "@" + mailboxDatabaseInfo.MonitoringAccountDomain;
				this.InsertSenderUsername(xmlNode, user);
				this.InsertSenderPassword(xmlNode, mailboxDatabaseInfo.MonitoringAccountPassword);
				this.InsertSenderTenantId(xmlNode, mailboxDatabaseInfo);
				this.xmlNodeCollection.Add(xmlNode);
			}
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x0001FAFC File Offset: 0x0001DCFC
		private void InsertSender(IEnumerable<ADUser> selected)
		{
			if (selected == null)
			{
				WTFDiagnostics.TraceDebug(ExTraceGlobals.GenericHelperTracer, base.TraceContext, "Empty sender list", null, "InsertSender", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\GenericWorkItemHelper\\TransportProbeHelper.cs", 1367);
				return;
			}
			foreach (ADUser aduser in selected)
			{
				XmlNode xmlNode = this.ExtensionNode.CloneNode(true);
				this.InsertSenderUsername(xmlNode, aduser.WindowsLiveID.ToString());
				if (this.PopulateSenderPassword)
				{
					this.InsertSenderPassword(xmlNode, this.SenderPassword);
				}
				this.InsertTenantId(xmlNode);
				this.xmlNodeCollection.Add(xmlNode);
			}
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x0001FBB8 File Offset: 0x0001DDB8
		private void InsertRecipient(IEnumerable<MailboxDatabaseInfo> selected)
		{
			if (selected == null)
			{
				WTFDiagnostics.TraceDebug(ExTraceGlobals.GenericHelperTracer, base.TraceContext, "Empty recipient list", null, "InsertRecipient", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\GenericWorkItemHelper\\TransportProbeHelper.cs", 1398);
				return;
			}
			foreach (MailboxDatabaseInfo mailboxDatabaseInfo in selected)
			{
				XmlNode xmlNode = this.ExtensionNode.CloneNode(true);
				string user = mailboxDatabaseInfo.MonitoringAccount + "@" + mailboxDatabaseInfo.MonitoringAccountDomain;
				this.InsertRecipientUsername(xmlNode, user);
				this.InsertRecipientPassword(xmlNode, mailboxDatabaseInfo.MonitoringAccountPassword);
				this.InsertAuthenticationAccount(xmlNode, user, mailboxDatabaseInfo.MonitoringAccountPassword);
				this.InsertCheckMailCredentials(xmlNode, user, mailboxDatabaseInfo.MonitoringAccountPassword);
				this.InsertRecipientTenantId(xmlNode, mailboxDatabaseInfo);
				this.xmlNodeCollection.Add(xmlNode);
			}
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x0001FC8C File Offset: 0x0001DE8C
		private void InsertRecipient(IEnumerable<ADUser> selected)
		{
			if (selected == null)
			{
				WTFDiagnostics.TraceDebug(ExTraceGlobals.GenericHelperTracer, base.TraceContext, "Empty recipient list", null, "InsertRecipient", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\GenericWorkItemHelper\\TransportProbeHelper.cs", 1430);
				return;
			}
			foreach (ADUser aduser in selected)
			{
				XmlNode xmlNode = this.ExtensionNode.CloneNode(true);
				this.InsertRecipientUsername(xmlNode, aduser.WindowsLiveID.ToString());
				string passwd = this.RecipientPasswordIsNeeded ? this.RecipientPassword : string.Empty;
				this.InsertRecipientPassword(xmlNode, passwd);
				this.InsertAuthenticationAccount(xmlNode, aduser.WindowsLiveID.ToString(), passwd);
				this.InsertCheckMailCredentials(xmlNode, aduser.WindowsLiveID.ToString(), passwd);
				this.InsertTenantId(xmlNode);
				this.xmlNodeCollection.Add(xmlNode);
			}
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x0001FD90 File Offset: 0x0001DF90
		private void InsertAuthenticationAccount(IEnumerable<MailboxDatabaseInfo> selected)
		{
			if (selected == null)
			{
				WTFDiagnostics.TraceDebug(ExTraceGlobals.GenericHelperTracer, base.TraceContext, "Empty recipient list", null, "InsertAuthenticationAccount", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\GenericWorkItemHelper\\TransportProbeHelper.cs", 1462);
				return;
			}
			foreach (MailboxDatabaseInfo mailboxDatabaseInfo in selected)
			{
				XmlNode xmlNode = this.ExtensionNode.CloneNode(true);
				string user = mailboxDatabaseInfo.MonitoringAccount + "@" + mailboxDatabaseInfo.MonitoringAccountDomain;
				this.InsertAuthenticationAccount(xmlNode, user, mailboxDatabaseInfo.MonitoringAccountPassword);
				this.InsertRecipientTenantId(xmlNode, mailboxDatabaseInfo);
				this.xmlNodeCollection.Add(xmlNode);
			}
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x0001FE40 File Offset: 0x0001E040
		private void InsertAuthenticationAccount(IEnumerable<ADUser> selected)
		{
			if (selected == null)
			{
				WTFDiagnostics.TraceDebug(ExTraceGlobals.GenericHelperTracer, base.TraceContext, "Empty recipient list", null, "InsertAuthenticationAccount", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\GenericWorkItemHelper\\TransportProbeHelper.cs", 1489);
				return;
			}
			foreach (ADUser aduser in selected)
			{
				XmlNode xmlNode = this.ExtensionNode.CloneNode(true);
				string passwd = this.RecipientPasswordIsNeeded ? this.RecipientPassword : string.Empty;
				this.InsertAuthenticationAccount(xmlNode, aduser.WindowsLiveID.ToString(), passwd);
				this.InsertTenantId(xmlNode);
				this.xmlNodeCollection.Add(xmlNode);
			}
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x0001FF00 File Offset: 0x0001E100
		private void InsertSenderUsername(XmlNode node, string user)
		{
			if (this.PopulateMailFrom)
			{
				XmlElement xmlElement = (node.SelectSingleNode("WorkContext/SendMail/MailFrom") as XmlElement) ?? (node.SelectSingleNode("WorkContext/MailFrom") as XmlElement);
				if (xmlElement != null)
				{
					string name = "Username";
					if (xmlElement.HasAttribute(name))
					{
						xmlElement.Attributes[name].Value = user;
						return;
					}
					xmlElement.SetAttribute(name, user);
				}
			}
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x0001FF68 File Offset: 0x0001E168
		private void InsertRecipientUsername(XmlNode node, string user)
		{
			if (this.PopulateMailTo)
			{
				XmlElement xmlElement = (node.SelectSingleNode("WorkContext/SendMail/MailTo") as XmlElement) ?? (node.SelectSingleNode("WorkContext/MailTo") as XmlElement);
				if (xmlElement != null)
				{
					string name = "Username";
					if (xmlElement.HasAttribute(name))
					{
						xmlElement.Attributes[name].Value = user;
						return;
					}
					xmlElement.SetAttribute(name, user);
				}
			}
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x0001FFD0 File Offset: 0x0001E1D0
		private void InsertSenderPassword(XmlNode node, string passwd)
		{
			if (this.PopulateSenderPassword)
			{
				XmlElement xmlElement = (node.SelectSingleNode("WorkContext/SendMail/MailFrom") as XmlElement) ?? (node.SelectSingleNode("WorkContext/MailFrom") as XmlElement);
				if (xmlElement != null)
				{
					string name = "Password";
					if (xmlElement.HasAttribute(name))
					{
						xmlElement.Attributes[name].Value = passwd;
						return;
					}
					xmlElement.SetAttribute(name, passwd);
				}
			}
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x00020038 File Offset: 0x0001E238
		private void InsertRecipientPassword(XmlNode node, string passwd)
		{
			if (this.PopulateRecipientPassword)
			{
				XmlElement xmlElement = (node.SelectSingleNode("WorkContext/SendMail/MailTo") as XmlElement) ?? (node.SelectSingleNode("WorkContext/MailTo") as XmlElement);
				if (xmlElement != null)
				{
					string name = "Password";
					if (xmlElement.HasAttribute(name))
					{
						xmlElement.Attributes[name].Value = passwd;
						return;
					}
					xmlElement.SetAttribute(name, passwd);
				}
			}
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x000200A0 File Offset: 0x0001E2A0
		private void InsertAuthenticationAccount(XmlNode node, string user, string passwd)
		{
			if (this.PopulateAuthenticationAccount)
			{
				XmlElement xmlElement = (node.SelectSingleNode("WorkContext/AuthenticationAccount") as XmlElement) ?? (node.SelectSingleNode("WorkContext/SendMail/AuthenticationAccount") as XmlElement);
				if (xmlElement != null)
				{
					string name = "Username";
					if (xmlElement.HasAttribute(name))
					{
						xmlElement.Attributes[name].Value = user;
					}
					else
					{
						xmlElement.SetAttribute(name, user);
					}
					string name2 = "Password";
					if (xmlElement.HasAttribute(name2))
					{
						xmlElement.Attributes[name2].Value = passwd;
						return;
					}
					xmlElement.SetAttribute(name2, passwd);
				}
			}
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x00020134 File Offset: 0x0001E334
		private void InsertCheckMailCredentials(XmlNode node, string user, string passwd)
		{
			if (this.PopulateCheckMailCredential)
			{
				XmlElement xmlElement = node.SelectSingleNode("WorkContext/CheckMail") as XmlElement;
				if (xmlElement != null)
				{
					string name = "Username";
					string name2 = "Password";
					if (xmlElement.HasAttribute(name))
					{
						xmlElement.Attributes[name].Value = user;
					}
					else
					{
						xmlElement.SetAttribute(name, user);
					}
					if (xmlElement.HasAttribute(name2))
					{
						xmlElement.Attributes[name2].Value = passwd;
						return;
					}
					xmlElement.SetAttribute(name2, passwd);
				}
			}
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x000201B4 File Offset: 0x0001E3B4
		private void InsertTenantId(XmlNode node)
		{
			XmlElement xmlElement = (node.SelectSingleNode("WorkContext/SendMail") ?? node.SelectSingleNode("WorkContext")) as XmlElement;
			if (!string.IsNullOrWhiteSpace(this.SenderFeatureTag))
			{
				xmlElement.SetAttribute("SenderTenantID", this.SenderTenantID.ToString());
			}
			if (!string.IsNullOrWhiteSpace(this.RecipientFeatureTag))
			{
				xmlElement.SetAttribute("RecipientTenantID", this.RecipientTenantID.ToString());
			}
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x0002023C File Offset: 0x0001E43C
		private void InsertSenderTenantId(XmlNode node, MailboxDatabaseInfo mailbox)
		{
			if (LocalEndpointManager.IsDataCenter)
			{
				XmlElement xmlElement = (node.SelectSingleNode("WorkContext/SendMail") ?? node.SelectSingleNode("WorkContext")) as XmlElement;
				xmlElement.SetAttribute("SenderTenantId", this.GetTenantId(mailbox));
			}
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x00020284 File Offset: 0x0001E484
		private void InsertRecipientTenantId(XmlNode node, MailboxDatabaseInfo mailbox)
		{
			if (LocalEndpointManager.IsDataCenter)
			{
				XmlElement xmlElement = (node.SelectSingleNode("WorkContext/SendMail") ?? node.SelectSingleNode("WorkContext")) as XmlElement;
				xmlElement.SetAttribute("RecipientTenantId", this.GetTenantId(mailbox));
			}
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x000202CC File Offset: 0x0001E4CC
		private string GetTenantId(MailboxDatabaseInfo mailbox)
		{
			Guid empty = Guid.Empty;
			StringBuilder stringBuilder = new StringBuilder();
			if (mailbox.MonitoringAccountOrganizationId != null)
			{
				if (MultiTenantTransport.TryGetExternalOrgId(mailbox.MonitoringAccountOrganizationId, out empty) != ADOperationResult.Success)
				{
					stringBuilder.AppendLine("Attempt to get ExternalOrgId using MonitoringAccountOrganizationId failed.");
				}
			}
			else
			{
				stringBuilder.AppendLine("Mailbox has null MonitoringAccountOrganizationId.");
			}
			if (empty == Guid.Empty)
			{
				stringBuilder.AppendLine("Could not get TenantId using MailboxDatabaseInfo.");
				WTFDiagnostics.TraceDebug(ExTraceGlobals.GenericHelperTracer, base.TraceContext, stringBuilder.ToString(), null, "GetTenantId", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\GenericWorkItemHelper\\TransportProbeHelper.cs", 1772);
				throw new InvalidOperationException(stringBuilder.ToString());
			}
			return empty.ToString();
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x00020379 File Offset: 0x0001E579
		private void GetRandomNumbers(int total, int max, out int count, out int skip)
		{
			count = max;
			skip = 0;
			if (max == 0 || max >= total)
			{
				count = total;
				return;
			}
			skip = TransportProbeHelper.random.Next(0, total - max + 1);
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x000203A0 File Offset: 0x0001E5A0
		private bool IsPopulateUserRequired(XmlNode node)
		{
			return node != null && string.IsNullOrWhiteSpace(node.InnerText) && string.IsNullOrWhiteSpace(DefinitionHelperBase.GetOptionalXmlAttribute<string>(node, "Username", string.Empty, base.TraceContext));
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x000203D4 File Offset: 0x0001E5D4
		private bool IsPopulatePasswordRequired(XmlNode node)
		{
			if (node == null)
			{
				return false;
			}
			string text = "Password";
			return ((XmlElement)node).HasAttribute(text) && string.IsNullOrWhiteSpace(DefinitionHelperBase.GetOptionalXmlAttribute<string>(node, text, string.Empty, base.TraceContext));
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x00020418 File Offset: 0x0001E618
		private bool CheckEssentialInfo()
		{
			this.extensionNode = base.DefinitionNode.SelectSingleNode("ExtensionAttributes");
			if (this.ExtensionNode == null)
			{
				return false;
			}
			this.mailFrom = (this.ExtensionNode.SelectSingleNode("WorkContext/SendMail/MailFrom") ?? this.ExtensionNode.SelectSingleNode("WorkContext/MailFrom"));
			this.mailTo = (this.ExtensionNode.SelectSingleNode("WorkContext/SendMail/MailTo") ?? this.ExtensionNode.SelectSingleNode("WorkContext/MailTo"));
			this.authenticationAccount = (this.ExtensionNode.SelectSingleNode("WorkContext/AuthenticationAccount") ?? this.ExtensionNode.SelectSingleNode("WorkContext/SendMail/AuthenticationAccount"));
			this.checkMail = this.ExtensionNode.SelectSingleNode("WorkContext/CheckMail");
			this.monitoringMailbox = DefinitionHelperBase.GetOptionalXmlEnumAttribute<TransportProbeHelper.MonitoringMailboxType>(base.DiscoveryContext.ContextNode, "MonitoringMailbox", TransportProbeHelper.MonitoringMailboxType.None, base.TraceContext);
			this.matchType = DefinitionHelperBase.GetOptionalXmlEnumAttribute<TransportProbeHelper.SenderRecipientMatchType>(base.DiscoveryContext.ContextNode, "SenderRecipientMatchType", TransportProbeHelper.SenderRecipientMatchType.ToOneself, base.TraceContext);
			this.senderFeatureTag = DefinitionHelperBase.GetOptionalXmlAttribute<string>(base.DiscoveryContext.ContextNode, "SenderFeatureTag", string.Empty, base.TraceContext);
			this.recipientFeatureTag = DefinitionHelperBase.GetOptionalXmlAttribute<string>(base.DiscoveryContext.ContextNode, "RecipientFeatureTag", string.Empty, base.TraceContext);
			this.selectTenantBasedOnEnv = DefinitionHelperBase.GetOptionalXmlAttribute<bool>(base.DiscoveryContext.ContextNode, "SelectTenantBasedOnEnv", false, base.TraceContext);
			this.populateMailFrom = this.IsPopulateUserRequired(this.MailFrom);
			this.populateMailTo = this.IsPopulateUserRequired(this.MailTo);
			this.populateSenderPassword = (this.PopulateMailFrom && this.IsPopulatePasswordRequired(this.MailFrom));
			this.populateRecipientPassword = (this.PopulateMailTo && this.IsPopulatePasswordRequired(this.MailTo));
			this.populateAuthenticationAccount = this.IsPopulateUserRequired(this.AuthenticationAccount);
			this.populateCheckMailCredential = (this.PopulateMailTo && this.CheckMail != null);
			this.senderTenantSession = null;
			this.senderOrganizationInfo = null;
			this.senderUsers = null;
			this.senderTenantID = Guid.Empty;
			this.recipientTenantSession = null;
			this.recipientOrganizationInfo = null;
			this.recipientUsers = null;
			this.recipientTenantID = Guid.Empty;
			this.mailboxCollectionForBackend = null;
			this.mailboxCollectionForCafe = null;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("TypeName: " + base.DefinitionNode.Attributes["TypeName"]);
			stringBuilder.AppendLine("WorkContext:");
			stringBuilder.AppendLine(this.extensionNode.InnerXml);
			stringBuilder.AppendLine("SenderFeatureTag: " + this.senderFeatureTag);
			stringBuilder.AppendLine("RecipientFeatureTag: " + this.recipientFeatureTag);
			stringBuilder.AppendLine("SelectTenantBasedOnEnv: " + this.selectTenantBasedOnEnv.ToString());
			stringBuilder.AppendLine("MonitoringMailbox: " + this.monitoringMailbox.ToString());
			WTFDiagnostics.TraceDebug(ExTraceGlobals.GenericHelperTracer, base.TraceContext, stringBuilder.ToString(), null, "CheckEssentialInfo", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\GenericWorkItemHelper\\TransportProbeHelper.cs", 1896);
			return true;
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x00020758 File Offset: 0x0001E958
		private ProbeOrganizationInfo GetTenantOrg(IEnumerable<ProbeOrganizationInfo> probeOrgs)
		{
			if (probeOrgs.Count<ProbeOrganizationInfo>() == 0)
			{
				return null;
			}
			IEnumerable<ProbeOrganizationInfo> source = probeOrgs;
			if (this.selectTenantBasedOnEnv)
			{
				if (FfoLocalEndpointManager.IsForefrontForOfficeDatacenter)
				{
					source = from o in probeOrgs
					where o.CustomerType == CustomerType.FilteringOnly
					select o;
				}
				else if (LocalEndpointManager.IsDataCenter)
				{
					source = from o in probeOrgs
					where o.CustomerType == CustomerType.Hosted
					select o;
				}
				if (source.Count<ProbeOrganizationInfo>() == 0)
				{
					source = probeOrgs;
				}
			}
			int count = TransportProbeHelper.random.Next(0, source.Count<ProbeOrganizationInfo>());
			return source.Skip(count).First<ProbeOrganizationInfo>();
		}

		// Token: 0x04000334 RID: 820
		private static Random random = new Random();

		// Token: 0x04000335 RID: 821
		private XmlNode extensionNode;

		// Token: 0x04000336 RID: 822
		private XmlNode mailFrom;

		// Token: 0x04000337 RID: 823
		private XmlNode mailTo;

		// Token: 0x04000338 RID: 824
		private XmlNode authenticationAccount;

		// Token: 0x04000339 RID: 825
		private XmlNode checkMail;

		// Token: 0x0400033A RID: 826
		private bool populateMailFrom;

		// Token: 0x0400033B RID: 827
		private bool populateMailTo;

		// Token: 0x0400033C RID: 828
		private bool populateAuthenticationAccount;

		// Token: 0x0400033D RID: 829
		private bool populateSenderPassword;

		// Token: 0x0400033E RID: 830
		private bool populateRecipientPassword;

		// Token: 0x0400033F RID: 831
		private bool populateCheckMailCredential;

		// Token: 0x04000340 RID: 832
		private TransportProbeHelper.MonitoringMailboxType monitoringMailbox;

		// Token: 0x04000341 RID: 833
		private TransportProbeHelper.SenderRecipientMatchType matchType;

		// Token: 0x04000342 RID: 834
		private List<XmlNode> xmlNodeCollection;

		// Token: 0x04000343 RID: 835
		private string senderFeatureTag;

		// Token: 0x04000344 RID: 836
		private string recipientFeatureTag;

		// Token: 0x04000345 RID: 837
		private ProbeOrganizationInfo senderOrganizationInfo;

		// Token: 0x04000346 RID: 838
		private ProbeOrganizationInfo recipientOrganizationInfo;

		// Token: 0x04000347 RID: 839
		private bool selectTenantBasedOnEnv;

		// Token: 0x04000348 RID: 840
		private ITenantRecipientSession senderTenantSession;

		// Token: 0x04000349 RID: 841
		private ITenantRecipientSession recipientTenantSession;

		// Token: 0x0400034A RID: 842
		private Guid senderTenantID;

		// Token: 0x0400034B RID: 843
		private Guid recipientTenantID;

		// Token: 0x0400034C RID: 844
		private IEnumerable<ADUser> senderUsers;

		// Token: 0x0400034D RID: 845
		private IEnumerable<ADUser> recipientUsers;

		// Token: 0x0400034E RID: 846
		private ICollection<MailboxDatabaseInfo> mailboxCollectionForBackend;

		// Token: 0x0400034F RID: 847
		private ICollection<MailboxDatabaseInfo> mailboxCollectionForCafe;

		// Token: 0x02000093 RID: 147
		private enum MonitoringMailboxType
		{
			// Token: 0x04000356 RID: 854
			None,
			// Token: 0x04000357 RID: 855
			Backend,
			// Token: 0x04000358 RID: 856
			Cafe,
			// Token: 0x04000359 RID: 857
			Both,
			// Token: 0x0400035A RID: 858
			Either
		}

		// Token: 0x02000094 RID: 148
		private enum SenderRecipientMatchType
		{
			// Token: 0x0400035C RID: 860
			OneToOne,
			// Token: 0x0400035D RID: 861
			OneToMany,
			// Token: 0x0400035E RID: 862
			ToOneself
		}
	}
}
