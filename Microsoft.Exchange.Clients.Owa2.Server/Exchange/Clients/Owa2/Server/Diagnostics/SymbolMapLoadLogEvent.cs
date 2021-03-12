using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Clients.Owa2.Server.Diagnostics
{
	// Token: 0x02000463 RID: 1123
	internal sealed class SymbolMapLoadLogEvent : ILogEvent
	{
		// Token: 0x0600259E RID: 9630 RVA: 0x00088619 File Offset: 0x00086819
		private SymbolMapLoadLogEvent()
		{
		}

		// Token: 0x170009ED RID: 2541
		// (get) Token: 0x0600259F RID: 9631 RVA: 0x00088621 File Offset: 0x00086821
		public string EventId
		{
			get
			{
				return "ClientWatsonSymbolsMapLoad";
			}
		}

		// Token: 0x060025A0 RID: 9632 RVA: 0x00088628 File Offset: 0x00086828
		public static SymbolMapLoadLogEvent CreateForError(Exception e)
		{
			return new SymbolMapLoadLogEvent
			{
				exception = e
			};
		}

		// Token: 0x060025A1 RID: 9633 RVA: 0x00088648 File Offset: 0x00086848
		public static SymbolMapLoadLogEvent CreateForError(string filePath, Exception e, TimeSpan elapsedTime)
		{
			return new SymbolMapLoadLogEvent
			{
				fileName = Path.GetFileNameWithoutExtension(filePath),
				exception = e,
				elapsedTimeInMilliseconds = elapsedTime.TotalMilliseconds
			};
		}

		// Token: 0x060025A2 RID: 9634 RVA: 0x00088680 File Offset: 0x00086880
		public static SymbolMapLoadLogEvent CreateForSuccess(string filePath, TimeSpan elapsedTime)
		{
			return new SymbolMapLoadLogEvent
			{
				fileName = Path.GetFileNameWithoutExtension(filePath),
				elapsedTimeInMilliseconds = elapsedTime.TotalMilliseconds
			};
		}

		// Token: 0x060025A3 RID: 9635 RVA: 0x000886B0 File Offset: 0x000868B0
		public ICollection<KeyValuePair<string, object>> GetEventData()
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>
			{
				{
					"IS",
					(this.exception == null) ? 1 : 0
				},
				{
					"N",
					this.fileName
				},
				{
					"LT",
					this.elapsedTimeInMilliseconds
				}
			};
			if (this.exception != null)
			{
				dictionary.Add("ET", this.exception.GetType().Name);
				dictionary.Add("EM", this.exception.Message);
			}
			return dictionary;
		}

		// Token: 0x040015E2 RID: 5602
		private const string LogEventId = "ClientWatsonSymbolsMapLoad";

		// Token: 0x040015E3 RID: 5603
		private const string IsSuccessfulKey = "IS";

		// Token: 0x040015E4 RID: 5604
		private const string FileNameKey = "N";

		// Token: 0x040015E5 RID: 5605
		private const string ExceptionTypeKey = "ET";

		// Token: 0x040015E6 RID: 5606
		private const string ExceptionMessageKey = "EM";

		// Token: 0x040015E7 RID: 5607
		private const string LoadTimeKey = "LT";

		// Token: 0x040015E8 RID: 5608
		private Exception exception;

		// Token: 0x040015E9 RID: 5609
		private string fileName;

		// Token: 0x040015EA RID: 5610
		private double elapsedTimeInMilliseconds;
	}
}
