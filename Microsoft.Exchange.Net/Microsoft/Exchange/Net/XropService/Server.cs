using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Security;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Net;
using Microsoft.Exchange.Net.WSTrust;

namespace Microsoft.Exchange.Net.XropService
{
	// Token: 0x02000BA6 RID: 2982
	internal sealed class Server : IDisposable
	{
		// Token: 0x06003FF3 RID: 16371 RVA: 0x000A8610 File Offset: 0x000A6810
		public Server(Uri endpoint, TokenValidator tokenValidator, IAuthorizationManager authorizationManager, IServerSessionProvider sessionProvider, IServerDiagnosticsHandler diagnosticsHandler)
		{
			ExTraceGlobals.XropServiceServerTracer.TraceDebug<Uri>((long)this.GetHashCode(), "Starting service host for endpoint: {0}", endpoint);
			BindingElementCollection listenerBindingElements = Binding.GetListenerBindingElements();
			Binding binding = new Binding(listenerBindingElements);
			this.serviceHost = new ServiceHost(typeof(ServerSession), new Uri[0]);
			Server.SetThrottling(this.serviceHost);
			this.serviceHost.Description.Behaviors.Add(new Server.CustomServiceEndpointBehavior(new Server.InstanceProvider(sessionProvider), diagnosticsHandler));
			foreach (X509Certificate2 item in tokenValidator.TrustedTokenIssuerCertificates)
			{
				this.serviceHost.Credentials.IssuedTokenAuthentication.KnownCertificates.Add(item);
			}
			foreach (X509Certificate2 item2 in tokenValidator.TokenDecryptionCertificates)
			{
				this.serviceHost.Credentials.IssuedTokenAuthentication.KnownCertificates.Add(item2);
			}
			this.serviceHost.Credentials.IssuedTokenAuthentication.CertificateValidationMode = X509CertificateValidationMode.None;
			this.serviceHost.Credentials.IssuedTokenAuthentication.AllowedAudienceUris.Add(tokenValidator.TargetUri.OriginalString);
			this.serviceHost.Authorization.PrincipalPermissionMode = PrincipalPermissionMode.None;
			this.serviceHost.Authorization.ServiceAuthorizationManager = new Server.CustomAuthorizationManager(tokenValidator, authorizationManager);
			this.serviceHost.Credentials.UserNameAuthentication.CustomUserNamePasswordValidator = new Server.ExchangeUserNamePasswordValidator();
			this.serviceHost.Credentials.UserNameAuthentication.UserNamePasswordValidationMode = UserNamePasswordValidationMode.Custom;
			this.serviceHost.AddServiceEndpoint(typeof(IService), binding, endpoint);
			this.serviceHost.Open();
			ExTraceGlobals.XropServiceServerTracer.TraceDebug<Uri>((long)this.GetHashCode(), "Started service host for endpoint: {0}", endpoint);
		}

		// Token: 0x06003FF4 RID: 16372 RVA: 0x000A880C File Offset: 0x000A6A0C
		public void Dispose()
		{
			this.serviceHost.Close();
			ExTraceGlobals.XropServiceServerTracer.TraceDebug((long)this.GetHashCode(), "Service host disposed");
		}

		// Token: 0x06003FF5 RID: 16373 RVA: 0x000A8830 File Offset: 0x000A6A30
		public static void InitializeGlobalErrorHandlers(IServerDiagnosticsHandler diagnosticsHandler)
		{
			if (Server.originalTransportExceptionHandler != null || Server.httpListenerExtendedTraceListener != null)
			{
				throw new InvalidOperationException("InitializeGlobalErrorHandlers called without terminate");
			}
			if (Binding.UseWCFTransportExceptionHandler.Value)
			{
				Server.xropWCFTransportExceptionHandler = new Server.WCFTransportExceptionHandler(diagnosticsHandler);
				Server.originalTransportExceptionHandler = ExceptionHandler.TransportExceptionHandler;
				ExceptionHandler.TransportExceptionHandler = Server.xropWCFTransportExceptionHandler;
			}
			if (Binding.UseHttpListenerExtendedErrorLogging.Value)
			{
				Server.httpListenerExtendedTraceListener = new Server.HttpListenerExtendedTraceListener(diagnosticsHandler);
				SystemTraceControl.AddExtendedErrorListener(SystemTraceControl.SourceHttpListener, Server.httpListenerExtendedTraceListener);
			}
		}

