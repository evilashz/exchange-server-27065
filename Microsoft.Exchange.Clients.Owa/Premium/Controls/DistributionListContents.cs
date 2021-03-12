using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000352 RID: 850
	internal class DistributionListContents : LegacySingleLineItemList
	{
		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x06002018 RID: 8216 RVA: 0x000BA125 File Offset: 0x000B8325
		protected override bool IsRenderColumnShadow
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x06002019 RID: 8217 RVA: 0x000BA128 File Offset: 0x000B8328
		protected override string ListViewStyle
		{
			get
			{
				return "dlIL";
			}
		}

		// Token: 0x0600201A RID: 8218 RVA: 0x000BA12F File Offset: 0x000B832F
		public DistributionListContents(UserContext userContext, ViewDescriptor viewDescriptor) : base(viewDescriptor, ColumnId.MemberDisplayName, SortOrder.Ascending, userContext)
		{
			base.AddProperty(ItemSchema.Id);
			base.AddProperty(RecipientSchema.EmailAddrType);
			base.AddProperty(ParticipantSchema.OriginItemId);
		}

		// Token: 0x0600201B RID: 8219 RVA: 0x000BA15D File Offset: 0x000B835D
		private static void RenderAttributes(TextWriter writer, string name, string value)
		{
			writer.Write(" ");
			writer.Write(name);
			writer.Write("=\"");
			Utilities.HtmlEncode(value, writer);
			writer.Write("\"");
		}

		// Token: 0x0600201C RID: 8220 RVA: 0x000BA190 File Offset: 0x000B8390
		protected override void RenderItemMetaDataExpandos(TextWriter writer)
		{
			AddressOrigin itemProperty = this.DataSource.GetItemProperty<AddressOrigin>(ItemSchema.RecipientType, AddressOrigin.Unknown);
			int itemProperty2 = this.DataSource.GetItemProperty<int>(RecipientSchema.EmailAddrType, 0);
			DistributionListContents.RenderAttributes(writer, "_dn", this.DataSource.GetItemProperty<string>(StoreObjectSchema.DisplayName, string.Empty));
			DistributionListContents.RenderAttributes(writer, "_em", this.DataSource.GetItemProperty<string>(ParticipantSchema.EmailAddress, string.Empty));
			DistributionListContents.RenderAttributes(writer, "_rt", this.DataSource.GetItemProperty<string>(ParticipantSchema.RoutingType, string.Empty));
			string name = "_ao";
			int num = (int)itemProperty;
			DistributionListContents.RenderAttributes(writer, name, num.ToString());
			if (itemProperty == AddressOrigin.Store)
			{
				DistributionListContents.RenderAttributes(writer, "_id", this.DataSource.GetItemProperty<string>(ParticipantSchema.OriginItemId, string.Empty));
				if (itemProperty2 != 0)
				{
					DistributionListContents.RenderAttributes(writer, "_ei", itemProperty2.ToString());
				}
			}
			base.RenderItemMetaDataExpandos(writer);
		}

		// Token: 0x0600201D RID: 8221 RVA: 0x000BA278 File Offset: 0x000B8478
		protected override void RenderTableCellAttributes(TextWriter writer, ColumnId columnId)
		{
			string text = null;
			if (columnId == ColumnId.MemberDisplayName)
			{
				text = this.DataSource.GetItemProperty<string>(StoreObjectSchema.DisplayName, null);
			}
			else if (columnId == ColumnId.MemberEmail)
			{
				Participant itemProperty = this.DataSource.GetItemProperty<Participant>(ContactSchema.Email1, null);
				if (itemProperty != null)
				{
					string text2;
					ContactUtilities.GetParticipantEmailAddress(itemProperty, out text, out text2);
				}
			}
			if (text != null)
			{
				DistributionListContents.RenderAttributes(writer, "title", text);
			}
		}

		// Token: 0x0600201E RID: 8222 RVA: 0x000BA2DC File Offset: 0x000B84DC
		protected override bool RenderIcon(TextWriter writer)
		{
			bool result = true;
			string itemClass;
			if ((itemClass = this.DataSource.GetItemClass()) != null)
			{
				if (itemClass == "IPM.DistList" || itemClass == "AD.RecipientType.Group")
				{
					base.UserContext.RenderThemeImage(writer, ThemeFileId.DistributionListOther);
					return result;
				}
				if (itemClass == "IPM.Contact")
				{
					base.UserContext.RenderThemeImage(writer, ThemeFileId.Contact);
					return result;
				}
				if (itemClass == "AD.RecipientType.User" || itemClass == "OneOff")
				{
					base.UserContext.RenderThemeImage(writer, ThemeFileId.DistributionListUser);
					return result;
				}
			}
			writer.Write("<img src=\"\">");
			return result;
		}
	}
}
