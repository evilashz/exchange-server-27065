using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000231 RID: 561
	internal class UserToCallsMap
	{
		// Token: 0x0600104C RID: 4172 RVA: 0x00048A50 File Offset: 0x00046C50
		internal int GetPhoneCallCount(string smtpAddress)
		{
			IList<Guid> list = null;
			lock (this.map)
			{
				if (!this.map.TryGetValue(smtpAddress, out list))
				{
					return 0;
				}
			}
			return list.Count;
		}

		// Token: 0x0600104D RID: 4173 RVA: 0x00048AA8 File Offset: 0x00046CA8
		internal void AddPhoneCall(string smtpAddress, Guid call)
		{
			lock (this.map)
			{
				IList<Guid> list = null;
				if (!this.map.TryGetValue(smtpAddress, out list))
				{
					list = new List<Guid>();
					this.map.Add(smtpAddress, list);
				}
				list.Add(call);
			}
		}

		// Token: 0x0600104E RID: 4174 RVA: 0x00048B10 File Offset: 0x00046D10
		internal void RemovePhoneCall(string smtpAddress, Guid call)
		{
			if (smtpAddress == null)
			{
				return;
			}
			lock (this.map)
			{
				IList<Guid> list = null;
				if (this.map.TryGetValue(smtpAddress, out list))
				{
					if (list.Count == 1)
					{
						this.map.Remove(smtpAddress);
					}
					else
					{
						list.Remove(call);
					}
				}
			}
		}

		// Token: 0x04000B8E RID: 2958
		private Dictionary<string, IList<Guid>> map = new Dictionary<string, IList<Guid>>();
	}
}