		// Token: 0x06003FF6 RID: 16374 RVA: 0x000A88A8 File Offset: 0x000A6AA8
		public static void TerminateGlobalErrorHandlers()
		{
			if (ExceptionHandler.TransportExceptionHandler == Server.xropWCFTransportExceptionHandler && Server.originalTransportExceptionHandler != null)
			{
				ExceptionHandler.TransportExceptionHandler = Server.originalTransportExceptionHandler;
				Server.originalTransportExceptionHandler = null;
				Server.xropWCFTransportExceptionHandler = null;
			}
			if (Server.httpListenerExtendedTraceListener != null)
			{
				SystemTraceControl.RemoveExtendedErrorListener(SystemTraceControl.SourceHttpListener, Server.httpListenerExtendedTraceListener);
				Server.httpListenerExtendedTraceListener = null;
			}
		}

		// Token: 0x06003FF7 RID: 16375 RVA: 0x000A88FC File Offset: 0x000A6AFC
		private static void SetThrottling(ServiceHost serviceHost)
		{
			ServiceThrottlingBehavior serviceThrottlingBehavior = new ServiceThrottlingBehavior();
			serviceThrottlingBehavior.MaxConcurrentCalls = 256;
			serviceThrottlingBehavior.MaxConcurrentInstances = int.MaxValue;
			serviceThrottlingBehavior.MaxConcurrentSessions = 65536;
			serviceHost.Description.Behaviors.Add(serviceThrottlingBehavior);
		}

		// Token: 0x04003773 RID: 14195
		private const string UserNameClaimSetClass = "UserNameClaimSet";

		// Token: 0x04003774 RID: 14196
		private ServiceHost serviceHost;

		// Token: 0x04003775 RID: 14197
		private static Server.HttpListenerExtendedTraceListener httpListenerExtendedTraceListener;

		// Token: 0x04003776 RID: 14198
		private static ExceptionHandler xropWCFTransportExceptionHandler;

		// Token: 0x04003777 RID: 14199
		private static ExceptionHandler originalTransportExceptionHandler;

		// Token: 0x02000BA7 RID: 2983
		private sealed class InstanceProvider : IInstanceProvider
		{
			// Token: 0x06003FF8 RID: 16376 RVA: 0x000A8941 File Offset: 0x000A6B41
			public InstanceProvider(IServerSessionProvider sessionProvider)
			{
				this.sessionProvider = sessionProvider;
			}

			// Token: 0x06003FF9 RID: 16377 RVA: 0x000A8974 File Offset: 0x000A6B74
			public object GetInstance(InstanceContext instanceContext)
			{
				object instance = null;
				ExWatson.SendReportOnUnhandledException(delegate()
				{
					instance = this.GetInstanceInternal(instanceContext, null);
				});
				return instance;
			}

			// Token: 0x06003FFA RID: 16378 RVA: 0x000A89DC File Offset: 0x000A6BDC
			public object GetInstance(InstanceContext instanceContext, Message message)
			{
				object instance = null;
				ExWatson.SendReportOnUnhandledException(delegate()
				{
					instance = this.GetInstanceInternal(instanceContext, message);
				});
				return instance;
			}

			// Token: 0x06003FFB RID: 16379 RVA: 0x000A8A24 File Offset: 0x000A6C24
			private object GetInstanceInternal(InstanceContext instanceContext, Message message)
			{
				object obj;
				if (!OperationContext.Current.ServiceSecurityContext.AuthorizationContext.Properties.TryGetValue("TokenValidationResults", out obj))
				{
					ExTraceGlobals.XropServiceServerTracer.TraceError((long)this.GetHashCode(), "Principal missing from the authorization context properties. Failing.");
					return null;
				}
				TokenValidationResults tokenValidationResults = obj as TokenValidationResults;
				if (tokenValidationResults == null)
				{
					ExTraceGlobals.XropServiceServerTracer.TraceError((long)this.GetHashCode(), "TokenValidationResults object in authorization context properties not expected type. Failing.");
					return null;
				}
				IServerSession serverSession = this.sessionProvider.Create(tokenValidationResults);
				if (serverSession == null)
				{
					ExTraceGlobals.XropServiceServerTracer.TraceError((long)this.GetHashCode(), "IServerSessionProvider did not provide a IServerSession instance. Failing.");
					return null;
				}
				ExTraceGlobals.XropServiceServerTracer.TraceDebug<int>((long)this.GetHashCode(), "Created new IServerSession: {0}", serverSession.GetHashCode());
				return new ServerSession(serverSession);
			}

