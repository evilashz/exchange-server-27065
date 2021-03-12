using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x02000077 RID: 119
	internal class SourceDataProviderManager
	{
		// Token: 0x060007D1 RID: 2001 RVA: 0x0001DD80 File Offset: 0x0001BF80
		public SourceDataProviderManager(IServiceClientFactory serviceClientFactory, CancellationToken abortTokenForTasks)
		{
			this.serviceClientFactory = serviceClientFactory;
			this.abortTokenForTasks = abortTokenForTasks;
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060007D2 RID: 2002 RVA: 0x0001DD96 File Offset: 0x0001BF96
		// (set) Token: 0x060007D3 RID: 2003 RVA: 0x0001DD9E File Offset: 0x0001BF9E
		public IProgressController ProgressController { get; set; }

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060007D4 RID: 2004 RVA: 0x0001DDA8 File Offset: 0x0001BFA8
		private bool IsStopRequested
		{
			get
			{
				return this.abortTokenForTasks.IsCancellationRequested;
			}
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x0001DE20 File Offset: 0x0001C020
		public void AutoDiscoverSourceServiceEndpoints(SourceInformationCollection allSourceInformation, Uri configurationServiceEndpoint, ICredentialHandler credentialHandler)
		{
			this.serviceClientFactory.CredentialHandler = credentialHandler;
			if (configurationServiceEndpoint != null && configurationServiceEndpoint.IsAbsoluteUri && configurationServiceEndpoint.Scheme == Uri.UriSchemeHttps)
			{
				if (this.IsStopRequested)
				{
					Tracer.TraceInformation("SourceDataProviderManager.CreateServiceClients: Stop requested when trying to auto discover with configuration service server", new object[0]);
					return;
				}
				Uri autoDiscoverUrl = new Uri(configurationServiceEndpoint.GetLeftPart(UriPartial.Authority) + "/autodiscover/autodiscover.svc");
				this.AutoDiscoverServiceEndpoints((from sourceInformation in allSourceInformation.Values
				where this.IsServiceEndpointNeededForSource(sourceInformation)
				select sourceInformation).ToList<SourceInformation>(), autoDiscoverUrl, 0);
				if (this.IsStopRequested)
				{
					Tracer.TraceInformation("SourceDataProviderManager.CreateServiceClients: Stop requested when trying to auto discover with configuration service endpoint", new object[0]);
					return;
				}
				using (IServiceClient<ISourceDataProvider> serviceClient = this.serviceClientFactory.CreateSourceDataProvider(configurationServiceEndpoint, this.abortTokenForTasks))
				{
					if (serviceClient.Connect())
					{
						SourceDataProviderManager.VerifyAndSetServiceEndpoint((from sourceInformation in allSourceInformation.Values
						where this.IsServiceEndpointNeededForSource(sourceInformation)
						select sourceInformation).ToList<SourceInformation>(), serviceClient);
					}
				}
			}
			IEnumerable<IGrouping<string, SourceInformation>> enumerable = from sourceInformation in allSourceInformation.Values
			where this.IsServiceEndpointNeededForSource(sourceInformation)
			group sourceInformation by SourceDataProviderManager.GetDomainFromSmtpEmailAddress(sourceInformation.Configuration.Id.StartsWith("\\") ? sourceInformation.Configuration.Name : sourceInformation.Configuration.Id);
			foreach (IGrouping<string, SourceInformation> grouping in enumerable)
			{
				if (this.IsStopRequested)
				{
					Tracer.TraceInformation("SourceDataProviderManager.CreateServiceClients: Stop requested when trying to auto discover with email domains of source mailboxes.", new object[0]);
					return;
				}
				this.AutoDiscoverServiceEndpointsWithEmailDomain(grouping.ToList<SourceInformation>(), grouping.Key.ToLowerInvariant());
			}
			SourceInformation sourceInformation2 = allSourceInformation.Values.FirstOrDefault((SourceInformation sourceInformation) => this.IsServiceEndpointNeededForSource(sourceInformation));
			if (sourceInformation2 != null && !this.IsStopRequested)
			{
				throw new ExportException(ExportErrorType.FailedToAutoDiscoverExchangeWebServiceUrl, sourceInformation2.Configuration.Id);
			}
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x0001E01C File Offset: 0x0001C21C
		public void CreateSourceServiceClients(SourceInformationCollection allSourceInformation)
		{
			Dictionary<Uri, IServiceClient<ISourceDataProvider>> dictionary = new Dictionary<Uri, IServiceClient<ISourceDataProvider>>(allSourceInformation.Count);
			foreach (SourceInformation sourceInformation in allSourceInformation.Values)
			{
				if (this.IsStopRequested)
				{
					Tracer.TraceInformation("SourceDataProviderManager.CreateServiceClients: Stop requested when creating service clients.", new object[0]);
					return;
				}
				if (this.IsServiceClientNeededForSource(sourceInformation))
				{
					IServiceClient<ISourceDataProvider> serviceClient;
					if (!dictionary.TryGetValue(sourceInformation.Configuration.ServiceEndpoint, out serviceClient))
					{
						serviceClient = this.serviceClientFactory.CreateSourceDataProvider(sourceInformation.Configuration.ServiceEndpoint, this.abortTokenForTasks);
						if (!serviceClient.Connect())
						{
							throw new ExportException(ExportErrorType.Unauthorized, sourceInformation.Configuration.ServiceEndpoint.ToString());
						}
						dictionary.Add(sourceInformation.Configuration.ServiceEndpoint, serviceClient);
					}
					sourceInformation.ServiceClient = serviceClient.FunctionalInterface;
				}
			}
			Tracer.TraceInformation("SourceDataProviderManager.CreateSourceServiceClients: number of service clients created: {0}", new object[]
			{
				dictionary.Count
			});
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x0001E12C File Offset: 0x0001C32C
		private static void VerifyAndSetServiceEndpoint(IEnumerable<SourceInformation> sourcesToVerify, IServiceClient<ISourceDataProvider> serviceClient)
		{
			foreach (SourceInformation sourceInformation in sourcesToVerify)
			{
				try
				{
					ISourceDataProvider functionalInterface = serviceClient.FunctionalInterface;
					string rootFolder = functionalInterface.GetRootFolder(sourceInformation.Configuration.Id.StartsWith("\\") ? sourceInformation.Configuration.Name : sourceInformation.Configuration.Id, false);
					if (!string.IsNullOrEmpty(rootFolder))
					{
						sourceInformation.Configuration.ServiceEndpoint = serviceClient.ServiceEndpoint;
					}
				}
				catch (ExportException)
				{
				}
			}
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x0001E1D8 File Offset: 0x0001C3D8
		private static string GetDomainFromSmtpEmailAddress(string email)
		{
			return email.Split(new char[]
			{
				'@'
			})[1].ToLowerInvariant();
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x0001E208 File Offset: 0x0001C408
		private void AutoDiscoverServiceEndpointsWithEmailDomain(IList<SourceInformation> sources, string emailDomain)
		{
			IList<SourceInformation> list = sources;
			string[] array = new string[]
			{
				string.Format("https://autodiscover.{0}/autodiscover/autodiscover.svc", emailDomain),
				string.Format("http://autodiscover.{0}/autodiscover/autodiscover.svc", emailDomain),
				string.Format("https://{0}/autodiscover/autodiscover.svc", emailDomain),
				string.Format("http://{0}/autodiscover/autodiscover.svc", emailDomain)
			};
			int num = 0;
			while (list.Count > 0 && num < array.Length)
			{
				string uriString = array[num++];
				this.AutoDiscoverServiceEndpoints(list, new Uri(uriString), 0);
				list = (from sourceInformation in sources
				where this.IsServiceEndpointNeededForSource(sourceInformation)
				select sourceInformation).ToList<SourceInformation>();
			}
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x0001E2DC File Offset: 0x0001C4DC
		private void AutoDiscoverServiceEndpoints(IList<SourceInformation> sources, Uri autoDiscoverUrl, int retriedTimes)
		{
			if (retriedTimes >= 3 && sources != null && sources.Count > 0)
			{
				Tracer.TraceError("SourceDataProviderManager.AutoDiscoverServiceEndpoints: Failed to auto discover service endpoint for mailbox {0} after retries for transient errors.", new object[]
				{
					sources[0].Configuration.Id
				});
				return;
			}
			if (autoDiscoverUrl != null)
			{
				List<SourceInformation> list = new List<SourceInformation>(ConstantProvider.AutoDiscoverBatchSize);
				for (int i = 0; i < sources.Count; i++)
				{
					if (this.IsStopRequested)
					{
						Tracer.TraceInformation("SourceDataProviderManager.AutoDiscoverServiceEndpoints: Stop requested when auto discovering from '{0}'.", new object[]
						{
							autoDiscoverUrl
						});
						return;
					}
					list.Add(sources[i]);
					if (list.Count >= ConstantProvider.AutoDiscoverBatchSize || i >= sources.Count - 1)
					{
						IServiceClient<IAutoDiscoverClient> serviceClient = this.serviceClientFactory.CreateAutoDiscoverClient(autoDiscoverUrl, this.abortTokenForTasks);
						List<AutoDiscoverResult> userEwsEndpoints;
						try
						{
							userEwsEndpoints = serviceClient.FunctionalInterface.GetUserEwsEndpoints(list.Select(delegate(SourceInformation sourceInformation)
							{
								if (!sourceInformation.Configuration.Id.StartsWith("\\"))
								{
									return sourceInformation.Configuration.Id;
								}
								return sourceInformation.Configuration.Name;
							}));
						}
						catch (ExportException ex)
						{
							Tracer.TraceError("SourceDataProviderManager.AutoDiscoverServiceEndpoints: Exeption happened when calling GetUserEwsEndpoints. Exception: {0}", new object[]
							{
								ex
							});
							list.Clear();
							break;
						}
						Dictionary<Uri, List<SourceInformation>> dictionary = new Dictionary<Uri, List<SourceInformation>>();
						Dictionary<string, List<SourceInformation>> dictionary2 = new Dictionary<string, List<SourceInformation>>();
						List<SourceInformation> list2 = new List<SourceInformation>();
						for (int j = 0; j < userEwsEndpoints.Count; j++)
						{
							AutoDiscoverResult autoDiscoverResult = userEwsEndpoints[j];
							Uri uri = null;
							string text = null;
							switch (autoDiscoverResult.ResultCode)
							{
							case AutoDiscoverResultCode.Success:
								list[j].Configuration.ServiceEndpoint = new Uri(autoDiscoverResult.ResultValue);
								break;
							case AutoDiscoverResultCode.TransientError:
								list2.Add(list[j]);
								break;
							case AutoDiscoverResultCode.EmailAddressRedirected:
								text = SourceDataProviderManager.GetDomainFromSmtpEmailAddress(autoDiscoverResult.ResultValue);
								break;
							case AutoDiscoverResultCode.UrlRedirected:
								uri = new Uri(autoDiscoverResult.ResultValue);
								break;
							}
							if (uri != null)
							{
								List<SourceInformation> list3;
								if (!dictionary.TryGetValue(uri, out list3))
								{
									list3 = new List<SourceInformation>();
									dictionary.Add(uri, list3);
								}
								list3.Add(list[j]);
							}
							if (!string.IsNullOrEmpty(text))
							{
								List<SourceInformation> list4;
								if (!dictionary2.TryGetValue(text, out list4))
								{
									list4 = new List<SourceInformation>();
									dictionary2.Add(text, list4);
								}
								list4.Add(list[j]);
							}
						}
						foreach (Uri uri2 in dictionary.Keys)
						{
							Tracer.TraceInformation("SourceDataProviderManager.AutoDiscoverServiceEndpoints: Further autodiscover via {0}", new object[]
							{
								uri2
							});
							this.AutoDiscoverServiceEndpoints(dictionary[uri2], uri2, 0);
						}
						foreach (string text2 in dictionary2.Keys)
						{
							Tracer.TraceInformation("SourceDataProviderManager.AutoDiscoverServiceEndpoints: Further autodiscover with email domain {0}", new object[]
							{
								text2
							});
							this.AutoDiscoverServiceEndpointsWithEmailDomain(dictionary2[text2], text2);
						}
						if (list2.Count > 0)
						{
							Tracer.TraceInformation("SourceDataProviderManager.AutoDiscoverServiceEndpoints: Retry autodiscover via {0}, retring times: {1}", new object[]
							{
								autoDiscoverUrl,
								retriedTimes + 1
							});
							this.AutoDiscoverServiceEndpoints(list2, autoDiscoverUrl, retriedTimes + 1);
						}
						list.Clear();
					}
				}
			}
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x0001E650 File Offset: 0x0001C850
		private bool IsServiceEndpointNeededForSource(SourceInformation source)
		{
			return source.ServiceClient == null && source.Configuration.ServiceEndpoint == null;
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x0001E66D File Offset: 0x0001C86D
		private bool IsServiceClientNeededForSource(SourceInformation source)
		{
			return source.ServiceClient == null;
		}

		// Token: 0x040002E1 RID: 737
		private readonly IServiceClientFactory serviceClientFactory;

		// Token: 0x040002E2 RID: 738
		private readonly CancellationToken abortTokenForTasks;
	}
}
