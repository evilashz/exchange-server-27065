using System;
using System.Collections.Specialized;
using System.Web;

namespace Microsoft.Exchange.Clients.Common.FBL
{
	// Token: 0x02000036 RID: 54
	internal class FblRequestParameters
	{
		// Token: 0x06000196 RID: 406 RVA: 0x0000B26C File Offset: 0x0000946C
		public FblRequestParameters(NameValueCollection queryParams)
		{
			if (!string.IsNullOrEmpty(queryParams["pd"]) && !ulong.TryParse(HttpUtility.UrlDecode(queryParams["pd"]), out this.Puid))
			{
				this.Puid = 0UL;
			}
			if (!string.IsNullOrEmpty(queryParams["aid"]))
			{
				this.PrimaryEmail = HttpUtility.UrlDecode(queryParams["aid"]);
			}
			this.CustomerGuid = new Guid(Convert.FromBase64String(AuthkeyAuthenticationRequest.UrlDecodeBase64String(queryParams["cid"])));
			this.MessageClass = HttpUtility.UrlDecode(queryParams["mc"]);
			this.MailGuid = new Guid(Convert.FromBase64String(AuthkeyAuthenticationRequest.UrlDecodeBase64String(queryParams["mg"])));
			this.OptIn = "1".Equals(queryParams["opt"]);
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0000B34F File Offset: 0x0000954F
		public FblRequestParameters(ulong puid, string primaryEmail, Guid customerGuid, bool optIn)
		{
			this.Puid = puid;
			this.PrimaryEmail = primaryEmail;
			this.CustomerGuid = customerGuid;
			this.OptIn = optIn;
			this.MailGuid = Guid.Empty;
			this.MessageClass = null;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x0000B386 File Offset: 0x00009586
		public FblRequestParameters(ulong puid, string primaryEmail, Guid customerGuid, Guid mailGuid, string messageClass)
		{
			this.Puid = puid;
			this.PrimaryEmail = primaryEmail;
			this.CustomerGuid = customerGuid;
			this.MessageClass = messageClass;
			this.MailGuid = mailGuid;
			this.OptIn = false;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0000B3BA File Offset: 0x000095BA
		public bool IsClassifyRequest()
		{
			return !string.IsNullOrEmpty(this.MessageClass);
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000B3CC File Offset: 0x000095CC
		public string ToQueryStringFragment()
		{
			NameValueCollection nameValueCollection = new NameValueCollection();
			if (this.Puid != 0UL)
			{
				nameValueCollection.Add("pd", this.Puid.ToString());
			}
			if (!string.IsNullOrEmpty(this.PrimaryEmail))
			{
				nameValueCollection.Add("aid", this.PrimaryEmail);
			}
			nameValueCollection.Add("cid", AuthkeyAuthenticationRequest.UrlEncodeBase64String(Convert.ToBase64String(this.CustomerGuid.ToByteArray())));
			nameValueCollection.Add("mc", this.MessageClass);
			nameValueCollection.Add("mg", AuthkeyAuthenticationRequest.UrlEncodeBase64String(Convert.ToBase64String(this.MailGuid.ToByteArray())));
			nameValueCollection.Add("opt", this.OptIn ? "1" : "0");
			return AuthkeyAuthenticationRequest.ConstructQueryString(nameValueCollection);
		}

		// Token: 0x040002FD RID: 765
		public readonly string PrimaryEmail;

		// Token: 0x040002FE RID: 766
		public readonly ulong Puid;

		// Token: 0x040002FF RID: 767
		public readonly Guid CustomerGuid;

		// Token: 0x04000300 RID: 768
		public readonly string MessageClass;

		// Token: 0x04000301 RID: 769
		public readonly Guid MailGuid;

		// Token: 0x04000302 RID: 770
		public readonly bool OptIn;
	}
}
