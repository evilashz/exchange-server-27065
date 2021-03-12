using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000F0A RID: 3850
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class EndPointDiscoveryInfo
	{
		// Token: 0x06008498 RID: 33944 RVA: 0x002432B8 File Offset: 0x002414B8
		internal EndPointDiscoveryInfo()
		{
			this.messages = new List<string>();
			this.Status = EndPointDiscoveryInfo.DiscoveryStatus.Success;
		}

		// Token: 0x1700232A RID: 9002
		// (get) Token: 0x06008499 RID: 33945 RVA: 0x002432D2 File Offset: 0x002414D2
		// (set) Token: 0x0600849A RID: 33946 RVA: 0x002432DA File Offset: 0x002414DA
		public EndPointDiscoveryInfo.DiscoveryStatus Status { get; private set; }

		// Token: 0x1700232B RID: 9003
		// (get) Token: 0x0600849B RID: 33947 RVA: 0x002432E3 File Offset: 0x002414E3
		public string Message
		{
			get
			{
				return string.Join(" ", this.messages);
			}
		}

		// Token: 0x0600849C RID: 33948 RVA: 0x002432F5 File Offset: 0x002414F5
		internal void AddInfo(EndPointDiscoveryInfo.DiscoveryStatus status, string message)
		{
			this.Status = status;
			this.messages.Add(message);
		}

		// Token: 0x040058CA RID: 22730
		private List<string> messages;

		// Token: 0x02000F0B RID: 3851
		public enum DiscoveryStatus
		{
			// Token: 0x040058CD RID: 22733
			Success,
			// Token: 0x040058CE RID: 22734
			Error,
			// Token: 0x040058CF RID: 22735
			IocNotFound,
			// Token: 0x040058D0 RID: 22736
			IocNoUri,
			// Token: 0x040058D1 RID: 22737
			IocException,
			// Token: 0x040058D2 RID: 22738
			OrNotFound,
			// Token: 0x040058D3 RID: 22739
			OrNoUri
		}
	}
}