			// Token: 0x06003FFC RID: 16380 RVA: 0x000A8AD8 File Offset: 0x000A6CD8
			public void ReleaseInstance(InstanceContext instanceContext, object instance)
			{
				IDisposable disposable = instance as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}

			// Token: 0x04003778 RID: 14200
			private IServerSessionProvider sessionProvider;
		}

		// Token: 0x02000BA8 RID: 2984
		private sealed class ExchangeUserNamePasswordValidator : UserNamePasswordValidator
		{
			// Token: 0x06003FFD RID: 16381 RVA: 0x000A8AF8 File Offset: 0x000A6CF8
			public override void Validate(string userName, string password)
			{
				if (userName == null)
				{
					throw new ArgumentNullException();
				}
				if (userName.Length == 0 || userName.IndexOf("@") < 0)
				{
					ExTraceGlobals.XropServiceServerTracer.TraceError((long)this.GetHashCode(), "Access denied: invalid SMTP email address on supporting UserName token.");
					throw new SecurityTokenException("Invalid SMTP address on UserName token");
				}
				ExTraceGlobals.XropServiceServerTracer.TraceDebug((long)this.GetHashCode(), "UserName token validated.");
			}
		}

		// Token: 0x02000BA9 RID: 2985
		private sealed class CustomServiceEndpointBehavior : IServiceBehavior
		{
			// Token: 0x06003FFF RID: 16383 RVA: 0x000A8B63 File Offset: 0x000A6D63
			public CustomServiceEndpointBehavior(Server.InstanceProvider instanceProvider, IServerDiagnosticsHandler diagnosticsHandler)
			{
				this.instanceProvider = instanceProvider;
				this.diagnosticsHandler = diagnosticsHandler;
			}

			// Token: 0x06004000 RID: 16384 RVA: 0x000A8B79 File Offset: 0x000A6D79
			public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
			{
			}

			// Token: 0x06004001 RID: 16385 RVA: 0x000A8B7B File Offset: 0x000A6D7B
			public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
			{
			}

			// Token: 0x06004002 RID: 16386 RVA: 0x000A8B80 File Offset: 0x000A6D80
			public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
			{
				foreach (ChannelDispatcherBase channelDispatcherBase in serviceHostBase.ChannelDispatchers)
				{
					ChannelDispatcher channelDispatcher = channelDispatcherBase as ChannelDispatcher;
					if (channelDispatcher != null)
					{
						foreach (EndpointDispatcher endpointDispatcher in channelDispatcher.Endpoints)
						{
							endpointDispatcher.DispatchRuntime.InstanceProvider = this.instanceProvider;
							endpointDispatcher.DispatchRuntime.MessageInspectors.Add(new Server.CustomMessageInspector());
						}
						channelDispatcher.ErrorHandlers.Add(new Server.CustomErrorHandler(this.diagnosticsHandler));
					}
				}
			}

			// Token: 0x06004003 RID: 16387 RVA: 0x000A8C4C File Offset: 0x000A6E4C
			public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
			{
			}

			// Token: 0x04003779 RID: 14201
			private Server.InstanceProvider instanceProvider;

			// Token: 0x0400377A RID: 14202
			private IServerDiagnosticsHandler diagnosticsHandler;
		}

		// Token: 0x02000BAA RID: 2986
		private sealed class AuthorizationPolicy : IAuthorizationPolicy, IAuthorizationComponent
		{
			// Token: 0x06004004 RID: 16388 RVA: 0x000A8C50 File Offset: 0x000A6E50
			public AuthorizationPolicy(TokenValidationResults tokenValidationResults)
			{
				this.id = Guid.NewGuid().ToString();
				this.tokenValidationResults = tokenValidationResults;
			}

