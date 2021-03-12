using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000DA8 RID: 3496
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class PerformInvitationResults
	{
		// Token: 0x17002020 RID: 8224
		// (get) Token: 0x06007817 RID: 30743 RVA: 0x0021206C File Offset: 0x0021026C
		internal PerformInvitationResultType Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x17002021 RID: 8225
		// (get) Token: 0x06007818 RID: 30744 RVA: 0x00212094 File Offset: 0x00210294
		internal ValidRecipient[] SucceededRecipients
		{
			get
			{
				if (this.succeededRecipients == null)
				{
					switch (this.Result)
					{
					case PerformInvitationResultType.Success:
						this.succeededRecipients = this.allRecipients;
						goto IL_AB;
					case PerformInvitationResultType.PartiallySuccess:
					{
						HashSet<string> failedRecipientSet = new HashSet<string>();
						foreach (InvalidRecipient invalidRecipient in this.FailedRecipients)
						{
							failedRecipientSet.Add(invalidRecipient.SmtpAddress);
						}
						this.succeededRecipients = Array.FindAll<ValidRecipient>(this.allRecipients, (ValidRecipient recipient) => !failedRecipientSet.Contains(recipient.SmtpAddress));
						goto IL_AB;
					}
					case PerformInvitationResultType.Failed:
						this.succeededRecipients = ValidRecipient.EmptyRecipients;
						goto IL_AB;
					}
					throw new InvalidOperationException();
				}
				IL_AB:
				return this.succeededRecipients;
			}
		}

		// Token: 0x17002022 RID: 8226
		// (get) Token: 0x06007819 RID: 30745 RVA: 0x00212152 File Offset: 0x00210352
		internal InvalidRecipient[] FailedRecipients
		{
			get
			{
				if (this.Result == PerformInvitationResultType.Ignored)
				{
					throw new InvalidOperationException();
				}
				if (this.exception != null)
				{
					return this.exception.InvalidRecipients;
				}
				return null;
			}
		}

		// Token: 0x0600781A RID: 30746 RVA: 0x00212178 File Offset: 0x00210378
		private PerformInvitationResults()
		{
			this.result = PerformInvitationResultType.Ignored;
		}

		// Token: 0x0600781B RID: 30747 RVA: 0x00212187 File Offset: 0x00210387
		internal PerformInvitationResults(ValidRecipient[] allRecipients)
		{
			Util.ThrowOnNullArgument(allRecipients, "allRecipients");
			if (allRecipients.Length == 0)
			{
				throw new ArgumentException("allRecipients");
			}
			this.result = PerformInvitationResultType.Success;
			this.allRecipients = allRecipients;
		}

		// Token: 0x0600781C RID: 30748 RVA: 0x002121B8 File Offset: 0x002103B8
		internal PerformInvitationResults(InvalidSharingRecipientsException exception)
		{
			Util.ThrowOnNullArgument(exception, "exception");
			this.result = PerformInvitationResultType.Failed;
			this.exception = exception;
		}

		// Token: 0x0600781D RID: 30749 RVA: 0x002121DC File Offset: 0x002103DC
		internal PerformInvitationResults(ValidRecipient[] allRecipients, InvalidSharingRecipientsException exception)
		{
			Util.ThrowOnNullArgument(exception, "exception");
			Util.ThrowOnNullArgument(allRecipients, "allRecipients");
			if (allRecipients.Length == 0)
			{
				throw new ArgumentException("allRecipients");
			}
			this.result = PerformInvitationResultType.PartiallySuccess;
			this.exception = exception;
			this.allRecipients = allRecipients;
		}

		// Token: 0x0600781E RID: 30750 RVA: 0x0021222C File Offset: 0x0021042C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("Result: " + this.Result.ToString());
			if (this.Result == PerformInvitationResultType.Success || this.result == PerformInvitationResultType.PartiallySuccess)
			{
				stringBuilder.AppendLine(", SucceededRecipients:");
				foreach (ValidRecipient validRecipient in this.SucceededRecipients)
				{
					stringBuilder.AppendLine(validRecipient.ToString() + ";");
				}
			}
			if (this.Result == PerformInvitationResultType.PartiallySuccess || this.Result == PerformInvitationResultType.Failed)
			{
				stringBuilder.AppendLine(", FailedRecipients:");
				foreach (InvalidRecipient invalidRecipient in this.FailedRecipients)
				{
					stringBuilder.AppendLine(invalidRecipient.ToString() + ";");
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0400531F RID: 21279
		private readonly PerformInvitationResultType result;

		// Token: 0x04005320 RID: 21280
		private readonly ValidRecipient[] allRecipients;

		// Token: 0x04005321 RID: 21281
		private readonly InvalidSharingRecipientsException exception;

		// Token: 0x04005322 RID: 21282
		private ValidRecipient[] succeededRecipients;

		// Token: 0x04005323 RID: 21283
		internal static PerformInvitationResults Ignored = new PerformInvitationResults();
	}
}
