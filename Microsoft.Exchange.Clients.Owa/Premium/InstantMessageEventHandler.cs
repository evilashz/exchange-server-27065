using System;
using System.Globalization;
using System.Linq;
using Microsoft.Exchange.Clients.Common;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x020004BB RID: 1211
	[OwaEventNamespace("IM")]
	[OwaEventSegmentation(Feature.InstantMessage)]
	internal sealed class InstantMessageEventHandler : OwaEventHandlerBase
	{
		// Token: 0x06002E24 RID: 11812 RVA: 0x00107616 File Offset: 0x00105816
		public static void Register()
		{
			OwaEventRegistry.RegisterHandler(typeof(InstantMessageEventHandler));
		}

		// Token: 0x06002E25 RID: 11813 RVA: 0x00107628 File Offset: 0x00105828
		[OwaEventParameter("cMsg", typeof(string))]
		[OwaEventParameter("sUris", typeof(string), true, false)]
		[OwaEventParameter("iType", typeof(int), false, true)]
		[OwaEventParameter("lDn", typeof(string), false, true)]
		[OwaEvent("SndNwChtMsg")]
		[OwaEventParameter("frmt", typeof(string))]
		public void SendNewChatMessage()
		{
			ExTraceGlobals.OehCallTracer.TraceDebug((long)this.GetHashCode(), "InstantMessageEventHandler.SendNewChatMessage");
			this.ThowIfInvalidProvider("SendNewChatMessage");
			string[] array = (string[])base.GetParameter("sUris");
			string text = (string)base.GetParameter("lDn");
			string text2 = (array != null && array.Length > 0) ? array[0] : string.Empty;
			string text3 = null;
			bool flag = false;
			if (text != null)
			{
				text3 = this.GetSipUriFromLegacyDn(text, text2);
				this.ThrowIfSipInvalid(text3, false);
				if (text3 != text2)
				{
					array = new string[]
					{
						text3
					};
					flag = true;
				}
			}
			int num;
			if (array.Length < 1)
			{
				ExTraceGlobals.InstantMessagingTracer.TraceDebug((long)this.GetHashCode(), "InstantMessageEventHandler.SendNewChatMessage. Recipients are empty.");
				num = -1;
			}
			else
			{
				InstantMessageProvider.ProviderMessage message = default(InstantMessageProvider.ProviderMessage);
				message.Body = (string)base.GetParameter("cMsg");
				message.Format = (string)base.GetParameter("frmt");
				message.Recipients = array;
				int[] addressTypes = new int[1];
				message.AddressTypes = addressTypes;
				if (base.IsParameterSet("iType"))
				{
					message.AddressTypes[0] = (int)base.GetParameter("iType");
				}
				num = base.UserContext.InstantMessageManager.Provider.SendNewChatMessage(message);
			}
			this.Writer.WriteLine("{");
			if (flag)
			{
				this.Writer.Write("_sip : '");
				this.Writer.Write(text3);
				this.Writer.WriteLine("',");
			}
			this.Writer.Write("_cid : '");
			this.Writer.Write(num.ToString(CultureInfo.InvariantCulture));
			this.Writer.WriteLine("'");
			this.Writer.Write("}");
			if (Globals.ArePerfCountersEnabled)
			{
				OwaSingleCounters.IMTotalInstantMessagesSent.Increment();
			}
		}

		// Token: 0x06002E26 RID: 11814 RVA: 0x0010780C File Offset: 0x00105A0C
		[OwaEventParameter("cId", typeof(string))]
		[OwaEventParameter("sUris", typeof(string), true, false)]
		[OwaEventParameter("frmt", typeof(string))]
		[OwaEventParameter("cMsg", typeof(string))]
		[OwaEvent("SndChtMsg")]
		public void SendChatMessage()
		{
			ExTraceGlobals.OehCallTracer.TraceDebug((long)this.GetHashCode(), "InstantMessageEventHandler.SendChatMessage");
			this.ThowIfInvalidProvider("SendChatMessage");
			string text = (string)base.GetParameter("cId");
			int chatSessionId;
			if (!int.TryParse(text, out chatSessionId))
			{
				throw new OwaInvalidRequestException("The chat ID format is not valid:" + text);
			}
			InstantMessageProvider.ProviderMessage message = default(InstantMessageProvider.ProviderMessage);
			message.Body = (string)base.GetParameter("cMsg");
			message.Format = (string)base.GetParameter("frmt");
			message.ChatSessionId = chatSessionId;
			message.Recipients = (string[])base.GetParameter("sUris");
			chatSessionId = base.UserContext.InstantMessageManager.Provider.SendMessage(message);
			this.Writer.WriteLine("{");
			this.Writer.Write("_cid : '");
			this.Writer.Write(chatSessionId.ToString(CultureInfo.InvariantCulture));
			this.Writer.WriteLine("'");
			this.Writer.Write("}");
			if (Globals.ArePerfCountersEnabled)
			{
				OwaSingleCounters.IMTotalInstantMessagesSent.Increment();
			}
		}

		// Token: 0x06002E27 RID: 11815 RVA: 0x0010793C File Offset: 0x00105B3C
		[OwaEventParameter("sbsUris", typeof(string))]
		[OwaEvent("SubChng")]
		[OwaEventParameter("unsbsUris", typeof(string))]
		public void PresenceSubscriptionChange()
		{
			ExTraceGlobals.OehCallTracer.TraceDebug((long)this.GetHashCode(), "InstantMessageEventHandler.PresenceSubscriptionChange");
			this.ThowIfInvalidProvider("PresenceSubscriptionChange");
			string text = (string)base.GetParameter("sbsUris");
			if (!string.IsNullOrEmpty(text))
			{
				base.UserContext.InstantMessageManager.Provider.AddSubscription(text.Split(new char[]
				{
					','
				}));
			}
			string text2 = (string)base.GetParameter("unsbsUris");
			if (!string.IsNullOrEmpty(text2))
			{
				string[] array = text2.Split(new char[]
				{
					','
				});
				foreach (string text3 in array)
				{
					try
					{
						base.UserContext.InstantMessageManager.Provider.RemoveSubscription(text3);
					}
					catch (ArgumentException ex)
					{
						ExTraceGlobals.OehCallTracer.TraceError<string, string>((long)this.GetHashCode(), "InstantMessageEventHandler.PresenceSubscriptionChange. Unsubscribing SIP Uri: {0}. Exception Message: {1}", (text3 == null) ? string.Empty : text3, (ex.Message == null) ? string.Empty : ex.Message);
					}
				}
			}
		}

		// Token: 0x06002E28 RID: 11816 RVA: 0x00107A74 File Offset: 0x00105C74
		[OwaEventParameter("qryUris", typeof(string))]
		[OwaEvent("QryPrsnc")]
		public void QueryPresence()
		{
			ExTraceGlobals.OehCallTracer.TraceDebug((long)this.GetHashCode(), "InstantMessageEventHandler.QueryPresence");
			this.ThowIfInvalidProvider("QueryPresence");
			string text = (string)base.GetParameter("qryUris");
			if (!string.IsNullOrEmpty(text))
			{
				string[] array = (from s in text.Split(new char[]
				{
					','
				})
				where s.Trim().Length > 0
				select s).ToArray<string>();
				if (array.Length != 0)
				{
					base.UserContext.InstantMessageManager.Provider.QueryPresence(array);
				}
			}
			if (Globals.ArePerfCountersEnabled)
			{
				OwaSingleCounters.IMTotalPresenceQueries.Increment();
			}
		}

		// Token: 0x06002E29 RID: 11817 RVA: 0x00107B24 File Offset: 0x00105D24
		[OwaEventParameter("dName", typeof(string), false, true)]
		[OwaEventParameter("bdyAddr", typeof(string))]
		[OwaEventParameter("msg", typeof(string), false, true)]
		[OwaEventParameter("lDn", typeof(string), false, true)]
		[OwaEventParameter("grpId", typeof(string), false, true)]
		[OwaEventParameter("grpNme", typeof(string), false, true)]
		[OwaEvent("AddBdy")]
		public void AddBuddy()
		{
			ExTraceGlobals.OehCallTracer.TraceDebug((long)this.GetHashCode(), "InstantMessageEventHandler.AddBuddy");
			this.ThowIfInvalidProvider("AddBuddy");
			string text = (string)base.GetParameter("lDn");
			string text2 = (string)base.GetParameter("bdyAddr");
			string text3 = string.Empty;
			if (base.IsParameterSet("msg"))
			{
				text3 = (string)base.GetParameter("msg");
				if (text3.Length > 300)
				{
					text3 = text3.Substring(0, 300);
				}
			}
			if (text != null)
			{
				text2 = this.GetSipUriFromLegacyDn(text, text2);
				this.ThrowIfSipInvalid(text2, false);
			}
			try
			{
				InstantMessageBuddy instantMessageBuddy = InstantMessageBuddy.Create((string)base.GetParameter("ID"), text2, (string)base.GetParameter("dName"));
				instantMessageBuddy.RequestMessage = text3;
				InstantMessageGroup group = InstantMessageGroup.Create((string)base.GetParameter("grpId"), (string)base.GetParameter("grpNme"));
				base.UserContext.InstantMessageManager.Provider.AddBuddy(instantMessageBuddy, group);
				if (text != null)
				{
					this.Writer.Write(text2);
				}
			}
			catch (ArgumentException innerException)
			{
				throw new OwaInvalidRequestException("The SipUri is not valid. SipUri:" + text2, innerException);
			}
		}

		// Token: 0x06002E2A RID: 11818 RVA: 0x00107C6C File Offset: 0x00105E6C
		[OwaEventParameter("bdyAddr", typeof(string))]
		[OwaEventParameter("iType", typeof(int), false, true)]
		[OwaEventParameter("lDn", typeof(string), false, true)]
		[OwaEvent("RemFrmBdyLst")]
		[OwaEventParameter("sGid", typeof(string), false, true)]
		public void RemoveFromBuddyList()
		{
			ExTraceGlobals.OehCallTracer.TraceDebug((long)this.GetHashCode(), "InstantMessageEventHandler.RemoveFromBuddyList");
			this.ThowIfInvalidProvider("RemoveFromBuddyList");
			string text = (string)base.GetParameter("lDn");
			string text2 = (string)base.GetParameter("bdyAddr");
			if (text != null)
			{
				text2 = this.GetSipUriFromLegacyDn(text, text2);
				this.ThrowIfSipInvalid(text2, false);
			}
			try
			{
				InstantMessageBuddy instantMessageBuddy = InstantMessageBuddy.Create((string)base.GetParameter("sGid"), text2, string.Empty);
				if (base.IsParameterSet("iType"))
				{
					instantMessageBuddy.AddressType = (int)base.GetParameter("iType");
				}
				base.UserContext.InstantMessageManager.Provider.RemoveBuddy(instantMessageBuddy);
				if (text != null)
				{
					this.Writer.Write(text2);
				}
			}
			catch (ArgumentException innerException)
			{
				throw new OwaInvalidRequestException("The SipUri is not valid. SipUri:" + text2, innerException);
			}
		}

		// Token: 0x06002E2B RID: 11819 RVA: 0x00107D5C File Offset: 0x00105F5C
		[OwaEvent("AcceptBdy")]
		[OwaEventParameter("iType", typeof(int))]
		[OwaEventParameter("grpNme", typeof(string), false, true)]
		[OwaEventParameter("bdyAddr", typeof(string))]
		[OwaEventParameter("grpId", typeof(string), false, true)]
		public void AcceptBuddy()
		{
			ExTraceGlobals.OehCallTracer.TraceDebug((long)this.GetHashCode(), "InstantMessageEventHandler.AcceptBuddy");
			this.ThowIfInvalidProvider("AcceptBuddy");
			InstantMessageBuddy instantMessageBuddy = InstantMessageBuddy.Create(string.Empty, InstantMessageUtilities.ToSipFormat((string)base.GetParameter("bdyAddr")), string.Empty);
			InstantMessageGroup group = InstantMessageGroup.Create((string)base.GetParameter("grpId"), (string)base.GetParameter("grpNme"), InstantMessageGroupType.Standard);
			if (base.IsParameterSet("iType"))
			{
				instantMessageBuddy.AddressType = (int)base.GetParameter("iType");
			}
			base.UserContext.InstantMessageManager.Provider.AcceptBuddy(instantMessageBuddy, group);
		}

		// Token: 0x06002E2C RID: 11820 RVA: 0x00107E10 File Offset: 0x00106010
		[OwaEventParameter("iType", typeof(int))]
		[OwaEventParameter("bdyAddr", typeof(string))]
		[OwaEvent("DeclineBdy")]
		public void DeclineBuddy()
		{
			ExTraceGlobals.OehCallTracer.TraceDebug((long)this.GetHashCode(), "InstantMessageEventHandler.DeclineBuddy");
			this.ThowIfInvalidProvider("DeclineBuddy");
			InstantMessageBuddy instantMessageBuddy = InstantMessageBuddy.Create(string.Empty, InstantMessageUtilities.ToSipFormat((string)base.GetParameter("bdyAddr")), string.Empty);
			if (base.IsParameterSet("iType"))
			{
				instantMessageBuddy.AddressType = (int)base.GetParameter("iType");
			}
			base.UserContext.InstantMessageManager.Provider.DeclineBuddy(instantMessageBuddy);
		}

		// Token: 0x06002E2D RID: 11821 RVA: 0x00107E9C File Offset: 0x0010609C
		[OwaEventParameter("iType", typeof(int))]
		[OwaEventParameter("bdyAddr", typeof(string))]
		[OwaEvent("UnblockBuddy")]
		public void UnblockBuddy()
		{
			ExTraceGlobals.OehCallTracer.TraceDebug((long)this.GetHashCode(), "InstantMessageEventHandler.UnblockBuddy");
			this.ThowIfInvalidProvider("UnblockBuddy");
			InstantMessageBuddy instantMessageBuddy = InstantMessageBuddy.Create(string.Empty, (string)base.GetParameter("bdyAddr"), string.Empty);
			if (base.IsParameterSet("iType"))
			{
				instantMessageBuddy.AddressType = (int)base.GetParameter("iType");
			}
			base.UserContext.InstantMessageManager.Provider.UnblockBuddy(instantMessageBuddy);
		}

		// Token: 0x06002E2E RID: 11822 RVA: 0x00107F24 File Offset: 0x00106124
		[OwaEvent("BlockBuddy")]
		[OwaEventParameter("bdyAddr", typeof(string))]
		[OwaEventParameter("iType", typeof(int))]
		public void BlockBuddy()
		{
			ExTraceGlobals.OehCallTracer.TraceDebug((long)this.GetHashCode(), "InstantMessageEventHandler.BlockBuddy");
			this.ThowIfInvalidProvider("BlockBuddy");
			InstantMessageBuddy instantMessageBuddy = InstantMessageBuddy.Create(string.Empty, (string)base.GetParameter("bdyAddr"), string.Empty);
			if (base.IsParameterSet("iType"))
			{
				instantMessageBuddy.AddressType = (int)base.GetParameter("iType");
			}
			base.UserContext.InstantMessageManager.Provider.BlockBuddy(instantMessageBuddy);
		}

		// Token: 0x06002E2F RID: 11823 RVA: 0x00107FAB File Offset: 0x001061AB
		[OwaEventParameter("sUri", typeof(string))]
		[OwaEvent("RemFrmPDL")]
		[OwaEventParameter("grpNme", typeof(string))]
		public void RemoveFromPersonalDistributionList()
		{
			ExTraceGlobals.OehCallTracer.TraceDebug((long)this.GetHashCode(), "InstantMessageEventHandler.RemoveFromPersonalDistributionList");
		}

		// Token: 0x06002E30 RID: 11824 RVA: 0x00107FC4 File Offset: 0x001061C4
		[OwaEventParameter("sGid", typeof(string))]
		[OwaEventParameter("dName", typeof(string), false, true)]
		[OwaEventParameter("tag", typeof(bool), false, true)]
		[OwaEventParameter("grpIds", typeof(string), true, true)]
		[OwaEventParameter("bdyAddr", typeof(string))]
		[OwaEvent("RemFrmGrp")]
		[OwaEventParameter("grpId", typeof(string))]
		[OwaEventParameter("grpNme", typeof(string))]
		public void RemoveFromGroup()
		{
			ExTraceGlobals.OehCallTracer.TraceDebug((long)this.GetHashCode(), "InstantMessageEventHandler.RemoveFromGroup");
			this.ThowIfInvalidProvider("RemoveFromGroup");
			InstantMessageGroup group = InstantMessageGroup.Create((string)base.GetParameter("grpId"), (string)base.GetParameter("grpNme"));
			InstantMessageBuddy instantMessageBuddy = InstantMessageBuddy.Create((string)base.GetParameter("sGid"), (string)base.GetParameter("bdyAddr"), (string)base.GetParameter("dName"));
			if (base.IsParameterSet("tag"))
			{
				instantMessageBuddy.Tagged = (bool)base.GetParameter("tag");
			}
			instantMessageBuddy.AddGroups((string[])base.GetParameter("grpIds"));
			base.UserContext.InstantMessageManager.Provider.RemoveFromGroup(group, instantMessageBuddy);
		}

		// Token: 0x06002E31 RID: 11825 RVA: 0x001080A0 File Offset: 0x001062A0
		[OwaEventParameter("grpIds", typeof(string), true, true)]
		[OwaEventParameter("oldGrpNme", typeof(string))]
		[OwaEventParameter("tag", typeof(bool), false, true)]
		[OwaEventParameter("bdyAddr", typeof(string))]
		[OwaEventParameter("sGid", typeof(string))]
		[OwaEventParameter("oldGrpId", typeof(string))]
		[OwaEventParameter("newGrpId", typeof(string))]
		[OwaEventParameter("newGrpNme", typeof(string))]
		[OwaEventParameter("dName", typeof(string), false, true)]
		[OwaEvent("MvBdy")]
		public void MoveBuddy()
		{
			ExTraceGlobals.OehCallTracer.TraceDebug((long)this.GetHashCode(), "InstantMessageEventHandler.MoveBuddy");
			this.ThowIfInvalidProvider("MoveBuddy");
			InstantMessageGroup oldGroup = InstantMessageGroup.Create((string)base.GetParameter("oldGrpId"), (string)base.GetParameter("oldGrpNme"));
			InstantMessageGroup newGroup = InstantMessageGroup.Create((string)base.GetParameter("newGrpId"), (string)base.GetParameter("newGrpNme"));
			InstantMessageBuddy instantMessageBuddy = InstantMessageBuddy.Create((string)base.GetParameter("sGid"), (string)base.GetParameter("bdyAddr"), (string)base.GetParameter("dName"));
			if (base.IsParameterSet("tag"))
			{
				instantMessageBuddy.Tagged = (bool)base.GetParameter("tag");
			}
			instantMessageBuddy.AddGroups((string[])base.GetParameter("grpIds"));
			base.UserContext.InstantMessageManager.Provider.MoveBuddy(oldGroup, newGroup, instantMessageBuddy);
		}

		// Token: 0x06002E32 RID: 11826 RVA: 0x001081A4 File Offset: 0x001063A4
		[OwaEvent("CpBdy")]
		[OwaEventParameter("grpIds", typeof(string), true, true)]
		[OwaEventParameter("bdyAddr", typeof(string))]
		[OwaEventParameter("sGid", typeof(string))]
		[OwaEventParameter("grpId", typeof(string))]
		[OwaEventParameter("grpNme", typeof(string))]
		[OwaEventParameter("dName", typeof(string), false, true)]
		[OwaEventParameter("tag", typeof(bool), false, true)]
		public void CopyBuddy()
		{
			ExTraceGlobals.OehCallTracer.TraceDebug((long)this.GetHashCode(), "InstantMessageEventHandler.CopyBuddy");
			this.ThowIfInvalidProvider("CopyBuddy");
			InstantMessageGroup group = InstantMessageGroup.Create((string)base.GetParameter("grpId"), (string)base.GetParameter("grpNme"));
			InstantMessageBuddy instantMessageBuddy = InstantMessageBuddy.Create((string)base.GetParameter("sGid"), (string)base.GetParameter("bdyAddr"), (string)base.GetParameter("dName"));
			if (base.IsParameterSet("tag"))
			{
				instantMessageBuddy.Tagged = (bool)base.GetParameter("tag");
			}
			instantMessageBuddy.AddGroups((string[])base.GetParameter("grpIds"));
			base.UserContext.InstantMessageManager.Provider.CopyBuddy(group, instantMessageBuddy);
		}

		// Token: 0x06002E33 RID: 11827 RVA: 0x0010827E File Offset: 0x0010647E
		[OwaEventParameter("sUri", typeof(string))]
		[OwaEvent("TgBdy")]
		public void TagBuddy()
		{
			ExTraceGlobals.OehCallTracer.TraceDebug((long)this.GetHashCode(), "InstantMessageEventHandler.TagBuddy");
		}

		// Token: 0x06002E34 RID: 11828 RVA: 0x00108298 File Offset: 0x00106498
		[OwaEventParameter("grpNme", typeof(string))]
		[OwaEvent("CrGrp")]
		public void CreateGroup()
		{
			ExTraceGlobals.OehCallTracer.TraceDebug((long)this.GetHashCode(), "InstantMessageEventHandler.CreateGroup");
			this.ThowIfInvalidProvider("CreateGroup");
			string groupName = (string)base.GetParameter("grpNme");
			base.UserContext.InstantMessageManager.Provider.CreateGroup(groupName);
		}

		// Token: 0x06002E35 RID: 11829 RVA: 0x001082F0 File Offset: 0x001064F0
		[OwaEventParameter("grpNme", typeof(string))]
		[OwaEvent("RemGrp")]
		[OwaEventParameter("grpId", typeof(string))]
		public void RemoveGroup()
		{
			ExTraceGlobals.OehCallTracer.TraceDebug((long)this.GetHashCode(), "InstantMessageEventHandler.RemoveGroup");
			this.ThowIfInvalidProvider("RemoveGroup");
			InstantMessageGroup group = InstantMessageGroup.Create((string)base.GetParameter("grpId"), (string)base.GetParameter("grpNme"));
			base.UserContext.InstantMessageManager.Provider.RemoveGroup(group);
		}

		// Token: 0x06002E36 RID: 11830 RVA: 0x0010835C File Offset: 0x0010655C
		[OwaEvent("RenGrp")]
		[OwaEventParameter("grpId", typeof(string))]
		[OwaEventParameter("newGrpNme", typeof(string))]
		[OwaEventParameter("oldGrpNme", typeof(string))]
		public void RenameGroup()
		{
			ExTraceGlobals.OehCallTracer.TraceDebug((long)this.GetHashCode(), "InstantMessageEventHandler.RenameGroup");
			this.ThowIfInvalidProvider("RenameGroup");
			InstantMessageGroup group = InstantMessageGroup.Create((string)base.GetParameter("grpId"), (string)base.GetParameter("oldGrpNme"));
			string newGroupName = (string)base.GetParameter("newGrpNme");
			base.UserContext.InstantMessageManager.Provider.RenameGroup(group, newGroupName);
		}

		// Token: 0x06002E37 RID: 11831 RVA: 0x001083D8 File Offset: 0x001065D8
		[OwaEvent("GtBdyLst")]
		public void GetBuddyList()
		{
			ExTraceGlobals.OehCallTracer.TraceDebug((long)this.GetHashCode(), "InstantMessageEventHandler.GetBuddyList");
			this.ThowIfInvalidProvider("GetBuddyList");
			base.UserContext.InstantMessageManager.Provider.GetBuddyList();
		}

		// Token: 0x06002E38 RID: 11832 RVA: 0x00108410 File Offset: 0x00106610
		[OwaEventParameter("sUris", typeof(string))]
		[OwaEventParameter("cId", typeof(string))]
		[OwaEvent("InvSmOne")]
		public void InviteSomeone()
		{
			ExTraceGlobals.OehCallTracer.TraceDebug((long)this.GetHashCode(), "InstantMessageEventHandler.InviteSomeone");
		}

		// Token: 0x06002E39 RID: 11833 RVA: 0x00108428 File Offset: 0x00106628
		[OwaEventParameter("cId", typeof(string))]
		[OwaEvent("RmvSmOne")]
		[OwaEventParameter("sUris", typeof(string))]
		public void RemoveSomeone()
		{
			ExTraceGlobals.OehCallTracer.TraceDebug((long)this.GetHashCode(), "InstantMessageEventHandler.RemoveSomeone");
		}

		// Token: 0x06002E3A RID: 11834 RVA: 0x00108440 File Offset: 0x00106640
		[OwaEvent("EndChtSsn")]
		[OwaEventParameter("fDisc", typeof(bool), false, true)]
		[OwaEventParameter("cId", typeof(string))]
		public void EndChatSession()
		{
			ExTraceGlobals.OehCallTracer.TraceDebug((long)this.GetHashCode(), "InstantMessageEventHandler.EndChatSession");
			this.ThowIfInvalidProvider("EndChatSession");
			bool disconnectSession = false;
			if (base.IsParameterSet("fDisc"))
			{
				disconnectSession = (bool)base.GetParameter("fDisc");
			}
			string text = (string)base.GetParameter("cId");
			int chatSessionId;
			if (int.TryParse(text, out chatSessionId))
			{
				base.UserContext.InstantMessageManager.Provider.EndChatSession(chatSessionId, disconnectSession);
				return;
			}
			throw new OwaInvalidRequestException("The chat ID format is not valid:" + text);
		}

		// Token: 0x06002E3B RID: 11835 RVA: 0x001084D4 File Offset: 0x001066D4
		[OwaEvent("NtfyTpng")]
		[OwaEventParameter("cId", typeof(string))]
		public void NotifyTyping()
		{
			ExTraceGlobals.OehCallTracer.TraceDebug((long)this.GetHashCode(), "InstantMessageEventHandler.NotifyTyping");
			this.ThowIfInvalidProvider("NotifyTyping");
			string text = (string)base.GetParameter("cId");
			int chatSessionId;
			if (int.TryParse(text, out chatSessionId))
			{
				base.UserContext.InstantMessageManager.Provider.NotifyTyping(chatSessionId, false);
				return;
			}
			throw new OwaInvalidRequestException("The chat ID format is not valid:" + text);
		}

		// Token: 0x06002E3C RID: 11836 RVA: 0x00108548 File Offset: 0x00106748
		[OwaEvent("NtfyTpngCncl")]
		[OwaEventParameter("cId", typeof(string))]
		public void NotifyTypingCancelled()
		{
			ExTraceGlobals.OehCallTracer.TraceDebug((long)this.GetHashCode(), "InstantMessageEventHandler.NotifyTypingCancelled");
			this.ThowIfInvalidProvider("NotifyTypingCancelled");
			string text = (string)base.GetParameter("cId");
			int chatSessionId;
			if (int.TryParse(text, out chatSessionId))
			{
				base.UserContext.InstantMessageManager.Provider.NotifyTyping(chatSessionId, true);
				return;
			}
			throw new OwaInvalidRequestException("The chat ID format is not valid:" + text);
		}

		// Token: 0x06002E3D RID: 11837 RVA: 0x001085B9 File Offset: 0x001067B9
		[OwaEventParameter("expLvl", typeof(int))]
		[OwaEvent("ExpndDL")]
		[OwaEventParameter("alias", typeof(string))]
		public void ExpandDistributionList()
		{
			ExTraceGlobals.OehCallTracer.TraceDebug((long)this.GetHashCode(), "InstantMessageEventHandler.ExpandDistributionList");
		}

		// Token: 0x06002E3E RID: 11838 RVA: 0x001085D4 File Offset: 0x001067D4
		[OwaEvent("SgnIn")]
		[OwaEventParameter("mnlSI", typeof(bool), false, true)]
		public void SignIn()
		{
			ExTraceGlobals.OehCallTracer.TraceDebug((long)this.GetHashCode(), "InstantMessageEventHandler.SignIn");
			if (base.UserContext.InstantMessageManager != null)
			{
				bool flag = false;
				if (base.IsParameterSet("mnlSI"))
				{
					flag = (bool)base.GetParameter("mnlSI");
				}
				if (flag)
				{
					base.UserContext.SaveSignedInToIMStatus();
				}
				base.UserContext.InstantMessageManager.StartProvider();
			}
		}

		// Token: 0x06002E3F RID: 11839 RVA: 0x00108642 File Offset: 0x00106842
		[OwaEvent("SgnOut")]
		public void SignOut()
		{
			ExTraceGlobals.OehCallTracer.TraceDebug((long)this.GetHashCode(), "InstantMessageEventHandler.SignOut");
			if (base.UserContext.InstantMessageManager != null)
			{
				base.UserContext.InstantMessageManager.SignOut();
				base.UserContext.SaveSignedOutOfIMStatus();
			}
		}

		// Token: 0x06002E40 RID: 11840 RVA: 0x00108684 File Offset: 0x00106884
		[OwaEvent("ChngPrsnc")]
		[OwaEventParameter("prsnce", typeof(int), false, true)]
		public void ChangePresence()
		{
			ExTraceGlobals.OehCallTracer.TraceDebug((long)this.GetHashCode(), "InstantMessageEventHandler.ChangePresence");
			this.ThowIfInvalidProvider("ChangePresence");
			if (base.IsParameterSet("prsnce"))
			{
				base.UserContext.InstantMessageManager.Provider.PublishSelfPresence((int)base.GetParameter("prsnce"));
				return;
			}
			base.UserContext.InstantMessageManager.Provider.PublishResetStatus();
		}

		// Token: 0x06002E41 RID: 11841 RVA: 0x001086FA File Offset: 0x001068FA
		[OwaEvent("AccptDeclIMMsg")]
		[OwaEventParameter("sUri", typeof(string))]
		[OwaEventParameter("accpt", typeof(int))]
		public void AcceptDeclineIMMessage()
		{
			ExTraceGlobals.OehCallTracer.TraceDebug((long)this.GetHashCode(), "InstantMessageEventHandler.AcceptDeclineIMMessage");
		}

		// Token: 0x06002E42 RID: 11842 RVA: 0x00108714 File Offset: 0x00106914
		[OwaEvent("IsSssnStrted")]
		public void IsSessionStarted()
		{
			ExTraceGlobals.OehCallTracer.TraceDebug((long)this.GetHashCode(), "InstantMessageEventHandler.IsSessionStarted");
			bool value = base.UserContext.InstantMessageManager != null && base.UserContext.InstantMessageManager.Provider != null && base.UserContext.InstantMessageManager.Provider.IsSessionStarted;
			this.Writer.Write(value);
		}

		// Token: 0x06002E43 RID: 11843 RVA: 0x0010877C File Offset: 0x0010697C
		[OwaEvent("GetItemIMInfo")]
		[OwaEventParameter("gtNrmSub", typeof(int), false, true)]
		[OwaEventParameter("mId", typeof(OwaStoreObjectId))]
		public void GetItemIMInfo()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "InstantMessageEventHandler.GetItemIMInfo");
			base.ResponseContentType = OwaEventContentType.Javascript;
			string text = null;
			string text2 = null;
			string text3 = null;
			string text4 = null;
			OwaStoreObjectId itemId = (OwaStoreObjectId)base.GetParameter("mId");
			bool getNormalizedSubject = false;
			if (base.IsParameterSet("gtNrmSub"))
			{
				getNormalizedSubject = ((int)base.GetParameter("gtNrmSub") == 1);
			}
			InstantMessageUtilities.GetItemIMInfo(itemId, getNormalizedSubject, base.UserContext, out text, out text2, out text3, out text4);
			this.Writer.WriteLine("{");
			this.Writer.Write("_dn : '");
			Utilities.JavascriptEncode((text == null) ? string.Empty : text, this.Writer);
			this.Writer.WriteLine("',");
			if (base.UserContext.InstantMessagingType == InstantMessagingTypeOptions.Ocs)
			{
				this.Writer.Write("_uri : '");
				Utilities.JavascriptEncode((text3 == null) ? string.Empty : text3, this.Writer);
				this.Writer.WriteLine("',");
			}
			this.Writer.Write("_sub : '");
			Utilities.JavascriptEncode((text4 == null) ? string.Empty : text4, this.Writer);
			this.Writer.WriteLine("'");
			this.Writer.Write("}");
		}

		// Token: 0x06002E44 RID: 11844 RVA: 0x001088CC File Offset: 0x00106ACC
		[OwaEvent("ParticipateInConversation")]
		[OwaEventParameter("cId", typeof(string))]
		public void ParticipateInConversation()
		{
			ExTraceGlobals.OehCallTracer.TraceDebug((long)this.GetHashCode(), "InstantMessageEventHandler.ParticipateInConversation");
			this.ThowIfInvalidProvider("ParticipateInConversation");
			string text = (string)base.GetParameter("cId");
			int conversationId;
			if (int.TryParse(text, out conversationId))
			{
				base.UserContext.InstantMessageManager.Provider.ParticipateInConversation(conversationId);
				return;
			}
			throw new OwaInvalidRequestException("The chat ID format is not valid:" + text);
		}

		// Token: 0x06002E45 RID: 11845 RVA: 0x0010893C File Offset: 0x00106B3C
		private void ThrowIfSipInvalid(string sipUri, bool isOperationAllowedOnSelf)
		{
			if (sipUri == null)
			{
				throw new OwaUserNotIMEnabledException(LocalizedStrings.GetNonEncoded(-1159901588));
			}
			if (!isOperationAllowedOnSelf && sipUri == base.OwaContext.UserContext.SipUri)
			{
				throw new OwaIMOperationNotAllowedToSelf(LocalizedStrings.GetNonEncoded(1382663936));
			}
		}

		// Token: 0x06002E46 RID: 11846 RVA: 0x0010897C File Offset: 0x00106B7C
		private void ThowIfInvalidProvider(string methodName)
		{
			if (base.UserContext.InstantMessageManager == null || base.UserContext.InstantMessageManager.Provider == null || !base.UserContext.InstantMessageManager.Provider.IsSessionStarted)
			{
				throw new OwaInstantMessageEventHandlerTransientException("The Instant Message Service is not started yet. Please try again later.");
			}
			if (base.UserContext.InstantMessagingType != InstantMessagingTypeOptions.Ocs)
			{
				throw new OwaInvalidOperationException(methodName + " was called with an instant messaging type that is not supported. Instant Messaging Type:" + base.UserContext.InstantMessagingType.ToString());
			}
		}

		// Token: 0x06002E47 RID: 11847 RVA: 0x00108A00 File Offset: 0x00106C00
		private string GetSipUriFromLegacyDn(string legacyDn, string defaultSipUri)
		{
			IRecipientSession recipientSession = Utilities.CreateADRecipientSession(ConsistencyMode.IgnoreInvalid, base.OwaContext.UserContext);
			Result<ADRawEntry>[] array = recipientSession.FindByLegacyExchangeDNs(new string[]
			{
				legacyDn
			}, InstantMessageEventHandler.recipientQueryProperties);
			if (array == null || array.Length != 1)
			{
				return defaultSipUri;
			}
			ADRawEntry data = array[0].Data;
			if (data == null)
			{
				return defaultSipUri;
			}
			return InstantMessageUtilities.GetSipUri((ProxyAddressCollection)data[ADRecipientSchema.EmailAddresses]);
		}

		// Token: 0x06002E48 RID: 11848 RVA: 0x00108A6C File Offset: 0x00106C6C
		[OwaEventParameter("grpIds", typeof(string), true)]
		[OwaEvent("PersistExpand")]
		public void PersistExpandStatus()
		{
			ExTraceGlobals.OehCallTracer.TraceDebug((long)this.GetHashCode(), "InstantMessageEventHandler.PersistExpandStatus");
			string[] groupIds = (string[])base.GetParameter("grpIds");
			InstantMessageExpandPersistence.SetExpandedGroups(base.UserContext, groupIds);
		}

		// Token: 0x04001FA3 RID: 8099
		public const string EventNamespace = "IM";

		// Token: 0x04001FA4 RID: 8100
		public const string MethodSendNewChatMessage = "SndNwChtMsg";

		// Token: 0x04001FA5 RID: 8101
		public const string MethodSendChatMessage = "SndChtMsg";

		// Token: 0x04001FA6 RID: 8102
		public const string MethodPresenceSubscriptionChange = "SubChng";

		// Token: 0x04001FA7 RID: 8103
		public const string MethodQueryPresence = "QryPrsnc";

		// Token: 0x04001FA8 RID: 8104
		public const string MethodAddBuddy = "AddBdy";

		// Token: 0x04001FA9 RID: 8105
		public const string MethodRemoveFromBuddyList = "RemFrmBdyLst";

		// Token: 0x04001FAA RID: 8106
		public const string MethodAcceptBuddy = "AcceptBdy";

		// Token: 0x04001FAB RID: 8107
		public const string MethodDeclineBuddy = "DeclineBdy";

		// Token: 0x04001FAC RID: 8108
		public const string MethodBlockBuddy = "BlockBuddy";

		// Token: 0x04001FAD RID: 8109
		public const string MethodUnblockBuddy = "UnblockBuddy";

		// Token: 0x04001FAE RID: 8110
		public const string MethodRemoveFromPersonalDistributionList = "RemFrmPDL";

		// Token: 0x04001FAF RID: 8111
		public const string MethodRemoveFromGroup = "RemFrmGrp";

		// Token: 0x04001FB0 RID: 8112
		public const string MethodMoveBuddy = "MvBdy";

		// Token: 0x04001FB1 RID: 8113
		public const string MethodCopyBuddy = "CpBdy";

		// Token: 0x04001FB2 RID: 8114
		public const string MethodTagBuddy = "TgBdy";

		// Token: 0x04001FB3 RID: 8115
		public const string MethodCreateGroup = "CrGrp";

		// Token: 0x04001FB4 RID: 8116
		public const string MethodRemoveGroup = "RemGrp";

		// Token: 0x04001FB5 RID: 8117
		public const string MethodRenameGroup = "RenGrp";

		// Token: 0x04001FB6 RID: 8118
		public const string MethodGetBuddyList = "GtBdyLst";

		// Token: 0x04001FB7 RID: 8119
		public const string MethodInviteSomeone = "InvSmOne";

		// Token: 0x04001FB8 RID: 8120
		public const string MethodEndChatSession = "EndChtSsn";

		// Token: 0x04001FB9 RID: 8121
		public const string MethodRemoveSomeone = "RmvSmOne";

		// Token: 0x04001FBA RID: 8122
		public const string MethodNotifyTyping = "NtfyTpng";

		// Token: 0x04001FBB RID: 8123
		public const string MethodNotifyTypingCancelled = "NtfyTpngCncl";

		// Token: 0x04001FBC RID: 8124
		public const string MethodExpandDistributionList = "ExpndDL";

		// Token: 0x04001FBD RID: 8125
		public const string MethodSignIn = "SgnIn";

		// Token: 0x04001FBE RID: 8126
		public const string MethodSignOut = "SgnOut";

		// Token: 0x04001FBF RID: 8127
		public const string MethodChangePresence = "ChngPrsnc";

		// Token: 0x04001FC0 RID: 8128
		public const string MethodAcceptDeclineIMMessage = "AccptDeclIMMsg";

		// Token: 0x04001FC1 RID: 8129
		public const string MethodIsSessionStarted = "IsSssnStrted";

		// Token: 0x04001FC2 RID: 8130
		public const string MethodGetItemIMInfo = "GetItemIMInfo";

		// Token: 0x04001FC3 RID: 8131
		public const string MethodPersistExpand = "PersistExpand";

		// Token: 0x04001FC4 RID: 8132
		public const string MethodParticipateInConversation = "ParticipateInConversation";

		// Token: 0x04001FC5 RID: 8133
		public const string LegacyDN = "lDn";

		// Token: 0x04001FC6 RID: 8134
		public const string SipUri = "sUri";

		// Token: 0x04001FC7 RID: 8135
		public const string DisconnectSession = "fDisc";

		// Token: 0x04001FC8 RID: 8136
		public const string ContactId = "sGid";

		// Token: 0x04001FC9 RID: 8137
		public const string BuddyAddress = "bdyAddr";

		// Token: 0x04001FCA RID: 8138
		public const string DisplayName = "dName";

		// Token: 0x04001FCB RID: 8139
		public const string Message = "msg";

		// Token: 0x04001FCC RID: 8140
		public const string SipUris = "sUris";

		// Token: 0x04001FCD RID: 8141
		public const string BuddyAddressType = "iType";

		// Token: 0x04001FCE RID: 8142
		public const string SipUrisToQuery = "qryUris";

		// Token: 0x04001FCF RID: 8143
		public const string SipUrisToUnsubscribe = "unsbsUris";

		// Token: 0x04001FD0 RID: 8144
		public const string SipUrisToSubscribe = "sbsUris";

		// Token: 0x04001FD1 RID: 8145
		public const string GroupName = "grpNme";

		// Token: 0x04001FD2 RID: 8146
		public const string GroupId = "grpId";

		// Token: 0x04001FD3 RID: 8147
		public const string ChatSessionId = "cId";

		// Token: 0x04001FD4 RID: 8148
		public const string ChatMessage = "cMsg";

		// Token: 0x04001FD5 RID: 8149
		public const string Format = "frmt";

		// Token: 0x04001FD6 RID: 8150
		public const string OldGroupName = "oldGrpNme";

		// Token: 0x04001FD7 RID: 8151
		public const string OldGroupId = "oldGrpId";

		// Token: 0x04001FD8 RID: 8152
		public const string NewGroupName = "newGrpNme";

		// Token: 0x04001FD9 RID: 8153
		public const string NewGroupId = "newGrpId";

		// Token: 0x04001FDA RID: 8154
		public const string AliasOfDL = "alias";

		// Token: 0x04001FDB RID: 8155
		public const string LevelOfExpansion = "expLvl";

		// Token: 0x04001FDC RID: 8156
		public const string AcceptFlag = "accpt";

		// Token: 0x04001FDD RID: 8157
		public const string NewPresence = "prsnce";

		// Token: 0x04001FDE RID: 8158
		public const string MItemId = "mId";

		// Token: 0x04001FDF RID: 8159
		public const string GetNormalizedSubject = "gtNrmSub";

		// Token: 0x04001FE0 RID: 8160
		public const string Password = "sp";

		// Token: 0x04001FE1 RID: 8161
		public const string SavePasswordFlag = "fsp";

		// Token: 0x04001FE2 RID: 8162
		public const string Tagged = "tag";

		// Token: 0x04001FE3 RID: 8163
		public const string ContactGroups = "grpIds";

		// Token: 0x04001FE4 RID: 8164
		public const string SignInManually = "mnlSI";

		// Token: 0x04001FE5 RID: 8165
		private static PropertyDefinition[] recipientQueryProperties = new PropertyDefinition[]
		{
			ADRecipientSchema.PrimarySmtpAddress
		};
	}
}
