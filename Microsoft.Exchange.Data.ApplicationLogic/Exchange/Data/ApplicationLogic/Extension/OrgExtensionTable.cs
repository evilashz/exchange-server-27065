using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Net.Security;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;
using Microsoft.Exchange.Data.ApplicationLogic.Owa;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.ApplicationLogic;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Win32;

namespace Microsoft.Exchange.Data.ApplicationLogic.Extension
{
	// Token: 0x0200010C RID: 268
	internal class OrgExtensionTable
	{
		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000B4E RID: 2894 RVA: 0x0002D880 File Offset: 0x0002BA80
		// (set) Token: 0x06000B4F RID: 2895 RVA: 0x0002D887 File Offset: 0x0002BA87
		internal static bool? UseGetClientExtensionTimeout
		{
			get
			{
				return OrgExtensionTable.useGetClientExtensionTimeout;
			}
			set
			{
				OrgExtensionTable.useGetClientExtensionTimeout = value;
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000B50 RID: 2896 RVA: 0x0002D88F File Offset: 0x0002BA8F
		internal static bool IsDatacenter
		{
			get
			{
				return OrgExtensionTable.IsDatacenterEnvironment.Member;
			}
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x0002DA14 File Offset: 0x0002BC14
		internal static Dictionary<string, ExtensionData> GetOrgExtensions(StringList requestedExtensionIds, string domain, bool shouldReturnEnabledOnly, bool isUserScope, string userId, StringList userEnabledExtensionIds, StringList userDisabledExtensionIds, out OrgExtensionTable.RequestData requestData, bool isDebug, out string orgMasterTableRawXml, bool retrieveOnly1_0 = false)
		{
			Dictionary<string, ExtensionData> dictionary = new Dictionary<string, ExtensionData>(StringComparer.OrdinalIgnoreCase);
			Collection<ClientExtension> clientExtensions = null;
			string orgRawXml = null;
			OrgExtensionTable.RequestData requestDataSave = new OrgExtensionTable.RequestData();
			requestData = requestDataSave;
			OrgExtensionTable.ExecuteWebServicesAction(delegate
			{
				Stopwatch stopwatch = new Stopwatch();
				stopwatch.Start();
				ExchangeService exchangeService = OrgExtensionTable.CreateExchangeServiceDelegate(domain, requestDataSave);
				if (exchangeService == null)
				{
					return;
				}
				if (OrgExtensionTable.GetClientExtensionTimeoutIsAllowed())
				{
					exchangeService.Timeout = 30000;
				}
				requestDataSave.CreateExchangeServiceTime = stopwatch.ElapsedMilliseconds;
				GetClientExtensionResponse clientExtension2 = exchangeService.GetClientExtension(requestedExtensionIds, shouldReturnEnabledOnly, isUserScope, userId, userEnabledExtensionIds, userDisabledExtensionIds, isDebug);
				stopwatch.Stop();
				requestDataSave.GetClientExtensionTime = stopwatch.ElapsedMilliseconds - requestDataSave.CreateExchangeServiceTime;
				requestDataSave.ExchangeServiceUri = exchangeService.Url;
				string text;
				if (exchangeService.HttpResponseHeaders.TryGetValue("request-id", out text))
				{
					requestDataSave.EwsRequestId = text.Replace("-", string.Empty);
				}
				else
				{
					requestDataSave.EwsRequestId = "NotFound";
				}
				clientExtensions = clientExtension2.ClientExtensions;
				if (isDebug && !string.IsNullOrWhiteSpace(clientExtension2.RawMasterTableXml))
				{
					try
					{
						byte[] bytes = Convert.FromBase64String(clientExtension2.RawMasterTableXml);
						orgRawXml = Encoding.UTF8.GetString(bytes);
					}
					catch (FormatException innerException)
					{
						throw new OwaExtensionOperationException(innerException);
					}
					catch (ArgumentException innerException2)
					{
						throw new OwaExtensionOperationException(innerException2);
					}
				}
			});
			if (clientExtensions != null)
			{
				foreach (ClientExtension clientExtension in clientExtensions)
				{
					try
					{
						ExtensionData extensionData = ExtensionData.CreateFromClientExtension(clientExtension);
						if (!retrieveOnly1_0 || !(extensionData.SchemaVersion != SchemaConstants.SchemaVersion1_0))
						{
							ExtensionData extensionData2;
							if (!dictionary.TryGetValue(extensionData.ExtensionId, out extensionData2) || !(extensionData.SchemaVersion < extensionData2.SchemaVersion))
							{
								dictionary[ExtensionDataHelper.FormatExtensionId(extensionData.ExtensionId)] = extensionData;
							}
						}
					}
					catch (OwaExtensionOperationException ex)
					{
						OrgExtensionTable.Tracer.TraceError<OwaExtensionOperationException>(0L, "skipping an org extension because it cannot be parsed. Exception: {0}", ex);
						ExtensionDiagnostics.Logger.LogEvent(ApplicationLogicEventLogConstants.Tuple_OrgExtensionParsingFailed, null, new object[]
						{
							"ProcessOrgExtensions",
							ExtensionDiagnostics.GetLoggedExceptionString(ex)
						});
					}
				}
			}
			orgMasterTableRawXml = orgRawXml;
			return dictionary;
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x0002DD30 File Offset: 0x0002BF30
		internal static void SetOrgExtension(string domain, SetClientExtensionActionId actionId, string extensionId, ExtensionData extensionData)
		{
			OrgExtensionTable.ExecuteWebServicesAction(delegate
			{
				OrgExtensionTable.RequestData arg = new OrgExtensionTable.RequestData();
				ExchangeService exchangeService = OrgExtensionTable.CreateExchangeServiceDelegate(domain, arg);
				if (exchangeService == null)
				{
					return;
				}
				MemoryStream memoryStream = null;
				XmlWriter xmlWriter = null;
				try
				{
					ClientExtension clientExtension = null;
					if (extensionData != null)
					{
						memoryStream = new MemoryStream();
						xmlWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
						extensionData.Manifest.WriteTo(xmlWriter);
						xmlWriter.Flush();
						memoryStream.Position = 0L;
						clientExtension = new ClientExtension(extensionData.Type.Value, extensionData.Scope.Value, memoryStream, extensionData.MarketplaceAssetID, extensionData.MarketplaceContentMarket, extensionData.IsAvailable, extensionData.IsMandatory, extensionData.IsEnabledByDefault, extensionData.ProvidedTo, (extensionData.SpecificUsers == null) ? null : new StringList(extensionData.SpecificUsers), extensionData.AppStatus, extensionData.Etoken);
					}
					exchangeService.SetClientExtension(new List<SetClientExtensionAction>
					{
						new SetClientExtensionAction(actionId, extensionId, clientExtension)
					});
				}
				finally
				{
					if (xmlWriter != null)
					{
						xmlWriter.Close();
					}
					if (memoryStream != null)
					{
						memoryStream.Close();
					}
				}
			});
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x0002DE04 File Offset: 0x0002C004
		internal static void ConfigureOrgExtension(string domain, string extensionId, bool isEnabled, bool isMandatory, bool isEnabledByDefault, ClientExtensionProvidedTo providedTo, string[] specificUsers)
		{
			OrgExtensionTable.ExecuteWebServicesAction(delegate
			{
				OrgExtensionTable.RequestData arg = new OrgExtensionTable.RequestData();
				ExchangeService exchangeService = OrgExtensionTable.CreateExchangeServiceDelegate(domain, arg);
				if (exchangeService == null)
				{
					return;
				}
				ClientExtension clientExtension = new ClientExtension(0, 0, null, null, null, isEnabled, isMandatory, isEnabledByDefault, providedTo, (specificUsers == null) ? null : new StringList(specificUsers), string.Empty, string.Empty);
				exchangeService.SetClientExtension(new List<SetClientExtensionAction>
				{
					new SetClientExtensionAction(2, extensionId, clientExtension)
				});
			});
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x0002DE60 File Offset: 0x0002C060
		private static ExchangeService CreateExchangeService(string domain, OrgExtensionTable.RequestData requestData)
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			ADUser orgMailbox = OrgExtensionTable.GetOrgMailbox(domain);
			requestData.OrgMailboxAdUserLookupTime = stopwatch.ElapsedMilliseconds;
			if (orgMailbox == null)
			{
				return null;
			}
			ExchangePrincipal exchangePrincipal;
			try
			{
				exchangePrincipal = ExchangePrincipal.FromADUser(orgMailbox, null);
			}
			catch (ObjectNotFoundException exception)
			{
				ExtensionDiagnostics.Logger.LogEvent(ApplicationLogicEventLogConstants.Tuple_OrganizationMailboxWebServiceUrlRetrievalFailed, domain, new object[]
				{
					"ProcessOrgExtensions",
					ExtensionDiagnostics.GetLoggedExceptionString(exception)
				});
				return null;
			}
			Uri backEndWebServicesUrl;
			try
			{
				backEndWebServicesUrl = BackEndLocator.GetBackEndWebServicesUrl(exchangePrincipal.MailboxInfo);
				stopwatch.Stop();
				requestData.WebServiceUrlLookupTime = stopwatch.ElapsedMilliseconds - requestData.OrgMailboxAdUserLookupTime;
			}
			catch (BackEndLocatorException exception2)
			{
				ExtensionDiagnostics.Logger.LogEvent(ApplicationLogicEventLogConstants.Tuple_OrganizationMailboxWebServiceUrlRetrievalFailed, domain, new object[]
				{
					"ProcessOrgExtensions",
					ExtensionDiagnostics.GetLoggedExceptionString(exception2)
				});
				return null;
			}
			ExchangeService exchangeService = new ExchangeService(4);
			exchangeService.Url = backEndWebServicesUrl;
			exchangeService.PrivilegedUserId = new PrivilegedUserId(0, 2, orgMailbox.PrimarySmtpAddress.ToString());
			exchangeService.PrivilegedUserId.BudgetType = new PrivilegedUserIdBudgetType?(2);
			RemoteCertificateValidationCallback callback = (object param0, X509Certificate param1, X509Chain param2, SslPolicyErrors param3) => true;
			CertificateValidationManager.RegisterCallback("Extension.OrgExtensionTable", callback);
			exchangeService.HttpHeaders.Add(CertificateValidationManager.ComponentIdHeaderName, "Extension.OrgExtensionTable");
			return exchangeService;
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x0002DFDC File Offset: 0x0002C1DC
		private static void ExecuteWebServicesAction(Action webServicesAction)
		{
			try
			{
				webServicesAction();
			}
			catch (ServiceLocalException ex)
			{
				OrgExtensionTable.Tracer.TraceError<ServiceLocalException>(0L, "OrgExtensionTable.ExecuteWebServicesAction Exception: {0}", ex);
				throw new OwaExtensionOperationException(ex);
			}
			catch (ServiceRemoteException ex2)
			{
				OrgExtensionTable.Tracer.TraceError<ServiceRemoteException>(0L, "OrgExtensionTable.ExecuteWebServicesAction Exception: {0}", ex2);
				throw new OwaExtensionOperationException(ex2);
			}
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x0002E044 File Offset: 0x0002C244
		internal static ADUser GetOrgMailbox(string domain)
		{
			ADSessionSettings adsessionSettings = null;
			if (OrgExtensionTable.IsDatacenter)
			{
				if (domain == null)
				{
					return null;
				}
				try
				{
					adsessionSettings = ADSessionSettings.FromTenantAcceptedDomain(domain);
				}
				catch (CannotResolveTenantNameException arg)
				{
					OrgExtensionTable.Tracer.TraceInformation<CannotResolveTenantNameException>(0, 0L, "Can't resolve tenant name. Exception: {0}", arg);
					ExtensionDiagnostics.Logger.LogEvent(ApplicationLogicEventLogConstants.Tuple_OrganizationMailboxRetrievalFailed, domain, new object[]
					{
						"ProcessOrgExtensions",
						domain
					});
					return null;
				}
			}
			if (adsessionSettings == null)
			{
				adsessionSettings = ADSessionSettings.FromRootOrgScopeSet();
			}
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(true, ConsistencyMode.IgnoreInvalid, adsessionSettings, 559, "GetOrgMailbox", "f:\\15.00.1497\\sources\\dev\\data\\src\\ApplicationLogic\\extension\\OrgExtensionTable.cs");
			List<ADUser> organizationMailboxesByCapability = OrganizationMailbox.GetOrganizationMailboxesByCapability(tenantOrRootOrgRecipientSession, OrganizationCapability.ClientExtensions);
			if (organizationMailboxesByCapability == null || 1 != organizationMailboxesByCapability.Count)
			{
				OrgExtensionTable.Tracer.TraceInformation(0, 0L, "Org mailbox is not configured.");
				ExtensionDiagnostics.Logger.LogEvent(ApplicationLogicEventLogConstants.Tuple_OrganizationMailboxRetrievalFailed, domain, new object[]
				{
					"ProcessOrgExtensions",
					domain
				});
				return null;
			}
			return organizationMailboxesByCapability[0];
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x0002E140 File Offset: 0x0002C340
		private static bool GetClientExtensionTimeoutIsAllowed()
		{
			if (OrgExtensionTable.registryChangeListener == null)
			{
				OrgExtensionTable.Tracer.TraceDebug(0L, "Setting Registry Change Listener for UseGetClientExtensionTimeout Key.");
				OrgExtensionTable.registryChangeListener = new RegistryChangeListener(OwaConstants.OwaSetupInstallKey, new EventArrivedEventHandler(OrgExtensionTable.UseGetClientExtensionTimeoutRegistryKeyChangeHandler));
			}
			if (OrgExtensionTable.UseGetClientExtensionTimeout == null)
			{
				OrgExtensionTable.UseGetClientExtensionTimeout = new bool?(OrgExtensionTable.UseGetClientExtensionTimeoutValueFromRegistry());
			}
			return OrgExtensionTable.UseGetClientExtensionTimeout.Value;
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x0002E1AC File Offset: 0x0002C3AC
		private static bool UseGetClientExtensionTimeoutValueFromRegistry()
		{
			bool flag = false;
			Exception ex = null;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(OwaConstants.OwaSetupInstallKey))
				{
					flag = RegistryReader.Instance.GetValue<bool>(registryKey, null, "UseGetClientExtensionTimeout", true);
					OrgExtensionTable.Tracer.TraceDebug<bool>(0L, "Use Get Client Extension Timeout From Registry. Value: {0}", flag);
				}
			}
			catch (SecurityException ex2)
			{
				ex = ex2;
			}
			catch (IOException ex3)
			{
				ex = ex3;
			}
			catch (UnauthorizedAccessException ex4)
			{
				ex = ex4;
			}
			if (ex != null)
			{
				OrgExtensionTable.Tracer.TraceError<string, string>(0L, "Cannot Read Value: {0} from Registry due to Exception {1}. Using true.", "UseGetClientExtensionTimeout", ex.ToString());
				flag = true;
			}
			return flag;
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x0002E26C File Offset: 0x0002C46C
		private static void UseGetClientExtensionTimeoutRegistryKeyChangeHandler(object sender, EventArrivedEventArgs e)
		{
			OrgExtensionTable.Tracer.TraceDebug(0L, "Registry Change Event Occurred. Get UseGetClientExtensionTimeout Key.");
			OrgExtensionTable.UseGetClientExtensionTimeout = new bool?(OrgExtensionTable.UseGetClientExtensionTimeoutValueFromRegistry());
		}

		// Token: 0x040005B0 RID: 1456
		private const string RequestId = "request-id";

		// Token: 0x040005B1 RID: 1457
		private const string ComponentId = "Extension.OrgExtensionTable";

		// Token: 0x040005B2 RID: 1458
		private const string ScenarioProcessOrgExtension = "ProcessOrgExtensions";

		// Token: 0x040005B3 RID: 1459
		private const int GetClientExtensionTimeout = 30000;

		// Token: 0x040005B4 RID: 1460
		private static RegistryChangeListener registryChangeListener;

		// Token: 0x040005B5 RID: 1461
		private static readonly Microsoft.Exchange.Diagnostics.Trace Tracer = ExTraceGlobals.ExtensionTracer;

		// Token: 0x040005B6 RID: 1462
		private static readonly LazyMember<bool> IsDatacenterEnvironment = new LazyMember<bool>(new InitializeLazyMember<bool>(Datacenter.IsMultiTenancyEnabled));

		// Token: 0x040005B7 RID: 1463
		private static bool? useGetClientExtensionTimeout = new bool?(true);

		// Token: 0x040005B8 RID: 1464
		internal static Func<string, OrgExtensionTable.RequestData, ExchangeService> CreateExchangeServiceDelegate = new Func<string, OrgExtensionTable.RequestData, ExchangeService>(OrgExtensionTable.CreateExchangeService);

		// Token: 0x0200010D RID: 269
		internal class RequestData
		{
			// Token: 0x040005BA RID: 1466
			public long CreateExchangeServiceTime;

			// Token: 0x040005BB RID: 1467
			public long OrgMailboxAdUserLookupTime;

			// Token: 0x040005BC RID: 1468
			public long WebServiceUrlLookupTime;

			// Token: 0x040005BD RID: 1469
			public long GetClientExtensionTime;

			// Token: 0x040005BE RID: 1470
			public Uri ExchangeServiceUri;

			// Token: 0x040005BF RID: 1471
			public string EwsRequestId;
		}
	}
}
