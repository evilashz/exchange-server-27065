using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200007B RID: 123
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IMessageItem : IToDoItem, IItem, IStoreObject, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x1700019C RID: 412
		// (get) Token: 0x0600089A RID: 2202
		// (set) Token: 0x0600089B RID: 2203
		bool IsDraft { get; set; }

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x0600089C RID: 2204
		// (set) Token: 0x0600089D RID: 2205
		bool IsRead { get; set; }

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x0600089E RID: 2206
		// (set) Token: 0x0600089F RID: 2207
		string InReplyTo { get; set; }

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x060008A0 RID: 2208
		IList<Participant> ReplyTo { get; }

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x060008A1 RID: 2209
		// (set) Token: 0x060008A2 RID: 2210
		bool IsGroupEscalationMessage { get; set; }

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x060008A3 RID: 2211
		RecipientCollection Recipients { get; }

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x060008A4 RID: 2212
		// (set) Token: 0x060008A5 RID: 2213
		Participant Sender { get; set; }

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x060008A6 RID: 2214
		// (set) Token: 0x060008A7 RID: 2215
		Participant From { get; set; }

		// Token: 0x060008A8 RID: 2216
		void SendWithoutSavingMessage();

		// Token: 0x060008A9 RID: 2217
		void SuppressAllAutoResponses();

		// Token: 0x060008AA RID: 2218
		void MarkRecipientAsSubmitted(IEnumerable<Participant> submittedParticipants);

		// Token: 0x060008AB RID: 2219
		void StampMessageBodyTag();

		// Token: 0x060008AC RID: 2220
		void CommitReplyTo();
	}
}
