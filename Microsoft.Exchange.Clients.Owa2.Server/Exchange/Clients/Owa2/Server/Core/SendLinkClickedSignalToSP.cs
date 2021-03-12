using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Security.OAuth;
using Microsoft.Exchange.Services.Core;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;
using Microsoft.Exchange.SharePointSignalStore;
using Microsoft.Exchange.UnifiedGroups;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200034E RID: 846
	internal class SendLinkClickedSignalToSP : ServiceCommand<bool>
	{
		// Token: 0x06001B98 RID: 7064 RVA: 0x00069D90 File Offset: 0x00067F90
		internal SendLinkClickedSignalToSP(CallContext callContext, SendLinkClickedSignalToSPRequest request) : base(callContext)
		{
			if (callContext == null)
			{
				throw new OwaInvalidRequestException("callContext parameter was null");
			}
			if (request == null)
			{
				throw new OwaInvalidRequestException("request parameter was null");
			}
			if (request.ID == null)
			{
				throw new OwaInvalidRequestException("request.ID parameter was null");
			}
			if (string.IsNullOrEmpty(request.Url))
			{
				throw new OwaInvalidRequestException("request.Url parameter was null");
			}
			this.callContext = callContext;
			this.itemid = request.ID;
			this.url = request.Url;
			this.title = request.Title;
			this.description = request.Description;
			this.imgurl = request.ImgURL;
			this.imgdimensions = request.ImgDimensions;
			this.linkStats = new LinkClickedSignalStats();
		}

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x06001B99 RID: 7065 RVA: 0x00069E48 File Offset: 0x00068048
		// (set) Token: 0x06001B9A RID: 7066 RVA: 0x00069E88 File Offset: 0x00068088
		internal IRecipientSession ADRecipientSession
		{
			get
			{
				IRecipientSession result;
				if ((result = this.adRecipientSession) == null)
				{
					result = (this.adRecipientSession = this.callContext.ADRecipientSessionContext.GetGALScopedADRecipientSession(this.callContext.EffectiveCaller.ClientSecurityContext));
				}
				return result;
			}
			set
			{
				this.adRecipientSession = value;
			}
		}

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x06001B9B RID: 7067 RVA: 0x00069E94 File Offset: 0x00068094
		// (set) Token: 0x06001B9C RID: 7068 RVA: 0x00069EBA File Offset: 0x000680BA
		internal ILogger Logger
		{
			get
			{
				ILogger result;
				if ((result = this.logger) == null)
				{
					result = (this.logger = new TraceLogger(false));
				}
				return result;
			}
			set
			{
				this.logger = value;
			}
		}

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x06001B9D RID: 7069 RVA: 0x00069EC4 File Offset: 0x000680C4
		// (set) Token: 0x06001B9E RID: 7070 RVA: 0x00069EF0 File Offset: 0x000680F0
		internal Func<MessageType> GetMailMessage
		{
			get
			{
				Func<MessageType> result;
				if ((result = this.getmailmessage) == null)
				{
					result = (this.getmailmessage = new Func<MessageType>(this.GetMessage));
				}
				return result;
			}
			set
			{
				this.getmailmessage = value;
			}
		}

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x06001B9F RID: 7071 RVA: 0x00069EFC File Offset: 0x000680FC
		// (set) Token: 0x06001BA0 RID: 7072 RVA: 0x00069F28 File Offset: 0x00068128
		internal Func<string> GetSharePointUrl
		{
			get
			{
				Func<string> result;
				if ((result = this.getsharepointurl) == null)
				{
					result = (this.getsharepointurl = new Func<string>(this.GetSharePointUrlFromAAD));
				}
				return result;
			}
			set
			{
				this.getsharepointurl = value;
			}
		}

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x06001BA1 RID: 7073 RVA: 0x00069F34 File Offset: 0x00068134
		// (set) Token: 0x06001BA2 RID: 7074 RVA: 0x00069F60 File Offset: 0x00068160
		internal Func<ICredentials> GetUserCredentials
		{
			get
			{
				Func<ICredentials> result;
				if ((result = this.getusercredentials) == null)
				{
					result = (this.getusercredentials = new Func<ICredentials>(this.GetCredentials));
				}
				return result;
			}
			set
			{
				this.getusercredentials = value;
			}
		}

		// Token: 0x06001BA3 RID: 7075 RVA: 0x00069F6C File Offset: 0x0006816C
		internal ItemResponseShape MakeItemResponseShape()
		{
			return new ItemResponseShape
			{
				AdditionalProperties = new PropertyPath[]
				{
					new PropertyUri(PropertyUriEnum.ToRecipients),
					new PropertyUri(PropertyUriEnum.CcRecipients),
					new PropertyUri(PropertyUriEnum.From)
				}
			};
		}

		// Token: 0x06001BA4 RID: 7076 RVA: 0x00069FAC File Offset: 0x000681AC
		internal GetItemRequest MakeGetItemRequest()
		{
			return new GetItemRequest
			{
				ItemShape = this.MakeItemResponseShape(),
				Ids = new BaseItemId[]
				{
					this.itemid
				}
			};
		}

		// Token: 0x06001BA5 RID: 7077 RVA: 0x00069FE4 File Offset: 0x000681E4
		private MessageType GetMessage()
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			GetItemRequest request = this.MakeGetItemRequest();
			GetItem getItem = new GetItem(base.CallContext, request);
			getItem.PreExecute();
			ServiceResult<ItemType[]> serviceResult = getItem.Execute();
			getItem.SetCurrentStepResult(serviceResult);
			getItem.PostExecute();
			this.Logger.LogInfo("GetMessage() took {0} seconds.", new object[]
			{
				stopwatch.Elapsed.TotalSeconds
			});
			return serviceResult.Value[0] as MessageType;
		}

		// Token: 0x06001BA6 RID: 7078 RVA: 0x0006A068 File Offset: 0x00068268
		private ICredentials GetCredentials()
		{
			ADUser accessingADUser = this.callContext.AccessingADUser;
			return OAuthCredentials.GetOAuthCredentialsForAppActAsToken(accessingADUser.OrganizationId, accessingADUser, null);
		}

		// Token: 0x06001BA7 RID: 7079 RVA: 0x0006A090 File Offset: 0x00068290
		private string GetSharePointUrlFromAAD()
		{
			ADUser accessingADUser = this.callContext.AccessingADUser;
			Uri rootSiteUrl = SharePointUrl.GetRootSiteUrl(accessingADUser.OrganizationId);
			if (rootSiteUrl != null)
			{
				string text = rootSiteUrl.ToString();
				this.Logger.LogInfo("Found SharePoint Url in AAD: {0}", new object[]
				{
					text
				});
				return text;
			}
			this.Logger.LogInfo("No SharePoint Url in AAD.", new object[0]);
			return "https://msft.spoppe.com";
		}

		// Token: 0x06001BA8 RID: 7080 RVA: 0x0006A100 File Offset: 0x00068300
		internal ADRecipient GetADRecipient(EmailAddressWrapper recipient)
		{
			ADRecipient result = null;
			if (recipient.RoutingType.Equals("SMTP", StringComparison.OrdinalIgnoreCase))
			{
				Directory.TryFindRecipient(recipient.EmailAddress, this.ADRecipientSession, out result);
			}
			else if (recipient.RoutingType.Equals("EX", StringComparison.OrdinalIgnoreCase))
			{
				result = this.ADRecipientSession.FindByLegacyExchangeDN(recipient.EmailAddress);
			}
			return result;
		}

		// Token: 0x06001BA9 RID: 7081 RVA: 0x0006A15E File Offset: 0x0006835E
		internal bool IsOpenDL(ADGroup group)
		{
			return group != null && group.MemberJoinRestriction == MemberUpdateType.Open;
		}

		// Token: 0x06001BAA RID: 7082 RVA: 0x0006A170 File Offset: 0x00068370
		internal bool IsPublicGroup(ADUser user)
		{
			GroupMailbox groupMailbox = GroupMailbox.FromDataObject(user);
			return groupMailbox.ModernGroupType == ModernGroupObjectType.Public;
		}

		// Token: 0x06001BAB RID: 7083 RVA: 0x0006A190 File Offset: 0x00068390
		internal List<string> GetOpenDLs(EmailAddressWrapper[] recipients)
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			int num = recipients.Length;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			List<string> list = new List<string>();
			foreach (EmailAddressWrapper emailAddressWrapper in recipients)
			{
				if (emailAddressWrapper.MailboxType == MailboxHelper.MailboxTypeType.PublicDL.ToString())
				{
					num2++;
					ADRecipient adrecipient = this.GetADRecipient(emailAddressWrapper);
					if (this.IsOpenDL(adrecipient as ADGroup))
					{
						num3++;
						list.Add(emailAddressWrapper.EmailAddress);
					}
				}
				else if (emailAddressWrapper.MailboxType == MailboxHelper.MailboxTypeType.GroupMailbox.ToString())
				{
					num4++;
					ADRecipient adrecipient2 = this.GetADRecipient(emailAddressWrapper);
					if (this.IsPublicGroup(adrecipient2 as ADUser))
					{
						num5++;
						list.Add(emailAddressWrapper.EmailAddress);
					}
				}
			}
			this.Logger.LogInfo("GetOpenDLs() took {0} seconds.", new object[]
			{
				stopwatch.Elapsed.TotalSeconds
			});
			this.Logger.LogInfo("Total recipients:{0} Total DLs:{1} Total open DLs:{2} Total groups:{3} Total open groups:{4}", new object[]
			{
				num,
				num2,
				num3,
				num4,
				num5
			});
			this.linkStats.nrRecipients = num;
			this.linkStats.nrDLs = num2;
			this.linkStats.nrOpenDLs = num3;
			this.linkStats.nrGroups = num4;
			this.linkStats.nrOpenGroups = num5;
			return list;
		}

		// Token: 0x06001BAC RID: 7084 RVA: 0x0006A334 File Offset: 0x00068534
		internal bool GetRecipients(out List<string> recipients, out string sender)
		{
			recipients = null;
			sender = null;
			MessageType messageType = this.GetMailMessage();
			if (messageType == null)
			{
				this.Logger.LogWarning("Unable to get mail item.", new object[0]);
				return false;
			}
			if (messageType.From == null)
			{
				this.Logger.LogInfo("No sender found in the mail, the From field was empty.", new object[0]);
				return false;
			}
			if ((messageType.ToRecipients == null || messageType.ToRecipients.Length == 0) && (messageType.CcRecipients == null || messageType.CcRecipients.Length == 0))
			{
				this.Logger.LogInfo("No To or CC recipients found in mail.", new object[0]);
				return false;
			}
			sender = messageType.From.Mailbox.EmailAddress;
			this.linkStats.userHash = LinkClickedSignalStats.GenerateObfuscatingHash(sender);
			List<EmailAddressWrapper> list = new List<EmailAddressWrapper>(messageType.ToRecipients ?? new EmailAddressWrapper[0]);
			list.AddRange(messageType.CcRecipients ?? new EmailAddressWrapper[0]);
			recipients = this.GetOpenDLs(list.ToArray());
			if (recipients.Count == 0)
			{
				this.Logger.LogInfo("No open public DLs or unified groups found in mail.", new object[0]);
				recipients = null;
				sender = null;
				return false;
			}
			return true;
		}

		// Token: 0x06001BAD RID: 7085 RVA: 0x0006A450 File Offset: 0x00068650
		internal bool FilterUrl(string clickedUrl, string spUrl)
		{
			return clickedUrl.StartsWith(spUrl, true, CultureInfo.InvariantCulture);
		}

		// Token: 0x06001BAE RID: 7086 RVA: 0x0006A460 File Offset: 0x00068660
		protected override bool InternalExecute()
		{
			bool result;
			try
			{
				this.linkStats.linkHash = LinkClickedSignalStats.GenerateObfuscatingHash(this.url);
				string text = this.GetSharePointUrl();
				if (text == null)
				{
					this.linkStats.isSPURLValid = false;
					this.Logger.LogWarning("No valid SharePoint Url found, aborting.", new object[0]);
					result = false;
				}
				else
				{
					text = text.TrimEnd(new char[]
					{
						'/'
					});
					if (this.FilterUrl(this.url, text))
					{
						this.linkStats.isInternalLink = true;
						this.Logger.LogInfo("The clicked Url is internal, skipping the signal.", new object[0]);
						result = true;
					}
					else
					{
						List<string> recipients = null;
						string sender = null;
						if (this.GetRecipients(out recipients, out sender))
						{
							SharePointSignalRestDataProvider sharePointSignalRestDataProvider = new SharePointSignalRestDataProvider();
							sharePointSignalRestDataProvider.AddAnalyticsSignalSource(new LinkClickedSignalSource(sender, this.url, this.title, this.description, this.imgurl, this.imgdimensions, recipients));
							this.linkStats.isValidSignal = true;
						}
						result = true;
					}
				}
			}
			catch (Exception ex)
			{
				this.Logger.LogWarning("Got exception of type: {0}", new object[]
				{
					ex.GetType().ToString()
				});
				throw;
			}
			finally
			{
				this.Logger.LogInfo(this.linkStats.GetLinkClickedSignalStatsLogString(), new object[0]);
				((TraceLogger)this.Logger).Close();
				this.logger = null;
			}
			return result;
		}

		// Token: 0x04000F95 RID: 3989
		private const string EdogSharePointUrl = "https://msft.spoppe.com";

		// Token: 0x04000F96 RID: 3990
		private readonly CallContext callContext;

		// Token: 0x04000F97 RID: 3991
		private readonly ItemId itemid;

		// Token: 0x04000F98 RID: 3992
		private readonly string url;

		// Token: 0x04000F99 RID: 3993
		private readonly string title;

		// Token: 0x04000F9A RID: 3994
		private readonly string description;

		// Token: 0x04000F9B RID: 3995
		private readonly string imgurl;

		// Token: 0x04000F9C RID: 3996
		private readonly string imgdimensions;

		// Token: 0x04000F9D RID: 3997
		private IRecipientSession adRecipientSession;

		// Token: 0x04000F9E RID: 3998
		private ILogger logger;

		// Token: 0x04000F9F RID: 3999
		private LinkClickedSignalStats linkStats;

		// Token: 0x04000FA0 RID: 4000
		private Func<MessageType> getmailmessage;

		// Token: 0x04000FA1 RID: 4001
		private Func<string> getsharepointurl;

		// Token: 0x04000FA2 RID: 4002
		private Func<ICredentials> getusercredentials;
	}
}
