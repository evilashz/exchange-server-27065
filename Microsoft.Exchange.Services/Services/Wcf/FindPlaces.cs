using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Search;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Diagnostics;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000913 RID: 2323
	internal class FindPlaces : ServiceCommand<IAsyncResult>
	{
		// Token: 0x0600434F RID: 17231 RVA: 0x000E2070 File Offset: 0x000E0270
		public FindPlaces(CallContext callContext, FindPlacesRequest request, AsyncCallback asyncCallback, object asyncState) : base(callContext)
		{
			this.request = request;
			if (this.request.MaxResults <= 0 || this.request.MaxResults > 200)
			{
				this.request.MaxResults = 200;
			}
			OwsLogRegistry.Register(FindPlaces.FindPlacesActionName, typeof(FindPlacesMetadata), new Type[0]);
			this.asyncResult = new FindPlacesAsyncResult(asyncCallback, asyncState, this.request.MaxResults, callContext);
		}

		// Token: 0x17000F74 RID: 3956
		// (get) Token: 0x06004350 RID: 17232 RVA: 0x000E20EF File Offset: 0x000E02EF
		private string ClientIpAddress
		{
			get
			{
				if (this.clientIpAddress == null)
				{
					this.clientIpAddress = this.GetClientIpAddress();
				}
				return this.clientIpAddress;
			}
		}

		// Token: 0x17000F75 RID: 3957
		// (get) Token: 0x06004351 RID: 17233 RVA: 0x000E210C File Offset: 0x000E030C
		private bool HasGeoCoordinates
		{
			get
			{
				return this.request.Latitude != null && this.request.Longitude != null;
			}
		}

		// Token: 0x06004352 RID: 17234 RVA: 0x000E2144 File Offset: 0x000E0344
		protected override IAsyncResult InternalExecute()
		{
			if (!PlacesConfigurationCache.Instance.IsFeatureEnabled)
			{
				ExTraceGlobals.FindPlacesCallTracer.TraceError<FindPlaces>((long)this.GetHashCode(), "{0}: Places feature is disabled.", this);
				this.asyncResult.CompleteWithFault(FaultExceptionUtilities.CreateFault(new LocationServicesDisabledException(), FaultParty.Sender, ExchangeVersion.Current));
				return this.asyncResult;
			}
			base.CallContext.ProtocolLog.Set(FindPlacesMetadata.QueryString, this.request.Query);
			if (this.HasGeoCoordinates)
			{
				double? latitude = this.request.Latitude;
				if (latitude.GetValueOrDefault() <= 90.0 || latitude == null)
				{
					double? latitude2 = this.request.Latitude;
					if (latitude2.GetValueOrDefault() >= -90.0 || latitude2 == null)
					{
						double? longitude = this.request.Longitude;
						if (longitude.GetValueOrDefault() <= 180.0 || longitude == null)
						{
							double? longitude2 = this.request.Longitude;
							if (longitude2.GetValueOrDefault() >= -180.0 || longitude2 == null)
							{
								goto IL_17A;
							}
						}
					}
				}
				ExTraceGlobals.FindPlacesCallTracer.TraceError<FindPlaces, double?, double?>((long)this.GetHashCode(), "{0}: Invalid geo-coordinates: {1}, {2}", this, this.request.Latitude, this.request.Longitude);
				this.asyncResult.CompleteWithFault(FaultExceptionUtilities.CreateFault(new LocationServicesInvalidRequestException(CoreResources.IDs.ErrorLocationServicesInvalidQuery), FaultParty.Sender, ExchangeVersion.Current));
				return this.asyncResult;
			}
			IL_17A:
			if (this.request.Sources.Contains(PlacesSource.History))
			{
				this.ProcessHistoryRequest();
			}
			else if (string.IsNullOrEmpty(this.request.Query) && this.request.Sources != PlacesSource.PhonebookServices)
			{
				this.asyncResult.CompleteWithFault(FaultExceptionUtilities.CreateFault(new LocationServicesInvalidRequestException(CoreResources.IDs.ErrorLocationServicesInvalidQuery), FaultParty.Sender, ExchangeVersion.Current));
			}
			else
			{
				if (this.request.Sources.Contains(PlacesSource.LocationServices))
				{
					this.asyncResult.InitializeLocationRequest();
				}
				if (this.request.Sources.Contains(PlacesSource.PhonebookServices))
				{
					this.asyncResult.InitializePhonebookRequest();
				}
				if (this.asyncResult.LocationAsyncState != null || this.asyncResult.PhonebookAsyncState != null)
				{
					this.asyncResult.StartTimeoutDetection();
				}
				if (this.asyncResult.LocationAsyncState != null)
				{
					this.BeginLocationRequest(this.asyncResult.LocationAsyncState);
				}
				if (this.asyncResult.PhonebookAsyncState != null)
				{
					this.BeginPhonebookRequest(this.asyncResult.PhonebookAsyncState);
				}
			}
			return this.asyncResult;
		}

		// Token: 0x06004353 RID: 17235 RVA: 0x000E23D8 File Offset: 0x000E05D8
		private static Persona CreatePlaceFromLocationService(string displayName, string street, string city, string state, string country, string postalCode, Microsoft.Exchange.Services.Wcf.Types.LocationSource locationSource, string locationUri, double? latitude, double? longitude, string phoneNumber, string businessHomePage, string personalHomePage)
		{
			Microsoft.Exchange.Services.Core.Types.PostalAddress value = new Microsoft.Exchange.Services.Core.Types.PostalAddress
			{
				Type = PostalAddressType.Business,
				Street = street,
				City = city,
				State = state,
				Country = country,
				PostalCode = postalCode,
				LocationSource = (LocationSourceType)locationSource,
				LocationUri = locationUri,
				Latitude = latitude,
				Longitude = longitude
			};
			Persona persona = new Persona();
			persona.DisplayName = displayName;
			persona.BusinessAddresses = new PostalAddressAttributedValue[]
			{
				new PostalAddressAttributedValue(value, FindPlaces.GenericAttribution)
			};
			if (!string.IsNullOrEmpty(phoneNumber))
			{
				persona.BusinessPhoneNumbers = new PhoneNumberAttributedValue[]
				{
					new PhoneNumberAttributedValue(new PhoneNumber(phoneNumber, PersonPhoneNumberType.Business), FindPlaces.GenericAttribution)
				};
			}
			if (!string.IsNullOrEmpty(personalHomePage))
			{
				persona.PersonalHomePages = new StringAttributedValue[]
				{
					new StringAttributedValue(personalHomePage, FindPlaces.GenericAttribution)
				};
			}
			if (!string.IsNullOrEmpty(businessHomePage))
			{
				persona.BusinessHomePages = new StringAttributedValue[]
				{
					new StringAttributedValue(businessHomePage, FindPlaces.GenericAttribution)
				};
			}
			persona.PersonaType = PersonaTypeConverter.ToString(PersonType.Place);
			return persona;
		}

		// Token: 0x06004354 RID: 17236 RVA: 0x000E24F0 File Offset: 0x000E06F0
		private static HttpWebRequest CreateBaseWebRequest(string url)
		{
			HttpWebRequest httpWebRequest = WebRequest.Create(url) as HttpWebRequest;
			httpWebRequest.Method = "GET";
			httpWebRequest.Accept = "*/*";
			httpWebRequest.Credentials = null;
			httpWebRequest.AllowAutoRedirect = true;
			httpWebRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)";
			Server localServer = LocalServerCache.LocalServer;
			if (localServer != null && localServer.InternetWebProxy != null)
			{
				httpWebRequest.Proxy = new WebProxy(localServer.InternetWebProxy, true);
			}
			return httpWebRequest;
		}

		// Token: 0x06004355 RID: 17237 RVA: 0x000E2562 File Offset: 0x000E0762
		private static T GetPropertyValueOrDefault<T>(object property, T defaultValue)
		{
			if (property != null && !(property is PropertyError))
			{
				return (T)((object)property);
			}
			return defaultValue;
		}

		// Token: 0x06004356 RID: 17238 RVA: 0x000E2578 File Offset: 0x000E0778
		private string GetClientIpAddress()
		{
			IPAddress ipaddress;
			IPAddress ipaddress2;
			GccUtils.GetClientIPEndpointsFromHttpRequest(base.CallContext.HttpContext, out ipaddress, out ipaddress2, true, false);
			string result = string.Empty;
			AddressFamily addressFamily = ipaddress.AddressFamily;
			if (addressFamily != AddressFamily.InterNetwork)
			{
				if (addressFamily == AddressFamily.InterNetworkV6)
				{
					if (ipaddress.IsIPv6LinkLocal || ipaddress.ScopeId != 0L)
					{
						ExTraceGlobals.FindPlacesCallTracer.TraceDebug<FindPlaces, IPAddress>((long)this.GetHashCode(), "{0}: Ignoring IPv6: {1}", this, ipaddress);
					}
					else
					{
						result = ipaddress.ToString();
					}
				}
			}
			else
			{
				byte[] addressBytes = ipaddress.GetAddressBytes();
				byte b = addressBytes[0];
				byte b2 = addressBytes[1];
				if (b == 10 || (b == 172 && b2 >= 16 && b2 <= 31) || (b == 192 && b2 == 168))
				{
					ExTraceGlobals.FindPlacesCallTracer.TraceDebug<FindPlaces, IPAddress>((long)this.GetHashCode(), "{0}: Ignoring IPv4: {1}", this, ipaddress);
				}
				else
				{
					result = ipaddress.ToString();
				}
			}
			return result;
		}

		// Token: 0x06004357 RID: 17239 RVA: 0x000E2650 File Offset: 0x000E0850
		private StringBuilder GetRequestUrl(string baseRequestUrl, string keyParameter, string keyValue, string positionParameter, string cultureParameter)
		{
			StringBuilder stringBuilder = new StringBuilder(baseRequestUrl);
			stringBuilder.AppendFormat(keyParameter, keyValue);
			stringBuilder.AppendFormat("&query={0}", HttpUtility.UrlEncode(this.request.Query));
			if (this.HasGeoCoordinates)
			{
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, positionParameter, new object[]
				{
					this.request.Latitude,
					this.request.Longitude
				});
			}
			if (!string.IsNullOrEmpty(this.request.Culture))
			{
				stringBuilder.AppendFormat(cultureParameter, this.request.Culture);
			}
			return stringBuilder;
		}

		// Token: 0x06004358 RID: 17240 RVA: 0x000E26F4 File Offset: 0x000E08F4
		private HttpWebRequest CreateWebRequest(string url)
		{
			HttpWebRequest httpWebRequest = FindPlaces.CreateBaseWebRequest(url);
			if (!string.IsNullOrEmpty(this.ClientIpAddress))
			{
				httpWebRequest.Headers.Add("Search-Client-IP", this.ClientIpAddress);
			}
			return httpWebRequest;
		}

		// Token: 0x06004359 RID: 17241 RVA: 0x000E272C File Offset: 0x000E092C
		private void BeginPhonebookRequest(BingRequestAsyncState phonebookAsyncState)
		{
			StringBuilder requestUrl = this.GetRequestUrl(PlacesConfigurationCache.Instance.PhonebookServicesUrl, "&AppId={0}", PlacesConfigurationCache.Instance.PhonebookServicesKey, "&Latitude={0}&Longitude={1}", "&Market={0}");
			requestUrl.AppendFormat("&Phonebook.Count={0}", Math.Min(this.request.MaxResults, 25));
			if (!string.IsNullOrEmpty(this.request.Id))
			{
				requestUrl.AppendFormat("&Phonebook.LocID={0}", this.request.Id);
			}
			phonebookAsyncState.Request = this.CreateWebRequest(requestUrl.ToString());
			ExTraceGlobals.FindPlacesCallTracer.TraceDebug<FindPlaces, Uri>((long)this.GetHashCode(), "{0}: Phonebook request URL: {1}", this, phonebookAsyncState.Request.Address);
			ExTraceGlobals.FindPlacesCallTracer.TraceDebug<FindPlaces, WebHeaderCollection>((long)this.GetHashCode(), "{0}: Phonebook request headers: {1}", this, phonebookAsyncState.Request.Headers);
			this.BeginProcessBingRequest<PhonebookServicesResponse>(phonebookAsyncState);
		}

		// Token: 0x0600435A RID: 17242 RVA: 0x000E280C File Offset: 0x000E0A0C
		private void BeginLocationRequest(BingRequestAsyncState locationAsyncState)
		{
			StringBuilder requestUrl = this.GetRequestUrl(PlacesConfigurationCache.Instance.LocationServicesUrl, "&key={0}", PlacesConfigurationCache.Instance.LocationServicesKey, "&userLocation={0},{1}", "&c={0}");
			if (!this.HasGeoCoordinates && !string.IsNullOrEmpty(this.ClientIpAddress))
			{
				requestUrl.AppendFormat("&userIp={0}", this.ClientIpAddress);
			}
			locationAsyncState.Request = this.CreateWebRequest(requestUrl.ToString());
			ExTraceGlobals.FindPlacesCallTracer.TraceDebug<FindPlaces, Uri>((long)this.GetHashCode(), "{0}: Location request URL: {1}", this, locationAsyncState.Request.Address);
			ExTraceGlobals.FindPlacesCallTracer.TraceDebug<FindPlaces, WebHeaderCollection>((long)this.GetHashCode(), "{0}: Location request headers: {1}", this, locationAsyncState.Request.Headers);
			this.BeginProcessBingRequest<LocationServicesResponse>(locationAsyncState);
		}

		// Token: 0x0600435B RID: 17243 RVA: 0x000E2914 File Offset: 0x000E0B14
		private void BeginProcessBingRequest<TBingResponse>(BingRequestAsyncState bingRequestAsyncState)
		{
			bingRequestAsyncState.Begin();
			this.ExecuteActionAndHandleException(delegate
			{
				bingRequestAsyncState.Request.BeginGetResponse(new AsyncCallback(this.ResponseCallback<TBingResponse>), bingRequestAsyncState);
			}, delegate(Exception e)
			{
				bingRequestAsyncState.InProgress(this.CallContext, e);
			});
		}

		// Token: 0x0600435C RID: 17244 RVA: 0x000E2C64 File Offset: 0x000E0E64
		private void ResponseCallback<TBingResponse>(IAsyncResult asynchronousResult)
		{
			BingRequestAsyncState placesAsyncState = (BingRequestAsyncState)asynchronousResult.AsyncState;
			this.ExecuteActionAndHandleException(delegate
			{
				using (HttpWebResponse httpWebResponse = (HttpWebResponse)placesAsyncState.Request.EndGetResponse(asynchronousResult))
				{
					using (Stream responseStream = httpWebResponse.GetResponseStream())
					{
						this.CallContext.ProtocolLog.Set(placesAsyncState.StatusCodeTag, (int)httpWebResponse.StatusCode);
						DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(typeof(TBingResponse));
						IBingResultSet bingResultSet = (IBingResultSet)dataContractJsonSerializer.ReadObject(responseStream);
						if (bingResultSet != null)
						{
							if (bingResultSet.Errors != null && bingResultSet.Errors.Length > 0)
							{
								this.CallContext.ProtocolLog.Set(placesAsyncState.ErrorCodeTag, bingResultSet.Errors[0].Code);
								this.CallContext.ProtocolLog.Set(placesAsyncState.RequestFailedTag, "True");
								this.CallContext.ProtocolLog.Set(placesAsyncState.ErrorMessageTag, bingResultSet.Errors[0].Message);
								if (bingResultSet.Results == null || bingResultSet.Results.Length == 0)
								{
									throw new WebException(bingResultSet.Errors[0].Message);
								}
							}
							if (bingResultSet.Results != null)
							{
								this.CallContext.ProtocolLog.Set(placesAsyncState.ResultsCountTag, bingResultSet.Results.Length);
								foreach (IBingResult bingResult in bingResultSet.Results)
								{
									if (!string.IsNullOrEmpty(this.request.Id) && this.request.Id != bingResult.LocationUri)
									{
										break;
									}
									Persona item = FindPlaces.CreatePlaceFromLocationService(bingResult.Name, bingResult.StreetAddress, bingResult.City, bingResult.State, bingResult.Country, bingResult.PostalCode, bingResult.LocationSource, bingResult.LocationUri, new double?(bingResult.Latitude), new double?(bingResult.Longitude), bingResult.PhoneNumber, bingResult.BusinessHomePage, bingResult.LocalHomePage);
									placesAsyncState.ResultsList.Add(item);
									if (placesAsyncState.ResultsList.Count >= this.request.MaxResults)
									{
										break;
									}
								}
							}
						}
					}
				}
			}, delegate(Exception e)
			{
				placesAsyncState.End(this.CallContext, e);
			});
		}

		// Token: 0x0600435D RID: 17245 RVA: 0x000E2CBC File Offset: 0x000E0EBC
		private void ProcessHistoryRequest()
		{
			if (this.request.Sources != PlacesSource.History)
			{
				this.asyncResult.CompleteWithFault(FaultExceptionUtilities.CreateFault(new LocationServicesInvalidRequestException(CoreResources.IDs.ErrorLocationServicesInvalidSource), FaultParty.Receiver, ExchangeVersion.Current));
				return;
			}
			AndFilter queryFilter = FindPlaces.isPlace;
			string text = string.IsNullOrEmpty(this.request.Query) ? null : this.request.Query.Trim();
			if (!string.IsNullOrEmpty(text))
			{
				queryFilter = new AndFilter(new QueryFilter[]
				{
					FindPlaces.isPlace,
					new TextFilter(ContactSchema.GivenName, text, MatchOptions.Prefix, MatchFlags.Loose)
				});
			}
			MailboxSession mailboxIdentityMailboxSession = base.CallContext.SessionCache.GetMailboxIdentityMailboxSession();
			using (Folder folder = Folder.Bind(mailboxIdentityMailboxSession, mailboxIdentityMailboxSession.GetDefaultFolderId(DefaultFolderType.Location)))
			{
				using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.None, queryFilter, FindPlaces.historySortOrder, FindPlaces.propertiesToPreload))
				{
					object[][] rows = queryResult.GetRows(this.request.MaxResults);
					ExTraceGlobals.FindPlacesCallTracer.TraceDebug<FindPlaces, int>((long)this.GetHashCode(), "{0}: History query returned {1} results.", this, rows.Length);
					ExDateTime t = ExDateTime.UtcNow.AddYears(-1);
					List<Persona> list = new List<Persona>();
					for (int i = 0; i < rows.Length; i++)
					{
						Persona persona = null;
						Microsoft.Exchange.Services.Wcf.Types.LocationSource propertyValueOrDefault = FindPlaces.GetPropertyValueOrDefault<Microsoft.Exchange.Services.Wcf.Types.LocationSource>(rows[i][1], Microsoft.Exchange.Services.Wcf.Types.LocationSource.None);
						if (propertyValueOrDefault == Microsoft.Exchange.Services.Wcf.Types.LocationSource.LocationServices || propertyValueOrDefault == Microsoft.Exchange.Services.Wcf.Types.LocationSource.PhonebookServices)
						{
							ExDateTime? propertyValueOrDefault2 = FindPlaces.GetPropertyValueOrDefault<ExDateTime?>(rows[i][10], null);
							if (propertyValueOrDefault2 != null && propertyValueOrDefault2 < t)
							{
								ExTraceGlobals.FindPlacesCallTracer.TraceDebug<FindPlaces, ExDateTime?>((long)this.GetHashCode(), "{0}: Skipping item due to expired time stamp {1}", this, propertyValueOrDefault2);
							}
							else
							{
								persona = FindPlaces.CreatePlaceFromLocationService(FindPlaces.GetPropertyValueOrDefault<string>(rows[i][0], null), FindPlaces.GetPropertyValueOrDefault<string>(rows[i][3], null), FindPlaces.GetPropertyValueOrDefault<string>(rows[i][4], null), FindPlaces.GetPropertyValueOrDefault<string>(rows[i][5], null), FindPlaces.GetPropertyValueOrDefault<string>(rows[i][6], null), FindPlaces.GetPropertyValueOrDefault<string>(rows[i][7], null), propertyValueOrDefault, FindPlaces.GetPropertyValueOrDefault<string>(rows[i][2], null), FindPlaces.GetPropertyValueOrDefault<double?>(rows[i][8], null), FindPlaces.GetPropertyValueOrDefault<double?>(rows[i][9], null), null, null, null);
							}
						}
						if (persona != null)
						{
							list.Add(persona);
						}
					}
					this.asyncResult.CompleteSuccessfully(list.ToArray());
				}
			}
		}

		// Token: 0x0600435E RID: 17246 RVA: 0x000E2FF4 File Offset: 0x000E11F4
		private void ExecuteActionAndHandleException(Action operation, Action<Exception> onOperationCompleted)
		{
			Exception exception = null;
			try
			{
				GrayException.MapAndReportGrayExceptions(delegate()
				{
					try
					{
						operation();
					}
					catch (WebException exception)
					{
						WebException exception = exception;
					}
					catch (ObjectDisposedException exception2)
					{
						WebException exception = exception2;
					}
					catch (InvalidOperationException exception3)
					{
						WebException exception = exception3;
					}
					catch (SerializationException exception4)
					{
						WebException exception = exception4;
					}
				});
			}
			catch (GrayException ex)
			{
				ExTraceGlobals.FindPlacesCallTracer.TraceError<FindPlaces, string>((long)this.GetHashCode(), "{0}: Request failed due to gray exception {1}", this, ex.Message);
				exception = ex;
			}
			if (exception != null)
			{
				ExTraceGlobals.FindPlacesCallTracer.TraceError<FindPlaces, Exception>((long)this.GetHashCode(), "{0}: Request failed due to exception {1}", this, exception);
			}
			onOperationCompleted(exception);
		}

		// Token: 0x04002736 RID: 10038
		private const string LocationKeyParameter = "&key={0}";

		// Token: 0x04002737 RID: 10039
		private const string PhonebookKeyParameter = "&AppId={0}";

		// Token: 0x04002738 RID: 10040
		private const string QueryParameter = "&query={0}";

		// Token: 0x04002739 RID: 10041
		private const string CountParameter = "&Phonebook.Count={0}";

		// Token: 0x0400273A RID: 10042
		private const string PhonebookIdParameter = "&Phonebook.LocID={0}";

		// Token: 0x0400273B RID: 10043
		private const string LocationGeoCoordinatesParameter = "&userLocation={0},{1}";

		// Token: 0x0400273C RID: 10044
		private const string LocationIpParameter = "&userIp={0}";

		// Token: 0x0400273D RID: 10045
		private const string PhonebookGeoCoordinatesParameter = "&Latitude={0}&Longitude={1}";

		// Token: 0x0400273E RID: 10046
		private const string LocationCultureParameter = "&c={0}";

		// Token: 0x0400273F RID: 10047
		private const string PhonebookCultureParameter = "&Market={0}";

		// Token: 0x04002740 RID: 10048
		private const string ClientIpAddressBingServicesHeader = "Search-Client-IP";

		// Token: 0x04002741 RID: 10049
		private const int DefaultNumberOfResults = 200;

		// Token: 0x04002742 RID: 10050
		private const int DefaultNumberOfResultsForPhonebook = 25;

		// Token: 0x04002743 RID: 10051
		private static readonly TargetFolderId rootFolderId = new TargetFolderId(new DistinguishedFolderId
		{
			Id = DistinguishedFolderIdName.root
		});

		// Token: 0x04002744 RID: 10052
		private static readonly IndexedPageView pageViewForOneResult = new IndexedPageView
		{
			Origin = BasePagingType.PagingOrigin.Beginning,
			Offset = 0,
			MaxRows = 1
		};

		// Token: 0x04002745 RID: 10053
		private static readonly PropertyDefinition[] propertiesToPreload = new PropertyDefinition[]
		{
			ContactSchema.GivenName,
			ContactSchema.WorkLocationSource,
			ContactSchema.WorkLocationUri,
			ContactSchema.WorkAddressStreet,
			ContactSchema.WorkAddressCity,
			ContactSchema.WorkAddressState,
			ContactSchema.WorkAddressCountry,
			ContactSchema.WorkAddressPostalCode,
			ContactSchema.WorkLatitude,
			ContactSchema.WorkLongitude,
			StoreObjectSchema.LastModifiedTime
		};

		// Token: 0x04002746 RID: 10054
		private static readonly SortBy[] historySortOrder = new SortBy[]
		{
			new SortBy(PlaceSchema.LocationRelevanceRank, SortOrder.Descending),
			new SortBy(ContactSchema.GivenName, SortOrder.Ascending)
		};

		// Token: 0x04002747 RID: 10055
		private static readonly AndFilter isPlace = new AndFilter(new QueryFilter[]
		{
			new ExistsFilter(ContactSchema.WorkLocationUri),
			new ExistsFilter(StoreObjectSchema.ItemClass),
			new ComparisonFilter(ComparisonOperator.Equal, StoreObjectSchema.ItemClass, "IPM.Contact.Place")
		});

		// Token: 0x04002748 RID: 10056
		private static readonly string[] GenericAttribution = new string[]
		{
			"0"
		};

		// Token: 0x04002749 RID: 10057
		private static readonly string FindPlacesActionName = typeof(FindPlaces).Name;

		// Token: 0x0400274A RID: 10058
		private readonly FindPlacesRequest request;

		// Token: 0x0400274B RID: 10059
		private readonly FindPlacesAsyncResult asyncResult;

		// Token: 0x0400274C RID: 10060
		private string clientIpAddress;

		// Token: 0x02000914 RID: 2324
		private enum LocationPropertiesIndex
		{
			// Token: 0x0400274E RID: 10062
			DisplayName,
			// Token: 0x0400274F RID: 10063
			WorkSource,
			// Token: 0x04002750 RID: 10064
			WorkUri,
			// Token: 0x04002751 RID: 10065
			WorkStreet,
			// Token: 0x04002752 RID: 10066
			WorkCity,
			// Token: 0x04002753 RID: 10067
			WorkState,
			// Token: 0x04002754 RID: 10068
			WorkCountry,
			// Token: 0x04002755 RID: 10069
			WorkPostalCode,
			// Token: 0x04002756 RID: 10070
			WorkLatitude,
			// Token: 0x04002757 RID: 10071
			WorkLongitude,
			// Token: 0x04002758 RID: 10072
			LastModifiedTime
		}
	}
}
