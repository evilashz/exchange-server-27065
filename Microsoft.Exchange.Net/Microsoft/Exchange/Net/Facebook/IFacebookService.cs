using System;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Web;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net.Facebook
{
	// Token: 0x0200072E RID: 1838
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[ServiceContract]
	internal interface IFacebookService
	{
		// Token: 0x06002318 RID: 8984
		[OperationContract(AsyncPattern = true)]
		[WebGet(UriTemplate = "/me/friends?access_token={accessToken}&fields={fields}&limit={limit}&offset={offset}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		IAsyncResult BeginGetFriends(string accessToken, string fields, string limit, string offset, AsyncCallback callback, object state);

		// Token: 0x06002319 RID: 8985
		FacebookUsersList EndGetFriends(IAsyncResult ar);

		// Token: 0x0600231A RID: 8986
		[OperationContract(AsyncPattern = true)]
		[GetUsersOperationBehavior]
		[WebGet(UriTemplate = "?access_token={accessToken}&ids={userIds}&fields={fields}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		IAsyncResult BeginGetUsers(string accessToken, string userIds, string fields, AsyncCallback callback, object state);

		// Token: 0x0600231B RID: 8987
		[WebGet(UriTemplate = "/me?access_token={accessToken}&fields={fields}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		FacebookUser GetProfile(string accessToken, string fields);

		// Token: 0x0600231C RID: 8988
		FacebookUsersList EndGetUsers(IAsyncResult ar);

		// Token: 0x0600231D RID: 8989
		[WebInvoke(Method = "DELETE", UriTemplate = "/me/permissions?access_token={accessToken}")]
		[OperationContract]
		void RemoveApplication(string accessToken);

		// Token: 0x0600231E RID: 8990
		[OperationContract]
		[WebInvoke(Method = "POST", UriTemplate = "/me/importcontacts?access_token={accessToken}&format={format}&encoding={encoding}&continuous={continuous}&async={async}&source={source}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		FacebookImportContactsResult ImportContacts(string accessToken, string format, string encoding, bool continuous, bool async, string source, Stream requestBody);
	}
}
