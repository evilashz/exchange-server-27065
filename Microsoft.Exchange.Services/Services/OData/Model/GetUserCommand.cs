using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000ECA RID: 3786
	internal class GetUserCommand : ODataCommand<GetUserRequest, GetUserResponse>
	{
		// Token: 0x06006246 RID: 25158 RVA: 0x00133915 File Offset: 0x00131B15
		public GetUserCommand(GetUserRequest request) : base(request)
		{
		}

		// Token: 0x06006247 RID: 25159 RVA: 0x00133920 File Offset: 0x00131B20
		protected override GetUserResponse InternalExecute()
		{
			UserQueryAdapter userQueryAdapter = new UserQueryAdapter(UserSchema.SchemaInstance, base.Request.ODataQueryOptions);
			User result;
			if (base.Request.Id.Equals("Me", StringComparison.OrdinalIgnoreCase))
			{
				ADUser accessingADUser = base.Request.ODataContext.CallContext.AccessingADUser;
				result = UserProvider.ADUserToEntity(accessingADUser, userQueryAdapter.RequestedProperties);
			}
			else
			{
				UserProvider userProvider = new UserProvider(base.Request.ODataContext.CallContext.ADRecipientSessionContext.GetADRecipientSession());
				result = userProvider.Read(base.Request.Id, userQueryAdapter);
			}
			return new GetUserResponse(base.Request)
			{
				Result = result
			};
		}
	}
}
