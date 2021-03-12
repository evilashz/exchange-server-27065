using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Exchange.EDiscovery.MailboxSearch.WebService.Model
{
	// Token: 0x02000041 RID: 65
	internal class SearchException : Exception
	{
		// Token: 0x060002F8 RID: 760 RVA: 0x0001453D File Offset: 0x0001273D
		public SearchException()
		{
			this.Parameters = new List<object>();
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x00014550 File Offset: 0x00012750
		public SearchException(Exception exception) : base(null, exception)
		{
			SearchException ex = exception as SearchException;
			if (ex != null)
			{
				this.Error = ex.Error;
				this.Parameters = ex.Parameters;
				return;
			}
			this.Error = KnownError.NA;
			this.Parameters = new List<object>();
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0001459A File Offset: 0x0001279A
		public SearchException(KnownError error)
		{
			this.Error = error;
			this.Parameters = new List<object>();
		}

		// Token: 0x060002FB RID: 763 RVA: 0x000145B4 File Offset: 0x000127B4
		public SearchException(KnownError error, params object[] parameters)
		{
			this.Error = error;
			this.Parameters = new List<object>();
			this.Parameters.AddRange(parameters);
		}

		// Token: 0x060002FC RID: 764 RVA: 0x000145DA File Offset: 0x000127DA
		public SearchException(KnownError error, Exception innerException) : base(error.ToString(), innerException)
		{
			this.Error = error;
			this.Parameters = new List<object>();
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060002FD RID: 765 RVA: 0x00014600 File Offset: 0x00012800
		// (set) Token: 0x060002FE RID: 766 RVA: 0x00014608 File Offset: 0x00012808
		public SearchSource ErrorSource { get; set; }

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060002FF RID: 767 RVA: 0x00014611 File Offset: 0x00012811
		// (set) Token: 0x06000300 RID: 768 RVA: 0x00014619 File Offset: 0x00012819
		public KnownError Error { get; set; }

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000301 RID: 769 RVA: 0x00014622 File Offset: 0x00012822
		// (set) Token: 0x06000302 RID: 770 RVA: 0x0001462A File Offset: 0x0001282A
		public List<object> Parameters { get; set; }

		// Token: 0x06000303 RID: 771 RVA: 0x00014634 File Offset: 0x00012834
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("Error: {0}\r\n", this.Error);
			if (this.ErrorSource != null)
			{
				stringBuilder.AppendFormat("SourceId: {0}\r\n", this.ErrorSource.ReferenceId);
				stringBuilder.AppendFormat("SourceType: {0}\r\n", this.ErrorSource.SourceType);
				stringBuilder.AppendFormat("SourceLocation: {0}\r\n", this.ErrorSource.SourceLocation);
				stringBuilder.AppendFormat("HasRecipient: {0}\r\n", this.ErrorSource.Recipient != null);
				stringBuilder.AppendFormat("HasMailbox: {0}\r\n", this.ErrorSource.MailboxInfo != null);
				stringBuilder.AppendFormat("HasExtendedAttributes: {0}\r\n", this.ErrorSource.ExtendedAttributes != null && this.ErrorSource.ExtendedAttributes.Count > 0);
			}
			if (this.Parameters != null)
			{
				for (int i = 0; i < this.Parameters.Count; i++)
				{
					stringBuilder.AppendFormat("Parameter{0}: {1}\r\n", i, this.Parameters[i]);
				}
			}
			stringBuilder.AppendLine(base.ToString());
			return stringBuilder.ToString();
		}
	}
}
