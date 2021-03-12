using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A2E RID: 2606
	[Cmdlet("Get", "ContentFilterPhrase", DefaultParameterSetName = "Identity")]
	public sealed class GetContentFilterPhrase : GetObjectWithIdentityTaskBase<ContentFilterPhraseIdParameter, ContentFilterPhrase>
	{
		// Token: 0x17001BF2 RID: 7154
		// (get) Token: 0x06005D38 RID: 23864 RVA: 0x00189160 File Offset: 0x00187360
		// (set) Token: 0x06005D39 RID: 23865 RVA: 0x00189168 File Offset: 0x00187368
		[Parameter]
		public new Fqdn DomainController
		{
			get
			{
				return base.DomainController;
			}
			set
			{
				base.DomainController = value;
			}
		}

		// Token: 0x17001BF3 RID: 7155
		// (get) Token: 0x06005D3A RID: 23866 RVA: 0x00189171 File Offset: 0x00187371
		// (set) Token: 0x06005D3B RID: 23867 RVA: 0x00189179 File Offset: 0x00187379
		[Parameter(Mandatory = false, ValueFromPipeline = false, ParameterSetName = "Phrase")]
		public ContentFilterPhraseIdParameter Phrase
		{
			get
			{
				return this.Identity;
			}
			set
			{
				this.Identity = value;
			}
		}

		// Token: 0x17001BF4 RID: 7156
		// (get) Token: 0x06005D3C RID: 23868 RVA: 0x00189182 File Offset: 0x00187382
		protected override QueryFilter InternalFilter
		{
			get
			{
				if (this.Phrase == null)
				{
					return null;
				}
				return new ContentFilterPhraseQueryFilter(this.Phrase.RawIdentity);
			}
		}

		// Token: 0x06005D3D RID: 23869 RVA: 0x001891A0 File Offset: 0x001873A0
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			this.configurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(this.DomainController, false, ConsistencyMode.PartiallyConsistent, this.ConfigurationSession.SessionSettings, 69, "InternalBeginProcessing", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\MessageHygiene\\ContentFilter\\GetContentFilterPhrase.cs");
			TaskLogger.LogExit();
		}

		// Token: 0x06005D3E RID: 23870 RVA: 0x001891F1 File Offset: 0x001873F1
		protected override IConfigDataProvider CreateSession()
		{
			return new ContentFilterPhraseDataProvider(this.configurationSession);
		}

		// Token: 0x040034A6 RID: 13478
		private IConfigurationSession configurationSession;
	}
}
