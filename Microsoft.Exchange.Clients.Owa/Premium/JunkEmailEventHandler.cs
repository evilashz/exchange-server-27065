using System;
using System.Web;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x020004BD RID: 1213
	[OwaEventSegmentation(Feature.JunkEMail)]
	[OwaEventNamespace("JunkEmail")]
	internal sealed class JunkEmailEventHandler : OwaEventHandlerBase
	{
		// Token: 0x06002E4E RID: 11854 RVA: 0x00108AE0 File Offset: 0x00106CE0
		public static void Register()
		{
			OwaEventRegistry.RegisterHandler(typeof(JunkEmailEventHandler));
		}

		// Token: 0x06002E4F RID: 11855 RVA: 0x00108AF4 File Offset: 0x00106CF4
		[OwaEvent("Add")]
		[OwaEventParameter("fFrmOpt", typeof(bool), false, false)]
		[OwaEventParameter("iLT", typeof(int), false, false)]
		[OwaEventParameter("sNE", typeof(string), false, false)]
		public void Add()
		{
			string email = HttpUtility.HtmlDecode((string)base.GetParameter("sNE"));
			JunkEmailListType junkEmailListType = (JunkEmailListType)base.GetParameter("iLT");
			bool isFromOptions = (bool)base.GetParameter("fFrmOpt");
			string empty = string.Empty;
			bool flag = false;
			if (!JunkEmailUtilities.Add(email, junkEmailListType, base.UserContext, isFromOptions, out empty))
			{
				flag = true;
			}
			if (flag)
			{
				this.Writer.Write("<div id=fErr>1</div>");
			}
			this.Writer.Write("<div id=sMsg>{0}</div>", Utilities.HtmlEncode(empty));
		}

		// Token: 0x06002E50 RID: 11856 RVA: 0x00108B84 File Offset: 0x00106D84
		[OwaEventParameter("sId", typeof(string), false, false)]
		[OwaEventParameter("fDmn", typeof(bool), false, false)]
		[OwaEvent("AddSender")]
		[OwaEventParameter("iLT", typeof(int), false, false)]
		public void AddSender()
		{
			JunkEmailListType junkEmailListType = (JunkEmailListType)base.GetParameter("iLT");
			string text = (string)base.GetParameter("sId");
			bool flag = (bool)base.GetParameter("fDmn");
			if (OwaStoreObjectId.CreateFromString(text).IsConversationId)
			{
				return;
			}
			string s = string.Empty;
			bool flag2 = false;
			string senderSmtpAddress = Utilities.GetSenderSmtpAddress(text, base.UserContext);
			if (string.IsNullOrEmpty(senderSmtpAddress))
			{
				flag2 = true;
				s = LocalizedStrings.GetNonEncoded(-562376136);
			}
			else
			{
				int num = senderSmtpAddress.IndexOf('@');
				if (num < 0)
				{
					flag2 = true;
					s = LocalizedStrings.GetNonEncoded(-562376136);
				}
				else
				{
					int length = senderSmtpAddress.Length;
					if (!JunkEmailUtilities.Add(flag ? senderSmtpAddress.Substring(num, length - num) : senderSmtpAddress, junkEmailListType, base.UserContext, false, out s))
					{
						flag2 = true;
					}
				}
			}
			if (flag2)
			{
				this.Writer.Write("<div id=fErr>1</div>");
			}
			this.Writer.Write("<div id=sMsg>{0}</div>", Utilities.HtmlEncode(s));
		}

		// Token: 0x06002E51 RID: 11857 RVA: 0x00108C80 File Offset: 0x00106E80
		[OwaEventParameter("sNE", typeof(string), false, false)]
		[OwaEventParameter("iLT", typeof(int), false, false)]
		[OwaEvent("Edit")]
		[OwaEventParameter("sOE", typeof(string), false, false)]
		public void Edit()
		{
			string oldEmail = HttpUtility.HtmlDecode((string)base.GetParameter("sOE"));
			string newEmail = HttpUtility.HtmlDecode((string)base.GetParameter("sNE"));
			JunkEmailListType junkEmailListType = (JunkEmailListType)base.GetParameter("iLT");
			string empty = string.Empty;
			bool flag = false;
			if (!JunkEmailUtilities.Edit(oldEmail, newEmail, junkEmailListType, base.UserContext, true, out empty))
			{
				flag = true;
			}
			if (flag)
			{
				this.Writer.Write("<div id=fErr>1</div>");
			}
			this.Writer.Write("<div id=sMsg>{0}</div>", Utilities.HtmlEncode(empty));
		}

		// Token: 0x06002E52 RID: 11858 RVA: 0x00108D14 File Offset: 0x00106F14
		[OwaEvent("Remove")]
		[OwaEventParameter("iLT", typeof(int), false, false)]
		[OwaEventParameter("sRE", typeof(string), false, false)]
		public void Remove()
		{
			string[] array = ((string)base.GetParameter("sRE")).Split(new char[]
			{
				'|'
			});
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = HttpUtility.HtmlDecode(array[i]);
			}
			JunkEmailListType junkEmailListType = (JunkEmailListType)base.GetParameter("iLT");
			JunkEmailUtilities.Remove(array, junkEmailListType, base.UserContext);
		}

		// Token: 0x06002E53 RID: 11859 RVA: 0x00108D7C File Offset: 0x00106F7C
		[OwaEventParameter("fEnbl", typeof(bool), false, false)]
		[OwaEvent("SaveOptions")]
		[OwaEventParameter("fTrstCnt", typeof(bool), false, false)]
		[OwaEventParameter("fSfOnly", typeof(bool), false, false)]
		public void SaveOptions()
		{
			bool isEnabled = (bool)base.GetParameter("fEnbl");
			bool isContactsTrusted = (bool)base.GetParameter("fTrstCnt");
			bool safeListsOnly = (bool)base.GetParameter("fSfOnly");
			JunkEmailUtilities.SaveOptions(isEnabled, isContactsTrusted, safeListsOnly, base.UserContext);
		}

		// Token: 0x04001FE8 RID: 8168
		public const string EventNamespace = "JunkEmail";

		// Token: 0x04001FE9 RID: 8169
		public const string MethodAdd = "Add";

		// Token: 0x04001FEA RID: 8170
		public const string MethodAddSender = "AddSender";

		// Token: 0x04001FEB RID: 8171
		public const string MethodEdit = "Edit";

		// Token: 0x04001FEC RID: 8172
		public const string MethodRemove = "Remove";

		// Token: 0x04001FED RID: 8173
		public const string MethodSaveOptions = "SaveOptions";

		// Token: 0x04001FEE RID: 8174
		public const string ListType = "iLT";

		// Token: 0x04001FEF RID: 8175
		public const string OldEntry = "sOE";

		// Token: 0x04001FF0 RID: 8176
		public const string NewEntry = "sNE";

		// Token: 0x04001FF1 RID: 8177
		public const string RemoveEntry = "sRE";

		// Token: 0x04001FF2 RID: 8178
		public const string IsFromOptions = "fFrmOpt";

		// Token: 0x04001FF3 RID: 8179
		public const string ItemId = "sId";

		// Token: 0x04001FF4 RID: 8180
		public const string IsAddDomain = "fDmn";

		// Token: 0x04001FF5 RID: 8181
		public const string IsEnabled = "fEnbl";

		// Token: 0x04001FF6 RID: 8182
		public const string IsContactsTrusted = "fTrstCnt";

		// Token: 0x04001FF7 RID: 8183
		public const string SafeListsOnly = "fSfOnly";

		// Token: 0x04001FF8 RID: 8184
		private const string DivError = "<div id=fErr>1</div>";

		// Token: 0x04001FF9 RID: 8185
		private const string DivMessage = "<div id=sMsg>{0}</div>";
	}
}