			// Token: 0x17000FB8 RID: 4024
			// (get) Token: 0x06004005 RID: 16389 RVA: 0x000A8C83 File Offset: 0x000A6E83
			public string Id
			{
				get
				{
					return this.id;
				}
			}

			// Token: 0x17000FB9 RID: 4025
			// (get) Token: 0x06004006 RID: 16390 RVA: 0x000A8C8B File Offset: 0x000A6E8B
			public ClaimSet Issuer
			{
				get
				{
					return ClaimSet.System;
				}
			}

			// Token: 0x06004007 RID: 16391 RVA: 0x000A8C94 File Offset: 0x000A6E94
			public bool Evaluate(EvaluationContext evaluationContext, ref object state)
			{
				if (evaluationContext.Properties.ContainsKey("TokenValidationResults"))
				{
					return true;
				}
				ExTraceGlobals.XropServiceServerTracer.TraceDebug<EvaluationContextTracer>((long)this.GetHashCode(), "Evaluating: {0}", new EvaluationContextTracer(evaluationContext));
				evaluationContext.Properties["TokenValidationResults"] = this.tokenValidationResults;
				return true;
			}

			// Token: 0x0400377B RID: 14203
			public const string TokenValidationResults = "TokenValidationResults";

			// Token: 0x0400377C RID: 14204
			private string id;

			// Token: 0x0400377D RID: 14205
			private TokenValidationResults tokenValidationResults;
		}

		// Token: 0x02000BAB RID: 2987
		private sealed class CustomAuthorizationManager : ServiceAuthorizationManager
		{
			// Token: 0x06004008 RID: 16392 RVA: 0x000A8CE8 File Offset: 0x000A6EE8
			public CustomAuthorizationManager(TokenValidator tokenValidator, IAuthorizationManager authorizationManager)
			{
				this.tokenValidator = tokenValidator;
				this.authorizationManager = authorizationManager;
			}

			// Token: 0x06004009 RID: 16393 RVA: 0x000A8D00 File Offset: 0x000A6F00
			protected override bool CheckAccessCore(OperationContext operationContext)
			{
				AuthorizationContext authorizationContext = operationContext.ServiceSecurityContext.AuthorizationContext;
				if (!authorizationContext.Properties.ContainsKey("TokenValidationResults"))
				{
					ExTraceGlobals.XropServiceServerTracer.TraceError((long)this.GetHashCode(), "Access denied: application did not authorize the caller.");
					return false;
				}
				ExTraceGlobals.XropServiceServerTracer.TraceDebug((long)this.GetHashCode(), "Access allowed.");
				return true;
			}

			// Token: 0x0600400A RID: 16394 RVA: 0x000A8D5C File Offset: 0x000A6F5C
			protected override ReadOnlyCollection<IAuthorizationPolicy> GetAuthorizationPolicies(OperationContext operationContext)
			{
				ReadOnlyCollection<IAuthorizationPolicy> readOnlyCollection = base.GetAuthorizationPolicies(operationContext);
				TokenValidationResults tokenValidationResults = this.Validate(operationContext);
				if (tokenValidationResults != null)
				{
					readOnlyCollection = new ReadOnlyCollection<IAuthorizationPolicy>(new List<IAuthorizationPolicy>(readOnlyCollection)
					{
						new Server.AuthorizationPolicy(tokenValidationResults)
					});
				}
				return readOnlyCollection;
			}

			// Token: 0x0600400B RID: 16395 RVA: 0x000A8D98 File Offset: 0x000A6F98
			private TokenValidationResults Validate(OperationContext operationContext)
			{
				foreach (SupportingTokenSpecification supportingTokenSpecification in operationContext.SupportingTokens)
				{
					TokenValidationResults tokenValidationResults = this.Validate(supportingTokenSpecification);
					if (tokenValidationResults != null)
					{
						return tokenValidationResults;
					}
				}
				return null;
			}

