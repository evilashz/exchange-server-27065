using System;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.AirSync;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Security.RightsManagement;

namespace Microsoft.Exchange.AirSync.SchemaConverter.XSO
{
	// Token: 0x02000233 RID: 563
	internal class XsoRightsManagementLicenseProperty : XsoNestedProperty
	{
		// Token: 0x060014FC RID: 5372 RVA: 0x0007B0DF File Offset: 0x000792DF
		public XsoRightsManagementLicenseProperty() : base(new RightsManagementLicenseData())
		{
			base.State = PropertyState.SetToDefault;
		}

		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x060014FD RID: 5373 RVA: 0x0007B0F4 File Offset: 0x000792F4
		public override INestedData NestedData
		{
			get
			{
				RightsManagementLicenseData rightsManagementLicenseData = (RightsManagementLicenseData)base.NestedData;
				if (rightsManagementLicenseData == null)
				{
					AirSyncDiagnostics.TraceError<INestedData>(ExTraceGlobals.RequestsTracer, null, "Invalid NestedData in RightsManagementLicenseData", base.NestedData);
					return null;
				}
				if (rightsManagementLicenseData.ContainsValidData())
				{
					return rightsManagementLicenseData;
				}
				Item mailboxItem = (Item)base.XsoItem;
				if (!this.LoadRightsManagementLicenseData(mailboxItem, rightsManagementLicenseData))
				{
					base.State = PropertyState.SetToDefault;
				}
				return rightsManagementLicenseData;
			}
		}

		// Token: 0x060014FE RID: 5374 RVA: 0x0007B150 File Offset: 0x00079350
		private bool LoadRightsManagementLicenseData(Item mailboxItem, RightsManagementLicenseData rightsManagementLicenseData)
		{
			if (!BodyConversionUtilities.IsMessageRestrictedAndDecoded(mailboxItem) && !BodyConversionUtilities.IsIRMFailedToDecode(mailboxItem))
			{
				return false;
			}
			if (BodyConversionUtilities.IsIRMFailedToDecode(mailboxItem))
			{
				rightsManagementLicenseData.InitNoRightsTemplate();
				return true;
			}
			RightsManagedMessageItem rightsManagedMessageItem = mailboxItem as RightsManagedMessageItem;
			rightsManagementLicenseData.EditAllowed = new bool?(rightsManagedMessageItem.UsageRights.IsUsageRightGranted(ContentRight.Edit));
			rightsManagementLicenseData.ReplyAllowed = new bool?(rightsManagedMessageItem.UsageRights.IsUsageRightGranted(ContentRight.Reply));
			rightsManagementLicenseData.ReplyAllAllowed = new bool?(rightsManagedMessageItem.UsageRights.IsUsageRightGranted(ContentRight.ReplyAll));
			rightsManagementLicenseData.ForwardAllowed = new bool?(rightsManagedMessageItem.UsageRights.IsUsageRightGranted(ContentRight.Forward));
			rightsManagementLicenseData.PrintAllowed = new bool?(rightsManagedMessageItem.UsageRights.IsUsageRightGranted(ContentRight.Print));
			rightsManagementLicenseData.ExtractAllowed = new bool?(rightsManagedMessageItem.UsageRights.IsUsageRightGranted(ContentRight.Extract));
			rightsManagementLicenseData.ProgrammaticAccessAllowed = new bool?(rightsManagedMessageItem.UsageRights.IsUsageRightGranted(ContentRight.ObjectModel));
			rightsManagementLicenseData.Owner = new bool?(rightsManagedMessageItem.UsageRights.IsUsageRightGranted(ContentRight.Owner));
			if (!AirSyncUtility.IsProtectedVoicemailItem(mailboxItem))
			{
				rightsManagementLicenseData.ExportAllowed = new bool?(rightsManagedMessageItem.UsageRights.IsUsageRightGranted(ContentRight.Export));
			}
			else
			{
				rightsManagementLicenseData.ExportAllowed = new bool?(false);
			}
			if (rightsManagedMessageItem.UsageRights.IsUsageRightGranted(ContentRight.Forward) && (!rightsManagedMessageItem.Restriction.RequiresRepublishingWhenRecipientsChange || rightsManagedMessageItem.CanRepublish))
			{
				rightsManagementLicenseData.ModifyRecipientsAllowed = new bool?(true);
			}
			else
			{
				rightsManagementLicenseData.ModifyRecipientsAllowed = new bool?(false);
			}
			rightsManagementLicenseData.ContentExpiryDate = new ExDateTime?(rightsManagedMessageItem.UserLicenseExpiryTime);
			rightsManagementLicenseData.ContentOwner = rightsManagedMessageItem.ConversationOwner.EmailAddress;
			RmsTemplate restriction = rightsManagedMessageItem.Restriction;
			if (restriction != null)
			{
				Guid id = restriction.Id;
				rightsManagementLicenseData.TemplateID = restriction.Id.ToString();
				rightsManagementLicenseData.TemplateName = restriction.GetName(Command.CurrentCommand.MailboxSession.PreferedCulture);
				rightsManagementLicenseData.TemplateDescription = restriction.GetDescription(Command.CurrentCommand.MailboxSession.PreferedCulture);
			}
			return true;
		}
	}
}
