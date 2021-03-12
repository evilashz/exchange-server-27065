using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000309 RID: 777
	public class AddressBookRecipientPicker
	{
		// Token: 0x06001D81 RID: 7553 RVA: 0x000AAD40 File Offset: 0x000A8F40
		private AddressBookRecipientPicker(Strings.IDs title, RecipientWell recipientWell, params Strings.IDs?[] wellLabels)
		{
			if (recipientWell == null)
			{
				throw new ArgumentNullException("recipientWell");
			}
			if (wellLabels.Length > 3 || wellLabels.Length == 0)
			{
				throw new ArgumentException("There must be at least 1 and no more than " + 3 + "recipientwells specified.");
			}
			this.title = title;
			this.recipientWell = recipientWell;
			this.wellLabels = new Strings.IDs?[wellLabels.Length];
			for (int i = 0; i < wellLabels.Length; i++)
			{
				this.wellLabels[i] = wellLabels[i];
			}
		}

		// Token: 0x06001D82 RID: 7554 RVA: 0x000AADD0 File Offset: 0x000A8FD0
		public void Render(UserContext userContext, TextWriter output)
		{
			string value = "&nbsp;<span class=\"abArrw\">-&gt;</span>";
			string attributes = "class=\"abRW fl\" tabindex=-1 ";
			ThemeFileId themeFileId = userContext.IsRtl ? ThemeFileId.PickerArrowRtl : ThemeFileId.PickerArrowLtr;
			StringBuilder stringBuilder = new StringBuilder("addSelectionToWell(\"", 30);
			int length = stringBuilder.Length;
			StringBuilder stringBuilder2 = new StringBuilder(60);
			output.Write("<div id=\"adrPkr\">");
			output.Write(LocalizedStrings.GetHtmlEncoded(this.title));
			output.Write("<div id=\"wls\">");
			for (int i = 0; i < AddressBookRecipientPicker.divIds.Length; i++)
			{
				if (i < this.wellLabels.Length && this.wellLabels[i] != null)
				{
					string value2;
					RecipientWellType type;
					string buttonId;
					if (this == AddressBookRecipientPicker.SendFromRecipients)
					{
						value2 = "divFrom";
						type = RecipientWellType.From;
						buttonId = "btnFrom";
					}
					else
					{
						value2 = AddressBookRecipientPicker.divIds[i];
						type = AddressBookRecipientPicker.types[i];
						buttonId = AddressBookRecipientPicker.buttonIds[i];
					}
					output.Write("<div id=\"wl\" class=\"w100\"><div id=\"arw\" class=\"frt\">");
					userContext.RenderThemeImage(output, themeFileId, string.Empty, new object[]
					{
						"id=\"arwi\" style=\"display:none\""
					});
					userContext.RenderThemeImage(output, ThemeFileId.Clear1x1, string.Empty, new object[]
					{
						"id=\"arwc\""
					});
					output.Write("</div>");
					Strings.IDs localizedID = this.wellLabels[i] ?? -269710455;
					stringBuilder2.Append(LocalizedStrings.GetHtmlEncoded(localizedID));
					stringBuilder2.Append(value);
					stringBuilder.Append(value2);
					stringBuilder.Append("\");");
					RenderingUtilities.RenderButton(output, buttonId, attributes, stringBuilder.ToString(), stringBuilder2.ToString(), true);
					this.recipientWell.Render(output, userContext, type);
					output.Write("</div>");
					stringBuilder.Length = length;
					stringBuilder2.Length = 0;
				}
			}
			output.Write("</div><div id=\"okCn\" class=\"frt\">");
			RenderingUtilities.RenderButton(output, "btnOk", string.Empty, "save();", LocalizedStrings.GetHtmlEncoded(2041362128));
			output.Write("&nbsp;");
			RenderingUtilities.RenderButton(output, "btnCncl", string.Empty, "window.close();", LocalizedStrings.GetHtmlEncoded(-1936577052));
			output.Write("</div></div>");
		}

		// Token: 0x040015B2 RID: 5554
		private const int MaxWells = 3;

		// Token: 0x040015B3 RID: 5555
		private const string FromDivId = "divFrom";

		// Token: 0x040015B4 RID: 5556
		private const string FromButtonId = "btnFrom";

		// Token: 0x040015B5 RID: 5557
		private static readonly string[] buttonIds = new string[]
		{
			"btnTo",
			"btnCc",
			"btnBcc"
		};

		// Token: 0x040015B6 RID: 5558
		private static readonly string[] divIds = new string[]
		{
			"divTo",
			"divCc",
			"divBcc"
		};

		// Token: 0x040015B7 RID: 5559
		private static readonly RecipientWellType[] types = new RecipientWellType[]
		{
			RecipientWellType.To,
			RecipientWellType.Cc,
			RecipientWellType.Bcc
		};

		// Token: 0x040015B8 RID: 5560
		internal static readonly AddressBookRecipientPicker Recipients = new AddressBookRecipientPicker(-2069160934, new MessageRecipientWell(), new Strings.IDs?[]
		{
			new Strings.IDs?(-269710455),
			new Strings.IDs?(2055888382),
			new Strings.IDs?(198978688)
		});

		// Token: 0x040015B9 RID: 5561
		internal static readonly AddressBookRecipientPicker Attendees = new AddressBookRecipientPicker(1925113256, new CalendarItemRecipientWell(), new Strings.IDs?[]
		{
			new Strings.IDs?(635407387),
			new Strings.IDs?(-1832858804),
			new Strings.IDs?(1672068383)
		});

		// Token: 0x040015BA RID: 5562
		internal static readonly AddressBookRecipientPicker DistributionListMember = new AddressBookRecipientPicker(-1893304058, new MessageRecipientWell(), new Strings.IDs?[]
		{
			new Strings.IDs?(-1619718267)
		});

		// Token: 0x040015BB RID: 5563
		internal static readonly AddressBookRecipientPicker UsersAndGroups = new AddressBookRecipientPicker(2104380933, new MessageRecipientWell(), new Strings.IDs?[]
		{
			new Strings.IDs?(1465898911)
		});

		// Token: 0x040015BC RID: 5564
		internal static readonly AddressBookRecipientPicker Rooms = new AddressBookRecipientPicker(-432893051, new CalendarItemRecipientWell(), new Strings.IDs?[]
		{
			null,
			null,
			new Strings.IDs?(1007756464)
		});

		// Token: 0x040015BD RID: 5565
		internal static readonly AddressBookRecipientPicker FromRecipients = new AddressBookRecipientPicker(-2069160934, new MessageRecipientWell(), new Strings.IDs?[]
		{
			new Strings.IDs?(-1327933906)
		});

		// Token: 0x040015BE RID: 5566
		internal static readonly AddressBookRecipientPicker ToRecipients = new AddressBookRecipientPicker(-2069160934, new MessageRecipientWell(), new Strings.IDs?[]
		{
			new Strings.IDs?(-269710455)
		});

		// Token: 0x040015BF RID: 5567
		internal static readonly AddressBookRecipientPicker SendFromRecipients = new AddressBookRecipientPicker(950402488, new MessageRecipientWell(), new Strings.IDs?[]
		{
			new Strings.IDs?(-1327933906)
		});

		// Token: 0x040015C0 RID: 5568
		internal static readonly AddressBookRecipientPicker SelectOtherMailboxRecipient = new AddressBookRecipientPicker(-2058911220, new MessageRecipientWell(), new Strings.IDs?[]
		{
			new Strings.IDs?(1264864827)
		});

		// Token: 0x040015C1 RID: 5569
		internal static readonly AddressBookRecipientPicker PersonalAutoAttendantCallers = new AddressBookRecipientPicker(-239997443, new MessageRecipientWell(), new Strings.IDs?[]
		{
			new Strings.IDs?(-1799156789)
		});

		// Token: 0x040015C2 RID: 5570
		internal static readonly AddressBookRecipientPicker ChatParticipants = new AddressBookRecipientPicker(1768257590, new MessageRecipientWell(), new Strings.IDs?[]
		{
			new Strings.IDs?(729061323)
		});

		// Token: 0x040015C3 RID: 5571
		internal static readonly AddressBookRecipientPicker PersonalAutoAttendantOneCaller = new AddressBookRecipientPicker(220395981, new MessageRecipientWell(), new Strings.IDs?[]
		{
			new Strings.IDs?(137777747)
		});

		// Token: 0x040015C4 RID: 5572
		internal static readonly AddressBookRecipientPicker AddBuddy = new AddressBookRecipientPicker(642911694, new MessageRecipientWell(), new Strings.IDs?[]
		{
			new Strings.IDs?(292745765)
		});

		// Token: 0x040015C5 RID: 5573
		internal static readonly AddressBookRecipientPicker ToMobileNumberOrDL = new AddressBookRecipientPicker(-2069160934, new MessageRecipientWell(), new Strings.IDs?[]
		{
			new Strings.IDs?(-269710455)
		});

		// Token: 0x040015C6 RID: 5574
		internal static readonly AddressBookRecipientPicker ToMobileNumber = new AddressBookRecipientPicker(-2069160934, new MessageRecipientWell(), new Strings.IDs?[]
		{
			new Strings.IDs?(-269710455)
		});

		// Token: 0x040015C7 RID: 5575
		internal static readonly AddressBookRecipientPicker Filter = new AddressBookRecipientPicker(-2069160934, new MessageRecipientWell(), new Strings.IDs?[]
		{
			new Strings.IDs?(1264864827)
		});

		// Token: 0x040015C8 RID: 5576
		private Strings.IDs title;

		// Token: 0x040015C9 RID: 5577
		private RecipientWell recipientWell;

		// Token: 0x040015CA RID: 5578
		private Strings.IDs?[] wellLabels;
	}
}
