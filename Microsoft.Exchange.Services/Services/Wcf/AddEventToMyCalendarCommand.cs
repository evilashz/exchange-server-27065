using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000908 RID: 2312
	internal sealed class AddEventToMyCalendarCommand : ServiceCommand<CalendarActionResponse>
	{
		// Token: 0x06004311 RID: 17169 RVA: 0x000DFDA2 File Offset: 0x000DDFA2
		public AddEventToMyCalendarCommand(CallContext callContext, AddEventToMyCalendarRequest request) : base(callContext)
		{
			if (callContext == null)
			{
				throw new ArgumentNullException("callContext");
			}
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			this.Request = request;
		}

		// Token: 0x06004312 RID: 17170 RVA: 0x000DFDD0 File Offset: 0x000DDFD0
		protected override CalendarActionResponse InternalExecute()
		{
			IdAndSession idAndSession = base.IdConverter.ConvertItemIdToIdAndSessionReadWrite(this.Request.ItemId);
			if (idAndSession != null)
			{
				try
				{
					using (Item rootXsoItem = idAndSession.GetRootXsoItem(AddEventToMyCalendarCommand.forwardReplyPropertyDefinitionArray))
					{
						CalendarActionResponse calendarActionResponse = new AddEventToMyCalendarCommandAction(base.MailboxIdentityMailboxSession, base.CallContext.AccessingPrincipal, base.CallContext.ClientCulture, rootXsoItem).Execute();
						if (!calendarActionResponse.WasSuccessful)
						{
							throw new ServiceInvalidOperationException((CoreResources.IDs)4004906780U);
						}
						return calendarActionResponse;
					}
				}
				catch (ObjectNotFoundException)
				{
					AddEventToMyCalendarCommand.TraceError(this.GetHashCode(), "AddEventToMyCalendarCommand.InternalExecute(): ObjectNotFoundException: forwardingItem is not found for the IdAndSession {0}", new object[]
					{
						idAndSession
					});
					throw new ServiceInvalidOperationException((CoreResources.IDs)4005418156U);
				}
			}
			AddEventToMyCalendarCommand.TraceError(this.GetHashCode(), "AddEventToMyCalendarCommand.InternalExecute(): The IdAndSession is null for ItemId: {0}", new object[]
			{
				this.Request.ItemId
			});
			throw new ServiceInvalidOperationException((CoreResources.IDs)4005418156U);
		}

		// Token: 0x06004313 RID: 17171 RVA: 0x000DFEDC File Offset: 0x000DE0DC
		internal static void TraceDebug(int hashCode, string messageFormat, params object[] args)
		{
			ExTraceGlobals.AddEventToMyCalendarTracer.TraceDebug((long)hashCode, messageFormat, args);
		}

		// Token: 0x06004314 RID: 17172 RVA: 0x000DFEEC File Offset: 0x000DE0EC
		internal static void TraceError(int hashCode, string messageFormat, params object[] args)
		{
			ExTraceGlobals.AddEventToMyCalendarTracer.TraceError((long)hashCode, messageFormat, args);
		}

		// Token: 0x04002716 RID: 10006
		private readonly AddEventToMyCalendarRequest Request;

		// Token: 0x04002717 RID: 10007
		private static readonly PropertyDefinition[] forwardReplyPropertyDefinitionArray = new PropertyDefinition[]
		{
			ItemSchema.SentTime,
			ItemSchema.IsClassified,
			ItemSchema.Classification,
			ItemSchema.ClassificationGuid,
			ItemSchema.ClassificationDescription,
			ItemSchema.ClassificationKeep
		};
	}
}
