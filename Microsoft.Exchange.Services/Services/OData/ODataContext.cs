using System;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net.Protocols;
using Microsoft.Exchange.Security.OAuth;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.OData.Model;
using Microsoft.Exchange.Services.OData.Web;
using Microsoft.OData.Core.UriParser;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Edm;

namespace Microsoft.Exchange.Services.OData
{
	// Token: 0x02000E2B RID: 3627
	internal class ODataContext : DisposeTrackableBase
	{
		// Token: 0x06005D79 RID: 23929 RVA: 0x00123368 File Offset: 0x00121568
		public ODataContext(HttpContext httpContext, Uri requestUri, ServiceModel serviceModel, ODataPathWrapper odataPath, ODataUriParser odataUriParser)
		{
			ArgumentValidator.ThrowIfNull("httpContext", httpContext);
			ArgumentValidator.ThrowIfNull("requestUri", requestUri);
			ArgumentValidator.ThrowIfNull("serviceModel", serviceModel);
			ArgumentValidator.ThrowIfNull("odataPath", odataPath);
			ArgumentValidator.ThrowIfNull("odataUriParser", odataUriParser);
			HttpContext.Current = httpContext;
			this.HttpContext = httpContext;
			this.RequestUri = requestUri;
			this.ServiceModel = serviceModel;
			this.ODataPath = odataPath;
			this.ODataQueryOptions = new ODataQueryOptions(httpContext, odataUriParser);
			this.InitializeCallContext();
		}

		// Token: 0x17001523 RID: 5411
		// (get) Token: 0x06005D7A RID: 23930 RVA: 0x001233EB File Offset: 0x001215EB
		// (set) Token: 0x06005D7B RID: 23931 RVA: 0x001233F3 File Offset: 0x001215F3
		public CallContext CallContext { get; private set; }

		// Token: 0x17001524 RID: 5412
		// (get) Token: 0x06005D7C RID: 23932 RVA: 0x001233FC File Offset: 0x001215FC
		// (set) Token: 0x06005D7D RID: 23933 RVA: 0x00123404 File Offset: 0x00121604
		public ServiceModel ServiceModel { get; private set; }

		// Token: 0x17001525 RID: 5413
		// (get) Token: 0x06005D7E RID: 23934 RVA: 0x0012340D File Offset: 0x0012160D
		// (set) Token: 0x06005D7F RID: 23935 RVA: 0x00123415 File Offset: 0x00121615
		public Uri RequestUri { get; private set; }

		// Token: 0x17001526 RID: 5414
		// (get) Token: 0x06005D80 RID: 23936 RVA: 0x0012341E File Offset: 0x0012161E
		// (set) Token: 0x06005D81 RID: 23937 RVA: 0x00123426 File Offset: 0x00121626
		public ODataPathWrapper ODataPath { get; private set; }

		// Token: 0x17001527 RID: 5415
		// (get) Token: 0x06005D82 RID: 23938 RVA: 0x0012342F File Offset: 0x0012162F
		public IEdmModel EdmModel
		{
			get
			{
				return this.ServiceModel.EdmModel;
			}
		}

		// Token: 0x17001528 RID: 5416
		// (get) Token: 0x06005D83 RID: 23939 RVA: 0x0012343C File Offset: 0x0012163C
		// (set) Token: 0x06005D84 RID: 23940 RVA: 0x00123444 File Offset: 0x00121644
		public ODataQueryOptions ODataQueryOptions { get; private set; }

		// Token: 0x17001529 RID: 5417
		// (get) Token: 0x06005D85 RID: 23941 RVA: 0x0012344D File Offset: 0x0012164D
		public IEdmEntityType EntityType
		{
			get
			{
				return this.ODataPath.EntityType;
			}
		}

		// Token: 0x1700152A RID: 5418
		// (get) Token: 0x06005D86 RID: 23942 RVA: 0x0012345A File Offset: 0x0012165A
		public IEdmNavigationSource NavigationSource
		{
			get
			{
				return this.ODataPath.NavigationSource;
			}
		}

		// Token: 0x1700152B RID: 5419
		// (get) Token: 0x06005D87 RID: 23943 RVA: 0x00123467 File Offset: 0x00121667
		// (set) Token: 0x06005D88 RID: 23944 RVA: 0x0012346F File Offset: 0x0012166F
		public HttpContext HttpContext { get; private set; }

		// Token: 0x1700152C RID: 5420
		// (get) Token: 0x06005D89 RID: 23945 RVA: 0x00123478 File Offset: 0x00121678
		// (set) Token: 0x06005D8A RID: 23946 RVA: 0x00123480 File Offset: 0x00121680
		public ADUser TargetMailbox { get; private set; }

		// Token: 0x1700152D RID: 5421
		// (get) Token: 0x06005D8B RID: 23947 RVA: 0x00123489 File Offset: 0x00121689
		public NameValueCollection QueryString
		{
			get
			{
				return this.HttpContext.Request.QueryString;
			}
		}

