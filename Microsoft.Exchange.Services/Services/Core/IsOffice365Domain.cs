using System;
using System.Diagnostics;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Diagnostics;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000344 RID: 836
	internal sealed class IsOffice365Domain : SingleStepServiceCommand<IsOffice365DomainRequest, bool>
	{
		// Token: 0x0600179A RID: 6042 RVA: 0x0007E524 File Offset: 0x0007C724
		public IsOffice365Domain(CallContext callContext, IsOffice365DomainRequest request) : base(callContext, request)
		{
			OwsLogRegistry.Register("IsOffice365Domain", typeof(IsOffice365DomainMetadata), new Type[0]);
			this.EmailAddress = SmtpAddress.Parse(request.EmailAddress);
		}

		// Token: 0x0600179B RID: 6043 RVA: 0x0007E598 File Offset: 0x0007C798
		internal override IExchangeWebMethodResponse GetResponse()
		{
			return new IsOffice365DomainResponse(base.Result.Code, base.Result.Error, base.Result.Value);
		}

		// Token: 0x0600179C RID: 6044 RVA: 0x0007E65C File Offset: 0x0007C85C
		internal override ServiceResult<bool> Execute()
		{
			ServiceResult<bool> result = null;
			this.ExecuteWithProtocolLogging(IsOffice365DomainMetadata.TotalTime, delegate
			{
				try
				{
					ADSessionSettings.FromTenantAcceptedDomain(this.EmailAddress.Domain);
					result = new ServiceResult<bool>(true);
				}
				catch (CannotResolveTenantNameException arg)
				{
					this.tracer.TraceInformation<string, SmtpAddress, CannotResolveTenantNameException>(this.GetHashCode(), 0L, "Failed to find Office365 tenant with domain name: {0} for email address {1}. Exception: {2}", this.EmailAddress.Domain, this.EmailAddress, arg);
					result = new ServiceResult<bool>(false);
				}
			});
			return result;
		}

		// Token: 0x0600179D RID: 6045 RVA: 0x0007E69C File Offset: 0x0007C89C
		private void ExecuteWithProtocolLogging(Enum logMetadata, Action operation)
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			operation();
			stopwatch.Stop();
			this.requestDetailsLogger_Set.Value(base.CallContext.ProtocolLog, logMetadata, stopwatch.ElapsedMilliseconds);
		}

		// Token: 0x04000FD8 RID: 4056
		private readonly SmtpAddress EmailAddress;

		// Token: 0x04000FD9 RID: 4057
		private readonly Microsoft.Exchange.Diagnostics.Trace tracer = ExTraceGlobals.IsOffice365DomainTracer;

		// Token: 0x04000FDA RID: 4058
		internal readonly Hookable<Func<RequestDetailsLogger, Enum, object, string>> requestDetailsLogger_Set = Hookable<Func<RequestDetailsLogger, Enum, object, string>>.Create(true, (RequestDetailsLogger protocolLog, Enum property, object value) => protocolLog.Set(property, value));
	}
}
