using System;
using System.Collections.Generic;
using Microsoft.Exchange.Clients.Common;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x020004A2 RID: 1186
	[OwaEventNamespace("EditPDL")]
	internal sealed class EditDistributionListEventHandler : ItemEventHandler
	{
		// Token: 0x06002DA6 RID: 11686 RVA: 0x0010247D File Offset: 0x0010067D
		public static void Register()
		{
			OwaEventRegistry.RegisterHandler(typeof(EditDistributionListEventHandler));
		}

		// Token: 0x06002DA7 RID: 11687 RVA: 0x00102490 File Offset: 0x00100690
		[OwaEventParameter("Id", typeof(OwaStoreObjectId), false, true)]
		[OwaEvent("Save")]
		[OwaEventParameter("CK", typeof(string), false, true)]
		[OwaEventParameter("fId", typeof(OwaStoreObjectId), false, true)]
		[OwaEventParameter("dn", typeof(string), false)]
		[OwaEventParameter("Itms", typeof(RecipientInfo), true, false)]
		[OwaEventParameter("notes", typeof(string), false, true)]
		[OwaEventVerb(OwaEventVerb.Post)]
		public void Save()
		{
			bool flag = base.IsParameterSet("Id");
			using (DistributionList distributionList = this.GetDistributionList(new PropertyDefinition[0]))
			{
				if (base.IsParameterSet("dn"))
				{
					string text = ((string)base.GetParameter("dn")).Trim();
					if (text.Length > 256)
					{
						text = text.Substring(0, 256);
					}
					if (!string.Equals(distributionList.DisplayName, text))
					{
						distributionList.DisplayName = text;
					}
				}
				if (base.IsParameterSet("notes"))
				{
					string text2 = (string)base.GetParameter("notes");
					if (text2 != null)
					{
						BodyConversionUtilities.SetBody(distributionList, text2, Markup.PlainText, base.UserContext);
					}
				}
				distributionList.Clear();
				RecipientInfo[] array = (RecipientInfo[])base.GetParameter("Itms");
				EditDistributionListEventHandler.AddMembersToDL(distributionList, array, this.CheckDuplicateItems(array));
				Utilities.SaveItem(distributionList, flag);
				distributionList.Load();
				if (!flag)
				{
					this.Writer.Write("<div id=itemId>");
					this.Writer.Write(Utilities.GetIdAsString(distributionList));
					this.Writer.Write("</div>");
				}
				this.Writer.Write("<div id=ck>");
				this.Writer.Write(distributionList.Id.ChangeKeyAsBase64String());
				this.Writer.Write("</div>");
				string text3 = EditDistributionListEventHandler.GetTextPropertyValue(distributionList, ContactBaseSchema.FileAs);
				if (string.IsNullOrEmpty(text3))
				{
					text3 = LocalizedStrings.GetNonEncoded(-1576800949);
				}
				this.Writer.Write("<div id=fa>");
				Utilities.HtmlEncode(text3, this.Writer);
				this.Writer.Write("</div>");
				base.MoveItemToDestinationFolderIfInScratchPad(distributionList);
			}
		}

		// Token: 0x06002DA8 RID: 11688 RVA: 0x00102654 File Offset: 0x00100854
		private static void AddMembersToDL(DistributionList distributionList, RecipientInfo[] recipients, int validCount)
		{
			int num = 0;
			while (num < recipients.Length && num < validCount)
			{
				Participant participant;
				recipients[num].ToParticipant(out participant);
				distributionList.Add(participant);
				num++;
			}
		}

		// Token: 0x06002DA9 RID: 11689 RVA: 0x00102686 File Offset: 0x00100886
		private static string GetTextPropertyValue(DistributionList distributionList, PropertyDefinition property)
		{
			return ItemUtility.GetProperty<string>(distributionList, property, string.Empty).Trim();
		}

		// Token: 0x06002DAA RID: 11690 RVA: 0x0010269C File Offset: 0x0010089C
		private DistributionList GetDistributionList(params PropertyDefinition[] prefetchProperties)
		{
			bool flag = base.IsParameterSet("Id");
			DistributionList result;
			if (flag)
			{
				result = base.GetRequestItem<DistributionList>(prefetchProperties);
			}
			else
			{
				OwaStoreObjectId folderId = base.GetParameter("fId") as OwaStoreObjectId;
				result = Utilities.CreateItem<DistributionList>(folderId);
				if (Globals.ArePerfCountersEnabled)
				{
					OwaSingleCounters.ItemsCreated.Increment();
				}
			}
			return result;
		}

		// Token: 0x06002DAB RID: 11691 RVA: 0x001026F0 File Offset: 0x001008F0
		[OwaEventParameter("Id", typeof(OwaStoreObjectId), false, true)]
		[OwaEventParameter("Itms", typeof(RecipientInfo), true, true)]
		[OwaEventParameter("dupChk", typeof(int), false, true)]
		[OwaEvent("UpdateListView")]
		[OwaEventVerb(OwaEventVerb.Post)]
		[OwaEventParameter("sC", typeof(ColumnId), false, true)]
		[OwaEventParameter("sO", typeof(SortOrder), false, true)]
		public void UpdateListView()
		{
			RecipientInfo[] array = (RecipientInfo[])base.GetParameter("Itms");
			int num = this.CheckDuplicateItems(array);
			if (base.IsParameterSet("dupChk") && array.Length != num)
			{
				int num2 = (int)base.GetParameter("dupChk");
				if (num2 == 1 || num2 == 2)
				{
					this.Writer.Write("<div id=\"dup\"></div>");
					if (num2 == 2)
					{
						return;
					}
				}
			}
			ColumnId sortedColumn = base.IsParameterSet("sC") ? ((ColumnId)base.GetParameter("sC")) : ColumnId.MemberDisplayName;
			SortOrder order = base.IsParameterSet("sO") ? ((SortOrder)base.GetParameter("sO")) : SortOrder.Ascending;
			ListView listView = new DistributionListMemberListView(base.UserContext, array, num, sortedColumn, order);
			this.Writer.Write("<div id=\"data\" sR=\"");
			this.Writer.Write((0 < listView.DataSource.TotalCount) ? (listView.StartRange + 1) : 0);
			this.Writer.Write("\" eR=\"");
			this.Writer.Write((0 < listView.DataSource.TotalCount) ? (listView.EndRange + 1) : 0);
			this.Writer.Write("\" tC=\"");
			this.Writer.Write(listView.DataSource.TotalCount);
			this.Writer.Write("\" sCki=\"");
			this.Writer.Write(listView.Cookie);
			this.Writer.Write("\" iLcid=\"");
			this.Writer.Write(listView.CookieLcid);
			this.Writer.Write("\" sPfdDC=\"");
			this.Writer.Write(Utilities.HtmlEncode(listView.PreferredDC));
			this.Writer.Write("\" uC=\"");
			this.Writer.Write(listView.DataSource.UnreadCount);
			this.Writer.Write("\"></div>");
			listView.Render(this.Writer, ListView.RenderFlags.Contents | ListView.RenderFlags.Headers);
		}

		// Token: 0x06002DAC RID: 11692 RVA: 0x001028F0 File Offset: 0x00100AF0
		private int CheckDuplicateItems(RecipientInfo[] items)
		{
			int result = 0;
			IComparer<RecipientInfo> comparer = new EditDistributionListEventHandler.RecipientInfoComparer();
			for (int i = 0; i < items.Length; i++)
			{
				if (items[i].AddressOrigin != AddressOrigin.Directory && items[i].AddressOrigin != AddressOrigin.Store && string.IsNullOrEmpty(items[i].RoutingType))
				{
					items[i].AddressOrigin = AddressOrigin.OneOff;
					RecipientAddress recipientAddress = AnrManager.ResolveAnrStringToOneOffEmail(items[i].RoutingAddress);
					if (recipientAddress != null)
					{
						items[i].RoutingType = recipientAddress.RoutingType;
						items[i].RoutingAddress = recipientAddress.RoutingAddress;
					}
					else
					{
						items[i].RoutingType = "SMTP";
					}
				}
			}
			Array.Sort<RecipientInfo>(items, comparer);
			for (int j = 0; j < items.Length; j++)
			{
				if ((items[j].AddressOrigin != AddressOrigin.Store || !items[j].StoreObjectId.Equals(base.IsParameterSet("Id") ? ((OwaStoreObjectId)base.GetParameter("Id")).StoreObjectId : null)) && (j <= 0 || comparer.Compare(items[j - 1], items[j]) != 0))
				{
					items[result++] = items[j];
				}
			}
			return result;
		}

		// Token: 0x04001EB0 RID: 7856
		private const int MaxListNameLength = 256;

		// Token: 0x04001EB1 RID: 7857
		public const string EventNamespace = "EditPDL";

		// Token: 0x04001EB2 RID: 7858
		public const string MethodUpdateListView = "UpdateListView";

		// Token: 0x04001EB3 RID: 7859
		public const string Items = "Itms";

		// Token: 0x04001EB4 RID: 7860
		public const string DisplayNameId = "dn";

		// Token: 0x04001EB5 RID: 7861
		public const string Notes = "notes";

		// Token: 0x04001EB6 RID: 7862
		public const string DuplicateCheck = "dupChk";

		// Token: 0x020004A3 RID: 1187
		private class RecipientInfoComparer : IComparer<RecipientInfo>
		{
			// Token: 0x06002DAE RID: 11694 RVA: 0x00102A04 File Offset: 0x00100C04
			int IComparer<RecipientInfo>.Compare(RecipientInfo x, RecipientInfo y)
			{
				if (x.AddressOrigin != y.AddressOrigin)
				{
					return x.AddressOrigin - y.AddressOrigin;
				}
				if (x.AddressOrigin == AddressOrigin.Store)
				{
					int num = string.Compare(x.StoreObjectId.ToBase64String(), y.StoreObjectId.ToBase64String());
					if (num != 0)
					{
						return num;
					}
					return x.EmailAddressIndex - y.EmailAddressIndex;
				}
				else
				{
					if (x.AddressOrigin == AddressOrigin.Directory || x.RoutingType == y.RoutingType)
					{
						return string.Compare(x.RoutingAddress, y.RoutingAddress, StringComparison.InvariantCultureIgnoreCase);
					}
					return string.Compare(x.RoutingType, y.RoutingType, StringComparison.InvariantCultureIgnoreCase);
				}
			}
		}

		// Token: 0x020004A4 RID: 1188
		public enum DuplicateCheckLevel
		{
			// Token: 0x04001EB8 RID: 7864
			RemoveSilently,
			// Token: 0x04001EB9 RID: 7865
			RemoveAndWarn,
			// Token: 0x04001EBA RID: 7866
			StopAndWarn
		}
	}
}