			// Token: 0x0600400C RID: 16396 RVA: 0x000A8DF0 File Offset: 0x000A6FF0
			private TokenValidationResults Validate(SupportingTokenSpecification supportingTokenSpecification)
			{
				if (supportingTokenSpecification == null)
				{
					return null;
				}
				if (supportingTokenSpecification.SecurityToken == null)
				{
					return null;
				}
				TokenValidationResults tokenValidationResults = this.tokenValidator.ValidateToken(supportingTokenSpecification, Offer.XropLogon);
				if (tokenValidationResults.Result != TokenValidationResult.Valid)
				{
					ExTraceGlobals.XropServiceServerTracer.TraceError<TokenValidationResults>((long)this.GetHashCode(), "SAML token did not pass validation. Validation result: {0}.", tokenValidationResults);
					return null;
				}
				tokenValidationResults = this.UpdateTokenValidationResults(supportingTokenSpecification, tokenValidationResults);
				if (!this.authorizationManager.CheckAccess(tokenValidationResults))
				{
					ExTraceGlobals.XropServiceServerTracer.TraceError<string>((long)this.GetHashCode(), "Application authorization manager did not allow access to {0}.", tokenValidationResults.EmailAddress);
					return null;
				}
				ExTraceGlobals.XropServiceServerTracer.TraceDebug<string>((long)this.GetHashCode(), "Application authorization manager allowed access to {0}.", tokenValidationResults.EmailAddress);
				return tokenValidationResults;
			}

			// Token: 0x0600400D RID: 16397 RVA: 0x000A8E94 File Offset: 0x000A7094
			private TokenValidationResults UpdateTokenValidationResults(SupportingTokenSpecification supportingTokenSpecification, TokenValidationResults results)
			{
				AuthorizationContext authorizationContext = AuthorizationContext.CreateDefaultAuthorizationContext(supportingTokenSpecification.SecurityTokenPolicies);
				foreach (ClaimSet claimSet in authorizationContext.ClaimSets)
				{
					if (claimSet.Issuer == ClaimSet.System && claimSet.GetType().Name == "UserNameClaimSet")
					{
						foreach (Claim claim in claimSet.FindClaims(ClaimTypes.Name, Rights.Identity))
						{
							string text = claim.Resource as string;
							if (text != null)
							{
								return new TokenValidationResults(results.ExternalId, text, results.Offer, results.SecurityToken, results.ProofToken, results.EmailAddresses);
							}
						}
					}
				}
				return results;
			}

			// Token: 0x0400377E RID: 14206
			private TokenValidator tokenValidator;

			// Token: 0x0400377F RID: 14207
			private IAuthorizationManager authorizationManager;
		}

		// Token: 0x02000BAC RID: 2988
		private sealed class CustomErrorHandler : IErrorHandler
		{
			// Token: 0x0600400E RID: 16398 RVA: 0x000A8F98 File Offset: 0x000A7198
			internal CustomErrorHandler(IServerDiagnosticsHandler diagnosticsHandler)
			{
				this.diagnosticsHandler = diagnosticsHandler;
			}

			// Token: 0x0600400F RID: 16399 RVA: 0x000A8FA7 File Offset: 0x000A71A7
			public bool HandleError(Exception error)
			{
				if (!Server.CustomErrorHandler.IsExceptionReported(error))
				{
					this.ReportException(error);
				}
				return true;
			}

			// Token: 0x06004010 RID: 16400 RVA: 0x000A8FBC File Offset: 0x000A71BC
			public void ProvideFault(Exception exception, MessageVersion version, ref Message fault)
			{
				Message message = fault;
				this.ReportException(exception);
				Server.CustomErrorHandler.SetErrorAlreadyReported(exception);
				bool flag = false;
				CommunicationException ex = exception as CommunicationException;
				if (ex != null && ex.GetType().FullName.Contains("MustUnderstandSoapException") && fault != null)
				{
					ExTraceGlobals.XropServiceServerTracer.TraceDebug((long)this.GetHashCode(), "[CustomErrorHandler::ProvideFault] Request failed due to the presence of soap:mustUnderstand on a header that XropService did not understand.");
					flag = true;
				}
				FaultException ex2 = exception as FaultException;
				if (ex2 == null)
				{
					Exception exception2 = exception;
					if (!flag)
					{
						this.diagnosticsHandler.AnalyseException(ref exception2);
					}
					ex2 = this.HandleInternalServerError(exception2);
				}
				if (fault == null)
				{
					MessageFault fault2 = ex2.CreateMessageFault();
					fault = Message.CreateMessage(version, fault2, "*");
				}
			}

