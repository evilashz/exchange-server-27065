using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020001E5 RID: 485
	[Serializable]
	public class WindowsLiveId
	{
		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x060010BA RID: 4282 RVA: 0x0003316B File Offset: 0x0003136B
		// (set) Token: 0x060010BB RID: 4283 RVA: 0x00033173 File Offset: 0x00031373
		public NetID NetId
		{
			get
			{
				return this.netId;
			}
			set
			{
				this.netId = value;
			}
		}

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x060010BC RID: 4284 RVA: 0x0003317C File Offset: 0x0003137C
		// (set) Token: 0x060010BD RID: 4285 RVA: 0x00033184 File Offset: 0x00031384
		public SmtpAddress SmtpAddress
		{
			get
			{
				return this.smtpAddress;
			}
			set
			{
				this.smtpAddress = value;
			}
		}

		// Token: 0x060010BE RID: 4286 RVA: 0x00033190 File Offset: 0x00031390
		public WindowsLiveId(string id)
		{
			if (NetID.TryParse(id, out this.netId))
			{
				return;
			}
			if (SmtpAddress.IsValidSmtpAddress(id))
			{
				this.SmtpAddress = new SmtpAddress(id);
				return;
			}
			throw new FormatException(DataStrings.ErrorIncorrectWindowsLiveIdFormat(id));
		}

		// Token: 0x060010BF RID: 4287 RVA: 0x000331E2 File Offset: 0x000313E2
		public WindowsLiveId(NetID netId, SmtpAddress smtpAddress)
		{
			this.netId = netId;
			this.smtpAddress = smtpAddress;
		}

		// Token: 0x060010C0 RID: 4288 RVA: 0x00033203 File Offset: 0x00031403
		public static WindowsLiveId Parse(string id)
		{
			return new WindowsLiveId(id);
		}

		// Token: 0x060010C1 RID: 4289 RVA: 0x0003320C File Offset: 0x0003140C
		public static bool TryParse(string id, out WindowsLiveId liveId)
		{
			bool result;
			try
			{
				liveId = WindowsLiveId.Parse(id);
				result = true;
			}
			catch (FormatException)
			{
				liveId = null;
				result = false;
			}
			return result;
		}

		// Token: 0x060010C2 RID: 4290 RVA: 0x00033240 File Offset: 0x00031440
		public override string ToString()
		{
			if (this.NetId != null)
			{
				return this.NetId.ToString();
			}
			if (this.SmtpAddress != SmtpAddress.Empty)
			{
				return this.SmtpAddress.ToString();
			}
			return string.Empty;
		}

		// Token: 0x04000A6F RID: 2671
		private NetID netId;

		// Token: 0x04000A70 RID: 2672
		private SmtpAddress smtpAddress = SmtpAddress.Empty;
	}
}
