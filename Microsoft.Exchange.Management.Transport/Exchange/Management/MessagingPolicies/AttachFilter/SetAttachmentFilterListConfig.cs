using System;
using System.IO;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.MessagingPolicies.AttachFilter
{
	// Token: 0x02000011 RID: 17
	[Cmdlet("set", "attachmentfilterlistconfig", SupportsShouldProcess = true)]
	public class SetAttachmentFilterListConfig : SetSingletonSystemConfigurationObjectTask<AttachmentFilteringConfig>
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00003964 File Offset: 0x00001B64
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetAttachmentfilterlistconfig;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000061 RID: 97 RVA: 0x0000396B File Offset: 0x00001B6B
		protected override ObjectId RootId
		{
			get
			{
				return ((IConfigurationSession)base.DataSession).GetOrgContainerId();
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000062 RID: 98 RVA: 0x0000397D File Offset: 0x00001B7D
		// (set) Token: 0x06000063 RID: 99 RVA: 0x00003994 File Offset: 0x00001B94
		[Parameter]
		public MultiValuedProperty<ReceiveConnectorIdParameter> ExceptionConnectors
		{
			get
			{
				return (MultiValuedProperty<ReceiveConnectorIdParameter>)base.Fields["ExceptionConnectors"];
			}
			set
			{
				base.Fields["ExceptionConnectors"] = value;
			}
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000039A7 File Offset: 0x00001BA7
		public SetAttachmentFilterListConfig()
		{
			base.Fields["ExceptionConnectors"] = null;
			base.Fields.ResetChangeTracking();
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000039CC File Offset: 0x00001BCC
		protected override void InternalProcessRecord()
		{
			MultiValuedProperty<string> attachmentNames = this.DataObject.AttachmentNames;
			if (attachmentNames != null)
			{
				foreach (string text in attachmentNames)
				{
					if (text != null && (!text.StartsWith(AttachmentType.ContentType.ToString() + ":", StringComparison.InvariantCulture) || text.Length < AttachmentType.ContentType.ToString().Length + 2) && (!text.StartsWith(AttachmentType.FileName.ToString() + ":", StringComparison.InvariantCulture) || text.Length < AttachmentType.FileName.ToString().Length + 2))
					{
						base.WriteError(new InvalidDataException(DirectoryStrings.AttachmentFilterEntryInvalid.ToString()), ErrorCategory.InvalidData, null);
						return;
					}
				}
			}
			if (base.Fields.IsModified("ExceptionConnectors"))
			{
				this.DataObject.ExceptionConnectors = base.ResolveIdParameterCollection<ReceiveConnectorIdParameter, ReceiveConnector, ADObjectId>(this.ExceptionConnectors, base.DataSession, this.RootId, null, (ExchangeErrorCategory)0, new Func<IIdentityParameter, LocalizedString>(Strings.ErrorReceiveConnectorNotFound), new Func<IIdentityParameter, LocalizedString>(Strings.ErrorReceiveConnectorNotUnique), null, null);
			}
			else if (this.DataObject.IsChanged(AttachmentFilteringConfigSchema.ExceptionConnectors) && this.DataObject.ExceptionConnectors != null && this.DataObject.ExceptionConnectors.Added.Length != 0)
			{
				foreach (ADObjectId adObjectId in this.DataObject.ExceptionConnectors.Added)
				{
					ReceiveConnectorIdParameter receiveConnectorIdParameter = new ReceiveConnectorIdParameter(adObjectId);
					base.GetDataObject<ReceiveConnector>(receiveConnectorIdParameter, base.DataSession, null, new LocalizedString?(Strings.ErrorReceiveConnectorNotFound(receiveConnectorIdParameter)), new LocalizedString?(Strings.ErrorReceiveConnectorNotUnique(receiveConnectorIdParameter)));
				}
			}
			base.InternalProcessRecord();
		}

		// Token: 0x04000026 RID: 38
		private const string ExceptionConnectorsKey = "ExceptionConnectors";
	}
}
