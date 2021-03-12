using System;
using System.Text;
using Microsoft.Exchange.PopImap.Core;

namespace Microsoft.Exchange.Pop3
{
	// Token: 0x02000016 RID: 22
	internal sealed class Pop3RequestUidl : Pop3RequestWithIntParameters
	{
		// Token: 0x06000083 RID: 131 RVA: 0x00003D77 File Offset: 0x00001F77
		public Pop3RequestUidl(Pop3ResponseFactory factory, string input) : base(factory, input, Pop3RequestWithIntParameters.ArgumentsTypes.one_optional)
		{
			this.PerfCounterTotal = base.Pop3CountersInstance.PerfCounter_UIDL_Total;
			this.PerfCounterFailures = base.Pop3CountersInstance.PerfCounter_UIDL_Failures;
			base.AllowedStates = Pop3State.Authenticated;
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00003DAB File Offset: 0x00001FAB
		public override bool NeedsStoreConnection
		{
			get
			{
				return base.Factory.NeedToLoadLegacyUIDs;
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003DB8 File Offset: 0x00001FB8
		public override ProtocolResponse ProcessAllMessages()
		{
			if (base.Factory.NeedToLoadLegacyUIDs)
			{
				base.Factory.LoadLegacyUids();
			}
			StringBuilder stringBuilder = new StringBuilder(base.Factory.Messages.Count * 16);
			stringBuilder.Append("\r\n");
			for (int i = 0; i < base.Factory.Messages.Count; i++)
			{
				if (!base.Factory.Messages[i].IsDeleted)
				{
					stringBuilder.Append(string.Format("{0} {1}\r\n", base.Factory.Messages[i].Index, base.Factory.Messages[i].LegacyUid));
					if (base.Session.LightLogSession != null)
					{
						if (base.Session.LightLogSession.RowsProcessed != null)
						{
							base.Session.LightLogSession.RowsProcessed++;
						}
						else
						{
							base.Session.LightLogSession.RowsProcessed = new int?(1);
						}
					}
				}
			}
			stringBuilder.Append(".");
			return new Pop3Response(Pop3Response.Type.ok, stringBuilder.ToString());
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003F11 File Offset: 0x00002111
		public override ProtocolResponse ProcessMessage(Pop3Message message)
		{
			if (message.NeedsLegacyUid)
			{
				base.Factory.LoadLegacyUid(message);
			}
			return new Pop3Response(Pop3Response.Type.ok, string.Format("{0} {1}\r\n", message.Index, message.LegacyUid));
		}
	}
}
