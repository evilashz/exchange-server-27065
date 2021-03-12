using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000BB3 RID: 2995
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class JunkEmailCollection : Collection<string>
	{
		// Token: 0x06006B02 RID: 27394 RVA: 0x001C8470 File Offset: 0x001C6670
		private static string MakeSmtpAddressFromDomain(string smtpDomainWithLeadingAt)
		{
			return "user" + smtpDomainWithLeadingAt;
		}

		// Token: 0x06006B03 RID: 27395 RVA: 0x001C847D File Offset: 0x001C667D
		private static bool IsValidFormat(string value)
		{
			return SmtpAddress.IsValidSmtpAddress(value) || SmtpAddress.IsValidSmtpAddress(JunkEmailCollection.MakeSmtpAddressFromDomain(value));
		}

		// Token: 0x06006B04 RID: 27396 RVA: 0x001C8494 File Offset: 0x001C6694
		private JunkEmailCollection()
		{
		}

		// Token: 0x06006B05 RID: 27397 RVA: 0x001C84A3 File Offset: 0x001C66A3
		internal static JunkEmailCollection Create(JunkEmailRule junkRule, JunkEmailCollection.ListType listType)
		{
			return JunkEmailCollection.Create(junkRule, listType, null);
		}

		// Token: 0x06006B06 RID: 27398 RVA: 0x001C84B0 File Offset: 0x001C66B0
		internal static JunkEmailCollection Create(JunkEmailRule junkRule, JunkEmailCollection.ListType listType, ICollection<string> invlidEntriesCollection)
		{
			return new JunkEmailCollection
			{
				junkRule = junkRule,
				listType = listType,
				invlidEntriesCollection = invlidEntriesCollection
			};
		}

		// Token: 0x06006B07 RID: 27399 RVA: 0x001C84D9 File Offset: 0x001C66D9
		public new int Add(string value)
		{
			base.Add(value);
			return base.Count - 1;
		}

		// Token: 0x06006B08 RID: 27400 RVA: 0x001C84EC File Offset: 0x001C66EC
		public void AddRange(string[] value)
		{
			foreach (string value2 in value)
			{
				this.Add(value2);
			}
		}

		// Token: 0x06006B09 RID: 27401 RVA: 0x001C8518 File Offset: 0x001C6718
		public JunkEmailCollection.ValidationProblem TryAdd(string value)
		{
			JunkEmailCollection.ValidationProblem validationProblem = this.validating ? this.CheckValue(value) : JunkEmailCollection.ValidationProblem.NoError;
			if (validationProblem == JunkEmailCollection.ValidationProblem.NoError)
			{
				base.Add(value);
			}
			return validationProblem;
		}

		// Token: 0x06006B0A RID: 27402 RVA: 0x001C8544 File Offset: 0x001C6744
		private void InternalSort(JunkEmailCollection.SortDelegate sortDelegate)
		{
			bool flag = this.validating;
			this.validating = false;
			try
			{
				sortDelegate();
			}
			finally
			{
				this.validating = flag;
			}
		}

		// Token: 0x06006B0B RID: 27403 RVA: 0x001C8592 File Offset: 0x001C6792
		public void Sort()
		{
			this.InternalSort(delegate
			{
				((List<string>)base.Items).Sort();
			});
		}

		// Token: 0x06006B0C RID: 27404 RVA: 0x001C85CC File Offset: 0x001C67CC
		public void Sort(Comparison<string> comparison)
		{
			this.InternalSort(delegate
			{
				((List<string>)this.Items).Sort(comparison);
			});
		}

		// Token: 0x06006B0D RID: 27405 RVA: 0x001C8624 File Offset: 0x001C6824
		public void Sort(IComparer<string> comparer)
		{
			this.InternalSort(delegate
			{
				((List<string>)this.Items).Sort(comparer);
			});
		}

		// Token: 0x06006B0E RID: 27406 RVA: 0x001C8688 File Offset: 0x001C6888
		public void Sort(int index, int count, IComparer<string> comparer)
		{
			this.InternalSort(delegate
			{
				((List<string>)this.Items).Sort(index, count, comparer);
			});
		}

		// Token: 0x17001D1F RID: 7455
		// (get) Token: 0x06006B0F RID: 27407 RVA: 0x001C86C9 File Offset: 0x001C68C9
		public int MaxNumberOfEntries
		{
			get
			{
				if (this.listType == JunkEmailCollection.ListType.TrustedList)
				{
					return this.MaxNumberOfTrustedEntries;
				}
				return this.MaxNumberOfBlockedEntries;
			}
		}

		// Token: 0x17001D20 RID: 7456
		// (get) Token: 0x06006B10 RID: 27408 RVA: 0x001C86E1 File Offset: 0x001C68E1
		public static int MaxEntrySize
		{
			get
			{
				return 512;
			}
		}

		// Token: 0x06006B11 RID: 27409 RVA: 0x001C86E8 File Offset: 0x001C68E8
		protected override void InsertItem(int index, string newItem)
		{
			this.Validate(newItem);
			base.InsertItem(index, newItem);
		}

		// Token: 0x06006B12 RID: 27410 RVA: 0x001C86F9 File Offset: 0x001C68F9
		protected override void SetItem(int index, string newItem)
		{
			this.Validate(newItem);
			base.SetItem(index, newItem);
		}

		// Token: 0x17001D21 RID: 7457
		// (get) Token: 0x06006B13 RID: 27411 RVA: 0x001C870A File Offset: 0x001C690A
		// (set) Token: 0x06006B14 RID: 27412 RVA: 0x001C8712 File Offset: 0x001C6912
		internal bool Validating
		{
			get
			{
				return this.validating;
			}
			set
			{
				this.validating = value;
			}
		}

		// Token: 0x06006B15 RID: 27413 RVA: 0x001C871C File Offset: 0x001C691C
		private void Validate(string value)
		{
			if (this.validating)
			{
				JunkEmailCollection.ValidationProblem validationProblem = this.CheckValue(value);
				if (validationProblem != JunkEmailCollection.ValidationProblem.NoError)
				{
					throw new JunkEmailValidationException(value, validationProblem);
				}
			}
		}

		// Token: 0x06006B16 RID: 27414 RVA: 0x001C8744 File Offset: 0x001C6944
		private JunkEmailCollection.ValidationProblem CheckValue(string value)
		{
			if (value == null)
			{
				return JunkEmailCollection.ValidationProblem.Null;
			}
			if (value.Length == 0)
			{
				return JunkEmailCollection.ValidationProblem.Empty;
			}
			if (value.Length > JunkEmailCollection.MaxEntrySize)
			{
				return JunkEmailCollection.ValidationProblem.TooBig;
			}
			if (base.Count >= this.MaxNumberOfEntries)
			{
				return JunkEmailCollection.ValidationProblem.TooManyEntries;
			}
			if (!JunkEmailCollection.IsValidFormat(value))
			{
				return JunkEmailCollection.ValidationProblem.FormatError;
			}
			foreach (string value2 in this)
			{
				if (value.Equals(value2, StringComparison.OrdinalIgnoreCase))
				{
					return JunkEmailCollection.ValidationProblem.Duplicate;
				}
			}
			if (this.invlidEntriesCollection != null && this.invlidEntriesCollection.Contains(value))
			{
				return JunkEmailCollection.ValidationProblem.EntryInInvalidEntriesList;
			}
			return JunkEmailCollection.ValidationProblem.NoError;
		}

		// Token: 0x17001D22 RID: 7458
		// (get) Token: 0x06006B17 RID: 27415 RVA: 0x001C87E8 File Offset: 0x001C69E8
		private int MaxNumberOfTrustedEntries
		{
			get
			{
				return this.junkRule.MaxNumberOfTrustedEntries;
			}
		}

		// Token: 0x17001D23 RID: 7459
		// (get) Token: 0x06006B18 RID: 27416 RVA: 0x001C87F5 File Offset: 0x001C69F5
		private int MaxNumberOfBlockedEntries
		{
			get
			{
				return this.junkRule.MaxNumberOfBlockedEntries;
			}
		}

		// Token: 0x04003D08 RID: 15624
		private const int MaximumEntrySize = 512;

		// Token: 0x04003D09 RID: 15625
		private bool validating = true;

		// Token: 0x04003D0A RID: 15626
		private JunkEmailCollection.ListType listType;

		// Token: 0x04003D0B RID: 15627
		private JunkEmailRule junkRule;

		// Token: 0x04003D0C RID: 15628
		private ICollection<string> invlidEntriesCollection;

		// Token: 0x02000BB4 RID: 2996
		internal enum ListType
		{
			// Token: 0x04003D0E RID: 15630
			TrustedList = 1,
			// Token: 0x04003D0F RID: 15631
			BlockedList
		}

		// Token: 0x02000BB5 RID: 2997
		internal enum ValidationProblem
		{
			// Token: 0x04003D11 RID: 15633
			NoError,
			// Token: 0x04003D12 RID: 15634
			Null,
			// Token: 0x04003D13 RID: 15635
			Duplicate,
			// Token: 0x04003D14 RID: 15636
			FormatError,
			// Token: 0x04003D15 RID: 15637
			Empty,
			// Token: 0x04003D16 RID: 15638
			TooBig,
			// Token: 0x04003D17 RID: 15639
			TooManyEntries,
			// Token: 0x04003D18 RID: 15640
			EntryInInvalidEntriesList
		}

		// Token: 0x02000BB6 RID: 2998
		// (Invoke) Token: 0x06006B1B RID: 27419
		private delegate void SortDelegate();
	}
}
