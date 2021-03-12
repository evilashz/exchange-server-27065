using System;
using System.Globalization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Conversations;
using Microsoft.Exchange.Security.RightsManagement;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x0200016F RID: 367
	internal sealed class RightsManagementLicenseDataProperty : ComplexPropertyBase, IToServiceObjectCommand, IPropertyCommand
	{
		// Token: 0x06000A7F RID: 2687 RVA: 0x00032F29 File Offset: 0x00031129
		public RightsManagementLicenseDataProperty(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x00032F32 File Offset: 0x00031132
		public static RightsManagementLicenseDataProperty CreateCommand(CommandContext commandContext)
		{
			return new RightsManagementLicenseDataProperty(commandContext);
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x00032F3A File Offset: 0x0003113A
		public void ToXml()
		{
			throw new InvalidOperationException("RightsManagedLicenseProperty.ToXml should not be called.");
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000A82 RID: 2690 RVA: 0x00032F46 File Offset: 0x00031146
		public override bool ToServiceObjectRequiresMailboxAccess
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x00032F4C File Offset: 0x0003114C
		public void ToServiceObject()
		{
			ToServiceObjectCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectCommandSettings>();
			PropertyInformation propertyInformation = this.commandContext.PropertyInformation;
			ItemResponseShape itemResponseShape = commandSettings.ResponseShape as ItemResponseShape;
			if (!itemResponseShape.ClientSupportsIrm)
			{
				return;
			}
			RightsManagementLicenseDataType value;
			if (this.LoadRightsManagementLicenseData(commandSettings.StoreObject as Item, commandSettings.IdAndSession.Session, out value))
			{
				ServiceObject serviceObject = commandSettings.ServiceObject;
				serviceObject.PropertyBag[propertyInformation] = value;
			}
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x00032FB8 File Offset: 0x000311B8
		private static BodyType ConvertBodyFormatToBodyType(Microsoft.Exchange.Data.Storage.BodyFormat format)
		{
			switch (format)
			{
			case Microsoft.Exchange.Data.Storage.BodyFormat.TextHtml:
			case Microsoft.Exchange.Data.Storage.BodyFormat.ApplicationRtf:
				return BodyType.HTML;
			}
			return BodyType.Text;
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x00032FE0 File Offset: 0x000311E0
		private bool LoadRightsManagementLicenseData(Item item, StoreSession session, out RightsManagementLicenseDataType rightsManagementLicenseData)
		{
			RightsManagedMessageItem rightsManagedMessageItem = item as RightsManagedMessageItem;
			if (rightsManagedMessageItem == null)
			{
				rightsManagementLicenseData = null;
				return false;
			}
			if (!IrmUtils.DoesSessionSupportIrm(session))
			{
				rightsManagementLicenseData = RightsManagementLicenseDataType.CreateNoRightsTemplate();
				rightsManagementLicenseData.RightsManagedMessageDecryptionStatus = (int)RightsManagedMessageDecryptionStatus.FeatureDisabled.FailureCode;
				return true;
			}
			if (rightsManagedMessageItem.DecryptionStatus.Failed)
			{
				rightsManagementLicenseData = RightsManagementLicenseDataType.CreateNoRightsTemplate();
				rightsManagementLicenseData.RightsManagedMessageDecryptionStatus = (int)rightsManagedMessageItem.DecryptionStatus.FailureCode;
				return true;
			}
			if (!rightsManagedMessageItem.IsDecoded)
			{
				rightsManagementLicenseData = null;
				return false;
			}
			rightsManagementLicenseData = new RightsManagementLicenseDataType(rightsManagedMessageItem.UsageRights);
			if (IrmUtils.IsProtectedVoicemailItem(rightsManagedMessageItem))
			{
				rightsManagementLicenseData.ExportAllowed = false;
			}
			else
			{
				rightsManagementLicenseData.ExportAllowed = rightsManagedMessageItem.UsageRights.IsUsageRightGranted(ContentRight.Export);
			}
			if (rightsManagedMessageItem.UsageRights.IsUsageRightGranted(ContentRight.Forward) && (!rightsManagedMessageItem.Restriction.RequiresRepublishingWhenRecipientsChange || rightsManagedMessageItem.CanRepublish))
			{
				rightsManagementLicenseData.ModifyRecipientsAllowed = true;
			}
			else
			{
				rightsManagementLicenseData.ModifyRecipientsAllowed = false;
			}
			rightsManagementLicenseData.ContentOwner = rightsManagedMessageItem.ConversationOwner.EmailAddress;
			rightsManagementLicenseData.ContentExpiryDate = ExDateTimeConverter.ToUtcXsdDateTime(rightsManagedMessageItem.UserLicenseExpiryTime);
			rightsManagementLicenseData.BodyType = RightsManagementLicenseDataProperty.ConvertBodyFormatToBodyType(rightsManagedMessageItem.ProtectedBody.Format);
			RmsTemplate restriction = rightsManagedMessageItem.Restriction;
			if (restriction != null)
			{
				RightsManagementLicenseDataType rightsManagementLicenseDataType = rightsManagementLicenseData;
				Guid id = restriction.Id;
				rightsManagementLicenseDataType.RmsTemplateId = restriction.Id.ToString();
				CultureInfo locale = CultureInfo.InvariantCulture;
				MailboxSession mailboxSession = session as MailboxSession;
				if (mailboxSession != null && mailboxSession.Capabilities.CanHaveCulture)
				{
					locale = mailboxSession.PreferedCulture;
				}
				rightsManagementLicenseData.TemplateName = restriction.GetName(locale);
				rightsManagementLicenseData.TemplateDescription = restriction.GetDescription(locale);
			}
			return true;
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x00033178 File Offset: 0x00031378
		internal static bool LoadRightsManagementLicenseData(ItemPart itemPart, out RightsManagementLicenseDataType rightsManagementLicenseData)
		{
			ItemPartIrmInfo irmInfo = itemPart.IrmInfo;
			if (irmInfo == null || !irmInfo.IsRestricted)
			{
				rightsManagementLicenseData = null;
				return false;
			}
			if (irmInfo.DecryptionStatus.Failed)
			{
				rightsManagementLicenseData = RightsManagementLicenseDataType.CreateNoRightsTemplate();
				rightsManagementLicenseData.RightsManagedMessageDecryptionStatus = (int)irmInfo.DecryptionStatus.FailureCode;
				return true;
			}
			rightsManagementLicenseData = new RightsManagementLicenseDataType(irmInfo.UsageRights);
			if (IrmUtils.IsProtectedVoicemailItem(itemPart))
			{
				rightsManagementLicenseData.ExportAllowed = false;
			}
			else
			{
				rightsManagementLicenseData.ExportAllowed = irmInfo.UsageRights.IsUsageRightGranted(ContentRight.Export);
			}
			if (irmInfo.UsageRights.IsUsageRightGranted(ContentRight.Forward) && (!irmInfo.RequiresRepublishingWhenRecipientsChange || irmInfo.CanRepublish))
			{
				rightsManagementLicenseData.ModifyRecipientsAllowed = true;
			}
			else
			{
				rightsManagementLicenseData.ModifyRecipientsAllowed = false;
			}
			rightsManagementLicenseData.ContentOwner = itemPart.IrmInfo.ConversationOwner;
			rightsManagementLicenseData.ContentExpiryDate = ExDateTimeConverter.ToUtcXsdDateTime(irmInfo.UserLicenseExpiryTime);
			rightsManagementLicenseData.TemplateDescription = irmInfo.TemplateDescription;
			rightsManagementLicenseData.TemplateName = irmInfo.TemplateName;
			rightsManagementLicenseData.BodyType = RightsManagementLicenseDataProperty.ConvertBodyFormatToBodyType(irmInfo.BodyFormat);
			return true;
		}
	}
}
