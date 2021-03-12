using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000D96 RID: 3478
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ImportCalendarResults
	{
		// Token: 0x17001FF8 RID: 8184
		// (get) Token: 0x060077AF RID: 30639 RVA: 0x0021091C File Offset: 0x0020EB1C
		internal IList<LocalizedString> RawErrors
		{
			get
			{
				return this.errors;
			}
		}

		// Token: 0x17001FF9 RID: 8185
		// (get) Token: 0x060077B0 RID: 30640 RVA: 0x00210924 File Offset: 0x0020EB24
		public LocalizedString[] Errors
		{
			get
			{
				return this.errors.ToArray();
			}
		}

		// Token: 0x17001FFA RID: 8186
		// (get) Token: 0x060077B1 RID: 30641 RVA: 0x00210931 File Offset: 0x0020EB31
		public int CountOfImportedItems
		{
			get
			{
				return this.countOfImportedItems;
			}
		}

		// Token: 0x17001FFB RID: 8187
		// (get) Token: 0x060077B2 RID: 30642 RVA: 0x00210939 File Offset: 0x0020EB39
		internal int CountOfUnchangedItems
		{
			get
			{
				return this.countOfUnchangedItems;
			}
		}

		// Token: 0x17001FFC RID: 8188
		// (get) Token: 0x060077B3 RID: 30643 RVA: 0x00210941 File Offset: 0x0020EB41
		public bool TimedOut
		{
			get
			{
				return this.timedOut;
			}
		}

		// Token: 0x17001FFD RID: 8189
		// (get) Token: 0x060077B4 RID: 30644 RVA: 0x00210949 File Offset: 0x0020EB49
		public ImportCalendarResultType Result
		{
			get
			{
				if (this.Errors.Length == 0 && !this.TimedOut)
				{
					return ImportCalendarResultType.Success;
				}
				if (this.CountOfImportedItems > 0)
				{
					return ImportCalendarResultType.PartiallySuccess;
				}
				return ImportCalendarResultType.Failed;
			}
		}

		// Token: 0x17001FFE RID: 8190
		// (get) Token: 0x060077B5 RID: 30645 RVA: 0x0021096B File Offset: 0x0020EB6B
		// (set) Token: 0x060077B6 RID: 30646 RVA: 0x0021097C File Offset: 0x0020EB7C
		public string CalendarName
		{
			get
			{
				return this.calendarName ?? string.Empty;
			}
			internal set
			{
				this.calendarName = value;
			}
		}

		// Token: 0x060077B7 RID: 30647 RVA: 0x00210985 File Offset: 0x0020EB85
		internal ImportCalendarResults()
		{
			this.errors = new List<LocalizedString>();
		}

		// Token: 0x060077B8 RID: 30648 RVA: 0x00210998 File Offset: 0x0020EB98
		internal void Reset()
		{
			this.countOfImportedItems = 0;
			this.countOfUnchangedItems = 0;
			this.errors.Clear();
			this.timedOut = false;
		}

		// Token: 0x060077B9 RID: 30649 RVA: 0x002109BA File Offset: 0x0020EBBA
		internal void Increment(bool changed)
		{
			this.countOfImportedItems++;
			if (!changed)
			{
				this.countOfUnchangedItems++;
			}
		}

		// Token: 0x060077BA RID: 30650 RVA: 0x002109DB File Offset: 0x0020EBDB
		public void SetTimeOut()
		{
			this.timedOut = true;
		}

		// Token: 0x060077BB RID: 30651 RVA: 0x002109E4 File Offset: 0x0020EBE4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("Result: " + this.Result.ToString());
			stringBuilder.AppendLine();
			stringBuilder.AppendFormat("--Name of Calendar: {0}", this.CalendarName);
			stringBuilder.AppendLine();
			stringBuilder.AppendFormat("--Count of Imported Items: {0}", this.CountOfImportedItems);
			stringBuilder.AppendLine();
			stringBuilder.AppendFormat("--Count of Unchanged Items: {0}", this.CountOfUnchangedItems);
			stringBuilder.AppendLine();
			stringBuilder.AppendFormat("--Count of Errors: {0}", this.Errors.Length);
			if (this.Errors.Length > 0)
			{
				foreach (LocalizedString localizedString in this.Errors)
				{
					stringBuilder.AppendLine();
					stringBuilder.AppendFormat("----Error: {0}", localizedString);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040052D2 RID: 21202
		private readonly List<LocalizedString> errors;

		// Token: 0x040052D3 RID: 21203
		private int countOfImportedItems;

		// Token: 0x040052D4 RID: 21204
		private int countOfUnchangedItems;

		// Token: 0x040052D5 RID: 21205
		private string calendarName;

		// Token: 0x040052D6 RID: 21206
		private bool timedOut;
	}
}