			// Token: 0x06004011 RID: 16401 RVA: 0x000A9057 File Offset: 0x000A7257
			private static void SetErrorAlreadyReported(Exception exception)
			{
				if (exception.Data != null)
				{
					exception.Data["ExceptionAlreadyReported"] = null;
				}
			}

			// Token: 0x06004012 RID: 16402 RVA: 0x000A9072 File Offset: 0x000A7272
			private static bool IsExceptionReported(Exception exception)
			{
				while (exception != null)
				{
					if (exception.Data != null && exception.Data.Contains("ExceptionAlreadyReported"))
					{
						return true;
					}
					exception = exception.InnerException;
				}
				return false;
			}

			// Token: 0x06004013 RID: 16403 RVA: 0x000A90A0 File Offset: 0x000A72A0
			private FaultException HandleInternalServerError(Exception exception)
			{
				Exception exception2 = (exception is CommunicationException) ? exception : new Server.CustomErrorHandler.InternalServerErrorException(exception);
				Server.CustomErrorHandler.XropServiceMessageFaultDetail messageFault = new Server.CustomErrorHandler.XropServiceMessageFaultDetail(exception2, Server.CustomErrorHandler.FaultParty.Receiver);
				return this.CreateFaultException(messageFault);
			}

			// Token: 0x06004014 RID: 16404 RVA: 0x000A90D0 File Offset: 0x000A72D0
			private void ReportException(Exception exception)
			{
				if (OperationContext.Current != null && OperationContext.Current.RequestContext != null && OperationContext.Current.RequestContext.RequestMessage != null)
				{
					ExTraceGlobals.XropServiceServerTracer.TraceError<Message, string, string>((long)this.GetHashCode(), "Request: {0}.  Exception Class: {1}, Exception Message: {2}", OperationContext.Current.RequestContext.RequestMessage, exception.GetType().FullName, exception.Message);
				}
				else
				{
					ExTraceGlobals.XropServiceServerTracer.TraceError<string, string>((long)this.GetHashCode(), "Exception Class: {0}, Exception Message: {1}", exception.GetType().FullName, exception.Message);
				}
				if (Binding.IncludeErrorDetailsInTrace.Value)
				{
					for (Exception innerException = exception.InnerException; innerException != null; innerException = innerException.InnerException)
					{
						ExTraceGlobals.XropServiceServerTracer.TraceError<string, string>((long)this.GetHashCode(), "Exception Class: {0}, Exception Message: {1}", innerException.GetType().FullName, innerException.Message);
					}
				}
				this.diagnosticsHandler.LogException(exception);
			}

			// Token: 0x06004015 RID: 16405 RVA: 0x000A91B4 File Offset: 0x000A73B4
			private FaultException CreateFaultException(MessageFault messageFault)
			{
				return new FaultException(messageFault)
				{
					Source = "XropService"
				};
			}

			// Token: 0x04003780 RID: 14208
			private const string GenericServiceName = "XropService";

			// Token: 0x04003781 RID: 14209
			private const string ExceptionAlreadyReported = "ExceptionAlreadyReported";

			// Token: 0x04003782 RID: 14210
			private const string XropInternalServerError = "InternalServiceFault";

			// Token: 0x04003783 RID: 14211
			private IServerDiagnosticsHandler diagnosticsHandler;

			// Token: 0x02000BAD RID: 2989
			internal enum FaultParty
			{
				// Token: 0x04003785 RID: 14213
				Sender,
				// Token: 0x04003786 RID: 14214
				Receiver
			}

