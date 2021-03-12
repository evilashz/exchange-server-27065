using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.EDiscovery.MailboxSearch.WebService.Infrastructure;
using Microsoft.Exchange.EDiscovery.MailboxSearch.WebService.Model;
using Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch;
using Microsoft.Exchange.Net.Protocols;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Security.OAuth;
using Microsoft.Exchange.SoapWebClient.EWS;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.EDiscovery.MailboxSearch.WebService
{
	// Token: 0x0200001C RID: 28
	internal class ExchangeProxy : IExchangeProxy
	{
		// Token: 0x060001A4 RID: 420 RVA: 0x0000DD28 File Offset: 0x0000BF28
		public ExchangeProxy(ISearchPolicy policy, FanoutParameters parameter) : this(policy, parameter.GroupId)
		{
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0000DD38 File Offset: 0x0000BF38
		public ExchangeProxy(ISearchPolicy policy, GroupId groupId)
		{
			Recorder.Trace(5L, TraceType.InfoTrace, new object[]
			{
				"ExchangeProxy.ctor GroupId:",
				groupId,
				"GroupType:",
				groupId.GroupType,
				"Uri:",
				groupId.Uri
			});
			this.groupId = groupId;
			this.policy = policy;
			ExchangeCredentials credentials;
			if (this.groupId.GroupType == GroupType.CrossPremise)
			{
				credentials = new OAuthCredentials(OAuthCredentials.GetOAuthCredentialsForAppToken(this.policy.CallerInfo.OrganizationId, groupId.Domain));
			}
			else
			{
				credentials = new WebCredentials();
			}
			string text = string.Format("{0}&FOUT=true", policy.CallerInfo.UserAgent);
			this.InitializeExchangeProxy(credentials, groupId.Uri, (long)groupId.ServerVersion);
			this.exchangeService.HttpHeaders[CertificateValidationManager.ComponentIdHeaderName] = typeof(IEwsClient).FullName;
			this.exchangeService.ClientRequestId = this.policy.CallerInfo.QueryCorrelationId.ToString();
			this.exchangeService.Timeout = (int)TimeSpan.FromMinutes(this.policy.ThrottlingSettings.DiscoverySearchTimeoutPeriod).TotalMilliseconds;
			if (this.groupId.GroupType == GroupType.CrossPremise)
			{
				this.exchangeService.UserAgent = text;
				this.exchangeService.ManagementRoles = new ManagementRoles(null, ExchangeProxy.mailboxSearchApplicationRole);
				return;
			}
			this.exchangeService.UserAgent = WellKnownUserAgent.GetEwsNegoAuthUserAgent(string.Format("{0}-{1}", ExchangeProxy.crossServerUserAgent, text));
			if (this.policy.CallerInfo.CommonAccessToken != null && !this.policy.CallerInfo.IsOpenAsAdmin)
			{
				this.exchangeService.HttpHeaders["X-CommonAccessToken"] = this.policy.CallerInfo.CommonAccessToken.Serialize();
				if (this.policy.CallerInfo.UserRoles != null || this.policy.CallerInfo.ApplicationRoles != null)
				{
					this.exchangeService.ManagementRoles = new ManagementRoles(this.policy.CallerInfo.UserRoles, this.policy.CallerInfo.ApplicationRoles);
				}
			}
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000DF70 File Offset: 0x0000C170
		public ExchangeProxy(ADUser user, Uri uri, long serverVersion)
		{
			OAuthCredentials credentials = new OAuthCredentials(OAuthCredentials.GetOAuthCredentialsForAppActAsToken(user.OrganizationId, user, null));
			this.InitializeExchangeProxy(credentials, uri, serverVersion);
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x0000DF9F File Offset: 0x0000C19F
		public ExchangeProxy(ExchangeCredentials credentials, Uri uri, long serverVersion)
		{
			this.InitializeExchangeProxy(credentials, uri, serverVersion);
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x0000DFB0 File Offset: 0x0000C1B0
		public ExchangeService ExchangeService
		{
			get
			{
				return this.exchangeService;
			}
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000DFB8 File Offset: 0x0000C1B8
		public TIn Create<TIn, TOut>() where TIn : SimpleServiceRequestBase where TOut : ServiceResponse
		{
			Recorder.Trace(5L, TraceType.InfoTrace, "ExchangeProxy.Create");
			ServiceRequestBase serviceRequestBase;
			if (typeof(TIn).BaseType == typeof(MultiResponseServiceRequest<TOut>))
			{
				serviceRequestBase = (Activator.CreateInstance(typeof(TIn), BindingFlags.Instance | BindingFlags.NonPublic, null, new object[]
				{
					this.ExchangeService,
					0
				}, null) as TIn);
			}
			else
			{
				serviceRequestBase = (Activator.CreateInstance(typeof(TIn), BindingFlags.Instance | BindingFlags.NonPublic, null, new object[]
				{
					this.ExchangeService
				}, null) as TIn);
			}
			this.SetVersion(serviceRequestBase);
			return serviceRequestBase as TIn;
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000E07C File Offset: 0x0000C27C
		public virtual IEnumerable<TOut> Execute<TIn, TOut>(TIn request) where TIn : SimpleServiceRequestBase where TOut : ServiceResponse
		{
			Recorder.Trace(5L, TraceType.InfoTrace, "ExchangeProxy.Execute Request:", request);
			MultiResponseServiceRequest<TOut> multiResponseServiceRequest = request as MultiResponseServiceRequest<TOut>;
			if (multiResponseServiceRequest != null)
			{
				return multiResponseServiceRequest.Execute();
			}
			return new TOut[]
			{
				request.InternalExecute() as TOut
			};
		}

		// Token: 0x060001AB RID: 427 RVA: 0x0000E0D8 File Offset: 0x0000C2D8
		public virtual IAsyncResult BeginExecute<TIn, TOut>(AsyncCallback callback, object state, TIn request) where TIn : SimpleServiceRequestBase where TOut : ServiceResponse
		{
			Recorder.Trace(5L, TraceType.InfoTrace, new object[]
			{
				"ExchangeProxy.BeginExecute State:",
				state,
				"Request:",
				request
			});
			return request.BeginExecute(callback, state);
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000E124 File Offset: 0x0000C324
		public virtual IEnumerable<TOut> EndExecute<TIn, TOut>(IAsyncResult result) where TIn : SimpleServiceRequestBase where TOut : ServiceResponse
		{
			Recorder.Trace(5L, TraceType.InfoTrace, "ExchangeProxy.EndExecute Result:", result);
			TIn tin = AsyncRequestResult.ExtractServiceRequest<TIn>(this.ExchangeService, result);
			MultiResponseServiceRequest<TOut> multiResponseServiceRequest = tin as MultiResponseServiceRequest<TOut>;
			if (multiResponseServiceRequest != null)
			{
				return multiResponseServiceRequest.EndExecute(result);
			}
			return new TOut[]
			{
				tin.EndInternalExecute(result) as TOut
			};
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0000E18C File Offset: 0x0000C38C
		public virtual void Abort(IAsyncResult result)
		{
			Recorder.Trace(5L, TraceType.InfoTrace, "ExchangeProxy.Abort Result:", result);
			AsyncRequestResult asyncRequestResult = result as AsyncRequestResult;
			if (asyncRequestResult != null)
			{
				asyncRequestResult.WebRequest.Abort();
			}
		}

		// Token: 0x060001AE RID: 430 RVA: 0x0000E1BC File Offset: 0x0000C3BC
		private void OnSerializeCustomSoapHeaders(XmlWriter writer)
		{
			if (this.groupId != null && this.groupId.GroupType != GroupType.CrossPremise && this.policy != null && this.policy.CallerInfo.IsOpenAsAdmin)
			{
				Recorder.Trace(5L, TraceType.InfoTrace, "ExchangeProxy.OnSerializeCustomSoapHeaders Admin");
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(OpenAsAdminOrSystemServiceType));
				OpenAsAdminOrSystemServiceType o = new OpenAsAdminOrSystemServiceType
				{
					ConnectingSID = new ConnectingSIDType
					{
						Item = new PrimarySmtpAddressType
						{
							Value = this.policy.CallerInfo.PrimarySmtpAddress
						}
					},
					LogonType = SpecialLogonType.Admin,
					BudgetType = 1,
					BudgetTypeSpecified = true
				};
				xmlSerializer.Serialize(writer, o);
			}
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0000E278 File Offset: 0x0000C478
		private void SetVersion(object request)
		{
			Recorder.Trace(5L, TraceType.InfoTrace, "ExchangeProxy.SetVersion Request:", request);
			IDiscoveryVersionable discoveryVersionable = request as IDiscoveryVersionable;
			if (discoveryVersionable != null)
			{
				discoveryVersionable.ServerVersion = this.serverVersion;
			}
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000E2AC File Offset: 0x0000C4AC
		private void InitializeExchangeProxy(ExchangeCredentials credentials, Uri uri, long serverVersion)
		{
			Recorder.Trace(5L, TraceType.InfoTrace, new object[]
			{
				"ExchangeProxy.InitializeExchangeProxy Uri:",
				uri,
				"Version:",
				serverVersion,
				"Credentials:",
				credentials
			});
			this.serverVersion = serverVersion;
			this.exchangeService = new ExchangeService();
			this.exchangeService.Url = uri;
			this.exchangeService.Credentials = credentials;
			this.exchangeService.OnSerializeCustomSoapHeaders += new CustomXmlSerializationDelegate(this.OnSerializeCustomSoapHeaders);
		}

		// Token: 0x040000D8 RID: 216
		private static string crossServerUserAgent = "DiscoveryEwsClient.XServer";

		// Token: 0x040000D9 RID: 217
		private static string mailboxSearchApplicationRole = "MailboxSearchApplication";

		// Token: 0x040000DA RID: 218
		private readonly GroupId groupId;

		// Token: 0x040000DB RID: 219
		private readonly ISearchPolicy policy;

		// Token: 0x040000DC RID: 220
		private long serverVersion;

		// Token: 0x040000DD RID: 221
		private ExchangeService exchangeService;
	}
}
