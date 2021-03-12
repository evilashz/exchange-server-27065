using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000ECD RID: 3789
	internal class FindUsersCommand : ODataCommand<FindUsersRequest, FindUsersResponse>
	{
		// Token: 0x0600624B RID: 25163 RVA: 0x001339E6 File Offset: 0x00131BE6
		public FindUsersCommand(FindUsersRequest request) : base(request)
		{
		}

		// Token: 0x0600624C RID: 25164 RVA: 0x001339F0 File Offset: 0x00131BF0
		protected override FindUsersResponse InternalExecute()
		{
			UserProvider userProvider = new UserProvider(base.Request.ODataContext.CallContext.ADRecipientSessionContext.GetADRecipientSession());
			IFindEntitiesResult<User> result = userProvider.Find(new UserQueryAdapter(UserSchema.SchemaInstance, base.Request.ODataQueryOptions));
			return new FindUsersResponse(base.Request)
			{
				Result = result
			};
		}
	}
}
