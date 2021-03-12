using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000114 RID: 276
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class RecipientCollector : BaseObject
	{
		// Token: 0x0600058B RID: 1419 RVA: 0x000104E0 File Offset: 0x0000E6E0
		internal RecipientCollector(int maxSize, PropertyTag[] extraPropertyTags, RecipientSerializationFlags recipientSerializationFlags)
		{
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				disposeGuard.Add<RecipientCollector>(this);
				if (maxSize < 0)
				{
					throw new BufferTooSmallException();
				}
				this.maxSize = maxSize;
				this.recipientRows = new List<RecipientRow>();
				this.extraPropertyTags = extraPropertyTags;
				this.recipientSerializationFlags = recipientSerializationFlags;
				disposeGuard.Success();
			}
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x00010560 File Offset: 0x0000E760
		internal RecipientCollector(int maxSize, PropertyTag[] extraPropertyTags, RecipientSerializationFlags recipientSerializationFlags, RecipientRow[] recipientRows) : this(maxSize, extraPropertyTags, recipientSerializationFlags)
		{
			this.recipientRows = new List<RecipientRow>(recipientRows);
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600058D RID: 1421 RVA: 0x00010578 File Offset: 0x0000E778
		internal PropertyTag[] ExtraPropertyTags
		{
			get
			{
				return this.extraPropertyTags;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600058E RID: 1422 RVA: 0x00010580 File Offset: 0x0000E780
		internal RecipientRow[] RecipientRows
		{
			get
			{
				return this.recipientRows.ToArray();
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x0600058F RID: 1423 RVA: 0x0001058D File Offset: 0x0000E78D
		internal RecipientSerializationFlags RecipientSerializationFlags
		{
			get
			{
				return this.recipientSerializationFlags;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000590 RID: 1424 RVA: 0x00010595 File Offset: 0x0000E795
		internal bool IsEmpty
		{
			get
			{
				return this.recipientRows.Count == 0;
			}
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x000105A8 File Offset: 0x0000E7A8
		public bool TryAddRecipientRow(RecipientRow row)
		{
			if (row.ExtraPropertyValues.Length > this.extraPropertyTags.Length)
			{
				string message = string.Format("Expecting at most {0} extra properties. Instead found {1}.", this.extraPropertyTags.Length, row.ExtraPropertyValues.Length);
				throw new ArgumentException(message, "row");
			}
			for (int i = 0; i < row.ExtraPropertyValues.Length; i++)
			{
				PropertyTag propertyTag = this.extraPropertyTags[i];
				PropertyTag propertyTag2 = row.ExtraPropertyValues[i].PropertyTag;
				if (propertyTag2 != propertyTag && propertyTag2.PropertyType != PropertyType.Error)
				{
					string message2 = string.Format("Expecting to find {0} for extra property {1}. Instead found {2}.", propertyTag, i, propertyTag2);
					throw new ArgumentException(message2, "row");
				}
			}
			if (this.recipientRows.Count == 255)
			{
				return false;
			}
			row.Serialize(this.writer, this.extraPropertyTags, this.recipientSerializationFlags);
			if (this.writer.Position > (long)this.maxSize)
			{
				return false;
			}
			this.recipientRows.Add(row);
			return true;
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x000106BD File Offset: 0x0000E8BD
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<RecipientCollector>(this);
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x000106C5 File Offset: 0x0000E8C5
		protected override void InternalDispose()
		{
			if (this.writer != null)
			{
				this.writer.Dispose();
				this.writer = null;
			}
			base.InternalDispose();
		}

		// Token: 0x04000314 RID: 788
		private readonly int maxSize;

		// Token: 0x04000315 RID: 789
		private PropertyTag[] extraPropertyTags;

		// Token: 0x04000316 RID: 790
		private RecipientSerializationFlags recipientSerializationFlags;

		// Token: 0x04000317 RID: 791
		private List<RecipientRow> recipientRows;

		// Token: 0x04000318 RID: 792
		private CountWriter writer = new CountWriter();
	}
}
