using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic
{
	// Token: 0x02000178 RID: 376
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IPeopleConnectApplicationConfig
	{
		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000EB6 RID: 3766
		string AppId { get; }

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000EB7 RID: 3767
		string AppSecretEncrypted { get; }

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000EB8 RID: 3768
		Func<string, string> DecryptAppSecret { get; }

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000EB9 RID: 3769
		string AppSecretClearText { get; }

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000EBA RID: 3770
		string AuthorizationEndpoint { get; }

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000EBB RID: 3771
		string GraphTokenEndpoint { get; }

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000EBC RID: 3772
		string GraphApiEndpoint { get; }

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06000EBD RID: 3773
		TimeSpan WebRequestTimeout { get; }

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06000EBE RID: 3774
		string RequestTokenEndpoint { get; }

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000EBF RID: 3775
		string AccessTokenEndpoint { get; }

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000EC0 RID: 3776
		string ProfileEndpoint { get; }

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000EC1 RID: 3777
		string ConnectionsEndpoint { get; }

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000EC2 RID: 3778
		string RemoveAppEndpoint { get; }

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000EC3 RID: 3779
		string ConsentRedirectEndpoint { get; }

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06000EC4 RID: 3780
		string WebProxyUri { get; }

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06000EC5 RID: 3781
		bool SkipContactUpload { get; }

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06000EC6 RID: 3782
		bool ContinueOnContactUploadFailure { get; }

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06000EC7 RID: 3783
		bool WaitForContactUploadCommit { get; }

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06000EC8 RID: 3784
		bool NotifyOnEachContactUpload { get; }

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06000EC9 RID: 3785
		int MaximumContactsToUpload { get; }

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06000ECA RID: 3786
		DateTime ReadTimeUtc { get; }

		// Token: 0x06000ECB RID: 3787
		IPeopleConnectApplicationConfig OverrideWith(IPeopleConnectApplicationConfig other);
	}
}
