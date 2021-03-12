using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.WorkingSet.SignalApi;

namespace Microsoft.Exchange.Data.Storage.WorkingSet.SignalApiEx
{
	// Token: 0x02000EE6 RID: 3814
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SignalFeederEx : AbstractSignalFeeder
	{
		// Token: 0x06008394 RID: 33684 RVA: 0x0023BC01 File Offset: 0x00239E01
		public SignalFeederEx(StoreSession session, StoreId folderId)
		{
			this.session = session;
			this.folderId = folderId;
		}

		// Token: 0x06008395 RID: 33685 RVA: 0x0023BC18 File Offset: 0x00239E18
		internal override void SendMail(Signal signal, List<string> recipients)
		{
			using (MessageItem messageItem = MessageItem.Create(this.session, this.folderId))
			{
				messageItem.ClassName = "IPM.WorkingSet.Signal";
				foreach (string text in recipients)
				{
					Participant participant = new Participant(text, text, "EX");
					messageItem.Recipients.Add(participant, RecipientItemType.To);
				}
				Packer.Pack(signal, messageItem);
				messageItem.SendWithoutSavingMessage();
			}
		}

		// Token: 0x040057FF RID: 22527
		private readonly StoreSession session;

		// Token: 0x04005800 RID: 22528
		private readonly StoreId folderId;
	}
}
