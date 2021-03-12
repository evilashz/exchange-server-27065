using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.InfoWorker.Common.Availability;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000DB0 RID: 3504
	internal class JsonMessageHeaderProcessor : MessageHeaderProcessor
	{
		// Token: 0x06005904 RID: 22788 RVA: 0x00115D20 File Offset: 0x00113F20
		internal JsonMessageHeaderProcessor()
		{
			this.RequestVersion = null;
			this.MailboxCulture = null;
			this.TimeZoneContext = null;
			this.DateTimePrecision = null;
			this.IsBackgroundLoad = false;
			this.ManagementRoleType = null;
		}

		// Token: 0x06005905 RID: 22789 RVA: 0x00115D65 File Offset: 0x00113F65
		private static DateTimePrecision ReadDateTimePrecisionHeader(XmlDictionaryReader reader)
		{
			return JsonMessageHeaderProcessor.ReadDateTimePrecisionHeader(reader.ReadString());
		}

		// Token: 0x06005906 RID: 22790 RVA: 0x00115D74 File Offset: 0x00113F74
		protected static DateTimePrecision ReadDateTimePrecisionHeader(string value)
		{
			DateTimePrecision result;
			try
			{
				result = EnumUtilities.Parse<DateTimePrecision>(value);
			}
			catch (ArgumentException)
			{
				throw FaultExceptionUtilities.CreateFault(new ServiceInvalidOperationException((CoreResources.IDs)3468080577U), FaultParty.Sender);
			}
			return result;
		}

		// Token: 0x06005907 RID: 22791 RVA: 0x00115DB4 File Offset: 0x00113FB4
		protected static ExchangeVersion ReadRequestVersionHeader(XmlDictionaryReader reader)
		{
			return JsonMessageHeaderProcessor.ReadRequestVersionHeader(reader.ReadString());
		}

		// Token: 0x06005908 RID: 22792 RVA: 0x00115DC4 File Offset: 0x00113FC4
		protected static ExchangeVersion ReadRequestVersionHeader(string version)
		{
			if (!string.IsNullOrEmpty(version))
			{
				try
				{
					return ExchangeVersion.MapRequestVersionToServerVersion(version);
				}
				catch (InvalidServerVersionException ex)
				{
					ExTraceGlobals.CommonAlgorithmTracer.TraceError<string>(0L, "[JsonMessageHeaderProcessor::ReadRequestVersionHeader] Invalid request version value. Message: {0}", ex.Message);
				}
			}
			return null;
		}

		// Token: 0x06005909 RID: 22793 RVA: 0x00115E10 File Offset: 0x00114010
		private static TimeZoneContextType ReadTimeZoneContextHeader(XmlDictionaryReader reader)
		{
			try
			{
				XmlReader reader2 = reader.ReadSubtree();
				TimeZoneContextType timeZoneContextType = new TimeZoneContextType();
				timeZoneContextType.TimeZoneDefinition = new TimeZoneDefinitionType();
				XElement xelement = XElement.Load(reader2, LoadOptions.None);
				XElement xelement2 = xelement.Element("TimeZoneDefinition");
				if (xelement2 != null)
				{
					foreach (XElement xelement3 in xelement2.Elements())
					{
						string localName;
						if ((localName = xelement3.Name.LocalName) != null)
						{
							if (!(localName == "Id"))
							{
								if (!(localName == "Name"))
								{
									if (!(localName == "Periods") && !(localName == "TransitionsGroups") && !(localName == "Transitions"))
									{
									}
								}
								else
								{
									timeZoneContextType.TimeZoneDefinition.Name = xelement3.Value;
								}
							}
							else
							{
								timeZoneContextType.TimeZoneDefinition.Id = xelement3.Value;
							}
						}
					}
				}
				return timeZoneContextType;
			}
			catch (XmlException ex)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceError<string>(0L, "[JsonMessageHeaderProcessor::ReadTimeZoneContextHeader] Caught XmlException exception while parsing timezone header.  Message: {0}", ex.Message);
			}
			return null;
		}

		// Token: 0x0600590A RID: 22794 RVA: 0x00115F4C File Offset: 0x0011414C
		protected static ManagementRoleType ReadManagementRoleHeader(XmlDictionaryReader reader)
		{
			try
			{
				XmlReader reader2 = reader.ReadSubtree();
				XElement root = XElement.Load(reader2, LoadOptions.None);
				ManagementRoleType managementRoleType = new ManagementRoleType
				{
					UserRoles = JsonMessageHeaderProcessor.GetRolesUnder(root, "UserRoles"),
					ApplicationRoles = JsonMessageHeaderProcessor.GetRolesUnder(root, "ApplicationRoles")
				};
				managementRoleType.ValidateAndConvert();
				return managementRoleType;
			}
			catch (XmlException ex)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceError<string>(0L, "[JsonMessageHeaderProcessor::ReadManagementRoleHeader] Caught XmlException exception while parsing timezone header.  Message: {0}", ex.Message);
			}
			return null;
		}

		// Token: 0x0600590B RID: 22795 RVA: 0x00115FD8 File Offset: 0x001141D8
		private static string[] GetRolesUnder(XElement root, string name)
		{
			XElement xelement = root.Element(name);
			if (xelement == null)
			{
				return null;
			}
			return (from property in xelement.Elements()
			select property.Value).ToArray<string>();
		}

		// Token: 0x0600590C RID: 22796 RVA: 0x00116024 File Offset: 0x00114224
		private static IEnumerable<TAttribute> GetCustomAttributes<TAttribute>(MethodInfo methodInfo)
		{
			return methodInfo.GetCustomAttributes(typeof(TAttribute), false).OfType<TAttribute>();
		}

		// Token: 0x0600590D RID: 22797 RVA: 0x0011603C File Offset: 0x0011423C
		protected void ProcessRequestVersion(Message request)
		{
			if (this.RequestVersion == null || !this.RequestVersion.Supports(ExchangeVersion.Exchange2012))
			{
				throw FaultExceptionUtilities.CreateFault(new InvalidServerVersionException(CoreResources.IDs.MessageInvalidServerVersionForJsonRequest), FaultParty.Sender);
			}
			request.Properties["WS_ServerVersionKey"] = this.RequestVersion;
			ExchangeVersion.Current = this.RequestVersion;
		}

		// Token: 0x0600590E RID: 22798 RVA: 0x001160AE File Offset: 0x001142AE
		internal static HashSet<string> BuildNoHeaderProcessingMap(Type serviceType)
		{
			return JsonMessageHeaderProcessor.BuildServiceMethodMap<JsonRequestFormatAttribute>(serviceType, (JsonRequestFormatAttribute attr) => attr.Format != JsonRequestFormat.HeaderBodyFormat);
		}

		// Token: 0x0600590F RID: 22799 RVA: 0x001160E1 File Offset: 0x001142E1
		internal static HashSet<string> BuildNoHeaderQueryProcessingMap(Type serviceType)
		{
			return JsonMessageHeaderProcessor.BuildServiceMethodMap<JsonRequestFormatAttribute>(serviceType, (JsonRequestFormatAttribute attr) => attr.Format != JsonRequestFormat.QueryString);
		}

		// Token: 0x06005910 RID: 22800 RVA: 0x00116114 File Offset: 0x00114314
		internal static HashSet<string> BuildNoHeaderHttpHeaderProcessingMap(Type serviceType)
		{
			return JsonMessageHeaderProcessor.BuildServiceMethodMap<JsonRequestFormatAttribute>(serviceType, (JsonRequestFormatAttribute attr) => attr.Format != JsonRequestFormat.HttpHeaders);
		}

		// Token: 0x06005911 RID: 22801 RVA: 0x0011613C File Offset: 0x0011433C
		internal static HashSet<string> BuildServiceMethodMap<TAttr>(Type serviceType, Func<TAttr, bool> predicateFunc)
		{
			HashSet<string> hashSet = new HashSet<string>();
			BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Public;
			foreach (Type type in serviceType.GetInterfaces())
			{
				foreach (MethodInfo methodInfo in type.GetMethods(bindingAttr))
				{
					if (JsonMessageHeaderProcessor.GetCustomAttributes<OperationContractAttribute>(methodInfo).Count<OperationContractAttribute>() != 0)
					{
						foreach (TAttr arg in JsonMessageHeaderProcessor.GetCustomAttributes<TAttr>(methodInfo))
						{
							if (predicateFunc(arg))
							{
								string text = methodInfo.Name.ToLowerInvariant();
								if (text.StartsWith("begin", StringComparison.Ordinal))
								{
									text = text.Substring("begin".Length);
								}
								hashSet.Add(text);
								break;
							}
						}
					}
				}
			}
			return hashSet;
		}

		// Token: 0x17001468 RID: 5224
		// (get) Token: 0x06005912 RID: 22802 RVA: 0x00116234 File Offset: 0x00114434
		// (set) Token: 0x06005913 RID: 22803 RVA: 0x0011623C File Offset: 0x0011443C
		internal TimeZoneContextType TimeZoneContext { get; set; }

		// Token: 0x17001469 RID: 5225
		// (get) Token: 0x06005914 RID: 22804 RVA: 0x00116245 File Offset: 0x00114445
		// (set) Token: 0x06005915 RID: 22805 RVA: 0x0011624D File Offset: 0x0011444D
		internal DateTimePrecision? DateTimePrecision { get; set; }

		// Token: 0x1700146A RID: 5226
		// (get) Token: 0x06005916 RID: 22806 RVA: 0x00116256 File Offset: 0x00114456
		// (set) Token: 0x06005917 RID: 22807 RVA: 0x0011625E File Offset: 0x0011445E
		internal string MailboxCulture { get; set; }

		// Token: 0x1700146B RID: 5227
		// (get) Token: 0x06005918 RID: 22808 RVA: 0x00116267 File Offset: 0x00114467
		// (set) Token: 0x06005919 RID: 22809 RVA: 0x0011626F File Offset: 0x0011446F
		internal ExchangeVersion RequestVersion { get; set; }

		// Token: 0x1700146C RID: 5228
		// (get) Token: 0x0600591A RID: 22810 RVA: 0x00116278 File Offset: 0x00114478
		// (set) Token: 0x0600591B RID: 22811 RVA: 0x00116280 File Offset: 0x00114480
		internal bool IsBackgroundLoad { get; set; }

		// Token: 0x1700146D RID: 5229
		// (get) Token: 0x0600591C RID: 22812 RVA: 0x00116289 File Offset: 0x00114489
		// (set) Token: 0x0600591D RID: 22813 RVA: 0x00116291 File Offset: 0x00114491
		internal bool IsServiceUnavailableOnTransientError { get; set; }

		// Token: 0x1700146E RID: 5230
		// (get) Token: 0x0600591E RID: 22814 RVA: 0x0011629A File Offset: 0x0011449A
		// (set) Token: 0x0600591F RID: 22815 RVA: 0x001162A2 File Offset: 0x001144A2
		internal ManagementRoleType ManagementRoleType { get; set; }

		// Token: 0x06005920 RID: 22816 RVA: 0x001162AC File Offset: 0x001144AC
		internal virtual void ProcessMessageHeaders(Message request)
		{
			try
			{
				XmlDictionaryReader readerAtBodyContents = request.GetReaderAtBodyContents();
				bool flag = false;
				while (readerAtBodyContents.Read())
				{
					if (readerAtBodyContents.IsStartElement("Header"))
					{
						flag = true;
					}
					else
					{
						if (readerAtBodyContents.IsStartElement("Body"))
						{
							break;
						}
						string name;
						if (flag && (name = readerAtBodyContents.Name) != null)
						{
							if (<PrivateImplementationDetails>{3E8D983F-1A9C-416D-80C3-9D441BA4C28E}.$$method0x6005591-1 == null)
							{
								<PrivateImplementationDetails>{3E8D983F-1A9C-416D-80C3-9D441BA4C28E}.$$method0x6005591-1 = new Dictionary<string, int>(8)
								{
									{
										"RequestServerVersion",
										0
									},
									{
										"MailboxCulture",
										1
									},
									{
										"TimeZoneContext",
										2
									},
									{
										"DateTimePrecision",
										3
									},
									{
										"BackgroundLoad",
										4
									},
									{
										"ExchangeImpersonation",
										5
									},
									{
										"SerializedSecurityContext",
										6
									},
									{
										"ManagementRole",
										7
									}
								};
							}
							int num;
							if (<PrivateImplementationDetails>{3E8D983F-1A9C-416D-80C3-9D441BA4C28E}.$$method0x6005591-1.TryGetValue(name, out num))
							{
								switch (num)
								{
								case 0:
									this.RequestVersion = JsonMessageHeaderProcessor.ReadRequestVersionHeader(readerAtBodyContents);
									break;
								case 1:
									this.MailboxCulture = readerAtBodyContents.ReadString();
									break;
								case 2:
									this.TimeZoneContext = JsonMessageHeaderProcessor.ReadTimeZoneContextHeader(readerAtBodyContents);
									break;
								case 3:
									this.DateTimePrecision = new DateTimePrecision?(JsonMessageHeaderProcessor.ReadDateTimePrecisionHeader(readerAtBodyContents));
									break;
								case 4:
									this.IsBackgroundLoad = bool.Parse(readerAtBodyContents.ReadString());
									break;
								case 7:
									this.ManagementRoleType = JsonMessageHeaderProcessor.ReadManagementRoleHeader(readerAtBodyContents);
									break;
								}
							}
						}
					}
				}
			}
			catch (XmlException ex)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceError<string>((long)this.GetHashCode(), "[JsonMessageHeaderProcessor::ProcessHeaders] Caught XmlException exception while parsing message headers. Message: {0}", ex.Message);
			}
			this.ProcessRequestVersion(request);
		}

		// Token: 0x06005921 RID: 22817 RVA: 0x0011646C File Offset: 0x0011466C
		internal virtual void ProcessMessageHeadersFromQueryString(Message request)
		{
			bool flag = false;
			HttpRequestMessageProperty httpRequestMessageProperty = (HttpRequestMessageProperty)request.Properties[HttpRequestMessageProperty.Name];
			if (httpRequestMessageProperty != null && !string.IsNullOrEmpty(httpRequestMessageProperty.QueryString))
			{
				NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(httpRequestMessageProperty.QueryString);
				foreach (object obj in nameValueCollection.Keys)
				{
					string text = (string)obj;
					string a;
					if ((a = text) != null)
					{
						if (!(a == "ManagementRole"))
						{
							if (a == "RequestServerVersion")
							{
								this.RequestVersion = JsonMessageHeaderProcessor.ReadRequestVersionHeader(nameValueCollection.Get(text));
							}
						}
						else
						{
							this.QueryStringXmlDictionaryReaderAction(nameValueCollection.Get(text), delegate(XmlDictionaryReader reader)
							{
								this.ManagementRoleType = JsonMessageHeaderProcessor.ReadManagementRoleHeader(reader);
							});
							flag = true;
						}
					}
				}
			}
			if (flag)
			{
				this.ProcessRequestVersion(request);
			}
		}

		// Token: 0x06005922 RID: 22818 RVA: 0x00116568 File Offset: 0x00114768
		internal virtual void ProcessHttpHeaders(Message request, ExchangeVersion defaultVersion)
		{
			object obj;
			if (request.Properties.TryGetValue(HttpRequestMessageProperty.Name, out obj))
			{
				HttpRequestMessageProperty httpRequestMessageProperty = obj as HttpRequestMessageProperty;
				if (httpRequestMessageProperty == null)
				{
					return;
				}
				string text = httpRequestMessageProperty.Headers["X-MailboxCulture"];
				if (!string.IsNullOrEmpty(text))
				{
					this.MailboxCulture = text;
				}
				string text2 = httpRequestMessageProperty.Headers["X-TimeZoneContext"];
				if (!string.IsNullOrEmpty(text2))
				{
					this.TimeZoneContext = new TimeZoneContextType
					{
						TimeZoneDefinition = new TimeZoneDefinitionType
						{
							Id = text2
						}
					};
				}
				string value = httpRequestMessageProperty.Headers["X-DateTimePrecision"];
				if (!string.IsNullOrEmpty(value))
				{
					this.DateTimePrecision = new DateTimePrecision?(JsonMessageHeaderProcessor.ReadDateTimePrecisionHeader(value));
				}
				string value2 = httpRequestMessageProperty.Headers["X-BackgroundLoad"];
				if (!string.IsNullOrEmpty(value2))
				{
					this.IsBackgroundLoad = bool.Parse(value2);
				}
				ExchangeVersion ewsVersionFromHttpHeaders = this.GetEwsVersionFromHttpHeaders(request, httpRequestMessageProperty);
				if (ewsVersionFromHttpHeaders != null)
				{
					this.RequestVersion = ewsVersionFromHttpHeaders;
				}
				else
				{
					string text3 = httpRequestMessageProperty.Headers["X-RequestServerVersion"];
					this.RequestVersion = (string.IsNullOrEmpty(text3) ? defaultVersion : JsonMessageHeaderProcessor.ReadRequestVersionHeader(text3));
				}
				this.ProcessRequestVersion(request);
				string value3 = httpRequestMessageProperty.Headers["X-OWA-ServiceUnavailableOnTransientError"];
				if (!string.IsNullOrEmpty(value3))
				{
					this.IsServiceUnavailableOnTransientError = bool.Parse(value3);
				}
			}
		}

		// Token: 0x06005923 RID: 22819 RVA: 0x001166C4 File Offset: 0x001148C4
		internal virtual void ProcessEwsVersionFromHttpHeaders(Message request)
		{
			object obj;
			if (request.Properties.TryGetValue(HttpRequestMessageProperty.Name, out obj))
			{
				HttpRequestMessageProperty httpRequestMessageProperty = obj as HttpRequestMessageProperty;
				if (httpRequestMessageProperty == null)
				{
					return;
				}
				ExchangeVersion ewsVersionFromHttpHeaders = this.GetEwsVersionFromHttpHeaders(request, httpRequestMessageProperty);
				if (ewsVersionFromHttpHeaders != null)
				{
					this.RequestVersion = ewsVersionFromHttpHeaders;
					this.ProcessRequestVersion(request);
				}
			}
		}

		// Token: 0x06005924 RID: 22820 RVA: 0x00116710 File Offset: 0x00114910
		private ExchangeVersion GetEwsVersionFromHttpHeaders(Message request, HttpRequestMessageProperty httpRequest)
		{
			ExchangeVersion result = null;
			string headerValue = httpRequest.Headers["X-EWS-TargetVersion"];
			ExchangeVersionHeader exchangeVersionHeader = new ExchangeVersionHeader(headerValue);
			if (!exchangeVersionHeader.IsMissing)
			{
				ExchangeVersionType version = exchangeVersionHeader.CheckAndGetRequestedVersion();
				result = new ExchangeVersion(version);
			}
			return result;
		}

		// Token: 0x06005925 RID: 22821 RVA: 0x00116750 File Offset: 0x00114950
		protected void QueryStringXmlDictionaryReaderAction(string value, Action<XmlDictionaryReader> action)
		{
			if (!string.IsNullOrEmpty(value))
			{
				try
				{
					using (XmlDictionaryReader xmlDictionaryReader = JsonReaderWriterFactory.CreateJsonReader(Encoding.Unicode.GetBytes(value), new XmlDictionaryReaderQuotas()))
					{
						if (xmlDictionaryReader.Read())
						{
							action(xmlDictionaryReader);
						}
					}
				}
				catch (XmlException ex)
				{
					ExTraceGlobals.CommonAlgorithmTracer.TraceError<string>((long)this.GetHashCode(), "[JsonMessageHeaderProcessor::ProcessMessageHeadersFromQueryString] Caught XmlException exception while parsing message headers. Message: {0}", ex.Message);
				}
			}
		}

		// Token: 0x06005926 RID: 22822 RVA: 0x001167D4 File Offset: 0x001149D4
		internal override void ProcessMailboxCultureHeader(Message request)
		{
			CultureInfo serverCulture;
			CultureInfo clientCulture;
			MessageHeaderProcessor.GetCulture(this.MailboxCulture, out serverCulture, out clientCulture);
			EWSSettings.ClientCulture = clientCulture;
			EWSSettings.ServerCulture = serverCulture;
		}

		// Token: 0x06005927 RID: 22823 RVA: 0x001167FC File Offset: 0x001149FC
		internal override void ProcessTimeZoneContextHeader(Message request)
		{
			if (this.TimeZoneContext != null && this.TimeZoneContext.TimeZoneDefinition != null && !string.IsNullOrEmpty(this.TimeZoneContext.TimeZoneDefinition.Id))
			{
				EWSSettings.RequestTimeZone = this.TimeZoneContext.TimeZoneDefinition.ExTimeZone;
			}
		}

		// Token: 0x06005928 RID: 22824 RVA: 0x0011684C File Offset: 0x00114A4C
		internal override void ProcessDateTimePrecisionHeader(Message request)
		{
			if (this.DateTimePrecision != null)
			{
				EWSSettings.DateTimePrecision = this.DateTimePrecision.Value;
			}
		}

		// Token: 0x06005929 RID: 22825 RVA: 0x0011687C File Offset: 0x00114A7C
		internal override bool ProcessBackgroundLoadHeader(Message request)
		{
			return this.IsBackgroundLoad;
		}

		// Token: 0x0600592A RID: 22826 RVA: 0x00116884 File Offset: 0x00114A84
		internal override bool ProcessServiceUnavailableOnTransientErrorHeader(Message request)
		{
			return this.IsServiceUnavailableOnTransientError;
		}

		// Token: 0x0600592B RID: 22827 RVA: 0x0011688C File Offset: 0x00114A8C
		internal override ManagementRoleType ProcessManagementRoleHeader(Message request)
		{
			return this.ManagementRoleType;
		}

		// Token: 0x0600592C RID: 22828 RVA: 0x00116894 File Offset: 0x00114A94
		internal override ProxyRequestType? ProcessRequestTypeHeader(Message request)
		{
			if (WebOperationContext.Current == null || WebOperationContext.Current.IncomingRequest == null || WebOperationContext.Current.IncomingRequest.Headers == null)
			{
				return null;
			}
			return base.ParseProxyRequestType(WebOperationContext.Current.IncomingRequest.Headers["RequestType"]);
		}

		// Token: 0x0600592D RID: 22829 RVA: 0x001168EE File Offset: 0x00114AEE
		internal override AuthZClientInfo ProcessProxyHeaders(Message incomingMessage, AuthZClientInfo callerClientInfo)
		{
			return null;
		}

		// Token: 0x0600592E RID: 22830 RVA: 0x001168F1 File Offset: 0x00114AF1
		internal override AuthZClientInfo ProcessImpersonationHeaders(Message request, AuthZClientInfo proxyClientInfo, AuthZClientInfo impersonatingClientInfo)
		{
			return null;
		}

		// Token: 0x0600592F RID: 22831 RVA: 0x001168F4 File Offset: 0x00114AF4
		internal override AuthZClientInfo ProcessSerializedSecurityContextHeaders(Message request)
		{
			return null;
		}

		// Token: 0x06005930 RID: 22832 RVA: 0x001168F7 File Offset: 0x00114AF7
		internal override AuthZClientInfo ProcessOpenAsAdminOrSystemServiceHeader(Message request, AuthZClientInfo impersonatingClientInfo, out SpecialLogonType? specialLogonType, out int? budgetType)
		{
			specialLogonType = null;
			budgetType = null;
			return null;
		}

		// Token: 0x0400315F RID: 12639
		private const string AsyncMethodPrefix = "begin";

		// Token: 0x04003160 RID: 12640
		public const string MailboxCultureHeaderName = "X-MailboxCulture";

		// Token: 0x04003161 RID: 12641
		public const string TimeZoneContextHeaderName = "X-TimeZoneContext";

		// Token: 0x04003162 RID: 12642
		public const string DateTimePrecisionHeaderName = "X-DateTimePrecision";

		// Token: 0x04003163 RID: 12643
		public const string BackgroundLoadHeaderName = "X-BackgroundLoad";

		// Token: 0x04003164 RID: 12644
		public const string RequestServerVersionHeaderName = "X-RequestServerVersion";

		// Token: 0x04003165 RID: 12645
		public const string ServiceUnavailableOnTransientErrorHeaderName = "X-OWA-ServiceUnavailableOnTransientError";
	}
}
