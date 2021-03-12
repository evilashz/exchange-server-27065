using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage.StoreConfigurableType;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Inference.GroupingModel
{
	// Token: 0x02000F5E RID: 3934
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class RecommendedGroupsAccessor : IRecommendedGroupsAccessor, IRecommendedGroupsGetter
	{
		// Token: 0x060086B6 RID: 34486 RVA: 0x0024F01E File Offset: 0x0024D21E
		public RecommendedGroupsAccessor()
		{
			this.modelVersionSelector = new GroupingModelVersionSelector(RecommendedGroupsAccessor.HookableGroupingModelConfiguration.Value);
		}

		// Token: 0x170023B1 RID: 9137
		// (get) Token: 0x060086B7 RID: 34487 RVA: 0x0024F03B File Offset: 0x0024D23B
		public static Hookable<IGroupingModelConfiguration> HookableGroupingModelConfiguration
		{
			get
			{
				return RecommendedGroupsAccessor.hookableGroupingModelConfiguration;
			}
		}

		// Token: 0x060086B8 RID: 34488 RVA: 0x0024F044 File Offset: 0x0024D244
		public RecommendedGroupsInfo ReadItem(Stream stream)
		{
			if (stream.Length <= 0L)
			{
				return null;
			}
			BinaryReader reader = new BinaryReader(stream);
			RecommendedGroupsInfo recommendedGroupsInfo = new RecommendedGroupsInfo();
			recommendedGroupsInfo.Read(reader);
			return recommendedGroupsInfo;
		}

		// Token: 0x060086B9 RID: 34489 RVA: 0x0024F074 File Offset: 0x0024D274
		public void WriteItem(Stream stream, RecommendedGroupsInfo item)
		{
			BinaryWriter writer = new BinaryWriter(stream);
			item.Write(writer);
			stream.SetLength(stream.Position);
		}

		// Token: 0x060086BA RID: 34490 RVA: 0x0024F09C File Offset: 0x0024D29C
		public IReadOnlyList<IRecommendedGroupInfo> GetRecommendedGroups(MailboxSession session, Action<string> traceDelegate, Action<Exception> traceErrorDelegate)
		{
			ArgumentValidator.ThrowIfNull("session", session);
			ArgumentValidator.ThrowIfNull("traceDelegate", traceDelegate);
			ArgumentValidator.ThrowIfNull("traceErrorDelegate", traceErrorDelegate);
			IReadOnlyList<IRecommendedGroupInfo> result = null;
			Exception ex = null;
			string text = string.Format("{0}.{1}", "Inference.RecommendedGroups", this.modelVersionSelector.GetModelVersionToAccessRecommendedGroups());
			try
			{
				traceDelegate(string.Format("Loading recommended groups from {0}", text));
				using (UserConfiguration folderConfiguration = UserConfigurationHelper.GetFolderConfiguration(session, session.GetDefaultFolderId(DefaultFolderType.Inbox), text, UserConfigurationTypes.Stream, false, false))
				{
					if (folderConfiguration != null)
					{
						using (Stream stream = folderConfiguration.GetStream())
						{
							RecommendedGroupsInfo recommendedGroupsInfo = this.ReadItem(stream);
							if (recommendedGroupsInfo != null)
							{
								result = recommendedGroupsInfo.RecommendedGroups;
							}
						}
					}
				}
			}
			catch (ObjectNotFoundException ex2)
			{
				ex = ex2;
			}
			catch (CorruptDataException ex3)
			{
				ex = ex3;
			}
			catch (SerializationException ex4)
			{
				ex = ex4;
			}
			if (ex != null)
			{
				traceErrorDelegate(ex);
			}
			return result;
		}

		// Token: 0x060086BB RID: 34491 RVA: 0x0024F1B0 File Offset: 0x0024D3B0
		public void SetRecommendedGroups(MailboxSession session, RecommendedGroupsInfo groupsInfo, int version, Action<string> traceDelegate, Action<Exception> traceErrorDelegate)
		{
			ArgumentValidator.ThrowIfNull("session", session);
			ArgumentValidator.ThrowIfNull("traceDelegate", traceDelegate);
			ArgumentValidator.ThrowIfNull("traceErrorDelegate", traceErrorDelegate);
			string text = string.Format("{0}.{1}", "Inference.RecommendedGroups", version);
			try
			{
				traceDelegate(string.Format("Writing out recommended groups to {0}", text));
				using (UserConfiguration folderConfiguration = UserConfigurationHelper.GetFolderConfiguration(session, session.GetDefaultFolderId(DefaultFolderType.Inbox), text, UserConfigurationTypes.Stream, true, false))
				{
					if (folderConfiguration != null)
					{
						using (Stream stream = folderConfiguration.GetStream())
						{
							this.WriteItem(stream, groupsInfo);
							folderConfiguration.Save();
						}
					}
				}
			}
			catch (SerializationException obj)
			{
				traceErrorDelegate(obj);
			}
		}

		// Token: 0x04005A1C RID: 23068
		private static readonly Hookable<IGroupingModelConfiguration> hookableGroupingModelConfiguration = Hookable<IGroupingModelConfiguration>.Create(true, GroupingModelConfiguration.LoadFromFile().AsReadOnly());

		// Token: 0x04005A1D RID: 23069
		private readonly GroupingModelVersionSelector modelVersionSelector;
	}
}
