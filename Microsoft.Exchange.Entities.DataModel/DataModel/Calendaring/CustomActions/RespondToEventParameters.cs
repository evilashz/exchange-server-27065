using System;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Entities.DataModel.Calendaring.CustomActions
{
	// Token: 0x02000040 RID: 64
	public sealed class RespondToEventParameters : EventWorkflowParameters<RespondToEventParametersSchema>
	{
		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000148 RID: 328 RVA: 0x00003D03 File Offset: 0x00001F03
		// (set) Token: 0x06000149 RID: 329 RVA: 0x00003D16 File Offset: 0x00001F16
		public string MeetingRequestIdToBeDeleted
		{
			get
			{
				return base.GetPropertyValueOrDefault<string>(base.Schema.MeetingRequestIdToBeDeletedProperty);
			}
			set
			{
				base.SetPropertyValue<string>(base.Schema.MeetingRequestIdToBeDeletedProperty, value);
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600014A RID: 330 RVA: 0x00003D2A File Offset: 0x00001F2A
		// (set) Token: 0x0600014B RID: 331 RVA: 0x00003D3D File Offset: 0x00001F3D
		public ExDateTime? ProposedStartTime
		{
			get
			{
				return base.GetPropertyValueOrDefault<ExDateTime?>(base.Schema.ProposedStartTimeProperty);
			}
			set
			{
				base.SetPropertyValue<ExDateTime?>(base.Schema.ProposedStartTimeProperty, value);
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00003D51 File Offset: 0x00001F51
		// (set) Token: 0x0600014D RID: 333 RVA: 0x00003D64 File Offset: 0x00001F64
		public ExDateTime? ProposedEndTime
		{
			get
			{
				return base.GetPropertyValueOrDefault<ExDateTime?>(base.Schema.ProposedEndTimeProperty);
			}
			set
			{
				base.SetPropertyValue<ExDateTime?>(base.Schema.ProposedEndTimeProperty, value);
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600014E RID: 334 RVA: 0x00003D78 File Offset: 0x00001F78
		// (set) Token: 0x0600014F RID: 335 RVA: 0x00003D8B File Offset: 0x00001F8B
		public ResponseType Response
		{
			get
			{
				return base.GetPropertyValueOrDefault<ResponseType>(base.Schema.ResponseProperty);
			}
			set
			{
				base.SetPropertyValue<ResponseType>(base.Schema.ResponseProperty, value);
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000150 RID: 336 RVA: 0x00003D9F File Offset: 0x00001F9F
		// (set) Token: 0x06000151 RID: 337 RVA: 0x00003DB2 File Offset: 0x00001FB2
		public bool SendResponse
		{
			get
			{
				return base.GetPropertyValueOrDefault<bool>(base.Schema.SendResponseProperty);
			}
			set
			{
				base.SetPropertyValue<bool>(base.Schema.SendResponseProperty, value);
			}
		}
	}
}