		// Token: 0x1700152E RID: 5422
		// (get) Token: 0x06005D8C RID: 23948 RVA: 0x0012349B File Offset: 0x0012169B
		public RequestDetailsLogger RequestDetailsLogger
		{
			get
			{
				return this.CallContext.ProtocolLog;
			}
		}

		// Token: 0x06005D8D RID: 23949 RVA: 0x001234A8 File Offset: 0x001216A8
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.CallContext != null)
			{
				this.CallContext.Dispose();
				this.CallContext = null;
			}
		}

		// Token: 0x06005D8E RID: 23950 RVA: 0x001234C7 File Offset: 0x001216C7
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ODataContext>(this);
		}

		// Token: 0x06005D8F RID: 23951 RVA: 0x001234D0 File Offset: 0x001216D0
		private void InitializeCallContext()
		{
			string impersonatedUser;
			string targetSmtpString;
			this.FindImpersonatedOrExplicitTargetUser(out impersonatedUser, out targetSmtpString);
			this.CallContext = CallContext.CreateForOData(this.HttpContext, impersonatedUser);
			this.TargetMailbox = this.CallContext.AccessingADUser;
			this.HandleExplicitTargetAccess(targetSmtpString);
			ExchangePrincipal exchangePrincipal;
			if (ExchangePrincipalCache.TryGetFromCache(this.TargetMailbox.Sid, this.CallContext.ADRecipientSessionContext, out exchangePrincipal) && !string.Equals(exchangePrincipal.MailboxInfo.Location.ServerFqdn, LocalServer.GetServer().Fqdn, StringComparison.OrdinalIgnoreCase))
			{
				WrongServerException ex = new WrongServerException(ServerStrings.PrincipalFromDifferentSite, exchangePrincipal.MailboxInfo.GetDatabaseGuid(), exchangePrincipal.MailboxInfo.Location.ServerFqdn, exchangePrincipal.MailboxInfo.Location.ServerVersion, null);
				string value = ex.RightServerToString();
				this.HttpContext.Response.Headers[WellKnownHeader.XDBMountedOnServer] = value;
				this.HttpContext.Response.Headers["X-BEServerException"] = typeof(IllegalCrossServerConnectionException).FullName;
				throw ex;
			}
			if (!this.CallContext.CallerHasAccess())
			{
				throw new ODataAuthorizationException(CoreResources.ErrorODataAccessDisabled);
			}
		}

		// Token: 0x06005D90 RID: 23952 RVA: 0x001235F8 File Offset: 0x001217F8
		private void FindImpersonatedOrExplicitTargetUser(out string impersonatedUser, out string explicitTargetUser)
		{
			impersonatedUser = null;
			explicitTargetUser = null;
			if (this.ODataPath.PathSegments.Count > 2)
			{
				EntitySetSegment entitySetSegment = this.ODataPath.FirstSegment as EntitySetSegment;
				if (entitySetSegment != null && entitySetSegment.EntitySet.Name.Equals(EntitySets.Users.Name))
				{
					KeySegment keySegment = this.ODataPath.PathSegments[1] as KeySegment;
					if (keySegment != null)
					{
						string idKey = keySegment.GetIdKey();
						if ("Me".Equals(idKey, StringComparison.OrdinalIgnoreCase))
						{
							return;
						}
						OAuthIdentity oauthIdentity = this.HttpContext.User.Identity as OAuthIdentity;
						if (oauthIdentity != null && oauthIdentity.OAuthApplication.ApplicationType == OAuthApplicationType.V1App && oauthIdentity.IsAppOnly)
						{
							impersonatedUser = idKey;
							return;
						}
						explicitTargetUser = idKey;
					}
				}
			}
		}

		// Token: 0x06005D91 RID: 23953 RVA: 0x001236D8 File Offset: 0x001218D8
		private void HandleExplicitTargetAccess(string targetSmtpString)
		{
			if (string.IsNullOrEmpty(targetSmtpString))
			{
				return;
			}
			ProxyAddress a = this.CallContext.AccessingADUser.EmailAddresses.FirstOrDefault((ProxyAddress x) => string.Equals(x.AddressString, targetSmtpString, StringComparison.OrdinalIgnoreCase));
			if (!(a == null))
			{
				return;
			}
			this.RequestDetailsLogger.AppendGenericInfo("ODataTargetMailbox", targetSmtpString);
			ADUser targetMailbox;
			if (ADIdentityInformationCache.Singleton.TryGetADUser(targetSmtpString, this.CallContext.ADRecipientSessionContext, out targetMailbox))
			{
				this.TargetMailbox = targetMailbox;
				this.CallContext.OwaExplicitLogonUser = this.TargetMailbox.PrimarySmtpAddress.ToString();
				return;
			}
			throw new InvalidUserException(targetSmtpString);
		}
	}
}
