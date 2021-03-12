using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.DataModel;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.OData.Model;

namespace Microsoft.Exchange.Services.OData
{
	// Token: 0x02000E2A RID: 3626
	internal abstract class EntityContainersCommand<TRequest, TResponse> : ExchangeServiceCommand<TRequest, TResponse> where TRequest : ODataRequest where TResponse : ODataResponse
	{
		// Token: 0x06005D75 RID: 23925 RVA: 0x0012320C File Offset: 0x0012140C
		public EntityContainersCommand(TRequest request) : base(request)
		{
		}

		// Token: 0x17001522 RID: 5410
		// (get) Token: 0x06005D76 RID: 23926 RVA: 0x00123218 File Offset: 0x00121418
		protected virtual IExchangeEntityContainers EntityContainers
		{
			get
			{
				if (this.entityContainers == null)
				{
					TRequest request = base.Request;
					MailboxSession mailboxIdentityMailboxSession = request.ODataContext.CallContext.SessionCache.GetMailboxIdentityMailboxSession();
					this.entityContainers = new ExchangeEntityContainers(mailboxIdentityMailboxSession);
				}
				return this.entityContainers;
			}
		}

		// Token: 0x06005D77 RID: 23927 RVA: 0x00123264 File Offset: 0x00121464
		protected CommandContext CreateCommandContext(QueryAdapter queryAdapter = null)
		{
			CommandContext commandContext = new CommandContext();
			CommandContext commandContext2 = commandContext;
			TRequest request = base.Request;
			commandContext2.IfMatchETag = request.IfMatchETag;
			if (queryAdapter != null)
			{
				List<Microsoft.Exchange.Entities.DataModel.PropertyBags.PropertyDefinition> list = new List<Microsoft.Exchange.Entities.DataModel.PropertyBags.PropertyDefinition>();
				foreach (Microsoft.Exchange.Services.OData.Model.PropertyDefinition key in queryAdapter.RequestedProperties)
				{
					Microsoft.Exchange.Entities.DataModel.PropertyBags.PropertyDefinition item = null;
					if (Microsoft.Exchange.Services.OData.Model.EventSchema.ODataToEdmPropertyMap.TryGetValue(key, out item))
					{
						list.Add(item);
					}
				}
				commandContext.RequestedProperties = list;
				commandContext.PageSizeOnReread = queryAdapter.GetPageSize();
			}
			return commandContext;
		}

		// Token: 0x06005D78 RID: 23928 RVA: 0x00123308 File Offset: 0x00121508
		protected ICalendarReference GetCalendarContainer(string calendarId = null)
		{
			ICalendarReference result;
			if (string.IsNullOrEmpty(calendarId) || string.Equals(calendarId, DistinguishedFolderIdName.calendar.ToString(), StringComparison.OrdinalIgnoreCase))
			{
				result = this.EntityContainers.Calendaring.Calendars.Default;
			}
			else
			{
				result = this.EntityContainers.Calendaring.Calendars[EwsIdConverter.ODataIdToEwsId(calendarId)];
			}
			return result;
		}

		// Token: 0x04003277 RID: 12919
		private IExchangeEntityContainers entityContainers;
	}
}
