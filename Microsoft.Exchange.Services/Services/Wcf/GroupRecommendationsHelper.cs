using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Inference.GroupingModel;
using Microsoft.Exchange.Data.Storage.StoreConfigurableType;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000900 RID: 2304
	internal static class GroupRecommendationsHelper
	{
		// Token: 0x060042EE RID: 17134 RVA: 0x000DF16C File Offset: 0x000DD36C
		internal static IReadOnlyList<IRecommendedGroupInfo> GetRecommendedGroupInfos(MailboxSession mailboxSession, Action<string> traceMessageDelegate, Action<Exception> traceExceptionDelegate)
		{
			ArgumentValidator.ThrowIfNull("mailboxSession", mailboxSession);
			ArgumentValidator.ThrowIfNull("traceMessageDelegate", traceMessageDelegate);
			ArgumentValidator.ThrowIfNull("traceExceptionDelegate", traceExceptionDelegate);
			IReadOnlyList<IRecommendedGroupInfo> result = null;
			if (GroupRecommendationsHelper.AreRecommendationsAvailable(mailboxSession, traceExceptionDelegate))
			{
				RecommendedGroupsAccessorFactory recommendedGroupsAccessorFactory = new RecommendedGroupsAccessorFactory();
				IRecommendedGroupsGetter readOnlyAccessor = recommendedGroupsAccessorFactory.GetReadOnlyAccessor();
				result = readOnlyAccessor.GetRecommendedGroups(mailboxSession, traceMessageDelegate, traceExceptionDelegate);
			}
			return result;
		}

		// Token: 0x060042EF RID: 17135 RVA: 0x000DF1C8 File Offset: 0x000DD3C8
		internal static IRecommendedGroupInfo GetLatentGroupRecommendation(MailboxSession mailboxSession, SmtpAddress smtpAddress, Action<string> traceMessageDelegate, Action<Exception> traceExceptionDelegate)
		{
			ArgumentValidator.ThrowIfNull("mailboxSession", mailboxSession);
			ArgumentValidator.ThrowIfNull("smtpAddress", smtpAddress);
			ArgumentValidator.ThrowIfInvalidValue<SmtpAddress>("smtpAddress", smtpAddress, (SmtpAddress addr) => addr.IsValidAddress);
			ArgumentValidator.ThrowIfNull("traceMessageDelegate", traceMessageDelegate);
			ArgumentValidator.ThrowIfNull("traceExceptionDelegate", traceExceptionDelegate);
			IRecommendedGroupInfo result = null;
			string local = smtpAddress.Local;
			Guid a;
			if (Guid.TryParse(local, out a))
			{
				IReadOnlyList<IRecommendedGroupInfo> recommendedGroupInfos = GroupRecommendationsHelper.GetRecommendedGroupInfos(mailboxSession, traceMessageDelegate, traceExceptionDelegate);
				foreach (IRecommendedGroupInfo recommendedGroupInfo in recommendedGroupInfos)
				{
					if (a == recommendedGroupInfo.ID)
					{
						result = recommendedGroupInfo;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x060042F0 RID: 17136 RVA: 0x000DF2A8 File Offset: 0x000DD4A8
		internal static ModernGroupType ConvertLatentGroupRecommendationToModernGroupType(IRecommendedGroupInfo recommendedGroupInfo, SmtpAddress smtpAddress)
		{
			ArgumentValidator.ThrowIfNull("recommendedGroupInfo", recommendedGroupInfo);
			ArgumentValidator.ThrowIfNull("smtpAddress", smtpAddress);
			ArgumentValidator.ThrowIfInvalidValue<SmtpAddress>("smtpAddress", smtpAddress, (SmtpAddress addr) => addr.IsValidAddress);
			return new ModernGroupType
			{
				SmtpAddress = GroupRecommendationsHelper.CreateOneOffGroupSmtpAddress(recommendedGroupInfo, smtpAddress.Domain),
				DisplayName = (recommendedGroupInfo.Words[0] ?? recommendedGroupInfo.ID.ToString()),
				IsPinned = false
			};
		}

		// Token: 0x060042F1 RID: 17137 RVA: 0x000DF34C File Offset: 0x000DD54C
		internal static GetModernGroupResponse ConvertLatentGroupRecommendationToModernGroupResponse(IRecommendedGroupInfo recommendedGroupInfo, SmtpAddress smtpAddress)
		{
			ArgumentValidator.ThrowIfNull("recommendedGroupInfo", recommendedGroupInfo);
			ArgumentValidator.ThrowIfInvalidValue<SmtpAddress>("smtpAddress", smtpAddress, (SmtpAddress addr) => addr.IsValidAddress);
			GetModernGroupResponse getModernGroupResponse = new GetModernGroupResponse();
			getModernGroupResponse.GeneralInfo = new ModernGroupGeneralInfoResponse
			{
				Description = string.Join(", ", recommendedGroupInfo.Words),
				IsMember = false,
				IsOwner = false,
				ModernGroupType = ModernGroupObjectType.None,
				Name = recommendedGroupInfo.Words[0],
				SmtpAddress = GroupRecommendationsHelper.CreateOneOffGroupSmtpAddress(recommendedGroupInfo, smtpAddress.Domain)
			};
			List<ModernGroupMemberType> list = new List<ModernGroupMemberType>(recommendedGroupInfo.Members.Count);
			for (int i = 0; i < recommendedGroupInfo.Members.Count; i++)
			{
				list.Add(new ModernGroupMemberType
				{
					Persona = GroupRecommendationsHelper.ConvertRecommendationMemberToPersona(recommendedGroupInfo.Members[i], null),
					IsOwner = false
				});
			}
			getModernGroupResponse.MembersInfo = new ModernGroupMembersResponse
			{
				Members = list.ToArray(),
				Count = list.Count
			};
			return getModernGroupResponse;
		}

		// Token: 0x060042F2 RID: 17138 RVA: 0x000DF474 File Offset: 0x000DD674
		private static Persona ConvertRecommendationMemberToPersona(string displayName, string smtpAddress)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("displayName", displayName);
			bool flag = string.IsNullOrEmpty(smtpAddress);
			return new Persona
			{
				DisplayName = displayName,
				EmailAddress = new EmailAddressWrapper
				{
					Name = displayName,
					RoutingType = (flag ? null : "SMTP"),
					MailboxType = (flag ? MailboxHelper.MailboxTypeType.OneOff.ToString() : MailboxHelper.MailboxTypeType.Contact.ToString())
				}
			};
		}

		// Token: 0x060042F3 RID: 17139 RVA: 0x000DF4E8 File Offset: 0x000DD6E8
		private static string CreateOneOffGroupSmtpAddress(IRecommendedGroupInfo info, string smtpDomain)
		{
			SmtpAddress smtpAddress = new SmtpAddress(info.ID.ToString(), smtpDomain);
			return smtpAddress.ToString();
		}

		// Token: 0x060042F4 RID: 17140 RVA: 0x000DF520 File Offset: 0x000DD720
		private static bool AreRecommendationsAvailable(MailboxSession mailboxSession, Action<Exception> traceExceptionDelegate)
		{
			bool result = false;
			try
			{
				using (UserConfiguration folderConfiguration = UserConfigurationHelper.GetFolderConfiguration(mailboxSession, mailboxSession.GetDefaultFolderId(DefaultFolderType.Inbox), GroupRecommendationsHelper.InferenceSettingsConfigurationName, UserConfigurationTypes.Dictionary, false, false))
				{
					if (folderConfiguration != null)
					{
						IDictionary dictionary = folderConfiguration.GetDictionary();
						if (dictionary != null && dictionary.Contains(GroupRecommendationsHelper.GroupsRecommendationReadyName))
						{
							return (bool)dictionary[GroupRecommendationsHelper.GroupsRecommendationReadyName];
						}
					}
				}
			}
			catch (Exception obj)
			{
				traceExceptionDelegate(obj);
				result = false;
			}
			return result;
		}

		// Token: 0x04002705 RID: 9989
		internal const int RecommendationsToReturn = 3;

		// Token: 0x04002706 RID: 9990
		internal static string InferenceSettingsConfigurationName = "Inference.Settings";

		// Token: 0x04002707 RID: 9991
		internal static string GroupsRecommendationReadyName = "IsInferenceGroupsRecommendationReady";
	}
}
