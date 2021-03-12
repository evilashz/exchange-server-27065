using System;
using System.Net;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net.LinkedIn
{
	// Token: 0x02000745 RID: 1861
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class LinkedInAppConfig
	{
		// Token: 0x17000972 RID: 2418
		// (get) Token: 0x0600242C RID: 9260 RVA: 0x0004B712 File Offset: 0x00049912
		// (set) Token: 0x0600242D RID: 9261 RVA: 0x0004B71A File Offset: 0x0004991A
		public string AppId { get; private set; }

		// Token: 0x17000973 RID: 2419
		// (get) Token: 0x0600242E RID: 9262 RVA: 0x0004B723 File Offset: 0x00049923
		// (set) Token: 0x0600242F RID: 9263 RVA: 0x0004B72B File Offset: 0x0004992B
		public string AppSecret { get; private set; }

		// Token: 0x17000974 RID: 2420
		// (get) Token: 0x06002430 RID: 9264 RVA: 0x0004B734 File Offset: 0x00049934
		// (set) Token: 0x06002431 RID: 9265 RVA: 0x0004B73C File Offset: 0x0004993C
		public string ProfileEndpoint { get; private set; }

		// Token: 0x17000975 RID: 2421
		// (get) Token: 0x06002432 RID: 9266 RVA: 0x0004B745 File Offset: 0x00049945
		// (set) Token: 0x06002433 RID: 9267 RVA: 0x0004B74D File Offset: 0x0004994D
		public string ConnectionsEndpoint { get; private set; }

		// Token: 0x17000976 RID: 2422
		// (get) Token: 0x06002434 RID: 9268 RVA: 0x0004B756 File Offset: 0x00049956
		// (set) Token: 0x06002435 RID: 9269 RVA: 0x0004B75E File Offset: 0x0004995E
		public string RemoveAppEndpoint { get; private set; }

		// Token: 0x17000977 RID: 2423
		// (get) Token: 0x06002436 RID: 9270 RVA: 0x0004B767 File Offset: 0x00049967
		// (set) Token: 0x06002437 RID: 9271 RVA: 0x0004B76F File Offset: 0x0004996F
		public TimeSpan WebRequestTimeout { get; private set; }

		// Token: 0x17000978 RID: 2424
		// (get) Token: 0x06002438 RID: 9272 RVA: 0x0004B778 File Offset: 0x00049978
		// (set) Token: 0x06002439 RID: 9273 RVA: 0x0004B780 File Offset: 0x00049980
		public IWebProxy WebProxy { get; private set; }

		// Token: 0x0600243A RID: 9274 RVA: 0x0004B78C File Offset: 0x0004998C
		public LinkedInAppConfig(string appId, string appSecret, string profileEndpoint, string connectionsEndpoint, string removeAppEndpoint, TimeSpan webRequestTimeout, string webProxyUri)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("appId", appId);
			ArgumentValidator.ThrowIfNullOrEmpty("appSecret", appSecret);
			ArgumentValidator.ThrowIfNullOrEmpty("profileEndpoint", profileEndpoint);
			ArgumentValidator.ThrowIfNullOrEmpty("connectionsEndpoint", connectionsEndpoint);
			ArgumentValidator.ThrowIfNullOrEmpty("removeAppEndpoint", removeAppEndpoint);
			this.AppId = appId;
			this.AppSecret = appSecret;
			this.ProfileEndpoint = profileEndpoint;
			this.ConnectionsEndpoint = connectionsEndpoint;
			this.RemoveAppEndpoint = removeAppEndpoint;
			this.WebRequestTimeout = (TimeSpan.Zero.Equals(webRequestTimeout) ? LinkedInAppConfig.DefaultWebRequestTimeout : webRequestTimeout);
			this.WebProxy = (string.IsNullOrWhiteSpace(webProxyUri) ? null : new WebProxy(webProxyUri));
		}

		// Token: 0x040021FC RID: 8700
		private static readonly TimeSpan DefaultWebRequestTimeout = TimeSpan.FromSeconds(10.0);
	}
}