			// Token: 0x02000BAE RID: 2990
			internal class XropServiceMessageFaultDetail : MessageFault
			{
				// Token: 0x06004016 RID: 16406 RVA: 0x000A91D4 File Offset: 0x000A73D4
				internal XropServiceMessageFaultDetail(Exception exception, Server.CustomErrorHandler.FaultParty faultParty)
				{
					this.exception = exception;
					this.faultCode = ((faultParty == Server.CustomErrorHandler.FaultParty.Sender) ? FaultCode.CreateSenderFaultCode("InternalServiceFault", "http://schemas.microsoft.com/exchange/2010/xrop") : FaultCode.CreateReceiverFaultCode("InternalServiceFault", "http://schemas.microsoft.com/exchange/2010/xrop"));
					this.faultReason = new FaultReason(exception.Message);
				}

				// Token: 0x17000FBA RID: 4026
				// (get) Token: 0x06004017 RID: 16407 RVA: 0x000A9228 File Offset: 0x000A7428
				public override FaultCode Code
				{
					get
					{
						return this.faultCode;
					}
				}

				// Token: 0x17000FBB RID: 4027
				// (get) Token: 0x06004018 RID: 16408 RVA: 0x000A9230 File Offset: 0x000A7430
				public override bool HasDetail
				{
					get
					{
						return Binding.IncludeDetailsInServiceFaults.Value;
					}
				}

				// Token: 0x17000FBC RID: 4028
				// (get) Token: 0x06004019 RID: 16409 RVA: 0x000A923C File Offset: 0x000A743C
				public override FaultReason Reason
				{
					get
					{
						return this.faultReason;
					}
				}

				// Token: 0x0600401A RID: 16410 RVA: 0x000A9244 File Offset: 0x000A7444
				protected override void OnWriteDetailContents(XmlDictionaryWriter writer)
				{
					if (!this.HasDetail)
					{
						return;
					}
					string faultDetails = this.GetFaultDetails();
					if (!string.IsNullOrEmpty(faultDetails))
					{
						writer.WriteRaw(faultDetails);
					}
				}

				// Token: 0x0600401B RID: 16411 RVA: 0x000A9270 File Offset: 0x000A7470
				private string GetFaultDetails()
				{
					SafeXmlDocument safeXmlDocument = new SafeXmlDocument();
					XmlElement xmlElement = safeXmlDocument.CreateElement("DiagnosticInformation", "http://schemas.microsoft.com/exchange/2010/xrop/Error");
					for (Exception innerException = this.exception; innerException != null; innerException = innerException.InnerException)
					{
						XmlElement xmlElement2 = safeXmlDocument.CreateElement("ExceptionMessage", "http://schemas.microsoft.com/exchange/2010/xrop/Error");
						xmlElement.AppendChild(xmlElement2);
						XmlText newChild = safeXmlDocument.CreateTextNode(innerException.Message);
						xmlElement2.AppendChild(newChild);
						if (Binding.IncludeStackInServiceFaults.Value)
						{
							XmlElement xmlElement3 = safeXmlDocument.CreateElement("StackTrace", "http://schemas.microsoft.com/exchange/2010/xrop/Error");
							xmlElement.AppendChild(xmlElement3);
							if (!string.IsNullOrEmpty(innerException.StackTrace))
							{
								XmlText newChild2 = safeXmlDocument.CreateTextNode(innerException.StackTrace);
								xmlElement3.AppendChild(newChild2);
							}
						}
					}
					return xmlElement.OuterXml;
				}

				// Token: 0x04003787 RID: 14215
				private FaultCode faultCode;

				// Token: 0x04003788 RID: 14216
				private FaultReason faultReason;

				// Token: 0x04003789 RID: 14217
				private Exception exception;
			}

			// Token: 0x02000BAF RID: 2991
			[Serializable]
			internal class InternalServerErrorException : Exception
			{
				// Token: 0x0600401C RID: 16412 RVA: 0x000A932F File Offset: 0x000A752F
				public InternalServerErrorException(Exception innerException) : base("InternalServiceFault", innerException)
				{
				}

				// Token: 0x0600401D RID: 16413 RVA: 0x000A933D File Offset: 0x000A753D
				public InternalServerErrorException(string message, Exception innerException) : base(message, innerException)
				{
				}
			}
		}

