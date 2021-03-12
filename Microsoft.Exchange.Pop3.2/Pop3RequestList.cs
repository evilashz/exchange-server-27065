using System;
using System.Text;
using Microsoft.Exchange.PopImap.Core;

namespace Microsoft.Exchange.Pop3
{
	// Token: 0x0200000D RID: 13
	internal sealed class Pop3RequestList : Pop3RequestWithIntParameters
	{
		// Token: 0x06000050 RID: 80 RVA: 0x000034B3 File Offset: 0x000016B3
		public Pop3RequestList(Pop3ResponseFactory factory, string input) : base(factory, input, Pop3RequestWithIntParameters.ArgumentsTypes.one_optional)
		{
			this.PerfCounterTotal = base.Pop3CountersInstance.PerfCounter_LIST_Total;
			this.PerfCounterFailures = base.Pop3CountersInstance.PerfCounter_LIST_Failures;
			base.AllowedStates = Pop3State.Authenticated;
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000051 RID: 81 RVA: 0x000034E7 File Offset: 0x000016E7
		public override bool NeedsStoreConnection
		{
			get
			{
				return base.Factory.ExactRFC822SizeEnabled && !base.Factory.HasAllMessagesBeenRetrieved;
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003508 File Offset: 0x00001708
		public override ProtocolResponse ProcessAllMessages()
		{
			StringBuilder stringBuilder = new StringBuilder(base.Factory.Messages.Count * 16);
			int num = 0;
			long num2 = 0L;
			for (int i = 0; i < base.Factory.Messages.Count; i++)
			{
				if (!base.Factory.Messages[i].IsDeleted)
				{
					stringBuilder.Append(string.Format("{0} {1}\r\n", base.Factory.Messages[i].Index, base.Factory.Messages[i].GetSize()));
					num++;
					num2 += base.Factory.Messages[i].GetSize();
				}
			}
			if (base.Session.LightLogSession != null)
			{
				base.Session.LightLogSession.RowsProcessed = new int?(num);
				base.Session.LightLogSession.TotalSize = new long?(num2);
			}
			stringBuilder.Insert(0, string.Format("{0} {1}\r\n", num, num2));
			stringBuilder.Append(".");
			return new Pop3Response(Pop3Response.Type.ok, stringBuilder.ToString());
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003640 File Offset: 0x00001840
		public override ProtocolResponse ProcessMessage(Pop3Message message)
		{
			if (base.Session.LightLogSession != null)
			{
				base.Session.LightLogSession.TotalSize = new long?(message.GetSize());
			}
			return new Pop3Response(Pop3Response.Type.ok, string.Format("{0} {1}\r\n", message.Index, message.GetSize()));
		}
	}
}
