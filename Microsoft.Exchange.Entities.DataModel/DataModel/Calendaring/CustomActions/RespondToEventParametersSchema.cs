using System;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Entities.DataModel.Calendaring.CustomActions
{
	// Token: 0x02000041 RID: 65
	public sealed class RespondToEventParametersSchema : EventWorkflowParametersSchema
	{
		// Token: 0x06000153 RID: 339 RVA: 0x00003DCE File Offset: 0x00001FCE
		public RespondToEventParametersSchema()
		{
			base.RegisterPropertyDefinition(RespondToEventParametersSchema.StaticMeetingRequestIdToBeDeletedProperty);
			base.RegisterPropertyDefinition(RespondToEventParametersSchema.StaticProposedStartTimeProperty);
			base.RegisterPropertyDefinition(RespondToEventParametersSchema.StaticProposedEndTimeProperty);
			base.RegisterPropertyDefinition(RespondToEventParametersSchema.StaticResponseProperty);
			base.RegisterPropertyDefinition(RespondToEventParametersSchema.StaticSendResponseProperty);
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00003E0D File Offset: 0x0000200D
		public TypedPropertyDefinition<string> MeetingRequestIdToBeDeletedProperty
		{
			get
			{
				return RespondToEventParametersSchema.StaticMeetingRequestIdToBeDeletedProperty;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00003E14 File Offset: 0x00002014
		public TypedPropertyDefinition<ExDateTime?> ProposedStartTimeProperty
		{
			get
			{
				return RespondToEventParametersSchema.StaticProposedStartTimeProperty;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000156 RID: 342 RVA: 0x00003E1B File Offset: 0x0000201B
		public TypedPropertyDefinition<ExDateTime?> ProposedEndTimeProperty
		{
			get
			{
				return RespondToEventParametersSchema.StaticProposedEndTimeProperty;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00003E22 File Offset: 0x00002022
		public TypedPropertyDefinition<ResponseType> ResponseProperty
		{
			get
			{
				return RespondToEventParametersSchema.StaticResponseProperty;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000158 RID: 344 RVA: 0x00003E29 File Offset: 0x00002029
		public TypedPropertyDefinition<bool> SendResponseProperty
		{
			get
			{
				return RespondToEventParametersSchema.StaticSendResponseProperty;
			}
		}

		// Token: 0x04000086 RID: 134
		private static readonly TypedPropertyDefinition<string> StaticMeetingRequestIdToBeDeletedProperty = new TypedPropertyDefinition<string>("RespondToEventParameters.MeetingRequestIdToBeDeleted", null, true);

		// Token: 0x04000087 RID: 135
		private static readonly TypedPropertyDefinition<ExDateTime?> StaticProposedStartTimeProperty = new TypedPropertyDefinition<ExDateTime?>("RespondToEventParameters.ProposedStartTime", null, true);

		// Token: 0x04000088 RID: 136
		private static readonly TypedPropertyDefinition<ExDateTime?> StaticProposedEndTimeProperty = new TypedPropertyDefinition<ExDateTime?>("RespondToEventParameters.ProposedEndTime", null, true);

		// Token: 0x04000089 RID: 137
		private static readonly TypedPropertyDefinition<ResponseType> StaticResponseProperty = new TypedPropertyDefinition<ResponseType>("RespondToEventParameters.Response", ResponseType.None, true);

		// Token: 0x0400008A RID: 138
		private static readonly TypedPropertyDefinition<bool> StaticSendResponseProperty = new TypedPropertyDefinition<bool>("RespondToEventParameters.SendResponse", false, true);
	}
}