		// Token: 0x02000BB0 RID: 2992
		private class CustomMessageInspector : IDispatchMessageInspector
		{
			// Token: 0x0600401E RID: 16414 RVA: 0x000A9347 File Offset: 0x000A7547
			internal CustomMessageInspector()
			{
				this.xropHostName = ComputerInformation.DnsPhysicalHostName;
			}

			// Token: 0x0600401F RID: 16415 RVA: 0x000A935C File Offset: 0x000A755C
			public void BeforeSendReply(ref Message reply, object correlationState)
			{
				object obj = null;
				HttpResponseMessageProperty httpResponseMessageProperty = null;
				if (reply.Properties.TryGetValue(HttpResponseMessageProperty.Name, out obj))
				{
					httpResponseMessageProperty = (obj as HttpResponseMessageProperty);
				}
				if (httpResponseMessageProperty == null)
				{
					httpResponseMessageProperty = new HttpResponseMessageProperty();
					reply.Properties.Add(HttpResponseMessageProperty.Name, httpResponseMessageProperty);
				}
				httpResponseMessageProperty.Headers.Add("X-DiagInfo", this.xropHostName);
				if (reply.IsFault && Binding.Use200ForSoapFaults.Value)
				{
					httpResponseMessageProperty.StatusCode = HttpStatusCode.OK;
				}
				if (reply.IsFault)
				{
					ExTraceGlobals.XropServiceServerTracer.TraceError<Message>((long)this.GetHashCode(), "Sending Fault: {0}.", reply);
					return;
				}
				ExTraceGlobals.XropServiceServerTracer.TraceDebug<Message>((long)this.GetHashCode(), "Sending Reply: {0}.", reply);
			}

			// Token: 0x06004020 RID: 16416 RVA: 0x000A9414 File Offset: 0x000A7614
			public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
			{
				ExTraceGlobals.XropServiceServerTracer.TraceDebug<Message>((long)this.GetHashCode(), "Receiving Request: {0}.", request);
				return null;
			}

			// Token: 0x0400378A RID: 14218
			private string xropHostName;
		}

		// Token: 0x02000BB1 RID: 2993
		private sealed class HttpListenerExtendedTraceListener : TraceListener
		{
			// Token: 0x06004021 RID: 16417 RVA: 0x000A942F File Offset: 0x000A762F
			public HttpListenerExtendedTraceListener(IServerDiagnosticsHandler diagnosticsHandler) : base("XTCHttpListenerExtendedTraceListener")
			{
				this.diagnosticsHandler = diagnosticsHandler;
			}

			// Token: 0x06004022 RID: 16418 RVA: 0x000A9443 File Offset: 0x000A7643
			public override void Write(string message)
			{
			}

			// Token: 0x06004023 RID: 16419 RVA: 0x000A9445 File Offset: 0x000A7645
			public override void WriteLine(string message)
			{
			}

			// Token: 0x06004024 RID: 16420 RVA: 0x000A9447 File Offset: 0x000A7647
			public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message)
			{
				if (eventType == TraceEventType.Critical || eventType == TraceEventType.Error)
				{
					this.diagnosticsHandler.LogMessage(message);
				}
			}

			// Token: 0x0400378B RID: 14219
			private IServerDiagnosticsHandler diagnosticsHandler;
		}

		// Token: 0x02000BB2 RID: 2994
		private sealed class WCFTransportExceptionHandler : ExceptionHandler
		{
			// Token: 0x06004025 RID: 16421 RVA: 0x000A945E File Offset: 0x000A765E
			public WCFTransportExceptionHandler(IServerDiagnosticsHandler diagnosticsHandler)
			{
				this.diagnosticsHandler = diagnosticsHandler;
			}

			// Token: 0x06004026 RID: 16422 RVA: 0x000A9470 File Offset: 0x000A7670
			public override bool HandleException(Exception exception)
			{
				this.diagnosticsHandler.LogException(exception);
				ExceptionHandler originalTransportExceptionHandler = Server.originalTransportExceptionHandler;
				return originalTransportExceptionHandler == null || originalTransportExceptionHandler.HandleException(exception);
			}

			// Token: 0x0400378C RID: 14220
			private IServerDiagnosticsHandler diagnosticsHandler;
		}
	}
}
