using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A2F RID: 2607
	[Cmdlet("Remove", "ContentFilterPhrase", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High, DefaultParameterSetName = "Identity")]
	public sealed class RemoveContentFilterPhrase : RemoveTaskBase<ContentFilterPhraseIdParameter, ContentFilterPhrase>
	{
		// Token: 0x17001BF5 RID: 7157
		// (get) Token: 0x06005D40 RID: 23872 RVA: 0x00189206 File Offset: 0x00187406
		// (set) Token: 0x06005D41 RID: 23873 RVA: 0x0018920E File Offset: 0x0018740E
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

		// Token: 0x17001BF6 RID: 7158
		// (get) Token: 0x06005D42 RID: 23874 RVA: 0x00189217 File Offset: 0x00187417
		// (set) Token: 0x06005D43 RID: 23875 RVA: 0x0018921F File Offset: 0x0018741F
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

		// Token: 0x17001BF7 RID: 7159
		// (get) Token: 0x06005D44 RID: 23876 RVA: 0x00189228 File Offset: 0x00187428
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveContentFilterPhrase(base.DataObject.Identity.ToString());
			}
		}

		// Token: 0x06005D45 RID: 23877 RVA: 0x00189240 File Offset: 0x00187440
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			this.configurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(this.DomainController, false, ConsistencyMode.PartiallyConsistent, this.ConfigurationSession.SessionSettings, 72, "InternalBeginProcessing", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\MessageHygiene\\ContentFilter\\RemoveContentFilterPhrase.cs");
			TaskLogger.LogExit();
		}

		// Token: 0x06005D46 RID: 23878 RVA: 0x00189291 File Offset: 0x00187491
		protected override IConfigDataProvider CreateSession()
		{
			return new ContentFilterPhraseDataProvider(this.configurationSession);
		}

		// Token: 0x040034A7 RID: 13479
		private IConfigurationSession configurationSession;
	}
}
