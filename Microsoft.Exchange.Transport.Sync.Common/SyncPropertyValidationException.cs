using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Transport.Sync.Common.Exceptions;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x0200000A RID: 10
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class SyncPropertyValidationException : SyncPermanentException
	{
		// Token: 0x060000DF RID: 223 RVA: 0x00004999 File Offset: 0x00002B99
		public SyncPropertyValidationException(string property, string value) : base(Strings.SyncPropertyValidationException(property, value))
		{
			this.property = property;
			this.value = value;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x000049B6 File Offset: 0x00002BB6
		public SyncPropertyValidationException(string property, string value, Exception innerException) : base(Strings.SyncPropertyValidationException(property, value), innerException)
		{
			this.property = property;
			this.value = value;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000049D4 File Offset: 0x00002BD4
		protected SyncPropertyValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.property = (string)info.GetValue("property", typeof(string));
			this.value = (string)info.GetValue("value", typeof(string));
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00004A29 File Offset: 0x00002C29
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("property", this.property);
			info.AddValue("value", this.value);
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x00004A55 File Offset: 0x00002C55
		public string Property
		{
			get
			{
				return this.property;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00004A5D File Offset: 0x00002C5D
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x040000CD RID: 205
		private readonly string property;

		// Token: 0x040000CE RID: 206
		private readonly string value;
	}
}
