using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Microsoft.Exchange.Data.TextConverters;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Conversations
{
	// Token: 0x020008E6 RID: 2278
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ItemPart
	{
		// Token: 0x0600556D RID: 21869 RVA: 0x001624C0 File Offset: 0x001606C0
		protected ItemPart(IEnumerable<PropertyDefinition> loadedProperties)
		{
			this.rawAttachments = new List<AttachmentInfo>();
			this.replyToParticipants = new List<IParticipant>();
			this.attachments = new ReadOnlyCollection<AttachmentInfo>(this.rawAttachments);
			this.recipients = new ParticipantTable();
			this.loadedProperties = new HashSet<PropertyDefinition>(loadedProperties);
		}

		// Token: 0x0600556E RID: 21870 RVA: 0x00162514 File Offset: 0x00160714
		internal ItemPart(StoreObjectId itemId, string subject, FragmentInfo uniqueBodyFragment, FragmentInfo disclaimerFragment, ParticipantTable recipients, IList<Participant> replyToParticipants, IStorePropertyBag storePropertyBag, PropertyDefinition[] loadedProperties) : this(loadedProperties)
		{
			this.itemId = itemId;
			this.subject = subject;
			this.storePropertyBag = storePropertyBag;
			this.uniqueBodyFragment = uniqueBodyFragment;
			this.disclaimerFragment = disclaimerFragment;
			this.recipients = recipients;
			this.replyToParticipants.AddRange(replyToParticipants);
		}

		// Token: 0x170017D7 RID: 6103
		// (get) Token: 0x0600556F RID: 21871 RVA: 0x00162563 File Offset: 0x00160763
		public virtual bool DidLoadSucceed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170017D8 RID: 6104
		// (get) Token: 0x06005570 RID: 21872 RVA: 0x00162566 File Offset: 0x00160766
		public StoreObjectId ObjectId
		{
			get
			{
				return ((VersionedId)this.StorePropertyBag.TryGetProperty(ItemSchema.Id)).ObjectId;
			}
		}

		// Token: 0x170017D9 RID: 6105
		// (get) Token: 0x06005571 RID: 21873 RVA: 0x00162582 File Offset: 0x00160782
		// (set) Token: 0x06005572 RID: 21874 RVA: 0x0016258A File Offset: 0x0016078A
		public string Subject
		{
			get
			{
				return this.subject;
			}
			protected set
			{
				if (this.subject != null)
				{
					throw new InvalidOperationException();
				}
				this.subject = value;
			}
		}

		// Token: 0x170017DA RID: 6106
		// (get) Token: 0x06005573 RID: 21875 RVA: 0x001625A4 File Offset: 0x001607A4
		public string BodyPart
		{
			get
			{
				if (this.bodyPart == null && this.uniqueBodyFragment != null)
				{
					using (StringWriter stringWriter = new StringWriter())
					{
						using (HtmlWriter htmlWriter = new HtmlWriter(stringWriter))
						{
							this.uniqueBodyFragment.WriteHtml(htmlWriter);
						}
						this.bodyPart = stringWriter.ToString();
					}
				}
				return this.bodyPart;
			}
		}

		// Token: 0x170017DB RID: 6107
		// (get) Token: 0x06005574 RID: 21876 RVA: 0x00162620 File Offset: 0x00160820
		// (set) Token: 0x06005575 RID: 21877 RVA: 0x00162628 File Offset: 0x00160828
		public StoreObjectId ItemId
		{
			get
			{
				return this.itemId;
			}
			protected set
			{
				if (this.itemId != null)
				{
					throw new InvalidOperationException();
				}
				this.itemId = value;
			}
		}

		// Token: 0x170017DC RID: 6108
		// (get) Token: 0x06005576 RID: 21878 RVA: 0x0016263F File Offset: 0x0016083F
		public virtual IList<IParticipant> ReplyToParticipants
		{
			get
			{
				return this.replyToParticipants;
			}
		}

		// Token: 0x170017DD RID: 6109
		// (get) Token: 0x06005577 RID: 21879 RVA: 0x00162647 File Offset: 0x00160847
		public ParticipantTable Recipients
		{
			get
			{
				return this.recipients;
			}
		}

		// Token: 0x170017DE RID: 6110
		// (get) Token: 0x06005578 RID: 21880 RVA: 0x0016264F File Offset: 0x0016084F
		public IList<AttachmentInfo> Attachments
		{
			get
			{
				return this.attachments;
			}
		}

		// Token: 0x170017DF RID: 6111
		// (get) Token: 0x06005579 RID: 21881 RVA: 0x00162657 File Offset: 0x00160857
		// (set) Token: 0x0600557A RID: 21882 RVA: 0x0016265F File Offset: 0x0016085F
		public IStorePropertyBag StorePropertyBag
		{
			get
			{
				return this.storePropertyBag;
			}
			protected set
			{
				if (this.StorePropertyBag != null && value == null)
				{
					throw new InvalidOperationException();
				}
				this.storePropertyBag = value;
			}
		}

		// Token: 0x170017E0 RID: 6112
		// (get) Token: 0x0600557B RID: 21883 RVA: 0x00162679 File Offset: 0x00160879
		public bool MayHaveHiddenDisclaimer
		{
			get
			{
				return this.DisclaimerFragmentInfo != null && !this.DisclaimerFragmentInfo.IsEmpty;
			}
		}

		// Token: 0x170017E1 RID: 6113
		// (get) Token: 0x0600557C RID: 21884 RVA: 0x00162693 File Offset: 0x00160893
		public virtual bool IsExtractedPart
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170017E2 RID: 6114
		// (get) Token: 0x0600557D RID: 21885 RVA: 0x00162696 File Offset: 0x00160896
		public ParticipantSet AllRecipients
		{
			get
			{
				if (this.allRecipients == null)
				{
					this.allRecipients = this.CalculateAllRecipients();
				}
				return this.allRecipients;
			}
		}

		// Token: 0x0600557E RID: 21886 RVA: 0x001626B4 File Offset: 0x001608B4
		private ParticipantSet CalculateAllRecipients()
		{
			ParticipantSet participantSet = new ParticipantSet();
			foreach (PropertyDefinition propertyDefinition in MessageItemSchema.SingleRecipientProperties)
			{
				if (this.loadedProperties.Contains(propertyDefinition))
				{
					Participant valueOrDefault = this.StorePropertyBag.GetValueOrDefault<Participant>(propertyDefinition, null);
					if (valueOrDefault != null)
					{
						participantSet.Add(valueOrDefault);
					}
				}
			}
			participantSet.UnionWith(this.Recipients.ToList());
			participantSet.UnionWith(this.ReplyToParticipants);
			return participantSet;
		}

		// Token: 0x170017E3 RID: 6115
		// (get) Token: 0x0600557F RID: 21887 RVA: 0x0016272F File Offset: 0x0016092F
		// (set) Token: 0x06005580 RID: 21888 RVA: 0x00162737 File Offset: 0x00160937
		internal FragmentInfo DisclaimerFragmentInfo
		{
			get
			{
				return this.disclaimerFragment;
			}
			set
			{
				if (this.disclaimerFragment != null && this.disclaimerFragment != FragmentInfo.Empty)
				{
					throw new InvalidOperationException("can't set DisclaimerFragmentInfo if it's already set");
				}
				this.disclaimerFragment = value;
			}
		}

		// Token: 0x170017E4 RID: 6116
		// (get) Token: 0x06005581 RID: 21889 RVA: 0x00162760 File Offset: 0x00160960
		// (set) Token: 0x06005582 RID: 21890 RVA: 0x00162768 File Offset: 0x00160968
		internal FragmentInfo UniqueFragmentInfo
		{
			get
			{
				return this.uniqueBodyFragment;
			}
			set
			{
				if (this.uniqueBodyFragment != null && this.uniqueBodyFragment != FragmentInfo.Empty)
				{
					throw new InvalidOperationException("can't set UniqeBodyFragment if it's already set");
				}
				this.uniqueBodyFragment = value;
			}
		}

		// Token: 0x06005583 RID: 21891 RVA: 0x00162791 File Offset: 0x00160991
		public void WriteDisclaimer(HtmlWriter writer)
		{
			this.DisclaimerFragmentInfo.WriteHtml(writer);
		}

		// Token: 0x06005584 RID: 21892 RVA: 0x0016279F File Offset: 0x0016099F
		public void WriteUniquePart(HtmlWriter writer)
		{
			this.UniqueFragmentInfo.WriteHtml(writer);
		}

		// Token: 0x06005585 RID: 21893 RVA: 0x001627AD File Offset: 0x001609AD
		public void WriteUniquePartWithoutQuotedText(HtmlWriter writer)
		{
			this.UniqueFragmentInfo.FragmentWithoutQuotedText.WriteHtml(writer);
		}

		// Token: 0x06005586 RID: 21894 RVA: 0x001627C0 File Offset: 0x001609C0
		public void WriteUniquePartQuotedText(HtmlWriter writer)
		{
			this.UniqueFragmentInfo.QuotedTextFragment.WriteHtml(writer);
		}

		// Token: 0x170017E5 RID: 6117
		// (get) Token: 0x06005587 RID: 21895 RVA: 0x001627D3 File Offset: 0x001609D3
		protected List<AttachmentInfo> RawAttachments
		{
			get
			{
				return this.rawAttachments;
			}
		}

		// Token: 0x170017E6 RID: 6118
		// (get) Token: 0x06005588 RID: 21896 RVA: 0x001627DB File Offset: 0x001609DB
		public virtual ItemPartIrmInfo IrmInfo
		{
			get
			{
				return ItemPartIrmInfo.NotRestricted;
			}
		}

		// Token: 0x04002DDE RID: 11742
		private StoreObjectId itemId;

		// Token: 0x04002DDF RID: 11743
		private string subject;

		// Token: 0x04002DE0 RID: 11744
		private string bodyPart;

		// Token: 0x04002DE1 RID: 11745
		private List<IParticipant> replyToParticipants;

		// Token: 0x04002DE2 RID: 11746
		private FragmentInfo uniqueBodyFragment;

		// Token: 0x04002DE3 RID: 11747
		private FragmentInfo disclaimerFragment;

		// Token: 0x04002DE4 RID: 11748
		private List<AttachmentInfo> rawAttachments;

		// Token: 0x04002DE5 RID: 11749
		private ReadOnlyCollection<AttachmentInfo> attachments;

		// Token: 0x04002DE6 RID: 11750
		private ParticipantTable recipients;

		// Token: 0x04002DE7 RID: 11751
		private IStorePropertyBag storePropertyBag;

		// Token: 0x04002DE8 RID: 11752
		private HashSet<PropertyDefinition> loadedProperties;

		// Token: 0x04002DE9 RID: 11753
		private ParticipantSet allRecipients;
	}
}
