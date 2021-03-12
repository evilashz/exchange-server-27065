using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000CB9 RID: 3257
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal abstract class ReplyAllParticipantsRepresentationProperty<T> : SmartPropertyDefinition
	{
		// Token: 0x06007154 RID: 29012 RVA: 0x001F6CAC File Offset: 0x001F4EAC
		internal ReplyAllParticipantsRepresentationProperty(string displayName) : base(displayName, typeof(IDictionary<RecipientItemType, IEnumerable<T>>), PropertyFlags.ReadOnly, PropertyDefinitionConstraint.None, ReplyAllParticipantsRepresentationProperty<T>.PropertyDependencies)
		{
		}

		// Token: 0x06007155 RID: 29013 RVA: 0x001F6CCC File Offset: 0x001F4ECC
		public static IDictionary<RecipientItemType, HashSet<RepType>> BuildReplyAllRecipients<RepType>(RepType sender, RepType from, IList<RepType> replyTo, IDictionary<RecipientItemType, HashSet<RepType>> recipients, IEqualityComparer<RepType> participantRepresentationComparer)
		{
			IDictionary<RecipientItemType, HashSet<RepType>> dictionary = ReplyAllParticipantsRepresentationProperty<T>.BuildToAndCCRecipients<RepType>(recipients, participantRepresentationComparer);
			if (replyTo == null || replyTo.Count == 0)
			{
				if (from != null)
				{
					dictionary[RecipientItemType.To].Add(from);
				}
				else if (sender != null)
				{
					dictionary[RecipientItemType.To].Add(sender);
				}
			}
			else
			{
				foreach (RepType item in replyTo)
				{
					dictionary[RecipientItemType.To].Add(item);
				}
			}
			dictionary[RecipientItemType.Cc].ExceptWith(dictionary[RecipientItemType.To]);
			return dictionary;
		}

		// Token: 0x06007156 RID: 29014 RVA: 0x001F6D74 File Offset: 0x001F4F74
		public static IDictionary<RecipientItemType, HashSet<RepType>> BuildToAndCCRecipients<RepType>(IDictionary<RecipientItemType, HashSet<RepType>> recipients, IEqualityComparer<RepType> participantRepresentationComparer)
		{
			IDictionary<RecipientItemType, HashSet<RepType>> dictionary = ReplyAllParticipantsRepresentationProperty<RepType>.InstantiateResultType(participantRepresentationComparer);
			foreach (KeyValuePair<RecipientItemType, HashSet<RepType>> keyValuePair in recipients)
			{
				if (keyValuePair.Key == RecipientItemType.To || keyValuePair.Key == RecipientItemType.Cc)
				{
					foreach (RepType item in keyValuePair.Value)
					{
						dictionary[keyValuePair.Key].Add(item);
					}
				}
			}
			dictionary[RecipientItemType.Cc].ExceptWith(dictionary[RecipientItemType.To]);
			return dictionary;
		}

		// Token: 0x06007157 RID: 29015 RVA: 0x001F6E38 File Offset: 0x001F5038
		protected static IDictionary<RecipientItemType, HashSet<T>> InstantiateResultType(IEqualityComparer<T> participantRepresentationComparer)
		{
			return new Dictionary<RecipientItemType, HashSet<T>>
			{
				{
					RecipientItemType.To,
					new HashSet<T>(participantRepresentationComparer)
				},
				{
					RecipientItemType.Cc,
					new HashSet<T>(participantRepresentationComparer)
				}
			};
		}

		// Token: 0x17001E56 RID: 7766
		// (get) Token: 0x06007158 RID: 29016 RVA: 0x001F6E68 File Offset: 0x001F5068
		private static PropertyDependency[] PropertyDependencies
		{
			get
			{
				if (ReplyAllParticipantsRepresentationProperty<T>.propertyDependencies == null)
				{
					List<PropertyDependency> list = new List<PropertyDependency>();
					list.AddRange(InternalSchema.From.Dependencies);
					list.AddRange(InternalSchema.Sender.Dependencies);
					list.Add(new PropertyDependency(InternalSchema.MapiReplyToNames, PropertyDependencyType.NeedForRead));
					list.Add(new PropertyDependency(InternalSchema.DisplayToInternal, PropertyDependencyType.NeedForRead));
					list.Add(new PropertyDependency(InternalSchema.DisplayCcInternal, PropertyDependencyType.NeedForRead));
					list.Add(new PropertyDependency(InternalSchema.ItemClass, PropertyDependencyType.NeedForRead));
					ReplyAllParticipantsRepresentationProperty<T>.propertyDependencies = list.ToArray();
				}
				return ReplyAllParticipantsRepresentationProperty<T>.propertyDependencies;
			}
		}

		// Token: 0x17001E57 RID: 7767
		// (get) Token: 0x06007159 RID: 29017
		public abstract IEqualityComparer<T> ParticipantRepresentationComparer { get; }

		// Token: 0x0600715A RID: 29018 RVA: 0x001F6EF6 File Offset: 0x001F50F6
		protected IParticipant GetSimpleParticipant(SmartPropertyDefinition definition, PropertyBag.BasicPropertyStore propertyBag)
		{
			return propertyBag.GetValue(definition) as IParticipant;
		}

		// Token: 0x0600715B RID: 29019 RVA: 0x001F6F05 File Offset: 0x001F5105
		protected override void InternalSetValue(PropertyBag.BasicPropertyStore propertyBag, object value)
		{
			throw new NotSupportedException(ServerStrings.PropertyIsReadOnly(base.Name));
		}

		// Token: 0x04004EA6 RID: 20134
		private static PropertyDependency[] propertyDependencies;
	}
}
