using System;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x02000485 RID: 1157
	[OwaEventNamespace("Compliance")]
	internal sealed class ComplianceEventHandler : OwaEventHandlerBase
	{
		// Token: 0x06002C92 RID: 11410 RVA: 0x000F9EF8 File Offset: 0x000F80F8
		public static void Register()
		{
			OwaEventRegistry.RegisterHandler(typeof(ComplianceEventHandler));
		}

		// Token: 0x06002C93 RID: 11411 RVA: 0x000F9F0C File Offset: 0x000F810C
		[OwaEvent("gtCMIB")]
		[OwaEventVerb(OwaEventVerb.Get)]
		[OwaEventParameter("cguid", typeof(string))]
		public void GetComplianceInfobar()
		{
			string g = (string)base.GetParameter("cguid");
			Guid empty = Guid.Empty;
			if (GuidHelper.TryParseGuid(g, out empty))
			{
				InfobarMessage compliance = InfobarMessageBuilder.GetCompliance(base.UserContext, empty);
				if (compliance != null)
				{
					Infobar.RenderMessage(this.Writer, compliance, base.UserContext);
				}
			}
		}

		// Token: 0x06002C94 RID: 11412 RVA: 0x000F9F5C File Offset: 0x000F815C
		[OwaEventVerb(OwaEventVerb.Get)]
		[OwaEventParameter("id", typeof(StoreObjectId), false, true)]
		[OwaEvent("gtCM")]
		public void GetComplianceContextMenu()
		{
			StoreObjectId storeObjectId = base.GetParameter("id") as StoreObjectId;
			string id = "0";
			if (storeObjectId != null)
			{
				using (Item item = Utilities.GetItem<Item>(base.UserContext, storeObjectId, ComplianceEventHandler.prefetchProperties))
				{
					if (Utilities.IrmDecryptIfRestricted(item, base.UserContext, true))
					{
						RightsManagedMessageItem rightsManagedMessageItem = (RightsManagedMessageItem)item;
						id = "1";
						if (rightsManagedMessageItem.Restriction != null && base.UserContext.ComplianceReader.RmsTemplateReader.LookupRmsTemplate(rightsManagedMessageItem.Restriction.Id) != null)
						{
							id = rightsManagedMessageItem.Restriction.Id.ToString();
						}
					}
					else
					{
						bool property = ItemUtility.GetProperty<bool>(item, ItemSchema.IsClassified, false);
						if (property)
						{
							string property2 = ItemUtility.GetProperty<string>(item, ItemSchema.ClassificationGuid, string.Empty);
							id = "1";
							Guid empty = Guid.Empty;
							if (GuidHelper.TryParseGuid(property2, out empty) && base.UserContext.ComplianceReader.MessageClassificationReader.LookupMessageClassification(empty, base.UserContext.UserCulture) != null)
							{
								id = empty.ToString();
							}
						}
					}
				}
			}
			ComplianceContextMenu complianceContextMenu = new ComplianceContextMenu(base.UserContext, id);
			complianceContextMenu.Render(this.Writer);
		}

		// Token: 0x04001D84 RID: 7556
		public const string EventNamespace = "Compliance";

		// Token: 0x04001D85 RID: 7557
		public const string Id = "id";

		// Token: 0x04001D86 RID: 7558
		public const string GuidArrayMap = "cguid";

		// Token: 0x04001D87 RID: 7559
		public const string MethodGetComplianceMenu = "gtCM";

		// Token: 0x04001D88 RID: 7560
		public const string MethodGetComplianceInfobar = "gtCMIB";

		// Token: 0x04001D89 RID: 7561
		private const string UnknownComplianceId = "1";

		// Token: 0x04001D8A RID: 7562
		private static readonly StorePropertyDefinition[] prefetchProperties = new StorePropertyDefinition[]
		{
			ItemSchema.IsClassified,
			ItemSchema.Classification,
			ItemSchema.ClassificationDescription,
			ItemSchema.ClassificationGuid
		};
	}
}
