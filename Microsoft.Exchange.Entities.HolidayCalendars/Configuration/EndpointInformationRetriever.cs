using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Exchange.Diagnostics.Components.Entities.HolidayCalendars;
using Microsoft.Exchange.Entities.HolidayCalendars.Configuration.Exceptions;

namespace Microsoft.Exchange.Entities.HolidayCalendars.Configuration
{
	// Token: 0x02000008 RID: 8
	internal class EndpointInformationRetriever : IEndpointInformationRetriever
	{
		// Token: 0x0600001E RID: 30 RVA: 0x000023E8 File Offset: 0x000005E8
		public EndpointInformationRetriever(Uri baseUrl, int timeout, IHolidayCalendarsService service = null)
		{
			if (baseUrl == null)
			{
				throw new ArgumentNullException("baseUrl");
			}
			this.baseUrl = baseUrl;
			this.timeout = timeout;
			this.service = (service ?? new HolidayCalendarsService());
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002450 File Offset: 0x00000650
		public EndpointInformation FetchEndpointInformation()
		{
			IReadOnlyDictionary<int, int> versionInfo = null;
			IReadOnlyList<CultureInfo> cultures = null;
			try
			{
				Action[] array = new Action[2];
				array[0] = delegate()
				{
					versionInfo = this.GetCalendarVersionMapping();
				};
				array[1] = delegate()
				{
					cultures = this.GetListOfAllCultures();
				};
				Parallel.Invoke(array);
			}
			catch (AggregateException ex)
			{
				throw ex.InnerException;
			}
			return new EndpointInformation(this.baseUrl, versionInfo, cultures, this.GetVersionFromUriPath());
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000025A0 File Offset: 0x000007A0
		private Dictionary<int, int> GetCalendarVersionMapping()
		{
			HttpWebRequest request = this.CreateRequest("calendarversions.txt");
			Dictionary<int, int> versions = new Dictionary<int, int>(120);
			try
			{
				this.service.GetResource(request, delegate(Stream response)
				{
					using (StreamReader streamReader = new StreamReader(response, Encoding.UTF8))
					{
						string text;
						while ((text = streamReader.ReadLine()) != null)
						{
							if (!string.IsNullOrWhiteSpace(text))
							{
								int[] array = (from s in text.Trim().Split(new char[]
								{
									','
								})
								select int.Parse(s, CultureInfo.InvariantCulture)).ToArray<int>();
								versions.Add(array[0], array[1]);
							}
						}
					}
				});
			}
			catch (WebException ex)
			{
				throw new EndpointConfigurationException(EndPointConfigurationError.UnableToFetchCalendarVersions, ex, "Status: {0}, Endpoint: {1}", new object[]
				{
					ex.Status.ToString(),
					this.baseUrl.AbsoluteUri
				});
			}
			return versions;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002640 File Offset: 0x00000840
		private HttpWebRequest CreateRequest(string resourceFileName)
		{
			HttpWebRequest result;
			try
			{
				Uri requestUri = new Uri(this.baseUrl, resourceFileName);
				HttpWebRequest httpWebRequest = WebRequest.Create(requestUri) as HttpWebRequest;
				if (httpWebRequest == null)
				{
					throw new EndpointConfigurationException(EndPointConfigurationError.UrlDidNotResolveToHttpRequest, "Url: {0}", new object[]
					{
						this.baseUrl.AbsoluteUri
					});
				}
				httpWebRequest.Timeout = this.timeout;
				result = httpWebRequest;
			}
			catch (NotSupportedException innerException)
			{
				throw new EndpointConfigurationException(EndPointConfigurationError.UrlSchemeNotSupported, innerException, "Url: {0}", new object[]
				{
					this.baseUrl.AbsoluteUri
				});
			}
			return result;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002770 File Offset: 0x00000970
		private List<CultureInfo> GetListOfAllCultures()
		{
			HttpWebRequest request = this.CreateRequest("cultures.txt");
			List<CultureInfo> cultures = new List<CultureInfo>(120);
			try
			{
				this.service.GetResource(request, delegate(Stream response)
				{
					using (StreamReader streamReader = new StreamReader(response, Encoding.UTF8))
					{
						string text;
						while ((text = streamReader.ReadLine()) != null)
						{
							if (!string.IsNullOrWhiteSpace(text))
							{
								try
								{
									CultureInfo cultureInfo = CultureInfo.GetCultureInfo(text.Trim());
									cultures.Add(cultureInfo);
								}
								catch (CultureNotFoundException)
								{
									ExTraceGlobals.EndpointConfigurationRetrieverTracer.TraceDebug<string>((long)this.GetHashCode(), "Unable to add culture {0} to list.", text);
								}
							}
						}
					}
				});
			}
			catch (WebException ex)
			{
				throw new EndpointConfigurationException(EndPointConfigurationError.UnableToFetchListOfCultures, ex, "Status: {0}, Endpoint: {1}", new object[]
				{
					ex.Status.ToString(),
					this.baseUrl.AbsoluteUri
				});
			}
			return cultures;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002818 File Offset: 0x00000A18
		private Version GetVersionFromUriPath()
		{
			string text = this.baseUrl.Segments.Last<string>().TrimEnd(new char[]
			{
				'/'
			});
			Version result;
			if (!Version.TryParse(text, out result))
			{
				throw new EndpointConfigurationException(EndPointConfigurationError.VersionNumberError, "Cannot parse version number. {0}", new object[]
				{
					text
				});
			}
			return result;
		}

		// Token: 0x0400000E RID: 14
		public const string CultureFileRelativePath = "cultures.txt";

		// Token: 0x0400000F RID: 15
		public const string VersionsFileRelativePath = "calendarversions.txt";

		// Token: 0x04000010 RID: 16
		private readonly Uri baseUrl;

		// Token: 0x04000011 RID: 17
		private readonly int timeout;

		// Token: 0x04000012 RID: 18
		private readonly IHolidayCalendarsService service;
	}
}
