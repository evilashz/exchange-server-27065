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
	// Token: 0x02000A2C RID: 2604
	[Cmdlet("Add", "ContentFilterPhrase", SupportsShouldProcess = true)]
	public sealed class AddContentFilterPhrase : NewTaskBase<ContentFilterPhrase>
	{
		// Token: 0x17001BED RID: 7149
		// (get) Token: 0x06005D2A RID: 23850 RVA: 0x00188FF1 File Offset: 0x001871F1
		// (set) Token: 0x06005D2B RID: 23851 RVA: 0x00188FF9 File Offset: 0x001871F9
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

		// Token: 0x17001BEE RID: 7150
		// (get) Token: 0x06005D2C RID: 23852 RVA: 0x00189002 File Offset: 0x00187202
		// (set) Token: 0x06005D2D RID: 23853 RVA: 0x0018900F File Offset: 0x0018720F
		[Alias(new string[]
		{
			"Identity"
		})]
		[Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
		public string Phrase
		{
			get
			{
				return this.DataObject.Phrase;
			}
			set
			{
				this.DataObject.Phrase = value;
			}
		}

		// Token: 0x17001BEF RID: 7151
		// (get) Token: 0x06005D2E RID: 23854 RVA: 0x0018901D File Offset: 0x0018721D
		// (set) Token: 0x06005D2F RID: 23855 RVA: 0x0018902A File Offset: 0x0018722A
		[Parameter(Mandatory = true, ValueFromPipeline = true)]
		public Influence Influence
		{
			get
			{
				return this.DataObject.Influence;
			}
			set
			{
				this.DataObject.Influence = value;
			}
		}

		// Token: 0x17001BF0 RID: 7152
		// (get) Token: 0x06005D30 RID: 23856 RVA: 0x00189038 File Offset: 0x00187238
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageAddContentFilterPhrase(this.Phrase, this.Influence.ToString());
			}
		}

		// Token: 0x06005D31 RID: 23857 RVA: 0x00189058 File Offset: 0x00187258
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			this.configurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(this.DomainController, false, ConsistencyMode.PartiallyConsistent, this.ConfigurationSession.SessionSettings, 82, "InternalBeginProcessing", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\MessageHygiene\\ContentFilter\\AddContentFilterPhrase.cs");
			TaskLogger.LogExit();
		}

		// Token: 0x06005D32 RID: 23858 RVA: 0x001890A9 File Offset: 0x001872A9
		protected override IConfigDataProvider CreateSession()
		{
			return new ContentFilterPhraseDataProvider(this.configurationSession);
		}

		// Token: 0x06005D33 RID: 23859 RVA: 0x001890B8 File Offset: 0x001872B8
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (this.Exists(this.DataObject.Phrase))
			{
				base.WriteError(new ArgumentException(Strings.DuplicateContentFilterPhrase(this.DataObject.Phrase), "Phrase"), ErrorCategory.InvalidArgument, this.DataObject);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06005D34 RID: 23860 RVA: 0x00189114 File Offset: 0x00187314
		private bool Exists(string phrase)
		{
			ContentFilterPhraseQueryFilter filter = new ContentFilterPhraseQueryFilter(this.DataObject.Phrase);
			IConfigurable[] array = base.DataSession.Find<ContentFilterPhrase>(filter, null, false, null);
			return array != null && array.Length > 0;
		}

		// Token: 0x040034A5 RID: 13477
		private IConfigurationSession configurationSession;
	}
}
