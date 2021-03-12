using System;
using System.Globalization;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.SystemConfigurationTasks;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SecureMail
{
	// Token: 0x02000088 RID: 136
	[Cmdlet("New", "MessageClassification", DefaultParameterSetName = "Identity", SupportsShouldProcess = true)]
	public sealed class NewMessageClassification : NewMultitenancySystemConfigurationObjectTask<MessageClassification>
	{
		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060004B1 RID: 1201 RVA: 0x00012175 File Offset: 0x00010375
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

		// Token: 0x060004B2 RID: 1202 RVA: 0x00012187 File Offset: 0x00010387
		public NewMessageClassification()
		{
			this.ClassificationID = Guid.NewGuid();
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x0001219A File Offset: 0x0001039A
		// (set) Token: 0x060004B4 RID: 1204 RVA: 0x000121A7 File Offset: 0x000103A7
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public Guid ClassificationID
		{
			get
			{
				return this.DataObject.ClassificationID;
			}
			set
			{
				this.DataObject.ClassificationID = value;
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x060004B5 RID: 1205 RVA: 0x000121B5 File Offset: 0x000103B5
		// (set) Token: 0x060004B6 RID: 1206 RVA: 0x000121C2 File Offset: 0x000103C2
		[Parameter(Mandatory = true)]
		public string DisplayName
		{
			get
			{
				return this.DataObject.DisplayName;
			}
			set
			{
				this.DataObject.DisplayName = value;
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060004B7 RID: 1207 RVA: 0x000121D0 File Offset: 0x000103D0
		// (set) Token: 0x060004B8 RID: 1208 RVA: 0x000121DD File Offset: 0x000103DD
		[Parameter(Mandatory = true)]
		public string SenderDescription
		{
			get
			{
				return this.DataObject.SenderDescription;
			}
			set
			{
				this.DataObject.SenderDescription = value;
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x060004B9 RID: 1209 RVA: 0x000121EB File Offset: 0x000103EB
		// (set) Token: 0x060004BA RID: 1210 RVA: 0x000121F8 File Offset: 0x000103F8
		[Parameter(Mandatory = false)]
		public string RecipientDescription
		{
			get
			{
				return this.DataObject.RecipientDescription;
			}
			set
			{
				this.DataObject.RecipientDescription = value;
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x060004BB RID: 1211 RVA: 0x00012206 File Offset: 0x00010406
		// (set) Token: 0x060004BC RID: 1212 RVA: 0x00012213 File Offset: 0x00010413
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public ClassificationDisplayPrecedenceLevel DisplayPrecedence
		{
			get
			{
				return this.DataObject.DisplayPrecedence;
			}
			set
			{
				this.DataObject.DisplayPrecedence = value;
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x060004BD RID: 1213 RVA: 0x00012221 File Offset: 0x00010421
		// (set) Token: 0x060004BE RID: 1214 RVA: 0x0001222E File Offset: 0x0001042E
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public bool PermissionMenuVisible
		{
			get
			{
				return this.DataObject.PermissionMenuVisible;
			}
			set
			{
				this.DataObject.PermissionMenuVisible = value;
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x060004BF RID: 1215 RVA: 0x0001223C File Offset: 0x0001043C
		// (set) Token: 0x060004C0 RID: 1216 RVA: 0x00012249 File Offset: 0x00010449
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public bool RetainClassificationEnabled
		{
			get
			{
				return this.DataObject.RetainClassificationEnabled;
			}
			set
			{
				this.DataObject.RetainClassificationEnabled = value;
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x060004C1 RID: 1217 RVA: 0x00012257 File Offset: 0x00010457
		// (set) Token: 0x060004C2 RID: 1218 RVA: 0x0001225F File Offset: 0x0001045F
		[Parameter(Mandatory = true, ParameterSetName = "Localized")]
		public CultureInfo Locale
		{
			get
			{
				return this.locale;
			}
			set
			{
				this.locale = value;
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x060004C3 RID: 1219 RVA: 0x00012268 File Offset: 0x00010468
		// (set) Token: 0x060004C4 RID: 1220 RVA: 0x00012270 File Offset: 0x00010470
		[Parameter(Mandatory = false)]
		public override SwitchParameter IgnoreDehydratedFlag { get; set; }

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x060004C5 RID: 1221 RVA: 0x00012279 File Offset: 0x00010479
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewMessageClassificationName(base.Name.ToString(), this.DisplayName.ToString(), this.SenderDescription.ToString());
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x060004C6 RID: 1222 RVA: 0x000122A1 File Offset: 0x000104A1
		protected override ObjectId RootId
		{
			get
			{
				return MessageClassificationIdParameter.DefaultRoot(base.DataSession);
			}
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x000122B0 File Offset: 0x000104B0
		protected override IConfigurable PrepareDataObject()
		{
			IConfigurationSession configurationSession = (IConfigurationSession)base.DataSession;
			MessageClassification messageClassification = (MessageClassification)base.PrepareDataObject();
			if (!messageClassification.IsModified(ClassificationSchema.RecipientDescription))
			{
				this.RecipientDescription = this.SenderDescription;
			}
			OrFilter filter = new OrFilter(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, base.Name),
				new ComparisonFilter(ComparisonOperator.Equal, ClassificationSchema.ClassificationID, this.ClassificationID)
			});
			MessageClassification[] array = configurationSession.Find<MessageClassification>((ADObjectId)this.RootId, QueryScope.SubTree, filter, null, 1);
			ADObjectId adobjectId = configurationSession.GetOrgContainerId().GetDescendantId(MessageClassificationIdParameter.MessageClassificationRdn);
			if (this.Locale != null)
			{
				if (array == null || array.Length == 0)
				{
					base.WriteError(new ManagementObjectNotFoundException(Strings.ErrorDefaultLocaleClassificationDoesNotExist(base.Name)), ErrorCategory.ObjectNotFound, messageClassification);
				}
				else
				{
					MessageClassification messageClassification2 = array[0];
					this.DataObject.ClassificationID = messageClassification2.ClassificationID;
					this.DataObject.DisplayPrecedence = messageClassification2.DisplayPrecedence;
					this.DataObject.PermissionMenuVisible = messageClassification2.PermissionMenuVisible;
					this.DataObject.RetainClassificationEnabled = messageClassification2.RetainClassificationEnabled;
					adobjectId = adobjectId.GetChildId(this.Locale.ToString());
				}
			}
			else
			{
				if (array.Length > 0)
				{
					string name = array[0].Name;
					base.WriteError(new ManagementObjectAmbiguousException(Strings.ErrorClassificationAlreadyDefined(name, this.ClassificationID)), ErrorCategory.InvalidOperation, base.Name);
				}
				adobjectId = (ADObjectId)this.RootId;
			}
			messageClassification.SetId(adobjectId.GetChildId(base.Name));
			return messageClassification;
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x0001243C File Offset: 0x0001063C
		protected override void InternalValidate()
		{
			IConfigurationSession configurationSession = (IConfigurationSession)base.DataSession;
			if (Server.IsSubscribedGateway(base.GlobalConfigSession))
			{
				base.WriteError(new CannotRunOnSubscribedEdgeException(), ErrorCategory.InvalidOperation, null);
			}
			MessageClassification[] array = configurationSession.Find<MessageClassification>(configurationSession.GetOrgContainerId().GetDescendantId(MessageClassificationIdParameter.MessageClassificationRdn), QueryScope.SubTree, null, null, NewMessageClassification.MaxMessageClassificationsPerTenant);
			if (base.OrganizationId != OrganizationId.ForestWideOrgId && array.Length == NewMessageClassification.MaxMessageClassificationsPerTenant)
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorExceededMaxClassificationsLimit(NewMessageClassification.MaxMessageClassificationsPerTenant)), ErrorCategory.InvalidOperation, null);
			}
			base.InternalValidate();
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x000124CC File Offset: 0x000106CC
		protected override void InternalProcessRecord()
		{
			if (SharedConfiguration.IsSharedConfiguration(this.DataObject.OrganizationId) && !base.ShouldContinue(Strings.ConfirmSharedConfiguration(this.DataObject.OrganizationId.OrganizationalUnit.Name)))
			{
				TaskLogger.LogExit();
				return;
			}
			if (base.DataSession.Read<Container>(this.DataObject.Id.Parent) == null)
			{
				Container container = new Container();
				container.SetId(this.DataObject.Id.Parent);
				base.DataSession.Save(container);
			}
			base.InternalProcessRecord();
		}

		// Token: 0x04000193 RID: 403
		private const string DefaultParameterSet = "Identity";

		// Token: 0x04000194 RID: 404
		private const string LocalizedParameterSet = "Localized";

		// Token: 0x04000195 RID: 405
		internal static readonly int MaxMessageClassificationsPerTenant = 15;

		// Token: 0x04000196 RID: 406
		private CultureInfo locale;
	}
}
