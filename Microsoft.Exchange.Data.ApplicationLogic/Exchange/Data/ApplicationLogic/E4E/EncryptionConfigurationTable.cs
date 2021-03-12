using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.ApplicationLogic;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.Data.ApplicationLogic.E4E
{
	// Token: 0x020000E9 RID: 233
	internal class EncryptionConfigurationTable
	{
		// Token: 0x060009C1 RID: 2497 RVA: 0x00026044 File Offset: 0x00024244
		internal static EncryptionConfigurationData GetEncryptionConfiguration(string domain, bool useCache, out EncryptionConfigurationTable.RequestData requestData)
		{
			EncryptionConfigurationData encryptionConfigurationData = null;
			if (useCache && EncryptionConfigurationTable.encryptionConfigurationDataCache.TryGetValue(domain, out encryptionConfigurationData))
			{
				requestData = new EncryptionConfigurationTable.RequestData();
				return encryptionConfigurationData;
			}
			EncryptionConfigurationTable.RequestData requestDataSave = new EncryptionConfigurationTable.RequestData();
			requestData = requestDataSave;
			EncryptionConfigurationTable.ExecuteWebServicesAction(delegate
			{
				Stopwatch stopwatch = new Stopwatch();
				stopwatch.Start();
				ExchangeService exchangeService = EncryptionConfigurationTable.CreateExchangeServiceDelegate(domain, requestDataSave);
				if (exchangeService == null)
				{
					return;
				}
				exchangeService.Timeout = 30000;
				requestDataSave.CreateExchangeServiceTime = stopwatch.ElapsedMilliseconds;
				GetEncryptionConfigurationResponse encryptionConfiguration = exchangeService.GetEncryptionConfiguration();
				stopwatch.Stop();
				requestDataSave.GetEncryptionConfigurationTime = stopwatch.ElapsedMilliseconds - requestDataSave.CreateExchangeServiceTime;
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
				encryptionConfigurationData = new EncryptionConfigurationData(encryptionConfiguration.ImageBase64, encryptionConfiguration.EmailText, encryptionConfiguration.PortalText, encryptionConfiguration.DisclaimerText, encryptionConfiguration.OTPEnabled);
			});
			if (useCache && encryptionConfigurationData != null)
			{
				EncryptionConfigurationTable.encryptionConfigurationDataCache.Add(domain, encryptionConfigurationData);
			}
			return encryptionConfigurationData;
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x00026168 File Offset: 0x00024368
		internal static void SetEncryptionConfiguration(string domain, string imageBase64, string emailText, string portalText, string disclaimerText, bool otpEnabled)
		{
			EncryptionConfigurationTable.ExecuteWebServicesAction(delegate
			{
				EncryptionConfigurationTable.RequestData arg = new EncryptionConfigurationTable.RequestData();
				ExchangeService exchangeService = EncryptionConfigurationTable.CreateExchangeServiceDelegate(domain, arg);
				if (exchangeService == null)
				{
					return;
				}
				exchangeService.SetEncryptionConfiguration(imageBase64, emailText, portalText, disclaimerText, otpEnabled);
				EncryptionConfigurationData data = new EncryptionConfigurationData(imageBase64, emailText, portalText, disclaimerText, otpEnabled);
				EncryptionConfigurationTable.encryptionConfigurationDataCache.Add(domain, data);
			});
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x000261BC File Offset: 0x000243BC
		private static ExchangeService CreateExchangeService(string domain, EncryptionConfigurationTable.RequestData requestData)
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			ADUser orgMailbox = EncryptionConfigurationTable.GetOrgMailbox(domain);
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
				EncryptionConfigurationTable.logger.LogEvent(ApplicationLogicEventLogConstants.Tuple_E4EOrganizationMailboxWebServiceUrlRetrievalFailed, domain, new object[]
				{
					"ProcessEncryptionConfiguration",
					EncryptionConfigurationTable.GetLoggedExceptionString(exception)
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
				EncryptionConfigurationTable.logger.LogEvent(ApplicationLogicEventLogConstants.Tuple_E4EOrganizationMailboxWebServiceUrlRetrievalFailed, domain, new object[]
				{
					"ProcessEncryptionConfiguration",
					EncryptionConfigurationTable.GetLoggedExceptionString(exception2)
				});
				return null;
			}
			ExchangeService exchangeService = new ExchangeService(4);
			exchangeService.Url = backEndWebServicesUrl;
			exchangeService.PrivilegedUserId = new PrivilegedUserId(0, 2, orgMailbox.PrimarySmtpAddress.ToString());
			exchangeService.PrivilegedUserId.BudgetType = new PrivilegedUserIdBudgetType?(2);
			RemoteCertificateValidationCallback callback = (object param0, X509Certificate param1, X509Chain param2, SslPolicyErrors param3) => true;
			CertificateValidationManager.RegisterCallback("E4E.EncryptionConfigurationTable", callback);
			exchangeService.HttpHeaders.Add(CertificateValidationManager.ComponentIdHeaderName, "E4E.EncryptionConfigurationTable");
			return exchangeService;
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x00026338 File Offset: 0x00024538
		private static void ExecuteWebServicesAction(Action webServicesAction)
		{
			try
			{
				webServicesAction();
			}
			catch (ServiceLocalException arg)
			{
				EncryptionConfigurationTable.Tracer.TraceError<ServiceLocalException>(0L, "EncryptionConfigurationTable.ExecuteWebServicesAction Exception: {0}", arg);
				throw;
			}
			catch (ServiceRemoteException arg2)
			{
				EncryptionConfigurationTable.Tracer.TraceError<ServiceRemoteException>(0L, "EncryptionConfigurationTable.ExecuteWebServicesAction Exception: {0}", arg2);
				throw;
			}
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x00026394 File Offset: 0x00024594
		private static ADUser GetOrgMailbox(string domain)
		{
			ADSessionSettings sessionSettings = null;
			try
			{
				sessionSettings = ADSessionSettings.FromTenantAcceptedDomain(domain);
			}
			catch (CannotResolveTenantNameException arg)
			{
				EncryptionConfigurationTable.Tracer.TraceInformation<CannotResolveTenantNameException>(0, 0L, "Can't resolve tenant name. Exception: {0}", arg);
				EncryptionConfigurationTable.logger.LogEvent(ApplicationLogicEventLogConstants.Tuple_E4EOrganizationMailboxRetrievalFailed, domain, new object[]
				{
					"ProcessEncryptionConfiguration",
					domain
				});
				return null;
			}
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(true, ConsistencyMode.IgnoreInvalid, sessionSettings, 334, "GetOrgMailbox", "f:\\15.00.1497\\sources\\dev\\data\\src\\ApplicationLogic\\E4E\\EncryptionConfigurationTable.cs");
			List<ADUser> organizationMailboxesByCapability = OrganizationMailbox.GetOrganizationMailboxesByCapability(tenantOrRootOrgRecipientSession, OrganizationCapability.OfficeMessageEncryption);
			if (organizationMailboxesByCapability == null || 1 != organizationMailboxesByCapability.Count)
			{
				EncryptionConfigurationTable.Tracer.TraceInformation(0, 0L, "Org mailbox is not configured.");
				EncryptionConfigurationTable.logger.LogEvent(ApplicationLogicEventLogConstants.Tuple_E4EOrganizationMailboxRetrievalFailed, domain, new object[]
				{
					"ProcessEncryptionConfiguration",
					domain
				});
				return null;
			}
			return organizationMailboxesByCapability[0];
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x00026478 File Offset: 0x00024678
		private static string GetLoggedExceptionString(Exception exception)
		{
			string text = exception.ToString();
			if (text.Length > 32000)
			{
				text = text.Substring(0, 2000) + "...\n" + text.Substring(text.Length - 20000, 20000);
			}
			return text;
		}

		// Token: 0x04000493 RID: 1171
		private const string RequestId = "request-id";

		// Token: 0x04000494 RID: 1172
		private const string ComponentId = "E4E.EncryptionConfigurationTable";

		// Token: 0x04000495 RID: 1173
		private const string ScenarioProcessEncryptionConfiguration = "ProcessEncryptionConfiguration";

		// Token: 0x04000496 RID: 1174
		private const int GetEncryptionConfigurationTimeout = 30000;

		// Token: 0x04000497 RID: 1175
		private static Guid eventLogComponentGuid = new Guid("{B02E4405-8AB4-4B3D-A534-1FCB53D47717}");

		// Token: 0x04000498 RID: 1176
		private static readonly ExEventLog logger = new ExEventLog(EncryptionConfigurationTable.eventLogComponentGuid, "MSExchangeApplicationLogic");

		// Token: 0x04000499 RID: 1177
		private static readonly Microsoft.Exchange.Diagnostics.Trace Tracer = ExTraceGlobals.E4ETracer;

		// Token: 0x0400049A RID: 1178
		private static MruDictionaryCache<string, EncryptionConfigurationData> encryptionConfigurationDataCache = new MruDictionaryCache<string, EncryptionConfigurationData>(5, 50000, 5);

		// Token: 0x0400049B RID: 1179
		internal static Func<string, EncryptionConfigurationTable.RequestData, ExchangeService> CreateExchangeServiceDelegate = new Func<string, EncryptionConfigurationTable.RequestData, ExchangeService>(EncryptionConfigurationTable.CreateExchangeService);

		// Token: 0x020000EA RID: 234
		internal class RequestData
		{
			// Token: 0x0400049D RID: 1181
			public long CreateExchangeServiceTime;

			// Token: 0x0400049E RID: 1182
			public long OrgMailboxAdUserLookupTime;

			// Token: 0x0400049F RID: 1183
			public long WebServiceUrlLookupTime;

			// Token: 0x040004A0 RID: 1184
			public long GetEncryptionConfigurationTime;

			// Token: 0x040004A1 RID: 1185
			public Uri ExchangeServiceUri;

			// Token: 0x040004A2 RID: 1186
			public string EwsRequestId;
		}
	}
}
