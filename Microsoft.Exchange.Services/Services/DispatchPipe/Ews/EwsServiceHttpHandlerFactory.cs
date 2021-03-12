using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Principal;
using System.ServiceModel.Channels;
using System.Web;
using System.Xml;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Services.Core;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.DispatchPipe.Base;
using Microsoft.Exchange.Services.Wcf;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Services.DispatchPipe.Ews
{
	// Token: 0x02000DD3 RID: 3539
	public sealed class EwsServiceHttpHandlerFactory : HttpHandlerFactoryBase<EWSService>
	{
		// Token: 0x06005A6C RID: 23148 RVA: 0x00119E70 File Offset: 0x00118070
		internal override bool UseHttpHandlerFactory(HttpContext httpContext)
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			Uri url = httpContext.Request.Url;
			if (EWSSettings.IsWsSecurityAddress(url) || EWSSettings.IsWsSecuritySymmetricKeyAddress(url) || EWSSettings.IsWsSecurityX509CertAddress(url))
			{
				return false;
			}
			if (!string.Equals("post", httpContext.Request.HttpMethod, StringComparison.OrdinalIgnoreCase))
			{
				return false;
			}
			if (!this.IsEwsHttpHandlerEnabledForCallerFlight(httpContext))
			{
				return false;
			}
			Stream stream = null;
			Message message = null;
			bool result;
			try
			{
				stream = httpContext.Request.GetBufferlessInputStream();
				if (!stream.CanSeek)
				{
					stream = BufferedRegionStream.CreateWithBufferPoolCollection(stream, 172032, true);
				}
				MessageVersion soapMessageVersion = this.GetSoapMessageVersion(stream, 163840);
				MessageEncoderWithXmlDeclaration messageEncoder = EwsServiceHttpHandlerFactory.GetMessageEncoder(soapMessageVersion);
				stream.Position = 0L;
				try
				{
					message = messageEncoder.ReadMessage(stream, 163840, httpContext.Request.ContentType);
				}
				catch (Exception arg)
				{
					ExTraceGlobals.CommonAlgorithmTracer.TraceWarning<Exception>((long)this.GetHashCode(), "Exception calling ReadMessage: {0}", arg);
					return true;
				}
				httpContext.Items["EwsHttpContextMessage"] = message;
				if (message.Properties.ContainsKey("DelayedException"))
				{
					result = true;
				}
				else if (!EwsServiceHttpHandlerFactory.MethodMap.Contains((string)message.Properties["MethodName"]))
				{
					result = false;
				}
				else
				{
					result = true;
				}
			}
			finally
			{
				if ((message == null || !message.Properties.ContainsKey("MessageStream") || message.Properties["MessageStream"] == null) && stream != null)
				{
					stream.Dispose();
				}
				stopwatch.Stop();
				if (message != null)
				{
					message.Properties["HttpHandlerFactoryCheckLatency"] = stopwatch.ElapsedMilliseconds;
				}
			}
			return result;
		}

		// Token: 0x06005A6D RID: 23149 RVA: 0x0011A124 File Offset: 0x00118324
		internal override string SelectOperation(string url, HttpContext httpContext, string requestType)
		{
			Message message = null;
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			string operation = "ErrorPlaceHolderMethod";
			try
			{
				ServiceDiagnostics.SendWatsonReportOnUnhandledException(delegate
				{
					message = (Message)httpContext.Items["EwsHttpContextMessage"];
					if (message == null)
					{
						ExTraceGlobals.CommonAlgorithmTracer.TraceWarning((long)this.GetHashCode(), "Skipping SelectOperation because of null message.");
						return;
					}
					if (message.Properties.ContainsKey("DelayedException"))
					{
						ExTraceGlobals.CommonAlgorithmTracer.TraceWarning((long)this.GetHashCode(), "Skipping SelectOperation because a delayed fault exception already exists");
						return;
					}
					try
					{
						operation = EwsServiceHttpHandlerFactory.OperationSelector.SelectOperation(ref message);
						httpContext.Items["EwsHttpContextMessage"] = message;
					}
					catch (Exception ex)
					{
						ExTraceGlobals.CommonAlgorithmTracer.TraceWarning<Exception>((long)this.GetHashCode(), "SelectOperation, storing a delayed fault exception: {0}", ex);
						message.Properties["DelayedException"] = ex;
					}
				});
			}
			finally
			{
				stopwatch.Stop();
				if (message != null)
				{
					message.Properties["SelectOperationLatency"] = stopwatch.ElapsedMilliseconds;
				}
			}
			return operation;
		}

		// Token: 0x06005A6E RID: 23150 RVA: 0x0011A1C4 File Offset: 0x001183C4
		internal static MessageEncoderWithXmlDeclaration GetMessageEncoder(MessageVersion messageVersion)
		{
			if (messageVersion == MessageVersion.Soap11)
			{
				return EwsServiceHttpHandlerFactory.Soap11MessageEncoder.Member;
			}
			if (messageVersion == MessageVersion.Soap12)
			{
				return EwsServiceHttpHandlerFactory.Soap12MessageEncoder.Member;
			}
			throw new NotImplementedException();
		}

		// Token: 0x06005A6F RID: 23151 RVA: 0x0011A1F4 File Offset: 0x001183F4
		private MessageVersion GetSoapMessageVersion(Stream stream, int maxSizeToExamine)
		{
			MessageVersion soap = MessageVersion.Soap11;
			try
			{
				using (XmlReader xmlReader = XmlReader.Create(stream))
				{
					while (xmlReader.Read())
					{
						if (stream.Position > (long)maxSizeToExamine)
						{
							ExTraceGlobals.CommonAlgorithmTracer.TraceError<int>((long)this.GetHashCode(), "Cannot get soap message version from the first {0} bytes", maxSizeToExamine);
							break;
						}
						if ("Envelope".Equals(xmlReader.LocalName, StringComparison.Ordinal) && EwsServiceHttpHandlerFactory.NamespaceUriToMessageVersion.TryGetValue(xmlReader.NamespaceURI, out soap))
						{
							ExTraceGlobals.CommonAlgorithmTracer.TraceError<MessageVersion>((long)this.GetHashCode(), "Soap message version for request is {0}", soap);
							return soap;
						}
					}
				}
			}
			catch (XmlException arg)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceError<XmlException>((long)this.GetHashCode(), "Caught exception {0} when getting soap message version, using default Soap11", arg);
			}
			return soap;
		}

		// Token: 0x06005A70 RID: 23152 RVA: 0x0011A2C4 File Offset: 0x001184C4
		private bool IsEwsHttpHandlerEnabledForCallerFlight(HttpContext httpContext)
		{
			IIdentity identity = httpContext.User.Identity;
			if (identity == null)
			{
				return false;
			}
			bool result;
			try
			{
				using (AuthZClientInfo authZClientInfo = AuthZClientInfo.ResolveIdentity(identity))
				{
					if (authZClientInfo == null || authZClientInfo.UserIdentity == null)
					{
						result = false;
					}
					else
					{
						VariantConfigurationSnapshot snapshot = VariantConfiguration.GetSnapshot(authZClientInfo.UserIdentity.ADUser.GetContext(null), null, null);
						result = snapshot.Ews.EwsHttpHandler.Enabled;
					}
				}
			}
			catch (AuthzException ex)
			{
				WindowsIdentity windowsIdentity = identity as WindowsIdentity;
				if (windowsIdentity == null)
				{
					throw;
				}
				RequestDetailsLoggerBase<RequestDetailsLogger> requestDetailsLogger = RequestDetailsLogger.Current;
				string key = "WindowsIdentity";
				IntPtr token = windowsIdentity.Token;
				RequestDetailsLoggerBase<RequestDetailsLogger>.SafeAppendGenericError(requestDetailsLogger, key, windowsIdentity.Token.ToString());
				RequestDetailsLoggerBase<RequestDetailsLogger>.SafeAppendGenericError(RequestDetailsLogger.Current, "AuthzException", ex.Message);
				result = false;
			}
			return result;
		}

		// Token: 0x06005A71 RID: 23153 RVA: 0x0011A3AC File Offset: 0x001185AC
		internal override IHttpAsyncHandler CreateAsyncHttpHandler(HttpContext httpContext, EWSService service, ServiceMethodInfo methodInfo)
		{
			return new EwsServiceHttpAsyncHandler(httpContext, service, methodInfo);
		}

		// Token: 0x06005A72 RID: 23154 RVA: 0x0011A3B6 File Offset: 0x001185B6
		internal override IHttpHandler CreateHttpHandler(HttpContext httpContext, EWSService service, ServiceMethodInfo methodInfo)
		{
			throw new NotImplementedException("All supported EWS service are async");
		}

		// Token: 0x06005A73 RID: 23155 RVA: 0x0011A3C2 File Offset: 0x001185C2
		internal override EWSService CreateServiceInstance()
		{
			return new EWSService();
		}

		// Token: 0x06005A74 RID: 23156 RVA: 0x0011A3C9 File Offset: 0x001185C9
		internal override bool TryGetServiceMethod(string actionName, out ServiceMethodInfo methodInfo)
		{
			if (actionName.Equals("ErrorPlaceHolderMethod", StringComparison.Ordinal))
			{
				methodInfo = EwsServiceHttpHandlerFactory.ErrorPlaceHolderServiceMethodInfo;
				return true;
			}
			return EwsServiceHttpHandlerFactory.MethodMap.TryGetMethodInfo(actionName, out methodInfo);
		}

		// Token: 0x040031F3 RID: 12787
		public const string HttpHandlerFactoryCheckLatencyKey = "HttpHandlerFactoryCheckLatency";

		// Token: 0x040031F4 RID: 12788
		public const string SelectOperationLatencyKey = "SelectOperationLatency";

		// Token: 0x040031F5 RID: 12789
		private const string ErrorPlaceHolderMethodName = "ErrorPlaceHolderMethod";

		// Token: 0x040031F6 RID: 12790
		internal static readonly ExceptionHandlerInspector ExceptionHandler = new ExceptionHandlerInspector();

		// Token: 0x040031F7 RID: 12791
		private static readonly LazyMember<MessageEncoderWithXmlDeclaration> Soap11MessageEncoder = new LazyMember<MessageEncoderWithXmlDeclaration>(() => new MessageEncoderWithXmlDeclaration(MessageVersion.Soap11));

		// Token: 0x040031F8 RID: 12792
		private static readonly LazyMember<MessageEncoderWithXmlDeclaration> Soap12MessageEncoder = new LazyMember<MessageEncoderWithXmlDeclaration>(() => new MessageEncoderWithXmlDeclaration(MessageVersion.Soap12));

		// Token: 0x040031F9 RID: 12793
		private static readonly DispatchByBodyElementOperationSelector OperationSelector = new DispatchByBodyElementOperationSelector();

		// Token: 0x040031FA RID: 12794
		private static readonly Dictionary<string, MessageVersion> NamespaceUriToMessageVersion = new Dictionary<string, MessageVersion>(StringComparer.Ordinal)
		{
			{
				"http://www.w3.org/2003/05/soap-envelope",
				MessageVersion.Soap12
			},
			{
				"http://schemas.xmlsoap.org/soap/envelope/",
				MessageVersion.Soap11
			}
		};

		// Token: 0x040031FB RID: 12795
		private static readonly EwsServiceMethodMap MethodMap = new EwsServiceMethodMap(typeof(EWSService));

		// Token: 0x040031FC RID: 12796
		private static readonly ServiceMethodInfo ErrorPlaceHolderServiceMethodInfo = new ServiceMethodInfo
		{
			Name = "ErrorPlaceHolderMethod",
			IsAsyncPattern = true
		};
	}
}
