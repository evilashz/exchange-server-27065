using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SecureMail
{
	// Token: 0x02000086 RID: 134
	[Cmdlet("Get", "MessageClassification", DefaultParameterSetName = "Identity")]
	public sealed class GetMessageClassification : GetMultitenancySystemConfigurationObjectTask<MessageClassificationIdParameter, MessageClassification>
	{
		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x060004A2 RID: 1186 RVA: 0x00011ECC File Offset: 0x000100CC
		// (set) Token: 0x060004A3 RID: 1187 RVA: 0x00011ED4 File Offset: 0x000100D4
		[Parameter(Mandatory = false)]
		public SwitchParameter IgnoreDehydratedFlag { get; set; }

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x060004A4 RID: 1188 RVA: 0x00011EDD File Offset: 0x000100DD
		protected override SharedTenantConfigurationMode SharedTenantConfigurationMode
		{
			get
			{
				if (!this.IgnoreDehydratedFlag)
				{
					return SharedTenantConfigurationMode.Dehydrateable;
				}
				return SharedTenantConfigurationMode.NotShared;
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060004A5 RID: 1189 RVA: 0x00011EEF File Offset: 0x000100EF
		// (set) Token: 0x060004A6 RID: 1190 RVA: 0x00011EF7 File Offset: 0x000100F7
		[Parameter(Mandatory = false)]
		public SwitchParameter IncludeLocales
		{
			get
			{
				return this.includeLocales;
			}
			set
			{
				this.includeLocales = value;
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060004A7 RID: 1191 RVA: 0x00011F00 File Offset: 0x00010100
		protected override ObjectId RootId
		{
			get
			{
				if (this.Identity != null)
				{
					return null;
				}
				return MessageClassificationIdParameter.DefaultRoot(base.DataSession);
			}
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x00011F18 File Offset: 0x00010118
		protected override IConfigDataProvider CreateSession()
		{
			IConfigDataProvider configDataProvider = base.CreateSession();
			((IConfigurationSession)configDataProvider).SessionSettings.IsSharedConfigChecked = true;
			((IConfigurationSession)configDataProvider).SessionSettings.IsRedirectedToSharedConfig = false;
			return configDataProvider;
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x00011F50 File Offset: 0x00010150
		internal override IConfigurationSession CreateConfigurationSession(ADSessionSettings sessionSettings)
		{
			IConfigurationSession configurationSession = base.CreateConfigurationSession(sessionSettings);
			configurationSession.SessionSettings.IsSharedConfigChecked = true;
			configurationSession.SessionSettings.IsRedirectedToSharedConfig = false;
			return configurationSession;
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x00011F7E File Offset: 0x0001017E
		protected override void InternalValidate()
		{
			if (base.OptionalIdentityData != null)
			{
				base.OptionalIdentityData.ConfigurationContainerRdn = MessageClassificationIdParameter.DefaultsRoot;
			}
			base.InternalValidate();
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x00011FA0 File Offset: 0x000101A0
		protected override void WriteResult(IConfigurable dataObject)
		{
			MessageClassification messageClassification = dataObject as MessageClassification;
			base.WriteResult(dataObject);
			if (this.IncludeLocales && messageClassification != null && messageClassification.IsDefault)
			{
				foreach (MessageClassification messageClassification2 in base.DataSession.FindPaged<MessageClassification>(new ComparisonFilter(ComparisonOperator.Equal, ClassificationSchema.ClassificationID, messageClassification.ClassificationID), MessageClassificationIdParameter.DefaultRoot(base.DataSession).Parent, true, null, 0))
				{
					if (!messageClassification2.IsDefault)
					{
						base.WriteResult(messageClassification2);
					}
				}
			}
		}

		// Token: 0x04000190 RID: 400
		private SwitchParameter includeLocales = new SwitchParameter(false);
	}
}
