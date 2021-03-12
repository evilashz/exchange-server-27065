using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x02000013 RID: 19
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class SubscriptionNameAlreadyExistsException : LocalizedException
	{
		// Token: 0x0600010D RID: 269 RVA: 0x00004E35 File Offset: 0x00003035
		public SubscriptionNameAlreadyExistsException(string name) : base(Strings.SubscriptionNameAlreadyExists(name))
		{
			this.name = name;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00004E4A File Offset: 0x0000304A
		public SubscriptionNameAlreadyExistsException(string name, Exception innerException) : base(Strings.SubscriptionNameAlreadyExists(name), innerException)
		{
			this.name = name;
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00004E60 File Offset: 0x00003060
		protected SubscriptionNameAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00004E8A File Offset: 0x0000308A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00004EA5 File Offset: 0x000030A5
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x040000D7 RID: 215
		private readonly string name;
	}
}
