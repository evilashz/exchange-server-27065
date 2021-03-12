using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Inference.Ranking
{
	// Token: 0x02000006 RID: 6
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ConversationTrendingModel : IRankingModel
	{
		// Token: 0x0600000C RID: 12 RVA: 0x00002101 File Offset: 0x00000301
		public ConversationTrendingModel()
		{
			this.featuresAndWeights = ConversationTrendingModel.DefaultFeaturesAndWeights;
			this.dependencies = this.GetDependencies();
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002120 File Offset: 0x00000320
		public HashSet<PropertyDefinition> Dependencies
		{
			get
			{
				return this.dependencies;
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002128 File Offset: 0x00000328
		public double Rank(object item)
		{
			double num = 0.0;
			foreach (Tuple<ConversationFeature, double> tuple in this.featuresAndWeights)
			{
				double num2 = tuple.Item1.FeatureValue(item) * tuple.Item2;
				num += num2;
			}
			return num;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002178 File Offset: 0x00000378
		private HashSet<PropertyDefinition> GetDependencies()
		{
			HashSet<PropertyDefinition> hashSet = new HashSet<PropertyDefinition>();
			foreach (Tuple<ConversationFeature, double> tuple in this.featuresAndWeights)
			{
				foreach (PropertyDefinition propertyDefinition in tuple.Item1.SupportingProperties)
				{
					ApplicationAggregatedProperty item = (ApplicationAggregatedProperty)propertyDefinition;
					hashSet.Add(item);
				}
			}
			return hashSet;
		}

		// Token: 0x04000003 RID: 3
		private static readonly int DefaultReplyCountFeatureWeight = 1;

		// Token: 0x04000004 RID: 4
		private static readonly int DefaultTotalItemLikesFeatureWeight = 1;

		// Token: 0x04000005 RID: 5
		private static readonly int DefaultConversationLikesFeatureWeight = 1;

		// Token: 0x04000006 RID: 6
		private static readonly int DefaultDirectParticipantsFeatureWeight = 5;

		// Token: 0x04000007 RID: 7
		private static readonly int DefaultConversationModifiedInLastHourFeatureWeight = 10;

		// Token: 0x04000008 RID: 8
		private static readonly int DefaultConversationModifiedInLastDayFeatureWeight = 5;

		// Token: 0x04000009 RID: 9
		private static readonly int DefaultConversationModifiedInLastWeekFeatureWeight = 1;

		// Token: 0x0400000A RID: 10
		private static readonly ConversationFeature ReplyCountFeature = new ConversationFeature(new List<PropertyDefinition>
		{
			AggregatedConversationSchema.ItemCount
		}, (IStorePropertyBag conversation) => Math.Log((double)conversation.GetValueOrDefault<int>(AggregatedConversationSchema.ItemCount, 1), 2.0));

		// Token: 0x0400000B RID: 11
		private static readonly ConversationFeature DirectParticipantsFeature = new ConversationFeature(new List<PropertyDefinition>
		{
			AggregatedConversationSchema.DirectParticipants
		}, (IStorePropertyBag conversation) => Math.Log((double)(1 + conversation.GetValueOrDefault<Participant[]>(AggregatedConversationSchema.DirectParticipants, null).Length), 2.0));

		// Token: 0x0400000C RID: 12
		private static readonly ConversationFeature TotalItemLikesFeature = new ConversationFeature(new List<PropertyDefinition>
		{
			AggregatedConversationSchema.TotalItemLikes
		}, (IStorePropertyBag conversation) => Math.Log((double)(1 + conversation.GetValueOrDefault<int>(AggregatedConversationSchema.TotalItemLikes, 0)), 2.0));

		// Token: 0x0400000D RID: 13
		private static readonly ConversationFeature ConversationLikesFeature = new ConversationFeature(new List<PropertyDefinition>
		{
			AggregatedConversationSchema.ConversationLikes
		}, (IStorePropertyBag conversation) => Math.Log((double)(1 + conversation.GetValueOrDefault<int>(AggregatedConversationSchema.ConversationLikes, 0)), 2.0));

		// Token: 0x0400000E RID: 14
		private static readonly ConversationFeature ConversationModifiedInLastHourFeature = new ConversationFeature(new List<PropertyDefinition>
		{
			AggregatedConversationSchema.LastDeliveryTime
		}, (IStorePropertyBag conversation) => (double)((ExDateTime.Now - conversation.GetValueOrDefault<ExDateTime>(AggregatedConversationSchema.LastDeliveryTime, ExDateTime.MinValue) <= TimeSpan.FromHours(1.0)) ? 1 : 0));

		// Token: 0x0400000F RID: 15
		private static readonly ConversationFeature ConversationModifiedInLastDayFeature = new ConversationFeature(new List<PropertyDefinition>
		{
			AggregatedConversationSchema.LastDeliveryTime
		}, (IStorePropertyBag conversation) => (double)((ExDateTime.Now - conversation.GetValueOrDefault<ExDateTime>(AggregatedConversationSchema.LastDeliveryTime, ExDateTime.MinValue) <= TimeSpan.FromDays(1.0)) ? 1 : 0));

		// Token: 0x04000010 RID: 16
		private static readonly ConversationFeature ConversationModifiedInLastWeekFeature = new ConversationFeature(new List<PropertyDefinition>
		{
			AggregatedConversationSchema.LastDeliveryTime
		}, (IStorePropertyBag conversation) => (double)((ExDateTime.Now - conversation.GetValueOrDefault<ExDateTime>(AggregatedConversationSchema.LastDeliveryTime, ExDateTime.MinValue) <= TimeSpan.FromDays(7.0)) ? 1 : 0));

		// Token: 0x04000011 RID: 17
		private static readonly Tuple<ConversationFeature, double>[] DefaultFeaturesAndWeights = new Tuple<ConversationFeature, double>[]
		{
			new Tuple<ConversationFeature, double>(ConversationTrendingModel.ReplyCountFeature, (double)ConversationTrendingModel.DefaultReplyCountFeatureWeight),
			new Tuple<ConversationFeature, double>(ConversationTrendingModel.TotalItemLikesFeature, (double)ConversationTrendingModel.DefaultTotalItemLikesFeatureWeight),
			new Tuple<ConversationFeature, double>(ConversationTrendingModel.ConversationLikesFeature, (double)ConversationTrendingModel.DefaultConversationLikesFeatureWeight),
			new Tuple<ConversationFeature, double>(ConversationTrendingModel.DirectParticipantsFeature, (double)ConversationTrendingModel.DefaultDirectParticipantsFeatureWeight),
			new Tuple<ConversationFeature, double>(ConversationTrendingModel.ConversationModifiedInLastHourFeature, (double)ConversationTrendingModel.DefaultConversationModifiedInLastHourFeatureWeight),
			new Tuple<ConversationFeature, double>(ConversationTrendingModel.ConversationModifiedInLastDayFeature, (double)ConversationTrendingModel.DefaultConversationModifiedInLastDayFeatureWeight),
			new Tuple<ConversationFeature, double>(ConversationTrendingModel.ConversationModifiedInLastWeekFeature, (double)ConversationTrendingModel.DefaultConversationModifiedInLastWeekFeatureWeight)
		};

		// Token: 0x04000012 RID: 18
		private readonly Tuple<ConversationFeature, double>[] featuresAndWeights;

		// Token: 0x04000013 RID: 19
		private readonly HashSet<PropertyDefinition> dependencies;
	}
}
