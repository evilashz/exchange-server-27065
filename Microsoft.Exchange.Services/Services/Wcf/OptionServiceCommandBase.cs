using System;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Diagnostics;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x0200097B RID: 2427
	internal abstract class OptionServiceCommandBase<TRequest, TResponse> : ServiceCommand<TResponse> where TResponse : OptionsResponseBase, new()
	{
		// Token: 0x06004593 RID: 17811 RVA: 0x000F448B File Offset: 0x000F268B
		protected OptionServiceCommandBase(CallContext callContext, TRequest request) : base(callContext)
		{
			this.request = request;
			this.instrumentationName = base.GetType().Name;
			OwsLogRegistry.Register(this.instrumentationName, typeof(DefaultOptionsActionMetadata), new Type[0]);
		}

		// Token: 0x06004594 RID: 17812 RVA: 0x000F44C8 File Offset: 0x000F26C8
		protected override TResponse InternalExecute()
		{
			this.LogRequestForDebug();
			TResponse tresponse;
			try
			{
				tresponse = this.CreateTaskAndExecute();
			}
			catch (CmdletException ex)
			{
				RequestDetailsLogger.Current.AppendGenericError("ErrCode", ((int)ex.ErrorCode).ToString());
				RequestDetailsLogger.Current.AppendGenericError("ErrMsg", ex.Message);
				tresponse = Activator.CreateInstance<TResponse>();
				tresponse.WasSuccessful = false;
				tresponse.ErrorCode = ex.ErrorCode;
				if (ex.ErrorCode == OptionsActionError.PromptUser)
				{
					tresponse.UserPrompt = ex.Message;
					tresponse.ErrorMessage = CoreResources.GetLocalizedString((CoreResources.IDs)2715027708U);
				}
				else
				{
					tresponse.ErrorMessage = ex.Message;
				}
			}
			this.LogResponseForDebug(tresponse);
			return tresponse;
		}

		// Token: 0x06004595 RID: 17813
		protected abstract TResponse CreateTaskAndExecute();

		// Token: 0x06004596 RID: 17814 RVA: 0x000F45A8 File Offset: 0x000F27A8
		protected void LogRequestForDebug()
		{
			ExTraceGlobals.OptionsTracer.TraceDebug<string, string, TRequest>((long)this.GetHashCode(), "[{0}] User = {1}, Request: {2}", this.instrumentationName, base.CallContext.AccessingPrincipal.Alias, this.request);
		}

		// Token: 0x06004597 RID: 17815 RVA: 0x000F45DC File Offset: 0x000F27DC
		protected void LogResponseForDebug(TResponse response)
		{
			ExTraceGlobals.OptionsTracer.TraceDebug<string, string, TResponse>((long)this.GetHashCode(), "[{0}] User = {1}, Response: {2}", this.instrumentationName, base.CallContext.AccessingPrincipal.Alias, response);
		}

		// Token: 0x0400286E RID: 10350
		public const string IdentityTaskPropertyName = "Identity";

		// Token: 0x0400286F RID: 10351
		public const string MailboxTaskPropertyName = "Mailbox";

		// Token: 0x04002870 RID: 10352
		protected readonly TRequest request;

		// Token: 0x04002871 RID: 10353
		private readonly string instrumentationName;
	}
}
