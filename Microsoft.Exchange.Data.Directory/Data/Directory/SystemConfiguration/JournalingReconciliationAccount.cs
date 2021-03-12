using System;
using System.Management.Automation;
using System.Net;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000493 RID: 1171
	[Serializable]
	public class JournalingReconciliationAccount : ADConfigurationObject
	{
		// Token: 0x1700100D RID: 4109
		// (get) Token: 0x06003550 RID: 13648 RVA: 0x000D222D File Offset: 0x000D042D
		internal override ADObjectSchema Schema
		{
			get
			{
				return JournalingReconciliationAccount.schema;
			}
		}

		// Token: 0x1700100E RID: 4110
		// (get) Token: 0x06003551 RID: 13649 RVA: 0x000D2234 File Offset: 0x000D0434
		internal override string MostDerivedObjectClass
		{
			get
			{
				return "msExchJournalingReconciliationRemoteAccount";
			}
		}

		// Token: 0x1700100F RID: 4111
		// (get) Token: 0x06003552 RID: 13650 RVA: 0x000D223B File Offset: 0x000D043B
		internal override ADObjectId ParentPath
		{
			get
			{
				return JournalingReconciliationAccount.parentPath;
			}
		}

		// Token: 0x17001010 RID: 4112
		// (get) Token: 0x06003553 RID: 13651 RVA: 0x000D2242 File Offset: 0x000D0442
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x17001011 RID: 4113
		// (get) Token: 0x06003554 RID: 13652 RVA: 0x000D2249 File Offset: 0x000D0449
		// (set) Token: 0x06003555 RID: 13653 RVA: 0x000D225B File Offset: 0x000D045B
		[Parameter]
		public Uri ArchiveUri
		{
			get
			{
				return (Uri)this[JournalingReconciliationAccountSchema.ArchiveUri];
			}
			set
			{
				this[JournalingReconciliationAccountSchema.ArchiveUri] = value;
			}
		}

		// Token: 0x17001012 RID: 4114
		// (get) Token: 0x06003556 RID: 13654 RVA: 0x000D2269 File Offset: 0x000D0469
		// (set) Token: 0x06003557 RID: 13655 RVA: 0x000D227B File Offset: 0x000D047B
		[Parameter]
		public PSCredential AuthenticationCredential
		{
			get
			{
				return (PSCredential)this[JournalingReconciliationAccountSchema.AuthenticationCredential];
			}
			set
			{
				this[JournalingReconciliationAccountSchema.AuthenticationCredential] = value;
			}
		}

		// Token: 0x17001013 RID: 4115
		// (get) Token: 0x06003558 RID: 13656 RVA: 0x000D2289 File Offset: 0x000D0489
		// (set) Token: 0x06003559 RID: 13657 RVA: 0x000D229B File Offset: 0x000D049B
		public MultiValuedProperty<ADObjectId> Mailboxes
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[JournalingReconciliationAccountSchema.Mailboxes];
			}
			internal set
			{
				this[JournalingReconciliationAccountSchema.Mailboxes] = value;
			}
		}

		// Token: 0x0600355A RID: 13658 RVA: 0x000D22AC File Offset: 0x000D04AC
		internal NetworkCredential GetNetworkCredential()
		{
			string text = (string)this[JournalingReconciliationAccountSchema.UserName];
			string text2 = (string)this[JournalingReconciliationAccountSchema.Password];
			if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(text2))
			{
				return null;
			}
			return new NetworkCredential(text, text2);
		}

		// Token: 0x0400240F RID: 9231
		public const string TaskNoun = "JournalingReconciliationAccount";

		// Token: 0x04002410 RID: 9232
		public const string ReconciliationAccountContainer = "Journaling Reconciliation Accounts";

		// Token: 0x04002411 RID: 9233
		public const string TransportSettingsContainer = "Transport Settings";

		// Token: 0x04002412 RID: 9234
		private const string MostDerivedObjectClassInternal = "msExchJournalingReconciliationRemoteAccount";

		// Token: 0x04002413 RID: 9235
		private static readonly string RootDnRelative = "CN=Journaling Reconciliation Accounts,CN=Transport Settings";

		// Token: 0x04002414 RID: 9236
		private static readonly ADObjectId parentPath = new ADObjectId(JournalingReconciliationAccount.RootDnRelative);

		// Token: 0x04002415 RID: 9237
		private static readonly JournalingReconciliationAccountSchema schema = ObjectSchema.GetInstance<JournalingReconciliationAccountSchema>();
	}
}
