using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Extensions;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000898 RID: 2200
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ReplyAllExtractor : IReplyAllExtractor
	{
		// Token: 0x06005235 RID: 21045 RVA: 0x001578EA File Offset: 0x00155AEA
		public ReplyAllExtractor(IMailboxSession session, IXSOFactory xsoFactory)
		{
			this.session = session;
			this.xsoFactory = xsoFactory;
		}

		// Token: 0x06005236 RID: 21046 RVA: 0x00157900 File Offset: 0x00155B00
		public bool TryRetrieveReplyAllDisplayNames(IStorePropertyBag propertyBag, out HashSet<string> displayNames)
		{
			ReplyAllParticipantsRepresentationProperty<string> replyAllDisplayNames = InternalSchema.ReplyAllDisplayNames;
			object obj = propertyBag.TryGetProperty(replyAllDisplayNames);
			displayNames = null;
			if (PropertyError.IsPropertyError(obj))
			{
				return false;
			}
			displayNames = this.RetrieveReplyAllData((IDictionary<RecipientItemType, HashSet<string>>)obj, replyAllDisplayNames.ParticipantRepresentationComparer);
			return true;
		}

		// Token: 0x06005237 RID: 21047 RVA: 0x00157940 File Offset: 0x00155B40
		public HashSet<string> RetrieveReplyAllDisplayNames(ICorePropertyBag propertyBag)
		{
			ReplyAllDisplayNamesProperty replyAllDisplayNames = InternalSchema.ReplyAllDisplayNames;
			object obj = propertyBag.TryGetProperty(replyAllDisplayNames);
			return this.RetrieveReplyAllData((IDictionary<RecipientItemType, HashSet<string>>)obj, replyAllDisplayNames.ParticipantRepresentationComparer);
		}

		// Token: 0x06005238 RID: 21048 RVA: 0x00157970 File Offset: 0x00155B70
		public ParticipantSet RetrieveReplyAllParticipants(ICorePropertyBag propertyBag)
		{
			ReplyAllParticipantsProperty replyAllParticipants = InternalSchema.ReplyAllParticipants;
			object obj = propertyBag.TryGetProperty(replyAllParticipants);
			return this.RetrieveReplyAllData((IDictionary<RecipientItemType, HashSet<IParticipant>>)obj);
		}

		// Token: 0x06005239 RID: 21049 RVA: 0x00157998 File Offset: 0x00155B98
		public ParticipantSet RetrieveReplyAllParticipants(IStorePropertyBag propertyBag)
		{
			ParticipantSet result;
			using (IItem item = this.xsoFactory.BindToItem(this.session, (VersionedId)propertyBag.TryGetProperty(ItemSchema.Id), new PropertyDefinition[0]))
			{
				ReplyAllParticipantsProperty replyAllParticipants = InternalSchema.ReplyAllParticipants;
				object obj = item.TryGetProperty(replyAllParticipants);
				result = this.RetrieveReplyAllData((IDictionary<RecipientItemType, HashSet<IParticipant>>)obj);
			}
			return result;
		}

		// Token: 0x0600523A RID: 21050 RVA: 0x00157A08 File Offset: 0x00155C08
		private ParticipantSet RetrieveReplyAllData(IDictionary<RecipientItemType, HashSet<IParticipant>> replyAllTable)
		{
			ParticipantSet participantSet = new ParticipantSet();
			foreach (KeyValuePair<RecipientItemType, HashSet<IParticipant>> keyValuePair in replyAllTable)
			{
				participantSet.UnionWith(keyValuePair.Value);
			}
			return participantSet;
		}

		// Token: 0x0600523B RID: 21051 RVA: 0x00157A60 File Offset: 0x00155C60
		private HashSet<string> RetrieveReplyAllData(IDictionary<RecipientItemType, HashSet<string>> replyAllTable, IEqualityComparer<string> comparer)
		{
			HashSet<string> hashSet = new HashSet<string>(comparer);
			foreach (KeyValuePair<RecipientItemType, HashSet<string>> keyValuePair in replyAllTable)
			{
				hashSet.AddRange(keyValuePair.Value);
			}
			return hashSet;
		}

		// Token: 0x04002CCC RID: 11468
		private readonly IMailboxSession session;

		// Token: 0x04002CCD RID: 11469
		private readonly IXSOFactory xsoFactory;
	}
}
