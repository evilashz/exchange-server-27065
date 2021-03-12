using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x0200020A RID: 522
	internal class PreviewItem : Dictionary<PropertyDefinition, object>, IComparable
	{
		// Token: 0x06000E03 RID: 3587 RVA: 0x0003D706 File Offset: 0x0003B906
		public PreviewItem(Dictionary<PropertyDefinition, object> properties, Guid mailboxGuid, Uri owaLink, ReferenceItem sortValue, UniqueItemHash itemHash) : this(properties, mailboxGuid, owaLink, sortValue, itemHash, null)
		{
		}

		// Token: 0x06000E04 RID: 3588 RVA: 0x0003D718 File Offset: 0x0003B918
		public PreviewItem(Dictionary<PropertyDefinition, object> properties, Guid mailboxGuid, Uri owaLink, ReferenceItem sortValue, UniqueItemHash itemHash, List<PropertyDefinition> additionalProperties) : base(properties)
		{
			Util.ThrowOnNull(sortValue, "sortValue");
			if (mailboxGuid == Guid.Empty)
			{
				throw new ArgumentException("Invalid mailbox guid");
			}
			this.owaLink = owaLink;
			this.sortValue = sortValue;
			this.mailboxGuid = mailboxGuid;
			this.itemHash = itemHash;
			this.additionalPropertyValues = Util.GetSelectedAdditionalPropertyValues(properties, additionalProperties);
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06000E05 RID: 3589 RVA: 0x0003D77C File Offset: 0x0003B97C
		public StoreId Id
		{
			get
			{
				return this.GetProperty<StoreId>(ItemSchema.Id, null);
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06000E06 RID: 3590 RVA: 0x0003D78A File Offset: 0x0003B98A
		public StoreId ParentItemId
		{
			get
			{
				return this.GetProperty<StoreId>(StoreObjectSchema.ParentItemId, null);
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000E07 RID: 3591 RVA: 0x0003D798 File Offset: 0x0003B998
		public string ItemClass
		{
			get
			{
				return this.GetProperty<string>(StoreObjectSchema.ItemClass, string.Empty);
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000E08 RID: 3592 RVA: 0x0003D7AA File Offset: 0x0003B9AA
		public UniqueItemHash ItemHash
		{
			get
			{
				return this.itemHash;
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06000E09 RID: 3593 RVA: 0x0003D7B2 File Offset: 0x0003B9B2
		public string InternetMessageId
		{
			get
			{
				return this.GetProperty<string>(ItemSchema.InternetMessageId, string.Empty);
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06000E0A RID: 3594 RVA: 0x0003D7C4 File Offset: 0x0003B9C4
		public Guid MailboxGuid
		{
			get
			{
				return this.mailboxGuid;
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06000E0B RID: 3595 RVA: 0x0003D7CC File Offset: 0x0003B9CC
		public Uri OwaLink
		{
			get
			{
				return this.owaLink;
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06000E0C RID: 3596 RVA: 0x0003D7D4 File Offset: 0x0003B9D4
		public string Sender
		{
			get
			{
				return this.GetProperty<string>(MessageItemSchema.SenderDisplayName, string.Empty);
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06000E0D RID: 3597 RVA: 0x0003D7E6 File Offset: 0x0003B9E6
		public string[] ToRecipients
		{
			get
			{
				return this.GetStringArrayProperty(ItemSchema.DisplayTo);
			}
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06000E0E RID: 3598 RVA: 0x0003D7F3 File Offset: 0x0003B9F3
		public string[] CcRecipients
		{
			get
			{
				return this.GetStringArrayProperty(ItemSchema.DisplayCc);
			}
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06000E0F RID: 3599 RVA: 0x0003D800 File Offset: 0x0003BA00
		public string[] BccRecipients
		{
			get
			{
				return this.GetStringArrayProperty(ItemSchema.DisplayBcc);
			}
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06000E10 RID: 3600 RVA: 0x0003D80D File Offset: 0x0003BA0D
		public ExDateTime CreationTime
		{
			get
			{
				return this.GetProperty<ExDateTime>(StoreObjectSchema.CreationTime, ExDateTime.MinValue);
			}
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06000E11 RID: 3601 RVA: 0x0003D81F File Offset: 0x0003BA1F
		public ExDateTime ReceivedTime
		{
			get
			{
				return this.GetProperty<ExDateTime>(ItemSchema.ReceivedTime, ExDateTime.MinValue);
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06000E12 RID: 3602 RVA: 0x0003D831 File Offset: 0x0003BA31
		public ExDateTime SentTime
		{
			get
			{
				return this.GetProperty<ExDateTime>(ItemSchema.SentTime, ExDateTime.MinValue);
			}
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06000E13 RID: 3603 RVA: 0x0003D843 File Offset: 0x0003BA43
		public string Subject
		{
			get
			{
				return this.GetProperty<string>(ItemSchema.Subject, null);
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06000E14 RID: 3604 RVA: 0x0003D851 File Offset: 0x0003BA51
		public int Size
		{
			get
			{
				return this.GetProperty<int>(ItemSchema.Size, 0);
			}
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06000E15 RID: 3605 RVA: 0x0003D85F File Offset: 0x0003BA5F
		public string Preview
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06000E16 RID: 3606 RVA: 0x0003D866 File Offset: 0x0003BA66
		public string Importance
		{
			get
			{
				return this.GetProperty<string>(ItemSchema.Importance, string.Empty);
			}
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06000E17 RID: 3607 RVA: 0x0003D878 File Offset: 0x0003BA78
		public bool Read
		{
			get
			{
				return this.GetProperty<bool>(MessageItemSchema.IsRead, false);
			}
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06000E18 RID: 3608 RVA: 0x0003D886 File Offset: 0x0003BA86
		public bool HasAttachment
		{
			get
			{
				return this.GetProperty<bool>(ItemSchema.HasAttachment, false);
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06000E19 RID: 3609 RVA: 0x0003D894 File Offset: 0x0003BA94
		public ReferenceItem SortValue
		{
			get
			{
				return this.sortValue;
			}
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06000E1A RID: 3610 RVA: 0x0003D89C File Offset: 0x0003BA9C
		public Dictionary<PropertyDefinition, object> AdditionalPropertyValues
		{
			get
			{
				return this.additionalPropertyValues;
			}
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06000E1B RID: 3611 RVA: 0x0003D8A4 File Offset: 0x0003BAA4
		// (set) Token: 0x06000E1C RID: 3612 RVA: 0x0003D8AC File Offset: 0x0003BAAC
		public MailboxInfo MailboxInfo { get; set; }

		// Token: 0x06000E1D RID: 3613 RVA: 0x0003D8B8 File Offset: 0x0003BAB8
		public int CompareTo(object obj)
		{
			if (obj == null)
			{
				return 1;
			}
			PreviewItem previewItem = obj as PreviewItem;
			if (previewItem != null)
			{
				return this.SortValue.CompareTo(previewItem.SortValue);
			}
			throw new ArgumentException("Object is not a PreviewItem");
		}

		// Token: 0x06000E1E RID: 3614 RVA: 0x0003D8FB File Offset: 0x0003BAFB
		private static string[] SplitRecipients(string recipient)
		{
			return (from addr in recipient.Split(PreviewItem.RecipientSeparators, StringSplitOptions.RemoveEmptyEntries)
			where !string.IsNullOrWhiteSpace(addr)
			select addr).ToArray<string>();
		}

		// Token: 0x06000E1F RID: 3615 RVA: 0x0003D930 File Offset: 0x0003BB30
		private T GetProperty<T>(StorePropertyDefinition propertyDef, T defaultValue)
		{
			if (base.ContainsKey(propertyDef) && base[propertyDef] is T)
			{
				return (T)((object)base[propertyDef]);
			}
			return defaultValue;
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x0003D958 File Offset: 0x0003BB58
		private string[] GetStringArrayProperty(StorePropertyDefinition propertyDef)
		{
			string property = this.GetProperty<string>(propertyDef, string.Empty);
			if (!string.IsNullOrEmpty(property))
			{
				return PreviewItem.SplitRecipients(property);
			}
			return null;
		}

		// Token: 0x040009A9 RID: 2473
		private readonly Uri owaLink;

		// Token: 0x040009AA RID: 2474
		private readonly ReferenceItem sortValue;

		// Token: 0x040009AB RID: 2475
		private readonly Guid mailboxGuid;

		// Token: 0x040009AC RID: 2476
		private readonly UniqueItemHash itemHash;

		// Token: 0x040009AD RID: 2477
		private readonly Dictionary<PropertyDefinition, object> additionalPropertyValues;

		// Token: 0x040009AE RID: 2478
		private static readonly string[] RecipientSeparators = new string[]
		{
			";",
			"; "
		};
	}
}
