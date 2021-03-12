using System;
using System.Diagnostics;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage.Authentication;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.RequestDispatch;
using Microsoft.Exchange.Net.WSTrust;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x020000AC RID: 172
	internal sealed class ExternalAuthenticationRequest : AsyncTask
	{
		// Token: 0x060003C9 RID: 969 RVA: 0x0000F9EA File Offset: 0x0000DBEA
		public ExternalAuthenticationRequest(RequestLogger requestLogger, ExternalAuthentication externalAuthentication, ADUser user, SmtpAddress emailAddress, TokenTarget target, Offer offer)
		{
			this.requestLogger = requestLogger;
			this.user = user;
			this.emailAddress = emailAddress;
			this.target = target;
			this.offer = offer;
			this.securityTokenService = externalAuthentication.GetSecurityTokenService(user.OrganizationId);
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060003CA RID: 970 RVA: 0x0000FA2A File Offset: 0x0000DC2A
		public LocalizedException Exception
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x060003CB RID: 971 RVA: 0x0000FA32 File Offset: 0x0000DC32
		public override void Abort()
		{
			base.Abort();
			if (this.asyncResult != null)
			{
				this.securityTokenService.AbortIssueToken(this.asyncResult);
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060003CC RID: 972 RVA: 0x0000FA53 File Offset: 0x0000DC53
		public RequestedToken RequestedToken
		{
			get
			{
				return this.requestedToken;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060003CD RID: 973 RVA: 0x0000FA5B File Offset: 0x0000DC5B
		public Offer Offer
		{
			get
			{
				return this.offer;
			}
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0000FA64 File Offset: 0x0000DC64
		public override void BeginInvoke(TaskCompleteCallback callback)
		{
			base.BeginInvoke(callback);
			this.stopwatch = Stopwatch.StartNew();
			try
			{
				DelegationTokenRequest request = new DelegationTokenRequest
				{
					FederatedIdentity = this.user.GetFederatedIdentity(),
					EmailAddress = this.GetFederatedSmtpAddress().ToString(),
					Target = this.target,
					Offer = this.offer
				};
				this.asyncResult = this.securityTokenService.BeginIssueToken(request, new AsyncCallback(this.Complete), null);
			}
			catch (LocalizedException ex)
			{
				this.exception = ex;
				base.Complete();
			}
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0000FB10 File Offset: 0x0000DD10
		private void Complete(IAsyncResult asyncResult)
		{
			try
			{
				this.requestedToken = this.securityTokenService.EndIssueToken(asyncResult);
			}
			catch (LocalizedException ex)
			{
				this.exception = ex;
			}
			finally
			{
				this.stopwatch.Stop();
				this.requestLogger.Add(RequestStatistics.Create(RequestStatisticsType.FederatedToken, this.stopwatch.ElapsedMilliseconds));
				base.Complete();
			}
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0000FB8C File Offset: 0x0000DD8C
		public SmtpAddress GetFederatedSmtpAddress()
		{
			return this.user.GetFederatedSmtpAddress(this.emailAddress);
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0000FBA0 File Offset: 0x0000DDA0
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"ExternalAuthenticationRequest as user ",
				this.user.Id,
				" to ",
				this.target
			});
		}

		// Token: 0x04000241 RID: 577
		private RequestLogger requestLogger;

		// Token: 0x04000242 RID: 578
		private SecurityTokenService securityTokenService;

		// Token: 0x04000243 RID: 579
		private ADUser user;

		// Token: 0x04000244 RID: 580
		private SmtpAddress emailAddress;

		// Token: 0x04000245 RID: 581
		private TokenTarget target;

		// Token: 0x04000246 RID: 582
		private Offer offer;

		// Token: 0x04000247 RID: 583
		private RequestedToken requestedToken;

		// Token: 0x04000248 RID: 584
		private IAsyncResult asyncResult;

		// Token: 0x04000249 RID: 585
		private Stopwatch stopwatch;

		// Token: 0x0400024A RID: 586
		private LocalizedException exception;

		// Token: 0x0400024B RID: 587
		private static readonly Microsoft.Exchange.Diagnostics.Trace RequestRoutingTracer = ExTraceGlobals.RequestRoutingTracer;
	}
}
