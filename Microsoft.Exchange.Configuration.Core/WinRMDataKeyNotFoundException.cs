using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Configuration.Core.LocStrings;

namespace Microsoft.Exchange.Configuration.Core
{
	// Token: 0x02000032 RID: 50
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class WinRMDataKeyNotFoundException : WinRMDataExchangeException
	{
		// Token: 0x06000118 RID: 280 RVA: 0x000074C0 File Offset: 0x000056C0
		public WinRMDataKeyNotFoundException(string identity, string key) : base(Strings.WinRMDataKeyNotFound(identity, key))
		{
			this.identity = identity;
			this.key = key;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x000074DD File Offset: 0x000056DD
		public WinRMDataKeyNotFoundException(string identity, string key, Exception innerException) : base(Strings.WinRMDataKeyNotFound(identity, key), innerException)
		{
			this.identity = identity;
			this.key = key;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x000074FC File Offset: 0x000056FC
		protected WinRMDataKeyNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.identity = (string)info.GetValue("identity", typeof(string));
			this.key = (string)info.GetValue("key", typeof(string));
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00007551 File Offset: 0x00005751
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("identity", this.identity);
			info.AddValue("key", this.key);
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600011C RID: 284 RVA: 0x0000757D File Offset: 0x0000577D
		public string Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00007585 File Offset: 0x00005785
		public string Key
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x040000C7 RID: 199
		private readonly string identity;

		// Token: 0x040000C8 RID: 200
		private readonly string key;
	}
}
