using System;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Web;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Services;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.ServiceCommands.Anonymous
{
	// Token: 0x020002DB RID: 731
	internal abstract class BaseAnonymousCommand<TRequest, TResponse> where TRequest : BaseJsonRequest where TResponse : BaseJsonResponse
	{
		// Token: 0x060018A5 RID: 6309 RVA: 0x000550D0 File Offset: 0x000532D0
		protected BaseAnonymousCommand(TRequest request)
		{
			this.TraceDebug("BaseAnonymousCommand():Invoking request with PublishingUrl={0}, Request={1}", new object[]
			{
				this.Context.PublishingUrl,
				this.Request
			});
			this.Request = request;
			this.ValidateHeader();
			this.SetCallContext();
			ExchangeVersion.Current = ExchangeVersion.Latest;
			TRequest request2 = this.Request;
			EWSSettings.RequestTimeZone = request2.Header.TimeZoneContext.TimeZoneDefinition.ExTimeZone;
		}

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x060018A6 RID: 6310 RVA: 0x00055157 File Offset: 0x00053357
		protected AnonymousUserContext Context
		{
			get
			{
				return AnonymousUserContext.Current;
			}
		}

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x060018A7 RID: 6311 RVA: 0x0005515E File Offset: 0x0005335E
		// (set) Token: 0x060018A8 RID: 6312 RVA: 0x00055166 File Offset: 0x00053366
		private protected TRequest Request { protected get; private set; }

		// Token: 0x060018A9 RID: 6313 RVA: 0x00055170 File Offset: 0x00053370
		internal TResponse Execute()
		{
			this.ValidateRequestBody();
			TResponse result;
			try
			{
				this.TraceDebug("Creating the published folder instance", new object[0]);
				using (PublishedCalendar publishedCalendar = (PublishedCalendar)PublishedFolder.Create(this.Context.PublishingUrl))
				{
					publishedCalendar.TimeZone = EWSSettings.RequestTimeZone;
					this.UpdateRequestBody(publishedCalendar);
					this.TraceDebug("Invoking the command", new object[0]);
					result = this.InternalExecute(publishedCalendar);
				}
			}
			catch (OverBudgetException exception)
			{
				result = this.CreateErrorResponse(exception, ResponseCodeType.ErrorServerBusy);
			}
			catch (PublishedFolderAccessDeniedException exception2)
			{
				result = this.CreateErrorResponse(exception2, ResponseCodeType.ErrorAccessDenied);
			}
			catch (FolderNotPublishedException exception3)
			{
				result = this.CreateErrorResponse(exception3, ResponseCodeType.ErrorAccessDenied);
			}
			return result;
		}

		// Token: 0x060018AA RID: 6314 RVA: 0x00055244 File Offset: 0x00053444
		protected string GetExceptionMessage(Exception exception)
		{
			string message = exception.Message;
			while (string.IsNullOrEmpty(message) && exception != null)
			{
				message = exception.Message;
				exception = exception.InnerException;
			}
			return message;
		}

		// Token: 0x060018AB RID: 6315
		protected abstract TResponse CreateErrorResponse(Exception exception, ResponseCodeType codeType);

		// Token: 0x060018AC RID: 6316
		protected abstract void ValidateRequestBody();

		// Token: 0x060018AD RID: 6317
		protected abstract void UpdateRequestBody(PublishedCalendar publishedFolder);

		// Token: 0x060018AE RID: 6318
		protected abstract TResponse InternalExecute(PublishedCalendar publishedFolder);

		// Token: 0x060018AF RID: 6319 RVA: 0x00055275 File Offset: 0x00053475
		protected void TraceDebug(string message, params object[] args)
		{
			ExTraceGlobals.AnonymousServiceCommandTracer.TraceDebug(0L, message, args);
		}

		// Token: 0x060018B0 RID: 6320 RVA: 0x00055285 File Offset: 0x00053485
		protected void TraceError(string message, params object[] args)
		{
			ExTraceGlobals.AnonymousServiceCommandTracer.TraceError(0L, message, args);
		}

		// Token: 0x060018B1 RID: 6321 RVA: 0x00055298 File Offset: 0x00053498
		private void ValidateHeader()
		{
			TRequest request = this.Request;
			if (request.Header != null)
			{
				TRequest request2 = this.Request;
				if (request2.Header.TimeZoneContext != null)
				{
					TRequest request3 = this.Request;
					if (request3.Header.TimeZoneContext.TimeZoneDefinition != null)
					{
						return;
					}
				}
			}
			this.TraceError("Missing timezone header", new object[0]);
			throw new FaultException(new FaultReason("Missing timezone header"));
		}

		// Token: 0x060018B2 RID: 6322 RVA: 0x00055318 File Offset: 0x00053518
		private void SetCallContext()
		{
			CallContext callContext = (CallContext)FormatterServices.GetUninitializedObject(typeof(CallContext));
			callContext.EncodeStringProperties = Global.EncodeStringProperties;
			HttpContext.Current.Items["CallContext"] = callContext;
		}
	}
}
