using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x020000BA RID: 186
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DxStoreKeyNotFoundException : LocalizedException
	{
		// Token: 0x060006D4 RID: 1748 RVA: 0x0001B175 File Offset: 0x00019375
		public DxStoreKeyNotFoundException(string keyName) : base(Strings.DxStoreKeyNotFoundException(keyName))
		{
			this.keyName = keyName;
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x0001B18A File Offset: 0x0001938A
		public DxStoreKeyNotFoundException(string keyName, Exception innerException) : base(Strings.DxStoreKeyNotFoundException(keyName), innerException)
		{
			this.keyName = keyName;
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x0001B1A0 File Offset: 0x000193A0
		protected DxStoreKeyNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.keyName = (string)info.GetValue("keyName", typeof(string));
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x0001B1CA File Offset: 0x000193CA
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("keyName", this.keyName);
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x060006D8 RID: 1752 RVA: 0x0001B1E5 File Offset: 0x000193E5
		public string KeyName
		{
			get
			{
				return this.keyName;
			}
		}

		// Token: 0x0400070C RID: 1804
		private readonly string keyName;
	}
}
