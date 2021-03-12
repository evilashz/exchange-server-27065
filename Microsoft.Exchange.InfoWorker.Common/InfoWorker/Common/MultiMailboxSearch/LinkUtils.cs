using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.InfoWorker.EventLog;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x02000218 RID: 536
	internal static class LinkUtils
	{
		// Token: 0x06000E9E RID: 3742 RVA: 0x000405AC File Offset: 0x0003E7AC
		public static Uri UpdateOwaLinkToItem(Uri baseLink, string itemId)
		{
			return LinkUtils.AppendQueryString(baseLink, new Dictionary<string, string>
			{
				{
					"ItemID",
					itemId
				}
			});
		}

		// Token: 0x06000E9F RID: 3743 RVA: 0x000405D4 File Offset: 0x0003E7D4
		public static Uri UpdateOwaLinkToSearchId(Uri baseLink, string searchId)
		{
			Dictionary<string, string> queryParameters = new Dictionary<string, string>
			{
				{
					"cmd",
					"contents"
				},
				{
					"module",
					"discovery"
				},
				{
					"discoveryid",
					searchId
				},
				{
					"exsvurl",
					"1"
				}
			};
			Uri baseUri = LinkUtils.AppendRelativePath(baseLink, "default.aspx", false);
			return LinkUtils.AppendQueryString(baseUri, queryParameters);
		}

		// Token: 0x06000EA0 RID: 3744 RVA: 0x0004063B File Offset: 0x0003E83B
		public static Uri UpdateOwaLinkWithMailbox(Uri baseLink, SmtpAddress smtpAddress)
		{
			return LinkUtils.AppendRelativePath(baseLink, string.Format("{0}", smtpAddress), true);
		}

		// Token: 0x06000EA1 RID: 3745 RVA: 0x00040654 File Offset: 0x0003E854
		public static Uri GetOwaMailboxItemLink(Action errorHandler, MailboxInfo mailboxInfo, bool supportsIntegratedAuth)
		{
			Uri uri = LinkUtils.GetOwaBaseLink(errorHandler, mailboxInfo, supportsIntegratedAuth);
			if (uri != null)
			{
				uri = LinkUtils.AppendQueryString(uri, LinkUtils.itemLinkParameters);
			}
			return uri;
		}

		// Token: 0x06000EA2 RID: 3746 RVA: 0x00040680 File Offset: 0x0003E880
		public static Uri GetOwaBaseLink(Action errorHandler, MailboxInfo mailboxInfo, bool supportsIntegratedAuth)
		{
			Util.ThrowOnNull(mailboxInfo, "mailboxInfo");
			Util.ThrowOnNull(mailboxInfo.ExchangePrincipal, "mailboxInfo.ExchangePrincipal");
			return LinkUtils.GetOwaBaseLink(errorHandler, mailboxInfo.ExchangePrincipal, supportsIntegratedAuth);
		}

		// Token: 0x06000EA3 RID: 3747 RVA: 0x000406AC File Offset: 0x0003E8AC
		public static Uri GetOwaBaseLink(Action errorHandler, ExchangePrincipal targetPrincipal, bool supportsIntegratedAuth)
		{
			Uri uri = null;
			if (VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled)
			{
				uri = FrontEndLocator.GetFrontEndOwaUrl(targetPrincipal);
				if (uri == null)
				{
					Factory.Current.EventLog.LogEvent(InfoWorkerEventLogConstants.Tuple_DiscoveryFailedToGetOWAUrl, null, new object[]
					{
						targetPrincipal.MailboxInfo.Location.ServerFqdn,
						targetPrincipal.MailboxInfo.OrganizationId.OrganizationalUnit.ObjectGuid,
						targetPrincipal.MailboxInfo.MailboxGuid
					});
				}
				else if (!string.IsNullOrEmpty(targetPrincipal.MailboxInfo.PrimarySmtpAddress.ToString()))
				{
					SmtpAddress primarySmtpAddress = targetPrincipal.MailboxInfo.PrimarySmtpAddress;
					if (!string.IsNullOrEmpty(primarySmtpAddress.Domain))
					{
						uri = LinkUtils.AppendRelativePath(uri, primarySmtpAddress.Domain, true);
					}
				}
			}
			else
			{
				OwaService owaService = LinkUtils.GetOwaService(targetPrincipal);
				if (owaService != null)
				{
					uri = owaService.Url;
					if (supportsIntegratedAuth && owaService.IntegratedFeaturesEnabled)
					{
						uri = LinkUtils.AppendRelativePath(uri, "integrated", true);
					}
				}
				else if (errorHandler != null)
				{
					errorHandler();
				}
			}
			return uri;
		}

		// Token: 0x06000EA4 RID: 3748 RVA: 0x000407E4 File Offset: 0x0003E9E4
		public static OwaService GetOwaService(ExchangePrincipal principal)
		{
			OwaService owaService = null;
			try
			{
				ServiceTopology currentServiceTopology = ServiceTopology.GetCurrentServiceTopology("f:\\15.00.1497\\sources\\dev\\infoworker\\src\\common\\MultiMailboxSearch\\LinkUtils.cs", "GetOwaService", 197);
				owaService = (from x in currentServiceTopology.FindAll<OwaService>(principal, ClientAccessType.External, "f:\\15.00.1497\\sources\\dev\\infoworker\\src\\common\\MultiMailboxSearch\\LinkUtils.cs", "GetOwaService", 200)
				where x.IsFrontEnd
				select x).FirstOrDefault<OwaService>();
				if (owaService == null)
				{
					owaService = (from x in currentServiceTopology.FindAll<OwaService>(principal, ClientAccessType.Internal, "f:\\15.00.1497\\sources\\dev\\infoworker\\src\\common\\MultiMailboxSearch\\LinkUtils.cs", "GetOwaService", 207)
					where x.IsFrontEnd
					select x).FirstOrDefault<OwaService>();
				}
				if (owaService == null)
				{
					Factory.Current.EventLog.LogEvent(InfoWorkerEventLogConstants.Tuple_DiscoveryFailedToGetOWAService, null, new object[]
					{
						principal.MailboxInfo.Location.ServerFqdn
					});
				}
			}
			catch (ReadTopologyTimeoutException ex)
			{
				Factory.Current.EventLog.LogEvent(InfoWorkerEventLogConstants.Tuple_DiscoveryFailedToGetOWAServiceWithException, null, new object[]
				{
					principal.MailboxInfo.Location.ServerFqdn,
					ex.ToString()
				});
			}
			return owaService;
		}

		// Token: 0x06000EA5 RID: 3749 RVA: 0x00040914 File Offset: 0x0003EB14
		public static Uri AppendRelativePath(Uri baseUri, string relativePath, bool terminateWithSlash)
		{
			UriBuilder uriBuilder = new UriBuilder(baseUri);
			uriBuilder.Path = VirtualPathUtility.Combine(VirtualPathUtility.AppendTrailingSlash(uriBuilder.Path), relativePath);
			if (terminateWithSlash)
			{
				uriBuilder.Path = VirtualPathUtility.AppendTrailingSlash(uriBuilder.Path);
			}
			return uriBuilder.Uri;
		}

		// Token: 0x06000EA6 RID: 3750 RVA: 0x0004095C File Offset: 0x0003EB5C
		public static Uri AppendQueryString(Uri baseUri, IDictionary<string, string> queryParameters)
		{
			UriBuilder uriBuilder = new UriBuilder(baseUri);
			if (queryParameters != null && queryParameters.Count > 0)
			{
				NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(baseUri.Query);
				foreach (KeyValuePair<string, string> keyValuePair in queryParameters)
				{
					nameValueCollection[keyValuePair.Key] = keyValuePair.Value;
				}
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < nameValueCollection.Count; i++)
				{
					stringBuilder.AppendFormat("{0}{1}={2}", (i > 0) ? "&" : string.Empty, nameValueCollection.AllKeys[i], Uri.EscapeDataString(nameValueCollection[i]));
				}
				uriBuilder.Query = stringBuilder.ToString();
			}
			return uriBuilder.Uri;
		}

		// Token: 0x040009F0 RID: 2544
		private static readonly Dictionary<string, string> itemLinkParameters = new Dictionary<string, string>
		{
			{
				"viewmodel",
				"ItemReadingPaneViewModelPopOutFactory"
			},
			{
				"IsDiscoveryView",
				"1"
			},
			{
				"exsvurl",
				"1"
			}
		};
	}
}
